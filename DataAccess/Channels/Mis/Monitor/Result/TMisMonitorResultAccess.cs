using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：分析结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// 修改日期：2013-4-23
    /// 修改人：潘德军
    /// 修改内容：为质控报表统计方法修改“#region 质控统计方法”下方法
    /// </summary>
    public partial class TMisMonitorResultAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_RESULT " + this.BuildWhereStatement(tMisMonitorResult);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorResultVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorResultVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorResult">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorResultVo Details(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT " + this.BuildWhereStatement(tMisMonitorResult));
            return SqlHelper.ExecuteObject(new TMisMonitorResultVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorResultVo> SelectByObject(TMisMonitorResultVo tMisMonitorResult, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT " + this.BuildWhereStatement(tMisMonitorResult));
            return SqlHelper.ExecuteObjectList(tMisMonitorResult, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        public DataTable Details1(string strResultID)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT where id in ({0})", strResultID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorResultVo tMisMonitorResult, int iIndex, int iCount)
        {
            string strSQL = " select * from T_MIS_MONITOR_RESULT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorResult));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResult"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = "select * from T_MIS_MONITOR_RESULT " + this.BuildWhereStatement(tMisMonitorResult);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据多个ID查询多条数据    黄飞20150720 【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <returns></returns>
        public DataTable SelectByTableOne(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = @"select distinct(A.ITEM_ID), B.ITEM_NAME from dbo.T_MIS_MONITOR_RESULT as A 
                                left join T_BASE_ITEM_INFO as B on (A.ITEM_ID = B.ID) WHERE A.ID IN (" + tMisMonitorResult.ID + ")";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResult"></param>
        /// <returns></returns>
        public DataTable SelectManagerByTable(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = @"select dbo.Rpt_AnalysisManagerID(ITEM_ID) AS ANALYSIS_MANAGER, dbo.Rpt_GetAnalysisID(ITEM_ID) AS ANALYSIS_ID 
                                from T_MIS_MONITOR_RESULT " + this.BuildWhereStatement(tMisMonitorResult);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <returns></returns>
        public TMisMonitorResultVo SelectByObject(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = "select * from T_MIS_MONITOR_RESULT " + this.BuildWhereStatement(tMisMonitorResult);
            return SqlHelper.ExecuteObject(new TMisMonitorResultVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorResult, TMisMonitorResultVo.T_MIS_MONITOR_RESULT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResult">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorResult, TMisMonitorResultVo.T_MIS_MONITOR_RESULT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorResult.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResult_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorResult_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultVo tMisMonitorResult_UpdateSet, TMisMonitorResultVo tMisMonitorResult_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorResult_UpdateSet, TMisMonitorResultVo.T_MIS_MONITOR_RESULT_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorResult_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_RESULT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = "delete from T_MIS_MONITOR_RESULT ";
            strSQL += this.BuildWhereStatement(tMisMonitorResult);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool updateResultBySample(string strSampleIDs, string strStatus, bool b)
        {
            string strSQL = @"update b set RESULT_STATUS='{1}' from T_MIS_MONITOR_SAMPLE_INFO a 
                             left join T_MIS_MONITOR_RESULT b on(a.ID=b.SAMPLE_ID)
                             left join T_BASE_ITEM_INFO c on(b.ITEM_ID=c.ID)
                             where c.IS_SAMPLEDEPT='是' and c.HAS_SUB_ITEM='0' and a.ID in({0})";
            strSQL = string.Format(strSQL, strSampleIDs, strStatus);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        
        /// <summary>
        /// 根据子任务ID获取非现场项目
        /// </summary>
        /// <param name="strSubtaskID">当前用户ID</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptWithSubtaskID(string strSubtaskID)
        {
            string strSQL = @"SELECT     *
                                FROM       T_MIS_MONITOR_RESULT
                                where  exists
                                (
                                select * from T_MIS_MONITOR_SAMPLE_INFO where  T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID= '{0}' and 
                                T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID and 
                                exists
                                (
                                select * from T_BASE_ITEM_INFO where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' 
                                and (T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否') and T_BASE_ITEM_INFO.ID =T_MIS_MONITOR_RESULT.ITEM_ID
                                )
                                )";
            strSQL = string.Format(strSQL, strSubtaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据子任务ID获取分析类项目
        /// </summary>
        /// <param name="strSubtaskID">当前用户ID</param>
        /// <returns></returns>
        /// <remarks>by lhm</remarks>
        public DataTable SelectSampleDeptWithSubtaskID2(string strSubtaskID, IList<string> sampleIdList=null)
        {
            var strIn = "";
            if (sampleIdList != null && sampleIdList.Count > 0)
            {
                 strIn = string.Format(" and T_MIS_MONITOR_SAMPLE_INFO.ID in ('{0}') ", string.Join("','", sampleIdList));
            }

            string strSQL = @"SELECT     *
                                FROM       T_MIS_MONITOR_RESULT
                                where  exists
                                (
                                select * from T_MIS_MONITOR_SAMPLE_INFO where  T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID= '{0}' {1} and 
                                T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID and 
                                exists
                                (
                                select * from T_BASE_ITEM_INFO where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' 
                                and (T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否') and T_BASE_ITEM_INFO.ID =T_MIS_MONITOR_RESULT.ITEM_ID  and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM!='1'
                                )
                                )";
            strSQL = string.Format(strSQL, strSubtaskID,strIn);

            
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据任务ID获取现场项目或分析项目（郑州） Create By：weilin 2014-06-10
        /// </summary>
        /// <param name="strTaskID">任务ID</param>
        /// <param name="b">true:现场项目；false：分析项目</param>
        /// <returns></returns>
        public DataTable SelectItemInfoWithTaskID(string strTaskID, bool b)
        {
            string strSQL = @"SELECT     *
                                FROM       T_MIS_MONITOR_RESULT
                                where  exists
                                (
                                select * from T_MIS_MONITOR_SAMPLE_INFO where 
                                T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID and 
                                exists
                                (
                                select * from T_MIS_MONITOR_SUBTASK where T_MIS_MONITOR_SUBTASK.TASK_ID='{0}' and
                                T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID=T_MIS_MONITOR_SUBTASK.ID and
                                exists
                                (
                                select * from T_BASE_ITEM_INFO where T_BASE_ITEM_INFO.ID =T_MIS_MONITOR_RESULT.ITEM_ID and T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' 
                                {1}
                                ) 
                                )
                                )";
            if (b)
                strSQL = string.Format(strSQL, strTaskID, "and (T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是') and isnull(T_MIS_MONITOR_RESULT.remark_4,'')<>'1'");
            else
                strSQL = string.Format(strSQL, strTaskID, "and (T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否') or T_MIS_MONITOR_RESULT.remark_4='1'");
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据任务ID和项目结果状态获取监测项目 Create By:weilin 2014-06-10
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <param name="strResultStatus"></param>
        /// <returns></returns>
        public DataTable SelectItemStatus(string strTaskID, string strResultStatus)
        {
            string strSQL = @"SELECT     *
                                FROM       T_MIS_MONITOR_RESULT
                                where  exists
                                (
                                select * from T_MIS_MONITOR_SAMPLE_INFO where  
                                T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID and 
                                exists
                                (
                                select * from T_MIS_MONITOR_SUBTASK where T_MIS_MONITOR_SUBTASK.TASK_ID='{0}' and
                                T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID=T_MIS_MONITOR_SUBTASK.ID
                                )
                                ) and RESULT_STATUS='{1}'";

            strSQL = string.Format(strSQL, strTaskID, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据样品ID获取非现场项目
        /// </summary>
        /// <param name="strSubtaskID">当前用户ID</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptWithSampleID(string strSampleID)
        {
            string strSQL = @"SELECT     *
                                FROM       T_MIS_MONITOR_RESULT
                                where  exists
                                (
                                select * from T_MIS_MONITOR_SAMPLE_INFO where  T_MIS_MONITOR_SAMPLE_INFO.ID='{0}' and T_MIS_MONITOR_RESULT.SAMPLE_ID=ID and 
                                exists
                                (
                                select * from T_BASE_ITEM_INFO where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' 
                                and (T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否' OR T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM='1') and T_BASE_ITEM_INFO.ID =T_MIS_MONITOR_RESULT.ITEM_ID
                                )
                                )";
            strSQL = string.Format(strSQL, strSampleID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 功能描述：根据子任务ID获取现场项目 并得到执行信息
        /// 创建时间：2013-5-11
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strSubtaskID">子任务ID</param>
        /// <returns></returns>
        public DataTable SelectResultAndAppWithSubtaskID(string strSubtaskID)
        {
            string strSQL = @"SELECT     result.*,app.HEAD_USERID,item.IS_ANYSCENE_ITEM,item.IS_SAMPLEDEPT,app.FINISH_DATE
                                FROM       T_MIS_MONITOR_RESULT result
                                INNER JOIN dbo.T_MIS_MONITOR_RESULT_APP app ON result.ID=app.RESULT_ID
                                INNER JOIN T_BASE_ITEM_INFO item ON result.ITEM_ID=item.ID and item.HAS_SUB_ITEM = '0' and item.IS_SAMPLEDEPT = '否'
                                where  exists
                                (
                                select * from T_MIS_MONITOR_SAMPLE_INFO where  T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID= '{0}' and 
                                result.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                )";
            strSQL = string.Format(strSQL, strSubtaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 功能描述：根据子任务ID获取监测项目 并得到执行信息
        /// 创建时间：2014-8-5
        /// 创建人：weilin
        /// </summary>
        /// <param name="strSubtaskID">子任务ID</param>
        /// <returns></returns>
        public DataTable SelectResultAndAppWithSubtaskID_QHD(string strSubtaskID)
        {
            string strSQL = @"SELECT     result.*,app.HEAD_USERID,item.IS_SAMPLEDEPT
                                FROM       T_MIS_MONITOR_RESULT result
                                INNER JOIN dbo.T_MIS_MONITOR_RESULT_APP app ON result.ID=app.RESULT_ID
                                INNER JOIN T_BASE_ITEM_INFO item ON result.ITEM_ID=item.ID and item.HAS_SUB_ITEM = '0'-- and item.IS_SAMPLEDEPT = '否'
                                where  exists
                                (
                                select * from T_MIS_MONITOR_SAMPLE_INFO where  T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID= '{0}' and 
                                result.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                )";
            strSQL = string.Format(strSQL, strSubtaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得任务下所有监测结果信息 汇总表
        /// 创建时间：2013-08-12
        /// 创建人：潘德军
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getTotalItemInfoByTaskID_ForSummary(string strTaskID, string strSampleCode, string strMonitorId, bool isLocal)
        {
            string strSQL = @"SELECT result.ITEM_ID,result.ITEM_RESULT,result.QC_TYPE,sample.ID as SAMPLE_ID,sample.SAMPLE_CODE,sample.SAMPLE_NAME,item.ITEM_NAME,item.ORDER_NUM,type.ID as MONITOR_ID,type.MONITOR_TYPE_NAME,point.POINT_NAME,dict.DICT_TEXT as ITEM_UNIT,
                                                (CASE WHEN subtask.SAMPLE_ASK_DATE IS NOT NULL THEN CONVERT(NVARCHAR(32),subtask.SAMPLE_ASK_DATE,23) ELSE '' END) as SAMPLE_ASK_DATE,
                                                sample.REMARK2,company.COMPANY_NAME,
                                                sample.REMARK4,sample.REMARK5 
                                                FROM dbo.T_MIS_MONITOR_RESULT result
                                                JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO sample ON result.SAMPLE_ID=sample.ID
                                                JOIN dbo.T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_ID='{0}' AND sample.SUBTASK_ID=subtask.ID
                                                JOIN dbo.T_BASE_ITEM_INFO item on result.ITEM_ID=item.ID
                                                JOIN dbo.T_BASE_MONITOR_TYPE_INFO type ON subtask.MONITOR_ID=type.ID
                                                LEFT JOIN dbo.T_MIS_MONITOR_TASK_POINT point ON point.ID = sample.POINT_ID 
                                                join T_MIS_MONITOR_TASK task on task.ID=subtask.TASK_ID
                                                left join T_MIS_MONITOR_TASK_COMPANY company on company.id=task.TESTED_COMPANY_ID 
                                                LEFT JOIN T_BASE_METHOD_ANALYSIS analysis ON result.ANALYSIS_METHOD_ID= analysis.ID 
                                                LEFT JOIN T_BASE_ITEM_ANALYSIS item_analysis ON item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=analysis.ID
                                                LEFT JOIN T_SYS_DICT dict ON dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT
                                                WHERE 1=1";
            if (!string.IsNullOrEmpty(strSampleCode))
            {
                strSQL += string.Format(" AND sample.SAMPLE_CODE like '%{0}%'", strSampleCode);
            }
            if (!string.IsNullOrEmpty(strMonitorId))
            {
                strSQL += string.Format(" AND type.ID='{0}'", strMonitorId);
            }
            string strIS_SAMPLEDEPT = "否";
            string strSubSql = " result.remark_4='1' or  result.ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "')";
            if (isLocal)
            {
                strIS_SAMPLEDEPT = "是";
                strSubSql = "(result.remark_4<>'1' or result.remark_4 is null) and  result.ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "')";
            }
            //strSQL += @" and result.ITEM_ID in (select ID from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SAMPLEDEPT='" + strIS_SAMPLEDEPT + "')";
            strSQL += @" and (" + strSubSql + ")";

            strSQL += " order by MONITOR_ID,SAMPLE_CODE,SAMPLE_NAME,ORDER_NUM";
            strSQL = string.Format(strSQL, strTaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 质控统计方法
        /// <summary>
        /// 按时间、类别筛选原始样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getResultWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            string strSql = @"SELECT    t.*,t2.ITEM_NAME,t2.MONITOR_ID,t3.MONITOR_TYPE_NAME 
                                FROM    T_MIS_MONITOR_RESULT AS t  
                                 JOIN T_MIS_MONITOR_RESULT_APP AS t1 ON t.ID = t1.RESULT_ID  
                                 JOIN T_BASE_ITEM_INFO AS t2 ON t.ITEM_ID = t2.ID
                                 JOIN T_BASE_MONITOR_TYPE_INFO as t3 ON t3.ID=t2.MONITOR_ID 
                                WHERE   t.QC_TYPE = '0'";
            strSql += (strMontorID.Length > 0) ? " and t2.MONITOR_ID = '" + strMontorID + "'" : "";
            strSql += (strStartTime.Length > 0) ? " and t1.FINISH_DATE >= '" + strStartTime + "'" : "";
            strSql += (strEndTime.Length > 0) ? " and t1.FINISH_DATE <= '" + strEndTime + "'" : "";
            strSql += " ORDER BY t.ITEM_ID";

            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 按时间、类别筛选现场空白样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getQCEmptyOutWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            string strSql = @"SELECT    t1.ITEM_ID AS ITEM_ID,t1.ITEM_RESULT, t.*,t2.FINISH_DATE,t1.RESULT_CHECKOUT LOWER_CHECKOUT ,t3.ITEM_NAME
                                FROM    T_MIS_MONITOR_RESULT_APP AS t2 
                                 JOIN T_MIS_MONITOR_RESULT AS t1 ON t2.RESULT_ID = t1.ID 
                                 JOIN T_BASE_ITEM_INFO AS t3 ON t1.ITEM_ID = t3.ID 
                                 JOIN T_MIS_MONITOR_QC_EMPTY_OUT AS t ON t1.ID = t.RESULT_ID_SRC
                                 --join T_BASE_ITEM_ANALYSIS as a on a.ITEM_ID=t1.ITEM_ID and a.ANALYSIS_METHOD_ID=t1.ANALYSIS_METHOD_ID 
                                WHERE   1=1 ";
            strSql += (strMontorID.Length > 0) ? " and t3.MONITOR_ID = '" + strMontorID + "'" : "";
            strSql += (strStartTime.Length > 0) ? " and t2.FINISH_DATE > '" + strStartTime + "'" : "";
            strSql += (strEndTime.Length > 0) ? " and t2.FINISH_DATE < '" + strEndTime + "'" : "";
            strSql += " ORDER BY t1.ITEM_ID";

            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 按时间、类别筛选实验室空白样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getQCEmptyBatWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            string strSql = @"SELECT    t1.ITEM_ID AS ITEM_ID,t1.ITEM_RESULT, t.* ,t2.FINISH_DATE,t1.RESULT_CHECKOUT LOWER_CHECKOUT ,t3.ITEM_NAME
                                FROM    T_MIS_MONITOR_RESULT_APP AS t2 
                                 JOIN T_MIS_MONITOR_RESULT AS t1 ON t2.RESULT_ID = t1.ID 
                                 JOIN T_BASE_ITEM_INFO AS t3 ON t1.ITEM_ID = t3.ID 
                                 JOIN T_MIS_MONITOR_QC_EMPTY_BAT AS t ON  t1.EMPTY_IN_BAT_ID = t.ID
                                  --join T_BASE_ITEM_ANALYSIS as a on a.ITEM_ID=t1.ITEM_ID and a.ANALYSIS_METHOD_ID=t1.ANALYSIS_METHOD_ID 
                               WHERE   1=1 ";
            strSql += (strMontorID.Length > 0) ? " and t3.MONITOR_ID = '" + strMontorID + "'" : "";
            strSql += (strStartTime.Length > 0) ? " and t2.FINISH_DATE > '" + strStartTime + "'" : "";
            strSql += (strEndTime.Length > 0) ? " and t2.FINISH_DATE < '" + strEndTime + "'" : "";
            strSql += " ORDER BY t1.ITEM_ID";

            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 按时间、类别筛选平行样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getQCTwinWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            string strSql = @"SELECT    t1.ITEM_ID AS ITEM_ID,t1.ITEM_RESULT, t.* ,t2.FINISH_DATE,t1.RESULT_CHECKOUT LOWER_CHECKOUT ,t3.ITEM_NAME
                                FROM    T_MIS_MONITOR_RESULT_APP AS t2 
                                 JOIN T_MIS_MONITOR_RESULT AS t1 ON t2.RESULT_ID = t1.ID 
                                 JOIN T_BASE_ITEM_INFO AS t3 ON t1.ITEM_ID = t3.ID 
                                 JOIN T_MIS_MONITOR_QC_TWIN AS t ON t1.ID = t.RESULT_ID_SRC
                                 --join T_BASE_ITEM_ANALYSIS as a on a.ITEM_ID=t1.ITEM_ID and a.ANALYSIS_METHOD_ID=t1.ANALYSIS_METHOD_ID 
                                WHERE   1=1 ";
            strSql += (strMontorID.Length > 0) ? " and t3.MONITOR_ID = '" + strMontorID + "'" : "";
            strSql += (strStartTime.Length > 0) ? " and t2.FINISH_DATE > '" + strStartTime + "'" : "";
            strSql += (strEndTime.Length > 0) ? " and t2.FINISH_DATE < '" + strEndTime + "'" : "";
            strSql += " ORDER BY t1.ITEM_ID";

            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 按时间、类别筛选加标样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getQCAddWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            string strSql = @"SELECT    t1.ITEM_ID AS ITEM_ID,t1.ITEM_RESULT, t.* ,t2.FINISH_DATE,t1.RESULT_CHECKOUT LOWER_CHECKOUT ,t3.ITEM_NAME
                                FROM    T_MIS_MONITOR_RESULT_APP AS t2 
                                 JOIN T_MIS_MONITOR_RESULT AS t1 ON t2.RESULT_ID = t1.ID 
                                 JOIN T_BASE_ITEM_INFO AS t3 ON t1.ITEM_ID = t3.ID 
                                 JOIN T_MIS_MONITOR_QC_ADD AS t ON t1.ID = t.RESULT_ID_SRC
                                 --join T_BASE_ITEM_ANALYSIS as a on a.ITEM_ID=t1.ITEM_ID and a.ANALYSIS_METHOD_ID=t1.ANALYSIS_METHOD_ID 
                                WHERE   1=1 ";
            strSql += (strMontorID.Length > 0) ? " and t3.MONITOR_ID = '" + strMontorID + "'" : "";
            strSql += (strStartTime.Length > 0) ? " and t2.FINISH_DATE > '" + strStartTime + "'" : "";
            strSql += (strEndTime.Length > 0) ? " and t2.FINISH_DATE < '" + strEndTime + "'" : "";
            strSql += " ORDER BY t1.ITEM_ID";

            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 按时间、类别筛选加标样分析结果
        /// </summary>
        /// <param name="strMontorID">监测类别</param>
        /// <param name="strStartTime">统计开始时间</param>
        /// <param name="strEndTime">统计结束时间</param>
        public DataTable getQCStWitnTimeAndType(string strMontorID, string strStartTime, string strEndTime)
        {
            string strSql = @"SELECT    t1.ITEM_ID AS ITEM_ID,t1.ITEM_RESULT, t.* ,t2.FINISH_DATE,t1.RESULT_CHECKOUT LOWER_CHECKOUT ,t3.ITEM_NAME
                                FROM    T_MIS_MONITOR_RESULT_APP AS t2 
                                 JOIN T_MIS_MONITOR_RESULT AS t1 ON t2.RESULT_ID = t1.ID 
                                 JOIN T_BASE_ITEM_INFO AS t3 ON t1.ITEM_ID = t3.ID 
                                 JOIN T_MIS_MONITOR_QC_ST AS t ON t1.ID = t.RESULT_ID_SRC
                                 --join T_BASE_ITEM_ANALYSIS as a on a.ITEM_ID=t1.ITEM_ID and a.ANALYSIS_METHOD_ID=t1.ANALYSIS_METHOD_ID 
                                WHERE   1=1 ";
            strSql += (strMontorID.Length > 0) ? " and t3.MONITOR_ID = '" + strMontorID + "'" : "";
            strSql += (strStartTime.Length > 0) ? " and t2.FINISH_DATE > '" + strStartTime + "'" : "";
            strSql += (strEndTime.Length > 0) ? " and t2.FINISH_DATE < '" + strEndTime + "'" : "";
            strSql += " ORDER BY t1.ITEM_ID";

            return SqlHelper.ExecuteDataTable(strSql);
        }
        #endregion

        #region 样品分析任务信息

        /// <summary>
        /// 获取样品分析任务信息
        /// </summary>
        /// <param name="strContractType">合同类型</param>
        /// <param name="strMonitorType">监测类别</param>
        /// <param name="strAnalyseAssignDate">要求分析完成时间</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getAssignmentSheetInfo(string strContractType, string strMonitorType, string strAnalyseAssignDate, string strFlowCode, string strCurrentUserId, string strResultStatus)
        {
            string strWhere0 = strContractType == "" ? "" : " and T_MIS_MONITOR_TASK.CONTRACT_TYPE = '" + strContractType + "'";
            string strWhere1 = strMonitorType == "" ? "" : " and T_MIS_MONITOR_SUBTASK.MONITOR_ID in( '" + strMonitorType + "')";
            string strWhere2 = strAnalyseAssignDate == "" ? "" : " and T_MIS_MONITOR_SUBTASK.ANALYSE_ASSIGN_DATE = '" + strAnalyseAssignDate + "'";

            string strSql = @"select ID,
                                                   SAMPLE_CODE,
                                                   SAMPLE_TYPE,
                                                   '' as ITEM_NAME,
                                                   (select ANALYSE_ASSIGN_DATE
                                                      from T_MIS_MONITOR_SUBTASK
                                                     where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID) ANALYSE_ASSIGN_DATE,
                                                   '' as FINISH_DATE
                                              from T_MIS_MONITOR_SAMPLE_INFO
                                             where NOSAMPLE = '0'
                                               and exists
                                             (select *
                                                      from T_MIS_MONITOR_SUBTASK
                                                     where T_MIS_MONITOR_SUBTASK.ID =
                                                           T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID {1} {2}
                                                       and exists
                                                     (select *
                                                              from T_MIS_MONITOR_TASK
                                                             where T_MIS_MONITOR_TASK.ID = T_MIS_MONITOR_SUBTASK.TASK_ID {0})
                                                       and exists
                                                     (select *
                                                              from T_SYS_DUTY
                                                             where T_SYS_DUTY.DICT_CODE = '{3}'
                                                               and T_SYS_DUTY.MONITOR_TYPE_ID =
                                                                   T_MIS_MONITOR_SUBTASK.MONITOR_ID
                                                               and exists
                                                             (select *
                                                                      from T_SYS_USER_DUTY
                                                                     where T_SYS_USER_DUTY.USERID = '{4}' )
                                               and exists
                                             (select *
                                                      from T_MIS_MONITOR_RESULT
                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                                       and T_MIS_MONITOR_RESULT.PRINTED = '0'
                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{5}'
                                                       and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10'or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                       and exists
                                                     (select *
                                                              from T_BASE_ITEM_INFO
                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))))";
            strSql = string.Format(strSql, strWhere0, strWhere1, strWhere2, strFlowCode, strCurrentUserId, strResultStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                string strSampleId = row["ID"].ToString();
                string strValue = getAssignmentSheetItemName(strSampleId, strResultStatus);
                if (strValue != "@")
                {
                    row["ITEM_NAME"] = strValue.Split('@')[0];
                    row["FINISH_DATE"] = strValue.Split('@')[1];
                }
            }
            return objTable;
        }
        /// <summary>
        /// 获取分析结果录入环节监测项目信息
        /// </summary>
        /// <param name="strSimpleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public string getAssignmentSheetItemName(string strSimpleId, string strResultStatus)
        {
            string strItemValue = "";
            string spit = "";
            string strMaxDate = "";
            string strSql = @"select sum_record.*, T_BASE_ITEM_INFO.ITEM_NAME
                                              from (select record.*,
                                                           T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                           T_MIS_MONITOR_RESULT_APP.ASKING_DATE
                                                      from (select ID, ITEM_ID
                                                              from T_MIS_MONITOR_RESULT
                                                             where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                               and T_MIS_MONITOR_RESULT.PRINTED = '0'
                                                               and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '10'or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                               and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                               and exists (select *
                                                                      from T_MIS_MONITOR_RESULT_APP
                                                                     where T_MIS_MONITOR_RESULT.ID =
                                                                           T_MIS_MONITOR_RESULT_APP.RESULT_ID)
                                                               and exists
                                                             (select *
                                                                      from T_BASE_ITEM_INFO
                                                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                       and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                       and T_BASE_ITEM_INFO.ID =
                                                                           T_MIS_MONITOR_RESULT.ITEM_ID)) record
                                                      left join T_MIS_MONITOR_RESULT_APP
                                                        on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                              left join T_BASE_ITEM_INFO
                                                on sum_record.ITEM_ID = T_BASE_ITEM_INFO.ID";
            strSql = string.Format(strSql, strSimpleId, strResultStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                strItemValue += spit + row["ITEM_NAME"].ToString();
                spit = ",";
                if (row["ASKING_DATE"] != null && row["ASKING_DATE"].ToString() != "")
                {
                    if (strMaxDate == "")
                        strMaxDate = row["ASKING_DATE"].ToString();
                    if (DateTime.Parse(row["ASKING_DATE"].ToString()) > DateTime.Parse(strMaxDate))
                        strMaxDate = row["ASKING_DATE"].ToString();
                }
            }
            return strItemValue + "@" + strMaxDate;
        }

        /// <summary>
        /// 根据勾选测样品获取详细的样品信息
        /// </summary>
        /// <param name="strSampleIds">样品ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getAssignmentSheetInfoBySample(string strSampleIds, string strResultStatus)
        {
            string strSql = @"select ID,
                                           SAMPLE_CODE,
                                           SAMPLE_TYPE,
                                           '' as ITEM_NAME,
                                           (select ANALYSE_ASSIGN_DATE
                                              from T_MIS_MONITOR_SUBTASK
                                             where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID) ANALYSE_ASSIGN_DATE,
                                           '' as FINISH_DATE
                                      from T_MIS_MONITOR_SAMPLE_INFO
                                     where ID in ('{0}')
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_RESULT
                                             where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                               and T_MIS_MONITOR_RESULT.PRINTED = '0'
                                               and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                               and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '10'or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                               and exists
                                             (select *
                                                      from T_BASE_ITEM_INFO
                                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                       and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                       and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))";
            strSql = string.Format(strSql, strSampleIds, strResultStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                string strSampleId = row["ID"].ToString();
                string strValue = getAssignmentSheetItemName(strSampleId, strResultStatus);
                if (strValue != "@")
                {
                    row["ITEM_NAME"] = strValue.Split('@')[0];
                    row["FINISH_DATE"] = strValue.Split('@')[1];
                }
            }
            return objTable;
        }
        /// <summary>
        /// 根据样品ID获取分析负责人和监测项目信息
        /// </summary>
        /// <param name="strSampleIds">样品Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public string getAssignmentSheetUserInfo(string strSampleIds, string strResultStatus)
        {
            string strSql = @"select USER_INFO.ID as USER_ID, USER_INFO.REAL_NAME, '' as ITEM_NAME
                                                  from (select distinct HEAD_USERID
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT_APP.HEAD_USERID is not null
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT_APP.RESULT_ID =
                                                                       T_MIS_MONITOR_RESULT.ID
                                                                   and T_MIS_MONITOR_RESULT.SAMPLE_ID in ('{0}')
                                                                   and T_MIS_MONITOR_RESULT.PRINTED = '0'
                                                                   and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '10'or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                                   and exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID))) DEFAULT_USER
                                                  left join T_SYS_USER USER_INFO
                                                    on DEFAULT_USER.HEAD_USERID = USER_INFO.ID";
            strSql = string.Format(strSql, strSampleIds, strResultStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                string strUserId = row["USER_ID"].ToString();
                string strItemName = getAssignmentSheetUserItemInfo(strSampleIds, strUserId, strResultStatus);
                row["ITEM_NAME"] = strItemName;
            }
            string strSumItemInfo = "";
            foreach (DataRow row in objTable.Rows)
            {
                strSumItemInfo += row["REAL_NAME"].ToString() + ":" + row["ITEM_NAME"].ToString() + "\n\n";
            }
            return strSumItemInfo;
        }
        /// <summary>
        /// 根据样品ID获取分析负责人和监测项目信息 Create By weilin
        /// </summary>
        /// <param name="strSampleIds">样品Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getAssignmentSheetUserInfoEx(string strSampleIds, string strResultStatus)
        {
            string strSql = @"select USER_INFO.ID as USER_ID, USER_INFO.REAL_NAME, '' as ITEM_NAME
                                                  from (select distinct HEAD_USERID
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT_APP.HEAD_USERID is not null
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT_APP.RESULT_ID =
                                                                       T_MIS_MONITOR_RESULT.ID
                                                                   and T_MIS_MONITOR_RESULT.SAMPLE_ID in ('{0}')
                                                                   and T_MIS_MONITOR_RESULT.PRINTED = '0'
                                                                   and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '10'or
                                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                                   and exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID))) DEFAULT_USER
                                                  left join T_SYS_USER USER_INFO
                                                    on DEFAULT_USER.HEAD_USERID = USER_INFO.ID";
            strSql = string.Format(strSql, strSampleIds, strResultStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                string strUserId = row["USER_ID"].ToString();
                string strItemName = getAssignmentSheetUserItemInfo(strSampleIds, strUserId, strResultStatus);
                row["ITEM_NAME"] = strItemName;
            }

            return objTable;
        }
        /// <summary>
        /// 根据样品号，默认负责人获取监测项目信息
        /// </summary>
        /// <param name="strSampleIds">样品号</param>
        /// <param name="strUserId">默认负责人ID</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public string getAssignmentSheetUserItemInfo(string strSampleIds, string strUserId, string strResultStatus)
        {
            string strSql = @"select record.*, T_BASE_ITEM_INFO.ITEM_NAME
                                          from (select distinct ITEM_ID
                                                  from T_MIS_MONITOR_RESULT
                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID in ('{0}')
                                                   and T_MIS_MONITOR_RESULT.PRINTED = '0'
                                                   and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '10'or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT.ID =
                                                               T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                           and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}'
                                                           and T_MIS_MONITOR_RESULT_APP.HEAD_USERID is not null)
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)) record
                                          left join T_BASE_ITEM_INFO
                                            on record.ITEM_ID = T_BASE_ITEM_INFO.ID";
            strSql = string.Format(strSql, strSampleIds, strUserId, strResultStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            string strItemValue = "";
            string spit = "";
            foreach (DataRow row in objTable.Rows)
            {
                strItemValue += spit + row["ITEM_NAME"].ToString();
                spit = ",";
            }
            return strItemValue;
        }

        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="strSampleIds">样品Id</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <param name="strPrintedStatus">打印状态</param>
        /// <returns></returns>
        public bool updateAssignmentSheetResultStatus(string strSampleIds, string strResultStatus, string strPrintedStatus)
        {
            string strSql = @"update T_MIS_MONITOR_RESULT set PRINTED='1'
                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID in ('{0}')
                                           and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '10'or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                           and exists
                                         (select *
                                                  from T_BASE_ITEM_INFO
                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                   and T_BASE_ITEM_INFO.ID =
                                                       T_MIS_MONITOR_RESULT.ITEM_ID)";
            strSql = string.Format(strSql, strSampleIds, strResultStatus, strPrintedStatus);
            return SqlHelper.ExecuteNonQuery(strSql) > 0 ? true : false;
        }



        #endregion

        #region 监测项目及检出限
        /// <summary>
        /// 功能描述：获得监测项目及检出限
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemInfoForReport(string strTaskID)
        {
            string strSQL = @"select result.ITEM_RESULT,sample.SAMPLE_CODE,item.ITEM_NAME,(analysis.ANALYSIS_NAME+method.METHOD_CODE) as METHOD_ANALYSIS,
                                                (item_analysis.LOWER_CHECKOUT+dict.DICT_TEXT) as LOW_VALUE
                                                from T_MIS_MONITOR_RESULT result
                                                join (select * from T_MIS_MONITOR_SAMPLE_INFO where SUBTASK_ID in (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}')) sample on result.SAMPLE_ID=sample.ID
                                                left join T_BASE_ITEM_INFO item on item.IS_DEL='0' and result.ITEM_ID=item.ID
                                                    left join T_BASE_METHOD_INFO method on method.IS_DEL='0' and result.STANDARD_ID=method.ID
                                                        left join T_BASE_METHOD_ANALYSIS analysis on analysis.IS_DEL='0' and result.ANALYSIS_METHOD_ID= analysis.ID 
                                                            left join T_BASE_ITEM_ANALYSIS item_analysis on item_analysis.IS_DEL='0' and item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=analysis.ID
                                                                left join T_SYS_DICT dict on dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT";
            strSQL = string.Format(strSQL, strTaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得任务下所有监测结果信息
        /// 创建时间：2013-05-07
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getTotalItemInfoByTaskID(string strTaskID, string strSampleCode, string strMonitorId)
        {
            string strSQL = @"SELECT result.ITEM_ID,result.ITEM_RESULT,result.QC_TYPE,sample.ID as SAMPLE_ID,sample.SAMPLE_CODE,sample.SAMPLE_NAME,item.ITEM_NAME,item.ORDER_NUM,type.ID as MONITOR_ID,type.MONITOR_TYPE_NAME,point.POINT_NAME,dict.DICT_TEXT as ITEM_UNIT,
                                                (CASE WHEN subtask.SAMPLE_FINISH_DATE IS NOT NULL THEN CONVERT(NVARCHAR(32),subtask.SAMPLE_FINISH_DATE,23) ELSE '' END) as SAMPLE_FINISH_DATE

                                                FROM dbo.T_MIS_MONITOR_RESULT result
                                                JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO sample ON result.SAMPLE_ID=sample.ID
                                                JOIN dbo.T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_ID='{0}' AND sample.SUBTASK_ID=subtask.ID
                                                JOIN dbo.T_BASE_ITEM_INFO item on item.IS_DEL='0' and result.ITEM_ID=item.ID
                                                JOIN dbo.T_BASE_MONITOR_TYPE_INFO type ON subtask.MONITOR_ID=type.ID
                                                LEFT JOIN dbo.T_MIS_MONITOR_TASK_POINT point ON point.TASK_ID='{0}' AND point.SUBTASK_ID=subtask.ID AND point.MONITOR_ID=type.ID
                                                LEFT JOIN T_BASE_METHOD_ANALYSIS analysis ON result.ANALYSIS_METHOD_ID= analysis.ID 
                                                LEFT JOIN T_BASE_ITEM_ANALYSIS item_analysis ON item_analysis.ITEM_ID=item.ID and item_analysis.ANALYSIS_METHOD_ID=analysis.ID
                                                LEFT JOIN T_SYS_DICT dict ON dict.DICT_TYPE='item_unit' and dict.DICT_CODE=item_analysis.UNIT
                                                WHERE 1=1";
            if (!string.IsNullOrEmpty(strSampleCode))
            {
                strSQL += string.Format(" AND sample.SAMPLE_CODE like '%{0}%'", strSampleCode);
            }
            if (!string.IsNullOrEmpty(strMonitorId))
            {
                strSQL += string.Format(" AND type.ID='{0}'", strMonitorId);
            }
            strSQL += " order by MONITOR_ID,SAMPLE_CODE,SAMPLE_NAME,ORDER_NUM";
            strSQL = string.Format(strSQL, strTaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得监测项目
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemInfoForReport(string strTaskID, string strPointID, string strMonitorType)
        {
            string strSQL = @"select distinct result.ITEM_ID,item.ITEM_NAME
                                                from T_MIS_MONITOR_RESULT result
                                                INNER JOIN T_BASE_ITEM_INFO item ON item.ID=result.ITEM_ID
                                                    where result.QC_TYPE='0' and 
                                                    result.SAMPLE_ID in (select ID from T_MIS_MONITOR_SAMPLE_INFO where QC_TYPE='0' and POINT_ID='{2}' and
                                                    SUBTASK_ID in (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' and MONITOR_ID='{1}'))";
            strSQL = string.Format(strSQL, strTaskID, strMonitorType, strPointID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 扩展函数
        /// <summary>
        ///  功能描述：获取样品所有项目详细信息（自送样报告用）
        ///  创建时间：2013-1-2
        ///  创建人：邵世卓
        /// </summary>
        /// <param name="strSampelID">样品ID</param>
        /// <param name="intPageIndex">序号</param>
        /// <param name="intPageSize">页码</param>
        /// <returns></returns>
        public DataTable SelectByTableForReport(string strSampelID, string strQcType, int intPageIndex, int intPageSize)
        {
            string strSQL = " select distinct sample.POINT_ID,info.ITEM_NAME,result.ITEM_ID,result.ITEM_RESULT,result.QC,u.REAL_NAME as HEAD_USER,(analysis.ANALYSIS_NAME+method.METHOD_CODE) as METHOD_NAME,apparatus.NAME as APPARATUS_NAME, " +
                            "(case when item.ST_LOWER is not null then item.ST_LOWER+(case when item.ST_UPPER is not null then '~'+item.ST_UPPER else '' end) else item.ST_UPPER end) as STANDARD_VALUE,result.REMARK_5,result.ID,result.RESULT_CHECKOUT" +
                            " from T_MIS_MONITOR_RESULT result" +// 关联样品结果表
                            " left join T_MIS_MONITOR_SAMPLE_INFO sample on  result.SAMPLE_ID=sample.ID" +//关联样品表
                            " left join T_MIS_MONITOR_RESULT_APP app on result.ID = app.RESULT_ID" +//关联分析执行表
                            " left join T_SYS_USER u on u.IS_DEL='0' and u.IS_USE='1' and u.ID=app.HEAD_USERID" +//关联分析负责人
                            " left join T_BASE_ITEM_ANALYSIS ia on ia.IS_DEL='0' and ia.ITEM_ID=result.ITEM_ID and ia.ID=result.ANALYSIS_METHOD_ID" +//关联监测项目分析方法管理表
                            " left join T_BASE_METHOD_ANALYSIS analysis on analysis.IS_DEL='0' and ia.ANALYSIS_METHOD_ID=analysis.ID" +//关联分析方法表
                            " left join T_BASE_METHOD_INFO method on method.IS_DEL='0' and analysis.METHOD_ID=method.ID" +//关联方法依据表
                            " left join T_BASE_APPARATUS_INFO apparatus on apparatus.IS_DEL='0' and apparatus.ID = ia.INSTRUMENT_ID" +//关联仪器信息表
                            " left join T_BASE_ITEM_INFO info on info.IS_DEL='0' and info.ID=result.ITEM_ID" +//关联项目表
                            " left join T_MIS_MONITOR_TASK_ITEM item on info.ID=item.ITEM_ID and sample.POINT_ID=item.TASK_POINT_ID";//关联项目明细表
            string strWhere = " where 1=1";
            if (strSampelID.Length > 0)
            {
                strWhere += string.Format(" and result.SAMPLE_ID='{0}' ", strSampelID);
            }
            if (strQcType.Length > 0)
            {
                strWhere += string.Format(" and result.QC_TYPE='{0}' ", strQcType);
            }
            strSQL += strWhere + "order by result.ITEM_ID";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取污染源列表 胡方扬 2013-03-10
        /// </summary>
        /// <returns></returns>
        public DataTable SelectPolSourceListTable(int iIndex, int iCount)
        {
            string strSQL = String.Format(@" SELECT DISTINCT (E.ID) ,
CONVERT(VARCHAR(100), D.SAMPLE_FINISH_DATE,23) AS SAMPLE_FINISH_DATE,
F.PROJECT_NAME,F.CONTRACT_CODE,H.REPORT_CODE,G.COMPANY_NAME,S.DICT_TEXT AS CONTRACT_TYPENAME
FROM T_MIS_MONITOR_RESULT_APP A  
LEFT JOIN  T_MIS_MONITOR_RESULT B ON B.ID=A.RESULT_ID AND B.ITEM_RESULT IS NOT NULL  AND QC_TYPE='0'
LEFT JOIN T_MIS_MONITOR_SAMPLE_INFO C ON C.ID=B.SAMPLE_ID 
LEFT JOIN T_MIS_MONITOR_SUBTASK D ON D.ID=C.SUBTASK_ID 
LEFT JOIN T_MIS_MONITOR_TASK E ON E.ID=D.TASK_ID 
INNER JOIN dbo.T_MIS_CONTRACT F ON F.ID=E.CONTRACT_ID 
LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON G.ID=E.TESTED_COMPANY_ID 
LEFT JOIN T_MIS_MONITOR_REPORT H ON H.TASK_ID=E.ID 
LEFT JOIN dbo.T_SYS_USER J ON J.ID=A.HEAD_USERID 
LEFT JOIN dbo.T_SYS_USER_POST K ON K.USER_ID=J.ID 
LEFT JOIN dbo.T_SYS_POST L ON L.ID=K.POST_ID 
LEFT JOIN T_SYS_DICT S ON S.DICT_CODE=F.CONTRACT_TYPE AND S.DICT_TYPE='Contract_Type'
WHERE A.ASKING_DATE IS NOT NULL ");
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }


        /// <summary>
        /// 获取污染源列表总数 胡方扬 2013-03-10
        /// </summary>
        /// <returns></returns>
        public int SelectPolSourceListTableCount()
        {
            string strSQL = String.Format(@" SELECT DISTINCT (E.ID) ,
CONVERT(VARCHAR(100), D.SAMPLE_FINISH_DATE,23) AS SAMPLE_FINISH_DATE,
F.PROJECT_NAME,F.CONTRACT_CODE,H.REPORT_CODE,G.COMPANY_NAME,S.DICT_TEXT AS CONTRACT_TYPENAME
FROM T_MIS_MONITOR_RESULT_APP A  
LEFT JOIN  T_MIS_MONITOR_RESULT B ON B.ID=A.RESULT_ID AND B.ITEM_RESULT IS NOT NULL  AND QC_TYPE='0'
LEFT JOIN T_MIS_MONITOR_SAMPLE_INFO C ON C.ID=B.SAMPLE_ID 
LEFT JOIN T_MIS_MONITOR_SUBTASK D ON D.ID=C.SUBTASK_ID 
LEFT JOIN T_MIS_MONITOR_TASK E ON E.ID=D.TASK_ID 
INNER JOIN dbo.T_MIS_CONTRACT F ON F.ID=E.CONTRACT_ID 
LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G ON G.ID=E.TESTED_COMPANY_ID 
LEFT JOIN T_MIS_MONITOR_REPORT H ON H.TASK_ID=E.ID 
LEFT JOIN dbo.T_SYS_USER J ON J.ID=A.HEAD_USERID 
LEFT JOIN dbo.T_SYS_USER_POST K ON K.USER_ID=J.ID 
LEFT JOIN dbo.T_SYS_POST L ON L.ID=K.POST_ID 
LEFT JOIN T_SYS_DICT S ON S.DICT_CODE=F.CONTRACT_TYPE AND S.DICT_TYPE='Contract_Type'
WHERE A.ASKING_DATE IS NOT NULL ");
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 获取污染源明细排口明细
        /// </summary>
        /// <param name="strTask_id"></param>
        /// <returns></returns>
        public DataTable GetPolSourceDetail(string strTask_id, int iIndex, int iCount)
        {

            string strSQL = String.Format(@"SELECT * FROM (
SELECT A.ID, B.ID AS RESULT_ID,
B.RESULT_STATUS,CONVERT (decimal(18,1),CASE WHEN B.ITEM_RESULT IS NULL THEN '0' WHEN B.ITEM_RESULT='NULL' THEN '0' ELSE  B.ITEM_RESULT END) AS ITEM_RESULT,C.ID AS SAMPLE_ID,C.SAMPLE_CODE,CONVERT(VARCHAR(100), D.SAMPLE_FINISH_DATE,23) AS SAMPLE_FINISH_DATE,E.ID AS TASK_ID,F.PROJECT_NAME,F.CONTRACT_CODE,H.REPORT_CODE,G.COMPANY_NAME,
I.ID AS ITEM_ID,I.ITEM_NAME,J.REAL_NAME,M.DICT_TEXT AS DEPT_NAME,N.POINT_ID,N.POINT_NAME,N.MONITOR_ID,
O.ST_UPPER,O.ST_LOWER,O.CONDITION_ID,CONVERT (decimal(18,1),CASE WHEN P.DISCHARGE_UPPER IS NULL THEN '0' WHEN P.DISCHARGE_UPPER='NULL' THEN '0' ELSE  P.DISCHARGE_UPPER END) AS DISCHARGE_UPPER,CONVERT(decimal(18,1),CASE WHEN P.DISCHARGE_LOWER IS NULL THEN '0' WHEN P.DISCHARGE_LOWER='NULL' THEN '0' ELSE  P.DISCHARGE_LOWER END) AS DISCHARGE_LOWER,
QJVALUE=DISCHARGE_UPPER+'~'+DISCHARGE_LOWER,R.DICT_TEXT AS UNITNAME,S.DICT_TEXT AS CONTRACT_TYPENAME
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
LEFT JOIN T_SYS_DICT R ON R.DICT_CODE=P.UNIT AND R.DICT_TYPE='item_unit' 
LEFT JOIN T_SYS_DICT S ON S.DICT_CODE=F.CONTRACT_TYPE AND S.DICT_TYPE='Contract_Type'
WHERE A.ASKING_DATE IS NOT NULL AND E.ID='{0}' ) tempTab
WHERE tempTab.ITEM_RESULT>tempTab.DISCHARGE_UPPER OR tempTab.ITEM_RESULT<tempTab.DISCHARGE_LOWER
", strTask_id);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }


        /// <summary>
        /// 获取污染源明细排口明细总数
        /// </summary>
        /// <param name="strTask_id"></param>
        /// <returns></returns>
        public int GetPolSourceDetailCount(string strTask_id)
        {

            string strSQL = String.Format(@"SELECT * FROM (
SELECT A.ID, B.ID AS RESULT_ID,
B.RESULT_STATUS,CONVERT (decimal(18,1),CASE WHEN B.ITEM_RESULT IS NULL THEN '0' WHEN B.ITEM_RESULT='NULL' THEN '0' ELSE  B.ITEM_RESULT END) AS ITEM_RESULT,C.ID AS SAMPLE_ID,C.SAMPLE_CODE,CONVERT(VARCHAR(100), D.SAMPLE_FINISH_DATE,23) AS SAMPLE_FINISH_DATE,E.ID AS TASK_ID,F.PROJECT_NAME,F.CONTRACT_CODE,H.REPORT_CODE,G.COMPANY_NAME,
I.ID AS ITEM_ID,I.ITEM_NAME,J.REAL_NAME,M.DICT_TEXT AS DEPT_NAME,N.POINT_ID,N.POINT_NAME,N.MONITOR_ID,
O.ST_UPPER,O.ST_LOWER,O.CONDITION_ID,CONVERT (decimal(18,1),CASE WHEN P.DISCHARGE_UPPER IS NULL THEN '0' WHEN P.DISCHARGE_UPPER='NULL' THEN '0' ELSE  P.DISCHARGE_UPPER END) AS DISCHARGE_UPPER,CONVERT(decimal(18,1),CASE WHEN P.DISCHARGE_LOWER IS NULL THEN '0' WHEN P.DISCHARGE_LOWER='NULL' THEN '0' ELSE  P.DISCHARGE_LOWER END) AS DISCHARGE_LOWER,
QJVALUE=DISCHARGE_UPPER+'~'+DISCHARGE_LOWER,R.DICT_TEXT AS UNITNAME,S.DICT_TEXT AS CONTRACT_TYPENAME
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
LEFT JOIN T_SYS_DICT R ON R.DICT_CODE=P.UNIT AND R.DICT_TYPE='item_unit' 
LEFT JOIN T_SYS_DICT S ON S.DICT_CODE=F.CONTRACT_TYPE AND S.DICT_TYPE='Contract_Type'
WHERE A.ASKING_DATE IS NOT NULL AND E.ID='{0}' ) tempTab
WHERE tempTab.ITEM_RESULT>tempTab.DISCHARGE_UPPER OR tempTab.ITEM_RESULT<tempTab.DISCHARGE_LOWER
", strTask_id);
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 邵世卓 清远需求
        /// <summary>
        /// 现场项目的结果信息
        /// </summary>
        /// <param name="TMisMonitorResultVo"></param>
        /// <returns></returns>
        public DataTable SelectSceneItemInfo(TMisMonitorResultVo TMisMonitorResultVo)
        {
            string strSQL = @"SELECT     result.*,(case when subtask.SAMPLE_FINISH_DATE is not null then convert(nvarchar(32),subtask.SAMPLE_FINISH_DATE,23) else '' end) as SAMPLE_FINISH_DATE,subtask.SAMPLING_MANAGER_ID,subtask.SAMPLING_ID,subtask.SAMPLING_MAN,subtask.SAMPLE_ASK_DATE
                                                FROM       (SELECT * FROM T_MIS_MONITOR_RESULT {0}) result
                                                INNER JOIN T_MIS_MONITOR_SAMPLE_INFO sample ON result.SAMPLE_ID=sample.ID
                                                INNER JOIN T_MIS_MONITOR_SUBTASK subtask ON subtask.ID=sample.SUBTASK_ID
                                                INNER JOIN T_BASE_ITEM_INFO item ON item.HAS_SUB_ITEM = '0' 
                                                and (item.IS_SAMPLEDEPT = '是' OR item.IS_ANYSCENE_ITEM='1') and item.ID =result.ITEM_ID";
            strSQL = string.Format(strSQL, BuildWhereStatement(TMisMonitorResultVo));
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        public DataTable SelectSceneItemInfo_MAS(TMisMonitorResultVo TMisMonitorResultVo, string strSample)
        {
            string strSQL = @"SELECT sample.REMARK4 as XieTongRenID ,sample.REMARK5 as CaiYangRenID, result.*,(case when subtask.SAMPLE_FINISH_DATE is not null then convert(nvarchar(32),subtask.SAMPLE_FINISH_DATE,23) else '' end) as SAMPLE_FINISH_DATE,subtask.SAMPLING_MANAGER_ID,subtask.SAMPLING_ID,subtask.SAMPLING_MAN,subtask.SAMPLE_ASK_DATE
                                                FROM       (SELECT * FROM T_MIS_MONITOR_RESULT {0}) result
                                                INNER JOIN T_MIS_MONITOR_SAMPLE_INFO sample ON result.SAMPLE_ID=sample.ID
                                                INNER JOIN T_MIS_MONITOR_SUBTASK subtask ON subtask.ID=sample.SUBTASK_ID
                                                INNER JOIN T_BASE_ITEM_INFO item ON item.HAS_SUB_ITEM = '0' 
                                                and ({1}) and item.ID =result.ITEM_ID";
            if (strSample == "1")
                strSQL = string.Format(strSQL, BuildWhereStatement(TMisMonitorResultVo), "item.IS_SAMPLEDEPT = '是'"); 
            else
                strSQL = string.Format(strSQL, BuildWhereStatement(TMisMonitorResultVo), "item.IS_ANYSCENE_ITEM='1'"); 
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：判断任务是否可以发送至下环节（清远 现场项目审核）
        /// 创建时间：2013-3-17
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strCurrentUserId">人员ID</param>
        /// <param name="strSubTaskStatus1">原子监测任务分析环节状态ID</param>
        /// <param name="strSubTaskStatus2">现场监测任务完成的状态ID</param>
        /// <returns></returns>
        public bool TaskCanSendInQcCheck_QY(string strTaskId, string strCurrentUserId, string strDutyCode, string strSubTaskStatus1, string strSubTaskStatus2)
        {
            string strSql = @"SELECT COUNT(*)
                                          FROM T_MIS_MONITOR_SUBTASK subtask1
                                         INNER JOIN T_MIS_MONITOR_SUBTASK subtask2
                                            ON subtask2.TASK_STATUS <> '{4}'
                                           AND subtask2.TASK_ID = '{0}'
                                           AND subtask2.REMARK1 = subtask1.ID
                                         INNER JOIN T_SYS_DUTY duty
                                            ON duty.DICT_CODE = '{2}'
                                           AND duty.MONITOR_TYPE_ID = subtask2.MONITOR_ID
                                         INNER JOIN (select * from T_SYS_USER_DUTY where USERID = '{1}') user_duty
                                            ON user_duty.DUTY_ID = duty.ID
                                         where subtask1.TASK_ID = '{0}'
                                           AND subtask1.TASK_STATUS = '{3}'";
            strSql = string.Format(strSql, strTaskId, strCurrentUserId, strDutyCode, strSubTaskStatus1, strSubTaskStatus2);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSql).ToString()) > 0 ? true : false;
        }

        /// <summary>
        /// 根据子任务ID获取现场项目
        /// </summary>
        /// <param name="strSubtaskID">监测子任务ID</param>
        /// <returns></returns>
        public DataTable SelectSampleItemWithSubtaskID(string strSubtaskID, IList<string> sampleIdList = null)
        {
            string strSQL = @"SELECT     result.*,item.ITEM_NAME,item.IS_ANYSCENE_ITEM
                                                FROM       T_MIS_MONITOR_RESULT result
                                                INNER JOIN 
                                                T_MIS_MONITOR_SAMPLE_INFO info ON 
                                                result.SAMPLE_ID = info.ID
                                                inner join  T_MIS_MONITOR_SUBTASK subtask on(subtask.ID=info.SUBTASK_ID or subtask.REMARK1=info.SUBTASK_ID)
                                                INNER JOIN
                                                T_BASE_ITEM_INFO item ON item.HAS_SUB_ITEM = '0' 
                                                and (item.IS_SAMPLEDEPT = '是') and item.ID =result.ITEM_ID where subtask.ID='{0}'";
            strSQL = string.Format(strSQL, strSubtaskID);

            if (sampleIdList != null && sampleIdList.Count > 0)
            {
                var str = string.Join("','", sampleIdList);

                strSQL += string.Format(" and info.ID in ('{0}') ", str);
            }

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据子任务ID把该任务下所有分析类现场监测项目的结果状态改为：00     Create by weilin
        /// </summary>
        /// <param name="strSubtaskID"></param>
        /// <returns></returns>
        public bool setSampleItemWithSubtaskID(string strSubtaskID)
        {
            string strSql = @"update a set a.RESULT_STATUS='00' 
                        from T_MIS_MONITOR_RESULT a 
                        left join T_MIS_MONITOR_SAMPLE_INFO b on(a.SAMPLE_ID=b.ID)
                        left join T_BASE_ITEM_INFO c on(a.ITEM_ID=c.ID)
                        where b.SUBTASK_ID='{0}' and c.IS_ANYSCENE_ITEM='{1}'";

            strSql = string.Format(strSql, strSubtaskID, "1");

            return SqlHelper.ExecuteNonQuery(strSql) > 0 ? true : false;
        }
        #endregion

        #region 胡方扬 清远需求
        /// <summary>
        /// 创建原因：获取分析类现场监测项目的分析结果
        /// 创建人：胡方扬
        /// 创建时间：2013-07-10
        /// </summary>
        /// <param name="tMisMonitorResult"></param>
        /// <returns></returns>
        public DataTable GetSampleAnalysisResult(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = @" SELECT     result.*,(case when subtask.SAMPLE_FINISH_DATE is not null then convert(nvarchar(32),subtask.SAMPLE_FINISH_DATE,23) else '' end) as SAMPLE_FINISH_DATE,subtask.SAMPLING_MANAGER_ID,subtask.SAMPLING_ID,subtask.SAMPLING_MAN
FROM (SELECT A.* FROM dbo.T_MIS_MONITOR_RESULT A 
LEFT JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO B ON A.SAMPLE_ID=B.ID
LEFT JOIN dbo.T_MIS_MONITOR_SUBTASK C ON C.ID=B.SUBTASK_ID
LEFT JOIN dbo.T_BASE_ITEM_INFO D ON D.ID=A.ITEM_ID AND D.HAS_SUB_ITEM = '0' 
WHERE A.QC_TYPE = '{0}' 
AND A.RESULT_STATUS = '{1}' AND A.SAMPLE_ID='{2}'AND D.IS_ANYSCENE_ITEM='1') result
INNER JOIN T_MIS_MONITOR_SAMPLE_INFO sample ON result.SAMPLE_ID=sample.ID
INNER JOIN T_MIS_MONITOR_SUBTASK subtask ON subtask.ID=sample.SUBTASK_ID";
            strSQL = String.Format(strSQL,tMisMonitorResult.QC_TYPE,tMisMonitorResult.RESULT_STATUS,tMisMonitorResult.SAMPLE_ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        public DataTable GetSampleAnalysisResult_MAS(TMisMonitorResultVo tMisMonitorResult)
        {
            string strSQL = @" SELECT     result.*,(case when subtask.SAMPLE_FINISH_DATE is not null then convert(nvarchar(32),subtask.SAMPLE_FINISH_DATE,23) else '' end) as SAMPLE_FINISH_DATE,subtask.SAMPLING_MANAGER_ID,subtask.SAMPLING_ID,subtask.SAMPLING_MAN
FROM (SELECT A.* FROM dbo.T_MIS_MONITOR_RESULT A 
LEFT JOIN dbo.T_MIS_MONITOR_SAMPLE_INFO B ON A.SAMPLE_ID=B.ID
LEFT JOIN dbo.T_MIS_MONITOR_SUBTASK C ON C.ID=B.SUBTASK_ID
LEFT JOIN dbo.T_BASE_ITEM_INFO D ON D.ID=A.ITEM_ID AND D.HAS_SUB_ITEM = '0' 
WHERE A.QC_TYPE = '{0}' 
AND A.ID = '{1}' AND A.SAMPLE_ID='{2}'AND D.IS_ANYSCENE_ITEM='1') result
INNER JOIN T_MIS_MONITOR_SAMPLE_INFO sample ON result.SAMPLE_ID=sample.ID
INNER JOIN T_MIS_MONITOR_SUBTASK subtask ON subtask.ID=sample.SUBTASK_ID";
            strSQL = String.Format(strSQL, tMisMonitorResult.QC_TYPE, tMisMonitorResult.ID, tMisMonitorResult.SAMPLE_ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 创建原因：根据指定的监测子任务ID获取监测样品信息
        /// 创建人：胡方扬
        /// 创建时间：2013-07-10
        /// </summary>
        /// <param name="strSubTaskId"></param>
        /// <returns></returns>
        public DataTable GetSampleAnalysisSample(string strSubTaskId,int intPageIndex,int intPageSize) {
            //string strSQL = @" SELECT * FROM  dbo.T_MIS_MONITOR_SAMPLE_INFO A
            string strSQL = @" SELECT DISTINCT(A.ID) AS SAMPLEINFOR_ID,A.* FROM  dbo.T_MIS_MONITOR_SAMPLE_INFO A
INNER JOIN dbo.T_MIS_MONITOR_RESULT B ON B.SAMPLE_ID=A.ID
INNER JOIN dbo.T_BASE_ITEM_INFO C ON C.ID=B.ITEM_ID  AND C.HAS_SUB_ITEM = '0'  AND C.IS_ANYSCENE_ITEM='1'
WHERE A.SUBTASK_ID='{0}'";
            strSQL = String.Format(strSQL, strSubTaskId);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL,intPageIndex,intPageSize));
        }

        /// <summary>
        /// 创建原因：根据指定的监测子任务ID获取监测样品信息条目数
        /// 创建人：胡方扬
        /// 创建时间：2013-07-10
        /// </summary>
        /// <param name="strSubTaskId"></param>
        /// <returns></returns>
        public int GetSampleAnalysisSampleCount(string strSubTaskId)
        {
            //string strSQL = @" SELECT * FROM  dbo.T_MIS_MONITOR_SAMPLE_INFO A
            string strSQL = @" SELECT DISTINCT(A.ID) AS SAMPLEINFOR_ID,A.* FROM  dbo.T_MIS_MONITOR_SAMPLE_INFO A
INNER JOIN dbo.T_MIS_MONITOR_RESULT B ON B.SAMPLE_ID=A.ID
INNER JOIN dbo.T_BASE_ITEM_INFO C ON C.ID=B.ITEM_ID  AND C.HAS_SUB_ITEM = '0'  AND C.IS_ANYSCENE_ITEM='1'
WHERE A.SUBTASK_ID='{0}'";
            strSQL = String.Format(strSQL, strSubTaskId);
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        public DataTable GetSampleAnalysisSample_MAS(string strSubTaskId, string strResultID, int intPageIndex, int intPageSize)
        {
            string strSQL = @" SELECT DISTINCT(A.ID) AS SAMPLEINFOR_ID,A.* FROM  dbo.T_MIS_MONITOR_SAMPLE_INFO A
INNER JOIN dbo.T_MIS_MONITOR_RESULT B ON B.SAMPLE_ID=A.ID
INNER JOIN dbo.T_BASE_ITEM_INFO C ON C.ID=B.ITEM_ID  AND C.HAS_SUB_ITEM = '0'  AND C.IS_ANYSCENE_ITEM='1'
WHERE A.SUBTASK_ID='{0}' AND B.ID='{1}'";
            strSQL = String.Format(strSQL, strSubTaskId, strResultID);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        public int GetSampleAnalysisSampleCount_MAS(string strSubTaskId, string strResultID)
        {
            string strSQL = @" SELECT DISTINCT(A.ID) AS SAMPLEINFOR_ID,A.* FROM  dbo.T_MIS_MONITOR_SAMPLE_INFO A
INNER JOIN dbo.T_MIS_MONITOR_RESULT B ON B.SAMPLE_ID=A.ID
INNER JOIN dbo.T_BASE_ITEM_INFO C ON C.ID=B.ITEM_ID  AND C.HAS_SUB_ITEM = '0'  AND C.IS_ANYSCENE_ITEM='1'
WHERE A.SUBTASK_ID='{0}' AND B.ID='{1}'";
            strSQL = String.Format(strSQL, strSubTaskId, strResultID);
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 潘德军 清远需求
        /// <summary>
        /// 监测数据统计表:导出已出报告的符合检索条件的报表（EXCEL格式）
        /// </summary>
        /// <param name="TMisMonitorResultVo"></param>
        /// <returns></returns>
        public DataTable SelectTestDataLst(string[] strWhereArr, int iIndex, int iCount)
        {
            string strSQL = @"select r.id,c.COMPANY_NAME,d.dict_text as CONTRACT_TYPE,m.MONITOR_TYPE_NAME as MONITOR_NAME,
                                case when s.POINT_ID is NULL then s.SAMPLE_NAME else p.POINT_NAME end as POINT_NAME,
                                st.SAMPLE_FINISH_DATE,i.ITEM_NAME,r.ITEM_RESULT + '('+iad.dict_text+')' as ITEM_RESULT,
                                case s.QC_TYPE when '0' then '原始样' when '1' then '现场空白'  when '2' then '现场加标' 
                                        when '3' then '现场平行' when '4' then '实验室密码平行'  when '5' then '实验室空白' 
                                        when '6' then '实验室加标' when '7' then '实验室明码平行'  when '8' then '标准样' end as QC_TYPE 
                         from T_MIS_MONITOR_RESULT r 
                            join T_MIS_MONITOR_SAMPLE_INFO s on  r.SAMPLE_ID=s.ID 
                            join T_MIS_MONITOR_SUBTASK st on st.ID=s.SUBTASK_ID 
                            join T_MIS_MONITOR_TASK t on t.ID=st.TASK_ID  
                            join T_MIS_MONITOR_TASK_COMPANY c on c.Id=t.TESTED_COMPANY_ID 
                            join T_SYS_DICT d on d.dict_code=t.CONTRACT_TYPE and d.dict_type='CONTRACT_TYPE' 
                            join T_BASE_MONITOR_TYPE_INFO m on m.ID=st.MONITOR_ID 
                            left join T_MIS_MONITOR_TASK_POINT p on p.ID=s.POINT_ID 
                            join T_BASE_ITEM_INFO i on i.ID=r.ITEM_ID  
                            left join T_BASE_ITEM_ANALYSIS ia on ia.ITEM_ID=r.ITEM_ID and ia.ANALYSIS_METHOD_ID=r.ANALYSIS_METHOD_ID
                            left join T_SYS_DICT iad on iad.dict_code=ia.UNIT and iad.dict_type='item_unit'  
                         where 1=1 and t.TASK_STATUS='11'";
            if (strWhereArr[0].Length > 0)
                strSQL += string.Format(" and c.COMPANY_NAME like '%{0}%'", strWhereArr[0]);
            if (strWhereArr[1].Length > 0)
                strSQL += string.Format(" and t.CONTRACT_TYPE = '{0}'", strWhereArr[1]);
            if (strWhereArr[2].Length > 0)
                strSQL += string.Format(" and st.SAMPLE_FINISH_DATE>='{0}'", strWhereArr[2]);
            if (strWhereArr[3].Length > 0)
                strSQL += string.Format(" and st.SAMPLE_FINISH_DATE<'{0}'", strWhereArr[3]);
            strSQL += " order by t.id,st.id,s.id";

            strSQL = string.Format(strSQL, strWhereArr);
            strSQL = BuildPagerExpress(strSQL, iIndex, iCount);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorResult">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectTestDataLstCount(string[] strWhereArr)
        {
            string strSQL = @"select c.COMPANY_NAME,d.dict_text as CONTRACT_TYPE,m.MONITOR_TYPE_NAME as MONITOR_NAME,
                                p.POINT_NAME,st.SAMPLE_FINISH_DATE,i.ITEM_NAME,r.ITEM_RESULT
                         from T_MIS_MONITOR_RESULT r 
                            join T_MIS_MONITOR_SAMPLE_INFO s on  r.SAMPLE_ID=s.ID 
                            join T_MIS_MONITOR_SUBTASK st on st.ID=s.SUBTASK_ID 
                            join T_MIS_MONITOR_TASK t on t.ID=st.TASK_ID  
                            join T_MIS_MONITOR_TASK_COMPANY c on c.Id=t.TESTED_COMPANY_ID 
                            join T_SYS_DICT d on d.dict_code=t.CONTRACT_TYPE and d.dict_type='CONTRACT_TYPE' 
                            join T_BASE_MONITOR_TYPE_INFO m on m.ID=st.MONITOR_ID 
                            left join T_MIS_MONITOR_TASK_POINT p on p.ID=s.POINT_ID 
                            join T_BASE_ITEM_INFO i on i.ID=r.ITEM_ID  
                         where 1=1  and t.TASK_STATUS='11' ";
            if (strWhereArr[0].Length > 0)
                strSQL += string.Format(" and c.COMPANY_NAME like '%{0}%'", strWhereArr[0]);
            if (strWhereArr[1].Length > 0)
                strSQL += string.Format(" and t.CONTRACT_TYPE = '{0}'", strWhereArr[1]);
            if (strWhereArr[2].Length > 0)
                strSQL += string.Format(" and st.SAMPLE_FINISH_DATE>='{0}'", strWhereArr[2]);
            if (strWhereArr[3].Length > 0)
                strSQL += string.Format(" and st.SAMPLE_FINISH_DATE<'{0}'", strWhereArr[3]);

            strSQL = string.Format("select Count(*) from ({0})t ", strSQL);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
        #endregion

        /// <summary>
        /// 报告份数统计表 Create By： weilin 2014-11-21
        /// </summary>
        /// <param name="TMisMonitorResultVo"></param>
        /// <returns></returns>
        public DataTable SelectReportDataLst(string strContractType, string strMonitorType, string strStartDate, string strEndDate)
        {
            string strMonitorName = "";
            if (strMonitorType.Length > 0) {
                strMonitorName = new i3.DataAccess.Channels.Base.MonitorType.TBaseMonitorTypeInfoAccess().Details(strMonitorType).MONITOR_TYPE_NAME;
            }

            string strSQL = @"select c.DICT_TEXT CONTRACT_TYPE, '{0}' MONITOR_TYPE,COUNT(*) ACCOUNT from T_MIS_MONITOR_TASK a
                            inner join T_MIS_MONITOR_REPORT b on(a.ID=b.TASK_ID)
                            left join T_SYS_DICT c on(a.CONTRACT_TYPE=c.DICT_CODE and c.DICT_TYPE='Contract_Type')
                            where a.TASK_STATUS='11' and 
                            exists(select * from T_MIS_MONITOR_SUBTASK where a.ID=TASK_ID {1})";
            strSQL = string.Format(strSQL, strMonitorName, strMonitorType == "" ? "" : " and MONITOR_ID='" + strMonitorType + "'");

            if (strContractType.Length > 0)
                strSQL += string.Format(" and a.CONTRACT_TYPE = '{0}'", strContractType);
            if (strStartDate.Length > 0)
                strSQL += string.Format(" and convert(char(10),a.FINISH_DATE,121)>='{0}'", strStartDate);
            if (strEndDate.Length > 0)
                strSQL += string.Format(" and convert(char(10),a.FINISH_DATE,121)<='{0}'", strEndDate);
            strSQL += " GROUP BY c.DICT_TEXT";

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        #region 潘德军 结果数据可追溯性
        /// <summary>
        /// 根据子任务ID获取结果数据DataTable
        /// </summary>
        /// <param name="strSubtaskID">监测子任务ID</param>
        /// <returns></returns>
        public DataTable SelectResult_WithUser(string strUserID, string strCurrResultStatus)
        {
            string strSQL = @"select *
                  from T_MIS_MONITOR_RESULT
                 where (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                       T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                       T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                       T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                       T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                       T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{0}'
                   and exists
                 (select *
                          from T_MIS_MONITOR_RESULT_APP
                         where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                           and (T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}' or
                               T_MIS_MONITOR_RESULT_APP.HEAD_USERID in
                               (select USER_ID
                                   from T_SYS_USER_PROXY
                                  where PROXY_USER_ID = '{1}')))
                   and exists
                 (select *
                          from T_BASE_ITEM_INFO
                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                   and exists
                 (select *
                          from T_MIS_MONITOR_SAMPLE_INFO
                         where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                           and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0') ";
            strSQL = string.Format(strSQL, strCurrResultStatus, strUserID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据发送的结果id串获取结果数据DataTable
        /// </summary>
        /// <param name="strResultIDs">结果id串，多个id用半角逗号分隔</param>
        /// <returns></returns>
        public DataTable SelectResult_WithIDs(string strResultIDs)
        {
            string strSQL = @"SELECT * FROM T_MIS_MONITOR_RESULT where ID in ({0}) ";
            strSQL = string.Format(strSQL, strResultIDs);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据发送的结果id串获取结果数据DataTable
        /// </summary>
        /// <param name="strSampleIDs">结果id串，多个id用半角逗号分隔</param>
        /// <returns></returns>
        public DataTable SelectResult_WithSampleIDs(string strSampleIDs, string strCurrentUserId, string strCurrResultStatus)
        {
            string strSql = @"select * from T_MIS_MONITOR_RESULT
                                                 where SAMPLE_ID in ({0})
                                                   and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '10'or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                           and (T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}' or
                                                               T_MIS_MONITOR_RESULT_APP.HEAD_USERID in
                                                               (select USER_ID
                                                                   from T_SYS_USER_PROXY
                                                                  where PROXY_USER_ID = '{1}')))
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)";
            strSql = string.Format(strSql, strSampleIDs, strCurrentUserId, strCurrResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 根据发送的结果id串获取结果分析执行表数据DataTable
        /// </summary>
        /// <param name="strSubtaskID">结果id串，多个id用半角逗号分隔</param>
        /// <returns></returns>
        public DataTable SelectResultApp_WithIDs(string strResultIDs)
        {
            string strSQL = @"SELECT * FROM T_MIS_MONITOR_RESULT_APP where RESULT_ID in ({0}) ";
            strSQL = string.Format(strSQL, strResultIDs);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 批量插入结果数据可追溯性
        /// </summary>
        /// <param name="arrLog">插入的结果字串数组</param>
        /// <returns>是否成功</returns>
        public bool InsertResultLog(string[] arrLog)
        {
            ArrayList arrSql = new ArrayList();
            for (int i = 0; i < arrLog.Length; i++)
            {
                string strLog = arrLog[i];
                string[] arrInsert = strLog.Split('|');
                string strSQL = string.Format("insert into {0} ({1}) values({2})", "T_MIS_MONITOR_RESULT_LOG",
                    "ID,RESULT_ID,OLD_RESULT,REMARK1,NEW_RESULT,REMARK2,HEAD_USERID,ASSISTANT_USERID,FINISH_DATE",
                    "'" + arrInsert[0] + "','" + arrInsert[1] + "','" + arrInsert[2] + "','" + arrInsert[3] + "','" + arrInsert[4] + "','" + arrInsert[5] + "','" + arrInsert[6] + "','" + arrInsert[7] + "'" + arrInsert[8] + "'");
                arrSql.Add(strSQL);
            }

            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }

        /// <summary>
        /// 获取分析结果校核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <returns></returns>
        public DataTable getTaskItemForSample(string strSampleId, int intPageIndex, int intPageSize)
        {
            string strSql = @"select record.*,  APPARATUS_INFO.APPARATUS_NAME,  APPARATUS_INFO.LOWER_CHECKOUT
                                  from  (select ID,
                                                       ITEM_ID,ITEM_RESULT,QC,ANALYSIS_METHOD_ID,
                                                        (select  DICT_TEXT from dbo.T_SYS_DICT
                                                                             where DICT_TYPE = 'item_unit'
                                                                               and DICT_CODE =
                                                                                   (select UNIT from T_BASE_ITEM_ANALYSIS
                                                                                     where ITEM_ID = T_MIS_MONITOR_RESULT.ITEM_ID
                                                                                       and ANALYSIS_METHOD_ID = T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID
                                                                                       and IS_DEL = '0')) as UNIT,
                                                       (select ANALYSIS_NAME
                                                          from T_BASE_METHOD_ANALYSIS
                                                         where ID = ANALYSIS_METHOD_ID) as ANALYSIS_NAME,
                                                       STANDARD_ID,REMARK_2,
                                                       RESULT_STATUS
                                                  from T_MIS_MONITOR_RESULT
                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                   and (T_MIS_MONITOR_RESULT.QC_TYPE = '0')
                   
                                                   and exists
                                                 (select * from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID =  T_MIS_MONITOR_RESULT.ITEM_ID)) record
          
                                  left join (select (select NAME from T_BASE_APPARATUS_INFO where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                    LOWER_CHECKOUT, ITEM_ID, ANALYSIS_METHOD_ID
                                               from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                    on APPARATUS_INFO.ITEM_ID = record.ITEM_ID and APPARATUS_INFO.ANALYSIS_METHOD_ID = record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSampleId);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 获取分析结果校核环节的监测项目数量
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <returns></returns>
        public int getTaskItemCheckForSampleCount(string strSampleId)
        {
            string strSql = @"select count(*)
                                          from (select record.*,  APPARATUS_INFO.APPARATUS_NAME,  APPARATUS_INFO.LOWER_CHECKOUT
                                              from  (select ID,
                                                                   ITEM_ID,ITEM_RESULT,QC,ANALYSIS_METHOD_ID,
                                                                    (select  DICT_TEXT from dbo.T_SYS_DICT
                                                                                         where DICT_TYPE = 'item_unit'
                                                                                           and DICT_CODE =
                                                                                               (select UNIT from T_BASE_ITEM_ANALYSIS
                                                                                                 where ITEM_ID = T_MIS_MONITOR_RESULT.ITEM_ID
                                                                                                   and ANALYSIS_METHOD_ID = T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID
                                                                                                   and IS_DEL = '0')) as UNIT,
                                                                   (select ANALYSIS_NAME
                                                                      from T_BASE_METHOD_ANALYSIS
                                                                     where ID = ANALYSIS_METHOD_ID) as ANALYSIS_NAME,
                                                                   STANDARD_ID,
                                                                   RESULT_STATUS
                                                              from T_MIS_MONITOR_RESULT
                                                             where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                               and (T_MIS_MONITOR_RESULT.QC_TYPE = '0')
                   
                                                               and exists
                                                             (select * from T_BASE_ITEM_INFO
                                                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                       and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                       and T_BASE_ITEM_INFO.ID =  T_MIS_MONITOR_RESULT.ITEM_ID)) record
          
                                              left join (select (select NAME from T_BASE_APPARATUS_INFO where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                                LOWER_CHECKOUT, ITEM_ID, ANALYSIS_METHOD_ID
                                                           from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                                on APPARATUS_INFO.ITEM_ID = record.ITEM_ID and APPARATUS_INFO.ANALYSIS_METHOD_ID = record.ANALYSIS_METHOD_ID) record_count";
            strSql = string.Format(strSql, strSampleId);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorResult"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorResultVo tMisMonitorResult)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorResult)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorResult.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorResult.ID.ToString()));
                }
                //样品ID
                if (!String.IsNullOrEmpty(tMisMonitorResult.SAMPLE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ID = '{0}'", tMisMonitorResult.SAMPLE_ID.ToString()));
                }
                //质控类型（原始样、现场空白、现场加标、现场平行、实验室密码平行，实验室空白、实验室加标、实验室明码平行）
                if (!String.IsNullOrEmpty(tMisMonitorResult.QC_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_TYPE = '{0}'", tMisMonitorResult.QC_TYPE.ToString()));
                }
                //质控原始样结果ID
                if (!String.IsNullOrEmpty(tMisMonitorResult.QC_SOURCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_SOURCE_ID = '{0}'", tMisMonitorResult.QC_SOURCE_ID.ToString()));
                }
                //最初原始样ID，质控样可能是在原始样上做外控，然后在外控上做内控；或者在原始样上直接内控。那么最初原始样记录的是最早那个原始样的ID
                if (!String.IsNullOrEmpty(tMisMonitorResult.SOURCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SOURCE_ID = '{0}'", tMisMonitorResult.SOURCE_ID.ToString()));
                }
                //检验项
                if (!String.IsNullOrEmpty(tMisMonitorResult.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tMisMonitorResult.ITEM_ID.ToString()));
                }
                //检验项结果
                if (!String.IsNullOrEmpty(tMisMonitorResult.ITEM_RESULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_RESULT = '{0}'", tMisMonitorResult.ITEM_RESULT.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tMisMonitorResult.ANALYSIS_METHOD_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_METHOD_ID = '{0}'", tMisMonitorResult.ANALYSIS_METHOD_ID.ToString()));
                }
                //标准依据ID
                if (!String.IsNullOrEmpty(tMisMonitorResult.STANDARD_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STANDARD_ID = '{0}'", tMisMonitorResult.STANDARD_ID.ToString()));
                }
                //使用的质控手段,对该样采用的质控手段，（现场空白1、现场加标2、现场平行4、实验室密码平行8，实验室空白16、实验室加标32、实验室明码平行64），位运算
                if (!String.IsNullOrEmpty(tMisMonitorResult.QC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC = '{0}'", tMisMonitorResult.QC.ToString()));
                }
                //空白批次表ID
                if (!String.IsNullOrEmpty(tMisMonitorResult.EMPTY_IN_BAT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EMPTY_IN_BAT_ID = '{0}'", tMisMonitorResult.EMPTY_IN_BAT_ID.ToString()));
                }
                //分样号
                if (!String.IsNullOrEmpty(tMisMonitorResult.SUB_SAMPLE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUB_SAMPLE_CODE = '{0}'", tMisMonitorResult.SUB_SAMPLE_CODE.ToString()));
                }
                //任务状态类别(发送，退回)
                if (!String.IsNullOrEmpty(tMisMonitorResult.TASK_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_TYPE = '{0}'", tMisMonitorResult.TASK_TYPE.ToString()));
                }
                //结果状态(分析任务分配：01分析结果填报：02，分析结果校核：03)
                if (!String.IsNullOrEmpty(tMisMonitorResult.RESULT_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_STATUS in ({0})", tMisMonitorResult.RESULT_STATUS.ToString()));
                }
                //采样仪器信息
                if (!String.IsNullOrEmpty(tMisMonitorResult.SAMPLING_INSTRUMENT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_INSTRUMENT = '{0}'", tMisMonitorResult.SAMPLING_INSTRUMENT.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorResult.REMARK_1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK_1 = '{0}'", tMisMonitorResult.REMARK_1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorResult.REMARK_2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK_2 = '{0}'", tMisMonitorResult.REMARK_2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorResult.REMARK_3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK_3 = '{0}'", tMisMonitorResult.REMARK_3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorResult.REMARK_4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK_4 = '{0}'", tMisMonitorResult.REMARK_4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorResult.REMARK_5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK_5 = '{0}'", tMisMonitorResult.REMARK_5.ToString()));
                }
                //结果检出限
                if (!String.IsNullOrEmpty(tMisMonitorResult.RESULT_CHECKOUT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_CHECKOUT = '{0}'", tMisMonitorResult.RESULT_CHECKOUT.ToString()));
                }
                //CCFLOW_ID1
                if (!String.IsNullOrEmpty(tMisMonitorResult.CCFLOW_ID1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID1 = '{0}'", tMisMonitorResult.CCFLOW_ID1.ToString()));
                }
                //CCFLOW_ID2
                if (!String.IsNullOrEmpty(tMisMonitorResult.CCFLOW_ID2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID2 = '{0}'", tMisMonitorResult.CCFLOW_ID2.ToString()));
                }
                //CCFLOW_ID3
                if (!String.IsNullOrEmpty(tMisMonitorResult.CCFLOW_ID3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID3 = '{0}'", tMisMonitorResult.CCFLOW_ID3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        #region 黄进军 质控要求
        /// <summary>
        /// 通知样品号查找质控要求
        /// </summary>
        public DataTable getZKYQ(string strSimpleId) 
        {
            ArrayList arrLi = new ArrayList();
            string strSql = @"select T_MIS_MONITOR_SAMPLE_INFO.*,T_MIS_MONITOR_SUBTASK.TASK_ID,T_MIS_MONITOR_TASK.PLAN_ID
                                from T_MIS_MONITOR_SAMPLE_INFO
                                left join T_MIS_MONITOR_SUBTASK
                                on T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID=T_MIS_MONITOR_SUBTASK.ID
                                left join T_MIS_MONITOR_TASK
                                on T_MIS_MONITOR_TASK.ID = T_MIS_MONITOR_SUBTASK.TASK_ID where T_MIS_MONITOR_SAMPLE_INFO.ID ={0}";
            strSql = string.Format(strSql, strSimpleId);
            //arrLi.Add(strSql);
            //return SqlHelper.ExecuteSQLByTransaction(arrLi);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        #endregion
    }
}
