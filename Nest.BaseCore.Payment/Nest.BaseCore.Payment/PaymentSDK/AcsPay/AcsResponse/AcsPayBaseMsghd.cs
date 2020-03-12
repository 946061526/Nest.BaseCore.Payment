using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay
{
    public class AcsPayBaseMsghd
    {
        /// <summary>
        /// 返回码(000000 成功)
        /// </summary>
        public string RspCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string RspMsg { get; set; }
    }
}
