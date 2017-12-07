using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //MemoryStream stream = new MemoryStream(Convert.FromBase64String(@"编码"));
        //Response.Clear();
        //Response.AppendHeader("Content-Disposition", "attachment;filename="+ HttpUtility.UrlEncode(Path.GetFileName("aaa.jpg"),System.Text.Encoding.UTF8));
        //Response.BinaryWrite(stream.ToArray());
        Response.Redirect("Portal/Login.aspx");
    }
}