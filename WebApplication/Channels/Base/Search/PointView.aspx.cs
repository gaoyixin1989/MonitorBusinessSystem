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

/// <summary>
/// 功能描述：点位查询
/// 创建时间：2012-11-30
/// 创建人：邵世卓
/// </summary>
public partial class Channels_Base_Search_PointView : PageBase
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
        TBaseCompanyPointVo objPoint = new TBaseCompanyPointLogic().Details(strID);

        TBaseEvaluationConInfoLogic logicCon = new TBaseEvaluationConInfoLogic();
        TBaseEvaluationInfoLogic logicSt = new TBaseEvaluationInfoLogic();
        //按行、地、国标优先顺序进行标准的选定
        //国标条件项
        if (objPoint.NATIONAL_ST_CONDITION_ID.Length > 0)
        {
            TBaseEvaluationConInfoVo objCon = logicCon.Details(objPoint.NATIONAL_ST_CONDITION_ID);
            TBaseEvaluationInfoVo objSt = logicSt.Details(objCon.STANDARD_ID);

            objPoint.NATIONAL_ST_CONDITION_ID = objSt.STANDARD_CODE + "" + objSt.STANDARD_NAME;
            objPoint.hidNATIONAL_ST_CON = objCon.ID;
        }
        //地标条件项
        if (objPoint.LOCAL_ST_CONDITION_ID.Length > 0)
        {
            TBaseEvaluationConInfoVo objCon = logicCon.Details(objPoint.LOCAL_ST_CONDITION_ID);
            TBaseEvaluationInfoVo objSt = logicSt.Details(objCon.STANDARD_ID);

            objPoint.LOCAL_ST_CONDITION_ID = objSt.STANDARD_CODE + "" + objSt.STANDARD_NAME;
            objPoint.hidLOCAL_ST_CON = objCon.ID;
        }
        //行标条件项
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
        TBaseCompanyPointVo objPoint = new TBaseCompanyPointLogic().Details(strID);

        TBaseAttrbuteValueVo objAttrValue = new TBaseAttrbuteValueVo();
        objAttrValue.IS_DEL = "0";
        objAttrValue.OBJECT_ID = strID;

        DataTable dt = new TBaseAttrbuteValueLogic().SelectByTable(objAttrValue);

        string strJson = DataTableToJson(dt);

        Response.Write(strJson);
        Response.End();
    }
}