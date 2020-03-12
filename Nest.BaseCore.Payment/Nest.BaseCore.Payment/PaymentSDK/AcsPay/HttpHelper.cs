using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Web;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay
{
    public class HttpHelper
    {
        /// <summary>
        /// http Post 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static Stream HttpPost(string url,Dictionary<string,object> paras)
        {
            if(string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(( sender,certificate,chain,sslPolicyErrors)=> { return true; });
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //如果需要POST数据  
            if (!(paras == null || paras.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in paras.Keys)
                {
                    string append = string.Format("{0}={1}", key, HttpUtility.UrlEncode(paras[key].ToString(), Encoding.UTF8));
                    if (i > 0)
                    {
                        append = "&" + append;
                    }
                    buffer.AppendFormat(append);
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse().GetResponseStream();
        }

        public static string Post(string url, Dictionary<string, object> paras)
        {
            using (StreamReader reader = new StreamReader(HttpPost(url, paras), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
