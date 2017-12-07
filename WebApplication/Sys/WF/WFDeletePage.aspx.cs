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

public partial class Sys_WF_WFDeletePage : PageBaseForWF
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strType = Request.QueryString["View"];
            string strID = Request.QueryString["ID"];
            hdID.Value = strID;
            hdType.Value = strType;
            //初始化的入口
            InitWFData();
        }
    }


    /// <summary>
    /// 初始化所有数据的入口
    /// </summary>
    public void InitWFData()
    {
        string strID = hdID.Value;
        string strType = hdType.Value;
        //先获取流程，环节数据等内容
        TWfInstControlVo control = new TWfInstControlLogic().Details(strID);
        List<TWfSettingTaskVo> taskList = new TWfSettingTaskLogic().SelectByObjectListForSetp(new TWfSettingTaskVo() { WF_ID = control.WF_ID });
        List<TWfInstTaskDetailVo> InstTaskList = new TWfInstTaskDetailLogic().SelectByObject(new TWfInstTaskDetailVo() { WF_INST_ID = strID }, 0, 200);
        CreatAllData(control, taskList, InstTaskList);

    }

    /// <summary>
    /// 输入3个参数，即可获得流程的起始化状态
    /// </summary>
    /// <param name="control">流程控制实例对象</param>
    /// <param name="taskList">流程环节配置列表</param>
    /// <param name="InstTaskList">流程环节实例列表</param>
    public void CreatAllData(TWfInstControlVo control, List<TWfSettingTaskVo> taskList, List<TWfInstTaskDetailVo> InstTaskList)
    {
        //首先根据流程实例来绘出 流程相关信息；
        string strWFHtml = "<div><table><tr><td>流程名称:{0}</td><td>流水号:{1}</td></tr><tr><td>业务名称:{2}</td><td>启动时间:{3}</td></tr></table></div>";

        strWFHtml = string.Format(strWFHtml, control.WF_CAPTION, control.WF_SERIAL_NO, control.WF_SERVICE_NAME, control.WF_STARTTIME);
        strWFHtml += "<div><table><tr><td style='background-color:Green;width:15px;height:15px;'></td><td>已处理</td><td style='background-color:Orange;width:15px;height:15px;'></td><td>待处理</td><td style='background-color:Gray;width:15px;height:15px;'></td><td>未生成</td></tr></table></div>";
        //根据流程环节配置列表来绘出 环节相关图形和关系；
        string strStepHtml = CreatStepSpanHtml(taskList, InstTaskList);
        spShowPic.InnerHtml = strWFHtml + strStepHtml;
        //在根据流程实例列表来绘出 各个环节运行的状态 已处理为绿色，正常待处理为黄色，已挂起（暂停）的为灰色，已回退为蓝色，销毁为红色

    }

    public string CreatStepSpanHtml(List<TWfSettingTaskVo> taskList, List<TWfInstTaskDetailVo> InstTaskList)
    {
        string strDivModel = "<div>{0}</div>";
        string strDivPanel = "";
        foreach (TWfSettingTaskVo temp in taskList)
        {
            List<TWfInstTaskDetailVo> list = new List<TWfInstTaskDetailVo>();
            foreach (TWfInstTaskDetailVo inst in InstTaskList)
            {
                if (inst.WF_TASK_ID == temp.WF_TASK_ID)
                    list.Add(inst);
            }
            //开始调用生成的函数
            strDivPanel += string.Format(strDivModel, GetStepModel(temp, list));
        }
        return strDivPanel;
    }

    /// <summary>
    /// 根据配置环节和对应的实例环节来绘图
    /// </summary>
    /// <param name="task">配置环节</param>
    /// <param name="instTaskList">实例环节集合</param>
    /// <returns>生成的Html标记（Table承载）</returns>
    public string GetStepModel(TWfSettingTaskVo task, List<TWfInstTaskDetailVo> instTaskList)
    {
        string strModel = "<table><tr><td style='width:100px;background-color:{6};' ></td><td style='width:280px;'>环节名称:{0}<br />环节状态:{1}<br />目标处理者:{2}<br />实际处理者:{3}<br />产生时间:{4}<br />处理时间:{5}<br /></td></tr></table>";
        string strTaskCaption = task.TASK_CAPTION;
        string strStepStateName = "";
        string strObjectUser = "";
        string strRealUser = "";
        string strTaskStartTime = "";
        string strTaskEndTime = "";
        string strStepColor = "Gray";
        foreach (TWfInstTaskDetailVo inst in instTaskList)
        {
            strStepStateName += GetStepStateName(inst.INST_TASK_STATE);
            strStepStateName += ",";
            strObjectUser += GetUserNameFromID(inst.OBJECT_USER, true);
            strObjectUser += ",";
            strRealUser += GetUserNameFromID(inst.REAL_USER, true);
            strRealUser += ",";
            strTaskStartTime = inst.INST_TASK_STARTTIME;
            strTaskEndTime += inst.INST_TASK_ENDTIME;
            strTaskEndTime += ",";
            strStepColor = GetColorFromState(strStepColor, inst.INST_TASK_STATE);
            break;
        }
        strStepStateName = strStepStateName.TrimEnd(',');
        strObjectUser = strObjectUser.TrimEnd(',').Replace(",", "<br />");
        strRealUser = strRealUser.TrimEnd(',').Replace(",", "<br />");
        strTaskEndTime = strTaskEndTime.TrimEnd(',').Replace(",", "<br />");

        string strStepSpan = string.Format(strModel,
            strTaskCaption,
            strStepStateName,
            strObjectUser,
            strRealUser,
            strTaskStartTime,
            strTaskEndTime,
            strStepColor);
        return strStepSpan;
    }

    /// <summary>
    /// 只有存在绿色的颜色，就显示绿色，不存在就显示最后一种颜色
    /// </summary>
    /// <param name="strStepColor"></param>
    /// <param name="strInstStepState"></param>
    /// <returns></returns>
    private string GetColorFromState(string strStepColor, string strInstStepState)
    {
        string strColor = GetStepStateColor(strInstStepState);
        if (strColor == strStepColor)
            return strStepColor;
        if (strColor == "Green")
            return strColor;
        if (strColor == "Orange")
            return strColor;
        else
            return strStepColor;
    }




}