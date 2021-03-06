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
    /// 功能：委托书缴费表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractFeeAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractFeeVo tMisContractFee)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_FEE " + this.BuildWhereStatement(tMisContractFee);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// 何海亮修改
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountByHHL(TMisContractFeeVo tMisContractFee)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_FEE " + this.BuildWhereStatement(tMisContractFee);
            strSQL += "and CONTRACT_ID IS NOT NULL";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractFeeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_FEE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisContractFeeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractFee">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractFeeVo Details(TMisContractFeeVo tMisContractFee)
        {
           string strSQL = String.Format("select * from  T_MIS_CONTRACT_FEE " + this.BuildWhereStatement(tMisContractFee));
           return SqlHelper.ExecuteObject(new TMisContractFeeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractFeeVo> SelectByObject(TMisContractFeeVo tMisContractFee, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_FEE " + this.BuildWhereStatement(tMisContractFee));
            return SqlHelper.ExecuteObjectList(tMisContractFee, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractFeeVo tMisContractFee, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_CONTRACT_FEE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractFee));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable
        /// 何海亮修改
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableCreateByHHL(TMisContractFeeVo tMisContractFee, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_CONTRACT_FEE {0}  and CONTRACT_ID IS NOT NULL";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractFee));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractFee"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractFeeVo tMisContractFee)
        {
            string strSQL = "select * from T_MIS_CONTRACT_FEE " + this.BuildWhereStatement(tMisContractFee);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <returns></returns>
        public TMisContractFeeVo SelectByObject(TMisContractFeeVo tMisContractFee)
        {
            string strSQL = "select * from T_MIS_CONTRACT_FEE " + this.BuildWhereStatement(tMisContractFee);
            return SqlHelper.ExecuteObject(new TMisContractFeeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractFeeVo tMisContractFee)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractFee, TMisContractFeeVo.T_MIS_CONTRACT_FEE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractFee">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractFeeVo tMisContractFee)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractFee, TMisContractFeeVo.T_MIS_CONTRACT_FEE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractFee.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractFee_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractFee_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractFeeVo tMisContractFee_UpdateSet, TMisContractFeeVo tMisContractFee_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractFee_UpdateSet, TMisContractFeeVo.T_MIS_CONTRACT_FEE_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractFee_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_FEE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractFeeVo tMisContractFee)
        {
            string strSQL = "delete from T_MIS_CONTRACT_FEE ";
	    strSQL += this.BuildWhereStatement(tMisContractFee);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获取统计缴费信息
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="tMisContractFee"></param>
        /// <returns></returns>
        public DataTable SelectTableForContractFree(TMisContractVo tMisContract, TMisContractFeeVo tMisContractFee,bool flag)
        {
            string StartDate = "", EndDate = "";
            string strSQL = String.Format(@"SELECT ROW_NUMBER() OVER(ORDER BY A.ID) AS ROWNUM,A.ID,A.CONTRACT_CODE,A.PROJECT_NAME,A.CONTRACT_YEAR,A.ASKING_DATE,B.TEST_FEE,B.ATT_FEE,BUDGET,
CONVERT(DECIMAL(18,2),CASE WHEN B.INCOME IS NULL THEN '0' WHEN B.INCOME='NULL' THEN '0' ELSE B.INCOME END) AS INCOME,
CASE WHEN B.IF_PAY IS NULL THEN '未缴费' WHEN B.IF_PAY='0' THEN '未缴费' ELSE '已缴费' END AS IF_PAY,
C.COMPANY_NAME,D.DICT_TEXT AS CONTRACT_TYPE FROM T_MIS_CONTRACT A 
LEFT JOIN T_MIS_CONTRACT_FEE B ON A.ID=B.CONTRACT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY C ON C.ID=TESTED_COMPANY_ID
LEFT JOIN T_SYS_DICT D ON D.DICT_CODE=A.CONTRACT_TYPE AND D.DICT_TYPE='Contract_Type'
WHERE A.CONTRACT_STATUS!='0'");
            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME)) {
                strSQL += String.Format(" AND A.PROJECT_NAME",tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND A.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }

            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND A.CONTRACT_TYPE='{0}'", tMisContract.CONTRACT_TYPE);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, tMisContract.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && !String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisContract.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisContract.REMARK3);
                }

                strSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            if (flag)
            {
                if (!String.IsNullOrEmpty(tMisContractFee.IF_PAY) && tMisContractFee.IF_PAY == "1")
                {
                    strSQL += String.Format(" AND B.IF_PAY='{0}'", tMisContractFee.IF_PAY);
                }
                else
                {
                    strSQL += String.Format(" AND (B.IF_PAY IS NULL or B.IF_PAY='0')");
                }
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 获取统计缴费信息记录数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="tMisContractFee"></param>
        /// <returns></returns>
        public DataTable SelectTableForContractFreeCount(TMisContractVo tMisContract, TMisContractFeeVo tMisContractFee)
        {
            string StartDate = "", EndDate = "";
            string strSQL = String.Format(@"SELECT CASE WHEN B.IF_PAY IS NULL THEN '未缴费' WHEN B.IF_PAY='0' THEN '未缴费' ELSE '已缴费' END AS FINISHTYPE,COUNT(*) AS FINISHSUM FROM T_MIS_CONTRACT A  
LEFT JOIN T_MIS_CONTRACT_FEE B ON A.ID=B.CONTRACT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY C ON C.ID=TESTED_COMPANY_ID
LEFT JOIN T_SYS_DICT D ON D.DICT_CODE=A.CONTRACT_TYPE AND D.DICT_TYPE='Contract_Type'
WHERE A.CONTRACT_STATUS!='0'");
            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND A.PROJECT_NAME", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND A.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }

            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND A.CONTRACT_TYPE='{0}'", tMisContract.CONTRACT_TYPE);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, tMisContract.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && !String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisContract.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisContract.REMARK3);
                }

                strSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            strSQL += " GROUP BY B.IF_PAY";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取统计缴费金额相关信息记录数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="tMisContractFee"></param>
        /// <returns></returns>
        public DataTable SelectTableForContractFreeSum(TMisContractVo tMisContract, TMisContractFeeVo tMisContractFee)
        {
            string StartDate = "", EndDate = "";
            string strSQL = String.Format(@"SELECT CASE WHEN B.IF_PAY IS NULL THEN '未缴费' WHEN B.IF_PAY='0' THEN '未缴费' ELSE '已缴费' END AS FINISHTYPE,SUM(CONVERT(INT, CASE WHEN B.INCOME='NULL' THEN '0' WHEN B.INCOME IS NULL THEN '0' ELSE B.INCOME END)) AS FINISHSUM FROM T_MIS_CONTRACT A   
LEFT JOIN T_MIS_CONTRACT_FEE B ON A.ID=B.CONTRACT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY C ON C.ID=TESTED_COMPANY_ID
LEFT JOIN T_SYS_DICT D ON D.DICT_CODE=A.CONTRACT_TYPE AND D.DICT_TYPE='Contract_Type'
WHERE A.CONTRACT_STATUS!='0'");
            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND A.PROJECT_NAME", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND A.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }

            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND A.CONTRACT_TYPE='{0}'", tMisContract.CONTRACT_TYPE);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, tMisContract.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && !String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisContract.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisContract.REMARK3);
                }

                strSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            strSQL += " GROUP BY B.IF_PAY";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 返回企业缴费信息明细记录
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable SelectGetCompayFreeDetailTable(TMisContractVo tMisContract, int iIndex, int iCount)
        {
            string StartDate = "", EndDate = "";
            string TempSQL = "";
            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                TempSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK3))
            {
                //月度
                TempSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, tMisContract.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && !String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisContract.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisContract.REMARK3);
                }

                TempSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            string strSQL = String.Format(@"SELECT COMPANY_ID AS ID,COMPANY_NAME,
  MAX(CASE IF_PAY when '已缴费' THEN CONVERT(DECIMAL(18,2),PAYSUM) ELSE 0 end) PAYED,
  MAX(CASE IF_PAY when '未缴费' THEN CONVERT(DECIMAL(18,2),PAYSUM) ELSE 0 end) NOPAY
FROM (SELECT T.COMPANY_ID,T.COMPANY_NAME,T.PAYSUM,T.IF_PAY,SUM(PAYSUM) OVER(PARTITION BY T.COMPANY_ID ) AS SUMTOTAL FROM 
(
SELECT A.ID,A.CONTRACT_CODE,A.PROJECT_NAME,A.CONTRACT_YEAR,A.ASKING_DATE,B.TEST_FEE,B.ATT_FEE,BUDGET,CONVERT(DECIMAL(18,2),CASE WHEN B.INCOME IS NULL THEN '0' WHEN B.INCOME='NULL' THEN '0' ELSE B.INCOME END) AS INCOME,CASE WHEN B.IF_PAY IS NULL THEN '未缴费' WHEN B.IF_PAY='0' THEN '未缴费' ELSE '已缴费' END AS IF_PAY,
SUM(CONVERT(INT,CASE WHEN B.INCOME IS NULL THEN '0' WHEN B.INCOME='NULL' THEN '0' ELSE B.INCOME END)) OVER(PARTITION BY C.COMPANY_ID,B.IF_PAY) AS PAYSUM,
C.COMPANY_ID,C.COMPANY_NAME,D.DICT_TEXT AS CONTRACT_TYPE FROM T_MIS_CONTRACT A 
LEFT JOIN T_MIS_CONTRACT_FEE B ON A.ID=B.CONTRACT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY C ON C.ID=TESTED_COMPANY_ID
LEFT JOIN T_SYS_DICT D ON D.DICT_CODE=A.CONTRACT_TYPE AND D.DICT_TYPE='Contract_Type'
WHERE A.CONTRACT_STATUS!='0'  {0} ) T GROUP BY T.COMPANY_ID,T.COMPANY_NAME,T.PAYSUM,T.IF_PAY)tb
GROUP BY COMPANY_ID,COMPANY_NAME", TempSQL);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress( strSQL,iIndex,iCount));
        }


        /// <summary>
        /// 返回企业缴费信息明细记录数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public int SelectGetCompayFreeDetailTableCount(TMisContractVo tMisContract)
        {
            string StartDate = "", EndDate = "";
            string TempSQL = "";
            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                TempSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK3))
            {
                //月度
                TempSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, tMisContract.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && !String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisContract.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisContract.REMARK3);
                }

                TempSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            string strSQL = String.Format(@"SELECT COMPANY_ID AS ID,COMPANY_NAME,
  MAX(CASE IF_PAY when '已缴费' THEN CONVERT(DECIMAL(18,2),PAYSUM) ELSE 0 end) PAYED,
  MAX(CASE IF_PAY when '未缴费' THEN CONVERT(DECIMAL(18,2),PAYSUM) ELSE 0 end) NOPAY
FROM (SELECT T.COMPANY_ID,T.COMPANY_NAME,T.PAYSUM,T.IF_PAY,SUM(PAYSUM) OVER(PARTITION BY T.COMPANY_ID ) AS SUMTOTAL FROM 
(
SELECT A.ID,A.CONTRACT_CODE,A.PROJECT_NAME,A.CONTRACT_YEAR,A.ASKING_DATE,B.TEST_FEE,B.ATT_FEE,BUDGET,CONVERT(DECIMAL(18,2),CASE WHEN B.INCOME IS NULL THEN '0' WHEN B.INCOME='NULL' THEN '0' ELSE B.INCOME END) AS INCOME,CASE WHEN B.IF_PAY IS NULL THEN '未缴费' WHEN B.IF_PAY='0' THEN '未缴费' ELSE '已缴费' END AS IF_PAY,
SUM(CONVERT(INT,CASE WHEN B.INCOME IS NULL THEN '0' WHEN B.INCOME='NULL' THEN '0' ELSE B.INCOME END)) OVER(PARTITION BY C.COMPANY_ID,B.IF_PAY) AS PAYSUM,
C.COMPANY_ID,C.COMPANY_NAME,D.DICT_TEXT AS CONTRACT_TYPE FROM T_MIS_CONTRACT A 
LEFT JOIN T_MIS_CONTRACT_FEE B ON A.ID=B.CONTRACT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY C ON C.ID=TESTED_COMPANY_ID
LEFT JOIN T_SYS_DICT D ON D.DICT_CODE=A.CONTRACT_TYPE AND D.DICT_TYPE='Contract_Type'
WHERE A.CONTRACT_STATUS!='0'  {0} ) T GROUP BY T.COMPANY_ID,T.COMPANY_NAME,T.PAYSUM,T.IF_PAY)tb
GROUP BY COMPANY_ID,COMPANY_NAME", TempSQL);
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 获取企业收费记录的明细记录列表
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable SelectGetCompanyDetailListInfor(TMisContractVo tMisContract,int iIndex,int iCount) {

            string StartDate = "", EndDate = "";
            string strSQL = String.Format(@"SELECT A.ID,A.CONTRACT_CODE,A.PROJECT_NAME,A.CONTRACT_YEAR,A.ASKING_DATE,B.TEST_FEE,B.ATT_FEE,BUDGET,CONVERT(DECIMAL(18,2),CASE WHEN B.INCOME IS NULL THEN '0' WHEN B.INCOME='NULL' THEN '0' ELSE B.INCOME END) AS INCOME,CASE WHEN B.IF_PAY IS NULL THEN '未缴费' WHEN B.IF_PAY='0' THEN '未缴费' ELSE '已缴费' END AS IF_PAY,
SUM(CONVERT(INT,CASE WHEN B.INCOME IS NULL THEN '0' WHEN B.INCOME='NULL' THEN '0' ELSE B.INCOME END)) OVER(PARTITION BY C.COMPANY_ID,B.IF_PAY) AS PAYSUM,
C.COMPANY_ID,C.COMPANY_NAME,D.DICT_TEXT AS CONTRACT_TYPE FROM T_MIS_CONTRACT A 
LEFT JOIN T_MIS_CONTRACT_FEE B ON A.ID=B.CONTRACT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY C ON C.ID=TESTED_COMPANY_ID
LEFT JOIN T_SYS_DICT D ON D.DICT_CODE=A.CONTRACT_TYPE AND D.DICT_TYPE='Contract_Type'
WHERE A.CONTRACT_STATUS!='0' ");

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_ID='{0}'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, tMisContract.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && !String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisContract.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisContract.REMARK3);
                }

                strSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            return SqlHelper.ExecuteDataTable( BuildPagerExpress( strSQL,iIndex,iCount));
        }

        /// <summary>
        /// 获取企业收费记录的明细记录总数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public int SelectGetCompanyDetailListInforCount(TMisContractVo tMisContract)
        {

            string StartDate = "", EndDate = "";
            string strSQL = String.Format(@"SELECT A.ID,A.CONTRACT_CODE,A.PROJECT_NAME,A.CONTRACT_YEAR,A.ASKING_DATE,B.TEST_FEE,B.ATT_FEE,BUDGET,CONVERT(DECIMAL(18,2),CASE WHEN B.INCOME IS NULL THEN '0' WHEN B.INCOME='NULL' THEN '0' ELSE B.INCOME END) AS INCOME,CASE WHEN B.IF_PAY IS NULL THEN '未缴费' WHEN B.IF_PAY='0' THEN '未缴费' ELSE '已缴费' END AS IF_PAY,
SUM(CONVERT(INT,CASE WHEN B.INCOME IS NULL THEN '0' WHEN B.INCOME='NULL' THEN '0' ELSE B.INCOME END)) OVER(PARTITION BY C.COMPANY_ID,B.IF_PAY) AS PAYSUM,
C.COMPANY_ID,C.COMPANY_NAME,D.DICT_TEXT AS CONTRACT_TYPE FROM T_MIS_CONTRACT A 
LEFT JOIN T_MIS_CONTRACT_FEE B ON A.ID=B.CONTRACT_ID
LEFT JOIN dbo.T_MIS_CONTRACT_COMPANY C ON C.ID=TESTED_COMPANY_ID
LEFT JOIN T_SYS_DICT D ON D.DICT_CODE=A.CONTRACT_TYPE AND D.DICT_TYPE='Contract_Type'
WHERE A.CONTRACT_STATUS!='0' ");

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_ID='{0}'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, tMisContract.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && !String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisContract.REMARK3, tMisContract.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisContract.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisContract.REMARK4) && String.IsNullOrEmpty(tMisContract.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisContract.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisContract.REMARK3);
                }

                strSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractFee"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractFeeVo tMisContractFee)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractFee)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisContractFee.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractFee.ID.ToString()));
				}	
				//委托书ID
				if (!String.IsNullOrEmpty(tMisContractFee.CONTRACT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractFee.CONTRACT_ID.ToString()));
				}	
				//监测费用，监测费用明细总和
				if (!String.IsNullOrEmpty(tMisContractFee.TEST_FEE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TEST_FEE = '{0}'", tMisContractFee.TEST_FEE.ToString()));
				}	
				//附加总费用，附加费用明细总和
				if (!String.IsNullOrEmpty(tMisContractFee.ATT_FEE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATT_FEE = '{0}'", tMisContractFee.ATT_FEE.ToString()));
				}	
				//预算总费用，监测总费用+附加总费用
				if (!String.IsNullOrEmpty(tMisContractFee.BUDGET.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND BUDGET = '{0}'", tMisContractFee.BUDGET.ToString()));
				}	
				//实际收费
				if (!String.IsNullOrEmpty(tMisContractFee.INCOME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INCOME = '{0}'", tMisContractFee.INCOME.ToString()));
				}
                //是否缴费
				if (!String.IsNullOrEmpty(tMisContractFee.IF_PAY.ToString().Trim()))
				{
                    if (tMisContractFee.IF_PAY.ToString().Trim() == "0")
                    {
                        strWhereStatement.Append(string.Format(" AND (IF_PAY = '{0}' or IF_PAY IS NULL)", tMisContractFee.IF_PAY.ToString()));
                    }
                    else
                        strWhereStatement.Append(string.Format(" AND IF_PAY = '{0}'", tMisContractFee.IF_PAY.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisContractFee.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractFee.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisContractFee.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractFee.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisContractFee.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractFee.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisContractFee.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractFee.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisContractFee.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractFee.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
