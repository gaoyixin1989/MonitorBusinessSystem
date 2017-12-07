using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Base.Item;
using Microsoft.Reporting.WebForms;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.MonitorType;

/// <summary>
/// 功能描述：质控报表（总表）
/// 创建日期：2013-1-3
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Statistice_QCAccount_Bak : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InitDdl();
        }
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    private void InitDdl()
    {
        TBaseMonitorTypeInfoVo objMonitorType = new TBaseMonitorTypeInfoVo();
        objMonitorType.IS_DEL = "0";
        this.dllMonitorID.DataSource = new TBaseMonitorTypeInfoLogic().SelectByTable(objMonitorType);
        this.dllMonitorID.DataTextField = TBaseMonitorTypeInfoVo.MONITOR_TYPE_NAME_FIELD;
        this.dllMonitorID.DataValueField = TBaseMonitorTypeInfoVo.ID_FIELD;
        this.dllMonitorID.DataBind();

        this.ddlYear.Items.Add(GetItem(DateTime.Now.AddYears(-2).Year.ToString(), DateTime.Now.AddYears(-2).Year.ToString()));
        this.ddlYear.Items.Add(GetItem(DateTime.Now.AddYears(-1).Year.ToString(), DateTime.Now.AddYears(-1).Year.ToString()));
        this.ddlYear.Items.Add(GetItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
        this.ddlYear.Items.Add(GetItem(DateTime.Now.AddYears(1).Year.ToString(), DateTime.Now.AddYears(1).Year.ToString()));
        this.ddlYear.Items.Add(GetItem(DateTime.Now.AddYears(2).Year.ToString(), DateTime.Now.AddYears(2).Year.ToString()));
        this.ddlYear.Items.Add(GetItem(DateTime.Now.AddYears(3).Year.ToString(), DateTime.Now.AddYears(3).Year.ToString()));
        this.ddlYear.SelectedIndex = -1;
        this.ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;

        this.ddlQuarter.Items.Add(GetItem("全年", "0"));
        this.ddlQuarter.Items.Add(GetItem("上半年", "-1"));
        this.ddlQuarter.Items.Add(GetItem("下半年", "-2"));
        this.ddlQuarter.Items.Add(GetItem("第一季度", "1"));
        this.ddlQuarter.Items.Add(GetItem("第二季度", "4"));
        this.ddlQuarter.Items.Add(GetItem("第三季度", "7"));
        this.ddlQuarter.Items.Add(GetItem("第四季度", "10"));
    }

    private ListItem GetItem(string strText, string strValue)
    {
        return new ListItem(strText, strValue);
    }

    private string GetBeginDate()
    {
        string str = "";

        int x = int.Parse(this.ddlQuarter.SelectedValue);
        if (x == 0 || x == -1)
        {
            str = this.ddlYear.SelectedValue + "-1-1";
            return str;
        }
        if (x == -2)
        {
            str = this.ddlYear.SelectedValue + "-7-1";
            return str;
        }

        return this.ddlYear.SelectedValue + "-" + x.ToString() + "-1";
    }

    private string GetEndDate()
    {
        string str = "";

        int x = int.Parse(this.ddlQuarter.SelectedValue);
        int y = (x == 10) ? 1 : (x + 3);
        int nextYear = (x < 10) ? int.Parse(this.ddlYear.SelectedValue) : (int.Parse(this.ddlYear.SelectedValue) + 1);

        if (x == 0 || x == -2)
        {
            nextYear = int.Parse(this.ddlYear.SelectedValue) + 1;
            str = nextYear.ToString() + "-1-1";
            return str;
        }
        if (x == -1)
        {
            str = this.ddlYear.SelectedValue + "-7-1";
            return str;
        }

        return nextYear.ToString() + "-" + y.ToString() + "-1";
    }

    protected void btnStatistical_Click(object sender, EventArgs e)
    {
        getQCInfo();
    }

    /// <summary>
    /// 获取质控信息
    /// </summary>
    protected void getQCInfo()
    {
        string strMonitorID = this.dllMonitorID.SelectedValue;
        string strStartTime = GetBeginDate(), strEndTime = GetEndDate();

        DataTable dtResult = new TMisMonitorResultLogic().getResultWitnTimeAndType(strMonitorID, strStartTime, strEndTime);
        DataTable dtEmptyOut = new TMisMonitorResultLogic().getQCEmptyOutWitnTimeAndType(strMonitorID, strStartTime, strEndTime);
        DataTable dtEmptyBat = new TMisMonitorResultLogic().getQCEmptyBatWitnTimeAndType(strMonitorID, strStartTime, strEndTime);
        DataTable dtTwin = new TMisMonitorResultLogic().getQCTwinWitnTimeAndType(strMonitorID, strStartTime, strEndTime);
        DataTable dtAdd = new TMisMonitorResultLogic().getQCAddWitnTimeAndType(strMonitorID, strStartTime, strEndTime);

        DataTable dt = DoneWithTable(dtResult, dtEmptyOut, dtEmptyBat, dtTwin, dtAdd);

        this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Channels/Statistice/ReportQCAccount.rdlc");
        this.ReportViewer1.LocalReport.DataSources.Clear();
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("QCAccountDataSet", dt));
        this.ReportViewer1.LocalReport.Refresh();
    }

    /// <summary>
    /// 质控总表信息统计
    /// </summary>
    protected DataTable DoneWithTable(DataTable dtResult, DataTable dtEmptyOut, DataTable dtEmptyBat, DataTable dtTwin, DataTable dtAdd)
    {
        DataTable dt = new DataTable();
        #region 组件DataTable列
        dt.Columns.Add("ID", System.Type.GetType("System.String"));
        dt.Columns.Add("ItemName", System.Type.GetType("System.String"));
        dt.Columns.Add("SampleCount", System.Type.GetType("System.String"));
        dt.Columns.Add("EmptyOutCount", System.Type.GetType("System.String"));
        dt.Columns.Add("EmptyOutIsOK", System.Type.GetType("System.String"));
        dt.Columns.Add("EmptyBatCount", System.Type.GetType("System.String"));
        dt.Columns.Add("EmptyBatOffset", System.Type.GetType("System.String"));
        dt.Columns.Add("EmptyBatIsOK", System.Type.GetType("System.String"));
        dt.Columns.Add("TwinCount", System.Type.GetType("System.String"));
        dt.Columns.Add("TwinProportion", System.Type.GetType("System.String"));
        dt.Columns.Add("TwinOffset", System.Type.GetType("System.String"));
        dt.Columns.Add("TwinIsOKCount", System.Type.GetType("System.String"));
        dt.Columns.Add("TwinIsOK", System.Type.GetType("System.String"));
        dt.Columns.Add("AddCount", System.Type.GetType("System.String"));
        dt.Columns.Add("AddProportion", System.Type.GetType("System.String"));
        dt.Columns.Add("AddBack", System.Type.GetType("System.String"));
        dt.Columns.Add("AddIsOKCount", System.Type.GetType("System.String"));
        dt.Columns.Add("AddIsOK", System.Type.GetType("System.String"));
        dt.Columns.Add("ZPBYGS", System.Type.GetType("System.String"));
        dt.Columns.Add("YPBL", System.Type.GetType("System.String"));
        dt.Columns.Add("XDWCFW", System.Type.GetType("System.String"));
        dt.Columns.Add("HGS", System.Type.GetType("System.String"));
        dt.Columns.Add("HGL", System.Type.GetType("System.String"));
        dt.Columns.Add("YZBYGS", System.Type.GetType("System.String"));
        dt.Columns.Add("SHL2", System.Type.GetType("System.String"));
        #endregion

        string strTempItem = "";
        int iCount = 1;
        for (int i = 0; i < dtResult.Rows.Count; i++)
        {
            if (strTempItem == dtResult.Rows[i]["ITEM_ID"].ToString())
                continue;
            strTempItem = dtResult.Rows[i]["ITEM_ID"].ToString();

            DataRow[] drResult = dtResult.Select("ITEM_ID= '" + strTempItem + "'");
            DataRow[] drEmptyOut = dtEmptyOut.Select("ITEMID='" + strTempItem + "'");
            DataRow[] drEmptyOutIsOK = dtEmptyOut.Select("ITEMID='" + strTempItem + "' and EMPTY_ISOK='0'");
            DataRow[] drEmptyBat = dtEmptyBat.Select("ITEMID='" + strTempItem + "'");
            DataRow[] drEmptyBatIsOK = dtEmptyBat.Select("ITEMID='" + strTempItem + "' and QC_EMPTY_ISOK='0'");
            DataRow[] drTwin = dtTwin.Select("ITEMID='" + strTempItem + "'");
            DataRow[] drTwinIsOK = dtTwin.Select("ITEMID='" + strTempItem + "' and TWIN_ISOK='0'");
            DataRow[] drAdd = dtAdd.Select("ITEMID='" + strTempItem + "'");
            DataRow[] drAddIsOK = dtAdd.Select("ITEMID='" + strTempItem + "' and ADD_ISOK='0'");

            DataRow dr = dt.NewRow();

            dr["ID"] = iCount.ToString();
            dr["ItemName"] = new TBaseItemInfoLogic().Details(strTempItem).ITEM_NAME;
            dr["SampleCount"] = drResult.Length.ToString();
            dr["EmptyOutCount"] = (drEmptyOut.Length > 0) ? drEmptyOut.Length.ToString() : "";
            dr["EmptyOutIsOK"] = (drEmptyOut.Length > 0) ? (drEmptyOutIsOK.Length / drEmptyOut.Length).ToString() : "";
            dr["EmptyBatCount"] = (drEmptyBat.Length > 0) ? drEmptyBat.Length.ToString() : "";
            string iBatMax = dtEmptyBat.Compute("Max(QC_EMPTY_OFFSET)", "").ToString();
            string iBatMin = dtEmptyBat.Compute("min(QC_EMPTY_OFFSET)", "").ToString();
            dr["EmptyBatOffset"] = (drEmptyBat.Length > 0) ? iBatMin + "~" + iBatMax : "";
            dr["EmptyBatIsOK"] = (drEmptyBat.Length > 0) ? (drEmptyBatIsOK.Length / drEmptyBat.Length).ToString() : "";
            dr["TwinCount"] = (drTwin.Length > 0) ? drTwin.Length.ToString() : "";
            dr["TwinProportion"] = (drTwin.Length > 0) ? (drTwin.Length / drResult.Length).ToString() : "";
            string iTwinMax = dtTwin.Compute("Max(TWIN_OFFSET)", "").ToString();
            string iTwinMin = dtTwin.Compute("min(TWIN_OFFSET)", "").ToString();
            dr["TwinOffset"] = (drTwin.Length > 0) ? iTwinMin + "~" + iTwinMax : "";
            dr["TwinIsOKCount"] = (drTwinIsOK.Length > 0) ? drTwinIsOK.Length.ToString() : "";
            dr["TwinIsOK"] = (drTwin.Length > 0) ? (drTwinIsOK.Length / drTwin.Length).ToString() : "";
            dr["AddCount"] = (drAdd.Length > 0) ? drAdd.Length.ToString() : "";
            dr["AddProportion"] = (drAdd.Length > 0) ? (drAdd.Length / drResult.Length).ToString() : "";
            string iAddMax = dtAdd.Compute("Max(ADD_BACK)", "").ToString();
            string iAddMin = dtAdd.Compute("min(ADD_BACK)", "").ToString();
            dr["AddBack"] = (drAdd.Length > 0) ? iAddMin + "~" + iAddMax : "";
            dr["AddIsOKCount"] = (drAddIsOK.Length > 0) ? drAddIsOK.Length.ToString() : "";
            dr["AddIsOK"] = (drAddIsOK.Length > 0) ? (drAddIsOK.Length / drResult.Length).ToString() : "";
            dt.Rows.Add(dr);

            iCount++;
        }

        return dt;
    }
}