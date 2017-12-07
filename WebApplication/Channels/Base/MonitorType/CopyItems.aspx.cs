using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject;
public partial class Channels_Base_MonitorType_CopyItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strId=this.TextBox1.Text.ToString();
        string strToId = this.TextBox2.Text.ToString();
        if (!String.IsNullOrEmpty(strId) && !String.IsNullOrEmpty(strToId))
        {
            TBaseItemInfoVo objImtes = new TBaseItemInfoVo();
            objImtes.MONITOR_ID = strId;
            objImtes.IS_DEL = "0";
            if (new TBaseItemInfoLogic().CopySameMonitorItemInfor(objImtes, strToId))
            {
                Response.Write("<script language='javascript'>alert('复制成功！');</script>");
            }
        }
        else {
            return;
        }
    }
}