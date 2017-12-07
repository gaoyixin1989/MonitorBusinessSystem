using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using i3.BusinessLogic.Channels.Env.Point.PolluteRule;
using System.Web.Services;
using i3.ValueObject.Sys.General;

public partial class Channels_Env_Point_PolluteRule_EnterpriseEdit : PageBase
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
        if (Request["type"] != null && Request["type"].ToString() == "getDict")
        {
            strResult = getDict(Request["dictType"].ToString());
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
        //加载数据
        if (Request["type"] != null && Request["type"].ToString() == "loadData")
        {
            strResult = frmLoadData();
            Response.Write(strResult);
            Response.End();
        }
        if (Request.QueryString["type"] != null && Request.QueryString["type"].ToString() == "LEVEL")
        {
            strResult = getDict(Request.QueryString["level"].ToString());
            Response.Write(strResult);
            Response.End();
        }
    }
    #region //增加数据
    public string frmAdd()
    {
        string strMsg = "";
        bool isSuccess = false;
        TEnvPEnterinfoVo TEnvPEnterInfo = autoBindRequest(Request, new TEnvPEnterinfoVo());
        TEnvPEnterInfo.ID = GetSerialNumber(SerialType.T_ENV_POINT_ENTERINFO);
        TEnvPEnterInfo.IS_DEL = "0";
        isSuccess = new i3.BusinessLogic.Channels.Env.Point.PolluteRule.TEnvPEnterinfoLogic().Create(TEnvPEnterInfo);
        if (isSuccess)
        {
            WriteLog("添加企业信息", "", LogInfo.UserInfo.USER_NAME + "添加企业信息" + TEnvPEnterInfo.ID);
            strMsg = "数据保存成功";
        }
        else
        {
            strMsg = "数据保存失败";
        }
        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
    #endregion

    #region//修改数据
        /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        string strMsg = "";
        bool isSuccess = false;
        TEnvPEnterinfoVo TEnvPEnterInfo = autoBindRequest(Request, new TEnvPEnterinfoVo());
        TEnvPEnterInfo.ID = Request["id"].ToString();
        TEnvPEnterInfo.IS_DEL = "0";
          isSuccess = new TEnvPEnterinfoLogic().Edit(TEnvPEnterInfo);
          if (isSuccess)
          {
              WriteLog("编辑企业信息", "", LogInfo.UserInfo.USER_NAME + "添加企业信息" + TEnvPEnterInfo.ID);
              strMsg = "数据更新成功";
          }
          else
          {
              strMsg = "数据更新失败";
          }

          return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";
    }
    #endregion

    #region// 加载数据
    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TEnvPEnterinfoVo TEnvPEnterInfo = new TEnvPEnterinfoVo();
        TEnvPEnterInfo.ID = Request["id"].ToString();
        TEnvPEnterinfoVo TEnvPRiverVoTemp = new TEnvPEnterinfoLogic().Details(TEnvPEnterInfo);
        return ToJson(TEnvPRiverVoTemp);
    }
    #endregion
    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict(string strDictType)
    {
        return getDictJsonString(strDictType);
    }

}