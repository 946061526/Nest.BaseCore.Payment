using Nest.BaseCore.Payment.PaymentSDK.AcsPay.AcsRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

namespace Nest.BaseCore.Payment.PaymentSDK.AcsPay
{
    public class MapUtils
    {

        /// <summary>
        /// 对象转换为字典
        /// </summary>
        /// <param name="obj">待转化的对象</param>
        /// <param name="isIgnoreNull">是否忽略NULL 这里我不需要转化NULL的值，正常使用可以不穿参数 默认全转换</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObjectToMap(object obj, bool isIgnoreNull = false)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();

            Type t = obj.GetType(); // 获取对象对应的类， 对应的类型

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance); // 获取当前type公共属性
            try
            {
                foreach (PropertyInfo p in pi)
                {
                    MethodInfo m = p.GetGetMethod();

                    //如果M是类型
                    if (m != null && m.IsPublic && m.ReflectedType.IsValueType == false
                        && p.PropertyType.Name.ToLower() != "string" && m.Name != "get_PaytimeOut")
                    {
                        var subObject = m.Invoke(obj, new object[] { });  //创建子对象
                        Dictionary<string, object> subMap = new Dictionary<string, object>();
                        Type subType = subObject.GetType(); // 获取对象对应的类， 对应的类型
                        PropertyInfo[] subPi = subType.GetProperties(BindingFlags.Public | BindingFlags.Instance); // 获取当前type公共属性

                        foreach (PropertyInfo sp in subPi)
                        {
                            MethodInfo sm = sp.GetGetMethod();

                            //进行判NULL处理
                            if (sm.Invoke(subObject, new object[] { }) != null || !isIgnoreNull)
                            {
                                if (!subMap.ContainsKey(sp.Name))
                                {
                                    subMap.Add(sp.Name, sm.Invoke(subObject, new object[] { })); // 向字典添加元素
                                }

                            }
                        }

                        map.Add(p.Name, subMap); // 向字典添加元素
                    }
                    else
                    {
                        // 进行判NULL处理 
                        if (m.Invoke(obj, new object[] { }) != null || !isIgnoreNull)
                        {
                            if (p.Name == "orderCode" || p.Name == "userId" || p.Name == "money" || p.Name == "clientIp" || p.Name == "totalAmount" || p.Name == "subNo" || p.Name == "cltNm")
                            {
                                continue;
                            }
                            map.Add(p.Name, m.Invoke(obj, new object[] { })); // 向字典添加元素
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return map;
        }

        /// <summary>
        /// 自定义对象映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestModel"></param>
        /// <param name="nameSpacePath">嵌套对象的命名空间</param>
        /// <param name="checkFlag">判空处理</param>
        /// <returns></returns>
        public static T ObjectMapConvert<T>(BaseRequestModel requestModel ,string nameSpacePath,bool checkFlag) where T : AcsPayBaseRequest, new()
        {
            //转换目标对象
            var target = new T();
            //目标对象的属性
            var targetTypeProperties = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //转换的源对象
            var properties = requestModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance| BindingFlags.NonPublic);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            //循环源对象 赋值到 目标对象中
            foreach (var item in properties)
            {
                MethodInfo methodInfo = item.GetGetMethod();
                //提取自定义特性标记
                var customAttribute = (ObjectMapAttribute)item.GetCustomAttributes(false).Where(x => x.GetType().Name == "ObjectMapAttribute").FirstOrDefault();

                if (customAttribute != null)
                {
                    //如果是嵌套对象
                    if (customAttribute.ClassName != null)
                    {
                        var subColumnName = customAttribute.ColumnName.Split('.');
                        //把创建的子对象方到字典中，已有的不再创建
                        if (!dic.ContainsKey(customAttribute.ClassName))
                        {
                            //创建嵌套对象
                            Assembly assembly = Assembly.GetExecutingAssembly();
                            dynamic subTarget = assembly.CreateInstance(nameSpacePath +"." + customAttribute.ClassName);
                            dic.Add(customAttribute.ClassName, subTarget);
                            //在目标对象中找到对于嵌套对象的元数据
                            var targetProp = targetTypeProperties.Where(x => x.Name == subColumnName[0]).FirstOrDefault();
                            //把嵌套对象添加到目标对象中
                            targetProp.SetValue(target, subTarget);
                        }
                        if (checkFlag)
                        {
                            //如果源对象该字段为空,则抛出
                            if (methodInfo.Invoke(requestModel, new object[] { }) == null)
                            {
                                throw new ArgumentNullException(customAttribute.Descriptext);
                            }
                        }

                        //把源对象该字段的值映射到目标对象中
                        object obj = null;
                        //取出嵌套对象
                        if (dic.TryGetValue(customAttribute.ClassName, out obj))
                        {
                            //获取嵌套对象的属性
                            var subTargetProperties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            //
                            var subitem = subTargetProperties.Where(x => x.Name == subColumnName[1]).FirstOrDefault();
                            if (subitem != null)
                            {
                                //赋值
                                subitem.SetValue(obj, methodInfo.Invoke(requestModel, new object[] { }));
                            }
                        }
                    }
                    else
                    {
                        if (checkFlag)
                        {
                            //空提示
                            if (methodInfo.Invoke(requestModel, new object[] { }) == null)
                            {
                                throw new ArgumentNullException(customAttribute.Descriptext);
                            }
                        }
                        //字段映射 到目标对象
                        var targetColumn = targetTypeProperties.Where(x => x.Name == customAttribute.ColumnName).FirstOrDefault();
                        if (targetColumn != null)
                        {
                            //赋值
                            targetColumn.SetValue(target, methodInfo.Invoke(requestModel, new object[] { }));
                        }
                    }
                }
            }
            return target;
        }
        /// <summary>
        /// xml 转换 JSON
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string XmlConventJson(string xml)
        {
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"GBK\"?>", "").Replace("version=\"1.5\"", "");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
            return jsonText;
        }

        /// <summary>
        /// 把表单方式提交的数据解析到dictionary
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseRquest(string url)
        {

            string baseUrl = "";
            if (string.IsNullOrEmpty(url))
                return null;
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                int questionMarkIndex = url.IndexOf('?');
                if (questionMarkIndex == -1)
                    baseUrl = url;
                else
                    baseUrl = url.Substring(0, questionMarkIndex);
                if (questionMarkIndex == url.Length - 1)
                    return null;
                string ps = url.Substring(questionMarkIndex + 1);

                // 开始分析参数对   
                System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", System.Text.RegularExpressions.RegexOptions.Compiled);
                System.Text.RegularExpressions.MatchCollection mc = re.Matches(ps);

                foreach (System.Text.RegularExpressions.Match m in mc)
                {
                    result.Add(m.Result("$2"), HttpUtility.UrlDecode(m.Result("$3")));
                }
            }
            catch
            {
                return null;
            }
            return result;

        }
    }
}
