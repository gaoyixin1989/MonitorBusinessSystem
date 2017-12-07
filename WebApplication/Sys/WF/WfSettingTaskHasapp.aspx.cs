using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;

public partial class Sys_WF_WfSettingTaskHasapp : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        //获取纸质数据审核记录信息
        if (Request["type"] != null && Request["type"].ToString() == "getAppInfo")
        {
            strResult = getAppInfo();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取纸质数据审核记录信息
    /// </summary>
    /// <returns></returns>
    private string getAppInfo()
    {
        TWfSettingTaskHasappVo tObjVo = new TWfSettingTaskHasappVo();
        
        //int intTotalCount = 0;
        //intTotalCount = new TWfSettingTaskHasappLogic().GetSelectResultCount_ForApp(base.LogInfo.UserInfo.ID);

        DataTable dt = new TWfSettingTaskHasappLogic().SelectByTable_ForApp(base.LogInfo.UserInfo.ID, 0, 0);

        #region 过滤重复列--可能存在同一任务，经过多次退回，存在多次审核的情况
        DataTable dttmp = new DataTable();
        for (int i=0;i<dt.Columns.Count;i++)
        {
            dttmp.Columns.Add(dt.Columns[i].ColumnName);
        }

        string strTmps = "";
        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        {
            string strtmp = "";
            for (int j = 1; j < dt.Columns.Count; j++)
            {
                strtmp += "," + dt.Rows[i][j].ToString();
            }
            strtmp += ",";

            if (!strTmps.Contains(strtmp))
            {
                strTmps += "|" + strtmp;

                DataRow dr = dttmp.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dr[j] = dt.Rows[i][j].ToString();
                }
                dttmp.Rows.InsertAt(dr, 0);
            }
        }
        #endregion

        string strJson = CreateToJson(dttmp, dttmp.Rows.Count);
        return strJson;
    }

    /// <summary>
    /// 纸质数据审核
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string appData(string strWF_ID,string strWF_TASK_ID,string strWF_TASK_Name,string strTASK_ID)
    {
        TWfSettingTaskHasappVo tObjVo = new TWfSettingTaskHasappVo();
        tObjVo.WF_ID = strWF_ID;
        tObjVo.WF_TASK_ID = strWF_TASK_ID;
        tObjVo.WF_TASK_NAME = strWF_TASK_Name;
        tObjVo.TASK_ID = strTASK_ID;
        tObjVo.HAS_APP = "1";
        tObjVo.ID = GetSerialNumber("T_WF_SETTING_TASK_HASAPP");

        bool isSuccess = new TWfSettingTaskHasappLogic().Create(tObjVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("纸质数据审核", "", new PageBase().LogInfo.UserInfo.USER_NAME + "设置已签" + strTASK_ID + "成功");
        }
        return isSuccess == true ? "1" : "0";
    }
}