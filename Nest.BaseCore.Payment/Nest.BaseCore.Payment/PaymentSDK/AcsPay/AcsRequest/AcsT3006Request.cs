using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT3006Request: AcsPayBaseRequest
    {
        /// <summary>
        /// 待查询支付单号(唯一) 
        /// </summary>
        public string BillNo { get; set; }
    }
}
