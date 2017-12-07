using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using i3.View;
using System.Text;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;

/// <summary>
/// 功能描述：数据汇总表导出
/// 创建时间：2013-5-13
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Mis_Report_Print : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["task_id"]))
            {
                this.TASK_ID.Value = Request.QueryString["task_id"].ToString();
            }
        }
    }

    /// <summary>
    /// 导出 Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        exportexcel();
    }

    /// <summary>
    /// 导出 Word
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        exportword();
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    void exportexcel()
    {
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(this.TASK_ID.Value);
        try
        {
            string strGetJson = HttpUtility.UrlDecode(this.hf.Value, Encoding.UTF8);//返回的Json数据字符串
            int intCount = Int32.Parse(this.Ccount.Value);//需导出的列数据
            //转化成DataTable
            DataTable dtExport = new PageBase().JSONToDataTable2(strGetJson);
            //构造DataTable
            for (int i = dtExport.Columns.Count - 1; i > intCount - 1; i--)
            {
                dtExport.Columns.RemoveAt(i);
            }
            //标题数组
            ArrayList arrTitle = new ArrayList();
            arrTitle.Add(new string[3] { "1", "水质样品监测结果汇总表", "true" });
            arrTitle.Add(new string[3] { "2", "ZHJC/JS001                               单位：mg/L                                 任务编号：" + objTask.TICKET_NUM, "false" });
            //列名集合
            ArrayList arrData = new ArrayList();
            for (int i = 0; i < dtExport.Columns.Count; i++)
            {
                arrData.Add(new string[2] { (i + 1).ToString(), dtExport.Columns[i].ColumnName.ToString() });
            }
            //结尾数组
            ArrayList arrEnd = new ArrayList();
            arrEnd.Add(new string[2] { "1", "监测负责人：                                 审核：                                 审定：                                 " });
            arrEnd.Add(new string[2] { "2", "填报科室：中心实验室" });
            arrEnd.Add(new string[2] { "3", DateTime.Now.ToString("yyyy-MM-dd") });
            new ExcelHelper().RenderDataTableToExcel(dtExport, "监测数据汇总表.xls", "../../../TempFile/DataTotal.xls", "监测数据汇总表", 3, true, arrTitle, arrData, arrEnd);
        }
        catch
        { }
    }

    /// <summary>
    /// 导出Word
    /// </summary>
    void exportword()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "utf-8";
        Response.AppendHeader("Content-Disposition", "attachment;filename=tmp.doc");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

        Response.ContentType = "application/ms-word";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        oHtmlTextWriter.WriteLine(hf.Value);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }

}