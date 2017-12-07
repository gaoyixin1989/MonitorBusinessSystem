using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.MonitorType;

using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
using System.Web.UI.HtmlControls;
using System.Web.Services;
/// <summary>
/// 功能描述：评价标准条件项查看
/// 创建日期：2012-12-25
/// 创建人  ：邵世卓
public partial class Channels_Base_Search_EvaluationTapView : PageBase
{
    protected string MenuNodesJson;
    //private string nodes, varNodes;
    public List<string> treenodes = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strReturn = "";
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getStandardInfo")
        {
            strReturn = getStandardInfo(Request.QueryString["standartId"]);
            Response.Write(strReturn);
            Response.End();
        }

        if (!IsPostBack)
        {
        }
    }
    /// <summary>
    /// 获得评价标准信息
    /// </summary>
    /// <param name="strStandardID">评价标准ID</param>
    /// <returns></returns>
    protected string getStandardInfo(string strStandardID)
    {
        //读取评价标准详细
        TBaseEvaluationInfoVo objEval = new TBaseEvaluationInfoLogic().Details(strStandardID);
        //定义标准类别名称
        objEval.REMARK1 = new TSysDictLogic().GetDictNameByDictCodeAndType(objEval.STANDARD_TYPE, "STANDARD_TYPE");
        //定义监测类别名称
        objEval.REMARK2 = new TBaseMonitorTypeInfoLogic().Details(objEval.MONITOR_ID).MONITOR_TYPE_NAME;
        return ToJson(objEval).Replace("\\r\\n", "");
    }
}