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
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

public partial class Sys_WF_WFSettingTaskInputDetailForJS : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strWF_ID = "";
        if (Request["WF_ID"] != null)
        {
            strWF_ID = this.Request["WF_ID"].ToString();
        }
        string strWF_TASK_ID = "";
        if (Request["WF_TASK_ID"] != null)
        {
            strWF_TASK_ID = this.Request["WF_TASK_ID"].ToString();
        }

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            GetData(strWF_ID, strWF_TASK_ID);
        }
    }

    //获取数据
    private void GetData(string strWF_ID,string strWF_TASK_ID)
    {
        TWfSettingTaskVo objVo = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strWF_TASK_ID, WF_ID = strWF_ID });
        TWfSettingTaskFormVo formTemp = new TWfSettingTaskFormLogic().Details(new TWfSettingTaskFormVo() { WF_TASK_ID = strWF_TASK_ID, WF_ID = strWF_ID });

        objVo.POSITION_IX = formTemp.UCM_TYPE;
        objVo.POSITION_IY = formTemp.UCM_ID;

        objVo.COMMAND_NAME = objVo.COMMAND_NAME.Replace("|", ";");
        if (objVo.COMMAND_NAME.EndsWith(";"))
            objVo.COMMAND_NAME = objVo.COMMAND_NAME.Substring(0, objVo.COMMAND_NAME.Length - 1);

        objVo.FUNCTION_LIST = objVo.FUNCTION_LIST.Replace("|", ";");
        if (objVo.FUNCTION_LIST.EndsWith(";"))
            objVo.FUNCTION_LIST = objVo.FUNCTION_LIST.Substring(0, objVo.FUNCTION_LIST.Length - 1);

        objVo.OPER_VALUE = objVo.OPER_VALUE.Replace("|", ";");
        if (objVo.OPER_VALUE.EndsWith(";"))
            objVo.OPER_VALUE = objVo.OPER_VALUE.Substring(0, objVo.OPER_VALUE.Length - 1);

        objVo.OPER_TYPE = GetUserNames(objVo.OPER_VALUE);

        string strJson = ToJson(objVo);

        Response.Write(strJson);
        Response.End();
    }

    private string GetUserNames(string strUserIDs)
    {
        string strUserNames = "";

        string[] arrUserID = strUserIDs.Split(';');
        for (int i = 0; i < arrUserID.Length; i++)
        {
            strUserNames += (strUserNames.Length > 0 ? ";" : "") + new TSysUserLogic().Details(arrUserID[i]).REAL_NAME;
        }

        return strUserNames;
    }
}