using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using i3.View;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using System.Data;
using System.Web.Services;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Channels.OA.SW;
using i3.BusinessLogic.Channels.OA.SW;

/// <summary>
/// 收文办理
/// 创建人：魏林
/// 创建时间：2013-07-17
/// </summary>
public partial class Channels_OA_SW_ZZ_SWHandle : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            InitPage();

            //加载收文数据
            if (Request["ID"] != null)
            {
                hidTaskId.Value = Request["ID"].ToString();
                InitData(Request["ID"].ToString());
            }
        }
    }

    //初始化
    private void InitPage()
    {
        //加载下拉列表
        BindDataDictToControl("FW_MJ", this.MJ);
        HandlerDataBind();

        this.SW_REG_DATE.Text = DateTime.Now.ToShortDateString();
        this.SW_SIGN_DATE.Text = DateTime.Now.ToShortDateString();

        this.hidUserID.Value = LogInfo.UserInfo.ID;
        
    }

    private void HandlerDataBind()
    {
        TSysUserVo objUser = new TSysUserVo();
        objUser.IS_USE = "1";
        objUser.IS_DEL = "0";
        objUser.IS_HIDE = "0";
        DataTable dt = new DataTable();
        dt = new TSysUserLogic().SelectByTable(objUser);

        HandlerList.DataSource = dt;
        HandlerList.DataTextField = "REAL_NAME";
        HandlerList.DataValueField = "ID";
        HandlerList.DataBind();

        //有直接上级就默认选择直接上级
        if (HandlerList.Items.Count > 0)
        {
            string strLocalUserID = base.LogInfo.UserInfo.ID;
            DataTable dtDeptAdmin = new TSysUserPostLogic().SelectDeptAdmin_byTable(strLocalUserID);
            if (dtDeptAdmin.Rows.Count > 0)
            {
                string strDeptAdminId = dtDeptAdmin.Rows[0]["user_id"].ToString();
                for (int i = 0; i < HandlerList.Items.Count; i++)
                {
                    if (HandlerList.Items[i].Value == strDeptAdminId)
                    {
                        HandlerList.SelectedIndex = i;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <param name="strID">收文ID</param>
    private void InitData(string strID)
    {
        DataTable dt = new TOaSwInfoLogic().GetSWDetails(strID);
        if (dt.Rows.Count > 0)
        {
            FROM_CODE.Text = dt.Rows[0]["FROM_CODE"].ToString();
            if (dt.Rows[0]["SW_REG_DATE"].ToString() != "")
                SW_REG_DATE.Text = DateTime.Parse(dt.Rows[0]["SW_REG_DATE"].ToString()).ToShortDateString();
            SW_TITLE.Text = dt.Rows[0]["SW_TITLE"].ToString();
            SUBJECT_WORD.Text = dt.Rows[0]["SUBJECT_WORD"].ToString();
            SW_FROM.Text = dt.Rows[0]["SW_FROM"].ToString();
            SW_COUNT.Text = dt.Rows[0]["SW_COUNT"].ToString();
            MJ.SelectedValue = dt.Rows[0]["SW_MJ"].ToString();
            SW_SIGN_ID.Text = dt.Rows[0]["SW_SIGN_ID"].ToString();
            if (dt.Rows[0]["SW_SIGN_DATE"].ToString() != "")
                SW_SIGN_DATE.Text = DateTime.Parse(dt.Rows[0]["SW_SIGN_DATE"].ToString()).ToShortDateString();
            if (dt.Rows[0]["PIGONHOLE_DATE"].ToString() != "")
                PIGONHOLE_DATE.Text = DateTime.Parse(dt.Rows[0]["PIGONHOLE_DATE"].ToString()).ToShortDateString();
            SW_CODE.Text = dt.Rows[0]["SW_CODE"].ToString();
            Hid_ReadUserIDs.Value = dt.Rows[0]["ReadUserID"].ToString();
            ReadUserNames.Value = dt.Rows[0]["ReadUserName"].ToString();
            Hid_MakeUserIDs.Value = dt.Rows[0]["MakeUserID"].ToString();
            MakeUserNames.Value = dt.Rows[0]["MakeUserName"].ToString();
            SW_PLAN2_INFO.Value = dt.Rows[0]["SW_PLAN2"].ToString();
            SW_PLAN3_INFO.Value = dt.Rows[0]["SW_PLAN3"].ToString();
            SW_PLAN4_INFO.Value = dt.Rows[0]["SW_PLAN4"].ToString();
            SW_PLAN5_INFO.Value = dt.Rows[0]["SW_PLAN5"].ToString();
        }
    }

    /// <summary>
    /// 获取批办意见
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetSuggions(string strSWID, string strS)
    {
        TOaSwHandleVo TOaSwHandle = new TOaSwHandleVo();
        TOaSwHandle.SW_ID = strSWID;
        TOaSwHandle.SW_HANDER = strS;
        TOaSwHandle.IS_OK = "1";

        DataTable dt = new TOaSwHandleLogic().SelectByTable(TOaSwHandle, true);

        string json = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            json += "意见：" + dt.Rows[i]["Suggion"].ToString()+"\n";
            json += "办理人：" + dt.Rows[i]["UserName"].ToString()+"\t\t\t";
            json += "办理时间：" + dt.Rows[i]["PlanDate"].ToString()+"\n\n";
        }

        return json;
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
        objVo.DICT_TYPE = "dept";
        dt = new TSysDictLogic().SelectByTable(objVo);
        listResult = LigerGridSelectDataToJson(dt, dt.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 获取选择部门的尚未选中的用户
    /// </summary>
    /// <param name="strPost_Dept"></param>
    /// <param name="strMessageId"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSubUserItems(string strPost_Dept, string strUserID)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        TSysUserVo objUser = new TSysUserVo();
        objUser.IS_DEL = "0";
        dt = new TSysUserLogic().SelectByTableUnderDept(strPost_Dept, 0, 0);

        DataTable dtItems = new DataTable();
        dtItems = dt.Copy();
        dtItems.Clear();
        if (strUserID.Length > 0)
        {
            for (int i = 0; i < strUserID.Split(',').Length; i++)
            {
                DataRow[] dr = dt.Select("ID='" + strUserID.Split(',')[i] + "'");
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

        dtItems = dt.Copy();

        listResult = LigerGridSelectDataToJson(dtItems, dtItems.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 获取选择部门的已选中的用户
    /// </summary>
    /// <param name="strPost_Dept"></param>
    /// <param name="strMonitorId"></param>
    /// <param name="strDutyType"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<object> GetSelectUserItems(string strUserID)
    {
        List<object> listResult = new List<object>();
        DataTable dt = new DataTable();
        DataTable dtDuty = new DataTable();
        dt = new TSysUserLogic().SelectByTableUnderDept("", 0, 0);

        DataTable dtItems = new DataTable();
        dtItems = dt.Copy();
        dtItems.Clear();

        if (strUserID.Length > 0)
        {
            for (int i = 0; i < strUserID.Split(',').Length; i++)
            {
                DataRow[] dr = dt.Select("ID='" + strUserID.Split(',')[i] + "'");
                if (dr != null)
                {
                    foreach (DataRow Temrow in dr)
                    {
                        dtItems.ImportRow(Temrow);
                    }
                }
            }
        }
        
        listResult = LigerGridSelectDataToJson(dtItems, dtItems.Rows.Count);
        return listResult;
    }

    /// <summary>
    /// 保存收文数据
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string SaveData(string LoginID, string strID,string strFromCode,string strSwCode,string strSwFrom, string strSwCount,string strSwMJ,string strSwTitle, string strSubWord,string strSwSignID,string strSwSignDate)
    {
        string strResult = "";
        TOaSwInfoVo objSW = new TOaSwInfoVo();
        objSW.FROM_CODE = strFromCode;            //原文编号
        objSW.SW_CODE = strSwCode;              //收文编号
        objSW.SW_FROM = strSwFrom;              //来文单位
        objSW.SW_COUNT = strSwCount;             //收文份数
        objSW.SW_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:00:00");              //收文日期
        objSW.SW_MJ = strSwMJ;                //密级
        objSW.SW_TITLE = strSwTitle;             //收文标题
        objSW.SUBJECT_WORD = strSubWord.Replace("，", ",");           //主题词
        objSW.SW_SIGN_ID = strSwSignID;           //签收人
        objSW.SW_SIGN_DATE = strSwSignDate;         //签收日期
        objSW.SW_REG_ID = LoginID;          //登记人ID
        objSW.SW_REG_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");          //登记日期
        objSW.SW_STATUS = "0";            //收文状态
        if (strID == "")
        {
            //新增
            objSW.ID = GetSerialNumber("t_oa_SWInfoID");
            if (new TOaSwInfoLogic().Create(objSW))
                strResult = objSW.ID;
        }
        else
        {
            //修改
            objSW.ID = strID;
            if (new TOaSwInfoLogic().Edit(objSW))
                strResult = objSW.ID;
        }
        
        return strResult;
    }

    /// <summary>
    /// 发送收文数据
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string SendData(string LoginID, string Action,string ID, string Status,string Reader,string Maker,string Handler,string SW_PLAN2,string SW_PLAN3,string SW_PLAN4,string SW_PLAN5)
    {
        bool b = false;
        string NewStatus = "";
        string Suggestion = "";
        //收文登记——》主任阅示
        if (Action == "Add" || Action == "Update")
        {
            NewStatus = "1";
            Suggestion = "";
            Status = "0";
        }
        if (Action == "Handle")
        {
            //主任阅示——》站长阅示
            if (Status == "1")
            {
                NewStatus = "2";
                Suggestion = SW_PLAN2;
            }
            //站长阅示——》分管阅办
            if (Status == "2" && Reader.Trim() != "")
            {
                NewStatus = "3";
                Suggestion = SW_PLAN3;
            }
            //站长阅示——》科室办结
            if (Status == "2" && Reader.Trim() == "" && Maker.Trim() != "")
            {
                NewStatus = "4";
                Suggestion = SW_PLAN3;
            }
            //分管阅办——》科室办结
            if (Status == "3" && Maker.Trim() != "")
            {
                NewStatus = "4";
                Suggestion = SW_PLAN4;
            }
            //分管阅办——》文件完结
            if (Status == "3" && Maker.Trim() == "")
            {
                NewStatus = "5";
                Suggestion = SW_PLAN4;
            }
            //科室办结——》文件完结
            if (Status == "4")
            {
                NewStatus = "5";
                Suggestion = SW_PLAN5;
            }
        }
        b = new TOaSwInfoLogic().SendSW(ID, NewStatus, Status, LoginID, Suggestion, Handler, Reader, Maker, "t_oa_SWHandleID");

        if (b)
            return "成功";
        else
            return "";
    }

    /// <summary>
    /// 完成（归档）收文
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string FinishData(string ID, string Status, string LoginID)
    {
        bool b = false;

        b = new TOaSwInfoLogic().FinishSW(ID, "6", Status, LoginID);

        if (b)
            return "成功";
        else
            return "";
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        string swID = this.hidTaskId.Value.Trim();

        if (swID == "")
            return;

        new ZZFWBase().SWPrint(swID);
    }
}