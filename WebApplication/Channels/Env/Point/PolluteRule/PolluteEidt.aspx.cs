using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using i3.BusinessLogic.Channels.Env.Point.PolluteRule;
using i3.BusinessLogic.Channels.Env.Point.Common;

public partial class Channels_Env_Point_PolluteRule_PolluteEidt : PageBase
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
        if (this.Request["twoGridId"] != null)
        {
            this.TypeId.Value = this.Request["twoGridId"].ToString();
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
            this.formId.Value = this.Request["id"].ToString();
            strResult = frmUpdate();
            Response.Write(strResult);
            Response.End();
        }
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TEnvPPolluteVo TEnvPPollute = new TEnvPPolluteVo();
        string str = this.formStatus.Value;
        TEnvPPollute.ID = Request["id"].ToString(); 
        TEnvPPolluteVo TEnvPRiverVTemp = new TEnvPPolluteLogic().Details(TEnvPPollute);
        return ToJson(TEnvPRiverVTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        string strMsg = "";
        bool isSuccess = true;
        TEnvPPolluteVo TEnvPPollute = autoBindRequest(Request, new TEnvPPolluteVo());
        TEnvPPollute.TYPE_ID = this.TypeId.Value;
        TEnvPPollute.IS_DEL = "0";
        string Meg = new CommonLogic().isExistRepeat(TEnvPPolluteVo.T_ENV_P_POLLUTE_TABLE, TEnvPPollute.YEAR, TEnvPPollute.SelectMonths, TEnvPPolluteVo.POINT_NAME_FIELD, TEnvPPollute.POINT_NAME, TEnvPPolluteVo.POINT_CODE_FIELD, TEnvPPollute.POINT_CODE, TEnvPPolluteVo.ID_FIELD, TEnvPPollute.ID, TEnvPPollute.TYPE_ID);
        if (!string.IsNullOrEmpty(Meg))
        {
            strMsg = Meg; isSuccess = false;
        }
        else
        {
            isSuccess = new TEnvPPolluteLogic().CreateInfo(TEnvPPollute, SerialType.T_ENV_POINT_POLLUTE);
            if (isSuccess)
            {
                WriteLog("添加监测点", "", LogInfo.UserInfo.USER_NAME + "添加监测点" + TEnvPPollute.ID);
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
        string strMsg = "";
        bool isSuccess = true;
        TEnvPPolluteVo TEnvPPollute = autoBindRequest(Request, new TEnvPPolluteVo());
        TEnvPPollute.ID = Request["id"].ToString();
        TEnvPPollute.IS_DEL = "0";
        string Meg = new CommonLogic().isExistRepeat(TEnvPPolluteVo.T_ENV_P_POLLUTE_TABLE, TEnvPPollute.YEAR, TEnvPPollute.MONTH, TEnvPPolluteVo.POINT_NAME_FIELD, TEnvPPollute.POINT_NAME, TEnvPPolluteVo.POINT_CODE_FIELD, TEnvPPollute.POINT_CODE, TEnvPPolluteVo.ID_FIELD, TEnvPPollute.ID,null);
        if (!string.IsNullOrEmpty(Meg))
        {
            strMsg = Meg; isSuccess = false;
        }
        else
        {
            isSuccess = new TEnvPPolluteLogic().Edit(TEnvPPollute);
            if (isSuccess)
            {
                WriteLog("编辑监测点", "", LogInfo.UserInfo.USER_NAME + "编辑监测点" + TEnvPPollute.ID);
                  strMsg = "数据保存成功";
            }
            else
                strMsg = "数据保存失败";
        }
        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict(string strDictType)
    {
        return getDictJsonString(strDictType);
    }
}