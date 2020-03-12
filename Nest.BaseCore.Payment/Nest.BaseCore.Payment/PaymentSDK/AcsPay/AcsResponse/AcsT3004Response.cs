using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsResponse
{
    public class AcsT3004Response
    {
        public AcsPayBaseMsghd Msghd { get; set; }
        public AcsT3004SrlNode Srl { get; set; }

    }
}
