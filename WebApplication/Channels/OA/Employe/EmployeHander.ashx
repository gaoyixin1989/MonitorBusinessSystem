<%@ WebHandler Language="C#" Class="EmployeHander" %>

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Web.SessionState;
using i3.View;
using i3.ValueObject.Channels.OA.EMPLOYE;
using i3.BusinessLogic.Channels.OA.EMPLOYE;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Channels.OA.ATT;
using i3.BusinessLogic.Channels.OA.ATT;

public class EmployeHander : PageBase, IHttpHandler, IRequiresSessionState
{
    public string strMessage = "";
    public string strAction = "", strType = "", result = "";//执行方法，字典类别,返回值
    public DataTable dt = new DataTable();
    //排序信息
    public string strSortname = "", strSortorder = "";
    //页数 每页记录数
    public int intPageIndex = 0, intPageSize = 0;
    //业务处理变量
    public string strUserName="",strUserID="", strEmployeID = "", strEmployeCode = "", strEmployeName = "", strIdCard = "", strSex = "", strBirthday = "", strAge = "", strNation = "", strPoliticalStatus = "", strEduLevel = "",
        strDepart = "", strPost = "", strPostion = "", strPostLevel = "", strEmployeType = "", strPostDate = "", strOrgType = "", strOrgDate = "", strEnterDate = "",
        strTechPost = "", strApplyDate = "", strGraduate = "", strGraduteDate = "", strSpec = "", strTechLevel = "", strSkillLevel = "", strPostStatus = "", strInfor = "";
    //业务处理变量---证书明细
    public string strEmployeQualID = "", strEmployeQualName = "", strEmployeQualCode = "", strEmployeQualIsDepart = "", strEmployeQualIsDate = "", strEmployeQualActiveDate = "";
    //业务处理变量--工作经历
    public string strEmployeWorkHistoryID = "", strEmployeWorkHistoryCompany = "", strEmployeWorkHistoryBeginDate = "", strEmployeWorkHistoryEndDate = "", strEmployeWorkHistoryPostion = "";
    //业绩成果
    public string strEmployeWorkResultID = "", strEmployeWorkResultContent = "", strEmployeWorkResultAccidents = "", strEmployeWorkResultAccidentDate = "", strEmployeWorkResultType = "";
    //考核历史及其附件
    public string strEmployeAttID = "", strAttID = "", strAttType = "", strEmployeExamID = "",strEX_YEAR="",strEX_INFO="";
    //培训履历
    public string strEmployeTrainID = "", strEmployeTrainAttName = "", strEmployeTrainAttUrl = "", strEmployeTrainAttResultl = "", strEmployeTrainAttBookNum = "", strEmployeTrainAttInfor = "";
    public string srhEmployeCode = "", srhEmployeName = "",srhDepart="",srhPostion = "",srhPostStatus="";
    public string strFileType = "";
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (String.IsNullOrEmpty(LogInfo.UserInfo.ID))
        {
            context.Response.Write("请勿尝试盗链,无效的会话，请先登陆！");
            context.Response.End();
            return;
        }
        GetRequestParme(context);
        if (!String.IsNullOrEmpty(strAction))
        {
            switch (strAction)
            {
                #region 员工档案
                //获取字典项
                case "GetDict":
                    context.Response.Write(GetDict());
                    context.Response.End();
                    break;
                //登录名检查
                case "GetIsExistLogName":
                    context.Response.Write(GetIsExistLogName());
                    context.Response.End();
                    break;
                //获取员工档案列表
                case "GetEmployeInfor":
                    context.Response.Write(GetEmployeInfor());
                    context.Response.End();
                    break;
                    //新增/修改员工档案
                case "SaveEmployeInfor":
                    context.Response.Write(SaveEmployeInfor());
                    context.Response.End();
                    break;
                    //删除员工档案
                case "DelEmployeData":
                    context.Response.Write(DelEmployeData());
                    context.Response.End();
                    break;
                    //验证员工档案编号是否重复
                case "IsExistData":
                    context.Response.Write(IsExistData());
                    context.Response.End();
                    break;
                #endregion
                #region 员工证书
                //员工证书信息
                case "GetEmployeQualDetail":
                    context.Response.Write(GetEmployeQualDetail());
                    context.Response.End();
                    break;
                //新增/修改证书信息
                case "SaveEmployeQualDetail":
                    context.Response.Write(SaveEmployeQualDetail());
                    context.Response.End();
                    break;
                //删除员工证书信息
                case "DelEmployeQualData":
                    context.Response.Write(DelEmployeQualData());
                    context.Response.End();
                    break;
                #endregion
                #region 工作经历
                //员工工作经历信息
                case "GetEmployeWorkHistoryDetail":
                    context.Response.Write(GetEmployeWorkHistoryDetail());
                    context.Response.End();
                    break;
                //新增/修改工作经历
                case "SaveEmployeWorkHistoryDetail":
                    context.Response.Write(SaveEmployeWorkHistoryDetail());
                    context.Response.End();
                    break;
                //删除工作经历
                case "DelEmployeWorkHistoryData":
                    context.Response.Write(DelEmployeWorkHistoryData());
                    context.Response.End();
                    break;
                #endregion
                #region 工作成果与事故
                //员工工作成果与事故
                case "GetEmployeWorkResultDetail":
                    context.Response.Write(GetEmployeWorkResultDetail());
                    context.Response.End();
                    break;
                //新增/修改工作成果与事故
                case "SaveEmployeWorkResultDetail":
                    context.Response.Write(SaveEmployeWorkResultDetail());
                    context.Response.End();
                    break;
                //删除工作成果与事故
                case "DelEmployeWorkResultData":
                    context.Response.Write(DelEmployeWorkResultData());
                    context.Response.End();
                    break;
                 #endregion
                #region 员工考核
                //员工考核信息
                case "GetEmployeExDetail":
                    context.Response.Write(GetEmployeExDetail());
                    context.Response.End();
                    break;
                //新增/修改证书信息
                case "SaveEmployeExDetail":
                    context.Response.Write(SaveEmployeExDetail());
                    context.Response.End();
                    break;
                //获取员工考核附件
                case "GetAttFiles":
                    context.Response.Write(GetAttFiles());
                    context.Response.End();
                    break;
                //保存员工考核附件基本信息
                case "SaveExamHistoryData":
                    context.Response.Write(SaveExamHistoryData());
                    context.Response.End();
                    break;
                //删除员工考核历史记录，考核附件被删除时考核历史记录也被删除，用户没做上传操作时，关闭后会删除已生成的考核历史记录ID
                case "DelExamHistoryData":
                    context.Response.Write(DelExamHistoryData());
                    context.Response.End();
                    break;
                //删除员工考核附件
                case "DelAttFiles":
                    context.Response.Write(DelAttFiles());
                    context.Response.End();
                    break;
                #endregion
                #region 工作经历
                //员工工作经历信息
                case "GetEmployeTrainHistoryDetail":
                    context.Response.Write(GetEmployeTrainHistoryDetail());
                    context.Response.End();
                    break;
                //新增/修改工作经历
                case "SaveEmployeTrainHistoryDetail":
                    context.Response.Write(SaveEmployeTrainHistoryDetail());
                    context.Response.End();
                    break;
                //删除工作经历
                case "DelEmployeTrainHistoryData":
                    context.Response.Write(DelEmployeTrainHistoryData());
                    context.Response.End();
                    break;
                #endregion
                default:
                    break;
            }
        }
    }

    #region 员工档案
    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string GetDict( )
    {
        return i3.View.PageBase.getDictJsonString(strType);
    }

    /// <summary>
    /// 检索是否存在该登录名
    /// </summary>
    /// <returns></returns>
    private string GetIsExistLogName() {
        result = "";
        DataTable dt = new DataTable();
        TSysUserVo objitem = new TSysUserVo();
        if (!String.IsNullOrEmpty(strUserName))
        {
            objitem.USER_NAME = strUserName;
        }
        if (!String.IsNullOrEmpty(strUserID)) {
            objitem.ID = strUserID;
        }
        objitem.IS_DEL="0";
        objitem.IS_HIDE="0";
        objitem.IS_USE="1";
        dt = new TSysUserLogic().SelectByTable(objitem);
        if (dt.Rows.Count > 0) {
            result = LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }
    /// <summary>
    /// 获取员工档案列表
    /// </summary>
    /// <returns></returns>
    private string GetEmployeInfor() {
        result = "";
        int CountNum = 0;
        dt = new DataTable();
        TOaEmployeInfoVo objItems = new TOaEmployeInfoVo();
        if (!String.IsNullOrEmpty(strEmployeID)) {
            objItems.ID = strEmployeID;
        }
        //自定义查询
        if (!String.IsNullOrEmpty(srhEmployeCode) || !String.IsNullOrEmpty(srhEmployeName) || !String.IsNullOrEmpty(srhDepart) || !String.IsNullOrEmpty(srhPostion) || !String.IsNullOrEmpty(srhPostStatus))
        {
            objItems.EMPLOYE_CODE = srhEmployeCode;
            objItems.EMPLOYE_NAME = srhEmployeName;
            objItems.DEPART = srhDepart;
            objItems.POSITION = srhPostion;
            objItems.POST_STATUS = srhPostStatus;
            dt = new TOaEmployeInfoLogic().SelectDefineByTable(objItems, intPageIndex, intPageSize);
            CountNum = new TOaEmployeInfoLogic().SelectDefineBytResult(objItems);
        }
        else//数据初始化或非查询
        {
            dt = new TOaEmployeInfoLogic().SelectByTable(objItems, intPageIndex, intPageSize);
            CountNum = new TOaEmployeInfoLogic().GetSelectResultCount(objItems);
        }
        result = LigerGridDataToJson(dt,CountNum);
        
        return result;
    }

    /// <summary>
    /// 新增/修改员工档案数据
    /// </summary>
    /// <returns></returns>
    private string SaveEmployeInfor() {
        result = "";
        TOaEmployeInfoVo objItems = new TOaEmployeInfoVo();
        objItems.APPLY_DATE = strApplyDate;
        objItems.BIRTHDAY = strBirthday;

        objItems.DEPART = strDepart;

        objItems.USER_ID = strUserID;
        objItems.EMPLOYE_CODE = strEmployeCode;
        objItems.EMPLOYE_NAME = strEmployeName;
        objItems.EMPLOYE_TYPE = strEmployeType;
        objItems.ENTRYDATE = strEnterDate;
        objItems.EDUCATIONLEVEL = strEduLevel;

        objItems.GRADUATE = strGraduate;
        objItems.GRADUATE_DATE = strGraduteDate;
        objItems.ID_CARD = strIdCard;
        objItems.NATION = strNation;
        objItems.ORGANIZATION_DATE = strOrgDate;
        objItems.ORGANIZATION_TYPE = strOrgType;
        objItems.POLITICALSTATUS = strPoliticalStatus;

        objItems.POST = strPost;
        objItems.POSITION = strPostion;
        objItems.POST_DATE = strPostDate;
        objItems.POST_LEVEL = strPostLevel;
        objItems.POST_STATUS = strPostStatus;

        objItems.SEX = strSex;
        objItems.SKILL_LEVEL = strSkillLevel;
        objItems.SPECIALITY = strSpec;
        objItems.TECHNOLOGY_LEVEL = strTechLevel;
        objItems.TECHNOLOGY_POST = strTechPost;
        objItems.INFO = strInfor;

        if (String.IsNullOrEmpty(strEmployeID))
        {
            objItems.ID = GetSerialNumber("t_oa_EmployeID");
            if (new TOaEmployeInfoLogic().Create(objItems))
            {
                result = "true";

                strMessage = LogInfo.UserInfo.USER_NAME + "新增用户档案信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddEmployeInfo, "", strMessage);
            }
        }
        else {
            objItems.ID = strEmployeID;
            if (new TOaEmployeInfoLogic().Edit(objItems))
            {
                result = "true";
                strMessage = LogInfo.UserInfo.USER_NAME + "编辑用户档案信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.EditEmployeInfo, "", strMessage);
            }
        }

        return result;
    }

   /// <summary>
   /// 是否存在相同编号的员工档案
   /// </summary>
   /// <returns></returns>
    private string IsExistData() {
        result = "";
        TOaEmployeInfoVo objItems = new TOaEmployeInfoVo();
        objItems.EMPLOYE_CODE = strEmployeCode;

        if (new TOaEmployeInfoLogic().IsExist(objItems)) {
            result = "true";
        }
        return result;
    }

    /// <summary>
    /// 删除员工档案
    /// </summary>
    /// <returns></returns>
    private string DelEmployeData() {
        result = "";
        TOaEmployeInfoVo objItems = new TOaEmployeInfoVo();
        objItems.ID = strEmployeID;
        if (new TOaEmployeInfoLogic().Delete(objItems)) {
            result = "true";

            strMessage = LogInfo.UserInfo.USER_NAME + "删除用户档案信息" + objItems.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.DelEmployeInfo, "", strMessage);
        }

        return result;
    }
    #endregion

    #region 资格证书
    /// <summary>
    /// 获取员工档案资格证书明细
    /// </summary>
    /// <returns></returns>
    private string GetEmployeQualDetail() {
        result = "";
        dt = new DataTable();
        TOaEmployeQualificationVo objItems = new TOaEmployeQualificationVo();
        if (!String.IsNullOrEmpty(strEmployeID))
        {
            objItems.EMPLOYEID = strEmployeID;
            if (!String.IsNullOrEmpty(strEmployeQualID))
            {
                objItems.ID = strEmployeQualID;
            }
            dt = new TOaEmployeQualificationLogic().SelectByUnionAttTable(objItems,strFileType);
            result = LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 新增/编辑证书明细
    /// </summary>
    /// <returns></returns>
    private string SaveEmployeQualDetail() {
        result = "";
        TOaEmployeQualificationVo objItems = new TOaEmployeQualificationVo();
        objItems.EMPLOYEID = strEmployeID;
        objItems.CERTITICATECODE = strEmployeQualCode;
        objItems.CERTITICATENAME = strEmployeQualName;
        objItems.ACTIVEDATE = strEmployeQualActiveDate;
        objItems.ISSUINDATE = strEmployeQualIsDate;
        objItems.ISSUINGAUTHO = strEmployeQualIsDepart;
        if (String.IsNullOrEmpty(strEmployeQualID))
        {
            objItems.ID = GetSerialNumber("t_oa_EmployeQuaID");

            if (new TOaEmployeQualificationLogic().Create(objItems))
            {
                result = "true";

                strMessage = LogInfo.UserInfo.USER_NAME + "新增证书信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddEmployeQual, "", strMessage);
            }
        }
        else {
            objItems.ID = strEmployeQualID;
            if (new TOaEmployeQualificationLogic().Edit(objItems)) {
                result = "true";
                strMessage = LogInfo.UserInfo.USER_NAME + "编辑证书信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.EditEmployeQual, "", strMessage);
            }
        }
        return result;
    }
    /// <summary>
    /// 删除员工证书信息
    /// </summary>
    /// <returns></returns>
    private string DelEmployeQualData() {
        result = "";
        TOaEmployeQualificationVo objItems = new TOaEmployeQualificationVo();
        objItems.ID = strEmployeQualID;
        if (new TOaEmployeQualificationLogic().Delete(objItems))
        {
            result = "true";
            strMessage = LogInfo.UserInfo.USER_NAME + "删除证书信息" + objItems.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.DelEmployeQual, "", strMessage);
        }

        return result;
    }
    #endregion

#region 工作履历
    /// <summary>
    /// 获取员工档案工作履历明细
    /// </summary>
    /// <returns></returns>
    private string GetEmployeWorkHistoryDetail()
    {
        result = "";
        dt = new DataTable();
        TOaEmployeWorkhistoryVo objItems = new TOaEmployeWorkhistoryVo();
        if (!String.IsNullOrEmpty(strEmployeID))
        {
            objItems.EMPLOYEID = strEmployeID;
            if (!String.IsNullOrEmpty(strEmployeWorkHistoryID))
            {
                objItems.ID = strEmployeWorkHistoryID;
            }
            dt = new TOaEmployeWorkhistoryLogic().SelectByTable(objItems);
            result = LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 新增/编辑工作履历明细
    /// </summary>
    /// <returns></returns>
    private string SaveEmployeWorkHistoryDetail()
    {
        result = "";
        TOaEmployeWorkhistoryVo objItems = new TOaEmployeWorkhistoryVo();
        objItems.EMPLOYEID = strEmployeID;
        objItems.WORKCOMPANY = strEmployeWorkHistoryCompany;
        objItems.POSITION = strEmployeWorkHistoryPostion;
        objItems.WORKBEGINDATE = strEmployeWorkHistoryBeginDate;
        objItems.WORKENDDATE = strEmployeWorkHistoryEndDate;
        if (String.IsNullOrEmpty(strEmployeWorkHistoryID))
        {
            objItems.ID = GetSerialNumber("t_oa_EmployeWorkID");

            if (new TOaEmployeWorkhistoryLogic().Create(objItems))
            {
                result = "true";

                strMessage = LogInfo.UserInfo.USER_NAME + "新增工作经历信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddEmployeWorkHistory, "", strMessage);
            }
        }
        else
        {
            objItems.ID = strEmployeWorkHistoryID;
            if (new TOaEmployeWorkhistoryLogic().Edit(objItems))
            {
                result = "true";
                strMessage = LogInfo.UserInfo.USER_NAME + "编辑工作经历信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.EditEmployeWorkHistory, "", strMessage);
            }
        }
        return result;
    }
    /// <summary>
    /// 删除员工履历信息
    /// </summary>
    /// <returns></returns>
    private string DelEmployeWorkHistoryData()
    {
        result = "";
        TOaEmployeWorkhistoryVo objItems = new TOaEmployeWorkhistoryVo();
        objItems.ID = strEmployeWorkHistoryID;
        if (new TOaEmployeWorkhistoryLogic().Delete(objItems))
        {
            result = "true";
            strMessage = LogInfo.UserInfo.USER_NAME + "删除工作经历信息" + objItems.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.DelEmployeWorkHistory, "", strMessage);
        }

        return result;
    }
#endregion

#region 工作成果
    /// <summary>
    /// 获取员工档案工作成果明细
    /// </summary>
    /// <returns></returns>
    private string GetEmployeWorkResultDetail()
    {
        result = "";
        dt = new DataTable();
        TOaEmployeResultorfaultVo objItems = new TOaEmployeResultorfaultVo();
        if (!String.IsNullOrEmpty(strEmployeID))
        {
            objItems.EMPLOYEID = strEmployeID;
            if (!String.IsNullOrEmpty(strEmployeWorkResultID))
            {
                objItems.ID = strEmployeWorkResultID;
            }
            if (!String.IsNullOrEmpty(strEmployeWorkResultType))
            {
                objItems.RESULT_OR_ACCIDENT = strEmployeWorkResultType;
            }
            dt = new TOaEmployeResultorfaultLogic().SelectByWorkResultTable(objItems,strFileType);
            result = LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 新增/编辑工作成果明细
    /// </summary>
    /// <returns></returns>
    private string SaveEmployeWorkResultDetail()
    {
        result = "";
        TOaEmployeResultorfaultVo objItems = new TOaEmployeResultorfaultVo();
        objItems.EMPLOYEID = strEmployeID;
        if (strEmployeWorkResultType == "1") {
            objItems.WORKRESULT = strEmployeWorkResultContent;
            objItems.RESULT_OR_ACCIDENT = "1";
        }
        if(strEmployeWorkResultType=="2"){
            objItems.ACCIDENTS = strEmployeWorkResultAccidents;
            objItems.RESULT_OR_ACCIDENT = "2";
            objItems.ACCIDENTHAPPENDATE = strEmployeWorkResultAccidentDate;
        }
        if (String.IsNullOrEmpty(strEmployeWorkResultID))
        {
            objItems.ID = GetSerialNumber("t_oa_EmployeWorkResultID");

            if (new TOaEmployeResultorfaultLogic().Create(objItems))
            {
                result = "true";

                strMessage = LogInfo.UserInfo.USER_NAME + "新增工作成果(事故)信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddEmployeWorkResult, "", strMessage);
            }
        }
        else
        {
            objItems.ID = strEmployeWorkResultID;
            if (new TOaEmployeResultorfaultLogic().Edit(objItems))
            {
                result = "true";
                strMessage = LogInfo.UserInfo.USER_NAME + "编辑工作成果(事故)信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.EditEmployeWorkResult, "", strMessage);
            }
        }
        return result;
    }
    /// <summary>
    /// 删除员工履历信息
    /// </summary>
    /// <returns></returns>
    private string DelEmployeWorkResultData()
    {
        result = "";
        TOaEmployeResultorfaultVo objItems = new TOaEmployeResultorfaultVo();
        objItems.ID = strEmployeWorkResultID;
        if (new TOaEmployeResultorfaultLogic().Delete(objItems))
        {
            result = "true";
            strMessage = LogInfo.UserInfo.USER_NAME + "删除工作成果(事故)信息" + objItems.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.DelEmployeWorkResult, "", strMessage);
        }

        return result;
    }
#endregion 

#region 员工考核
    /// <summary>
    /// 获取员工考核明细
    /// </summary>
    /// <returns></returns>
    private string GetEmployeExDetail() {
        result = "";
        dt = new DataTable();
        TOaEmployeExaminehistoryVo objItems = new TOaEmployeExaminehistoryVo();
        if (!String.IsNullOrEmpty(strEmployeID))
        {
            objItems.EMPLOYEID = strEmployeID;
            if (!String.IsNullOrEmpty(strEmployeExamID))
            {
                objItems.ID = strEmployeExamID;
            }
            dt = new TOaEmployeExaminehistoryLogic().SelectByUnionAttTable(objItems,strFileType);
            result = LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 新增/编辑考核明细
    /// </summary>
    /// <returns></returns>
    private string SaveEmployeExDetail() {
        result = "";
        TOaEmployeExaminehistoryVo objItems = new TOaEmployeExaminehistoryVo();
        objItems.EMPLOYEID = strEmployeID;
        objItems.EX_YEAR = strEX_YEAR;
        objItems.EX_INFO = strEX_INFO;

        if (String.IsNullOrEmpty(strEmployeExamID))
        {
            objItems.ID = GetSerialNumber("t_oa_EmployeHistoryID");

            if (new TOaEmployeExaminehistoryLogic().Create(objItems))
            {
                result = "true";

                strMessage = LogInfo.UserInfo.USER_NAME + "新增考核信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddEmployeExam, "", strMessage);
            }
        }
        else {
            objItems.ID = strEmployeExamID;
            if (new TOaEmployeExaminehistoryLogic().Edit(objItems)) {
                result = "true";
                strMessage = LogInfo.UserInfo.USER_NAME + "编辑考核信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddEmployeExam, "", strMessage);
            }
        }
        return result;
    }
    
    /// <summary>
    /// 获取员工考核附件
    /// </summary>
    /// <returns></returns>
    private string GetAttFiles() {
        result = "";
        dt = new DataTable();
        TOaAttVo objItems = new TOaAttVo();
        objItems.BUSINESS_TYPE = strFileType;
        
        objItems.REMARKS = strEmployeID;

        dt = new TOaAttLogic().SelectUnionEmployeTable(objItems);
        result = LigerGridDataToJson(dt, dt.Rows.Count);

        return result;
    }

    /// <summary>
    /// 附加上传之前先保存一次考核历史记录，返回该历史记录信息作为附件业务ID
    /// </summary>
    /// <returns></returns>
    private string SaveExamHistoryData() {
        result = "";
        dt = new DataTable();
        TOaEmployeExaminehistoryVo objitems = new TOaEmployeExaminehistoryVo();
        objitems.EMPLOYEID = strEmployeID;

        objitems.ID = GetSerialNumber("t_oa_EmployeHistoryID");
        if (new TOaEmployeExaminehistoryLogic().Create(objitems))
        {
            result = objitems.ID;
            strMessage = LogInfo.UserInfo.USER_NAME + "新增考核历史信息" + objitems.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.AddEmployeExam, "", strMessage);
        }

        return result;
    }

    /// <summary>
    /// 考核附件删除后删除考核历史
    /// </summary>
    /// <returns></returns>
    private string DelExamHistoryData()
    {
        result = "";
        dt = new DataTable();
        TOaEmployeExaminehistoryVo objitems = new TOaEmployeExaminehistoryVo();
        objitems.ID = strEmployeExamID;
        if (new TOaEmployeExaminehistoryLogic().Delete(objitems))
        {
            result = "true";

            strMessage = LogInfo.UserInfo.USER_NAME + "删除考核信息" + objitems.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.DelEmployeExam, "", strMessage);
        }

        return result;
    }
    /// <summary>
    /// 删除指定的考核附件
    /// </summary>
    /// <returns></returns>
    private string DelAttFiles() {
        result = "";
        //获取主文件路径
        string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
        TOaAttVo TOaAttVo = new TOaAttVo();
        TOaAttVo.ID = strAttID;
        //获取路径信息
        DataTable objTable = new TOaAttLogic().SelectByTable(TOaAttVo);
        if (objTable.Rows.Count > 0)
        {
            //获取该记录的ID
            string strId = objTable.Rows[0]["ID"].ToString();
            //获取原来文件的路径
            string strOldFilePath = objTable.Rows[0]["UPLOAD_PATH"].ToString();
            //如果存在的话，删除原来的文件
            if (File.Exists(mastPath + "\\" + strOldFilePath))
                File.Delete(mastPath + "\\" + strOldFilePath);
            //删除数据库信息
            if (new TOaAttLogic().Delete(TOaAttVo))
            {
                //删除考核历史记录信息
                strEmployeExamID = objTable.Rows[0]["BUSINESS_ID"].ToString();
                if (DelExamHistoryData()=="true")
                {
                    result = "true";
                }

                strMessage = LogInfo.UserInfo.USER_NAME + "删除附件信息" + TOaAttVo.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.DelAtt, "", strMessage);
            }
        }

        return result;

    }
#endregion

#region 培训履历
    /// <summary>
    /// 获取员工档案培训履历明细
    /// </summary>
    /// <returns></returns>
    private string GetEmployeTrainHistoryDetail()
    {
        result = "";
        dt = new DataTable();
        TOaEmployeTrainhistoryVo objItems = new TOaEmployeTrainhistoryVo();
        if (!String.IsNullOrEmpty(strEmployeID))
        {
            objItems.EMPLOYEID = strEmployeID;
            
            if (!String.IsNullOrEmpty(strEmployeTrainID))
            {
                objItems.ID = strEmployeTrainID;
            }
            dt = new TOaEmployeTrainhistoryLogic().SelectByTrainAttTable(objItems,strFileType);
            result = LigerGridDataToJson(dt, dt.Rows.Count);
        }
        return result;
    }

    /// <summary>
    /// 新增/编辑培训履历明细
    /// </summary>
    /// <returns></returns>
    private string SaveEmployeTrainHistoryDetail()
    {
        result = "";
        TOaEmployeTrainhistoryVo objItems = new TOaEmployeTrainhistoryVo();
        objItems.EMPLOYEID = strEmployeID;
        objItems.ATT_NAME = strEmployeTrainAttName;
        objItems.ATT_URL = strEmployeTrainAttUrl;
        objItems.ATT_INFO = strEmployeTrainAttInfor;
        objItems.BOOK_NUM = strEmployeTrainAttBookNum;
        objItems.TRAIN_RESULT = strEmployeTrainAttResultl;
        if (String.IsNullOrEmpty(strEmployeTrainID))
        {
            objItems.ID = GetSerialNumber("t_oa_EmployeTranID");

            if (new TOaEmployeTrainhistoryLogic().Create(objItems))
            {
                result = "true";

                strMessage = LogInfo.UserInfo.USER_NAME + "新增培训经历信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.AddEmployeTrain, "", strMessage);
            }
        }
        else
        {
            objItems.ID = strEmployeTrainID;
            if (new TOaEmployeTrainhistoryLogic().Edit(objItems))
            {
                result = "true";
                strMessage = LogInfo.UserInfo.USER_NAME + "编辑培训经历信息" + objItems.ID + "成功";
                WriteLog(i3.ValueObject.ObjectBase.LogType.EditEmployeTrain, "", strMessage);
            }
        }
        return result;
    }
    /// <summary>
    /// 删除员工履历信息
    /// </summary>
    /// <returns></returns>
    private string DelEmployeTrainHistoryData()
    {
        result = "";
        TOaEmployeTrainhistoryVo objItems = new TOaEmployeTrainhistoryVo();
        objItems.ID = strEmployeTrainID;
        if (new TOaEmployeTrainhistoryLogic().Delete(objItems))
        {
            result = "true";
            strMessage = LogInfo.UserInfo.USER_NAME + "删除培训经历信息" + objItems.ID + "成功";
            WriteLog(i3.ValueObject.ObjectBase.LogType.DelEmployeTrain, "", strMessage);
        }

        return result;
    }
    #endregion

#region  获取Url传参
  /// <summary>
    /// 获取Url传参
    /// </summary>
    /// <param name="context"></param>
    private void GetRequestParme(HttpContext context)
    {
        //排序信息
        if (!String.IsNullOrEmpty(context.Request.Params["sortname"]))
        {
            strSortname = context.Request["sortname"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["sortorder"]))
        {
            strSortorder = context.Request.Params["sortorder"].Trim();
        }
        //当前页面
        if (!String.IsNullOrEmpty(context.Request.Params["page"]))
        {
            intPageIndex = Convert.ToInt32(context.Request.Params["page"].Trim());
        }
        //每页记录数
        if (!String.IsNullOrEmpty(context.Request.Params["pagesize"]))
        {
            intPageSize = Convert.ToInt32(context.Request.Params["pagesize"].Trim());
        }
        //方法
        if (!String.IsNullOrEmpty(context.Request.Params["action"]))
        {
            strAction = context.Request.Params["action"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["type"]))
        {
            strType = context.Request.Params["type"].Trim();
        }
        //业务处理参数
        if (!String.IsNullOrEmpty(context.Request.Params["strUserName"]))
        {
            strUserName = context.Request.Params["strUserName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strUserID"]))
        {
            strUserID = context.Request.Params["strUserID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeID"]))
        {
            strEmployeID = context.Request.Params["strEmployeID"].Trim();
        }
        
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeCode"]))
        {
            strEmployeCode = context.Request.Params["strEmployeCode"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeName"]))
        {
            strEmployeName = context.Request.Params["strEmployeName"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeType"]))
        {
            strEmployeType = context.Request.Params["strEmployeType"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strIdCard"]))
        {
            strIdCard = context.Request.Params["strIdCard"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strSex"]))
        {
            strSex = context.Request.Params["strSex"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strBirthday"]))
        {
            strBirthday = context.Request.Params["strBirthday"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strAge"]))
        {
            strAge = context.Request.Params["strAge"].Trim();
        }


        if (!String.IsNullOrEmpty(context.Request.Params["strNation"]))
        {
            strNation = context.Request.Params["strNation"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strPoliticalStatus"]))
        {
            strPoliticalStatus = context.Request.Params["strPoliticalStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEduLevel"]))
        {
            strEduLevel = context.Request.Params["strEduLevel"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strDepart"]))
        {
            strDepart = context.Request.Params["strDepart"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strPost"]))
        {
            strPost = context.Request.Params["strPost"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strPostDate"]))
        {
            strPostDate = context.Request.Params["strPostDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPostion"]))
        {
            strPostion = context.Request.Params["strPostion"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strPostLevel"]))
        {
            strPostLevel = context.Request.Params["strPostLevel"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPostStatus"]))
        {
            strPostStatus = context.Request.Params["strPostStatus"].Trim();
        }


        if (!String.IsNullOrEmpty(context.Request.Params["strOrgType"]))
        {
            strOrgType = context.Request.Params["strOrgType"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strOrgDate"]))
        {
            strOrgDate = context.Request.Params["strOrgDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strGraduate"]))
        {
            strGraduate = context.Request.Params["strGraduate"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strGraduteDate"]))
        {
            strGraduteDate = context.Request.Params["strGraduteDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEnterDate"]))
        {
            strEnterDate = context.Request.Params["strEnterDate"].Trim();
        }


        if (!String.IsNullOrEmpty(context.Request.Params["strTechPost"]))
        {
            strTechPost = context.Request.Params["strTechPost"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTechLevel"]))
        {
            strTechLevel = context.Request.Params["strTechLevel"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strApplyDate"]))
        {
            strApplyDate = context.Request.Params["strApplyDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strGraduate"]))
        {
            strGraduate = context.Request.Params["strGraduate"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strGraduteDate"]))
        {
            strGraduteDate = context.Request.Params["strGraduteDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strSpec"]))
        {
            strSpec = context.Request.Params["strSpec"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strTechPost"]))
        {
            strTechPost = context.Request.Params["strTechPost"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strTechLevel"]))
        {
            strTechLevel = context.Request.Params["strTechLevel"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strSkillLevel"]))
        {
            strSkillLevel = context.Request.Params["strSkillLevel"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strInfor"]))
        {
            strInfor = context.Request.Params["strInfor"].Trim();
        }
        //证书信息参数
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeQualID"]))
        {
            strEmployeQualID = context.Request.Params["strEmployeQualID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeQualCode"]))
        {
            strEmployeQualCode = context.Request.Params["strEmployeQualCode"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeQualName"]))
        {
            strEmployeQualName = context.Request.Params["strEmployeQualName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeQualIsDepart"]))
        {
            strEmployeQualIsDepart = context.Request.Params["strEmployeQualIsDepart"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeQualIsDate"]))
        {
            strEmployeQualIsDate = context.Request.Params["strEmployeQualIsDate"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeQualActiveDate"]))
        {
            strEmployeQualActiveDate = context.Request.Params["strEmployeQualActiveDate"].Trim();
        }
        //工作经历 
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeWorkHistoryID"]))
        {
            strEmployeWorkHistoryID = context.Request.Params["strEmployeWorkHistoryID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeWorkHistoryCompany"]))
        {
            strEmployeWorkHistoryCompany = context.Request.Params["strEmployeWorkHistoryCompany"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeWorkHistoryPostion"]))
        {
            strEmployeWorkHistoryPostion = context.Request.Params["strEmployeWorkHistoryPostion"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeWorkHistoryBeginDate"]))
        {
            strEmployeWorkHistoryBeginDate = context.Request.Params["strEmployeWorkHistoryBeginDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeWorkHistoryEndDate"]))
        {
            strEmployeWorkHistoryEndDate = context.Request.Params["strEmployeWorkHistoryEndDate"].Trim();
        }
        //工作成果相关
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeWorkResultID"]))
        {
            strEmployeWorkResultID = context.Request.Params["strEmployeWorkResultID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeWorkResultContent"]))
        {
            strEmployeWorkResultContent = context.Request.Params["strEmployeWorkResultContent"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeWorkResultAccidents"]))
        {
            strEmployeWorkResultAccidents = context.Request.Params["strEmployeWorkResultAccidents"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeWorkResultAccidentDate"]))
        {
            strEmployeWorkResultAccidentDate = context.Request.Params["strEmployeWorkResultAccidentDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeWorkResultType"]))
        {
            strEmployeWorkResultType = context.Request.Params["strEmployeWorkResultType"].Trim();
        }
        //考核历史记录 
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeAttID"]))
        {
            strEmployeAttID = context.Request.Params["strEmployeAttID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAttID"]))
        {
            strAttID = context.Request.Params["strAttID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAttType"]))
        {
            strAttType = context.Request.Params["strAttType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeExamID"]))
        {
            strEmployeExamID = context.Request.Params["strEmployeExamID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEX_YEAR"]))
        {
            strEX_YEAR = context.Request.Params["strEX_YEAR"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEX_INFO"]))
        {
            strEX_INFO = context.Request.Params["strEX_INFO"].Trim();
        }
        //培训履历
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeTrainID"]))
        {
            strEmployeTrainID = context.Request.Params["strEmployeTrainID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeTrainAttName"]))
        {
            strEmployeTrainAttName = context.Request.Params["strEmployeTrainAttName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeTrainAttUrl"]))
        {
            strEmployeTrainAttUrl = context.Request.Params["strEmployeTrainAttUrl"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeTrainAttResultl"]))
        {
            strEmployeTrainAttResultl = context.Request.Params["strEmployeTrainAttResultl"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeTrainAttBookNum"]))
        {
            strEmployeTrainAttBookNum = context.Request.Params["strEmployeTrainAttBookNum"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEmployeTrainAttInfor"]))
        {
            strEmployeTrainAttInfor = context.Request.Params["strEmployeTrainAttInfor"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["srhEmployeCode"]))
        {
            srhEmployeCode = context.Request.Params["srhEmployeCode"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["srhEmployeName"]))
        {
            srhEmployeName = context.Request.Params["srhEmployeName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["srhDepart"]))
        {
            srhDepart = context.Request.Params["srhDepart"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["srhPostion"]))
        {
            srhPostion = context.Request.Params["srhPostion"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["srhPostStatus"]))
        {
            srhPostStatus = context.Request.Params["srhPostStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strFileType"]))
        {
            strFileType = context.Request.Params["strFileType"].Trim();
        }
    }
#endregion
    public bool IsReusable {
        get {
            return false;
        }
    }

}