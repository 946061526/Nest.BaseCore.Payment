using System;

namespace Nest.BaseCore.Payment
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ObjectMapAttribute : Attribute
    {
        /// <summary>
        /// 映射的类名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 映射的字段
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 为空时提示信息
        /// </summary>
        public string Descriptext { get; set; }
        public ObjectMapAttribute(string ClassName)
        {
            this.ClassName = ClassName;
        }
        public ObjectMapAttribute(string ColumnName, string Descriptext)
        {
            this.ColumnName = ColumnName;
            this.Descriptext = Descriptext;
        }
        public ObjectMapAttribute(string ClassName, string ColumnName, string Descriptext)
        {
            this.ClassName = ClassName;
            this.ColumnName = ColumnName;
            this.Descriptext = Descriptext;
        }
    }
}
