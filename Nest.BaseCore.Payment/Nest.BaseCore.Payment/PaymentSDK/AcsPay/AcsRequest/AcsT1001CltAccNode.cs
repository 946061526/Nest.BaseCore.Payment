using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT1001CltAccNode
    {
        /// <summary>
        /// 客户号（客户在合作方系统中的唯一编号)
        /// </summary>
        public string CltNo { get; set; }
        /// <summary>
        /// 资金账号 FcFlg=2/3 时必填
        /// </summary>
        public string SubNo { get; set; }
        /// <summary>
        /// 户名
        /// </summary>
        public string CltNm { get; set; }
    }
}
