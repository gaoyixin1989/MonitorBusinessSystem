using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;

/// <summary>
/// 功能描述：工作流管理
/// 创建日期：2012-11-08
/// 创建人  ：石磊
/// 修改说明：改为ligerui
/// 修改时间：2013-01-07
/// 修改人  ：潘德军
/// </summary>
public partial class Sys_WF_WFSettingFlowInputForJS : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strID = "";
        if (Request["strid"] != null)
        {
            strID = this.Request["strid"].ToString();
        }

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            GetData(strID);
        }
    }

    //获取数据
    private void GetData(string strID)
    {
        TWfSettingFlowVo twfsfv = new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { ID = strID });

        string strJson = ToJson(twfsfv);

        Response.Write(strJson);
        Response.End();
    }
}