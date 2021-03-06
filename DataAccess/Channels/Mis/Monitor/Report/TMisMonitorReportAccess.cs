using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.ValueObject;
using i3.ValueObject.Channels.Mis.Monitor.Task;

namespace i3.DataAccess.Channels.Mis.Monitor.Report
{
    /// <summary>
    /// 功能：监测报告表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorReportAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorReport">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorReportVo tMisMonitorReport)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_REPORT " + this.BuildWhereStatement(tMisMonitorReport);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorReportVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_REPORT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorReportVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorReport">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorReportVo Details(TMisMonitorReportVo tMisMonitorReport)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_REPORT " + this.BuildWhereStatement(tMisMonitorReport));
            return SqlHelper.ExecuteObject(new TMisMonitorReportVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorReport">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorReportVo> SelectByObject(TMisMonitorReportVo tMisMonitorReport, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_REPORT " + this.BuildWhereStatement(tMisMonitorReport));
            return SqlHelper.ExecuteObjectList(tMisMonitorReport, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorReport">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorReportVo tMisMonitorReport, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_REPORT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorReport));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorReport"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorReportVo tMisMonitorReport)
        {
            string strSQL = "select * from T_MIS_MONITOR_REPORT " + this.BuildWhereStatement(tMisMonitorReport);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorReport">对象</param>
        /// <returns></returns>
        public TMisMonitorReportVo SelectByObject(TMisMonitorReportVo tMisMonitorReport)
        {
            string strSQL = "select * from T_MIS_MONITOR_REPORT " + this.BuildWhereStatement(tMisMonitorReport);
            return SqlHelper.ExecuteObject(new TMisMonitorReportVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorReport">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorReportVo tMisMonitorReport)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorReport, TMisMonitorReportVo.T_MIS_MONITOR_REPORT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorReport">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorReportVo tMisMonitorReport)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorReport, TMisMonitorReportVo.T_MIS_MONITOR_REPORT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorReport.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorReport_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorReport_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorReportVo tMisMonitorReport_UpdateSet, TMisMonitorReportVo tMisMonitorReport_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorReport_UpdateSet, TMisMonitorReportVo.T_MIS_MONITOR_REPORT_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorReport_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_REPORT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorReportVo tMisMonitorReport)
        {
            string strSQL = "delete from T_MIS_MONITOR_REPORT ";
            strSQL += this.BuildWhereStatement(tMisMonitorReport);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 报告领取信息 行数
        /// </summary>
        /// <param name="tMisMonitorReport">报告对象</param>
        /// <param name="tMisMonitorTask">监测任务对象</param>
        /// <returns>返回影响行数</returns>
        public int GetSelectResultCount(TMisMonitorReportVo tMisMonitorReport, TMisMonitorTaskVo tMisMonitorTask)
        {
            string strSQL = "select count(*)" +
                                        " from (select * from T_MIS_MONITOR_REPORT {0}) report " +
                                        " join (select * from T_MIS_MONITOR_TASK {1}) task on report.TASK_ID=task.ID";
            #region 监测任务查询条件构造 strTaskWhere
            string strTaskWhere = " where 1=1";
            //委托书编号
            if (!string.IsNullOrEmpty(tMisMonitorTask.CONTRACT_CODE))
            {
                strTaskWhere += string.Format(" and CONTRACT_CODE='{0}'", tMisMonitorTask.CONTRACT_CODE);
            }
            //委托类型
            if (!string.IsNullOrEmpty(tMisMonitorTask.CONTRACT_TYPE))
            {
                strTaskWhere += string.Format(" and CONTRACT_TYPE='{0}'", tMisMonitorTask.CONTRACT_TYPE);
            }
            //项目名称
            if (!string.IsNullOrEmpty(tMisMonitorTask.PROJECT_NAME))
            {
                strTaskWhere += string.Format(" and PROJECT_NAME like '%{0}%'", tMisMonitorTask.PROJECT_NAME);
            }
            //委托单位
            if (!string.IsNullOrEmpty(tMisMonitorTask.CLIENT_COMPANY_ID))
            {
                strTaskWhere += string.Format(" and CLIENT_COMPANY_ID='{0}'", tMisMonitorTask.CLIENT_COMPANY_ID);
            }
            //签订日期
            if (!string.IsNullOrEmpty(tMisMonitorTask.CONSIGN_DATE))
            {
                if (tMisMonitorTask.CONSIGN_DATE.Contains("|"))
                {
                    string[] arrDate = tMisMonitorTask.CONSIGN_DATE.Split('|');
                    if (!string.IsNullOrEmpty(arrDate[0].ToString()))
                    {
                        strTaskWhere += string.Format(" and CONSIGN_DATE>='{0}'", arrDate[0].ToString());
                    }
                    if (!string.IsNullOrEmpty(arrDate[1].ToString()))
                    {
                        strTaskWhere += string.Format(" and CONSIGN_DATE<='{0}'", arrDate[1].ToString());
                    }
                }
            }
            #endregion
            strSQL = string.Format(strSQL, BuildWhereStatement(tMisMonitorReport), strTaskWhere);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 报告领取信息
        /// </summary>
        /// <param name="tMisMonitorReport">报告对象</param>
        /// <param name="tMisMonitorTask">监测任务对象</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="pageSize">单页显示数</param>
        /// <returns>返回数据集</returns>
        public DataTable SelectByTableForManager(TMisMonitorReportVo tMisMonitorReport, TMisMonitorTaskVo tMisMonitorTask, int pageIndex, int pageSize)
        {
            string strSQL = "select report.ID,report.REPORT_CODE,report.REMARK1 ,task.CONTRACT_ID,report.TASK_ID," +
                                        " (CASE fee.IF_PAY WHEN '1' THEN '已缴费' WHEN '2' THEN '未缴费(已启动)'  ELSE '未缴费' END) as IS_FEE," +
                                        " (CASE report.IF_GET WHEN '1' THEN '已领取' ELSE '未领取' END) as IF_GET," +
                                        " task.CONTRACT_TYPE,task.CONTRACT_CODE,task.PROJECT_NAME,task.CLIENT_COMPANY_ID," +
                                        " (case when task.CONSIGN_DATE is not null then  convert(nvarchar(16),task.CONSIGN_DATE,23) else '' end) as CONSIGN_DATE" +
                                        " from (select * from T_MIS_MONITOR_REPORT {0}) report " +
                                        " join (select * from T_MIS_MONITOR_TASK {1}) task on report.TASK_ID=task.ID" +
                                        " left join T_MIS_CONTRACT_FEE fee on fee.CONTRACT_ID=task.CONTRACT_ID order by report.REMARK1 desc";
            #region 监测任务查询条件构造 strTaskWhere
            string strTaskWhere = " where 1=1 and TASK_STATUS='" + tMisMonitorTask.TASK_STATUS + "'";
            //委托书编号
            if (!string.IsNullOrEmpty(tMisMonitorTask.CONTRACT_CODE))
            {
                strTaskWhere += string.Format(" and CONTRACT_CODE='{0}'", tMisMonitorTask.CONTRACT_CODE);
            }
            //委托类型
            if (!string.IsNullOrEmpty(tMisMonitorTask.CONTRACT_TYPE))
            {
                strTaskWhere += string.Format(" and CONTRACT_TYPE='{0}'", tMisMonitorTask.CONTRACT_TYPE);
            }
            //项目名称
            if (!string.IsNullOrEmpty(tMisMonitorTask.PROJECT_NAME))
            {
                strTaskWhere += string.Format(" and PROJECT_NAME like '%{0}%'", tMisMonitorTask.PROJECT_NAME);
            }
            //委托单位
            if (!string.IsNullOrEmpty(tMisMonitorTask.CLIENT_COMPANY_ID))
            {
                strTaskWhere += string.Format(" and CLIENT_COMPANY_ID='{0}'", tMisMonitorTask.CLIENT_COMPANY_ID);
            }
            //签订日期
            if (!string.IsNullOrEmpty(tMisMonitorTask.CONSIGN_DATE))
            {
                if (tMisMonitorTask.CONSIGN_DATE.Contains("|"))
                {
                    string[] arrDate = tMisMonitorTask.CONSIGN_DATE.Split('|');
                    if (!string.IsNullOrEmpty(arrDate[0].ToString()))
                    {
                        strTaskWhere += string.Format(" and CONSIGN_DATE>='{0}'", arrDate[0].ToString());
                    }
                    if (!string.IsNullOrEmpty(arrDate[1].ToString()))
                    {
                        strTaskWhere += string.Format(" and CONSIGN_DATE<='{0}'", arrDate[1].ToString());
                    }
                }
            }
            #endregion
            strSQL = string.Format(strSQL, BuildWhereStatement(tMisMonitorReport), strTaskWhere);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, pageIndex, pageSize));
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorReport"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorReportVo tMisMonitorReport)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorReport)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorReport.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorReport.ID.ToString()));
                }
                //监测计划ID
                if (!String.IsNullOrEmpty(tMisMonitorReport.TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_ID = '{0}'", tMisMonitorReport.TASK_ID.ToString()));
                }
                //报告单号
                if (!String.IsNullOrEmpty(tMisMonitorReport.REPORT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REPORT_CODE = '{0}'", tMisMonitorReport.REPORT_CODE.ToString()));
                }
                //报告完成日期
                if (!String.IsNullOrEmpty(tMisMonitorReport.REPORT_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REPORT_DATE = '{0}'", tMisMonitorReport.REPORT_DATE.ToString()));
                }
                //报告要求完成时间
                if (!String.IsNullOrEmpty(tMisMonitorReport.RPT_ASK_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RPT_ASK_DATE = '{0}'", tMisMonitorReport.RPT_ASK_DATE.ToString()));
                }	

                //验收报告附件ID
                if (!String.IsNullOrEmpty(tMisMonitorReport.REPORT_EX_ATTACHE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REPORT_EX_ATTACHE_ID = '{0}'", tMisMonitorReport.REPORT_EX_ATTACHE_ID.ToString()));
                }
                //是否领取
                if (!String.IsNullOrEmpty(tMisMonitorReport.IF_GET.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IF_GET = '{0}'", tMisMonitorReport.IF_GET.ToString()));
                }
                //是否发送分配
                if (!String.IsNullOrEmpty(tMisMonitorReport.IF_SEND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IF_SEND = '{0}'", tMisMonitorReport.IF_SEND.ToString()));
                }
                //是否确认
                if (!String.IsNullOrEmpty(tMisMonitorReport.IF_ACCEPT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IF_ACCEPT = '{0}'", tMisMonitorReport.IF_ACCEPT.ToString()));
                }	
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorReport.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorReport.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorReport.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorReport.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorReport.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorReport.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorReport.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorReport.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorReport.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorReport.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
