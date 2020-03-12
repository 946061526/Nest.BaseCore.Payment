using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT3008Request: AcsPayBaseRequest
    {
        /// <summary>
        /// 报文节点  <MSGHD>
        /// </summary>
        //public AcsPayHeadNode MSGHD { get; set; }
        /// <summary>
        /// 报文节点 <CltAcc>
        /// </summary>
        public AcsPayAccNode CltAcc { get; set; }

       public AcsT3008BillInfoNode billInfo { get; set; }
        /// <summary>
        /// 发送端标记:0 手机;1PC 端
        /// </summary>
        public string ReqFlg { get; set; }
        /// <summary>
        ///  页面通知 URL
        /// </summary>
        public string NotificationURL { get; set; }
        /// <summary>
        /// 后台通知 URL 若不传值则默认按照后台配置的地址进行通知交易结果
        /// </summary>
        public string ServNoticURL { get; set; }
        /// <summary>
        ///  资金用途(附言)
        /// </summary>
        public string Usage { get; set; }
        /// <summary>
        /// 合作方自定义备注 1 SecPayType =6 POS 线下订单可填写操作员编号
        /// </summary>
        public string DRemark1 { get; set; }
        /// <summary>
        /// 合作方自定义备注 2 SecPayType =6 POS 线下订单可填写商户门店编号
        /// </summary>
        public string DRemark2 { get; set; }
        /// <summary>
        /// 合作方自定义备注 3
        /// </summary>
        public string DRemark3 { get; set; }
        /// <summary>
        /// 合作方自定义备注 4
        /// </summary>
        public string DRemark4 { get; set; }
        /// <summary>
        /// 合作方自定义备注 5
        /// </summary>
        public string DRemark5 { get; set; }
        /// <summary>
        /// 合作方自定义备注 6
        /// </summary>
        public string DRemark6 { get; set; }
        /// <summary>
        /// 业务标示
        ///A00 普通收款B00 收款方收款成功后，再冻结资
        /// </summary>
        public string TrsFlag { get; set; }
    }


}
