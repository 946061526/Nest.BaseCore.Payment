namespace Nest.BaseCore.Payment
{
    /// <summary>
    /// 第三方授权类型枚举
    /// </summary>
    public enum ThridOAuthScope
    {
        /// <summary>
        /// 静默授权（不弹出授权页面，直接跳转）
        /// </summary>
        SnsapiBase = 0,
        /// <summary>
        /// 提示授权（弹出授权页面，能获取其信息）
        /// </summary>
        SnsapiUserInfo = 1
    }
}
