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

public partial class Sys_WF_WFDealPage : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ID.Value = Request.QueryString[TWfSettingTaskVo.ID_FIELD];
        if (!string.IsNullOrEmpty(ID.Value))
        {

            TWfSettingTaskVo dealtStep = new TWfSettingTaskVo();
            TWfInstTaskDetailVo realStep = new TWfInstTaskDetailLogic().Details(ID.Value);
            string strFirstStepUrl = "";
            bool bIsHaveRight = IsHaveRightToDealTheStep(this.LogInfo.UserInfo.ID, ID.Value, ref dealtStep, ref strFirstStepUrl);
            if (bIsHaveRight)
            {
                //以下是处理转向的问题
                //增加对参数的自适应功能  (strFirstStepUrl.IndexOf("?") > -1 ? "&" : "?")
                string strStatment = string.Format((strFirstStepUrl.IndexOf("?") > -1 ? "&" : "?") + "{0}={1}&{2}={3}&IS_WF_DEAL=1&{4}={5}&{6}={7}&{8}={9}", 
                    TWfSettingTaskVo.WF_ID_FIELD, dealtStep.WF_ID,
                    TWfSettingTaskVo.WF_TASK_ID_FIELD, dealtStep.WF_TASK_ID,
                    TWfInstTaskOpinionsVo.WF_INST_TASK_ID_FIELD, realStep.ID,
                    TWfInstTaskDetailVo.WF_INST_ID_FIELD, realStep.WF_INST_ID,
                    TWfSettingTaskVo.TASK_ORDER_FIELD,dealtStep.TASK_ORDER);  //环节序号（刘静楠，2013/7/17）
                Response.Redirect(strFirstStepUrl + strStatment);
            }
            else
            {
                //Alert("您不具备操作环节：" + ID.Value + "的权限，请咨询管理员");
                //让其转向到另一个页面
                //Update By SSZ LigerUI 信息提示方法，并关闭当前窗口 2013-2-4
                LigerDialogPageCloseAlert("<b>您不具备操作环节：" + ID.Value + "的权限<br />或者配置中已无此环节信息，请咨询管理员</b>", DialogMold.waitting.ToString(), "3300");
                //让其转向到另一个页面
            }
        }
    }

    /// <summary>
    /// 看有没有权限启动相对应的流程
    /// </summary>
    /// <param name="strUserID">用户编号</param>
    /// <param name="strWF_ID">流程编号</param>
    /// <returns>返回值</returns>
    private bool IsHaveRightToDealTheStep(string strUserID, string strID, ref TWfSettingTaskVo dealStep, ref string strUrl)
    {
        TWfInstTaskDetailVo step = new TWfInstTaskDetailLogic().Details(new TWfInstTaskDetailVo() { ID = strID });
        if (string.IsNullOrEmpty(step.ID))
            return false;
        TWfSettingTaskVo task = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_ID = step.WF_ID, WF_TASK_ID = step.WF_TASK_ID, SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD, SORT_TYPE = " asc " });
        if (string.IsNullOrEmpty(task.ID))
            return false;
        //判断是否有相关启动权限

        strUrl = new TWfSettingTaskFormLogic().Details(new TWfSettingTaskFormVo() { WF_ID = step.WF_ID, WF_TASK_ID = step.WF_TASK_ID }).UCM_ID;
        dealStep = task;
        return true;

        #region 原具备环节权限的判定

        //if (task.OPER_TYPE == "01" && task.OPER_VALUE.IndexOf(strUserID) > -1)
        //{
        //    strUrl = new TWfSettingTaskFormLogic().Details(new TWfSettingTaskFormVo() { WF_ID = step.WF_ID, WF_TASK_ID = step.WF_TASK_ID }).UCM_ID;
        //    dealStep = task;
        //    return true;
        //}

        //if (task.OPER_TYPE == "02")
        //{
        //    DataTable dtUserPost = new TSysUserPostLogic().SelectByTable(new TSysUserPostVo());
        //    foreach (DataRow dr in dtUserPost.Rows)
        //    {
        //        if (dr[TSysUserPostVo.USER_ID_FIELD].ToString() == strUserID)
        //        {
        //            if (task.OPER_VALUE.IndexOf(dr[TSysUserPostVo.POST_ID_FIELD].ToString()) > -1)
        //            {
        //                //目前只处理了页面处理方式的连接地址
        //                dealStep = task;
        //                strUrl = new TWfSettingTaskFormLogic().Details(new TWfSettingTaskFormVo() { WF_ID = step.WF_ID, WF_TASK_ID = task.WF_TASK_ID }).UCM_ID;
        //                if (!string.IsNullOrEmpty(strUrl))
        //                    return true;
        //            }
        //        }
        //    }
        //}
        //return false;

        #endregion
    }
}