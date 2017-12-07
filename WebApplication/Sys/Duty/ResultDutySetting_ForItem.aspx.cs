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
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;

/// <summary>
/// 功能描述： "分析岗位职责--按项目"功能
/// 创建日期：2013.6.28
/// 创建人  ：潘德军
/// </summary>
public partial class Sys_Duty_ResultDutySetting_ForItem : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetItems")
        {
            GetItems();
            Response.End();
        }
        if (Request.Params["Action"] == "LoadSetUserList")
        {
            Response.Write(LoadSetUserList());
            Response.End();
        }
    }

    //获取监测项目
    private void GetItems()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhMONITOR_ID = (Request.Params["SrhMONITOR_ID"] != null) ? Request.Params["SrhMONITOR_ID"] : "";
        string strSrhITEM_NAME = (Request.Params["SrhITEM_NAME"] != null) ? Request.Params["SrhITEM_NAME"] : "";

        if (strSortname == null || strSortname.Length == 0)
            strSortname = TBaseItemInfoVo.ORDER_NUM_FIELD;

        TBaseItemInfoVo objItem = new TBaseItemInfoVo();
        objItem.IS_DEL = "0";
        objItem.HAS_SUB_ITEM = "0";//监测项目管理默认直接子项目。说明，父项目，在用户界面以项目包形式存在，不属于项目范畴；子项目、项目包，在数据库存一个表，对用户应该是两个概念。
        objItem.IS_SUB = "1";

        objItem.MONITOR_ID = strSrhMONITOR_ID;
        objItem.ITEM_NAME = strSrhITEM_NAME;

        objItem.SORT_FIELD = strSortname;
        objItem.SORT_TYPE = strSortorder;
        TBaseItemInfoLogic logicItem = new TBaseItemInfoLogic();

        int intTotalCount = logicItem.GetSelectResultCount(objItem); ;//总计的数据条数
        DataTable dt = logicItem.SelectByTable_ByJoinMonitorType(objItem, intPageIdx, intPagesize);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i][TBaseItemInfoVo.ORDER_NUM_FIELD] = (dt.Rows[i][TBaseItemInfoVo.ORDER_NUM_FIELD].ToString().Length > 0) ? (int.Parse(dt.Rows[i][TBaseItemInfoVo.ORDER_NUM_FIELD].ToString()).ToString()) : "";
        }


        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    /// <summary>
    /// 加载采样类岗位设置人员列表
    /// </summary>
    /// <param name="strMonitor"></param>
    /// <param name="strDutyType"></param>
    /// <param name="iIndex"></param>
    /// <param name="iCount"></param>
    /// <returns></returns>
    public string LoadSetUserList()
    {
        //创建标准JSON数据
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];

        string strDutyType = Request.Params["strDutyType"];
        string strItemID = Request.Params["strItemID"];

        TSysDutyVo objItems = new TSysDutyVo();
        objItems.MONITOR_ITEM_ID = strItemID;
        objItems.DICT_CODE = strDutyType;
        DataTable dt = new TSysDutyLogic().SelectByUnionTableForWhere(objItems, 0, 0);

        string reslut = LigerGridDataToJson(dt, dt.Rows.Count);
        return reslut;
    }

    /// <summary>
    /// 获取选择部门的尚未选中的用户
    /// </summary>
    /// <param name="strPost_Dept"></param>
    /// <param name="strItemID"></param>
    /// <param name="strDutyType"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSubUserItems(string strPost_Dept, string strItemID, string strDutyType)
    {
        DataTable dt = new TSysUserLogic().SelectByTableUnderDept(strPost_Dept, 0, 0);

        TSysDutyVo objItems = new TSysDutyVo();
        objItems.MONITOR_ITEM_ID = strItemID;
        objItems.DICT_CODE = strDutyType;
        DataTable dtDuty = new TSysDutyLogic().SelectByUnionTableForWhere(objItems, 0, 0);

        DataTable dtItems = new DataTable();
        dtItems = dt.Copy();
        dtItems.Clear();

        if (dtDuty.Rows.Count > 0)
        {
            for (int i = 0; i < dtDuty.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dtDuty.Rows[i]["ID"].ToString()))
                {
                    DataRow[] dr = dt.Select("ID='" + dtDuty.Rows[i]["ID"].ToString() + "'");
                    if (dr != null)
                    {
                        foreach (DataRow Temrow in dr)
                        {
                            Temrow.Delete();
                            dt.AcceptChanges();
                        }
                    }
                }
            }
        }

        dtItems = dt.Copy();

        List<object> listResult = LigerGridSelectDataToJson(dtItems, dtItems.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 获取已选中的用户
    /// </summary>
    /// <param name="strItemID"></param>
    /// <param name="strDutyType"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSelectUserItems(string strItemID, string strDutyType)
    {
        DataTable dt = new TSysUserLogic().SelectByTableUnderDept("", 0, 0);

        TSysDutyVo objItems = new TSysDutyVo();
        objItems.MONITOR_ITEM_ID = strItemID;
        objItems.DICT_CODE = strDutyType;
        DataTable dtDuty = new TSysDutyLogic().SelectByUnionTableForWhere(objItems, 0, 0);

        DataTable dtItems = new DataTable();
        dtItems = dt.Copy();
        dtItems.Clear();

        if (dtDuty.Rows.Count > 0)
        {
            for (int i = 0; i < dtDuty.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dtDuty.Rows[i]["ID"].ToString()))
                {
                    DataRow[] dr = dt.Select("ID='" + dtDuty.Rows[i]["ID"].ToString() + "'");
                    if (dr != null)
                    {
                        foreach (DataRow Temrow in dr)
                        {
                            dtItems.ImportRow(Temrow);
                        }
                    }
                }
            }
        }

        List<object> listResult = LigerGridSelectDataToJson(dtItems, dtItems.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 插入选择的用户
    /// </summary>
    /// <param name="strMonitor"></param>
    /// <param name="strUserId"></param>
    /// <param name="strDutyType"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool InsertSelectedUser(string strUserId, string strItemID, string strDutyType)
    {
        string strDutyID = "";
        string strMonitorID = new TBaseItemInfoLogic().Details(strItemID).MONITOR_ID;

        TSysDutyVo objDutySrh = new TSysDutyVo();
        objDutySrh.MONITOR_TYPE_ID = strMonitorID;
        objDutySrh.MONITOR_ITEM_ID = strItemID;
        objDutySrh.DICT_CODE = strDutyType;
        DataTable dtDuty = new TSysDutyLogic().SelectByTable(objDutySrh);
        if (dtDuty.Rows.Count == 0)
        {
            //如果不存在岗位配置，增加
            objDutySrh.ID = GetSerialNumber("duty_set_infor");
            if (new TSysDutyLogic().Create(objDutySrh))
            {
                strDutyID = objDutySrh.ID;
            }
        }
        else
            strDutyID = dtDuty.Rows[0]["ID"].ToString();

        TSysUserDutyVo objUserDutySrh = new TSysUserDutyVo();
        objUserDutySrh.DUTY_ID = strDutyID;
        DataTable dtUserDuty = new TSysUserDutyLogic().SelectByTable(objUserDutySrh);

        //删除移除的
        for (int i = 0; i < dtUserDuty.Rows.Count; i++)
        {
            string strTmpUserID = dtUserDuty.Rows[i]["USERID"].ToString();
            if (!strUserId.Contains(strTmpUserID))
            {
                new TSysUserDutyLogic().Delete(dtUserDuty.Rows[i]["ID"].ToString());
            }
        }

        string[] strUerArr = strUserId.Split(';');
        new TSysUserDutyLogic().InsertSelectedUser(strUerArr, strDutyID);

        
        return true;
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string DelUser(string strUSERDUTYIDs)
    {
        string[] strUSERDUTYIDsArr = strUSERDUTYIDs.Split(';');
        foreach (string str in strUSERDUTYIDsArr)
        {
            new TSysUserDutyLogic().Delete(str);
        }
        
        return   "1" ;
    }

    /// <summary>
    /// 默认负责人
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string SetDefaultAu(string strUSERDUTYIDs)
    {
        string[] strUSERDUTYIDsArr = strUSERDUTYIDs.Split(';');
        foreach (string str in strUSERDUTYIDsArr)
        {
            TSysUserDutyVo objUserDuty = new TSysUserDutyLogic().Details(str);

            new TSysUserDutyLogic().Edit(new TSysUserDutyVo() { IF_DEFAULT = "1" }, new TSysUserDutyVo { DUTY_ID = objUserDuty.DUTY_ID });
            new TSysUserDutyLogic().Edit(new TSysUserDutyVo() { IF_DEFAULT = "0" }, new TSysUserDutyVo { ID = str });
        }

        return "1";
    }

    /// <summary>
    /// 默认协同人
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string SetDefaultEx(string strUSERDUTYIDs)
    {
        string[] strUSERDUTYIDsArr = strUSERDUTYIDs.Split(';');
        foreach (string str in strUSERDUTYIDsArr)
        {
            TSysUserDutyVo objUserDuty = new TSysUserDutyLogic().Details(str);

            if (objUserDuty.IF_DEFAULT_EX == "0")
                new TSysUserDutyLogic().Edit(new TSysUserDutyVo() { IF_DEFAULT_EX = "1" }, new TSysUserDutyVo { ID = str });
            else
                new TSysUserDutyLogic().Edit(new TSysUserDutyVo() { IF_DEFAULT_EX = "0" }, new TSysUserDutyVo { ID = str });
        }

        return "1";
    }
}