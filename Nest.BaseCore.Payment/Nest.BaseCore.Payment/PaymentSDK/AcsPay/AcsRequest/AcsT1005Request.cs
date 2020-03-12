using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT1005Request: AcsPayBaseRequest
    {
        /// <summary>
        /// 请求报文节点 CltAcc
        /// </summary>
        public AcsT1004CltAccNode CltAcc { get; set; }
    }
}
