using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Sys.General
{
    /// <summary>
    /// 功能：用户管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysUserVo : i3.Core.ValueObject.ObjectBase
    {
		
		#region 静态引用
		//静态表格引用
		/// <summary>
		/// 对象对应的表格名称
		/// </summary>
		public static string T_SYS_USER_TABLE = "T_SYS_USER";
		//静态字段引用
		/// <summary>
		/// 用户编号
		/// </summary>
		public static string ID_FIELD = "ID";
		/// <summary>
		/// 用户登录名
		/// </summary>
		public static string USER_NAME_FIELD = "USER_NAME";
		/// <summary>
		/// 真实姓名
		/// </summary>
		public static string REAL_NAME_FIELD = "REAL_NAME";
		/// <summary>
		/// 名次排序
		/// </summary>
		public static string ORDER_ID_FIELD = "ORDER_ID";
		/// <summary>
		/// 用户类型
		/// </summary>
		public static string USER_TYPE_FIELD = "USER_TYPE";
		/// <summary>
		/// 登录密码
		/// </summary>
		public static string USER_PWD_FIELD = "USER_PWD";
		/// <summary>
		/// 单位编码
		/// </summary>
		public static string UNITS_CODE_FIELD = "UNITS_CODE";
		/// <summary>
		/// 地区编码
		/// </summary>
		public static string REGION_CODE_FIELD = "REGION_CODE";
		/// <summary>
		/// 职务编码
		/// </summary>
		public static string DUTY_CODE_FIELD = "DUTY_CODE";
		/// <summary>
		/// 业务代码
		/// </summary>
		public static string BUSINESS_CODE_FIELD = "BUSINESS_CODE";
		/// <summary>
		/// 性别
		/// </summary>
		public static string SEX_FIELD = "SEX";
		/// <summary>
		/// 出生日期
		/// </summary>
		public static string BIRTHDAY_FIELD = "BIRTHDAY";
		/// <summary>
		/// 邮件地址
		/// </summary>
		public static string EMAIL_FIELD = "EMAIL";
		/// <summary>
		/// 详细地址
		/// </summary>
		public static string ADDRESS_FIELD = "ADDRESS";
		/// <summary>
		/// 邮编
		/// </summary>
		public static string POSTCODE_FIELD = "POSTCODE";
		/// <summary>
		/// 手机号码
		/// </summary>
		public static string PHONE_MOBILE_FIELD = "PHONE_MOBILE";
		/// <summary>
		/// 家庭电话
		/// </summary>
		public static string PHONE_HOME_FIELD = "PHONE_HOME";
		/// <summary>
		/// 办公电话
		/// </summary>
		public static string PHONE_OFFICE_FIELD = "PHONE_OFFICE";
		/// <summary>
		/// 限定登录IP
		/// </summary>
		public static string ALLOW_IP_FIELD = "ALLOW_IP";
		/// <summary>
		/// 启用标记,1启用,0停用
		/// </summary>
		public static string IS_USE_FIELD = "IS_USE";
		/// <summary>
		/// 删除标记,1为删除,
		/// </summary>
		public static string IS_DEL_FIELD = "IS_DEL";
        /// <summary>
        /// 隐藏标记,1为隐藏,
        /// </summary>
        public static string IS_HIDE_FIELD = "IS_HIDE";

        /// <summary>
        /// 苹果手机MAC地址
        /// </summary>
        public static string IOS_MAC_FIELD = "IOS_MAC";
        /// <summary>
        /// 苹果手机是否启用,1启用,0停用
        /// </summary>
        public static string IF_IOS_FIELD = "IF_IOS";
        /// <summary>
        /// 安卓手机MAC地址
        /// </summary>
        public static string ANDROID_MAC_FIELD = "ANDROID_MAC";
        /// <summary>
        /// 安卓手机是否启用,1启用,0停用
        /// </summary>
        public static string IF_ANDROID_FIELD = "IF_ANDROID";

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
		/// <summary>
		/// 备注4
		/// </summary>
		public static string REMARK4_FIELD = "REMARK4";
		/// <summary>
		/// 备注5
		/// </summary>
		public static string REMARK5_FIELD = "REMARK5";
		
		#endregion
		
		public TSysUserVo()
		{
			this.ID = "";
			this.USER_NAME = "";
			this.REAL_NAME = "";
			this.ORDER_ID = "";
			this.USER_TYPE = "";
			this.USER_PWD = "";
			this.UNITS_CODE = "";
			this.REGION_CODE = "";
			this.DUTY_CODE = "";
			this.BUSINESS_CODE = "";
			this.SEX = "";
			this.BIRTHDAY = "";
			this.EMAIL = "";
			this.ADDRESS = "";
			this.POSTCODE = "";
			this.PHONE_MOBILE = "";
			this.PHONE_HOME = "";
			this.PHONE_OFFICE = "";
			this.ALLOW_IP = "";
			this.IS_USE = "";
			this.IS_DEL = "";
            this.IS_HIDE = "";

            this.IOS_MAC = "";
            this.IF_IOS = "";
            this.ANDROID_MAC = "";
            this.IF_ANDROID = "";

			this.CREATE_ID = "";
			this.CREATE_TIME = "";
            
			this.REMARK = "";
			this.REMARK1 = "";
			this.REMARK2 = "";
			this.REMARK3 = "";
			this.REMARK4 = "";
			this.REMARK5 = "";
		}
		
		#region 属性
			/// <summary>
		/// 用户编号
		/// </summary>
		public string ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 用户登录名
		/// </summary>
		public string USER_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 真实姓名
		/// </summary>
		public string REAL_NAME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 名次排序
		/// </summary>
		public string ORDER_ID
		{
			set ;
			get ;
		}
		/// <summary>
		/// 用户类型
		/// </summary>
		public string USER_TYPE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 登录密码
		/// </summary>
		public string USER_PWD
		{
			set ;
			get ;
		}
		/// <summary>
		/// 单位编码
		/// </summary>
		public string UNITS_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 地区编码
		/// </summary>
		public string REGION_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 职务编码
		/// </summary>
		public string DUTY_CODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 业务代码
		/// </summary>
		public string BUSINESS_CODE
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
		/// 邮件地址
		/// </summary>
		public string EMAIL
		{
			set ;
			get ;
		}
		/// <summary>
		/// 详细地址
		/// </summary>
		public string ADDRESS
		{
			set ;
			get ;
		}
		/// <summary>
		/// 邮编
		/// </summary>
		public string POSTCODE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 手机号码
		/// </summary>
		public string PHONE_MOBILE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 家庭电话
		/// </summary>
		public string PHONE_HOME
		{
			set ;
			get ;
		}
		/// <summary>
		/// 办公电话
		/// </summary>
		public string PHONE_OFFICE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 限定登录IP
		/// </summary>
		public string ALLOW_IP
		{
			set ;
			get ;
		}
		/// <summary>
		/// 启用标记,1启用,0停用
		/// </summary>
		public string IS_USE
		{
			set ;
			get ;
		}
		/// <summary>
		/// 删除标记,1为删除,
		/// </summary>
		public string IS_DEL
		{
			set ;
			get ;
		}
        /// <summary>
        /// 隐藏标记,1为隐藏,
        /// </summary>
        public string IS_HIDE
        {
            set;
            get;
        }

        /// <summary>
        /// 苹果手机MAC地址
        /// </summary>
        public string IOS_MAC
        {
            set;
            get;
        }
        /// <summary>
        /// 苹果手机是否启用
        /// </summary>
        public string IF_IOS
        {
            set;
            get;
        }
        /// <summary>
        /// 安卓手机MAC地址
        /// </summary>
        public string ANDROID_MAC
        {
            set;
            get;
        }
        /// <summary>
        /// 安卓手机是否启用
        /// </summary>
        public string IF_ANDROID
        {
            set;
            get;
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
    /// <summary>
    /// 功能：用户登陆错误
    /// 创建日期：2011-04-12
    /// 创建人：郑义
    /// </summary>
    public class UserLogError
    {
        public string _userName;
        public string _loginIP;
        public string _loginType;
        public int _loginTimes;
        public DateTime _loginStartTime;
        public UserLogError()
        {
            _userName = "";
            _loginIP = "";
            _loginType = "";
            _loginTimes = 0;
            _loginStartTime = DateTime.MinValue;
        }
    }
}