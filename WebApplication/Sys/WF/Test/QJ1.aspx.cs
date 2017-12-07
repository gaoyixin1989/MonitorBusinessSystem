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
public partial class Sys_WF_Test_QJ1 : PageBaseForWF, IWFStepRules
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //开始启动所有的事件处理程序
            wfControl.InitWFDict();
            wfControl.ServiceCode = "QJ-001";
            wfControl.ServiceName = "普通员工请假流程";
        }
    }


    void IWFStepRules.LoadAndViewBusinessData()
    {
        //这里是载入和显示业务数据的地方
        List<TWfInstTaskServiceVo> myServiceList = wfControl.INST_STEP_SERVICE_LIST_FOR_OLD;
    }

    bool IWFStepRules.BuildAndValidateBusinessData(out string strMsg)
    {
        //这里是组件和验证业务数据的地方
        strMsg = "";
        return true;
    }

    void IWFStepRules.CreatAndRegisterBusinessData()
    {
        //这里是产生和注册业务数据的地方
        wfControl.SaveInstStepServiceData("测试1", "IDs", "1234556");
        wfControl.SaveInstStepServiceData("测试2", "Names", "测试名字");
        wfControl.SaveInstStepServiceData("测试3", "Codes", "boding");
        wfControl.SaveInstStepServiceData("测试1", "Gotos", "Hi");
    }

    void IWFStepRules.SaveBusinessDataFromPageControl()
    {
        //这里是执行业务数据保存的地方，此处由工作流控件间接调用
        string strMessage = "";
        for (int ii = 0; ii < 100; ii++)
        {
            strMessage += ii.ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //
        wfControl.SetMoreDealUserFlag = true;
        wfControl.MoreDealUserForAdd("000000001");
        //List<string> listUser=new List<string>();
        //listUser.Add("000000141");
        //wfControl.MoreDealUserForAddList(listUser);
        wfControl.MoreDealUserForAdd("000000141");
        wfControl.MoreDealUserForAdd("000000142");

    }

}