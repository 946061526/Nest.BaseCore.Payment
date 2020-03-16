namespace Nest.BaseCore.Payment.Service
{
    /// <summary>
    /// 第三方平台授权
    /// </summary>
    public interface IThirdOAuth : IThird
    {
        /// <summary>
        /// 获取验证地址
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <param name="state"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        string GetThirdOAuthUrl(string redirectUrl, string state = "", ThirdOAuthScope scope = ThirdOAuthScope.SnsapiBase);

        /// <summary>
        /// 获取第三方授权
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ThirdOpenAuthorizeViewModel GetThirdOAuth(string code);

    }
}
