using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.Service
{
    /// <summary>
    /// 支付业务异常
    /// </summary>
    public class PaymentException : Exception
    {
        /// <summary>
        /// 实例化异常
        /// </summary>
        /// <param name="message">异常消息</param>
        public PaymentException(string message) : base(message)
        {
        }
        /// <summary>
        /// 实例化异常
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="exception">异常对象</param>
        public PaymentException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
