using Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest;

namespace Nest.BaseCore.Payment.Service
{
    public interface IAcsTransService
    {
        /// <summary>
        /// T3004交易 订单支付 可用于在中金都开有户的会员相互转账
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResultModel<string> AcsT3004Trans(AcsT3004Request request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResultModel<string> AcsT3006Trans(AcsT3006Request request);
        /// <summary>
        /// T2009交易 渠道_出金-申请  用于提现
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResultModel<string> AcsT2009Trans(AcsT2009Request request);
        /// <summary>
        /// 开销户[T1001]  账户增删改 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResultModel<string> AcsT1001Trans(AcsT1001Request request);
 
        /// <summary>
        /// [T1001] 结果查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResultModel<string> AcsT1002Trans(AcsT1002Request request);

        /// <summary>
        /// 结算账户维护[T1004]   绑定银行卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResultModel<string> AcsT1004Trans(AcsT1004Request request);

        /// <summary>
        /// 资金账户余额查询[T1005]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResultModel<string> AcsT1005Trans(AcsT1005Request request);
        /// <summary>
        /// 构建请求头参数
        /// </summary>
        /// <returns></returns>
        AcsPayHeadNode BulidHeadNode(string transNo);
        /// <summary>
        /// 出入金结果查询 [T2012]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResultModel<string> AcsT2012Trans(AcsT2012Request request);
        /// <summary>
        /// 结算账户信息查询[T1007] 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResultModel<string> AcsT1007Trans(AcsT1007Request request);
        /// <summary>
        ///  收款结果查询[T3010]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResultModel<string> AcsT3010Trans(AcsT3010Request request);

    }
}
