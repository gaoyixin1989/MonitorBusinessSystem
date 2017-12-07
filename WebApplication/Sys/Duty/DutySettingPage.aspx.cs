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
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
/// <summary>
/// Create By Castle(胡方扬) 2012-11-12
/// 功能：监测项目岗位职责管理 采样
/// </summary>

public partial class Sys_Duty_DutySettingPage : PageBase
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
                    case "GetMonitors":
                        Response.Write(GetMonitors(intPageIndex, intPageSize));
                        Response.End();
                        break;
                    case "LoadSetUserList":
                        Response.Write(LoadSetUserList(strMonitor, strType, intPageIndex, intPageSize));
                        Response.End();
                        break;
                    default:
                        break;
                }
        }
    }


    /// <summary>
    /// 获取监测类别
    /// </summary>
    /// <param name="iIndex"></param>
    /// <param name="iCount"></param>
    /// <returns></returns>
    public string GetMonitors(int iIndex, int iCount)
    {
        string reslut = "";
        DataTable dtSt = new DataTable();
        TBaseMonitorTypeInfoVo objtd = new TBaseMonitorTypeInfoVo();
        objtd.IS_DEL = "0";
        dtSt = new TBaseMonitorTypeInfoLogic().SelectByTable(objtd, iIndex, iCount);
        int intTotalCount = new TBaseMonitorTypeInfoLogic().GetSelectResultCount(objtd);
        reslut = LigerGridDataToJson(dtSt, intTotalCount);
        return reslut;
    }

    /// <summary>
    /// 加载采样类岗位设置人员列表
    /// </summary>
    /// <param name="strMonitor"></param>
    /// <param name="strDutyType"></param>
    /// <param name="iIndex"></param>
    /// <param name="iCount"></param>
    /// <returns></returns>
    public string LoadSetUserList(string strMonitor, string strDutyType, int iIndex, int iCount)
    {
        string reslut = "";
        dt = new DataTable();
        TSysDutyVo objItems = new TSysDutyVo();
        objItems.MONITOR_TYPE_ID = strMonitor;
        objItems.DICT_CODE = strDutyType;
        dt = new TSysDutyLogic().SelectByUnionTable(objItems,iIndex, iCount);
        int intCount = new TSysDutyLogic().GetSelectByUnionResultCount(objItems);
        //TSysUserVo objVo = new TSysUserVo();
        //objVo.IS_DEL = "0";
        //objVo.IS_USE = "1";
        //objVo.IS_HIDE = "0";
        //dt = new TSysUserLogic().SelectByTable(objVo, iIndex, iCount);
        //int intCount = new TSysUserLogic().GetSelectResultCount(objVo);
        //DataTable dtnew = new DataTable();
        //dtnew = new TSysUserDutyLogic().GetSamplingSetUser(strMonitor, strDutyType);

        //dt.Columns.Add(new DataColumn("IF_DEFAULT", typeof(string)));
        //dt.Columns.Add(new DataColumn("IF_DEFAULT_EX", typeof(string)));
        //dt.Columns.Add(new DataColumn("ISEXIST", typeof(string)));
        //if (!String.IsNullOrEmpty(strMonitor))
        //{
        //    if (dtnew.Rows.Count > 0)
        //    {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                foreach (DataRow dr in dtnew.Rows)
        //                {
        //                    if (dt.Rows[i]["ID"].ToString() == dr["USERID"].ToString())
        //                    {
        //                        dt.Rows[i]["IF_DEFAULT"] = dr["IF_DEFAULT"].ToString();
        //                        dt.Rows[i]["IF_DEFAULT_EX"] = dr["IF_DEFAULT_EX"].ToString();
        //                        dt.Rows[i]["ISEXIST"] = "True";
        //                    }
        //                }
        //            }
        //            dt.AcceptChanges();

        //        DataRow[] drr = dt.Select("ISEXIST='True'");
        //        DataTable dtTemp = dt.Copy();
        //        dtTemp.Clear();
        //        foreach (DataRow ddr in drr)
        //        {
        //            dtTemp.ImportRow(ddr);
        //        }

        //        dt.Clear();
        //        dt = dtTemp.Copy();
        //    }
        //    else
        //    {
        //        dt.Clear();
        //    }
        //}
        //else
        //{
        //    dt.Clear();
        //}
        //intCount = dt.Rows.Count;
        reslut = LigerGridDataToJson(dt, intCount);
        return reslut;
    }
    /// <summary>
    /// 设置采样岗位职责人员
    /// </summary>
    /// <param name="strMonitor"></param>
    /// <param name="strUserId"></param>
    /// <param name="strDutyType"></param>
    /// <param name="isAuDefault"></param>
    /// <returns></returns>
    [WebMethod]
    public static string EditSampling(string strMonitor, string strUserId, string strDutyType, string isAuDefault)
    {
        string result = "";
        string[] strUerArr = null;
        TSysDutyVo objtd = new TSysDutyVo();
        objtd.MONITOR_TYPE_ID = strMonitor;
        objtd.DICT_CODE = strDutyType;
        strUerArr = strUserId.Split(';');

        if (strUerArr != null)
        {
            //如果存在岗位配置，则直接插入用户岗位表
            DataTable dt = new DataTable();
            dt = new TSysDutyLogic().SelectByTable(objtd);

            bool isDefAu = false;

            if (isAuDefault == "true")
            {
                isDefAu = true;
            }
            if (dt.Rows.Count > 0)
            {
                if (new TSysUserDutyLogic().InsertSamplingUser(strUerArr, dt.Rows[0]["ID"].ToString(), isDefAu))
                {
                    result = "true";
                }

            }
            else
            {
                objtd.ID = GetSerialNumber("duty_set_infor");

                if (new TSysDutyLogic().Create(objtd))
                {
                    if (new TSysUserDutyLogic().InsertSamplingUser( strUerArr, objtd.ID, isDefAu))
                    {
                        result = "true";
                    }
                }
            }
        }
        return result;
    }

    public static bool InsertUserDutyForSamp(string strUserId, string strDutyId, string isAuDefault)
    {
        bool flag = false;
        TSysUserDutyVo objUv = new TSysUserDutyVo();
        objUv.DUTY_ID = strDutyId;
        objUv.USERID = strUserId;
        if (!String.IsNullOrEmpty(isAuDefault))
        {
            if (isAuDefault == "true")
            {
                objUv.IF_DEFAULT = "0";
            }
            else
            {
                objUv.IF_DEFAULT_EX = "0";
            }
        }
        DataTable dt = new DataTable();
        dt = new TSysUserDutyLogic().SelectByTable(objUv);
        if (dt.Rows.Count > 0)
        {
            flag = true;
        }
        else
        {
            objUv.ID = GetSerialNumber("user_duty_infor");
            if (new TSysUserDutyLogic().Create(objUv))
            {
                flag = true;
            }
        }
        return flag;
    }

    /// <summary>
    /// 删除默认负责人和默认协同人
    /// </summary>
    /// <param name="strUserId"></param>
    /// <param name="strMonitor"></param>
    /// <param name="strDutyType"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool DelSampUserDuty(string strUserId, string strMonitor, string strDutyType)
    {
        bool flag = false;
        string[] strUser_Id = strUserId.Split(';');
        if (new TSysUserDutyLogic().DeleteUserMonitorSet(strUser_Id, strMonitor, strDutyType))
        {
            flag = true;
        }
        return flag;
    }
    /// <summary>
    /// 清理已设置了的默认负责人和默认协同人
    /// </summary>
    /// <param name="strUserId"></param>
    /// <param name="strMonitor"></param>
    /// <param name="strDutyType"></param>
    /// <param name="isAuDefault"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool ClearSampUserDuty(string strUserId, string strMonitor, string strDutyType, string isAuDefault)
    {
        bool flag = false, strMoveDefaultAu = false, strMoveDefaultEx = false;
        string[] strUser_Id = strUserId.Split(';');
        if(isAuDefault=="true")
        {
            strMoveDefaultAu=true;
        }
        else
        {
            strMoveDefaultEx=true;
        }
        if (new TSysUserDutyLogic().MoveDefaultSet(strUser_Id, strMonitor, strDutyType, strMoveDefaultAu, strMoveDefaultEx))
        {
            flag = true;
        }
        return flag;
    }

    /// <summary>
    /// 获取部门
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetDeptItems()
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        TSysDictVo objVo = new TSysDictVo();
        objVo.DICT_TYPE="dept";
        dt = new TSysDictLogic().SelectByTable(objVo);
        listResult = LigerGridSelectDataToJson(dt, dt.Rows.Count);
        return listResult;
    }
    /// <summary>
    /// 获取选择部门的尚未选中的用户
    /// </summary>
    /// <param name="strPost_Dept"></param>
    /// <param name="strMonitorId"></param>
    /// <param name="strDutyType"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSubUserItems(string strPost_Dept,string strMonitorId,string strDutyType)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        DataTable dtDuty = new DataTable();
        dt = new TSysUserLogic().SelectByTableUnderDept(strPost_Dept,0,0);

        dtDuty = new TSysUserDutyLogic().SelectUserDuty(strDutyType, strMonitorId);

        DataTable dtItems = new DataTable();
        dtItems = dt.Copy();
        dtItems.Clear();

        if (dtDuty.Rows.Count > 0)
        {
            for (int i = 0; i < dtDuty.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dtDuty.Rows[i]["USERID"].ToString()))
                {
                    DataRow[] dr = dt.Select("ID='" + dtDuty.Rows[i]["USERID"].ToString() + "'");
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

        listResult = LigerGridSelectDataToJson(dtItems, dtItems.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 获取选择部门的尚已选中的用户
    /// </summary>
    /// <param name="strPost_Dept"></param>
    /// <param name="strMonitorId"></param>
    /// <param name="strDutyType"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSelectUserItems(string strMonitorId, string strDutyType)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        DataTable dtDuty = new DataTable();
        dt = new TSysUserLogic().SelectByTableUnderDept("", 0, 0);

        dtDuty = new TSysUserDutyLogic().SelectUserDuty(strDutyType, strMonitorId);

        DataTable dtItems = new DataTable();
        dtItems = dt.Copy();
        dtItems.Clear();

        if (dtDuty.Rows.Count > 0)
        {
            for (int i = 0; i < dtDuty.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dtDuty.Rows[i]["USERID"].ToString()))
                {
                    DataRow[] dr = dt.Select("ID='" + dtDuty.Rows[i]["USERID"].ToString() + "'");
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

        listResult = LigerGridSelectDataToJson(dtItems, dtItems.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 插入选择的当前监测类别可设置的用户
    /// </summary>
    /// <param name="strMonitor"></param>
    /// <param name="strUserId"></param>
    /// <param name="strDutyType"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool InsertSelectedUser(string strUserId, string strMonitor, string strDutyType, string strMoveUserId)
    {
        bool result = false;
        string[] strUerArr = null,strMoveUserArr=null;
        TSysDutyVo objtd = new TSysDutyVo();
        objtd.MONITOR_TYPE_ID = strMonitor;
        objtd.DICT_CODE = strDutyType;
        strUerArr = strUserId.Split(';');

        strMoveUserArr = strMoveUserId.Split(';');

        //如果存在岗位配置，则直接插入用户岗位表
        DataTable dt = new DataTable();
        dt = new TSysDutyLogic().SelectByTable(objtd);
        if (strMoveUserArr != null && strUerArr != null)
        {
            //如果要删除的 存在，则先进行删除，然后再进行添加
            if (new TSysUserDutyLogic().DeleteUserMonitorSet(strMoveUserArr, strMonitor, strDutyType))
            {
                if (dt.Rows.Count > 0)
                {
                    if (new TSysUserDutyLogic().InsertSelectedUser(strUerArr, dt.Rows[0]["ID"].ToString()))
                    {
                        result = true;
                    }

                }
                else
                {
                    objtd.ID = GetSerialNumber("duty_set_infor");

                    if (new TSysDutyLogic().Create(objtd))
                    {
                        if (new TSysUserDutyLogic().InsertSelectedUser(strUerArr, objtd.ID))
                        {
                            result = true;
                        }
                    }
                }
            }
        }
        //如果删除的不存在，则直接进行添加操作
        if (strMoveUserId == null && strUerArr != null)
        {

            if (dt.Rows.Count > 0)
            {
                if (new TSysUserDutyLogic().InsertSelectedUser(strUerArr, dt.Rows[0]["ID"].ToString()))
                {
                    result = true;
                }

            }
            else
            {
                objtd.ID = GetSerialNumber("duty_set_infor");

                if (new TSysDutyLogic().Create(objtd))
                {
                    if (new TSysUserDutyLogic().InsertSelectedUser(strUerArr, objtd.ID))
                    {
                        result = true;
                    }
                }
            }
           
        }
        return result;
    }
}