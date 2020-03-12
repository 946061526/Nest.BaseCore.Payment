using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.Service
{
    /// <summary>
    /// 支付处理结果响应
    /// </summary>
    public class PaymentReponse
    {
        /// <summary>
        /// 是否成功（true=成功，false=失败）
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 状态编码
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// 响应内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
