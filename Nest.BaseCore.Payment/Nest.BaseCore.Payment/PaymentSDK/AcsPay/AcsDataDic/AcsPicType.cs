using System.ComponentModel;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsDataDic
{
    /// <summary>
    /// 影像资料类型 
    /// </summary>
    public enum AcsPicType
    {
        /// <summary>
        /// 身份证正面
        /// </summary>
        [Description("1101")]
        IdentityFace=0,
        /// <summary>
        /// 身份证反面
        /// </summary>
        [Description("1102")]
        IdentityBack = 1,
        /// <summary>
        /// 营业执照/统一社会信用代码证
        /// </summary>
        [Description("1201")]
        Other = 2
    }
}
