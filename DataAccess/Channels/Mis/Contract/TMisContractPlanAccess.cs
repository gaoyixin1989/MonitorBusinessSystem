using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject;
using i3.ValueObject.Channels.Mis.Monitor.Task;
namespace i3.DataAccess.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书监测预约表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPlanAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPlan">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_PLAN " + this.BuildWhereStatement(tMisContractPlan);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPlanVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_PLAN  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisContractPlanVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPlan">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPlanVo Details(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_PLAN " + this.BuildWhereStatement(tMisContractPlan));
            return SqlHelper.ExecuteObject(new TMisContractPlanVo(), strSQL);
        }


        public TMisContractPlanVo DetailsByCCFlowID(string ccflowID)
        {
            string strSQL = String.Format(@"select a.* from  T_MIS_CONTRACT_PLAN a inner join T_MIS_CONTRACT b
on a.CONTRACT_ID=b.ID and a.CCFLOW_ID1='{0}'
union 
select * from T_MIS_CONTRACT_PLAN where  CCFLOW_ID1='{0}'", ccflowID);

            return SqlHelper.ExecuteObject(new TMisContractPlanVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPlanVo> SelectByObject(TMisContractPlanVo tMisContractPlan, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_CONTRACT_PLAN " + this.BuildWhereStatement(tMisContractPlan));
            return SqlHelper.ExecuteObjectList(tMisContractPlan, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPlanVo tMisContractPlan, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_CONTRACT_PLAN {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractPlan));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = "select * from T_MIS_CONTRACT_PLAN " + this.BuildWhereStatement(tMisContractPlan);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPlan">对象</param>
        /// <returns></returns>
        public TMisContractPlanVo SelectByObject(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = "select * from T_MIS_CONTRACT_PLAN " + this.BuildWhereStatement(tMisContractPlan);
            return SqlHelper.ExecuteObject(new TMisContractPlanVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractPlan">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractPlan, TMisContractPlanVo.T_MIS_CONTRACT_PLAN_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlan">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPlan, TMisContractPlanVo.T_MIS_CONTRACT_PLAN_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractPlan.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlan_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractPlan_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanVo tMisContractPlan_UpdateSet, TMisContractPlanVo tMisContractPlan_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPlan_UpdateSet, TMisContractPlanVo.T_MIS_CONTRACT_PLAN_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractPlan_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_PLAN where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = "delete from T_MIS_CONTRACT_PLAN ";
            strSQL += this.BuildWhereStatement(tMisContractPlan);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获取指定日期监测计划
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableContractPlan(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = String.Format("SELECT A.*,B.PROJECT_NAME,B.CONTRACT_CODE,B.TEST_TYPES,C.COMPANY_NAME,D.DICT_TEXT AS AREA_NAME FROM T_MIS_CONTRACT_PLAN A" +
                                                            " INNER JOIN dbo.T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID" +
                                                             " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY C ON B.TESTED_COMPANY_ID=C.ID" +
                                                               "  LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE WHERE A.PLAN_YEAR='{0}' AND A.PLAN_MONTH='{1}' AND A.PLAN_DAY='{2}'  AND A.HAS_DONE IS NULL", tMisContractPlan.PLAN_YEAR, tMisContractPlan.PLAN_MONTH, tMisContractPlan.PLAN_DAY);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取指定条件委托书的监测任务计划（快速录入）记录数 胡方扬 2013-02-25
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public int GetContractPlanForQuicklyCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask)
        {
            //string strSQL = String.Format("SELECT A.*,B.PROJECT_NAME,B.CONTRACT_CODE,B.TEST_TYPES,C.COMPANY_NAME,D.DICT_TEXT AS AREA_NAME FROM T_MIS_CONTRACT_PLAN A" +
            //                                                " INNER JOIN dbo.T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID" +
            //                                                 " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY C ON B.TESTED_COMPANY_ID=C.ID" +
            //                                                   "  INNER JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE WHERE 1=1");

            string strSQL = String.Format("SELECT A.*,B.PROJECT_NAME,B.CONTRACT_CODE,B.TEST_TYPES,B.CONTRACT_TYPE,B.SAMPLE_SOURCE,B.PROJECT_ID,B.QC_STEP,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE," +
                                                    " D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,E.ASKING_DATE,E.QC_STATUS,F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  FROM T_MIS_CONTRACT_PLAN A" +
                                                   " INNER JOIN dbo.T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID" +
                                                    " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY C ON B.TESTED_COMPANY_ID=C.ID" +
                                                     " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY G ON B.CLIENT_COMPANY_ID=G.ID" +
                                                      "  LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE" +
                                                        " INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.CONTRACT_ID=A.CONTRACT_ID" +
                                                         " INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
            }
            strSQL += "AND A.HAS_DONE IS NULL";

            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += String.Format(" AND B.CONTRACT_STATUS='{0}'", tMisContract.CONTRACT_STATUS);
            }
            if (!String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format(" AND B.ISQUICKLY='{0}'", tMisContract.ISQUICKLY);
            }

            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND B.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND B.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND B.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND B.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 获取指定条件委托书的监测任务计划（快速录入） 胡方扬 2013-02-25
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableContractPlanForQuickly(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask)
        {
            //string strSQL = String.Format("SELECT A.*,B.PROJECT_NAME,B.CONTRACT_CODE,B.TEST_TYPES,C.COMPANY_NAME,D.DICT_TEXT AS AREA_NAME FROM T_MIS_CONTRACT_PLAN A" +
            //                                                " INNER JOIN dbo.T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID" +
            //                                                 " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY C ON B.TESTED_COMPANY_ID=C.ID" +
            //                                                   "  LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE WHERE 1=1");
            string strSQL = String.Format("SELECT A.*,B.PROJECT_NAME,B.CONTRACT_CODE,B.TEST_TYPES,B.CONTRACT_TYPE,B.SAMPLE_SOURCE,B.PROJECT_ID,B.QC_STEP,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE," +
                                                    " D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,E.ASKING_DATE,E.QC_STATUS,F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  FROM T_MIS_CONTRACT_PLAN A" +
                                                   " INNER JOIN dbo.T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID" +
                                                    " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY C ON B.TESTED_COMPANY_ID=C.ID" +
                                                     " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY G ON B.CLIENT_COMPANY_ID=G.ID" +
                                                      "  LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE" +
                                                        " INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.CONTRACT_ID=A.CONTRACT_ID" +
                                                         " INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
            }
            strSQL += "AND A.HAS_DONE IS NULL";

            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += String.Format(" AND B.CONTRACT_STATUS='{0}'", tMisContract.CONTRACT_STATUS);
            }
            if (!String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format(" AND B.ISQUICKLY='{0}'", tMisContract.ISQUICKLY);
            }
            if (String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format(" AND B.ISQUICKLY  IS NULL ");
            }

            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND B.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND B.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND B.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND B.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM)) {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }

            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS =  '{0}'", tMisMonitorTask.QC_STATUS);
            }

            if (String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS IS NULL");
            }
 
            strSQL += " order by A.ID ASC";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取指定条件下委托书监测计划任务列表（待预约的，已预约的） 胡方扬 2013-04-01
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableContractPlanForPending(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract,TMisMonitorTaskVo tMisMonitorTask, bool strType, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT A.*,B.PROJECT_NAME,B.CONTRACT_CODE,B.TEST_TYPES,B.CONTRACT_TYPE,B.SAMPLE_SOURCE,B.PROJECT_ID,B.QC_STEP,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE," +
                                                             " D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,E.ASKING_DATE,E.QC_STATUS,F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  FROM T_MIS_CONTRACT_PLAN A" +
                                                            " INNER JOIN dbo.T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID" +
                                                             " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY C ON B.TESTED_COMPANY_ID=C.ID" +
                                                              " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY G ON B.CLIENT_COMPANY_ID=G.ID" +
                                                               "  LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE" +
                                                                 " INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.CONTRACT_ID=A.CONTRACT_ID" +
                                                                  " INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID WHERE 1=1");
            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                }
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }
            strSQL += "AND A.HAS_DONE IS NULL";
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += String.Format(" AND B.CONTRACT_STATUS='{0}'", tMisContract.CONTRACT_STATUS);
            }
            if (!String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format(" AND B.ISQUICKLY='{0}'", tMisContract.ISQUICKLY);
            }
            if (String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format("  AND (B.ISQUICKLY IS NULL OR B.ISQUICKLY!='1')");
            }

            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND B.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND B.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND B.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND B.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
            }
            if(!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM)){
             strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            strSQL += " order by A.ID ASC";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取指定条件下委托书监测计划任务列表（待预约的，已预约的） 胡方扬 2013-04-24
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableContractPlanForPending(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            LEFT JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisContractPlan.ID))
            {
                strSQL += String.Format(" AND A.ID='{0}'", tMisContractPlan.ID);
            }
            strSQL += " order by A.ID ASC";
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 获取指定条件下委托书监测计划任务列表（待预约的，已预约的） 胡方扬 2013-04-24
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableContractPlanForPending(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract)
        {
            string strSQL = String.Format("SELECT A.*,B.PROJECT_NAME,B.CONTRACT_CODE,B.TEST_TYPES,B.CONTRACT_TYPE,B.SAMPLE_SOURCE,B.PROJECT_ID,E.REMARK1 QC_STEP,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE," +
                                                             " D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,E.ASKING_DATE,E.QC_STATUS,F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  FROM T_MIS_CONTRACT_PLAN A" +
                                                            " INNER JOIN dbo.T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID" +
                                                             " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY C ON B.TESTED_COMPANY_ID=C.ID" +
                                                              " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY G ON B.CLIENT_COMPANY_ID=G.ID" +
                                                               "  LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE" +
                                                                 " INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.CONTRACT_ID=A.CONTRACT_ID" +
                                                                  " INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisContractPlan.ID))
            {
                strSQL += String.Format(" AND A.ID='{0}'", tMisContractPlan.ID);
            }

            if (!String.IsNullOrEmpty(tMisContract.ID))
            {
                strSQL += String.Format(" AND B.ID='{0}'", tMisContract.ID);
            }
            strSQL += " order by A.ID ASC";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取指定条件下委托书监测计划任务列表数目（待预约的，已预约的） 胡方扬 2013-04-01
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public int SelectByTableContractPlanForPendingCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract,TMisMonitorTaskVo tMisMonitorTask, bool strType)
        {
            string strSQL = String.Format("SELECT A.*,B.PROJECT_NAME,B.CONTRACT_CODE,B.TEST_TYPES,B.CONTRACT_TYPE,B.SAMPLE_SOURCE,B.PROJECT_ID,B.QC_STEP,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE," +
                                                             " D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,E.ASKING_DATE,E.QC_STATUS,F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  FROM T_MIS_CONTRACT_PLAN A" +
                                                            " INNER JOIN dbo.T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID" +
                                                             " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY C ON B.TESTED_COMPANY_ID=C.ID" +
                                                              " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY G ON B.CLIENT_COMPANY_ID=G.ID" +
                                                               "  LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE" +
                                                                 " INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.CONTRACT_ID=A.CONTRACT_ID" +
                                                                  " INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID WHERE 1=1");
            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                }
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }
            strSQL += "AND A.HAS_DONE IS NULL";

            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += String.Format(" AND B.CONTRACT_STATUS='{0}'", tMisContract.CONTRACT_STATUS);
            }
            if (!String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format(" AND B.ISQUICKLY='{0}'", tMisContract.ISQUICKLY);
            }
            if (String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format("  AND (B.ISQUICKLY IS NULL OR B.ISQUICKLY!='1')");
            }

            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND B.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND B.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND B.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND B.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
            }
            if(!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM)){
             strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 完全根据任务去获取待预约的任务
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectByTablePlanForTask(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType,int iIndex, int iCount)
        {
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE,C.ID TEST_COMPANY_ID
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");

            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                //}
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                //}
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                //}
                strSQL += String.Format(" AND (ISNULL(E.QC_STATUS,'3')='' OR ISNULL(E.QC_STATUS,'')='8') ");
            }
            else
            {
                strSQL += String.Format(" AND (ISNULL(E.QC_STATUS,'')='' OR ISNULL(E.QC_STATUS,'')='0') ");

                //strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                //strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                //strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
                //strSQL += "AND A.HAS_DONE IS NULL";
            }
            


            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND E.TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND (E.TASK_TYPE IS NULL OR E.TASK_TYPE='0')", tMisMonitorTask.TASK_TYPE);
            }
            //strSQL += " order by A.ID ASC";
            strSQL += " order by CREATE_DATE DESC";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        
        }

        /// <summary>
        /// 完全根据任务去获取待预约的任务的总条数
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectByTablePlanForTaskCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType)
        {
//            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
//                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
//                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
//                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
//                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
//                                            FROM T_MIS_CONTRACT_PLAN A 
//                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
//                                            LEFT JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
//                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
//                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
//                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
//                                            WHERE 1=1");

//            if (strType)
//            {
//                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
//                {
//                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
//                }
//                else
//                {
//                    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
//                }
//                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
//                {
//                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
//                }
//                else
//                {
//                    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
//                }
//                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
//                {
//                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
//                }
//                else
//                {
//                    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
//                }
//            }
//            else
//            {
//                strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
//                strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
//                strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
//            }
//            strSQL += "AND A.HAS_DONE IS NULL";


//            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
//            {
//                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
//            }
//            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
//            {
//                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
//            }
//            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
//            {
//                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
//            }
//            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
//            {
//                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
//            }

//            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
//            {
//                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
//            }
//            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
//            {
//                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
//            }
//            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
//            {
//                strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
//            }
//            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
//            {
//                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
//            }
//            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
//            {
//                strSQL += String.Format(" AND E.TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE);
//            }
//            if (String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
//            {
//                strSQL += String.Format(" AND (E.TASK_TYPE IS NULL OR E.TASK_TYPE IN ('0','1'))", tMisMonitorTask.TASK_TYPE);
//            }
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");

            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                //}
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                //}
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                //}
                strSQL += String.Format(" AND (ISNULL(E.QC_STATUS,'3')='' OR ISNULL(E.QC_STATUS,'')='8') ");
            }
            else
            {
                strSQL += String.Format(" AND (ISNULL(E.QC_STATUS,'')='' OR ISNULL(E.QC_STATUS,'')='0') ");
                //strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                //strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                //strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }
            //strSQL += "AND A.HAS_DONE IS NULL";


            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND E.TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND (E.TASK_TYPE IS NULL OR E.TASK_TYPE='0')", tMisMonitorTask.TASK_TYPE);
            }
            strSQL += " order by A.ID ASC";
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;

        }

        /// <summary>
        /// 完全根据任务去获取待预约的任务
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectByTablePlanForDoTask(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType, int iIndex, int iCount)
        {
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            LEFT JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");

            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                //}
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                //}
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                //}
            }
            else
            {
                //strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                //strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                //strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.HAS_DONE))
            {
                string strHashDone = tMisContractPlan.HAS_DONE.Replace("|", "','");
                strSQL += String.Format(" AND A.HAS_DONE IN ('{0}') ", strHashDone);
            }
            //else
            //{
            //    strSQL += " AND (A.HAS_DONE IS NULL OR A.HAS_DONE='0')";
            //}


            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND E.TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND (E.TASK_TYPE IS NULL OR E.TASK_TYPE IN ('0','1'))", tMisMonitorTask.TASK_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                string strQCStatus = tMisMonitorTask.QC_STATUS.Replace("|", "','");
                strSQL += String.Format(" AND E.QC_STATUS IN ('{0}')", strQCStatus);
                //strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                //strSQL += String.Format(" AND (E.QC_STATUS='1' OR E.QC_STATUS='3')", tMisMonitorTask.QC_STATUS);
                strSQL += String.Format(" AND E.QC_STATUS IS NULL", tMisMonitorTask.QC_STATUS);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.STATE))
            {
                strSQL += String.Format(" AND E.STATE IN ('{0}')", tMisMonitorTask.STATE);
            }
            //strSQL += " order by A.ID ASC";
            strSQL += " order by CREATE_DATE DESC";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 完全根据任务去获取待预约的任务的总条数
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectByTablePlanForDoTaskCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType)
        {
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");

            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                //}
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                //}
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                //else
                //{
                //    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                //}
            }
            else
            {
                //strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                //strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                //strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.HAS_DONE))
            {
                string strHashDone = tMisContractPlan.HAS_DONE.Replace("|", "','");
                strSQL += String.Format(" AND A.HAS_DONE IN ('{0}') ", strHashDone);
            }
            //else
            //{
            //    strSQL += "AND (A.HAS_DONE IS NULL OR A.HAS_DONE='0')";
            //}


            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND E.TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND (E.TASK_TYPE IS NULL OR E.TASK_TYPE='0')", tMisMonitorTask.TASK_TYPE);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                string strQCStatus = tMisMonitorTask.QC_STATUS.Replace("|", "','");
                strSQL += String.Format(" AND E.QC_STATUS IN ('{0}')", strQCStatus);
                //strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
                //strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS IS NULL", tMisMonitorTask.QC_STATUS);
                //strSQL += String.Format(" AND (E.QC_STATUS='1' OR E.QC_STATUS='3')", tMisMonitorTask.QC_STATUS);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;

        }

        /// <summary>
        /// 获取已预约办理的任务
        /// 创建时间：2014-09-30
        /// 创建人：魏林
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectByTablePlanForDoTask_Done(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, int iIndex, int iCount)
        {
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            LEFT JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");

            
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
            }
            //else
            //{
            //    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
            //}
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
            }
            //else
            //{
            //    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
            //}
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
            }
            //else
            //{
            //    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
            //}
            
            if (!String.IsNullOrEmpty(tMisContractPlan.HAS_DONE))
            {
                string strHashDone = tMisContractPlan.HAS_DONE.Replace("|", "','");
                strSQL += String.Format(" AND A.HAS_DONE IN ('{0}') ", strHashDone);
            }
            else
            {
                strSQL += " AND (A.HAS_DONE IS NULL OR A.HAS_DONE='0')";
            }


            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND E.TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND (E.TASK_TYPE IS NULL OR E.TASK_TYPE IN ('0','1'))", tMisMonitorTask.TASK_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                string strQCStatus = tMisMonitorTask.QC_STATUS.Replace("|", "','");
                strSQL += String.Format(" AND E.QC_STATUS IN ('{0}')", strQCStatus);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS IS NULL", tMisMonitorTask.QC_STATUS);
            }
            strSQL += " order by CREATE_DATE DESC";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));

        }
        /// <summary>
        /// 获取已预约办理的任务的总条数
        /// 创建时间：2014-09-30
        /// 创建人：魏林
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectByTablePlanForDoTaskCount_Done(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");

            
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
            {
                strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
            }
            
            if (!String.IsNullOrEmpty(tMisContractPlan.HAS_DONE))
            {
                string strHashDone = tMisContractPlan.HAS_DONE.Replace("|", "','");
                strSQL += String.Format(" AND A.HAS_DONE IN ('{0}') ", strHashDone);
            }
            else
            {
                strSQL += "AND (A.HAS_DONE IS NULL OR A.HAS_DONE='0')";
            }


            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            
            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                string strQCStatus = tMisMonitorTask.QC_STATUS.Replace("|", "','");
                strSQL += String.Format(" AND E.QC_STATUS IN ('{0}')", strQCStatus);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS IS NULL", tMisMonitorTask.QC_STATUS);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;

        }

        /// <summary>
        /// 完全根据任务去获取指令性已经完成任务下达任务列表
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectByTablePlanForDoOrderTask(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType, int iIndex, int iCount)
        {
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");

            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                }
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.HAS_DONE))
            {
                string strHashDone = tMisContractPlan.HAS_DONE.Replace("|", "','");
                strSQL += String.Format(" AND (A.HAS_DONE IN ('{0}') OR A.HAS_DONE IS NULL) ", strHashDone);
            }
            else
            {
                strSQL += "AND A.HAS_DONE IS NULL";
            }


            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND E.TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND (E.TASK_TYPE IS NULL OR E.TASK_TYPE='0')", tMisMonitorTask.TASK_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                string strQCStatus = tMisMonitorTask.QC_STATUS.Replace("|", "','");
                strSQL += String.Format(" AND E.QC_STATUS IN ('{0}')", strQCStatus);
                //strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
               // strSQL += String.Format(" AND (E.QC_STATUS='1' OR E.QC_STATUS='3')", tMisMonitorTask.QC_STATUS);
                strSQL += String.Format(" AND E.QC_STATUS IS NULL", tMisMonitorTask.QC_STATUS);
            }
            strSQL += " order by A.ID ASC";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 完全根据任务去获取指令性已经完成任务下达任务列表的总条数
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectByTablePlanForDoOrderTaskCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType)
        {
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");

            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                }
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }
            if (!String.IsNullOrEmpty(tMisContractPlan.HAS_DONE))
            {
                string strHashDone = tMisContractPlan.HAS_DONE.Replace("|", "','");
                strSQL += String.Format(" AND (A.HAS_DONE IN ('{0}') OR A.HAS_DONE IS NULL) ", strHashDone);
            }
            else
            {
                strSQL += "AND A.HAS_DONE IS NULL";
            }


            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND E.TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND (E.TASK_TYPE IS NULL OR E.TASK_TYPE='0')", tMisMonitorTask.TASK_TYPE);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                string strQCStatus = tMisMonitorTask.QC_STATUS.Replace("|", "','");
                strSQL += String.Format(" AND E.QC_STATUS IN ('{0}')", strQCStatus);
            }
            if (String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS IS NULL", tMisMonitorTask.QC_STATUS);
               // strSQL += String.Format(" AND (E.QC_STATUS='1' OR E.QC_STATUS='3')", tMisMonitorTask.QC_STATUS);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;

        }


        /// <summary>
        /// 完全根据任务去获取任务计划信息，不做TASK_STATUS限定
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectByTablePlanForTaskAnyTaskStatus(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType, int iIndex, int iCount)
        {
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");

            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                }
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }
            strSQL += "AND A.HAS_DONE IS NULL";


            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND E.TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
            }
            strSQL += " order by A.ID ASC";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 完全根据任务去获取任务计划信息，不做TASK_STATUS限定的总条数
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int SelectByTablePlanForTaskAnyTaskStatusCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType)
        {
            string strSQL = String.Format(@"SELECT A.*,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE,D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,
                                            E.PLAN_ID,E.CONTRACT_CODE,E.CONTRACT_YEAR,E.PROJECT_NAME,E.CONTRACT_TYPE,E.TEST_TYPE,E.TEST_PURPOSE,E.CLIENT_COMPANY_ID,
                                            E.TESTED_COMPANY_ID,E.CONSIGN_DATE,E.ASKING_DATE,E.FINISH_DATE,E.SAMPLE_SOURCE,E.CONTACT_ID,E.MANAGER_ID,E.CREATOR_ID,
                                            E.PROJECT_ID,E.CREATE_DATE,E.STATE,E.TASK_STATUS,E.TASK_TYPE,E.COMFIRM_STATUS,E.ALLQC_STATUS,E.QC_STATUS,
                                            F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE  
                                            FROM T_MIS_CONTRACT_PLAN A 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.PLAN_ID=A.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C ON E.TESTED_COMPANY_ID=C.ID 
                                            INNER JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON E.CLIENT_COMPANY_ID=G.ID  
                                            LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE 
                                            WHERE 1=1");

            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                }
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }
            strSQL += "AND A.HAS_DONE IS NULL";


            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND E.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND E.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND E.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND E.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TICKET_NUM))
            {
                strSQL += String.Format(" AND E.TICKET_NUM LIKE  '%{0}%'", tMisMonitorTask.TICKET_NUM);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.TASK_TYPE))
            {
                strSQL += String.Format(" AND E.TASK_TYPE = '{0}'", tMisMonitorTask.TASK_TYPE);
            }
            if (!String.IsNullOrEmpty(tMisMonitorTask.QC_STATUS))
            {
                strSQL += String.Format(" AND E.QC_STATUS = '{0}'", tMisMonitorTask.QC_STATUS);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 获取月份详细中某天的监测计划
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectTableContractByMonth(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = String.Format("SELECT COUNT(DISTINCT (SAMPLING_MANAGER_ID))AS SAMPINGGROUP,COUNT(DISTINCT(CONTRACT_ID)) AS CONTRACTGROUP  FROM  T_MIS_CONTRACT_USERDUTY A  WHERE A.CONTRACT_ID IN(  SELECT DISTINCT (B.CONTRACT_ID) FROM T_MIS_CONTRACT_PLAN B " +
                                                        " WHERE B.PLAN_YEAR='{0}' " +
                                                        "  AND B.PLAN_MONTH='{1}' AND B.PLAN_DAY='{2}' AND  B.HAS_DONE IS NULL) ", tMisContractPlan.PLAN_YEAR, tMisContractPlan.PLAN_MONTH, tMisContractPlan.PLAN_DAY);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取周详细中某天的监测计划
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectTableContractByWeek(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = String.Format("SELECT COUNT(T.CONTRACT_ID) AS COMPANNUM,T.REAL_NAME,T.USERID,T.AREANAME FROM (SELECT C.*,D.REAL_NAME,E.TESTED_COMPANY_ID,F.COMPANY_NAME,G.DICT_TEXT AS AREANAME FROM (SELECT DISTINCT (SAMPLING_MANAGER_ID) AS USERID,CONTRACT_ID FROM  " +
                                                            "  T_MIS_CONTRACT_USERDUTY A  WHERE A.CONTRACT_ID IN(  SELECT DISTINCT (B.CONTRACT_ID) " +
                                                            "  FROM T_MIS_CONTRACT_PLAN B  WHERE B.PLAN_YEAR='{0}' AND B.PLAN_MONTH='{1}' AND B.PLAN_DAY='{2}'  AND B.HAS_DONE IS NULL )) C" +
                                                             " INNER JOIN T_SYS_USER D ON D.ID=C.USERID" +
                                                               " INNER JOIN dbo.T_MIS_CONTRACT E ON E.ID=C.CONTRACT_ID" +
                                                                " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY F ON F.ID=E.TESTED_COMPANY_ID" +
                                                                " LEFT JOIN T_SYS_DICT G ON G.DICT_TYPE='administrative_area' AND F.AREA=G.DICT_CODE" +
                                                                " )T GROUP BY T.REAL_NAME,T.AREANAME,T.USERID", tMisContractPlan.PLAN_YEAR, tMisContractPlan.PLAN_MONTH, tMisContractPlan.PLAN_DAY);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取具体监测预约计划
        /// </summary>
        /// <param name="tMisContractPlan">预约计划ID</param>
        /// <returns></returns>
        public DataTable GetPlanPointSetted(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = String.Format("SELECT * FROM dbo.T_MIS_CONTRACT_PLAN A " +
                                                " INNER JOIN dbo.T_MIS_CONTRACT_PLAN_POINT B ON B.PLAN_ID=A.ID" +
                                                " INNER JOIN dbo.T_MIS_CONTRACT_POINT_FREQ C ON C.ID=B.POINT_FREQ_ID" +
                                                " INNER JOIN T_MIS_CONTRACT_POINT D ON D.ID=C.CONTRACT_POINT_ID" +
                                                 " INNER JOIN T_BASE_MONITOR_TYPE_INFO E ON E.ID=D.MONITOR_ID" +
                                                " WHERE A.ID='{0}'", tMisContractPlan.ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取指定监测预约计划的委托书信息
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable GetContractInfor(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = String.Format("SELECT B.* FROM dbo.T_MIS_CONTRACT_PLAN A " +
                                                            " INNER JOIN T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID WHERE A.ID='{0}' ", tMisContractPlan.ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        public DataTable SelectMaxPlanNum(TMisContractPlanVo tMisContractPlan)
        {
            string strSQL = String.Format("SELECT MAX(CONVERT (int,PLAN_NUM)) AS NUM FROM dbo.T_MIS_CONTRACT_PLAN  WHERE CONTRACT_ID='{0}'", tMisContractPlan.CONTRACT_ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取指定条件下预监测任务列表（已预约的）根据采样完成时间是否为NULL进行划分采样与分析环节
        /// 创建时间：2013-5-7
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableForPlanTask(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, string strQcStatus, bool strType, string strTaskCode, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT DISTINCT A.*,B.PROJECT_NAME,B.CONTRACT_CODE,B.TEST_TYPES,B.CONTRACT_TYPE,B.SAMPLE_SOURCE,B.PROJECT_ID,B.QC_STEP,C.COMPANY_NAME,C.CONTACT_NAME,C.PHONE," +
                                                             " D.DICT_TEXT AS AREA_NAME,E.ID AS TASK_ID,E.TICKET_NUM,E.QC_STATUS,F.REPORT_CODE,G.COMPANY_NAME AS CLIENT_COMPANY_NAME,G.CONTACT_NAME AS CLIENT_CONTACT_NAME,G.PHONE AS CLIENT_PHONE,  " +
                                                            " (CASE WHEN subtask.SAMPLE_FINISH_DATE is not null then CONVERT(NVARCHAR(32),subtask.SAMPLE_FINISH_DATE,23) ELSE '' END) as SAMPLE_FINISH_DATE," +
                                                            " (CASE WHEN E.ASKING_DATE is not null then CONVERT(NVARCHAR(32),E.ASKING_DATE,23) ELSE '' END) as TASK_ASKING_DATE" +
                                                            " FROM T_MIS_CONTRACT_PLAN A" +
                                                            " INNER JOIN dbo.T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID" +
                                                             " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY C ON B.TESTED_COMPANY_ID=C.ID" +
                                                              " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY G ON B.CLIENT_COMPANY_ID=G.ID" +
                                                               "  LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE" +
                                                                 " INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.CONTRACT_ID=A.CONTRACT_ID AND E.PLAN_ID=A.ID" +
                                                                 " INNER JOIN dbo.T_MIS_MONITOR_SUBTASK subtask ON subtask.ANALYSE_ASSIGN_DATE IS NULL AND subtask.TASK_ID=E.ID" +
                                                                  " INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID WHERE 1=1");
            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                }
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += String.Format(" AND B.CONTRACT_STATUS='{0}'", tMisContract.CONTRACT_STATUS);
            }
            if (!String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format(" AND B.ISQUICKLY='{0}'", tMisContract.ISQUICKLY);
            }
            if (String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format("  AND (B.ISQUICKLY IS NULL OR B.ISQUICKLY!='1')");
            }

            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND B.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND B.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND B.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND B.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(strQcStatus))
            {
                strSQL += String.Format(" AND E.QC_STATUS = '{0}'", strQcStatus);
            }
            if (!string.IsNullOrEmpty(strTaskCode))
            {
                strSQL += string.Format(" AND E.TICKET_NUM='{0}'", strTaskCode);
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 功能描述：获取指定条件下预监测任务列表总数（已预约的）
        /// 创建时间：2013-5-7
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public int SelectCountForPlanTask(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, string strQcStatus, bool strType, string strTaskCode)
        {
            string strSQL = String.Format("SELECT COUNT(DISTINCT A.ID)" +
                                                            " FROM T_MIS_CONTRACT_PLAN A" +
                                                            " INNER JOIN dbo.T_MIS_CONTRACT B ON A.CONTRACT_ID=B.ID" +
                                                             " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY C ON B.TESTED_COMPANY_ID=C.ID" +
                                                              " INNER JOIN dbo.T_MIS_CONTRACT_COMPANY G ON B.CLIENT_COMPANY_ID=G.ID" +
                                                               "  LEFT JOIN dbo.T_SYS_DICT D ON D.DICT_TYPE='administrative_area' AND C.AREA=D.DICT_CODE" +
                                                                 " INNER JOIN dbo.T_MIS_MONITOR_TASK E ON E.CONTRACT_ID=A.CONTRACT_ID AND E.PLAN_ID=A.ID" +
                                                                 " INNER JOIN dbo.T_MIS_MONITOR_SUBTASK subtask ON subtask.ANALYSE_ASSIGN_DATE IS NULL AND subtask.TASK_ID=E.ID" +
                                                                  " INNER JOIN dbo.T_MIS_MONITOR_REPORT F ON F.TASK_ID=E.ID WHERE 1=1");
            if (strType)
            {
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR='{0}' ", tMisContractPlan.PLAN_YEAR);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_YEAR IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH='{0}' ", tMisContractPlan.PLAN_MONTH);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_MONTH IS NOT NULL ");
                }
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR))
                {
                    strSQL += String.Format(" AND A.PLAN_DAY='{0}' ", tMisContractPlan.PLAN_DAY);
                }
                else
                {
                    strSQL += String.Format(" AND A.PLAN_DAY IS NOT NULL ");
                }
            }
            else
            {
                strSQL += String.Format(" AND A.PLAN_YEAR IS NULL ");
                strSQL += String.Format(" AND A.PLAN_MONTH IS NULL ");
                strSQL += String.Format(" AND A.PLAN_DAY IS NULL ");
            }

            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += String.Format(" AND B.CONTRACT_STATUS='{0}'", tMisContract.CONTRACT_STATUS);
            }
            if (!String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format(" AND B.ISQUICKLY='{0}'", tMisContract.ISQUICKLY);
            }
            if (String.IsNullOrEmpty(tMisContract.ISQUICKLY))
            {
                strSQL += String.Format("  AND (B.ISQUICKLY IS NULL OR B.ISQUICKLY!='1')");
            }

            if (!String.IsNullOrEmpty(tMisContract.PROJECT_NAME))
            {
                strSQL += String.Format(" AND B.PROJECT_NAME LIKE '%{0}%'", tMisContract.PROJECT_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format(" AND B.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_YEAR))
            {
                strSQL += String.Format(" AND B.CONTRACT_YEAR = '{0}'", tMisContract.CONTRACT_YEAR);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format(" AND B.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }

            if (!String.IsNullOrEmpty(tMisContract.TESTED_COMPANY_ID))
            {
                strSQL += String.Format(" AND C.COMPANY_NAME LIKE '%{0}%'", tMisContract.TESTED_COMPANY_ID);
            }
            if (!String.IsNullOrEmpty(tMisContract.REMARK4))
            {
                strSQL += String.Format(" AND D.DICT_CODE = '{0}'", tMisContract.REMARK4);
            }
            if (!String.IsNullOrEmpty(strQcStatus))
            {
                strSQL += String.Format(" AND E.QC_STATUS = '{0}'", strQcStatus);
            }
            if (!string.IsNullOrEmpty(strTaskCode))
            {
                strSQL += string.Format(" AND E.TICKET_NUM='{0}'", strTaskCode);
            }
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 获取指定监测计划的监测点位
        /// 创建时间：2013-06-06 
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="strPlanId"></param>
        /// <param name="strIfPlan"></param>
        public DataTable GetPointInforsForPlan(string strPlanId,string strIfPlan)
        {
            string strSQL=String.Format(@"SELECT A.ID,A.PLAN_ID,A.CONTRACT_POINT_ID,B.POINT_NAME,B.MONITOR_ID,C.FREQ,C.NUM,C.IF_PLAN,C.SAMPLE_FREQ,D.MONITOR_TYPE_NAME,D.REMARK1
                                        FROM dbo.T_MIS_CONTRACT_PLAN_POINT A
                                        INNER JOIN dbo.T_MIS_CONTRACT_POINT B ON B.ID=A.CONTRACT_POINT_ID
                                        INNER JOIN dbo.T_MIS_CONTRACT_POINT_FREQ C ON C.ID=A.POINT_FREQ_ID
                                        INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO D ON D.ID=B.MONITOR_ID
                                         WHERE 1=1");
            if(!String.IsNullOrEmpty(strPlanId))
            {
                strSQL+=String.Format(" AND A.PLAN_ID='{0}'",strPlanId);
            }

            if (!String.IsNullOrEmpty(strIfPlan))
            {
                strSQL+=String.Format(" AND C.IF_PLAN='{0}'",strIfPlan);
            }

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取指定监测计划的监测点位个数
        /// 创建时间：2013-06-06 
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="strPlanId"></param>
        /// <param name="strIfPlan"></param>
        public int GetPointInforsForPlanCount(string strPlanId, string strIfPlan)
        {
            string strSQL = String.Format(@"SELECT A.ID,A.PLAN_ID,A.CONTRACT_POINT_ID,B.POINT_NAME,B.MONITOR_ID,C.FREQ,C.NUM,C.IF_PLAN,C.SAMPLE_FREQ,D.MONITOR_TYPE_NAME 
                                        FROM dbo.T_MIS_CONTRACT_PLAN_POINT A
                                        LEFT JOIN dbo.T_MIS_CONTRACT_POINT B ON B.ID=A.CONTRACT_POINT_ID
                                        LEFT JOIN dbo.T_MIS_CONTRACT_POINT_FREQ C ON C.ID=A.POINT_FREQ_ID
                                        LEFT JOIN dbo.T_BASE_MONITOR_TYPE_INFO D ON D.ID=B.MONITOR_ID
                                         WHERE 1=1");
            if (!String.IsNullOrEmpty(strPlanId))
            {
                strSQL += String.Format(" AND A.PLAN_ID='{0}'", strPlanId);
            }

            if (!String.IsNullOrEmpty(strIfPlan))
            {
                strSQL += String.Format(" AND C.IF_PLAN='{0}'", strIfPlan);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractPlanVo tMisContractPlan)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractPlan)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisContractPlan.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractPlan.ID.ToString()));
                }
                //委托书ID
                if (!String.IsNullOrEmpty(tMisContractPlan.CONTRACT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractPlan.CONTRACT_ID.ToString()));
                }
                //受检企业ID
                if (!String.IsNullOrEmpty(tMisContractPlan.CONTRACT_COMPANY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_COMPANY_ID = '{0}'", tMisContractPlan.CONTRACT_COMPANY_ID.ToString()));
                }

                //年度
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PLAN_YEAR = '{0}'", tMisContractPlan.PLAN_YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PLAN_MONTH = '{0}'", tMisContractPlan.PLAN_MONTH.ToString()));
                }
                //日期
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PLAN_DAY = '{0}'", tMisContractPlan.PLAN_DAY.ToString()));
                }
                //是否已执行
                if (!String.IsNullOrEmpty(tMisContractPlan.HAS_DONE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HAS_DONE = '{0}'", tMisContractPlan.HAS_DONE.ToString()));
                }
                //执行日期
                if (!String.IsNullOrEmpty(tMisContractPlan.DONE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DONE_DATE = '{0}'", tMisContractPlan.DONE_DATE.ToString()));
                }

                //执行频次
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PLAN_NUM = '{0}'", tMisContractPlan.PLAN_NUM.ToString()));
                }
                //预约类型，环境质量为（1-17）,污染源为空
                if (!String.IsNullOrEmpty(tMisContractPlan.PLAN_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PLAN_TYPE = '{0}'", tMisContractPlan.PLAN_TYPE.ToString()));
                }
                //REAMRK1
                if (!String.IsNullOrEmpty(tMisContractPlan.REAMRK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAMRK1 = '{0}'", tMisContractPlan.REAMRK1.ToString()));
                }
                //REAMRK2
                if (!String.IsNullOrEmpty(tMisContractPlan.REAMRK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAMRK2 = '{0}'", tMisContractPlan.REAMRK2.ToString()));
                }
                //REAMRK3
                if (!String.IsNullOrEmpty(tMisContractPlan.REAMRK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAMRK3 = '{0}'", tMisContractPlan.REAMRK3.ToString()));
                }
                //REAMRK4
                if (!String.IsNullOrEmpty(tMisContractPlan.REAMRK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAMRK4 = '{0}'", tMisContractPlan.REAMRK4.ToString()));
                }
                //REAMRK5
                if (!String.IsNullOrEmpty(tMisContractPlan.REAMRK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAMRK5 = '{0}'", tMisContractPlan.REAMRK5.ToString()));
                }
                //CCFLOW_ID1
                if (!String.IsNullOrEmpty(tMisContractPlan.CCFLOW_ID1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID1 = '{0}'", tMisContractPlan.CCFLOW_ID1.ToString()));
                }
                //CCFLOW_ID2
                if (!String.IsNullOrEmpty(tMisContractPlan.CCFLOW_ID2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID2 = '{0}'", tMisContractPlan.CCFLOW_ID2.ToString()));
                }
                //CCFLOW_ID3
                if (!String.IsNullOrEmpty(tMisContractPlan.CCFLOW_ID3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID3 = '{0}'", tMisContractPlan.CCFLOW_ID3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
