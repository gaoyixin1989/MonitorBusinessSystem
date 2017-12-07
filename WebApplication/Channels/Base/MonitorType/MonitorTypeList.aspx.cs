using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject;

/// <summary>
/// 功能描述：监测类别管理
/// 创建日期：2012-11-01
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_MonitorType_MonitorTypeList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
    }

    //获取监测类别数据
    private void GetData()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        TBaseMonitorTypeInfoVo objMonitorType = new TBaseMonitorTypeInfoVo();
        objMonitorType.IS_DEL = "0";
        objMonitorType.SORT_FIELD = "SORT_NUM";
        objMonitorType.SORT_TYPE = "asc";
        TBaseMonitorTypeInfoLogic logicMonitorType = new TBaseMonitorTypeInfoLogic();

        int intTotalCount = logicMonitorType.GetSelectResultCount(objMonitorType); ;//总计的数据条数
        DataTable dt = logicMonitorType.SelectByTable(objMonitorType, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteData(string strDelIDs)
    {
        if (strDelIDs.Length == 0)
            return "0";

        string[] arrDelIDs = strDelIDs.Split(',');

        bool isSuccess = true;
        for (int i = 0; i < arrDelIDs.Length; i++)
        {
            TBaseMonitorTypeInfoVo objMonitorType = new TBaseMonitorTypeInfoVo();
            objMonitorType.ID = arrDelIDs[i];
            objMonitorType.IS_DEL = "1";

            isSuccess = new TBaseMonitorTypeInfoLogic().Edit(objMonitorType);
        }

        if (isSuccess)
        {
            new PageBase().WriteLog("删除监测类别", "", new PageBase().LogInfo.UserInfo.USER_NAME + "删除监测类别" + arrDelIDs[0].ToString() + "成功！");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="strMONITOR_TYPE_NAME">类型名称</param>
    /// <param name="strDESCRIPTION">描述</param>
    /// <returns></returns>
    [WebMethod]
    public static string AddData(string strMONITOR_TYPE_NAME, string strDESCRIPTION,string strSORT_NUM)
    {
        bool isSuccess = true;

        TBaseMonitorTypeInfoVo objMonitorType = new TBaseMonitorTypeInfoVo();
        objMonitorType.ID = GetSerialNumber("t_base_monitor_type_info_id");
        objMonitorType.IS_DEL = "0";
        objMonitorType.MONITOR_TYPE_NAME = strMONITOR_TYPE_NAME;
        objMonitorType.DESCRIPTION = strDESCRIPTION;
        objMonitorType.SORT_NUM = strSORT_NUM;
        isSuccess = new TBaseMonitorTypeInfoLogic().Create(objMonitorType);

        if (isSuccess)
        {
            new PageBase().WriteLog("新增监测类别", "", new PageBase().LogInfo.UserInfo.USER_NAME + "新增监测类别" + objMonitorType.ID + "成功！");
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 编辑数据
    /// </summary>
    /// <param name="strID">id</param>
    /// <param name="strMONITOR_TYPE_NAME">类型名称</param>
    /// <param name="strDESCRIPTION">描述</param>
    /// <returns></returns>
    [WebMethod]
    public static string EditData(string strID, string strMONITOR_TYPE_NAME, string strDESCRIPTION,string strSORT_NUM)
    {
        bool isSuccess = true;

        TBaseMonitorTypeInfoVo objMonitorType = new TBaseMonitorTypeInfoVo();
        objMonitorType.ID = strID;
        objMonitorType.MONITOR_TYPE_NAME = strMONITOR_TYPE_NAME;
        objMonitorType.DESCRIPTION = strDESCRIPTION.Length > 0 ? strDESCRIPTION : ConstValues.SpecialCharacter.EmptyValuesFillChar;
        objMonitorType.SORT_NUM = strSORT_NUM;
        isSuccess = new TBaseMonitorTypeInfoLogic().Edit(objMonitorType);

        if (isSuccess)
        {
            new PageBase().WriteLog("修改监测类别", "", new PageBase().LogInfo.UserInfo.USER_NAME + "修改监测类别" + objMonitorType.ID + "成功！");
            return "1";
        }
        else
        {
            return "0";
        }
    }
}