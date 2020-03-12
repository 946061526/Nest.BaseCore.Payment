using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT2012Request:AcsPayBaseRequest
    {
        /// <summary>
        /// 原出入金交易的合作方交易流水号
        /// </summary>
        public string OrgSrl { get; set; }
    }
}
