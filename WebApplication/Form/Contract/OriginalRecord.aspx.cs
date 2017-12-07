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
namespace n27
{

    public partial class Form_Contract_OriginalRecord : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // /Form/Contract/OriginalRecord.aspx?FK_Flow=140&FK_Node=14009&FID=751&WorkID=752&AtPara=@GroupMark=qi|1-2hao&IsRead=0&T=201512015564&NodeID=14009&UserNo=administrator
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

                CCFlowFacade.Node_CreateBlankWork(UserNo, "141", UserNo, null, workID, fid, flowId, nodeId, UserNo, 14103, "administrator", "@GroupMark=xxxx");





                Response.Write("true");
                Response.ContentType = "text/plain";
                Response.End();
            }

            //如果是方向条件判断
            if (!string.IsNullOrEmpty(Request.QueryString["DirectionType"]))
            {
                var type = Request.QueryString["DirectionType"];
                var direction = Request.QueryString["Direction"];

                var workID = Request.QueryString["WorkId"];
                var fid = Request.QueryString["FID"];//fid为0，待解决
                workID = workID ?? Int32.MinValue.ToString();

                var atPara = Request.QueryString["AtPara"];//没有传入，待解决

                switch (type)
                {
                    case "type1":

                        if (direction == "d1")
                        {
                            if (atPara.Contains("shui") || atPara.Contains("yanchong"))
                            {
                                Response.Write("1");
                            }
                            else
                            {
                                Response.Write("1");
                            }
                        }
                        else
                        {
                            Response.Write("1");
                        }

                        Response.End();
                        break;
                    default:
                        Response.Write("0");
                        Response.End();
                        break;

                }
            }
            else
            {
                if (!IsPostBack)
                {
                    //子线程业务标识&AtPara=@GroupMark=abc
                    var atPara = Request.QueryString["AtPara"];
                    this.lblIdentification.Text += atPara;


                    var workID = Request.QueryString["WorkID"];
                    var fid = Request.QueryString["FID"];//FID为父流程ID

                    workID = workID ?? Int32.MinValue.ToString();//如果为空会查询出记录，所以查询时workID不能为空

                    var contract = new TSysLogLogic().SelectByObject(new TSysLogVo { REMARK3 = fid });

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
}