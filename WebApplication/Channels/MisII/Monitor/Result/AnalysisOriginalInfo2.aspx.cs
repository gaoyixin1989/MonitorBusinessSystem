using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.DataAccess;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
/// <summary>
/// 功能描述：数据原始记录信息
/// 创建日期：2014-01-21
/// 创建人  ：魏林
/// </summary>
public partial class Channels_MisII_Monitor_Result_AnalysisOriginalInfo2 : PageBase
{
    public string ccflowWorkId = "";
    public string ccflowFid = "";
    public string ccflowUserNo = "";
    public string ccflowItemList = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ccflowWorkId = Request.QueryString["ccflowWorkId"];
            ccflowFid = Request.QueryString["ccflowFid"];

            // yinchengyi 2015-4-25 Excel表单显示
            ccflowUserNo = Request.QueryString["ccflowUserNo"];
            ccflowItemList = GetItemList(ccflowWorkId);
        }
    }

    // yinchengyi 2015-4-25 excel表单显示 通过ccflow流程ID获取Excel表单名称列表
    private string GetItemList(string ccflowWorkId)
    {
        string strItemList = "";

        string strSQL = @"select *
                             from T_MIS_MONITOR_RESULT
                             where CCFLOW_ID1 = '" + ccflowWorkId + "'";

        DataTable dt = SqlHelper.ExecuteDataTable(strSQL);

        if (dt.Rows.Count == 0)
        {
            strItemList = "unknown item";
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string strExcelName = GetExcelName(Convert.ToString(dt.Rows[i]["ITEM_ID"]));

            if (strExcelName == "unsupport")
            {
                strItemList = strExcelName;

                return strItemList;
            }

            if (i == dt.Rows.Count - 1)
            {
                strItemList = strExcelName;
            }
            else
            {
                strItemList = strExcelName + ",";
            }
        }

        return strItemList;
    }

    //yinchengyi 2015-4-25 excel表单显示 通过ccflow流程ID获取Excel表单名称列表
    //todo-yinchengyi 根据监测项目信息找到对应的ccflow表单
    private string GetExcelName(string strItemID)
    {
        string strExcelName = "";

        switch (strItemID)
        {
            case "000002025":
                strExcelName = "LIMIS_KMnO4";   //高锰酸钾
                break;
            case "000002024":
                strExcelName = "LIMIS_COD"; //化学需氧量
                break;
            case "000002026":
                strExcelName = "LIMIS_BOD5";    //五日生化需氧量
                break;
            case "000002225":
                strExcelName = "LIMIS_CL2"; //活性氯
                break;
            case "000002015":
                strExcelName = "LIMIS_TP";  //总磷
                break;
            //case "1":暂时只提供一种方法 见LIMIS_NO3
            //    strExcelName = "LIMIS_NH3_N";   //氨氮
            //    break;
            //case "1": 暂时只提供一种方法 见NH3_NF2
            //    strExcelName = "LIMIS_V_P"; //挥发酚
            //    break;
            case "000002032":
                strExcelName = "LIMIS_RCOOM";   //阴离子表面活剂
                break;
            case "000002022":
            case "000002123":
                strExcelName = "LIMIS_S";   //硫化物
                break;
            case "000002051":
            case "000002080":
                strExcelName = "LIMIS_HCHO";    //甲醛
                break;
            case "000002219":
                strExcelName = "LIMIS_SHJ";//水合肼
                break;
            case "000002224":
                strExcelName = "LIMIS_DHJYS";//丁基黄原酸
                break;
            case "000002027":
                strExcelName = "NH3_NF2";//挥发酚
                break;
            case "000002011":
                strExcelName = "LIMIS_NO3";//氨氮
                break;
            case "000002014":
                strExcelName = "LIMIS_TN";//总氮
                break;
            default:
                strExcelName = "unsupport";
                break;
        }

        return strExcelName;
    }

}