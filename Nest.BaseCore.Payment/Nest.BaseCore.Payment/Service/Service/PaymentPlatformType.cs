using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.Service
{
    /// <summary>
    /// 支付通道方式
    /// </summary>
    public enum PaymentPlatformType
    {
        /// <summary>
        /// 微信支付
        /// </summary>
        [Description("微信支付")]
        Wechat =1,
        /// <summary>
        /// 支付宝支付
        /// </summary>
        [Description("支付宝支付")]
        Alipay = 2,
        /// <summary>
        /// 中金支付
        /// </summary>
        [Description("中金支付")]
        Zhongjin = 3,
        /// <summary>
        /// 余额支付
        /// </summary>
        [Description("余额支付")]
        Blance = 4
    }
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// 线上支付
        /// </summary>
        [Description("线上支付")]
        Online =1,
        /// <summary>
        /// 线下支付
        /// </summary>
        [Description("线下支付")]
        Offline =2,
    }
}
