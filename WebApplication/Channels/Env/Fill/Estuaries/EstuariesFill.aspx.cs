using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Text;
using System.Data;
using i3.BusinessLogic.Channels.Env.Point.River;
using i3.ValueObject.Channels.Env.Point.River;
using i3.BusinessLogic.Channels.Env.Fill.River;
using i3.BusinessLogic.Channels.Env.Point.Estuaries;
using i3.ValueObject.Channels.Env.Point.Estuaries;
using i3.BusinessLogic.Channels.Env.Fill.Estuaries;
using System.Web.Services;
using i3.BusinessLogic.Channels.Env.Point.Common;
using i3.ValueObject.Channels.Env.Fill.Estuaries;
using NPOI.SS.UserModel;
using System.IO;

/// <summary>
/// 功能描述：入海河口数据填报
/// 创建人：钟杰华
/// 创建日期：2013-02-22
/// </summary>
public partial class Channels_Env_Fill_Estuaries_EstuariesFill : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                switch (Request["action"])
                {
                    case "GetYear":
                        GetYear();
                        break;
                    case "GetPoint":
                        GetPoint();
                        break;
                    case "GetData":
                        GetData();
                        break;
                    case "SaveData":
                        SaveData();
                        break;
                    case "GetDay":
                        GetDay();
                        break;
                    case "GetDict":
                        getDict(Request["dictType"].ToString());
                        break;
                    case "GetFillID":
                        GetFillID();
                        break;
                }
            }
        }
    }

    #region 获取年份

    private void GetYear()
    {
        string json = getYearInfo();

        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取监测点
     
    private void GetPoint()
    {
        string year = Request["year"];
        string month=Request["month"];
        TEnvPointEstuariesVo model = new TEnvPointEstuariesVo();
        model.YEAR = year;
        model.MONTH = month;
        model.IS_DEL = "0";
        DataTable dt = new TEnvPointEstuariesLogic().SelectByTable(model);

        //加入全部
        DataRow dr = dt.NewRow();
        dr["section_name"] = "--全部--";
        dt.Rows.InsertAt(dr, 0);

        string json = DataTableToJson(dt);

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取填报ID

    private void GetFillID()
    {
        string year = Request["year"];
        string month = Request["month"];

        TEnvFillEstuariesVo envFillEstuariesVo = new TEnvFillEstuariesVo();
        envFillEstuariesVo.YEAR = year;
        envFillEstuariesVo.MONTH = month;

        envFillEstuariesVo = new TEnvFillEstuariesLogic().Details(envFillEstuariesVo);

        string json = "{\"ID\":\"" + envFillEstuariesVo.ID + "\",\"STATUS\":\"" + envFillEstuariesVo.STATUS + "\"}";

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取数据

    private void GetData()
    {
        string strWhere = "1=1";
        string year = Request["year"];
        string month = Request["month"];
        string pointId = Request["pointId"];
        //DataTable dt = new TEnvFillEstuariesLogic().GetEstuariesFillData(year, month, pointId);
        //string json = DataTableToJsonUnsureCol(dt, "_unSure");
        //Response.ContentType = "application/json";
        if (year != "")
            strWhere += " and YEAR='" + year + "'";
        if (month != "")
            strWhere += " and MONTH='" + month + "'";
        if (pointId != "")
            strWhere += " and ID='" + pointId + "'";
        DataTable dtShow = new DataTable();//填报表需要显示信息
        dtShow = new TEnvFillEstuariesLogic().CreateShowDT();

        CommonLogic com = new CommonLogic(); 
        DataTable dt = com.GetFillData(strWhere,
                                       dtShow,
                                       TEnvPointEstuariesVo.T_ENV_POINT_ESTUARIES_TABLE,
                                       TEnvPointEstuariesVerticalVo.T_ENV_POINT_ESTUARIES_VERTICAL_TABLE,
                                       TEnvPointEstuariesVItemVo.T_ENV_POINT_ESTUARIES_V_ITEM_TABLE,
                                       TEnvFillEstuariesVo.T_ENV_FILL_ESTUARIES_TABLE,
                                       TEnvFillEstuariesItemVo.T_ENV_FILL_ESTUARIES_ITEM_TABLE,
                                       SerialType.T_ENV_FILL_ESTUARIES,
                                       SerialType.T_ENV_FILL_ESTUARIES_ITEM,
                                       "1");
        string json = DataTableToJsonUnsureCol(dt);
        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 保存数据

    private void SaveData()
    {
        //string data = Request["data"];
        //DataTable dtData = JSONToDataTable2(data);
        //bool result = new TEnvFillEstuariesLogic().SaveEstuariesFillData(dtData);
        bool result = false;
        string data = "[" + Request["data"] + "]";
        string ID = Request["id"];
        string updateName = Request["updateName"];
        string value = Request["value"];
        string ConditionID = "";
        CommonLogic com = new CommonLogic();
        //特殊处理采样日期
        if (updateName.Contains("DAY"))
        {
            com.UpdateTableByWhere(TEnvFillEstuariesVo.T_ENV_FILL_ESTUARIES_TABLE, "SAMPLING_DAY=YEAR+'-'+MONTH+'-'+'" + value + "'", "ID='" + ID + "'");
        }
        result = com.UpdateCommonFill(updateName, value, ID, ConditionID);
        Response.ContentType = "application/json";
        Response.Write("{\"result\":\"" + (result ? "success" : "fail") + "\"}");
        Response.End();
    }

    #endregion

    #region 获取日期

    private void GetDay()
    {
        string month = Request["month"];
        string year = Request["year"];

        string json = GetDayByMonth(year, Convert.ToInt32(month));

        Response.ContentType = "application/json;charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    #endregion

    #region 获取下拉字典项
    private void getDict(string strDictType)
    {
        string strJson = getDictJsonString(strDictType);
        Response.ContentType = "application/json";
        Response.Write(strJson);
        Response.End();
    }
    #endregion

    #region//获取字典项名称
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
    #endregion

    #region//获取断面名称
    /// <summary>
    /// 获取断面名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getSectionName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPointEstuariesVo.T_ENV_POINT_ESTUARIES_TABLE, "SECTION_NAME", "ID", strV);
    }
    #endregion

    #region//获取测点名称
    /// <summary>
    /// 获取测点名称
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getPointName(string strV)
    {
        CommonLogic com = new CommonLogic();
        return com.getNameByID(TEnvPointEstuariesVerticalVo.T_ENV_POINT_ESTUARIES_VERTICAL_TABLE, "VERTICAL_NAME", "ID", strV);
    }
    #endregion

    #region//Excel导出 (暂不用)
    //protected void btnExcelOut_Click(object sender, EventArgs e)
    //{
    //    string strXmlPath = Server.MapPath("../../xmlTemp/Export/EstuariesTemple.xml");
    //    string strExcelPath = Server.MapPath("../../excelTemp/入海河口导出.xls");
    //    ISheet sheet = new i3.BusinessLogic.Channels.Env.Import.ImportExcelLogic().GetExportExcelSheet(strXmlPath, strExcelPath, "Sheet1", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
    //    using (MemoryStream stream = new MemoryStream())
    //    { 
    //        sheet.Workbook.Write(stream);
    //        HttpContext curContext = HttpContext.Current;
    //        // 设置编码和附件格式   
    //        curContext.Response.ContentType = "application/vnd.ms-excel";
    //        curContext.Response.ContentEncoding = Encoding.UTF8;
    //        curContext.Response.Charset = "";
    //        curContext.Response.AppendHeader("Content-Disposition",
    //            "attachment;filename=" + HttpUtility.UrlEncode("入海河口.xls", Encoding.UTF8));
    //        curContext.Response.BinaryWrite(stream.GetBuffer());
    //        curContext.Response.End();
    //    }
    //}

    #endregion
}