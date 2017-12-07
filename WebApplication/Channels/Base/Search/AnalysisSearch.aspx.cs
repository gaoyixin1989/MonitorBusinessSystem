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

/// <summary>
/// 功能描述：评价标准查询
/// 创建时间：2012-11-29
/// 创建 人：邵世卓
/// </summary>
public partial class Channels_Base_Search_AnalysisSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strResult = string.Empty;
            if (!string.IsNullOrEmpty(Request.Params["action"]) && Request.Params["action"].ToString() == "GetEvaluData")
            {
                Response.Write(GetEvaluData());
                Response.End();
            }
            //标准类型
            if (Request["type"] != null && Request["type"].ToString() == "getStandardType")
            {
                strResult = getStandardInfo();
                Response.Write(strResult);
                Response.End();
            }
            //监测类型
            if (Request["type"] != null && Request["type"].ToString() == "getMonitorType")
            {
                strResult = getMonitorType();
                Response.Write(strResult);
                Response.End();
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
        //页数
        int pageIndex = Int32.Parse(Request.Params["page"].ToString());
        //分页数
        int pageSize = Int32.Parse(Request.Params["pagesize"].ToString());
        DataTable dtEval = new DataTable();
        //评价标准对象
        TBaseEvaluationInfoVo objEval = new TBaseEvaluationInfoVo();
        //获取评价标准数据，无限定条件
        //dtEval = new TBaseEvaluationInfoLogic().SelectByTable(objEval);
        //创建标准JSON数据
        objEval.SORT_FIELD = Request.Params["sortname"];
        objEval.SORT_TYPE = Request.Params["sortorder"];
        objEval.IS_DEL = "0";
        //查询使用
        objEval.STANDARD_CODE = !String.IsNullOrEmpty(Request.Params["srhStandard_Code"]) ? Request.Params["srhStandard_Code"].ToString() : "";
        objEval.STANDARD_NAME = !String.IsNullOrEmpty(Request.Params["srhStandard_Name"]) ? Request.Params["srhStandard_Name"].ToString() : "";
        objEval.STANDARD_TYPE = !String.IsNullOrEmpty(Request.Params["srhStandard_Type"]) ? Request.Params["srhStandard_Type"].ToString() : "";
        objEval.MONITOR_ID = !String.IsNullOrEmpty(Request.Params["srhMonitor_Id"]) ? Request.Params["srhMonitor_Id"].ToString() : "";
        intTotalCount = new TBaseEvaluationInfoLogic().GetSelecDefinedtResultCount(objEval);
        dtEval = new TBaseEvaluationInfoLogic().SelectDefinedTadble(objEval, pageIndex, pageSize);

        result = LigerGridDataToJson(dtEval, intTotalCount);
        return result;
    }

    /// <summary>
    /// 获得监测类别
    /// </summary>
    /// <returns></returns>
    public string getMonitorType()
    {
        TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.IS_DEL = "0";
        DataTable dt = new TBaseMonitorTypeInfoLogic().SelectByTable(TBaseMonitorTypeInfoVo);
        return DataTableToJson(dt);
    }

    /// <summary>
    /// 获得标准类型
    /// </summary>
    /// <returns></returns>
    public string getStandardInfo()
    {
        TSysDictVo objtd = new TSysDictVo();
        objtd.DICT_TYPE = "STANDARD_TYPE";
        DataTable dt = new TSysDictLogic().SelectByTable(objtd);
        return DataTableToJson(dt);
    }

    /// <summary>
    /// 获取监测值类型下拉列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetMonitor(string strValue)
    {
        TBaseMonitorTypeInfoVo objtd = new TBaseMonitorTypeInfoVo();
        objtd.IS_DEL = "0";
        objtd.ID = strValue;
        objtd = new TBaseMonitorTypeInfoLogic().Details(objtd);
        return objtd.MONITOR_TYPE_NAME;
    }

    /// <summary>
    /// 获取标准类型数据下拉列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetStandard(string strValue)
    {
        TSysDictVo objtd = new TSysDictVo();
        objtd.DICT_TYPE = "STANDARD_TYPE";
        objtd.DICT_CODE = strValue;
        objtd = new TSysDictLogic().Details(objtd);
        return objtd.DICT_TEXT;
    }
}