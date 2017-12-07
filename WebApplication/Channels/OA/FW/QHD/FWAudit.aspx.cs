using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.OA.FW;
using i3.BusinessLogic.Channels.OA.FW;
using i3.BusinessLogic.Sys.General;
using System.Web.Services;
using System.Data;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Channels.OA.SW;
using i3.BusinessLogic.Channels.OA.SW;
namespace n4
{

    /// <summary>
    /// 功能描述：发文核稿
    /// 创建日期：2013-2-3
    /// 创建人  ：苏成斌
    /// </summary>
    public partial class Channels_OA_FW_QHD_FWAudit : PageBaseForWF, IWFStepRules
    {
        private string strBtnType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request["fw_id"] != null && this.Request["fw_id"].ToString().Length > 0)
            {
                this.hidTaskId.Value = this.Request["fw_id"].ToString();
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
            TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
            this.YWNO.Text = objFW.YWNO;
            this.FWNO.Text = objFW.FWNO;
            this.FW_TITLE.Text = objFW.FW_TITLE;
            this.ZB_DEPT.Text = objFW.ZB_DEPT;
            this.DRAFT_ID.Text = new TSysUserLogic().Details(objFW.DRAFT_ID).REAL_NAME;
            this.MJ.Text = getDictName(objFW.MJ, "FW_MJ");
            this.FW_DATE.Text = DateTime.Parse(objFW.FW_DATE).ToShortDateString();
            this.START_DATE.Text = DateTime.Parse(objFW.START_DATE).ToShortDateString();
            this.END_DATE.Text = DateTime.Parse(objFW.END_DATE).ToShortDateString();

            if (this.hidTask_Tatus.Value == "1")
            {
                this.TReadUserList.Attributes.Add("style", "display:none");
            }
            else if (this.hidTask_Tatus.Value == "2")
            {
                this.ISSUE_INFO.Value = objFW.ISSUE_INFO;
                this.ISSUE_ID.Text = new TSysUserLogic().Details(objFW.ISSUE_ID).REAL_NAME;
                this.ISSUE_DATE.Text = DateTime.Parse(objFW.ISSUE_DATE).ToShortDateString();

                List<TOaSwReadVo> list = new TOaSwReadLogic().SelectByReadUser(objFW.ID);
                string strName = "";
                string strId = "";
                for (int i = 0; i < list.Count; i++)
                {
                    string swid = list[i].SW_PLAN_ID;
                    string swname = new TSysUserLogic().Details(swid).REAL_NAME;
                    if (i == 0)
                    {
                        strName = swname;
                        strId = swid;
                    }
                    else if (i > 0)
                    {
                        strName = strName + "," + swname;
                        strId = strId + "," + swid;
                    }

                }
                this.ReadUserNames.Value = strName;
                this.Hid_ReadUserIDs.Value = strId;

                this.ISSUE_INFO.Disabled = true;
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

            this.hidTask_Tatus.Value = new TOaFwInfoLogic().Details(this.hidTaskId.Value).FW_STATUS;
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
                TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
                objFW.ID = this.hidTaskId.Value;

                if (this.hidTask_Tatus.Value == "1")
                {
                    objFW.ISSUE_INFO = this.ISSUE_INFO.Value;
                    objFW.ISSUE_ID = LogInfo.UserInfo.ID;
                    objFW.ISSUE_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objFW.FW_STATUS = "2";
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "2");
                }
                else if (this.hidTask_Tatus.Value == "2")
                {
                    objFW.ISSUE_INFO = this.ISSUE_INFO.Value;
                    objFW.ISSUE_ID = LogInfo.UserInfo.ID;
                    objFW.ISSUE_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objFW.FW_STATUS = "9";
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "9");
                }
                new TOaFwInfoLogic().Edit(objFW);
            }
            else if (this.hidTaskId.Value.Length > 0 && strBtnType == "back")
            {
                //这里是产生和注册业务数据的地方
                TOaFwInfoVo objFW = new TOaFwInfoLogic().Details(this.hidTaskId.Value);
                objFW.ID = this.hidTaskId.Value;

                if (this.hidTask_Tatus.Value == "1")
                {
                    objFW.FW_STATUS = "0";
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "0");
                }
                else if (this.hidTask_Tatus.Value == "2")
                {
                    objFW.ISSUE_ID = "###";
                    objFW.ISSUE_DATE = "###";
                    objFW.FW_STATUS = "1";
                    wfControl.SaveInstStepServiceData("发文ID", "fw_id", objFW.ID, "1");
                }
                new TOaFwInfoLogic().Edit(objFW);
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
                string fwid = list[i].ID;
                new TOaSwReadLogic().Delete(fwid);
            }

            string[] str = strUserID.Split(',');
            foreach (string userid in str)
            {
                if (userid == "")
                {
                    break;
                }
                TOaSwReadVo objSR = new TOaSwReadVo();
                objSR.IS_OK = "0";
                objSR.REMARK1 = "1";
                objSR.SW_ID = strSWID;
                objSR.SW_PLAN_ID = userid;
                objSR.ID = GetSerialNumber("t_oa_sfw_read");
                new TOaSwReadLogic().Create(objSR);
            }
            return "1";
        }

    }
}