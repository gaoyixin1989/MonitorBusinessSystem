﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCFlow.WF
{
    public partial class DBA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Redirect("./Comm/Rpt2DBA.aspx?Rpt2Name=BP.WF.Data.DBAFlowRpt", true);
        }
    }
}