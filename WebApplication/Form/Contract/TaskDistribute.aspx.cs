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
namespace n28
{
    public partial class Form_Contract_TaskDistribute : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] == "check")
            {

                
                var workID = Convert.ToInt32(Request.QueryString["OID"]);
                var flowId = Request.QueryString["FK_Flow"];
                var nodeId = Convert.ToInt32(Request.QueryString["FK_Node"]);
                var fid = Convert.ToInt32(Request.QueryString["FID"]);

                //1、水 1-2号 administrator
                //2、气 1-2号 administrator
                //3、气 3-4号 administrator
                //4、气 1-2号 llw

                var result = CCFlowFacade.SetNextNodeFH(flowId, workID, nodeId, "administrator@shui|1-2hao,administrator@qi|1-2hao", fid);//中文会乱码，待解决
                //var result = server.SetNextNodeFH(flowId, workID, nodeId, "administrator@shui|1-2hao,administrator@qi|1-2hao,administrator@qi|3-4hao,llw@qi|1-2hao", fid);//中文会乱码，待解决



                Response.Write("true");


                Response.ContentType = "text/plain";
                Response.End();
            }


            if (!IsPostBack)
            {
                var workID = Request.QueryString["WorkID"];

                workID = workID ?? Int32.MinValue.ToString();//如果为空会查询出记录，所以查询时workID不能为空

                var contract = new TSysLogLogic().SelectByObject(new TSysLogVo { REMARK3 = workID });

                if (string.IsNullOrEmpty(contract.ID))
                {
                    this.hidIsNew.Value = "true";
                }
                else
                {
                    this.hidIsNew.Value = "false";
                    this.hidContractId.Value = contract.ID;
                }

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
            //
            //var workID = Convert.ToInt32(Request.QueryString["WorkID"]);
            //var flowId = Request.QueryString["FK_Flow"];
            //var nodeId = Convert.ToInt32(Request.QueryString["FK_Node"]);
            //var fid = Convert.ToInt32(Request.QueryString["FID"]);

            ////1、水 1-2号 administrator
            ////2、气 1-2号 administrator
            ////3、气 3-4号 administrator
            ////4、气 1-2号 llw

            //var result = server.SetNextNodeFH(flowId, workID, nodeId,"administrator@shui|1-2hao,administrator@qi|1-2hao", fid);//中文会乱码，待解决
            ////var result = server.SetNextNodeFH(flowId, workID, nodeId, "administrator@shui|1-2hao,administrator@qi|1-2hao,administrator@qi|3-4hao,llw@qi|1-2hao", fid);//中文会乱码，待解决
        }
    }
}