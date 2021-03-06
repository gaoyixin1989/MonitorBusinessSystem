using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.EMPLOYE
{
    /// <summary>
    /// 功能：员工基本信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_EMPLOYE_INFO_TABLE = "T_OA_EMPLOYE_INFO";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// USER_ID
		/// </summary>
		public static string USER_ID_FIELD = "USER_ID";
		/// <summary>
		/// 员工编号
		/// </summary>
		public static string EMPLOYE_CODE_FIELD = "EMPLOYE_CODE";
		/// <summary>
		/// 员工姓名
		/// </summary>
		public static string EMPLOYE_NAME_FIELD = "EMPLOYE_NAME";
		/// <summary>
		/// 身份证号
		/// </summary>
		public static string ID_CARD_FIELD = "ID_CARD";
		/// <summary>
		/// 性别
		/// </summary>
		public static string SEX_FIELD = "SEX";
		/// <summary>
		/// 出生日期
		/// </summary>
		public static string BIRTHDAY_FIELD = "BIRTHDAY";
		/// <summary>
		/// 
		/// </summary>
		public static string AGE_FIELD = "AGE";
		/// <summary>
		/// 民族
		/// </summary>
		public static string NATION_FIELD = "NATION";
		/// <summary>
		/// 政治面貌
		/// </summary>
		public static string POLITICALSTATUS_FIELD = "POLITICALSTATUS";
		/// <summary>
		/// 文化程度
		/// </summary>
		public static string EDUCATIONLEVEL_FIELD = "EDUCATIONLEVEL";
		/// <summary>
		/// 所在部门
		/// </summary>
		public static string DEPART_FIELD = "DEPART";
		/// <summary>
		/// 
		/// </summary>
		public static string POST_FIELD = "POST";
		/// <summary>
		/// 岗位
		/// </summary>
		public static string POSITION_FIELD = "POSITION";
		/// <summary>
		/// 级别
		/// </summary>
		public static string POST_LEVEL_FIELD = "POST_LEVEL";
		/// <summary>
		/// 人员分类
		/// </summary>
		public static string EMPLOYE_TYPE_FIELD = "EMPLOYE_TYPE";
		/// <summary>
		/// 现任职时间
		/// </summary>
		public static string POST_DATE_FIELD = "POST_DATE";
		/// <summary>
		/// 编制类别
		/// </summary>
		public static string ORGANIZATION_TYPE_FIELD = "ORGANIZATION_TYPE";
		/// <summary>
		/// 入编时间
		/// </summary>
		public static string ORGANIZATION_DATE_FIELD = "ORGANIZATION_DATE";
		/// <summary>
		/// 工作时间
		/// </summary>
		public static string ENTRYDATE_FIELD = "ENTRYDATE";
		/// <summary>
		/// 聘任专业技术职务
		/// </summary>
		public static string TECHNOLOGY_POST_FIELD = "TECHNOLOGY_POST";
		/// <summary>
		/// 入本单位时间
		/// </summary>
		public static string APPLY_DATE_FIELD = "APPLY_DATE";
		/// <summary>
		/// 毕业院校
		/// </summary>
		public static string GRADUATE_FIELD = "GRADUATE";
		/// <summary>
		/// 所学专业
		/// </summary>
		public static string SPECIALITY_FIELD = "SPECIALITY";
		/// <summary>
		/// 毕业时间
		/// </summary>
		public static string GRADUATE_DATE_FIELD = "GRADUATE_DATE";
		/// <summary>
		/// 专业技术等级
		/// </summary>
		public static string TECHNOLOGY_LEVEL_FIELD = "TECHNOLOGY_LEVEL";
		/// <summary>
		/// 工勤技能等级
		/// </summary>
		public static string SKILL_LEVEL_FIELD = "SKILL_LEVEL";
		/// <summary>
		/// 工作状态,1在职、2离职、3退休
		/// </summary>
		public static string POST_STATUS_FIELD = "POST_STATUS";
		/// <summary>
		/// 备注
		/// </summary>
		public static string INFO_FIELD = "INFO";
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
		
		public TOaEmployeInfoVo()
		{
			this.ID = "";
			this.USER_ID = "";
			this.EMPLOYE_CODE = "";
			this.EMPLOYE_NAME = "";
			this.ID_CARD = "";
			this.SEX = "";
			this.BIRTHDAY = "";
			this.AGE = "";
			this.NATION = "";
			this.POLITICALSTATUS = "";
			this.EDUCATIONLEVEL = "";
			this.DEPART = "";
			this.POST = "";
			this.POSITION = "";
			this.POST_LEVEL = "";
			this.EMPLOYE_TYPE = "";
			this.POST_DATE = "";
			this.ORGANIZATION_TYPE = "";
			this.ORGANIZATION_DATE = "";
			this.ENTRYDATE = "";
			this.TECHNOLOGY_POST = "";
			this.APPLY_DATE = "";
			this.GRADUATE = "";
			this.SPECIALITY = "";
			this.GRADUATE_DATE = "";
			this.TECHNOLOGY_LEVEL = "";
			this.SKILL_LEVEL = "";
			this.POST_STATUS = "";
			this.INFO = "";
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
		/// USER_ID
		/// </summary>
		public string USER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 员工编号
		/// </summary>
		public string EMPLOYE_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 员工姓名
		/// </summary>
		public string EMPLOYE_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 身份证号
		/// </summary>
		public string ID_CARD
		{
			set ;
			get ;
		}
		/// <summary>
		/// 性别
		/// </summary>
		public string SEX
		{
			set ;
			get ;
		}
		/// <summary>
		/// 出生日期
		/// </summary>
		public string BIRTHDAY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 
		/// </summary>
		public string AGE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 民族
		/// </summary>
		public string NATION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 政治面貌
		/// </summary>
		public string POLITICALSTATUS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 文化程度
		/// </summary>
		public string EDUCATIONLEVEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 所在部门
		/// </summary>
		public string DEPART
		{
			set ;
			get ;
		}
		/// <summary>
		/// 
		/// </summary>
		public string POST
		{
			set ;
			get ;
		}
		/// <summary>
		/// 岗位
		/// </summary>
		public string POSITION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 级别
		/// </summary>
		public string POST_LEVEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 人员分类
		/// </summary>
		public string EMPLOYE_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 现任职时间
		/// </summary>
		public string POST_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 编制类别
		/// </summary>
		public string ORGANIZATION_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 入编时间
		/// </summary>
		public string ORGANIZATION_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 工作时间
		/// </summary>
		public string ENTRYDATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 聘任专业技术职务
		/// </summary>
		public string TECHNOLOGY_POST
		{
			set ;
			get ;
		}
		/// <summary>
		/// 入本单位时间
		/// </summary>
		public string APPLY_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 毕业院校
		/// </summary>
		public string GRADUATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 所学专业
		/// </summary>
		public string SPECIALITY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 毕业时间
		/// </summary>
		public string GRADUATE_DATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 专业技术等级
		/// </summary>
		public string TECHNOLOGY_LEVEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 工勤技能等级
		/// </summary>
		public string SKILL_LEVEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 工作状态,1在职、2离职、3退休
		/// </summary>
		public string POST_STATUS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string INFO
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