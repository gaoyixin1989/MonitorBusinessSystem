using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;

public partial class Channels_OA_Part_QHD_PartTest_AdminAduit : PageBaseForWF, IWFStepRules
{
    private string task_id = "", strTaskProjectName = "";
    private string strBtnType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
        strBtnType = this.hidBtnType.Value.ToString();
        //获取隐藏域参数
        GetHiddenParme();
        if (!String.IsNullOrEmpty(task_id))
        {
            //注册编号
            //wfControl.ServiceCode = "CG" + DateTime.Now.ToString("yyyyMMddHHmmss");
            //注册名称
            //wfControl.ServiceName = "";
        }
    }
    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
    }
    #region 工作流
    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是验证组件和业务数据的地方

        strMsg = "";
        return true;

    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        //这里是产生和注册业务数据的地方
        if (!String.IsNullOrEmpty(task_id) && String.IsNullOrEmpty(strBtnType) )
        {
            TOaPartBuyRequstVo objItems = new TOaPartBuyRequstVo();
            objItems.ID = task_id;
            objItems.STATUS = "9";
            objItems.APPLY_TYPE = "02";
            objItems.APP_LEADER_ID = this.hidUserId.Value.ToString();
            objItems.APP_LEADER_INFO = this.hidOptionContent.Value.ToString();
            objItems.APP_LEADER_DATE = this.hidOptionDate.Value.ToString();
            if (new TOaPartBuyRequstLogic().Edit(objItems))
            {
                wfControl.SaveInstStepServiceData("申请计划ID", "task_id", task_id, "3");                
            }
            else
            {
                LigerDialogAlert("数据发送失败！", "error");
                return;
            }
            //updateAccept(objItems);
        }
        else if (!String.IsNullOrEmpty(task_id) && strBtnType == "back")
        {
            TOaPartBuyRequstVo objItems = new TOaPartBuyRequstVo();
            objItems.ID = task_id;
            objItems.STATUS = "2";
            objItems.APPLY_TYPE = "02";
            objItems.APP_LEADER_ID = this.hidUserId.Value.ToString();
            objItems.APP_LEADER_INFO = this.hidOptionContent.Value.ToString();
            objItems.APP_LEADER_DATE = this.hidOptionDate.Value.ToString();
            if (new TOaPartBuyRequstLogic().Edit(objItems))
            {
                wfControl.SaveInstStepServiceData("申请计划ID", "task_id", task_id, "2");
            }
            else
            {
                LigerDialogAlert("数据退回失败！", "error");
                return;
            }
        }
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }
    #endregion

    //void updateAccept(TOaPartBuyRequstVo objItemb)
    //{
    //    DataTable dt = new DataTable();
    //    TOaPartBuyRequstLstVo objItems = new TOaPartBuyRequstLstVo();
    //    TOaPartInfoVo objItemPart = new TOaPartInfoVo();
    //    objItems.STATUS = "0";
    //    dt = new TOaPartBuyRequstLstLogic().SelectUnionPartByTable(objItems, objItemPart, objItemb, 0, 1);

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        DataRow dr = dt.Rows[i];
    //        TOaPartAcceptedVo objItem = new TOaPartAcceptedVo();
    //        objItem.PART_ID = dr["PART_ID"].ToString();
    //        objItem.NEED_QUANTITY = dr["NEED_QUANTITY"].ToString();
    //        objItem.USERDO = dr["USERDO"].ToString();
    //        objItem.ID = GetSerialNumber("t_oa_AcceptedID");
    //        if (new TOaPartAcceptedLogic().Create(objItem))
    //        {
    //            TOaPartAcceptedlistVo objItemNew = new TOaPartAcceptedlistVo();
    //            objItemNew.ID = GetSerialNumber("t_oa_AcceptedLstID");
    //            objItemNew.ACCEPTED_ID = objItems.ID;
    //            objItemNew.REQUST_LST_ID = objItemb.ID;
    //            new TOaPartAcceptedlistLogic().Create(objItemNew);

    //        }
    //    }
    //}
}