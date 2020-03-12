using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT3004Request: AcsPayBaseRequest
    {
        /// <summary>
        /// 报文节点  <MSGHD>
        /// </summary>
        //public AcsPayHeadNode MSGHD { get; set; }
        /// <summary>
        ///   报文节点  billInfo
        /// </summary>
        public AcsT3004BillInfoNode billInfo { get; set; }
        /// <summary>
        /// 业务标示 A00 普通订单支付 B00 收款方收款成功后，再冻结资金
        ///    B01 付款方解冻资金后，再支付给收款方
        /// </summary>
        public string TrsFlag { get; set; }
    }
}
