using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.PaymentSDK
{
    public class AcsPayConfig
    {
        #region 测试配置
        ///// <summary>
        ///// 合作方编号(由融资平台分配) -- 根据实际情况更改
        ///// </summary>
        ////public const string ptncode = "SWWL2074";

        ///// <summary>
        ///// 托管方编号(由融资平台分配) -- 根据实际情况更改
        ///// </summary>
        //public const string bkcode = "XNYH8888";
        //public const string password = "123456";
        ///// <summary>
        ///// 融资平台融资接口地址
        ///// </summary>
        ////public const string acsTrzUrl = "http://acs.51zhir.cn/acswk/interfaceI.htm";
        ///// <summary>
        ///// 融资平台交易接口地址 (中金客服说两个地址都可以用,但一般用下面这个)
        ///// </summary>
        //public const string acsTrdUrl = "https://zhirong.cpcn.com.cn/acswk/interfaceII.htm";
        ////public const string acsTrdUrl = "http://acs.51zhir.cn/acswk/interfaceII.htm";
        #endregion

        //#if debug
        ////正式配置
        /// <summary>
        /// 合作方编号(由融资平台分配) -- 根据实际情况更改
        /// </summary>
        public const string ptncode = "DGSW2018";
        /// <summary>
        /// 托管方编号(由融资平台分配) -- 根据实际情况更改
        /// </summary>
        public const string bkcode = "XNYH8888";
        public const string password = "123456";
        /// <summary>
        /// 融资平台交易接口地址 
        /// </summary>
        public const string acsTrdUrl = "https://zhirong.cpcn.com.cn/acswk/interfaceII.htm";

        ////#else
        //测试配置
        ///// <summary>
        ///// 合作方编号(由融资平台分配) -- 根据实际情况更改
        ///// </summary>
        //public const string ptncode = "SWWL2074";
        ///// <summary>
        ///// 托管方编号(由融资平台分配) -- 根据实际情况更改
        ///// </summary>
        //public const string bkcode = "XNYH8888";
        //public const string password = "1";
        ///// <summary>
        ///// 融资平台交易接口地址
        ///// </summary>
        //public const string acsTrdUrl = "http://acs.51zhir.cn/acswk/interfaceII.htm";

        //#endif

        #region 公司正式账号配置
        //公司账号
        public const string CltNo = "15338358980";  //客户号
        public const string CltPid = "1820015000070121";  //平台客户号
        public const string SubNo = "1820015000073761"; //资金账号 1817811000066431
        public const string CltNm = "xxxx有限公司";  //户名，流水号： <PtnSrl>2153198652873</PtnSrl> <PlatSrl>1820000010585127</PlatSrl>

        //虚拟账号
        public const string CltNo_VR = "15338358980_VR";  //客户号
        public const string CltPid_VR = "1818816000066385";  //平台客户号
        public const string SubNo_VR = "1818816000069956"; //资金账号 1817811000066431
        public const string CltNm_VR = "xxx网络旗舰店VR";  //户名
#endregion
    }
}
