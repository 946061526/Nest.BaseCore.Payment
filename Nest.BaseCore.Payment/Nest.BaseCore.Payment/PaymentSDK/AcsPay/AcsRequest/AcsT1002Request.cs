using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT1002Request: AcsPayBaseRequest
    {
        /// <summary>
        /// 开户时合作方交易流水号
        /// </summary>
        public string SrcSrl { get; set; }
    }
}
