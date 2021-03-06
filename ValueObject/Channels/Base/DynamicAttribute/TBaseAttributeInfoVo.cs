using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.DynamicAttribute
{
    /// <summary>
    /// 功能：属性信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttributeInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_ATTRIBUTE_INFO_TABLE = "T_BASE_ATTRIBUTE_INFO";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 属性名称
		/// </summary>
		public static string ATTRIBUTE_NAME_FIELD = "ATTRIBUTE_NAME";
		/// <summary>
		/// 控件ID
		/// </summary>
		public static string CONTROL_ID_FIELD = "CONTROL_ID";
		/// <summary>
		/// 控件名称
		/// </summary>
		public static string CONTROL_NAME_FIELD = "CONTROL_NAME";
		/// <summary>
		/// 控件宽度
		/// </summary>
		public static string WIDTH_FIELD = "WIDTH";
		/// <summary>
		/// 是否可编辑
		/// </summary>
		public static string ENABLE_FIELD = "ENABLE";
		/// <summary>
		/// 可否为空
		/// </summary>
		public static string IS_NULL_FIELD = "IS_NULL";
		/// <summary>
		/// 最大长度
		/// </summary>
		public static string MAX_LENGTH_FIELD = "MAX_LENGTH";
		/// <summary>
		/// 字典项
		/// </summary>
		public static string DICTIONARY_FIELD = "DICTIONARY";
		/// <summary>
		/// 数据库表名
		/// </summary>
		public static string TABLE_NAME_FIELD = "TABLE_NAME";
		/// <summary>
		/// 文本字段
		/// </summary>
		public static string TEXT_FIELD_FIELD = "TEXT_FIELD";
		/// <summary>
		/// 值字段
		/// </summary>
		public static string VALUE_FIELD_FIELD = "VALUE_FIELD";
		/// <summary>
		/// 排序
		/// </summary>
		public static string SN_FIELD = "SN";
        /// <summary>
        /// 删除标记
        /// </summary>
        public static string IS_DEL_FIELD = "IS_DEL";
		/// <summary>
		/// 备注1
		/// </summary>
		public static string REMARK1_FIELD = "REMARK1";
		/// <summary>
		/// 备注2
		/// </summary>
		public static string REMARK2_FIELD = "REMARK2";
		/// <summary>
		/// 备注3
		/// </summary>
		public static string REMARK3_FIELD = "REMARK3";
		/// <summary>
		/// 备注4
		/// </summary>
		public static string REMARK4_FIELD = "REMARK4";
		/// <summary>
		/// 备注5
		/// </summary>
		public static string REMARK5_FIELD = "REMARK5";
		
		#endregion
		
		public TBaseAttributeInfoVo()
		{
			this.ID = "";
			this.ATTRIBUTE_NAME = "";
			this.CONTROL_ID = "";
			this.CONTROL_NAME = "";
			this.WIDTH = "";
			this.ENABLE = "";
			this.IS_NULL = "";
			this.MAX_LENGTH = "";
			this.DICTIONARY = "";
			this.TABLE_NAME = "";
			this.TEXT_FIELD = "";
			this.VALUE_FIELD = "";
			this.SN = "";
            this.IS_DEL = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// ID
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 属性名称
		/// </summary>
		public string ATTRIBUTE_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 控件ID
		/// </summary>
		public string CONTROL_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 控件名称
		/// </summary>
		public string CONTROL_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 控件宽度
		/// </summary>
		public string WIDTH
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否可编辑
		/// </summary>
		public string ENABLE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 可否为空
		/// </summary>
		public string IS_NULL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最大长度
		/// </summary>
		public string MAX_LENGTH
		{
			set ;
			get ;
		}
		/// <summary>
		/// 字典项
		/// </summary>
		public string DICTIONARY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 数据库表名
		/// </summary>
		public string TABLE_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文本字段
		/// </summary>
		public string TEXT_FIELD
		{
			set ;
			get ;
		}
		/// <summary>
		/// 值字段
		/// </summary>
		public string VALUE_FIELD
		{
			set ;
			get ;
		}
		/// <summary>
		/// 排序
		/// </summary>
		public string SN
		{
			set ;
			get ;
		}
        /// <summary>
        /// 删除标记
        /// </summary>
        public string IS_DEL
        {
            set;
            get;
        }
		/// <summary>
		/// 备注1
		/// </summary>
		public string REMARK1
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注2
		/// </summary>
		public string REMARK2
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注3
		/// </summary>
		public string REMARK3
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注4
		/// </summary>
		public string REMARK4
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注5
		/// </summary>
		public string REMARK5
		{
			set ;
			get ;
		}
	
		
		#endregion
		
    }
}