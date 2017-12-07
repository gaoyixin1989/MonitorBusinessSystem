using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.Log;
using i3.BusinessLogic.Sys.Log;

public partial class Form_Contract_FenXiShiFuHe : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var url = Request.RawUrl;


        if (!IsPostBack)
        {
             
            


            var workID = Request.QueryString["WorkID"];
            var fid = Request.QueryString["FID"];

            workID = workID ?? Int32.MinValue.ToString();//如果为空会查询出记录，所以查询时workID不能为空

            

            

            //来自工作流节点对表单的配置信息，根据配置信息控制表单的显示
            var isView = Request.QueryString["IsView"];

            if (isView == "true")
            {

            }


        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {

    }
    protected void btnDistribute_Click(object sender, EventArgs e)
    {

    }
}