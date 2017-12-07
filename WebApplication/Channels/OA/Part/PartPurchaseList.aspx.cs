using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;

public partial class Channels_OA_Part_PartPurchaseList : SWPrint
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 导出、打印发文
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string fwID = this.hidFwId.Value;
        WSExport(fwID);
    }    
}