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
using i3.BusinessLogic.Sys.WF;
using System.Data;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.General;
/// <summary>
/// 功能描述：收文登记(秦皇岛)
/// 创建日期：20140703
/// 创建人  ：黄进军
/// </summary>

public partial class Channels_OA_SW_QHD_NewSWAudit : PageBaseForWF, IWFStepRules
{
    private string strBtnType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request["SwID"] != null && this.Request["SwID"].ToString().Length > 0)
        {
            this.hidTaskId.Value = this.Request["SwID"].ToString();
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
    }

    //页面初始化
    private void InitPage()
    {
        TOaSwInfoVo objSW = new TOaSwInfoLogic().Details(this.hidTaskId.Value);

        this.FROM_CODE.Text = objSW.FROM_CODE;
        this.SW_CODE.Text = objSW.SW_CODE;
        this.SW_FROM.Text = objSW.SW_FROM;
        this.SW_COUNT.Text = objSW.SW_COUNT;
        this.SW_REG_DATE.Text = DateTime.Parse(objSW.SW_DATE).ToShortDateString();
        this.SW_TITLE.Text = objSW.SW_TITLE;
        this.SW_MJ.Text = getDictName(objSW.SW_MJ, "FW_MJ"); ;
        this.SW_SIGN_ID.Text = objSW.SW_SIGN_ID;
        this.SW_SIGN_DATE.Text = DateTime.Parse(objSW.SW_SIGN_DATE).ToShortDateString();
        if (this.hidTask_Tatus.Value == "1")
        {
            this.TReadUserList.Attributes.Add("style", "display:none");
        }
        else if (this.hidTask_Tatus.Value == "2")
        {
            this.SW_APP_INFO.Value = objSW.SW_APP_INFO;
            this.SW_APP_ID.Text = new TSysUserLogic().Details(objSW.SW_APP_ID).REAL_NAME;
            this.SW_APP_DATE.Text = DateTime.Parse(objSW.SW_APP_DATE).ToShortDateString();

            List<TOaSwReadVo> list = new TOaSwReadLogic().SelectByReadUser(objSW.ID);
            string strName = "";
            string strId = "";
            for (int i = 0; i < list.Count; i++)
            {
                string swid = list[i].SW_PLAN_ID;
                string swname = new TSysUserLogic().Details(swid).REAL_NAME;
                if (i == 0) {
                    strName = swname;
                    strId = swid;
                }
                else if (i > 0) {
                    strName = strName + "," + swname;
                    strId = strId + "," + swid;
                }
                 
            }
            this.ReadUserNames.Value = strName;
            this.Hid_ReadUserIDs.Value = strId;

            this.SW_APP_INFO.Disabled = true;
        }
        else if (this.hidTask_Tatus.Value == "9")
        {
            this.TReadUserList.Attributes.Add("style", "display:none");
        }
    }

    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
        this.hidTaskId.Value = myServiceList[0].SERVICE_KEY_VALUE;

        this.hidTask_Tatus.Value = new TOaSwInfoLogic().Details(this.hidTaskId.Value).SW_STATUS;
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方

        strMsg = "";
        return strMsg == "" ? true : false;

    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        //这里是产生和注册业务数据的地方
        if (this.hidTaskId.Value.Length > 0 && String.IsNullOrEmpty(strBtnType))
        {
            //这里是产生和注册业务数据的地方
            TOaSwInfoVo objSW = new TOaSwInfoLogic().Details(this.hidTaskId.Value);
            objSW.ID = this.hidTaskId.Value;

            if (this.hidTask_Tatus.Value == "1")
            {
                this.TReadUserList.Attributes.Add("style", "display:none");
                objSW.SW_APP_INFO = this.SW_APP_INFO.Value;
                objSW.SW_APP_ID = LogInfo.UserInfo.ID;
                objSW.SW_APP_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objSW.SW_STATUS = "2";
                wfControl.SaveInstStepServiceData("收文ID", "SwID", objSW.ID, "2");
            }
            else if (this.hidTask_Tatus.Value == "2")
            {
                objSW.PIGONHOLE_ID = LogInfo.UserInfo.ID;
                objSW.PIGONHOLE_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objSW.SW_STATUS = "9";
                wfControl.SaveInstStepServiceData("收文ID", "SwID", objSW.ID, "9");
            }
            new TOaSwInfoLogic().Edit(objSW);
        }
        else if (this.hidTaskId.Value.Length > 0 && strBtnType == "back")
        {
            //这里是产生和注册业务数据的地方
            TOaSwInfoVo objSW = new TOaSwInfoLogic().Details(this.hidTaskId.Value);
            objSW.ID = this.hidTaskId.Value;

            if (this.hidTask_Tatus.Value == "1")
            {
                this.TReadUserList.Attributes.Add("style", "display:none");
                objSW.SW_STATUS = "0";
                wfControl.SaveInstStepServiceData("收文ID", "SwID", objSW.ID, "0");
            }
            else if (this.hidTask_Tatus.Value == "2")
            {
                objSW.SW_APP_ID = "###";
                objSW.SW_APP_DATE = "###";
                objSW.SW_STATUS = "1";
                wfControl.SaveInstStepServiceData("收文ID", "SwID", objSW.ID, "1");
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

    /// <summary>
    /// 点击确定按钮保存到收发文阅办表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string AddSFWRead(string strUserID, string strSWID)
    {
        List<TOaSwReadVo> list = new TOaSwReadLogic().SelectByReadUser(strSWID);
        for (int i = 0; i < list.Count; i++)
        {
            string swid = list[i].ID;
            new TOaSwReadLogic().Delete(swid);
        }

        string[] str = strUserID.Split(',');
        foreach(string userid in str)
        {
            if (userid == "")
            {
                break;
            }
            TOaSwReadVo objSR = new TOaSwReadVo(); 
            objSR.IS_OK = "0";
            objSR.REMARK1 = "0";
            objSR.SW_ID = strSWID;
            objSR.SW_PLAN_ID = userid;
            objSR.ID = GetSerialNumber("t_oa_sfw_read");
            new TOaSwReadLogic().Create(objSR);
        }
        return "1";
    }

}