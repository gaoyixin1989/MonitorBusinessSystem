using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Mis.Contract;
using i3.BusinessLogic.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;

/// <summary>
/// 功能描述：采样任务
/// 创建日期：2012-12-11
/// 创建人  ：苏成斌
/// </summary>

public partial class Channels_Mis_Monitor_sampling_QY_SamplingView : PageBase
{
    public static string strId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["strSubtaskID"]))
            {
                //监测子任务ID
                this.SUBTASK_ID.Value = Request.QueryString["strSubtaskID"].ToString();
            }

            //点位动态属性信息拷贝
            AttributeValueCopy();
            //样品编号
            //SetSampleCode();


            //委托书信息
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"] == "getContractInfo")
            {
                strResult = GetContractInfo();
                Response.Write(strResult);
                Response.End();
            }
        }
    }

    /// <summary>
    /// 样品编号设置
    /// </summary>
    /// <returns></returns>
    protected void SetSampleCode()
    {
        TMisMonitorSampleInfoVo objSample = new TMisMonitorSampleInfoVo();
        objSample.SUBTASK_ID = this.SUBTASK_ID.Value;
        objSample.QC_TYPE = "0";
        objSample.SORT_FIELD = "POINT_ID";
        DataTable dtSample = new DataTable();
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSample, 0, 0);
        dtSample = dt.Clone();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtSample.ImportRow(dt.Rows[i]);
            objSample.QC_TYPE = "";
            objSample.QC_SOURCE_ID = dt.Rows[i]["ID"].ToString();
            objSample.SORT_FIELD = "QC_TYPE";
            DataTable dtQcSample = new TMisMonitorSampleInfoLogic().SelectByTableForPoint(objSample, 0, 0);
            for (int j = 0; j < dtQcSample.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                dr = dtQcSample.Rows[j];
                dtSample.ImportRow(dr);
            }
        }

        for (int j = 0; j < dtSample.Rows.Count; j++)
        {
            if (dtSample.Rows[j]["SAMPLE_CODE"].ToString().Length > 0)
                continue;
            string[] strSampleCode = new string[2] { "S" + DateTime.Now.Year + DateTime.Now.Month, i3.View.PageBase.GetSerialNumber("monitor_samplecode") };
            objSample = new TMisMonitorSampleInfoVo();
            objSample.ID = dtSample.Rows[j]["ID"].ToString();
            objSample.SAMPLE_CODE = i3.View.PageBase.CreateSerialNumber(strSampleCode);

            new TMisMonitorSampleInfoLogic().Edit(objSample);
        }

    }

    /// <summary>
    /// 点位描述信息拷贝
    /// </summary>
    /// <returns></returns>
    protected void AttributeValueCopy()
    {
        TMisMonitorTaskPointVo objTaskPoint = new TMisMonitorTaskPointVo();
        objTaskPoint.SUBTASK_ID = this.SUBTASK_ID.Value;
        DataTable dt = new TMisMonitorTaskPointLogic().SelectByTable(objTaskPoint);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TBaseAttrbuteValueVo objAttrbuteValue = new TBaseAttrbuteValueVo();
            objAttrbuteValue.OBJECT_ID = dt.Rows[i]["POINT_ID"].ToString();

            TBaseAttrbuteValue3Vo objAttrbuteValue3 = new TBaseAttrbuteValue3Vo();
            objAttrbuteValue3.OBJECT_ID = dt.Rows[i]["ID"].ToString();

            DataTable dtAtt3 = new TBaseAttrbuteValue3Logic().SelectByTable(objAttrbuteValue3);
            if (dtAtt3.Rows.Count > 0)
                return;

            DataTable dtAtt = new TBaseAttrbuteValueLogic().SelectByTable(objAttrbuteValue);
            for (int j = 0; j < dtAtt.Rows.Count; j++)
            {
                objAttrbuteValue3 = new TBaseAttrbuteValue3Vo();
                objAttrbuteValue3.ID = GetSerialNumber("t_base_attribute_value3_id");
                objAttrbuteValue3.OBJECT_TYPE = dtAtt.Rows[j]["OBJECT_TYPE"].ToString();
                objAttrbuteValue3.OBJECT_ID = dt.Rows[i]["ID"].ToString();
                objAttrbuteValue3.ATTRBUTE_CODE = dtAtt.Rows[j]["ATTRBUTE_CODE"].ToString();
                objAttrbuteValue3.ATTRBUTE_VALUE = dtAtt.Rows[j]["ATTRBUTE_VALUE"].ToString();
                objAttrbuteValue3.IS_DEL = dtAtt.Rows[j]["IS_DEL"].ToString();

                new TBaseAttrbuteValue3Logic().Create(objAttrbuteValue3);
            }
        }
    }

    /// <summary>
    /// 获得委托书信息
    /// </summary>
    /// <returns>Json</returns>
    protected string GetContractInfo()
    {
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(this.SUBTASK_ID.Value);
        TMisMonitorTaskVo objTask = new TMisMonitorTaskLogic().Details(objSubtask.TASK_ID);

        TMisMonitorTaskCompanyVo objConCompany = new TMisMonitorTaskCompanyVo();
        objConCompany.TASK_ID = objTask.ID;
        objConCompany = new TMisMonitorTaskCompanyLogic().Details(objConCompany);
        objSubtask.MONITOR_ID = getMonitorTypeName(objSubtask.MONITOR_ID);
        objSubtask.SAMPLING_MANAGER_ID = new TSysUserLogic().Details(LogInfo.UserInfo.ID).REAL_NAME;
        objSubtask.REMARK1 = objTask.CONTRACT_CODE;
        objSubtask.REMARK2 = getDictName(objTask.CONTRACT_TYPE, "Contract_Type");
        objSubtask.REMARK3 = objConCompany.COMPANY_NAME;
        objSubtask.REMARK4 = objConCompany.CONTACT_NAME;
        objSubtask.REMARK5 = objConCompany.LINK_PHONE;
        objSubtask.SAMPLE_ASK_DATE = DateTime.Parse(objSubtask.SAMPLE_ASK_DATE).ToString("yyyy-MM-dd");
        objSubtask.SAMPLE_FINISH_DATE = DateTime.Parse(objSubtask.SAMPLE_FINISH_DATE).ToString("yyyy-MM-dd");

        return ToJson(objSubtask);
    }

    /// <summary>
    /// 获取监测类别名称
    /// </summary>
    /// <param name="strMonitorTypeId">监测类别Id</param>
    /// <returns></returns>
    public static string getMonitorTypeName(string strMonitorTypeId)
    {
        i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo TBaseMonitorTypeInfoVo = new i3.ValueObject.Channels.Base.MonitorType.TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoVo.ID = strMonitorTypeId;
        string strMonitorTypeName = new i3.BusinessLogic.Channels.Base.MonitorType.TBaseMonitorTypeInfoLogic().Details(TBaseMonitorTypeInfoVo).MONITOR_TYPE_NAME;
        return strMonitorTypeName;
    }

    /// <summary>
    /// 获取字典项名称
    /// </summary>
    /// <param name="strDictCode">字典项代码</param>
    /// <param name="strDictType">字典项类型</param>
    /// <returns></returns>
    public static string getDictName(string strDictCode, string strDictType)
    {
        return PageBase.getDictName(strDictCode, strDictType);
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string btnSendClick(string strSubtaskID)
    {
        bool isSuccess = true;
        TMisMonitorSubtaskVo objSubtask = new TMisMonitorSubtaskLogic().Details(strSubtaskID);
        objSubtask.TASK_STATUS = "022";
        objSubtask.TASK_TYPE = "发送";

        isSuccess = new TMisMonitorSubtaskLogic().Edit(objSubtask);
        return isSuccess == true ? "1" : "0";
    }
}