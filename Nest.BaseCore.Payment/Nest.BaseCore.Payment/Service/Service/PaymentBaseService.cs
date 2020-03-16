using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nest.BaseCore.Payment.Service
{
    /// <summary>
    /// 支付服务
    /// </summary>
    public abstract class PaymentBaseService : IPaymentService, IThridOAuth
    {
        /// <summary>
        /// 支付超时时间  分
        /// </summary>
        protected int PayTimeOut
        {
            get
            {
                var timeOut = Convert.ToInt32(AppSettingsHelper.Configuration[ConfigConstants.PayTimeOut]) * 60;
                if (timeOut <= 0)
                {
                    return 24 * 60;
                }
                return timeOut;
            }
        }
        /// <summary>
        /// 请求源
        /// </summary>
        protected BrowserType BrowserType { get; set; }

        public PaymentBaseService(BrowserType sourceOption)
        {
            BrowserType = sourceOption;
        }

        /// <summary>
        /// 支付通知地址
        /// </summary>
        public virtual string NotifyUrl => throw new NotImplementedException("未配置支付结果通知地址");

        /// <summary>
        /// 获取第三方授权
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public abstract ThirdOpenAuthorizeViewModel GetThridOAuth(string code);

        public abstract string GetThridOAuthUrl(string redirectUrl, string state = "", ThridOAuthScope scope = ThridOAuthScope.SnsapiBase);

        /// <summary>
        /// 支付结果通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public abstract ApiResultModel<string> PayNotify(HttpRequestMessage requestMessage);

        /// <summary>
        /// 主动查询支付结果
        /// </summary>
        /// <param name="orderCode">平台交易单号</param>
        /// <returns></returns>
        public abstract ApiResultModel<string> QueryPayResult(string orderCode);

        /// <summary>
        /// 发起订单退款
        /// </summary>
        /// <param name="refundRequest">退款申请参数</param>
        /// <returns></returns>
        public abstract ApiResultModel<string> RefundPay(RefundBaseRequest refundRequest);

        /// <summary>
        /// 发起订单支付
        /// </summary>
        /// <returns></returns>
        public abstract ApiResultModel<string> SubmitPay(PayBaseRequest payRequest);
    }
}
