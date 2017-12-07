using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using System.Data.SqlClient;

using i3.View;
using System.Data;
using System.Web.Services;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.BusinessLogic.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using i3.BusinessLogic.Channels.Mis.Monitor.Task;
using i3.ValueObject.Channels.Base.Item;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.SubTask;
using i3.BusinessLogic.Channels.Mis.Monitor.Return;
using i3.ValueObject.Channels.Base.Method;
using i3.BusinessLogic.Channels.Base.Method;
using WebApplication;
using System.Text;
using WebApplication.Channels.Base.ProcessMgm;

/// <summary>
/// 功能描述：分析结果录入\分析结果校核
/// 创建日期：2015-01-21
/// 创建人  ：魏林
/// </summary>
public partial class Channels_MisII_Monitor_Result_AnalysisResult : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // ccflow结点发送前事件
            if (Request["type"] != null && Request["type"].ToString() == "check")
            {
                var workID = Convert.ToInt64(Request.QueryString["OID"]);

                var flowId = Request.QueryString["FK_Flow"];
                var nodeId = Convert.ToInt32(Request.QueryString["FK_Node"]);
                var fid = Convert.ToInt32(Request.QueryString["FID"]);


                var identification = CCFlowFacade.GetFlowIdentification(Request["UserNo"], workID);

                //// yinchengyi 2015-4-2 记录monistor_result对应的子流程ID和父线程ID
                //TMisMonitorResultVo objMomRstVo = new TMisMonitorResultVo();
                //objMomRstVo.ID = identification;

                //objMomRstVo.CCFLOW_ID1 = workID.ToString();
                //objMomRstVo.CCFLOW_ID2 = fid.ToString();

                //if (!new TMisMonitorResultLogic().Edit(objMomRstVo))
                //{
                //    Response.Write("false流程ID更新失败，不能发送");
                    
                    
                //}

                Response.ContentType = "text/plain";
                Response.ContentEncoding = Encoding.UTF8;
                Response.Write("true");              
                Response.End();
            }

            //黄飞 20150916  监测分析  批处理
            if (Request.QueryString["WorkIDs"] != null || Request.QueryString["WorkID"] != null)
            {
                string strWorkIDs = Request.QueryString["WorkIDs"];
                if(string.IsNullOrEmpty(strWorkIDs))
                    strWorkIDs = Request.QueryString["WorkID"];

                string[] strarr = strWorkIDs.Split(',');
                for (int i = 0; i < strarr.Length; i++)
                {
                    if (strarr[i] != "" || !string.IsNullOrEmpty(strarr[i]))
                    {

                        var workID = Convert.ToInt64(strarr[i]);
                        var identification = CCFlowFacade.GetFlowIdentification(LogInfo.UserInfo.USER_NAME, workID);

                        this.RESULT_ID.Value = this.RESULT_ID.Value + "," + identification;

                        // yinchengyi 2015-4-2 记录monistor_result对应的子流程ID和父线程ID
                        TMisMonitorResultVo objMomRstVo = new TMisMonitorResultVo();
                        objMomRstVo.ID = identification;

                        var fatherID = CCFlowFacade.GetFatherThreadIDOfSubFlow(LogInfo.UserInfo.USER_NAME, workID);

                        objMomRstVo.CCFLOW_ID1 = workID.ToString();
                        objMomRstVo.CCFLOW_ID2 = fatherID.ToString();

                        if (!new TMisMonitorResultLogic().Edit(objMomRstVo))
                        {
                            //todo-yinchengyi 

                        }
                    }
                }
            }

           


            //定义结果
            string strResult = "";
            //任务信息
            if (Request["type"] != null && Request["type"].ToString() == "getOneGridInfo")
            {
                strResult = getOneGridInfo(Request["strResultID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getTwoGridInfo")
            {
                strResult = getTwoGridInfo(Request["oneGridId"].ToString(), Request["strResultID"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目实验室质控信息
            if (Request["type"] != null && Request["type"].ToString() == "getThreeGridInfo")
            {
                strResult = getThreeGridInfo(Request["twoGridId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getAnalysisByItemId")
            {
                strResult = getAnalysisByItemId(Request["strItemId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //监测项目信息
            if (Request["type"] != null && Request["type"].ToString() == "getSampleInfor")
            {
                strResult = getSampleInfor(Request["strSampleId"].ToString());
                Response.Write(strResult);
                Response.End();
            }
            //获取样品编号及结果值
            if (Request["type"] != null && Request["type"].ToString() == "getItemInfo")
            {
                strResult = getItemInfo(Request["resultids"].ToString());
                Response.Write(strResult);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 获取监测点信息
    /// </summary>
    /// <returns></returns>
    private string getOneGridInfo(string strResultID)
    {
        //TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
        //objResultVo.ID = strResultID;
        //DataTable dt = new TMisMonitorResultLogic().SelectByTable(objResultVo);

        //string strJson = CreateToJson(dt, dt.Rows.Count);
        //return strJson;

        TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
        strResultID = strResultID.Substring(strResultID.IndexOf(",") + 1);
        strResultID = "'" + strResultID + "'";
        strResultID = strResultID.Replace(",", "','");
        objResultVo.ID = strResultID;

        //DataTable dt = new TMisMonitorResultLogic().SelectByTable(objResultVo);
        //黄飞20150720
        DataTable dt = new TMisMonitorResultLogic().SelectByTableOne(objResultVo);
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;


    }
    /// <summary>
    /// 获取监测项目类别信息
    /// </summary>
    /// <returns></returns>
    private string getTwoGridInfo(string strOneGridId, string strResultID)
    {
        //string strSortname = Request.Params["sortname"];
        //string strSortorder = Request.Params["sortorder"];
        ////当前页面
        //int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        ////每页记录数
        //int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);
        //DataTable dt = new TMisMonitorResultLogic().getSampleCodeInResult_MAS(strOneGridId);

        //string strJson = CreateToJson(dt, dt.Rows.Count);
        //return strJson;

        strResultID = strResultID.Substring(strResultID.IndexOf(",") + 1);
        strResultID = "'" + strResultID + "'";
        strResultID = strResultID.Replace(",", "','");
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        //当前页面
        int intPageIndex = Convert.ToInt32(Request.Params["page"]);
        //每页记录数
        int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

        //DataTable dt = new TMisMonitorResultLogic().getSampleCodeInResult_MAS(strOneGridId);
        //批量处理  黄飞20150916
        DataTable dt = new TMisMonitorResultLogic().getSampleCodeInResult_MAS_Batch(strOneGridId, strResultID);

        //by yinchengyi 2015-9-21 从管理时限模块加载完成时间
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string strCurRstID = dt.Rows[i]["ID"].ToString();

            string strStartTime = "";
            string strFinishTime = "";
            ProcessMgm objProcessMgm = new ProcessMgm();
            int nRst = objProcessMgm.GetAskingTimeByResultID(strCurRstID, "ANALYSE_DATE", ref strStartTime, ref strFinishTime);
            if (0 == nRst)
            {
                dt.Rows[i]["ASKING_DATE"] = strFinishTime;
            }
        }

        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;

    }
    /// <summary>
    /// 获取实验室质控信息
    /// </summary>
    /// <param name="strTwoGridId"></param>
    /// <returns></returns>
    private string getThreeGridInfo(string strTwoGridId)
    {
        DataTable dt = new TMisMonitorResultLogic().getQcDetailInfo_QY(strTwoGridId, "1", "", "");
        string strJson = CreateToJson(dt, dt.Rows.Count);
        return strJson;
    }
    /// <summary>
    /// 根据监测项目获取分析方法
    /// </summary>
    /// <param name="strItemId">监测项目ID</param>
    /// <returns></returns>
    public string getAnalysisByItemId(string strItemId)
    {
        DataTable dt = new TMisMonitorResultLogic().getAnalysisByItemId(strItemId);
        return DataTableToJson(dt);
    }

    /// <summary>
    /// 创建原因：根据样品ID 获取样品信息
    /// 创建人：胡方扬
    /// 创建日期：2013-07-10
    /// </summary>
    /// <param name="strSampleId"></param>
    /// <returns></returns>
    public string getSampleInfor(string strSampleId) {
        DataTable dt = new TMisMonitorSampleInfoLogic().SelectByTable(new TMisMonitorSampleInfoVo { ID = strSampleId });
        return LigerGridDataToJson(dt, dt.Rows.Count);
    }
    /// <summary>
    /// 获取监测项目相关信息
    /// </summary>
    /// <param name="strItemCode">监测项目ID</param>
    /// <param name="strItemName">监测项目信息名称</param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemInfoName(string strItemCode, string strItemName)
    {
        string strReturnValue = "";
        i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.ValueObject.Channels.Base.Item.TBaseItemInfoVo();
        TBaseItemInfoVo.ID = strItemCode;
        DataTable dt = new i3.BusinessLogic.Channels.Base.Item.TBaseItemInfoLogic().SelectByTable(TBaseItemInfoVo);
        if (dt.Rows.Count > 0)
            strReturnValue = dt.Rows[0][strItemName].ToString();
        return strReturnValue;
    }

    /// <summary>
    /// 获取默认分析负责人名称
    /// </summary>
    /// <param name="strUserId">分析负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserName(string strUserId)
    {
        return new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserId).REAL_NAME;
    }
    /// <summary>
    /// 获取默认分析协同人信息
    /// </summary>
    /// <param name="strUserId">分析负责人ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserExName(string strUserId)
    {
        if (strUserId.Trim() == "") return "";
        List<string> list = strUserId.Split(',').ToList();
        string strSumUserExName = "";
        string spit = "";
        foreach (string strUserExId in list)
        {
            string strUserName = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserExId).REAL_NAME;
            strSumUserExName = strSumUserExName + spit + strUserName;
            spit = ",";
        }
        return strSumUserExName;
    }
    /// <summary>
    /// 获取获取分析协同人信息
    /// </summary>
    /// <param name="strResultId">结果ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string getDefaultUserExNameEdit(string strResultId)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UserId", typeof(string));
        dt.Columns.Add("UserName", typeof(string));

        TMisMonitorResultAppVo TMisMonitorResultAppVo = new TMisMonitorResultAppVo();
        TMisMonitorResultAppVo.RESULT_ID = strResultId;
        DataTable objTable = new TMisMonitorResultAppLogic().SelectByTable(TMisMonitorResultAppVo);
        if (objTable.Rows.Count == 0) return DataTableToJson(dt);

        string strUserExIds = objTable.Rows[0]["ASSISTANT_USERID"].ToString();
        if (strUserExIds == "") return DataTableToJson(dt);

        List<string> list = strUserExIds.Split(',').ToList();
        string strSumUserExName = "";
        string spit = "";
        foreach (string strUserExId in list)
        {
            string strUserName = new i3.BusinessLogic.Sys.General.TSysUserLogic().Details(strUserExId).REAL_NAME;
            strSumUserExName = strSumUserExName + spit + strUserName;
            spit = ",";
        }
        DataRow row = dt.NewRow();
        row["UserId"] = strUserExIds;
        row["UserName"] = strSumUserExName;
        dt.Rows.Add(row);

        return DataTableToJson(dt);
    }
    /// <summary>
    /// 获取质控手段名称
    /// </summary>
    /// <param name="strQcId">质控手段编码</param>
    /// <returns></returns>
    [WebMethod]
    public static string getQcName(string strQcId)
    {
        string strQcName = "";

        if (strQcId == "") return "";
        List<string> list = strQcId.Split(',').ToList();
        string spit = "";
        foreach (string strQc in list)
        {
            if (strQc == "0")
                strQcName = strQcName + spit + "原始样";
            if (strQc == "1")
                strQcName = strQcName + spit + "现场空白";
            if (strQc == "2")
                strQcName = strQcName + spit + "现场加标";
            if (strQc == "3")
                strQcName = strQcName + spit + "现场平行";
            if (strQc == "4")
                strQcName = strQcName + spit + "实验室密码平行";
            if (strQc == "5")
                strQcName = strQcName + spit + "实验室空白";
            if (strQc == "6")
                strQcName = strQcName + spit + "实验室加标";
            if (strQc == "7")
                strQcName = strQcName + spit + "实验室明码平行";
            if (strQc == "8")
                strQcName = strQcName + spit + "标准样";
            spit = ",";
        }
        return strQcName;
    }
    /// <summary>
    /// 保存监测项目信息
    /// </summary>
    /// <param name="id">分析结果ID</param>
    /// <param name="strItemResult">分析结果</param>
    /// <param name="strAnalysisMethod">分析方法</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveItemInfo(string id, string strColumnName, string strValue)
    {
        bool isSuccess = true;
        TMisMonitorResultVo TMisMonitorResultVo = new TMisMonitorResultVo();
        TMisMonitorResultVo.ID = id;
        switch (strColumnName)
        {
            case "ITEM_RESULT":
                TMisMonitorResultVo.ITEM_RESULT = strValue == "" ? "###" : strValue;
                break;
            case "ANALYSIS_METHOD_ID":
                TMisMonitorResultVo.ANALYSIS_METHOD_ID = strValue;
                TBaseItemAnalysisVo objItemAnalysisVo = new TBaseItemAnalysisLogic().Details(strValue);
                TMisMonitorResultVo.RESULT_CHECKOUT = objItemAnalysisVo.LOWER_CHECKOUT;
                break;
            case "RESULT_CHECKOUT":
                TMisMonitorResultVo.RESULT_CHECKOUT = strValue;
                break;
            default:
                break;
        }

        isSuccess = new TMisMonitorResultLogic().Edit(TMisMonitorResultVo);

        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 保存分析实际完成时间
    /// </summary>
    /// 黄进军 添加 20150424   
    /// <param name="id">RESULT_ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string saveItemFinishDate(string id, string strFinishDate)
    {
        bool isSuccess = true;
        TMisMonitorResultAppVo resultapp = new TMisMonitorResultAppVo();
        resultapp.RESULT_ID = id;
        DataTable dt = new TMisMonitorResultAppLogic().SelectByTable(resultapp);
        if (dt.Rows.Count > 0)
        {
            resultapp.ID = dt.Rows[0]["ID"].ToString();
            resultapp.FINISH_DATE = strFinishDate;
            isSuccess = new TMisMonitorResultAppLogic().Edit(resultapp);
        }
        else {
            isSuccess = false;
        }

        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 修改检出限
    /// </summary>
    /// <param name="id"></param>
    /// <param name="strItemResult"></param>
    /// <param name="strAnalysisMethod"></param>
    /// <param name="strFinishDate"></param>
    /// <returns></returns>
    [WebMethod]
    public static string saveCheckInfo(string strItemID, string strcheckID, string strLowResult, string strAnalysisMethod)
    {
        bool isSuccess = false;
        TBaseItemAnalysisVo objItemAnalysisSet = new TBaseItemAnalysisVo();
        objItemAnalysisSet.ID = strcheckID;
        objItemAnalysisSet.ITEM_ID = strItemID;
        objItemAnalysisSet.ANALYSIS_METHOD_ID = strAnalysisMethod;
        objItemAnalysisSet.LOWER_CHECKOUT = strLowResult;
        isSuccess = new TBaseItemAnalysisLogic().Edit(objItemAnalysisSet);
        return isSuccess == true ? "1" : "0";
    }



    /// <summary>
    /// 删除实验室质控样 Create By：weilin 2013-12-13
    /// </summary>
    /// <param name="id"></param>
    /// <param name="qc_type">实验室质控类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteQcAnalysis(string id, string qc_type, string result_id)
    {
        bool isSuccess = false;
        string strQC = (new TMisMonitorResultLogic().Details(result_id).QC + ",").Replace(qc_type + ",", "").TrimEnd(',').TrimStart(',');

        TMisMonitorResultVo objResultVo = new TMisMonitorResultVo();
        objResultVo.ID = result_id;
        objResultVo.QC = strQC == "" ? "0" : strQC;

        if (new TMisMonitorResultLogic().Edit(objResultVo))
        {
            isSuccess = new TMisMonitorResultLogic().deleteQcAnalysis(id, qc_type);
        }
        return isSuccess == true ? "1" : "0";
    }

    public string CheckDustyTable(string strSumResultId)
    {
        bool isSuccess = true;
        DataTable dtDustinfor = new TMisMonitorDustinforLogic().SelectTableByID(strSumResultId);
        DataRow[] drDustinfor;

        //如果是烟尘,粉尘,总悬浮颗粒物,颗粒物（000001827,000001945,000000130,000000220）初重、终重、重量 不能为空
        drDustinfor = dtDustinfor.Select("ITEM_ID in('000001827','000001945','000000130','000000220')");
        for (int i = 0; i < drDustinfor.Length; i++)
        {
            TMisMonitorDustattributeVo objDustattribute = new TMisMonitorDustattributeVo();
            objDustattribute.BASEINFOR_ID = drDustinfor[i]["ID"].ToString();
            DataTable dt = new TMisMonitorDustattributeLogic().SelectByTable(objDustattribute);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["FITER_BEGIN_WEIGHT"].ToString().Length == 0 && dt.Rows[j]["SAMPLE_CODE"].ToString() != "平均")
                {
                    isSuccess = false;
                    break;
                }
                if (dt.Rows[j]["FITER_AFTER_WEIGHT"].ToString().Length == 0 && dt.Rows[j]["SAMPLE_CODE"].ToString() != "平均")
                {
                    isSuccess = false;
                    break;
                }
                if (dt.Rows[j]["SAMPLE_WEIGHT"].ToString().Length == 0 && dt.Rows[j]["SAMPLE_CODE"].ToString() != "平均")
                {
                    isSuccess = false;
                    break;
                }
            }

            TMisMonitorDustattributePmVo objDustattributePm = new TMisMonitorDustattributePmVo();
            objDustattributePm.BASEINFOR_ID = drDustinfor[i]["ID"].ToString();
            dt = new TMisMonitorDustattributePmLogic().SelectByTable(objDustattributePm);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["SAMPLE_FWEIGHT"].ToString().Length == 0 && dt.Rows[j]["FITER_CODE"].ToString() != "平均")
                {
                    isSuccess = false;
                    break;
                }
                if (dt.Rows[j]["SAMPLE_EWEIGHT"].ToString().Length == 0 && dt.Rows[j]["FITER_CODE"].ToString() != "平均")
                {
                    isSuccess = false;
                    break;
                }
                if (dt.Rows[j]["SAMPLE_WEIGHT"].ToString().Length == 0 && dt.Rows[j]["FITER_CODE"].ToString() != "平均")
                {
                    isSuccess = false;
                    break;
                }
            }
        }
        drDustinfor = dtDustinfor.Select("ITEM_ID not in('000001827','000001945','000000130','000000220')");
        for (int i = 0; i < drDustinfor.Length; i++)
        {
            TMisMonitorDustattributeVo objDustattribute = new TMisMonitorDustattributeVo();
            objDustattribute.BASEINFOR_ID = drDustinfor[i]["ID"].ToString();
            DataTable dt = new TMisMonitorDustattributeLogic().SelectByTable(objDustattribute);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["SMOKE_POTENCY"].ToString().Length == 0 && dt.Rows[j]["SAMPLE_CODE"].ToString() != "平均")
                {
                    isSuccess = false;
                    break;
                }
            }

            TMisMonitorDustattributePmVo objDustattributePm = new TMisMonitorDustattributePmVo();
            objDustattributePm.BASEINFOR_ID = drDustinfor[i]["ID"].ToString();
            dt = new TMisMonitorDustattributePmLogic().SelectByTable(objDustattributePm);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["SAMPLE_CONCENT"].ToString().Length == 0 && dt.Rows[j]["FITER_CODE"].ToString() != "平均")
                {
                    isSuccess = false;
                    break;
                }
            }
        }

        return isSuccess == true ? "1" : "0";
    }

    /// <summary>
    /// 更新样品的前处理说明
    /// </summary>
    /// <param name="strValue"></param>
    /// <param name="strRemark"></param>
    /// <returns></returns>
    [WebMethod]
    public static string SaveRemark(string strValue, string strSampleRemark)
    {
        string result = "";
        TMisMonitorResultVo objItems = new TMisMonitorResultVo();
        objItems.ID = strValue;
        objItems.REMARK_2 = strSampleRemark;
        if (new TMisMonitorResultLogic().Edit(objItems))
        {
            result = "true";
        }
        return result;
    }

    /// <summary>
    /// 获取仪器数据
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetLims(string strValue)
    {
        bool isSuccess = true;

        string strConnection = ConfigurationManager.ConnectionStrings["Lims"].ToString();

        DataTable dt = new TMisMonitorResultLogic().getSimpleCodeInResult_QY(strValue, new PageBase().LogInfo.UserInfo.ID, "20");
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["ITEM_RESULT"].ToString().Length == 0)
            {
                string strResultId = dr["ID"].ToString();
                string strSampleCode = dr["SAMPLE_CODE"].ToString();
                string strApparatusCode = dr["APPARATUS_CODE"].ToString();
                string strItemId = dr["ITEM_ID"].ToString();
                string strUnitSrc = "mg/L";//暂时写死单位为mg/L
                string strItemName = "";//获取项目在仪器中对应的化学式，暂时写死
                if ("000000052|000000147|000000690|000000896|000000897|000001261|000001358|000001374|000001471|000001487|000001584|000001600|000001697|000001713|000001810".Contains(strItemId))
                    strItemName = "As4";
                if ("000000056|000000151|000000694|000000890|000000891|000001265|000001356|000001378|000001469|000001491|000001582|000001604|000001695|000001717|000001808".Contains(strItemId))
                    strItemName = "Cd";
                if ("000000059|000000142|000000158|000000697|000000884|000000886|000000887|000000888|000001268|000001354|000001381|000001467|000001494|000001580|000001607|000001693|000001720|000001806".Contains(strItemId))
                    strItemName = "Hg4";
                if ("000000073|000000711|000001282|000001395|000001508|000001621|000001734".Contains(strItemId))
                    strItemName = "TN";

                TMisMonitorTaskItemLogic objLogic = new TMisMonitorTaskItemLogic();
                string strSql = "select * from T_LIMS_APPARATUS_DATA where APPARATUS_CODE='{0}'";
                strSql = string.Format(strSql, strApparatusCode);
                DataTable dtTable = objLogic.SelectSQL_ByTable_forMobile(strSql, 0, 0);

                string strRe = "", strUnit = "", strSrcID = "",strTableName="";
                #region get Lims Data
                if (dtTable.Rows.Count > 0)
                {
                    DataRow drTable = dtTable.Rows[0];
                    strTableName = drTable["LIMS_TABLE"].ToString();
                    string strRltColName = drTable["LIMS_RESULT_COL"].ToString();
                    string strUnitColName = drTable["LIMS_UNIT_COL"].ToString();
                    string strSampleColName = drTable["LIMS_SAMPLE_COL"].ToString();
                    string strItemColName = drTable["LIMS_ITEM_COL"].ToString();

                    DataTable objTable = new DataTable();
                    string strLimsSql = "select * from " + strTableName + " where IF_ERR='0' and " + strSampleColName + " ='" + strSampleCode + "' and " + strItemColName + " = '" + strItemName + "'";
                    objTable = ExecuteDataTableEx(strLimsSql, strConnection);

                    if (objTable.Rows.Count > 0)
                    {
                        DataRow drResult = objTable.Rows[0];
                        strRe = drResult[strRltColName].ToString();
                        strUnit = drResult[strUnitColName].ToString();
                        strSrcID = drResult["ID"].ToString();
                    }
                }
                #endregion
                if (strRe.Length > 0)
                {
                    TMisMonitorResultVo objResult = new TMisMonitorResultVo();
                    objResult.ID = strResultId;
                    objResult.REMARK_4 = strSrcID;
                    objResult.REMARK_5 = strTableName;
                    objResult.REMARK_3 = "1";
                    if (strUnit == "ug/L" || strUnit == "μg/L")
                    {
                        if (strUnitSrc == "mg/L")
                        {
                            float fRe = float.Parse(strRe) / 1000;
                            objResult.ITEM_RESULT = fRe.ToString();
                        }
                    }
                    else
                    {
                        objResult.ITEM_RESULT = strRe;
                    }
                    new TMisMonitorResultLogic().Edit(objResult);
                }
            }
        }
        
        return isSuccess == true ? "1" : "0";
    }

    private static DataTable ExecuteDataTableEx(string strCommandText, string strConnectionStatic)
    {
        SqlCommand objCommand = new SqlCommand(strCommandText);
        objCommand.CommandType = CommandType.Text;
        SqlConnection objConnection = new SqlConnection(strConnectionStatic);
        SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
        DataTable objTable = new DataTable();

        try
        {
            PrepareCommand(objCommand, objConnection, null, CommandType.Text, strCommandText);

            objAdapter.Fill(objTable);
        }
        catch (Exception ex)
        {
            //Tips.AppendLine(ex.Message);
        }
        finally
        {
            objAdapter.Dispose();
            objCommand.Dispose();
            objConnection.Close();
            objConnection.Dispose();
        }

        return objTable;
    }

    /// <summary>
    /// 命令准备
    /// </summary>
    /// <param name="objCommand">命令</param>
    /// <param name="objConnection">连接</param>
    /// <param name="objTransaction">事务</param>
    /// <param name="strCommandType">命令类型</param>
    /// <param name="strCommandText">命令明细</param>
    private static void PrepareCommand(SqlCommand objCommand, SqlConnection objConnection, SqlTransaction objTransaction, CommandType strCommandType, string strCommandText)
    {
        if (objConnection.State != ConnectionState.Open)
        {
            objConnection.Open();
        }

        objCommand.Connection = objConnection;
        objCommand.CommandText = strCommandText;

        if (objTransaction != null)
        {
            objCommand.Transaction = objTransaction;
        }

        objCommand.CommandType = strCommandType;
    }

    public string getItemInfo(string strResultIDs)
    {
        DataTable dt = new TMisMonitorResultLogic().getItemInfoForQC(strResultIDs);

        string strJson = DataTableToJson(dt);
        return strJson;
    }
    /// <summary>
    /// 根据样品获取任务ID
    /// </summary>
    /// <param name="strQcId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getTaskID(string strSubtask)
    {
        string strTaskID = "";
        DataTable dt = new TMisMonitorResultLogic().getTaskID(strSubtask);
        if (dt.Rows.Count > 0)
        {
            strTaskID = dt.Rows[0]["TASK_ID"].ToString();
        }
        return strTaskID;
    }
    /// <summary>
    /// 获取监测项目
    /// </summary>
    /// <param name="strSubtask"></param>
    /// <returns></returns>
    [WebMethod]
    public static string getItemId(string strSubtask)
    {
        string strTaskID = "";
        DataTable dt = new TMisMonitorResultLogic().getItemId(strSubtask);
        if (dt.Rows.Count > 0)
        {
            strTaskID = dt.Rows[0]["ITEM_ID"].ToString();
        }
        return strTaskID;
    }
}