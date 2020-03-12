using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Util;
using Nest.BaseCore.Payment.PaymentSDK.Alipay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Nest.BaseCore.Payment.Service
{
    ///// <summary>
    ///// 支付宝支付
    ///// </summary>
    //public class AlipayPayment : PaymentOrderBaseService
    //{
    //    private string Subject
    //    {
    //        get
    //        {
    //            string subject = "";
    //            switch (BrowserType)
    //            {
    //                case BrowserType.AppLife:
    //                    subject = "速微生活-在线支付";
    //                    break;
    //                case BrowserType.App:
    //                    subject = "速微开店宝-在线支付";
    //                    break;
    //                default:
    //                    subject = "速微开店宝-在线支付";
    //                    break;
    //            }
    //            return subject;
    //        }
    //    }
    //    /// <summary>
    //    /// 支付宝支付通知地址
    //    /// </summary>
    //    public override string NotifyUrl
    //    {
    //        get
    //        {
    //            string notifyUrl = ConfigHelper.GetAppsettingValue(ConfigConstants.AliPaymentNotifyUrl);
    //            if (notifyUrl.IsNullOrWhiteSpace())
    //            {
    //                return base.NotifyUrl;
    //            }
    //            return notifyUrl;
    //        }
    //    }
    //    public AlipayPayment(BrowserType sourceOption) : base(sourceOption)
    //    {

    //    }
    //    /// <summary>
    //    /// 获取验证地址
    //    /// </summary>
    //    /// <param name="redirectUrl"></param>
    //    /// <param name="scope"></param>
    //    /// <param name="state"></param>
    //    /// <returns></returns>
    //    public override string GetThridOAuthUrl(string redirectUrl, string state = "Alipay", ThridOAuthScope scope = ThridOAuthScope.SnsapiBase)
    //    {
    //        redirectUrl = Utils.UrlEncode(redirectUrl);
    //        string aliScope = scope == ThridOAuthScope.SnsapiBase ? "auth_base" : "auth_user";
    //        string oauthUrl = string.Format("{0}?app_id={1}&scope={2}&redirect_uri={3}&state={4}",
    //            AliPayConfig.AppAuthorizeUrl,
    //            AliPayConfig.AppId,
    //            aliScope,
    //            redirectUrl,
    //            state);

    //        return oauthUrl;
    //    }
    //    /// <summary>
    //    /// 根据Code获取第三方access_token信息
    //    /// </summary>
    //    /// <param name="code"></param>
    //    /// <returns></returns>
    //    public override ThirdOpenAuthorizeViewModel GetThridOAuth(string code)
    //    {
    //        DefaultAopClient client = new DefaultAopClient(AliPayConfig.gatewayUrl, AliPayConfig.AppId, AliPayConfig.privatekey, "json", "1.0", AliPayConfig.sign_type, AliPayConfig.alipublickey, AliPayConfig.charset, false);
    //        AlipaySystemOauthTokenRequest request = new AlipaySystemOauthTokenRequest
    //        {
    //            Code = code,
    //            GrantType = AliPayConfig.granttype
    //        };

    //        AlipaySystemOauthTokenResponse oauthTokenResponse = client.Execute(request);
    //        if (oauthTokenResponse.IsError)
    //        {
    //            throw new Exception(oauthTokenResponse.SubMsg);
    //        }
    //        ThirdOpenAuthorizeViewModel result = new ThirdOpenAuthorizeViewModel
    //        {
    //            AlipayId = oauthTokenResponse.UserId,
    //            Token = oauthTokenResponse.AccessToken,
    //            Expires = int.Parse(oauthTokenResponse.ExpiresIn)
    //        };

    //        return result;
    //    }
    //    /// <summary>
    //    /// 获取阿里支付回调参数字典
    //    /// </summary>
    //    /// <returns></returns>
    //    private IDictionary<string, string> GetAlipayRequestPost(HttpRequestMessage requestMessage)
    //    {
    //        HttpContextBase context = (HttpContextBase)requestMessage.Properties["MS_HttpContext"];//获取传统context  
    //        IDictionary<string, string> sArray = new Dictionary<string, string>();

    //        for (int i = 0; i < context.Request.Form.Count; i++)
    //        {
    //            sArray.Add(context.Request.Form.Keys[i], context.Request.Form[i]);
    //        }

    //        return sArray;
    //    }
    //    /// <summary>
    //    /// 支付结果回调
    //    /// </summary>
    //    /// <param name="requestMessage"></param>
    //    /// <returns></returns>
    //    public override ApiResultModel<string> PayNotify(HttpRequestMessage requestMessage)
    //    {
    //        var res = new ApiResultModel<string>()
    //        {
    //            Status = ResponseStatus.Fail,
    //            BusinessData = "fail"
    //        };

    //        var sPara = GetAlipayRequestPost(requestMessage);
    //        LogHelper.LogInfo("AlipayNotify->AlipayPayment", "支付宝支付回调通知：" + sPara.ToJsonString());
    //        //记录支付宝回调的日志
    //        WritePostThridApi(ThirdPlatformBusinessType.Payment, sPara["out_trade_no"], ThirdPlatformType.Alipay, requestMessage.RequestUri.LocalPath, sPara.ToJsonString(), DateTime.Now, "", DateTime.Now, true);
    //        bool flag = AlipaySignature.RSACheckV1(sPara, AliPayConfig.alipublickey, AliPayConfig.charset, "RSA2", false);
    //        if (flag)
    //        {
    //            string orderCode = sPara["out_trade_no"];
    //            LogHelper.LogInfo("AlipayNotify->AlipayPayment", "1,OrderCode:" + orderCode);
    //            //支付宝交易号
    //            string trade_no = sPara["trade_no"];

    //            //交易状态
    //            string trade_status = sPara["trade_status"];
    //            //获得调用方的appid；
    //            //如果是非授权模式，appid是商户的appid；如果是授权模式（token调用），appid是系统商的appid
    //            string app_id = sPara["app_id"];
    //            if (!app_id.Equals(AliPayConfig.AppId))
    //            {
    //                res.ErrorMessage = "支付宝支付结果通知错误，不是本APPID提交的支付单";
    //                return res;
    //            }
    //            LogHelper.LogInfo("AlipayNotify->AlipayPayment", "2,OrderCode:" + orderCode);
    //            //支付宝回调状态
    //            if (trade_status.IsNullOrWhiteSpace())
    //            {
    //                res.ErrorMessage = "支付宝支付结果通知错误，trade_status 是空值";
    //                return res;
    //            }
    //            LogHelper.LogInfo("AlipayNotify->AlipayPayment", "3,OrderCode:" + orderCode);
    //            var order = GetSmallOrderModel(orderCode);
    //            if (order.IsNull())
    //            {
    //                res.ErrorMessage = "未找到平台订单信息,OrderCode:" + orderCode;
    //                return res;
    //            }
    //            LogHelper.LogInfo("AlipayNotify->AlipayPayment", "4,OrderCode:" + orderCode);

    //            if ((order.Status == OrderStatusType.Confirm || order.Status == OrderStatusType.UnConfirm) && order.PayStatus == PayStatusType.UnPay)
    //            {
    //                LogHelper.LogInfo("AlipayNotify->AlipayPayment", "5,OrderCode:" + orderCode);
    //                switch (trade_status.ToUpper())
    //                {
    //                    case "TRADE_SUCCESS":
    //                        //TRADE_SUCCESS 交易支付成功，可退款 交易支付成功
    //                        res = ChangeOrderPayStatus(orderCode, trade_no, PayStatusType.Payed, thirdAppId: app_id, thirdUserId: sPara["buyer_id"]);
    //                        break;
    //                    case "TRADE_CLOSED":
    //                        //TRADE_CLOSED 未付款交易超时关闭，或支付完成后全额退款 交易关闭    true（触发通知）
    //                        res = ChangeOrderPayStatus(orderCode, trade_no, PayStatusType.TimeOut, thirdAppId: app_id, thirdUserId: sPara["buyer_id"]);
    //                        break;
    //                    case "TRADE_FINISHED":
    //                        //TRADE_FINISHED 交易结束，不可退款 交易完成    true（触发通知）
    //                        res = ChangeOrderPayStatus(orderCode, trade_no, PayStatusType.Payed, thirdAppId: app_id, thirdUserId: sPara["buyer_id"]);
    //                        break;
    //                    default:
    //                        res.ErrorMessage = "支付宝支付结果通知错误，trade_status 是空值";
    //                        return res;

    //                }
    //                if (res.Status == ResponseStatus.Success)
    //                {
    //                    res.BusinessData = "success";
    //                }
    //                LogHelper.LogInfo("AlipayNotify->AlipayPayment", "6,OrderCode:" + orderCode);
    //            }
    //            else
    //            {
    //                res.ErrorMessage = "平台订单信息当前状态不允许支付 OrderCode：" + orderCode;
    //                LogHelper.LogInfo("AlipayNotify->AlipayPayment", "7,OrderCode:" + orderCode);
    //            }
    //            return res;
    //        }
    //        else
    //        {
    //            res.ErrorMessage = "支付宝支付结果：验证失败";
    //            return res;
    //        }
    //    }


    //    /// <summary>
    //    /// 发起订单支付
    //    /// </summary>
    //    /// <param name="payRequest"></param>
    //    /// <returns></returns>
    //    public override ApiResultModel<string> SubmitPay(PayBaseRequest payRequest)
    //    {
    //        if (BrowserType == BrowserType.Alipay)
    //        {
    //            return GetWapAlipay(payRequest.orderCode, payRequest.money, payRequest.PaytimeOut);
    //        }
    //        else
    //        {
    //            return GetAppAliPayApiParam(payRequest.orderCode, payRequest.money, payRequest.PaytimeOut);
    //        }
    //    }
    //    /// <summary>
    //    /// 获取wap支付宝支付订单参数
    //    /// </summary>
    //    /// <param name="orderCode"></param>
    //    /// <param name="money"></param>
    //    /// <param name="timeOut">订单支付超时时间</param>
    //    /// <returns></returns>
    //    private ApiResultModel<string> GetWapAlipay(string orderCode, decimal money,int? timeOut)
    //    {
    //        IAopClient client = new DefaultAopClient(AliPayConfig.gatewayUrl, AliPayConfig.AppId, AliPayConfig.privatekey, "json", "1.0", AliPayConfig.sign_type, AliPayConfig.alipublickey, AliPayConfig.charset, false);
    //        //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
    //        AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
    //        //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
    //        AlipayTradeWapPayModel alipayModel = new AlipayTradeWapPayModel
    //        {
    //            //Body = "buygoods",
    //            Subject = Subject,
    //            TotalAmount = decimal.Round(money, 2).ToString("f2"),
    //            ProductCode = "QUICK_WAP_PAY",
    //            OutTradeNo = orderCode,
    //            //        TimeoutExpress = "30m",
    //            TimeoutExpress = (timeOut?? PayTimeOut).ToString() + "m",
    //        };
    //        request.SetReturnUrl(ConfigHelper.GetAppsettingValue(ConfigConstants.AliPaymentResultUrl));
    //        request.SetNotifyUrl(NotifyUrl);
    //        request.SetBizModel(alipayModel);
    //        //这里和普通的接口调用不同，使用的是pageExecute
    //        var reqTime = DateTime.Now;
    //        AlipayTradeWapPayResponse response = null;
    //        try
    //        {
    //            response = client.pageExecute(request, null, "post");
    //            //记录支付宝调用的日志

    //            WritePostThridApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime, response.Body, DateTime.Now, true);

    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.LogError("提交支付宝预付款订单 SubmitPay", ex.ToString());
    //            //记录支付宝调用的日志
    //            WritePostThridApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime,
    //                response.IsNull() ? ex.ToString() : response.Body, DateTime.Now, true);

    //        }
    //        var res = new ApiResultModel<string>()
    //        {
    //            Status = ResponseStatus.Success,
    //            BusinessData = response.Body
    //        };
    //        return res;
    //    }

    //    /// <summary>
    //    /// 获取APP支付宝支付订单参数
    //    /// </summary>
    //    /// <param name="orderCode">平台订单号</param>
    //    /// <param name="money">金额</param>
    //    /// <param name="timeOut">订单支付超时时间</param>
    //    /// <returns></returns>
    //    private ApiResultModel<string> GetAppAliPayApiParam(string orderCode, decimal money, int? timeOut)
    //    {
    //        IAopClient client = new DefaultAopClient(AliPayConfig.gatewayUrl, AliPayConfig.AppId, AliPayConfig.privatekey, "json", "1.0", AliPayConfig.sign_type, AliPayConfig.alipublickey, AliPayConfig.charset, false);
    //        //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
    //        AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
    //        //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
    //        AlipayTradeAppPayModel pmodel = new AlipayTradeAppPayModel
    //        {
    //            Subject = Subject,
    //            TotalAmount = decimal.Round(money, 2).ToString("f2"),
    //            ProductCode = "QUICK_MSECURITY_PAY",
    //            OutTradeNo = orderCode,
    //            //         TimeoutExpress = "30m"
    //            TimeoutExpress = (timeOut ?? PayTimeOut).ToString() + "m",
    //        };
    //        request.SetBizModel(pmodel);
    //        request.SetNotifyUrl(NotifyUrl);
    //        //这里和普通的接口调用不同，使用的是sdkExecute
    //        AlipayTradeAppPayResponse response = null;
    //        var reqTime = DateTime.Now;
    //        //HttpUtility.HtmlEncode是为了输出到页面时防止被浏览器将关键参数html转义，实际打印到日志以及http传输不会有这个问题
    //        try
    //        {
    //            response = client.SdkExecute(request);
    //            //记录支付宝调用的日志

    //            WritePostThridApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime, response.Body, DateTime.Now, true);

    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.LogError("提交支付宝预付款订单 SubmitPay", ex.ToString());
    //            //记录支付宝调用的日志
    //            WritePostThridApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime,
    //                response.IsNull() ? ex.ToString() : response.Body, DateTime.Now, true);

    //        }
    //        var res = new ApiResultModel<string>()
    //        {
    //            Status = ResponseStatus.Success,
    //            BusinessData = response.Body
    //        };
    //        return res;
    //    }
    //    /// <summary>
    //    /// 主动查询支付结果
    //    /// </summary>
    //    /// <param name="orderCode">平台交易单号</param>
    //    /// <returns></returns>
    //    public override ApiResultModel<string> QueryPayResult(string orderCode)
    //    {
    //        IAopClient client = new DefaultAopClient(AliPayConfig.gatewayUrl, AliPayConfig.AppId, AliPayConfig.privatekey, "json", "1.0", AliPayConfig.sign_type, AliPayConfig.alipublickey, AliPayConfig.charset, false);
    //        //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
    //        AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
    //        //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
    //        AlipayTradeQueryModel pmodel = new AlipayTradeQueryModel
    //        {
    //            TradeNo = "",
    //            OutTradeNo = orderCode
    //        };
    //        request.SetBizModel(pmodel);
    //        //这里和普通的接口调用不同，使用的是sdkExecute
    //        AlipayTradeQueryResponse response = null;
    //        var reqTime = DateTime.Now;
    //        //HttpUtility.HtmlEncode是为了输出到页面时防止被浏览器将关键参数html转义，实际打印到日志以及http传输不会有这个问题
    //        try
    //        {
    //            response = client.Execute(request);
    //            //记录支付宝调用的日志

    //            WritePostThridApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime, response.Body, DateTime.Now, true);

    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.LogError(" 主动查询支付结果 - 支付宝预付款订单 QueryPayResult", ex.ToString());
    //            //记录支付宝调用的日志
    //            WritePostThridApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime,
    //                response.IsNull() ? ex.ToString() : response.Body, DateTime.Now, true);
    //        }
    //        if (response != null&&!response.IsError)
    //        {
    //            PayStatusType payStatusType = PayStatusType.UnPay;
    //            if (response.TradeStatus == "WAIT_BUYER_PAY")
    //            {
    //                payStatusType = PayStatusType.UnPay;
    //            }
    //            else if (response.TradeStatus == "TRADE_CLOSED")
    //            {
    //                payStatusType = PayStatusType.TimeOut;
    //            }
    //            else if (response.TradeStatus == "TRADE_SUCCESS")
    //            {
    //                payStatusType = PayStatusType.Payed;
    //            }
    //            else if (response.TradeStatus == "TRADE_FINISHED")
    //            {
    //                payStatusType = PayStatusType.Payed;
    //            }
    //            string tradeNo = response.TradeNo;
    //            ConfrimPayResult(orderCode, tradeNo, payStatusType);
    //        }
    //        var res = new ApiResultModel<string>()
    //        {
    //            Status = ResponseStatus.Success,
    //            BusinessData = response.Body
    //        };
    //        return res;
    //    }
    //    #region 退款
    //    /// <summary>
    //    /// 发起订单退款（此方法，以后应拆分2个方法处理退款，一个更新退款状态，另一个MQ调用微信退款申请）
    //    /// </summary>
    //    /// <param name="refundRequest">退款申请参数</param>
    //    /// <returns></returns>
    //    public override ApiResultModel<string> RefundPay(RefundBaseRequest refundRequest)
    //    {
    //        ApiResultModel<string> res = new ApiResultModel<string> { Status = ResponseStatus.Fail };

    //        if (refundRequest.OrderCode.IsNullOrWhiteSpace())
    //        {
    //            res.ErrorMessage = "订单编号参数为空，无法退款";
    //            return res;
    //        }
    //        //1、校验订单状态
    //        var resCheck = CheckOrderCanRefund(refundRequest);
    //        if (resCheck.Status != ResponseStatus.Success)
    //        {
    //            res.ErrorMessage = resCheck.ErrorMessage;
    //            res.Status = resCheck.Status;
    //            return res;
    //        }

    //        //处理退款金额，不能超过支付金额
    //        var orderRefund = resCheck.BusinessData;
    //        var orderRefundCode = orderRefund.RefundRemark;
    //        orderRefund.RefundRemark = "";

    //        //2、实际退款请求

    //        string total_fee = orderRefund.OrderAmount.GetValueOrDefault(0).ToString("#0.00");

    //        string refund_fee = orderRefund.Amount.GetValueOrDefault(0).ToString("#0.00");


    //        //2.1、实际退款请求
    //        IAopClient client = new DefaultAopClient(AliPayConfig.gatewayUrl, AliPayConfig.AppId, AliPayConfig.privatekey, "json", "1.0", AliPayConfig.sign_type, AliPayConfig.alipublickey, AliPayConfig.charset, false);
    //        AlipayTradeRefundRequest request = new AlipayTradeRefundRequest();
    //        AlipayTradeRefundModel alipayModel = new AlipayTradeRefundModel()
    //        {
    //            OutTradeNo = orderRefund.OrderCode,
    //            TradeNo = orderRefundCode,
    //            RefundAmount = refund_fee,
    //            RefundReason = "用户申请退款",
    //        };
    //        request.SetBizModel(alipayModel);
    //        AlipayTradeRefundResponse response = client.Execute(request);
    //        //2.2、记录请求日志
    //        //记录支付宝退款调用的日志
    //        WritePostThridApi(ThirdPlatformBusinessType.Payment, orderRefund.OrderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.BizContent, DateTime.Now, response.Body, DateTime.Now, true);

    //        //3、退款结果验证
    //        if (!response.SubCode.IsNullOrWhiteSpace())
    //        {
    //            orderRefund.RefundRemark = response.SubMsg;
    //            res = UpdateOrderRefundStatus(orderRefund, OrderRefundStatusType.Fail);
    //            if (res.Status != ResponseStatus.Success)
    //            {
    //                return res;
    //            }

    //            res.ErrorMessage = response.SubMsg;
    //            return res;
    //        }
    //        if (response.Code != "10000")
    //        {
    //            orderRefund.RefundRemark = response.Msg;
    //            res = UpdateOrderRefundStatus(orderRefund, OrderRefundStatusType.Fail);
    //            if (res.Status != ResponseStatus.Success)
    //            {
    //                return res;
    //            }
    //            res.ErrorMessage = response.Msg;
    //            return res;
    //        }


    //        //4、设置退款状态
    //        res = UpdateOrderRefundStatus(orderRefund, OrderRefundStatusType.Complete);
    //        if (res.Status != ResponseStatus.Success)
    //        {
    //            return res;
    //        }

    //        //5、返回退款结果
    //        res.Status = ResponseStatus.Success;

    //        return res;
    //    }
    //    #endregion
    //}
}
