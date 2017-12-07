using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i3.ValueObject
{
    /// <summary>
    /// 功能描述：数据对象基类
    /// 创建日期：2011-4-6 20:53:35
    /// 创 建 人：陈国迎
    /// </summary>
    public struct ObjectBase
    {
        /// <summary>
        /// 字典项绑定列
        /// </summary>
        public struct DataDictBindFields
        {
            /// <summary>
            /// 文本
            /// </summary>
            public static string DataTextField = "DICT_TEXT";
            /// <summary>
            /// 值
            /// </summary>
            public static string DataValueField = "DICT_CODE";
        }
        /// <summary>
        /// 序列号类型
        /// </summary>
        public struct SerialType
        {
            #region 系统相关序列号
            public static string SerialId = "serial_id";
            public static string DictId = "dict_id";
            #endregion

            #region 报告相关序列号
            /// <summary>
            /// 空序列号默认值，解决序列号不能为空的问题
            /// </summary>
            public static string NullSerialNumber = "libertine";
            #endregion
        }
        /// <summary>
        /// 字典项是否自动加载
        /// </summary>
        public struct IsAutoLoadState
        {
            public static string IS_Auto = "1";
            public static string NO_Auto = "0";
        }
        /// <summary>
        /// 日志类型
        /// </summary>
        public struct LogType
        {
            #region 系统相关
            //登陆操作类
            public static string LogIn = "登入";
            public static string LogOut = "登出";

            //区域操作类
            public static string AddArea = "新增区域信息";
            public static string DelArea = "删除区域信息";
            public static string EditArea = "编辑区域信息";

            //系统变量配置操作类
            public static string AddConfig = "新增系统变量配置";
            public static string DelConfig = "删除系统变量配置";
            public static string EditConfig = "编辑系统变量配置";

            //字典操作类
            public static string AddDict = "新增字典信息";
            public static string DelDict = "删除字典信息";
            public static string EditDict = "编辑字典信息";

            //序列号操作类Serial
            public static string AddSerial = "新增序列号信息";
            public static string DelSerial = "删除序列号信息";
            public static string EditSerial = "编辑序列号信息";

            //动态属性操作
            public static string AddDynaAttr = "新增动态属性信息";
            public static string DelDynaAttr = "删除动态属性信息";
            public static string EditDynaAttr = "编辑动态属性信息";

            //录音文件操作
            public static string DelOverproof = "删除录音文件信息";

            //编辑用户类
            public static string AddUser = "新增用户信息";
            public static string DelUser = "删除用户信息";
            public static string EditUser = "编辑用户信息";


            //附件信息
            public static string AddAtt = "新增附件信息";
            public static string EditAtt = "编辑附件信息";
            public static string DelAtt = "删除附件信息";
            //修改用户权限类UserRoleAuthorize
            public static string EditUserRole = "编辑用户权限信息";

            //编辑维护工程师类
            public static string AddEngineer = "新增维护工程师信息";
            public static string DelEngineer = "删除维护工程师信息";
            public static string EditEngineer = "编辑维护工程师信息";

            //操作角色组类
            public static string AddUserGroup = "新增操作角色信息";
            public static string EditUserGroup = "编辑操作角色信息";
            public static string DelUserGroup = "删除操作角色信息";
            public static string CopyGroupTree = "复制用户组信息";//复制用户组

            //操作树菜单类
            public static string AddTreeMenu = "新增树形菜单信息";
            public static string EditTreeMenu = "编辑树形菜单信息";
            public static string DelTreeMenu = "删除树形菜单信息";

            //修改密码类
            public static string EditPassWord = "编辑密码信息";

            //报表管理ReportManage
            public static string AddReportManage = "新增报表管理信息";
            public static string EditReportManage = "编辑报表管理信息";
            public static string DelReportManage = "删除报表管理信息";

            //报表模板管理TemplateManage
            public static string AddTemplateManage = "新增报表模板信息";
            public static string EditTemplateManage = "编辑报表模板信息";
            public static string DelTemplateManage = "删除报表模板信息";

            //报表配置ReportTemplateManage
            public static string AddReportTemplate = "新增报表配置信息";
            public static string EditReportTemplate = "编辑报表配置信息";
            public static string DelReportTemplate = "删除报表配置信息";

            //页面浏览
            public static string ViewPage = "查看页面信息";

            //职位操作相关
            public static string AddSysPost = "新增职位信息";
            public static string EditSysPost = "编辑职务位信息";
            public static string DelSysPost = "删除职位信息";
            public static string EditUserPost = "编辑用户职位信息";

            //权限相关
            public static string EditUserRight = "编辑用户权限信息";
            #endregion

            #region 基础资料
            //区域操作类
            public static string AddMonitorType = "新增监测类别信息";
            public static string DelMonitorType = "编辑监测类别信息";
            public static string EditMonitorType = "删除监测类别信息";

            //仪器资料
            public static string AddApparatusInfo = "新增仪器资料信息";
            public static string EditApparatusInfo = "编辑仪器资料信息";
            public static string DelApparatusInfo = "删除仪器资料信息";

            //评价标准
            public static string AddEvaluationInfo = "新增评价标准信息";
            public static string EditEvaluationInfo = "编辑评价标准信息";
            public static string DelEvaluationInfo = "删除评价标准信息";


            //条件项目
            public static string AddEvaluationConInfo = "新增评价标准条件项信息";
            public static string EditEvaluationConInfo = "编辑评价标准条件项信息";
            public static string DelEvaluationConInfo = "删除评价标准条件项信息";

            //条件项目
            public static string AddEvaluationConItemInfo = "新增条件项项目信息";
            public static string EditEvaluationConItemInfo = "编辑条件项项目信息";
            public static string DelEvaluationConItemInfo = "删除条件项项目信息";

            //岗位职责
            public static string AddDutyInfo = "新增岗位职责信息";
            public static string EditDutyInfo = "新增岗位职责信息";
            public static string DelDutyInfo = "新增岗位职责信息";

            //用户上岗资质
            public static string AddUserDutyInfo = "新增上岗资质信息";
            public static string EditUserDutyInfo = "编辑上岗资质信息";
            public static string DelUserDutyInfo = "删除上岗资质信息";
            //企业信息
            public static string AddCompanyInfo = "新增企业信息";
            public static string EditCompanyInfo = "编辑企业信息";
            public static string DelCompanyInfo = "删除企业信息";

            //企业信息
            public static string AddCompanyPointInfo = "新增企业点位信息";
            public static string EditCompanyPointInfo = "编辑企业点位信息";
            public static string DelCompanyPointInfo = "删除企业点位信息";
            #endregion

            #region 业务信息 Create By Castle(胡方扬) 2012-12-25
            /// 委托书信息
            public static string AddContractInfo = "新增委托书信息";
            public static string EditContractInfo = "编辑委托书信息";
            public static string DelContractInfo = "删除委托书信息";

            /// 委托书企业信息
            public static string AddContractCompanyInfo = "新增委托书企业信息";
            public static string EditContractCompanyInfo = "编辑委托书企业信息";
            public static string DelContractCompanyInfo = "删除委托书企业信息";

            /// 委托书点位信息
            public static string AddContractPointInfo = "新增委托书点位信息";
            public static string EditContractPointInfo = "编辑委托书点位信息";
            public static string DelContractPointInfo = "删除委托书点位信息";
            //自送样委托书样品信息
            public static string AddContractSampleInfo = "新增委托书样品信息";
            public static string EditContractSampleInfo = "编辑委托书样品信息";
            public static string DelContractSampleInfo = "删除委托书样品信息";
            //自送扬委托书样品监测项目信息
            public static string AddContractSampleItemsInfo = "新增委托书样品监测项目信息";
            public static string EditContractSampleItemsInfo = "编辑委托书样品监测项目信息";
            public static string DelContractSampleItemsInfo = "删除委托书样品监测项目信息";
            /// 委托书监测项目信息
            public static string AddContractPointItemsInfo = "新增点位监测项目信息";
            public static string EditContractPointItemsInfo = "编辑点位监测项目信息";
            public static string DelContractPointItemsInfo = "删除点位监测项目信息";
            //委托书点位附加项目信息
            public static string AddContractAttItemsInfo = "新增点位附加项目信息";
            public static string EditContractAttItemsInfo = "编辑点位附加项目信息";
            public static string DelContractAttItemsInfo = "删除点位附加项目信息";
            //采样预约计划ID
            public static string AddContractPlanInfo = "新增预约计划信息";
            public static string EditContractPlanInfo = "编辑预约计划信息";
            public static string DelContractPlanInfo = "删除预约计划信息";
            //委托书点位频次
            public static string AddContractPlanFreqInfo = "新增委托书点位频次信息";
            public static string EditContractPlanFreqInfo = "编辑委托书点位频次信息";
            public static string DelContractPlanFreqInfo = "删除委托书点位频次信息";
            //采样任务预约点位
            public static string AddContractPlanPointInfo = "新增采样任务预约点位信息";
            public static string EditContractPlanPointInfo = "编辑采样任务预约点位信息";
            public static string DelContractPlanPointInfo = "删除采样任务预约点位信息";

            //采样任务负责人
            public static string AddContractPlanUserDutyInfo = "新增采样任务负责人信息";
            public static string EditContractPlanUserDutyInfo = "编辑采样任务负责人信息";
            public static string DelContractPlanUserDutyInfo = "删除采样任务负责人信息";

            #region 站务管理
            //用户人事档案
            public static string AddEmployeInfo = "新增员工档案信息";
            public static string EditEmployeInfo = "编辑员工档案信息";
            public static string DelEmployeInfo = "删除员工档案信息";
            //用户档案信息--证书信息
            public static string AddEmployeQual = "新增证书信息";
            public static string EditEmployeQual = "编辑证书信息";
            public static string DelEmployeQual = "删除证书信息";
            //用户档案信息--工作经历
            public static string AddEmployeWorkHistory= "新增工作经历信息";
            public static string EditEmployeWorkHistory = "编辑工作经历信息";
            public static string DelEmployeWorkHistory = "删除工作经历信息";

            //用户档案信息--工作成果
            public static string AddEmployeWorkResult = "新增工作经历信息";
            public static string EditEmployeWorkResult = "编辑工作经历信息";
            public static string DelEmployeWorkResult = "删除工作经历信息";
            //考核历史信息
            public static string AddEmployeExam = "新增考核历史信息";
            public static string EditEmployeExam = "编辑考核历史信息";
            public static string DelEmployeExam = "删除考核历史信息";
            //培训经历信息
            public static string AddEmployeTrain = "新增培训经历信息";
            public static string EditEmployeTrain = "编辑培训经历信息";
            public static string DelEmployeTrain = "删除培训经历信息";
            //人事考核
            public static string AddExamInfor = "新增人事考核信息";
            public static string EditExamInfor = "编辑人事考核信息";
            public static string DelExamInfor = "删除人事考核信息";

            //员工培训
            public static string AddTrainPlan = "新增培训计划信息";
            public static string EditTrainPlan = "编辑培训计划信息";
            public static string DelTrainPlan = "删除培训计划信息";

            //员工培训附件信息
            public static string AddTrainPlanFile = "新增培训计划附件信息";
            public static string EditTrainPlanFile = "编辑培训计划附件信息";
            public static string DelTrainPlanFile = "删除培训计划附件信息";
            #endregion
            #endregion

            #region 工作流信息

            public static string WFAddSettingInfo = "新增工作流设置信息";
            public static string WFEidtSettingInfo = "编辑工作流设置信息";
            public static string WFCreateInstStepInfo = "操作工作流实例信息";
            

            #endregion
        }

        #region 报告生成相关参数
        /// <summary>
        /// 命令类型
        /// </summary>
        public struct CommandType
        {
            /// <summary>
            /// 加载监测项目
            /// </summary>
            public static string LOAD_TEST_ITEM = "0";
            /// <summary>
            /// 加载监测结果
            /// </summary>
            public static string LOAD_TEST_RESULT = "1";
        }
        public struct TestRuesultType
        {
            /// <summary>
            /// 水
            /// </summary>
            public static string Water = "1";
            /// <summary>
            /// 气
            /// </summary>
            public static string GAS = "2";
            /// <summary>
            /// 声
            /// </summary>
            public static string SOUND = "3";
            /// <summary>
            /// 固
            /// </summary>
            public static string SOLID = "4";
            /// <summary>
            /// 机
            /// </summary>
            public static string MOTOR = "5";
            /// <summary>
            /// 射
            /// </summary>
            public static string EMISSIVE = "6";
            /// <summary>
            /// 室
            /// </summary>
            public static string ROOM = "7";
            /// <summary>
            /// 综
            /// </summary>
            public static string INTEGRATION = "8";
        }
        public struct ReportTableColumnCount
        {
            /// <summary>
            /// 【监测结果-废气-中型】
            /// </summary>
            public static int RESULT_GAS_MEDIUM = 3;
            /// <summary>
            /// 【监测结果-废水-换行】
            /// </summary>
            public static int RESULT_WATER_WIDTH = 7;
        }
        #endregion
    }
}
