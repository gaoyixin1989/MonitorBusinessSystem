using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;

public partial class Channels_Base_Method_CopyMethod : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(this.TextBox1.Text)) {
            return;
        }
        if (String.IsNullOrEmpty(this.TextBox2.Text))
        {
            return;
        }

        else {
            string strSourceType = this.TextBox1.Text.ToString();
            string strToType = this.TextBox2.Text.ToString();

            TBaseMethodInfoVo TBaseMethodInfoVo = new TBaseMethodInfoVo();
            if (new TBaseMethodInfoLogic().CopyInfor(strSourceType, strToType)) {
                Response.Write("<script language='javascript'>alert('复制成功');</script>");
            }
        }
    }
}