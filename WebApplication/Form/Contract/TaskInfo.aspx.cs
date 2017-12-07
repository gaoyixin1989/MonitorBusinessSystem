using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.Log;
using i3.BusinessLogic.Sys.Log;
namespace n29
{

    public partial class Form_Contract_TaskInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
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
    }
}