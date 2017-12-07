using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Env.Point.PayFor;
using i3.BusinessLogic.Channels.Env.Point.PayFor;
using i3.BusinessLogic.Channels.Env.Point.Common;

/// <summary>
/// 功能描述：生态补偿水质断面信息编辑
/// 创建日期：2013-5-9
/// 创建人  ：潘德军
/// 修改人：魏林 2013-06-14
/// </summary>
public partial class Channels_Env_Point_Payfor_PayforEdit : PageBase
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
            strResult = getYearInfo();
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

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TEnvPPayforVo TEnvPPayfor = new TEnvPPayforVo();
        TEnvPPayfor.ID = Request["id"].ToString();
        TEnvPPayfor.IS_DEL = "0";
        TEnvPPayforVo TEnvPPayforTemp = new TEnvPPayforLogic().Details(TEnvPPayfor);

        //获取流域名称
        string strValley = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPPayforTemp.VALLEY_ID).DICT_TEXT;
        TEnvPPayforTemp.REMARK2 = strValley;
        //获取河流名称
        string strRiver = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().Details(TEnvPPayforTemp.RIVER_ID).DICT_TEXT;
        TEnvPPayforTemp.REMARK3 = strRiver;

        return ToJson(TEnvPPayforTemp);
    }

    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TEnvPPayforVo TEnvPPayfor = autoBindRequest(Request, new TEnvPPayforVo());
        TEnvPPayfor.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;
        //验证数据是否重复
        strMsg = new CommonLogic().isExistDatas(TEnvPPayforVo.T_ENV_P_PAYFOR_TABLE, TEnvPPayfor.YEAR, TEnvPPayfor.SelectMonths, TEnvPPayforVo.POINT_NAME_FIELD, TEnvPPayfor.POINT_NAME, TEnvPPayforVo.POINT_CODE_FIELD, TEnvPPayfor.POINT_CODE, TEnvPPayforVo.ID_FIELD, TEnvPPayfor.ID, 0);
        if (strMsg=="")
        {
            isSuccess = new TEnvPPayforLogic().Create(TEnvPPayfor, SerialType.T_ENV_P_PAYFOR);
            if (isSuccess)
            {
                WriteLog("添加生态补偿水质断面监测点", "", LogInfo.UserInfo.USER_NAME + "添加生态补偿水质断面监测点" + TEnvPPayfor.ID);
                strMsg = "数据保存成功";
            }
            else
                strMsg = "数据保存失败";
        }
        
        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TEnvPPayforVo TEnvPPayfor = autoBindRequest(Request, new TEnvPPayforVo());
        TEnvPPayfor.ID = Request["id"].ToString();
        TEnvPPayfor.IS_DEL = "0";
        string strMsg = "";
        bool isSuccess = false;

        if (hidYear.Value.Trim() != TEnvPPayfor.YEAR || hidMonth.Value.Trim() != TEnvPPayfor.MONTH || hidValue.Value.Trim() != TEnvPPayfor.POINT_NAME || hidCode.Value.Trim() != TEnvPPayfor.POINT_CODE)
        {
            //验证数据是否重复
            strMsg = new CommonLogic().isExistDatas(TEnvPPayforVo.T_ENV_P_PAYFOR_TABLE, TEnvPPayfor.YEAR, TEnvPPayfor.MONTH, TEnvPPayforVo.POINT_NAME_FIELD, TEnvPPayfor.POINT_NAME, TEnvPPayforVo.POINT_CODE_FIELD, TEnvPPayfor.POINT_CODE, TEnvPPayforVo.ID_FIELD, TEnvPPayfor.ID, 0);
        }

        if (strMsg=="")
        {
            isSuccess = new TEnvPPayforLogic().Edit(TEnvPPayfor);
            if (isSuccess)
            {
                WriteLog("编辑生态补偿水质断面监测点", "", LogInfo.UserInfo.USER_NAME + "编辑生态补偿水质断面监测点" + TEnvPPayfor.ID);
                strMsg = "数据更新成功";
            }
            else
                strMsg = "数据更新失败";
        }

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
}