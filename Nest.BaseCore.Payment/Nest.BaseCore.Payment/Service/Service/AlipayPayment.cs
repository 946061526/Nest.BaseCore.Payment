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
    /// <summary>
    /// 支付宝支付
    /// </summary>
    public class AlipayPayment : PaymentBaseService
    {
        public AlipayPayment(BrowserType sourceOption) : base(sourceOption)
        {

        }

        /// <summary>
        /// 标题
        /// </summary>
        private string Subject
        {
            get
            {
                string subject = "";
                switch (BrowserType)
                {
                    case BrowserType.App:
                        subject = "xxx-在线支付";
                        break;
                    default:
                        subject = "xxx-在线支付";
                        break;
                }
                return subject;
            }
        }

        /// <summary>
        /// 支付宝支付通知地址
        /// </summary>
        public override string NotifyUrl
        {
            get
            {
                string notifyUrl = AppSettingsHelper.Configuration[ConfigConstants.AliPaymentNotifyUrl];
                if (string.IsNullOrEmpty(notifyUrl))
                {
                    return base.NotifyUrl;
                }
                return notifyUrl;
            }
        }

        /// <summary>
        /// 根据Code获取第三方access_token信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public override ThirdOpenAuthorizeViewModel GetThirdOAuth(string code)
        {
            DefaultAopClient client = new DefaultAopClient(AliPayConfig.gatewayUrl, AliPayConfig.AppId, AliPayConfig.privatekey, "json", "1.0", AliPayConfig.sign_type, AliPayConfig.alipublickey, AliPayConfig.charset, false);
            AlipaySystemOauthTokenRequest request = new AlipaySystemOauthTokenRequest
            {
                Code = code,
                GrantType = AliPayConfig.granttype
            };

            AlipaySystemOauthTokenResponse oauthTokenResponse = client.Execute(request);
            if (oauthTokenResponse.IsError)
            {
                throw new Exception(oauthTokenResponse.SubMsg);
            }
            ThirdOpenAuthorizeViewModel result = new ThirdOpenAuthorizeViewModel
            {
                AlipayId = oauthTokenResponse.UserId,
                Token = oauthTokenResponse.AccessToken,
                Expires = int.Parse(oauthTokenResponse.ExpiresIn)
            };

            return result;
        }

        /// <summary>
        /// 获取第三方授权地址
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <param name="state"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public override string GetThirdOAuthUrl(string redirectUrl, string state = "Alipay", ThirdOAuthScope scope = ThirdOAuthScope.SnsapiBase)
        {
            redirectUrl = Utils.UrlEncode(redirectUrl);
            string aliScope = scope == ThirdOAuthScope.SnsapiBase ? "auth_base" : "auth_user";
            string oauthUrl = $"{AliPayConfig.AppAuthorizeUrl}?app_id={AliPayConfig.AppId}&scope={aliScope}&redirect_uri={redirectUrl}&state={state}";

            return oauthUrl;
        }

        /// <summary>
        /// 支付结果回调
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override ApiResultModel<string> PayNotify(HttpRequestMessage requestMessage)
        {
            var res = new ApiResultModel<string>()
            {
                Code = ApiResultCode.Fail,
                Message = "fail"
            };

            var sPara = GetAlipayRequestPost(requestMessage);
            //LogHelper.LogInfo("AlipayNotify->AlipayPayment", "支付宝支付回调通知：" + sPara.ToJsonString());
            //记录支付宝回调的日志
            //WritePostThirdApi(ThirdPlatformBusinessType.Payment, sPara["out_trade_no"], ThirdPlatformType.Alipay, requestMessage.RequestUri.LocalPath, sPara.ToJsonString(), DateTime.Now, "", DateTime.Now, true);
            bool flag = AlipaySignature.RSACheckV1(sPara, AliPayConfig.alipublickey, AliPayConfig.charset, "RSA2", false);
            if (flag)
            {
                string orderCode = sPara["out_trade_no"];
                //LogHelper.LogInfo("AlipayNotify->AlipayPayment", "1,OrderCode:" + orderCode);
                //支付宝交易号
                string trade_no = sPara["trade_no"];

                //交易状态
                string trade_status = sPara["trade_status"];
                //获得调用方的appid；
                //如果是非授权模式，appid是商户的appid；如果是授权模式（token调用），appid是系统商的appid
                string app_id = sPara["app_id"];
                if (!app_id.Equals(AliPayConfig.AppId))
                {
                    res.Message = "支付宝支付结果通知错误，不是本APPID提交的支付单";
                    return res;
                }
                //LogHelper.LogInfo("AlipayNotify->AlipayPayment", "2,OrderCode:" + orderCode);
                //支付宝回调状态
                if (string.IsNullOrEmpty(trade_status))
                {
                    res.Message = "支付宝支付结果通知错误，trade_status 是空值";
                    return res;
                }
                //LogHelper.LogInfo("AlipayNotify->AlipayPayment", "3,OrderCode:" + orderCode);
                //var order = GetSmallOrderModel(orderCode);
                //if (order.IsNull())
                //{
                //    res.Message = "未找到平台订单信息,OrderCode:" + orderCode;
                //    return res;
                //}
                //LogHelper.LogInfo("AlipayNotify->AlipayPayment", "4,OrderCode:" + orderCode);

                //if ((order.Status == OrderStatusType.Confirm || order.Status == OrderStatusType.UnConfirm) && order.PayStatus == PayStatusType.UnPay)
                //{
                //    LogHelper.LogInfo("AlipayNotify->AlipayPayment", "5,OrderCode:" + orderCode);
                //    switch (trade_status.ToUpper())
                //    {
                //        case "TRADE_SUCCESS":
                //            //TRADE_SUCCESS 交易支付成功，可退款 交易支付成功
                //            res = ChangeOrderPayStatus(orderCode, trade_no, PayStatusType.Payed, thirdAppId: app_id, thirdUserId: sPara["buyer_id"]);
                //            break;
                //        case "TRADE_CLOSED":
                //            //TRADE_CLOSED 未付款交易超时关闭，或支付完成后全额退款 交易关闭    true（触发通知）
                //            res = ChangeOrderPayStatus(orderCode, trade_no, PayStatusType.TimeOut, thirdAppId: app_id, thirdUserId: sPara["buyer_id"]);
                //            break;
                //        case "TRADE_FINISHED":
                //            //TRADE_FINISHED 交易结束，不可退款 交易完成    true（触发通知）
                //            res = ChangeOrderPayStatus(orderCode, trade_no, PayStatusType.Payed, thirdAppId: app_id, thirdUserId: sPara["buyer_id"]);
                //            break;
                //        default:
                //            res.Message = "支付宝支付结果通知错误，trade_status 是空值";
                //            return res;

                //    }
                //    if (res.Status == ResponseStatus.Success)
                //    {
                //        res.BusinessData = "success";
                //    }
                //    LogHelper.LogInfo("AlipayNotify->AlipayPayment", "6,OrderCode:" + orderCode);
                //}
                //else
                //{
                //    res.Message = "平台订单信息当前状态不允许支付 OrderCode：" + orderCode;
                //    LogHelper.LogInfo("AlipayNotify->AlipayPayment", "7,OrderCode:" + orderCode);
                //}

                switch (trade_status.ToUpper())
                {
                    case "TRADE_SUCCESS":
                        res.Message = "交易支付成功，可退款";
                        break;
                    case "TRADE_CLOSED":
                        res.Message = "交易关闭";
                        break;
                    case "TRADE_FINISHED":
                        res.Message = "交易结束，不可退款";
                        break;
                    default:
                        res.Message = "支付宝支付结果通知错误，trade_status 是空值";
                        break;
                }
                res.Code = ApiResultCode.Success;
                return res;
            }
            else
            {
                res.Message = "支付宝支付结果：验证失败";
                return res;
            }
        }

        /// <summary>
        /// 主动查询支付结果
        /// </summary>
        /// <param name="orderCode">平台交易单号</param>
        /// <returns></returns>
        public override ApiResultModel<string> QueryPayResult(string orderCode)
        {
            IAopClient client = new DefaultAopClient(AliPayConfig.gatewayUrl, AliPayConfig.AppId, AliPayConfig.privatekey, "json", "1.0", AliPayConfig.sign_type, AliPayConfig.alipublickey, AliPayConfig.charset, false);
            //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
            AlipayTradeQueryModel pmodel = new AlipayTradeQueryModel
            {
                TradeNo = "",
                OutTradeNo = orderCode
            };
            request.SetBizModel(pmodel);
            //这里和普通的接口调用不同，使用的是sdkExecute
            AlipayTradeQueryResponse response = null;
            var reqTime = DateTime.Now;
            //HttpUtility.HtmlEncode是为了输出到页面时防止被浏览器将关键参数html转义，实际打印到日志以及http传输不会有这个问题
            try
            {
                response = client.Execute(request);

                //记录支付宝调用的日志
                //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime, response.Body, DateTime.Now, true);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(" 主动查询支付结果 - 支付宝预付款订单 QueryPayResult", ex.ToString());
                ////记录支付宝调用的日志
                //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime,
                //    response.IsNull() ? ex.ToString() : response.Body, DateTime.Now, true);
            }
            if (response != null && !response.IsError)
            {
                PayStatusType payStatusType = PayStatusType.UnPay;
                if (response.TradeStatus == "WAIT_BUYER_PAY")
                {
                    payStatusType = PayStatusType.UnPay;
                }
                else if (response.TradeStatus == "TRADE_CLOSED")
                {
                    payStatusType = PayStatusType.TimeOut;
                }
                else if (response.TradeStatus == "TRADE_SUCCESS")
                {
                    payStatusType = PayStatusType.Paid;
                }
                else if (response.TradeStatus == "TRADE_FINISHED")
                {
                    payStatusType = PayStatusType.Paid;
                }
                string tradeNo = response.TradeNo;
                //ConfrimPayResult(orderCode, tradeNo, payStatusType);
            }
            var res = new ApiResultModel<string>()
            {
                Code = ApiResultCode.Success,
                Message = response.Body
            };
            return res;
        }

        /// <summary>
        /// 发起订单退款
        /// </summary>
        /// <param name="refundRequest">退款申请参数</param>
        /// <returns></returns>
        public override ApiResultModel<string> RefundPay(RefundBaseRequest refundRequest)
        {
            var res = new ApiResultModel<string>() { Code = ApiResultCode.Fail, Message = "fail" };

            string total_fee = refundRequest.OrderMoney.ToString("#0.00");
            string refund_fee = refundRequest.RefundMoney.ToString("#0.00");
            //退款请求
            IAopClient client = new DefaultAopClient(AliPayConfig.gatewayUrl, AliPayConfig.AppId, AliPayConfig.privatekey, "json", "1.0", AliPayConfig.sign_type, AliPayConfig.alipublickey, AliPayConfig.charset, false);
            AlipayTradeRefundRequest request = new AlipayTradeRefundRequest();
            AlipayTradeRefundModel alipayModel = new AlipayTradeRefundModel()
            {
                OutTradeNo = refundRequest.OrderCode,
                TradeNo = refundRequest.TradeNo,
                RefundAmount = refund_fee,
                RefundReason = "用户申请退款",
            };
            request.SetBizModel(alipayModel);
            AlipayTradeRefundResponse response = client.Execute(request);
            //2.2、记录请求日志
            //记录支付宝退款调用的日志
            //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderRefund.OrderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.BizContent, DateTime.Now, response.Body, DateTime.Now, true);
            if (response.Code != "10000")
            {
                res.Message = response.Msg;
                return res;
            }
            res.Code = ApiResultCode.Success;
            res.Message = "ok";
            return res;
        }

        /// <summary>
        /// 发起订单支付
        /// </summary>
        /// <param name="payRequest"></param>
        /// <returns></returns>
        public override ApiResultModel<string> SubmitPay(PayBaseRequest payRequest)
        {
            if (BrowserType == BrowserType.Alipay)
            {
                return GetWapAlipayPram(payRequest.orderCode, payRequest.money, payRequest.PaytimeOut);
            }
            else
            {
                return GetAppAliPayParam(payRequest.orderCode, payRequest.money, payRequest.PaytimeOut);
            }
        }

        #region private

        /// <summary>
        /// 获取阿里支付回调参数字典
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> GetAlipayRequestPost(HttpRequestMessage requestMessage)
        {
            // System.Web.HttpUtility.co context = (HttpContextBase)requestMessage.Properties["MS_HttpContext"];//获取传统context  
            IDictionary<string, string> sArray = new Dictionary<string, string>();

            //for (int i = 0; i < context.Request.Form.Count; i++)
            //{
            //    sArray.Add(context.Request.Form.Keys[i], context.Request.Form[i]);
            //}

            return sArray;
        }

        /// <summary>
        /// 获取wap支付宝支付订单参数
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="money"></param>
        /// <param name="timeOut">订单支付超时时间</param>
        /// <returns></returns>
        private ApiResultModel<string> GetWapAlipayPram(string orderCode, decimal money, int? timeOut)
        {
            IAopClient client = new DefaultAopClient(AliPayConfig.gatewayUrl, AliPayConfig.AppId, AliPayConfig.privatekey, "json", "1.0", AliPayConfig.sign_type, AliPayConfig.alipublickey, AliPayConfig.charset, false);
            //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
            AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
            //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
            AlipayTradeWapPayModel alipayModel = new AlipayTradeWapPayModel
            {
                //Body = "buygoods",
                Subject = Subject,
                TotalAmount = decimal.Round(money, 2).ToString("f2"),
                ProductCode = "QUICK_WAP_PAY",
                OutTradeNo = orderCode,
                //        TimeoutExpress = "30m",
                TimeoutExpress = (timeOut ?? PayTimeOut).ToString() + "m",
            };
            request.SetReturnUrl(AppSettingsHelper.Configuration[ConfigConstants.AliPaymentResultUrl]);
            request.SetNotifyUrl(NotifyUrl);
            request.SetBizModel(alipayModel);
            //这里和普通的接口调用不同，使用的是pageExecute
            var reqTime = DateTime.Now;
            AlipayTradeWapPayResponse response = null;
            try
            {
                response = client.pageExecute(request, null, "post");

                //记录支付宝调用的日志
                //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime, response.Body, DateTime.Now, true);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError("提交支付宝预付款订单 SubmitPay", ex.ToString());
                ////记录支付宝调用的日志
                //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime,
                //    response.IsNull() ? ex.ToString() : response.Body, DateTime.Now, true);

            }
            var res = new ApiResultModel<string>()
            {
                Code = ApiResultCode.Success,
                Message = response.Body
            };
            return res;
        }

        /// <summary>
        /// 获取APP支付宝支付订单参数
        /// </summary>
        /// <param name="orderCode">平台订单号</param>
        /// <param name="money">金额</param>
        /// <param name="timeOut">订单支付超时时间</param>
        /// <returns></returns>
        private ApiResultModel<string> GetAppAliPayParam(string orderCode, decimal money, int? timeOut)
        {
            IAopClient client = new DefaultAopClient(AliPayConfig.gatewayUrl, AliPayConfig.AppId, AliPayConfig.privatekey, "json", "1.0", AliPayConfig.sign_type, AliPayConfig.alipublickey, AliPayConfig.charset, false);
            //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
            AlipayTradeAppPayModel pmodel = new AlipayTradeAppPayModel
            {
                Subject = Subject,
                TotalAmount = decimal.Round(money, 2).ToString("f2"),
                ProductCode = "QUICK_MSECURITY_PAY",
                OutTradeNo = orderCode,
                //         TimeoutExpress = "30m"
                TimeoutExpress = (timeOut ?? PayTimeOut).ToString() + "m",
            };
            request.SetBizModel(pmodel);
            request.SetNotifyUrl(NotifyUrl);
            //这里和普通的接口调用不同，使用的是sdkExecute
            AlipayTradeAppPayResponse response = null;
            var reqTime = DateTime.Now;
            //HttpUtility.HtmlEncode是为了输出到页面时防止被浏览器将关键参数html转义，实际打印到日志以及http传输不会有这个问题
            try
            {
                response = client.SdkExecute(request);

                //记录支付宝调用的日志
                //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime, response.Body, DateTime.Now, true);

            }
            catch (Exception ex)
            {
                //LogHelper.LogError("提交支付宝预付款订单 SubmitPay", ex.ToString());
                ////记录支付宝调用的日志
                //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.Alipay, AliPayConfig.gatewayUrl, request.ToJsonString(), reqTime,
                //    response.IsNull() ? ex.ToString() : response.Body, DateTime.Now, true);

            }
            var res = new ApiResultModel<string>()
            {
                Code = ApiResultCode.Success,
                Message = response.Body
            };
            return res;
        }

        #endregion

    }
}
