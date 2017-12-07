using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using i3.View;
using System.Data;

using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Item;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
/// <summary>
/// Create By Castle(胡方扬) 2012-11-12
/// 功能：监测项目岗位职责管理
/// </summary>
/// 
public partial class Sys_Duty_UserDutySetting : PageBase
{
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strAction = Request.Params["Action"];
        string strMonitor = Request.Params["strMonitor"];
        string strType = Request.Params["strType"];
        //创建标准JSON数据
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(strAction))
                switch (strAction)
            {
                case "GetUserList":
                    Response.Write(GetUserList(intPageIndex,intPageSize));
                    Response.End();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 加载用户列表
    /// </summary>
    /// <param name="iIndex"></param>
    /// <param name="iCount"></param>
    /// <returns></returns>
    public string GetUserList(int iIndex,int iCount)
    {
        string reslut = "";
        dt = new DataTable();

       // dt = new TSysDutyLogic().SelectByUnionTable(iIndex,iCount);
        TSysUserVo objVo = new TSysUserVo();
        objVo.IS_DEL = "0";
        objVo.IS_USE = "1";
        objVo.IS_HIDE = "0";
        if (Request.QueryString["strSrhDept_ID"] != null)
            objVo.REMARK4 = Request.QueryString["strSrhDept_ID"];
        if (Request.QueryString["srhUserName"] != null)
            objVo.REAL_NAME = Request.QueryString["srhUserName"];
        dt = new TSysUserLogic().SelectByTable_ByDept(objVo, iIndex, iCount);
        int intCount = new TSysUserLogic().GetSelectResultCount_ByDept(objVo);
        reslut = LigerGridDataToJson(dt, intCount);
        return reslut;
    }

    /// <summary>
    /// 获取监测类型下拉列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetMonitor()
    {
        List<object> reslut = new List<object>();
        DataTable dtSt = new DataTable();
        TBaseMonitorTypeInfoVo objtd = new TBaseMonitorTypeInfoVo();
        objtd.IS_DEL = "0";
        objtd.SORT_FIELD = "SORT_NUM";
        objtd.SORT_TYPE = "ASC";
        dtSt = new TBaseMonitorTypeInfoLogic().SelectByTable(objtd);
        dtSt.DefaultView.Sort = "SORT_NUM ASC";
        DataTable dtTemp = dtSt.DefaultView.ToTable();

        reslut = LigerGridSelectDataToJson(dtTemp, dtTemp.Rows.Count);
        return reslut;
    }

    /// <summary>
    /// 获取当前用户未选择的监测项目列表
    /// </summary>
    /// <param name="strMonitor">监测项目类别</param>
    /// <param name="strUserId">用户ID</param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetMonitorItems(string strMonitor,string strUserId)
    {
        List<object> reslut = new List<object>();


        DataTable dtSt = new DataTable();
        TBaseItemInfoVo objtd = new TBaseItemInfoVo();
        objtd.IS_DEL = "0";
        objtd.MONITOR_ID = strMonitor;
        dtSt = new TBaseItemInfoLogic().SelectByTable(objtd);
        DataTable dtnew = new DataTable();
        dtnew = new TSysUserDutyLogic().GetExistMonitor(strMonitor, strUserId,false,false);
        for (int i = 0; i < dtnew.Rows.Count;i++ )
        {
            DataRow[] drr=dtSt.Select("ID='" + dtnew.Rows[i]["ITEM_ID"] + "'");
            if (drr.Length>0)
            {
                foreach(DataRow drow in drr)
                {
                    drow.Delete();
                }
            }
        }

        dtSt.AcceptChanges();

        reslut = LigerGridSelectDataToJson(dtSt, dtSt.Rows.Count);

        return reslut;
    }
    /// <summary>
    /// 获取当前用户已经选择的监测项目列表
    /// </summary>
    /// <param name="strMonitor">监测项目类别</param>
    /// <param name="strUserId">用户ID</param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetExistMonitorItems(string strMonitor, string strUserId, string strDefaultAu, string strDefaultEx)
    {
        List<object> reslut = new List<object>();

        DataTable dtnew = new DataTable();
        DataTable dtAll = new DataTable();
        //获取当前用户加载作为项目负责人的项目
        if (!String.IsNullOrEmpty(strDefaultAu))
        {
            dtnew = new TSysUserDutyLogic().GetExistMonitor(strMonitor, strUserId, true, false);
            //获取分析岗位职责中 设置监测类型为岗位职责的数据
            dtAll = new TSysUserDutyLogic().GetExistMonitorType(strMonitor, strUserId, true, false);
            for (int i = 0; i < dtAll.Rows.Count;i++ )
            {
                if (i == 0)
                {
                    dtAll.Rows[0]["ITEM_NAME"] = "所有(" + dtAll.Rows[0]["ITEM_NAME"].ToString() + "类)";
                    dtAll.AcceptChanges();
                    dtnew.ImportRow(dtAll.Rows[0]);
                }
            }
        }
        //获取当前用户加载作为项目协同人的项目
        if (!String.IsNullOrEmpty(strDefaultEx))
        {
            dtnew = new TSysUserDutyLogic().GetExistMonitor(strMonitor, strUserId, false, true);
            dtAll = new TSysUserDutyLogic().GetExistMonitorType(strMonitor, strUserId, false, true);
            for (int i = 0; i < dtAll.Rows.Count; i++)
            {
                if (i == 0)
                {
                    dtAll.Rows[0]["ITEM_NAME"] = "所有(" + dtAll.Rows[0]["ITEM_NAME"].ToString() + "类)";
                    dtAll.AcceptChanges();
                    dtnew.ImportRow(dtAll.Rows[0]);
                }
            }
        }
        if (String.IsNullOrEmpty(strDefaultAu) && String.IsNullOrEmpty(strDefaultEx))
        {
            //获取当前用户已设置的监测项目
            dtnew = new TSysUserDutyLogic().GetExistMonitor(strMonitor, strUserId, false, false);
            dtAll = new TSysUserDutyLogic().GetExistMonitorType(strMonitor, strUserId, false, false);
            for (int i = 0; i < dtAll.Rows.Count; i++)
            {
                if (i == 0)
                {
                    dtAll.Rows[0]["ITEM_NAME"] = "所有(" + dtAll.Rows[0]["ITEM_NAME"].ToString() + "类)";
                    dtAll.AcceptChanges();
                    dtnew.ImportRow(dtAll.Rows[0]);
                }
            }
        }
        reslut = LigerGridSelectDataToJson(dtnew, dtnew.Rows.Count);
        return reslut;
    }
    /// <summary>
    /// 修改 添加 移动分析类岗位职责
    /// </summary>
    /// <param name="strUserId">用户ID</param>
    /// <param name="strMonitor">监测类型</param>
    /// <param name="strDutyType">职责类型</param>
    /// <param name="strMonitorItems">监测项目</param>
    /// <param name="strMoveId">移除的监测项目</param>
    /// <param name="strAuItems">默认负责人项目</param>
    /// <param name="strExItems">默认协同人项目</param>
    /// <param name="MoveAuId">正移除的负责人项目</param>
    /// <param name="MoveExId">正移除的协同人项目</param>
    /// <returns>返回值</returns>
    [WebMethod]
    public static bool EditData(string strUserId,string strMonitor,string strDutyType, string strMonitorItems,string strMoveId,string strAuItems,string strExItems, string MoveAuId,string MoveExId)
    {
        bool result = false;
        DataTable dtis = new DataTable();
        TSysDutyVo objcitems = new TSysDutyVo();
        objcitems.MONITOR_TYPE_ID = strMonitor;
        dtis = new TSysDutyLogic().SelectByTable(objcitems);
        string[] strItems = null, strmoItems = null, sAuItems = null, sExItems = null, mAuItems = null, mExItems = null;
        if (!String.IsNullOrEmpty(strMonitorItems))
        {
            strItems = strMonitorItems.Split(';');
        }
        if (!String.IsNullOrEmpty(strMoveId))
        {
            strmoItems = strMoveId.Split(';');
        }
        if (!String.IsNullOrEmpty(strAuItems))
        {
            sAuItems = strAuItems.Split(';');
        }
        if (!String.IsNullOrEmpty(strExItems))
        {
            sExItems = strExItems.Split(';');
        }
        if (!String.IsNullOrEmpty(MoveAuId))
        {
            mAuItems = MoveAuId.Split(';');
        }
        if (!String.IsNullOrEmpty(MoveExId))
        {
           mExItems = MoveExId.Split(';');
        }
        //如果移除已选的和新增已选的项目均不为空 则执行该方法
        if (!String.IsNullOrEmpty(strMoveId.ToString()) &&!String.IsNullOrEmpty(strMonitorItems.ToString()))
        {
            if (DelMoveMonitorItems(strMonitor, strmoItems, strDutyType, strUserId,false))
            {
                if (InsertAddItems(objcitems, strDutyType, strMonitor, strUserId, dtis, strItems, sAuItems, sExItems, mAuItems, mExItems,false))
                {
                    result = true;
                }
            }
        }
        //如果移除的不为空，新增的监测项目为空
        if (!String.IsNullOrEmpty(strMoveId.ToString()) && String.IsNullOrEmpty(strMonitorItems.ToString()))
        {
            if (DelMoveMonitorItems(strMonitor, strmoItems, strDutyType, strUserId,false))
            {
                result = true;
            }
        }
        //如果移除的为空，新增的监测项目不为空
        if (String.IsNullOrEmpty(strMoveId.ToString()) && !String.IsNullOrEmpty(strMonitorItems.ToString()))
        {
            if (InsertAddItems(objcitems, strDutyType, strMonitor, strUserId, dtis, strItems, sAuItems, sExItems, mAuItems, mExItems,false))
            {
               
                result = true;
            }
        }

        return result;
    }

    /// <summary>
    /// 修改 添加 移动分析类岗位职责
    /// </summary>
    /// <param name="strUserId">用户ID</param>
    /// <param name="strMonitor">监测类型</param>
    /// <param name="strDutyType">职责类型</param>
    /// <param name="strMonitorItems">监测项目</param>
    /// <param name="strMoveId">移除的监测项目</param>
    /// <param name="strAuItems">默认负责人项目</param>
    /// <param name="strExItems">默认协同人项目</param>
    /// <param name="MoveAuId">正移除的负责人项目</param>
    /// <param name="MoveExId">正移除的协同人项目</param>
    /// <returns>返回值</returns>
    [WebMethod]
    public static bool SaveTypeDivDate(string strUserId, string strMonitor, string strDutyType, string strMonitorItems, string strMoveId, string strAuItems, string strExItems, string MoveAuId, string MoveExId)
    {
        bool result = false;
        DataTable dtis = new DataTable();
        TSysDutyVo objcitems = new TSysDutyVo();
        objcitems.MONITOR_TYPE_ID = strMonitor;
        dtis = new TSysDutyLogic().SelectByTable(objcitems);
        string[] strItems = null, strmoItems = null, sAuItems = null, sExItems = null, mAuItems = null, mExItems = null;
        if (!String.IsNullOrEmpty(strMonitorItems))
        {
            strItems = strMonitorItems.Split(';');
        }
        if (!String.IsNullOrEmpty(strMoveId))
        {
            strmoItems = strMoveId.Split(';');
        }
        if (!String.IsNullOrEmpty(strAuItems))
        {
            sAuItems = strAuItems.Split(';');
        }
        if (!String.IsNullOrEmpty(strExItems))
        {
            sExItems = strExItems.Split(';');
        }
        if (!String.IsNullOrEmpty(MoveAuId))
        {
            mAuItems = MoveAuId.Split(';');
        }
        if (!String.IsNullOrEmpty(MoveExId))
        {
            mExItems = MoveExId.Split(';');
        }
        //如果移除已选的和新增已选的项目均不为空 则执行该方法
        if (!String.IsNullOrEmpty(strMoveId.ToString()) && !String.IsNullOrEmpty(strMonitorItems.ToString()))
        {
            if (DelMoveMonitorItems(strMonitor, strmoItems, strDutyType, strUserId,true))
            {
                if (InsertAddMonitorItems(objcitems, strDutyType, strMonitor, strUserId, dtis, strItems, sAuItems, sExItems, mAuItems, mExItems,true))
                {

                    result = true;
                }
            }
        }
        //如果移除的不为空，新增的监测项目为空
        if (!String.IsNullOrEmpty(strMoveId.ToString()) && String.IsNullOrEmpty(strMonitorItems.ToString()))
        {
            if (DelMoveMonitorItems(strMonitor, strmoItems, strDutyType, strUserId,true))
            {
                result = true;
            }
        }
        //如果移除的为空，新增的监测项目不为空
        if (String.IsNullOrEmpty(strMoveId.ToString()) && !String.IsNullOrEmpty(strMonitorItems.ToString()))
        {
            if (InsertAddMonitorItems(objcitems, strDutyType, strMonitor, strUserId, dtis, strItems, sAuItems, sExItems, mAuItems, mExItems, true))
            {

                result = true;
            }
        }

        return result;
    }

    /// <summary>
    /// 插入分析类岗位的 监测类别
    /// </summary>
    /// <param name="objcitems"></param>
    /// <param name="strDutyType"></param>
    /// <param name="strMonitor"></param>
    /// <param name="strItems"></param>
    /// <param name="strUserId"></param>
    /// <param name="dtis"></param>
    /// <param name="sAuItems"></param>
    /// <param name="sExItems"></param>
    /// <param name="mAuItems"></param>
    /// <param name="mExItems"></param>
    /// <returns></returns>
    public static bool InsertAddMonitorItems(TSysDutyVo objcitems, string strDutyType, string strMonitor, string strUserId, DataTable dtis, string[] strItems, string[] sAuItems, string[] sExItems, string[] mAuItems, string[] mExItems,bool isMonitorType)
    {
        bool flag = false;
        string flag_au = "", flag_ex = "", flag_mau = "", flag_mex = "";
        string dutId = "";
        int newNum = 0, newSuc = 0;
        int strCount = strItems.Length-1;
        foreach (string str in strItems)
        {
            if (!String.IsNullOrEmpty(str))
            {
                //是否存在已经设置了的监测项目组合
                DataRow[] dr = dtis.Select("MONITOR_ITEM_ID IS NULL AND DICT_CODE='" + strDutyType + "'");
                if (dr.Length > 0)
                {
                    //获取已经存在符合条件的ID
                    dutId = dr[0]["ID"].ToString();
                }
                else
                {
                    objcitems.ID = GetSerialNumber("duty_set_infor");
                    dutId = objcitems.ID;
                    objcitems.DICT_CODE = strDutyType;
                    bool flgs = new TSysDutyLogic().Create(objcitems);
                    if (flgs)
                    {

                        string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "新增岗位职责" + objcitems.ID + "成功";
                        new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.AddDutyInfo, "", strMessage);

                    }
                }

                //直接插入岗位职责用户关联数据

                if (UserDutyItem(strUserId, "", dutId))
                {
                    newNum++;
                }
                else
                {
                    newSuc++;
                }

                //插入默认负责人和默认协同人项目
                flag_au = UpdateDefaultAuValue(strUserId, strMonitor, strDutyType, sAuItems, isMonitorType);
                flag_ex = UpdateDefaultExValue(strUserId, strMonitor, strDutyType, sExItems, isMonitorType);
                flag_mau = UpdateDefaultMoveAuValue(strUserId, strMonitor, strDutyType, mAuItems, isMonitorType);
                flag_mex = UpdateDefaultMoveExValue(strUserId, strMonitor, strDutyType, mExItems, isMonitorType);
            }
        }

        if ((newNum + newSuc) == strCount && flag_au != "0" && flag_ex != "0" && flag_mau != "0" & @flag_mex != "0")
        {
            flag = true;
        }
        return flag;
    }



    /// <summary>
    /// 插入分析类岗位监测项目
    /// </summary>
    /// <param name="objcitems"></param>
    /// <param name="strDutyType"></param>
    /// <param name="strMonitor"></param>
    /// <param name="strItems"></param>
    /// <param name="strUserId"></param>
    /// <param name="dtis"></param>
    /// <param name="sAuItems"></param>
    /// <param name="sExItems"></param>
    /// <param name="mAuItems"></param>
    /// <param name="mExItems"></param>
    /// <returns></returns>
    public static bool InsertAddItems(TSysDutyVo objcitems, string strDutyType, string strMonitor, string strUserId, DataTable dtis, string[] strItems, string[] sAuItems, string[] sExItems, string[] mAuItems, string[] mExItems,bool isMonitorType)
    {
        bool flag = false;
        string flag_au="", flag_ex="", flag_mau="", flag_mex="";
        string dutId = "";
        int newNum = 0, newSuc = 0;
        int strCount = strItems.Length-1;
        foreach (string str in strItems)
        {
            if (!String.IsNullOrEmpty(str))
            {
                objcitems.MONITOR_ITEM_ID = str;
                //是否存在已经设置了的监测项目组合
                DataRow[] dr = dtis.Select("MONITOR_ITEM_ID='" + str + "' AND DICT_CODE='" + strDutyType + "'");
                if (dr.Length > 0)
                {
                    //获取已经存在符合条件的ID
                    dutId = dr[0]["ID"].ToString();
                }
                else
                {
                    objcitems.ID = GetSerialNumber("duty_set_infor");
                    dutId = objcitems.ID;
                    objcitems.DICT_CODE = strDutyType;
                    bool flgs = new TSysDutyLogic().Create(objcitems);
                    if (flgs) {

                       string  strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "新增岗位职责" + objcitems.ID + "成功";
                       new PageBase(). WriteLog(i3.ValueObject.ObjectBase.LogType.AddDutyInfo, "", strMessage);
                    }

                }

                //直接插入岗位职责用户关联数据

                if (UserDutyItem(strUserId, str, dutId))
                {
                    newNum++;
                }
                else
                {
                    newSuc++;
                }

                //插入默认负责人和默认协同人项目
                flag_au = UpdateDefaultAuValue(strUserId, strMonitor, strDutyType, sAuItems, isMonitorType);
                flag_ex = UpdateDefaultExValue(strUserId, strMonitor, strDutyType, sExItems, isMonitorType);
                flag_mau = UpdateDefaultMoveAuValue(strUserId, strMonitor, strDutyType, mAuItems, isMonitorType);
                flag_mex = UpdateDefaultMoveExValue(strUserId, strMonitor, strDutyType, mExItems, isMonitorType);
            }
        }

        if ((newNum + newSuc) == strCount && flag_au!="0" && flag_ex!="0"&&flag_mau!="0"&@flag_mex!="0")
        {
            flag = true;

            string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "新增用户上岗资质成功";
            new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.AddUserDutyInfo, "", strMessage);
        }
        return flag;
    }

    /// <summary>
    /// 检查当前用户是否已经进行了设置，如果没有设置则插入
    /// </summary>
    /// <param name="strUserId"></param>
    /// <param name="strDutyId"></param>
    /// <returns></returns>
    public static bool UserDutyItem(string strUserId,string strMonitor,string strDutyId)
    {
        bool flag=false;
        DataTable dt = new DataTable();

        TSysUserDutyVo objuv = new TSysUserDutyVo();
        objuv.USERID = strUserId;
        objuv.DUTY_ID = strDutyId;

        dt = new TSysUserDutyLogic().SelectByTable(objuv);

        if (dt.Rows.Count < 1)
        {
            objuv.ID = GetSerialNumber("user_duty_infor");

            if (new TSysUserDutyLogic().Create(objuv))
            {
                flag = true;

                string strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "新增用户上岗资质" + objuv.ID + "成功";
                new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.AddUserDutyInfo, "", strMessage);
            }
        }

        return flag;
    }

    /// <summary>
    /// 更新分析类默认负责人
    /// </summary>
    /// <param name="strUserId"></param>
    /// <param name="strMonitor"></param>
    /// <param name="strDutyType"></param>
    /// <param name="sAuItems"></param>
    /// <returns></returns>
    public static string UpdateDefaultAuValue(string strUserId, string strMonitor, string strDutyType, string[] sAuItems, bool isMonitorType)
    {
        string flag = "";

        if (sAuItems != null)
        {
            if (new TSysUserDutyLogic().UpdateDefaultSet(strUserId, strMonitor, sAuItems, strDutyType, "0", "",isMonitorType))
            {
                flag = "1";
            }
            else
            {
                flag = "0";
            }
        }

        return flag;
    }
    /// <summary>
    /// 更新默认协同人
    /// </summary>
    /// <param name="strUserId"></param>
    /// <param name="strMonitor"></param>
    /// <param name="strDutyType"></param>
    /// <param name="sExItems"></param>
    /// <returns></returns>
    public static string UpdateDefaultExValue(string strUserId, string strMonitor, string strDutyType, string[] sExItems,bool isMonitorType)
    {
        string flag = "";
        if (sExItems != null)
        {
            if (new TSysUserDutyLogic().UpdateDefaultSet(strUserId, strMonitor, sExItems, strDutyType, "", "0",isMonitorType))
            {
                flag = "1";
            }
            else
            {
                flag = "0";
            }
        }
        return flag;
 
    }
    /// <summary>
    /// 移除默认负责人
    /// </summary>
    /// <param name="strUserId"></param>
    /// <param name="strMonitor"></param>
    /// <param name="strDutyType"></param>
    /// <param name="mAuItems"></param>
    /// <returns></returns>
    public static string UpdateDefaultMoveAuValue(string strUserId, string strMonitor, string strDutyType, string[] mAuItems, bool isMonitorType)
    {
        string flag = "";
        if (mAuItems != null)
        {
            if (new TSysUserDutyLogic().MoveDefaultSet(strUserId, strMonitor, mAuItems, strDutyType, true, false,isMonitorType))
            {
                flag = "1";
            }
            else
            {
                flag = "0";
            }
        }

        return flag;
    }
    /// <summary>
    /// 移除默认协同人
    /// </summary>
    /// <param name="strUserId"></param>
    /// <param name="strMonitor"></param>
    /// <param name="strDutyType"></param>
    /// <param name="mExItems"></param>
    /// <returns></returns>
    public static string UpdateDefaultMoveExValue(string strUserId, string strMonitor, string strDutyType, string[] mExItems, bool isMonitorType)
    {
        string flag = "";
        if (mExItems != null)
        {
            if (new TSysUserDutyLogic().MoveDefaultSet(strUserId, strMonitor, mExItems, strDutyType, false, true,isMonitorType))
            {
                flag = "1";
            }
            else
            {
                flag = "0";
            }
        }

        return flag;
    }
    /// <summary>
    /// 删除移除的项目
    /// </summary>
    /// <param name="objcitems">对象集合</param>
    /// <param name="strMove">传入的监测项目ID数组</param>
    /// <param name="dtis">比较表</param>
    /// <returns></returns>
    /// <returns></returns>
    public static bool DelMoveMonitorItems(string strMonitor, string[] strMove,string strDutyType,string strUserId,bool isMonitorTye)
    {
        bool flag = false;
        int count = strMove.Length;
        if (strMove != null)
        {
            if (new TSysUserDutyLogic().DeleteUserMonitorSet(strUserId, strMonitor, strMove, strDutyType, isMonitorTye))
            {
                flag = true;
               string  strMessage = new PageBase().LogInfo.UserInfo.USER_NAME + "删除用户上岗资质成功";
               new PageBase().WriteLog(i3.ValueObject.ObjectBase.LogType.DelUserDutyInfo, "", strMessage);
            }
        }

        return flag;
    }

}