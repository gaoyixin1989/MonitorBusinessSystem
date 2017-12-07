using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;

/// <summary>
/// 点位编辑：项目包项目设置
/// 创建日期：2012-11-14
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_Item_ItemBagItemEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.POINT_IDs.Text = new TBaseItemInfoLogic().Details(this.Request["selBagID"].ToString()).ITEM_NAME;
            BindList();
        }
    }

    protected void BindList()
    {
        string strMonitorID = new TBaseItemInfoLogic().Details(this.Request["selBagID"].ToString()).MONITOR_ID;

        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.IS_DEL = "0";
        objItem.IS_SUB = "1";
        objItem.HAS_SUB_ITEM = "0";
        objItem.MONITOR_ID = strMonitorID;
        List<TBaseItemInfoVo> lstItem = new TBaseItemInfoLogic().SelectByObject(objItem, 0, 0);
        lstItem.Sort(delegate(TBaseItemInfoVo a, TBaseItemInfoVo b) { return a.ORDER_NUM.CompareTo(b.ORDER_NUM); });

        this.ListBox1.DataSource = lstItem;
        this.ListBox1.DataValueField = TBaseItemInfoVo.ID_FIELD;
        this.ListBox1.DataTextField = TBaseItemInfoVo.ITEM_NAME_FIELD;
        this.ListBox1.DataBind();

        TBaseItemSubItemVo objSubItem = new TBaseItemSubItemVo();
        objSubItem.IS_DEL = "0";
        objSubItem.PARENT_ITEM_ID = this.Request["selBagID"].ToString();
        List<TBaseItemSubItemVo> lstSubItem = new TBaseItemSubItemLogic().SelectByObject(objSubItem, 0, 0);

        string strSubItemIDs = "";
        for (int i = 0; i < lstSubItem.Count; i++)
        {
            strSubItemIDs += "," + lstSubItem[i].ITEM_ID;
        }
        strSubItemIDs = strSubItemIDs + ",";

        for (int i = lstItem.Count - 1; i >= 0; i--)
        {
            if (!strSubItemIDs.Contains(lstItem[i].ID))
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