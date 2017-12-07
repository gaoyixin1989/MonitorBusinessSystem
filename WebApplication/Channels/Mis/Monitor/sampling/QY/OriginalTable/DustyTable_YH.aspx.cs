using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;

using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using System.Text.RegularExpressions;

/// <summary>
/// 创建原因：实现烟气黑度动态属性（原始记录表）数据保存功能-- 也可根据配置配置其他
/// 创建人：魏林
/// 创建时间：2014-11-12 11：10
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QY_OriginalTable_DustyTable_YH : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!String.IsNullOrEmpty(strAction)) {

        //    switch (strAction) { 
        //        case "SaveBaseInfor":
        //            Response.Write(SaveBaseInfor());
        //            Response.End();
        //            break;
        //        case "getBaseInfor":
        //            Response.Write(getBaseInfor());
        //            Response.End();
        //            break;
        //        case "SaveAttInfor":
        //            Response.Write(SaveAttInfor());
        //            Response.End();
        //            break;
        //        case "getAttInfor":
        //            Response.Write(getAttInfor());
        //            Response.End();
        //            break;
        //        case "UpdateAttValue":
        //            Response.Write(UpdateAttValue());
        //            Response.End();
        //            break;
        //        case "getCompanyInfor":
        //            Response.Write(getCompanyInfor());
        //            Response.End();
        //            break;
        //        case "delAttInfor":
        //            Response.Write(delAttInfor());
        //            Response.End();
        //            break;
        //        default: 
        //            break;
        //    }
        //}
    }

}