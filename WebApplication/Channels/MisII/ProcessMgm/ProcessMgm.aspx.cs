using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.ProcessMgm;
using i3.BusinessLogic.Channels.Mis.ProcessMgm;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using System.Configuration;

namespace WebApplication.Channels.Base.ProcessMgm
{
    public partial class ProcessMgm : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string strPlanId = GetPlanID();
                this.strPlanId.Value = strPlanId;

                // 前台修改后，保存对应的控件值，并且
                if (Request["type"] != null && Request["type"].ToString() == "saveDate")
                {
                    string strType = Request["strType"].ToString(); //更新数据类型
                    string strStart = Request["strStart"].ToString();
                    string strFinish = Request["strFinish"].ToString();
                    string strStartMofified = Request["bStartMofified"].ToString();

                    saveDate(strPlanId, strType, strStart, strFinish, strStartMofified);

                    // 更新前台控件值，显示各阶段时间
                    string strRetJson = initFrontPage(strPlanId);

                    Response.Write(strRetJson);
                    Response.End();
                }

                // 首次调用，调取此接口，用于创建某个任务各阶段的时限
                if (Request["type"] != null && Request["type"].ToString() == "createDate")
                {
                    
                    // 更新前台控件值，显示各阶段时间
                    createDate(strPlanId);

                    string strRetJson = initFrontPage(strPlanId);

                    Response.Write(strRetJson);
                    Response.End();
                }


                if (Request["type"] != null && Request["type"].ToString() == "getNodeTime")
                {
                    string strNodeName = Request["strNodeName"].ToString(); //更新数据类型
                    string strStartTime = "";
                    string strFinishTime = "";

                    getNodeTimeFrmDB(strPlanId, strNodeName, ref strStartTime, ref strFinishTime);

                    // 更新前台控件值，显示各阶段时间
                    string strRetJson = "";
                    strRetJson = appendJson(strRetJson, "strStartTime", strStartTime);
                    strRetJson = appendJson(strRetJson, "strFinishTime", strFinishTime);

                    Response.Write(strRetJson);
                    Response.End();
                }
            }
        }

        // 用户手动调整时间后，数据更新
        public void saveDate(string strPlanId, string strNodeName, string strStartTime, string strFinishTime, string strStartMofified)
        {
            TMisMonitorProcessMgmLogic objLogic = new TMisMonitorProcessMgmLogic();
            TMisMonitorProcessMgmVo objVo = new TMisMonitorProcessMgmVo();

            int i = 0;
            for (; i < arrTaskName.Length; i++)
            {
                if (strNodeName == arrTaskName[i])
                {
                    break;
                }
            }

            if (i == arrTaskName.Length)
            {
                // 没有找到该节点的配置信息，应该是前台传过来的参数不对
                return;
            }

            // 任务开始时间更新，结束时间自动更新
            if ("true" == strStartMofified)
            {
                strFinishTime = GetNextNDate(strStartTime, arrTaskValue[i]);
            }


            // 更新本节点信息
            objVo.TASK_ID = strPlanId;
            objVo.MONITOR_TYPE = strNodeName;
            objVo.MONITOR_TIME_START = strStartTime;
            objVo.MONITOR_TIME_FINISH = strFinishTime;

            objLogic.Edit(objVo);

            // 更新后续节点的信息
            string strFlowingNodeStart = strFinishTime;

            if (0 == i)
            {
                //任务总体时间更新，各节点不自动更新
                return;
            }

            ++i;
            for (; i < arrTaskName.Length; i++)
            {
                objVo.Init();

                objVo.TASK_ID = strPlanId;
                objVo.MONITOR_TYPE = arrTaskName[i];
                objVo.MONITOR_TIME_START = strFlowingNodeStart;
                objVo.MONITOR_TIME_FINISH = GetNextNDate(strFlowingNodeStart, arrTaskValue[i]);

                objLogic.Edit(objVo);

                strFlowingNodeStart = objVo.MONITOR_TIME_FINISH;
            }
        }

        // 首次调用此表单需要创建数据
        public void createDate(string strPlanId)
        {
            TMisMonitorProcessMgmLogic objLogic = new TMisMonitorProcessMgmLogic();
            TMisMonitorProcessMgmVo objVo = new TMisMonitorProcessMgmVo();

            objVo.TASK_ID = strPlanId;

            DataTable dt = objLogic.SelectByTable(objVo);

            // 第2+次加载此页面时，不用重复创建信息
            if (0 != dt.Rows.Count)
            {
                return;
            }


            for (int i = 0; i < arrTaskName.Length; i++)
            {
                objVo.Init();

                objVo.TASK_ID = strPlanId;

                objVo.ID = i3.View.PageBase.GetSerialNumber("t_mis_process_mgm_id");

                string strNodeName = arrTaskName[i];
                string strStartTime = "";
                string strFinishTime = "";

                GetNodeDate(strNodeName, ref strStartTime, ref strFinishTime);

                objVo.MONITOR_TIME_START = strStartTime;
                objVo.MONITOR_TIME_FINISH = strFinishTime;
                objVo.MONITOR_TYPE = strNodeName;

                objLogic.Create(objVo);
            }

            return;
        }

        // 从数据库中查询某任务相关的记录，并初始化控件显示
        private string initFrontPage(string strPlanId)
        {
            // 初始化前台各控件
            string strStartTime = "";
            string strFinishTime = "";

            // 任务总体时间
            getNodeTimeFrmDB(strPlanId, "TASK_DATE_TOTAL", ref strStartTime, ref strFinishTime);

            string strRetJson = "";
            strRetJson = appendJson(strRetJson, "taskDateStart", strStartTime);
            strRetJson = appendJson(strRetJson, "taskDateFinish", strFinishTime);


            // 采样时间
            getNodeTimeFrmDB(strPlanId, "SAMPLE_DATE", ref strStartTime, ref strFinishTime);
            strRetJson = appendJson(strRetJson, "sampleDateStart", strStartTime);
            strRetJson = appendJson(strRetJson, "sampleDateFinish", strFinishTime);

            // 分析时间
            getNodeTimeFrmDB(strPlanId, "ANALYSE_DATE", ref strStartTime, ref strFinishTime);
            strRetJson = appendJson(strRetJson, "analyseDateStart", strStartTime);
            strRetJson = appendJson(strRetJson, "analyseDateFinish", strFinishTime);

            // 分析室主任审核日期
            getNodeTimeFrmDB(strPlanId, "AUDIT_LAB_LEADER", ref strStartTime, ref strFinishTime);
            strRetJson = appendJson(strRetJson, "auditLabLeaderFinish", strFinishTime);


            //质控室审核日期
            getNodeTimeFrmDB(strPlanId, "AUDIT_QC", ref strStartTime, ref strFinishTime);
            strRetJson = appendJson(strRetJson, "auditQCFinish", strFinishTime);

            //主管副站长审核日期
            getNodeTimeFrmDB(strPlanId, "AUDIT_CAPTION", ref strStartTime, ref strFinishTime);
            strRetJson = appendJson(strRetJson, "auditCaptionFinish", strFinishTime);

            //质量负责人审核日期
            getNodeTimeFrmDB(strPlanId, "AUDIT_QC_LEADER", ref strStartTime, ref strFinishTime);
            strRetJson = appendJson(strRetJson, "auditQCLeaderFinish", strFinishTime);

            //技术负责人审核日期
            getNodeTimeFrmDB(strPlanId, "AUDIT_TECH_LEADER", ref strStartTime, ref strFinishTime);
            strRetJson = appendJson(strRetJson, "AuditTechLeaderFinish", strFinishTime);


            return strRetJson;

        }

        private int getNodeTimeFrmDB(string strPlanId, string strNodeType, ref string strStartTime, ref string strFinishTime)
        {
            strStartTime = "";
            strFinishTime = "";

            TMisMonitorProcessMgmLogic objLogic = new TMisMonitorProcessMgmLogic();
            TMisMonitorProcessMgmVo objVo = new TMisMonitorProcessMgmVo();
            objVo.TASK_ID = strPlanId;

            DataTable dt = objLogic.SelectByTable(objVo);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strTmpNodeType = dt.Rows[i]["MONITOR_TYPE"].ToString().Trim();

                if (strTmpNodeType == strNodeType)
                {
                    strStartTime = dt.Rows[i]["MONITOR_TIME_START"].ToString().Trim();
                    strFinishTime = dt.Rows[i]["MONITOR_TIME_FINISH"].ToString().Trim();

                    return 0;
                }
            }

            return 1;
        }

        //获取某一个流程节点的计划完成时间
        private void GetNodeDate(string strNodeName, ref string strStartTime, ref string strFinishTime)
        {
            strStartTime = "";
            strFinishTime = "";

            // 获取上一个节点的时间，并增加偏移量
            int i = 0;
            for(;i<arrTaskName.Length;i++)
            {
                if (strNodeName == arrTaskName[i])
                {
                    break;
                }
            }

            if (i == arrTaskName.Length)
            {
                return;
            }

            if (0 == i)
            {
                strStartTime = GetCurrentDate();
                strFinishTime = GetNextNDate(strStartTime, arrTaskValue[i]);
            }
            else if (1 == i)
            {
                strStartTime = GetCurrentDate();
                strFinishTime = GetNextNDate(strStartTime, arrTaskValue[i]);
            }
            else
            {
                string strTmpStartTime = "";
                string strTmpFinishTime = "";

                GetNodeDate(arrTaskName[i - 1], ref strTmpStartTime, ref strTmpFinishTime);

                strStartTime = strTmpFinishTime;
                strFinishTime = GetNextNDate(strStartTime, arrTaskValue[i]);
            }

        }

        // 此方法供后台调用，用resultID查任务要求时间
        public int GetAskingTimeByResultID(string strRstID, string strNodeType, ref string strStartTime, ref string strFinishTime)
        {
            TMisMonitorResultLogLogic objMomRstVo = new TMisMonitorResultLogLogic();
            string strPlanId = objMomRstVo.GetPlanID(strRstID);

            return getNodeTimeFrmDB(strPlanId, strNodeType, ref strStartTime, ref strFinishTime);
        }

        private string InitCfg()
        {
            //if (arrTaskName.Length == 0)
            {
                string strTmpCfg = ConfigurationManager.AppSettings["DATE_SHIFT_OF_TASK_NAME"].ToString();

                arrTaskName = strTmpCfg.Split(',');

                strTmpCfg = ConfigurationManager.AppSettings["DATE_SHIFT_OF_TASK_VALUE"].ToString();

                arrTaskValue = strTmpCfg.Split(',');

                if (arrTaskName.Length != arrTaskValue.Length
                    || arrTaskName.Length == 0)
                {
                    return "config file error.";
                }
            }

            return "init cfg success.";
        }

        // 
        private string GetPlanID()
        {
            string strPlanId;

            if (Request["strPlanId"] != null)
            {
                strPlanId = Request["strPlanId"].ToString();
            }
            else if (Request["strResultID"] != null)
            {
                string strResultID = Request["strResultID"].ToString();

                TMisMonitorResultLogLogic objMomRstVo = new TMisMonitorResultLogLogic();
                strPlanId = objMomRstVo.GetPlanID(strResultID);
            }
            else if (Request["strSampleID"] != null)
            {
                string strResultID = Request["strSampleID"].ToString();

                TMisMonitorSampleInfoLogic objSampleLgc = new TMisMonitorSampleInfoLogic();
                strPlanId = objSampleLgc.GetPlanID(strResultID);
            }
            else if (Request["strContratId"] != null)
            {
                string strTaskID = Request["strContratId"].ToString();

                TMisMonitorTaskLogic objTakeLgc = new TMisMonitorTaskLogic();
                strPlanId = objTakeLgc.GetPlanIDByContractID(strTaskID);
            }
            else
            {
                strPlanId = "error";
            }

            return strPlanId;
        }

        public ProcessMgm()
        {
            arrTaskName = new string[1];
            arrTaskValue = new string[1];

            InitCfg();
        }

        private string[] arrTaskName;
        private string[] arrTaskValue;
    }
}