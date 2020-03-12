using System.Net.Http;

namespace Nest.BaseCore.Payment.Service
{
    /// <summary>
    /// 支付业务接口
    /// </summary>
    public interface IPaymentService : IThrid
    {
        /// <summary>
        /// 支付通知地址
        /// </summary>
        string NotifyUrl { get; }

        /// <summary>
        /// 发起订单支付
        /// </summary>
        /// <param name="payRequest">订单号</param>
        /// <returns></returns>
        ApiResultModel<string> SubmitPay(PayBaseRequest payRequest);

        /// <summary>
        /// 支付结果通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        ApiResultModel<string> PayNotify(HttpRequestMessage requestMessage);

        /// <summary>
        /// 发起订单退款（此方法，以后应拆分2个方法处理退款，一个更新退款状态，另一个MQ调用微信退款申请）
        /// </summary>
        /// <param name="refundRequest">退款申请参数</param>
        /// <returns></returns>
        ApiResultModel<string> RefundPay(RefundBaseRequest refundRequest);

        /// <summary>
        /// 主动查询支付结果
        /// </summary>
        /// <param name="orderCode">平台交易单号</param>
        /// <returns></returns>
        ApiResultModel<string> QueryPayResult(string orderCode);
    }
}

