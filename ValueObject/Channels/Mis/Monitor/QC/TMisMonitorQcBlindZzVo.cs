using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：标准盲样
    /// 创建日期：2013-07-02
    /// 创建人：熊卫华
    /// </summary>
    public class TMisMonitorQcBlindZzVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_MIS_MONITOR_QC_BLIND_ZZ_TABLE = "T_MIS_MONITOR_QC_BLIND_ZZ";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 平行样分析结果 ID
		/// </summary>
		public static string RESULT_ID_FIELD = "RESULT_ID";
		/// <summary>
		/// 标准值
		/// </summary>
		public static string STANDARD_VALUE_FIELD = "STANDARD_VALUE";
		/// <summary>
		/// 不确定度
		/// </summary>
		public static string UNCETAINTY_FIELD = "UNCETAINTY";
		/// <summary>
		/// 测定值
		/// </summary>
		public static string BLIND_VALUE_FIELD = "BLIND_VALUE";
		/// <summary>
		/// 偏移量（%）
		/// </summary>
		public static string OFFSET_FIELD = "OFFSET";
		/// <summary>
		/// 是否合格
		/// </summary>
		public static string BLIND_ISOK_FIELD = "BLIND_ISOK";
		/// <summary>
		/// 质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样 9、质控平行 10、空白加标 11、标准盲样）
		/// </summary>
		public static string QC_TYPE_FIELD = "QC_TYPE";
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
		
		public TMisMonitorQcBlindZzVo()
		{
			this.ID = "";
			this.RESULT_ID = "";
			this.STANDARD_VALUE = "";
			this.UNCETAINTY = "";
			this.BLIND_VALUE = "";
			this.OFFSET = "";
			this.BLIND_ISOK = "";
			this.QC_TYPE = "";
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
		/// 平行样分析结果 ID
		/// </summary>
		public string RESULT_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 标准值
		/// </summary>
		public string STANDARD_VALUE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 不确定度
		/// </summary>
		public string UNCETAINTY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 测定值
		/// </summary>
		public string BLIND_VALUE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 偏移量（%）
		/// </summary>
		public string OFFSET
		{
			set ;
			get ;
		}
		/// <summary>
		/// 是否合格
		/// </summary>
		public string BLIND_ISOK
		{
			set ;
			get ;
		}
		/// <summary>
		/// 质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样 9、质控平行 10、空白加标 11、标准盲样）
		/// </summary>
		public string QC_TYPE
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