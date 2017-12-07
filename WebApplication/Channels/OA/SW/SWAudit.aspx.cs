using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.OA.SW;
using i3.BusinessLogic.Channels.OA.SW;
using i3.BusinessLogic.Sys.General;
using System.Data;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.General;
/// <summary>
/// 功能描述：收文办理
/// 创建日期：
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_OA_SW_SWAudit : PageBaseForWF, IWFStepRules
{
    private string strBtnType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request["sw_id"] != null && this.Request["sw_id"].ToString().Length > 0)
        {
            this.hidTaskId.Value = this.Request["sw_id"].ToString();
        }
        if (this.Request["task_tatus"] != null && this.Request["task_tatus"].ToString().Length > 0)
        {
            this.hidTask_Tatus.Value = this.Request["task_tatus"].ToString();
        }
        strBtnType = this.hidBtnType.Value.ToString();
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
            InitPage();
        }
        //工作流开启多用户发送模式
        if (this.hidTask_Tatus.Value == "2")
            wfControl.SetMoreDealUserFlag = true;
        Button1.Click += new EventHandler(wfControl.btnSend_Click);
    }

    //页面初始化
    private void InitPage()
    {
        if (this.hidTaskId.Value.Length == 0)
            return;

        TOaSwInfoVo objSW = new TOaSwInfoLogic().Details(this.hidTaskId.Value);
        this.FROM_CODE.Text = objSW.FROM_CODE;
        this.SW_CODE.Text = objSW.SW_CODE;
        this.SW_TITLE.Text = objSW.SW_TITLE;
        this.SW_FROM.Text = objSW.SW_FROM;
        this.SW_COUNT.Text = objSW.SW_COUNT;
        this.MJ.Text = getDictName(objSW.SW_MJ, "FW_MJ");
        this.SW_SIGN_ID.Text = objSW.SW_SIGN_ID;
        this.SW_SIGN_DATE.Text = DateTime.Parse(objSW.SW_SIGN_DATE).ToShortDateString();
        this.SW_REG_DATE.Text = DateTime.Parse(objSW.SW_REG_DATE).ToShortDateString();

        if (this.hidTask_Tatus.Value == "1")
        {
            this.SW_PLAN_APP_INFO.Disabled = true;
            this.SW_APP_INFO.Disabled = true;
        }
        else if (this.hidTask_Tatus.Value == "2")
        {
            this.SW_PLAN_INFO.Value = objSW.SW_PLAN_INFO;
            this.SW_PLAN_ID.Text = new TSysUserLogic().Details(objSW.SW_PLAN_ID).REAL_NAME;
            this.SW_PLAN_DATE.Text = DateTime.Parse(objSW.SW_PLAN_DATE).ToShortDateString();

            this.SW_PLAN_INFO.Disabled = true;
            this.SW_APP_INFO.Disabled = true;

            this.dAcceptUserLst.Visible = true;
            this.wfControl.Visible = false;
            this.Button1.Visible = true;
        }
        else if (this.hidTask_Tatus.Value == "3")
        {
            this.SW_PLAN_INFO.Value = objSW.SW_PLAN_INFO;
            this.SW_PLAN_ID.Text = new TSysUserLogic().Details(objSW.SW_PLAN_ID).REAL_NAME;
            this.SW_PLAN_DATE.Text = DateTime.Parse(objSW.SW_PLAN_DATE).ToShortDateString();

            this.SW_PLAN_APP_INFO.Value = objSW.SW_PLAN_APP_INFO;
            this.SW_PLAN_APP_ID.Text = new TSysUserLogic().Details(objSW.SW_PLAN_APP_ID).REAL_NAME;
            this.SW_PLAN_APP_DATE.Text = DateTime.Parse(objSW.SW_PLAN_APP_DATE).ToShortDateString();

            this.SW_PLAN_INFO.Disabled = true;
            this.SW_PLAN_APP_INFO.Disabled = true;
        }
        else if (this.hidTask_Tatus.Value == "9")
        {
            this.SW_PLAN_INFO.Value = objSW.SW_PLAN_INFO;
            this.SW_PLAN_ID.Text = new TSysUserLogic().Details(objSW.SW_PLAN_ID).REAL_NAME;
            this.SW_PLAN_DATE.Text = DateTime.Parse(objSW.SW_PLAN_DATE).ToShortDateString();

            this.SW_PLAN_APP_INFO.Value = objSW.SW_PLAN_APP_INFO;
            this.SW_PLAN_APP_ID.Text = new TSysUserLogic().Details(objSW.SW_PLAN_APP_ID).REAL_NAME;
            this.SW_PLAN_APP_DATE.Text = DateTime.Parse(objSW.SW_PLAN_APP_DATE).ToShortDateString();

            //this.SW_APP_INFO.Value = objSW.SW_APP_INFO;
            //this.SW_APP_ID.Text = new TSysUserLogic().Details(objSW.SW_APP_ID).REAL_NAME;
            //this.SW_APP_DATE.Text = DateTime.Parse(objSW.SW_APP_DATE).ToShortDateString();

            this.SW_PLAN_INFO.Disabled = true;
            this.SW_PLAN_APP_INFO.Disabled = true;
            this.SW_APP_INFO.Disabled = true;

            string strSwAppInfo = "";
            TOaSwHandleVo objSwHandle = new TOaSwHandleVo();
            objSwHandle.SW_ID = this.hidTaskId.Value;
            DataTable dt = new TOaSwHandleLogic().SelectByTable(objSwHandle);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strUserName = new TSysUserLogic().Details(dt.Rows[i]["SW_PLAN_ID"].ToString()).REAL_NAME;
                strSwAppInfo += strUserName + "：" + dt.Rows[i]["SW_PLAN_APP_INFO"].ToString() + "\r\n";
            }
            this.SW_APP_INFO.Value = strSwAppInfo;
        }
    }

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        if (myServiceList != null && myServiceList.Count > 0)
        {
            this.hidTaskId.Value = myServiceList[0].SERVICE_KEY_VALUE;
            this.hidTask_Tatus.Value = new TOaSwInfoLogic().Details(this.hidTaskId.Value).SW_STATUS;
        }
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方
        strMsg = "";
        return true;

    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        if (this.hidTaskId.Value.Length > 0 && String.IsNullOrEmpty(strBtnType))
        {
            //这里是产生和注册业务数据的地方
            TOaSwInfoVo objSW = new TOaSwInfoLogic().Details(this.hidTaskId.Value);
            if (this.hidTask_Tatus.Value == "1")
            {
                objSW.SW_PLAN_INFO = this.SW_PLAN_INFO.Value;
                objSW.SW_PLAN_ID = LogInfo.UserInfo.ID;
                objSW.SW_PLAN_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objSW.SW_STATUS = "2";
                wfControl.SaveInstStepServiceData("收文ID", "sw_id", objSW.ID, "2");
            }
            else if (this.hidTask_Tatus.Value == "2")
            {
                objSW.SW_PLAN_APP_INFO = this.SW_PLAN_APP_INFO.Value;
                objSW.SW_PLAN_APP_ID = LogInfo.UserInfo.ID;
                objSW.SW_PLAN_APP_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objSW.SW_STATUS = "3";
                wfControl.SaveInstStepServiceData("收文ID", "sw_id", objSW.ID, "3");

                for (int i = 0; i < this.HID_USERIDS.Value.Split(',').Length; i++)
                {
                    string strUserID = this.HID_USERIDS.Value.Split(',')[i];
                    TOaSwHandleVo objSwHandle = new TOaSwHandleVo();
                    objSwHandle.ID = GetSerialNumber("t_oa_SWHandleID");
                    objSwHandle.SW_ID = objSW.ID;
                    objSwHandle.SW_PLAN_ID = strUserID;
                    objSwHandle.IS_OK = "0";

                    new TOaSwHandleLogic().Create(objSwHandle);
                    wfControl.MoreDealUserForAdd(strUserID);
                }
            }
            else if (this.hidTask_Tatus.Value == "3")
            {
                TOaSwHandleVo objSwHandle = new TOaSwHandleVo();
                objSwHandle.SW_ID = objSW.ID;
                objSwHandle.SW_PLAN_ID = LogInfo.UserInfo.ID;
                objSwHandle = new TOaSwHandleLogic().Details(objSwHandle);
                objSwHandle.IS_OK = "1";
                objSwHandle.SW_PLAN_APP_INFO = this.SW_APP_INFO.Value;
                objSwHandle.SW_PLAN_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objSwHandle.SW_PLAN_ID = LogInfo.UserInfo.ID;
                new TOaSwHandleLogic().Edit(objSwHandle);

                //查看是否全部完成，变更收文记录状态
                objSwHandle = new TOaSwHandleVo();
                objSwHandle.SW_ID = objSW.ID;
                objSwHandle.IS_OK = "0";
                DataTable dt = new TOaSwHandleLogic().SelectByTable(objSwHandle);
                if(dt.Rows.Count == 0)
                    objSW.SW_STATUS = "9";

                wfControl.SaveInstStepServiceData("收文ID", "sw_id", objSW.ID, "9");
            }
            else if (this.hidTask_Tatus.Value == "9")
            {
                wfControl.SaveInstStepServiceData("收文ID", "sw_id", objSW.ID, "9");
            }
            new TOaSwInfoLogic().Edit(objSW);
        }
        else if (this.hidTaskId.Value.Length > 0 && strBtnType == "back")
        {
            //这里是产生和注册业务数据的地方
            TOaSwInfoVo objSW = new TOaSwInfoLogic().Details(this.hidTaskId.Value);
            if (this.hidTask_Tatus.Value == "1")
            {
                objSW.SW_PLAN_ID = "###";
                objSW.SW_PLAN_DATE = "###";
                objSW.SW_STATUS = "0";
                wfControl.SaveInstStepServiceData("收文ID", "sw_id", objSW.ID, "1");
            }
            else if (this.hidTask_Tatus.Value == "2")
            {
                objSW.SW_PLAN_APP_ID = "###";
                objSW.SW_PLAN_APP_DATE = "###";
                objSW.SW_STATUS = "1";
                wfControl.SaveInstStepServiceData("收文ID", "sw_id", objSW.ID, "1");
            }
            else if (this.hidTask_Tatus.Value == "3")
            {
                objSW.SW_APP_ID = "###";
                objSW.SW_APP_DATE = "###";
                objSW.SW_STATUS = "2";
                wfControl.SaveInstStepServiceData("收文ID", "sw_id", objSW.ID, "1");
            }
            new TOaSwInfoLogic().Edit(objSW);
        }
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }
    #endregion

    /// <summary>
    /// 获取部门
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetDeptItems()
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        TSysDictVo objVo = new TSysDictVo();
        objVo.DICT_TYPE = "dept";
        dt = new TSysDictLogic().SelectByTable(objVo);
        listResult = LigerGridSelectDataToJson(dt, dt.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 获取选择部门的尚未选中的用户
    /// </summary>
    /// <param name="strPost_Dept"></param>
    /// <param name="strMessageId"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSubUserItems(string strPost_Dept, string strUserID)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        TSysUserVo objUser = new TSysUserVo();
        objUser.IS_DEL = "0";
        dt = new TSysUserLogic().SelectByTableUnderDept(strPost_Dept, 0, 0);

        DataTable dtItems = new DataTable();
        dtItems = dt.Copy();
        dtItems.Clear();
        if (strUserID.Length > 0)
        {
            for (int i = 0; i < strUserID.Split(',').Length; i++)
            {
                DataRow[] dr = dt.Select("ID='" + strUserID.Split(',')[i] + "'");
                if (dr != null)
                {
                    foreach (DataRow Temrow in dr)
                    {
                        Temrow.Delete();
                        dt.AcceptChanges();
                    }
                }
            }
        }

        dtItems = dt.Copy();

        listResult = LigerGridSelectDataToJson(dtItems, dtItems.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 获取选择部门的已选中的用户
    /// </summary>
    /// <param name="strPost_Dept"></param>
    /// <param name="strMonitorId"></param>
    /// <param name="strDutyType"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSelectUserItems(string strUserID)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        DataTable dtDuty = new DataTable();
        dt = new TSysUserLogic().SelectByTableUnderDept("", 0, 0);

        DataTable dtItems = new DataTable();
        dtItems = dt.Copy();
        dtItems.Clear();

        if (strUserID.Length > 0)
        {
            for (int i = 0; i < strUserID.Split(',').Length; i++)
            {
                DataRow[] dr = dt.Select("ID='" + strUserID.Split(',')[i] + "'");
                if (dr != null)
                {
                    foreach (DataRow Temrow in dr)
                    {
                        dtItems.ImportRow(Temrow);
                    }
                }
            }
        }

        listResult = LigerGridSelectDataToJson(dtItems, dtItems.Rows.Count);
        return listResult;
    }
}