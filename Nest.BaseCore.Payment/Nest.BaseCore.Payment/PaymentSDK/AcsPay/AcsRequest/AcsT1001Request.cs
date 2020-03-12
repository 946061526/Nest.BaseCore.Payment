using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT1001Request: AcsPayBaseRequest
    {
        /// <summary>
        /// CltAcc 节点
        /// </summary>
        public AcsT1001CltAccNode CltAcc { get; set; }
        /// <summary>
        /// Clt 节点
        /// </summary>
        public AcsT1001CltNode Clt { get; set; }
        /// <summary>
        /// Srl 节点
        /// </summary>
        public AcsT2009SrlNode Srl { get; set; }
        /// <summary>
        /// 业务功能标示(1:开户、2：修改、3：销户) 
        /// </summary>
        public string FcFlg { get; set; }
        /// <summary>
        /// 账户类型( 
        ///1:客户资金账户 
        ///2:合作方备付金账户 
        ///3:合作方收益账户)
        /// </summary>
        public string AccTp { get; set; }
    }
}
