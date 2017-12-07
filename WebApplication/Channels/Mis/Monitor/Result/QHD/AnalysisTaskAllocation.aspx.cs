using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;


using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
namespace n14
{
    /// <summary>
    /// 功能描述：分析任务分配
    /// 创建日期：2012-11-29
    /// 创建人  ：熊卫华
    /// </summary>
    public partial class Channels_Mis_Monitor_Result_QHD_AnalysisTaskAllocation : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //定义结果
                string strResult = "";
                //任务信息
                if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
                {
                    strResult = getOneGridInfo(Request["Sample"].ToString());
                    Response.Write(strResult);
                    Response.End();
                }
                //监测项目类别
                if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
                {
                    strResult = getTwoGridInfo(Request["oneGridId"].ToString());
                    Response.Write(strResult);
                    Response.End();
                }
                //样品号信息
                if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
                {
                    strResult = getThreeGridInfo(Request["twoGridId"].ToString(), Request["strMonitorID"].ToString(), Request["Sample"].ToString());
                    Response.Write(strResult);
                    Response.End();
                }
                //监测项目信息
                if (Request["type"] != null && Request["type"].ToString() == "getFourGridInfo")
                {
                    strResult = getFourGridInfo(Request["threeGridId"].ToString(), Request["strMonitorID"].ToString(), Request["Sample"].ToString());
                    Response.Write(strResult);
                    Response.End();
                }
                //判断是否能退回
                if (Request["type"] != null && Request["type"].ToString() == "IsCanGoToBack")
                {
                    strResult = IsCanGoToBack(Request["strTaskId"].ToString());
                    Response.Write(strResult);
                    Response.End();
                }
                //退回
                if (Request["type"] != null && Request["type"].ToString() == "GoToBack")
                {
                    strResult = GoToBack(Request["strTaskId"].ToString());
                    Response.Write(strResult);
                    Response.End();
                }
                //发送
                if (Request["type"] != null && Request["type"].ToString() == "SendToNext")
                {
                    strResult = SendToNext(Request["strTaskId"].ToString(), Request["Sample"].ToString());
                    Response.Write(strResult);
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 获取监测点信息
        /// </summary>
        /// <returns></returns>
        private string getOneGridInfo(string iSample)
        {
            string strSortname = Request.Params["sortname"];
            string strSortorder = Request.Params["sortorder"];
            //当前页面
            int intPageIndex = Convert.ToInt32(Request.Params["page"]);
            //每页记录数
            int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

            DataTable dt = new TMisMonitorResultLogic().getTaskInfo_QHD("03,021,02", "01", "2", iSample, intPageIndex, intPageSize);
            int intTotalCount = new TMisMonitorResultLogic().getTaskInfoCount_QHD("03,021,02", "01", "2", iSample);
            string strJson = CreateToJson(dt, intTotalCount);
            return strJson;
        }

        /// <summary>
        /// 获取监测项目类别信息
        /// </summary>
        /// <returns></returns>
        private string getTwoGridInfo(string strOneGridId)
        {
            DataTable dt = new TMisMonitorResultLogic().getItemTypeInfo_QHD(strOneGridId, "03,021,02", "01", "2", false);
            string strJson = CreateToJson(dt, 0);
            return strJson;
        }

        /// <summary>
        /// 获取样品信息
        /// </summary>
        /// <returns></returns>
        private string getThreeGridInfo(string strTwoGridId, string strMonitorID, string iSample)
        {
            DataTable dt = new TMisMonitorResultLogic().getSimpleCodeInAlloction_QHD(strTwoGridId, LogInfo.UserInfo.ID, "01", "2", iSample, strMonitorID);
            string strJson = CreateToJson(dt, 0);
            return strJson;
        }
        /// <summary>
        /// 获取监测项目信息
        /// </summary>
        /// <returns></returns>
        private string getFourGridInfo(string strThreeGridId, string strMonitorID, string iSample)
        {
            string strSortname = Request.Params["sortname"];
            string strSortorder = Request.Params["sortorder"];
            //当前页面
            int intPageIndex = Convert.ToInt32(Request.Params["page"]);
            //每页记录数
            int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

            DataTable dt = new TMisMonitorResultLogic().getItemInfoInAlloction_QHD(strThreeGridId, "01", "2", iSample, strMonitorID, intPageIndex, intPageSize);
            int intTotalCount = new TMisMonitorResultLogic().getItemInfoCountInAlloction_QHD(strThreeGridId, "01", "2", iSample, strMonitorID);
            string strJson = CreateToJson(dt, intTotalCount);
            return strJson;
        }
        /// <summary>
        /// 退回之前判断任务是否可以回退
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <returns></returns>
        public string IsCanGoToBack(string strTaskId)
        {
            bool IsCanGoToBack = new TMisMonitorResultLogic().IsCanGoToBack(strTaskId, LogInfo.UserInfo.ID, "duty_other_analyse", "01");
            return IsCanGoToBack == true ? "1" : "0";
        }
        /// <summary>
        /// 退回到上一环节
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <returns></returns>
        public string GoToBack(string strTaskId)
        {
            bool IsSuccess = true;
            IsSuccess = new TMisMonitorResultLogic().SampleGoToBack_QHD(strTaskId, "2", "1", "01");
            IsSuccess = new TMisMonitorResultLogic().subTaskGoToBack_QHD(strTaskId, "03", "021", "2", "01");
            return IsSuccess == true ? "1" : "0";
        }
        /// <summary>
        /// 发送到下一环节
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <returns></returns>
        public string SendToNext(string strTaskId, string iSample)
        {
            bool IsSuccess = new TMisMonitorResultLogic().sendToNextFlow_QHD(strTaskId, LogInfo.UserInfo.ID, "03,021,02", "2", "01", "20", iSample);
            return IsSuccess == true ? "1" : "0";
        }
        /// <summary>
        /// 获取企业名称信息
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strCompanyId">企业ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string getCompanyName(string strTaskId, string strCompanyId)
        {
            if (strCompanyId == "") return "";
            i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo TMisMonitorTaskCompanyVo = new i3.ValueObject.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyVo();
            TMisMonitorTaskCompanyVo.ID = strCompanyId;
            string strCompanyName = new i3.BusinessLogic.Channels.Mis.Monitor.Task.TMisMonitorTaskCompanyLogic().Details(TMisMonitorTaskCompanyVo).COMPANY_NAME;
            return strCompanyName;
        }
        /// <summary>
        /// 获取监测类别名称
        /// </summary>
        /// <param name="strMonitorTypeId">监测类别Id</param>
        /// <returns></returns>
        [WebMethod]
        public static string getMonitorTypeName(string strMonitorTypeId)
        {
            i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo();
            TBaseMonitorTypeInfoVo.ID = strMonitorTypeId;
            string strMonitorTypeName = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(TBaseMonitorTypeInfoVo).MONITOR_TYPE_NAME;
            return strMonitorTypeName;
        }
        /// <summary>
        /// 获取监测项目相关信息
        /// </summary>
        /// <param name="strItemCode">监测项目ID</param>
        /// <param name="strItemName">监测项目信息名称</param>
        /// <returns></returns>
        [WebMethod]
        public static string getItemInfoName(string strItemCode, string strItemName)
        {
            string strReturnValue = "";
            i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo();
            TBaseItemInfoVo.ID = strItemCode;
            DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemInfoLogic().SelectByTable(TBaseItemInfoVo);
            if (dt.Rows.Count > 0)
                strReturnValue = dt.Rows[0][strItemName].ToString();
            return strReturnValue;
        }
        /// <summary>
        /// 获取获取分析负责人信息
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string getDefaultUserName(string strResultId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(string));
            dt.Columns.Add("UserName", typeof(string));

            TMisMonitorResultAppVo TMisMonitorResultAppVo = new TMisMonitorResultAppVo();
            TMisMonitorResultAppVo.RESULT_ID = strResultId;
            DataTable objTable = new TMisMonitorResultAppLogic().SelectByTable(TMisMonitorResultAppVo);
            if (objTable.Rows.Count == 0) return DataTableToJson(dt);

            string strUserId = objTable.Rows[0]["HEAD_USERID"].ToString();
            if (strUserId == "") return DataTableToJson(dt);

            //将获取用户ID信息转换成用户名称进行返回
            string strUserName = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserId).REAL_NAME;
            DataRow row = dt.NewRow();
            row["UserId"] = strUserId;
            row["UserName"] = strUserName;
            dt.Rows.Add(row);

            return DataTableToJson(dt);
        }
        /// <summary>
        /// 获取获取分析协同人信息
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string getDefaultUserExName(string strResultId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(string));
            dt.Columns.Add("UserName", typeof(string));

            TMisMonitorResultAppVo TMisMonitorResultAppVo = new TMisMonitorResultAppVo();
            TMisMonitorResultAppVo.RESULT_ID = strResultId;
            DataTable objTable = new TMisMonitorResultAppLogic().SelectByTable(TMisMonitorResultAppVo);
            if (objTable.Rows.Count == 0) return DataTableToJson(dt);

            string strUserExIds = objTable.Rows[0]["ASSISTANT_USERID"].ToString();
            if (strUserExIds == "") return DataTableToJson(dt);

            List<string> list = strUserExIds.Split(',').ToList();
            string strSumUserExName = "";
            string spit = "";
            foreach (string strUserExId in list)
            {
                string strUserName = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserExId).REAL_NAME;
                strSumUserExName = strSumUserExName + spit + strUserName;
                spit = ",";
            }
            DataRow row = dt.NewRow();
            row["UserId"] = strUserExIds;
            row["UserName"] = strSumUserExName;
            dt.Rows.Add(row);

            return DataTableToJson(dt);
        }
        /// <summary>
        /// 获取分析完成时间
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string getAskingDate(string strResultId)
        {
            DataTable dt = new DataTable();

            TMisMonitorResultAppVo TMisMonitorResultAppVo = new TMisMonitorResultAppVo();
            TMisMonitorResultAppVo.RESULT_ID = strResultId;
            DataTable objTable = new TMisMonitorResultAppLogic().SelectByTable(TMisMonitorResultAppVo);
            if (objTable.Rows.Count == 0) return DataTableToJson(dt);

            string strAskingDate = objTable.Rows[0]["ASKING_DATE"].ToString();
            if (strAskingDate != "")
                strAskingDate = DateTime.Parse(strAskingDate).ToString("yyyy-MM-dd");
            return strAskingDate;
        }
        /// <summary>
        /// 根据默认负责人获取已经分配的项目信息
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strDefaultUser">默认负责人ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string getAssignedDefaultItemName(string strTaskId, string strDefaultUser)
        {
            DataTable objTable = new TMisMonitorResultLogic().getAssignedDefaultItem(strTaskId, strDefaultUser);
            if (objTable.Rows.Count == 0) return "";
            string strSumItemName = "";
            string spit = "";
            foreach (DataRow row in objTable.Rows)
            {
                strSumItemName = strSumItemName + spit + row["ITEM_NAME"].ToString();
                spit = ",";
            }
            return strSumItemName;
        }
        /// <summary>
        /// 根据默认协同人获取已经分配的项目信息
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strDefaultUser">默认负责人ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string getAssignedDefaultItemNameEx(string strTaskId, string strDefaultUser)
        {
            DataTable objTable = new TMisMonitorResultLogic().getAssignedDefaultItemEx(strTaskId, strDefaultUser);
            if (objTable.Rows.Count == 0) return "";
            string strSumItemName = "";
            string spit = "";
            foreach (DataRow row in objTable.Rows)
            {
                strSumItemName = strSumItemName + spit + row["ITEM_NAME"].ToString();
                spit = ",";
            }
            return strSumItemName;
        }
        /// <summary>
        /// 根据默认协同人获取已经分配的样品号
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strDefaultUser">默认负责人ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string getAssignedSampleCode(string strTaskId, string strDefaultUser)
        {
            DataTable objTable = new TMisMonitorResultLogic().getAssignedSampleCode(strTaskId, strDefaultUser);
            if (objTable.Rows.Count == 0) return "";
            string strSumSampleCode = "";
            string spit = "";
            foreach (DataRow row in objTable.Rows)
            {
                strSumSampleCode = strSumSampleCode + spit + row["SAMPLE_CODE"].ToString();
                spit = ",";
            }
            return strSumSampleCode;
        }
        /// <summary>
        /// 获取字典项名称
        /// </summary>
        /// <param name="strDictCode">字典项代码</param>
        /// <param name="strDictType">字典项类型</param>
        /// <returns></returns>
        [WebMethod]
        public static string getDictName(string strDictCode, string strDictType)
        {
            return PageBase.getDictName(strDictCode, strDictType);
        }
    }
}