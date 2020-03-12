using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT3010Request: AcsPayBaseRequest
    {
    //    public AcsPayHeadNode MSGHD { get;set; }
        public AcsT3010BillInfoNode billInfo { get; set; }
    }
}
