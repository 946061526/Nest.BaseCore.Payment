using System.ComponentModel;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsDataDic
{
    public enum AcsInvokedType
    {
        /// <summary>
        /// 发起方 
        /// </summary>
        [Description("F")]
        Soway = 0,
        /// <summary>
        /// 发起方 中金
        /// </summary>
        [Description("R")]
        Acs = 1
      
    }
}
