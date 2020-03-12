namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{

    public class AcsPayHeadNode
    {
        /// <summary>
        /// 交易码
        /// </summary>
        public string TrCd { get; set; }
        /// <summary>
        /// 交易日期 YYYYMMDD
        /// </summary>
        public string TrDt { get; set; }
        /// <summary>
        /// 交易时间 HH24MISS
        /// </summary>
        public string TrTm { get; set; }
        /// <summary>
        /// 交易发起方(详见数据字典)
        /// </summary>
        public string TrSrc { get; set; }
        /// <summary>
        /// 合作方编号
        /// </summary>
        public string PtnCd { get; set; }
        /// <summary>
        /// 托管方编号
        /// </summary>
        public string BkCd { get; set; }
    }
}
