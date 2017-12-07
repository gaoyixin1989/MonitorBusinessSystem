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

/// <summary>
/// 功能描述：工作流管理
/// 创建日期：2012-11-08
/// 创建人  ：石磊
/// 修改说明：改为ligerui
/// 修改时间：2013-01-07
/// 修改人  ：潘德军
/// </summary>
public partial class Sys_WF_WFSettingFlowListForJS : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }

        //获取信息
        if (Request["type"] != null && Request["type"].ToString() == "getData")
        {
            getData();
        }
    }

    //获取信息
    private void getData()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TWfSettingFlowLogic logic = new TWfSettingFlowLogic();
        TWfSettingFlowVo setting = new TWfSettingFlowVo();
        setting.WF_STATE = "1";
        setting.SORT_FIELD = strSortname;
        setting.SORT_TYPE = strSortorder;
        DataTable dt = logic.SelectByTable(setting,  intPageIndex, intPageSize);
        int intTotalCount = logic.GetSelectResultCount(setting);
        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }

    // 删除信息
    [WebMethod]
    public static string deleteData(string strValue)
    {
        bool isSuccess = true;

        TWfSettingFlowVo objFolw = new TWfSettingFlowLogic().Details(strValue);

        //有实例，不可删除
        int intInstCountForFlow = new TWfInstControlLogic().GetSelectResultCount(new TWfInstControlVo() { WF_ID = objFolw.WF_ID });
        if (intInstCountForFlow > 0)
        {
            return "2";
        }

        new TWfSettingTaskCmdLogic().Delete(new TWfSettingTaskCmdVo() { WF_ID = objFolw.WF_ID });
        new TWfSettingTaskFormLogic().Delete(new TWfSettingTaskFormVo() { WF_ID = objFolw.WF_ID });
        new TWfSettingTaskLogic().Delete(new TWfSettingTaskVo() { WF_ID = objFolw.WF_ID });

        bool bIsSucess = new TWfSettingFlowLogic().Delete(strValue);
        
        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 获取流程分类信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetClassName(string strValue)
    {
        string strWfName = "";
        DataTable dtWF = new TWfSettingBelongsLogic().SelectByTable(new TWfSettingBelongsVo());

        foreach (DataRow dr in dtWF.Rows)
            if (dr[TWfSettingBelongsVo.WF_CLASS_ID_FIELD].ToString().Trim().ToUpper() == strValue.ToUpper())
                strWfName = dr[TWfSettingBelongsVo.WF_CLASS_NAME_FIELD].ToString();

        return strWfName;
    }

    //保存数据
    [WebMethod]
    public static string SaveData(string strid, string strWF_CAPTION, string strWF_ID, string strWF_CLASS_ID, string strFSTEP_RETURN_URL,string strWF_NOTE)
    {
        bool isSuccess = true;

        TWfSettingFlowVo twfsfv = new TWfSettingFlowVo();
        
        twfsfv.WF_CAPTION = strWF_CAPTION;
        twfsfv.WF_ID = strWF_ID;
        twfsfv.WF_CLASS_ID = strWF_CLASS_ID;
        twfsfv.FSTEP_RETURN_URL = strFSTEP_RETURN_URL;
        twfsfv.WF_NOTE = strWF_NOTE;
        
        if (strid != "0")
        {
            twfsfv.ID = strid;

            twfsfv.DEAL_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            twfsfv.DEAL_TYPE = "1";
            twfsfv.DEAL_USER = new PageBase().LogInfo.UserInfo.ID;

            isSuccess = new TWfSettingFlowLogic().Edit(twfsfv);

            if (isSuccess)
            {
                new PageBase().WriteLog("编辑流程", "", new PageBase().LogInfo.UserInfo.USER_NAME + "编辑流程" + strid + "成功");
            }
        }
        else
        {
            long i = 1;
            Guid guid = Guid.NewGuid();
            foreach (byte b in guid.ToByteArray())
                i *= ((int)b + 1);
            twfsfv.ID = string.Format("{0:x}", i - DateTime.Now.Ticks).ToUpper();

            twfsfv.WF_VERSION = WF_VERSION_FIELD;
            twfsfv.WF_STATE = "1";
            twfsfv.WF_FORM_MAIN = "";
            twfsfv.CREATE_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            twfsfv.CREATE_USER = new PageBase().LogInfo.UserInfo.ID;

            isSuccess = new TWfSettingFlowLogic().Create(twfsfv);

            if (isSuccess)
            {
                new PageBase().WriteLog("新增流程", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增流程" + strid + "成功");
            }
        }

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }
}