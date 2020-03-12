using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT1004Request: AcsPayBaseRequest
    {
        /// <summary>
        /// CltAcc报文节点
        /// </summary>
        public AcsT1004CltAccNode CltAcc { get; set; }
        /// <summary>
        /// BkAcc 报文节点
        /// </summary>
        public AcsT1004BkAccNode BkAcc { get; set; }
        /// <summary>
        /// Srl 报文节点
        /// </summary>
        public AcsT2009SrlNode Srl { get; set; }
        /// <summary>
        /// 业务功能标示(1:绑定、2：变更、3：删除
        /// </summary>
        public string FcFlg { get; set; }
    }
}
