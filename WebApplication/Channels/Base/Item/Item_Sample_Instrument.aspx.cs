using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;

/// <summary>
/// 功能描述：现场分析仪器新增与编辑功能
/// 创建日期：2013-06-14
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Base_Item_Item_Sample_Instrument : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        if (Request["id"] == null)
        {
            this.ITEM_ID.Value = Request["ITEM_ID"].ToString();
            this.formStatus.Value = "add";
        }
        else
        {
            this.formStatus.Value = "update";
            this.formId.Value = this.Request["id"].ToString();
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
    /// 加载数据
    /// </summary>
    /// <returns></returns>
    public string frmLoadData()
    {
        TBaseItemSamplingInstrumentVo TBaseItemSamplingInstrumentVo = new TBaseItemSamplingInstrumentVo();
        TBaseItemSamplingInstrumentVo.ID = Request["id"].ToString();
        TBaseItemSamplingInstrumentVo.IS_DEL = "0";
        TBaseItemSamplingInstrumentVo TBaseItemSamplingInstrumentVoTemp = new TBaseItemSamplingInstrumentLogic().Details(TBaseItemSamplingInstrumentVo);
        return ToJson(TBaseItemSamplingInstrumentVoTemp);
    }
    /// <summary>
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TBaseItemSamplingInstrumentVo TBaseItemSamplingInstrumentVo = autoBindRequest(Request, new TBaseItemSamplingInstrumentVo());
        TBaseItemSamplingInstrumentVo.ID = GetSerialNumber("Item_Sampling_Instrument_Id");
        TBaseItemSamplingInstrumentVo.ITEM_ID = Request["ITEM_ID"].ToString();
        TBaseItemSamplingInstrumentVo.IS_DEFAULT = "0";
        TBaseItemSamplingInstrumentVo.IS_DEL = "0";
        bool isSuccess = new TBaseItemSamplingInstrumentLogic().Create(TBaseItemSamplingInstrumentVo);
        if (isSuccess)
        {
            WriteLog(i3.ValueObject.ObjectBase.LogType.AddApparatusInfo, "", LogInfo.UserInfo.USER_NAME + "添加采样仪器信息" + TBaseItemSamplingInstrumentVo.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <returns></returns>
    public string frmUpdate()
    {
        TBaseItemSamplingInstrumentVo TBaseItemSamplingInstrumentVo = autoBindRequest(Request, new TBaseItemSamplingInstrumentVo());
        TBaseItemSamplingInstrumentVo.ID = Request["id"].ToString();
        TBaseItemSamplingInstrumentVo.IS_DEL = "0";
        bool isSuccess = new TBaseItemSamplingInstrumentLogic().Edit(TBaseItemSamplingInstrumentVo);
        if (isSuccess)
        {
            WriteLog(i3.ValueObject.ObjectBase.LogType.EditApparatusInfo, "", LogInfo.UserInfo.USER_NAME + "修改采样仪器信息" + TBaseItemSamplingInstrumentVo.ID + "成功！");
        }
        return isSuccess == true ? "1" : "0";
    }
}