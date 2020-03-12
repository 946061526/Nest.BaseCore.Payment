using Nest.BaseCore.Payment.PaymentSDK;
using Nest.BaseCore.Payment.PaymentSDK.AcsPay;
using Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsDataDic;
using Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest;
using Nest.BaseCore.Payment.PaymentSDK.Alipay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Nest.BaseCore.Payment.Service
{
   // public class AcsPayment : PaymentOrderBaseService //, IAcsTransService
   // {
   //     private string Subject
   //     {
   //         get
   //         {
   //             string subject = "";
   //             switch (BrowserType)
   //             {
   //                 case BrowserType.AppLife:
   //                     subject = "速微生活-在线支付";
   //                     break;
   //                 case BrowserType.App:
   //                     subject = "速微开店宝-在线支付";
   //                     break;
   //                 default:
   //                     subject = "速微开店宝-在线支付";
   //                     break;
   //             }
   //             return subject;
   //         }
   //     }
   //     public override string GetThridOAuthUrl(string redirectUrl, string state = "Acs", ThridOAuthScope scope = ThridOAuthScope.SnsapiBase)
   //     {
   //         throw new NotImplementedException();
   //     }

   //     public IAcsTransService _acsTransService = null;
   //     public IAcsTransService acsTransService
   //     {
   //         get
   //         {
   //             if (_acsTransService == null)
   //             {
   //                 return AutofacRegister.Resolve<IAcsTransService>();
   //             }
   //             return _acsTransService;
   //         }
   //     }
   //     public AcsPayment(BrowserType sourceOption) : base(sourceOption)
   //     {

   //     }

   //     public override ThirdOpenAuthorizeViewModel GetThridOAuth(string code)
   //     {
   //         throw new NotImplementedException();
   //     }
   //     /// <summary>
   //     /// 中金支付通知地址
   //     /// </summary>
   //     public override string NotifyUrl
   //     {
   //         get
   //         {
   //             string notifyUrl = ConfigHelper.GetAppsettingValue(ConfigConstants.AcsPaymentNotifyUrl);
   //             if (notifyUrl.IsNullOrWhiteSpace())
   //             {
   //                 return base.NotifyUrl;
   //             }
   //             return notifyUrl;
   //         }
   //     }
   //     /// <summary>
   //     /// 支付结果回调
   //     /// </summary>
   //     /// <param name="requestMessage"></param>
   //     /// <returns></returns>
   //     public override BusinessBaseViewModel<string> PayNotify(HttpRequestMessage requestMessage)
   //     {
   //         string resultXML = string.Empty;
   //         string orderCode = string.Empty;
   //         Dictionary<string, string> dict = null;
   //         Dictionary<string, object> message = null;
            
   //         BusinessBaseViewModel<string> businessBaseViewModel = new BusinessBaseViewModel<string>() { Status = ResponseStatus.Fail, BusinessData = "" };
   //         try
   //         {
   //             string requestData = string.Empty;

   //             using (StreamReader sr = new StreamReader(requestMessage.Content.ReadAsStreamAsync().Result))
   //             {
   //                 sr.BaseStream.Position = 0;
   //                 requestData = sr.ReadToEnd();
   //             }

   //             dict = MapUtils.ParseRquest(requestData);
   //             string xml = Encoding.UTF8.GetString(Convert.FromBase64String(dict["message"]));
   //             message = AcsPayTransXml.FromXML(xml);
   //             //订单号
   //             Dictionary<string,object> billInfo = (Dictionary<string, object>)message["billInfo"];
   //             orderCode = billInfo["BillNo"].ToString();
   //             //记录中金回调的日志
   //             WritePostThridApi(ThirdPlatformBusinessType.Payment, orderCode, ThirdPlatformType.ZhongjinPay, requestMessage.RequestUri.LocalPath, message.ToJsonString(), DateTime.Now, "", DateTime.Now, true);

   //         }
   //         catch (Exception ex)
   //         {
   //             resultXML = string.Format("<?xml version =\"1.0\" encoding=\"GBK\"?><MSG version=\"1.5\"><MSGHD><RspCode>{0}</RspCode><RspMsg>{1}</RspMsg></MSGHD></MSG>", "SDER02", "远程通讯失败");
   //             //            var resultData = Convert.ToBase64String(Encoding.UTF8.GetBytes(resultXML));
   //             //resultXML = AcsPaySign.Sign(resultXML);
   //             businessBaseViewModel.BusinessData = resultXML;
   //             businessBaseViewModel.ErrorMessage =  ex.Message;
   //             return businessBaseViewModel;
   //         }
   //         //回去相应订单对象
   //         var order = GetSmallOrderModel(orderCode);
   //         if (order.IsNull())
   //         {
   //             resultXML = string.Format("<?xml version =\"1.0\" encoding=\"GBK\"?><MSG version=\"1.5\"><MSGHD><RspCode>{0}</RspCode><RspMsg>{1}</RspMsg></MSGHD></MSG>", "SDER02", "未找到合作平台订单信息");
   //             //      var resultData = Convert.ToBase64String(Encoding.UTF8.GetBytes(resultXML));
   //             //resultXML = AcsPaySign.Sign(resultXML);
   //             businessBaseViewModel.BusinessData = resultXML;
   //             businessBaseViewModel.ErrorMessage = "未找到平台订单信息,OrderCode:" + orderCode;
   //             return businessBaseViewModel;
   //         }
   //         //交易结果 1.成功  2.失败 5.撤销
   //         string state = message["State"].ToString();
   //         //失败原因
   //         // string open = message["Opion"].ToString();
   //         //平台交易流水
   //          var srl = (Dictionary<string, object>)message["Srl"];
   //         string transNo = srl["PlatSrl"].ToString();
   //         string errorMessage = string.Empty;
   //         //订单状态
   //         if ((order.Status == OrderStatusType.Confirm || order.Status == OrderStatusType.UnConfirm) && order.PayStatus == PayStatusType.UnPay)
   //         {
   //             if(state == "1")
   //             {
   //                 businessBaseViewModel= ChangeOrderPayStatus(orderCode, transNo, PayStatusType.Payed,ThirdPaymentType.Acs);
   //             }
   //             else if(state == "2")
   //             {
   //                 businessBaseViewModel = ChangeOrderPayStatus(orderCode, transNo, PayStatusType.TimeOut, ThirdPaymentType.Acs);
   //             }
   //             else if(state == "5")
   //             {
   //                 businessBaseViewModel = ChangeOrderPayStatus(orderCode, transNo, PayStatusType.CancelPay, ThirdPaymentType.Acs);
   //             }
   //             if (businessBaseViewModel.Status == ResponseStatus.Success)
   //             {
   //                 resultXML = string.Format("<?xml version =\"1.0\" encoding=\"GBK\"?><MSG version=\"1.5\"><MSGHD><RspCode>{0}</RspCode><RspMsg>{1}</RspMsg></MSGHD></MSG>", "000000", "交易成功。");
   //             }
   //             else
   //             {
   //                  errorMessage = "平台订单信息状态修改失败 OrderCode：" + orderCode;
   //                 resultXML = string.Format("<?xml version =\"1.0\" encoding=\"GBK\"?><MSG version=\"1.5\"><MSGHD><RspCode>{0}</RspCode><RspMsg>{1}</RspMsg></MSGHD></MSG>", "000000", errorMessage);
   //             }
   //         }
   //         else if(order.PayStatus == PayStatusType.Payed)
   //         {
   //             errorMessage = "平台订单信息当前状态已支付 OrderCode：" + orderCode;
   //             resultXML = string.Format("<?xml version =\"1.0\" encoding=\"GBK\"?><MSG version=\"1.5\"><MSGHD><RspCode>{0}</RspCode><RspMsg>{1}</RspMsg></MSGHD></MSG>", "000000", "交易成功。");
   //         }
   //         else
   //         {
   //             errorMessage = "平台订单信息当前状态不允许支付 OrderCode：" + orderCode;
   //             resultXML = string.Format("<?xml version =\"1.0\" encoding=\"GBK\"?><MSG version=\"1.5\"><MSGHD><RspCode>{0}</RspCode><RspMsg>{1}</RspMsg></MSGHD></MSG>", "000000", "交易成功。");
   //         }
            
   //         if(!string.IsNullOrEmpty(errorMessage))
   //         {
   //             businessBaseViewModel.ErrorMessage = errorMessage;
   //         }
   ////         var resultBase = Convert.ToBase64String(Encoding.UTF8.GetBytes(resultXML));
   //        // resultXML = AcsPaySign.Sign(resultXML);
   //         businessBaseViewModel.BusinessData = resultXML;

   //         return businessBaseViewModel;
   //     }
   //     /// <summary>
   //     /// 构建交易请求参数
   //     /// </summary>
   //     /// <param name="request"></param>
   //     /// <param name="xml"></param>
   //     /// <returns></returns>
   //     private Dictionary<string,object> CreateTransParam(AcsPayBaseRequest request, out string xml)
   //     {

   //         //构建DIC,用于创建XML
   //         var map = MapUtils.ObjectToMap(request, true);
   //         //创建XML
   //         xml = AcsPayTransXml.BuildMsg(map, request.MSGHD.TrCd);
   //         //签名
   //         string signature = AcsPaySign.Sign(xml);
   //         //转换BASE64 
   //         string message = AcsPaySign.ConvertBase(xml);
   //         //创建Pos参数
   //         Dictionary<string, object> dict = new Dictionary<string, object>() {
   //             { "ptncode",AcsPayConfig.ptncode},
   //             { "trdcode",request.MSGHD.TrCd},
   //             { "message", message},
   //             { "signature",signature}
   //         };
   //         return dict;
   //     }

   //     /// <summary>
   //     /// 发起订单支付
   //     /// </summary>
   //     /// <param name="orderCode">订单号</param>
   //     /// <param name="userId">付款用户</param>
   //     /// <param name="money">交易金额</param>
   //     /// <returns></returns>
   //     public override BusinessBaseViewModel<string> SubmitPay(PayBaseRequest payRequest)
   //     {
   //         BusinessBaseViewModel<string> businessBaseViewModel = new BusinessBaseViewModel<string>()
   //         {
   //             Status = ResponseStatus.Fail
   //         };
   //         AcsT3008Request request = this.CreateAcsT3008RequestParams(payRequest);
   //         //参数校验
   //         string ret = CheckParams(request);
   //         if (ret != "")
   //         {
   //             businessBaseViewModel.ErrorMessage = ret;
   //             return businessBaseViewModel;
   //         }
   //         //end

   //         //构建DIC,用于创建XML
   //         //var map = MapUtils.ObjectToMap(request, true);
   //         //创建XML
   //         //var xml = AcsPayTransXml.BuildMsg(map, request.MSGHD.TrCd);
   //         //签名
   //         //string signature = AcsPaySign.Sign(xml);
   //         //转换BASE64 
   //         // string message = AcsPaySign.ConvertBase(xml);
   //         //创建Pos参数
   //         //Dictionary<string, object> dict = new Dictionary<string, object>() {
   //         //    { "ptncode",AcsPayConfig.ptncode},
   //         //    { "trdcode",request.MSGHD.TrCd},
   //         //    { "message", message},
   //         //    { "signature",signature}
   //         //};

   //         var dict = CreateTransParam(request, out string xml);
   //         LogHelper.LogInfo("提交中金预付款订单 SubmitPay-XML", xml);

   //         string result = null;
   //         try
   //         {
   //             result = PaymentSDK.AcsPay.HttpHelper.Post(AcsPayConfig.acsTrdUrl, dict);
   //             result = Encoding.UTF8.GetString(Convert.FromBase64String(result));

   //             LogHelper.LogInfo("提交中金预付款订单 响应XML", result);
   //             result = MapUtils.XmlConventJson(result);
   //             var objResult = JsonHelper.DeserializeObject<dynamic>(result);
   //             if(objResult.MSGHD.RspCode != "000000")
   //             {
   //                 businessBaseViewModel.ErrorMessage = objResult.MSGHD.RspMsg;
   //                 return businessBaseViewModel;
   //             }

   //             //第三方支付参数
   //             result = objResult.AuthCode;

   //             //记录中金调用的日志
   //             WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AcsPayConfig.acsTrdUrl, request.ToJsonString(), DateTime.Now, result, DateTime.Now, true);

   //         }
   //         catch(Exception ex)
   //         {
   //             LogHelper.LogError("提交中金预付款订单 SubmitPay", ex.ToString());
   //             //记录中金调用的日志
   //             WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AliPayConfig.gatewayUrl, request.ToJsonString(), DateTime.Now,
   //                 result.IsNull() ? ex.ToString() : result, DateTime.Now, true);
   //         }

   //         businessBaseViewModel.Status = ResponseStatus.Success;
   //         businessBaseViewModel.BusinessData = result;

   //         return businessBaseViewModel;
   //     }
   //     /// <summary>
   //     /// 检查T3008参数
   //     /// </summary>
   //     /// <param name="request"></param>
   //     /// <returns></returns>
   //     private string CheckParams(AcsT3008Request request)
   //     {

   //         if (string.IsNullOrEmpty(request.MSGHD.TrCd))
   //         {
   //             return "交易码不能为空!";
   //         }
   //         if (string.IsNullOrEmpty(request.MSGHD.TrDt))
   //         {
   //             return "交易日期不能为空!";
   //         }
   //         if (string.IsNullOrEmpty(request.MSGHD.TrSrc))
   //         {
   //             return "交易发起方不能为空!";
   //         }
   //         if (string.IsNullOrEmpty(request.MSGHD.PtnCd))
   //         {
   //             return "合作方编号不能为空!";
   //         }
   //         if (string.IsNullOrEmpty(request.MSGHD.BkCd))
   //         {
   //             return " 托管方编号不能为空!";
   //         }

   //         if (string.IsNullOrEmpty(request.CltAcc.SubNo))
   //         {
   //             return " 收款方资金账号不能为空!";
   //         }
   //         if (string.IsNullOrEmpty(request.CltAcc.CltNm))
   //         {
   //             return " 收款方名称不能为空!";
   //         }

   //         if (string.IsNullOrEmpty(request.billInfo.BillNo))
   //         {
   //             return " 支付单号不能为空!";
   //         }
   //         if (string.IsNullOrEmpty(request.billInfo.OrderNo))
   //         {
   //             return " 业务单号不能为空!";
   //         }
   //         decimal billAmt = 0.00m;
   //         if (string.IsNullOrEmpty(request.billInfo.BillAmt) || !decimal.TryParse(request.billInfo.BillAmt, out billAmt))
   //         {
   //             return " 订单金额不能为空!";
   //         }
   //         if (billAmt <= 0)
   //         {
   //             return "订单金额不能小于零!";
   //         }
   //         decimal aclAmt = 0.00M;
   //         if (string.IsNullOrEmpty(request.billInfo.AclAmt) || !decimal.TryParse(request.billInfo.AclAmt, out aclAmt))
   //         {
   //             return " 支付金额不能为空!";
   //         }
   //         if (aclAmt <= 0)
   //         {
   //             return "支付金额不能小于零!";
   //         }

   //         if (string.IsNullOrEmpty(request.billInfo.CcyCd))
   //         {
   //             return "币种不能为空!";
   //         }
   //         if (string.IsNullOrEmpty(request.billInfo.PayType))
   //         {
   //             return "支付方式不能为空!";
   //         }
   //         if (string.IsNullOrEmpty(request.billInfo.SecPayType))
   //         {
   //             return "支付方式二级分类不能为空!";
   //         }
   //         if (string.IsNullOrEmpty(request.ReqFlg))
   //         {
   //             return "发送端标记不能为空!";
   //         }
   //         if (string.IsNullOrEmpty(request.TrsFlag))
   //         {
   //             return "业务标示不能为空!";
   //         }
   //         return "";
   //     }

   //     /// <summary>
   //     /// 创建中金请求T3008报文参数
   //     /// </summary>
   //     /// <returns></returns>
   //     private AcsT3008Request CreateAcsT3008RequestParams(PayBaseRequest request)
   //     {
   //         DateTime dt = DateTime.Now.AddMinutes(10);

   //         AcsPayHeadNode msghdNode = new AcsPayHeadNode
   //         {
   //             TrCd = "T3008",
   //             TrDt = dt.ToString("yyyyMMdd"),
   //             TrTm = dt.ToString("HHmmss"),
   //             TrSrc = AcsInvokedType.Soway.GetEnumDescription(),
   //             PtnCd = AcsPayConfig.ptncode,
   //             BkCd = AcsPayConfig.bkcode
   //         };
           
   //         AcsPayAccNode cltAccNode = new AcsPayAccNode
   //         {
   //             SubNo = request.subNo,
   //             CltNm = request.cltNm
   //         };
   //         AcsT3008BillInfoNode billInfoNode = new AcsT3008BillInfoNode
   //         {
   //             PsubNo = "",
   //             Pnm = "",
   //             BillNo = request.orderCode,
   //             BillAmt = (request.totalAmount * 100).ToString("f0"),
   //             AclAmt = (request.money * 100).ToString("f0"),
   //             CcyCd = "CNY",
   //             PayType = "8",
   //             SecPayType = (BrowserType == BrowserType.Wechat || BrowserType == BrowserType.SamllApp || BrowserType == BrowserType.ScanSamllApp) ? "4" : "3" ,
   //             MiniTag = (BrowserType == BrowserType.SamllApp || BrowserType == BrowserType.ScanSamllApp) ? "1":"0",
   //             BankID = "",
   //             KJBndSrl = "",
   //             KJSMSFlg = "",
   //             Subject = Subject,
   //             GoodsDesc = "线下扫码支付",
   //             UserID = request.userId,
   //             PAuthCode = "",
   //             OrderNo = request.orderCode,
   //         };
   //         AcsT3008Request t3008Request = new AcsT3008Request
   //         {
   //             MSGHD = msghdNode,
   //             billInfo = billInfoNode,
   //             CltAcc = cltAccNode,
   //             ReqFlg = "0",
   //             NotificationURL = "",
   //             ServNoticURL = NotifyUrl,
   //             Usage = "",
   //             DRemark1 = "",
   //             DRemark2 = "",
   //             DRemark3 = "",
   //             DRemark4 = "",
   //             DRemark5 = "",
   //             DRemark6 = "",
   //             TrsFlag = "A00"
   //         };

   //         return t3008Request;
   //     }
   //     #region 

   //     /// <summary>
   //     /// T3004交易 订单支付 可用于在中金都开有户的会员相互转账
   //     /// </summary>
   //     /// <param name="request"></param>
   //     /// <returns></returns>
   //     //public BusinessBaseViewModel<string> AcsT3004Trans(AcsT3004Request request)
   //     //{
   //     //    BusinessBaseViewModel<string> businessBaseViewModel = new BusinessBaseViewModel<string>()
   //     //    {
   //     //        Status = ResponseStatus.Fail
   //     //    };
   //     //    //参数校验
   //     //    string ret = "";//CheckParams(request);
   //     //    #region 检查参数
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrCd))
   //     //    {
   //     //        ret = "交易码不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrDt))
   //     //    {
   //     //        ret = "交易日期不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrSrc))
   //     //    {
   //     //        ret = "交易发起方不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.PtnCd))
   //     //    {
   //     //        ret = "合作方编号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.BkCd))
   //     //    {
   //     //        ret = " 托管方编号不能为空!";
   //     //    }

   //     //    if(string.IsNullOrEmpty(request.billInfo.PSubNo))
   //     //    {
   //     //        ret = "付款方资金账号不能为空";
   //     //    }

   //     //    if (string.IsNullOrEmpty(request.billInfo.PNm))
   //     //    {
   //     //        ret = "付款方户名不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.billInfo.RSubNo))
   //     //    {
   //     //        ret = "收款方资金账号不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.billInfo.RCltNm))
   //     //    {
   //     //        ret = "收款方户名不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.billInfo.OrderNo))
   //     //    {
   //     //        ret = "业务单号不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.billInfo.BillNo))
   //     //    {
   //     //        ret = "支付单号不能为空";
   //     //    }
   //     //    decimal aclAmt = 0.00m;
   //     //    if (string.IsNullOrEmpty(request.billInfo.AclAmt) && decimal.TryParse(request.billInfo.AclAmt,out aclAmt))
   //     //    {
   //     //        ret = "支付金额不能为空";
   //     //    }
   //     //    if(aclAmt <= 0)
   //     //    {
   //     //        ret = "支付金额不能小于0";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.billInfo.PayFee))
   //     //    {
   //     //        ret = "付款方手续费不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.billInfo.PayeeFee))
   //     //    {
   //     //        ret = "收款方手续费不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.billInfo.CcyCd))
   //     //    {
   //     //        ret = "币种不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.TrsFlag))
   //     //    {
   //     //        ret = "业务标示 不能为空";
   //     //    }
   //     //    #endregion
   //     //    if (ret != "")
   //     //    {
   //     //        businessBaseViewModel.ErrorMessage = ret;
   //     //        return businessBaseViewModel;
   //     //    }
   //     //    //end
   //     //    var dict = CreateTransParam(request);

   //     //    string result = null;
   //     //    try
   //     //    {
   //     //        result = PaymentSDK.AcsPay.HttpHelper.Post(AcsPayConfig.acsTrdUrl, dict);
   //     //        result = Encoding.UTF8.GetString(Convert.FromBase64String(result));
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AcsPayConfig.acsTrdUrl, request.ToJsonString(), DateTime.Now, result, DateTime.Now, true);

   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        LogHelper.LogError("提交中金交易T3004", ex.ToString());
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AliPayConfig.gatewayUrl, request.ToJsonString(), DateTime.Now,
   //     //            result.IsNull() ? ex.ToString() : result, DateTime.Now, true);
   //     //    }

   //     //    businessBaseViewModel.Status = ResponseStatus.Success;
   //     //    businessBaseViewModel.BusinessData = result;

   //     //    return businessBaseViewModel;

   //     //}
   //     /// <summary>
   //     /// T2009交易 渠道_出金-申请  用于提现
   //     /// </summary>
   //     /// <param name="request"></param>
   //     /// <returns></returns>
   //     //public BusinessBaseViewModel<string> AcsT2009Trans(AcsT2009Request request)
   //     //{
   //     //    BusinessBaseViewModel<string> businessBaseViewModel = new BusinessBaseViewModel<string>()
   //     //    {
   //     //        Status = ResponseStatus.Fail
   //     //    };
   //     //    //参数校验
   //     //    string ret = "";//CheckParams(request);
   //     //    #region 检查参数
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrCd))
   //     //    {
   //     //        ret = "交易码不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrDt))
   //     //    {
   //     //        ret = "交易日期不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrSrc))
   //     //    {
   //     //        ret = "交易发起方不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.PtnCd))
   //     //    {
   //     //        ret = "合作方编号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.BkCd))
   //     //    {
   //     //        ret = " 托管方编号不能为空!";
   //     //    }

   //     //    if (string.IsNullOrEmpty(request.CltAcc.SubNo))
   //     //    {
   //     //        ret = "户名不能为空";
   //     //    }

   //     //    if (string.IsNullOrEmpty(request.CltAcc.CltNm))
   //     //    {
   //     //        ret = "付款方户名不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BkAcc.AccNo))
   //     //    {
   //     //        ret = "银行账号(卡号)不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BkAcc.AccNm))
   //     //    {
   //     //        ret = "开户名称不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Amt.AclAmt))
   //     //    {
   //     //        ret = "发生额不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Amt.FeeAmt))
   //     //    {
   //     //        ret = "转账手续费不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Amt.TAmt))
   //     //    {
   //     //        ret = "总金额(发生额+转账手续费)不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Amt.CcyCd))
   //     //    {
   //     //        ret = "币种不能为空";
   //     //    }

   //     //    if (string.IsNullOrEmpty(request.TrsFlag))
   //     //    {
   //     //        ret = "业务标示不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BalFlag))
   //     //    {
   //     //        ret = "结算方式标示不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Srl.PtnSrl))
   //     //    {
   //     //        ret = "合作方交易流水号不能为空";
   //     //    }

   //     //    #endregion
   //     //    if (ret != "")
   //     //    {
   //     //        businessBaseViewModel.ErrorMessage = ret;
   //     //        return businessBaseViewModel;
   //     //    }
   //     //    //end
   //     //    var dict = CreateTransParam(request);

   //     //    string result = null;
   //     //    try
   //     //    {
   //     //        result = PaymentSDK.AcsPay.HttpHelper.Post(AcsPayConfig.acsTrdUrl, dict);
   //     //        result = Encoding.UTF8.GetString(Convert.FromBase64String(result));
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AcsPayConfig.acsTrdUrl, request.ToJsonString(), DateTime.Now, result, DateTime.Now, true);

   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        LogHelper.LogError("提交中金交易T2009", ex.ToString());
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AliPayConfig.gatewayUrl, request.ToJsonString(), DateTime.Now,
   //     //            result.IsNull() ? ex.ToString() : result, DateTime.Now, true);
   //     //    }

   //     //    businessBaseViewModel.Status = ResponseStatus.Success;
   //     //    businessBaseViewModel.BusinessData = result;

   //     //    return businessBaseViewModel;
   //     //}

   //     /// <summary>
   //     ///  开销户[T1001]  账户增删改 
   //     /// </summary>
   //     /// <param name="request"></param>
   //     /// <returns></returns>
   //     //public BusinessBaseViewModel<string> AcsT1001Trans(AcsT1001Request request)
   //     //{
   //     //    BusinessBaseViewModel<string> businessBaseViewModel = new BusinessBaseViewModel<string>()
   //     //    {
   //     //        Status = ResponseStatus.Fail
   //     //    };
   //     //    //参数校验
   //     //    string ret = "";//CheckParams(request);
   //     //    #region 检查参数
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrCd))
   //     //    {
   //     //        ret = "交易码不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrDt))
   //     //    {
   //     //        ret = "交易日期不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrSrc))
   //     //    {
   //     //        ret = "交易发起方不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.PtnCd))
   //     //    {
   //     //        ret = "合作方编号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.BkCd))
   //     //    {
   //     //        ret = " 托管方编号不能为空!";
   //     //    }

   //     //    if (string.IsNullOrEmpty(request.CltAcc.CltNo))
   //     //    {
   //     //        ret = "客户号不能为空";
   //     //    }

   //     //    if (string.IsNullOrEmpty(request.CltAcc.CltNm))
   //     //    {
   //     //        ret = "户名不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Clt.Kd))
   //     //    {
   //     //        ret = "客户性质不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Clt.Nm))
   //     //    {
   //     //        ret = "姓名不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Clt.CdTp))
   //     //    {
   //     //        ret = "法定代表人/自然人证件类型不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Clt.CdNo))
   //     //    {
   //     //        ret = "法定代表人/自然人证件号码不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Clt.MobNo))
   //     //    {
   //     //        ret = "手机号码不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Srl.PtnSrl))
   //     //    {
   //     //        ret = "合作方交易流水号不能为空";
   //     //    }

   //     //    if (string.IsNullOrEmpty(request.FcFlg))
   //     //    {
   //     //        ret = "业务功能标示不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.AccTp))
   //     //    {
   //     //        ret = "账户类型(不能为空";
   //     //    }

   //     //    #endregion
   //     //    if (ret != "")
   //     //    {
   //     //        businessBaseViewModel.ErrorMessage = ret;
   //     //        return businessBaseViewModel;
   //     //    }
   //     //    //end
   //     //    var dict = CreateTransParam(request);

   //     //    string result = null;
   //     //    try
   //     //    {
   //     //        result = PaymentSDK.AcsPay.HttpHelper.Post(AcsPayConfig.acsTrdUrl, dict);
   //     //        result = Encoding.UTF8.GetString(Convert.FromBase64String(result));
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AcsPayConfig.acsTrdUrl, request.ToJsonString(), DateTime.Now, result, DateTime.Now, true);

   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        LogHelper.LogError("提交中金交易T1001", ex.ToString());
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AliPayConfig.gatewayUrl, request.ToJsonString(), DateTime.Now,
   //     //            result.IsNull() ? ex.ToString() : result, DateTime.Now, true);
   //     //    }

   //     //    businessBaseViewModel.Status = ResponseStatus.Success;
   //     //    businessBaseViewModel.BusinessData = result;

   //     //    return businessBaseViewModel;
   //     //}
   //     /// <summary>
   //     /// 开户记过查询
   //     /// </summary>
   //     /// <param name="request"></param>
   //     /// <returns></returns>
   //     //public BusinessBaseViewModel<string> AcsT1002Trans(AcsT1002Request request)
   //     //{
   //     //    BusinessBaseViewModel<string> businessBaseViewModel = new BusinessBaseViewModel<string>()
   //     //    {
   //     //        Status = ResponseStatus.Fail
   //     //    };
   //     //    //参数校验
   //     //    string ret = "";//CheckParams(request);
   //     //    #region 检查参数
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrCd))
   //     //    {
   //     //        ret = "交易码不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrDt))
   //     //    {
   //     //        ret = "交易日期不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrSrc))
   //     //    {
   //     //        ret = "交易发起方不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.PtnCd))
   //     //    {
   //     //        ret = "合作方编号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.BkCd))
   //     //    {
   //     //        ret = " 托管方编号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.SrcSrl))
   //     //    {
   //     //        ret = "合作方交易流水号不能为空";
   //     //    }
   //     //    #endregion
   //     //    if (ret != "")
   //     //    {
   //     //        businessBaseViewModel.ErrorMessage = ret;
   //     //        return businessBaseViewModel;
   //     //    }
   //     //    //end
   //     //    var dict = CreateTransParam(request);

   //     //    string result = null;
   //     //    try
   //     //    {
   //     //        result = PaymentSDK.AcsPay.HttpHelper.Post(AcsPayConfig.acsTrdUrl, dict);
   //     //        result = Encoding.UTF8.GetString(Convert.FromBase64String(result));
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AcsPayConfig.acsTrdUrl, request.ToJsonString(), DateTime.Now, result, DateTime.Now, true);

   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        LogHelper.LogError("提交中金交易T1002", ex.ToString());
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AliPayConfig.gatewayUrl, request.ToJsonString(), DateTime.Now,
   //     //            result.IsNull() ? ex.ToString() : result, DateTime.Now, true);
   //     //    }

   //     //    businessBaseViewModel.Status = ResponseStatus.Success;
   //     //    businessBaseViewModel.BusinessData = result;

   //     //    return businessBaseViewModel;
   //     //}

   //     //public BusinessBaseViewModel<string> AcsT1004Trans(AcsT1004Request request)
   //     //{
   //     //    BusinessBaseViewModel<string> businessBaseViewModel = new BusinessBaseViewModel<string>()
   //     //    {
   //     //        Status = ResponseStatus.Fail
   //     //    };
   //     //    //参数校验
   //     //    string ret = "";//CheckParams(request);
   //     //    #region 检查参数
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrCd))
   //     //    {
   //     //        ret = "交易码不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrDt))
   //     //    {
   //     //        ret = "交易日期不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrSrc))
   //     //    {
   //     //        ret = "交易发起方不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.PtnCd))
   //     //    {
   //     //        ret = "合作方编号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.BkCd))
   //     //    {
   //     //        ret = " 托管方编号不能为空!";
   //     //    }

   //     //    if (string.IsNullOrEmpty(request.CltAcc.SubNo))
   //     //    {
   //     //        ret = " 资金账号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.CltAcc.CltNm))
   //     //    {
   //     //        ret = " 户名不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BkAcc.BkId))
   //     //    {
   //     //        ret = " 银行编号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BkAcc.AccNo))
   //     //    {
   //     //        ret = " 银行账号(卡号)不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BkAcc.AccNm))
   //     //    {
   //     //        ret = " 开户名称不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BkAcc.AccTp))
   //     //    {
   //     //        ret = " 账户类型不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BkAcc.CrdTp))
   //     //    {
   //     //        ret = " 银行账户(卡)类型不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BkAcc.CdTp))
   //     //    {
   //     //        ret = " 开户证件类型不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BkAcc.CdNo))
   //     //    {
   //     //        ret = " 证件号码不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.BkAcc.CrsMk))
   //     //    {
   //     //        ret = " 跨行标示不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.Srl.PtnSrl))
   //     //    {
   //     //        ret = " 合作方交易流水号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.FcFlg))
   //     //    {
   //     //        ret = "业务功能标示不能为空";
   //     //    }
   //     //    #endregion
   //     //    if (ret != "")
   //     //    {
   //     //        businessBaseViewModel.ErrorMessage = ret;
   //     //        return businessBaseViewModel;
   //     //    }
   //     //    //end
   //     //    var dict = CreateTransParam(request);

   //     //    string result = null;
   //     //    try
   //     //    {
   //     //        result = PaymentSDK.AcsPay.HttpHelper.Post(AcsPayConfig.acsTrdUrl, dict);
   //     //        result = Encoding.UTF8.GetString(Convert.FromBase64String(result));
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AcsPayConfig.acsTrdUrl, request.ToJsonString(), DateTime.Now, result, DateTime.Now, true);

   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        LogHelper.LogError("提交中金交易T1001", ex.ToString());
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AliPayConfig.gatewayUrl, request.ToJsonString(), DateTime.Now,
   //     //            result.IsNull() ? ex.ToString() : result, DateTime.Now, true);
   //     //    }

   //     //    businessBaseViewModel.Status = ResponseStatus.Success;
   //     //    businessBaseViewModel.BusinessData = result;

   //     //    return businessBaseViewModel;
   //     //}

   //     //public BusinessBaseViewModel<string> AcsT1005Trans(AcsT1005Request request)
   //     //{
   //     //    BusinessBaseViewModel<string> businessBaseViewModel = new BusinessBaseViewModel<string>()
   //     //    {
   //     //        Status = ResponseStatus.Fail
   //     //    };
   //     //    //参数校验
   //     //    string ret = "";//CheckParams(request);
   //     //    #region 检查参数
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrCd))
   //     //    {
   //     //        ret = "交易码不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrDt))
   //     //    {
   //     //        ret = "交易日期不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.TrSrc))
   //     //    {
   //     //        ret = "交易发起方不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.PtnCd))
   //     //    {
   //     //        ret = "合作方编号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.MSGHD.BkCd))
   //     //    {
   //     //        ret = " 托管方编号不能为空!";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.CltAcc.SubNo))
   //     //    {
   //     //        ret = "资金账号水号不能为空";
   //     //    }
   //     //    if (string.IsNullOrEmpty(request.CltAcc.CltNm))
   //     //    {
   //     //        ret = "户名不能为空";
   //     //    }
   //     //    #endregion
   //     //    if (ret != "")
   //     //    {
   //     //        businessBaseViewModel.ErrorMessage = ret;
   //     //        return businessBaseViewModel;
   //     //    }
   //     //    //end
   //     //    var dict = CreateTransParam(request);

   //     //    string result = null;
   //     //    try
   //     //    {
   //     //        result = PaymentSDK.AcsPay.HttpHelper.Post(AcsPayConfig.acsTrdUrl, dict);
   //     //        result = Encoding.UTF8.GetString(Convert.FromBase64String(result));
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AcsPayConfig.acsTrdUrl, request.ToJsonString(), DateTime.Now, result, DateTime.Now, true);

   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        LogHelper.LogError("提交中金交易T1002", ex.ToString());
   //     //        //记录中金调用的日志
   //     //        WritePostThridApi(ThirdPlatformBusinessType.Payment, request.orderCode, ThirdPlatformType.ZhongjinPay, AliPayConfig.gatewayUrl, request.ToJsonString(), DateTime.Now,
   //     //            result.IsNull() ? ex.ToString() : result, DateTime.Now, true);
   //     //    }

   //     //    businessBaseViewModel.Status = ResponseStatus.Success;
   //     //    businessBaseViewModel.BusinessData = result;

   //     //    return businessBaseViewModel;
   //     //}
   //     #endregion

   //     /// <summary>
   //     /// 主动查询支付结果
   //     /// </summary>
   //     /// <param name="orderCode">平台交易单号</param>
   //     /// <returns></returns>
   //     public override BusinessBaseViewModel<string> QueryPayResult(string orderCode)
   //     {
   //         BusinessBaseViewModel<string> businessBaseViewModel = new BusinessBaseViewModel<string>() { Status = ResponseStatus.Fail };

   //         AcsPayResultRequest request = new AcsPayResultRequest
   //         {
   //             BillNo = orderCode
   //         };
   //         AcsT3010Request T3010Request = MapUtils.ObjectMapConvert<AcsT3010Request>(request, "Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest", false);
   //         T3010Request.MSGHD = _acsTransService.BulidHeadNode("T3010");

   //         businessBaseViewModel = _acsTransService.AcsT3010Trans(T3010Request);
   //         //解析报文
   //         dynamic transferInfo = JObject.Parse(businessBaseViewModel.BusinessData);
   //         if (transferInfo != null && transferInfo.Msghd.RspCode == "000000")
   //         {
   //             PayStatusType payStatusType = PayStatusType.UnPay;
   //             if(transferInfo.BillInfo.BillState == "1")   //支付成功
   //             {
   //                 payStatusType = PayStatusType.Payed;
   //             }
   //             else if(transferInfo.BillInfo.BillState == "2")   //支付失败
   //             {
   //                 payStatusType = PayStatusType.TimeOut;
   //             }
   //             else if(transferInfo.BillInfo.BillState == "3")
   //             {
   //                 payStatusType = PayStatusType.UnPay;
   //             }
   //             else if(transferInfo.BillInfo.BillState == "5")
   //             {
   //                 payStatusType = PayStatusType.CancelPay;
   //             }
   //             string tradeNo = transferInfo.Srl.PlatSrl;
   //             ConfrimPayResult(orderCode, tradeNo, payStatusType, ThirdPaymentType.Acs);

   //         }

   //         return businessBaseViewModel;
   //     }
   //     #region 退款
   //     /// <summary>
   //     /// 发起订单退款
   //     /// </summary>
   //     /// <param name="refundRequest">退款申请参数</param>
   //     /// <returns></returns>
   //     public override BusinessBaseViewModel<string> RefundPay(RefundBaseRequest refundRequest)
   //     {
   //         throw new NotImplementedException();
   //     }
   //     #endregion
   // }
}
