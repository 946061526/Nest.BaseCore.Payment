namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsPayAccNode
    {
        /// <summary>
        /// 收款方资金账号
        /// </summary>
        public string SubNo { get; set; }
        /// <summary>
        /// 收款方名称
        /// </summary>
        public string CltNm { get; set; }
    }
}
