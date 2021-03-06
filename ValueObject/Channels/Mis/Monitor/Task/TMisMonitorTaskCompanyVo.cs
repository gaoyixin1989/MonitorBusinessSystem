using System;
using System.Collections.Generic;
using System.Text;

namespace i3.ValueObject.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务企业信息表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskCompanyVo : i3.Core.ValueObject.ObjectBase
    {

        #region 静态引用
        //静态表格引用
        /// <summary>
        /// 对象对应的表格名称
        /// </summary>
        public static string T_MIS_MONITOR_TASK_COMPANY_TABLE = "T_MIS_MONITOR_TASK_COMPANY";
        //静态字段引用
        /// <summary>
        /// ID
        /// </summary>
        public static string ID_FIELD = "ID";
        /// <summary>
        /// 任务ID
        /// </summary>
        public static string TASK_ID_FIELD = "TASK_ID";
        /// <summary>
        /// 委托书企业ID
        /// </summary>
        public static string COMPANY_ID_FIELD = "COMPANY_ID";
        /// <summary>
        /// 企业法人代码
        /// </summary>
        public static string COMPANY_CODE_FIELD = "COMPANY_CODE";
        /// <summary>
        /// 企业名称
        /// </summary>
        public static string COMPANY_NAME_FIELD = "COMPANY_NAME";
        /// <summary>
        /// 拼音编码
        /// </summary>
        public static string PINYIN_FIELD = "PINYIN";
        /// <summary>
        /// 主管部门
        /// </summary>
        public static string DIRECTOR_DEPT_FIELD = "DIRECTOR_DEPT";
        /// <summary>
        /// 经济类型
        /// </summary>
        public static string ECONOMY_TYPE_FIELD = "ECONOMY_TYPE";
        /// <summary>
        /// 所在区域
        /// </summary>
        public static string AREA_FIELD = "AREA";
        /// <summary>
        /// 企业规模
        /// </summary>
        public static string SIZE_FIELD = "SIZE";
        /// <summary>
        /// 污染源类型
        /// </summary>
        public static string POLLUTE_TYPE_FIELD = "POLLUTE_TYPE";
        /// <summary>
        /// 行业类别
        /// </summary>
        public static string INDUSTRY_FIELD = "INDUSTRY";
        /// <summary>
        /// 废气控制级别
        /// </summary>
        public static string GAS_LEAVEL_FIELD = "GAS_LEAVEL";
        /// <summary>
        /// 废水控制级别
        /// </summary>
        public static string WATER_LEAVEL_FIELD = "WATER_LEAVEL";
        /// <summary>
        /// 开业时间
        /// </summary>
        public static string PRACTICE_DATE_FIELD = "PRACTICE_DATE";
        /// <summary>
        /// 联系人
        /// </summary>
        public static string CONTACT_NAME_FIELD = "CONTACT_NAME";
        /// <summary>
        /// 联系部门
        /// </summary>
        public static string LINK_DEPT_FIELD = "LINK_DEPT";
        /// <summary>
        /// 电子邮件
        /// </summary>
        public static string EMAIL_FIELD = "EMAIL";
        /// <summary>
        /// 联系电话
        /// </summary>
        public static string LINK_PHONE_FIELD = "LINK_PHONE";
        /// <summary>
        /// 委托代理人
        /// </summary>
        public static string FACTOR_FIELD = "FACTOR";
        /// <summary>
        /// 办公电话
        /// </summary>
        public static string PHONE_FIELD = "PHONE";
        /// <summary>
        /// 移动电话
        /// </summary>
        public static string MOBAIL_PHONE_FIELD = "MOBAIL_PHONE";
        /// <summary>
        /// 传真号码
        /// </summary>
        public static string FAX_FIELD = "FAX";
        /// <summary>
        /// 邮政编码
        /// </summary>
        public static string POST_FIELD = "POST";
        /// <summary>
        /// 联系地址
        /// </summary>
        public static string CONTACT_ADDRESS_FIELD = "CONTACT_ADDRESS";
        /// <summary>
        /// 监测地址
        /// </summary>
        public static string MONITOR_ADDRESS_FIELD = "MONITOR_ADDRESS";
        /// <summary>
        /// 企业网址
        /// </summary>
        public static string WEB_SITE_FIELD = "WEB_SITE";
        /// <summary>
        /// 经度
        /// </summary>
        public static string LONGITUDE_FIELD = "LONGITUDE";
        /// <summary>
        /// 纬度
        /// </summary>
        public static string LATITUDE_FIELD = "LATITUDE";
        /// <summary>
        /// 是否删除
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
        //add by ssz for QHD 2013-1-16
        /// <summary>
        /// 企业级别
        /// </summary>
        public static string COMPANY_LEVEL_FIELD = "COMPANY_LEVEL";
        /// <summary>
        /// 法人代表
        /// </summary>
        public static string COMPANY_MAN_FIELD = "COMPANY_MAN";
        /// <summary>
        /// 废水最终排放去向
        /// </summary>
        public static string WATER_FOLLOW_FIELD = "WATER_FOLLOW";
        /// <summary>
        /// 现有工程环评批复时间及文号
        /// </summary>
        public static string CHECK_TIME_FIELD = "CHECK_TIME";
        /// <summary>
        /// 现有工程竣工环境保护验收时间
        /// </summary>
        public static string ACCEPTANCE_TIME_FIELD = "ACCEPTANCE_TIME";
        /// <summary>
        /// 执行标准
        /// </summary>
        public static string STANDARD_FIELD = "STANDARD";
        /// <summary>
        /// 主要环保设施名称、数量
        /// </summary>
        public static string MAIN_APPARATUS_FIELD = "MAIN_APPARATUS";
        /// <summary>
        /// 环保设施运行情况
        /// </summary>
        public static string APPARATUS_STATUS_FIELD = "APPARATUS_STATUS";
        /// <summary>
        /// 主要产品名称
        /// </summary>
        public static string MAIN_PROJECT_FIELD = "MAIN_PROJECT";
        /// <summary>
        /// 主要生产原料
        /// </summary>
        public static string MAIN_GOOD_FIELD = "MAIN_GOOD";
        /// <summary>
        /// 设计生产能力
        /// </summary>
        public static string DESIGN_ANBILITY_FIELD = "DESIGN_ANBILITY";
        /// <summary>
        /// 实际生产能力
        /// </summary>
        public static string ANBILITY_FIELD = "ANBILITY";
        /// <summary>
        /// 监测期间生产负荷（%）
        /// </summary>
        public static string CONTRACT_PER_FIELD = "CONTRACT_PER";
        /// <summary>
        /// 全年平均生产负荷（%）
        /// </summary>
        public static string AVG_PER_FIELD = "AVG_PER";
        /// <summary>
        /// 废水排放量
        /// </summary>
        public static string WATER_COUNT_FIELD = "WATER_COUNT";
        /// <summary>
        /// 年运行时间
        /// </summary>
        public static string YEAR_TIME_FIELD = "YEAR_TIME";
        #endregion

        public TMisMonitorTaskCompanyVo()
        {
            this.ID = "";
            this.TASK_ID = "";
            this.COMPANY_ID = "";
            this.COMPANY_CODE = "";
            this.COMPANY_NAME = "";
            this.PINYIN = "";
            this.DIRECTOR_DEPT = "";
            this.ECONOMY_TYPE = "";
            this.AREA = "";
            this.SIZE = "";
            this.POLLUTE_TYPE = "";
            this.INDUSTRY = "";
            this.GAS_LEAVEL = "";
            this.WATER_LEAVEL = "";
            this.PRACTICE_DATE = "";
            this.CONTACT_NAME = "";
            this.LINK_DEPT = "";
            this.EMAIL = "";
            this.LINK_PHONE = "";
            this.FACTOR = "";
            this.PHONE = "";
            this.MOBAIL_PHONE = "";
            this.FAX = "";
            this.POST = "";
            this.CONTACT_ADDRESS = "";
            this.MONITOR_ADDRESS = "";
            this.WEB_SITE = "";
            this.LONGITUDE = "";
            this.LATITUDE = "";
            this.IS_DEL = "";
            this.REMARK1 = "";
            this.REMARK2 = "";
            this.REMARK3 = "";
            this.REMARK4 = "";
            this.REMARK5 = "";

            this.COMPANY_LEVEL = "";
            this.COMPANY_MAN = "";
            this.WATER_FOLLOW = "";
            this.CHECK_TIME = "";
            this.ACCEPTANCE_TIME = "";
            this.STANDARD = "";
            this.MAIN_APPARATUS = "";
            this.APPARATUS_STATUS = "";
            this.MAIN_PROJECT = "";
            this.MAIN_GOOD = "";
            this.DESIGN_ANBILITY = "";
            this.ANBILITY = "";
            this.CONTRACT_PER = "";
            this.AVG_PER = "";
            this.WATER_COUNT = "";
            this.YEAR_TIME = "";
        }

        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            set;
            get;
        }
        /// <summary>
        /// 任务ID
        /// </summary>
        public string TASK_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 委托书企业ID
        /// </summary>
        public string COMPANY_ID
        {
            set;
            get;
        }
        /// <summary>
        /// 企业法人代码
        /// </summary>
        public string COMPANY_CODE
        {
            set;
            get;
        }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string COMPANY_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 拼音编码
        /// </summary>
        public string PINYIN
        {
            set;
            get;
        }
        /// <summary>
        /// 主管部门
        /// </summary>
        public string DIRECTOR_DEPT
        {
            set;
            get;
        }
        /// <summary>
        /// 经济类型
        /// </summary>
        public string ECONOMY_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 所在区域
        /// </summary>
        public string AREA
        {
            set;
            get;
        }
        /// <summary>
        /// 企业规模
        /// </summary>
        public string SIZE
        {
            set;
            get;
        }
        /// <summary>
        /// 污染源类型
        /// </summary>
        public string POLLUTE_TYPE
        {
            set;
            get;
        }
        /// <summary>
        /// 行业类别
        /// </summary>
        public string INDUSTRY
        {
            set;
            get;
        }
        /// <summary>
        /// 废气控制级别
        /// </summary>
        public string GAS_LEAVEL
        {
            set;
            get;
        }
        /// <summary>
        /// 废水控制级别
        /// </summary>
        public string WATER_LEAVEL
        {
            set;
            get;
        }
        /// <summary>
        /// 开业时间
        /// </summary>
        public string PRACTICE_DATE
        {
            set;
            get;
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string CONTACT_NAME
        {
            set;
            get;
        }
        /// <summary>
        /// 联系部门
        /// </summary>
        public string LINK_DEPT
        {
            set;
            get;
        }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string EMAIL
        {
            set;
            get;
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LINK_PHONE
        {
            set;
            get;
        }
        /// <summary>
        /// 委托代理人
        /// </summary>
        public string FACTOR
        {
            set;
            get;
        }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string PHONE
        {
            set;
            get;
        }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string MOBAIL_PHONE
        {
            set;
            get;
        }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string FAX
        {
            set;
            get;
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string POST
        {
            set;
            get;
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string CONTACT_ADDRESS
        {
            set;
            get;
        }
        /// <summary>
        /// 监测地址
        /// </summary>
        public string MONITOR_ADDRESS
        {
            set;
            get;
        }
        /// <summary>
        /// 企业网址
        /// </summary>
        public string WEB_SITE
        {
            set;
            get;
        }
        /// <summary>
        /// 经度
        /// </summary>
        public string LONGITUDE
        {
            set;
            get;
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public string LATITUDE
        {
            set;
            get;
        }
        /// <summary>
        /// 是否删除
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
            set;
            get;
        }
        /// <summary>
        /// 备注2
        /// </summary>
        public string REMARK2
        {
            set;
            get;
        }
        /// <summary>
        /// 备注3
        /// </summary>
        public string REMARK3
        {
            set;
            get;
        }
        /// <summary>
        /// 备注4
        /// </summary>
        public string REMARK4
        {
            set;
            get;
        }
        /// <summary>
        /// 备注5
        /// </summary>
        public string REMARK5
        {
            set;
            get;
        }
        /// <summary>
        /// 企业级别
        /// </summary>
        public string COMPANY_LEVEL
        {
            set;
            get;
        }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string COMPANY_MAN
        {
            set;
            get;
        }
        /// <summary>
        /// 废水最终排放去向
        /// </summary>
        public string WATER_FOLLOW
        {
            set;
            get;
        }
        /// <summary>
        /// 现有工程环评批复时间及文号
        /// </summary>
        public string CHECK_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 现有工程竣工环境保护验收时间
        /// </summary>
        public string ACCEPTANCE_TIME
        {
            set;
            get;
        }
        /// <summary>
        /// 执行标准
        /// </summary>
        public string STANDARD
        {
            set;
            get;
        }
        /// <summary>
        /// 主要环保设施名称、数量
        /// </summary>
        public string MAIN_APPARATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 环保设施运行情况
        /// </summary>
        public string APPARATUS_STATUS
        {
            set;
            get;
        }
        /// <summary>
        /// 主要产品名称
        /// </summary>
        public string MAIN_PROJECT
        {
            set;
            get;
        }
        /// <summary>
        /// 主要生产原料
        /// </summary>
        public string MAIN_GOOD
        {
            set;
            get;
        }
        /// <summary>
        /// 设计生产能力
        /// </summary>
        public string DESIGN_ANBILITY
        {
            set;
            get;
        }
        /// <summary>
        /// 实际生产能力
        /// </summary>
        public string ANBILITY
        {
            set;
            get;
        }
        /// <summary>
        /// 监测期间生产负荷（%）
        /// </summary>
        public string CONTRACT_PER
        {
            set;
            get;
        }
        /// <summary>
        /// 全年平均生产负荷（%）
        /// </summary>
        public string AVG_PER
        {
            set;
            get;
        }
        /// <summary>
        /// 废水排放量
        /// </summary>
        public string WATER_COUNT
        {
            set;
            get;
        }
        /// <summary>
        /// 年运行时间
        /// </summary>
        public string YEAR_TIME
        {
            set;
            get;
        }

        #endregion

    }
}