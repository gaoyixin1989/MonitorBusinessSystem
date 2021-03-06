using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.DynamicAttribute
{
    /// <summary>
    /// 功能：属性类别配置表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttributeTypeValueVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_ATTRIBUTE_TYPE_VALUE_TABLE = "T_BASE_ATTRIBUTE_TYPE_VALUE";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 监测类别
		/// </summary>
		public static string ITEM_TYPE_FIELD = "ITEM_TYPE";
		/// <summary>
		/// 排口点位类别
		/// </summary>
		public static string OUTLETPOINT_TYPE_FIELD = "OUTLETPOINT_TYPE";
		/// <summary>
		/// 属性类别
		/// </summary>
		public static string ATTRIBUTE_TYPE_ID_FIELD = "ATTRIBUTE_TYPE_ID";
		/// <summary>
		/// 属性
		/// </summary>
		public static string ATTRIBUTE_ID_FIELD = "ATTRIBUTE_ID";
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
		
		public TBaseAttributeTypeValueVo()
		{
			this.ID = "";
			this.ITEM_TYPE = "";
			this.OUTLETPOINT_TYPE = "";
			this.ATTRIBUTE_TYPE_ID = "";
			this.ATTRIBUTE_ID = "";
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
		/// 监测类别
		/// </summary>
		public string ITEM_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 排口点位类别
		/// </summary>
		public string OUTLETPOINT_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 属性类别
		/// </summary>
		public string ATTRIBUTE_TYPE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 属性
		/// </summary>
		public string ATTRIBUTE_ID
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