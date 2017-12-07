using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.Log;
using i3.BusinessLogic.Sys.Log;
namespace n24
{

    public partial class Form_Contract_ContractInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //调用表单/Form/Contract/ContractInfo.aspx?FK_Flow=140&FK_Node=14001&T=2015120145313&WorkID=739&NodeID=14001&FID=0&UserNo=administrator
            //发送事件/Form/Contract/ContractInfo.aspx?type=check&UserNo=administrator&OID=739&FK_Flow=140&FK_Node=14001&T=2015120145313&EntityName=BP.WF.GEStartWork&EntityPK=OID&EntityPKVal=739&FK_Event=ND14001_SendWhen
            var url = Request.RawUrl;

            //如果是发送前事件
            if (Request.QueryString["type"] == "check")
            {
                var workID = Request.QueryString["OID"];//OID为流程ID

                workID = workID ?? Int32.MinValue.ToString();

                var contract = new TSysLogLogic().SelectByObject(new TSysLogVo { REMARK3 = workID });

                if (string.IsNullOrEmpty(contract.ID))
                {
                    Response.Write(HttpUtility.UrlEncode("false委托书没有保存，不能发送"));
                }
                else
                {
                    Response.Write("true");
                }

                Response.ContentType = "text/plain";
                Response.End();
            }

            //如果是方向条件判断
            if (!string.IsNullOrEmpty(Request.QueryString["DirectionType"]))
            {
                var type = Request.QueryString["DirectionType"];
                var direction = Request.QueryString["Direction"];

                var workID = Request.QueryString["WorkId"];
                workID = workID ?? Int32.MinValue.ToString();

                switch (type)
                {
                    case "type1":

                        var contract = new TSysLogLogic().SelectByObject(new TSysLogVo { REMARK3 = workID });

                        if (string.IsNullOrEmpty(contract.ID))
                        {
                            Response.Write("0");
                        }
                        else
                        {
                            if (direction == "feiyongshenhe")
                            {
                                if (contract.LOG_CONTENT == "一般性委托")
                                {
                                    Response.Write("1");
                                }
                                else
                                {
                                    Response.Write("0");
                                }
                            }
                            //else if (direction == "fanganbianzhi")
                            //{
                            //    Response.Write("1");
                            //}
                            else
                            {
                                Response.Write("1");
                            }
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
                        this.txtContent.Text = contract.LOG_CONTENT;

                        //有审核信息时显示审核信息
                        if (!string.IsNullOrEmpty(contract.REMARK2))
                        {
                            this.txtApprove.Text = contract.REMARK2;
                            this.txtApprove.Visible = true;
                            this.txtApprove.Enabled = false;
                        }
                    }

                    //来自工作流节点对表单的配置信息，根据配置信息控制表单的显示
                    var isView = Request.QueryString["IsView"];

                    if (isView == "true")
                    {
                        this.txtContent.Enabled = false;
                        this.btnSave.Visible = false;
                    }

                    var approveType = Request.QueryString["ApproveType"];

                    if (approveType == "approve1")
                    {
                        this.txtApprove.Visible = true;
                        this.btnApprove.Visible = true;
                    }

                    var IsCanUpload = Request.QueryString["IsCanUpload"];

                    if (IsCanUpload == "true")
                    {
                        this.uploadDiv.Visible = true;
                    }

                    var isShowAttachment = Request.QueryString["IsShowAttachment"];

                    if (isShowAttachment == "true")
                    {
                        this.attachmentDiv.Visible = true;
                    }


                }

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var workID = Request.QueryString["WorkID"];


            var logic = new TSysLogLogic();

            if (this.hidIsNew.Value == "true")
            {
                var vo = new TSysLogVo();
                vo.ID = i3.View.PageBase.GetSerialNumber("log_id");
                vo.LOG_CONTENT = txtContent.Text;
                vo.LOG_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                vo.REMARK3 = workID;


                logic.Create(vo);
            }
            else
            {
                var vo = logic.SelectByObject(new TSysLogVo { ID = this.hidContractId.Value });
                vo.LOG_CONTENT = txtContent.Text;

                logic.Edit(vo);
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            var logic = new TSysLogLogic();

            var vo = logic.SelectByObject(new TSysLogVo { ID = this.hidContractId.Value });
            vo.REMARK2 = txtApprove.Text;

            logic.Edit(vo);
        }
    }
}