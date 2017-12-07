using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.MonitorType;

using i3.BusinessLogic.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
/// <summary>
/// 功能描述：评价标准条件项管理(添加修改删除功能)
/// 创建日期：2012-11-05
/// 创建人  ：胡方扬
/// 时间：2012-11-05
/// 修改人  ：

/// </summary>

public partial class Channels_Base_Evaluation_EvaluationTapSetting : PageBase
{
    protected string MenuNodesJson;
    private string nodes, varNodes;
    public List<string> treenodes = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strAction = Request["action"];
            string strStandardId = Request["standartId"];
            string strstandid = Request["standid"];
            string strConditionId = Request["strConditionId"];
            string strMonitorId = Request["strMonitorId"];
            string strItemId = Request["strItemId"];
            switch (strAction)
            {
                //获取字典项信息
                case "GetEvaluConChild":
                    GetEvaluConChild("0",strStandardId);
                    MenuNodesJson = varNodes;
                    //============Begin By Castle(胡方扬)  2013-01-09 如果当前条件项目树为空 则创建一个=================
                    if (String.IsNullOrEmpty(MenuNodesJson)||MenuNodesJson=="[]") {
                        if (CreateRoot(strStandardId))
                        {
                            GetEvaluConChild("0", strStandardId);
                            MenuNodesJson = varNodes;
                        }
                    }
                    //============End By Castle(胡方扬) 2013-01-09  如果当前条件项目树为空 则创建一个=================
                    Response.Write(MenuNodesJson);
                    Response.End();
                    break;
                case "GetStandardConInfor":
                    Response.Write(GetStandardConInfor());
                    Response.End();
                    break;
                case "GetEvaluItemData":
                    Response.Write(GetEvaluItemData(strstandid));
                    Response.End();
                    break;
                case "GetMonitorItemsJson":
                    Response.Write(GetMonitorItemsJson(strMonitorId));
                    Response.End();
                    break;
                case "isExistItem":
                    Response.Write(isExistItem(strMonitorId,strItemId,strStandardId,strConditionId));
                    Response.End();
                    break;
                default: break;
            }
        }
    }

    /// <summary>
    /// 获取条件项目树，从根节点为0的开始
    /// </summary>
    /// <param name="ParentId">父节点</param>
    /// <param name="strStandardId">评价标准ID</param>
    public void GetEvaluConChild(string ParentId,string strStandardId)
    {
        DataTable dtConInfor = new DataTable();
        TBaseEvaluationConInfoVo coninfo = new TBaseEvaluationConInfoVo();
        coninfo.STANDARD_ID = strStandardId;
        coninfo.IS_DEL = "0";
        coninfo.PARENT_ID = ParentId;
        dtConInfor = new TBaseEvaluationConInfoLogic().SelectByTable(coninfo);

        DataView dv = new DataView(dtConInfor);
        dv.Sort = " PARENT_ID ASC";
        //排序结果 重载
        dtConInfor = dv.ToTable();
        for (int i = 0; i < dtConInfor.Rows.Count; i++)
        {
            string fisrt = "false";
            nodes = "{ id:'" + dtConInfor.Rows[i]["ID"] + "', pId:'" + dtConInfor.Rows[i]["PARENT_ID"] + "', code:'" + dtConInfor.Rows[i]["CONDITION_CODE"] + "', name:'" + dtConInfor.Rows[i]["CONDITION_NAME"] + "',STANDARD_ID:'" + dtConInfor.Rows[i]["STANDARD_ID"] + "',CONDITION_REMARK:'" + dtConInfor.Rows[i]["CONDITION_REMARK"] + "'";
            if (dtConInfor.Rows[i]["PARENT_ID"].ToString() == "0" && i == 0)
            {
                fisrt = "true";
                nodes += ", open:true";
            }
            nodes += ",first:'" + fisrt + "'";
            nodes += "}";
            treenodes.Add(nodes);
            //循环方法，取当前行的ID值作为父ID 查找儿子，依次循环 达到无限
            GetEvaluConChild(dtConInfor.Rows[i]["ID"].ToString(), strStandardId);
        }
        varNodes = "[" + String.Join(",\r\n", treenodes.ToArray()) + "]";
    }

    /// <summary>
    /// 动态创建根节点
    /// </summary>
    /// <param name="_strStandardId"></param>
    /// <returns></returns>
    private bool CreateRoot(string _strStandardId){
        bool blresult = false;
    if(String.IsNullOrEmpty(varNodes)||varNodes=="[]"){
    
        string result = CreateData("0", _strStandardId, "", "条件项目集合", "");
        if (!String.IsNullOrEmpty(result))
        {
            blresult = true;
        }
    }
    return blresult;
    }
    /// <summary>
    /// 新增评价标准条件项目
    /// </summary>
    /// <param name="strPARENT_ID">父亲ID</param>
    /// <param name="strSTANDARD_ID">评价标准ID</param>
    /// <param name="strCONDITION_CODE">条件项目编码</param>
    /// <param name="strCONDITION_NAME">条件项目名称</param>
    /// <param name="strCONDITION_REMARK">备注</param>
    /// <returns></returns>
    [WebMethod]
    public static string CreateData(string strPARENT_ID, string strSTANDARD_ID, string strCONDITION_CODE, string strCONDITION_NAME, string strCONDITION_REMARK)
    {
        string result = "";
        TBaseEvaluationConInfoVo objConVo = new TBaseEvaluationConInfoVo();

        objConVo.ID = GetSerialNumber("t_base_evaluation_con_info_id");

        objConVo.PARENT_ID = strPARENT_ID;
        objConVo.STANDARD_ID = strSTANDARD_ID;
        if (!String.IsNullOrEmpty(strCONDITION_CODE))
        {
            objConVo.CONDITION_CODE = strCONDITION_CODE;
        }
        objConVo.CONDITION_NAME = strCONDITION_NAME;
        objConVo.CONDITION_REMARK = strCONDITION_REMARK;
        objConVo.IS_DEL = "0";
        if (new TBaseEvaluationConInfoLogic().Create(objConVo))
        {
            result = objConVo.ID;
            
           string strMessage = new i3.View.PageBase().LogInfo.UserInfo.USER_NAME + "新增评价标准条件项" + objConVo.ID + "成功";
            new i3.View.PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.AddEvaluationConInfo, "", strMessage); 

        }
        return result;
    }
/// <summary>
/// 编辑条件项目
/// </summary>
/// <param name="strID">项目ID</param>
/// <param name="strPARENT_ID">项目父ID</param>
/// <param name="strSTANDARD_ID">项目所属标准</param>
/// <param name="strCONDITION_CODE">项目编码</param>
/// <param name="strCONDITION_NAME">项目名称</param>
/// <param name="strCONDITION_REMARK">项目备注</param>
/// <returns></returns>
    [WebMethod]
    public static string EditDataCon(string strID, string strPARENT_ID, string strSTANDARD_ID, string strCONDITION_CODE, string strCONDITION_NAME, string strCONDITION_REMARK)
    {
        string result = "false";
        TBaseEvaluationConInfoVo objConVo = new TBaseEvaluationConInfoVo();
        objConVo.ID = strID;
        objConVo.PARENT_ID = strPARENT_ID;
        objConVo.STANDARD_ID = strSTANDARD_ID;
        objConVo.CONDITION_CODE = strCONDITION_CODE;
        objConVo.CONDITION_NAME = strCONDITION_NAME;
        objConVo.CONDITION_REMARK = strCONDITION_REMARK;
        if (new TBaseEvaluationConInfoLogic().Edit(objConVo))
        {
            result = "true";
            string strMessage = new i3.View.PageBase().LogInfo.UserInfo.USER_NAME + "编辑评价标准条件项" + objConVo.ID + "成功";
            new i3.View.PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.EditEvaluationConInfo, "", strMessage);
        }
        return result;
    }
    /// <summary>
    /// 删除条件项目
    /// </summary>
    /// <param name="strID">条件项目ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string DelData(string strID)
    {
        string result = "false";
        TBaseEvaluationConInfoVo objtec = new TBaseEvaluationConInfoVo();
        objtec.ID = strID;
        objtec.IS_DEL = "1";
        if (new TBaseEvaluationConInfoLogic().Edit(objtec))
        {
            result = "true";

            string strMessage = new i3.View.PageBase().LogInfo.UserInfo.USER_NAME + "删除评价标准条件项" + objtec.ID + "成功";
            new PageBase(). WriteLog(i3.ValueObject.ObjectBase.LogType.EditEvaluationConInfo, "", strMessage);
        }
        return result;
    }


    /// <summary>
    /// 条件项目排序
    /// </summary>
    /// <param name="strValue">排序的内容</param>
    /// <returns></returns>
    [WebMethod]
    public static string SortData(string strValue)
    {
        string result = "false";
        if (new TBaseEvaluationConInfoLogic().UpdateSortByTransaction(strValue))
            result = "true";
        return result;
    }

    /// <summary>
    /// 获取评价标准
    /// </summary>
    /// <returns></returns>
    public string GetStandardConInfor()
    {
        string reslut = "";
        DataTable dtSt = new DataTable();
        TBaseEvaluationInfoVo objtd = new TBaseEvaluationInfoVo();
        dtSt = new TBaseEvaluationInfoLogic().SelectByTable(objtd);
        reslut = DataTableToJson(dtSt);
        return reslut;
    }
    /// <summary>
    /// 设置标准阀值
    /// </summary>
    /// <returns></returns>
    public string GetEvaluItemData(string strConditonId)
    {
        string reslut = "";
        DataTable dtSt = new DataTable();
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        //dtEval = new TBaseEvaluationInfoLogic().SelectUnionByTable();
        TBaseEvaluationConItemVo objEvItem = new TBaseEvaluationConItemVo();
        objEvItem.CONDITION_ID = strConditonId;
        objEvItem.IS_DEL = "0";
        objEvItem.SORT_FIELD = strSortname;
        objEvItem.SORT_TYPE = strSortorder;
        //dtSt = new TBaseEvaluationConItemLogic().SelectByTable(objEvItem,intPageIndex,intPageSize);
        //int intTotalCount = new TBaseEvaluationConItemLogic().GetSelectResultCount(objEvItem);
        dtSt = new TBaseEvaluationConItemLogic().GetEvaluItemDataDatable(objEvItem, intPageIndex, intPageSize);
        int intTotalCount = new TBaseEvaluationConItemLogic().GetEvaluItemDataDatableCount(objEvItem);
        reslut =LigerGridDataToJson(dtSt,intTotalCount);
        return reslut;
    }

    /// <summary>
    /// 获取监测单位数据下拉列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetItemUnit()
    {
        List<object> reslut = new List<object>();
        DataTable dtSt = new DataTable();
        TSysDictVo objtd = new TSysDictVo();
        objtd.DICT_TYPE = "item_unit";
        dtSt = new TSysDictLogic().SelectByTable(objtd);
        reslut = LigerGridSelectDataToJson(dtSt,dtSt.Rows.Count);
        //reslut = gridDataToJson(dtSt, dtSt.Rows.Count, objtd);
        return reslut;
    }


    /// <summary>
    /// 获取监测单位数据下拉列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetOpreator()
    {
        List<object> reslut = new List<object>();
        DataTable dtSt = new DataTable();
        TSysDictVo objtd = new TSysDictVo();
        objtd.DICT_TYPE = "logic_operator";
        dtSt = new TSysDictLogic().SelectByTable(objtd);
        reslut = LigerGridSelectDataToJson(dtSt, dtSt.Rows.Count);
        //reslut = gridDataToJson(dtSt, dtSt.Rows.Count, objtd);
        return reslut;
    }

    /// <summary>
    /// 获取监测值类型下拉列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetMonitorValue()
    {
        List<object> reslut = new List<object>();
        DataTable dtSt = new DataTable();
        TSysDictVo objtd = new TSysDictVo();
        objtd.DICT_TYPE = "MONITOR_VALUE_TYPE";
        dtSt = new TSysDictLogic().SelectByTable(objtd);
        reslut = LigerGridSelectDataToJson(dtSt, dtSt.Rows.Count);
        //reslut = gridDataToJson(dtSt, dtSt.Rows.Count, objtd);
        return reslut;
    }

    /// <summary>
    /// ligerGrid行上修改阀值
    /// </summary>
    /// <param name="strId">ID号</param>
    /// <param name="strItemId">监测值ID</param>
    /// <param name="strUpperOperator">上线符号</param>
    /// <param name="strLowerOperator">下限符号</param>
    /// <param name="strUpperChar">上限值</param>
    /// <param name="strLowerChar">下限符号</param>
    /// <param name="strUnit">单位</param>
    /// <returns></returns>
    [WebMethod]
    public static string EditGridColumnData(string strId, string strMonitorValue, string strItemId, string strUpperOperator, string strLowerOperator, string strUpperChar, string strLowerChar, string strUnit)
    {
        string result="false";
        TBaseEvaluationConItemVo objEvacon = new TBaseEvaluationConItemVo();
        objEvacon.ID = strId;
        objEvacon.MONITOR_VALUE_ID = strMonitorValue;
        objEvacon.ITEM_ID = strItemId;
        objEvacon.UPPER_OPERATOR = strUpperOperator;
        objEvacon.DISCHARGE_UPPER = strUpperChar;
        objEvacon.LOWER_OPERATOR = strLowerOperator;
        objEvacon.DISCHARGE_LOWER = strLowerChar;
        objEvacon.UNIT = strUnit;

        if (new TBaseEvaluationConItemLogic().Edit(objEvacon))
        {
            result = "true";
            string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "编辑条件项项目" + objEvacon.ID + "成功";
           new PageBase(). WriteLog(i3.ValueObject.ObjectBase.LogType.EditEvaluationConItemInfo, "", strMessage);
        }
        return result;
    }

/// <summary>
    /// ligerGrid行上粘贴复制的阀值
/// </summary>
/// <param name="strStandardId">评价标准ID</param>
/// <param name="strConditionId">条件项目ID</param>
/// <param name="strMonitorId">监测项目ID</param>
/// <param name="strItemId">监测值ID</param>
/// <param name="strUpperOperator">上限运算符ID</param>
    /// <param name="strLowerOperator">下限运算符ID</param>
    /// <param name="strUpperChar">上限值</param>
/// <param name="strLowerChar">下限值</param>
/// <param name="strUnit">监测项目单位ID</param>
/// <returns></returns>
    [WebMethod]
    public static string InsertCopyData(string strStandardId, string strConditionId, string strMonitorId,string strMonitorValue, string strItemId, string strUpperOperator, string strLowerOperator, string strUpperChar, string strLowerChar, string strUnit)
    {
        string result = "false";
        TBaseEvaluationConItemVo objEvacon = new TBaseEvaluationConItemVo();
        objEvacon.ID = GetSerialNumber("t_base_evaluation_con_item_id");
        objEvacon.STANDARD_ID = strStandardId;
        objEvacon.CONDITION_ID = strConditionId;
        objEvacon.MONITOR_ID = strMonitorId;
        objEvacon.MONITOR_VALUE_ID = strMonitorValue;
        objEvacon.ITEM_ID = strItemId;
        objEvacon.UPPER_OPERATOR = strUpperOperator;
        objEvacon.DISCHARGE_UPPER = strUpperChar;
        objEvacon.LOWER_OPERATOR = strLowerOperator;
        objEvacon.DISCHARGE_LOWER = strLowerChar;
        objEvacon.UNIT = strUnit;
        objEvacon.IS_DEL = "0";

        if (new TBaseEvaluationConItemLogic().Create(objEvacon))
        {
            result = "true";
            string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "复制条件项项目" + objEvacon.ID + "成功";
            new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.AddEvaluationConItemInfo, "", strMessage);
        }
        return result;
    }


    [WebMethod]
    public static List<object> GetMonitor()
    {
        List<object> reslut = new List<object>();
        DataTable dtSt = new DataTable();
        TBaseMonitorTypeInfoVo objtd = new TBaseMonitorTypeInfoVo();
        objtd.IS_DEL = "0";
        dtSt = new TBaseMonitorTypeInfoLogic().SelectByTable(objtd);
        reslut = LigerGridSelectDataToJson(dtSt, dtSt.Rows.Count);
        //reslut = gridDataToJson(dtSt, dtSt.Rows.Count, objtd);
        return reslut;
    }


    /// <summary>
    /// 获取监测项目下拉列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetMonitorItems(string strMonitor)
    {
        List<object> reslut = new List<object>();


        DataTable dtSt = new DataTable();
        TBaseItemInfoVo objtd = new TBaseItemInfoVo();
        objtd.IS_DEL = "0";
        objtd.MONITOR_ID = strMonitor;
        dtSt = new TBaseItemInfoLogic().SelectByTable(objtd);


        reslut = LigerGridSelectDataToJson(dtSt, dtSt.Rows.Count);
        //reslut = gridDataToJson(dtSt, dtSt.Rows.Count, objtd);
        return reslut;
    }

    public  string GetMonitorItemsJson(string strMonitor)
    {
        string reslut = "";

        DataTable dtSt = new DataTable();
        TBaseItemInfoVo objtd = new TBaseItemInfoVo();
        objtd.IS_DEL = "0";
        objtd.MONITOR_ID = strMonitor;
        dtSt = new TBaseItemInfoLogic().SelectByTable(objtd);


        reslut = LigerGridDataToJson(dtSt, dtSt.Rows.Count);
        return reslut;
    }

    /// <summary>
    /// 创建原因：获取是否存在相同条件下的监测项目
    /// </summary>
    /// <param name="strMonitorId"></param>
    /// <param name="strItemId"></param>
    /// <param name="strStandId"></param>
    /// <param name="strConditionId"></param>
    /// <returns></returns>
    public string isExistItem(string strMonitorId, string strItemId, string strStandId, string strConditionId) {
        string result="";
        TBaseEvaluationConItemVo objEvItem = new TBaseEvaluationConItemVo();
        objEvItem.STANDARD_ID = strStandId;
        objEvItem.CONDITION_ID = strConditionId;
        objEvItem.MONITOR_ID = strMonitorId;
        objEvItem.ITEM_ID = strItemId;
        objEvItem.IS_DEL = "0";

        DataTable dt = new TBaseEvaluationConItemLogic().SelectByTable(objEvItem);

        result = LigerGridDataToJson(dt,dt.Rows.Count);
        return result;
    }

    [WebMethod]
    public static List<object> GetMonitorSubItems(string strStandId, string strCondtionId, string strMonitor)
    {
        List<object> reslut = new List<object>();
        DataTable dtSt = new DataTable();
        TBaseEvaluationConItemVo objtd = new TBaseEvaluationConItemVo();
        objtd.IS_DEL = "0";
        objtd.MONITOR_ID = strMonitor;
        objtd.STANDARD_ID = strStandId;
        objtd.CONDITION_ID = strCondtionId;
        dtSt = new TBaseEvaluationConItemLogic().SelectByTable(objtd);

        DataTable dt = new DataTable();
        TBaseItemInfoVo objitem = new TBaseItemInfoVo();
        objitem.MONITOR_ID = strMonitor;
        objitem.IS_DEL = "0";


        dt = new TBaseItemInfoLogic().SelectByTable(objitem);

        DataTable dtItem = new DataTable();

        dtItem = dt.Copy();
        dtItem.Clear();
        if (dtSt.Rows.Count > 0)
        {
            for (int i = 0; i < dtSt.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dtSt.Rows[i]["ITEM_ID"].ToString()))
                {
                    DataRow[] dr = dt.Select("ID='" + dtSt.Rows[i]["ITEM_ID"].ToString() + "'");
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

        dtItem = dt.Copy();
        reslut = LigerGridSelectDataToJson(dtItem, dtItem.Rows.Count);
        //reslut = gridDataToJson(dtSt, dtSt.Rows.Count, objtd);
        return reslut;
    }

    /// <summary>
    /// 获取监测项目下拉列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSelectedMonitorItems(string strStandId, string strCondtionId, string strMonitor)
    {
        List<object> reslut = new List<object>();
        DataTable dtSt = new DataTable();
        TBaseEvaluationConItemVo objtd = new TBaseEvaluationConItemVo();
        objtd.IS_DEL = "0";
        objtd.MONITOR_ID = strMonitor;
        objtd.STANDARD_ID = strStandId;
        objtd.CONDITION_ID = strCondtionId;
        dtSt = new TBaseEvaluationConItemLogic().SelectByTable(objtd);

        DataTable dt = new DataTable();
        TBaseItemInfoVo objitem = new TBaseItemInfoVo();
        objitem.MONITOR_ID = strMonitor;

        dt = new TBaseItemInfoLogic().SelectByTable(objitem);

        DataTable dtItem = new DataTable();

        dtItem = dt.Copy();
        dtItem.Clear();

        for (int i = 0; i < dtSt.Rows.Count; i++)
        {
            if (!String.IsNullOrEmpty(dtSt.Rows[i]["ITEM_ID"].ToString()))
            {
                DataRow[] dr = dt.Select("ID='" + dtSt.Rows[i]["ITEM_ID"].ToString() + "'");
                if (dr != null)
                {
                    foreach (DataRow Temrow in dr)
                    {
                        dtItem.ImportRow(Temrow);
                    }
                }
            }
        }
        reslut = LigerGridSelectDataToJson(dtItem, dtItem.Rows.Count);
        //reslut = gridDataToJson(dtSt, dtSt.Rows.Count, objtd);
        return reslut;
    }

    /// <summary>
    /// 修改条件项目关联监测项目
    /// </summary>
    /// <param name="strStandId">评价标准ID</param>
    /// <param name="strCondtionId">条件项目ID</param>
    /// <param name="strMonitor">监测类型ID</param>
    /// <param name="strMonitorItems">监测项目ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string EditData(string strStandId, string strCondtionId, string strMonitor, string strMonitorItems, string strMoveId)
    {
        string result = "";
        DataTable dtis = new DataTable();
        TBaseEvaluationConItemVo objcitems = new TBaseEvaluationConItemVo();
        objcitems.STANDARD_ID = strStandId;
        objcitems.CONDITION_ID = strCondtionId;
        objcitems.MONITOR_ID = strMonitor;
        objcitems.IS_DEL = "0";
        dtis = new TBaseEvaluationConItemLogic().SelectByTable(objcitems);
        string[] strItems = strMonitorItems.Split(';');

        //string [] strMove=strMoveId.Split(';');

        if (!String.IsNullOrEmpty(strMoveId.ToString()))
        {
            string[] strMove = strMoveId.Split(';');
            if (DelMoveItems(objcitems, strMove, dtis))
            {
                if (EditItems(objcitems, strItems, dtis))
                {
                    result = "true";
                    string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "编辑条件项项目成功";
                    new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.EditEvaluationConItemInfo, "", strMessage);
                }
            }
        }
        else
        {
            if (EditItems(objcitems, strItems, dtis))
            {
                result = "true";
                string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "编辑条件项项目成功";
                new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.EditEvaluationConItemInfo, "", strMessage);
            }
        }

        return result;
    }

    /// <summary>
    /// 删除移除的项目
    /// </summary>
    /// <param name="objcitems">对象集合</param>
    /// <param name="strMove">传入的监测项目ID数组</param>
    /// <param name="dtis">比较表</param>
    /// <returns></returns>
    /// <returns></returns>
    public static bool DelMoveItems(TBaseEvaluationConItemVo objcitems, string[] strMove, DataTable dtis)
    {
        bool flag = false;
        int count = 0, sum = 0;
        if (strMove.Length > 0)
        {
            foreach (string strm in strMove)
            {
                if (!String.IsNullOrEmpty(strm))
                {
                    objcitems.ITEM_ID = strm;
                    DataRow[] row = dtis.Select("ITEM_ID='" + objcitems.ITEM_ID + "'");

                    if (row.Length > 0)
                    {
                        count++;
                        foreach (DataRow drr in row)
                        {
                            objcitems.ID = drr["ID"].ToString();
                            objcitems.IS_DEL = "1";
                            if (new TBaseEvaluationConItemLogic().Edit(objcitems))
                            {
                                sum++;
                            }
                        }
                    }
                }
            }
        }

            if (sum == count)
            {
                flag = true;
                string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "删除条件项项目成功";
                new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.DelEvaluationConItemInfo, "", strMessage);
            }
        

        return flag;
    }

    /// <summary>
    /// 修改或新增监测项目
    /// </summary>
    /// <param name="objcitems">对象集合</param>
    /// <param name="strItems">传入的监测项目ID数组</param>
    /// <param name="dtis">比较表</param>
    /// <returns></returns>
    public static bool EditItems(TBaseEvaluationConItemVo objcitems, string[] strItems, DataTable dtis)
    {
        bool flag = false;
        int count = 0, sum = 0;
        if (strItems.Length > 0)
        {
            foreach (string str in strItems)
            {
                if (!String.IsNullOrEmpty(str))
                {
                    objcitems.ITEM_ID = str;
                    DataRow[] row = dtis.Select("ITEM_ID='" + objcitems.ITEM_ID + "'");
                    if (row.Length < 1)
                    {
                        count++;
                        objcitems.ID = GetSerialNumber("t_base_evaluation_con_item_id");
                        objcitems.IS_DEL = "0";
                        if (new TBaseEvaluationConItemLogic().Create(objcitems))
                        {
                            sum++;
                        }
                    }
                }
            }
        }

        if (sum == count)
        {
            flag = true;
            string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "新增条件项项目成功";
            new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.DelEvaluationConItemInfo, "", strMessage);
        }

        return flag;
    }
}