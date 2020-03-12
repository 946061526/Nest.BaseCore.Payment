using Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsResponse
{
    public class AcsT3010Response
    {

        public AcsPayBaseMsghd Msghd { get; set; }
        public AcsPayAccNode CltAcc { get; set; }
        public AcsPayT3010BillInfoResponse BillInfo { get; set; }
        public string PayType { get; set; }
        public string SecPayType { get; set; }
        public string TrsFlag { get; set; }
        public string RestTime { get; set; }
        public string Usage { get; set; }
        public string DRemark1 { get; set; }
        public string DRemark2 { get; set; }
        public string DRemark3 { get; set; }
        public string DRemark4 { get; set; }
        public string DRemark5 { get; set; }
        public string DRemark6 { get; set; }
        public string FDate { get; set; }
        public string FTime { get; set; }
        public string Spec1 { get; set; }
        public string Spec2 { get; set; }
    }
}
