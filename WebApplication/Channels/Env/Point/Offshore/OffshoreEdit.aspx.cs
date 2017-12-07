using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.Offshore;
using i3.BusinessLogic.Channels.Env.Point.Offshore;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：近岸直排监测点编辑
/// 创建日期：2011-11-20
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Env_Point_Offshore_OffshoreEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = ""; 
        if (Request["id"] == null)
        {
            this.formStatus.Value = "add";
        }
        else
        {
            this.formStatus.Value = "update";
            this.formId.Value = this.Request["id"].ToString();
        }
        //获取下拉列表信息
        if (Request["type"] != null && Request["type"].ToString() == "getDict")
        {
            strResult = getDict(Request["dictType"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        //获取年度下拉列表数据
        if (Request["type"] != null && Request["type"].ToString() == "getYearInfo")
        {
            strResult = getYearInfo(5, 5);
            Response.Write(strResult);
            Response.End();
        }
        //获取月度下拉列表数据
        if (Request["type"] != null && Request["type"].ToString() == "getMonthInfo")
        {
            strResult = getMonthInfo();
            Response.Write(strResult);
            Response.End();
        }
        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            strResult = frmLoadData();
            Response.Write(strResult);
            Response.End();
        }
        //增加数据
        if (Request["formStatus"] != null && Request["formStatus"].ToString() == "add")
        {
            strResult = frmAdd();
            Response.Write(strResult);
            Response.End();
        }
        //修改数据
        if (Request["formStatus"] != null && Request["formStatus"].ToString() == "update")
        {
            strResult = frmUpdate();
            Response.Write(strResult);
            Response.End();
        }
    }

    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict(string strDictType)
    {
        return getDictJsonString(strDictType);
    }

    #region//加载数据
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TEnvPointOffshoreVo TEnvPointOffshoreVo = new TEnvPointOffshoreVo();
        TEnvPointOffshoreVo.ID = Request["id"].ToString();
        TEnvPointOffshoreVo.IS_DEL = "0";
        TEnvPointOffshoreVo TEnvPointOffshoreVoTemp = new TEnvPointOffshoreLogic().Details(TEnvPointOffshoreVo);
        //条件项ID
        string strConditionId = TEnvPointOffshoreVoTemp.CONDITION_ID;
        //根据条件项获取评价标准名称
        string strStanderId = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationConInfoLogic().Details(strConditionId).STANDARD_ID;
        //根据评价标准ID获取评价标准名称
        string strStanderName = new i3.BusinessLogic.Channels.Base.Evaluation.TBaseEvaluationInfoLogic().Details(strStanderId).STANDARD_NAME;
        TEnvPointOffshoreVoTemp.REMARK1 = strStanderName;
        return ToJson(TEnvPointOffshoreVoTemp);
    }
    #endregion

    #region// 增加数据
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        string strMsg = "";
        bool isSuccess = true;
        TEnvPointOffshoreVo TEnvPointOffshoreVo = autoBindRequest(Request, new TEnvPointOffshoreVo());
       // TEnvPointOffshoreVo.ID = GetSerialNumber("offshorepoint_id");
        TEnvPointOffshoreVo.IS_DEL = "0";
        string Meg = new CommonLogic().isExistDatas(TEnvPointOffshoreVo.T_ENV_POINT_OFFSHORE_TABLE, TEnvPointOffshoreVo.YEAR, TEnvPointOffshoreVo.SelectMonths, TEnvPointOffshoreVo.POINT_NAME_FIELD, TEnvPointOffshoreVo.POINT_NAME, TEnvPointOffshoreVo.POINT_CODE_FIELD, TEnvPointOffshoreVo.POINT_CODE,TEnvPointOffshoreVo.ID_FIELD,TEnvPointOffshoreVo.ID, 0);
        if (!string.IsNullOrEmpty(Meg))
        {
            strMsg = Meg; isSuccess = false;
        }
        else
        {
            isSuccess = new TEnvPointOffshoreLogic().Create(TEnvPointOffshoreVo, SerialType.T_ENV_POINT_OFFSHORE_TABLE);
            if (isSuccess)
            {
                WriteLog("添加近岸直排监测点", "", LogInfo.UserInfo.USER_NAME + "添加近岸直排监测点:" + TEnvPointOffshoreVo.ID);
                strMsg = "数据保存成功";
            }
            else
                strMsg = "数据保存失败";
        }
        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";

    }
        #endregion

    #region//  修改数据
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TEnvPointOffshoreVo TEnvPointOffshoreVo = autoBindRequest(Request, new TEnvPointOffshoreVo());
        TEnvPointOffshoreVo.ID = Request["id"].ToString();
        TEnvPointOffshoreVo.IS_DEL = "0";
        //bool isSuccess = new TEnvPointOffshoreLogic().Edit(TEnvPointOffshoreVo);
        //if (isSuccess)
        //    WriteLog("编辑近岸直排监测点", "", LogInfo.UserInfo.USER_NAME + "编辑近岸直排监测点" + TEnvPointOffshoreVo.ID);
        //return isSuccess == true ? "1" : "0";

        string strMsg = "";
        bool isSuccess = true;
        if (hidYear.Value.Trim() != TEnvPointOffshoreVo.YEAR || hidMonth.Value.Trim() != TEnvPointOffshoreVo.MONTH || hidValue.Value.Trim() != TEnvPointOffshoreVo.POINT_NAME || hidValues.Value.Trim() != TEnvPointOffshoreVo.POINT_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPointOffshoreVo.T_ENV_POINT_OFFSHORE_TABLE, TEnvPointOffshoreVo.YEAR, TEnvPointOffshoreVo.MONTH, TEnvPointOffshoreVo.POINT_NAME_FIELD, TEnvPointOffshoreVo.POINT_NAME, TEnvPointOffshoreVo.POINT_CODE_FIELD, TEnvPointOffshoreVo.POINT_CODE, TEnvPointOffshoreVo.ID_FIELD, TEnvPointOffshoreVo.ID, 0);
            if (string.IsNullOrEmpty(strMsg))
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
        }

        if (isSuccess)
        {
            isSuccess = new TEnvPointOffshoreLogic().Edit(TEnvPointOffshoreVo);
            if (isSuccess)
            {
                WriteLog("编辑近岸直排监测点", "", LogInfo.UserInfo.USER_NAME + "编辑近岸直排监测点" + TEnvPointOffshoreVo.ID);
                strMsg = "数据更新成功";
            }
            else
            {
                strMsg = "数据更新失败";
            }
        }
        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";

    }
        #endregion
}