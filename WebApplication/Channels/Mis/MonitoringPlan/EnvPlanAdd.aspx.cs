using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using System.Collections;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.BusinessLogic.Channels.Mis.Monitor.QC;
using i3.ValueObject.Sys.General;
using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.ValueObject.Channels.Env.Point.River;
using i3.BusinessLogic.Channels.Env.Point.River;

using i3.ValueObject.Channels.Mis.Monitor.Task;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.BusinessLogic.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

public partial class Channels_Mis_MonitoringPlan_EnvPlanAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //data:"{'strPlanId':'"++"','strEvnTypeId':'"++"','strPointItem':'"++"','strPointItemName':'"+()+"','strProjectName':'"+()+"','strTableName':'"++"','strKeyColumns':'"++"'}",

    [WebMethod]
    public static string InsertEnvContractPoint_Pan(string strPlanId, string strEvnTypeId, string strPointItem, string strPointItemName, string strProjectName, string strTableName, string strKeyColumns)
    {
        string flag = "0";
        removeRepeatPoint(ref strPointItem, ref strPointItemName);

        TMisContractPointVo objItems = new TMisContractPointVo();
        objItems.MONITOR_ID = strEvnTypeId;
        objItems.POINT_ID = strPointItem;
        objItems.POINT_NAME = strPointItemName;
        
        //if (new TMisContractPointLogic().InsertEnvPoints(objItems, strPlanId, strKeyColumns, strTableName))
        //{
            SavePlanPeopleForEnv(strPlanId);
            flag = "1";
        //}
        return flag;
    }

    public static void removeRepeatPoint(ref string strPointItem, ref string strPointItemName)
    {
        string[] itemArr = strPointItem.Split(';');
        string[] itemName = strPointItemName.Split(';');

        string strAAA = "";
        string strBBB = "";

        for (int i = 0; i < itemArr.Length; i++)
        {
            if (!strAAA.Contains(itemArr[i]))
            {
                strAAA += (strAAA.Length > 0 ? ";" : "") + itemArr[i];
                strBBB += (strBBB.Length > 0 ? ";" : "") + itemName[i];
            }
        }

        if (strAAA.Length > 0)
        {
            strPointItem = strAAA.TrimEnd(';');
            strPointItemName = strBBB.TrimEnd(';');
        }
       
    }

    /// <summary>
    /// 保存默认监测计划岗位负责人 Create By Castle (胡方扬) 2013-4-1
    /// </summary>
    /// <returns></returns>
    public static void SavePlanPeopleForEnv(string strPlanId)
    {
        DataTable dtMonitor = new DataTable();
        TMisContractPointFreqVo objItemspan = new TMisContractPointFreqVo();
        objItemspan.IF_PLAN = "0";
        dtMonitor = new TMisContractPointFreqLogic().GetPointMonitorInforForPlan(objItemspan, strPlanId);

        //DataTable dtMonitor = GetPointMonitorInfor(strPlanId);

        DataTable dtTemple = new DataTable();
        TSysDutyVo objItemspan1 = new TSysDutyVo();
        objItemspan1.DICT_CODE = "duty_sampling";
        dtTemple = new TSysDutyLogic().SelectTableDutyUser(objItemspan1);
        //DataTable dtTemple = GetMonitorDutyInforTable();
        DataTable dtMonitorDutyUser = new DataTable();
        dtMonitorDutyUser = dtTemple.Copy();

        dtMonitorDutyUser.Clear();
        //获取默认负责人
        for (int n = 0; n < dtMonitor.Rows.Count; n++)
        {
            DataRow[] drowArr = dtTemple.Select(" IF_DEFAULT='0' AND MONITOR_TYPE_ID='" + dtMonitor.Rows[0]["ID"].ToString() + "'");
            if (drowArr.Length > 0)
            {
                foreach (DataRow drow in drowArr)
                {
                    dtMonitorDutyUser.ImportRow(drow);
                }
                dtMonitorDutyUser.AcceptChanges();
            }
            else
            {
                drowArr = dtTemple.Select(" MONITOR_TYPE_ID='" + dtMonitor.Rows[0]["ID"].ToString() + "'");
                if (drowArr.Length > 0)
                {
                    dtMonitorDutyUser.ImportRow(drowArr[0]);
                }
                dtMonitorDutyUser.AcceptChanges();
            }
        }

        //if (drowArr.Length > 0)
        //{
        //    foreach (DataRow drow in drowArr)
        //    {
        //        dtMonitorDutyUser.ImportRow(drow);
        //    }
        //    dtMonitorDutyUser.AcceptChanges();
        //}
        string strMonitorId = "", strUserId = "";
        foreach (DataRow drr in dtMonitor.Rows)
        {
            for (int i = 0; i < dtMonitorDutyUser.Rows.Count; i++)
            {
                if (drr["ID"].ToString() == dtMonitorDutyUser.Rows[i]["MONITOR_TYPE_ID"].ToString())
                {
                    strMonitorId += drr["ID"].ToString() + ";";
                    strUserId += dtMonitorDutyUser.Rows[i]["USERID"].ToString() + ";";
                }
            }
        }
        TMisContractUserdutyVo objItems = new TMisContractUserdutyVo();
        if (!String.IsNullOrEmpty(strPlanId))
        {
            objItems.CONTRACT_PLAN_ID = strPlanId;
            string[] strMonitArr = null, strUserArr = null;
            if (!String.IsNullOrEmpty(strMonitorId) && !String.IsNullOrEmpty(strUserId))
            {
                strMonitArr = strMonitorId.Substring(0, strMonitorId.Length - 1).Split(';');
                strUserArr = strUserId.Substring(0, strUserId.Length - 1).Split(';');
                if (strMonitArr != null && strUserArr != null)
                    new TMisContractUserdutyLogic().SaveContractPlanDutyForEnv(objItems, strMonitArr, strUserArr);

            }
        }
    }
    /// <summary>
    /// 获取监测项目相关信息
    /// </summary>
    /// <param name="strItemCode">监测项目ID</param>
    /// <param name="strItemName">监测项目信息名称</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemInfoName(string strItemCode, string strItemName)
    {
        string strReturnValue = "";
        i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo();
        TBaseItemInfoVo.ID = strItemCode;
        DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemInfoLogic().SelectByTable(TBaseItemInfoVo);
        if (dt.Rows.Count > 0)
            strReturnValue = dt.Rows[0][strItemName].ToString();
        return strReturnValue;
    }
    [WebMethod]
    public static string getMonitorType(string strTypeId)
    {
        string strReturnValue = "";
        i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo objMonitorTypeVo = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(strTypeId);
        strReturnValue = objMonitorTypeVo.REMARK1 == "" ? objMonitorTypeVo.ID : objMonitorTypeVo.REMARK1;
        return strReturnValue;
    }
}