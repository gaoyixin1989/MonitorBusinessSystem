using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Base.Apparatus
{
    /// <summary>
    /// 功能：仪器信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusInfoVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_BASE_APPARATUS_INFO_TABLE = "T_BASE_APPARATUS_INFO";
		//静态字段引用
		/// <summary>
		/// ID
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 仪器编号
		/// </summary>
		public static string APPARATUS_CODE_FIELD = "APPARATUS_CODE";
		/// <summary>
		/// 档案类别
		/// </summary>
		public static string ARCHIVES_TYPE_FIELD = "ARCHIVES_TYPE";
		/// <summary>
		/// 类别1(辅助，非辅助)
		/// </summary>
		public static string SORT1_FIELD = "SORT1";
		/// <summary>
		/// 类别2(L流量计，离子计,汽车尾气.......)
		/// </summary>
		public static string SORT2_FIELD = "SORT2";
		/// <summary>
		/// 所属仪器或者项目
		/// </summary>
		public static string BELONG_TO_FIELD = "BELONG_TO";
		/// <summary>
		/// 仪器名称
		/// </summary>
		public static string NAME_FIELD = "NAME";
		/// <summary>
		/// 规格型号
		/// </summary>
		public static string MODEL_FIELD = "MODEL";
		/// <summary>
		/// 出厂编号
		/// </summary>
		public static string SERIAL_NO_FIELD = "SERIAL_NO";
		/// <summary>
		/// 仪器供应商
		/// </summary>
		public static string APPARATUS_PROVIDER_FIELD = "APPARATUS_PROVIDER";
		/// <summary>
		/// 配件供应商
		/// </summary>
		public static string FITTINGS_PROVIDER_FIELD = "FITTINGS_PROVIDER";
		/// <summary>
		/// 仪器供应商网址
		/// </summary>
		public static string WEB_SITE_FIELD = "WEB_SITE";
		/// <summary>
		/// 仪器性能(合格,一级合格,正常)
		/// </summary>
		public static string CAPABILITY_FIELD = "CAPABILITY";
		/// <summary>
		/// 联系人
		/// </summary>
		public static string LINK_MAN_FIELD = "LINK_MAN";
		/// <summary>
		/// 联系电话
		/// </summary>
		public static string LINK_PHONE_FIELD = "LINK_PHONE";
		/// <summary>
		/// 邮编
		/// </summary>
		public static string POST_FIELD = "POST";
		/// <summary>
		/// 联系地址
		/// </summary>
		public static string ADDRESS_FIELD = "ADDRESS";
        /// <summary>
        /// 购买时间
        /// </summary>
        public static string BUY_TIME_FIELD = "BUY_TIME";
        /// <summary>
        /// 报废时间
        /// </summary>
        public static string SCRAP_TIME_FIELD = "SCRAP_TIME";
		/// <summary>
		/// 量值溯源方式(校准、自校、检定)
		/// </summary>
		public static string CERTIFICATE_TYPE_FIELD = "CERTIFICATE_TYPE";
		/// <summary>
		/// 溯源结果(合格，不合格)
		/// </summary>
		public static string TRACE_RESULT_FIELD = "TRACE_RESULT";
		/// <summary>
		/// 检定方式(不检，来检,送检,暂不能检，不详)
		/// </summary>
		public static string TEST_MODE_FIELD = "TEST_MODE";
		/// <summary>
		/// 校正周期
		/// </summary>
		public static string VERIFY_CYCLE_FIELD = "VERIFY_CYCLE";
		/// <summary>
		/// 使用科室
		/// </summary>
		public static string DEPT_FIELD = "DEPT";
		/// <summary>
		/// 保管人
		/// </summary>
		public static string KEEPER_FIELD = "KEEPER";
		/// <summary>
		/// 放置地点
		/// </summary>
		public static string POSITION_FIELD = "POSITION";
		/// <summary>
		/// 使用状况(在用，未用)
		/// </summary>
		public static string STATUS_FIELD = "STATUS";
		/// <summary>
		/// 档案上传地址
		/// </summary>
		public static string ARCHIVES_ADDRESS_FIELD = "ARCHIVES_ADDRESS";
		/// <summary>
		/// 最近检定/校准时间
		/// </summary>
		public static string BEGIN_TIME_FIELD = "BEGIN_TIME";
		/// <summary>
		/// 到期检定/校准时间
		/// </summary>
		public static string END_TIME_FIELD = "END_TIME";
		/// <summary>
		/// 扩展不确定度
		/// </summary>
		public static string EXPANDED_UNCETAINTY_FIELD = "EXPANDED_UNCETAINTY";
		/// <summary>
		/// 测量范围
		/// </summary>
		public static string MEASURING_RANGE_FIELD = "MEASURING_RANGE";
		/// <summary>
		/// 检定单位
		/// </summary>
		public static string EXAMINE_DEPARTMENT_FIELD = "EXAMINE_DEPARTMENT";
		/// <summary>
		/// 检定单位电话
		/// </summary>
		public static string DEPARTMENT_PHONE_FIELD = "DEPARTMENT_PHONE";
		/// <summary>
		/// 检定单位联系人
		/// </summary>
		public static string DEPARTMENT_LINKMAN_FIELD = "DEPARTMENT_LINKMAN";
		/// <summary>
		/// 期间核查方式
		/// </summary>
		public static string VERIFICATION_WAY_FIELD = "VERIFICATION_WAY";
		/// <summary>
		/// 期间核查结果
		/// </summary>
		public static string VERIFICATION_RESULT_FIELD = "VERIFICATION_RESULT";
		/// <summary>
		/// 最近期间核查时间
		/// </summary>
		public static string VERIFICATION_BEGIN_TIME_FIELD = "VERIFICATION_BEGIN_TIME";
		/// <summary>
		/// 最近期间核查时间
		/// </summary>
		public static string VERIFICATION_END_TIME_FIELD = "VERIFICATION_END_TIME";
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
		
		public TBaseApparatusInfoVo()
		{
			this.ID = "";
			this.APPARATUS_CODE = "";
			this.ARCHIVES_TYPE = "";
			this.SORT1 = "";
			this.SORT2 = "";
			this.BELONG_TO = "";
			this.NAME = "";
			this.MODEL = "";
			this.SERIAL_NO = "";
			this.APPARATUS_PROVIDER = "";
			this.FITTINGS_PROVIDER = "";
			this.WEB_SITE = "";
			this.CAPABILITY = "";
			this.LINK_MAN = "";
			this.LINK_PHONE = "";
			this.POST = "";
			this.ADDRESS = "";
            this.BUY_TIME = "";
            this.SCRAP_TIME = "";
			this.CERTIFICATE_TYPE = "";
			this.TRACE_RESULT = "";
			this.TEST_MODE = "";
			this.VERIFY_CYCLE = "";
			this.DEPT = "";
			this.KEEPER = "";
			this.POSITION = "";
			this.STATUS = "";
			this.ARCHIVES_ADDRESS = "";
			this.BEGIN_TIME = "";
			this.END_TIME = "";
			this.EXPANDED_UNCETAINTY = "";
			this.MEASURING_RANGE = "";
			this.EXAMINE_DEPARTMENT = "";
			this.DEPARTMENT_PHONE = "";
			this.DEPARTMENT_LINKMAN = "";
			this.VERIFICATION_WAY = "";
			this.VERIFICATION_RESULT = "";
			this.VERIFICATION_BEGIN_TIME = "";
			this.VERIFICATION_END_TIME = "";
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
		/// 仪器编号
		/// </summary>
		public string APPARATUS_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 档案类别
		/// </summary>
		public string ARCHIVES_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 类别1(辅助，非辅助)
		/// </summary>
		public string SORT1
		{
			set ;
			get ;
		}
		/// <summary>
		/// 类别2(l流量计，离子计,汽车尾气.......)
		/// </summary>
		public string SORT2
		{
			set ;
			get ;
		}
		/// <summary>
		/// 所属仪器或者项目
		/// </summary>
		public string BELONG_TO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 仪器名称
		/// </summary>
		public string NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 规格型号
		/// </summary>
		public string MODEL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 出厂编号
		/// </summary>
		public string SERIAL_NO
		{
			set ;
			get ;
		}
		/// <summary>
		/// 仪器供应商
		/// </summary>
		public string APPARATUS_PROVIDER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 配件供应商
		/// </summary>
		public string FITTINGS_PROVIDER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 仪器供应商网址
		/// </summary>
		public string WEB_SITE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 仪器性能(合格,一级合格,正常)
		/// </summary>
		public string CAPABILITY
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
		public string LINK_PHONE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 邮编
		/// </summary>
		public string POST
		{
			set ;
			get ;
		}
		/// <summary>
		/// 联系地址
		/// </summary>
		public string ADDRESS
		{
			set ;
			get ;
		}
        /// <summary>
        /// 购买时间
        /// </summary>
        public string BUY_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 报废时间
        /// </summary>
        public string SCRAP_TIME
        {
            set;
            get;
        }
		/// <summary>
		/// 量值溯源方式(校准、自校、检定)
		/// </summary>
		public string CERTIFICATE_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 溯源结果(合格，不合格)
		/// </summary>
		public string TRACE_RESULT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 检定方式(不检，来检,送检,暂不能检，不详)
		/// </summary>
		public string TEST_MODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 校正周期
		/// </summary>
		public string VERIFY_CYCLE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 使用科室
		/// </summary>
		public string DEPT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 保管人
		/// </summary>
		public string KEEPER
		{
			set ;
			get ;
		}
		/// <summary>
		/// 放置地点
		/// </summary>
		public string POSITION
		{
			set ;
			get ;
		}
		/// <summary>
		/// 使用状况(在用，未用)
		/// </summary>
		public string STATUS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 档案上传地址
		/// </summary>
		public string ARCHIVES_ADDRESS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最近检定/校准时间
		/// </summary>
		public string BEGIN_TIME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 到期检定/校准时间
		/// </summary>
		public string END_TIME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 扩展不确定度
		/// </summary>
		public string EXPANDED_UNCETAINTY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 测量范围
		/// </summary>
		public string MEASURING_RANGE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 检定单位
		/// </summary>
		public string EXAMINE_DEPARTMENT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 检定单位电话
		/// </summary>
		public string DEPARTMENT_PHONE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 检定单位联系人
		/// </summary>
		public string DEPARTMENT_LINKMAN
		{
			set ;
			get ;
		}
		/// <summary>
		/// 期间核查方式
		/// </summary>
		public string VERIFICATION_WAY
		{
			set ;
			get ;
		}
		/// <summary>
		/// 期间核查结果
		/// </summary>
		public string VERIFICATION_RESULT
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最近期间核查时间
		/// </summary>
		public string VERIFICATION_BEGIN_TIME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 最近期间核查时间
		/// </summary>
		public string VERIFICATION_END_TIME
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