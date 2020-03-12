using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsResponse
{
    public class AcsT3008Response
    {
        public AcsPayBaseMsghd Msghd { get; set; }
        /// <summary>
        ///  返回地址
        ///PayType=2 时网银跳转地址
        ///PayType=6 时为二维码的
        ///CODE 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// PayType=6 时返回二维码图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// [PayType=5 且
        ///KJSMSFlg=2]或
        ///[PayType = 3 / 7] 时返回交
        ///易结果:
        ///1 成功
        ///2 失败
        ///3 处理中
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// 平台交易流水号
        /// </summary>
        public class Srl {
            public string PlatSrl { get; set; }
        }

    }
}
