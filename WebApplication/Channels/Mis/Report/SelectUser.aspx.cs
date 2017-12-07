using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Channels.OA.ARCHIVES;
using i3.ValueObject.Channels.OA.ARCHIVES;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.BusinessLogic.Channels.OA.EMPLOYE;
using i3.ValueObject.Channels.OA.EMPLOYE;
using i3.ValueObject.Sys.Duty;
using System.Data;
using i3.BusinessLogic.Sys.Duty;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Base.MonitorType;

/// <summary>
/// 功能描述：人员选择
/// 创建时间：2013-1-11
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Report_SelectUser : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        // 获取监测任务ID
        if (Request["task_id"] != null)
        {
            this.hdnTaskID.Value = Request.QueryString["task_id"];
        }
        // 获取用户信息
        if (Request["type"] != null && Request["type"].ToString() == "getUserInfo")
        {
            strResult = getUserInfo();
            Response.Write(strResult);
            Response.End();
        }
        // 获取部门
        if (Request["type"] != null && Request["type"].ToString() == "getDept")
        {
            strResult = getDept();
            Response.Write(strResult);
            Response.End();
        }
        // 获取用户信息
        if (Request["type"] != null && Request["type"].ToString() == "getDict")
        {
            strResult = getDict();
            Response.Write(strResult);
            Response.End();
        }
        //获取用户信息
        if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getUserList")
        {
            strResult = getUserList();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <returns></returns>
    protected string getUserInfo()
    {
        //获得所有监测类别
        DataTable dtSonTask = new TMisMonitorSubtaskLogic().selectAllContractType(this.hdnTaskID.Value);
        string reslut = "";
        DataTable dtTotal = new DataTable();
        TSysDutyVo objItems = new TSysDutyVo();
        /*
        foreach (DataRow dr in dtSonTask.Rows)
        {
            DataTable dt = new DataTable();
            objItems.DICT_CODE = "duty_reportschedule";
            objItems.MONITOR_TYPE_ID = dr["MONITOR_ID"].ToString();
            dt = new TSysDutyLogic().SelectByUnionTable(objItems, 0, 0);
            dt.Columns.Add("MONITOR_NAME");
            dt.Columns.Add("POST_NAME");
            //添加监测类别列
            foreach (DataRow drSon in dt.Rows)
            {
                drSon["MONITOR_NAME"] = new TBaseMonitorTypeInfoLogic().Details(dr["MONITOR_ID"].ToString()).MONITOR_TYPE_NAME;
                drSon["POST_NAME"] = new TSysPostLogic().Details(new TSysUserPostLogic().Details(new TSysUserPostVo() { USER_ID = drSon["ID"].ToString() }).POST_ID).POST_NAME;
            }
            dtTotal.Merge(dt);
        }
        */
        DataTable dt = new DataTable();
        objItems.DICT_CODE = "duty_reportschedule";
        objItems.MONITOR_TYPE_ID = "000000001";  //默认报告编制人是废水类型的
        dt = new TSysDutyLogic().SelectByUnionTable(objItems, 0, 0);
        dt.Columns.Add("MONITOR_NAME");
        dt.Columns.Add("POST_NAME");
        //添加监测类别列
        foreach (DataRow drSon in dt.Rows)
        {
            drSon["MONITOR_NAME"] = new TBaseMonitorTypeInfoLogic().Details("000000001").MONITOR_TYPE_NAME;
            drSon["POST_NAME"] = new TSysPostLogic().Details(new TSysUserPostLogic().Details(new TSysUserPostVo() { USER_ID = drSon["ID"].ToString() }).POST_ID).POST_NAME;
        }
        dtTotal.Merge(dt);
        reslut = LigerGridDataToJson(dtTotal, dtTotal.Rows.Count);
        return reslut;
    }

    /// <summary>
    ///  获取部门
    /// </summary>
    /// <returns></returns>
    protected string getDept()
    {
        List<TSysDictVo> objDict = new TSysDictLogic().GetDataDictListByType("dept");
        objDict.Insert(0, new TSysDictVo() { DICT_TEXT = "所有", DICT_CODE = "" });
        return ToJson(objDict);
    }

    /// <summary>
    ///  获取字典项数据
    /// </summary>
    /// <returns></returns>
    protected string getDict()
    {
        return new TSysDictLogic().GetDictNameByDictCodeAndType(Request.QueryString["dict_code"], Request.QueryString["dict_type"]);
    }

    /// <summary>
    /// 获取用户数据集
    /// </summary>
    /// <returns></returns>
    protected string getUserList()
    {
        DataTable dt = new DataTable();
        TSysDutyVo objItems = new TSysDutyVo();
        objItems.DICT_CODE = "duty_reportschedule";
        dt = new TSysDutyLogic().SelectByUnionTableForWhere(objItems, 0, 0);
        return DataTableToJson(dt);
    }

}