using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Point;
using i3.BusinessLogic.Channels.Base.Point;
using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.BusinessLogic.Channels.Base.DynamicAttribute;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;

/// <summary>
/// 点位编辑：采样-点位信息管理
/// 创建日期：2012-12-14
/// 创建人  ：苏成斌
/// </summary>
public partial class Channels_Mis_Monitor_sampling_QY_SamplePointEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strID = "";
        if (Request["strid"] != null)
        {
            strID = this.Request["strid"].ToString();
        }

        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            GetData(strID);
        }

        //获取指定动态属性类别的动态属性数据
        if (Request["type"] != null && Request["type"].ToString() == "GetAttrrbute")
        {
            GetAttrrbute();
        }

        //获取指定动态属性类别的动态属性数据
        if (Request["type"] != null && Request["type"].ToString() == "GetAttrValue")
        {
            GetAttrValue(strID);
        }
    }

    //获取数据
    private void GetData(string strID)
    {
        TMisMonitorTaskPointVo objPoint = new TMisMonitorTaskPointLogic().Details(strID);

        TBaseEvaluationConInfoLogic logicCon = new TBaseEvaluationConInfoLogic();
        TBaseEvaluationInfoLogic logicSt = new TBaseEvaluationInfoLogic();

        if (objPoint.NATIONAL_ST_CONDITION_ID.Length > 0)
        {
            TBaseEvaluationConInfoVo objCon = logicCon.Details(objPoint.NATIONAL_ST_CONDITION_ID);
            TBaseEvaluationInfoVo objSt = logicSt.Details(objCon.STANDARD_ID);

            objPoint.NATIONAL_ST_CONDITION_ID = objSt.STANDARD_CODE + "" + objSt.STANDARD_NAME;
            objPoint.hidNATIONAL_ST_CON = objCon.ID;
        }

        if (objPoint.LOCAL_ST_CONDITION_ID.Length > 0)
        {
            TBaseEvaluationConInfoVo objCon = logicCon.Details(objPoint.LOCAL_ST_CONDITION_ID);
            TBaseEvaluationInfoVo objSt = logicSt.Details(objCon.STANDARD_ID);

            objPoint.LOCAL_ST_CONDITION_ID = objSt.STANDARD_CODE + "" + objSt.STANDARD_NAME;
            objPoint.hidLOCAL_ST_CON = objCon.ID;
        }

        if (objPoint.INDUSTRY_ST_CONDITION_ID.Length > 0)
        {
            TBaseEvaluationConInfoVo objCon = logicCon.Details(objPoint.INDUSTRY_ST_CONDITION_ID);
            TBaseEvaluationInfoVo objSt = logicSt.Details(objCon.STANDARD_ID);

            objPoint.INDUSTRY_ST_CONDITION_ID = objSt.STANDARD_CODE + "" + objSt.STANDARD_NAME;
            objPoint.hidINDUSTRY_ST_CON = objCon.ID;
        }

        string strJson = ToJson(objPoint);

        Response.Write(strJson);
        Response.End();
    }

    //获取指定动态属性类别的动态属性数据
    private void GetAttrrbute()
    {
        DataTable dt = new TBaseAttributeInfoLogic().SelectByTableByJoin();

        string strJson = DataTableToJson(dt);

        Response.Write(strJson);
        Response.End();
    }

    //获取点位对应的动态属性值
    private void GetAttrValue(string strID)
    {
        TMisMonitorTaskPointVo objPoint = new TMisMonitorTaskPointLogic().Details(strID);

        TBaseAttrbuteValue3Vo objAttrValue = new TBaseAttrbuteValue3Vo();
        objAttrValue.IS_DEL = "0";
        objAttrValue.OBJECT_ID = strID;

        DataTable dt = new TBaseAttrbuteValue3Logic().SelectByTable(objAttrValue);

        string strJson = DataTableToJson(dt);

        Response.Write(strJson);
        Response.End();
    }
}