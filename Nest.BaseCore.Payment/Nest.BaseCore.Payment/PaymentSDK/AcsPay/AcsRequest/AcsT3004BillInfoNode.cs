using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT3004BillInfoNode
    {
        /// <summary>
        /// 付款方资金账号 
        /// </summary>
        public string PSubNo { get; set; }
        /// <summary>
        /// 付款方户名 
        /// </summary>
        public string PNm { get; set; }
        /// <summary>
        /// 收款方资金账号
        /// </summary>
        public string RSubNo { get; set; }
        /// <summary>
        /// 收款方户名 
        /// </summary>
        public string RCltNm { get; set; }
        /// <summary>
        /// 业务单号 
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 支付单号(唯一)
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 支付金额 
        /// </summary>
        public string AclAmt { get; set; }
        /// <summary>
        /// 付款方手续费,暂定 0
        /// </summary>
        public string PayFee { get; set; }
        /// <summary>
        /// 收款方手续费,暂定 0 
        /// </summary>
        public string PayeeFee { get; set; }
        /// <summary>
        /// 币种，默认“CNY” 
        /// </summary>
        public string CcyCd { get; set; }
        /// <summary>
        /// 资金用途(附言) 
        /// </summary>
        public string Usage { get; set; }
    }
}
