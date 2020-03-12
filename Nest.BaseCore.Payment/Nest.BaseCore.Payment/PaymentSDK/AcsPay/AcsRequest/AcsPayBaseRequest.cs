using Nest.BaseCore.Payment.Service;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsPayBaseRequest: PayBaseRequest
    {
        /// <summary>
        /// 报文节点  <MSGHD>
        /// </summary>
        public AcsPayHeadNode MSGHD { get; set; }
    }
}
