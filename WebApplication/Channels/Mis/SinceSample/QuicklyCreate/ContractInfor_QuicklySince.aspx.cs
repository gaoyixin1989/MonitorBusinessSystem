using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Reflection;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Contract;
/// <summary>
/// 功能描述：自送样委托书录入
/// 创建时间：2012-12-18
/// 创建人：胡方扬
/// </summary>
public partial class Channels_Mis_SinceSample_QuicklyCreate_ContractInfor_Since : PageBase
{
    private string task_id = "", strTaskCode = "", strTaskProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
        //获取隐藏域参数
        GetHiddenParme();
      
    }
    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
        strTaskCode = this.hidTaskCode.Value.ToString();
        strTaskProjectName = this.hidTaskProjectName.Value.ToString();
    }
}