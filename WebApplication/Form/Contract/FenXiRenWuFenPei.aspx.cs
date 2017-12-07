using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.Log;
using i3.BusinessLogic.Sys.Log;
using WebApplication;

public partial class Form_Contract_FenXiRenWuFenPei : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var url = Request.RawUrl;

        if (Request.QueryString["type"] == "check")
        {
            var workID = Convert.ToInt32(Request.QueryString["OID"]);//OID为流程ID

            var flowId = Request.QueryString["FK_Flow"];
            var nodeId = Convert.ToInt32(Request.QueryString["FK_Node"]);
            var fid = Convert.ToInt32(Request.QueryString["FID"]);

            var UserNo = Request.QueryString["UserNo"];//有两个UserNo???等解决
            UserNo = UserNo.Split(',').Count() > 1 ? UserNo.Split(',')[1] : UserNo;

            //server.Node_CreateBlankWork(LogInfo.UserInfo.USER_NAME,141,LogInfo.UserInfo.USER_NAME,workID,fid,140,14009,

            CCFlowFacade.Node_CreateBlankWork(UserNo, "142", UserNo, null, workID, fid, "140", nodeId, UserNo, 14202, "administrator", "@GroupMark=xxxx");

            CCFlowFacade.Node_CreateBlankWork(UserNo, "142", UserNo, null, workID, fid, "140", nodeId, UserNo, 14202, "administrator", "@GroupMark=yyyy");

            CCFlowFacade.Node_CreateBlankWork(UserNo, "143", UserNo, null, workID, fid, "140", nodeId, UserNo, 14302, "administrator", "@GroupMark=zzzz");

            Response.Write("true");
            Response.ContentType = "text/plain";
            Response.End();
        }


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