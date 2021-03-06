using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractVo tMisContract)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT " + this.BuildWhereStatement(tMisContract);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisContractVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContract">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractVo Details(TMisContractVo tMisContract)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT " + this.BuildWhereStatement(tMisContract));
            return SqlHelper.ExecuteObject(new TMisContractVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractVo> SelectByObject(TMisContractVo tMisContract, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_CONTRACT " + this.BuildWhereStatement(tMisContract));
            return SqlHelper.ExecuteObjectList(tMisContract, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractVo tMisContract, int iIndex, int iCount)
        {
            //string strSQL = "";
            //if (tMisContract.REMARK5 == "0")
            //{
            //     strSQL = " select * from T_MIS_CONTRACT WHERE exists(SELECT * FROM T_MIS_MONITOR_TASK WHERE T_MIS_MONITOR_TASK.CONTRACT_ID=T_MIS_CONTRACT.ID AND TASK_STATUS<>'11') OR CONTRACT_STATUS<>'9' {0} ";
            //}
            //else if (tMisContract.REMARK5 == "1")
            //{
            //     strSQL = " select * from T_MIS_CONTRACT WHERE exists(SELECT * FROM T_MIS_MONITOR_TASK WHERE T_MIS_MONITOR_TASK.CONTRACT_ID=T_MIS_CONTRACT.ID AND TASK_STATUS='11') {0} ";
            //}
            string strSQL = " select * from T_MIS_CONTRACT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContract));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tMisContract, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractVo tMisContract)
        {
            string strSQL = "select * from T_MIS_CONTRACT " + this.BuildWhereStatement(tMisContract);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取对象DataTable Create By : weilin 2014-09-17
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableEx(TMisContractVo tMisContract, string strStatus, int iIndex, int iCount)
        {
            string strSQL = " select * from T_MIS_CONTRACT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContract));
            if (strStatus == "0")
            {
                strSQL += "and ((exists(SELECT * FROM T_MIS_MONITOR_TASK WHERE T_MIS_MONITOR_TASK.CONTRACT_ID=T_MIS_CONTRACT.ID AND TASK_STATUS<>'11') and CONTRACT_STATUS='9') OR CONTRACT_STATUS<>'9')";
            }
            else if (strStatus == "1")
            {
                strSQL += "and exists(SELECT * FROM T_MIS_MONITOR_TASK WHERE T_MIS_MONITOR_TASK.CONTRACT_ID=T_MIS_CONTRACT.ID AND TASK_STATUS='11')";
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tMisContract, strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 获得查询结果总行数，用于分页 Create By : weilin 2014-09-17
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountEx(TMisContractVo tMisContract, string strStatus)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT " + this.BuildWhereStatement(tMisContract);
            if (strStatus == "0")
            {
                strSQL += "and ((exists(SELECT * FROM T_MIS_MONITOR_TASK WHERE T_MIS_MONITOR_TASK.CONTRACT_ID=T_MIS_CONTRACT.ID AND TASK_STATUS<>'11') and CONTRACT_STATUS='9') OR CONTRACT_STATUS<>'9')";
            }
            else if (strStatus == "1")
            {
                strSQL += "and exists(SELECT * FROM T_MIS_MONITOR_TASK WHERE T_MIS_MONITOR_TASK.CONTRACT_ID=T_MIS_CONTRACT.ID AND TASK_STATUS='11')";
            }
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <returns></returns>
        public TMisContractVo SelectByObject(TMisContractVo tMisContract)
        {
            string strSQL = "select * from T_MIS_CONTRACT " + this.BuildWhereStatement(tMisContract);
            return SqlHelper.ExecuteObject(new TMisContractVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractVo tMisContract)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContract, TMisContractVo.T_MIS_CONTRACT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContract">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractVo tMisContract)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContract, TMisContractVo.T_MIS_CONTRACT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContract.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContract_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContract_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractVo tMisContract_UpdateSet, TMisContractVo tMisContract_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContract_UpdateSet, TMisContractVo.T_MIS_CONTRACT_TABLE);
            strSQL += this.BuildWhereStatement(tMisContract_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractVo tMisContract)
        {
            string strSQL = "delete from T_MIS_CONTRACT ";
            strSQL += this.BuildWhereStatement(tMisContract);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        #region 自定义查询
        /// <summary>
        /// 自定义查询 总数
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="strDutyCode">任务单号</param>
        /// <param name="strReportCode">报告单号</param>
        /// <returns>总数</returns>
        public int GetSelectResultCountForSearch(TMisContractVo tMisContract, string strDutyCode, string strReportCode)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT " + this.BuildWhereStatement(tMisContract);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 自定义查询 数据集
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="strDutyCode">任务单号</param>
        /// <param name="strReportCode">报考单号</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">单页显示数</param>
        /// <returns>数据集</returns>
        public DataTable SelectByTableForSearch(TMisContractVo tMisContract, string strDutyCode, string strReportCode, int intPageIndex, int intPageSize)
        {
            // 委托书查询
            string strSQL = " select * from T_MIS_CONTRACT WHERE 1=1 ";
            //查询条件构造
            if (!string.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
                strSQL += " and CONTRACT_YEAR='" + tMisContract.CONTRACT_YEAR + "'";
            if (!string.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
                strSQL += " and CONTRACT_TYPE='" + tMisContract.CONTRACT_TYPE + "'";
            if (!string.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
                strSQL += " and CONTRACT_CODE='" + tMisContract.CONTRACT_CODE + "'";
            if (!string.IsNullOrEmpty(tMisContract.CLIENT_COMPANY_ID))
                strSQL += " and CLIENT_COMPANY_ID in (SELECT COMPANY_ID FROM T_MIS_MONITOR_TASK_COMPANY WHERE IS_USE='0' AND COMPANY_NAME like '%" + tMisContract.CLIENT_COMPANY_ID + "%')";
            if (!string.IsNullOrEmpty(tMisContract.CLIENT_COMPANY_ID))
                strSQL += " and TESTED_COMPANY_ID in (SELECT COMPANY_ID FROM T_MIS_MONITOR_TASK_COMPANY WHERE IS_USE='0' AND COMPANY_NAME like '%" + tMisContract.TESTED_COMPANY_ID + "%')";
            if (!string.IsNullOrEmpty(tMisContract.TEST_TYPES))
                strSQL += " and TEST_TYPES like '%" + tMisContract.TEST_TYPES + "%'";
            if (!string.IsNullOrEmpty(tMisContract.PROJECT_NAME))
                strSQL += "and PROJECT_NAME like '%" + tMisContract.PROJECT_NAME + "%'";

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }
        #endregion

        #region 自定义查询委托书列表 胡方扬 2012-12-06
        /// <summary>
        /// 自定义查询 总数
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <returns>总数</returns>
        public int GetSelectResultCountForSearchList(TMisContractVo tMisContract)
        {
            // 委托书查询
            string strSQL = " SELECT A.* from T_MIS_CONTRACT  A";
            if (!String.IsNullOrEmpty(tMisContract.CLIENT_COMPANY_ID))
            {
                strSQL += " INNER JOIN T_MIS_CONTRACT_COMPANY B  ON A.CLIENT_COMPANY_ID=B.ID AND B.IS_DEL='0' AND B.COMPANY_NAME LIKE '%" + tMisContract.CLIENT_COMPANY_ID + "%'";
            }
            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += " INNER JOIN T_MIS_CONTRACT_COMPANY B  ON A.TESTED_COMPANY_ID=B.ID AND B.IS_DEL='0' AND B.COMPANY_NAME LIKE '%" + tMisContract.TESTED_COMPANY_ID + "%'";
            }
            strSQL += " WHERE 1=1";
            //查询条件构造
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
                strSQL += " AND A.CONTRACT_YEAR='" + tMisContract.CONTRACT_YEAR + "'";
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
                strSQL += " AND A.CONTRACT_TYPE='" + tMisContract.CONTRACT_TYPE + "'";
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
                strSQL += " AND A.CONTRACT_CODE='" + tMisContract.CONTRACT_CODE + "'";
            //if (!string.IsNullOrEmpty(tMisContract.CLIENT_COMPANY_ID))
            //    strSQL += " and CLIENT_COMPANY_ID in (SELECT ID FROM T_MIS_CONTRACT_COMPANY WHERE IS_DEL='0' AND COMPANY_NAME like '%" + tMisContract.CLIENT_COMPANY_ID + "%')";
            //if (!string.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            //    strSQL += " and TESTED_COMPANY_ID in (SELECT ID FROM T_MIS_CONTRACT_COMPANY WHERE IS_DEL='0' AND COMPANY_NAME like '%" + tMisContract.TESTED_COMPANY_ID + "%')";
            if (!String.IsNullOrEmpty(tMisContract.TEST_TYPES))
                strSQL += " AND A.TEST_TYPES LIKE '%" + tMisContract.TEST_TYPES + "%'";
            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
                strSQL += " AND A.PROJECT_NAME LIKE '%" + tMisContract.PROJECT_NAME + "%'";
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
                strSQL += " AND A. CONTRACT_STATUS = '" + tMisContract.CONTRACT_STATUS + "'";
            if (!String.IsNullOrEmpty(tMisContract.ISQUICKLY))
                strSQL += " AND A.ISQUICKLY = '" + tMisContract.ISQUICKLY + "'";
            else strSQL += " AND A.ISQUICKLY IS  NULL";
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 自定义查询 数据集
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">单页显示数</param>
        /// <returns>数据集</returns>
        public DataTable SelectByTableForSearchList(TMisContractVo tMisContract, int intPageIndex, int intPageSize)
        {
            // 委托书查询
            string strSQL = " SELECT A.* from T_MIS_CONTRACT  A";
            if (!String.IsNullOrEmpty(tMisContract.CLIENT_COMPANY_ID))
            {
                strSQL += " INNER JOIN T_MIS_CONTRACT_COMPANY B  ON A.CLIENT_COMPANY_ID=B.ID AND B.IS_DEL='0' AND B.COMPANY_NAME LIKE '%" + tMisContract.CLIENT_COMPANY_ID + "%'";
            }
            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += " INNER JOIN T_MIS_CONTRACT_COMPANY B  ON A.TESTED_COMPANY_ID=B.ID AND B.IS_DEL='0' AND B.COMPANY_NAME LIKE '%" + tMisContract.TESTED_COMPANY_ID + "%'";
            }
            strSQL += " WHERE 1=1";
            //查询条件构造
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
                strSQL += " AND A.CONTRACT_YEAR='" + tMisContract.CONTRACT_YEAR + "'";
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
                strSQL += " AND A.CONTRACT_TYPE='" + tMisContract.CONTRACT_TYPE + "'";
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
                strSQL += " AND A.CONTRACT_CODE='" + tMisContract.CONTRACT_CODE + "'";
            //if (!string.IsNullOrEmpty(tMisContract.CLIENT_COMPANY_ID))
            //    strSQL += " and CLIENT_COMPANY_ID in (SELECT ID FROM T_MIS_CONTRACT_COMPANY WHERE IS_DEL='0' AND COMPANY_NAME like '%" + tMisContract.CLIENT_COMPANY_ID + "%')";
            //if (!string.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            //    strSQL += " and TESTED_COMPANY_ID in (SELECT ID FROM T_MIS_CONTRACT_COMPANY WHERE IS_DEL='0' AND COMPANY_NAME like '%" + tMisContract.TESTED_COMPANY_ID + "%')";
            if (!String.IsNullOrEmpty(tMisContract.TEST_TYPES))
                strSQL += " AND A.TEST_TYPES LIKE '%" + tMisContract.TEST_TYPES + "%'";
            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
                strSQL += " AND A.PROJECT_NAME LIKE '%" + tMisContract.PROJECT_NAME + "%'";
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
                strSQL += " AND A. CONTRACT_STATUS in (" + tMisContract.CONTRACT_STATUS + ")";
            if (!String.IsNullOrEmpty(tMisContract.ISQUICKLY))
                strSQL += "AND A.ISQUICKLY = '" + tMisContract.ISQUICKLY + "'";
            else strSQL += " AND A.ISQUICKLY IS  NULL";
            strSQL += " order by A.ID  DESC";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 获取委托书和受检企业关联信息条目数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable SelectDefineTableContractResult(TMisContractVo tMisContract,string strCompanyName,string strArea)
        {
            // 委托书查询
            string strSQL = " SELECT A.*,B.AREA,B.COMPANY_NAME,B.INDUSTRY FROM T_MIS_CONTRACT A LEFT JOIN T_MIS_CONTRACT_COMPANY B ON A.TESTED_COMPANY_ID=B.ID WHERE 1=1";
            if (!String.IsNullOrEmpty(strCompanyName))
            {
                strSQL += " AND B.COMPANY_NAME LIKE '%" + strCompanyName + "%'";
            }
            if (!String.IsNullOrEmpty(strArea))
            {
                strSQL += " AND B.AREA='" + strArea + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.ID))
            {
                strSQL += " AND A.ID='" + tMisContract.ID + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += " AND A.CONTRACT_CODE LIKE '%" + tMisContract.CONTRACT_CODE + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.TEST_TYPES))
            {
                strSQL += " AND A.TEST_TYPES  LIKE '%" + tMisContract.TEST_TYPES + "%'";
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += " AND A.CONTRACT_STATUS='" + tMisContract.CONTRACT_STATUS + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.CCFLOW_ID1))
            {
                strSQL += " AND A.CCFLOW_ID1='" + tMisContract.CCFLOW_ID1 + "'";
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 获取委托书和受检企业关联信息
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <returns></returns>
        public DataTable SelectDefineTableContract(TMisContractVo tMisContract,string strCompanyName,string strArea, int intPageIndex, int intPageSize)
        {
            // 委托书查询
            string strSQL = " SELECT A.*,B.AREA,B.COMPANY_NAME,B.INDUSTRY FROM T_MIS_CONTRACT A LEFT JOIN T_MIS_CONTRACT_COMPANY B ON A.TESTED_COMPANY_ID=B.ID WHERE 1=1";
            if (!String.IsNullOrEmpty(strCompanyName))
            {
                strSQL += " AND B.COMPANY_NAME LIKE '%"+strCompanyName+"%'";
            }
            if (!String.IsNullOrEmpty(strArea))
            {
                strSQL += " AND B.AREA='" + strArea + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.ID))
            {
                strSQL += " AND A.ID='"+tMisContract.ID+"'";
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += " AND A.CONTRACT_CODE LIKE '%" + tMisContract.CONTRACT_CODE + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.TEST_TYPES))
            {
                strSQL += " AND A.TEST_TYPES  LIKE '%" + tMisContract.TEST_TYPES + "%'";
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += " AND A.CONTRACT_STATUS='" + tMisContract.CONTRACT_STATUS + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.CCFLOW_ID1))
            {
                strSQL += " AND A.CCFLOW_ID1='" + tMisContract.CCFLOW_ID1 + "'";
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 获取委托书列表关联获取委托、受检企业信息 Create By weilin 2014-10-16
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAcceptContract(string strContractID)
        {
            // 委托书查询
            string strSQL = @"select contract.*,client.COMPANY_ID COMPANYID,client.COMPANY_NAME COMPANYNAME,client.AREA,client.CONTACT_NAME CONTRACTNAME,client.PHONE,
                            client.CONTACT_ADDRESS CONTRACTADDRESS,tested.COMPANY_ID COMPANYIDFRIM,tested.COMPANY_NAME COMPANYNAMEFRIM,tested.AREA AREAFRIM,
                            tested.CONTACT_NAME CONTRACTNAMEFRIM,tested.PHONE PHONEFRIM,tested.CONTACT_ADDRESS CONTRACTADDRESSFRIM from T_MIS_CONTRACT contract
                            left join T_MIS_CONTRACT_COMPANY client on(contract.CLIENT_COMPANY_ID=client.ID)
                            left join T_MIS_CONTRACT_COMPANY tested on(contract.TESTED_COMPANY_ID=tested.ID) where contract.ID='{0}'";
            strSQL = string.Format(strSQL, strContractID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 获取委托书和委托企业关联信息
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <returns></returns>
        public DataTable SelectByUnionTable(TMisContractVo tMisContract, int intPageIndex, int intPageSize)
        {

            string strSQL = " SELECT A.*,B.COMPANY_NAME FROM T_MIS_CONTRACT A INNER JOIN T_MIS_CONTRACT_COMPANY B ON A.CLIENT_COMPANY_ID=B.ID WHERE 1=1";
            if (!String.IsNullOrEmpty(tMisContract.CLIENT_COMPANY_ID))
            {
                strSQL += " AND B.COMPANY_NAME LIKE '%" + tMisContract.CLIENT_COMPANY_ID + "%'";
            }
            if (!String.IsNullOrEmpty(tMisContract.ID))
            {
                strSQL += " AND A.ID='" + tMisContract.ID + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += " AND A.CONTRACT_CODE LIKE '%" + tMisContract.CONTRACT_CODE + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.TEST_TYPES))
            {
                strSQL += " AND A.TEST_TYPES  LIKE '%" + tMisContract.TEST_TYPES + "%'";
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += " AND A.CONTRACT_STATUS='" + tMisContract.CONTRACT_STATUS + "'";
            }

            if (!String.IsNullOrEmpty(tMisContract.BOOKTYPE))
            {
                strSQL += " AND A.BOOKTYPE='" + tMisContract.BOOKTYPE + "'";
            }
            
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 获取委托书和委托企业关联信息委托书总数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <returns></returns>
        public int SelectByUnionTableResult(TMisContractVo tMisContract)
        {

            string strSQL = " SELECT A.*,B.COMPANY_NAME FROM T_MIS_CONTRACT A INNER JOIN T_MIS_CONTRACT_COMPANY B ON A.CLIENT_COMPANY_ID=B.ID WHERE 1=1";
            if (!String.IsNullOrEmpty(tMisContract.CLIENT_COMPANY_ID))
            {
                strSQL += " AND B.COMPANY_NAME LIKE '%" + tMisContract.CLIENT_COMPANY_ID + "%'";
            }
            if (!String.IsNullOrEmpty(tMisContract.ID))
            {
                strSQL += " AND A.ID='" + tMisContract.ID + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += " AND A.CONTRACT_CODE LIKE '%" + tMisContract.CONTRACT_CODE + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.TEST_TYPES))
            {
                strSQL += " AND A.TEST_TYPES  LIKE '%" + tMisContract.TEST_TYPES + "%'";
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += " AND A.CONTRACT_STATUS='" + tMisContract.CONTRACT_STATUS + "'";
            }
            if (!String.IsNullOrEmpty(tMisContract.BOOKTYPE))
            {
                strSQL += " AND A.BOOKTYPE='" + tMisContract.BOOKTYPE + "'";
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        /// <summary>
        /// 获取自送样采样计划
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable GetContractInforUnionSamplePlan(TMisContractVo tMisContract,TMisContractSamplePlanVo tMisContractSamplePlan) {
            string strSQL = "SELECT A.ID,A.CONTRACT_CODE,A.CONTRACT_YEAR,A.PROJECT_NAME,A.CONTRACT_TYPE,A.TEST_TYPES,A.CLIENT_COMPANY_ID,A.TESTED_COMPANY_ID,A.ASKING_DATE,A.SAMPLE_FREQ,A.PROJECT_ID,A.SAMPLE_SOURCE," +
                                        " B.ID AS SAMPLE_ID,B.FREQ,B.NUM AS SAMPLENUM,B.IF_PLAN,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.ID AS TASK_ID,D.TICKET_NUM,D.ASKING_DATE,E.ID AS SUBTASK_ID,F.COMPANY_NAME AS CLIENT_COMPANY_NAME,F.CONTACT_NAME AS CLIENT_CONTACT_NAME,F.PHONE AS CLIENT_PNONE,G.ID AS REPORT_ID,G.REPORT_CODE FROM T_MIS_CONTRACT A " +
                                        " INNER JOIN T_MIS_CONTRACT_SAMPLE_PLAN B ON B.CONTRACT_ID=A.ID"+
                                        " INNER JOIN T_MIS_CONTRACT_COMPANY C ON C.ID=A.CLIENT_COMPANY_ID"+
                                        " INNER JOIN dbo.T_MIS_MONITOR_TASK D ON D.CONTRACT_ID=B.CONTRACT_ID AND D.PLAN_ID=B.ID"+
                                        " INNER JOIN dbo.T_MIS_MONITOR_SUBTASK E ON E.TASK_ID=D.ID" +
                                        " INNER JOIN T_MIS_CONTRACT_COMPANY F ON F.ID=A.CLIENT_COMPANY_ID " +
                                        " INNER JOIN dbo.T_MIS_MONITOR_REPORT G ON G.TASK_ID=D.ID " +
                                        " WHERE 1=1";
            if (!String.IsNullOrEmpty(tMisContract.ID))
            {
                strSQL += String.Format(" AND A.ID='{0}'", tMisContract.ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.BOOKTYPE)) {
                strSQL += String.Format("  AND A.BOOKTYPE='{0}' ", tMisContract.BOOKTYPE);
            }
            if (!String.IsNullOrEmpty(tMisContractSamplePlan.IF_PLAN))
            {
                strSQL += String.Format("  AND B.IF_PLAN='{0}' ", tMisContractSamplePlan.IF_PLAN);
            }

            if (!String.IsNullOrEmpty(tMisContractSamplePlan.ID)) {
                strSQL += String.Format("  AND B.ID='{0}' ", tMisContractSamplePlan.ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取非快捷模式委托书的总计数,用于该类委托书单号生成
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public int GetContractCodeCount(TMisContractVo tMisContract) {
            string strSQL = " SELECT ID,PROJECT_NAME,CONTRACT_CODE FROM T_MIS_CONTRACT WHERE 1=1 ";
            strSQL += " AND CONTRACT_CODE IS NOT NULL";
            if (!String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format(" AND ISQUICKLY='{0}'", tMisContract.ISQUICKLY);
            }
            if (String.IsNullOrEmpty(tMisContract.ISQUICKLY)) {
                strSQL += String.Format(" AND ISQUICKLY IS NULL OR ISQUICKLY =''");
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 获取委托书导出数据 胡方扬 2013-04-23
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable GetExportInforData(TMisContractVo tMisContract) {
            string strSQL = @" SELECT A.*,B.COMPANY_NAME,B.CONTACT_NAME,B.PHONE,B.CONTACT_ADDRESS,B.POST,C.COMPANY_NAME TESTED_COMPANY_NAME,C.CONTACT_NAME TESTED_CONTACT_NAME,C.PHONE TESTED_PHONE,C.CONTACT_ADDRESS TESTED_CONTACT_ADDRESS,C.POST TESTED_POST,D.BUDGET,D.INCOME
                               FROM T_MIS_CONTRACT A INNER JOIN T_MIS_CONTRACT_COMPANY B ON A.CLIENT_COMPANY_ID=B.ID 
                               INNER JOIN T_MIS_CONTRACT_COMPANY C ON A.TESTED_COMPANY_ID=C.ID
                               LEFT JOIN T_MIS_CONTRACT_FEE D ON A.ID=D.CONTRACT_ID
                               WHERE 1=1 ";
            if (!String.IsNullOrEmpty(tMisContract.ID)) {
                strSQL +=String.Format(" AND A.ID ='{0}'",tMisContract.ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 获取委托书费用明细导出数据 魏林 2014-02-25(清远)
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable GetContractFreeData(TMisContractVo tMisContract)
        {
            string strSQL = @" select A.ID,B.MONITOR_ID,E.MONITOR_TYPE_NAME,F.ITEM_NAME,D.PRETREATMENT_FEE,D.TEST_ANSY_FEE,D.TEST_NUM,D.FREQ,D.TEST_POINT_NUM,D.TEST_POWER_PRICE,D.TEST_PRICE,D.FEE_COUNT,A.PROJECT_NAME,A.TEST_TYPES,G.TEST_FEE,G.ATT_FEE,G.BUDGET from 
                            T_MIS_CONTRACT A INNER JOIN T_MIS_CONTRACT_POINT B on(A.ID=B.CONTRACT_ID)
                            INNER JOIN T_MIS_CONTRACT_POINTITEM C on(B.ID=C.CONTRACT_POINT_ID)
                            INNER JOIN T_MIS_CONTRACT_TESTFEE D on(A.ID=D.CONTRACT_ID and C.ID=D.CONTRACT_POINTITEM_ID)
                            LEFT JOIN T_BASE_MONITOR_TYPE_INFO E on(B.MONITOR_ID=E.ID)
                            LEFT JOIN T_BASE_ITEM_INFO F on(C.ITEM_ID=F.ID)
                            LEFT JOIN T_MIS_CONTRACT_FEE G on(A.ID=G.CONTRACT_ID)
                            WHERE 1=1 ";
            if (!String.IsNullOrEmpty(tMisContract.ID))
            {
                strSQL += String.Format(" AND A.ID ='{0}'", tMisContract.ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取委托书附加费用信息 魏林 2014-02-25
        /// </summary>
        /// <param name="strContractID"></param>
        /// <returns></returns>
        public DataTable GetContractAttFeeData(string strContractID)
        {
            string strSQL = "select A.ID,B.ATT_FEE_ITEM,A.FEE from T_MIS_CONTRACT_ATTFEE A left join T_MIS_CONTRACT_ATTFEEITEM B on(A.ATT_FEE_ITEM_ID=B.ID) where A.CONTRACT_ID='" + strContractID + "'";

            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractVo tMisContract)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContract)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisContract.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContract.ID.ToString()));
                }
                //合同编号
                if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_CODE = '{0}'", tMisContract.CONTRACT_CODE.ToString()));
                }
                //委托年度
                if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR.ToString()));
                }
                //项目名称
                if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROJECT_NAME  LIKE '%{0}%'", tMisContract.PROJECT_NAME.ToString()));
                }
                //委托类型
                if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE.ToString()));
                }
                //委托类型
                if (!String.IsNullOrEmpty(tMisContract.BOOKTYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BOOKTYPE = '{0}'", tMisContract.BOOKTYPE.ToString()));
                }
                //监测类型,冗余字段，如：废水,废气,噪声
                if (!String.IsNullOrEmpty(tMisContract.TEST_TYPES.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEST_TYPES = '{0}'", tMisContract.TEST_TYPES.ToString()));
                }
                //报告类型
                if (!String.IsNullOrEmpty(tMisContract.TEST_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEST_TYPE = '{0}'", tMisContract.TEST_TYPE.ToString()));
                }
                //监测目的
                if (!String.IsNullOrEmpty(tMisContract.TEST_PURPOSE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEST_PURPOSE = '{0}'", tMisContract.TEST_PURPOSE.ToString()));
                }
                //委托企业ID
                if (!String.IsNullOrEmpty(tMisContract.CLIENT_COMPANY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CLIENT_COMPANY_ID = '{0}'", tMisContract.CLIENT_COMPANY_ID.ToString()));
                }
                //受检企业ID
                if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TESTED_COMPANY_ID = '{0}'", tMisContract.TESTED_COMPANY_ID.ToString()));
                }
                //要求完成日期
                if (!String.IsNullOrEmpty(tMisContract.ASKING_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ASKING_DATE = '{0}'", tMisContract.ASKING_DATE.ToString()));
                }
                //报告领取方式
                if (!String.IsNullOrEmpty(tMisContract.RPT_WAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RPT_WAY = '{0}'", tMisContract.RPT_WAY.ToString()));
                }
                //是否同意分包
                if (!String.IsNullOrEmpty(tMisContract.AGREE_OUTSOURCING.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AGREE_OUTSOURCING = '{0}'", tMisContract.AGREE_OUTSOURCING.ToString()));
                }
                //是否同意使用的监测方法
                if (!String.IsNullOrEmpty(tMisContract.AGREE_METHOD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AGREE_METHOD = '{0}'", tMisContract.AGREE_METHOD.ToString()));
                }
                //是否同意使用非标准方法
                if (!String.IsNullOrEmpty(tMisContract.AGREE_NONSTANDARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AGREE_NONSTANDARD = '{0}'", tMisContract.AGREE_NONSTANDARD.ToString()));
                }
                //是否同意其他
                if (!String.IsNullOrEmpty(tMisContract.AGREE_OTHER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AGREE_OTHER = '{0}'", tMisContract.AGREE_OTHER.ToString()));
                }
                //样品来源,1,抽样，2，自送样
                if (!String.IsNullOrEmpty(tMisContract.SAMPLE_SOURCE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_SOURCE = '{0}'", tMisContract.SAMPLE_SOURCE.ToString()));
                }
                //送样人
                if (!String.IsNullOrEmpty(tMisContract.SAMPLE_SEND_MAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_SEND_MAN = '{0}'", tMisContract.SAMPLE_SEND_MAN.ToString()));
                }
                //接样人ID
                if (!String.IsNullOrEmpty(tMisContract.SAMPLE_ACCEPTER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ACCEPTER_ID = '{0}'", tMisContract.SAMPLE_ACCEPTER_ID.ToString()));
                }
                //送样频次
                if (!String.IsNullOrEmpty(tMisContract.SAMPLE_FREQ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_FREQ = '{0}'", tMisContract.SAMPLE_FREQ.ToString()));
                }
                //项目负责人ID
                if (!String.IsNullOrEmpty(tMisContract.PROJECT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROJECT_ID = '{0}'", tMisContract.PROJECT_ID.ToString()));
                }
                //状态
                if (!String.IsNullOrEmpty(tMisContract.STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STATE = '{0}'", tMisContract.STATE.ToString()));
                }
                //委托书状态
                if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_STATUS = '{0}'", tMisContract.CONTRACT_STATUS.ToString()));
                }
                //是否为快捷录入标识 默认为0
                if (!String.IsNullOrEmpty(tMisContract.ISQUICKLY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ISQUICKLY = '{0}'", tMisContract.ISQUICKLY.ToString()));
                }
                //所需提供资料
                if (!String.IsNullOrEmpty(tMisContract.PROVIDE_DATA.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVIDE_DATA = '{0}'", tMisContract.PROVIDE_DATA.ToString()));
                }
                //委托方其他要求
                if (!String.IsNullOrEmpty(tMisContract.OTHER_ASKING.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OTHER_ASKING = '{0}'", tMisContract.OTHER_ASKING.ToString()));
                }
                //监测依据
                if (!String.IsNullOrEmpty(tMisContract.MONITOR_ACCORDING.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ACCORDING = '{0}'", tMisContract.MONITOR_ACCORDING.ToString()));
                }
                //质控要求
                if (!String.IsNullOrEmpty(tMisContract.QC_STEP.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_STEP = '{0}'", tMisContract.QC_STEP.ToString()));
                }

                //质控样设置
                if (!String.IsNullOrEmpty(tMisContract.QCRULE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QCRULE = '{0}'", tMisContract.QCRULE.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisContract.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContract.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisContract.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContract.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisContract.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContract.REMARK4.ToString()));
                }
                //备注5
                if (tMisContract.REMARK5!=null&&!String.IsNullOrEmpty(tMisContract.REMARK5.ToString().Trim()))//by lhm
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContract.REMARK5.ToString()));
                }
                //CCFLOW_ID1
                if (!String.IsNullOrEmpty(tMisContract.CCFLOW_ID1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID1 = '{0}'", tMisContract.CCFLOW_ID1.ToString()));
                }
                //CCFLOW_ID2
                if (!String.IsNullOrEmpty(tMisContract.CCFLOW_ID2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID2 = '{0}'", tMisContract.CCFLOW_ID2.ToString()));
                }
                //CCFLOW_ID3
                if (!String.IsNullOrEmpty(tMisContract.CCFLOW_ID3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID3 = '{0}'", tMisContract.CCFLOW_ID3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
