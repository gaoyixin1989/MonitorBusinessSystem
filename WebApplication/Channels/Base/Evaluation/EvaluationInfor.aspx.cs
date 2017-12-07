using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using i3.View;
using System.Data;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// Create By Castle(胡方扬) 2012-11-01
/// 功能：评价标准信息列表
/// </summary>

public partial class Channels_Base_Evaluation_EvaluationInfor : PageBase
{
    public string strSortname="", strSortorder="";
    public int intPageIndex = 0, intPageSize = 0;
    public string srhStandard_Code = "", srhStandard_Name = "", srhStandard_Type = "", srhMonitor_Id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strSortname = Request.Params["sortname"];
        strSortorder = Request.Params["sortorder"];
        //当前页面
        intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        if (!String.IsNullOrEmpty(Request.Params["srhStandard_Code"]))
        {
            srhStandard_Code = Request.Params["srhStandard_Code"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["srhStandard_Name"]))
        {
            srhStandard_Name = Request.Params["srhStandard_Name"].Trim();
        }
        if (!String.IsNullOrEmpty(Request.Params["srhStandard_Type"]))
        {
            srhStandard_Type = Request.Params["srhStandard_Type"];
        }
        if (!String.IsNullOrEmpty(Request.Params["srhMonitor_Id"]))
        {
            srhMonitor_Id = Request.Params["srhMonitor_Id"];
        }
        string Action = Request["action"];
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Action))
            {
                switch (Action)
                {
                    case "GetEvaluData":
                        Response.Write(GetEvaluData());
                        Response.End();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    /// <summary>
    /// 获取评价标准列表数据
    /// </summary>
    /// <returns></returns>
    private string GetEvaluData()
    {
        string result = "";
        int intTotalCount = 0;
        DataTable dtEval = new DataTable();
        //评价标准对象
        TBaseEvaluationInfoVo objEval = new TBaseEvaluationInfoVo();
        //获取评价标准数据，无限定条件
        //dtEval = new TBaseEvaluationInfoLogic().SelectByTable(objEval);
        //创建标准JSON数据
        objEval.SORT_FIELD = strSortname;
        objEval.SORT_TYPE = strSortorder;
        objEval.IS_DEL = "0";
        //自定义查询使用
        if (!String.IsNullOrEmpty(srhStandard_Code) || !String.IsNullOrEmpty(srhStandard_Name) || !String.IsNullOrEmpty(srhStandard_Type) || !String.IsNullOrEmpty(srhMonitor_Id))
        {
            objEval.STANDARD_CODE = srhStandard_Code;
            objEval.STANDARD_NAME = srhStandard_Name;
            objEval.STANDARD_TYPE = srhStandard_Type;
            objEval.MONITOR_ID = srhMonitor_Id;
            intTotalCount = new TBaseEvaluationInfoLogic().GetSelecDefinedtResultCount(objEval);
            dtEval = new TBaseEvaluationInfoLogic().SelectDefinedTadble(objEval, intPageIndex, intPageSize);
        }
        //无条件首次加载用
        else
        {
            dtEval = new TBaseEvaluationInfoLogic().SelectByTable(objEval, intPageIndex, intPageSize);
            intTotalCount = new TBaseEvaluationInfoLogic().GetSelectResultCount(objEval);
        }
        result = LigerGridDataToJson(dtEval, intTotalCount);
        return result;
    }
    /// <summary>
    /// 删除选中的数据
    /// </summary>
    /// <param name="strEvaluId">前台传入的要删除评价标准的ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string DelEvaluData(string strID)
    {
        string result = "false";
        string[] EvaluId = strID.Split(',');
        bool success = false;
        int successCount=0;
        int DelCount=EvaluId.Length;
        foreach (string strid in EvaluId)
        {
            TBaseEvaluationInfoVo objeil = new TBaseEvaluationInfoVo();
            objeil.ID = strid;
            objeil.IS_DEL = "1";
            success=  new TBaseEvaluationInfoLogic().Edit(objeil);
            if(success==true)
            {
                successCount++;
            }
        }

        if (success==true && (successCount == DelCount))
        {
            result = "true";

            string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "删除评价标准成功";
            new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.AddEvaluationInfo, "", strMessage); 
        }
        return result;
    }
    /// <summary>
    /// 编辑评价标准
    /// </summary>
    /// <param name="strID">ID</param>
    /// <param name="strSTANDARD_CODE">评价标准编码</param>
    /// <param name="strSTANDARD_NAME">评价标准名称</param>
    /// <param name="strstrSTANDARD_TYPE">评价标准类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string EditEvaluData(string strID, string strSTANDARD_CODE, string strSTANDARD_NAME, string strSTANDARD_TYPE, string strMONITOR_ID)
    {
        string result = "";
        bool success = false;
        TBaseEvaluationInfoVo objtev = new TBaseEvaluationInfoVo();
        objtev.ID = strID;
        objtev.STANDARD_CODE = strSTANDARD_CODE;
        objtev.STANDARD_NAME = strSTANDARD_NAME;
        objtev.STANDARD_TYPE = strSTANDARD_TYPE;
        objtev.MONITOR_ID = strMONITOR_ID;
        success = new TBaseEvaluationInfoLogic().Edit(objtev);

        if (success)
        {
            result = "true";
            string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "编辑评价标准" + objtev.ID + "成功";
            new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.EditEvaluationInfo, "", strMessage); 
        }
        return result;
    }

    /// <summary>
    /// 新增评价标准
    /// </summary>
    /// <param name="strID">ID</param>
    /// <param name="strSTANDARD_CODE">评价标准编码</param>
    /// <param name="strSTANDARD_NAME">评价标准名称</param>
    /// <param name="strstrSTANDARD_TYPE">评价标准类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string AddEvaluData(string strSTANDARD_CODE, string strSTANDARD_NAME, string strSTANDARD_TYPE, string strMONITOR_ID)
    {
        string result = "";
        bool success = false;
        TBaseEvaluationInfoVo objtev = new TBaseEvaluationInfoVo();
        objtev.ID = GetSerialNumber("t_base_evaluation_type_info_id");
        objtev.STANDARD_CODE = strSTANDARD_CODE;
        objtev.STANDARD_NAME = strSTANDARD_NAME;
        objtev.STANDARD_TYPE = strSTANDARD_TYPE;
        objtev.MONITOR_ID = strMONITOR_ID;
        objtev.EFFECTIVE_DATE = DateTime.Now.ToString();
        objtev.IS_DEL = "0";
        success = new TBaseEvaluationInfoLogic().Create(objtev);

        if (success)
        {
            result = "true";
            string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "新增评价标准" + objtev.ID + "成功";
            new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.DelEvaluationInfo, "", strMessage); 
        }
        return result;
    }

    /// <summary>
    /// 获取监测值类型下拉列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetMonitor()
    {
        List<object> reslut = new List<object>();
        DataTable dtSt = new DataTable();
        TBaseMonitorTypeInfoVo objtd = new TBaseMonitorTypeInfoVo();
        objtd.IS_DEL = "0";
        dtSt = new TBaseMonitorTypeInfoLogic().SelectByTable(objtd);
        reslut = LigerGridSelectDataToJson(dtSt, dtSt.Rows.Count);
        //reslut = gridDataToJson(dtSt, dtSt.Rows.Count, objtd);
        return reslut;
    }

    /// <summary>
    /// 获取标准类型数据下拉列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetStandard()
    {
        List<object> reslut = new List<object>();
        DataTable dtSt = new DataTable();
        TSysDictVo objtd = new TSysDictVo();
        objtd.DICT_TYPE = "STANDARD_TYPE";
        dtSt = new TSysDictLogic().SelectByTable(objtd);
        reslut = LigerGridSelectDataToJson(dtSt, dtSt.Rows.Count);
        //reslut = gridDataToJson(dtSt, dtSt.Rows.Count, objtd);
        return reslut;
    }

}