using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ComleaderFlow.AppServices
{
    public partial class DoType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            string userNo = Request.QueryString["userNo"];
            string url = "";
            if (string.IsNullOrEmpty(BP.Web.WebUser.No) || !BP.Web.WebUser.No.Equals(userNo))
            {
                BP.Port.Emp emp = new BP.Port.Emp(userNo);
                BP.Web.WebUser.SignInOfGener(emp);
            }

            switch (type)
            {
                case "start":
                    url = "../AppDemoLigerUI/Start.aspx";
                    break;

                case "empworks":
                    url = "../AppDemoLigerUI/EmpWorks.aspx";

                    break;

                case "empcc":
                    url = "../AppDemoLigerUI/CC.aspx";
                    break;
                case "runing":
                    url = "../AppDemoLigerUI/Running.aspx";

                    break;
                case "sharework":
                    url = "../WF/TaskPoolSharing.aspx";
                    break;

                case "batch":
                    url = "../WF/Batch.aspx";
                    break;
                case "designer":
                    //url = "../WF/Admin/Xap/Designer.aspx?UserNo="+userNo;
                    url = "../WF/Admin/Xap/Designer.aspx?UserNo=admin&SID=gaoling&s=ddd" + BP.Web.WebUser.SID;
                    break;
                case "hungup":
                    url = "../AppDemoLigerUI/HungUp.aspx";
                    break;
                case "search":
                    url = "../AppDemoLigerUI/keySearch.aspx";
                    break;
                default:
                    throw new Exception("传入的类型不正确!");
            }

            Response.Redirect(url, true);
            return;

        }
    }
}