using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.BusinessLogic.Channels.Base.Apparatus;

/// <summary>
/// 功能描述：仪器信息新增与编辑功能
/// 创建日期：2011-11-01
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_Apparatus_ApparatusInfoEdit : PageBase
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
        TBaseApparatusInfoVo TBaseApparatusInfoVo = new TBaseApparatusInfoVo();
        TBaseApparatusInfoVo.ID = Request["id"].ToString();
        TBaseApparatusInfoVo.IS_DEL = "0";
        TBaseApparatusInfoVo TBaseApparatusInfoVoTemp = new TBaseApparatusInfoLogic().Details(TBaseApparatusInfoVo);
        return ToJson(TBaseApparatusInfoVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        bool isSuccess = false;
        TBaseApparatusInfoVo TBaseApparatusInfoVo = autoBindRequest(Request, new TBaseApparatusInfoVo());
        TBaseApparatusInfoVo.ID = GetSerialNumber("Apparatus_Id");
        TBaseApparatusInfoVo.IS_DEL = "0";

        TBaseApparatusInfoVo iApparatusInfoVo = new TBaseApparatusInfoVo();
        iApparatusInfoVo.APPARATUS_CODE = TBaseApparatusInfoVo.APPARATUS_CODE;
        iApparatusInfoVo.IS_DEL = "0";
        iApparatusInfoVo = new TBaseApparatusInfoLogic().Details(iApparatusInfoVo);
        if (iApparatusInfoVo.ID.Length > 0)
        {
            isSuccess = false;
        }
        else
        {
            isSuccess = new TBaseApparatusInfoLogic().Create(TBaseApparatusInfoVo);
            if (isSuccess)
            {
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddApparatusInfo, "", LogInfo.UserInfo.USER_NAME + "添加仪器信息" + TBaseApparatusInfoVo.ID + "成功！");
            }
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TBaseApparatusInfoVo TBaseApparatusInfoVo = autoBindRequest(Request, new TBaseApparatusInfoVo());
        TBaseApparatusInfoVo.ID = Request["id"].ToString();
        TBaseApparatusInfoVo.IS_DEL = "0";
        bool isSuccess = new TBaseApparatusInfoLogic().Edit(TBaseApparatusInfoVo);
        if (isSuccess)
        {
            WriteLog(i3.ValueObject.ObjectBase.LogType.EditApparatusInfo, "", LogInfo.UserInfo.USER_NAME + "修改仪器信息" + TBaseApparatusInfoVo.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }
}