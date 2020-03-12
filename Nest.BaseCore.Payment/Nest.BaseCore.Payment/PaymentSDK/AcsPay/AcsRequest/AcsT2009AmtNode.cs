using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT2009AmtNode
    {
        /// <summary>
        /// 发生额
        /// </summary>
        public string AclAmt { get; set; }
        /// <summary>
        /// 转账手续费
        /// </summary>
        public string FeeAmt { get; set; }
        /// <summary>
        /// 总金额(发生额+转账手续费) 
        /// </summary>
        public string TAmt { get; set; }
        /// <summary>
        /// 币种，默认“CNY” 
        /// </summary>
        public string CcyCd { get; set; }
    }
}
