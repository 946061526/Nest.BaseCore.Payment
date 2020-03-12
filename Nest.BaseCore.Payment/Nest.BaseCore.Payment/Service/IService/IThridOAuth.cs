namespace Nest.BaseCore.Payment.Service
{
    /// <summary>
    /// 第三方平台授权
    /// </summary>
    public interface IThridOAuth : IThrid
    {
        /// <summary>
        /// 获取验证地址
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <param name="state"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        string GetThridOAuthUrl(string redirectUrl, string state = "", ThridOAuthScope scope = ThridOAuthScope.SnsapiBase);

        /// <summary>
        /// 获取第三方授权
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ThirdOpenAuthorizeViewModel GetThridOAuth(string code);

    }
}
