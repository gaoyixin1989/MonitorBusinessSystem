using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.Log;
using i3.BusinessLogic.Sys.Log;
namespace n26
{

    public partial class Form_Contract_GeneralForm : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ///通过超链接创建并跳转到第二个节点Form/Contract/GeneralForm.aspx?FormTitle=XianChangShiFuHe&&FK_Flow=141&FK_Node=14103&FID=0&WorkID=754&AtPara=&NodeID=14103&UserNo=administrator
            ///通过超链接创建进到第一个节点Form/Contract/GeneralForm.aspx?FK_Flow=141&PWorkID=756&PNodeID=14011&PFlowNo=140&PFID=755&WorkID=760&NodeID=14101&FK_Node=14101&FID=0&UserNo=administrator&SID=gvc4msu1lxjuym3ssmoa5lan
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
                var title = Request.QueryString["FormTitle"];

                switch (title)
                {
                    case "XianChangShiFuHe":
                        this.lblFormTitle.Text = "现场室复核";
                        break;
                    case "XianChangZhuRenShenHe":
                        this.lblFormTitle.Text = "现场室主任审核";
                        break;
                    case "FenXiShiFuHe":
                        this.lblFormTitle.Text = "分析室复核";
                        break;
                    case "CaiYanRenHeLu":
                        this.lblFormTitle.Text = "采样人核录";
                        break;
                    case "ZhiLiangFuZeRenShenHe":
                        this.lblFormTitle.Text = "质量负责人审核";
                        break;
                    case "BaoGaoBianZhi":
                        this.lblFormTitle.Text = "报告编制";
                        break;
                    case "FenXiJieGuoTianBao":
                        this.lblFormTitle.Text = "分析结果填报";
                        break;
                    default:
                        break;
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