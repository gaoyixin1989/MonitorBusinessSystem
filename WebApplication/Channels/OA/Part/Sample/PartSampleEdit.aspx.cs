using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.ValueObject.Channels.OA.PART.SAMPLE;
using i3.BusinessLogic.Channels.OA.PART.SAMPLE;
/// <summary>
/// 标准样品新增功能
/// 创建人：魏林 2013-09-16
/// </summary>
public partial class Channels_OA_Part_Sample_PartSampleEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Json = string.Empty;
        if (!IsPostBack)
        {
            if (Request["type"] != null)
            {
                switch (Request["type"].ToString())
                {
                    case "getDict":
                        Json = getDict(Request["dictType"].ToString());
                        Response.ContentType = "application/json";
                        Response.Write(Json);
                        Response.End();
                        break;
                    case "add":
                        this.Status.Value = "add";
                        break;
                    case "update":
                        this.Status.Value = "update";
                        this.ID.Value = this.Request["id"].ToString();
                        break;
                }

            }
        }

        //增加数据
        if (Request["Status"] != null && Request["Status"].ToString() == "add")
        {
            Json = frmAdd();
            Response.ContentType = "application/json";
            Response.Write(Json);
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
    /// 增加数据
    /// </summary>
    /// <returns></returns>
    public string frmAdd()
    {
        TOaPartstandInfoVo PartstandInfoVo = autoBindRequest(Request, new TOaPartstandInfoVo());
        PartstandInfoVo.ID = GetSerialNumber("t_oa_partstand_info_id");
        PartstandInfoVo.TOTAL_INVENTORY = PartstandInfoVo.INVENTORY;
        string strMsg = "";
        bool isSuccess = false;

        isSuccess = new TOaPartstandInfoLogic().Create(PartstandInfoVo);
        if (isSuccess)
        {
            strMsg = "数据保存成功";
        }
        else
            strMsg = "数据保存失败";

        return isSuccess == true ? "{\"result\":\"success\",\"msg\":\"" + strMsg + "\"}" : "{\"result\":\"fail\",\"msg\":\"" + strMsg + "\"}";

    }
}