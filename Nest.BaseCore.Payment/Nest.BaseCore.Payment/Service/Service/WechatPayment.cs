using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.Service
{
    ///// <summary>
    ///// 微信支付业务处理
    ///// </summary>
    //public class WechatPayment : PaymentOrderBaseService
    //{
    //    private string ModulsName = "WechatPayment";
    //    private string AppId
    //    {
    //        get
    //        {
    //            switch (BrowserType)
    //            {
    //                case BrowserType.Wap:
    //                case BrowserType.Wechat:
    //                    return WechatConfig.AppId;
    //                case BrowserType.SamllApp:
    //                    return WechatConfig.AppIdBySamllApp;
    //                case BrowserType.App:
    //                    return WechatConfig.AppIdByApp;
    //                case BrowserType.AppLife:
    //                    return WechatConfig.AppIdByLifeApp;
    //                case BrowserType.ScanSamllApp:
    //                    return WechatConfig.AppIdByScanSamllApp;
    //                default:
    //                    return WechatConfig.AppIdByApp;
    //            }
    //        }
    //    }
    //    private string MchId
    //    {
    //        get
    //        {
    //            switch (BrowserType)
    //            {
    //                case BrowserType.Wap:
    //                case BrowserType.Wechat:
    //                    return WechatConfig.MchId;
    //                case BrowserType.SamllApp:
    //                    return WechatConfig.MchIdBySamllApp;
    //                case BrowserType.App:
    //                    return WechatConfig.MchIdByApp;
    //                case BrowserType.AppLife:
    //                    return WechatConfig.MchIdByLifeApp;
    //                case BrowserType.ScanSamllApp:
    //                    return WechatConfig.MchIdByScanSamllApp;
    //                default:
    //                    return WechatConfig.MchIdByApp;
    //            }
    //        }
    //    }
    //    private string Key
    //    {
    //        get
    //        {
    //            switch (BrowserType)
    //            {
    //                case BrowserType.Wap:
    //                case BrowserType.Wechat:
    //                    return WechatConfig.Key;
    //                case BrowserType.SamllApp:
    //                    return WechatConfig.KeyBySamllApp;
    //                case BrowserType.App:
    //                    return WechatConfig.KeyByApp;
    //                case BrowserType.AppLife:
    //                    return WechatConfig.KeyByLifeApp;
    //                case BrowserType.ScanSamllApp:
    //                    return WechatConfig.KeyByScanSamllApp;
    //                default:
    //                    return WechatConfig.Key;
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// 微信支付通知地址
    //    /// </summary>
    //    public override string NotifyUrl
    //    {
    //        get
    //        {
    //            string notifyUrl = ConfigHelper.GetAppsettingValue(ConfigConstants.WeixinPaymentNotifyUrl);
    //            if (notifyUrl.IsNullOrWhiteSpace())
    //            {
    //                return base.NotifyUrl;
    //            }
    //            return notifyUrl;
    //        }
    //    }
    //    public WechatPayment(BrowserType sourceOption) : base(sourceOption)
    //    {

    //    }
    //    /// <summary>
    //    /// 获取验证地址
    //    /// </summary>
    //    /// <param name="redirectUrl"></param>
    //    /// <param name="scope"></param>
    //    /// <param name="state"></param>
    //    /// <returns></returns>
    //    public override string GetThridOAuthUrl(string redirectUrl, string state = "Wechat", ThridOAuthScope scope = ThridOAuthScope.SnsapiBase)
    //    {
    //        redirectUrl = Utils.UrlEncode(redirectUrl);
    //        string wxScope = scope == ThridOAuthScope.SnsapiBase ? "snsapi_base" : "snsapi_userinfo";
    //        string wechatOauthUrl = "https://open.weixin.qq.com/connect/oauth2/authorize";
    //        string oauthUrl = string.Format("{0}?appid={1}&redirect_uri={2}&response_type=code&scope={3}&state={4}#wechat_redirect",
    //            wechatOauthUrl,
    //            WechatConfig.AppId,
    //            redirectUrl,
    //            wxScope,
    //            state);

    //        return oauthUrl;
    //    }

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
    //                case BrowserType.SamllApp:
    //                    subject = "速微生活-在线支付";
    //                    break;
    //                case BrowserType.ScanSamllApp:
    //                    subject = "速微生活";
    //                    break;
    //                case BrowserType.Wechat:
    //                    subject = "速微生活";
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
    //    public override ThirdOpenAuthorizeViewModel GetThridOAuth(string code)
    //    {
    //        string accessTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token";
    //        string accessUrl = string.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
    //            accessTokenUrl,
    //            WechatConfig.AppId,
    //            WechatConfig.Secret,
    //            code);
    //        var oauthTokenResponse = HttpHelper.HttpGet<dynamic>(accessUrl);
    //        ThirdOpenAuthorizeViewModel result = new ThirdOpenAuthorizeViewModel
    //        {
    //            OpenId = oauthTokenResponse.openid,
    //            UnionId = oauthTokenResponse.unionid,
    //            Token = oauthTokenResponse.access_token,
    //            Expires = oauthTokenResponse.expires_in
    //        };

    //        return result;
    //    }
    //    /// <summary>
    //    /// 查询微信订单真实性
    //    /// </summary>
    //    /// <param name="transaction_id"></param>
    //    /// <param name="isApp"></param>
    //    /// <returns></returns>
    //    private bool QueryOrder(string transaction_id, bool isApp, string key = WechatConfig.Key)
    //    {
    //        WxPayData req = new WxPayData();
    //        req.SetValue("transaction_id", transaction_id);
    //        //if (isApp)
    //        //{
    //        req.SetValue("appid", AppId);
    //        req.SetValue("mch_id", MchId);
    //        //}
    //        WxPayData res = WxPayApi.OrderQuery(req, key);
    //        if (res.GetValue("return_code").ToString() == "SUCCESS" &&
    //            res.GetValue("result_code").ToString() == "SUCCESS")
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    /// <summary>
    //    /// 支付结果回调
    //    /// </summary>
    //    /// <param name="requestMessage"></param>
    //    /// <returns></returns>
    //    public override BusinessBaseViewModel<string> PayNotify(HttpRequestMessage requestMessage)
    //    {
    //        string data = "";//requestMessage.Content.ReadAsStringAsync().Result;
    //        using (StreamReader sr = new StreamReader(requestMessage.Content.ReadAsStreamAsync().Result))
    //        {
    //            sr.BaseStream.Position = 0;
    //            data = sr.ReadToEnd();
    //        }

    //        LogHelper.LogInfo(ModulsName, "微信支付回传结果:" + data);
    //        //返回对象
    //        WxPayData res = new WxPayData();
    //        res.SetValue("return_code", "FAIL");
    //        res.SetValue("return_msg", "失败");

    //        var business = new BusinessBaseViewModel<string>() { Status = ResponseStatus.Fail, BusinessData = res.ToXml() };

    //        if (data.IsNullOrWhiteSpace())
    //        {
    //            LogHelper.LogError(ModulsName, "微信支付回传结果参数为空 ");
    //            business.ErrorMessage = "微信支付回传结果参数为空";
    //            return business;
    //        }

    //        WxPayData notifyData = new WxPayData();
    //        try
    //        {
    //            notifyData.FromXml(data, Key);
    //        }
    //        catch (WxPayException ex)
    //        {
    //            //若签名错误，则立即返回结果给微信支付后台
    //            LogHelper.LogError(ModulsName, "Sign check error : " + ex.Message);
    //            business.ErrorMessage = "签名错误";
    //            return business;
    //        }
    //        WritePostThridApi(ThirdPlatformBusinessType.Payment, notifyData.GetValue("out_trade_no").ToString(), ThirdPlatformType.WechatPay, requestMessage.RequestUri.LocalPath, data, DateTime.Now, "", DateTime.Now, true);

    //        //检查支付结果中transaction_id是否存在
    //        if (!notifyData.IsSet("transaction_id"))
    //        {
    //            //若transaction_id不存在，则立即返回结果给微信支付后台
    //            business.ErrorMessage = "支付结果中微信订单号不存在";
    //            return business;
    //        }

    //        string transaction_id = notifyData.GetValue("transaction_id").ToString();
    //        string orderCode = notifyData.GetValue("out_trade_no").ToString();
    //        bool isApp = false;
    //        if (notifyData.IsSet("trade_type") && notifyData.GetValue("trade_type").ToString().ToUpper() == "APP")
    //        {
    //            isApp = true;
    //        }
    //        var appid = notifyData.GetValue("appid").ToString().ToUpper();
    //        SetBrowerType(appid);
    //        var order = GetSmallOrderModel(orderCode);

    //        if (order.IsNull())
    //        {
    //            business.ErrorMessage = "未找到平台订单信息,OrderCode:" + orderCode;
    //            return business;
    //        }
    //        //查询订单，判断订单真实性
    //        if (!isApp && !QueryOrder(transaction_id, isApp, Key))
    //        {
    //            //若订单查询失败，则立即返回结果给微信支付后台
    //            business.ErrorMessage = "支付结果中微信订单来源错误";
    //            return business;
    //        }
    //        var openId = notifyData.GetValue("openid").ToString();
    //        if ((order.Status == OrderStatusType.Confirm || order.Status == OrderStatusType.UnConfirm) && order.PayStatus == PayStatusType.UnPay)
    //        {
    //            //TRADE_SUCCESS 交易支付成功，可退款 交易支付成功
    //            business = ChangeOrderPayStatus(orderCode, transaction_id, PayStatusType.Payed, thirdAppId: AppId, thirdUserId: openId);
    //            if (business.Status == ResponseStatus.Success)
    //            {
    //                res = new WxPayData();
    //                res.SetValue("return_code", "SUCCESS");
    //                res.SetValue("return_msg", "OK");
    //                business.BusinessData = res.ToXml();
    //            }
    //        }
    //        else
    //        {
    //            business.ErrorMessage = "平台订单信息当前状态不允许支付 OrderCode：" + orderCode;
    //        }
    //        return business;
    //    }
    //    /// <summary>
    //    /// 根据APPID 设置不同支付主体
    //    /// </summary>
    //    /// <param name="appid"></param>
    //    private void SetBrowerType(string appid)
    //    {
    //        if (appid == WechatConfig.AppIdByApp.ToUpper())
    //        {
    //            this.BrowserType = BrowserType.App;
    //        }
    //        else if (appid == WechatConfig.AppIdByLifeApp.ToUpper())
    //        {
    //            this.BrowserType = BrowserType.AppLife;
    //        }
    //        else if (appid == WechatConfig.AppIdBySamllApp.ToUpper())
    //        {
    //            this.BrowserType = BrowserType.SamllApp;
    //        }
    //        else if (appid == WechatConfig.AppIdByScanSamllApp.ToUpper())
    //        {
    //            this.BrowserType = BrowserType.ScanSamllApp;
    //        }
    //    }

    //    /// <summary>
    //    /// 发起订单支付
    //    /// </summary>
    //    /// <param name="payRequest"></param>
    //    /// <returns></returns>
    //    public override BusinessBaseViewModel<string> SubmitPay(PayBaseRequest payRequest)
    //    {
    //        if (BrowserType == BrowserType.Wechat)
    //        {
    //            return GetWeChatPayJavaScriptApiParam(payRequest.orderCode, payRequest.userId, payRequest.money, payRequest.clientIp, payRequest.PaytimeOut);
    //        }
    //        else if (BrowserType == BrowserType.Wap)
    //        {
    //            return GetWapWeChatPayApiParam(payRequest.orderCode, payRequest.userId, payRequest.money, payRequest.clientIp, payRequest.PaytimeOut);
    //        }
    //        else if (BrowserType == BrowserType.App || BrowserType == BrowserType.AppLife)
    //        {
    //            return GetAppWeChatPayApiParam(payRequest.orderCode, payRequest.userId, payRequest.money, payRequest.clientIp, payRequest.PaytimeOut);
    //        }
    //        else if (BrowserType == BrowserType.SamllApp || BrowserType == BrowserType.ScanSamllApp)
    //        {
    //            return GetWeChatPayJavaScriptApiParam(payRequest.orderCode, payRequest.userId, payRequest.money, payRequest.clientIp, payRequest.PaytimeOut);
    //        }
    //        else
    //        {
    //            return GetAppWeChatPayApiParam(payRequest.orderCode, payRequest.userId, payRequest.money, payRequest.clientIp, payRequest.PaytimeOut);
    //        }
    //    }

    //    #region 微信公众号环境支付

    //    /// <summary>
    //    /// 获取微信支付JS订单参数
    //    /// </summary>
    //    /// <param name="orderCode">订单号</param>
    //    /// <param name="userId">付款用户</param>
    //    /// <param name="money">交易金额</param>
    //    ///  <param name="clientIp">客户端ip</param>
    //    /// <param name="timeOut">支付超时时间</param>
    //    /// <returns></returns>
    //    private BusinessBaseViewModel<string> GetWeChatPayJavaScriptApiParam(string orderCode, string userId, decimal money, string clientIp, int? timeOut)
    //    {
    //        BusinessBaseViewModel<string> response = new BusinessBaseViewModel<string> { Status = ResponseStatus.Fail };


    //        string total_fee = (money * 100).ToString("f0");
    //        int.TryParse(total_fee, out int _total_fee);
    //        JsApiPay jsApiPay = new JsApiPay();
    //        string openid = userId;

    //        WxPayData data = new WxPayData();
    //        data.SetValue("body", Subject);
    //        data.SetValue("attach", "BUYGOODS");
    //        data.SetValue("out_trade_no", orderCode);
    //        data.SetValue("total_fee", total_fee);
    //        data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
    //        //         data.SetValue("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));
    //        data.SetValue("time_expire", DateTime.Now.AddMinutes(timeOut ?? PayTimeOut).ToString("yyyyMMddHHmmss"));
    //        data.SetValue("goods_tag", "");
    //        data.SetValue("trade_type", "JSAPI");
    //        data.SetValue("openid", openid);
    //        data.SetValue("appid", AppId);
    //        data.SetValue("mch_id", MchId);//商户号
    //        //prepay_id
    //        if (!clientIp.IsNullOrEmpty())
    //        {
    //            data.SetValue("spbill_create_ip", clientIp);//终端ip	 
    //        }
    //        LogHelper.LogError("GetWeChatPayJavaScriptApiParam", "data=" + data.ToXml());
    //        //JSAPI支付预处理
    //        try
    //        {
    //            WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(data, NotifyUrl, Key);
    //            WritePostThridApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.WechatPay, "JsApiPay", data.ToXml(), DateTime.Now, unifiedOrderResult.ToXml(), DateTime.Now, true);
    //            response.BusinessData = jsApiPay.GetJsApiParameters(Key);//获取H5调起JS API参数   
    //            response.Status = ResponseStatus.Success;
    //            return response;
    //        }
    //        catch (WxPayFallException ex)
    //        {
    //            LogHelper.LogError("GetWeChatPayJavaScriptApiParam", "checkout_ex=" + ex.Message);
    //            response.ErrorMessage = ex.Message;
    //            return response;
    //        }
    //        catch (Exception ex)
    //        {
    //            LogHelper.LogError("GetWeChatPayJavaScriptApiParam", "checkout_ex=" + ex.ToString());
    //            response.ErrorMessage = "微信统一下单失败（" + ex.Message + "）";
    //            return response;
    //        }
    //    }

    //    #endregion
    //    /// <summary>
    //    /// 主动查询支付结果
    //    /// </summary>
    //    /// <param name="orderCode">平台交易单号</param>
    //    /// <returns></returns>
    //    public override BusinessBaseViewModel<string> QueryPayResult(string orderCode)
    //    { return null; }
    //    #region 浏览器环境支付
    //    /// <summary>
    //    /// 获取Wap微信支付订单参数
    //    /// </summary>
    //    /// <param name="orderCode">订单号</param>
    //    /// <param name="userId">付款用户</param>
    //    /// <param name="money">交易金额</param>
    //    ///  <param name="clientIp">客户端ip</param>
    //    /// <returns></returns>
    //    private BusinessBaseViewModel<string> GetWapWeChatPayApiParam(string orderCode, string userId, decimal money, string clientIp, int? timeOut)
    //    {
    //        BusinessBaseViewModel<string> response = new BusinessBaseViewModel<string> { Status = ResponseStatus.Fail };


    //        string total_fee = (money * 100).ToString("f0");
    //        int.TryParse(total_fee, out int _total_fee);

    //        WxPayData data = new WxPayData();
    //        data.SetValue("body", Subject);
    //        data.SetValue("attach", "BUYGOODS");
    //        data.SetValue("out_trade_no", orderCode);
    //        data.SetValue("total_fee", total_fee);
    //        data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
    //        //          data.SetValue("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));
    //        data.SetValue("time_expire", DateTime.Now.AddMinutes(timeOut ?? PayTimeOut).ToString("yyyyMMddHHmmss"));
    //        data.SetValue("goods_tag", "");
    //        data.SetValue("trade_type", "MWEB");
    //        data.SetValue("openid", userId);
    //        data.SetValue("appid", AppId);
    //        if (!clientIp.IsNullOrEmpty())
    //        {
    //            data.SetValue("spbill_create_ip", clientIp);//终端ip	 
    //        }
    //        var scene_info = new { h5_info = new { type = "Wap", wap_url = WechatConfig.WechatWebPath, wap_name = Subject } };
    //        data.SetValue("scene_info", JsonHelper.SerializeObject(scene_info));

    //        WxPayData wxdata = WxPayApi.UnifiedOrder(data, NotifyUrl);
    //        WritePostThridApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.WechatPay, "WxPayApi", data.ToXml(), DateTime.Now, wxdata.ToXml(), DateTime.Now, true);
    //        if (wxdata.GetValue("return_code").ToString() == "FAIL")
    //        {
    //            response.ErrorMessage = wxdata.GetValue("return_msg").ToString();
    //            return response;
    //        }

    //        if (wxdata.IsSet("result_code") && wxdata.GetValue("result_code").ToString() != "SUCCESS")
    //        {
    //            response.ErrorMessage = "生成失败";
    //            return response;
    //        }
    //        Dictionary<string, object> param = new Dictionary<string, object>();
    //        param.Add("appid", wxdata.GetValue("appid").ToString());
    //        param.Add("prepay_id", wxdata.GetValue("prepay_id").ToString());
    //        param.Add("mweb_url", wxdata.GetValue("mweb_url").ToString());
    //        response.Status = ResponseStatus.Success;
    //        response.BusinessData = JsonHelper.SerializeObject(param);
    //        response.ErrorMessage = "SUCCESS";
    //        return response;
    //    }

    //    #endregion

    //    #region APP环境支付
    //    /// <summary>
    //    /// 获取APP微信支付订单参数
    //    /// </summary>
    //    /// <param name="orderCode">订单号</param>
    //    /// <param name="userId">付款用户</param>
    //    /// <param name="money">交易金额</param>
    //    ///  <param name="clientIp">客户端ip</param>
    //    /// <returns></returns>
    //    private BusinessBaseViewModel<string> GetAppWeChatPayApiParam(string orderCode, string userId, decimal money, string clientIp, int? timeOut)
    //    {
    //        BusinessBaseViewModel<string> response = new BusinessBaseViewModel<string> { Status = ResponseStatus.Fail };


    //        var total_fee = (money * 100).ToInt();

    //        WxPayData data = new WxPayData();
    //        data.SetValue("appid", AppId);
    //        data.SetValue("mch_id", MchId);
    //        data.SetValue("body", Subject);
    //        data.SetValue("attach", "BUYGOODS");
    //        data.SetValue("out_trade_no", orderCode);
    //        data.SetValue("total_fee", total_fee);
    //        data.SetValue("trade_type", "APP");
    //        data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
    //        data.SetValue("time_expire", DateTime.Now.AddMinutes(timeOut ?? PayTimeOut).ToString("yyyyMMddHHmmss"));
    //        if (!clientIp.IsNullOrEmpty())
    //        {
    //            data.SetValue("spbill_create_ip", clientIp);//终端ip	 
    //        }
    //        //string key = WechatConfig.KeyByApp;
    //        //if(BrowserType == BrowserType.AppLife)
    //        //{
    //        //    key = WechatConfig.KeyByLifeApp;
    //        //}
    //        WxPayData wxdata = WxPayApi.UnifiedOrder(data, NotifyUrl, key: Key);
    //        WritePostThridApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.WechatPay, "WxPayApi APP", data.ToXml(), DateTime.Now, wxdata.ToXml(), DateTime.Now, true);
    //        if (wxdata.GetValue("return_code").ToString() == "FAIL")
    //        {
    //            response.ErrorMessage = wxdata.GetValue("return_msg").ToString();
    //            return response;
    //        }

    //        if (wxdata.IsSet("result_code") && wxdata.GetValue("result_code").ToString() != "SUCCESS")
    //        {
    //            response.ErrorMessage = "生成失败";
    //            return response;
    //        }
    //        WxPayData jsApiParam = new WxPayData();
    //        jsApiParam.SetValue("appid", wxdata.GetValue("appid").ToString());
    //        jsApiParam.SetValue("partnerid", wxdata.GetValue("appid").ToString());
    //        jsApiParam.SetValue("prepayid", wxdata.GetValue("prepay_id").ToString());
    //        jsApiParam.SetValue("package", "Sign=WXPay");
    //        jsApiParam.SetValue("noncestr", WxPayApi.GenerateNonceStr());
    //        jsApiParam.SetValue("timestamp", WxPayApi.GenerateTimeStamp());
    //        jsApiParam.SetValue("sign", jsApiParam.MakeSign(Key));
    //        var appPaymentInfo = new
    //        {
    //            PartnerId = wxdata.GetValue("appid").ToString(),
    //            PrepayId = wxdata.GetValue("prepay_id").ToString(),


    //            NonceStr = jsApiParam.GetValue("noncestr").ToString(),
    //            TimeStamp = jsApiParam.GetValue("timestamp").ToString(),
    //            PackageValue = jsApiParam.GetValue("package").ToString(),
    //            Sign = jsApiParam.GetValue("sign").ToString(),
    //        };

    //        response.Status = ResponseStatus.Success;
    //        response.BusinessData = JsonHelper.SerializeObject(appPaymentInfo);
    //        response.ErrorMessage = "SUCCESS";
    //        return response;
    //    }
    //    #endregion



    //    #region 退款
    //    /// <summary>
    //    /// 发起订单退款（此方法，以后应拆分2个方法处理退款，一个更新退款状态，另一个MQ调用微信退款申请）
    //    /// </summary>
    //    /// <param name="refundRequest">退款申请参数</param>
    //    /// <returns></returns>
    //    public override BusinessBaseViewModel<string> RefundPay(RefundBaseRequest refundRequest)
    //    {
    //        BusinessBaseViewModel<string> res = new BusinessBaseViewModel<string> { Status = ResponseStatus.Fail };

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

    //        var orderExt = OrderExtendRepository.FirstOrDefault(a => a.OrderCode == refundRequest.OrderCode);
    //        if (orderExt != null)
    //        {
    //            var appId = orderExt.ThreeAppId.ToUpper();
    //            if (appId == WechatConfig.AppIdByApp.ToUpper())
    //            {
    //                this.BrowserType = BrowserType.App;
    //            }
    //            else if (appId == WechatConfig.AppIdByLifeApp.ToUpper())
    //            {
    //                this.BrowserType = BrowserType.AppLife;
    //            }
    //            else if (appId == WechatConfig.AppIdBySamllApp.ToUpper())
    //            {
    //                this.BrowserType = BrowserType.SamllApp;
    //            }
    //        }

    //        //处理退款金额，不能超过支付金额
    //        var orderRefund = resCheck.BusinessData;
    //        var orderRefundCode = orderRefund.RefundRemark;
    //        orderRefund.RefundRemark = "";

    //        //2、实际退款请求
    //        string total_fee = (orderRefund.OrderAmount.GetValueOrDefault(0) * 100).ToString("f0");
    //        int.TryParse(total_fee, out int _total_fee);

    //        string refund_fee = (orderRefund.Amount.GetValueOrDefault(0) * 100).ToString("f0");
    //        int.TryParse(refund_fee, out int _refund_fee);

    //        WxPayData data = new WxPayData();
    //        data.SetValue("transaction_id", orderRefundCode);
    //        data.SetValue("out_trade_no", orderRefund.OrderCode);
    //        data.SetValue("total_fee", _total_fee);
    //        data.SetValue("refund_fee", _refund_fee);
    //        data.SetValue("refund_desc", "用户申请退款");
    //        data.SetValue("out_refund_no", orderRefund.RefundCode);
    //        data.SetValue("appid", AppId);
    //        data.SetValue("mch_id", MchId);

    //        //2.1、实际退款请求
    //        var result = WxPayApi.Refund(data, key: Key);

    //        //2.2、记录请求日志
    //        //记录微信退款调用的日志
    //        string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
    //        WritePostThridApi(ThirdPlatformBusinessType.Payment, orderRefund.OrderCode, ThirdPlatformType.WechatPay, url, data.ToXml(), DateTime.Now, result.ToXml(), DateTime.Now, true);

    //        //3、退款结果验证
    //        if (result.GetValue("return_code").ToString() != "SUCCESS")
    //        {
    //            orderRefund.RefundRemark = result.GetValue("return_msg").ToString();
    //            res = UpdateOrderRefundStatus(orderRefund, OrderRefundStatusType.Fail);
    //            if (res.Status != ResponseStatus.Success)
    //            {
    //                return res;
    //            }
    //            res.ErrorMessage = string.Format("{0}（{1}）", result.GetValue("return_msg").ToString(), result.GetValue("return_code").ToString());
    //            //res.Status = ResponseStatus.BusinessError;
    //            return res;
    //        }
    //        if (result.GetValue("result_code").ToString() != "SUCCESS")
    //        {
    //            orderRefund.RefundRemark = result.GetValue("err_code_des").ToString();
    //            res = UpdateOrderRefundStatus(orderRefund, OrderRefundStatusType.Fail);
    //            if (res.Status != ResponseStatus.Success)
    //            {
    //                return res;
    //            }
    //            res.ErrorMessage = string.Format("{0}（{1}）", result.GetValue("err_code_des").ToString(), result.GetValue("result_code").ToString());
    //            //res.Status = ResponseStatus.BusinessError;
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
