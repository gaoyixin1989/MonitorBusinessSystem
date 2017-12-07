<%@ WebHandler Language="C#" Class="PartHandler" %>

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Web.SessionState;
using System.Configuration;
using i3.View;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;
using i3.ValueObject.Sys.WF;
using i3.BusinessLogic.Sys.WF;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
/// <summary>
/// 创建人：胡方扬
/// 创建时间：2012-01-28
/// 原因：采购计划后台处理方法
/// </summary>
public class PartHandler :  PageBase, IHttpHandler, IRequiresSessionState {
       //系统用户信息
    public string strUserID = "";
    public string strMessage = "";
    public string strAction = "", strType = "", result = "";//执行方法，字典类别,返回值
    public DataTable dt = new DataTable();
    //排序信息
    public string strSortname = "", strSortorder = "";
    //页数 每页记录数
    public int intPageIndex = 0, intPageSize = 0;
    public string strtaskID = "", strStatus = "", strPartId = "", strPartPlanId = "", strDept = "", strDate = "", strBt = "", strPartCode = "", strPartName = "", strNeedQuatity = "", strDevDate = "", strBudgetMoney = "", strUserDo = "", strPartAcceptId = "", strPartAcceptLstId = "", strReqStatus = "", strPrice = "", strAmount = "", strEnterpriseName = "", strReciveDate = "", strCheckDate = "", strCheckUserID = "", strCheckResult = "", strRemark = "", strUserName = "", strPartCollarId = "", strWarnValue = "", strReal_Name = "";
    public string strBeginDate = "", strEndDate = "", strGetType = "", strREMARK1 = "", strMyPartAcceptedID = "", SEA_NAME = "", StartTime = "", EndTime = "", strApplyType = "";
    public string type = "";//验收标志位
    public string keshi = "";
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
                //获取申请列表
                case "GetPartPushViewList":
                    context.Response.Write(GetPartPushViewList());
                    context.Response.End();
                    break;
                    //获取当前登录用户信息
                case "GetLoginUserInfor":
                    context.Response.Write(GetLoginUserInfor());
                    context.Response.End();
                    break;
                    //获取指定用户信息
                case "GetUserInfor":
                    context.Response.Write(GetUserInfor());
                    context.Response.End();
                    break;
                //获取物料信息
                case "GetPartList":
                    context.Response.Write(GetPartList());
                    context.Response.End();
                    break;
                    //获取出入库明细
                case "GetPartIOList":
                    context.Response.Write(GetPartIOList());
                    context.Response.End();
                    break;
                    //获取采购计划列表
                case "GetPartPlanList":
                    context.Response.Write(GetPartPlanList());
                    context.Response.End();
                    break;
                    //获取已验收过的验收清单
                case "GetAcceptPartPlanList":
                    context.Response.Write(GetAcceptPartPlanList());
                    context.Response.End();
                    break;
                //查询返回用户信息
                case "GetUnionUserInfor":
                    context.Response.Write(GetUnionUserInfor());
                    context.Response.End();
                    break;
                //获取领用物料明细
                case "GetPartCollarInfor":
                    context.Response.Write(GetPartCollarInfor());
                    context.Response.End();
                    break;
                    //采购计划单基础数据
                case "SaveBaseData":
                    context.Response.Write(SaveBaseData());
                    context.Response.End();
                    break;
                //保存采购计划物料数据
                case "SavePartPlanDate":
                    context.Response.Write(SavePartPlanDate());
                    context.Response.End();
                    break;
                //保存验收物料数据，生成物料验收与采购计划关联数据
                case "SavePartAcceptedInfor":
                    context.Response.Write(SavePartAcceptedInfor());
                    context.Response.End();
                    break;
                //获取入库物料明细
                case "GetInStoreData":
                    context.Response.Write(GetInStoreData());
                    context.Response.End();
                    break;
                //保存物料入库数据
                case "SavePartAccepted":
                    context.Response.Write(SavePartAccepted());
                    context.Response.End();
                    break;
                //保存物料领用记录信息
                case "SavePartCollarDate":
                    context.Response.Write(SavePartCollarDate());
                    context.Response.End();
                    break;
                    //修改物料报警阀值
                case "UpdatePartInfor":
                    context.Response.Write(UpdatePartInfor());
                    context.Response.End();
                    break;
                    //删除采购计划单
                case "DeletePartBuyRequstInfor":
                    context.Response.Write(DeletePartBuyRequstInfor());
                    context.Response.End();
                    break;
                //删除已经设置的采购物料
                case "DeletePartBuyRequstLstInfor":
                    context.Response.Write(DeletePartBuyRequstLstInfor());
                    context.Response.End();
                    break;
                //查询物料入库时间段
                case "GetPartInputTimeList":
                    context.Response.Write(GetPartInputTimeList());
                    context.Response.End();
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 保存验收物料数据，生成物料验收与采购计划关联数据
    /// </summary>
    /// <returns></returns>
    public string SavePartAcceptedInfor() {
        result = "";
        TOaPartAcceptedVo objItems = new TOaPartAcceptedVo();
        objItems.PART_ID = strPartId;
        objItems.NEED_QUANTITY = strNeedQuatity;
        objItems.PRICE = strPrice;
        objItems.AMOUNT = strAmount;
        objItems.CHECK_DATE = strCheckDate;
        objItems.CHECK_RESULT = strCheckResult;
        objItems.CHECK_USERID = strCheckUserID;
        objItems.RECIVEPART_DATE = strReciveDate;
        objItems.ENTERPRISE_NAME = strEnterpriseName;
        objItems.USERDO = strUserDo;
        objItems.REMARK1 = strRemark;
       //通过标识判断是领料申请单(remark=0)还是呈报单(remark=1),呈报单在验收时，不反写库存量；领料申请单要反写库存数量
        DataTable dt = new TOaPartBuyRequstLstLogic().SelectRemarks(strPartPlanId);//strPartPlanId为物料采购申请清单ID
        if (dt.Rows.Count > 0)
        {
            objItems.FLAG = dt.Rows[0][0].ToString(); result = "true";
        }
        else
        {
            objItems.FLAG = ""; result = "";
        }
        #region//判断返回的remark的值，如果是1，说明是呈报单，在验收时，往领用明细中插入一条数据
        if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()) && dt.Rows[0][0].ToString().Equals("1"))
        {
            DataTable Getdt = new TOaPartBuyRequstLstLogic().GetInfo(strPartPlanId);//strPartPlanId为物料采购申请清单ID
            if (Getdt.Rows.Count > 0)
            {
                TOaPartCollarVo objCollar = new TOaPartCollarVo();
                objCollar.PART_ID = Getdt.Rows[0][1].ToString();//物料ID
                objCollar.USER_ID = Getdt.Rows[0][0].ToString();//用户ID
                objCollar.LASTIN_DATE = DateTime.Now.ToString();//时间
                objCollar.ID = GetSerialNumber("t_oa_PartCollarID");//明细单ID
                bool flag = new TOaPartCollarLogic().Create(objCollar);
                result = "true";
            }
            else
            {
                result = "";
            }
        }
        #endregion
        if (!String.IsNullOrEmpty(strPartAcceptId))
        {
            objItems.ID = strPartAcceptId;
            if (new TOaPartAcceptedLogic().Edit(objItems))
            {
                result = "true";
            }
        }
        else {
            objItems.ID = GetSerialNumber("t_oa_AcceptedID");
            if (new TOaPartAcceptedLogic().Create(objItems)) {
                TOaPartAcceptedlistVo objItemNew = new TOaPartAcceptedlistVo();
                objItemNew.ID = GetSerialNumber("t_oa_AcceptedLstID");
                objItemNew.ACCEPTED_ID = objItems.ID;
                objItemNew.REQUST_LST_ID = strPartPlanId;

                if (new TOaPartAcceptedlistLogic().Create(objItemNew)) {
                    result = objItemNew.ID;
                }
            }
        }
        return result;
        
    }

    /// <summary>
    /// 保存物料入库数据
    /// </summary>
    /// <returns></returns>
    public string SavePartAccepted()
    {
        result = "";
        TOaPartAcceptedVo objItems = new TOaPartAcceptedVo();
        objItems.PART_ID = strPartId;
        objItems.NEED_QUANTITY = strNeedQuatity;
        objItems.PRICE = strPrice;
        objItems.AMOUNT = strAmount;
        objItems.CHECK_DATE = strCheckDate;
        objItems.CHECK_RESULT = strCheckResult;
        objItems.CHECK_USERID = strCheckUserID;
        objItems.RECIVEPART_DATE = strReciveDate;
        objItems.ENTERPRISE_NAME = strEnterpriseName;
        objItems.USERDO = strUserDo;
        objItems.REMARK1 = strRemark;
        objItems.FLAG = "0";

        if (!String.IsNullOrEmpty(strMyPartAcceptedID))
        {
            objItems.ID = strMyPartAcceptedID;
            if (new TOaPartAcceptedLogic().Edit(objItems))
            {
                result = objItems.ID;
            }
        }
        if (String.IsNullOrEmpty(strMyPartAcceptedID))
        {
            objItems.ID = GetSerialNumber("t_oa_AcceptedID");
            if (new TOaPartAcceptedLogic().Create(objItems))
            {
                result = objItems.ID;
            }
        }
        

        return result;

    }
    
    /// <summary>
    /// 保存表单基础数据
    /// </summary>
    /// <returns></returns>
    public string SaveBaseData() {
        result = "";
        TOaPartBuyRequstVo objItems = new TOaPartBuyRequstVo();
        objItems.APPLY_DEPT_ID = strDept;
        objItems.APPLY_USER_ID = strUserID;
        objItems.APPLY_DATE = strDate;
        objItems.APPLY_TITLE = strBt;
        objItems.STATUS = strStatus;
        objItems.APPLY_TYPE = strApplyType;
        if (String.IsNullOrEmpty(strtaskID))
        {
            objItems.ID = GetSerialNumber("t_oa_PartBuyID");
            if (new TOaPartBuyRequstLogic().Create(objItems)) {
                result = objItems.ID;
            }
        }
        else {
            objItems.ID = strtaskID;
            if (new TOaPartBuyRequstLogic().Edit(objItems)) {
                result = objItems.ID;
            } 
        }
        return result;
    }

    /// <summary>
    /// 保存物料采购计划
    /// </summary>
    /// <returns></returns>
    public string SavePartPlanDate() {
        result = "";
        TOaPartBuyRequstLstVo objItems = new TOaPartBuyRequstLstVo();
        objItems.DELIVERY_DATE = strDevDate;
        objItems.BUDGET_MONEY = strBudgetMoney;
        objItems.NEED_QUANTITY = strNeedQuatity;
        objItems.USERDO = strUserDo;
        objItems.PART_ID = strPartId;
        objItems.REQUST_ID = strtaskID;
        objItems.STATUS = "0";
        if (String.IsNullOrEmpty(strPartPlanId))
        {
            objItems.ID = GetSerialNumber("t_oa_PartPlanID");
            if (new TOaPartBuyRequstLstLogic().Create(objItems))
            {
                result = objItems.ID;
            }
        }
        else {
            objItems.ID = strPartPlanId;
            if (new TOaPartBuyRequstLstLogic().Edit(objItems)) {
                result = objItems.ID;
            }
        }
        return result;
    }

    /// <summary>
    /// 保存物料领用信息
    /// </summary>
    /// <returns></returns>
    public string SavePartCollarDate() {
        result = "";
        TOaPartCollarVo objItems = new TOaPartCollarVo();
        objItems.PART_ID = strPartId;
        objItems.USER_ID = strUserID;
        objItems.USED_QUANTITY = strNeedQuatity;
        objItems.LASTIN_DATE = DateTime.Now.ToString();
        objItems.REASON = strRemark;
        objItems.REMARK1 = strREMARK1;

        if (!String.IsNullOrEmpty(strPartCollarId)) {
            objItems.ID = strPartCollarId;
            if (new TOaPartCollarLogic().Edit(objItems))
            {
                result = objItems.ID;
            }
        }
        if (String.IsNullOrEmpty(strPartCollarId)) {
            objItems.ID = GetSerialNumber("t_oa_PartCollarID");
            if (new TOaPartCollarLogic().Create(objItems))
            {
                result = objItems.ID;
            }
        }

        return result;
    }
    /// <summary>
    /// 获取验收列表
    /// </summary>
    /// <returns></returns>
    public string GetAcceptPartPlanList() {
        result = "";
        dt = new DataTable();
        TOaPartAcceptedlistVo objItems = new TOaPartAcceptedlistVo();
        TOaPartInfoVo objItemPart = new TOaPartInfoVo();

        if (!String.IsNullOrEmpty(strPartPlanId))
        {
            objItems.REQUST_LST_ID = strPartPlanId;
        }
        if (!String.IsNullOrEmpty(strPartAcceptId)) {
            objItems.ACCEPTED_ID = strPartAcceptId;
        }
        if (!String.IsNullOrEmpty(strPartAcceptLstId)) {
            objItems.ID = strPartAcceptLstId;
        }
        if (!String.IsNullOrEmpty(strBeginDate)) {
            objItems.REMARK4 = strBeginDate;
        }
        if (!String.IsNullOrEmpty(strEndDate))
        {
            objItems.REMARK5 = strEndDate;
        }
        if (!String.IsNullOrEmpty(strPartCode)) {
            objItemPart.PART_CODE = strPartCode;
        }
        if (!String.IsNullOrEmpty(strPartName))
        {
            objItemPart.PART_NAME = strPartName;
        }
        dt = new TOaPartAcceptedlistLogic().SelectUnionByTable(objItems,objItemPart, intPageIndex, intPageSize);
        int CountNum = new TOaPartAcceptedlistLogic().GetSelectUnionByTableCount(objItems, objItemPart);
        DataView dv = new DataView();
        dv = dt.DefaultView;
        if (dt.Rows.Count > 0)
        {
            dv.Sort = "CHECK_DATE  DESC";
        }
        result = LigerGridDataToJson(dv.ToTable(), CountNum);

        return result;
    }
    /// <summary>
    /// 获取采购物料计划
    /// </summary>
    /// <returns></returns>
    public string GetPartPlanList() {
        result = "";
        int CountNum = 0;
        dt = new DataTable();
        DataView dv = new DataView();
        TOaPartBuyRequstLstVo objItems = new TOaPartBuyRequstLstVo();
        TOaPartInfoVo objItemPart = new TOaPartInfoVo();
        TOaPartBuyRequstVo objItemBy = new TOaPartBuyRequstVo();

        if (!String.IsNullOrEmpty(strGetType))
        {
            dv = new DataView();
            if (!String.IsNullOrEmpty(strtaskID))
            {
                objItems.REQUST_ID = strtaskID;
                dt = new TOaPartBuyRequstLstLogic().SelectUnionPartByTable(objItems, objItemPart, objItemBy, intPageIndex, intPageSize);
                CountNum = new TOaPartBuyRequstLstLogic().GetSelectUnionPartByTableResult(objItems, objItemPart, objItemBy);
            }
        }
        else
        {
            if (!String.IsNullOrEmpty(strtaskID) || !String.IsNullOrEmpty(strPartPlanId) || !String.IsNullOrEmpty(strPartId) || !String.IsNullOrEmpty(strReqStatus))
            {
                objItems.REQUST_ID = strtaskID;
                objItems.ID = strPartPlanId;
                objItems.PART_ID = strPartId;
                objItems.STATUS = strReqStatus;
            }
            if (!String.IsNullOrEmpty(strUserName) || !String.IsNullOrEmpty(strEndDate) || !String.IsNullOrEmpty(strBeginDate))
            {
                objItems.REMARK3 = strUserName;
                objItems.REMARK4 = strBeginDate;
                objItems.REMARK5 = strEndDate;
            }
            if (!String.IsNullOrEmpty(strStatus) || !String.IsNullOrEmpty(strDept))
            {
                objItemBy.APP_DEPT_ID = strDept;
                objItemBy.STATUS = strStatus;
            }

            if (!String.IsNullOrEmpty(strPartName) || !String.IsNullOrEmpty(strPartCode))
            {
                objItemPart.PART_NAME = strPartName;
                objItemPart.PART_CODE = strPartCode;
            }
            dt = new TOaPartBuyRequstLstLogic().SelectUnionPartByTable(objItems, objItemPart, objItemBy, intPageIndex, intPageSize);
           CountNum = new TOaPartBuyRequstLstLogic().GetSelectUnionPartByTableResult(objItems, objItemPart, objItemBy);
           //if (!String.IsNullOrEmpty(type) && type == "1") {
           //    DataRow[] rows = dt.Select("APPLY_TYPE = '02' or APPLY_TYPE = '03'");
           //    for (int i = 0; i < rows.Length; i++) {
           //        dt.Rows.Remove(rows[i]);
           //    }
           //    CountNum = dt.Rows.Count; 
           //}
        }
        dv = new DataView();
        dv = dt.DefaultView;
        if (dt.Rows.Count > 0)
        {
            dv.Sort = "DELIVERY_DATE";
        }
        if (dt.Rows.Count > 0)
        {
            result = LigerGridDataToJson(dv.ToTable(), CountNum);
        }
        return result;
    }
    /// <summary>
    ///获取物料申请列表（或获取某一具体物料申请表详细信息）
    /// </summary>
    /// <returns></returns>
    public string GetPartPushViewList() {
        result = "";
        dt = new DataTable();
        TOaPartBuyRequstVo objItems = new TOaPartBuyRequstVo();
        
        TOaPartInfoVo objPart = new TOaPartInfoVo();
        if (keshi == "1") {
            objPart.PART_TYPE = "01,02,03";
        }
        else if (keshi == "2") {
            objPart.PART_TYPE = "06";
        }
        else if (keshi == "3") {
            objPart.PART_TYPE = "01,02,03,06";
            objItems.APPLY_TYPE = "02,03";
        }
        
        objItems.STATUS = strStatus;
        //objItems.APPLY_USER_ID = base.LogInfo.UserInfo.ID;
        if (!String.IsNullOrEmpty(strtaskID)) {
            objItems.ID = strtaskID;
        }
        if (objPart.PART_TYPE == "")
            dt = new TOaPartBuyRequstLogic().SelectByTable(objItems, intPageIndex, intPageSize);
        else
            dt = new TOaPartBuyRequstLogic().SelectByPart(objItems, objPart, intPageIndex, intPageSize);
        int CountNum=new TOaPartBuyRequstLogic().GetSelectResultCount(objItems);
        result = LigerGridDataToJson(dt,CountNum);
        return result;
    }
    //获取物料列表
    public string GetPartList() {
        result = "";
        dt = new DataTable();
        TOaPartInfoVo objItems = new TOaPartInfoVo();
        objItems.IS_DEL = "0";
        objItems.PART_NAME = strPartName;
        objItems.PART_CODE = strPartCode;

        if (keshi == "1")
        {
            objItems.PART_TYPE = "01,02,03";
        }
        if (keshi == "2")
        {
            objItems.PART_TYPE = "06";
        }
        if (keshi == "3")
        {
            objItems.PART_TYPE = "01,02,03";
        }
        
        if (type == "Equipment")
        {
            objItems.PART_TYPE = "01,02,03";
        }
        if (type == "Office")
        {
            objItems.PART_TYPE = "06";
        }
        if (!String.IsNullOrEmpty(strPartId))
        {
            objItems.ID = strPartId;
            dt = new TOaPartInfoLogic().SelectByTable(objItems);
            result = LigerGridDataToJson(dt, dt.Rows.Count);
        }
        else {
            dt = new TOaPartInfoLogic().SelectByTable(objItems, intPageIndex, intPageSize);
            int CountNum=new TOaPartInfoLogic().GetSelectResultCount(objItems);
            DataRow[] rows = dt.Select("INVENTORY is null ");
            for (int i = 0; i < rows.Length; i++) {
                rows[i]["INVENTORY"] = 0;
            }                   
            result = LigerGridDataToJson(dt,CountNum);
        }
        return result;
    }
    //获取出入库物料明细列表
    public string GetPartIOList()
    {
        string rtninfo = string.Empty;
        DataTable dt = new TOaPartInfoLogic().GetLineInfo(SEA_NAME, StartTime, EndTime);
        rtninfo = LigerGridDataToJson(dt, dt.Rows.Count);
        return rtninfo; 
    }
    /// <summary>
    /// 获取登录用户的用户信息
    /// </summary>
    /// <returns></returns>
    public string GetLoginUserInfor() {
        result = "";
        dt = new DataTable();
        TSysUserVo objItems = new TSysUserVo();
        objItems.ID = LogInfo.UserInfo.ID;
        objItems.IS_HIDE = "0";
        objItems.IS_DEL = "0";
        objItems.IS_USE = "1";

        dt = new TSysUserLogic().SelectByTable(objItems);
        result = LigerGridDataToJson(dt,dt.Rows.Count);
        return result;
    }
    /// <summary>
    /// 获取指定用户信息
    /// </summary>
    /// <returns></returns>
    public string GetUserInfor() {
        result = "";
        dt = new DataTable();
        TSysUserVo objItems = new TSysUserVo();
        if (!String.IsNullOrEmpty(strUserID))
        {
            objItems.ID = strUserID;
        }
        objItems.IS_DEL = "0";
        objItems.IS_USE = "1";
        objItems.IS_HIDE = "0";
        dt = new TSysUserLogic().SelectByTable(objItems);
        result = LigerGridDataToJson(dt,dt.Rows.Count);
        return result;
    }

    /// <summary>
    /// 联合查询用户信息
    /// </summary>
    /// <returns></returns>
    public string GetUnionUserInfor()
    {
        result = "";
        dt = new DataTable();
        TSysUserVo objItems = new TSysUserVo();

        objItems.REMARK5 = strDept;
        dt = new TSysUserLogic().SelectUnionByDefineTable(objItems,intPageIndex,intPageSize);
        int CountNum = new TSysUserLogic().GetSelectUnionByDefineTableCount(objItems);
        result = LigerGridDataToJson(dt,CountNum);
        return result;
    }
    
    /// <summary>
    /// 获取入库物料明细清单
    /// </summary>
    /// <returns></returns>
    public string GetInStoreData()
    {
        result = "";
        dt = new DataTable();
        TOaPartAcceptedVo objItems = new TOaPartAcceptedVo();
        objItems.PART_ID = strPartId;
        dt = new TOaPartAcceptedLogic().SelectByTableEx(objItems, intPageIndex, intPageSize);
        int CountNum = new TOaPartAcceptedLogic().GetSelectByTableExCount(objItems);
        result = LigerGridDataToJson(dt,CountNum);
        return result;
    }

    /// <summary>
    /// 获取入库物料时间段的明细清单
    /// </summary>
    /// <returns></returns>
    public string GetPartInputTimeList()
    {
        result = "";
        dt = new DataTable();
        TOaPartAcceptedVo objItems = new TOaPartAcceptedVo();
        objItems.PART_ID = strPartId;
        objItems.CHECK_USERID = strReal_Name;
        dt = new TOaPartAcceptedLogic().SelectByTimeList(objItems, StartTime, EndTime, intPageIndex, intPageSize);
        int CountNum = new TOaPartAcceptedLogic().GetSelectByTimeListCount(objItems, StartTime, EndTime);
        result = LigerGridDataToJson(dt, CountNum);
        return result;
    }
    
    /// <summary>
    /// 获取已领用物料明细清单
    /// </summary>
    /// <returns></returns>
    public string GetPartCollarInfor() {
        result = "";
        dt = new DataTable();
        TOaPartCollarVo objItems = new TOaPartCollarVo();
        TOaPartInfoVo objItemParts = new TOaPartInfoVo();
        if (!String.IsNullOrEmpty(strPartCollarId)) {
            objItems.ID = strPartCollarId;
        }
        objItems.USER_ID = strReal_Name;
        if (!String.IsNullOrEmpty(strBeginDate) || !String.IsNullOrEmpty(strEndDate))
        {
            objItems.REMARK4 = strBeginDate;
            objItems.REMARK5 = strEndDate;
        }
        
        if (!String.IsNullOrEmpty(strPartId)) {
            objItemParts.ID = strPartId;
        }

        dt = new TOaPartCollarLogic().SelectUnionPartByTable(objItems, objItemParts, intPageIndex, intPageSize);
        int CountNum = new TOaPartCollarLogic().GetUnionPartByTableCount(objItems, objItemParts);
        result = LigerGridDataToJson(dt,CountNum);
        return result;
    }
    /// <summary>
    /// 修改物料报警阀值
    /// </summary>
    /// <returns></returns>
    public string UpdatePartInfor() {
        result = "";
        TOaPartInfoVo objItems = new TOaPartInfoVo();
        objItems.ID = strPartId;
        objItems.ALARM = strWarnValue;
        if (new TOaPartInfoLogic().Edit(objItems)) {
            result = "true";
        }
        return result;
    }
    /// <summary>
    /// 删除采购计划申请单数据
    /// </summary>
    /// <returns></returns>
    public string DeletePartBuyRequstInfor() {
        result = "";
        TOaPartBuyRequstVo objItems = new TOaPartBuyRequstVo();
        if (!String.IsNullOrEmpty(strtaskID))
        {
            objItems.ID = strtaskID;
            if (new TOaPartBuyRequstLogic().Delete(objItems.ID))
            {
                result = "true";
            }
        }
        return result;
    }

    /// <summary>
    /// 删除已经计划的采购物料
    /// </summary>
    /// <returns></returns>
    public string DeletePartBuyRequstLstInfor()
    {
        result = "";
        TOaPartBuyRequstLstVo objItems = new TOaPartBuyRequstLstVo();
        if (!String.IsNullOrEmpty(strPartPlanId))
        {
            objItems.ID = strPartPlanId;
            if (new TOaPartBuyRequstLstLogic().Delete(objItems.ID))
            {
                result = "true";
            }
        }
        return result;
    }
  /// <summary>
    /// 获取URL参数
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

        //业务参数相关
        if (!String.IsNullOrEmpty(context.Request.Params["strtaskID"]))
        {
            strtaskID = context.Request.Params["strtaskID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strUserID"]))
        {
            strUserID = context.Request.Params["strUserID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strStatus"]))
        {
            strStatus = context.Request.Params["strStatus"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPartPlanId"]))
        {
            strPartPlanId = context.Request.Params["strPartPlanId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPartId"]))
        {
            strPartId = context.Request.Params["strPartId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDept"]))
        {
            strDept = context.Request.Params["strDept"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDate"]))
        {
            strDate = context.Request.Params["strDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strBt"]))
        {
            strBt = context.Request.Params["strBt"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPartCode"]))
        {
            strPartCode = context.Request.Params["strPartCode"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPartName"]))
        {
            strPartName = context.Request.Params["strPartName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strDevDate"]))
        {
            strDevDate = context.Request.Params["strDevDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strBudgetMoney"]))
        {
            strBudgetMoney = context.Request.Params["strBudgetMoney"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strNeedQuatity"]))
        {
            strNeedQuatity = context.Request.Params["strNeedQuatity"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strUserDo"]))
        {
            strUserDo = context.Request.Params["strUserDo"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPartAcceptId"]))
        {
            strPartAcceptId = context.Request.Params["strPartAcceptId"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strPartAcceptLstId"]))
        {
            strPartAcceptLstId = context.Request.Params["strPartAcceptLstId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strReqStatus"]))
        {
            strReqStatus = context.Request.Params["strReqStatus"].Trim();
        }

        if (!String.IsNullOrEmpty(context.Request.Params["strPrice"]))
        {
            strPrice = context.Request.Params["strPrice"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strAmount"]))
        {
            strAmount = context.Request.Params["strAmount"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEnterpriseName"]))
        {
            strEnterpriseName = context.Request.Params["strEnterpriseName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strReciveDate"]))
        {
            strReciveDate = context.Request.Params["strReciveDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCheckDate"]))
        {
            strCheckDate = context.Request.Params["strCheckDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCheckUserID"]))
        {
            strCheckUserID = context.Request.Params["strCheckUserID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strCheckResult"]))
        {
            strCheckResult = context.Request.Params["strCheckResult"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strRemark"]))
        {
            strRemark = context.Request.Params["strRemark"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strUserName"]))
        {
            strUserName = context.Request.Params["strUserName"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strBeginDate"]))
        {
            strBeginDate = context.Request.Params["strBeginDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strEndDate"]))
        {
            strEndDate = context.Request.Params["strEndDate"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strPartCollarId"]))
        {
            strPartCollarId = context.Request.Params["strPartCollarId"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strWarnValue"]))
        {
            strWarnValue = context.Request.Params["strWarnValue"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strGetType"]))
        {
            strGetType = context.Request.Params["strGetType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strReal_Name"]))
        {
            strReal_Name = context.Request.Params["strReal_Name"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strREMARK1"]))
        {
            strREMARK1 = context.Request.Params["strREMARK1"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strMyPartAcceptedID"]))
        {
            strMyPartAcceptedID = context.Request.Params["strMyPartAcceptedID"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["SEA_NAME"]))
        {
            SEA_NAME = context.Request.Params["SEA_NAME"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["StartTime"]))
        {
            StartTime = context.Request.Params["StartTime"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["EndTime"]))
        {
            EndTime = context.Request.Params["EndTime"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["strApplyType"]))
        {
            strApplyType = context.Request.Params["strApplyType"].Trim();
        }
        if (!String.IsNullOrEmpty(context.Request.Params["type"]))
        {
            type = context.Request.Params["type"].Trim();
        }
        //黄进军添加
        if (!string.IsNullOrEmpty(context.Request.Params["keshi"])) 
        { 
            keshi = context.Request.Params["keshi"].Trim();
        }
        
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}