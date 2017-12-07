using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using System.Data;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using i3.BusinessLogic.Channels.Env.Point.PolluteRule;

/// <summary>
/// 点位编辑：监测项目选择功能
/// 创建日期：2012-11-14
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Env_Point_ItemInfoSetting : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.POINT_NAME.Text = Request["strItemListTitleName"].ToString();
            BindList();
        }
    }
    protected void BindList()
    {
        //数据库表名
        string strTableName = Request["TableName"].ToString();
        //数据库列名
        string strColumnName = Request["ColumnName"].ToString();
        //需要查询的数据库列名
        string strWhereColumnName = Request["strWhereColumnName"].ToString();
        //垂线代码
        string strPointCode = Request["PointCode"].ToString();
        //监测类型
        string strMonitorType = Request["MonitorType"].ToString();

        if (strTableName == "T_ENV_P_POLLUTE_ITEM")
        {
            #region//常规污染源
            TEnvPPolluteVo TEnvPEnterInfo = new TEnvPPolluteVo();
            TEnvPEnterInfo.ID = strPointCode;//监测项目ID
            string type = new TEnvPPolluteLogic().GetType(TEnvPEnterInfo);
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("废水"))
                {
                    strMonitorType = "000000001";
                }
                if (type.Equals("废气"))
                {
                    strMonitorType = "000000002";
                }
                if(type.Equals("噪声"))
                {
                    strMonitorType = "000000004";
                }
            }
            #endregion
        }
        else
        {
            strMonitorType = new i3.BusinessLogic.Channels.Env.Point.Common.CommonLogic().getNameByID("T_BASE_MONITOR_TYPE_INFO", "REMARK1", "ID", strMonitorType);
        }
        TBaseItemInfoVo objItem = new TBaseItemInfoVo(); 
        
        objItem.IS_DEL = "0";
        objItem.MONITOR_ID = strMonitorType;
        List<TBaseItemInfoVo> lstItem = new TBaseItemInfoLogic().SelectByObject(objItem, 0, 0);
        lstItem.Sort(delegate(TBaseItemInfoVo a, TBaseItemInfoVo b) { return a.ORDER_NUM.CompareTo(b.ORDER_NUM); });

        this.ListBox1.DataSource = lstItem;
        this.ListBox1.DataValueField = TBaseItemInfoVo.ID_FIELD;
        this.ListBox1.DataTextField = TBaseItemInfoVo.ITEM_NAME_FIELD;
        this.ListBox1.DataBind();

        DataTable dt = new i3.BusinessLogic.Channels.Env.Point.Common.CommonLogic().getVerticalItem(strTableName, strWhereColumnName, strPointCode);

        string strPointItemIDs = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strPointItemIDs += "," + dt.Rows[i][strColumnName].ToString();
        }
        strPointItemIDs = strPointItemIDs + ",";

        for (int i = lstItem.Count - 1; i >= 0; i--)
        {
            if (!("," + strPointItemIDs + ",").Contains("," + lstItem[i].ID + ","))
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

        lbItemNum.Text = ListBox2.Items.Count.ToString();
    }
}