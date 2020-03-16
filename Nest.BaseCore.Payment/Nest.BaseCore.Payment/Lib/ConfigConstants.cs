using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment
{
    public class ConfigConstants
    {
        /// <summary>
        /// 是否启用中金支付
        /// </summary>
        public const string IsOpenAcsPayment = "IsOpenAcsPayment";
        /// <summary>
        /// 中金支付回调通知地址
        /// </summary>
        public const string AcsPaymentNotifyUrl = "AcsPaymentNotifyUrl";
        /// <summary>
        /// 微信支付回调通知地址
        /// </summary>
        public const string WeixinPaymentNotifyUrl = "WeixinPaymentNotifyUrl";
        /// <summary>
        /// 支付宝支付回调通知地址
        /// </summary>
        public const string AliPaymentNotifyUrl = "AliPaymentNotifyUrl";
        /// <summary>
        /// 支付宝支付同步回调地址
        /// </summary>
        public const string AliPaymentResultUrl = "AliPaymentResultUrl";

        /// <summary>
        /// 接口服务配置APPID
        /// </summary>
        public const string WebApiAppId = "WebApi.AppId";
        /// <summary>
        /// 接口服务配置APPSecret
        /// </summary>
        public const string WebApiAppSecret = "WebApi.AppSecret";

        /// <summary>
        /// 支付超时时间 
        /// </summary>
        public const string PayTimeOut = "PayTimeOut";
    }
}
