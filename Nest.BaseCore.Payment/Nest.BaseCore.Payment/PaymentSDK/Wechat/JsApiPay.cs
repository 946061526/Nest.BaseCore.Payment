using System;

namespace Nest.BaseCore.Payment.PaymentSDK.Wechat
{
    public class JsApiPay
    {
        /// <summary>
        /// 统一下单接口返回结果
        /// </summary>
        public WxPayData unifiedOrderResult { get; set; }


        /// <summary>
        /// 调用统一下单，获得下单结果
        /// </summary>
        /// <returns> 统一下单结果 失败时抛异常WxPayException</returns>
        public WxPayData GetUnifiedOrderResult(string orderCode,string openId,string totalFee,string notifyUrl)
        {
            //统一下单
            WxPayData data = new WxPayData();
            data.SetValue("body", "test");
            data.SetValue("attach", "test");
            data.SetValue("out_trade_no", orderCode);
            data.SetValue("total_fee", totalFee);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            data.SetValue("goods_tag", "test");
            data.SetValue("trade_type", "JSAPI");
            data.SetValue("openid", openId);

            WxPayData result = WxPayApi.UnifiedOrder(data, notifyUrl);
            if (result.IsSet("return_code") && result.GetValue("return_code").ToString().Equals("FAIL"))
            {
                //出错
                throw new WxPayFallException(result.GetValue("return_msg").ToString());
            }
            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
            {
                Log.Error(this.GetType().ToString(), "UnifiedOrder response error!");
                throw new WxPayException("UnifiedOrder response error!");
            }

            unifiedOrderResult = result;
            return result;
        }
      
        /// <summary>
        /// 调用统一下单，获得下单结果
        /// </summary>
        /// <param name="data"></param>
        /// <returns>统一下单结果,失败时抛异常WxPayException</returns>
        public WxPayData GetUnifiedOrderResult(WxPayData data, string notifyUrl,string key=WechatConfig.Key)
        {
            //统一下单
            WxPayData result = WxPayApi.UnifiedOrder(data, notifyUrl, key:key);
            if (result.IsSet("return_code") && result.GetValue("return_code").ToString().Equals("FAIL"))
            {
                //出错
                throw new WxPayException(result.GetValue("return_msg").ToString());
            }
           
            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
            {
                Log.Error(this.GetType().ToString(), "UnifiedOrder response error!");
                throw new WxPayException("ex:" + result.IsSet("appid") + result.IsSet("prepay_id") + (result.IsSet("prepay_id")?result.GetValue("prepay_id").ToString():""));
            }
            unifiedOrderResult = result;
            return result;
        }
        /// <summary>
        ///  从统一下单成功返回的数据中获取微信浏览器调起jsapi支付所需的参数，
        /// 微信浏览器调起JSAPI时的输入参数格式如下：
        /// {
        ///   "appId" : "wx2421b1c4370ec43b",     //公众号名称，由商户传入     
        ///    "timeStamp":" 1395712654",         //时间戳，自1970年以来的秒数     
        ///   "nonceStr" : "e61463f8efa94090b1f366cccfbbb444", //随机串     
        ///    "package" : "prepay_id=u802345jgfjsdfgsdg888",     
        ///    "signType" : "MD5",         //微信签名方式:    
        ///    "paySign" : "70EA570631E4BB79628FBCA90534C63FF7FADD89" //微信签名 
        /// }
        /// </summary>
        /// <returns>
        /// 微信浏览器调起JSAPI时的输入参数，json格式可以直接做参数用
        /// 更详细的说明请参考网页端调起支付API：http://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_7
        /// </returns>
        public string GetJsApiParameters(string key=WechatConfig.Key)
        {
            string parameters = string.Empty;
            try
            {
                WxPayData jsApiParam = new WxPayData();
                jsApiParam.SetValue("appId", unifiedOrderResult.GetValue("appid"));
                jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
                jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
                jsApiParam.SetValue("package", "prepay_id=" + unifiedOrderResult.GetValue("prepay_id"));
                jsApiParam.SetValue("signType", "MD5");
                jsApiParam.SetValue("paySign", jsApiParam.MakeSign(key));
                parameters = jsApiParam.ToJson();
                
            }
            catch (Exception ex)
            {
                //LogHelper.LogError("JsApiPay", "JsApiPay GetJsApiParameters :Err " + ex.Message);

            }
            return parameters;
        }
        public string GetH5ApiParameters()
        {
            string parameters = string.Empty;
            try
            {
                WxPayData jsApiParam = new WxPayData();
                jsApiParam.SetValue("appid", unifiedOrderResult.GetValue("appid"));
                jsApiParam.SetValue("partnerid", "1388358202");
                jsApiParam.SetValue("prepayid", unifiedOrderResult.GetValue("prepay_id"));
                jsApiParam.SetValue("package", "Sign=WXPay");
                jsApiParam.SetValue("noncestr", WxPayApi.GenerateNonceStr());
                jsApiParam.SetValue("timestamp", WxPayApi.GenerateTimeStamp());
                jsApiParam.SetValue("sign", jsApiParam.MakeSign());
                
                parameters = jsApiParam.ToUrl1();
            }
            catch (Exception ex)
            {
                //LogHelper.LogError("JsApiPay", "JsApiPay GetH5ApiParameters :Err " + ex.Message);
            }
            return parameters;
        }

      
    }
}