using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.DynamicAttribute
{
    /// <summary>
    /// 功能：属性类别表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttributeTypeVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_ATTRIBUTE_TYPE_TABLE = "T_BASE_ATTRIBUTE_TYPE";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
		/// </summary>
		public static string MONITOR_ID_FIELD = "MONITOR_ID";
		/// <summary>
		/// 类别名称
		/// </summary>
		public static string SORT_NAME_FIELD = "SORT_NAME";
		/// <summary>
		/// 添加人
		/// </summary>
		public static string INSERT_BY_FIELD = "INSERT_BY";
		/// <summary>
		/// 添加时间
		/// </summary>
		public static string INSERT_DATE_FIELD = "INSERT_DATE";
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
		
		public TBaseAttributeTypeVo()
		{
			this.ID = "";
			this.MONITOR_ID = "";
			this.SORT_NAME = "";
			this.INSERT_BY = "";
			this.INSERT_DATE = "";
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
		/// 监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
		/// </summary>
		public string MONITOR_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 类别名称
		/// </summary>
		public string SORT_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 添加人
		/// </summary>
		public string INSERT_BY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public string INSERT_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 删除标记
		/// </summary>
        public string IS_DEL
		{
			set ;
			get ;
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