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

public partial class Sys_WF_WFSettingTaskInputDetail : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WF_ID.Value = Request.QueryString[TWfSettingTaskVo.WF_ID_FIELD];
            WF_TASK_ID.Value = Request.QueryString[TWfSettingTaskVo.WF_TASK_ID_FIELD];
            InitListData();
            if (!string.IsNullOrEmpty(WF_TASK_ID.Value))
                InitSetpData(WF_TASK_ID.Value);
        }
    }



    public void InitListData()
    {
        TWfSettingFlowVo twf = new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { WF_ID = WF_ID.Value });
        lblCurFlowName.Text = twf.WF_CAPTION;
        lblCurFlowName.ForeColor = System.Drawing.Color.Red;
        InitUserList();
    }

    private void InitSetpData(string strID)
    {
        TWfSettingTaskVo setStep = new TWfSettingTaskLogic().Details(strID);
        BindObjectToControlsMode(setStep);
        string strCommandList = setStep.COMMAND_NAME;
        string strOperList = setStep.OPER_VALUE;
        string strFunctionList = setStep.FUNCTION_LIST;
        TWfSettingTaskFormVo formTemp = new TWfSettingTaskFormLogic().Details(new TWfSettingTaskFormVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
        //开始给表单和空间地址赋值
        UCM_TYPE.SelectedIndex = UCM_TYPE.Items.IndexOf(UCM_TYPE.Items.FindByValue(formTemp.UCM_TYPE));
        UCM_ID.Text = formTemp.UCM_ID;
        rdbtnlstAndOr.SelectedIndex = rdbtnlstAndOr.Items.IndexOf(rdbtnlstAndOr.Items.FindByValue(setStep.TASK_AND_OR));
        //开始给命令节点赋值
        string[] strList1 = strCommandList.Split('|');
        foreach (string strTemp in strList1)
        {
            foreach (ListItem li in ckbxlstCMDList.Items)
            {
                if (li.Value == strTemp)
                    li.Selected = true;
            }
        }
        //开始给附加功能赋值
        string[] strList3 = strFunctionList.Split('|');
        foreach (string strTemp in strList3)
        {
            foreach (ListItem li in ckbxlstPowerList.Items)
            {
                if (li.Value == strTemp)
                    li.Selected = true;
            }
        }


        //开始给用户|职位赋值
        rdbtnlstOperType.SelectedIndex = rdbtnlstOperType.Items.IndexOf(rdbtnlstOperType.Items.FindByValue(setStep.OPER_TYPE));
        rdbtnlstOperType_SelectedIndexChanged(new object(), new EventArgs());
        string[] strList2 = strOperList.Split('|');
        foreach (string strTemp in strList2)
        {
            ListItem li = lsbAll.Items.FindByValue(strTemp);
            if (null != li)
            {
                ListItem liTemp = new ListItem(li.Text, li.Value);
                lsbStep.Items.Add(liTemp);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("WFSettingTaskInput.aspx?WF_ID=" + WF_ID.Value);
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //向右增加
        if (null == lsbAll.SelectedItem)
            return;
        ListItem li = new ListItem();
        li.Text = lsbAll.SelectedItem.Text;
        li.Value = lsbAll.SelectedItem.Value;
        if (lsbStep.Items.IndexOf(li) == -1)
        {
            lsbStep.Items.Add(li);
        }

    }
    protected void btnSub_Click(object sender, EventArgs e)
    {
        //删除掉一个
        if (null == lsbStep.SelectedItem)
            return;
        lsbStep.Items.Remove(lsbStep.SelectedItem);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        // 清空所有
        lsbStep.Items.Clear();
    }
    protected void rdbtnlstOperType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lsbStep.Items.Clear();
        //刷新用户和职位数据
        if (rdbtnlstOperType.SelectedValue == "01")
        {
            InitUserList();
        }
        else if (rdbtnlstOperType.SelectedValue == "02")
        {
            InitPostList();
        }
    }

    public void InitUserList()
    {
        lsbAll.Items.Clear();
        DataTable dt = new TSysUserLogic().SelectByTable(new TSysUserVo() { IS_DEL = "0" });
        lsbAll.DataSource = dt.DefaultView;
        lsbAll.DataTextField = TSysUserVo.REAL_NAME_FIELD;
        lsbAll.DataValueField = TSysUserVo.ID_FIELD;
        lsbAll.DataBind();
    }

    public void InitPostList()
    {
        lsbAll.Items.Clear();
        DataTable dt = new TSysPostLogic().SelectByTable(new TSysPostVo() { IS_DEL = "0" });
        lsbAll.DataSource = dt.DefaultView;
        lsbAll.DataTextField = TSysPostVo.POST_NAME_FIELD;
        lsbAll.DataValueField = TSysPostVo.ID_FIELD;
        lsbAll.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strTips = ValidateData();
        if (!string.IsNullOrEmpty(strTips))
        {
            Alert(strTips);
            return;
        }
        //构建节点记录
        TWfSettingTaskVo setStep = new TWfSettingTaskVo();
        BindControlsToObjectMode(setStep);
        setStep.WF_ID = WF_ID.Value;
        setStep.WF_TASK_ID = WF_TASK_ID.Value == "" ? this.GetGUID() : WF_TASK_ID.Value;
        setStep.ID = setStep.WF_TASK_ID;
        setStep.TASK_AND_OR = rdbtnlstAndOr.SelectedValue;
        //暂时指定为01，后续扩展
        setStep.TASK_TYPE = "01";
        setStep.OPER_TYPE = rdbtnlstOperType.SelectedValue;

        //构建操作人类型和数值
        foreach (ListItem li in lsbStep.Items)
        {
            setStep.OPER_VALUE += li.Value + "|";
        }
        //构建排序的数值


        //构建附加功能值
        foreach (ListItem li in ckbxlstPowerList.Items)
        {
            if (li.Selected)
                setStep.FUNCTION_LIST += li.Value + "|";
        }
        //如果是空，则说明不附加任何功能，直接置空
        setStep.FUNCTION_LIST = setStep.FUNCTION_LIST == "" ? "###" : setStep.FUNCTION_LIST;

        //构建节点命令集合
        List<TWfSettingTaskCmdVo> cmdSetpList = new List<TWfSettingTaskCmdVo>();
        foreach (ListItem li in ckbxlstCMDList.Items)
        {
            if (li.Selected)
            {
                TWfSettingTaskCmdVo cmdTemp = new TWfSettingTaskCmdVo();
                cmdTemp.ID = this.GetGUID();
                cmdTemp.WF_CMD_ID = cmdTemp.ID;
                cmdTemp.WF_ID = WF_ID.Value;
                cmdTemp.WF_TASK_ID = setStep.WF_TASK_ID;
                cmdTemp.CMD_NAME = li.Value;
                cmdTemp.CMD_NOTE = li.Text;
                cmdSetpList.Add(cmdTemp);
                //在节点表中存储一个字符串
                setStep.COMMAND_NAME += cmdTemp.CMD_NAME + "|";
            }
        }
        //构建节点表单页面记录
        TWfSettingTaskFormVo formStep = new TWfSettingTaskFormVo();
        formStep.WF_ID = setStep.WF_ID;
        formStep.WF_TASK_ID = setStep.WF_TASK_ID;
        formStep.ID = this.GetGUID();
        formStep.WF_TF_ID = formStep.ID;
        formStep.UCM_ID = UCM_ID.Text;
        formStep.UCM_TYPE = UCM_TYPE.SelectedValue;

        //保存按钮的处理
        if (string.IsNullOrEmpty(WF_TASK_ID.Value))
        {
            //新增
            TWfSettingTaskLogic taskLogic = new TWfSettingTaskLogic();
            TWfSettingTaskCmdLogic cmdLogic = new TWfSettingTaskCmdLogic();
            TWfSettingTaskFormLogic formLogic = new TWfSettingTaskFormLogic();
            //新增是构建 TASK_ORDER
            setStep.TASK_ORDER = (taskLogic.GetSelectResultCount(new TWfSettingTaskVo() { WF_ID = setStep.WF_ID }) + 1).ToString();

            bool bIsSucess = taskLogic.Create(setStep);
            if (bIsSucess)
            {
                cmdLogic.Create(cmdSetpList);
                formLogic.Create(formStep);
            }
            this.Alert("添加成功");
            //日志记录
        }
        else
        {
            //修改
            TWfSettingTaskLogic taskLogic = new TWfSettingTaskLogic();
            TWfSettingTaskCmdLogic cmdLogic = new TWfSettingTaskCmdLogic();
            TWfSettingTaskFormLogic formLogic = new TWfSettingTaskFormLogic();
            bool bIsSucess = taskLogic.Edit(setStep);
            if (bIsSucess)
            {
                cmdLogic.Delete(new TWfSettingTaskCmdVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
                cmdLogic.Create(cmdSetpList);
                formLogic.Delete(new TWfSettingTaskFormVo() { WF_TASK_ID = setStep.WF_TASK_ID, WF_ID = setStep.WF_ID });
                formLogic.Create(formStep);
            }
            this.Alert("修改成功");
        }

    }

    public string ValidateData()
    {
        //如果简称也描述为空，则提示
        if (string.IsNullOrEmpty(TASK_CAPTION.Text.Trim()) || string.IsNullOrEmpty(TASK_NOTE.Text.Trim()))
        {
            return "节点简称和节点描述不能为空";

        }
        //人员和职位必须存在数据
        if (lsbStep.Items.Count < 1)
        {
            return "工作流节点要求必须有处理者";

        }
        //命令集合表单页面也必须存在数据
        int iCount = 0;
        foreach (ListItem li in ckbxlstCMDList.Items)
        {
            if (li.Selected)
                iCount++;
        }
        if (iCount == 0 || (iCount > 0 && (!ckbxlstCMDList.Items[0].Selected)))
        {
            return "节点处理至少要包含[发送]命令";
        }

        if (UCM_ID.Text.Replace("~/", "") == "" || UCM_TYPE.SelectedValue == "")
        {
            return "表单集必须包含[表单|页面|控件]中的一种";
        }

        return "";
    }
    protected void btnGoNextStep_Click(object sender, EventArgs e)
    {
        //
        if (!string.IsNullOrEmpty(WF_TASK_ID.Value))
        {
            string strWF_ID = WF_ID.Value;
            string strWF_TASK_ID = WF_TASK_ID.Value;
            TWfSettingTaskVo task = new TWfSettingTaskLogic().GetNextStep(new TWfSettingTaskVo() { WF_TASK_ID = strWF_TASK_ID, WF_ID = strWF_ID });
            Response.Redirect(string.Format("WFSettingTaskInputDetail.aspx?{0}={1}&{2}={3} ", TWfSettingTaskVo.WF_ID_FIELD, strWF_ID, TWfSettingTaskVo.WF_TASK_ID_FIELD, task.WF_TASK_ID));
        }
    }
}