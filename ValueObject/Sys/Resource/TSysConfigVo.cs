using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.Resource
{
    /// <summary>
    /// 功能：系统配置管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysConfigVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_SYS_CONFIG_TABLE = "T_SYS_CONFIG";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
        
        /// <summary>
		/// 配置编名
		/// </summary>
        public static string CONFIG_TEXT_FIELD = "CONFIG_TEXT";
		/// <summary>
		/// 配置编码
		/// </summary>
		public static string CONFIG_CODE_FIELD = "CONFIG_CODE";
		/// <summary>
		/// 配置值
		/// </summary>
		public static string CONFIG_VALUE_FIELD = "CONFIG_VALUE";
		/// <summary>
		/// 配置类型
		/// </summary>
		public static string CONFIG_TYPE_FIELD = "CONFIG_TYPE";
		/// <summary>
		/// 创建人ID
		/// </summary>
		public static string CREATE_ID_FIELD = "CREATE_ID";
		/// <summary>
		/// 创建时间
		/// </summary>
		public static string CREATE_TIME_FIELD = "CREATE_TIME";
		/// <summary>
		/// 备注
		/// </summary>
		public static string REMARK_FIELD = "REMARK";
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
		
		#endregion
		
		public TSysConfigVo()
		{
			this.ID = "";
            this.CONFIG_TEXT = "";
			this.CONFIG_CODE = "";
			this.CONFIG_VALUE = "";
			this.CONFIG_TYPE = "";
			this.CREATE_ID = "";
			this.CREATE_TIME = "";
			this.REMARK = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
        /// <summary>
        /// 配置名
        /// </summary>
        public string CONFIG_TEXT
        {
            set;
            get;
        }
		/// <summary>
		/// 配置编码
		/// </summary>
		public string CONFIG_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 配置值
		/// </summary>
		public string CONFIG_VALUE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 配置类型
		/// </summary>
		public string CONFIG_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 创建人ID
		/// </summary>
		public string CREATE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public string CREATE_TIME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string REMARK
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
	
		
		#endregion
		
    }
}