using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.WF;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;
using System.Web.Services;
using i3.ValueObject.Channels.OA.FW;
using i3.BusinessLogic.Channels.OA.FW;

public partial class Channels_OA_Part_ZZ_FillMaterials : PageBaseForWF, IWFStepRules
{
    private string task_id = "", strTaskProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            wfControl.InitWFDict();
        }
        //获取隐藏域参数
        GetHiddenParme();
        if (!String.IsNullOrEmpty(task_id))//第一个环节
        {
            if (string.IsNullOrEmpty(this.Task_Order.Value.ToString()))
            {
                //注册编号
                wfControl.ServiceCode = "CG" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //注册名称
                 wfControl.ServiceName ="物料采购申请:" +strTaskProjectName ;
                //wfControl.ServiceName = strTaskProjectName;
            }
        }
    }
    /// <summary>
    /// 获取页面隐藏域参数
    /// </summary>
    private void GetHiddenParme()
    {
        task_id = this.hidTaskId.Value.ToString();
        strTaskProjectName = this.hidTaskProjectName.Value.ToString();
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
        if (!String.IsNullOrEmpty(task_id))
        {
            TOaPartBuyRequstVo objItems = new TOaPartBuyRequstVo();
            if (string.IsNullOrEmpty(this.Task_Order.Value.ToString()))
            {
                #region//第一个环节
                    objItems.ID = task_id;
                    objItems.STATUS = "1";
                    objItems.REMARK1 = "0";
                    if (new TOaPartBuyRequstLogic().Edit(objItems))
                    {
                        wfControl.SaveInstStepServiceData("采购计划ID", "task_id", task_id, "1");
                    }
                    else
                    {
                        LigerDialogAlert("数据发送失败！", "error");
                        return;
                    }

                #endregion
            }
            else
            {
                #region//第二个环节(科室意见)
                if (this.Task_Order.Value.ToString().Equals("2"))
                {
                    objItems.ID = task_id;
                    objItems.REMARK1 = "0";
                    objItems.APP_DEPT_ID = this.hidUserId.Value.ToString();
                    objItems.APP_DEPT_INFO = this.hidOptionContent.Value.ToString();
                    objItems.APP_DEPT_DATE = this.hidOptionDate.Value.ToString();
                    if (new TOaPartBuyRequstLogic().Edit(objItems))
                    {
                        wfControl.SaveInstStepServiceData("采购计划ID", "task_id", task_id, "2");
                    }
                    else
                    {
                        LigerDialogAlert("数据发送失败！", "error");
                        return;
                    }
                }
                #endregion

                #region//第三个环节（仓管员审核）
                if (this.Task_Order.Value.ToString().Equals("3"))
                {
                    objItems.ID = task_id;
                    objItems.REMARK1 = "0";
                    objItems.STATUS = "9";
                    objItems.APP_MANAGER_ID = this.hidUserId.Value.ToString();
                    objItems.APP_MANAGER_INFO = this.hidOptionContent.Value.ToString();
                    objItems.APP_MANAGER_DATE = this.hidOptionDate.Value.ToString();
                    if (new TOaPartBuyRequstLogic().Edit(objItems))
                    {
                        wfControl.SaveInstStepServiceData("采购计划ID", "task_id", task_id, "3");
                    }
                    else
                    {
                        LigerDialogAlert("数据发送失败！", "error");
                        return;
                    }
                }
                #endregion
            }
        }
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
    }
    #endregion

    /// <summary>
    /// 附件上传前先做保存
    /// </summary>
    /// <returns></returns>
    /// <remarks>ok</remarks>
    [WebMethod]
    public static string saveSWData(string Remark5)   
    {
        string strResult = "";

        TOaPartBuyRequstVo objFW = new TOaPartBuyRequstVo();
        objFW.REMARK5 = Remark5;
        objFW.STATUS = "0";
        objFW.ID = GetSerialNumber("t_oa_PartBuyID");
        if (new TOaPartBuyRequstLogic().Create(objFW))
        {
            strResult = objFW.ID;
        }
        else
        {
            strResult = "";
        }
        return strResult;
    }

}