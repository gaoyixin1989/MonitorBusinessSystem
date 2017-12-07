using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using System.Collections;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.BusinessLogic.Channels.Mis.Monitor.QC;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.ValueObject.Channels.Env.Point.River;
using i3.BusinessLogic.Channels.Env.Point.River;

using i3.ValueObject.Channels.Mis.Monitor.Task;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

public partial class Channels_MisII_EnvPlan_EnvPlanCheck : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    //[WebMethod]
    //public static string getMonitorType(string strTypeId)
    //{
    //    string strReturnValue = "";
    //    i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo objMonitorTypeVo = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(strTypeId);
    //    strReturnValue = objMonitorTypeVo.REMARK1 == "" ? objMonitorTypeVo.ID : objMonitorTypeVo.REMARK1;
    //    return strReturnValue;
    //}
}