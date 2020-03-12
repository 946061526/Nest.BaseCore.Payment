using System.ComponentModel;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsDataDic
{
    /// <summary>
    /// ACS 证件类型
    /// </summary>
    public enum AcsCredentialsType
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("A")]
        Identity = 0,
        /// <summary>
        /// 户口簿
        /// </summary>
        [Description("B")]
        House = 1,
        /// <summary>
        /// 军官证
        /// </summary>
        [Description("C")]
        Pla = 2,
        /// <summary>
        /// 警官证
        /// </summary>
        [Description("D")]
        Police = 3,
        /// <summary>
        /// 护照
        /// </summary>
        [Description("E")]
        Passport = 4,
        /// <summary>
        /// 港澳通行证
        /// </summary>
        [Description("F")]
        HKandMacau = 5,
        /// <summary>
        /// 社会统一信用代码证
        /// </summary>
        [Description("G")]
        Insurance = 6,
        /// <summary>
        /// 营业执照 
        /// </summary>
        [Description("H")]
        Business = 7,
        /// <summary>
        ///  文职干部证
        /// </summary>
        [Description("I")]
        Cadre = 8,
        /// <summary>
        /// 士兵证 
        /// </summary>
        [Description("J")]
        Soldiers = 9,
        /// <summary>
        /// 台湾通行证 
        /// </summary>
        [Description("K")]
        Taiwan = 10,
        /// <summary>
        /// 其他  
        /// </summary>
        [Description("L")]
        Other = 11,
    }
}
