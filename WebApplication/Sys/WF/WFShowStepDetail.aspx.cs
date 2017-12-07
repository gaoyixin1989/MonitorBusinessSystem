using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using i3.View;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

public partial class Sys_WF_WFShowStepDetail : PageBaseForWF
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
        List<TWfInstTaskDetailVo> InstTaskList = new TWfInstTaskDetailLogic().SelectByObject(new TWfInstTaskDetailVo() { WF_INST_ID = strID, SORT_FIELD = TWfInstTaskDetailVo.INST_TASK_STARTTIME_FIELD, SORT_TYPE = i3.ValueObject.ConstValues.SortType.ASC }, 0, 200);
        //CreatAllData(control, taskList, InstTaskList);
        CreatShowData(control, taskList, InstTaskList);
    }

    public void CreatShowData(TWfInstControlVo control, List<TWfSettingTaskVo> taskList, List<TWfInstTaskDetailVo> InstTaskList)
    {
        //首先根据流程实例来绘出 流程相关信息；
        //string strWFHtml = "<div class='layout20121'><div style='margin-bottom;8px;'><table><tr><td style='width:60px;'> 流程名称：</td><td>{0}</td></tr><tr><td>业务单号：</td><td>{1}</td></tr><tr><td>业务名称：</td><td>{2}</td></tr><tr><td>启动时间：</td><td>{3}</td></tr></table></div> #TULI# #STEP#</div>";
        //strWFHtml = string.Format(strWFHtml, control.WF_CAPTION, control.WF_SERVICE_CODE, control.WF_SERVICE_NAME, control.WF_STARTTIME);
        string strWFHtml = "<div class='layout20121'> #TULI# #STEP#</div>";

        strWFHtml = strWFHtml.Replace("#TULI#", "<div style= 'margin-bottom;8px;'><table><tr><td>图例：</td><td style='background-color:#5a8f5a;width:15px;height:15px;'></td><td>已处理</td><td style='background-color:#de9a1d;width:15px;height:15px;'></td><td>待处理</td><td style='background-color:#e34323;width:15px;height:15px;'></td><td>特殊处理</td><td style='background-color:#a9a9a9;width:15px;height:15px;'></td><td>未流转</td></tr><tr style='Height:5px;' ><td></td></tr></table></div>");
        string strStepHtml = CreatStepHtml(taskList, InstTaskList);
        //在根据流程实例列表来绘出 各个环节运行的状态 已处理为绿色，正常待处理为黄色，已挂起（暂停）的为灰色，已回退为蓝色，销毁为红色
        spShowPic.InnerHtml = strWFHtml.Replace("#STEP#", strStepHtml);
    }

    public string CreatStepHtml(List<TWfSettingTaskVo> taskList, List<TWfInstTaskDetailVo> InstTaskList)
    {

        string strJianTou = "<img src='img/down2012.gif' />";
        //string strModel = "<div " + strConstColor + ">#A1#</div>" + strJianTou;//增加向下的箭头的话，在这个里面加就行了。在最后时移除一个
        string strModel = "<div class='#DCD#'><h2>{0}</h2><p><span>环节状态：</span><strong>{1}</strong><br /><span>处理者：</span>{2}&nbsp;&nbsp;&nbsp;<span>办理时间：</span>{3}</p>" + strJianTou + "</div>";
        //string strInstStepHtml = "环节名称：{0}<br />环节状态：<span style='font-weight:bold;'>{1}</span><br />处理者：{2}";
        //<div class="{#BCD#}"><h2>{0}</h2><p><span>环节状态：</span><strong>{1}</strong><br /><span>处理者：</span>{2}</p></div>

        //update by ssz 当任务未进入工作流时直接返回 begin
        if (InstTaskList.Count <= 0)
        {
            return "";
        }
        //update end
        //获取最后一在用的环节 
        TWfInstTaskDetailVo instTaskLast = InstTaskList[InstTaskList.Count - 1];
        List<TWfInstTaskDetailVo> instHaveStep = GetTrueInstStep(InstTaskList);
        int iStepOrder = 0;
        string strHtmlString = "";
        foreach (TWfSettingTaskVo tsdv in taskList)
        {

            string strHeader = "";
            //设置环节颜色
            string strStepColor = "listgreen";
            //if (instTaskLast.WF_TASK_ID == tsdv.WF_TASK_ID)
            //{
            //    strStepColor = "listyellow";
            //    iStepOrder = int.Parse(tsdv.TASK_ORDER);
            //}
            if (instTaskLast.WF_TASK_ID == tsdv.WF_TASK_ID && instTaskLast.INST_TASK_STATE == "2A")
            {
                strStepColor = "listyellow";
                iStepOrder = int.Parse(tsdv.TASK_ORDER);
            }
            if (int.Parse(tsdv.TASK_ORDER) > iStepOrder && iStepOrder != 0)
            {
                //如果是退回的节点，则要显示不同颜色
                bool bIsBack = false;
                foreach (TWfInstTaskDetailVo ttdv in InstTaskList)
                {
                    if (tsdv.WF_TASK_ID == ttdv.WF_TASK_ID && ttdv.INST_TASK_DEAL_STATE == TWfCommDict.StepDealState.ForBack)
                    {
                        bIsBack = true;
                        break;
                    }
                }
                if (bIsBack)
                    strStepColor = "listred";
                else
                    strStepColor = "listgray";
            }
            //先替换配置环节内容
            strHeader = strModel.Replace("#DCD#", strStepColor);
            //增加信号量，如果一直没有匹配上，则说明需要现实配置信息，环节配置信息也需要显示
            bool bIsHaveInstTask = false;

            foreach (TWfInstTaskDetailVo ttdv in instHaveStep)
            {
                if (ttdv.WF_TASK_ID == tsdv.WF_TASK_ID)
                {
                    bIsHaveInstTask = true;

                    string strHeaderUser = ttdv.REAL_USER.Trim().Length > 0 ? ttdv.REAL_USER : ttdv.OBJECT_USER;

                    strHeader = string.Format(strHeader,
                            ttdv.INST_TASK_CAPTION,
                           GetStepStateName(ttdv.INST_TASK_STATE, ttdv.INST_TASK_DEAL_STATE),
                           GetUserNameFromID(strHeaderUser, true),
                           ttdv.INST_TASK_ENDTIME
                           );

                    break;
                }
            }
            if (!bIsHaveInstTask)
            {
                //只显示配置本身信息，
                strHeader = string.Format(strHeader, tsdv.TASK_CAPTION, "未启动", "","");
            }

            strHtmlString += strHeader;

        }
        return strHtmlString.Remove(strHtmlString.Length - strJianTou.Length - 6);
    }

    /// <summary>
    /// 排序，取出时间最靠后的所有环节，组成单一数据
    /// </summary>
    /// <param name="InstTaskList"></param>
    /// <returns></returns>
    private List<TWfInstTaskDetailVo> GetTrueInstStep(List<TWfInstTaskDetailVo> InstTaskList)
    {
        List<TWfInstTaskDetailVo> list = new List<TWfInstTaskDetailVo>();
        foreach (TWfInstTaskDetailVo temp in InstTaskList)
        {
            if (list.Contains(temp))
                continue;
            //如果没有，则开始比对
            TWfInstTaskDetailVo baidu = new TWfInstTaskDetailVo();
            bool bIsHave = false;
            bool bIsExsit = false;
            foreach (TWfInstTaskDetailVo google in list)
            {
                if (google.WF_TASK_ID == temp.WF_TASK_ID)
                {
                    bIsExsit = true;
                    if (DateTime.Parse(google.INST_TASK_STARTTIME) < DateTime.Parse(temp.INST_TASK_STARTTIME))
                    {
                        bIsHave = true;
                        baidu = temp;
                    }
                }
            }
            if (!bIsExsit)
                list.Add(temp);
            else if (bIsHave)
            {
                foreach (TWfInstTaskDetailVo tt in list)
                {
                    if (tt.WF_TASK_ID == temp.WF_TASK_ID && tt.ID != temp.ID)
                    {
                        list.Remove(tt);
                        list.Add(baidu);
                        break;
                    }
                }
            }
        }

        return list;
    }

    /// <summary>
    /// 获得指定对象在列表出现的位置（只针对转发和回退，返元的那种）
    /// </summary>
    /// <param name="InstTaskList"></param>
    /// <param name="ttdv"></param>
    /// <returns></returns>
    private TWfInstTaskDetailVo GetObjectFromType(List<TWfInstTaskDetailVo> InstTaskList, string strKey, string strType)
    {
        foreach (TWfInstTaskDetailVo d in InstTaskList)
        {
            if (strType == "ID" && d.ID == strKey)
                return d;
            if (strType == "PRE_ID" && d.PRE_INST_TASK_ID == strKey)
                return d;
        }
        return new TWfInstTaskDetailVo();
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