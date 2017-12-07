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
using i3.ValueObject.Channels.Base.CodeRule;
using i3.BusinessLogic.Channels.Base.CodeRule;
using i3.ValueObject;

/// <summary>
/// 功能描述：监测类别管理
/// 创建日期：2012-11-01
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_Base_CodeRule_CodeRuleList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }

        if (Request.Params["Action"] == "GetUnionCode")
        {
            Response.Write(GetUnionCode());
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

        TBaseSerialruleVo objMonitorType = new TBaseSerialruleVo();
        objMonitorType.SORT_FIELD = strSortname;
        objMonitorType.SORT_TYPE = strSortorder;
        TBaseSerialruleLogic logicMonitorType = new TBaseSerialruleLogic();

        int intTotalCount =logicMonitorType.GetSelectResultCount (objMonitorType); //总计的数据条数
        DataTable dt = logicMonitorType.SelectByTable(objMonitorType, intPageIdx, intPagesize);

        DataView dtView = dt.DefaultView;
        dtView.Sort = " ID ASC";
        dt = dtView.ToTable();

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    /// <summary>
    /// 获取辅助编号数据
    /// </summary>
    /// <returns></returns>
    public string GetUnionCode(){
        string result="";
        TBaseSerialruleVo objItems=new TBaseSerialruleVo();
        objItems.SERIAL_TYPE="5";
        DataTable dt=new TBaseSerialruleLogic().SelectByTable(objItems);
        result=LigerGridDataToJson(dt,dt.Rows.Count);
        return result;
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
            TBaseSerialruleVo objMonitorType = new TBaseSerialruleVo();
            objMonitorType.ID = arrDelIDs[i];

            isSuccess = new TBaseSerialruleLogic().Delete(objMonitorType);
        }

        if (isSuccess)
        {
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
    public static string AddData(string strSERIAL_NAME, string strSERIAL_TYPE, string strSERIAL_RULE, string strSEARIAL_NUMBER_BIT, string strMONITOR_TYPE_ID, string strSAMPLE_SOURCE, string strStartNum, string strMaxNum, string strStatus, string strIsUnion, string strUnionSerialId, string strUnionDefault)
    {
        bool isSuccess = true;

        TBaseSerialruleVo objMonitorType = new TBaseSerialruleVo();
        objMonitorType.ID = GetSerialNumber("t_base_Code_RuleID");
        objMonitorType.SERIAL_NAME = strSERIAL_NAME;
        objMonitorType.SERIAL_RULE = strSERIAL_RULE;
        objMonitorType.SERIAL_TYPE = strSERIAL_TYPE;
        objMonitorType.SERIAL_NUMBER_BIT = strSEARIAL_NUMBER_BIT;
        objMonitorType.SERIAL_TYPE_ID = strMONITOR_TYPE_ID;
        objMonitorType.SAMPLE_SOURCE = strSAMPLE_SOURCE;
        objMonitorType.SERIAL_START_NUM = strStartNum;
        objMonitorType.SERIAL_MAX_NUM = strMaxNum;
        objMonitorType.STATUS = strStatus;
        objMonitorType.SERIAL_YEAR = DateTime.Now.Year.ToString();
        objMonitorType.IS_UNION = strIsUnion;
        objMonitorType.UNION_SEARIAL_ID = strUnionSerialId;
        objMonitorType.UNION_DEFAULT = strUnionDefault;
        isSuccess = new TBaseSerialruleLogic().Create(objMonitorType);

        if (isSuccess)
        {
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
    public static string EditData(string strID, string strSERIAL_NAME, string strSERIAL_TYPE, string strSERIAL_RULE, string strSEARIAL_NUMBER_BIT, string strMONITOR_TYPE_ID, string strSAMPLE_SOURCE, string strStartNum, string strMaxNum, string strStatus, string strIsUnion, string strUnionSerialId, string strUnionDefault)
    {
        bool isSuccess = true;

        TBaseSerialruleVo objMonitorType = new TBaseSerialruleVo();
        objMonitorType.ID = strID;
        objMonitorType.SERIAL_NAME = strSERIAL_NAME;
        objMonitorType.SERIAL_RULE = strSERIAL_RULE;
        objMonitorType.SERIAL_TYPE = strSERIAL_TYPE;
        objMonitorType.SERIAL_NUMBER_BIT = strSEARIAL_NUMBER_BIT;
        objMonitorType.SERIAL_TYPE_ID = strMONITOR_TYPE_ID;
        objMonitorType.SAMPLE_SOURCE = strSAMPLE_SOURCE;
        objMonitorType.SERIAL_START_NUM = strStartNum;
        objMonitorType.SERIAL_MAX_NUM = strMaxNum;
        objMonitorType.STATUS = strStatus;
        objMonitorType.SERIAL_YEAR = DateTime.Now.Year.ToString();
        objMonitorType.IS_UNION = strIsUnion;
        objMonitorType.UNION_SEARIAL_ID = strUnionSerialId;
        objMonitorType.UNION_DEFAULT = strUnionDefault;
        isSuccess = new TBaseSerialruleLogic().Edit(objMonitorType);

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }
}