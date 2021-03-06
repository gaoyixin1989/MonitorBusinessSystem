using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject;
using i3.ValueObject.Channels.Mis.Contract;
namespace i3.DataAccess.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：结果分析执行表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorResultAppAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_RESULT_APP " + this.BuildWhereStatement(tMisMonitorResultApp);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorResultAppVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT_APP  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorResultAppVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorResultAppVo Details(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT_APP " + this.BuildWhereStatement(tMisMonitorResultApp));
            return SqlHelper.ExecuteObject(new TMisMonitorResultAppVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorResultAppVo> SelectByObject(TMisMonitorResultAppVo tMisMonitorResultApp, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT_APP " + this.BuildWhereStatement(tMisMonitorResultApp));
            return SqlHelper.ExecuteObjectList(tMisMonitorResultApp, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorResultAppVo tMisMonitorResultApp, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_RESULT_APP {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorResultApp));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResultApp"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            string strSQL = "select * from T_MIS_MONITOR_RESULT_APP " + this.BuildWhereStatement(tMisMonitorResultApp);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象</param>
        /// <returns></returns>
        public TMisMonitorResultAppVo SelectByObject(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            string strSQL = "select * from T_MIS_MONITOR_RESULT_APP " + this.BuildWhereStatement(tMisMonitorResultApp);
            return SqlHelper.ExecuteObject(new TMisMonitorResultAppVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorResultApp, TMisMonitorResultAppVo.T_MIS_MONITOR_RESULT_APP_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultApp">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorResultApp, TMisMonitorResultAppVo.T_MIS_MONITOR_RESULT_APP_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorResultApp.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultApp_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorResultApp_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultAppVo tMisMonitorResultApp_UpdateSet, TMisMonitorResultAppVo tMisMonitorResultApp_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorResultApp_UpdateSet, TMisMonitorResultAppVo.T_MIS_MONITOR_RESULT_APP_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorResultApp_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_RESULT_APP where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            string strSQL = "delete from T_MIS_MONITOR_RESULT_APP ";
            strSQL += this.BuildWhereStatement(tMisMonitorResultApp);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获得分析执行表信息
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable SelectByTableByTaskID(string strTaskID, string strItemType)
        {
            string strSQL = @"select app.*,u1.REAL_NAME as HEADER,u2.REAL_NAME as ANALYSIER,result.ITEM_ID
                                            from T_MIS_MONITOR_RESULT_APP app
                                            left join T_SYS_USER u1 on app.HEAD_USERID=u1.ID
                                            left join T_SYS_USER u2 on app.ASSISTANT_USERID=u2.ID
                                            left join T_MIS_MONITOR_RESULT result on app.RESULT_ID=result.ID
                                            where result.SAMPLE_ID in 
                                            (select ID from T_MIS_MONITOR_SAMPLE_INFO where SUBTASK_ID in 
                                            (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' {1}))";
            if (!string.IsNullOrEmpty(strItemType))
            {
                strSQL = string.Format(strSQL, strTaskID, string.Format(" and CHARINDEX(MONITOR_ID,'{0}')>0", strItemType));
            }
            else
            {
                strSQL = string.Format(strSQL, strTaskID, "");
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取完成及时率列表
        /// </summary>
        /// <param name="type">true,获取超时的，false获取正常完成的</param>
        /// <returns></returns>
        public DataTable GetAnalyseResultFinished(TMisMonitorResultAppVo tMisMonitorResultApp, string strContract_Code, string strDept, string strUserName, bool type)
        {
            string StartDate = "", EndDate = "";
            string strSQL = String.Format("SELECT ROW_NUMBER() OVER(ORDER BY A.ID) AS ROWNUM,A.ID, CONVERT(VARCHAR(100), A.ASKING_DATE,23) AS ASKING_DATE ,CONVERT(VARCHAR(100), A.FINISH_DATE,23) AS FINISH_DATE,B.RESULT_STATUS,E.PROJECT_NAME,E.CONTRACT_CODE,H.REPORT_CODE,C.SAMPLE_CODE,I.ITEM_NAME,J.REAL_NAME,M.DICT_TEXT AS DEPT_NAME" +
                                     " FROM T_MIS_MONITOR_RESULT_APP A " +
                                     " LEFT JOIN  T_MIS_MONITOR_RESULT B ON B.ID=A.RESULT_ID " +
                                     " LEFT JOIN T_MIS_MONITOR_SAMPLE_INFO C ON C.ID=B.SAMPLE_ID" +
                                     " LEFT JOIN T_MIS_MONITOR_SUBTASK D ON D.ID=C.SUBTASK_ID" +
                                     " LEFT JOIN T_MIS_MONITOR_TASK E ON E.ID=D.TASK_ID" +
                                     " LEFT JOIN dbo.T_MIS_CONTRACT F ON F.ID=E.CONTRACT_ID" +
                                     " LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON G.ID=E.TESTED_COMPANY_ID" +
                                     " LEFT JOIN T_MIS_MONITOR_REPORT H ON H.TASK_ID=E.ID" +
                                     " LEFT JOIN dbo.T_BASE_ITEM_INFO I ON I.ID=B.ITEM_ID" +
                                     " LEFT JOIN dbo.T_SYS_USER J ON J.ID=A.HEAD_USERID" +
                                     " LEFT JOIN dbo.T_SYS_USER_POST K ON K.USER_ID=J.ID" +
                                     " LEFT JOIN dbo.T_SYS_POST L ON L.ID=K.POST_ID" +
                                     " LEFT JOIN T_SYS_DICT M ON M.DICT_CODE=L.POST_DEPT_ID  AND M.DICT_TYPE='dept' " +
                                     " WHERE B.RESULT_STATUS NOT IN ('01','02','00') AND  B.RESULT_STATUS IS NOT NULL AND A.ASKING_DATE IS NOT NULL");
            if (type)
            {
                //已正常完成的
                strSQL += String.Format("  AND CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)>=CONVERT(DATETIME, CONVERT(VARCHAR(100), A.FINISH_DATE,23),111)");
            }
            else
            {
                //超时完成的
                strSQL += String.Format("  AND CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)<CONVERT(DATETIME, CONVERT(VARCHAR(100), A.FINISH_DATE,23),111)");
            }

            if (!String.IsNullOrEmpty(strContract_Code))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE='{0}'", strContract_Code);
            }
            if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK4) && String.IsNullOrEmpty(tMisMonitorResultApp.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorResultApp.REMARK3, tMisMonitorResultApp.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorResultApp.REMARK3, tMisMonitorResultApp.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisMonitorResultApp.REMARK4) && !String.IsNullOrEmpty(tMisMonitorResultApp.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorResultApp.REMARK3, tMisMonitorResultApp.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorResultApp.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisMonitorResultApp.REMARK4) && String.IsNullOrEmpty(tMisMonitorResultApp.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisMonitorResultApp.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisMonitorResultApp.REMARK3);
                }

                strSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            //执行部门
            if (!String.IsNullOrEmpty(strDept))
            {
                strSQL += String.Format(" AND M.DICT_TEXT LIKE '%{0}%'", strDept);
            }

            //执行人
            if (!String.IsNullOrEmpty(strUserName))
            {
                strSQL += String.Format(" AND J.REAL_NAME LIKE '%{0}%'", strUserName);
            }
            strSQL += String.Format(" ORDER BY A.ASKING_DATE");

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取完成及时率
        /// </summary>
        /// <param name="type">true,h获取超时的，false获取正常完成的</param>
        /// <returns></returns>
        public DataTable GetAnalyseResultFinishedCount(TMisMonitorResultAppVo tMisMonitorResultApp, string strContract_Code, string strDept, string strUserName)
        {
            string StartDate = "", EndDate = "";
            string strSQL = @"SELECT CASE WHEN  CONVERT(DATETIME, CONVERT(varchar(100), A.ASKING_DATE,23),111)>=CONVERT(DATETIME, CONVERT(varchar(100), A.FINISH_DATE,23),111) THEN '正常完成' ELSE '超时完成' END AS FINISHTYPE,COUNT(*) AS FINISHSUM
                                    FROM T_MIS_MONITOR_RESULT_APP A 
                                    LEFT JOIN  T_MIS_MONITOR_RESULT B ON B.ID=A.RESULT_ID 
                                    LEFT JOIN T_MIS_MONITOR_SAMPLE_INFO C ON C.ID=B.SAMPLE_ID
                                    LEFT JOIN T_MIS_MONITOR_SUBTASK D ON D.ID=C.SUBTASK_ID
                                    LEFT JOIN T_MIS_MONITOR_TASK E ON E.ID=D.TASK_ID
                                    LEFT JOIN dbo.T_MIS_CONTRACT F ON F.ID=E.CONTRACT_ID
                                    LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON G.ID=E.TESTED_COMPANY_ID
                                    LEFT JOIN T_MIS_MONITOR_REPORT H ON H.TASK_ID=E.ID
                                    LEFT JOIN dbo.T_BASE_ITEM_INFO I ON I.ID=B.ITEM_ID
                                    LEFT JOIN dbo.T_SYS_USER J ON J.ID=A.HEAD_USERID
                                    LEFT JOIN dbo.T_SYS_USER_POST K ON K.USER_ID=J.ID
                                    LEFT JOIN dbo.T_SYS_POST L ON L.ID=K.POST_ID
                                    LEFT JOIN T_SYS_DICT M ON M.DICT_CODE=L.POST_DEPT_ID
                                    WHERE B.RESULT_STATUS NOT IN ('01','02','00') AND  B.RESULT_STATUS IS NOT NULL AND A.ASKING_DATE IS NOT NULL";
            if (!String.IsNullOrEmpty(strContract_Code))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE='{0}'", strContract_Code);
            }
            if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK3))
            {
                //月度
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), A.ASKING_DATE,23),111)  ");
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK4) && String.IsNullOrEmpty(tMisMonitorResultApp.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorResultApp.REMARK3, tMisMonitorResultApp.REMARK4);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorResultApp.REMARK3, tMisMonitorResultApp.REMARK4);
                }

                //季度
                if (String.IsNullOrEmpty(tMisMonitorResultApp.REMARK4) && !String.IsNullOrEmpty(tMisMonitorResultApp.REMARK5))
                {
                    StartDate = String.Format(" {0}-{1}-01", tMisMonitorResultApp.REMARK3, tMisMonitorResultApp.REMARK5);
                    DateTime strMonth = DateTime.Parse(StartDate);
                    EndDate = String.Format(" {0}-{1}-31", tMisMonitorResultApp.REMARK3, strMonth.AddMonths(+2).Month.ToString());
                }
                //年度
                if (String.IsNullOrEmpty(tMisMonitorResultApp.REMARK4) && String.IsNullOrEmpty(tMisMonitorResultApp.REMARK5))
                {
                    StartDate = String.Format(" {0}-01-01", tMisMonitorResultApp.REMARK3);
                    EndDate = String.Format(" {0}-12-31", tMisMonitorResultApp.REMARK3);
                }

                strSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }
            //执行部门
            if (!String.IsNullOrEmpty(strDept))
            {
                strSQL += String.Format(" AND M.DICT_TEXT LIKE '%{0}%'", strDept);
            }

            //执行人
            if (!String.IsNullOrEmpty(strUserName))
            {
                strSQL += String.Format(" AND J.REAL_NAME LIKE '%{0}%'", strUserName);
            }
            strSQL += "  GROUP BY A.ASKING_DATE,A.FINISH_DATE";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取指定年份、企业、监测类别、点位、监测项目等数据，构造曲线图表 胡方扬 2013-03-07
        /// </summary>
        /// <param name="tMisMonitorResultApp"></param>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable GetPollutantSourceReport(TMisMonitorResultAppVo tMisMonitorResultApp, TMisContractVo tMisContract)
        {
            string StartDate = "", EndDate = "";
            string strSQL = String.Format(@"SELECT ROW_NUMBER() OVER(ORDER BY A.ID) AS ROWNUM,A.ID, B.ID AS RESULT_ID,
B.RESULT_STATUS,B.ITEM_RESULT,C.ID AS SAMPLE_ID,C.SAMPLE_CODE,CONVERT(VARCHAR(100), D.SAMPLE_FINISH_DATE,23) AS SAMPLE_FINISH_DATE,E.ID AS TASK_ID,F.PROJECT_NAME,F.CONTRACT_CODE,H.REPORT_CODE,G.COMPANY_NAME,
I.ID AS ITEM_ID,I.ITEM_NAME,J.REAL_NAME,M.DICT_TEXT AS DEPT_NAME,N.POINT_ID,N.POINT_NAME,N.MONITOR_ID,
O.ST_UPPER,O.ST_LOWER,O.CONDITION_ID,CONVERT (DECIMAL(18,1),CASE WHEN P.DISCHARGE_UPPER IS NULL THEN '0' WHEN P.DISCHARGE_UPPER='NULL' THEN '0' ELSE  P.DISCHARGE_UPPER END) AS DISCHARGE_UPPER,CONVERT(DECIMAL(18,1),CASE WHEN P.DISCHARGE_LOWER IS NULL THEN '0' WHEN P.DISCHARGE_LOWER='NULL' THEN '0' ELSE  P.DISCHARGE_LOWER END) AS DISCHARGE_LOWER,QJVALUE=DISCHARGE_UPPER+'~'+DISCHARGE_LOWER,R.DICT_TEXT AS UNITNAME
FROM T_MIS_MONITOR_RESULT_APP A  
LEFT JOIN  T_MIS_MONITOR_RESULT B ON B.ID=A.RESULT_ID AND B.ITEM_RESULT IS NOT NULL  AND QC_TYPE='0'
LEFT JOIN T_MIS_MONITOR_SAMPLE_INFO C ON C.ID=B.SAMPLE_ID 
LEFT JOIN T_MIS_MONITOR_SUBTASK D ON D.ID=C.SUBTASK_ID 
LEFT JOIN T_MIS_MONITOR_TASK E ON E.ID=D.TASK_ID 
INNER JOIN dbo.T_MIS_CONTRACT F ON F.ID=E.CONTRACT_ID 
LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON G.ID=E.TESTED_COMPANY_ID 
LEFT JOIN T_MIS_MONITOR_REPORT H ON H.TASK_ID=E.ID 
LEFT JOIN dbo.T_BASE_ITEM_INFO I ON I.ID=B.ITEM_ID 
LEFT JOIN dbo.T_SYS_USER J ON J.ID=A.HEAD_USERID 
LEFT JOIN dbo.T_SYS_USER_POST K ON K.USER_ID=J.ID 
LEFT JOIN dbo.T_SYS_POST L ON L.ID=K.POST_ID 
LEFT JOIN T_SYS_DICT M ON M.DICT_CODE=L.POST_DEPT_ID  AND M.DICT_TYPE='dept'  
LEFT JOIN T_MIS_MONITOR_TASK_POINT N ON N.TASK_ID=E.ID
LEFT JOIN T_MIS_MONITOR_TASK_ITEM O ON O.TASK_POINT_ID=N.ID AND O.ITEM_ID=I.ID
LEFT JOIN dbo.T_BASE_EVALUATION_CON_ITEM P ON P.CONDITION_ID=O.CONDITION_ID AND P.ITEM_ID=O.ITEM_ID
LEFT JOIN T_SYS_DICT R ON R.DICT_CODE=P.UNIT AND R.DICT_TYPE='item_unit' ");
            if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK3) || !String.IsNullOrEmpty(tMisMonitorResultApp.REMARK4) || !String.IsNullOrEmpty(tMisMonitorResultApp.REMARK5)
                || !String.IsNullOrEmpty(tMisContract.REMARK3) || !String.IsNullOrEmpty(tMisContract.REMARK4) || !String.IsNullOrEmpty(tMisContract.REMARK5))
            {
                strSQL += " WHERE 1=1 AND A.ASKING_DATE IS NOT NULL";
            }
            else
            {
                strSQL += " WHERE 1=2";
            }
            //企业名称
            if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK3))
            {
                strSQL += String.Format(" AND G.COMPANY_NAME LIKE '%{0}%'", tMisMonitorResultApp.REMARK3);
            }
            //监测项目ID
            if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK4))
            {
                strSQL += String.Format(" AND I.ITEM_NAME LIKE '%{0}%'", tMisMonitorResultApp.REMARK4);
            }
            //监测点位ID
            if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK5))
            {
                strSQL += String.Format(" AND N.POINT_NAME LIKE '%{0}%'", tMisMonitorResultApp.REMARK5);
            }

            //监测类别
            if (!String.IsNullOrEmpty(tMisContract.REMARK3))
            {
                strSQL += String.Format(" AND N.MONITOR_ID = '{0}'", tMisContract.REMARK3);
            }
            //委托类型
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND F.CONTRACT_TYPE='{0}'", tMisContract.REMARK4);
            }
            //采样年份
            if (!String.IsNullOrEmpty(tMisContract.REMARK5))
            {
                strSQL += String.Format(" AND  CONVERT(DATETIME, CONVERT(VARCHAR(100), D.SAMPLE_FINISH_DATE,23),111)  ");
                StartDate = String.Format(" {0}-01-01", tMisContract.REMARK5);
                EndDate = String.Format(" {0}-12-31", tMisContract.REMARK5);

                strSQL += String.Format(" BETWEEN  CONVERT(DATETIME, CONVERT(VARCHAR(100),'{0}' ,23),111) AND CONVERT(DATETIME, CONVERT(VARCHAR(100), '{1}' ,23),111) ", StartDate, EndDate);
            }

            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region//分析及时率统计查询行数
        public int GetResultCount(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime)
        {
            StringBuilder sb = new StringBuilder(50000);
            sb.Append(" select  count(*) ");
            sb.Append(" from T_MIS_MONITOR_RESULT_APP a   left join T_MIS_MONITOR_RESULT b on a.RESULT_ID=b.id ");
            sb.Append(" left join T_MIS_MONITOR_SAMPLE_INFO c on  b.SAMPLE_ID=c.id ");
            sb.Append(" left join  T_MIS_MONITOR_TASK d on c.SAMPLE_NAME=d.SAMPLE_SOURCE ");
            sb.Append(" left join T_SYS_USER e on a.HEAD_USERID=e.id");
            sb.Append(" left join T_BASE_ITEM_INFO f on b.ITEM_ID=f.id  where 1=1 ");
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                sb.Append("  and CONVERT(varchar(11),a.FINISH_DATE,120) between '" + StartTime + "' and '" + EndTime + "'");
            }
            if (!string.IsNullOrEmpty(HEAD_USERID))//分析负责人
            {
                sb.Append(" and e.REAL_NAME='" + HEAD_USERID + "'");
            }
            if (!string.IsNullOrEmpty(TICKET_NUM))//任务单号
            {
                sb.Append(" and d.TICKET_NUM='" + TICKET_NUM + "'");
            }
            if (!string.IsNullOrEmpty(OverTime))////是否超期完成{0：全部；1：是(实际完成时间超过要求完成时间)；2：否（实际完成时间没有超过要求完成时间）}
            {
                if (OverTime.Equals("1"))
                {
                    sb.Append(" and CONVERT(varchar(11),a.FINISH_DATE,120)>CONVERT(varchar,a.ASKING_DATE,120) ");
                }
                else if (OverTime.Equals("2"))
                {
                    sb.Append(" and CONVERT(varchar(11),a.FINISH_DATE,120)<CONVERT(varchar,a.ASKING_DATE,120) ");
                }
            }
            return int.Parse(SqlHelper.ExecuteScalar(sb.ToString()).ToString());
        }
        #endregion

        #region//分析及时率统计查询
        public DataTable SearchDataEx(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime, int iIndex, int iCount)
        {
            StringBuilder sb = new StringBuilder(50000);
            sb.Append(" select  a.id,CONVERT(varchar(11),a.ASKING_DATE,120)  as ASKING_DATE,CONVERT(varchar(11),a.FINISH_DATE,120)  as FINISH_DATE,f.REAL_NAME,g.ITEM_NAME,c.SAMPLE_NAME,e.TICKET_NUM,  ");
            sb.Append(" (case  when CONVERT(varchar(11),a.FINISH_DATE,120)>CONVERT(varchar(11),a.ASKING_DATE,120) then '是' when CONVERT(varchar(11),a.FINISH_DATE,120)<CONVERT(varchar(11),a.ASKING_DATE,120) then '否'  end) as Is_OverTime ");
            sb.Append(" from T_MIS_MONITOR_RESULT_APP a   left join T_MIS_MONITOR_RESULT b on a.RESULT_ID=b.id ");//结果分析执行表,T_MIS_MONITOR_RESULT
            sb.Append(" left join T_MIS_MONITOR_SAMPLE_INFO c on  b.SAMPLE_ID=c.id ");//T_MIS_MONITOR_SAMPLE_INFO
            sb.Append(" left join T_MIS_MONITOR_SUBTASK d on c.SUBTASK_ID=d.id ");//监测子任务
            sb.Append(" left join  T_MIS_MONITOR_TASK e on d.TASK_ID=e.id ");//监测任务企业信息
            sb.Append(" left join T_SYS_USER f on a.HEAD_USERID=f.id  ");//用户表
            sb.Append("left join T_BASE_ITEM_INFO g on b.ITEM_ID=g.id where 1=1 ");//监测项目表 
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                sb.Append("  and CONVERT(varchar(11),a.FINISH_DATE,120) between '" + StartTime + "' and '" + EndTime + "'");
            }
            if (!string.IsNullOrEmpty(HEAD_USERID))//分析负责人
            {
                sb.Append(" and f.REAL_NAME='" + HEAD_USERID + "'");
            }
            if (!string.IsNullOrEmpty(TICKET_NUM))//任务单号
            {
                sb.Append(" and e.TICKET_NUM='" + TICKET_NUM + "'");
            }
            if (!string.IsNullOrEmpty(OverTime))////是否超期完成{0：全部；1：是(实际完成时间超过要求完成时间)；2：否（实际完成时间没有超过要求完成时间）}
            {
                if (OverTime.Equals("1"))
                {
                    sb.Append(" and CONVERT(varchar(11),a.FINISH_DATE,120)>CONVERT(varchar,a.ASKING_DATE,120) ");
                }
                else if (OverTime.Equals("2"))
                {
                    sb.Append(" and CONVERT(varchar(11),a.FINISH_DATE,120)<CONVERT(varchar,a.ASKING_DATE,120) ");
                }
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(sb.ToString(), iIndex, iCount));
        }
        #endregion

        #region//使用记录报表行数
        public int GetUseRecordCount(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime,string Name)
        {
            StringBuilder sb = new StringBuilder(50000);
            sb.Append(" select  count(*) ");
            sb.Append(" from T_MIS_MONITOR_RESULT_APP a   left join T_MIS_MONITOR_RESULT b on a.RESULT_ID=b.id ");
            sb.Append(" left join T_MIS_MONITOR_SAMPLE_INFO c on  b.SAMPLE_ID=c.id ");
            sb.Append(" left join  T_MIS_MONITOR_TASK d on c.SAMPLE_NAME=d.SAMPLE_SOURCE ");
            sb.Append(" left join T_SYS_USER e on a.HEAD_USERID=e.id");
            sb.Append(" left join T_BASE_ITEM_INFO f on b.ITEM_ID=f.id   ");
            sb.Append(" left join T_BASE_ITEM_ANALYSIS h on b.ANALYSIS_METHOD_ID=h.ANALYSIS_METHOD_ID");
            sb.Append(" left join T_BASE_APPARATUS_INFO j on h.INSTRUMENT_ID=j.id where 1=1 ");
            if (!string.IsNullOrEmpty(HEAD_USERID))//分析负责人
            {
                sb.Append(" and e.REAL_NAME='" + HEAD_USERID + "'");
            }
            if (!string.IsNullOrEmpty(TICKET_NUM))//任务单号
            {
                sb.Append(" and d.TICKET_NUM='" + TICKET_NUM + "'");
            }
            if (!string.IsNullOrEmpty(Name))//仪器名称
            {
                sb.Append(" and j.NAME='" + Name + "'");
            }
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                sb.Append("  and ((('" + StartTime + "' between CONVERT(varchar(11),b.APPARTUS_START_TIME,120) and  CONVERT(varchar(11),b.APPARTUS_END_TIME,120) ) and ('" + EndTime + "'between CONVERT(varchar(11),b.APPARTUS_START_TIME,120) and  CONVERT(varchar(11),b.APPARTUS_END_TIME,120) )) or  ");
                sb.Append(" ((CONVERT(varchar(11),b.APPARTUS_START_TIME,120) between '" + StartTime + "' and '" + EndTime + "') and ( CONVERT(varchar(11),b.APPARTUS_END_TIME,120)  between '" + StartTime + "' and '" + EndTime + "')) or   ");
                sb.Append(" (CONVERT(varchar(11),b.APPARTUS_START_TIME,120) between '" + StartTime + "' and '" + EndTime + "') or ");
                sb.Append("  ( CONVERT(varchar(11),b.APPARTUS_END_TIME,120)  between '" + StartTime + "' and '" + EndTime + "')) ");
            }
            if (!string.IsNullOrEmpty(OverTime))////是否超期完成{0：全部；1代表是，仪器开始/结束时间被包含在（最近检定/校准时间和到期检定/校准时间）内；2：代表否，反之}
            {
                if (OverTime.Equals("1"))
                {
                    sb.Append(" and ((CONVERT(varchar(11),b.APPARTUS_START_TIME,120)>=j.BEGIN_TIME and  CONVERT(varchar(11),b.APPARTUS_END_TIME,120) <=j.END_TIME) ");
                }
                else if (OverTime.Equals("2"))
                {
                    sb.Append(" and ((CONVERT(varchar(11),b.APPARTUS_START_TIME,120)<j.BEGIN_TIME and  CONVERT(varchar(11),b.APPARTUS_END_TIME,120) >j.END_TIME) ");
                }
            }
  
            return int.Parse(SqlHelper.ExecuteScalar(sb.ToString()).ToString());
        }
        #endregion

        #region//使用记录报表
        public DataTable SearchUseReocrdData(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime, string Name,int iIndex, int iCount)
        {
            StringBuilder sb = new StringBuilder(50000);
            sb.Append(" select  a.id,e.TICKET_NUM,c.SAMPLE_NAME,g.ITEM_NAME,f.REAL_NAME,j.NAME,CONVERT(varchar(11),b.APPARTUS_START_TIME,120) as APPARTUS_START_TIME, ");
            sb.Append(" CONVERT(varchar(11),b.APPARTUS_END_TIME,120) as APPARTUS_END_TIME, CONVERT(varchar(11), j.BEGIN_TIME,120) as BEGIN_TIME, CONVERT(varchar(11), j.END_TIME,120) as END_TIME, ");
            sb.Append(" (case when (CONVERT(varchar(11),b.APPARTUS_START_TIME,120)>= CONVERT(varchar(11), j.BEGIN_TIME,120)  and  CONVERT(varchar(11),b.APPARTUS_END_TIME,120) <=CONVERT(varchar(11), j.END_TIME,120) ) then '是'  ");
            sb.Append(" when (CONVERT(varchar(11),b.APPARTUS_END_TIME,120) < CONVERT(varchar(11), j.BEGIN_TIME,120)  and  CONVERT(varchar(11),b.APPARTUS_END_TIME,120) >CONVERT(varchar(11), j.END_TIME,120) ) then '否' end)  as Is_OverTime ");
            sb.Append(" from T_MIS_MONITOR_RESULT_APP a   left join T_MIS_MONITOR_RESULT b on a.RESULT_ID=b.id ");//结果分析执行表,T_MIS_MONITOR_RESULT
            sb.Append(" left join T_MIS_MONITOR_SAMPLE_INFO c on  b.SAMPLE_ID=c.id ");//T_MIS_MONITOR_SAMPLE_INFO
            sb.Append(" left join T_MIS_MONITOR_SUBTASK d on c.SUBTASK_ID=d.id ");//监测子任务
            sb.Append(" left join  T_MIS_MONITOR_TASK e on d.TASK_ID=e.id ");//监测任务企业信息
            sb.Append(" left join T_SYS_USER f on a.HEAD_USERID=f.id  ");//用户表
            sb.Append("left join T_BASE_ITEM_INFO g on b.ITEM_ID=g.id  ");//监测项目表
            sb.Append(" left join T_BASE_ITEM_ANALYSIS h on b.ANALYSIS_METHOD_ID=h.ANALYSIS_METHOD_ID");
            sb.Append(" left join T_BASE_APPARATUS_INFO j on h.INSTRUMENT_ID=j.id where 1=1 ");
            if (!string.IsNullOrEmpty(HEAD_USERID))//分析负责人
            {
                sb.Append(" and f.REAL_NAME='" + HEAD_USERID + "'");
            }
            if (!string.IsNullOrEmpty(TICKET_NUM))//任务单号
            {
                sb.Append(" and e.TICKET_NUM='" + TICKET_NUM + "'");
            }
            if (!string.IsNullOrEmpty(Name))//仪器名称
            {
                sb.Append(" and j.NAME='" + Name + "'");
            }
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                sb.Append("  and ('" + StartTime + "' between CONVERT(varchar(11),b.APPARTUS_START_TIME,120) and  CONVERT(varchar(11),b.APPARTUS_END_TIME,120) ) or ('" + EndTime + "'between CONVERT(varchar(11),b.APPARTUS_START_TIME,120) and  CONVERT(varchar(11),b.APPARTUS_END_TIME,120)) or  ");
                sb.Append(" (CONVERT(varchar(11),b.APPARTUS_START_TIME,120) between '" + StartTime + "' and '" + EndTime + "') or (CONVERT(varchar(11),b.APPARTUS_END_TIME,120)  between '" + StartTime + "' and '" + EndTime + "')   ");
            }
            if (!string.IsNullOrEmpty(OverTime))//是否超期完成{0：全部；1代表是，仪器开始/结束时间被包含在（最近检定/校准时间和到期检定/校准时间）内；2：代表否，反之}
            {
                if (OverTime.Equals("1"))
                {
                    sb.Append(" and ((CONVERT(varchar(11),b.APPARTUS_START_TIME,120)>=j.BEGIN_TIME and  CONVERT(varchar(11),b.APPARTUS_END_TIME,120) <=j.END_TIME) ");
                }
                else if (OverTime.Equals("2"))
                {
                    sb.Append(" and ((CONVERT(varchar(11),b.APPARTUS_START_TIME,120)<j.BEGIN_TIME and  CONVERT(varchar(11),b.APPARTUS_END_TIME,120) >j.END_TIME) ");
                }
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(sb.ToString(), iIndex, iCount));
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorResultApp"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorResultApp)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorResultApp.ID.ToString()));
                }
                //样品结果表ID
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.RESULT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID = '{0}'", tMisMonitorResultApp.RESULT_ID.ToString()));
                }
                //分析负责人员ID
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.HEAD_USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HEAD_USERID = '{0}'", tMisMonitorResultApp.HEAD_USERID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.ASSISTANT_USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ASSISTANT_USERID = '{0}'", tMisMonitorResultApp.ASSISTANT_USERID.ToString()));
                }
                //执行人接受确认
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.TEST_CORFIRM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEST_CORFIRM = '{0}'", tMisMonitorResultApp.TEST_CORFIRM.ToString()));
                }
                //要求时间
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.ASKING_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ASKING_DATE = '{0}'", tMisMonitorResultApp.ASKING_DATE.ToString()));
                }
                //完成时间
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.FINISH_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FINISH_DATE = '{0}'", tMisMonitorResultApp.FINISH_DATE.ToString()));
                }
                //校核人ID
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.CHECK_USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHECK_USERID = '{0}'", tMisMonitorResultApp.CHECK_USERID.ToString()));
                }
                //校核时间
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.CHECK_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHECK_DATE = '{0}'", tMisMonitorResultApp.CHECK_DATE.ToString()));
                }
                //校核意见
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.CHECK_OPINION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHECK_OPINION = '{0}'", tMisMonitorResultApp.CHECK_OPINION.ToString()));
                }
                //复核人ID
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.APPROVE_USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPROVE_USERID = '{0}'", tMisMonitorResultApp.APPROVE_USERID.ToString()));
                }
                //复核时间
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.APPROVE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPROVE_DATE = '{0}'", tMisMonitorResultApp.APPROVE_DATE.ToString()));
                }
                //复核意见
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.APPROVE_OPINION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND APPROVE_OPINION = '{0}'", tMisMonitorResultApp.APPROVE_OPINION.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorResultApp.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorResultApp.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorResultApp.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorResultApp.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorResultApp.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorResultApp.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

    }
}
