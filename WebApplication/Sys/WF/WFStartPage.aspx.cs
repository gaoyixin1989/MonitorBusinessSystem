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
using i3.BusinessLogic.Channels.OA.PART;
public partial class Sys_WF_WFStartPage : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Update By 邵世卓 获取原业务Url传递参数，并据此串接返回指定的页面,获取格式为arr=1|arr2=2|arr3=3,返回格式为arr=1&arr2=2&arr3=3
            string type = string.Empty;
            string strtaskID = string.Empty;
            string strUrlType = Request.QueryString["action"];
            string strReturnType = "";
            if (!string.IsNullOrEmpty(strUrlType))
            {
                string[] strType = strUrlType.Split('|');
               string [] Arrytype = strType[1].Split('=');
               type = Arrytype[1];
               if (strType.Length == 4)
               {
                   string[] ArrystrtaskID = strType[3].Split('=');
                   if (ArrystrtaskID.Length >=2)
                   { 
                       strtaskID = ArrystrtaskID[1];
                   }
               }
                foreach (string str in strType)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        strReturnType += "&" + str;
                    }
                }
            }
            if (type.ToLower().Equals("false"))
            {
                #region//判断查询明细的数据是呈报单的还是填报申请单的
                DataTable dt = new TOaPartBuyRequstLogic().SelectRemarks(strtaskID);
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()) && dt.Rows[0][0].ToString().Equals("1"))
                    { WF_ID.Value = "WorkSubmit"; }
                    else
                    { WF_ID.Value = Request.QueryString[TWfSettingTaskVo.WF_ID_FIELD]; }
                }
                else
                { WF_ID.Value = Request.QueryString[TWfSettingTaskVo.WF_ID_FIELD]; }
                #endregion
            }
            else
            {
                //Update End 
                WF_ID.Value = Request.QueryString[TWfSettingTaskVo.WF_ID_FIELD];
            }
            //WF_ID.Value = "OA_QJ_01";
            if (!string.IsNullOrEmpty(WF_ID.Value))
            {
                //bool bIsHaveRight = IsHaveRightToStartTheWF(this.LogInfo.UserInfo.ID, WF_ID.Value);
                TWfSettingTaskVo firstStep = new TWfSettingTaskVo();
                string strFirstStepUrl = "";
                bool bIsHaveRight = IsHaveRightToStartTheWF(this.LogInfo.UserInfo.ID, WF_ID.Value, ref firstStep, ref strFirstStepUrl);
                if (bIsHaveRight)
                {
                    //以下是处理转向的问题
                    string strParam = "";
                    strParam = string.Format(((strFirstStepUrl.IndexOf("?") > -1 ? "&" : "?") + "{0}={1}&{2}={3}&IS_WF_START=1"), TWfSettingTaskVo.WF_ID_FIELD, firstStep.WF_ID, TWfSettingTaskVo.WF_TASK_ID_FIELD, firstStep.WF_TASK_ID);
                    Response.Redirect(strFirstStepUrl + strParam + strReturnType);
                }
                else
                {
                    //Update By SSZ LigerUI 信息提示方法，并关闭当前窗口 2013-2-4
                    LigerDialogPageCloseAlert("<b>您不具备启动流程：" + WF_ID.Value + "的权限，请咨询管理员</b>", DialogMold.waitting.ToString(), "3300");
                    //让其转向到另一个页面
                }
            }
        }
    }

    /// <summary>
    /// 看有没有权限启动相对应的流程
    /// </summary>
    /// <param name="strUserID">用户编号</param>
    /// <param name="strWF_ID">流程编号</param>
    /// <returns>返回值</returns>
    private bool IsHaveRightToStartTheWF(string strUserID, string strWF_ID, ref TWfSettingTaskVo firstStep, ref string strUrl)
    {
        TWfSettingFlowVo flow = new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { WF_ID = strWF_ID });
        if (string.IsNullOrEmpty(flow.ID))
            return false;
        List<TWfSettingTaskVo> taskList = new TWfSettingTaskLogic().SelectByObjectList(new TWfSettingTaskVo() { WF_ID = strWF_ID, SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD, SORT_TYPE = " asc " });
        if (taskList.Count == 0)
            return false;
        //判断是否有相关启动权限

        #region 不需要权限
        //if (taskList[0].OPER_TYPE == "01" && taskList[0].OPER_VALUE.IndexOf(strUserID) > -1)
        //{
        //    strUrl = new TWfSettingTaskFormLogic().Details(new TWfSettingTaskFormVo() { WF_ID = strWF_ID, WF_TASK_ID = taskList[0].WF_TASK_ID }).UCM_ID;
        //    firstStep = taskList[0];
        //    return true;
        //}

        //if (taskList[0].OPER_TYPE == "02")
        //{
        //    DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(new TSysUserPostVo());
        //    foreach (DataRow dr in dtUserPost.Rows)
        //    {
        //        if (dr[TSysUserPostVo.USER_ID_FIELD].ToString() == strUserID)
        //        {
        //            if (taskList[0].OPER_VALUE.IndexOf(dr[TSysUserPostVo.POST_ID_FIELD].ToString()) > -1)
        //            {
        //                //目前只处理了页面处理方式的连接地址
        //                firstStep = taskList[0];
        //                strUrl = new TWfSettingTaskFormLogic().Details(new TWfSettingTaskFormVo() { WF_ID = strWF_ID, WF_TASK_ID = taskList[0].WF_TASK_ID }).UCM_ID;
        //                if (!string.IsNullOrEmpty(strUrl))
        //                    return true;
        //            }
        //        }
        //    }
        //}
        //return false;
        #endregion

        //石磊调整，不加入限制，任何人都可以启动流程
        //2013-05-02

        strUrl = new TWfSettingTaskFormLogic().Details(new TWfSettingTaskFormVo() { WF_ID = strWF_ID, WF_TASK_ID = taskList[0].WF_TASK_ID }).UCM_ID;
        firstStep = taskList[0];
        return true;

    }

}