var downloadAppUrl = 'http://userhtml.sw.sowaynet.com/#/download';

layui.use(['jquery', 'rate', 'carousel', 'form'], function () {
    var $ = layui.jquery
        , carousel = layui.carousel
        , layer = layui.layer;
	

    var pageload = function () {

        loadNearbyStore();
        loadAdvert();
    };


    //收藏店铺
    $('.Collection').on("click", function () {
        var $this = $(this);
        if (!$this.hasClass("disabled")) {
            var index = layer.load(2, { time: 60 * 1000 });
            $.ajax({
                url: '/ScanCode/CollectionStore',
                data: {
                    userId: currentUserId,
                    storeId: currentStoreId,
                },
                type: 'post',
                dataType: 'json',
                success: function (res) {
                    if (res.Status == 200) {
                        res = res.Data;
                        if (res.Status == 1) {
                            $this.addClass("disabled");
                            layer.msg("收藏成功");
                        }
                        else {
                            layer.msg(res.ErrorMessage);
                        }
                    }
                    else {
                        layer.msg('请求错误：' + res.ErrorMessage + '，' + res.Status);
                    }
                    layer.close(index);
                },
                error: function () {
                    layer.close(index);
                }
            });
        }
        else {
            layer.msg("已收藏");
        }
    });

    //点击店铺事件
    $('.NearbyStoreList').on('click', '.shop-info-detail .shop-name', function () {
        var dataSource = $(this).attr('data-DataSource');
        var id = $(this).attr('data-id');
        if (dataSource == 1) {
            clickAdvert(id, 1, goDownloadApp);
        }
        else {
            goDownloadApp();
        }
    });
    //点击广告事件
    $('#son').on('click', 'a', function () {
        var id = $(this).attr('data-id');
        clickAdvert(id, 2, goDownloadApp);
    });
    //领取福利点击事件
    $('.welfare button').on('click', function () {
        goDownloadApp();
    });

    /**
     * 加载附近店铺信息
     */
    var loadNearbyStore = function () {
        var index = layer.load(2, { time: 60 * 1000 });
        $.ajax({
            url: '/ScanCode/FindNearbyStoreList',
            data: {
                storeId: currentStoreId,
            },
            type: 'post',
            dataType: 'json',
            success: function (res) {
                if (res.Status == 200) {
                    res = res.Data;
                    if (res.Status == 1) {
                        setNearbyStore(res.BusinessData);
                    }
                    else {
                        layer.msg(res.ErrorMessage);
                    }
                }
                else {
                    layer.msg('请求错误：' + res.ErrorMessage + '，' + res.Status);
                }
                layer.close(index);
            },
            error: function () {
                layer.close(index);
            }
        });
    };

    /**
     * 加载广告列表
     */
    var loadAdvert = function () {
        var index = layer.load(2, { time: 60 * 1000 });
        $.ajax({
            url: '/ScanCode/FindAdvertList',
            type: 'post',
            dataType: 'json',
            success: function (res) {
                if (res.Status == 200) {
                    res = res.Data;
                    if (res.Status == 1) {
                        setAdvert(res.BusinessData);
                    }
                    else {
                        layer.msg(res.ErrorMessage);
                    }
                }
                else {
                    layer.msg('请求错误：' + res.ErrorMessage + '，' + res.Status);
                }
                layer.close(index);
            },
            error: function () {
                layer.close(index);
            }
        });
    };
    /**
     * 设置广告到页面
     * @param {any} data 广告数据
     */
    var setAdvert = function (data) {
        if (data.length > 0) {
            var html = [];
            $(data).each(function () {
                //暂时全部跳转下载APP
                //this.Url;
                html.push('<div><a href="javascript:;" data-id="'+this.Id+'"><img src="' + this.ImagePath + '" alt=" " /></a></div>');
            });
            $('#son').html(html.join('\n'));

            //图片轮播
            carousel.render({
                elem: '#test10',
                width: '100%',
                height: '200px',
                interval: 5000
            });

        }
        else {
            $('#ad-lunbo').hide();
        }
    };
    /**
     * 跳转下载APP页面
     */
    var goDownloadApp = function () {
        window.location.href = downloadAppUrl;
    };
    /**
     * 收集点击量
     * @param {any} id 广告Id
     * @param {any} adType 广告类型 1=广告互投、2=系统广告
     * @param {any} callback 回调
     */
    var clickAdvert = function (id, adType, callback) {
        var index = layer.load(2, { time: 60 * 1000 });
        $.ajax({
            url: '/ScanCode/AddAdvertClicksByAdvertId',
            type: 'post',
            data: {
                advertId: id,
                advertType: adType
            },
            dataType: 'json',
            success: function (res) {
                layer.close(index);
                callback();
            },
            error: function () {
                layer.close(index);
            }
        });
    }
    /**
     * 设置附近店铺到页面
     * @param {any} data 附近店铺数据
     */
    var setNearbyStore = function (data) {
        if (data.length > 0) {
            var html = [];
            $(data).each(function () {
                html.push('<!--每一个商家详情-->');
                html.push('<div class="shop-info-detail ">');
                html.push('    <!--左边显示图片-->');
                html.push('    <div class="shop-left ">');
                html.push('        <img src="' + (this.ShopBannerPic||this.StoreLogo) + '" alt=" " />');
                html.push('    </div>');
                html.push('    <!--中间显示商家详情-->');
                html.push('    <div class="shop-middle ">');
                html.push('        <div class="shop-name " data-id="'+this.Id+'" data-DataSource="' + this.DataSource+'">' + this.StoreName + '</div>');
                html.push('        <!--显示星级评分-->');
                html.push('        <div class="shop-star ">');
                html.push(getStarHtml(this.Score));
                html.push('        </div>');
                html.push('        <!--显示商家类型及地址-->');
                html.push('        <div class="shop-address ">');
                html.push('            <span>' + (this.BrandTypeName||'') + '</span>');
                html.push('            <span>' + this.Area + '</span>');
                html.push('        </div>');
                var flag = false;
                var couponTitle1 = setCouponTitle(this.CouponList, 2);
                if (couponTitle1) {
                    flag = true;
                    html.push('        <!--领券福利-->');
                    html.push('        <div class="fuli ">');
                    html.push('            <div class="public-fm-icon fuli1 ">折扣</div>');
                    html.push('            <div class="public-fm-text ">' + couponTitle1 + '</div>');
                    html.push('        </div>');
                    if (couponTitle1.length <= 16) {
                        flag = false;
                    }
                }
                var couponTitle2 = setCouponTitle(this.CouponList, 1);
                if (couponTitle2) {
                    flag = true;
                    html.push('        <!--满减-->');
                    html.push('        <div class="manjian ">');
                    html.push('            <div class="public-fm-icon manjian1 ">满减</div>');
                    html.push('            <div class="public-fm-text ">' + couponTitle2 + '</div>');
                    html.push('        </div>');
                    if (couponTitle2.length <= 16) {
                        flag = false;
                    }
                }
                html.push('    </div>');
                html.push('    <!--右边显示商家距离-->');
                html.push('    <div class="shop-right ">');
                html.push('        <div class="distance ">' + getDistanceText(this.Distance) + '</div>');
                if (flag) {
                    html.push('        <div class="right-arr ">');
                    html.push('            <i class="layui-icon layui-icon-down fs "></i>');
                    html.push('        </div>');
                }
                html.push('    </div>');
                html.push('</div>');
            });
            $('.NearbyStoreList').append(html.join('\n'));
        }
    }
    /**
     * 获取距离文本
     * @param {any} distance 距离
     */
    var getDistanceText = function (distance) {
        var txt = '';
        if (distance != null && distance != Number.MAX_VALUE) {
            if (distance < 1000) {
                txt = distance + 'm';
            }
            else {
                txt = (distance / 1000).toFixed(2) + 'km';
            }
        }
        return txt;
    }
    /**
     * 获取评分HTML
     * @param {any} score 评分
     */
    var getStarHtml = function (score) {
        var html = [];
        for (var i = 1; i <= 5; i++) {
            if (i <= score) {
                html.push('<span class="star-all"><img src="/Content/static/images/star-all.png " alt=" " /></span>');
            }
            else if (Math.ceil(i) == Math.ceil(score)) {
                html.push('<span class="star-all"><img src="/Content/static/images/star-half.png " alt=" " /></span>');
            }
            else {
                html.push('<span class="star-all"><img src="/Content/static/images/star-none.png " alt=" " /></span>');
            }
        }
        //var txtScore = score % 1 === 0 ? score : score.toFixed(1);//整数或几点几
        var txtScore = score == 0 ? 0 : score.toFixed(1);
        html.push('<span class="scro-num">' + txtScore + '分</span>');
        return html.join('\n');
    };

    /**
     * 设置优惠券标题
     * @param {any} data
     * @param {any} couponType
     */
    var setCouponTitle = function (data, couponType) {
        var title = [];
        data = data.filter(function (n, m) {
            return n.CouponType == couponType;
        });
        $(data).each(function () {
            title.push(this.CouponName);
        });
        return title.join(',');
    };
    
    $(".NearbyStoreList").on('click','.right-arr .layui-icon',function () {
        if ($(this).hasClass("layui-icon-down")) {
            $(this).parents('.shop-info-detail').find(".public-fm-text").addClass("text-open");
            $(this).addClass("layui-icon-up").removeClass("layui-icon-down");
        } else {
            $(this).parents('.shop-info-detail').find(".public-fm-text").removeClass("text-open");
            $(this).removeClass("layui-icon-up").addClass("layui-icon-down");
        }
    });

    pageload();
});