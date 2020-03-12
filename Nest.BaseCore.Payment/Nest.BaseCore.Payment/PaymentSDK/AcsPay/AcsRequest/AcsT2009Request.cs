using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT2009Request: AcsPayBaseRequest
    {
        /// <summary>
        /// 报文节点  CltAcc
        /// </summary>
        public AcsPayAccNode CltAcc { get; set; }
        /// <summary>
        /// 报文节点 BkAcc
        /// </summary>
        public AcsT2009BkAccNode BkAcc { get; set; }
        /// <summary>
        /// 报文节点 Amt
        /// </summary>
        public AcsT2009AmtNode Amt { get; set; }
        /// <summary>
        /// 资金用途(附言) 
        /// </summary>
        public string Usage { get; set; }
        /// <summary>
        /// 业务标示 
        ///A00 正常出金
        ///B01 解冻资金后，再出金
        /// </summary>
        public string TrsFlag { get; set; }
        /// <summary>
        /// 结算方式标示 
        ///AA=正常结算(默认)
        ///T0=T0 代付出金
        ///T1=T1 代付出金
        /// </summary>
        public string BalFlag { get; set; }
        /// <summary>
        /// 报文节点 Srl
        /// </summary>
        public AcsT2009SrlNode Srl { get; set; }
    } 
}
