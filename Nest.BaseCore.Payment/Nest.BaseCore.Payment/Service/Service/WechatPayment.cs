using Nest.BaseCore.Payment.PaymentSDK.Wechat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.Service
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WechatPayment : PaymentBaseService
    {
        public WechatPayment(BrowserType sourceOption) : base(sourceOption)
        {

        }

        #region 私有变量

        private string ModulsName = "WechatPayment";
        private string AppId
        {
            get
            {
                switch (BrowserType)
                {
                    case BrowserType.Wap:
                    case BrowserType.Wechat:
                        return WechatConfig.AppId;
                    case BrowserType.MiniApp:
                        return WechatConfig.AppIdByMiniApp;
                    case BrowserType.App:
                        return WechatConfig.AppIdByApp;
                    case BrowserType.ScanMiniApp:
                        return WechatConfig.AppIdByScanMiniApp;
                    default:
                        return WechatConfig.AppIdByApp;
                }
            }
        }
        private string MchId
        {
            get
            {
                switch (BrowserType)
                {
                    case BrowserType.Wap:
                    case BrowserType.Wechat:
                        return WechatConfig.MchId;
                    case BrowserType.MiniApp:
                        return WechatConfig.MchIdByMiniApp;
                    case BrowserType.App:
                        return WechatConfig.MchIdByApp;
                    case BrowserType.ScanMiniApp:
                        return WechatConfig.MchIdByScanMiniApp;
                    default:
                        return WechatConfig.MchIdByApp;
                }
            }
        }
        private string Key
        {
            get
            {
                switch (BrowserType)
                {
                    case BrowserType.Wap:
                    case BrowserType.Wechat:
                        return WechatConfig.Key;
                    case BrowserType.MiniApp:
                        return WechatConfig.KeyByMiniApp;
                    case BrowserType.App:
                        return WechatConfig.KeyByApp;
                    case BrowserType.ScanMiniApp:
                        return WechatConfig.KeyByScanMiniApp;
                    default:
                        return WechatConfig.Key;
                }
            }
        }

        #endregion

        /// <summary>
        /// 微信支付通知地址
        /// </summary>
        public override string NotifyUrl
        {
            get
            {
                string notifyUrl = AppSettingsHelper.Configuration[ConfigConstants.WeixinPaymentNotifyUrl];
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
            string accessTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token";
            string accessUrl = $"{accessTokenUrl}?appid={WechatConfig.AppId}&secret={WechatConfig.Secret}&code={code}&grant_type=authorization_code";
            var oauthTokenResponse = HttpHelper.HttpGet<dynamic>(accessUrl);
            ThirdOpenAuthorizeViewModel result = new ThirdOpenAuthorizeViewModel
            {
                OpenId = oauthTokenResponse.openid,
                UnionId = oauthTokenResponse.unionid,
                Token = oauthTokenResponse.access_token,
                Expires = oauthTokenResponse.expires_in
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
        public override string GetThirdOAuthUrl(string redirectUrl, string state = "", ThirdOAuthScope scope = ThirdOAuthScope.SnsapiBase)
        {
            redirectUrl = Utils.UrlEncode(redirectUrl);
            string wxScope = scope == ThirdOAuthScope.SnsapiBase ? "snsapi_base" : "snsapi_userinfo";
            string wechatOauthUrl = "https://open.weixin.qq.com/connect/oauth2/authorize";
            string oauthUrl = $"{wechatOauthUrl}?appid={WechatConfig.AppId}&redirect_uri={redirectUrl}&response_type=code&scope={wxScope}&state={state}#wechat_redirect";

            return oauthUrl;
        }

        /// <summary>
        /// 支付结果回调
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override ApiResultModel<string> PayNotify(HttpRequestMessage requestMessage)
        {
            string data = "";//requestMessage.Content.ReadAsStringAsync().Result;
            using (StreamReader sr = new StreamReader(requestMessage.Content.ReadAsStreamAsync().Result))
            {
                sr.BaseStream.Position = 0;
                data = sr.ReadToEnd();
            }

            // LogHelper.LogInfo(ModulsName, "微信支付回传结果:" + data);
            //返回对象
            WxPayData res = new WxPayData();
            res.SetValue("return_code", "FAIL");
            res.SetValue("return_msg", "失败");

            var result = new ApiResultModel<string>() { Code = ApiResultCode.Fail, Message = res.ToXml() };

            if (string.IsNullOrEmpty(data))
            {
                //LogHelper.LogError(ModulsName, "微信支付回传结果参数为空 ");
                result.Message = "微信支付回传结果参数为空";
                return result;
            }

            WxPayData notifyData = new WxPayData();
            try
            {
                notifyData.FromXml(data, Key);
            }
            catch (WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                //LogHelper.LogError(ModulsName, "Sign check error : " + ex.Message);
                result.Message = "签名错误";
                return result;
            }
            //WritePostThirdApi(ThirdPlatformBusinessType.Payment, notifyData.GetValue("out_trade_no").ToString(), ThirdPlatformType.WechatPay, requestMessage.RequestUri.LocalPath, data, DateTime.Now, "", DateTime.Now, true);

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                result.Message = "支付结果中微信订单号不存在";
                return result;
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();
            string orderCode = notifyData.GetValue("out_trade_no").ToString();
            bool isApp = false;
            if (notifyData.IsSet("trade_type") && notifyData.GetValue("trade_type").ToString().ToUpper() == "APP")
            {
                isApp = true;
            }
            var appid = notifyData.GetValue("appid").ToString().ToUpper();
            //SetBrowerType(appid);
            //var order = GetSmallOrderModel(orderCode);

            //if (order.IsNull())
            //{
            //    res.ErrorMessage = "未找到平台订单信息,OrderCode:" + orderCode;
            //    return res;
            //}
            ////查询订单，判断订单真实性
            //if (!isApp && !QueryOrder(transaction_id, isApp, Key))
            //{
            //    //若订单查询失败，则立即返回结果给微信支付后台
            //    res.ErrorMessage = "支付结果中微信订单来源错误";
            //    return res;
            //}
            //var openId = notifyData.GetValue("openid").ToString();
            //if ((order.Status == OrderStatusType.Confirm || order.Status == OrderStatusType.UnConfirm) && order.PayStatus == PayStatusType.UnPay)
            //{
            //    //TRADE_SUCCESS 交易支付成功，可退款 交易支付成功
            //    res = ChangeOrderPayStatus(orderCode, transaction_id, PayStatusType.Payed, thirdAppId: AppId, thirdUserId: openId);
            //    if (res.Status == ResponseStatus.Success)
            //    {
            //        res = new WxPayData();
            //        res.SetValue("return_code", "SUCCESS");
            //        res.SetValue("return_msg", "OK");
            //        res.BusinessData = res.ToXml();
            //    }
            //}
            //else
            //{
            //    res.ErrorMessage = "平台订单信息当前状态不允许支付 OrderCode：" + orderCode;
            //}

            res = new WxPayData();
            res.SetValue("return_code", "SUCCESS");
            res.SetValue("return_msg", "OK");
            result.Message = res.ToXml();
            return result;
        }

        /// <summary>
        /// 主动查询支付结果
        /// </summary>
        /// <param name="orderCode">平台交易单号</param>
        /// <returns></returns>
        public override ApiResultModel<string> QueryPayResult(string orderCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 发起订单退款
        /// </summary>
        /// <param name="refundRequest">退款申请参数</param>
        /// <returns></returns>
        public override ApiResultModel<string> RefundPay(RefundBaseRequest refundRequest)
        {
            ApiResultModel<string> res = new ApiResultModel<string> { Code = ApiResultCode.Fail };

            //处理退款金额，不能超过支付金额
            var orderRefund = refundRequest.OrderMoney;
            var orderRefundCode = refundRequest.RefundRemark;


            //2、实际退款请求
            string total_fee = (refundRequest.OrderMoney * 100).ToString("f0");
            int.TryParse(total_fee, out int _total_fee);

            string refund_fee = (refundRequest.RefundMoney * 100).ToString("f0");
            int.TryParse(refund_fee, out int _refund_fee);

            WxPayData data = new WxPayData();
            data.SetValue("transaction_id", refundRequest.TradeNo);
            data.SetValue("out_trade_no", refundRequest.OrderCode);
            data.SetValue("total_fee", _total_fee);
            data.SetValue("refund_fee", _refund_fee);
            data.SetValue("refund_desc", "用户申请退款");
            data.SetValue("out_refund_no", orderRefundCode);
            data.SetValue("appid", AppId);
            data.SetValue("mch_id", MchId);

            //2.1、实际退款请求
            var result = WxPayApi.Refund(data, key: Key);

            //2.2、记录请求日志
            //记录微信退款调用的日志
            //string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderRefund.OrderCode, ThirdPlatformType.WechatPay, url, data.ToXml(), DateTime.Now, result.ToXml(), DateTime.Now, true);

            //3、退款结果验证
            if (result.GetValue("return_code").ToString() != "SUCCESS")
            {
                res.Message = string.Format("{0}（{1}）", result.GetValue("return_msg").ToString(), result.GetValue("return_code").ToString());
                return res;
            }
            if (result.GetValue("result_code").ToString() != "SUCCESS")
            {
                res.Message = string.Format("{0}（{1}）", result.GetValue("err_code_des").ToString(), result.GetValue("result_code").ToString());
                return res;
            }

            res.Code = ApiResultCode.Success;
            return res;
        }

        /// <summary>
        /// 发起订单支付
        /// </summary>
        /// <param name="payRequest"></param>
        /// <returns></returns>
        public override ApiResultModel<string> SubmitPay(PayBaseRequest payRequest)
        {
            if (BrowserType == BrowserType.Wechat)
            {
                return GetWeChatPayJavaScriptApiParam(payRequest.orderCode, payRequest.userId, payRequest.money, payRequest.clientIp, payRequest.PaytimeOut);
            }
            else if (BrowserType == BrowserType.Wap)
            {
                return GetWapWeChatPayApiParam(payRequest.orderCode, payRequest.userId, payRequest.money, payRequest.clientIp, payRequest.PaytimeOut);
            }
            //else if (BrowserType == BrowserType.App || BrowserType == BrowserType.AppLife)
            //{
            //    return GetAppWeChatPayApiParam(payRequest.orderCode, payRequest.userId, payRequest.money, payRequest.clientIp, payRequest.PaytimeOut);
            //}
            else if (BrowserType == BrowserType.MiniApp || BrowserType == BrowserType.ScanMiniApp)
            {
                return GetWeChatPayJavaScriptApiParam(payRequest.orderCode, payRequest.userId, payRequest.money, payRequest.clientIp, payRequest.PaytimeOut);
            }
            else
            {
                return GetAppWeChatPayApiParam(payRequest.orderCode, payRequest.userId, payRequest.money, payRequest.clientIp, payRequest.PaytimeOut);
            }
        }


        #region private

        private string Subject
        {
            get
            {
                string subject = "";
                switch (BrowserType)
                {
                    case BrowserType.MiniApp:
                        subject = "xxx-在线支付";
                        break;
                    case BrowserType.ScanMiniApp:
                        subject = "xxx";
                        break;
                    case BrowserType.Wechat:
                        subject = "xxx";
                        break;
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
        /// 获取微信支付JS订单参数（微信公众号环境支付）
        /// </summary>
        /// <param name="orderCode">订单号</param>
        /// <param name="userId">付款用户</param>
        /// <param name="money">交易金额</param>
        ///  <param name="clientIp">客户端ip</param>
        /// <param name="timeOut">支付超时时间</param>
        /// <returns></returns>
        private ApiResultModel<string> GetWeChatPayJavaScriptApiParam(string orderCode, string userId, decimal money, string clientIp, int? timeOut)
        {
            ApiResultModel<string> result = new ApiResultModel<string> { Code = ApiResultCode.Fail };

            string total_fee = (money * 100).ToString("f0");
            int.TryParse(total_fee, out int _total_fee);
            JsApiPay jsApiPay = new JsApiPay();
            string openid = userId;

            WxPayData data = new WxPayData();
            data.SetValue("body", Subject);
            data.SetValue("attach", "BUYGOODS");
            data.SetValue("out_trade_no", orderCode);
            data.SetValue("total_fee", total_fee);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            //         data.SetValue("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(timeOut ?? PayTimeOut).ToString("yyyyMMddHHmmss"));
            data.SetValue("goods_tag", "");
            data.SetValue("trade_type", "JSAPI");
            data.SetValue("openid", openid);
            data.SetValue("appid", AppId);
            data.SetValue("mch_id", MchId);//商户号
            //prepay_id
            if (!string.IsNullOrEmpty(clientIp))
            {
                data.SetValue("spbill_create_ip", clientIp);//终端ip	 
            }
            //LogHelper.LogError("GetWeChatPayJavaScriptApiParam", "data=" + data.ToXml());
            //JSAPI支付预处理
            try
            {
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(data, NotifyUrl, Key);
                //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.WechatPay, "JsApiPay", data.ToXml(), DateTime.Now, unifiedOrderResult.ToXml(), DateTime.Now, true);
                result.Message = jsApiPay.GetJsApiParameters(Key);//获取H5调起JS API参数   
                result.Code = ApiResultCode.Success;
                return result;
            }
            catch (WxPayFallException ex)
            {
                //LogHelper.LogError("GetWeChatPayJavaScriptApiParam", "checkout_ex=" + ex.Message);
                result.Message = ex.Message;
                return result;
            }
            catch (Exception ex)
            {
                //LogHelper.LogError("GetWeChatPayJavaScriptApiParam", "checkout_ex=" + ex.ToString());
                result.Message = "微信统一下单失败（" + ex.Message + "）";
                return result;
            }
        }

        /// <summary>
        /// 获取Wap微信支付订单参数（浏览器环境支付）
        /// </summary>
        /// <param name="orderCode">订单号</param>
        /// <param name="userId">付款用户</param>
        /// <param name="money">交易金额</param>
        ///  <param name="clientIp">客户端ip</param>
        /// <returns></returns>
        private ApiResultModel<string> GetWapWeChatPayApiParam(string orderCode, string userId, decimal money, string clientIp, int? timeOut)
        {
            ApiResultModel<string> result = new ApiResultModel<string> { Code = ApiResultCode.Fail };

            string total_fee = (money * 100).ToString("f0");
            int.TryParse(total_fee, out int _total_fee);

            WxPayData data = new WxPayData();
            data.SetValue("body", Subject);
            data.SetValue("attach", "BUYGOODS");
            data.SetValue("out_trade_no", orderCode);
            data.SetValue("total_fee", total_fee);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            //          data.SetValue("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(timeOut ?? PayTimeOut).ToString("yyyyMMddHHmmss"));
            data.SetValue("goods_tag", "");
            data.SetValue("trade_type", "MWEB");
            data.SetValue("openid", userId);
            data.SetValue("appid", AppId);
            if (!string.IsNullOrEmpty(clientIp))
            {
                data.SetValue("spbill_create_ip", clientIp);//终端ip	 
            }
            var scene_info = new { h5_info = new { type = "Wap", wap_url = WechatConfig.WechatWebPath, wap_name = Subject } };
            data.SetValue("scene_info", JsonHelper.SerializeObject(scene_info));

            WxPayData wxdata = WxPayApi.UnifiedOrder(data, NotifyUrl);
            //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.WechatPay, "WxPayApi", data.ToXml(), DateTime.Now, wxdata.ToXml(), DateTime.Now, true);
            if (wxdata.GetValue("return_code").ToString() == "FAIL")
            {
                result.Message = wxdata.GetValue("return_msg").ToString();
                return result;
            }

            if (wxdata.IsSet("result_code") && wxdata.GetValue("result_code").ToString() != "SUCCESS")
            {
                result.Message = "生成失败";
                return result;
            }
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("appid", wxdata.GetValue("appid").ToString());
            param.Add("prepay_id", wxdata.GetValue("prepay_id").ToString());
            param.Add("mweb_url", wxdata.GetValue("mweb_url").ToString());
            result.Code = ApiResultCode.Success;
            result.Message = JsonHelper.SerializeObject(param);
            return result;
        }

        /// <summary>
        /// 获取APP微信支付订单参数（APP环境支付）
        /// </summary>
        /// <param name="orderCode">订单号</param>
        /// <param name="userId">付款用户</param>
        /// <param name="money">交易金额</param>
        ///  <param name="clientIp">客户端ip</param>
        /// <returns></returns>
        private ApiResultModel<string> GetAppWeChatPayApiParam(string orderCode, string userId, decimal money, string clientIp, int? timeOut)
        {
            ApiResultModel<string> result = new ApiResultModel<string> { Code = ApiResultCode.Fail };

            var total_fee = Convert.ToInt32(money * 100);

            WxPayData data = new WxPayData();
            data.SetValue("appid", AppId);
            data.SetValue("mch_id", MchId);
            data.SetValue("body", Subject);
            data.SetValue("attach", "BUYGOODS");
            data.SetValue("out_trade_no", orderCode);
            data.SetValue("total_fee", total_fee);
            data.SetValue("trade_type", "APP");
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(timeOut ?? PayTimeOut).ToString("yyyyMMddHHmmss"));
            if (!string.IsNullOrEmpty(clientIp))
            {
                data.SetValue("spbill_create_ip", clientIp);//终端ip	 
            }
            //string key = WechatConfig.KeyByApp;
            //if(BrowserType == BrowserType.AppLife)
            //{
            //    key = WechatConfig.KeyByLifeApp;
            //}
            WxPayData wxdata = WxPayApi.UnifiedOrder(data, NotifyUrl, key: Key);
            //WritePostThirdApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.WechatPay, "WxPayApi APP", data.ToXml(), DateTime.Now, wxdata.ToXml(), DateTime.Now, true);
            if (wxdata.GetValue("return_code").ToString() == "FAIL")
            {
                result.Message = wxdata.GetValue("return_msg").ToString();
                return result;
            }

            if (wxdata.IsSet("result_code") && wxdata.GetValue("result_code").ToString() != "SUCCESS")
            {
                result.Message = "生成失败";
                return result;
            }
            WxPayData jsApiParam = new WxPayData();
            jsApiParam.SetValue("appid", wxdata.GetValue("appid").ToString());
            jsApiParam.SetValue("partnerid", wxdata.GetValue("appid").ToString());
            jsApiParam.SetValue("prepayid", wxdata.GetValue("prepay_id").ToString());
            jsApiParam.SetValue("package", "Sign=WXPay");
            jsApiParam.SetValue("noncestr", WxPayApi.GenerateNonceStr());
            jsApiParam.SetValue("timestamp", WxPayApi.GenerateTimeStamp());
            jsApiParam.SetValue("sign", jsApiParam.MakeSign(Key));
            var appPaymentInfo = new
            {
                PartnerId = wxdata.GetValue("appid").ToString(),
                PrepayId = wxdata.GetValue("prepay_id").ToString(),


                NonceStr = jsApiParam.GetValue("noncestr").ToString(),
                TimeStamp = jsApiParam.GetValue("timestamp").ToString(),
                PackageValue = jsApiParam.GetValue("package").ToString(),
                Sign = jsApiParam.GetValue("sign").ToString(),
            };

            result.Code = ApiResultCode.Success;
            result.Message = JsonHelper.SerializeObject(appPaymentInfo);
            return result;
        }

        #endregion

    }
}
