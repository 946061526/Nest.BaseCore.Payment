using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest
{
    public class AcsT1004BkAccNode
    {
        /// <summary>
        /// 银行编号(详见《银行编码.pdf》
        /// </summary>
        public string BkId { get; set; }
        /// <summary>
        ///  银行账号(卡号)
        /// </summary>
        public string AccNo { get; set; }
        /// <summary>
        ///  开户名称 
        /// </summary>
        public string AccNm { get; set; }
        /// <summary>
        /// 账户类型(1: 对公; 2: 对私)
        /// </summary>
        public string AccTp { get; set; }
        /// <summary>
        /// 银行账户(卡)类型 
        ///1:个人借记卡(储蓄卡)，默认 
        ///2:个人贷记卡(信用卡)
        ///3:个人电子账户(银行二类户)
        ///A:企业一般结算账户，默认
        ///B:企业电子账户
        /// </summary>
        public string CrdTp { get; set; }
        /// <summary>
        /// 开户证件类型(详见数据字典) 
        /// </summary>
        public string CdTp { get; set; }
        /// <summary>
        /// 证件号码 
        /// </summary>
        public string CdNo { get; set; }
        /// <summary>
        /// 银行预留手机号码 个人银行卡时必填
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 跨行标示(1:本行;2:跨行) 默认 2 跨行
        /// </summary>
        public string CrsMk { get; set; }
        /// <summary>
        /// 开户网点编号(人行分配的12 位编号)-详见《数据字典-开户行信息.rar》 
        ///资金托管方是银行且跨行标示 = 2 跨行时，此字段必填；资金托管方不是银行时，此字段可不填
        /// </summary>
        public string OpenBkCd { get; set; }
        /// <summary>
        /// 开户网点名称-详见《数据字典-开户行信息.rar》 资金托管方是银行且跨行标示 = 2 跨行时，此字段必填；
        ///资金托管方不是银行时，此字段可不填
        /// </summary>
        public string OpenBkNm { get; set; }
        /// <summary>
        /// 开户网点省份编号-详见《数据字典-行政区域.rar》 资金托管方是银行时，此字段可不填；资金托管方不是银行且跨行标示 = 2 跨行时填值
        /// </summary>
        public string PrcCd { get; set; }
        /// <summary>
        /// 开户网点省份名称-详见《数据字典-行政区域.rar》 资金托管方是银行时，此字段可不填；资金托管方不是银行且跨行标示 = 2 跨行时填值
        /// </summary>
        public string PrcNm { get; set; }
        /// <summary>
        /// 开户网点城市编号-详见《数据字典-行政区域.rar》 资金托管方是银行时，此字段可不填；资金托管方不是银行且跨行标示 = 2 跨行时填值
        /// </summary>
        public string CityCd { get; set; }
        /// <summary>
        /// 开户网点城市名称-详见《数据字典-行政区域.rar》 资金托管方是银行时，此字段可不填；资金托管方不是银行且跨行标示 = 2 跨行时填值
        /// </summary>
        public string CityNm { get; set; }
    }
}
