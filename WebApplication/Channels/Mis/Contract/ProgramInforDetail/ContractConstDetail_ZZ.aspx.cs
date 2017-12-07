using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Channels_Mis_Contract_ProgramInforDetail_ContractConstDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strFilePath = "../TempFile/Fee_ZZ.xls";
        System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(strFilePath)); 
    }
}