using System.ComponentModel.DataAnnotations;

namespace Nest.BaseCore.Payment
{
    /// <summary>
    /// 接口请求参数基类
    /// </summary>
    public class BaseRequestModel
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        [Required]
        public string Timestamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        [Required]
        public string Nonce { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [Required]
        public string Sign { get; set; }
    }
}
