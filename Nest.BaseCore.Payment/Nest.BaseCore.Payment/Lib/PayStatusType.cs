using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment
{
    /// <summary>
    /// 订单支付状态
    /// </summary>
    public enum PayStatusType
    {
        /// <summary>
        /// 待支付
        /// </summary>
        [Description("待支付")]
        UnPay = 0,
        /// <summary>
        /// 已支付
        /// </summary>
        [Description("已支付")]
        Paid = 1,
        /// <summary>
        /// 取消支付
        /// </summary>
        [Description("取消支付")]
        CancelPay = 2,
        /// <summary>
        /// 超时未付
        /// </summary>
        [Description("超时未付")]
        TimeOut = 3,
        /// <summary>
        /// 已退款
        /// </summary>
        [Description("已退款")]
        Refund = 4,

    }
}
