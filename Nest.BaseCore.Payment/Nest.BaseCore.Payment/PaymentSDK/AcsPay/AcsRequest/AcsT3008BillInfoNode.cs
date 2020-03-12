using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT3008BillInfoNode
    {
        #region 报文节点 billInfo
        /// <summary>
        /// 付款方标示
        /// </summary>
        public string PsubNo { get; set; }
        /// <summary>
        /// 付款方名称
        /// </summary>
        public string Pnm { get; set; }
        /// <summary>
        /// 支付单号(唯一)
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 业务单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public string BillAmt { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string AclAmt { get; set; }
        /// <summary>
        /// 币种，默认"CNY"
        /// </summary>
        public string CcyCd { get; set; }
        /// <summary>
        /// 支付方式：
        ///2：网银
        ///5：快捷支付
        ///6：正扫支付
        ///7：反扫支付
        ///8：公众号支付
        ///9：银联无卡支付
        ///A：手机 APP 跳转支付
        ///P: POS 支付
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        ///  支付方式二级分类
        ///1：企业网银
        /// PayType = 2 必输
        ///2：个人网银
        ///PayType = 2 必输
        ///3：支付宝
        ///PayType = 6 / 7 / 8 / A 必输
        ///4：微信
        ///PayType = 6 / 7 / 8 / A 必输
        ///5：银联 PayType = 6 必输
        ///6：POS 线下订单
        ///PayType=P 必输
        ///7：POS O2O 支付订单
        ///PayType = P 必输
        /// </summary>
        public string SecPayType { get; set; }
        /// <summary>
        /// 小程序标识 PayType=8 时必输 微信： 0 非小程序 1 小程序
        /// </summary>
        public string MiniTag { get; set; }
        /// <summary>
        /// 付款银行编号 PayType=2 时必输
        /// </summary>
        public string BankID { get; set; }
        /// <summary>
        /// 原快捷绑卡交易流水号
        /// PayType=5 时必输
        /// 可通过 T4001/T3011 交易完
        /// 成绑卡操作
        /// 原绑定设备的流水号
        /// PayType =P 时必输，
        /// SecPayType =6 POS 线下订单
        /// </summary>
        public string KJBndSrl { get; set; }
        /// <summary>
        /// 快捷业务是否需要短信确认
        ///1：需要
        ///2：不需要
        ///PayType = 5 时必输
        /// </summary>
        public string KJSMSFlg { get; set; }
        /// <summary>
        /// 订单标题 PayType=6/7/8/A 时必输
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 商品描述（微信平台配置的商品标记，用于满减和优惠券）
        ///PayType=6/7/8/A 时必输
        ///注意：PayType=8 时该字段长度不超过 30；PayType=A时该字段超度不超过 60
        /// </summary>
        public string GoodsDesc { get; set; }
        /// <summary>
        /// 用户 ID
        /// PayType=8 时必输
        /// 微信：openid
        /// 支付宝：buyer_user_id
        /// PayType = P 且 SecPayType = 7时 必输 POS 商户号 PayType=A 填写 app 的AppId
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 支付授权码 PayType=7 时必输
        /// </summary>
        public string PAuthCode { get; set; }
        #endregion
    }
}
