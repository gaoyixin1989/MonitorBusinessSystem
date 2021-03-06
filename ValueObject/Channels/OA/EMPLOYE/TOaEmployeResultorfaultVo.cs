using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.EMPLOYE
{
    /// <summary>
    /// 功能：员工工作成果与事故
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeResultorfaultVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_EMPLOYE_RESULTORFAULT_TABLE = "T_OA_EMPLOYE_RESULTORFAULT";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 员工编号
		/// </summary>
		public static string EMPLOYEID_FIELD = "EMPLOYEID";
		/// <summary>
		/// 工作成果
		/// </summary>
		public static string WORKRESULT_FIELD = "WORKRESULT";
		/// <summary>
		/// 质量事故
		/// </summary>
		public static string ACCIDENTS_FIELD = "ACCIDENTS";
		/// <summary>
		/// 成果或事故，1成果，2事故
		/// </summary>
		public static string RESULT_OR_ACCIDENT_FIELD = "RESULT_OR_ACCIDENT";
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
		
		public TOaEmployeResultorfaultVo()
		{
			this.ID = "";
			this.EMPLOYEID = "";
			this.WORKRESULT = "";
			this.ACCIDENTS = "";
			this.RESULT_OR_ACCIDENT = "";
            this.ACCIDENTHAPPENDATE = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
			
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
		/// 员工编号
		/// </summary>
		public string EMPLOYEID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 工作成果
		/// </summary>
		public string WORKRESULT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 质量事故
		/// </summary>
		public string ACCIDENTS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 成果或事故，1成果，2事故
		/// </summary>
		public string RESULT_OR_ACCIDENT
		{
			set ;
			get ;
		}

        //事故发生时间
        public string ACCIDENTHAPPENDATE
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