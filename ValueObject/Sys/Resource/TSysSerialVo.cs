using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.Resource
{
    /// <summary>
    /// 功能：序列号管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysSerialVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_SYS_SERIAL_TABLE = "T_SYS_SERIAL";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 编码
		/// </summary>
		public static string SERIAL_CODE_FIELD = "SERIAL_CODE";
		/// <summary>
		/// 名称
		/// </summary>
		public static string SERIAL_NAME_FIELD = "SERIAL_NAME";
		/// <summary>
		/// 分组
		/// </summary>
		public static string SERIAL_GROUP_FIELD = "SERIAL_GROUP";
		/// <summary>
		/// 序列号
		/// </summary>
		public static string SERIAL_NUMBER_FIELD = "SERIAL_NUMBER";
		/// <summary>
		/// 位数
		/// </summary>
		public static string LENGTH_FIELD = "LENGTH";
		/// <summary>
		/// 前缀
		/// </summary>
		public static string PREFIX_FIELD = "PREFIX";
		/// <summary>
		/// 粒度
		/// </summary>
		public static string GRANULARITY_FIELD = "GRANULARITY";
		/// <summary>
		/// 最小值
		/// </summary>
		public static string MIN_FIELD = "MIN";
		/// <summary>
		/// 最大值
		/// </summary>
		public static string MAX_FIELD = "MAX";
		/// <summary>
		/// 创建人
		/// </summary>
		public static string CREATE_ID_FIELD = "CREATE_ID";
		/// <summary>
		/// 创建日期
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
		
		public TSysSerialVo()
		{
			this.ID = "";
			this.SERIAL_CODE = "";
			this.SERIAL_NAME = "";
			this.SERIAL_GROUP = "";
			this.SERIAL_NUMBER = "";
			this.LENGTH = "";
			this.PREFIX = "";
			this.GRANULARITY = "";
			this.MIN = "";
			this.MAX = "";
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
		/// 编码
		/// </summary>
		public string SERIAL_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string SERIAL_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 分组
		/// </summary>
		public string SERIAL_GROUP
		{
			set ;
			get ;
		}
		/// <summary>
		/// 序列号
		/// </summary>
		public string SERIAL_NUMBER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 位数
		/// </summary>
		public string LENGTH
		{
			set ;
			get ;
		}
		/// <summary>
		/// 前缀
		/// </summary>
		public string PREFIX
		{
			set ;
			get ;
		}
		/// <summary>
		/// 粒度
		/// </summary>
		public string GRANULARITY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最小值
		/// </summary>
		public string MIN
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最大值
		/// </summary>
		public string MAX
		{
			set ;
			get ;
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public string CREATE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 创建日期
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