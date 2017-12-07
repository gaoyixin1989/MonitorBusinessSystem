using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;

using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
/// <summary>
/// 功能描述：分析任务分配单查询与导出
/// 创建日期：2012-12-13
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Mis_Monitor_Result_AssignmentSheet : PageBase
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
                strResult = getOneGridInfo();
                Response.Write(strResult);
                Response.End();
            }
            //获取下拉列表信息
            if (Request["type"] != null && Request["type"].ToString() == "getDict")
            {
                strResult = getDict(Request["dictType"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取监测类别信息
            if (Request["type"] != null && Request["type"].ToString() == "getMonitorType")
            {
                strResult = getMonitorType();
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict(string strDictType)
    {
        return getDictJsonString(strDictType);
    }
    /// <summary>
    /// 获取监测类别信息
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
    /// 获取任务信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo()
    {
        string strContractType = Request["strContractType"] == null ? "" : Request["strContractType"].ToString();
        string strAnalyseAssignDate = Request["strAnalyseAssignDate"] == null ? "" : Request["strAnalyseAssignDate"].ToString();
        string strMonitorType = "";
        string spit = "";
        if (Request["strMonitorType"] != null && Request["strMonitorType"].ToString() != "")
        {
            foreach (string strMonitorTypeTemp in Request["strMonitorType"].ToString().Split(',').ToList())
            {
                strMonitorType += spit + strMonitorTypeTemp;
                spit = "','";
            }
        }
        DataTable dt = new TMisMonitorResultLogic().getAssignmentSheetInfo(strContractType, strMonitorType, strAnalyseAssignDate, "duty_other_analyse", LogInfo.UserInfo.ID, "20");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strSampleId = this.strSampleId.Value;
        //获取基本信息
        DataTable dt = new TMisMonitorResultLogic().getAssignmentSheetInfoBySample(strSampleId, "20");
        //变更打印状态
        new TMisMonitorResultLogic().updateAssignmentSheetResultStatus(strSampleId, "20", "1");
        //获取分析人员信息
        //string strUserItemInfo = new TMisMonitorResultLogic().getAssignmentSheetUserInfo(strSampleId, "20");

        FileStream file = new FileStream(HttpContext.Current.Server.MapPath("template/AssignmentSheet.xls"), FileMode.Open, FileAccess.Read);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        sheet.GetRow(2).GetCell(0).SetCellValue("分样日期：" + dt.Rows[0]["ANALYSE_ASSIGN_DATE"].ToString() == "" ? "" : DateTime.Parse(dt.Rows[0]["ANALYSE_ASSIGN_DATE"].ToString()).ToString("yyyy-MM-dd"));

        int iR = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTable dtUserItemInfo = new TMisMonitorResultLogic().getAssignmentSheetUserInfoEx(dt.Rows[i]["ID"].ToString(), "20");
            int iH = 0;
            for (int j = 0; j < dtUserItemInfo.Rows.Count; j++)
            {
                sheet.GetRow((iH + iR) + 3).GetCell(0).SetCellValue("样品编号");
                sheet.GetRow((iH + iR) + 3).GetCell(1).SetCellValue("样品类型");
                sheet.GetRow((iH + iR) + 3).GetCell(2).SetCellValue("分配日期");
                sheet.GetRow((iH + iR) + 3).GetCell(3).SetCellValue("保存方法");
                sheet.GetRow((iH + iR) + 3).GetCell(4).SetCellValue("分析项目");
                sheet.GetRow((iH + iR) + 3).GetCell(5).SetCellValue("结果要求上报时间");
                sheet.GetRow((iH + iR) + 3).GetCell(6).SetCellValue("分析人员");

                sheet.GetRow((iH + iR) + 4).GetCell(0).SetCellValue(dt.Rows[i]["SAMPLE_CODE"].ToString());
                sheet.GetRow((iH + iR) + 4).GetCell(1).SetCellValue(dt.Rows[i]["SAMPLE_TYPE"].ToString());
                sheet.GetRow((iH + iR) + 4).GetCell(2).SetCellValue(dt.Rows[i]["ANALYSE_ASSIGN_DATE"].ToString() == "" ? "" : DateTime.Parse(dt.Rows[i]["ANALYSE_ASSIGN_DATE"].ToString()).ToString("yyyy-MM-dd"));
                sheet.GetRow((iH + iR) + 4).GetCell(4).SetCellValue(dtUserItemInfo.Rows[j]["ITEM_NAME"].ToString());
                sheet.GetRow((iH + iR) + 4).GetCell(5).SetCellValue(dt.Rows[i]["FINISH_DATE"].ToString() == "" ? "" : DateTime.Parse(dt.Rows[i]["FINISH_DATE"].ToString()).ToString("yyyy-MM-dd"));
                sheet.GetRow((iH + iR) + 4).GetCell(6).SetCellValue(dtUserItemInfo.Rows[j]["REAL_NAME"].ToString());

                iH = iH + 2;
            }
            iR += iH;
            //iR += dtUserItemInfo.Rows.Count + 1;
        }


        using (MemoryStream stream = new MemoryStream())
        {
            hssfworkbook.Write(stream);
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("样品分析任务表.xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
    }
}