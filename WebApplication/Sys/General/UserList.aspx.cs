using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;

/// <summary>
/// 功能描述：用户列表
/// 创建日期：2011-04-07 15:00
/// 创建人  ：郑义
/// 修改时间：2011-04-14 17:00
/// 修改人  ：郑义
/// 修改内容：更改所有符合开发规范
/// 修改时间：2012-11-19
/// 修改内容：根据新操作模式，重构代码
/// 修改人：潘德军
/// </summary>
public partial class Sys_General_UserList : PageBase
{
    string strUserID;
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义结果
        string strResult = "";
        strUserID = LogInfo.UserInfo.ID;
        //获取点位信息
        if (Request["type"] != null && Request["type"].ToString() == "getData")
        {
            strResult = getUserList();
            Response.Write(strResult);
            Response.End();
        }
    }

    //获取点位信息
    private string getUserList()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        if (strSortname == null || strSortname.Length < 0)
            strSortname = TSysUserVo.ORDER_ID_FIELD;

        string strDeptID = "";string Real_Name="";
        if (Request["strSrhDept_ID"] != null)
            strDeptID = Request.Params["strSrhDept_ID"];
        if (Request["Real_Name"] != null)
            Real_Name = Request.Params["Real_Name"];

        TSysUserVo objVo = new TSysUserVo();
        objVo.IS_DEL = "0";
        //objVo.IS_USE = "1";
        objVo.IS_HIDE = "0";
        objVo.REMARK4 = strDeptID;
        objVo.SORT_FIELD = strSortname;
        objVo.SORT_TYPE = strSortorder;
        objVo.REAL_NAME = Real_Name;
        int intTotalCount = new TSysUserLogic().GetSelectResultCount_ByDept(objVo);
        DataTable dt = new TSysUserLogic().SelectByTable_ByDept(objVo, intPageIndex, intPageSize);

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["IS_USE"].ToString() == "0")
                dr["IS_USE"] = "是";
            else
                dr["IS_USE"] = "";
        }
        dt.AcceptChanges();
        
        string strJson = CreateToJson(dt, intTotalCount);
        return strJson;
    }

    // 删除点位信息
    [WebMethod]
    public static string deleteData(string strValue)
    {
        TSysUserVo objVo = new TSysUserVo();
        objVo.ID = strValue;
        objVo.IS_DEL = "1";
        bool isSuccess = new TSysUserLogic().Edit(objVo);
        if (isSuccess)
        {
            new PageBase().WriteLog("删除用户", "", new UserLogInfo().UserInfo.USER_NAME + "删除用户" + strValue);
        }
        return isSuccess == true ? "1" : "0";
    }

    //编辑点位数据
    [WebMethod]
    public static string SaveData(string strID, string strUSER_NAME, string strREAL_NAME, string strORDER_ID, string strBIRTHDAY, string strSEX, string strPHONE_OFFICE,
        string strPHONE_MOBILE, string strPHONE_HOME, string strEMAIL, string strADDRESS, string strPOSTCODE, string strIS_USE,
        string strIOS_MAC,string strIF_IOS,string strANDROID_MAC,string strIF_ANDROID,
        string strREMARK1,string strUSER_PWD)
    {
        bool isSuccess = true;

        TSysUserVo objVo = new TSysUserVo();
        objVo.ID = strID.Length > 0 ? strID : GetSerialNumber("user_info_id");
        objVo.IS_DEL = "0";
        objVo.IS_HIDE = "0";
        objVo.USER_NAME = strUSER_NAME;
        objVo.REAL_NAME = strREAL_NAME;
        objVo.ORDER_ID = strORDER_ID;
        objVo.BIRTHDAY = strBIRTHDAY;
        objVo.SEX = strSEX;
        objVo.PHONE_OFFICE = strPHONE_OFFICE;
        objVo.PHONE_MOBILE = strPHONE_MOBILE;
        objVo.PHONE_HOME = strPHONE_HOME;
        objVo.EMAIL = strEMAIL;
        objVo.ADDRESS = strADDRESS;
        objVo.POSTCODE = strPOSTCODE;
        objVo.IS_USE = strIS_USE.Length > 0 ? strIS_USE : "1";
        objVo.USER_PWD = strUSER_PWD;
        objVo.IOS_MAC = strIOS_MAC;
        objVo.IF_IOS = strIF_IOS.Length > 0 ? strIF_IOS : "0";
        objVo.ANDROID_MAC = strANDROID_MAC;
        objVo.IF_ANDROID = strIF_ANDROID.Length > 0 ? strIF_ANDROID : "0";

        i3.View.PageBase objP = new i3.View.PageBase();
        if (string.IsNullOrEmpty(objVo.USER_PWD))
        {
            objVo.USER_PWD = objP.ToMD5("1");
        }
        else
        {
            objVo.USER_PWD = objP.ToMD5(objVo.USER_PWD);
        }
        objVo.CREATE_ID = objP.LogInfo.UserInfo.ID;
        objVo.CREATE_TIME = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        if (strID.Length > 0)
        {
            isSuccess = new TSysUserLogic().Edit(objVo);
            if (isSuccess)
            {
                new PageBase().WriteLog("编辑用户", "", new UserLogInfo().UserInfo.USER_NAME + "编辑用户" + objVo.ID);
            }
        }
        else
        {
            isSuccess = new TSysUserLogic().Create(objVo);
            if (isSuccess)
            {
                new PageBase().WriteLog("添加用户", "", new UserLogInfo().UserInfo.USER_NAME + "添加用户" + objVo.ID);
            }
        }

        TSysUserPostLogic logicUp = new TSysUserPostLogic();
        if (strID.Length > 0)
        {
            TSysUserPostVo objUserPostDel = new TSysUserPostVo();
            objUserPostDel.USER_ID = strID;
            logicUp.Delete(objUserPostDel);
        }

        string strPostId = strREMARK1.Split('|')[0];
        string[] arrPostId = strPostId.Split('，');
        for (int i = 0; i < arrPostId.Length; i++)
        {
            TSysUserPostVo objUserPost = new TSysUserPostVo();
            objUserPost.USER_ID = objVo.ID;
            objUserPost.POST_ID = arrPostId[i];
            objUserPost.ID = GetSerialNumber("user_post_infor");
            isSuccess = new TSysUserPostLogic().Create(objUserPost);
            if (isSuccess)
            {
                new PageBase().WriteLog("添加职位菜单", "", new UserLogInfo().UserInfo.USER_NAME + "添加职位菜单" + objUserPost.ID);
            }
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
    /// 获取职位信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getPostName(string strValue)
    {
        string strResults = "";

        DataTable dt = new TSysPostLogic().SelectByTable_byUser(strValue);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strResults += (strResults.Length > 0 ? "，" : "") + dt.Rows[i]["POST_NAME"].ToString();
        }
        return strResults;
    }

    /// <summary>
    /// 获取部门信息
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDeptName(string strValue)
    {
        string strResults = "";

        List<TSysDictVo> lstDict = new TSysDictLogic().GetDataDictListByType("dept");

        DataTable dt = new TSysPostLogic().SelectByTable_byUser(strValue);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string strDeptCode = dt.Rows[i]["POST_DEPT_ID"].ToString();
            if (strDeptCode.Length > 0)
            {
                for (int j = 0; j < lstDict.Count; j++)
                {
                    if (lstDict[j].DICT_CODE == strDeptCode)
                    {
                        if (strResults.IndexOf(lstDict[j].DICT_TEXT)<0)
                            strResults += (strResults.Length > 0 ? "，" : "") + lstDict[j].DICT_TEXT;
                    }
                }
            }

        }
        return strResults;
    }
}