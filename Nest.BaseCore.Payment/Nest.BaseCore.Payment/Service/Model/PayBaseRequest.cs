using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.Service
{

    public class PayBaseRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string orderCode { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal money { get; set; }
        /// <summary>
        /// 支付密码
        /// </summary>
        public string PayPassword { get; set; }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string clientIp { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal totalAmount { get; set; }
        /// <summary>
        /// 商家-中金商户号
        /// </summary>
        public string subNo { get; set; }
        /// <summary>
        /// 商家-中金商户名称
        /// </summary>
        public string cltNm { get; set; }
        /// <summary>
        /// 订单支付超时时间
        /// </summary>
        public int? PaytimeOut { get; set; }
    }
}
