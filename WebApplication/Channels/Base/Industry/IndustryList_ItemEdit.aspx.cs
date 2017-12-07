using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Industry;
using i3.BusinessLogic.Channels.Base.Industry;
using i3.BusinessLogic.Channels.Base.MonitorType;

/// <summary>
/// 模块功能：行业项目设置
/// 创建日期：2012-11-14
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_Industry_IndustryList_ItemEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.Industry_IDs.Text = new TBaseIndustryInfoLogic().Details(this.Request["IndustryID"].ToString()).INDUSTRY_NAME;
            BindList();
        }
    }

    protected void BindList()
    {
        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.IS_DEL = "0";
        List<TBaseItemInfoVo> lstItem = new TBaseItemInfoLogic().SelectByObject(objItem, 0, 0);
        lstItem.Sort(delegate(TBaseItemInfoVo a, TBaseItemInfoVo b) {
            if (a.MONITOR_ID.CompareTo(b.MONITOR_ID) == 0)
                return a.ORDER_NUM.CompareTo(b.ORDER_NUM);
            else
                return a.MONITOR_ID.CompareTo(b.MONITOR_ID);
        });

        for (int i = 0; i < lstItem.Count; i++)
        {
            lstItem[i].ITEM_NAME = new TBaseMonitorTypeInfoLogic().Details(lstItem[i].MONITOR_ID).MONITOR_TYPE_NAME + "—" + lstItem[i].ITEM_NAME;
        }

        this.ListBox1.DataSource = lstItem;
        this.ListBox1.DataValueField = TBaseItemInfoVo.ID_FIELD;
        this.ListBox1.DataTextField = TBaseItemInfoVo.ITEM_NAME_FIELD;
        this.ListBox1.DataBind();

        TBaseIndustryItemVo objIndustryItem = new TBaseIndustryItemVo();
        objIndustryItem.INDUSTRY_ID = this.Request["IndustryID"].ToString();
        List<TBaseIndustryItemVo> lstIndustryItem = new TBaseIndustryItemLogic().SelectByObject(objIndustryItem, 0, 0);

        string strIndustryItemIDs = "";
        for (int i = 0; i < lstIndustryItem.Count; i++)
        {
            strIndustryItemIDs += "," + lstIndustryItem[i].ITEM_ID;
        }
        strIndustryItemIDs +=  ",";

        for (int i = lstItem.Count - 1; i >= 0; i--)
        {
            if (!strIndustryItemIDs.Contains(lstItem[i].ID))
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
}