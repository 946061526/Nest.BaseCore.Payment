using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.Service
{
    /// <summary>
    /// 退款请求实体
    /// </summary>
    public class RefundBaseRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundMoney { get; set; }
        /// <summary>
        /// 退款原因
        /// </summary>
        public string ReasonDicCodes { get; set; }
        /// <summary>
        /// 退款备注
        /// </summary>
        public string RefundRemark { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public string LinkMan { get; set; }
        /// <summary>
        /// 申请人电话
        /// </summary>
        public string Phone { get; set; }
    }
}
