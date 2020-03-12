layui.use(['jquery', 'layer'], function () {
    FastClick.attach(document.body)

    var $ = layui.jquery
        , layer = layui.layer;

    //var md = new MobileDetect(window.navigator.userAgent);
    //var data = {
    //    mobile: md.mobile(),
    //    phone: md.phone(),
    //    tablet: md.tablet(),
    //    userAgent: md.userAgent(),
    //    os: md.os(),
    //    build: md.versionStr('Build'),
    //    mobile: md.mobile(),
    //};


    /**
     * 是否正在支付
     **/
    var isPaying = false;
    var str = '<span class="span"></span>';
    /**
     * #初始化数字键盘事件
     **/
    $(".keyboard").slideDown();
    var $paymoney = $(".money strong");
    // 大写金额
    $(".money strong").focus(function () {
        $(".keyboard").slideDown();
        document.activeElement.blur();
    });
    /**
     * 处理金额
     * @@param thisText 当前输入值
     */
    var handleMoney = function (thisText) {
        var money = $paymoney.text();
        //确定输入值的类型
        if (thisText == '0') {
            //0值
            if ($.trim(money) == '0' || $.trim(money) == '0.0') {
                return;
            }
        }
        else if (thisText == '.') {
            //.值
            if ($.trim(money) == '' || money.indexOf(".") > -1) {
                return;
            }
        }
        else {
            //1-9值
            if ($.trim(money) == '0') {
                return;
            }
            $('.key_confirm').removeClass('current');
        }

        //只支持2位小数
        if (money.indexOf(".") != -1 && money.split('.')[1].length == 2) {
            return;
        }
        if (!checkMaxMoney(money + thisText)) {
            return;
        }

        $paymoney.text(money + thisText);
    };
    /**
     * 检查最大金额
     * @@param money 当前金额
     */
    var checkMaxMoney = function (money) {
        if (money > 5000.00) {
            layer.msg("支付金额不能大于5000");
            return false;
        }
        return true;
    }

    //1-9，0，.点击事件
    $(".key_cell,.key_0,.pay-float").click(function () {
        if (!isPaying) {
            handleMoney($(this).text());
        }
    });
    //清除键
    $(".key_d").click(function () {
        if (!isPaying) {
            var money = $paymoney.text();
            $paymoney.text(money.substring(0, money.length - 1))
            if (!$paymoney.text()) {
                $('.key_confirm').addClass('current')
            }
        }
    })
    //确认支付
    $('.key_confirm').click(function () {
        if (!isPaying) {
            var money = $paymoney.text();
            if (!checkMaxMoney(money)) {
                return;
            }
            if ($.trim(money) == "" || parseFloat(money) < 0.01) {
                $('.key_confirm').addClass('current')
                layer.msg("请输入支付金额");
                return
            }
            //调用支付
            goPay(parseFloat(money));
        }
        return false;
    });

    /**
     * 处理支付
     **/
    var goPay = function (money) {
        //禁用输入
        isPaying = true;
        $('.key_confirm').html(str)
        $('.masking').css({
            'display': 'block'
        });

        //调用生成支付参数接口
        paramData.Money = money;
        $.ajax({
            url: '/ScanCode/SubmitOrder',
            data: paramData,
            type: 'post',
            dataType: 'json',
            success: function (res) {
                if (res.Status == 200) {
                    res = res.Data;
                    if (res.Status == 1) {
                        console.log(res.BusinessData);
                        var payData = res.BusinessData.PayData;
                        orderCode = res.BusinessData.OrderCode;
                        if (browserType == 1) {
                            //微信支付
                            onBridgeReady(JSON.parse(payData));
                        }
                        else if (browserType == 2) {
                            //支付宝支付（表单提交）
                            var cDiv = document.createElement('div');
                            cDiv.innerHTML = payData;
                            document.getElementById('alipayExcute').appendChild(cDiv);
                            document.forms['alipaysubmit'].submit();
                        }
                    }
                    else {
                        resetView();
                        layer.msg(res.ErrorMessage);
                    }
                }
                else {
                    resetView();
                    layer.msg('请求错误：' + res.ErrorMessage + '，' + res.Status);
                }
            }
        });
    };

    /**
     * 重置视图-可重新输入金额
     **/
    var resetView = function () {
        $paymoney.text("");
        isPaying = false;
        $('.key_confirm').html("确认<br>支付")
        $('.masking').css({
            'display': 'none'
        });
    };

    var orderCode = "";
    /**
     *  调起微信支付
     **/
    var onBridgeReady = function (paymentdata) {
        console.log(paymentdata);
        WeixinJSBridge.invoke(
            'getBrandWCPayRequest',
            paymentdata,
            function (res) {
                // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
                if (res.err_msg == 'get_brand_wcpay_request:ok') {
                    /*支付成功回调*/
                    layer.msg("支付成功", function () {
                        window.location.href = "/ScanCode/Result?out_trade_no=" + orderCode;
                    });
                }
                if (res.err_msg == 'get_brand_wcpay_request:cancel') {
                    resetView();
                    layer.msg("支付取消");
                }
                if (res.err_msg == 'get_brand_wcpay_request:fail') {
                    resetView();
                    layer.msg("支付失败，" + res.err_desc);
                }
            }
        )
    }

});


