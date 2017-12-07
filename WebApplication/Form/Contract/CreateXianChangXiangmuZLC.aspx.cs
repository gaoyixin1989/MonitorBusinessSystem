using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.Log;
using i3.BusinessLogic.Sys.Log;
using System.Net;
using System.IO;
namespace n25
{

    public partial class Form_Contract_CreateXianChangXiangmuZLC : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var url = Request.RawUrl;


            if (!IsPostBack)
            {
                //子线程业务标识&AtPara=@GroupMark=abc
                var atPara = Request.QueryString["AtPara"];
                this.lblIdentification.Text += atPara;


                var workID = Request.QueryString["WorkID"];
                var fid = Request.QueryString["FID"];

                workID = workID ?? Int32.MinValue.ToString();//如果为空会查询出记录，所以查询时workID不能为空



                try
                {
                    //var url2 = "http://192.168.1.31:10002/WF/MyFlow.aspx?FK_Flow=137&PWorkID=" + workID + "&PNodeID=14011&PFlowNo=140&PFID=" + fid + "&JumpToNode=13703&JumpToEmp=administrator";
                    //WebRequest request = WebRequest.Create(url2);
                    //WebResponse response = request.GetResponse();


                    Response.Write("bb");
                    Response.End();



                }
                catch (WebException ex)
                {

                    //throw ex;

                    Response.Write("aa");
                    Response.End();
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