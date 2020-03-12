using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment
{
    /// <summary>
    /// 第三方开放授权视图模型
    /// </summary>
    public class ThirdOpenAuthorizeViewModel
    {
        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 微信UnionId
        /// </summary>
        public string UnionId { get; set; }
        /// <summary>
        /// 支付宝用户的唯一userId
        /// </summary>
        public string AlipayId { get; set; }
        /// <summary>
        /// 授权令牌
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 到期秒数
        /// </summary>
        public int Expires { get; set; }
    }
}
