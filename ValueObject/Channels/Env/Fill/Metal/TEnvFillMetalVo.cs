using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Env.Fill.Metal
{
    /// <summary>
    /// 功能：底泥重金属数据填报表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillMetalVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_ENV_FILL_METAL_TABLE = "T_ENV_FILL_METAL";
		//静态字段引用
		/// <summary>
		/// 主键ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 饮用水断面监测点ID
		/// </summary>
		public static string SEDIMENT_METAL_POINT_ID_FIELD = "SEDIMENT_METAL_POINT_ID";
		/// <summary>
		/// 月份
		/// </summary>
		public static string MONTH_FIELD = "MONTH";
		/// <summary>
		/// 垂线ID，对应T_BAS_POINT_RIVER_VERTICAL主键
		/// </summary>
		public static string VERTICAL_ID_FIELD = "VERTICAL_ID";
		/// <summary>
		/// 枯水期、平水期、枯水期
		/// </summary>
		public static string KPF_FIELD = "KPF";
		/// <summary>
		/// 采样日期
		/// </summary>
		public static string SAMPLING_DAY_FIELD = "SAMPLING_DAY";
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
		
		public TEnvFillMetalVo()
		{
			this.ID = "";
			this.SEDIMENT_METAL_POINT_ID = "";
			this.MONTH = "";
			this.VERTICAL_ID = "";
			this.KPF = "";
			this.SAMPLING_DAY = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
		}
		
		#region 属性
			/// <summary>
		/// 主键ID
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 饮用水断面监测点ID
		/// </summary>
		public string SEDIMENT_METAL_POINT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 月份
		/// </summary>
		public string MONTH
		{
			set ;
			get ;
		}
		/// <summary>
		/// 垂线ID，对应T_BAS_POINT_RIVER_VERTICAL主键
		/// </summary>
		public string VERTICAL_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 枯水期、平水期、枯水期
		/// </summary>
		public string KPF
		{
			set ;
			get ;
		}
		/// <summary>
		/// 采样日期
		/// </summary>
		public string SAMPLING_DAY
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