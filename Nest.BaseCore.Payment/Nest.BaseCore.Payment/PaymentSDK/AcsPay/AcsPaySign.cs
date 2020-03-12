using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay
{
    public class AcsPaySign
    {
        /// <summary>
        /// 中金交易 XML签名 采用ShaWithRsa 算法，
        /// </summary>
        /// <param name="orgXml">中金交易类型原文XML</param>
        /// <returns></returns>
        public static string Sign(string orgXml)
        {
            //X509Certificate2 cert = new X509Certificate2(AcsPayConfig.prikeyPath, AcsPayConfig.password, X509KeyStorageFlags.Exportable);
            X509Certificate2 cert = new X509Certificate2(AppSettingsHelper.Configuration["AcsPaySign:prikeyPath"], AcsPayConfig.password, X509KeyStorageFlags.Exportable);
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            string privatekey = cert.PrivateKey.ToXmlString(true);
            provider.FromXmlString(privatekey);
            byte[] abc = provider.SignData(Encoding.UTF8.GetBytes(orgXml), new SHA1CryptoServiceProvider());
            return BitConverter.ToString(abc, 0).Replace("-", string.Empty);
        }
        /// <summary>
        /// 中金交易 XMLBase64
        /// </summary>
        /// <param name="orgXml">中金交易类型原文XML</param>
        /// <returns></returns>
        public static string ConvertBase(string orgXml)
        {
            byte[] bytes =Encoding.UTF8.GetBytes(orgXml);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 中金交易 把交易报文提交到服务接口中
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="transTypeCode"></param>
        /// <returns></returns>
        public  static string Post(string xml,string transTypeCode)
        {
            var signStr = Sign(xml);
            var base64Str = ConvertBase(xml);

            Dictionary<string, object> dict = new Dictionary<string, object>() {
                { "ptncode",AcsPayConfig.ptncode},
                { "trdcode",transTypeCode},
                { "message", base64Str},
                { "signature",signStr}
            };

            string result = HttpHelper.Post(AcsPayConfig.acsTrdUrl, dict);

            result = Encoding.UTF8.GetString(Convert.FromBase64String(result));
            return result;

        }
    }
}
