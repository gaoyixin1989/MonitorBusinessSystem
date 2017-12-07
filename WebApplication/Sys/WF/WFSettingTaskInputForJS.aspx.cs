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
/// 修改时间：2013-01-09
/// 修改人  ：潘德军
/// </summary>
public partial class Sys_WF_WFSettingTaskInputForJS : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }

        //获取信息
        if (Request["type"] != null && Request["type"].ToString() == "getData")
        {
            string strID = Request.QueryString[TWfSettingTaskVo.ID_FIELD];
            string strWFID = Request.QueryString[TWfSettingTaskVo.WF_ID_FIELD];
            //把ID转换为WF_ID
            if (string.IsNullOrEmpty(strWFID))
                strWFID = new TWfSettingFlowLogic().Details(strID).WF_ID;

            getData(strWFID);
        }
    }

    #region 获取信息
    //获取信息
    private void getData(string strWFID)
    {
        string strSortname = TWfSettingTaskVo.TASK_ORDER_FIELD;
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        TWfSettingTaskLogic logic = new TWfSettingTaskLogic();
        TWfSettingTaskVo tv = new TWfSettingTaskVo();
        tv.WF_ID = strWFID;
        tv.SORT_FIELD = strSortname;
        tv.SORT_TYPE = strSortorder;
        DataTable dt = logic.SelectByTable(tv, intPageIndex, intPageSize);
        int intTotalCount = logic.GetSelectResultCount(tv);
        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
        Response.End();
    }
    #endregion

    #region 保存环节
    //保存环节
    [WebMethod]
    public static string SaveData(string strid, string stWF_ID, string strWF_TASK_ID, string strTASK_CAPTION, string strTASK_NOTE, string strCOMMAND_NAME, string strCOMMAND_NAME_Text,
        string strFUNCTION_LIST,string strTASK_AND_OR, string strPOSITION_IX, string strPOSITION_IY, string strOPER_VALUE)
    {
        bool isSuccess = true;

        //构建节点记录
        TWfSettingTaskVo setStep = new TWfSettingTaskVo();
        setStep.WF_ID = stWF_ID;
        setStep.WF_TASK_ID = strWF_TASK_ID == "0" ? new PageBaseForWF().GetGUID() : strWF_TASK_ID;
        setStep.ID = strid == "0" ? setStep.WF_TASK_ID : strid;

        setStep.TASK_CAPTION = strTASK_CAPTION;
        setStep.TASK_NOTE = strTASK_NOTE;
        setStep.COMMAND_NAME = strCOMMAND_NAME.Replace(";","|");
        setStep.FUNCTION_LIST = strFUNCTION_LIST.Length == 0 ? "###" : strFUNCTION_LIST.Replace(";", "|");
        setStep.TASK_AND_OR = strTASK_AND_OR;
        setStep.TASK_TYPE = "01";//暂时指定为01，后续扩展
        setStep.OPER_TYPE = "01";//暂时指定为用户，屏蔽用户类型“职位”，因为工作流设计无法适应用户、职位同时选的情况
        setStep.OPER_VALUE = strOPER_VALUE.Replace(";", "|");

        //构建节点命令集合
        List<TWfSettingTaskCmdVo> cmdSetpList = new List<TWfSettingTaskCmdVo>();
        string[] arrCMDName = strCOMMAND_NAME.Split(';');
        string[] arrCMDNameText = strCOMMAND_NAME_Text.Split(';');
        for (int i = 0; i < arrCMDName.Length; i++)
        {
            TWfSettingTaskCmdVo cmdTemp = new TWfSettingTaskCmdVo();
            cmdTemp.ID = new PageBaseForWF().GetGUID();
            cmdTemp.WF_CMD_ID = cmdTemp.ID;
            cmdTemp.WF_ID = stWF_ID;
            cmdTemp.WF_TASK_ID = setStep.WF_TASK_ID;
            cmdTemp.CMD_NAME = arrCMDName[i];
            cmdTemp.CMD_NOTE = arrCMDNameText[i];
            cmdSetpList.Add(cmdTemp);
        }

        //构建节点表单页面记录
        TWfSettingTaskFormVo formStep = new TWfSettingTaskFormVo();
        formStep.WF_ID = setStep.WF_ID;
        formStep.WF_TASK_ID = setStep.WF_TASK_ID;
        formStep.ID = new PageBaseForWF().GetGUID();
        formStep.WF_TF_ID = formStep.ID;
        formStep.UCM_ID = strPOSITION_IY;
        formStep.UCM_TYPE = strPOSITION_IX;

        TWfSettingTaskLogic taskLogic = new TWfSettingTaskLogic();
        TWfSettingTaskCmdLogic cmdLogic = new TWfSettingTaskCmdLogic();
        TWfSettingTaskFormLogic formLogic = new TWfSettingTaskFormLogic();
        if (strid == "0")
        {
            //新增是构建 TASK_ORDER
            setStep.TASK_ORDER = (taskLogic.GetSelectResultCount(new TWfSettingTaskVo() { WF_ID = setStep.WF_ID }) + 1).ToString();

            bool bIsSucess = taskLogic.Create(setStep);
            if (bIsSucess)
            {
                cmdLogic.Create(cmdSetpList);
                formLogic.Create(formStep);
            }
            string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "增加环节:" + setStep.WF_TASK_ID + " 成功";
            new PageBase().WriteLog("增加环节", "", strMessage);
        }
        else
        {
            bool bIsSucess = taskLogic.Edit(setStep);
            if (bIsSucess)
            {
                cmdLogic.Delete(new TWfSettingTaskCmdVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
                cmdLogic.Create(cmdSetpList);
                formLogic.Delete(new TWfSettingTaskFormVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
                formLogic.Create(formStep);
            }
            string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "修改环节:" + setStep.WF_TASK_ID + " 成功";
            new PageBase().WriteLog("修改环节", "", strMessage);
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
    #endregion

    #region 删除环节
    // 删除环节
    [WebMethod]
    public static string deleteData(string strValue)
    {
        TWfSettingTaskVo setStep = new TWfSettingTaskLogic().Details(strValue);
        TWfSettingTaskLogic taskLogic = new TWfSettingTaskLogic();
        TWfSettingTaskCmdLogic cmdLogic = new TWfSettingTaskCmdLogic();
        TWfSettingTaskFormLogic formLogic = new TWfSettingTaskFormLogic();
        bool bIsSucess = taskLogic.Delete(strValue);
        if (bIsSucess)
        {
            cmdLogic.Delete(new TWfSettingTaskCmdVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
            formLogic.Delete(new TWfSettingTaskFormVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
        }
        string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "删除环节:" + setStep.WF_TASK_ID + " 成功";
        new PageBase().WriteLog("删除环节", "", strMessage);

        return bIsSucess == true ? "1" : "0";
    }
    #endregion

    #region 排序
    // 提前一步
    [WebMethod]
    public static string upData(string strID, string strWFID)
    {
        bool bIsSucess = false;
        TWfSettingTaskLogic logic = new TWfSettingTaskLogic();

        TWfSettingTaskVo tv = new TWfSettingTaskVo();
        tv.WF_ID = strWFID;
        tv.SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD;
        tv.SORT_TYPE = " ASC ";
        DataTable GetStepListTable = logic.SelectByTable(tv);

        for (int i = 0; i < GetStepListTable.Rows.Count; i++)
        {
            if (GetStepListTable.Rows[i][TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strID)
            {
                //开始排序
                if (i == 0)
                    return "1";
                string strOrderFlag1 = GetStepListTable.Rows[i][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                string strOrderFlag2 = GetStepListTable.Rows[i - 1][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                string strID1 = GetStepListTable.Rows[i][TWfSettingTaskVo.ID_FIELD].ToString();
                string strID2 = GetStepListTable.Rows[i - 1][TWfSettingTaskVo.ID_FIELD].ToString();
                TWfSettingTaskVo temp1 = new TWfSettingTaskVo() { ID = strID1, TASK_ORDER = strOrderFlag2 };
                TWfSettingTaskVo temp2 = new TWfSettingTaskVo() { ID = strID2, TASK_ORDER = strOrderFlag1 };
                TWfSettingTaskLogic tempLogic = new TWfSettingTaskLogic();
                bIsSucess=tempLogic.Edit(temp1);
                if (bIsSucess)
                    bIsSucess = tempLogic.Edit(temp2);

                string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "调整环节顺序:" + strID1 + " 成功";
                new PageBase().WriteLog("调整环节顺序", "", strMessage);
            }
        }

        return bIsSucess == true ? "1" : "0";
    }

    // 后退一步
    [WebMethod]
    public static string downData(string strID, string strWFID)
    {
        bool bIsSucess = false;
        TWfSettingTaskLogic logic = new TWfSettingTaskLogic();

        TWfSettingTaskVo tv = new TWfSettingTaskVo();
        tv.WF_ID = strWFID;
        tv.SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD;
        tv.SORT_TYPE = " ASC ";
        DataTable GetStepListTable = logic.SelectByTable(tv);

        for (int i = 0; i < GetStepListTable.Rows.Count; i++)
        {
            if (GetStepListTable.Rows[i][TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strID)
            {
                //开始排序
                //开始排序
                if (i == GetStepListTable.Rows.Count - 1)
                    return "1";
                string strOrderFlag1 = GetStepListTable.Rows[i][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                string strOrderFlag2 = GetStepListTable.Rows[i + 1][TWfSettingTaskVo.TASK_ORDER_FIELD].ToString();
                string strID1 = GetStepListTable.Rows[i][TWfSettingTaskVo.ID_FIELD].ToString();
                string strID2 = GetStepListTable.Rows[i + 1][TWfSettingTaskVo.ID_FIELD].ToString();
                TWfSettingTaskVo temp1 = new TWfSettingTaskVo() { ID = strID1, TASK_ORDER = strOrderFlag2 };
                TWfSettingTaskVo temp2 = new TWfSettingTaskVo() { ID = strID2, TASK_ORDER = strOrderFlag1 };
                TWfSettingTaskLogic tempLogic = new TWfSettingTaskLogic();
                bIsSucess = tempLogic.Edit(temp1);
                if (bIsSucess)
                    bIsSucess = tempLogic.Edit(temp2);

                string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "调整环节顺序:" + strID1 + " 成功";
                new PageBase().WriteLog("调整环节顺序", "", strMessage);
            }
        }

        return bIsSucess == true ? "1" : "0";
    }
    #endregion

    #region 获取信息
    /// <summary>
    /// 获取流程名称
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetWFName(string strValue)
    {
        string strWfName = "";
        DataTable dtWF = new TWfSettingFlowLogic().SelectByTable(new TWfSettingFlowVo());

        foreach (DataRow dr in dtWF.Rows)
            if (dr[TWfSettingFlowVo.WF_ID_FIELD].ToString().Trim().ToUpper() == strValue.ToUpper())
                strWfName = dr[TWfSettingFlowVo.WF_CAPTION_FIELD].ToString();

        return strWfName;
    }

    /// <summary>
    /// 获取命令集
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetCMDName(string strValue)
    {
        string[] strList = strValue.Split('|');
        string strText = "";
        foreach (string strTemp in strList)
        {
            if (string.IsNullOrEmpty(strTemp))
                continue;
            strText += new PageBaseForWF().GetCMDNameFromCode(strTemp);
            strText += "|";
        }
      
        return strText.EndsWith("|") ? strText.Substring(0, strText.Length - 1) : strText;
    }

    /// <summary>
    /// 获取附加功能
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetFUNCName(string strValue)
    {
        string[] strList = strValue.Split('|');
        string strText = "";
        foreach (string strTemp in strList)
        {
            if (string.IsNullOrEmpty(strTemp))
                continue;
            strText += new PageBaseForWF().GetFUNCTIONNameFromCode(strTemp);
            strText += "|";
        }
        
        return strText.EndsWith("|") ? strText.Substring(0, strText.Length - 1) : strText;
    }
    #endregion
}