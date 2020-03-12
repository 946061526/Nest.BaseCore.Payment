using System;
using System.Collections.Generic;
using System.Xml;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay
{
    public class AcsPayTransXml
    {
        public static string BuildMsgHead(string trcd)
        {
            string xml = @"<MSGHD><TrCd>$TrCd</TrCd><TrDt>$TrDt</TrDt><TrTm>$TrTm</TrTm><TrSrc>F</TrSrc><PtnCd>$PtnCd</PtnCd><BkCd>$BkCd</BkCd></MSGHD>";
            xml = xml.Replace("$TrCd", trcd);
            xml = xml.Replace("$TrDt", DateTime.Today.ToString("yyyyMMdd"));
            xml = xml.Replace("$TrTm", DateTime.Now.ToString("HHmmss"));
            xml = xml.Replace("$PtnCd", AcsPayConfig.ptncode);
            xml = xml.Replace("$BkCd", AcsPayConfig.bkcode);
            return xml;
        }
        public static string BuildMsg(Dictionary<string, object> paras, string trcd)
        {
            if (paras == null || paras.Count == 0)
            {
                return string.Empty;
            }
            if (string.IsNullOrWhiteSpace(trcd))
            {
                return string.Empty;
            }
            //          string head = BuildMsgHead(trcd);
            string xml = "<?xml version=\"1.0\" encoding=\"GBK\"?><MSG version=\"1.5\">";///+ head;
            foreach (string key in paras.Keys)
            {
                if (paras[key].GetType() != paras.GetType())
                {
                    xml += "<" + key + ">" + paras[key] + "</" + key + ">";
                }
                else
                {
                    xml += "<" + key + ">";
                    Dictionary<string, object> subDict = paras[key] as Dictionary<string, object>;
                    foreach (string subKey in subDict.Keys)
                    {
                        xml += "<" + subKey + ">" + subDict[subKey] + "</" + subKey + ">";
                    }
                    xml += "</" + key + ">";
                }
            }
            xml += "</MSG>";
            return xml;
        }
        /// <summary>
        /// XML 转化为字典数据结构
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static Dictionary<string, object> FromXML(string xml)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.ChildNodes[1];//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                if (xn.ChildNodes.Count > 0 && xn.InnerText != xn.InnerXml)
                {
                    Dictionary<string, object> subDict = new Dictionary<string, object>();
                    foreach (XmlNode subXn in xn.ChildNodes)
                    {
                        //XmlElement subXe = (XmlElement)subXn;
                        subDict[subXn.Name] = subXn.InnerText;//subXe.InnerText;
                    }
                    dict[xn.Name] = subDict;
                }
                else
                {
                    XmlElement xe = (XmlElement)xn;
                    dict[xe.Name] = xe.InnerText;
                }
            }
            return dict;
        }
    }
}
