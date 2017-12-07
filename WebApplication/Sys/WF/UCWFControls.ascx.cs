using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using System.Collections;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;

using i3.ValueObject.Channels.OA.Message;
using i3.BusinessLogic.Channels.OA.Message;
using System.Configuration;

public partial class Sys_WF_UCWFControls : System.Web.UI.UserControl
{

    #region 基础获取ID和时间的方法
    /// <summary>
    /// 获得GUID编号
    /// </summary>
    /// <returns></returns>
    private string GetGUID()
    {
        long i = 1;
        Guid guid = Guid.NewGuid();
        foreach (byte b in guid.ToByteArray())
            i *= ((int)b + 1);
        return string.Format("{0:x}", i - DateTime.Now.Ticks).ToUpper();
    }
    /// <summary>
    /// 获得标准时间格式字符串
    /// </summary>
    /// <returns></returns>
    private string GetDateTimeToStanString()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
    /// <summary>
    /// 获得20121105182451098的时间形式字符
    /// </summary>
    /// <returns></returns>
    private string GetDateTimeToStringFor17()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString().PadLeft(3, '0');
    }

    /// <summary>
    /// 获得121105182451098的时间形式字符
    /// </summary>
    /// <returns></returns>
    private string GetDateTimeToStringFor15()
    {
        return DateTime.Now.ToString("yyMMddHHmmss") + DateTime.Now.Millisecond.ToString().PadLeft(3, '0');
    }

    /// <summary>
    /// 指定用GUID方式或者时间方式来生成编号，0为16位GUID，1为15位时间编码,2为17位时间编码，其他情况返回""
    /// </summary>
    /// <param name="strType">输入类别</param>
    /// <returns></returns>
    public string CreatInstFlowID(string strType)
    {
        if (strType == "0")
            return this.GetGUID();
        else if (strType == "1")
            return this.GetDateTimeToStringFor15();
        else if (strType == "2")
            return this.GetDateTimeToStringFor17();
        return "";
    }


    #endregion

    #region 流程操作日志记录
    /// <summary>
    /// 流程操作记录页面，只限于此控件使用
    /// </summary>
    /// <param name="strUserName">用户名，登陆名</param>
    /// <param name="strInstWFID">实例流程编号</param>
    /// <param name="strInstTaskID">实例环节编号</param>
    /// <param name="strWFID">流程编号</param>
    /// <param name="strTaskID">环节编号</param>
    /// <param name="strOper">操作人</param>
    /// <param name="strOpAction">操作类型</param>
    /// <param name="strOpNote">操作内容记录</param>
    /// <param name="strAgentName">被代理的人</param>
    public void WriteWFOpLog(string strUserName,
        string strInstWFID,
        string strInstTaskID,
        string strWFID,
        string strTaskID,
        string strOper,
        string strOpAction,
        string strOpNote,
        string strAgentName)
    {
        TWfInstOpLogVo tLog = new TWfInstOpLogVo();
        tLog.ID = this.GetGUID();
        tLog.IS_AGENT = string.IsNullOrEmpty(strAgentName) ? "0" : "1";
        tLog.AGENT_USER = strAgentName;
        tLog.OP_ACTION = strOpAction;
        tLog.OP_NOTE = strOpNote;
        tLog.OP_TIME = this.GetDateTimeToStanString();
        tLog.OP_USER = strOper;
        tLog.WF_ID = strWFID;
        tLog.WF_TASK_ID = strTaskID;
        tLog.WF_INST_ID = strInstWFID;
        tLog.WF_INST_TASK_ID = strInstTaskID;

        new TWfInstOpLogLogic().Create(tLog);
    }

    #endregion

    #region 数据寄存区域

    /// <summary>
    /// 业务系统启动工作流时必须先初始化的流程编号
    /// </summary>
    public string START_PAGE_WFID
    {
        get
        {
            string strValue = ViewState["START_PAGE_WFID"] as string;
            if (string.IsNullOrEmpty(strValue)) return ""; else return strValue;
        }
        set { ViewState["START_PAGE_WFID"] = value; }
    }

    /// <summary>
    /// 业务系统启动工作流时必须先初始化的初始环节编号【初始环节编号在基类中有方法调用】
    /// </summary>
    public string START_PAGE_STEPID
    {
        get
        {
            string strValue = ViewState["START_PAGE_STEPID"] as string;
            if (string.IsNullOrEmpty(strValue)) return ""; else return strValue;
        }
        set { ViewState["START_PAGE_STEPID"] = value; }
    }


    /// <summary>
    /// 环节实例编号寄存
    /// </summary>
    public string WF_INST_TASK_ID
    {
        get { return ViewState[TWfInstTaskOpinionsVo.WF_INST_TASK_ID_FIELD] as string; }
        set { ViewState[TWfInstTaskOpinionsVo.WF_INST_TASK_ID_FIELD] = value; }
    }
    /// <summary>
    /// 流程实例编号寄存
    /// </summary>
    public string WF_INST_ID
    {
        get { return ViewState[TWfInstTaskOpinionsVo.WF_INST_ID_FIELD] as string; }
        set { ViewState[TWfInstTaskOpinionsVo.WF_INST_ID_FIELD] = value; }
    }
    /// <summary>
    /// 流程编号寄存
    /// </summary>
    public string WF_ID
    {
        get { return ViewState[TWfSettingTaskCmdVo.WF_ID_FIELD] as string; }
        set { ViewState[TWfSettingTaskCmdVo.WF_ID_FIELD] = value; this.hiddWF_ID.Value = value; }
    }

    /// <summary>
    /// 环节编号寄存
    /// </summary>
    public string TASK_ID
    {
        get { return ViewState[TWfSettingTaskCmdVo.WF_TASK_ID_FIELD] as string; }
        set { ViewState[TWfSettingTaskCmdVo.WF_TASK_ID_FIELD] = value; }
    }

    /// <summary>
    /// 是否是起始节点 由业务系统传入的参数限制
    /// </summary>
    public bool IS_WF_START
    {
        get { return null == ViewState["IS_WF_START"] ? false : (bool)ViewState["IS_WF_START"]; }
        set { ViewState["IS_WF_START"] = value; }
    }

    /// <summary>
    /// 是否是结束节点 由本页面自己计算得出
    /// </summary>
    public bool IS_WF_END_STEP
    {
        get { return null == ViewState["IS_WF_END_STEP"] ? false : (bool)ViewState["IS_WF_END_STEP"]; }
        set { ViewState["IS_WF_END_STEP"] = value; }
    }
    /// <summary>
    /// 是否已保存页面
    /// </summary>
    public bool IS_PAGE_SAVE
    {
        get { return null == ViewState["IS_PAGE_SAVE"] ? false : (bool)ViewState["IS_PAGE_SAVE"]; }
        set { ViewState["IS_PAGE_SAVE"] = value; }
    }


    /// <summary>
    /// 错误的提示信息寄存
    /// </summary>
    public string ERROR_TIPS
    {
        get { return ViewState["ERROR_TIPS"] as string; }
        set { ViewState["ERROR_TIPS"] = value; }
    }

    /// <summary>
    /// 寄存的流程实例控制数据
    /// </summary>
    public TWfInstControlVo INST_WF_CONTROL
    {
        get { return Session["INST_WF_CONTROL"] as TWfInstControlVo; }
        set { Session["INST_WF_CONTROL"] = value; }
    }

    /// <summary>
    /// 寄存的流程环节实例详细数据
    /// </summary>
    public TWfInstTaskDetailVo INST_STEP_DETAIL
    {
        get { return Session["INST_STEP_DETAIL"] as TWfInstTaskDetailVo; }
        set { Session["INST_STEP_DETAIL"] = value; }
    }

    /// <summary>
    /// 【下环节寄存的数据】寄存和业务系统交互的数据，主要指业务主键，ID等内容
    /// </summary>
    public List<TWfInstTaskServiceVo> INST_STEP_SERVICE_LIST
    {
        get { return Session["INST_STEP_SERVICE_LIST"] as List<TWfInstTaskServiceVo>; }
        set { Session["INST_STEP_SERVICE_LIST"] = value; }
    }

    /// <summary>
    /// 【上环节获取的信息】寄存和业务系统交互的数据，主要指业务主键，ID等内容
    /// </summary>
    public List<TWfInstTaskServiceVo> INST_STEP_SERVICE_LIST_FOR_OLD
    {
        get { return Session["INST_STEP_SERVICE_LIST_FOR_OLD"] as List<TWfInstTaskServiceVo>; }
        set { Session["INST_STEP_SERVICE_LIST_FOR_OLD"] = value; }
    }

    /// <summary>
    ///  设置业务编码
    /// </summary>
    public string ServiceCode
    {
        get
        {
            if (null == INST_WF_CONTROL)
                return "";
            else
                return INST_WF_CONTROL.WF_SERVICE_CODE;
        }
        set
        {
            if (null == INST_WF_CONTROL)
                INST_WF_CONTROL = new TWfInstControlVo();
            INST_WF_CONTROL.WF_SERVICE_CODE = value;
        }
    }

    /// <summary>
    ///  设置业务名称
    /// </summary>
    public string ServiceName
    {
        get
        {
            if (null == INST_WF_CONTROL)
                return "";
            else
                return INST_WF_CONTROL.WF_SERVICE_NAME;
        }
        set
        {
            if (null == INST_WF_CONTROL)
                INST_WF_CONTROL = new TWfInstControlVo();
            INST_WF_CONTROL.WF_SERVICE_NAME = value;
        }
    }

    /// <summary>
    /// 是否是路由节点
    /// </summary>
    private bool IsAndOrStep
    {
        get
        {
            if (null == ViewState["IS_AND_OR_STEP"])
                return false;
            else
                return bool.Parse(ViewState["IS_AND_OR_STEP"].ToString());
        }
        set { ViewState["IS_AND_OR_STEP"] = value; }
    }

    /// <summary>
    /// 设置工作流采用多用户发送模式，此模式启用后，单用户模式自动关闭，此标记只需打开一次即可
    /// 一般此方法和SetMoreDealUserForAdd方法联合使用；
    /// </summary>
    public bool SetMoreDealUserFlag
    {
        get
        {
            if (null == ViewState["MORE_DEAL_USER_FLAG"])
                return false;
            else
                return bool.Parse(ViewState["MORE_DEAL_USER_FLAG"].ToString());
        }
        set { ViewState["MORE_DEAL_USER_FLAG"] = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    private List<string> MoreDealUserList
    {
        get
        {
            if (null == Session["MORE_DEAL_USER_LIST"])
                return new List<string>();
            else
                return ((List<string>)(Session["MORE_DEAL_USER_LIST"]));
        }
        set { Session["MORE_DEAL_USER_LIST"] = value; }
    }


    #endregion

    #region 流程开始时载入的初始化方法


    /// <summary>
    /// 根据流程简码和环节编号可判定是否显示某些内容，例如：评论，选人，附件，以及功能按钮
    /// </summary>
    /// <param name="strFlowID">使用的流程简称</param>
    /// <param name="strStepID">使用的环节编号</param>
    /// <param name="strInstFlowID">当前的流程序号</param>
    /// <param name="strInstTaskID">当前的环节编号</param>
    public void InitDivViewAndTaskData(string strFlowID, string strStepID, string strInstFlowID, string strInstTaskID)
    {

        TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strStepID, WF_ID = strFlowID });
        string strCommandList = task.COMMAND_NAME;
        string strFunctionLists = task.FUNCTION_LIST;
        #region 按钮显示
        //如果是开始环节，可以支持保存功能，其他任何环节暂不支持保存功能。
        //如果是最后环节，则可以把“发送”改为“完成”
        string[] strCmdList = strCommandList.Split('|');
        foreach (string strTemp in strCmdList)
        {
            switch (strTemp)
            {
                case "00":
                    btnBack.Visible = true;
                    break;
                case "01":
                    btnSend.Visible = true;
                    break;
                case "02":
                    btnZSend.Visible = true;
                    break;
                case "03":
                    btnHold.Visible = true;
                    break;
                case "04":
                    btnReLoad.Visible = true;
                    break;
                case "05":
                    btnPause.Visible = true;
                    break;
                case "06":
                    btnReStart.Visible = true;
                    break;
                case "07":
                    btnCallBack.Visible = true;
                    break;
                case "09":
                    btnKill.Visible = true;
                    break;
                case "10":
                    btnSave.Visible = true;
                    break;
                default:
                    break;
            }
        }
        #endregion
        //跳转功能不属于按钮和不属于附加功能 暂定义为附加功能
        if (strCommandList.IndexOf("08") > -1)
        {
            //初始化跳转后的环节的内容
            List<TWfSettingTaskVo> twft = new TWfSettingTaskLogic().SelectByObjectList(new TWfSettingTaskVo() { WF_ID = strFlowID });
            int iIndexCurrent = int.Parse(task.TASK_ORDER);
            foreach (TWfSettingTaskVo twTemp in twft)
            {
                int iIndex1 = int.Parse(twTemp.TASK_ORDER);
                if (iIndex1 > iIndexCurrent + 1)
                {
                    ListItem liTemp = new ListItem(twTemp.TASK_CAPTION, twTemp.WF_TASK_ID);
                    STEP_NAME.Items.Add(liTemp);
                }
            }
            //获取Ajax访问URL
            CreateAjaxDropUrl();
            if (STEP_NAME.Items.Count > 0)
            {
                //附带测试GetNextStep方法；
                string strNextTaskID = new TWfSettingTaskLogic().GetNextStep(new TWfSettingTaskVo() { WF_TASK_ID = TASK_ID, WF_ID = strFlowID }).WF_TASK_ID;
                STEP_NAME.Items.Insert(0, new ListItem("下一环节", strNextTaskID));
                dvGoToStep.Visible = true;
            }

        }


        //如果存在评论信息则改变“更多评论”的显示颜色 Add By：weilin
        TWfInstTaskOpinionsVo InstTaskOpinionsVo = new TWfInstTaskOpinionsVo();
        InstTaskOpinionsVo.WF_INST_ID = strInstFlowID;
        if (new TWfInstTaskOpinionsLogic().GetSelectResultCount(InstTaskOpinionsVo) > 0)
            hidColor.Value = "red";


        #region 附加功能显示
        string[] strFuncList = strFunctionLists.Split('|');
        foreach (string strTemp in strFuncList)
        {
            if (strTemp == TWfCommDict.FunctionList.ForUser)
            {
                dvOpUsers.Visible = true;
                continue;
            }
            else if (strTemp == TWfCommDict.FunctionList.ForFile)
            {
                dvUpLoad.Visible = true;
                continue;
            }
            else if (strTemp == TWfCommDict.FunctionList.ForOpinion)
            {
                dvOpinions.Visible = true;
                //显示的同时，载入评论意见


                continue;
            }
        }

        #endregion

        #region 设定的用户信息显示
        //这里需要设定下一个节点的处理人员

        TWfSettingTaskVo nextStep = new TWfSettingTaskLogic().GetNextStep(task);
        string strUserList = "";
        DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(new TSysUserPostVo());
        DataTable dtUserInfo = new TSysUserLogic().SelectByTable(new TSysUserVo() { IS_DEL = "0", IS_USE = "1" });
        //增加限定处理人员信息，先将以职位存储的数据转换为系统用户
        if (nextStep.OPER_TYPE == "02")
        {
            foreach (string strPost in nextStep.OPER_VALUE.Split('|'))
                foreach (DataRow dr in dtUserPost.Rows)
                    if (dr[TSysUserPostVo.POST_ID_FIELD].ToString() == strPost)
                        if (strUserList.IndexOf(dr[TSysUserPostVo.USER_ID_FIELD].ToString()) < 0)
                        {
                            strUserList += dr[TSysUserPostVo.USER_ID_FIELD].ToString() + "|";
                            continue;
                        }
        }
        else
            strUserList = nextStep.OPER_VALUE;
        //-------------------------------------------------------------------------
        //开始处理用户显示信息,将一串用户ID，转换为checkboxlist上显示的文字
        ShowUserNameFromIDList(dtUserInfo, chkbxlstSelectUsers, strUserList);
        //将所有用户都放到界面上
        ShowUserNameFromDB(dtUserInfo, chkbxlstAllUsers);
        //默认选中第一个
        chkbxlstSelectUsers.SelectedIndex = chkbxlstSelectUsers.Items.Count > 0 ? 0 : -1;
        //有直接上级就默认选择直接上级
        if (chkbxlstSelectUsers.Items.Count > 0)
        {
            string strLocalUserID = (this.Page as PageBaseForWF).LogInfo.UserInfo.ID;
            DataTable dtDeptAdmin = new TSysUserPostLogic().SelectDeptAdmin_byTable(strLocalUserID);
            if (dtDeptAdmin.Rows.Count > 0)
            {
                string strDeptAdminId = dtDeptAdmin.Rows[0]["user_id"].ToString();
                for (int i = 0; i < chkbxlstSelectUsers.Items.Count; i++)
                {
                    if (chkbxlstSelectUsers.Items[i].Value == strDeptAdminId)
                    {
                        chkbxlstSelectUsers.SelectedIndex = i;
                    }
                }
            }
        }
        //如果chkbxlstSelectUsers没有人员，则不用考虑，直接启用chkbxlstAllUsers
        chkbxlstAllUsers.Enabled = chkbxlstSelectUsers.Items.Count == 0 ? true : false;
        if (chkbxlstAllUsers.Enabled)
            chkbxlstAllUsers.SelectedIndex = chkbxlstAllUsers.Items.Count > 0 ? 0 : -1;
        #endregion
    }

    /// <summary>
    /// 获取Ajax访问URL 胡方扬 2013-02-25
    /// </summary>
    private void CreateAjaxDropUrl()
    {
        string strReturnUrl = this.Request.Url.ToString().ToUpper();
        string strVUrl = this.Request.AppRelativeCurrentExecutionFilePath.ToString().ToUpper().Replace("~/", "");
        strReturnUrl = strReturnUrl.Substring(0, strReturnUrl.IndexOf(strVUrl));
        string strAjaxUrl = strReturnUrl + "Sys/WF/WFPageHandler.ashx";
        this.hiddAjaxUrl.Value = strAjaxUrl;
    }
    /// <summary>
    /// 选择跳转后，根据跳转到的环节，进行环节数据重新绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void STEP_NAME_SelectedChanged(object sender, EventArgs e)
    //{
    //    this.STEP_NAME.Attributes.Add("onchange", "change_event()");
    //}

    /// <summary>
    /// 在指定的数据表格中检索，将以一串用户ID变换成名字，放进选择框中
    /// </summary>
    /// <param name="dtUserInfo">指定的用户数据表格</param>
    /// <param name="lc">listcontrol类的控件都可以</param>
    /// <param name="strUserIDList">用户ID的组合</param>
    public void ShowUserNameFromIDList(DataTable dtUserInfo, ListControl lc, string strUserIDList)
    {
        lc.Items.Clear();
        foreach (string strTemp in strUserIDList.Split('|'))
        {
            foreach (DataRow dr in dtUserInfo.Rows)
            {
                if (dr[TSysUserVo.ID_FIELD].ToString() == strTemp)
                {
                    ListItem li = new ListItem(dr[TSysUserVo.REAL_NAME_FIELD].ToString(), dr[TSysUserVo.ID_FIELD].ToString());
                    lc.Items.Add(li);
                    continue;
                }
            }
        }
        
    }

    /// <summary>
    /// 在指定的数据表格中检索，取得所有可用的用户数据，放进选择框中
    /// </summary>
    /// <param name="lc">listcontrol类的控件都可以</param>
    public void ShowUserNameFromDB(DataTable dtUserInfo, ListControl lc)
    {
        foreach (DataRow dr in dtUserInfo.Rows)
        {
            ListItem li = new ListItem(dr[TSysUserVo.REAL_NAME_FIELD].ToString(), dr[TSysUserVo.ID_FIELD].ToString());
            lc.Items.Add(li);
        }
    }

    #endregion

    /// <summary>
    /// 第一步：初始化流程相关内容,且检验流程是否应该在此页面启动
    /// </summary>
    /// <param name="strFlowID">流程编号</param>
    /// <param name="strStepID">环节编号</param>
    /// <param name="strInstFlowID">实例流程编号</param>
    /// <param name="strInstTaskID">实例环节编号</param>
    /// <param name="bIsStartWF">是否流程的起始环节</param>
    /// <returns>页面启动是否和本流程启动页面匹配，如不匹配则由页面处理</returns>
    public bool LoadWFData(string strFlowID, string strStepID, string strInstFlowID, string strInstTaskID, bool bIsStartWF)
    {
        WF_ID = strFlowID;
        TASK_ID = strStepID;
        IS_WF_START = bIsStartWF;
        if (string.IsNullOrEmpty(strInstFlowID))
            WF_INST_ID = this.GetGUID();
        if (string.IsNullOrEmpty(strInstTaskID))
            WF_INST_TASK_ID = this.GetGUID();
        //看看流程配置时第一个页面是否是这个页面，如果不是直接返回false
        TWfSettingTaskFormVo task = new TWfSettingTaskFormLogic().Details(new TWfSettingTaskFormVo() { WF_ID = WF_ID, WF_TASK_ID = TASK_ID });
        try
        {
            string strUrl = this.Page.Request.Url.ToString().ToUpper();
            string strUCPages = task.UCM_ID.Replace("~/", "").ToUpper();
            string strUrlPages = strUrl.Substring(strUrl.LastIndexOf("/") + 1, strUrl.LastIndexOf("?") - strUrl.LastIndexOf("/") - 1);
            if (task.UCM_TYPE == TWfCommDict.FormType.ForPage && strUCPages.IndexOf(strUrlPages) == -1)
                return false;
        }
        catch { }
        return true;
    }

    /// <summary>
    /// 第二步：初始化相关页面信息，包括需要现实的，需要获取的所有资料
    /// </summary>
    public void LoadUserControls()
    {
        InitDivViewAndTaskData(WF_ID, TASK_ID, WF_INST_ID, WF_INST_TASK_ID);

        //判定是否为结束节点
        IS_WF_END_STEP = IsEndStep(this.WF_ID, this.TASK_ID);
    }

    /// <summary>
    /// 第三步：初始化业务数据，由业务系统完成，或者由业务系统自己调用也行
    /// </summary>
    public void LoadBusinessData()
    {
        //调用父页面载入和显示业务数据接口
        (this.Page as IWFStepRules).LoadAndViewBusinessData();
    }

    /// <summary>
    /// 第四步，创建新的工作流节点,或者载入本环节数据
    /// </summary>
    public void BuildNewStepDataOrLoadCurrentStepData()
    {
        //完善本节点处理的信息，然后生成下个节点的数据
        if (IS_WF_START)
        {
            //将流程设定信息调出来
            TWfSettingFlowVo wfConfig = new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { WF_ID = WF_ID });
            //起始环节的特殊判定
            TWfInstControlVo control = new TWfInstControlVo();
            control.ID = WF_INST_ID;
            //是否子流程的判断，暂时都确定为非子流程。后续扩展子流程的时候再扩展
            control.IS_SUB_FLOW = "0";
            control.WF_CAPTION = wfConfig.WF_CAPTION;
            control.WF_ID = WF_ID;
            control.WF_NOTE = wfConfig.WF_NOTE;
            control.WF_PRIORITY = TWfCommDict.WfPriority.Priority_1;
            control.WF_SERIAL_NO = CreatInstFlowID("1");
            control.WF_STARTTIME = GetDateTimeToStanString();
            control.WF_STATE = TWfCommDict.WfState.StateNormal; //默认状态都为正常
            control.WF_TASK_ID = WF_INST_TASK_ID;
            control.WF_SERVICE_CODE = ServiceCode;//增加ServiceCode的支持
            control.WF_SERVICE_NAME = ServiceName;//增加ServiceName的支持
            //寄存在页面内
            INST_WF_CONTROL = control;

            //然后构建StepDetail数据
            List<TWfSettingTaskVo> stConfigList = new TWfSettingTaskLogic().SelectByObject(new TWfSettingTaskVo() { WF_ID = WF_ID, SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD, SORT_TYPE = " asc " }, 0, 10);
            if (stConfigList.Count < 2)
            {
                ERROR_TIPS = "流程配置有问题，缺少必要的环节数据";
                return;
            }
            TWfSettingTaskVo stConfig = stConfigList[0];
            TWfInstTaskDetailVo detail = new TWfInstTaskDetailVo();
            detail.WF_TASK_ID = TASK_ID;
            detail.WF_SERIAL_NO = control.WF_SERIAL_NO;
            detail.WF_INST_ID = WF_INST_ID;
            detail.WF_ID = WF_ID;
            detail.OBJECT_USER = (this.Page as PageBaseForWF).LogInfo.UserInfo.ID;//增加节点的时候，肯定是当前用户
            detail.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
            detail.INST_TASK_STARTTIME = GetDateTimeToStanString();
            detail.INST_TASK_CAPTION = stConfig.TASK_CAPTION;
            detail.INST_NOTE = stConfig.TASK_NOTE;
            detail.ID = WF_INST_TASK_ID;
            //寄存在页面内
            INST_STEP_DETAIL = detail;
        }
        else
        {
            //正常环节的判定
            if (null == INST_WF_CONTROL || string.IsNullOrEmpty(INST_WF_CONTROL.ID))
                INST_WF_CONTROL = new TWfInstControlLogic().Details(WF_INST_ID);
            //INST_WF_CONTROL = new TWfInstControlLogic().Details(new TWfInstControlVo() { ID = WF_INST_ID });
            if (null == INST_STEP_DETAIL || string.IsNullOrEmpty(INST_STEP_DETAIL.ID))
                INST_STEP_DETAIL = new TWfInstTaskDetailLogic().Details(WF_INST_TASK_ID);
            //INST_STEP_DETAIL = new TWfInstTaskDetailLogic().Details(new TWfInstTaskDetailVo() { ID = WF_INST_TASK_ID });
            //加载普通环节的原有业务数据
            INST_STEP_SERVICE_LIST_FOR_OLD = new TWfInstTaskServiceLogic().SelectByObject(new TWfInstTaskServiceVo() { WF_INST_TASK_ID = INST_STEP_DETAIL.ID, WF_INST_ID = INST_STEP_DETAIL.WF_INST_ID }, 0, 100); 
            //如果当前环节是一个 AND节点，则需要判断此前节点
            TWfSettingTaskVo twtv1 = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { ID = INST_STEP_DETAIL.WF_TASK_ID, WF_ID = INST_STEP_DETAIL.WF_ID });
            if (twtv1.TASK_AND_OR == "1")
            {
                IsAndOrStep = true;
                //如果是合并节点，则要判断所有实例环节是否还有未完成的，提示仍有未处理数据，需等待处理；
                DataTable dtTemp1 = new TWfInstTaskDetailLogic().SelectByTableForAndOr(INST_WF_CONTROL.ID);
                int iOrderNum = int.Parse(twtv1.TASK_ORDER);
                foreach (DataRow dr in dtTemp1.Rows)
                {
                    if (int.Parse(dr[TWfSettingTaskVo.TASK_ORDER_FIELD].ToString().Trim()) < iOrderNum && dr[TWfInstTaskDetailVo.INST_TASK_STATE_FIELD].ToString().Trim() == TWfCommDict.StepState.StateNormal)
                    {
                        //已判断出来 在路由节点处理时，仍存在无处理完成的前置节点，所以本次无法执行操作，需要等待前置节点处理完成才允许处理
                        btnSend.Enabled = false;
                        (this.Page as PageBase).Alert("本任务仍存在未处理的前置环节，待前置环节完成后再执行本次操作");
                    }
                }
            }
            //----------------------合并节点处理完毕
        }
        //最后一个环节的判定
        if (IS_WF_END_STEP)
        {
            //最后一个环节,不能在选取任何人了,但附件和评论根据配置显示。
            dvOpUsers.Visible = false;
            //也不能显示 可选定操作人的提示信息
            dvToOpUsers.Visible = false;

            //最后一个环节的时候，发送按钮的文字需要变化
            btnSend.Text = "完成";

            foreach (ListItem li in chkbxlstSelectUsers.Items)
                li.Selected = false;
            chkbxlstSelectUsers.Enabled = false;
        }
    }

    /// <summary>
    /// 第五步，业务数据封装，进入缓存，后面会统一写入环节数据，以便于在下次载入的时候能恢复业务数据，有业务系统调用
    /// </summary>
    public bool BuildBusinessData()
    {
        string strMsg = "";
        //调用父页面重写的验证方法
        if ((this.Page as IWFStepRules).BuildAndValidateBusinessData(out strMsg))
        {
            //调用入库程序，注册业务数据
            (this.Page as IWFStepRules).CreatAndRegisterBusinessData();

        }
        else
        {
            ERROR_TIPS = "业务数据未完善:" + strMsg;
            return false;
        }
        string strMessage = (this.Page as PageBase).LogInfo.UserInfo.USER_NAME + "已验证环节:" + WF_INST_TASK_ID + " 数据成功";
        return true;
    }

    /// <summary>
    /// 重要的处理函数
    /// 更新数据和提交数据
    /// </summary>
    public void UpdateAndSendWFData()
    {
        //开始组装新数据结构，插入数据库，然后更新控制表和当前环节明细表，即可
        List<string> strUserList = new List<string>();

        //采用新的用户构建方法
        //2013-02-05提取
        if (SetMoreDealUserFlag)
            strUserList = MoreDealUserList;
        else
            strUserList = GetUserList();
        //------------------------------


        //最后一个环节不能加任何发送人，必须是裸体的
        if (strUserList.Count == 0 && !IS_WF_END_STEP)
        {
            ERROR_TIPS = "没有选择发送的对象，系统不予通过";
            return;
        }
        //人员找到了，可以找生成数据了
        TWfSettingTaskVo taskNext = new TWfSettingTaskLogic().GetNextStep(new TWfSettingTaskVo() { WF_ID = WF_ID, WF_TASK_ID = TASK_ID });
        List<TWfInstTaskDetailVo> detailList = new List<TWfInstTaskDetailVo>();
        //对跳转功能的支持，需要确定跳转的下一个环节而已
        bool bIsJump = false;
        if (STEP_NAME.Visible && STEP_NAME.Items.Count > 0 && STEP_NAME.SelectedValue != "")
        {
            bIsJump = true;
            taskNext = new TWfSettingTaskLogic().Details(STEP_NAME.SelectedValue);
        }
        //---------跳转功能支持完毕；

        //首先给目前实例环节的真实处理人
        INST_STEP_DETAIL.REAL_USER = (this.Page as PageBase).LogInfo.UserInfo.ID;

        foreach (string strUserID in strUserList)
        {
            TWfInstTaskDetailVo taskTemp = new TWfInstTaskDetailVo();
            taskTemp.ID = this.GetGUID();
            taskTemp.INST_TASK_CAPTION = taskNext.TASK_CAPTION;
            taskTemp.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
            taskTemp.INST_TASK_STARTTIME = GetDateTimeToStanString();
            taskTemp.OBJECT_USER = strUserID;
            taskTemp.PRE_INST_TASK_ID = WF_INST_TASK_ID;
            taskTemp.PRE_TASK_ID = TASK_ID;
            taskTemp.REAL_USER = "";
            taskTemp.INST_NOTE = taskNext.TASK_NOTE;
            taskTemp.WF_ID = WF_ID;
            taskTemp.WF_INST_ID = WF_INST_ID;//这里应该填入寄存的流程实例编号
            taskTemp.WF_SERIAL_NO = INST_WF_CONTROL.WF_SERIAL_NO;
            taskTemp.WF_TASK_ID = taskNext.WF_TASK_ID;
            //增加一个发送人的数据，发送人就是上个环节的实际处理人
            taskTemp.SRC_USER = INST_STEP_DETAIL.REAL_USER;
            detailList.Add(taskTemp);
        }

        //开始更新control表和当前环节数据
        INST_STEP_DETAIL.INST_TASK_ENDTIME = GetDateTimeToStanString();
        INST_STEP_DETAIL.INST_TASK_STATE = TWfCommDict.StepState.StateDown;
        INST_STEP_DETAIL.INST_TASK_DEAL_STATE = bIsJump ? TWfCommDict.StepDealState.ForJump : "";
        INST_STEP_DETAIL.REAL_USER = (this.Page as PageBaseForWF).LogInfo.UserInfo.ID;

        INST_WF_CONTROL.WF_TASK_ID = taskNext.ID;
        //增加对目前实例环节状态的说明信息，暂存并行环节中最后一个环节的ID
        INST_WF_CONTROL.WF_INST_TASK_ID = detailList.Count > 0 ? detailList[detailList.Count - 1].ID : "";

        //如果是最后一个环节，则直接更新主流程控制表，更新为结束状态
        if (IS_WF_END_STEP)
        {
            INST_WF_CONTROL.WF_ENDTIME = GetDateTimeToStanString();
            INST_WF_CONTROL.WF_STATE = TWfCommDict.WfState.StateFinish;
            //结束节点，清除所有新增的环节信息
            detailList.Clear();
            //如果是最后一个环节，则此节点直接为空
            INST_WF_CONTROL.WF_INST_TASK_ID = i3.ValueObject.ConstValues.SpecialCharacter.EmptyValuesFillChar;
        }

        //开始更新所有数据
        TWfInstTaskDetailLogic logicTemp = new TWfInstTaskDetailLogic();
        TWfInstControlLogic logicWf = new TWfInstControlLogic();
        //如果没有相关数据，则生成，如果存在则更新
        if (string.IsNullOrEmpty(logicTemp.Details(INST_STEP_DETAIL.ID).ID))
            logicTemp.Create(INST_STEP_DETAIL);
        else
            logicTemp.Edit((new TWfInstTaskDetailVo()
            {
                INST_TASK_ENDTIME = INST_STEP_DETAIL.INST_TASK_ENDTIME,
                INST_TASK_STATE = INST_STEP_DETAIL.INST_TASK_STATE,
                REAL_USER = INST_STEP_DETAIL.REAL_USER,
                INST_TASK_DEAL_STATE = INST_STEP_DETAIL.INST_TASK_DEAL_STATE,
                ID = WF_INST_TASK_ID
            }));
        if (string.IsNullOrEmpty(logicWf.Details(new TWfInstControlVo() { ID = INST_WF_CONTROL.ID }).ID))
            logicWf.Create(INST_WF_CONTROL);
        else
            logicWf.Edit(new TWfInstControlVo()
            {
                ID = WF_INST_ID,
                WF_TASK_ID = INST_WF_CONTROL.WF_TASK_ID,
                WF_STATE = INST_WF_CONTROL.WF_STATE,
                WF_ENDTIME = INST_WF_CONTROL.WF_ENDTIME,
                WF_INST_TASK_ID = INST_WF_CONTROL.WF_INST_TASK_ID
            });

        //如果是路由节点，则合并所有再用数据，生成一个单一数据进行处理
        if (IsAndOrStep)
        {
            //把到此环节的所有数据标记为已处理
            //获取该环节下的所有实例化环节，并都标记为已处理，然后只生成一条数据
            string strSerialNo = INST_WF_CONTROL.WF_SERIAL_NO;
            string strTaskID = TASK_ID;
            List<TWfInstTaskDetailVo> detailAndOrStepList = new List<TWfInstTaskDetailVo>();
            detailAndOrStepList = new TWfInstTaskDetailLogic().SelectAllByObject(new TWfInstTaskDetailVo() { WF_SERIAL_NO = strSerialNo, WF_TASK_ID = strTaskID, INST_TASK_STATE = TWfCommDict.StepState.StateNormal });

            //将这些数据统一处理掉，合成一个数据
            foreach (TWfInstTaskDetailVo tempInstDetail in detailAndOrStepList)
            {
                logicTemp.Edit((new TWfInstTaskDetailVo()
                {
                    INST_TASK_ENDTIME = INST_STEP_DETAIL.INST_TASK_ENDTIME,
                    INST_TASK_STATE = INST_STEP_DETAIL.INST_TASK_STATE,
                    REAL_USER = INST_STEP_DETAIL.REAL_USER,
                    INST_TASK_DEAL_STATE = INST_STEP_DETAIL.INST_TASK_DEAL_STATE,
                    ID = tempInstDetail.ID
                }));
            }

            //-------------------


            //logicTemp.Edit((new TWfInstTaskDetailVo()
            //{
            //    INST_TASK_ENDTIME = INST_STEP_DETAIL.INST_TASK_ENDTIME,
            //    INST_TASK_STATE = INST_STEP_DETAIL.INST_TASK_STATE,
            //    REAL_USER = INST_STEP_DETAIL.REAL_USER,
            //    INST_TASK_DEAL_STATE = INST_STEP_DETAIL.INST_TASK_DEAL_STATE,
            //    WF_INST_ID = WF_INST_ID,
            //    WF_TASK_ID = TASK_ID
            //}));
            ////然后把所有在路由环节前一个阶段的任务都糅合到一起，做一个处理。然后再生成路由成一条环节数据
            //if (detailList.Count > 1)
            //    detailList.RemoveRange(0, detailList.Count - 1);
        }

        //写入流程产生的新数据

        foreach (TWfInstTaskDetailVo td in detailList)
        {
            //在添加环节数据的同时，也需要将所有的业务数据都增加入数据库
            logicTemp.Create(td);
        }
        //处理评论，附件，审核等内容
        //此版本不支持保存功能
        //附件的处理
        if (dvUpLoad.Visible && UpLoadFileName.Value != "")
        {
            List<TWfInstFileListVo> twFileList = CreatFileListData();
            foreach (TWfInstFileListVo fileTemp in twFileList)
                new TWfInstFileListLogic().Create(fileTemp);
        }
        //评论的处理
        if (dvOpinions.Visible && txtOpinionText.Text != "")
        {
            TWfInstTaskOpinionsVo opinion = CreatOpinionData();
            new TWfInstTaskOpinionsLogic().Create(opinion);
        }

        //业务数据不需要整合，已预留对象供前台整理。
        TWfInstTaskServiceLogic serviceLogic = new TWfInstTaskServiceLogic();
        List<TWfInstTaskServiceVo> serviceList = new List<TWfInstTaskServiceVo>();
        foreach (TWfInstTaskDetailVo td in detailList)
        {
            if (null != INST_STEP_SERVICE_LIST)
                foreach (TWfInstTaskServiceVo service in INST_STEP_SERVICE_LIST)
                {
                    //增加ID，流程实例编号、环节实例编号等内容，业务代码，Key和Value由业务系统自己处理
                    TWfInstTaskServiceVo stemp = new TWfInstTaskServiceVo();
                    stemp.ID = this.GetGUID();
                    stemp.WF_INST_ID = td.WF_INST_ID;
                    stemp.WF_INST_TASK_ID = td.ID;
                    stemp.SERVICE_NAME = service.SERVICE_NAME;
                    stemp.SERVICE_KEY_NAME = service.SERVICE_KEY_NAME;
                    stemp.SERVICE_KEY_VALUE = service.SERVICE_KEY_VALUE;
                    stemp.SERVICE_ROW_SIGN = service.SERVICE_ROW_SIGN;
                    serviceList.Add(stemp);
                }
        }
        foreach (TWfInstTaskServiceVo serviceTemp in serviceList)
            serviceLogic.Create(serviceTemp);

        //处理完毕。已经生成了所有的数据
        //
        string strMessage = (this.Page as PageBase).LogInfo.UserInfo.USER_NAME + "发送环节:" + WF_INST_TASK_ID + " 成功";
        (this.Page as PageBase).WriteLog(i3.ValueObject.ObjectBase.LogType.WFCreateInstStepInfo, "", strMessage);

        //////潘德军增加，发送手机短信
        //不是最后一环节
        if (!IS_WF_END_STEP)
        {
            string strMobileMsgContent = (this.Page as PageBase).LogInfo.UserInfo.REAL_NAME + "给您发送了一条" + taskNext.TASK_CAPTION + "任务， ，主题：" + INST_WF_CONTROL.WF_SERVICE_NAME + "。";// 消息体
            string strMobileSendBy = (this.Page as PageBase).LogInfo.UserInfo.ID;//发送人的user id
            string strMobileAccUserIDs = "";//接收人的USER ID，如果多个用逗号分隔
            for (int i = 0; i < strUserList.Count; i++)
            {
                if (strUserList[i].Length > 0)
                    strMobileAccUserIDs += (strMobileAccUserIDs.Length > 0 ? "," : "") + strUserList[i];
            }

            string strMobileErrMsg = "";
            //手机短信发送函数
            //new SendMobileMsg().AutoSenMobilMsg(strMobileMsgContent, strMobileSendBy, strMobileAccUserIDs, true, "", ref strMobileErrMsg);
        }

        DealDownGotoNewPages(INST_STEP_DETAIL.ID, bIsJump ? TWfCommDict.StepDealState.ForJump : TWfCommDict.StepDealState.ForSend);
    }


    /// <summary>
    /// 转发的处理函数
    /// </summary>
    public void ZSendWFData()
    {
        //开始组装新数据结构，插入数据库，然后更新控制表和当前环节明细表，即可
        List<string> strUserList = new List<string>();

        //采用新的用户构建方法
        //2013-02-05提取
        if (SetMoreDealUserFlag)
            strUserList = MoreDealUserList;
        else
            strUserList = GetUserList();
        //------------------------------


        //最后一个环节不能加任何发送人，必须是裸体的
        if (strUserList.Count == 0 && !IS_WF_END_STEP)
        {
            ERROR_TIPS = "没有选择发送的对象，系统不予通过";
            return;
        }
        //转发的意思就是把此环节的数据copyN份后，生成N份数据，系统还在当前环节
        //如果后续环节有路由环节的话，则还要遵循路由环节。如果没有路由环节，则相当于传阅的概念；
        TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_ID = WF_ID, WF_TASK_ID = TASK_ID });
        TWfInstTaskDetailVo taskInst = new TWfInstTaskDetailLogic().Details(new TWfInstTaskDetailVo() { ID = WF_INST_TASK_ID });

        //首先给目前实例环节的真实处理人
        taskInst.REAL_USER = (this.Page as PageBase).LogInfo.UserInfo.ID;

        List<TWfInstTaskDetailVo> detailList = new List<TWfInstTaskDetailVo>();
        foreach (string strUserID in strUserList)
        {
            TWfInstTaskDetailVo taskTemp = new TWfInstTaskDetailVo();
            taskTemp.ID = this.GetGUID();
            taskTemp.INST_TASK_CAPTION = task.TASK_CAPTION;
            taskTemp.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
            taskTemp.INST_TASK_STARTTIME = GetDateTimeToStanString();
            taskTemp.OBJECT_USER = strUserID;
            taskTemp.PRE_INST_TASK_ID = taskInst.ID;//这里有一个衔接，上一个环节实例就是被转发的那个环节实例，这样可以追踪到
            taskTemp.PRE_TASK_ID = taskInst.PRE_TASK_ID;
            taskTemp.REAL_USER = "";
            taskTemp.INST_NOTE = task.TASK_NOTE;
            taskTemp.WF_ID = taskInst.WF_ID;
            taskTemp.WF_INST_ID = taskInst.WF_INST_ID;//这里应该填入寄存的流程实例编号
            taskTemp.WF_SERIAL_NO = taskInst.WF_SERIAL_NO;
            taskTemp.WF_TASK_ID = taskInst.WF_TASK_ID;
            //然后补充新节点的发送人
            taskTemp.SRC_USER = taskInst.REAL_USER;//增加一个发送人数据

            detailList.Add(taskTemp);
        }
        //更新现有环节，将标志更新为
        taskInst.INST_TASK_STATE = TWfCommDict.StepState.StateDown;
        taskInst.INST_TASK_ENDTIME = GetDateTimeToStanString();
        taskInst.REAL_USER = (this.Page as PageBaseForWF).LogInfo.UserInfo.ID;
        taskInst.INST_TASK_DEAL_STATE = TWfCommDict.StepDealState.ForZSend;

        //先更新自己处理的数据
        TWfInstTaskDetailLogic logicTemp = new TWfInstTaskDetailLogic();
        logicTemp.Edit((new TWfInstTaskDetailVo()
        {
            INST_TASK_ENDTIME = taskInst.INST_TASK_ENDTIME,
            INST_TASK_STATE = taskInst.INST_TASK_STATE,
            REAL_USER = taskInst.REAL_USER,
            INST_TASK_DEAL_STATE = taskInst.INST_TASK_DEAL_STATE,
            ID = taskInst.ID
        }));

        TWfInstControlLogic instWFLogic = new TWfInstControlLogic();
        instWFLogic.Edit(new TWfInstControlVo()
        {
            ID = detailList[0].WF_INST_ID,
            WF_INST_TASK_ID = detailList[0].ID,
            WF_TASK_ID = detailList[0].WF_TASK_ID
        });

        //写入流程产生的新数据
        foreach (TWfInstTaskDetailVo td in detailList)
        {
            logicTemp.Create(td);
        }
        //附件的处理
        if (dvUpLoad.Visible && UpLoadFileName.Value != "")
        {
            List<TWfInstFileListVo> twFileList = CreatFileListData();
            foreach (TWfInstFileListVo fileTemp in twFileList)
                new TWfInstFileListLogic().Create(fileTemp);
        }
        //评论的处理
        if (dvOpinions.Visible && txtOpinionText.Text != "")
        {
            TWfInstTaskOpinionsVo opinion = CreatOpinionData();
            new TWfInstTaskOpinionsLogic().Create(opinion);
        }

        //业务数据不需要整合，已预留对象供前台整理。
        TWfInstTaskServiceLogic serviceLogic = new TWfInstTaskServiceLogic();
        List<TWfInstTaskServiceVo> serviceList = new List<TWfInstTaskServiceVo>();
        foreach (TWfInstTaskDetailVo td in detailList)
        {
            if (null != INST_STEP_SERVICE_LIST_FOR_OLD)
                foreach (TWfInstTaskServiceVo service in INST_STEP_SERVICE_LIST_FOR_OLD)
                {
                    //增加ID，流程实例编号、环节实例编号等内容，业务代码，Key和Value由业务系统自己处理
                    TWfInstTaskServiceVo stemp = new TWfInstTaskServiceVo();
                    stemp.ID = this.GetGUID();
                    stemp.WF_INST_ID = td.WF_INST_ID;
                    stemp.WF_INST_TASK_ID = td.ID;
                    stemp.SERVICE_NAME = service.SERVICE_NAME;
                    stemp.SERVICE_KEY_NAME = service.SERVICE_KEY_NAME;
                    stemp.SERVICE_KEY_VALUE = service.SERVICE_KEY_VALUE;
                    stemp.SERVICE_ROW_SIGN = service.SERVICE_ROW_SIGN;
                    serviceList.Add(stemp);
                }
        }
        foreach (TWfInstTaskServiceVo serviceTemp in serviceList)
            serviceLogic.Create(serviceTemp);

        string strMessage = (this.Page as PageBase).LogInfo.UserInfo.USER_NAME + "转发环节:" + WF_INST_TASK_ID + " 成功";
        (this.Page as PageBase).WriteLog(i3.ValueObject.ObjectBase.LogType.WFCreateInstStepInfo, "", strMessage);

        //////潘德军增加，发送手机短信
        if (true)
        {
            string strMobileMsgContent = (this.Page as PageBase).LogInfo.UserInfo.REAL_NAME + "给您转发了一条" + task.TASK_CAPTION + "任务，单号："
                + INST_WF_CONTROL.WF_SERVICE_CODE + "，主题：" + INST_WF_CONTROL.WF_SERVICE_NAME + "。";// 消息体
            string strMobileSendBy = (this.Page as PageBase).LogInfo.UserInfo.ID;//发送人的user id
            string strMobileAccUserIDs = "";//接收人的USER ID，如果多个用逗号分隔
            for (int i = 0; i < strUserList.Count; i++)
            {
                if (strUserList[i].Length > 0)
                    strMobileAccUserIDs += (strMobileAccUserIDs.Length > 0 ? "," : "") + strUserList[i];
            }

            string strMobileErrMsg = "";
            //手机短信发送函数
            //new SendMobileMsg().AutoSenMobilMsg(strMobileMsgContent, strMobileSendBy, strMobileAccUserIDs, true, "", ref strMobileErrMsg);
        }

        //处理完毕。已经生成了所有的数据
        DealDownGotoNewPages(INST_STEP_DETAIL.ID, TWfCommDict.StepDealState.ForZSend);


    }

    /// <summary>
    /// 退回的处理步骤
    /// </summary>
    public void BackWFData()
    {
        TWfInstTaskDetailLogic instTaskLogic = new TWfInstTaskDetailLogic();
        TWfInstTaskDetailVo taskInst = instTaskLogic.Details(new TWfInstTaskDetailVo() { ID = WF_INST_TASK_ID });
        //TWfSettingTaskVo taskPreSetting = new TWfSettingTaskLogic().GetPreStep(new TWfSettingTaskVo() { WF_ID = taskInst.WF_ID, ID = taskInst.WF_TASK_ID });
        //如果是跳转后的回退，则直接退回至上一个环节，就是跳转前的页面，而不是设定的前一个环节
        TWfSettingTaskVo taskPreSetting = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_ID = taskInst.WF_ID, ID = taskInst.PRE_TASK_ID });
        //TWfInstTaskDetailVo taskPre = instTaskLogic.Details(new TWfInstTaskDetailVo() { ID = taskInst.PRE_INST_TASK_ID });
        TWfInstTaskDetailVo taskPre = instTaskLogic.Details(new TWfInstTaskDetailVo() { WF_SERIAL_NO = taskInst.WF_SERIAL_NO, WF_TASK_ID=taskPreSetting.WF_TASK_ID });   //Modify By weilin 2014-11-19
        TWfInstControlVo wfInst = new TWfInstControlLogic().Details(taskInst.WF_INST_ID);
        //根据上一个环节的数据来重新生成所有数据

        TWfInstTaskDetailVo taskNew = new TWfInstTaskDetailVo();
        taskNew.ID = this.GetGUID();
        taskNew.INST_NOTE = taskPreSetting.TASK_NOTE;
        taskNew.INST_TASK_CAPTION = taskPreSetting.TASK_CAPTION;
        taskNew.INST_TASK_STARTTIME = this.GetDateTimeToStanString();
        taskNew.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
        taskNew.OBJECT_USER = taskPre.OBJECT_USER;//使用上环节的目标处理人
        taskNew.PRE_INST_TASK_ID = taskInst.ID;//上一个环节的编号，将成为本环节的上环节编号
        //这里比较拗口，一定要理解，涉及到连环退回的情况，只有一种可能会出错，就是退到起始节点了，还在退的情况，而这种情况是由配置不当引起，此刻不做处理。
        taskNew.PRE_TASK_ID = new TWfSettingTaskLogic().GetPreStep(new TWfSettingTaskVo() { WF_ID = taskInst.WF_ID, ID = taskPreSetting.WF_TASK_ID }).ID;
        taskNew.WF_ID = taskPreSetting.WF_ID;
        taskNew.WF_INST_ID = taskPre.WF_INST_ID;
        taskNew.WF_SERIAL_NO = taskPre.WF_SERIAL_NO;
        taskNew.WF_TASK_ID = taskPreSetting.ID;

        //首先给目前实例环节的真实处理人
        taskInst.REAL_USER = (this.Page as PageBase).LogInfo.UserInfo.ID;

        //然后补充新节点的发送人
        taskNew.SRC_USER = taskInst.REAL_USER;//增加一个发送人数据

        //将原环节表的标志位更新为完成
        taskInst.INST_TASK_ENDTIME = this.GetDateTimeToStanString();
        taskInst.INST_TASK_STATE = TWfCommDict.StepState.StateDown;
        taskInst.INST_TASK_DEAL_STATE = TWfCommDict.StepDealState.ForBack;


        //环节表更新完毕，接着更新控制表
        //更新控制表信息
        //退回时要把附件和评论信息放入数据库，业务数据也要全部退回
        //写入流程产生的新数据

        instTaskLogic.Create(taskNew);
        instTaskLogic.Edit(new TWfInstTaskDetailVo()
        {
            ID = taskInst.ID,
            INST_TASK_ENDTIME = taskInst.INST_TASK_ENDTIME,
            INST_TASK_STATE = taskInst.INST_TASK_STATE,
            INST_TASK_DEAL_STATE = taskInst.INST_TASK_DEAL_STATE,
            REAL_USER = taskInst.REAL_USER
        });
        TWfInstControlLogic instWFLogic = new TWfInstControlLogic();
        instWFLogic.Edit(new TWfInstControlVo()
        {
            ID = wfInst.ID,
            WF_INST_TASK_ID = taskNew.ID,
            WF_TASK_ID = taskNew.WF_TASK_ID
        });

        //附件的处理
        if (dvUpLoad.Visible && UpLoadFileName.Value != "")
        {
            List<TWfInstFileListVo> twFileList = CreatFileListData();
            foreach (TWfInstFileListVo fileTemp in twFileList)
                new TWfInstFileListLogic().Create(fileTemp);
        }
        //评论的处理
        if (dvOpinions.Visible && txtOpinionText.Text != "")
        {
            TWfInstTaskOpinionsVo opinion = CreatOpinionData();
            new TWfInstTaskOpinionsLogic().Create(opinion);
        }

        //业务数据不需要整合，已预留对象供前台整理。
        TWfInstTaskServiceLogic serviceLogic = new TWfInstTaskServiceLogic();
        List<TWfInstTaskServiceVo> serviceList = new List<TWfInstTaskServiceVo>();

        if (null != INST_STEP_SERVICE_LIST_FOR_OLD)
            foreach (TWfInstTaskServiceVo service in INST_STEP_SERVICE_LIST_FOR_OLD)
            {
                //增加ID，流程实例编号、环节实例编号等内容，业务代码，Key和Value由业务系统自己处理
                TWfInstTaskServiceVo stemp = new TWfInstTaskServiceVo();
                stemp.ID = this.GetGUID();
                stemp.WF_INST_ID = wfInst.ID;
                stemp.WF_INST_TASK_ID = taskNew.ID;
                stemp.SERVICE_NAME = service.SERVICE_NAME;
                stemp.SERVICE_KEY_NAME = service.SERVICE_KEY_NAME;
                stemp.SERVICE_KEY_VALUE = service.SERVICE_KEY_VALUE;
                stemp.SERVICE_ROW_SIGN = service.SERVICE_ROW_SIGN;
                serviceList.Add(stemp);
            }

        foreach (TWfInstTaskServiceVo serviceTemp in serviceList)
            serviceLogic.Create(serviceTemp);

        //WriteLog(i3.ValueObject.ObjectBase.LogType.AddApparatusInfo, "",strMessage); 
        string strMessage = (this.Page as PageBase).LogInfo.UserInfo.USER_NAME + "退回环节:" + WF_INST_TASK_ID + " 成功";
        (this.Page as PageBase).WriteLog(i3.ValueObject.ObjectBase.LogType.WFCreateInstStepInfo, "", strMessage);

        //////潘德军增加，发送手机短信
        if (true)
        {
            string strMobileMsgContent = (this.Page as PageBase).LogInfo.UserInfo.REAL_NAME + "给您退回了一条" + taskPreSetting.TASK_CAPTION + "任务，主题：" + INST_WF_CONTROL.WF_SERVICE_NAME + "。";// 消息体
            string strMobileSendBy = (this.Page as PageBase).LogInfo.UserInfo.ID;//发送人的user id
            string strMobileAccUserIDs = taskPre.OBJECT_USER;//接收人的USER ID

            string strMobileErrMsg = "";
            //手机短信发送函数
            //new SendMobileMsg().AutoSenMobilMsg(strMobileMsgContent, strMobileSendBy, strMobileAccUserIDs, true, "", ref strMobileErrMsg);
        }

        //处理完毕。已经生成了所有的数据
        DealDownGotoNewPages(INST_STEP_DETAIL.ID, TWfCommDict.StepDealState.ForBack);

    }

    /// <summary>
    /// 返元的处理
    /// </summary>
    private void GoStartWFData()
    {
        //具有返元权限的操作人员可以做
        TWfInstTaskDetailLogic instTaskLogic = new TWfInstTaskDetailLogic();
        TWfInstTaskDetailVo taskInst = instTaskLogic.Details(new TWfInstTaskDetailVo() { ID = WF_INST_TASK_ID });
        List<TWfSettingTaskVo> taskSettingList = new TWfSettingTaskLogic().SelectByObjectListForSetp(new TWfSettingTaskVo() { WF_ID = WF_ID });
        List<TWfInstTaskDetailVo> taskInstList = instTaskLogic.SelectByObject(new TWfInstTaskDetailVo() { WF_ID = WF_ID, WF_INST_ID = taskInst.WF_INST_ID }, 0, 100);
        TWfInstControlVo wfInst = new TWfInstControlLogic().Details(taskInst.WF_INST_ID);
        //如果配置信息没有任何节点，则返回
        if (taskSettingList.Count < 1 && taskInstList.Count < 1)
            return;

        TWfInstTaskDetailVo taskNew = new TWfInstTaskDetailVo();
        taskNew.ID = this.GetGUID();
        taskNew.INST_NOTE = taskSettingList[0].TASK_NOTE;
        taskNew.INST_TASK_CAPTION = taskSettingList[0].TASK_CAPTION;
        taskNew.INST_TASK_STARTTIME = this.GetDateTimeToStanString();
        taskNew.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
        taskNew.OBJECT_USER = taskInstList[0].OBJECT_USER;//使用上环节的目标处理人
        taskNew.PRE_INST_TASK_ID = taskInst.ID;//上一个环节的编号，将成为本环节的上环节编号
        //返元的所有新节点的前一个节点肯定是空的，直接置空即可
        //taskNew.PRE_TASK_ID = i3.ValueObject.ConstValues.SpecialCharacter.EmptyValuesFillChar;
        taskNew.WF_ID = taskSettingList[0].WF_ID;
        taskNew.WF_INST_ID = taskInstList[0].WF_INST_ID;
        taskNew.WF_SERIAL_NO = taskInstList[0].WF_SERIAL_NO;
        taskNew.WF_TASK_ID = taskSettingList[0].ID;


        //首先给目前实例环节的真实处理人
        taskInst.REAL_USER = (this.Page as PageBase).LogInfo.UserInfo.ID;

        //然后补充新节点的发送人
        taskNew.SRC_USER = taskInst.REAL_USER;//增加一个发送人数据


        //将原环节表的标志位更新为完成
        taskInst.INST_TASK_ENDTIME = this.GetDateTimeToStanString();
        taskInst.INST_TASK_STATE = TWfCommDict.StepState.StateDown;
        taskInst.INST_TASK_DEAL_STATE = TWfCommDict.StepDealState.ForToZero;
        taskInst.REAL_USER = (this.Page as PageBase).LogInfo.UserInfo.ID;

        //环节表更新完毕，接着更新控制表
        //更新控制表信息
        //退回时要把附件和评论信息放入数据库，业务数据也要全部退回
        //写入流程产生的新数据

        instTaskLogic.Create(taskNew);
        instTaskLogic.Edit(new TWfInstTaskDetailVo()
        {
            ID = taskInst.ID,
            INST_TASK_ENDTIME = taskInst.INST_TASK_ENDTIME,
            INST_TASK_STATE = taskInst.INST_TASK_STATE,
            INST_TASK_DEAL_STATE = taskInst.INST_TASK_DEAL_STATE,
            REAL_USER = taskInst.REAL_USER
        });
        TWfInstControlLogic instWFLogic = new TWfInstControlLogic();
        instWFLogic.Edit(new TWfInstControlVo()
        {
            ID = wfInst.ID,
            WF_INST_TASK_ID = taskNew.ID,
            WF_TASK_ID = taskNew.WF_TASK_ID
        });

        //附件的处理
        if (dvUpLoad.Visible && UpLoadFileName.Value != "")
        {
            List<TWfInstFileListVo> twFileList = CreatFileListData();
            foreach (TWfInstFileListVo fileTemp in twFileList)
                new TWfInstFileListLogic().Create(fileTemp);
        }
        //评论的处理
        if (dvOpinions.Visible && txtOpinionText.Text != "")
        {
            TWfInstTaskOpinionsVo opinion = CreatOpinionData();
            new TWfInstTaskOpinionsLogic().Create(opinion);
        }

        //业务数据不需要整合，已预留对象供前台整理。
        TWfInstTaskServiceLogic serviceLogic = new TWfInstTaskServiceLogic();
        List<TWfInstTaskServiceVo> serviceList = new List<TWfInstTaskServiceVo>();

        if (null != INST_STEP_SERVICE_LIST_FOR_OLD)
            foreach (TWfInstTaskServiceVo service in INST_STEP_SERVICE_LIST_FOR_OLD)
            {
                //增加ID，流程实例编号、环节实例编号等内容，业务代码，Key和Value由业务系统自己处理
                TWfInstTaskServiceVo stemp = new TWfInstTaskServiceVo();
                stemp.ID = this.GetGUID();
                stemp.WF_INST_ID = wfInst.ID;
                stemp.WF_INST_TASK_ID = taskNew.ID;
                stemp.SERVICE_NAME = service.SERVICE_NAME;
                stemp.SERVICE_KEY_NAME = service.SERVICE_KEY_NAME;
                stemp.SERVICE_KEY_VALUE = service.SERVICE_KEY_VALUE;
                stemp.SERVICE_ROW_SIGN = service.SERVICE_ROW_SIGN;
                serviceList.Add(stemp);
            }

        foreach (TWfInstTaskServiceVo serviceTemp in serviceList)
            serviceLogic.Create(serviceTemp);

        string strMessage = (this.Page as PageBase).LogInfo.UserInfo.USER_NAME + "返元环节:" + WF_INST_TASK_ID + " 成功";
        (this.Page as PageBase).WriteLog(i3.ValueObject.ObjectBase.LogType.WFCreateInstStepInfo, "", strMessage);
        //处理完毕。已经生成了所有的数据
        DealDownGotoNewPages(INST_STEP_DETAIL.ID, TWfCommDict.StepDealState.ForToZero);

    }



    /// <summary>
    /// 时间不够，不在开发保存环节，直接提交的那种，后续再安排开发计划
    /// </summary>
    public void SaveWFData()
    {
        //开始更新control表和当前环节数据
        INST_STEP_DETAIL.INST_TASK_STARTTIME = GetDateTimeToStanString();
        INST_STEP_DETAIL.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
        INST_STEP_DETAIL.REAL_USER = (this.Page as PageBaseForWF).LogInfo.UserInfo.ID;

        INST_WF_CONTROL.WF_TASK_ID = TASK_ID;

        TWfInstTaskDetailLogic logicTemp = new TWfInstTaskDetailLogic();
        TWfInstControlLogic logicWf = new TWfInstControlLogic();
        //如果没有相关数据，则生成
        if (string.IsNullOrEmpty(logicTemp.Details(INST_STEP_DETAIL.ID).ID))
            logicTemp.Create(INST_STEP_DETAIL);

        if (string.IsNullOrEmpty(logicWf.Details(new TWfInstControlVo() { ID = INST_WF_CONTROL.ID }).ID))
            logicWf.Create(INST_WF_CONTROL);

        //保存为主附件、评论、审核意见的相关内容,

    }

    /// <summary>
    /// 寄存业务数据的唯一入口函数
    /// <param name="strServiceName">业务系统自己标记的名称</param>
    /// <param name="strServiceKeyName">Key（相当于HasTable的Key）</param>
    /// <param name="strServiceKeyValue">Value（相当于HasTable的Value）</param>
    /// </summary>
    public void SaveInstStepServiceData(string strServiceName, string strServiceKeyName, string strServiceKeyValue)
    {
        TWfInstTaskServiceVo temp = new TWfInstTaskServiceVo();
        temp.SERVICE_NAME = strServiceName;
        temp.SERVICE_KEY_NAME = strServiceKeyName;
        temp.SERVICE_KEY_VALUE = strServiceKeyValue;
        if (null == INST_STEP_SERVICE_LIST)
            INST_STEP_SERVICE_LIST = new List<TWfInstTaskServiceVo>();
        INST_STEP_SERVICE_LIST.Add(temp);
    }
    /// <summary>
    /// 寄存业务数据的唯一入口函数
    /// <param name="strServiceName">业务系统自己标记的名称</param>
    /// <param name="strServiceKeyName">Key（相当于HasTable的Key）</param>
    /// <param name="strServiceKeyValue">Value（相当于HasTable的Value）</param>
    /// <param name="strRowSign">增加标记位</param>
    /// </summary>
    public void SaveInstStepServiceData(string strServiceName, string strServiceKeyName, string strServiceKeyValue, string strRowSign)
    {
        TWfInstTaskServiceVo temp = new TWfInstTaskServiceVo();
        temp.SERVICE_NAME = strServiceName;
        temp.SERVICE_KEY_NAME = strServiceKeyName;
        temp.SERVICE_KEY_VALUE = strServiceKeyValue;
        temp.SERVICE_ROW_SIGN = strRowSign;
        if (null == INST_STEP_SERVICE_LIST)
            INST_STEP_SERVICE_LIST = new List<TWfInstTaskServiceVo>();
        INST_STEP_SERVICE_LIST.Add(temp);
    }


    /// <summary>
    /// 生成评论信息
    /// </summary>
    /// <returns></returns>
    private TWfInstTaskOpinionsVo CreatOpinionData()
    {
        TWfInstTaskOpinionsVo opinion = new TWfInstTaskOpinionsVo();
        opinion.ID = this.GetGUID();
        opinion.WF_INST_ID = WF_INST_ID;
        opinion.WF_INST_TASK_ID = WF_INST_TASK_ID;
        opinion.WF_IT_OPINION = txtOpinionText.Text;
        opinion.WF_IT_OPINION_TYPE = "";//评论类型，后续开发
        opinion.WF_IT_OPINION_USER = (this.Page as PageBaseForWF).LogInfo.UserInfo.ID;
        opinion.WF_IT_SHOW_TYPE = "";//评论显示问题，后续开发，主要分自己可见，还是所有人可见。
        opinion.WF_IT_OPINION_TIME = GetDateTimeToStanString();
        return opinion;
    }

    /// <summary>
    /// 生成上传附件数据
    /// </summary>
    /// <returns></returns>
    private List<TWfInstFileListVo> CreatFileListData()
    {
        string[] strFileList = UpLoadFileName.Value.Split('|');
        List<TWfInstFileListVo> fileList = new List<TWfInstFileListVo>();
        string strRootPath = Server.MapPath("~/Web.Config").Replace("Web.Config", "") + TWfCommDict.strFileUpLoadFolder;
        foreach (string strFileName in strFileList)
        {
            if (string.IsNullOrEmpty(strFileName) || strFileName.Trim() == "")
                continue;
            TWfInstFileListVo twFile = new TWfInstFileListVo();
            twFile.ID = this.GetGUID();
            twFile.WF_FILE_FULLNAME = strRootPath + strFileName;
            twFile.WF_FILE_ICO = "";
            twFile.WF_FILE_NAME = strFileName;
            twFile.WF_ID = WF_ID;
            twFile.WF_INST_ID = WF_INST_ID;
            twFile.WF_SERIAL_NO = INST_WF_CONTROL.WF_SERIAL_NO;
            twFile.UPLOAD_USER = (this.Page as PageBaseForWF).LogInfo.UserInfo.ID;
            twFile.UPLOAD_TIME = GetDateTimeToStanString();
            fileList.Add(twFile);
        }
        return fileList;
    }

    private void DealDownGotoNewPages(string strID, string strDealType)
    {
        string strDealName = "";
        switch (strDealType)
        {
            case TWfCommDict.StepDealState.ForBack:
                strDealName = "退回成功";
                break;
            case TWfCommDict.StepDealState.ForCallBack:
                strDealName = "回收成功";
                break;
            case TWfCommDict.StepDealState.ForJump:
                strDealName = "跳转成功";
                break;
            case TWfCommDict.StepDealState.ForToZero:
                strDealName = "返元成功";
                break;
            case TWfCommDict.StepDealState.ForZSend:
                strDealName = "转发成功";
                break;
            case TWfCommDict.StepDealState.ForSend:
                strDealName = "发送成功";
                break;
            default:
                strDealName = "处理成功";
                break;
        }

        if (IS_WF_END_STEP && strDealType == TWfCommDict.StepDealState.ForSend)
            strDealName = "本流程已处理完毕";
        //增加提示，是否关闭页面
        //strDealName += " 将转向至任务办理界面";
        string strGoReturnURL = "Sys/WF/WFTaskListForJS.aspx";
        DataTable dtMenuTaskList= new TSysMenuLogic().SelectByTable(new TSysMenuVo(){MENU_URL="../Sys/Wf/WfTaskListForJS_Tab.aspx"});
        if (dtMenuTaskList.Rows.Count > 0)
        {
            strGoReturnURL = "Sys/WF/WfTaskListForJS_Tab.aspx";
        }

        string strTitle = "任务办理";
        //如果是初始环节，则要考虑首环节退回页面的问题
        if (IS_WF_START)
        {
            strGoReturnURL = new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { WF_ID = WF_ID }).FSTEP_RETURN_URL;
            string srhReturnURL = "../" + strGoReturnURL;
            strTitle = new TSysMenuLogic().SelectByObject(new TSysMenuVo { MENU_URL = srhReturnURL }).MENU_TEXT;
        }

        //--------------------
        string strReturnUrl = this.Request.Url.ToString().ToUpper();
        string strVUrl = this.Request.AppRelativeCurrentExecutionFilePath.ToString().ToUpper().Replace("~/", "");
        strReturnUrl = strReturnUrl.Substring(0, strReturnUrl.IndexOf(strVUrl)) + strGoReturnURL;
        if (this.Page is PageBase)
        {
            (this.Page as PageBase).LigerDialogConfirmAlert(strDealName, strTitle, strReturnUrl);
            //(this.Page as PageBase).LigerDialogConfirmAlert(strDealName, "工作流操作提示", strReturnUrl);
        }
        else
            //new i3.View.PageBase().LigerDialogConfirmAlert(strDealName, "工作流操作提示", strReturnUrl);
            new i3.View.PageBase().LigerDialogConfirmAlert(strDealName, strTitle, strReturnUrl);

    }

    /// <summary>
    /// 不知道干啥写了一堆。可能有用
    /// </summary>
    public void NNNNNNNNNNNNNNNNNNNNNNN()
    {
        //提交数据的时候，先生成两个数据体，然后再更新控制表和上一环节的数据
        TWfSettingTaskLogic logic = new TWfSettingTaskLogic();
        TWfSettingTaskVo task = logic.Details(new TWfSettingTaskVo() { WF_ID = WF_ID, WF_TASK_ID = TASK_ID });
        TWfSettingTaskVo taskPre = logic.GetPreStep(task);
        TWfSettingTaskVo taskNext = logic.GetNextStep(task);
        DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(new TSysUserPostVo());
        List<string> strUserList = new List<string>();
        if (task.OPER_TYPE == "02")
        {
            foreach (string strPost in task.OPER_VALUE.Split('|'))
                foreach (DataRow dr in dtUserPost.Rows)
                    if (dr[TSysUserPostVo.POST_ID_FIELD].ToString() == strPost)
                        strUserList.Add(dr[TSysUserPostVo.USER_ID_FIELD].ToString());
        }
        else
            foreach (string strTemp in task.OPER_VALUE.Split('|'))
                strUserList.Add(strTemp);

        //用户取出来之后，要针对用户进行环节的处理
    }




    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    /// <summary>
    /// 初始化流程函数
    /// 在 页面初始化是调用，就是在Page_Load的 !IsPostBack下调用
    /// </summary>
    public void InitWFDict()
    {
        string strWF_ID = Request.QueryString[TWfSettingTaskVo.WF_ID_FIELD];
        string strWF_TASK_ID = Request.QueryString[TWfSettingTaskVo.WF_TASK_ID_FIELD];

        string strWF_INST_TASK_ID = Request.QueryString[TWfInstTaskOpinionsVo.WF_INST_TASK_ID_FIELD];
        string strWF_INST_ID = Request.QueryString[TWfInstTaskDetailVo.WF_INST_ID_FIELD];

        bool bIS_WF_START = Request.QueryString["IS_WF_START"] == "1" ? true : false;

        //如果无法获取，则全置空，不能报错
        if (string.IsNullOrEmpty(strWF_ID))
            strWF_ID = START_PAGE_WFID;
        if (string.IsNullOrEmpty(strWF_TASK_ID))
            strWF_TASK_ID = START_PAGE_STEPID;



        this.WF_ID = strWF_ID;
        this.TASK_ID = strWF_TASK_ID;
        this.WF_INST_TASK_ID = strWF_INST_TASK_ID;
        this.WF_INST_ID = strWF_INST_ID;

        this.ClearLastSessionData();
        this.LoadWFData(this.WF_ID, this.TASK_ID, WF_INST_ID, WF_INST_TASK_ID, bIS_WF_START);
        this.LoadUserControls();
        this.BuildNewStepDataOrLoadCurrentStepData();
        //在进行所有5步初始化后，在进行业务数据载入
        this.LoadBusinessData();

        //载入Opinion的客户端URL连接
        SetOpinionAndFileURL();
    }

    /// <summary>
    /// 清空几个Session缓存，原因是：用ViewState无法序列化相关数据结构，用Session可以做到，但又怕影响到后续功能，故清空
    /// </summary>
    public void ClearLastSessionData()
    {
        try
        {
            Session.Remove("INST_WF_CONTROL");
            Session.Remove("INST_STEP_DETAIL");
            Session.Remove("INST_STEP_SERVICE_LIST");
            Session.Remove("INST_STEP_SERVICE_LIST_FOR_OLD");
            //2013-02-05增加多用户支持寄存，也需要删除
            Session.Remove("MORE_DEAL_USER_LIST");
        }
        catch { }
    }

    /// <summary>
    /// 鉴定是否为最后一个环节的判定
    /// </summary>
    /// <param name="strWFID">使用的流程编号</param>
    /// <param name="strStepID">目前的环节编号</param>
    /// <returns>是否为最后一个环节</returns>
    private bool IsEndStep(string strWFID, string strStepID)
    {
        string strStep1 = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strStepID }).WF_TASK_ID;
        DataTable dt = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo() { WF_ID = strWFID, SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD, SORT_TYPE = " asc " });
        if (dt.Rows.Count > 0 && dt.Rows[dt.Rows.Count - 1][TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strStep1)
        {
            return true;
        }
        return false;
    }



    #region 各种事件的处理方法

    protected void lnkbtnViewOpinion_Click(object sender, EventArgs e)
    {
        //点击弹出对话框
        string strReturnUrl = this.Request.Url.ToString().ToUpper();
        string strVUrl = this.Request.AppRelativeCurrentExecutionFilePath.ToString().ToUpper().Replace("~/", "");
        strReturnUrl = strReturnUrl.Substring(0, strReturnUrl.IndexOf(strVUrl)) + "Sys/WF/WFShowMoreOpinion.aspx";
        strReturnUrl += "?" + TWfInstTaskDetailVo.WF_INST_ID_FIELD + "=" + WF_INST_ID + "&" + TWfInstTaskOpinionsVo.WF_INST_TASK_ID_FIELD + "=" + WF_INST_TASK_ID;

        if (this.Page is PageBase)
        {
            (this.Page as PageBase).LigerOpenWindow("更多评论", strReturnUrl, "600", "400");
        }
        else
            //
            new i3.View.PageBase().LigerOpenWindow("更多评论", strReturnUrl, "600", "400");
    }

    protected void SetOpinionAndFileURL()
    {
        //点击弹出对话框
        string strReturnUrl = this.Request.Url.ToString().ToUpper();
        string strVUrl = this.Request.AppRelativeCurrentExecutionFilePath.ToString().ToUpper().Replace("~/", "");
        strReturnUrl = strReturnUrl.Substring(0, strReturnUrl.IndexOf(strVUrl));
        string strOpinionUrl = strReturnUrl + "Sys/WF/WFShowMoreOpinion.aspx";
        string strFileUrl = strReturnUrl + "Sys/WF/WFShowMoreFileList.aspx";
        string strPara = "?" + TWfInstTaskDetailVo.WF_INST_ID_FIELD + "=" + WF_INST_ID + "&" + TWfInstTaskOpinionsVo.WF_INST_TASK_ID_FIELD + "=" + WF_INST_TASK_ID;
        this.hiddUrl.Value = strOpinionUrl + strPara;
        this.hiddFile.Value = strFileUrl + strPara;

    }


    public void btnSend_Click(object sender, EventArgs e)
    {
        //先处理业务数据
        bool bIsGoTo = BuildBusinessData();
        if (!bIsGoTo)
        {
            //提示信息
            (this.Page as PageBase).LigerDialogAlert(ERROR_TIPS, "warn");
            return;
        }
        //验证没有问题，则执行发送的处理方法
        UpdateAndSendWFData();
    }
    protected void btnZSend_Click(object sender, EventArgs e)
    {
        //先处理业务数据
        bool bIsGoTo = BuildBusinessData();
        if (!bIsGoTo)
        {
            //提示信息
            (this.Page as PageBase).LigerDialogAlert(ERROR_TIPS, "warn");
            return;
        }
        //转发的处理方法
        ZSendWFData();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //先处理业务数据
        bool bIsGoTo = BuildBusinessData();
        if (!bIsGoTo)
        {
            //提示信息
            (this.Page as PageBase).LigerDialogAlert(ERROR_TIPS, "warn");
            return;
        }

        //退回的处理方法
        BackWFData();
    }

    protected void btnHold_Click(object sender, EventArgs e)
    {
        //挂起的处理方法
        //调用页面基类的处理方法即可
        new PageBaseForWF().WFOperateForHold(WF_INST_ID);
    }
    protected void btnReLoad_Click(object sender, EventArgs e)
    {
        //恢复的处理方法
        //调用页面基类的处理方法即可
        new PageBaseForWF().WFOperateForReNormal(WF_INST_ID);
    }
    protected void btnPause_Click(object sender, EventArgs e)
    {
        //暂停的处理方法
    }
    protected void btnReStart_Click(object sender, EventArgs e)
    {
        //先处理业务数据
        bool bIsGoTo = BuildBusinessData();
        if (!bIsGoTo)
        {
            //提示信息
            (this.Page as PageBase).LigerDialogAlert(ERROR_TIPS, "warn");
            return;
        }
        //返元的处理方法
        GoStartWFData();
    }


    protected void btnKill_Click(object sender, EventArgs e)
    {
        //销毁的处理方法
        //调用页面基类的处理方法即可
        new PageBaseForWF().WFOperateForKill(WF_INST_ID);
    }

    protected void btnCallBack_Click(object sender, EventArgs e)
    {

    }
    #endregion


    protected void btnUpLoading_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(FileUpload1.FileName) || FileUpload1.FileName == "")
            return;
        //上传按钮的处理
        string strFileName = this.GetGUID() + "_" + FileUpload1.FileName;
        string strFileFullName = Server.MapPath("~/Web.Config").Replace("Web.Config", "") + TWfCommDict.strFileUpLoadFolder;
        if (!System.IO.File.Exists(strFileFullName))
            System.IO.Directory.CreateDirectory(strFileFullName);
        FileUpload1.SaveAs(strFileFullName + strFileName);
        UpLoadFileName.Value += strFileName;
        UpLoadFileName.Value += "|";
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //调用页面实例化的接口方法
        (this.Page as IWFStepRules).SaveBusinessDataFromPageControl();
        //保存完毕后，置灰按钮
        //btnSave.Enabled = false;
    }


    /// <summary>
    /// 复制一个序列进入另一个序列
    /// </summary>
    /// <param name="taskInstList"></param>
    /// <returns></returns>
    public static List<TWfInstTaskDetailVo> ListToList(List<TWfInstTaskDetailVo> taskInstList)
    {
        List<TWfInstTaskDetailVo> aList = new List<TWfInstTaskDetailVo>();
        foreach (TWfInstTaskDetailVo temp in taskInstList)
        {
            TWfInstTaskDetailVo google = new TWfInstTaskDetailVo();
            google = (TWfInstTaskDetailVo)temp.Clone();
            aList.Add(google);
        }
        return aList;
    }

    private List<string> GetUserList()
    {
        List<string> strUserList = new List<string>();
        if (chkbxlstSelectUsers.Items.Count == 0)
        {
            foreach (ListItem li in chkbxlstAllUsers.Items)
                if (li.Selected)
                    strUserList.Add(li.Value);
        }
        else
        {
            //必须给指定的人员发送
            foreach (ListItem li in chkbxlstSelectUsers.Items)
                if (li.Selected)
                    strUserList.Add(li.Value);
        }
        return strUserList;
    }

    /// <summary>
    /// 将UserID(一定是ID，不是Name)填充至目标列表，以供多用户模式使用；不支持空值，不支持错误值
    /// 只有启用了多用户模式，此方法才有效
    /// </summary>
    /// <param name="strUserID"></param>
    public void MoreDealUserForAdd(string strUserID)
    {
        List<string> strUserList = MoreDealUserList;
        strUserList.Add(strUserID);
        MoreDealUserList = strUserList;
    }
    /// <summary>
    /// 将UserID从目标序列中移除掉
    /// 只有启用了多用户模式，此方法才有效
    /// </summary>
    /// <param name="strUserID"></param>
    public void MoreDealUserForRemove(string strUserID)
    {
        List<string> strUserList = MoreDealUserList;
        strUserList.Remove(strUserID);
        MoreDealUserList = strUserList;

    }
    /// <summary>
    /// 将UserID的序列，以供多用户模式使用；每个UserID必须合法，由于非法数据造成的错误，由各业务块自行处理
    /// 只有启用了多用户模式，此方法才有效
    /// </summary>
    /// <param name="strUserIDList">用户序列 </param>
    public void MoreDealUserForAddList(List<string> strUserIDList)
    {
        List<string> strUserList = MoreDealUserList;
        foreach (string strTemp in strUserIDList)
            strUserList.Add(strTemp);
        MoreDealUserList = strUserList;
    }

    /// <summary>
    /// 根据TASK_ID获取当前环节配置明细信息 胡方扬
    /// </summary>
    /// <returns></returns>
    public TWfSettingTaskVo GetTaskOrder()
    {
        TWfSettingTaskVo objitems = new TWfSettingTaskLogic().Details(TASK_ID);
        return objitems;
    }

    #region 委托书业务流程统一处理方式 胡方扬 2013-03-28 创建

    /// <summary>
    /// 委托书业务处理函数，提供委托书各种情况下数据的处理方法，后续可补充
    /// </summary>
    /// <param name="strTaskId">业务ID</param>
    /// <param name="strCompanyId">企业ID</param>
    /// <param name="strBtnType">按钮类别 send OR back</param>
    /// <param name="strConfigSetting">配置值，暂时不用</param>
    public void DoContractTaskWF(string strTaskId, string strCompanyId, string strBtnType, string strConfigSetting)
    {
        //前台多环节选择跳转，加载用户时，使用无刷新加载对应环节用户，此时需使用工作流多用户模式，将前提无刷新页面用户传入后台
        string strHidSelectedUserList = this.hidSelectUserId.Value.ToString();
        if (!String.IsNullOrEmpty(strHidSelectedUserList))
        {
            //开启多用户模式
            SetMoreDealUserFlag = true;
            //加载用户列表到多用户模式用户容器
            MoreDealUserForAdd(strHidSelectedUserList);
        }
        //获取当前环节处于第几环节
        TWfSettingTaskVo orderItems = GetTaskOrder();
        //发送情况下业务数据处理
        if (!String.IsNullOrEmpty(strTaskId) && strBtnType == "send")
        {
            TMisContractVo objItems = new TMisContractVo();
            objItems.ID = strTaskId;
            //如果是第一环节下发送流程，则修改委托书状态为1，即处于流转状态
            if (IS_WF_START)
            {
                objItems.CONTRACT_STATUS = "1";
                if (new TMisContractLogic().Edit(objItems))
                {
                    SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
                }
                else
                {
                    // new PageBase().LigerDialogAlert("委托书审批流程异常导致失败！", PageBase.DialogMold.warn.ToString());
                    return;
                }
            }
            //如果是最后一个环节，发送后修改委托书状态为9
            if (IS_WF_END_STEP)
            {
                //0表示未提交 1表示已提交 流转中，9表示已审核，中间其他数字待议，留做备用
                objItems.CONTRACT_STATUS = "9";
                if (new TMisContractLogic().Edit(objItems))
                {
                    //最后一步进行委托书状态更新 表示走完流程
                    if (CreateContractPlan(strTaskId, strCompanyId, strConfigSetting))
                    {
                        SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    // new PageBase().LigerDialogAlert("委托书审批流程异常导致失败！", PageBase.DialogMold.warn.ToString());
                    return;
                }
            }
            //如果为非第一环节和最后一环节，状态任然为1。
            else if (!IS_WF_START && !IS_WF_END_STEP)
            {
                objItems.CONTRACT_STATUS = "1";
                if (new TMisContractLogic().Edit(objItems))
                {
                    SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
                }
            }
        }
        //退回情况下业务数据处理
        if (!String.IsNullOrEmpty(strTaskId) && strBtnType == "back")
        {
            TMisContractVo objItems = new TMisContractVo();
            objItems.ID = strTaskId;
            //如果是第一环节，也就是第一个节点 退回时，不允许退回
            if (IS_WF_START)
            {
                new PageBase().LigerDialogAlert("当前环节不允许退回！", PageBase.DialogMold.warn.ToString());
                return;
            }
            //如果为最后一个环节，允许退回到上一环节，状态任然修改为1，在没办结情况下，默认已经是1
            if (IS_WF_END_STEP)
            {
                objItems.CONTRACT_STATUS = "1";
                if (new TMisContractLogic().Edit(objItems))
                {
                    SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
                }
            }
            //如果当前环节为第2环节，则退回时，修改委托书状态为0
            if (orderItems.TASK_ORDER == "2")
            {
                objItems.CONTRACT_STATUS = "00";
                if (new TMisContractLogic().Edit(objItems))
                {
                    SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
                }
            }
            //其他情况下，即非第一和最后一环节，不修改委托书流程状态
            else if (!IS_WF_START && !IS_WF_END_STEP && orderItems.TASK_ORDER != "2")
            {
                SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
            }
        }
    }

    /// <summary>
    /// 自送样委托书处理
    /// </summary>
    /// <param name="strTaskId">业务ID</param>
    /// <param name="strCompanyId">企业ID</param>
    /// <param name="strBtnType">按钮类别 send OR back</param>
    /// <param name="strConfigSetting">配置值，暂时不用</param>
    /// <param name="strTaskType">委托书类型</param>
    public void DoContractTaskWF(string strTaskId, string strCompanyId, string strBtnType, string strConfigSetting, string strTaskType)
    {
        //前台多环节选择跳转，加载用户时，使用无刷新加载对应环节用户，此时需使用工作流多用户模式，将前提无刷新页面用户传入后台
        string strHidSelectedUserList = this.hidSelectUserId.Value.ToString();
        if (!String.IsNullOrEmpty(strHidSelectedUserList))
        {
            //开启多用户模式
            SetMoreDealUserFlag = true;
            //加载用户列表到多用户模式用户容器
            MoreDealUserForAdd(strHidSelectedUserList);
        }
        //获取当前环节处于第几环节
        TWfSettingTaskVo orderItems = GetTaskOrder();
        //发送情况下业务数据处理
        if (!String.IsNullOrEmpty(strTaskId) && strBtnType == "send")
        {
            TMisContractVo objItems = new TMisContractVo();
            objItems.ID = strTaskId;
            //如果是第一环节下发送流程，则修改委托书状态为1，即处于流转状态
            if (IS_WF_START)
            {
                objItems.CONTRACT_STATUS = "1";
                if (new TMisContractLogic().Edit(objItems))
                {
                    SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
                }
                else
                {
                    // new PageBase().LigerDialogAlert("委托书审批流程异常导致失败！", PageBase.DialogMold.warn.ToString());
                    return;
                }
            }
            //如果是最后一个环节，发送后修改委托书状态为9
            if (IS_WF_END_STEP)
            {
                //0表示未提交 1表示已提交 流转中，9表示已审核，中间其他数字待议，留做备用
                objItems.CONTRACT_STATUS = "9";
                if (new TMisContractLogic().Edit(objItems))
                {
                    TMisContractVo ObjDetail = new TMisContractLogic().Details(strTaskId);
                    //最后一步进行委托书状态更新 表示走完流程
                    if (CreateSamplePlan(strTaskId, ObjDetail.SAMPLE_FREQ))
                    {
                        SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    // new PageBase().LigerDialogAlert("委托书审批流程异常导致失败！", PageBase.DialogMold.warn.ToString());
                    return;
                }
            }
            //如果为非第一环节和最后一环节，状态任然为1。
            else if (!IS_WF_START && !IS_WF_END_STEP)
            {
                objItems.CONTRACT_STATUS = "1";
                if (new TMisContractLogic().Edit(objItems))
                {
                    SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
                }
            }
        }
        //退回情况下业务数据处理
        if (!String.IsNullOrEmpty(strTaskId) && strBtnType == "back")
        {
            TMisContractVo objItems = new TMisContractVo();
            objItems.ID = strTaskId;
            //如果是第一环节，也就是第一个节点 退回时，不允许退回
            if (IS_WF_START)
            {
                new PageBase().LigerDialogAlert("当前环节不允许退回！", PageBase.DialogMold.warn.ToString());
                return;
            }
            //如果为最后一个环节，允许退回到上一环节，状态任然修改为1，在没办结情况下，默认已经是1
            if (IS_WF_END_STEP)
            {
                objItems.CONTRACT_STATUS = "1";
                if (new TMisContractLogic().Edit(objItems))
                {
                    SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
                }
            }
            //如果当前环节为第2环节，则退回时，修改委托书状态为0
            if (orderItems.TASK_ORDER == "2")
            {
                objItems.CONTRACT_STATUS = "0";
                if (new TMisContractLogic().Edit(objItems))
                {
                    SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
                }
            }
            //其他情况下，即非第一和最后一环节，不修改委托书流程状态
            else if (!IS_WF_START && !IS_WF_END_STEP && orderItems.TASK_ORDER != "2")
            {
                SaveInstStepServiceData("委托书ID", "task_id", strTaskId);
            }
        }

    }

    /// <summary>
    /// 获取委托书监测点位
    /// </summary>
    /// <param name="strTaskId">委托书业务ID</param>
    /// <returns></returns>
    public DataTable GetMonitorPoint(string strTaskId)
    {
        DataTable dt = new DataTable();
        TMisContractPointVo objItems = new TMisContractPointVo();
        objItems.CONTRACT_ID = strTaskId;

        dt = new TMisContractPointLogic().SelectByTable(objItems);

        return dt;
    }

    /// <summary>
    /// 插入监测计划信息
    /// </summary>
    /// <returns></returns>
    public bool CreateContractPlan(string strTaskId, string strCompanyId, string strConfigSetting)
    {
        string strSampleDay = "1";
        bool flag = false;
        DataTable dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.CONTRACT_ID = strTaskId;
        //获取当前委托书监测点位
        dt = GetMonitorPoint(strTaskId);
        #region 清远点位频次处理方式
        DataTable objDtF =PageBase.getDictList("FreqTask");
        if (objDtF.Rows.Count > 0 && objDtF.Rows[0]["DICT_CODE"].ToString() == "1")
        {
            //if (objDtF.Rows[0]["DICT_CODE"].ToString() == "1")
            //{
                DataTable dtDict = PageBase.getDictList("Freq");
                DataTable objDtContractType = PageBase.getDictList("FreqMonitor");
                string strContractTypeId = "";
                if (objDtContractType.Rows.Count > 0)
                {
                    strContractTypeId = objDtContractType.Rows[0]["DICT_CODE"].ToString();
                }
                TMisContractVo objTmisContract = new TMisContractLogic().Details(strTaskId);
                if (objDtF.Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(strContractTypeId) && strContractTypeId != "0" && (strContractTypeId.IndexOf(objTmisContract.CONTRACT_TYPE) >= 0))
                    {
                        if (dt != null && dtDict != null)
                        {
                            if (new TMisContractPointFreqLogic().CreatePlanPoint(dt, dtDict, strTaskId))
                            {
                                flag = true;
                            }
                        }
                    }
                    else {
                        if (dt != null)
                        {
                            if (new TMisContractPointFreqLogic().CreatePlanPoint(dt, strTaskId))
                            {
                                //如果为清远，则暂时使用原来方式
                                if (!String.IsNullOrEmpty(strConfigSetting) && strConfigSetting == "1")
                                {
                                    flag = true;
                                }
                                else
                                {
                                    if (dt.Rows.Count > 0)
                                        strSampleDay = dt.Rows[0]["SAMPLE_DAY"].ToString();
                                    if (SavePlanInfor(strTaskId, strCompanyId))
                                    {
                                        flag = true;
                                    }
                                }
                            }
                        }
                    }
                }
            //}
        }

        #endregion

        #region 其他地方点位处理方式
        else
        {
            if (dt != null)
            {
                if (new TMisContractPointFreqLogic().CreatePlanPoint(dt, strTaskId))
                {
                    //如果为清远，则暂时使用原来方式
                    if (!String.IsNullOrEmpty(strConfigSetting) && strConfigSetting == "1")
                    {
                        flag = true;
                    }
                    else
                    {
                        if (SavePlanInfor(strTaskId, strCompanyId))
                        {
                            flag = true;
                        }
                    }
                }
            }
        }
        #endregion
        return flag;
    }

    /// <summary>
    /// 保存监测预约计划 胡方扬 2013-03-27
    /// </summary>
    /// <returns></returns>
    public bool SavePlanInfor(string strTaskId, string strCompanyId)
    {
        bool flag = false;
        DataTable dt = new DataTable();
        TMisContractPlanVo objItems = new TMisContractPlanVo();
        if (!String.IsNullOrEmpty(strTaskId))
        {
            objItems.ID = PageBase.GetSerialNumber("t_mis_contract_planId");
            objItems.CONTRACT_ID = strTaskId;
            dt = new TMisContractPlanLogic().SelectMaxPlanNum(objItems);
            objItems.CONTRACT_COMPANY_ID = strCompanyId;

            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["NUM"].ToString()) && PageBase.IsNumeric(dt.Rows[0]["NUM"].ToString()))
            {
                objItems.PLAN_NUM = (Convert.ToInt32(dt.Rows[0]["NUM"].ToString()) + 1).ToString();
            }
            else
            {
                objItems.PLAN_NUM = "1";
            }
            if (new TMisContractPlanLogic().Create(objItems))
            {
                if (SavePlanPoint(strTaskId, objItems.ID))
                {
                    SavePlanPeopleAnsy(strTaskId, objItems.ID);


                    if (doPlanTask(objItems.ID))
                    {
                        flag = true;
                    }
                    //if (SavePlanPeople(strTaskId, objItems.ID))
                    //{
                    //    if (doPlanTask(objItems.ID))
                    //    {
                    //        flag = true;
                    //    }
                    //}
                }
            }
        }
        return flag;
    }

    /// <summary>
    /// 插入监测任务预约点位表信息  胡方扬 2013-03-27
    /// </summary>
    /// <returns></returns>
    public bool SavePlanPoint(string strTaskId, string strPlanId)
    {
        bool flag = false;
        if (new TMisContractPlanPointLogic().SavePlanPoint(strTaskId, strPlanId))
        {
            flag = true;
        }
        return flag;
    }

    /// <summary>
    /// 获取委托书下下有监测点位的所有监测类别 Create By Castle (胡方扬) 2013-4-1
    /// </summary>
    /// <returns></returns>
    public DataTable GetPointMonitorInfor(string strTaskId, string strPlanId)
    {

        DataTable dt = new DataTable();
        TMisContractPointFreqVo objItems = new TMisContractPointFreqVo();
        objItems.CONTRACT_ID = strTaskId;
        objItems.IF_PLAN = "0";
        dt = new TMisContractPointFreqLogic().GetPointMonitorInforForPlan(objItems, strPlanId);
        return dt;
    }

    /// <summary>
    /// 获取指定监测类别的岗位职责信息 Create By Castle (胡方扬) 2013-4-1
    /// </summary>
    /// <returns></returns>
    public DataTable GetMonitorDutyInfor()
    {
        DataTable dt = new DataTable();
        TSysDutyVo objItems = new TSysDutyVo();
        objItems.DICT_CODE = "duty_sampling";
        dt = new TSysDutyLogic().SelectTableDutyUser(objItems);
        return dt;
    }

    /// <summary>
    /// 保存默认监测计划岗位负责人 Create By Castle (胡方扬) 2013-4-1
    /// </summary>
    /// <returns></returns>
    private bool SavePlanPeople(string strTaskId, string strPlanId)
    {
        bool flag = false;
        DataTable dtMonitor = GetPointMonitorInfor(strTaskId, strPlanId);
        DataTable dtTemple = GetMonitorDutyInfor();
        DataTable dtMonitorDutyUser = new DataTable();
        dtMonitorDutyUser = dtTemple.Copy();
        dtMonitorDutyUser.Clear();
        //获取默认负责人
        DataRow[] drowArr = dtTemple.Select(" IF_DEFAULT='0'");

        if (drowArr.Length > 0)
        {
            foreach (DataRow drow in drowArr)
            {
                dtMonitorDutyUser.ImportRow(drow);
            }
            dtMonitorDutyUser.AcceptChanges();
        }
        string strMonitorId = "", strUserId = "";
        foreach (DataRow drr in dtMonitor.Rows)
        {
            for (int i = 0; i < dtMonitorDutyUser.Rows.Count; i++)
            {
                if (drr["ID"].ToString() == dtMonitorDutyUser.Rows[i]["MONITOR_TYPE_ID"].ToString())
                {
                    strMonitorId += drr["ID"].ToString() + ";";
                    strUserId += dtMonitorDutyUser.Rows[i]["USERID"].ToString() + ";";
                }
            }
        }
        TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
        if (!String.IsNullOrEmpty(strTaskId))
        {
            objItems.CONTRACT_ID = strTaskId;
            objItems.CONTRACT_PLAN_ID = strPlanId;
            string[] strMonitArr = null, strUserArr = null;
            if (!String.IsNullOrEmpty(strMonitorId) && !String.IsNullOrEmpty(strUserId))
            {
                strMonitArr = strMonitorId.Substring(0, strMonitorId.Length - 1).Split(';');
                strUserArr = strUserId.Substring(0, strUserId.Length - 1).Split(';');
                if (strMonitArr != null && strUserArr != null)
                    if (new TMisContractUserdutyLogic().SaveContractPlanDuty(objItems, strMonitArr, strUserArr))
                    {
                        flag = true;
                    }
            }
        }
        return flag;
    }

    /// <summary>
    /// 保存默认监测计划岗位负责人 Create By Castle (胡方扬) 2013-4-1 不需返回值
    /// </summary>
    /// <returns></returns>
    private void SavePlanPeopleAnsy(string strTaskId, string strPlanId)
    {
        bool flag = false;
        DataTable dtMonitor = GetPointMonitorInfor(strTaskId, strPlanId);
        DataTable dtTemple = GetMonitorDutyInfor();
        DataTable dtMonitorDutyUser = new DataTable();
        dtMonitorDutyUser = dtTemple.Copy();
        dtMonitorDutyUser.Clear();
        //获取默认负责人
        DataRow[] drowArr = dtTemple.Select(" IF_DEFAULT='0'");

        if (drowArr.Length > 0)
        {
            foreach (DataRow drow in drowArr)
            {
                dtMonitorDutyUser.ImportRow(drow);
            }
            dtMonitorDutyUser.AcceptChanges();
        }
        string strMonitorId = "", strUserId = "";
        foreach (DataRow drr in dtMonitor.Rows)
        {
            for (int i = 0; i < dtMonitorDutyUser.Rows.Count; i++)
            {
                if (drr["ID"].ToString() == dtMonitorDutyUser.Rows[i]["MONITOR_TYPE_ID"].ToString())
                {
                    strMonitorId += drr["ID"].ToString() + ";";
                    strUserId += dtMonitorDutyUser.Rows[i]["USERID"].ToString() + ";";
                }
            }
        }
        TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
        if (!String.IsNullOrEmpty(strTaskId))
        {
            objItems.CONTRACT_ID = strTaskId;
            objItems.CONTRACT_PLAN_ID = strPlanId;
            string[] strMonitArr = null, strUserArr = null;
            if (!String.IsNullOrEmpty(strMonitorId) && !String.IsNullOrEmpty(strUserId))
            {
                strMonitArr = strMonitorId.Substring(0, strMonitorId.Length - 1).Split(';');
                strUserArr = strUserId.Substring(0, strUserId.Length - 1).Split(';');
                if (strMonitArr != null && strUserArr != null)
                    if (new TMisContractUserdutyLogic().SaveContractPlanDuty(objItems, strMonitArr, strUserArr))
                    {
                        flag = true;
                    }
            }
        }
    }

    /// <summary>
    /// 常规预约办理（任务、报告、样品等生成）
    /// </summary>
    /// <param name="strPlanID">预约ID</param>
    /// <returns>返回true Or false</returns>
    protected bool doPlanTask(string strPlanID)
    {
        bool strReturn = false;
        string strTaskFreqType = "0";
        strTaskFreqType = System.Configuration.ConfigurationManager.AppSettings["TaskFreqType"].ToString();
        //预约表对象
        TMisContractPlanVo objContractPlan = new TMisContractPlanLogic().Details(strPlanID);
        if (objContractPlan != null)
        {
            //获取委托书信息
            TMisContractVo objContract = new TMisContractLogic().Details(objContractPlan.CONTRACT_ID);

            #region 构造监测任务对象
            TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
            PageBase.CopyObject(objContract, objTask);
            objTask.ID = PageBase.GetSerialNumber("t_mis_monitor_taskId");
            //生成任务编号
            TBaseSerialruleVo objSerialTask = new TBaseSerialruleVo();
            objSerialTask.SERIAL_TYPE = "4";

            ////潘德军 2013-12-23 任务单号可改，且初始不生成
            //objTask.TICKET_NUM = PageBase.CreateBaseDefineCode(objSerialTask, objContract);
            string flag = ConfigurationManager.AppSettings["DeptInfor"];
            if (flag == "郑州市环境保护监测站")
            {
                objTask.TICKET_NUM = "未编号";
            }
            else
            {
                objTask.TICKET_NUM = PageBase.CreateBaseDefineCode(objSerialTask, objContract);
            }

            //DataTable dtSerialTask = new TBaseSerialruleLogic().SelectByTable(objSerialTask);
            //foreach (DataRow dr in dtSerialTask.Rows)
            //{
            //    if (dr["SERIAL_TYPE_ID"].ToString().IndexOf(objContract.CONTRACT_TYPE) >= 0)
            //    {
            //        TBaseSerialruleVo objSerialItemsTask = new TBaseSerialruleVo();
            //        string StartSerialNumTask = dr["SERIAL_START_NUM"].ToString();
            //        if (dr["STATUS"].ToString() == "1")
            //        {
            //            objSerialItemsTask.ID = dr["ID"].ToString();
            //            objSerialItemsTask.SERIAL_YEAR = dr["SERIAL_YEAR"].ToString();
            //            if (new TBaseSerialruleLogic().UpdateInitStartNumForNewYear(objSerialItemsTask, DateTime.Now.Year.ToString()))
            //            {
            //                StartSerialNumTask = "1";
            //            }
            //        }
            //        objTask.TICKET_NUM = PageBase.CreateDefineCodeForYear(dr["SERIAL_RULE"].ToString(), Convert.ToInt32(dr["SERIAL_NUMBER_BIT"].ToString()), StartSerialNumTask);

            //        if (!String.IsNullOrEmpty(objTask.TICKET_NUM))
            //        {
            //            //将变号自动加1;
            //            TBaseSerialruleVo objSerialUp = new TBaseSerialruleVo();
            //            objSerialUp.ID = dr["ID"].ToString();
            //            new TBaseSerialruleLogic().UpdateSerialNum(objSerialUp);
            //        }
            //    }
            //}
            objTask.CONTRACT_ID = objContract.ID;
            objTask.PLAN_ID = strPlanID;
            objTask.CONSIGN_DATE = objContract.ASKING_DATE;
            objTask.CREATOR_ID = new PageBase().LogInfo.UserInfo.ID;
            objTask.CREATE_DATE = DateTime.Now.ToString();
            objTask.TASK_STATUS = "01";

            //update by ssz 增加默认的确认状态
            objTask.COMFIRM_STATUS = "0";
            #endregion

            #region 构造监测任务委托企业信息
            //委托企业信息
            TMisContractCompanyVo objContractClient = new TMisContractCompanyLogic().Details(objContract.CLIENT_COMPANY_ID);
            //受检企业信息
            TMisContractCompanyVo objContractTested = new TMisContractCompanyLogic().Details(objContract.TESTED_COMPANY_ID);
            //构造监测任务委托企业信息
            TMisMonitorTaskCompanyVo objTaskClient = new TMisMonitorTaskCompanyVo();
            PageBase.CopyObject(objContractClient, objTaskClient);//复制对象
            objTaskClient.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
            objTaskClient.TASK_ID = objTask.ID;
            objTaskClient.COMPANY_ID = objContract.CLIENT_COMPANY_ID;
            //构造监测任务受检企业信息
            TMisMonitorTaskCompanyVo objTaskTested = new TMisMonitorTaskCompanyVo();
            PageBase.CopyObject(objContractTested, objTaskTested);//复制对象
            objTaskTested.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
            objTaskTested.TASK_ID = objTask.ID;
            objTaskTested.COMPANY_ID = objContract.TESTED_COMPANY_ID;

            //重新赋值监测任务企业ID
            objTask.CLIENT_COMPANY_ID = objTaskClient.ID;
            objTask.TESTED_COMPANY_ID = objTaskTested.ID;
            #endregion

            #region 监测报告 胡方扬 2013-04-23 Modify  将报告记录初始化生成数据移到委托书办理完毕后就生成
            TMisMonitorReportVo objReportVo = new TMisMonitorReportVo();
            objReportVo.ID = PageBase.GetSerialNumber("t_mis_monitor_report_id");
            //objReportVo.REPORT_CODE = objContract.CONTRACT_CODE;
            //生成报告编号  胡方扬 2013-04-24
            //TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
            //objSerial.SERIAL_TYPE = "3";
            //objReportVo.REPORT_CODE = PageBase.CreateBaseDefineCode(objSerial, objContract);
            DataTable objDt = PageBase.getDictList("RptISWT_Code");
            if (objDt.Rows.Count > 0)
            {
                if (objDt.Rows[0]["DICT_CODE"].ToString() == "1")
                {
                    objReportVo.REPORT_CODE = objTask.TICKET_NUM;
                }
            }

            //DataTable dtSerial = new TBaseSerialruleLogic().SelectByTable(objSerial);
            //foreach (DataRow dr in dtSerial.Rows)
            //{
            //    if (dr["SERIAL_TYPE_ID"].ToString().IndexOf(objContract.CONTRACT_TYPE) >= 0)
            //    {
            //        TBaseSerialruleVo objSerialItems = new TBaseSerialruleVo();
            //        string StartSerialNum = dr["SERIAL_START_NUM"].ToString();
            //        if (dr["STATUS"].ToString() == "1")
            //        {
            //            objSerialItems.ID = dr["ID"].ToString();
            //            objSerialItems.SERIAL_YEAR = dr["SERIAL_YEAR"].ToString();
            //            if (new TBaseSerialruleLogic().UpdateInitStartNumForNewYear(objSerialItems, DateTime.Now.Year.ToString()))
            //            {
            //                StartSerialNum = "1";
            //            }
            //        }
            //        objReportVo.REPORT_CODE = PageBase.CreateDefineCodeForYear(dr["SERIAL_RULE"].ToString(), Convert.ToInt32(dr["SERIAL_NUMBER_BIT"].ToString()), StartSerialNum);

            //        if (!String.IsNullOrEmpty(objReportVo.REPORT_CODE))
            //        {
            //            //将变号自动加1;
            //            TBaseSerialruleVo objSerialUp = new TBaseSerialruleVo();
            //            objSerialUp.ID = dr["ID"].ToString();
            //            new TBaseSerialruleLogic().UpdateSerialNum(objSerialUp);
            //        }
            //    }
            //}
            objReportVo.TASK_ID = objTask.ID;
            objReportVo.IF_GET = "0";
            #endregion

            #region 监测子任务信息 根据委托书监测类别进行构造
            //监测子任务信息 根据委托书监测类别进行构造
            ArrayList arrSubTask = new ArrayList();//监测子任务集合
            ArrayList arrTaskPoint = new ArrayList();//监测点位集合
            ArrayList arrPointItem = new ArrayList();//监测点位明细集合
            ArrayList arrSample = new ArrayList();//样品集合
            ArrayList arrSampleResult = new ArrayList();//样品结果集合 
            ArrayList arrSampleResultApp = new ArrayList();//样品分析执行表
            #endregion

            //监测类别集合
            DataTable dtTestType = new TMisContractPointLogic().GetTestType(strPlanID);
            //预约点位
            DataTable dtPoint = new TMisContractPointLogic().SelectPointTable(strPlanID);
            //获取预约点位明细信息
            DataTable dtContractPoint = new TMisContractPointLogic().SelectByTableForPlan(strPlanID);
            //监测子任务
            #region 监测子任务
            if (dtTestType.Rows.Count > 0)
            {
                string strSubTaskIDs = new PageBase().GetSerialNumberList("t_mis_monitor_subtaskId", dtTestType.Rows.Count);
                string[] arrSubTaskIDs = strSubTaskIDs.Split(',');
                for (int i = 0; i < dtTestType.Rows.Count; i++)
                {
                    string str = dtTestType.Rows[i]["MONITOR_ID"].ToString();//监测类别
                    if (str.Length > 0)
                    {
                        #region 监测子任务
                        //监测子任务
                        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
                        string strSampleManagerID = "";//采样负责人ID
                        string strSampleID = "";//采样协同人ID串
                        GetSamplingMan(str, objContract.ID, ref strSampleManagerID, ref strSampleID);
                        objSubtask.ID = arrSubTaskIDs[i];
                        objSubtask.TASK_ID = objTask.ID;
                        objSubtask.MONITOR_ID = str;
                        if (objContract.PROJECT_ID != "")
                        {
                            objSubtask.SAMPLING_MANAGER_ID = objContract.PROJECT_ID;
                        }
                        else
                        {
                            objSubtask.SAMPLING_MANAGER_ID = strSampleManagerID;
                        }
                        objSubtask.SAMPLING_ID = strSampleID;
                        objSubtask.TASK_TYPE = "发送";
                        objSubtask.TASK_STATUS = "01";
                        arrSubTask.Add(objSubtask);
                        #endregion

                        #region 按类别分点位
                        //按类别分点位
                        DataRow[] dtTypePoint = dtPoint.Select("MONITOR_ID='" + str + "'");
                        if (dtTypePoint.Length > 0)
                        {
                            string strTaskPointIDs = new PageBase().GetSerialNumberList("t_mis_monitor_taskpointId", dtTypePoint.Length);
                            string[] arrTaskPointIDs = strTaskPointIDs.Split(',');
                            string strSampleIDs = new PageBase().GetSerialNumberList("MonitorSampleId", dtTypePoint.Length);
                            string[] arrSampleIDs = strSampleIDs.Split(',');

                            for (int j = 0; j < dtTypePoint.Length; j++)
                            {
                                DataRow drPoint = dtTypePoint[j];
                                #region 监测点位
                                // 监测点位 
                                TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointVo();
                                objTaskPoint.ID = arrTaskPointIDs[j];
                                objTaskPoint.TASK_ID = objTask.ID;
                                objTaskPoint.SUBTASK_ID = objSubtask.ID;
                                objTaskPoint.POINT_ID = drPoint["POINT_ID"].ToString();
                                objTaskPoint.MONITOR_ID = str;
                                objTaskPoint.COMPANY_ID = objTaskTested.ID;
                                objTaskPoint.CONTRACT_POINT_ID = drPoint["ID"].ToString();
                                objTaskPoint.POINT_NAME = drPoint["POINT_NAME"].ToString();
                                objTaskPoint.DYNAMIC_ATTRIBUTE_ID = drPoint["DYNAMIC_ATTRIBUTE_ID"].ToString();
                                objTaskPoint.ADDRESS = drPoint["ADDRESS"].ToString();
                                objTaskPoint.LONGITUDE = drPoint["LONGITUDE"].ToString();
                                objTaskPoint.LATITUDE = drPoint["LATITUDE"].ToString();
                                objTaskPoint.FREQ = drPoint["FREQ"].ToString();
                                objTaskPoint.DESCRIPTION = drPoint["DESCRIPTION"].ToString();
                                objTaskPoint.NATIONAL_ST_CONDITION_ID = drPoint["NATIONAL_ST_CONDITION_ID"].ToString();
                                objTaskPoint.INDUSTRY_ST_CONDITION_ID = drPoint["INDUSTRY_ST_CONDITION_ID"].ToString();
                                objTaskPoint.LOCAL_ST_CONDITION_ID = drPoint["LOCAL_ST_CONDITION_ID"].ToString();
                                objTaskPoint.IS_DEL = "0";
                                objTaskPoint.NUM = drPoint["NUM"].ToString();
                                objTaskPoint.CREATE_DATE = DateTime.Now.ToString();
                                arrTaskPoint.Add(objTaskPoint);
                                #endregion

                                //点位采用的标准条件项ID
                                string strConditionID = "";
                                if (!string.IsNullOrEmpty(objTaskPoint.NATIONAL_ST_CONDITION_ID))
                                {
                                    strConditionID = objTaskPoint.NATIONAL_ST_CONDITION_ID;
                                }
                                if (!string.IsNullOrEmpty(objTaskPoint.LOCAL_ST_CONDITION_ID))
                                {
                                    strConditionID = objTaskPoint.LOCAL_ST_CONDITION_ID;
                                }
                                if (!string.IsNullOrEmpty(objTaskPoint.INDUSTRY_ST_CONDITION_ID))
                                {
                                    strConditionID = objTaskPoint.INDUSTRY_ST_CONDITION_ID;
                                }

                                //计算采样频次

                                DataTable dtDict = PageBase.getDictList("is_Zhengzhou");//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
                                string strIsZhengzhou = "";
                                if (dtDict.Rows.Count > 0)
                                {
                                    strIsZhengzhou = dtDict.Rows[0]["DICT_CODE"].ToString();
                                }

                                int intFreq = 1, intSampleDay = 1;
                                if (drPoint["FREQ"].ToString().Length > 0)
                                {
                                    intFreq = int.Parse(drPoint["FREQ"].ToString());
                                }
                                if (strIsZhengzhou == "1")//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
                                {
                                    if (drPoint["SAMPLE_FREQ"].ToString().Length > 0)
                                    {
                                        intFreq = int.Parse(drPoint["SAMPLE_FREQ"].ToString());
                                    }
                                }
                                //计算周期
                                if (drPoint["SAMPLE_DAY"].ToString().Length > 0)
                                {
                                    intSampleDay = int.Parse(drPoint["SAMPLE_DAY"].ToString());
                                }
                                #region 样品信息、结果、结果执行
                                #region 如果 strTaskFreqType 判断为0
                                if (!String.IsNullOrEmpty(strTaskFreqType) && strTaskFreqType == "0")
                                {
                                    #region 样品
                                    //样品 与点位对应
                                    TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
                                    objSampleInfo.ID = arrSampleIDs[j];
                                    objSampleInfo.SUBTASK_ID = objSubtask.ID;
                                    objSampleInfo.POINT_ID = objTaskPoint.ID;
                                    objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME;
                                    objSampleInfo.QC_TYPE = "0";//默认原始样
                                    objSampleInfo.NOSAMPLE = "0";//默认未采样
                                    arrSample.Add(objSampleInfo);
                                    #endregion

                                    //预约项目明细
                                    DataRow[] dtPointItem = dtContractPoint.Select("CONTRACT_POINT_ID=" + drPoint["ID"].ToString());
                                    if (dtPointItem.Length > 0)
                                    {
                                        string strTaskItemIDs = new PageBase().GetSerialNumberList("t_mis_monitor_task_item_id", dtPointItem.Length);
                                        string[] arrTaskItemIDs = strTaskItemIDs.Split(',');
                                        string strSampleResultIDs = new PageBase().GetSerialNumberList("MonitorResultId", dtPointItem.Length);
                                        string[] arrSampleResultIDs = strSampleResultIDs.Split(',');
                                        string strResultAppIDs = new PageBase().GetSerialNumberList("MonitorResultAppId", dtPointItem.Length);
                                        string[] arrResultAppIDs = strResultAppIDs.Split(',');

                                        for (int k = 0; k < dtPointItem.Length; k++)
                                        {
                                            DataRow drPointItem = dtPointItem[k];
                                            //项目采用的标准上限、下限
                                            string strUp = "";
                                            string strLow = "";
                                            string strConditionType = "";
                                            getStandardValue(drPointItem["ITEM_ID"].ToString(), strConditionID, ref strUp, ref strLow, ref strConditionType);
                                            #region 构造监测任务点位明细表
                                            //构造监测任务点位明细表
                                            TMisMonitorTaskItemVo objMonitorTaskItem = new TMisMonitorTaskItemVo();
                                            objMonitorTaskItem.ID = arrTaskItemIDs[k];
                                            objMonitorTaskItem.TASK_POINT_ID = objTaskPoint.ID;
                                            objMonitorTaskItem.ITEM_ID = drPointItem["ITEM_ID"].ToString();
                                            objMonitorTaskItem.CONDITION_ID = strConditionID;
                                            objMonitorTaskItem.CONDITION_TYPE = strConditionType;
                                            objMonitorTaskItem.ST_UPPER = strUp;
                                            objMonitorTaskItem.ST_LOWER = strLow;
                                            objMonitorTaskItem.IS_DEL = "0";
                                            arrPointItem.Add(objMonitorTaskItem);
                                            #endregion

                                            #region 构造样品结果表
                                            //构造样品结果表
                                            string strAnalysisID = "";//分析方法ID
                                            string strStandardID = "";//方法依据ID
                                            string strCheckOut = ""; //检出限
                                            getMethod(drPointItem["ITEM_ID"].ToString(), ref strAnalysisID, ref strStandardID, ref strCheckOut);
                                            TMisMonitorResultVo objSampleResult = new TMisMonitorResultVo();
                                            objSampleResult.ID = arrSampleResultIDs[k];
                                            objSampleResult.SAMPLE_ID = objSampleInfo.ID;
                                            objSampleResult.QC_TYPE = objSampleInfo.QC_TYPE;
                                            objSampleResult.ITEM_ID = drPointItem["ITEM_ID"].ToString();
                                            objSampleResult.SAMPLING_INSTRUMENT = GetItemSamplingInstrumentID(drPointItem["ITEM_ID"].ToString());
                                            objSampleResult.ANALYSIS_METHOD_ID = strAnalysisID;
                                            objSampleResult.STANDARD_ID = strStandardID;
                                            objSampleResult.RESULT_CHECKOUT = strCheckOut;
                                            objSampleResult.QC = "0";// 默认原始样手段
                                            objSampleResult.TASK_TYPE = "发送";
                                            objSampleResult.RESULT_STATUS = "01";//默认分析结果填报
                                            objSampleResult.PRINTED = "0";//默认未打印
                                            arrSampleResult.Add(objSampleResult);
                                            #endregion

                                            #region 构造样品执行表
                                            //构造样品执行表
                                            TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                                            objResultApp.ID = arrResultAppIDs[k];
                                            objResultApp.RESULT_ID = objSampleResult.ID;
                                            objResultApp.HEAD_USERID = drPointItem["ANALYSIS_MANAGER"].ToString();
                                            objResultApp.ASSISTANT_USERID = drPointItem["ANALYSIS_ID"].ToString();
                                            arrSampleResultApp.Add(objResultApp);
                                            #endregion
                                        }
                                    }
                                }
                                #endregion

                                #region 如果 strTaskFreqType 判断为1
                                if (!String.IsNullOrEmpty(strTaskFreqType) && strTaskFreqType == "1")
                                {
                                    bool bNeedDayAndFreq = true;//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
                                    if (intSampleDay == 1 && intFreq == 1)//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
                                    {
                                        bNeedDayAndFreq = false;
                                    }

                                    for (int r = 0; r < intSampleDay; r++)
                                    {
                                        for (int s = 0; s < intFreq; s++)
                                        {
                                            #region 样品
                                            //样品 与点位对应
                                            TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
                                            objSampleInfo.ID = new PageBase().GetSerialNumberList("MonitorSampleId", 1);
                                            objSampleInfo.SUBTASK_ID = objSubtask.ID;
                                            objSampleInfo.POINT_ID = objTaskPoint.ID;
                                            //objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME;
                                            if (bNeedDayAndFreq)//郑州的，点位信息增加 几天几次，一天一次的无需增加,潘德军 2013-10-25
                                                objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME + " 第" + (r + 1).ToString() + "天" + " 第" + (s + 1).ToString() + "次";
                                            else
                                                objSampleInfo.SAMPLE_NAME = objTaskPoint.POINT_NAME;
                                            objSampleInfo.QC_TYPE = "0";//默认原始样
                                            objSampleInfo.NOSAMPLE = "0";//默认未采样
                                            arrSample.Add(objSampleInfo);
                                            #endregion

                                            //预约项目明细
                                            DataRow[] dtPointItem = dtContractPoint.Select("CONTRACT_POINT_ID=" + drPoint["ID"].ToString());
                                            if (dtPointItem.Length > 0)
                                            {
                                                string strTaskItemIDs = new PageBase().GetSerialNumberList("t_mis_monitor_task_item_id", dtPointItem.Length);
                                                string[] arrTaskItemIDs = strTaskItemIDs.Split(',');
                                                string strSampleResultIDs = new PageBase().GetSerialNumberList("MonitorResultId", dtPointItem.Length);
                                                string[] arrSampleResultIDs = strSampleResultIDs.Split(',');
                                                string strResultAppIDs = new PageBase().GetSerialNumberList("MonitorResultAppId", dtPointItem.Length);
                                                string[] arrResultAppIDs = strResultAppIDs.Split(',');

                                                for (int k = 0; k < dtPointItem.Length; k++)
                                                {
                                                    DataRow drPointItem = dtPointItem[k];
                                                    //项目采用的标准上限、下限
                                                    string strUp = "";
                                                    string strLow = "";
                                                    string strConditionType = "";
                                                    getStandardValue(drPointItem["ITEM_ID"].ToString(), strConditionID, ref strUp, ref strLow, ref strConditionType);
                                                    #region 构造监测任务点位明细表
                                                    //构造监测任务点位明细表
                                                    TMisMonitorTaskItemVo objMonitorTaskItem = new TMisMonitorTaskItemVo();
                                                    objMonitorTaskItem.ID = arrTaskItemIDs[k];
                                                    objMonitorTaskItem.TASK_POINT_ID = objTaskPoint.ID;
                                                    objMonitorTaskItem.ITEM_ID = drPointItem["ITEM_ID"].ToString();
                                                    objMonitorTaskItem.CONDITION_ID = strConditionID;
                                                    objMonitorTaskItem.CONDITION_TYPE = strConditionType;
                                                    objMonitorTaskItem.ST_UPPER = strUp;
                                                    objMonitorTaskItem.ST_LOWER = strLow;
                                                    objMonitorTaskItem.IS_DEL = "0";
                                                    arrPointItem.Add(objMonitorTaskItem);
                                                    #endregion

                                                    #region 构造样品结果表
                                                    //构造样品结果表
                                                    string strAnalysisID = "";//分析方法ID
                                                    string strStandardID = "";//方法依据ID
                                                    string strCheckOut = ""; //检出限
                                                    getMethod(drPointItem["ITEM_ID"].ToString(), ref strAnalysisID, ref strStandardID, ref strCheckOut);
                                                    TMisMonitorResultVo objSampleResult = new TMisMonitorResultVo();
                                                    objSampleResult.ID = arrSampleResultIDs[k];
                                                    objSampleResult.SAMPLE_ID = objSampleInfo.ID;
                                                    objSampleResult.QC_TYPE = objSampleInfo.QC_TYPE;
                                                    objSampleResult.ITEM_ID = drPointItem["ITEM_ID"].ToString();
                                                    objSampleResult.SAMPLING_INSTRUMENT = GetItemSamplingInstrumentID(drPointItem["ITEM_ID"].ToString());
                                                    objSampleResult.ANALYSIS_METHOD_ID = strAnalysisID;
                                                    objSampleResult.STANDARD_ID = strStandardID;
                                                    objSampleResult.RESULT_CHECKOUT = strCheckOut;
                                                    objSampleResult.QC = "0";// 默认原始样手段
                                                    objSampleResult.TASK_TYPE = "发送";
                                                    objSampleResult.RESULT_STATUS = "01";//默认分析结果填报
                                                    objSampleResult.PRINTED = "0";//默认未打印
                                                    arrSampleResult.Add(objSampleResult);
                                                    #endregion

                                                    #region 构造样品执行表
                                                    //构造样品执行表
                                                    TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                                                    objResultApp.ID = arrResultAppIDs[k];
                                                    objResultApp.RESULT_ID = objSampleResult.ID;
                                                    objResultApp.HEAD_USERID = drPointItem["ANALYSIS_MANAGER"].ToString();
                                                    objResultApp.ASSISTANT_USERID = drPointItem["ANALYSIS_ID"].ToString();
                                                    arrSampleResultApp.Add(objResultApp);
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #endregion
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion

            if (new TMisMonitorTaskLogic().SaveTrans(objTask, objTaskClient, objTaskTested, objReportVo, arrTaskPoint, arrSubTask, arrPointItem, arrSample, arrSampleResult, arrSampleResultApp))
            {
                strReturn = true;
            }
        }
        return strReturn;
    }

    /// <summary>
    /// 自送样预约办理（任务、报告、样品等生成）
    /// </summary>
    /// <param name="strTask_Id">委托书业务ID</param>
    /// <param name="dtSamplePlan">预约计划记录表</param>
    /// <returns>返回true Or false</returns>
    protected bool doPlanTaskForSample(string strTask_Id, DataTable dtSamplePlan)
    {
        bool strReturn = false;
        if (dtSamplePlan.Rows.Count > 0)
        {
            for (int m = 0; m < dtSamplePlan.Rows.Count; m++)
            {
                //预约表对象
                string strSamplePlanId = dtSamplePlan.Rows[m]["ID"].ToString();
                TMisContractSamplePlanVo objContractPlan = new TMisContractSamplePlanLogic().Details(strSamplePlanId);
                if (strTask_Id != "")
                {
                    //获取委托书信息
                    TMisContractVo objContract = new TMisContractLogic().Details(strTask_Id);
                    objContract.ID = strTask_Id;
                    #region 构造监测任务对象
                    TMisMonitorTaskVo objTask = new TMisMonitorTaskVo();
                    PageBase.CopyObject(objContract, objTask);
                    objTask.ID = PageBase.GetSerialNumber("t_mis_monitor_taskId");
                    objTask.CONTRACT_ID = objContract.ID;
                    objTask.PLAN_ID = strSamplePlanId;
                    objTask.CONSIGN_DATE = objContract.ASKING_DATE;
                    objTask.CREATOR_ID = new PageBase().LogInfo.UserInfo.ID;
                    objTask.CREATE_DATE = DateTime.Now.ToString();
                    objTask.TASK_STATUS = "01";
                    objTask.SAMPLE_SOURCE = objContract.SAMPLE_SOURCE;
                    objTask.COMFIRM_STATUS = "0";
                    objTask.SAMPLE_SEND_MAN = objContract.SAMPLE_SEND_MAN;
                    //生成任务编号
                    TBaseSerialruleVo objSerialTask = new TBaseSerialruleVo();
                    objSerialTask.SERIAL_TYPE = "4";
                    ////潘德军 2013-12-23 任务单号可改，且初始不生成
                    //objTask.TICKET_NUM = PageBase.CreateBaseDefineCode(objSerialTask, objContract);
                    string flag = ConfigurationManager.AppSettings["DeptInfor"];
                    if (flag == "郑州市环境保护监测站")
                    {
                        objTask.TICKET_NUM = "未编号";
                    }
                    else
                    {
                        objTask.TICKET_NUM = PageBase.CreateBaseDefineCode(objSerialTask, objContract);
                    }
                    #endregion

                    #region 构造监测任务委托企业信息
                    //委托企业信息
                    TMisContractCompanyVo objContractClient = new TMisContractCompanyLogic().Details(objContract.CLIENT_COMPANY_ID);
                    //受检企业信息
                    TMisContractCompanyVo objContractTested = new TMisContractCompanyLogic().Details(objContract.TESTED_COMPANY_ID);
                    //构造监测任务委托企业信息
                    TMisMonitorTaskCompanyVo objTaskClient = new TMisMonitorTaskCompanyVo();
                    PageBase.CopyObject(objContractClient, objTaskClient);//复制对象
                    objTaskClient.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
                    objTaskClient.TASK_ID = objTask.ID;
                    objTaskClient.COMPANY_ID = objContract.CLIENT_COMPANY_ID;
                    //构造监测任务受检企业信息
                    TMisMonitorTaskCompanyVo objTaskTested = new TMisMonitorTaskCompanyVo();
                    PageBase.CopyObject(objContractTested, objTaskTested);//复制对象
                    objTaskTested.ID = PageBase.GetSerialNumber("t_mis_monitor_taskcompanyId");
                    objTaskTested.TASK_ID = objTask.ID;
                    objTaskTested.COMPANY_ID = objContract.TESTED_COMPANY_ID;

                    //重新赋值监测任务企业ID
                    objTask.CLIENT_COMPANY_ID = objTaskClient.ID;
                    objTask.TESTED_COMPANY_ID = objTaskTested.ID;
                    #endregion

                    #region 监测报告
                    TMisMonitorReportVo objReportVo = new TMisMonitorReportVo();
                    objReportVo.ID = PageBase.GetSerialNumber("t_mis_monitor_report_id");
                    //objReportVo.REPORT_CODE = objContract.CONTRACT_CODE;
                    //生成报告编号  胡方扬 2013-04-24
                    TBaseSerialruleVo objSerial = new TBaseSerialruleVo();
                    objSerial.SERIAL_TYPE = "3";
                    //objReportVo.REPORT_CODE = PageBase.CreateBaseDefineCode(objSerial, objContract);
                   
                    objReportVo.TASK_ID = objTask.ID;
                    objReportVo.IF_GET = "0";
                    #endregion

                    #region 监测子任务信息 根据委托书监测类别进行构造
                    //监测子任务信息 根据委托书监测类别进行构造
                    ArrayList arrSubTask = new ArrayList();//监测子任务集合
                    ArrayList arrSample = new ArrayList();//样品集合--取自送样
                    ArrayList arrSampleResult = new ArrayList();//样品结果集合 
                    ArrayList arrSampleResultApp = new ArrayList();//样品分析执行表
                    #endregion

                    //样品类别集合
                    TMisContractSamplePlanVo objSamplePlan = new TMisContractSamplePlanVo();
                    objSamplePlan.ID = strSamplePlanId;
                    DataTable dtTestType = new TMisContractSamplePlanLogic().GetSamplePlanMonitor(objSamplePlan);

                    //获取样品信息
                    TMisContractSampleVo objSampleInfor = new TMisContractSampleVo();
                    objSampleInfor.CONTRACT_ID = strTask_Id;
                    DataTable objDtSampleInfor = new TMisContractSampleLogic().SelectByTable(objSampleInfor);

                    //获取预约点位明细信息
                    DataTable dtContractSamplePlanItems = new TMisContractSamplePlanLogic().GetSamplePlanItems(objSamplePlan);
                    //监测子任务
                    #region 监测子任务
                    if (dtTestType.Rows.Count > 0)
                    {
                        string strSubTaskIDs = new PageBase().GetSerialNumberList("t_mis_monitor_subtaskId", dtTestType.Rows.Count);
                        string[] arrSubTaskIDs = strSubTaskIDs.Split(',');
                        for (int i = 0; i < dtTestType.Rows.Count; i++)
                        {
                            string str = dtTestType.Rows[i]["MONITOR_ID"].ToString();//监测类别
                            if (str.Length > 0)
                            {
                                #region 监测子任务
                                //监测子任务
                                TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskVo();
                                string strSampleManagerID = "";//采样负责人ID
                                string strSampleID = "";//采样协同人ID串
                                GetSamplingMan(str, objContract.ID, ref strSampleManagerID, ref strSampleID);
                                objSubtask.ID = arrSubTaskIDs[i];
                                objSubtask.TASK_ID = objTask.ID;
                                objSubtask.MONITOR_ID = str;
                                objSubtask.SAMPLING_MANAGER_ID = objContract.SAMPLE_ACCEPTER_ID;
                                objSubtask.SAMPLING_ID = strSampleID;
                                //objSubtask.TASK_TYPE = "发送";
                                //objSubtask.TASK_STATUS = "024";
                                arrSubTask.Add(objSubtask);
                                #endregion

                                #region 按类别分样品
                                //监测按类别分样品
                                DataRow[] dtTypePoint = objDtSampleInfor.Select(" MONITOR_ID=" + str);
                                if (dtTypePoint.Length > 0)
                                {
                                    for (int n = 0; n < dtTypePoint.Length; n++)
                                    {
                                        string strSampleIDs = PageBase.GetSerialNumber("MonitorSampleId");

                                        #region 构造样品表数据
                                        //样品 
                                        TMisMonitorSampleInfoVo objSampleInfo = new TMisMonitorSampleInfoVo();
                                        objSampleInfo.ID = strSampleIDs;
                                        objSampleInfo.SUBTASK_ID = objSubtask.ID;
                                        //objSampleInfo.POINT_ID = dtTypePoint[n]["ID"].ToString();
                                        objSampleInfo.SAMPLE_NAME = dtTypePoint[n]["SAMPLE_NAME"].ToString();
                                        objSampleInfo.SAMPLE_TYPE = dtTypePoint[n]["SAMPLE_TYPE"].ToString();
                                        objSampleInfo.SAMPLE_STATUS = dtTypePoint[n]["SAMPLE_STATUS"].ToString();
                                        objSampleInfo.SRC_CODEORNAME = dtTypePoint[n]["SRC_CODEORNAME"].ToString();
                                        objSampleInfo.SAMPLE_ACCEPT_DATEORACC = dtTypePoint[n]["SAMPLE_ACCEPT_DATEORACC"].ToString();
                                        objSampleInfo.QC_TYPE = "0";//默认原始样
                                        objSampleInfo.NOSAMPLE = "0";//默认未采样
                                        arrSample.Add(objSampleInfo);
                                        #endregion

                                        DataRow[] dtTypeItem = dtContractSamplePlanItems.Select("SAMPLE_ID=" + dtTypePoint[n]["ID"].ToString() + "");
                                        string strSampleResultIDs = new PageBase().GetSerialNumberList("MonitorResultId", dtTypeItem.Length);
                                        string[] arrSampleResultIDs = strSampleResultIDs.Split(',');
                                        string strResultAppIDs = new PageBase().GetSerialNumberList("MonitorResultAppId", dtTypeItem.Length);
                                        string[] arrResultAppIDs = strResultAppIDs.Split(',');
                                        for (int k = 0; k < dtTypeItem.Length; k++)
                                        {

                                            #region 构造样品分析结果表数据
                                            //构造样品结果表
                                            string strAnalysisID = "";//分析方法ID
                                            string strStandardID = "";//方法依据ID
                                            string strCheckOut = ""; //检出限
                                            getMethod(dtTypeItem[k]["ITEM_ID"].ToString(), ref strAnalysisID, ref strStandardID, ref strCheckOut);
                                            TMisMonitorResultVo objSampleResult = new TMisMonitorResultVo();
                                            objSampleResult.ID = arrSampleResultIDs[k];
                                            objSampleResult.SAMPLE_ID = objSampleInfo.ID;
                                            objSampleResult.QC_TYPE = objSampleInfo.QC_TYPE;
                                            objSampleResult.ITEM_ID = dtTypeItem[k]["ITEM_ID"].ToString();
                                            objSampleResult.SAMPLING_INSTRUMENT = GetItemSamplingInstrumentID(dtTypeItem[k]["ITEM_ID"].ToString());
                                            objSampleResult.ANALYSIS_METHOD_ID = strAnalysisID;
                                            objSampleResult.STANDARD_ID = strStandardID;
                                            objSampleResult.RESULT_CHECKOUT = strCheckOut;
                                            objSampleResult.QC = "0";// 默认原始样手段
                                            objSampleResult.TASK_TYPE = "发送";
                                            objSampleResult.RESULT_STATUS = "01";//默认分析结果填报
                                            objSampleResult.PRINTED = "0";//默认未打印
                                            arrSampleResult.Add(objSampleResult);
                                            #endregion

                                            #region 构造样品分析结果执行表数据
                                            //构造样品执行表
                                            TMisMonitorResultAppVo objResultApp = new TMisMonitorResultAppVo();
                                            objResultApp.ID = arrResultAppIDs[k];
                                            objResultApp.RESULT_ID = objSampleResult.ID;
                                            objResultApp.HEAD_USERID =GetAnayUser(str,dtTypeItem[k]["ITEM_ID"].ToString(),"duty_analyse",true) ;
                                            objResultApp.ASSISTANT_USERID = GetAnayUser(str, dtTypeItem[k]["ITEM_ID"].ToString(), "duty_analyse", false);
                                            arrSampleResultApp.Add(objResultApp);
                                            #endregion
                                        }

                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion

                    if (new TMisMonitorTaskLogic().SaveSampleTrans(objTask, objTaskClient, objTaskTested, objReportVo, arrSubTask, arrSample, arrSampleResult, arrSampleResultApp))
                    {
                        TMisContractSamplePlanVo objItemSap = new TMisContractSamplePlanVo();
                        objItemSap.ID = strSamplePlanId;
                        objItemSap.IF_PLAN = "1";
                        if (new TMisContractSamplePlanLogic().Edit(objItemSap))
                        {
                            strReturn = true;
                        }
                    }
                }
            }
        }

        return strReturn;
    }

    #region 获取指定监测类别 监测项目 分析负责人 分析协同人 Create By 胡方扬  2013-07-17
    public string GetAnayUser(string strMonitorId, string strItemsId, string strDictCode, bool isHead)
    {
        string result = "";

        //潘德军修改2013-7-19 环境质量的监测类别取对应的污染源类别的岗位职责
        i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo objMonitorType = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(strMonitorId);
        if (objMonitorType.REMARK1.Trim().Length > 0)
        {
            strMonitorId = objMonitorType.REMARK1.Trim();
        }

        TSysDutyVo objDuty = new TSysDutyVo();
        objDuty.DICT_CODE = "duty_analyse";
        objDuty.MONITOR_TYPE_ID = strMonitorId;
        objDuty.MONITOR_ITEM_ID = strItemsId;
        DataTable dt = new TSysDutyLogic().GetDutyUser(objDuty);
        if (dt.Rows.Count > 0)
        {
            if (isHead)
            {
                DataRow[] drArr = dt.Select(" IF_DEFAULT='0'");
                if (drArr.Length > 0)
                {
                    result = drArr[0]["USERID"].ToString();
                }
                else
                {
                    result = dt.Rows[0]["USERID"].ToString();
                }
            }
            else
            {
                DataRow[] drArr = dt.Select(" IF_DEFAULT_EX='0'");
                if (drArr.Length > 0)
                {
                    foreach (DataRow drr in drArr)
                    {
                        result += drr["USERID"].ToString() + ",";
                    }
                    result = result.Substring(0, result.Length - 1);
                }
            }
        }

        return result;
    }
    #endregion

    #region 采样、分析默认人员
    /// <summary>
    /// 获得采样人员相关信息
    /// </summary>
    /// <param name="strMonitorID">监测类别</param>
    /// <param name="strItemID">监测项目</param>
    /// <param name="strSampleManager">采样负责人ID</param>
    /// <param name="strSampleID">采样协同人ID</param>
    protected void GetSamplingMan(string strMonitorID, string strContractID, ref string strSampleManager, ref string strSampleID)
    {
        if (!String.IsNullOrEmpty(strMonitorID) && !string.IsNullOrEmpty(strContractID))
        {
            TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
            objItems.CONTRACT_ID = strContractID;
            objItems.MONITOR_TYPE_ID = strMonitorID;
            DataTable dt = new TMisContractUserdutyLogic().SelectDutyUser(objItems);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i] as DataRow;
                    strSampleManager = dt.Rows[0]["SAMPLING_MANAGER_ID"].ToString().Length > 0 ? dt.Rows[0]["SAMPLING_MANAGER_ID"].ToString() : (dr["SAMPLING_MANAGER_ID"].ToString().Length > 0 ? dr["SAMPLING_MANAGER_ID"].ToString() : strSampleManager);
                    strSampleID += strSampleID.Contains(dr["SAMPLING_ID"].ToString()) ? "" : (dr["SAMPLING_ID"].ToString().Length > 0 ? dr["SAMPLING_ID"].ToString() + "," : "");
                }
            }
        }
        strSampleID = strSampleID.Length > 0 ? strSampleID.Remove(strSampleID.LastIndexOf(",")) : "";
    }
    #endregion

    #region 分析方法、依据获取
    /// <summary>
    /// 获取指定监测项目的默认分析方法、依据
    /// </summary>
    /// <param name="strItemID">监测项目ID</param>
    /// <param name="strAnalysisID">分析方法ID</param>
    /// <param name="strStandardID">方法依据ID</param>
    /// <param name="strCheckOut">最底检出限</param>
    protected void getMethod(string strItemID, ref string strAnalysisID, ref string strStandardID, ref string strCheckOut)
    {
        TBaseItemAnalysisVo objAnalysis = new TBaseItemAnalysisVo();
        objAnalysis.ITEM_ID = strItemID;
        objAnalysis.IS_DEL = "0";
        DataTable dtItemAnalysis = new TBaseItemAnalysisLogic().SelectByTable(objAnalysis);
        if (dtItemAnalysis.Rows.Count > 0)
        {
            for (int i = 0; i < dtItemAnalysis.Rows.Count; i++)
            {
                if (dtItemAnalysis.Rows[i]["IS_DEFAULT"].ToString() == "是")//默认负责人，否则取第一条方法
                {
                    strAnalysisID = dtItemAnalysis.Rows[i]["ID"].ToString();
                    strCheckOut = dtItemAnalysis.Rows[i]["LOWER_CHECKOUT"].ToString();
                    TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(dtItemAnalysis.Rows[i]["ANALYSIS_METHOD_ID"].ToString());
                    if (objMethod != null)
                    {
                        strStandardID = objMethod.METHOD_ID;
                    }

                    break;//默认方法 唯一
                }
                else
                {
                    strAnalysisID = dtItemAnalysis.Rows[0]["ID"].ToString();
                    strCheckOut = dtItemAnalysis.Rows[0]["LOWER_CHECKOUT"].ToString();
                    TBaseMethodAnalysisVo objMethod = new TBaseMethodAnalysisLogic().Details(dtItemAnalysis.Rows[0]["ANALYSIS_METHOD_ID"].ToString());
                    if (objMethod != null)
                    {
                        strStandardID = objMethod.METHOD_ID;
                    }
                }
            }
            
        }
    }
    #endregion

    /// <summary>
    /// 获取采用的标准项的上下限
    /// </summary>
    /// <param name="strItemID">项目ID</param>
    /// <param name="strConditionID">条件项ID</param>
    /// <param name="strUp">上限</param>
    /// <param name="strLow">下限</param>
    protected void getStandardValue(string strItemID, string strConditionID, ref string strUp, ref string strLow, ref string strConditionType)
    {
        TBaseEvaluationConItemVo objConItemVo = new TBaseEvaluationConItemVo();
        objConItemVo.ITEM_ID = strItemID;
        objConItemVo.CONDITION_ID = strConditionID;
        objConItemVo.IS_DEL = "0";
        objConItemVo = new TBaseEvaluationConItemLogic().Details(objConItemVo);
        //上限处理
        if (objConItemVo.DISCHARGE_UPPER.Length > 0)
        {
            //上限单位
            string strUnit = new TSysDictLogic().GetDictNameByDictCodeAndType(objConItemVo.UPPER_OPERATOR, "logic_operator");
            if (strUnit.Length > 0)
            {
                if (strUnit.IndexOf("≤") >= 0)
                {
                    strUnit = "<=";
                }
                else if (strUnit.IndexOf("≥") >= 0)
                {
                    strUnit = ">=";
                }
            }
            if (objConItemVo.DISCHARGE_UPPER.Contains(","))
            {
                string[] strValue = objConItemVo.DISCHARGE_UPPER.Split(',');
                foreach (string str in strValue)
                {
                    if (str.Length > 0)
                    {
                        strUp += (strUnit + str) + ",";
                    }
                }
                if (strUp.Length > 0)
                {
                    strUp = strUp.Remove(strUp.LastIndexOf(","));
                }
            }
            else
            {
                strUp = strUnit + objConItemVo.DISCHARGE_UPPER;
            }
        }
        //下限处理
        if (objConItemVo.DISCHARGE_LOWER.Length > 0)
        {
            //下限单位
            string strUnit = new TSysDictLogic().GetDictNameByDictCodeAndType(objConItemVo.LOWER_OPERATOR, "logic_operator");
            if (strUnit.Length > 0)
            {
                if (strUnit.IndexOf("≤") >= 0)
                {
                    strUnit = "<=";
                }
                else if (strUnit.IndexOf("≥") >= 0)
                {
                    strUnit = ">=";
                }
            }
            if (objConItemVo.DISCHARGE_LOWER.Contains(","))
            {
                string[] strValue = objConItemVo.DISCHARGE_LOWER.Split(',');
                foreach (string str in strValue)
                {
                    if (str.Length > 0)
                    {
                        strLow += (strUnit + str) + ",";
                    }
                }
                if (strLow.Length > 0)
                {
                    strLow = strLow.Remove(strLow.LastIndexOf(","));
                }
            }
            else
            {
                strLow = strUnit + objConItemVo.DISCHARGE_LOWER;
            }
        }
        strConditionType = new TBaseEvaluationInfoLogic().Details(new TBaseEvaluationConInfoLogic().Details(strConditionID).STANDARD_ID).STANDARD_TYPE;
    }

    /// <summary>
    /// 执行插入自送样监测任务计划
    /// </summary>
    /// <param name="strTask_id">委托书业务ID</param>
    /// <param name="strFreq">监测频次</param>
    /// <returns></returns>
    protected bool CreateSamplePlan(string strTask_id, string strFreq)
    {
        bool flag = false;
        //if (new TMisContractSamplePlanLogic().CreateSamplePlan(strTask_id, strFreq))
        if (new TMisContractSamplePlanLogic().CreateSamplePlan(strTask_id, "1"))
        {
            DataTable dt = new DataTable();

            dt = new TMisContractSamplePlanLogic().SelectByTable(new TMisContractSamplePlanVo { CONTRACT_ID = strTask_id });
            if (doPlanTaskForSample(strTask_id, dt))
            {
                flag = true;
            }
        }
        return flag;
    }

    /// <summary>
    /// 创建原因：获取指定监测项目ID的采样仪器ID
    /// 创建人：胡方扬
    /// 创建日期：2013-07-04
    /// </summary>
    /// <param name="strItemID">监测项目ID</param>
    /// <returns></returns>
    public string GetItemSamplingInstrumentID(string strItemID) {
        string result = "";
        DataTable dt = new TBaseItemSamplingInstrumentLogic().GetItemSamplingInstrument(strItemID);
        if (dt.Rows.Count > 0) { 
        result=dt.Rows[0]["ID"].ToString();
        }
        return result;
    }
    #endregion

    #region 公共属性区域

    /// <summary>
    /// 指定操作人部分的 显示
    /// </summary>
    public bool Visible_dvToOpUsers
    {
        get { return dvToOpUsers.Visible; }
        set { dvToOpUsers.Visible = value; }
    }

    #endregion

}