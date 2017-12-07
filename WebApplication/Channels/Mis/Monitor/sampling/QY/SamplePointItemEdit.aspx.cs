using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;

/// <summary>
/// 点位编辑：采样-点位项目设置
/// 创建日期：2012-12-14
/// 创建人  ：苏成斌
/// </summary>

public partial class Channels_Mis_Monitor_sampling_QY_SamplePointItemEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.POINT_IDs.Text = new TBaseCompanyPointLogic().Details(this.Request["PointID"].ToString()).POINT_NAME;
            BindList();
        }
    }

    protected void BindList()
    {
        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.IS_DEL = "0";
        objItem.MONITOR_ID = new TMisMonitorSubtaskLogic().Details(this.Request["SubtaskID"].ToString()).MONITOR_ID;
        List<TBaseItemInfoVo> lstItem = new TBaseItemInfoLogic().SelectByObject(objItem, 0, 0);
        lstItem.Sort(delegate(TBaseItemInfoVo a, TBaseItemInfoVo b) { return a.ORDER_NUM.CompareTo(b.ORDER_NUM); });

        this.ListBox1.DataSource = lstItem;
        this.ListBox1.DataValueField = TBaseItemInfoVo.ID_FIELD;
        this.ListBox1.DataTextField = TBaseItemInfoVo.ITEM_NAME_FIELD;
        this.ListBox1.DataBind();

        TMisMonitorTaskItemVo objPointItem = new TMisMonitorTaskItemVo();
        objPointItem.IS_DEL = "0";
        objPointItem.TASK_POINT_ID = new TMisMonitorSampleInfoLogic().Details(this.Request["PointID"].ToString()).POINT_ID;
        List<TMisMonitorTaskItemVo> lstPointItem = new TMisMonitorTaskItemLogic().SelectByObject(objPointItem, 0, 0);

        string strPointItemIDs = "";
        for (int i = 0; i < lstPointItem.Count; i++)
        {
            strPointItemIDs += "," + lstPointItem[i].ITEM_ID;
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
}