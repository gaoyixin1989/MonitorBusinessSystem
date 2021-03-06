using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.OA.TRAIN
{
    /// <summary>
    /// 功能：员工培训记录
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaTrainInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_OA_TRAIN_INFO_TABLE = "T_OA_TRAIN_INFO";
		//静态字段引用
		/// <summary>
		/// 编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 培训ID
		/// </summary>
		public static string TRAIN_ID_FIELD = "TRAIN_ID";
		/// <summary>
		/// 员工ID
		/// </summary>
		public static string EMPLOYE_ID_FIELD = "EMPLOYE_ID";
		/// <summary>
		/// 培训教材评估
		/// </summary>
		public static string ENTRYDATE_FIELD = "ENTRYDATE";
		/// <summary>
		/// 培训教师评估
		/// </summary>
		public static string TRAINDATE_FIELD = "TRAINDATE";
		/// <summary>
		/// 培训成绩
		/// </summary>
		public static string TRAINPROJECT_FIELD = "TRAINPROJECT";
		/// <summary>
		/// 培训结果
		/// </summary>
		public static string TRAINRESULT_FIELD = "TRAINRESULT";
		/// <summary>
		/// 证书编号
		/// </summary>
		public static string CERTIFICATECODE_FIELD = "CERTIFICATECODE";
		/// <summary>
		/// 自我总结
		/// </summary>
		public static string TRAINCONTENT_FIELD = "TRAINCONTENT";
		/// <summary>
		/// 总结时间
		/// </summary>
		public static string REMARKS_FIELD = "REMARKS";
		/// <summary>
		/// 理论掌握能力评估
		/// </summary>
		public static string THEORY_SKILL_FIELD = "THEORY_SKILL";
		/// <summary>
		/// 实际操作能力评估
		/// </summary>
		public static string OPER_SKILL_FIELD = "OPER_SKILL";
		/// <summary>
		/// 样品和质控样分析能力评估
		/// </summary>
		public static string TEST_SKILL_FIELD = "TEST_SKILL";
		/// <summary>
		/// 评价结论
		/// </summary>
		public static string JUDGE_FIELD = "JUDGE";
		/// <summary>
		/// 评价人ID
		/// </summary>
		public static string JUDGE_ID_FIELD = "JUDGE_ID";
		/// <summary>
		/// 评价日期
		/// </summary>
		public static string JUDGE_DATE_FIELD = "JUDGE_DATE";
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
		
		public TOaTrainInfoVo()
		{
			this.ID = "";
			this.TRAIN_ID = "";
			this.EMPLOYE_ID = "";
			this.ENTRYDATE = "";
			this.TRAINDATE = "";
			this.TRAINPROJECT = "";
			this.TRAINRESULT = "";
			this.CERTIFICATECODE = "";
			this.TRAINCONTENT = "";
			this.REMARKS = "";
			this.THEORY_SKILL = "";
			this.OPER_SKILL = "";
			this.TEST_SKILL = "";
			this.JUDGE = "";
			this.JUDGE_ID = "";
			this.JUDGE_DATE = "";
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
		/// 培训ID
		/// </summary>
		public string TRAIN_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 员工ID
		/// </summary>
		public string EMPLOYE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训教材评估
		/// </summary>
		public string ENTRYDATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训教师评估
		/// </summary>
		public string TRAINDATE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训成绩
		/// </summary>
		public string TRAINPROJECT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 培训结果
		/// </summary>
		public string TRAINRESULT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 证书编号
		/// </summary>
		public string CERTIFICATECODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 自我总结
		/// </summary>
		public string TRAINCONTENT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 总结时间
		/// </summary>
		public string REMARKS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 理论掌握能力评估
		/// </summary>
		public string THEORY_SKILL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 实际操作能力评估
		/// </summary>
		public string OPER_SKILL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 样品和质控样分析能力评估
		/// </summary>
		public string TEST_SKILL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 评价结论
		/// </summary>
		public string JUDGE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 评价人ID
		/// </summary>
		public string JUDGE_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 评价日期
		/// </summary>
		public string JUDGE_DATE
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