using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Reflection;
using System.Data;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using System.Configuration;

/// 委托书--采样任务下达
/// 创建时间：2015-01-30
/// 创建人：weilin
public partial class Channels_MisII_Contract_Programed_ContractProgramed : PageBase
{
    private string task_id = "", strBtnType = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHiddenParme();
        if (!IsPostBack)
        {
        }
    }
    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
        strBtnType = this.hidBtnType.Value.ToString();
    }

    
}