using System.Linq;

namespace Nest.BaseCore.Payment.Service
{
    /// <summary>
    /// 第三方业务工厂
    /// </summary>
    public sealed class PaymentFactory
    {
        private PaymentFactory() { }
        /// <summary>
        /// 收款方门店ID
        /// </summary>
        public string StoreId { get; set; }
        /// <summary>
        /// 静态实例
        /// </summary>
        public static PaymentFactory Instance { get; } = new PaymentFactory();
        /// <summary>
        /// 创建授权对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceOption"></param>
        /// <returns></returns>
        public T Create<T>(BrowserType sourceOption = BrowserType.Wechat) where T : IThird
        {
            return (T)CreateOAuth(sourceOption);
        }

        /// <summary>
        /// 创建授权对象
        /// </summary>
        /// <param name="sourceOption"></param>
        /// <returns></returns>
        private IPaymentService CreateOAuth(BrowserType sourceOption)
        {
            switch (sourceOption)
            {
                //case BrowserType.Alipay:
                //    return new AlipayPayment(sourceOption);
                //case BrowserType.Wechat:
                //    return new WechatPayment(sourceOption);
                default:
                    throw new PaymentException("未找到可以使用的授权通道");

            }
        }
        /// <summary>
        /// 创建第三方实现对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="platformType"></param>
        /// <param name="sourceOption"></param>
        /// <returns></returns>
        public T Create<T>(PaymentType type, PaymentPlatformType platformType, BrowserType sourceOption = BrowserType.Wechat, string acsSubNo = "") where T : IThird
        {
            if (type == PaymentType.Online)
            {
                return (T)CreateOnline(platformType, sourceOption);
            }
            else
            {
                return (T)CreateOffline(platformType, sourceOption, acsSubNo);
            }
        }
        /// <summary>
        /// 创建线下支付通道
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="sourceOption"></param>
        /// <returns></returns>
        private IPaymentService CreateOffline(PaymentPlatformType platformType, BrowserType sourceOption = BrowserType.Wechat, string acsSubNo = "")
        {
            //var flag = AppSettingsHelper.Configuration["ConfigConstants.IsOpenAcsPayment"].ToBoolean();
            switch (platformType)
            {
                //case PaymentPlatformType.Alipay:
                //    //中金暂不支持支付宝支付，这里启用公司支付宝
                //    //if (flag)
                //    //{
                //    //    return new AcsPayment(sourceOption);
                //    //}
                //    return new AlipayPayment(sourceOption);
                //case PaymentPlatformType.Zhongjin:
                //    return new AcsPayment(sourceOption);
                //case PaymentPlatformType.Wechat:
                //    //if (flag && !acsSubNo.IsNullOrWhiteSpace() && !ConfigConstants.PaymentWechatStore.Contains(StoreId))
                //    //{
                //    //    return new AcsPayment(sourceOption);
                //    //}
                //    return new WechatPayment(sourceOption);
                //case PaymentPlatformType.Blance:
                //    return new BalancePayment(sourceOption);
                default:
                    throw new PaymentException("未找到可以使用的支付通道");

            }
        }
        /// <summary>
        /// 创建线上支付通道
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="sourceOption"></param>
        /// <returns></returns>
        private IPaymentService CreateOnline(PaymentPlatformType platformType, BrowserType sourceOption = BrowserType.Wechat)
        {
            switch (platformType)
            {
                //case PaymentPlatformType.Alipay:
                //    return new AlipayPayment(sourceOption);
                //case PaymentPlatformType.Wechat:
                //    return new WechatPayment(sourceOption);
                //case PaymentPlatformType.Blance:
                //    return new BalancePayment(sourceOption);
                default:
                    throw new PaymentException("未找到可以使用的支付通道");

            }
        }
    }
}
