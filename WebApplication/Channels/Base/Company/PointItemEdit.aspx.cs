using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.ValueObject.Channels.Base.Industry;
using i3.BusinessLogic.Channels.Base.Industry;

/// <summary>
/// 点位编辑：点位项目设置
/// 创建日期：2012-11-14
/// 创建人  ：潘德军
/// 修改时间：2013-03-14
/// 修改人：胡方扬
/// 修改原因：为用户提供监测类别可按照行业类别进行检索设置
/// </summary>
public partial class Channels_Base_Company_PointItemEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetIndustyItems();
            this.POINT_IDs.Text = new TBaseCompanyPointLogic().Details(this.Request["PointID"].ToString()).POINT_NAME;
            BindList();
        }
    }

    protected void BindList()
    {
        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.IS_DEL = "0";
        objItem.MONITOR_ID = this.Request["MonitorType"].ToString();
        List<TBaseItemInfoVo> lstItem = new TBaseItemInfoLogic().SelectByObject(objItem, 0, 0);
        lstItem.Sort(delegate(TBaseItemInfoVo a, TBaseItemInfoVo b) { return a.ORDER_NUM.CompareTo(b.ORDER_NUM); });

        this.ListBox1.DataSource = lstItem;
        this.ListBox1.DataValueField = TBaseItemInfoVo.ID_FIELD;
        this.ListBox1.DataTextField = TBaseItemInfoVo.ITEM_NAME_FIELD;
        this.ListBox1.DataBind();

        TBaseCompanyPointItemVo objPointItem = new TBaseCompanyPointItemVo();
        objPointItem.IS_DEL = "0";
        objPointItem.POINT_ID = this.Request["PointID"].ToString();
        List<TBaseCompanyPointItemVo> lstPointItem = new TBaseCompanyPointItemLogic().SelectByObject(objPointItem, 0, 0);

        string strPointItemIDs = "";
        for (int i = 0; i < lstPointItem.Count; i++)
        {
            strPointItemIDs +=  ","  + lstPointItem[i].ITEM_ID;
        }
        strPointItemIDs = strPointItemIDs + ",";

        for (int i = lstItem.Count - 1; i >= 0; i--)
        {
            if (!strPointItemIDs.Contains(lstItem[i].ID))
                lstItem.RemoveAt(i);
        }

        this.ListBox2.DataSource = lstItem;
        this.ListBox2.DataValueField = TBaseItemInfoVo.ID_FIELD;
        this.ListBox2.DataTextField = TBaseItemInfoVo.ITEM_NAME_FIELD;
        this.ListBox2.DataBind();


        for (int i = 0; i < ListBox2.Items.Count; i++)
        {
            ListItem item = ListBox2.Items[i];
            ListBox1.Items.Remove(item);
        }
    }
    /// <summary>
    /// 获取指定行业类别的监测项目 胡方扬 2013-03-14
    /// </summary>
    /// <returns></returns>
    private void GetIndurstyAllItems(string strIndustryId)
    {
        string strMonitorID = "", strPointID = "";
        DataTable dt = new DataTable();
        TBaseIndustryInfoVo objItem = new TBaseIndustryInfoVo();
        objItem.IS_DEL = "0";
        objItem.ID = strIndustryId;

        if (!String.IsNullOrEmpty(Request.Params["MonitorType"]))
        {
            strMonitorID = Request.Params["MonitorType"].ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["PointID"]))
        {
            strPointID = Request.Params["PointID"].ToString();
        }

        dt = new TBaseIndustryInfoLogic().SelectByObjectForIndustry(objItem, strMonitorID, 0, 0);
        this.ListBox1.Items.Clear();
        if(dt.Rows.Count>0){
            this.ListBox1.DataSource = dt;
            this.ListBox1.DataTextField = "ITEM_NAME";
            this.ListBox1.DataValueField = "ITEM_ID";
            this.ListBox1.DataBind();
        }
    }
    /// <summary>
    /// 根据点位，行业类别，确定企业已选的监测项目 胡方扬 2013-03-14
    /// </summary>
    /// <returns></returns>
    private void GetIndurstySelectedItems(string strIndustryId)
    {
        string strMonitorID = "", strPointID = "";
        DataTable dt = new DataTable();
        TBaseIndustryInfoVo objItem = new TBaseIndustryInfoVo();
        objItem.IS_DEL = "0";
        objItem.ID = strIndustryId;

        if (!String.IsNullOrEmpty(Request.Params["MonitorType"]))
        {
            strMonitorID = Request.Params["MonitorType"].ToString();
        }
        if (!String.IsNullOrEmpty(Request.Params["PointID"]))
        {
            strPointID = Request.Params["PointID"].ToString();
        }

        dt = new TBaseIndustryInfoLogic().SelectByObjectForFinishedIndustry(objItem, strMonitorID,strPointID, 0, 0);
        this.ListBox2.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            this.ListBox2.DataSource = dt;
            this.ListBox2.DataTextField = "ITEM_NAME";
            this.ListBox2.DataValueField = "ITEM_ID";
            this.ListBox2.DataBind();

            for (int i = 0; i < ListBox2.Items.Count; i++)
            {
                ListItem item = ListBox2.Items[i];
                ListBox1.Items.Remove(item);
            }
        }
    }

    /// <summary>
    /// 获取行业类别 胡方扬 2013-03-14
    /// </summary>
    /// <returns></returns>
    protected void GetIndustyItems() {
        DataTable dt = new DataTable();
        TBaseIndustryInfoVo objItems = new TBaseIndustryInfoVo();
        objItems.IS_DEL = "0";
        objItems.IS_SHOW = "1";
        dt = new TBaseIndustryInfoLogic().SelectByTable(objItems);
        this.DropIndustry.DataSource = dt;
        this.DropIndustry.DataTextField = "INDUSTRY_NAME";
        this.DropIndustry.DataValueField = "ID";
        this.DropIndustry.DataBind();
        this.DropIndustry.Items.Insert(0, new ListItem("=请选择=", "0"));
    }
    /// <summary>
    /// 行业类别选择事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropIndustry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DropIndustry.SelectedValue.ToString() == "0") {
            return;
        }
        GetIndurstyAllItems(this.DropIndustry.SelectedValue.ToString());
        GetIndurstySelectedItems(this.DropIndustry.SelectedValue.ToString());
    }
}