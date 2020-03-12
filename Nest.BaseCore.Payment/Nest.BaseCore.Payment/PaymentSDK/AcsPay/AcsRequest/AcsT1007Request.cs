using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT1007Request: AcsPayBaseRequest
    {
        /// <summary>
        /// CltAcc报文节点
        /// </summary>
        public AcsT1004CltAccNode CltAcc { get; set; }
        /// <summary>
        /// Srl 报文节点
        /// </summary>
        public AcsT2009SrlNode Srl { get; set; }
    }
}
