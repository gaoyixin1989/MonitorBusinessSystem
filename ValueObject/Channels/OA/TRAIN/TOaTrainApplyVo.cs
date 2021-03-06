using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.TRAIN
{
    /// <summary>
    /// 功能：培训申请
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaTrainApplyVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_TRAIN_APPLY_TABLE = "T_OA_TRAIN_APPLY";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 计划内或计划外
		/// </summary>
		public static string IF_PALN_FIELD = "IF_PALN";
		/// <summary>
		/// 培训项目
		/// </summary>
		public static string TRAIN_PROJECT_FIELD = "TRAIN_PROJECT";
		/// <summary>
		/// 培训内容
		/// </summary>
		public static string TRAIN_CONTENT_FIELD = "TRAIN_CONTENT";
		/// <summary>
		/// 天时
		/// </summary>
		public static string TRAIN_DAYS_FIELD = "TRAIN_DAYS";
		/// <summary>
		/// 学时
		/// </summary>
		public static string TRAIN_HOURS_FIELD = "TRAIN_HOURS";
		/// <summary>
		/// 开始时间
		/// </summary>
		public static string BEGIN_DATE_FIELD = "BEGIN_DATE";
		/// <summary>
		/// 结束时间
		/// </summary>
		public static string FINISH_DATE_FIELD = "FINISH_DATE";
		/// <summary>
		/// 培训单位
		/// </summary>
		public static string TRAIN_DEPT_FIELD = "TRAIN_DEPT";
		/// <summary>
		/// 培训地点
		/// </summary>
		public static string TRAIN_PLACE_FIELD = "TRAIN_PLACE";
		/// <summary>
		/// 培训教师
		/// </summary>
		public static string TRAIN_TEACHER_FIELD = "TRAIN_TEACHER";
		/// <summary>
		/// 联系人
		/// </summary>
		public static string LINK_MAN_FIELD = "LINK_MAN";
		/// <summary>
		/// 联系电话
		/// </summary>
		public static string LINK_TEL_FIELD = "LINK_TEL";
		/// <summary>
		/// 发证单位
		/// </summary>
		public static string CERTIFICATION_DEPT_FIELD = "CERTIFICATION_DEPT";
		/// <summary>
		/// 考核办法,1笔试、2口试、3实际操作，复选
		/// </summary>
		public static string TEST_METHOD_FIELD = "TEST_METHOD";
		/// <summary>
		/// 有效性评价
		/// </summary>
		public static string JUDGE_FIELD = "JUDGE";
		/// <summary>
		/// 部门审批人ID
		/// </summary>
		public static string DEPT_APP_ID_FIELD = "DEPT_APP_ID";
		/// <summary>
		/// 部门审批时间
		/// </summary>
		public static string DEPT_APP_DATE_FIELD = "DEPT_APP_DATE";
		/// <summary>
		/// 部门审批意见
		/// </summary>
		public static string DEPT_APP_INFO_FIELD = "DEPT_APP_INFO";
		/// <summary>
		/// 站长审批人ID
		/// </summary>
		public static string LEADER_APP_ID_FIELD = "LEADER_APP_ID";
		/// <summary>
		/// 站长审批时间
		/// </summary>
		public static string LEADER_APP_DATE_FIELD = "LEADER_APP_DATE";
		/// <summary>
		/// 站长审批意见
		/// </summary>
		public static string LEADER_APP_INFO_FIELD = "LEADER_APP_INFO";
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
		
		public TOaTrainApplyVo()
		{
			this.ID = "";
			this.IF_PALN = "";
			this.TRAIN_PROJECT = "";
			this.TRAIN_CONTENT = "";
			this.TRAIN_DAYS = "";
			this.TRAIN_HOURS = "";
			this.BEGIN_DATE = "";
			this.FINISH_DATE = "";
			this.TRAIN_DEPT = "";
			this.TRAIN_PLACE = "";
			this.TRAIN_TEACHER = "";
			this.LINK_MAN = "";
			this.LINK_TEL = "";
			this.CERTIFICATION_DEPT = "";
			this.TEST_METHOD = "";
			this.JUDGE = "";
			this.DEPT_APP_ID = "";
			this.DEPT_APP_DATE = "";
			this.DEPT_APP_INFO = "";
			this.LEADER_APP_ID = "";
			this.LEADER_APP_DATE = "";
			this.LEADER_APP_INFO = "";
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
		/// 计划内或计划外
		/// </summary>
		public string IF_PALN
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训项目
		/// </summary>
		public string TRAIN_PROJECT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训内容
		/// </summary>
		public string TRAIN_CONTENT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 天时
		/// </summary>
		public string TRAIN_DAYS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 学时
		/// </summary>
		public string TRAIN_HOURS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 开始时间
		/// </summary>
		public string BEGIN_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 结束时间
		/// </summary>
		public string FINISH_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训单位
		/// </summary>
		public string TRAIN_DEPT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训地点
		/// </summary>
		public string TRAIN_PLACE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训教师
		/// </summary>
		public string TRAIN_TEACHER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string LINK_MAN
		{
			set ;
			get ;
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public string LINK_TEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 发证单位
		/// </summary>
		public string CERTIFICATION_DEPT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 考核办法,1笔试、2口试、3实际操作，复选
		/// </summary>
		public string TEST_METHOD
		{
			set ;
			get ;
		}
		/// <summary>
		/// 有效性评价
		/// </summary>
		public string JUDGE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 部门审批人ID
		/// </summary>
		public string DEPT_APP_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 部门审批时间
		/// </summary>
		public string DEPT_APP_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 部门审批意见
		/// </summary>
		public string DEPT_APP_INFO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 站长审批人ID
		/// </summary>
		public string LEADER_APP_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 站长审批时间
		/// </summary>
		public string LEADER_APP_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 站长审批意见
		/// </summary>
		public string LEADER_APP_INFO
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