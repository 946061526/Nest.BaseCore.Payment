namespace Nest.BaseCore.Payment.PaymentSDK.Wechat
{

    /// <summary>
    /// 微信公众号配置
    /// </summary>
    public class WechatConfig
    {
        /// <summary>
        /// 编码集
        /// </summary>
        public const string Charset = "UTF-8";
        /// <summary>
        /// 终端ip
        /// </summary>
        public const string IP = "39.108.39.100";

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        internal const int REPORT_LEVENL = 1;
        internal static readonly string SSLCERT_PATH = AppSettingsHelper.Configuration["WechatConfig:SSLCERT_PATH"];
        internal static readonly string SSLCERT_APP_PATH = AppSettingsHelper.Configuration["WechatConfig:SSLCERT_APP_PATH"];
        internal static readonly string SSLCERT_Life_APP_PATH = AppSettingsHelper.Configuration["WechatConfig:SSLCERT_Life_APP_PATH"];
        internal static readonly string SSLCERT_PASSWORD = AppSettingsHelper.Configuration["WechatConfig:SSLCERT_PASSWORD"];
        internal static readonly string SSLCERT_APP_PASSWORD = AppSettingsHelper.Configuration["WechatConfig:SSLCERT_APP_PASSWORD"];
        internal static readonly string SSLCERT_Life_APP_PASSWORD = AppSettingsHelper.Configuration["WechatConfig:SSLCERT_Life_APP_PASSWORD"];
        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        internal const int LOG_LEVENL = 3;
        /// <summary>
        /// 微信wap站点地址
        /// </summary>
        internal const string WechatWebPath = "";
#if DEBUG


        ////测试（屋里人）
        ///// <summary>
        ///// APPID
        ///// </summary>
        //public const string AppId = "wx68eb2ed800cece5a";
        ///// <summary>
        ///// APPSecret
        ///// </summary>
        //public const string Secret = "9ddb88720d02c2cdd64a022d28db81d9";
        ///// <summary>
        ///// 商户号
        ///// </summary>
        //public const string MchId = "1397809102";
        ///// <summary>
        ///// Key
        ///// </summary>
        //public const string Key = "GXL00000000000000000000000000000";
        //正式（速微生活）
        /// <summary>
        /// APPID
        /// </summary>
        public const string AppId = "wx0545e6d0360fd788";
        /// <summary>
        /// APPSecret
        /// </summary>
        public const string Secret = "1a6764f7d1794033e5149f22c806d5e6";
        /// <summary>
        /// 商户号
        /// </summary>
        public const string MchId = "1502128331";//1522743841
        /// <summary>
        /// Key
        /// </summary>
        public const string Key = "GXL00000000000000000000000000000";
#else
        //正式
        /// <summary>
        /// APPID
        /// </summary>
        public const string AppId = "wx0545e6d0360fd788";
        /// <summary>
        /// APPSecret
        /// </summary>
        public const string Secret = "1a6764f7d1794033e5149f22c806d5e6";
          /// <summary>
        /// 商户号
        /// </summary>
        public const string MchId = "1502128331"; 
        /// <summary>
        /// Key
        /// </summary>
        public const string Key = "GXL00000000000000000000000000000";//LJH00000000000000000000000000000";
        
#endif
        #region 微信app配置
#if DEBUG
        //正式
        /// <summary>
        /// APPID APP
        /// </summary>
        public const string AppIdByApp = "wx710c5a77cff4cbd5";
        /// <summary>
        /// APPSecret APP
        /// </summary>
        public const string SecretByApp = "92a973fcf4ed86e9a4942da1f85718cd";
        /// <summary>
        /// 商户号 APP
        /// </summary>
        public const string MchIdByApp = "1521481291";
        /// <summary>
        /// Key APP
        /// </summary>
        public const string KeyByApp = "GXL00000000000000000000000000000";

        // 速微生活（用户端）
        /// <summary>
        /// APPID APP
        /// </summary>
        public const string AppIdByLifeApp = "wx98f2c1534e5afa8a";
        /// <summary>
        /// APPSecret APP
        /// </summary>
        public const string SecretByLifeApp = "a920894b2e1b2cd87e65210d5b1def66";
        /// <summary>
        /// 商户号 APP
        /// </summary>
        public const string MchIdByLifeApp = "1522743841";
        /// <summary>
        /// Key APP
        /// </summary>
        public const string KeyByLifeApp = "GXL00000000000000000000000000000";
#else
        //测试 开店宝
        /// <summary>
        /// APPID APP
        /// </summary>
        public const string AppIdByApp = "wx710c5a77cff4cbd5";
        /// <summary>
        /// APPSecret APP
        /// </summary>
        public const string SecretByApp = "92a973fcf4ed86e9a4942da1f85718cd";
        /// <summary>
        /// 商户号 APP
        /// </summary>
        public const string MchIdByApp = "1521481291";
        /// <summary>
        /// Key APP
        /// </summary>
        public const string KeyByApp = "GXL00000000000000000000000000000";

        // 速微生活（用户端）
        /// <summary>
        /// APPID APP
        /// </summary>
        public const string AppIdByLifeApp = "wx98f2c1534e5afa8a";
        /// <summary>
        /// APPSecret APP
        /// </summary>
        public const string SecretByLifeApp = "a920894b2e1b2cd87e65210d5b1def66";
        /// <summary>
        /// 商户号 APP
        /// </summary>
        public const string MchIdByLifeApp = "1522743841";
        /// <summary>
        /// Key APP
        /// </summary>
        public const string KeyByLifeApp = "GXL00000000000000000000000000000";
#endif
        #endregion

        #region 小程序配置

        /// <summary>
        /// 小程序配置 APPID 
        /// </summary>
        public const string AppIdByMiniApp = "wxe8c50832ad372392";
        /// <summary>
        /// 小程序配置 MCHID 
        /// </summary>
        public const string MchIdByMiniApp = "1522743841";
        /// <summary>
        /// 小程序配置  APP
        /// </summary>
        public const string SecretByMiniApp = "4ee65dcebbed82536089cf4be0b00087";
        /// <summary>
        ///小程序配置  Key APP
        /// </summary>
        public const string KeyByMiniApp = "GXL00000000000000000000000000000";
        #endregion

        #region 扫码小程序配置
        /// <summary>
        /// 扫码小程序配置 APPID 
        /// </summary>
        public const string AppIdByScanMiniApp = "wxe8c50832ad372392"; //wx929d8e91a27e9420
        /// <summary>
        /// 扫码小程序配置 MCHID 
        /// </summary>
        public const string MchIdByScanMiniApp = "1522743841";//1502128331
        /// <summary>
        /// 扫码小程序配置  APP
        /// </summary>
        public const string SecretByScanMiniApp = "4ee65dcebbed82536089cf4be0b00087";//ffb56018d2f8d174957ae40d96f6f279
        /// <summary>
        /// 扫码小程序配置  Key APP
        /// </summary>
        public const string KeyByScanMiniApp = "GXL00000000000000000000000000000";//GXL00000000000000000000000000000
        #endregion

    }
}
