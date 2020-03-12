using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT2009BkAccNode
    {
        /// <summary>
        /// 银行账号(卡号) 
        /// </summary>
        public string AccNo { get; set; }
        /// <summary>
        /// 开户名称 
        /// </summary>
        public string AccNm { get; set; }
    }
}
