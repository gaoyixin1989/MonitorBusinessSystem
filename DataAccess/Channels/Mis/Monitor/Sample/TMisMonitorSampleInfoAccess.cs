using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：样品表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleInfoAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_SAMPLE_INFO " + this.BuildWhereStatement(tMisMonitorSampleInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_INFO  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorSampleInfoVo(), strSQL);
        }

        public DataTable SelectByTable1(string strResultID)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_INFO  Where 1=1  AND ID in (" + strResultID + ")";
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleInfoVo Details(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_INFO " + this.BuildWhereStatement(tMisMonitorSampleInfo));
            return SqlHelper.ExecuteObject(new TMisMonitorSampleInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleInfoVo> SelectByObject(TMisMonitorSampleInfoVo tMisMonitorSampleInfo, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_INFO " + this.BuildWhereStatement(tMisMonitorSampleInfo));
            return SqlHelper.ExecuteObjectList(tMisMonitorSampleInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleInfoVo tMisMonitorSampleInfo, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_SAMPLE_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSampleInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tMisMonitorSampleInfo, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable，点位、质控信息显示
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableForPoint(TMisMonitorSampleInfoVo tMisMonitorSampleInfo, int iIndex, int iCount)
        {
            string strSQL = @"SELECT     ID, SUBTASK_ID,POINT_ID, SAMPLE_CODE,SAMPLE_BARCODE, SAMPLE_NAME, SAMPLE_COUNT, STATUS, NOSAMPLE, NOSAMPLEREMARK, SPECIALREMARK,SUBSAMPLE_NUM,SAMPLE_ACCEPT_DATEORACC,SAMPLE_REMARK,
                            CASE WHEN qc_type = '0' THEN '原始样' WHEN qc_type = '1' THEN '现场空白' WHEN qc_type = '2' THEN '现场加标' WHEN qc_type = '3' THEN '现场平行' WHEN qc_type = '4' THEN '密码平行' WHEN qc_type='11' THEN '现场密码' ELSE '' END
                            AS REMARK1, QC_SOURCE_ID, QC_TYPE,REMARK2, REMARK3, REMARK4, REMARK5, cast(SAMPLE_FREQ as int) SAMPLE_FREQ 
                            FROM  T_MIS_MONITOR_SAMPLE_INFO
                            left join(
                            select a.ID REPQ_ID,b.SAMPLE_FREQ from T_MIS_MONITOR_TASK_POINT a left join T_MIS_CONTRACT_POINT b on(a.CONTRACT_POINT_ID=b.ID)
                            ) REPQ on(T_MIS_MONITOR_SAMPLE_INFO.POINT_ID=REPQ.REPQ_ID)
                            {0}";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSampleInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tMisMonitorSampleInfo, strSQL, iIndex, iCount));
        }
        public int SelectByTableForPointCount(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            string strSQL = @"SELECT Count(*)
                            FROM  T_MIS_MONITOR_SAMPLE_INFO
                            left join(
                            select a.ID REPQ_ID,b.SAMPLE_FREQ from T_MIS_MONITOR_TASK_POINT a left join T_MIS_CONTRACT_POINT b on(a.CONTRACT_POINT_ID=b.ID)
                            ) REPQ on(T_MIS_MONITOR_SAMPLE_INFO.POINT_ID=REPQ.REPQ_ID)
                            {0}";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSampleInfo));
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
        /// <summary>
        /// 获取采样前质控样品信息 by xwh 2013.9.23
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strQcSourceId">原始样ID</param>
        /// <returns></returns>
        public DataTable getSampleInfoInQcBeginSampling(string strSubTaskId, string strQcSourceId)
        {
            string strSql = @"SELECT ID,
                               SUBTASK_ID,
                               POINT_ID,
                               SAMPLE_CODE,
                               SAMPLE_NAME,
                               SAMPLE_COUNT,
                               STATUS,
                               NOSAMPLE,
                               NOSAMPLEREMARK,
                               SPECIALREMARK,
                               SUBSAMPLE_NUM,
                               CASE
                                 WHEN qc_type = '0' THEN
                                  '原始样'
                                 WHEN qc_type = '1' THEN
                                  '现场空白'
                                 WHEN qc_type = '2' THEN
                                  '现场加标'
                                 WHEN qc_type = '3' THEN
                                  '现场平行'
                                 WHEN qc_type = '4' THEN
                                  '密码平行'
                                 WHEN qc_type = '11' THEN
                                  '现场密码'
                                 ELSE
                                  ''
                               END AS REMARK1,
                               QC_SOURCE_ID,
                               QC_TYPE,
                               REMARK2,
                               REMARK3,
                               REMARK4,
                               REMARK5
                          FROM T_MIS_MONITOR_SAMPLE_INFO
                         Where (SUBTASK_ID = '{0}' AND QC_SOURCE_ID = '{1}')
                            or (SUBTASK_ID = '{0}' and QC_TYPE = '11')";
            strSql = string.Format(strSql, strSubTaskId, strQcSourceId);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_INFO " + this.BuildWhereStatement(tMisMonitorSampleInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary> 
        /// 获取子任务中包含有“'氮氧化物','二氧化硫','烟尘'”项目的样品  Create By weilin 2014-04-14
        /// </summary>
        /// <param name="strSubTaskID"></param>
        /// <returns></returns>
        public DataTable SelectByTableForSO2(string strSubTaskID)
        {
            string strSQL = @"select distinct a.* from T_MIS_MONITOR_SAMPLE_INFO a 
                            left join T_MIS_MONITOR_RESULT b on(a.ID=b.SAMPLE_ID) 
                            left join T_BASE_ITEM_INFO c on(b.ITEM_ID=c.ID)
                            where a.subtask_id='{0}' and c.ITEM_NAME in('氮氧化物','二氧化硫','烟尘','粉尘')";
            strSQL = string.Format(strSQL, strSubTaskID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary> 
        /// 获取样品中包含有“'氮氧化物','二氧化硫','烟尘'”项目的项目结果  Create By weilin 2014-04-14
        /// </summary>
        /// <param name="strSubTaskID"></param>
        /// <returns></returns>
        public DataTable SelectResultForSO2(string strSampleID)
        {
            string strSQL = @"select a.*,b.ITEM_NAME from T_MIS_MONITOR_RESULT a
                            left join T_BASE_ITEM_INFO b on(a.ITEM_ID=b.ID)
                            where a.SAMPLE_ID='{0}' and b.ITEM_NAME in('氮氧化物','二氧化硫','烟尘','粉尘')";
            strSQL = string.Format(strSQL, strSampleID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleInfoVo SelectByObject(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_INFO " + this.BuildWhereStatement(tMisMonitorSampleInfo);
            return SqlHelper.ExecuteObject(new TMisMonitorSampleInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorSampleInfo, TMisMonitorSampleInfoVo.T_MIS_MONITOR_SAMPLE_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleInfo, TMisMonitorSampleInfoVo.T_MIS_MONITOR_SAMPLE_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorSampleInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorSampleInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleInfoVo tMisMonitorSampleInfo_UpdateSet, TMisMonitorSampleInfoVo tMisMonitorSampleInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleInfo_UpdateSet, TMisMonitorSampleInfoVo.T_MIS_MONITOR_SAMPLE_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorSampleInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_SAMPLE_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            string strSQL = "delete from T_MIS_MONITOR_SAMPLE_INFO ";
            strSQL += this.BuildWhereStatement(tMisMonitorSampleInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 功能描述：根据点位ID获取样品信息
        /// 创建时间：2012-12-6
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strPointId">点位 ID</param>
        /// <returns>样品对象</returns>
        public TMisMonitorSampleInfoVo GetSampleInfoByPointID(string strPointId)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_INFO where POINT_ID='{0}'", strPointId);
            return SqlHelper.ExecuteObject(new TMisMonitorSampleInfoVo(), strSQL);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息
        /// 创建时间：2013-1-2
        /// 创建人：邵世卓
        /// <param name="strTaskId">监测任务ID</param>
        /// <returns></returns>
        public List<TMisMonitorSampleInfoVo> GetSampleInfoSourceByTask(string strTaskId)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_INFO where QC_TYPE='0' and SUBTASK_ID in " +
                                        " (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}')";
            strSQL = string.Format(strSQL, strTaskId);
            return SqlHelper.ExecuteObjectList(new TMisMonitorSampleInfoVo(), strSQL);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息
        /// 创建时间：2013-4-27
        /// 创建人：潘德军
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strItemType">选择的监测类型ID串</param>
        /// <returns></returns>
        public List<TMisMonitorSampleInfoVo> GetSampleInfoSourceByTask_ByItemType(string strTaskId, string strItemType)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_INFO where QC_TYPE='0' and SUBTASK_ID in " +
                                        " (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' and CHARINDEX(MONITOR_ID,'{1}')>0)";
            strSQL = string.Format(strSQL, strTaskId, strItemType);
            return SqlHelper.ExecuteObjectList(new TMisMonitorSampleInfoVo(), strSQL);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息
        /// 创建时间：2013-1-2
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public DataTable GetSampleInfoSourceByTask(string strTaskId, string strType, int intPageIndex, int intPageSize)
        {
            //string strSQL = "select sample.ID,sample.SUBTASK_ID,sample.SAMPLE_CODE,sample.QC_TYPE,sample.SAMPLE_REMARK,sample.SPECIALREMARK,point.POINT_NAME from T_MIS_MONITOR_SAMPLE_INFO sample  " +
            //                " JOIN (select ID from T_MIS_MONITOR_SUBTASK {0}) subtask ON subtask.ID=sample.SUBTASK_ID" +
            //                " LEFT JOIN T_MIS_MONITOR_TASK_POINT point on sample.POINT_ID=point.ID" +
            //                " where sample.QC_TYPE='0'  order by point.MONITOR_ID,point.NUM,sample.SAMPLE_CODE";

            //string strWhere = "where 1=1";
            //if (!string.IsNullOrEmpty(strType))
            //{
            //    strWhere += " and MONITOR_ID='" + strType + "'";
            //}
            //if (!string.IsNullOrEmpty(strTaskId))
            //{
            //    strWhere += " and TASK_ID ='" + strTaskId + "'";
            //}
            //strSQL = string.Format(strSQL, strWhere);
            //return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
            string strSQL = "select sample.ID,sample.SUBTASK_ID,sample.SAMPLE_CODE,sample.SAMPLE_NAME,sample.QC_TYPE,sample.SAMPLE_REMARK,sample.SPECIALREMARK,point.POINT_NAME from T_MIS_MONITOR_SAMPLE_INFO sample  " +
                            " JOIN (select ID from T_MIS_MONITOR_SUBTASK {0}) subtask ON subtask.ID=sample.SUBTASK_ID" +
                            " LEFT JOIN T_MIS_MONITOR_TASK_POINT point on sample.POINT_ID=point.ID" +
                            " where sample.QC_TYPE='0'  order by point.MONITOR_ID,point.NUM,sample.SAMPLE_CODE";

            string strWhere = "where 1=1";
            if (!string.IsNullOrEmpty(strType))
            {
                strWhere += " and MONITOR_ID='" + strType + "'";
            }
            if (!string.IsNullOrEmpty(strTaskId))
            {
                strWhere += " and TASK_ID ='" + strTaskId + "'";
            }
            strSQL = string.Format(strSQL, strWhere);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息总数
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public int GetSampleInfoCountByTask(string strTaskId, string strType)
        {
            string strSQL = "select count(*) from T_MIS_MONITOR_SAMPLE_INFO sample  " +
                  " JOIN (select ID from T_MIS_MONITOR_SUBTASK {0}) subtask ON subtask.ID=sample.SUBTASK_ID" +
                  " where sample.QC_TYPE='0' ";

            string strWhere = "where 1=1";
            if (!string.IsNullOrEmpty(strType))
            {
                strWhere += " and MONITOR_ID='" + strType + "'";
            }
            if (!string.IsNullOrEmpty(strTaskId))
            {
                strWhere += " and TASK_ID ='" + strTaskId + "'";
            }
            strSQL = string.Format(strSQL, strWhere);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息
        /// 创建时间：2013-1-2
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public DataTable GetSampleInfoSourceByTaskForQY_XC(string strSubTaskID, string strTaskId, string strType, int intPageIndex, int intPageSize)
        {
            string strSQL = @"select DISTINCT(sample.ID) AS ID,sample.POINT_ID,sample.SUBTASK_ID,sample.SAMPLE_CODE,sample.SAMPLE_NAME,sample.QC_TYPE,sample.SAMPLE_REMARK,sample.SPECIALREMARK,point.POINT_NAME,point.MONITOR_ID,point.NUM from 
                            T_MIS_MONITOR_SAMPLE_INFO sample   
                            JOIN (select ID from T_MIS_MONITOR_SUBTASK where 1=1 and TASK_ID ='{0}' and ID='{1}' union all select REMARK1 from T_MIS_MONITOR_SUBTASK where 1=1 and TASK_ID ='{0}' and ID='{1}') subtask ON subtask.ID=sample.SUBTASK_ID 
                            LEFT JOIN T_MIS_MONITOR_TASK_POINT point on sample.POINT_ID=point.ID 
                            inner join dbo.T_MIS_MONITOR_TASK_ITEM taskitems on taskitems.TASK_POINT_ID=point.ID 
                            inner join dbo.T_BASE_ITEM_INFO items on items.ID=taskitems.ITEM_ID
                            where sample.QC_TYPE='0'  and items.IS_SAMPLEDEPT='是' OR items.IS_ANYSCENE_ITEM='1'
                            order by point.MONITOR_ID,point.NUM,sample.SAMPLE_CODE";

            strSQL = string.Format(strSQL, strTaskId, strSubTaskID);
            //string strWhere = "where 1=1";
            //if (!string.IsNullOrEmpty(strType))
            //{
            //    strWhere += " and MONITOR_ID='" + strType + "'";
            //}
            //if (!string.IsNullOrEmpty(strTaskId))
            //{
            //    strWhere += " and TASK_ID ='" + strTaskId + "'";
            //}
            //strSQL = string.Format(strSQL, strWhere);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息总数
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public int GetSampleInfoCountByTaskForQY_XC(string strTaskId, string strType)
        {
            string strSQL = @"select Count (DISTINCT(sample.ID)) 
from T_MIS_MONITOR_SAMPLE_INFO sample   
JOIN (select ID from T_MIS_MONITOR_SUBTASK where 1=1 and TASK_ID ='{0}') subtask ON subtask.ID=sample.SUBTASK_ID 
LEFT JOIN T_MIS_MONITOR_TASK_POINT point on sample.POINT_ID=point.ID 
inner join dbo.T_MIS_MONITOR_TASK_ITEM taskitems on taskitems.TASK_POINT_ID=point.ID 
inner join dbo.T_BASE_ITEM_INFO items on items.ID=taskitems.ITEM_ID
where sample.QC_TYPE='0'  and items.IS_SAMPLEDEPT='是' OR items.IS_ANYSCENE_ITEM='1'";
            strSQL = string.Format(strSQL, strTaskId);
            //string strWhere = "where 1=1";
            //if (!string.IsNullOrEmpty(strType))
            //{
            //    strWhere += " and MONITOR_ID='" + strType + "'";
            //}
            //if (!string.IsNullOrEmpty(strTaskId))
            //{
            //    strWhere += " and TASK_ID ='" + strTaskId + "'";
            //}
            //strSQL = string.Format(strSQL, strWhere);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息
        /// 创建时间：2013-7-6
        /// 创建人：潘德军
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public DataTable GetSampleInfoSourceByTask_Ex(string strTaskId, string strType, int intPageIndex, int intPageSize)
        {
            string strSQL = "select monitor.MONITOR_TYPE_NAME,sample.* from T_MIS_MONITOR_SAMPLE_INFO sample  " +
                            " JOIN (select ID,MONITOR_ID from T_MIS_MONITOR_SUBTASK {0}) subtask ON subtask.ID=sample.SUBTASK_ID" +
                            " JOIN T_BASE_MONITOR_TYPE_INFO monitor ON monitor.ID=subtask.MONITOR_ID" +
                            " where sample.QC_TYPE='0'  order by subtask.MONITOR_ID,sample.SAMPLE_CODE";

            string strWhere = "where 1=1";
            if (!string.IsNullOrEmpty(strType))
            {
                strWhere += " and MONITOR_ID='" + strType + "'";
            }
            if (!string.IsNullOrEmpty(strTaskId))
            {
                strWhere += " and TASK_ID ='" + strTaskId + "'";
            }
            strSQL = string.Format(strSQL, strWhere);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息总数
        /// 创建时间：2013-7-6
        /// 创建人：潘德军
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public int GetSampleInfoCountByTask_Ex(string strTaskId, string strType)
        {
            string strSQL = "select count(*) from T_MIS_MONITOR_SAMPLE_INFO sample  " +
                  " JOIN (select ID from T_MIS_MONITOR_SUBTASK {0}) subtask ON subtask.ID=sample.SUBTASK_ID" +
                  " where sample.QC_TYPE='0' ";

            string strWhere = "where 1=1";
            if (!string.IsNullOrEmpty(strType))
            {
                strWhere += " and MONITOR_ID='" + strType + "'";
            }
            if (!string.IsNullOrEmpty(strTaskId))
            {
                strWhere += " and TASK_ID ='" + strTaskId + "'";
            }
            strSQL = string.Format(strSQL, strWhere);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有样品信息
        /// 创建时间：2013-1-2
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public DataTable GetAllSampleInfoSourceByTask(string strTaskId, string strType)
        {
            //string strSQL = "select sample.ID,sample.SAMPLE_CODE,sample.QC_TYPE,case when sample.POINT_ID is null then sample.sample_name else point.POINT_NAME end as POINT_NAME " +
            //                            " from T_MIS_MONITOR_SAMPLE_INFO sample  " +
            //                            " JOIN (select ID from T_MIS_MONITOR_SUBTASK {0}) subtask ON subtask.ID=sample.SUBTASK_ID" +
            //                            " LEFT JOIN T_MIS_MONITOR_TASK_POINT point on sample.POINT_ID=point.ID  order by point.MONITOR_ID,point.NUM,sample.SAMPLE_CODE";
            //string strWhere = "where 1=1";
            //if (!string.IsNullOrEmpty(strType))
            //{
            //    strWhere += " and MONITOR_ID='" + strType + "'";
            //}
            //if (!string.IsNullOrEmpty(strTaskId))
            //{
            //    strWhere += " and TASK_ID ='" + strTaskId + "'";
            //}
            //strSQL = string.Format(strSQL, strWhere);
            //return SqlHelper.ExecuteDataTable(strSQL);
            string strSQL = "select sample.ID,sample.POINT_ID,sample.SAMPLE_CODE,sample.SAMPLE_NAME,sample.QC_TYPE,case when sample.POINT_ID is null then sample.sample_name else point.POINT_NAME end as POINT_NAME,sample.SAMPLE_ACCEPT_DATEORACC " +
                                        " from T_MIS_MONITOR_SAMPLE_INFO sample  " +
                                        " JOIN (select ID from T_MIS_MONITOR_SUBTASK {0}) subtask ON subtask.ID=sample.SUBTASK_ID" +
                                        " LEFT JOIN T_MIS_MONITOR_TASK_POINT point on sample.POINT_ID=point.ID  order by point.MONITOR_ID,point.NUM,sample.SAMPLE_CODE";
            string strWhere = "where 1=1";
            if (!string.IsNullOrEmpty(strType))
            {
                strWhere += " and MONITOR_ID='" + strType + "'";
            }
            if (!string.IsNullOrEmpty(strTaskId))
            {
                strWhere += " and TASK_ID ='" + strTaskId + "'";
            }
            strSQL = string.Format(strSQL, strWhere);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息总数
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public int GetAllSampleInfoCountByTask(string strTaskId, string strType)
        {
            string strSQL = "select count(*) from T_MIS_MONITOR_SAMPLE_INFO sample  " +
                            " JOIN (select ID from T_MIS_MONITOR_SUBTASK {0}) subtask ON subtask.ID=sample.SUBTASK_ID";
            string strWhere = "where 1=1";
            if (!string.IsNullOrEmpty(strType))
            {
                strWhere += " and MONITOR_ID='" + strType + "'";
            }
            if (!string.IsNullOrEmpty(strTaskId))
            {
                strWhere += " and TASK_ID ='" + strTaskId + "'";
            }
            strSQL = string.Format(strSQL, strWhere);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 功能描述：获取监测任务的某种监测类型样品名称（特定针对秦皇岛报告，点位1、点位2等点位）
        /// 创建时间：2013-1-19
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public DataTable GetSampleInfoSourceByTaskForLicense(string strTaskID, string strType)
        {
            string strSQL = @"select distinct substring(SAMPLE_NAME,1,len (SAMPLE_NAME)-1) as POINT_NAME 
                                                from T_MIS_MONITOR_SAMPLE_INFO where SUBTASK_ID in 
                                                (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' and MONITOR_ID='{1}') ";
            strSQL = string.Format(strSQL, strTaskID, strType);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        #region 采样任务分配单
        /// <summary>
        /// 获取采样任务分配单信息
        /// </summary>
        /// <param name="strContractType">合同类型</param>
        /// <param name="strMonitorType">监测类别</param>
        /// <param name="strSamplingAskDate">采样日期</param>
        /// <param name="strFlowCode">采样环节代码：duty_sampling</param>
        /// <param name="strCurrentUserId">当前登录用户</param>
        /// <param name="strTaskStatus">任务状态，采样为02</param>
        /// <returns></returns>
        public DataTable getSamplingSheetInfo(string strContractType, string strMonitorType, string strSamplingAskDate, string strFlowCode, string strCurrentUserId, string strTaskStatus)
        {
            string strWhere0 = strContractType == "" ? "" : " and CONTRACT_TYPE = '" + strContractType + "'";
            string strWhere1 = strMonitorType == "" ? "" : " and MONITOR_ID in( '" + strMonitorType + "')";
            string strWhere2 = strSamplingAskDate == "" ? "" : " and SAMPLE_ASK_DATE = '" + strSamplingAskDate + "'";
            string strSql = @"select ID,
                                           (select company.COMPANY_NAME
                                              from T_MIS_MONITOR_TASK_COMPANY company
                                             where company.TASK_ID = T_MIS_MONITOR_SUBTASK.TASK_ID
                                               and company.ID in
                                                   (select TESTED_COMPANY_ID
                                                      from T_MIS_MONITOR_TASK TASK
                                                     where TASK.ID = T_MIS_MONITOR_SUBTASK.TASK_ID)) COMPANY_NAME,
                                           SAMPLE_ASK_DATE,
                                           (select DICT.DICT_TEXT
                                              from T_SYS_DICT DICT
                                             where DICT.DICT_TYPE = 'Contract_Type'
                                               and DICT.DICT_CODE in
                                                   (select CONTRACT_TYPE
                                                      from T_MIS_MONITOR_TASK TASK
                                                     where task.id = T_MIS_MONITOR_SUBTASK.TASK_ID)) CONTRACT_TYPE_NAME,
                                           (select MONITOR_TYPE_NAME
                                              from T_BASE_MONITOR_TYPE_INFO
                                             where ID = T_MIS_MONITOR_SUBTASK.MONITOR_ID) MONITOR_TYPE_NAME,
                                           (select REAL_NAME
                                              from T_SYS_USER
                                             where ID = T_MIS_MONITOR_SUBTASK.SAMPLING_MANAGER_ID) SAMPLING_MANAGER_NAME,
                                           SAMPLING_MAN
                                      from T_MIS_MONITOR_SUBTASK
                                     where TASK_STATUS = '{5}' {1}{2}
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_TASK
                                             where T_MIS_MONITOR_TASK.ID = T_MIS_MONITOR_SUBTASK.TASK_ID {0})
                                       and exists
                                     (select *
                                              from T_SYS_DUTY
                                             where T_SYS_DUTY.DICT_CODE = '{3}'
                                               and T_SYS_DUTY.MONITOR_TYPE_ID = T_MIS_MONITOR_SUBTASK.MONITOR_ID
                                               and exists
                                             (select *
                                                      from T_SYS_USER_DUTY
                                                     where (T_SYS_USER_DUTY.USERID = '{4}' or
                                                           T_SYS_USER_DUTY.USERID in
                                                           (select USER_ID
                                                               from T_SYS_USER_PROXY
                                                              where PROXY_USER_ID = '{4}'))
                                                       and T_SYS_USER_DUTY.DUTY_ID = T_SYS_DUTY.ID))";
            strSql = string.Format(strSql, strWhere0, strWhere1, strWhere2, strFlowCode, strCurrentUserId, strTaskStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            return objTable;
        }
        /// <summary>
        /// 根据子任务ID获取任务详细信息
        /// </summary>
        /// <param name="strSubTaskId">子任务Id</param>
        /// <param name="strTaskStatus">任务状态，采样为02</param>
        /// <returns></returns>
        public DataTable getSamplingSheetInfoBySubTaskId(string strSubTaskId, string strTaskStatus)
        {
            string strSql = @"select ID,
                                               (select company.COMPANY_NAME
                                                  from T_MIS_MONITOR_TASK_COMPANY company
                                                 where company.TASK_ID = T_MIS_MONITOR_SUBTASK.TASK_ID
                                                   and company.ID in
                                                       (select TESTED_COMPANY_ID
                                                          from T_MIS_MONITOR_TASK TASK
                                                         where TASK.ID = T_MIS_MONITOR_SUBTASK.TASK_ID)) COMPANY_NAME,
                                               SAMPLE_ASK_DATE,
                                               (select DICT.DICT_TEXT
                                                  from T_SYS_DICT DICT
                                                 where DICT.DICT_TYPE = 'Contract_Type'
                                                   and DICT.DICT_CODE in
                                                       (select CONTRACT_TYPE
                                                          from T_MIS_MONITOR_TASK TASK
                                                         where task.id = T_MIS_MONITOR_SUBTASK.TASK_ID)) CONTRACT_TYPE_NAME,
                                               (select MONITOR_TYPE_NAME
                                                  from T_BASE_MONITOR_TYPE_INFO
                                                 where ID = T_MIS_MONITOR_SUBTASK.MONITOR_ID) MONITOR_TYPE_NAME,
                                               (select REAL_NAME
                                                  from T_SYS_USER
                                                 where ID = T_MIS_MONITOR_SUBTASK.SAMPLING_MANAGER_ID) SAMPLING_MANAGER_NAME,
                                               SAMPLING_MAN
                                          from T_MIS_MONITOR_SUBTASK
                                         where ID in('{0}') and TASK_STATUS = '{1}'";
            strSql = string.Format(strSql, strSubTaskId, strTaskStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            return objTable;
        }
        #endregion

        #region 样品交接记录表
        /// <summary>
        /// 样品交接-样品相关信息
        /// </summary>
        /// <param name="strContractType">合同类型</param>
        /// <param name="strMonitorType">监测类别</param>
        /// <param name="strSamplingDate">采样日期</param>
        /// <param name="strSubTaskStatus">子任务状态 分析任务分配：03</param>
        /// <returns></returns>
        public DataTable getSamplingAllocationSheet(string strTaskID, string strMonitorType, string strSubTaskStatus)
        {
            string strWhere0 = strTaskID == "" ? "" : " and T_MIS_MONITOR_TASK.ID = '" + strTaskID + "'";
            string strWhere1 = strMonitorType == "" ? "" : " and T_MIS_MONITOR_SUBTASK.ID ='" + strMonitorType + "'";
            string strSql = @"select ID,
                                   POINT_ID,
                                   QC_TYPE,
                                   SAMPLE_CODE,
                                   SAMPLE_NAME,
                                   SPECIALREMARK,
                                   SAMPLE_COUNT,
                                   '' as ITEM_NAME,
                                   '完好' as IS_OK,
                                   SAMPLE_COUNT,
                                   (select REAL_NAME
                                      from T_SYS_USER
                                     where ID in
                                           (select SAMPLING_MANAGER_ID
                                              from T_MIS_MONITOR_SUBTASK
                                             where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)) SAMPLING_MANAGER_NAME,
                                   (select REAL_NAME
                                      from T_SYS_USER
                                     where ID in
                                           (select SAMPLE_ACCESS_ID
                                              from T_MIS_MONITOR_SUBTASK
                                             where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)) SAMPLE_ACCESS_NAME,
                                   (select SAMPLE_ACCESS_DATE
                                      from T_MIS_MONITOR_SUBTASK
                                     where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID) SAMPLE_ACCESS_DATE,REMARK1,REMARK2,REMARK3,REMARK4,REMARK5,SAMPLE_STATUS
                              from T_MIS_MONITOR_SAMPLE_INFO
                             where NOSAMPLE = '0'
                               and exists
                             (select *
                                      from T_MIS_MONITOR_SUBTASK
                                     where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{2}'
                                       and T_MIS_MONITOR_SUBTASK.ID =
                                           T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID {1}
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_TASK
                                             where T_MIS_MONITOR_TASK.ID = T_MIS_MONITOR_SUBTASK.TASK_ID {0}))
                               and exists
                             (select *
                                      from T_MIS_MONITOR_RESULT
                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                       and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                     (select *
                                              from T_BASE_ITEM_INFO
                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)))";
            strSql = string.Format(strSql, strWhere0, strWhere1, strSubTaskStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                string strSampleId = row["ID"].ToString();
                string strValue = getSamplingAllocationSheetItemName(strSampleId);
                if (strValue != "")
                    row["ITEM_NAME"] = strValue;
            }
            return objTable;
        }


        /// <param name="ItemCondition_WithOut">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        public DataTable getSamplingAllocationSheet_MAS(string strTaskID, string strSubTaskID, string ItemCondition_WithOut = "1")
        {
            string sqlItemCondition = getItemCondition_WithOut(ItemCondition_WithOut);
            string strWhere0 = strTaskID == "" ? "" : " and T_MIS_MONITOR_TASK.ID = '" + strTaskID + "'";
            string strWhere1 = strSubTaskID == "" ? "" : " and T_MIS_MONITOR_SUBTASK.ID ='" + strSubTaskID + "'";
            string strSql = @"select ID,
                                   POINT_ID,
                                   QC_TYPE,
                                   SAMPLE_CODE,
                                   SAMPLE_NAME,
                                   SPECIALREMARK,
                                   SAMPLE_COUNT,
                                   '' as ITEM_NAME,
                                   '完好' as IS_OK,
                                   SAMPLE_COUNT,
                                   (select REAL_NAME
                                      from T_SYS_USER
                                     where ID in
                                           (select SAMPLING_MANAGER_ID
                                              from T_MIS_MONITOR_SUBTASK
                                             where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)) SAMPLING_MANAGER_NAME,
                                   (select REAL_NAME
                                      from T_SYS_USER
                                     where ID in
                                           (select SAMPLE_ACCESS_ID
                                              from T_MIS_MONITOR_SUBTASK
                                             where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)) SAMPLE_ACCESS_NAME,
                                   (select SAMPLE_ACCESS_DATE
                                      from T_MIS_MONITOR_SUBTASK
                                     where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID) SAMPLE_ACCESS_DATE,REMARK1,REMARK2,REMARK3,REMARK4,REMARK5,SAMPLE_STATUS
                              from T_MIS_MONITOR_SAMPLE_INFO
                             where exists
                             (select *
                                      from T_MIS_MONITOR_SUBTASK
                                     where T_MIS_MONITOR_SUBTASK.ID =
                                           T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID {1}
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_TASK
                                             where T_MIS_MONITOR_TASK.ID = T_MIS_MONITOR_SUBTASK.TASK_ID {0}))
                               and exists
                             (select *
                                      from T_MIS_MONITOR_RESULT
                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                       and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                     (select *
                                              from T_BASE_ITEM_INFO
                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' {2}
                                               --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)))";
            strSql = string.Format(strSql, strWhere0, strWhere1, sqlItemCondition);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                string strSampleId = row["ID"].ToString();
                string strValue = getSamplingAllocationSheetItemName(strSampleId, ItemCondition_WithOut);
                if (strValue != "")
                    row["ITEM_NAME"] = strValue;
            }
            return objTable;
        }
        /// <summary>
        /// 样品交接-样品相关信息
        /// </summary>
        /// <param name="strContractType">合同类型</param>
        /// <param name="strMonitorType">监测类别</param>
        /// <param name="strSamplingDate">采样日期</param>
        /// <param name="strSubTaskStatus">子任务状态 分析任务分配：03</param>
        /// <returns></returns>
        public DataTable getSamplingAllocationSheet_QHD(string strTaskID, string strMonitorType, string strSubTaskStatus)
        {
            string strWhere0 = strTaskID == "" ? "" : " and T_MIS_MONITOR_TASK.ID = '" + strTaskID + "'";
            string strWhere1 = strMonitorType == "" ? "" : " and T_MIS_MONITOR_SUBTASK.ID ='" + strMonitorType + "'";
            string strSql = @"select ID,
                                   QC_TYPE,
                                   SAMPLE_CODE,
                                   SAMPLE_NAME,
                                   SPECIALREMARK,
                                   SAMPLE_COUNT,
                                   '' as ITEM_NAME,
                                   '' as INSTRUMENT_NAME,
                                   '完好' as IS_OK,
                                   (select REAL_NAME
                                      from T_SYS_USER
                                     where ID in
                                           (select SAMPLING_MANAGER_ID
                                              from T_MIS_MONITOR_SUBTASK
                                             where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)) SAMPLING_MANAGER_NAME,
                                   (select REAL_NAME
                                      from T_SYS_USER
                                     where ID in
                                           (select SAMPLE_ACCESS_ID
                                              from T_MIS_MONITOR_SUBTASK
                                             where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)) SAMPLE_ACCESS_NAME,
                                   (select SAMPLE_ACCESS_DATE
                                      from T_MIS_MONITOR_SUBTASK
                                     where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID) SAMPLE_ACCESS_DATE,REMARK1,REMARK2,REMARK3,REMARK4,REMARK5
                              from T_MIS_MONITOR_SAMPLE_INFO
                             where NOSAMPLE = '1'
                               and exists
                             (select *
                                      from T_MIS_MONITOR_SUBTASK
                                     where T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({2})
                                       and T_MIS_MONITOR_SUBTASK.ID =
                                           T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID {1}
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_TASK
                                             where T_MIS_MONITOR_TASK.ID = T_MIS_MONITOR_SUBTASK.TASK_ID {0}))
                               and exists
                             (select *
                                      from T_MIS_MONITOR_RESULT
                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                       and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or T_MIS_MONITOR_RESULT.RESULT_STATUS='01' or exists
                                     (select *
                                              from T_BASE_ITEM_INFO
                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))) order by ISNULL(SAMPLE_COUNT,'9999'),POINT_ID,ID";
            strSql = string.Format(strSql, strWhere0, strWhere1, strSubTaskStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                string strSampleId = row["ID"].ToString();
                string strValue = getSamplingAllocationSheetItemName_QHD(strSampleId);
                if (strValue != "")
                    row["ITEM_NAME"] = strValue;

                strValue = getSamplingInstrumentName(strSampleId);
                row["INSTRUMENT_NAME"] = strValue;
            }
            return objTable;
        }
        /// <summary>
        /// 获取样品交接表信息
        /// </summary>
        /// <param name="strContractType">合同类型</param>
        /// <param name="strMonitorType">监测类别</param>
        /// <param name="strSamplingDate">采样日期</param>
        /// <param name="strSubTaskStatus">子任务状态 分析任务分配：03</param>
        /// <returns></returns>
        public DataTable getSamplingAllocationSheetInfo(string strContractType, string strMonitorType, string strSamplingDate, string strSubTaskStatus)
        {
            string strWhere0 = strContractType == "" ? "" : " and T_MIS_MONITOR_TASK.CONTRACT_TYPE = '" + strContractType + "'";
            string strWhere1 = strMonitorType == "" ? "" : " and T_MIS_MONITOR_SUBTASK.MONITOR_ID in( '" + strMonitorType + "')";
            string strWhere2 = strSamplingDate == "" ? "" : " and T_MIS_MONITOR_SUBTASK.SAMPLE_FINISH_DATE = '" + strSamplingDate + "'";
            string strSql = @"select ID,
                                               SAMPLE_CODE,
                                               SAMPLE_NAME,
                                               SAMPLE_COUNT,
                                               '' as ITEM_NAME,
                                               '完好' as IS_OK,
                                               (select REAL_NAME
                                                  from T_SYS_USER
                                                 where ID in
                                                       (select SAMPLING_MANAGER_ID
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)) SAMPLING_MANAGER_NAME,
                                               (select REAL_NAME
                                                  from T_SYS_USER
                                                 where ID in
                                                       (select SAMPLE_ACCESS_ID
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)) SAMPLE_ACCESS_NAME,
                                               (select SAMPLE_ACCESS_DATE
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID) SAMPLE_ACCESS_DATE
                                          from T_MIS_MONITOR_SAMPLE_INFO
                                         where NOSAMPLE = '0' and PRINTED='0'
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{3}'
                                                 and T_MIS_MONITOR_SUBTASK.ID =
                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID {1} {2}
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_TASK
                                                         where T_MIS_MONITOR_TASK.ID = T_MIS_MONITOR_SUBTASK.TASK_ID {0}))
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT
                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                                   and (   T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                   and (remark_4='1' or exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)))";
            strSql = string.Format(strSql, strWhere0, strWhere1, strWhere2, strSubTaskStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                string strSampleId = row["ID"].ToString();
                string strValue = getSamplingAllocationSheetItemName(strSampleId);
                if (strValue != "")
                    row["ITEM_NAME"] = strValue;
            }
            return objTable;
        }
        /// <summary>
        /// 获取样品交接信息表中的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="ItemCondition_WithOut">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public string getSamplingAllocationSheetItemName(string strSampleId, string ItemCondition_WithOut = "1")
        {
            string sqlItemCondition = getItemCondition_WithOut(ItemCondition_WithOut);
            string strItemValue = "";
            string spit = "";
            string strSql = @"select sum_record.*, T_BASE_ITEM_INFO.ITEM_NAME
                                          from (select ID, ITEM_ID
                                                  from T_MIS_MONITOR_RESULT
                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                   and (   T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                   and (remark_4='1' or exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' {1}
                                                           --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))) sum_record
                                          left join T_BASE_ITEM_INFO
                                            on sum_record.ITEM_ID = T_BASE_ITEM_INFO.ID";
            strSql = string.Format(strSql, strSampleId, sqlItemCondition);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                strItemValue += spit + row["ITEM_NAME"].ToString();
                spit = ",";
            }
            return strItemValue;
        }

        /// <summary>
        /// 获取样品交接信息表中的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <returns></returns>
        public string getSamplingAllocationSheetItemName_QHD(string strSampleId)
        {
            string strItemValue = "";
            string spit = "";
            string strSql = @"select sum_record.*, T_BASE_ITEM_INFO.ITEM_NAME
                                          from (select ID, ITEM_ID
                                                  from T_MIS_MONITOR_RESULT
                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                   and (   T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                   and (remark_4='1' or RESULT_STATUS='01' or exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))) sum_record
                                          left join T_BASE_ITEM_INFO
                                            on sum_record.ITEM_ID = T_BASE_ITEM_INFO.ID";
            strSql = string.Format(strSql, strSampleId);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                strItemValue += spit + row["ITEM_NAME"].ToString();
                spit = ",";
            }
            return strItemValue;
        }
        /// <summary>
        /// 获取样品交接信息表中的采样容器信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <returns></returns>
        public string getSamplingInstrumentName(string strSampleId)
        {
            string strValue = "";
            string strSql = @"select distinct INSTRUMENT_NAME from T_BASE_ITEM_SAMPLING_INSTRUMENT where id in
                            (select SAMPLING_INSTRUMENT from T_MIS_MONITOR_RESULT where SAMPLE_ID='{0}')";
            strSql = string.Format(strSql, strSampleId);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                strValue += row["INSTRUMENT_NAME"].ToString() + ",";
            }
            return strValue.TrimEnd(',');
        }

        /// <summary>
        /// 根据样品信息获取样品交接表信息
        /// </summary>
        /// <param name="strSampleIds">样品ID</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <returns></returns>
        public DataTable getSamplingAllocationSheetInfoBySampleId(string strSampleIds, string strSubTaskStatus, string strNOSAMPLE)
        {
            string strSql = @"select distinct T_MIS_MONITOR_SAMPLE_INFO.ID,
                                           T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE,
                                           T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_NAME,
                                           T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_COUNT,
                                           T_MIS_MONITOR_TASK.TICKET_NUM,
                                           '' as ITEM_NAME,
                                           '完好' as IS_OK,   
                                            (select MONITOR_TYPE_NAME
                                                from T_BASE_MONITOR_TYPE_INFO
                                                where ID in 
                                                        (select MONITOR_ID 
                                                             from T_MIS_MONITOR_SUBTASK 
                                                             where ID =T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID )) MONITOR_TYPE_NAME,
                                           (select REAL_NAME
                                              from T_SYS_USER
                                             where ID in
                                                   (select SAMPLING_MANAGER_ID
                                                      from T_MIS_MONITOR_SUBTASK
                                                     where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)) SAMPLING_MANAGER_NAME,
                                           (select REAL_NAME
                                              from T_SYS_USER
                                             where ID in
                                                   (select SAMPLE_ACCESS_ID
                                                      from T_MIS_MONITOR_SUBTASK
                                                     where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)) SAMPLE_ACCESS_NAME,
                                           (select SAMPLE_ACCESS_DATE
                                              from T_MIS_MONITOR_SUBTASK
                                             where ID = T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID) SAMPLE_ACCESS_DATE
                                      from T_MIS_MONITOR_SAMPLE_INFO 
                                      LEFT JOIN T_MIS_MONITOR_SUBTASK 
                                      ON T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID = T_MIS_MONITOR_SUBTASK.ID 
                                      LEFT JOIN T_MIS_MONITOR_TASK
                                      ON T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                     where T_MIS_MONITOR_SAMPLE_INFO.ID in ('{0}')
                                       and NOSAMPLE = '{2}' 
                                       and exists (select *
                                              from T_MIS_MONITOR_SUBTASK
                                             where T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({1})
                                               and T_MIS_MONITOR_SUBTASK.ID =
                                                   T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_RESULT
                                             where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                               and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                               and exists
                                             (select *
                                                      from T_BASE_ITEM_INFO
                                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                       and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                       and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                        order by T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE asc";
            strSql = string.Format(strSql, strSampleIds, strSubTaskStatus, strNOSAMPLE);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in objTable.Rows)
            {
                string strSampleId = row["ID"].ToString();
                string strValue = getSamplingAllocationSheetItemName(strSampleId);
                if (strValue != "")
                    row["ITEM_NAME"] = strValue;
            }
            return objTable;
        }

        /// <summary>
        /// 更新样品交接表样品打印状态
        /// </summary>
        /// <param name="strSampleIds">样品状态</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strPrintedStatus">打印状态</param>
        /// <returns></returns>
        public bool updateSamplingAllocationSheetInfoStatus(string strSampleIds, string strSubTaskStatus, string strPrintedStatus)
        {
            string strSql = @"update T_MIS_MONITOR_SAMPLE_INFO
                                           set PRINTED = '{2}'
                                         where T_MIS_MONITOR_SAMPLE_INFO.ID in ('{0}')
                                           and NOSAMPLE = '0'
                                           and exists (select *
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{1}'
                                                   and T_MIS_MONITOR_SUBTASK.ID =
                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT
                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                                   and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))";
            strSql = string.Format(strSql, strSampleIds, strSubTaskStatus, strPrintedStatus);
            return SqlHelper.ExecuteNonQuery(strSql) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// 功能描述：获取现场监测项目所属的样品信息（针对双线）
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <param name="strTaskStatus">子任务状态</param>
        /// <param name="intPageIndex">分页码</param>
        /// <param name="intPageSize">总页数</param>
        /// <returns></returns>
        public DataTable getSamplingForSampleItem(string strSubTaksId, string strTaskStatus, int intPageIndex, int intPageSize)
        {
            string strSQL = @"SELECT sample.* FROM
                                            T_MIS_MONITOR_SAMPLE_INFO sample
                                            INNER JOIN
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{1}' AND subtask.ID = '{0}' AND subtask.REMARK1 = sample.SUBTASK_ID
                                            WHERE sample.QC_TYPE='0'";
            strSQL = string.Format(strSQL, strSubTaksId, strTaskStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 功能描述：获取现场监测项目所属的样品信息总数（针对双线）
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <param name="strTaskStatus">子任务状态</param>
        /// <returns></returns>
        public int getSamplingCountForSampleItem(string strSubTaksId, string strTaskStatus)
        {
            string strSQL = @"SELECT COUNT(*) FROM
                                            T_MIS_MONITOR_SAMPLE_INFO sample
                                            INNER JOIN
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{1}' AND subtask.ID = '{0}' AND subtask.REMARK1 = sample.SUBTASK_ID
                                            WHERE sample.QC_TYPE='0'";
            strSQL = string.Format(strSQL, strSubTaksId, strTaskStatus);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        public DataTable getSamplingForSampleItem_MAS(string strSubTaksId, int intPageIndex, int intPageSize)
        {
            string strSQL = @"SELECT sample.* FROM
                                            T_MIS_MONITOR_SAMPLE_INFO sample
                                            INNER JOIN
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.ID = '{0}' AND subtask.REMARK1 = sample.SUBTASK_ID
                                            WHERE sample.QC_TYPE='0'";
            strSQL = string.Format(strSQL, strSubTaksId);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }
        public int getSamplingCountForSampleItem_MAS(string strSubTaksId)
        {
            string strSQL = @"SELECT COUNT(*) FROM
                                            T_MIS_MONITOR_SAMPLE_INFO sample
                                            INNER JOIN
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.ID = '{0}' AND subtask.REMARK1 = sample.SUBTASK_ID
                                            WHERE sample.QC_TYPE='0'";
            strSQL = string.Format(strSQL, strSubTaksId);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 功能描述：获取现场监测项目所属的样品信息（针对单线）
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <param name="strTaskStatus">子任务状态</param>
        /// <param name="intPageIndex">分页码</param>
        /// <param name="intPageSize">总页数</param>
        /// <returns></returns>
        public DataTable getSamplingForSampleItemOne(string strSubTaksId, string strTaskStatus, int intPageIndex, int intPageSize)
        {
            string strSQL = @"SELECT sample.* FROM
                                            T_MIS_MONITOR_SAMPLE_INFO sample
                                            INNER JOIN
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{1}' AND subtask.ID = '{0}' AND subtask.ID = sample.SUBTASK_ID
                                            WHERE sample.QC_TYPE='0'";
            strSQL = string.Format(strSQL, strSubTaksId, strTaskStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 功能描述：获取现场监测项目所属的样品信息总数（针对单线）
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <param name="strTaskStatus">子任务状态</param>
        /// <returns></returns>
        public int getSamplingCountForSampleItemOne(string strSubTaksId, string strTaskStatus)
        {
            string strSQL = @"SELECT COUNT(*) FROM
                                            T_MIS_MONITOR_SAMPLE_INFO sample
                                            INNER JOIN
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.TASK_STATUS='{1}' AND subtask.ID = '{0}' AND subtask.ID = sample.SUBTASK_ID
                                            WHERE sample.QC_TYPE='0'";
            strSQL = string.Format(strSQL, strSubTaksId, strTaskStatus);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 功能描述：获取有现场监测项目的监测点位
        /// 创建时间：2015-04-25
        /// 创建人：黄进军
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <returns></returns>
        public DataTable SelectSampleSitePoint(string strSubTaksId)
        {
            string strSQL = @"SELECT s.*
                                FROM T_MIS_MONITOR_TASK_POINT p inner join T_MIS_MONITOR_SAMPLE_INFO s on p.ID = s.POINT_ID
                                where p.SUBTASK_ID='{0}' and p.IS_DEL='0' and  exists
                                (
                                select * from T_MIS_MONITOR_TASK_ITEM where  T_MIS_MONITOR_TASK_ITEM.TASK_POINT_ID= p.ID and 
                                exists
                                (
                                select * from T_BASE_ITEM_INFO where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是' and T_BASE_ITEM_INFO.ID =T_MIS_MONITOR_TASK_ITEM.ITEM_ID
                                )
                                )";
            strSQL = string.Format(strSQL, strSubTaksId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取有现场监测项目的监测点位总数
        /// 创建时间：2015-04-25
        /// 创建人：黄进军
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <returns></returns>
        public int SelectSampleSitePointCount(string strSubTaksId)
        {
            string strSQL = @"SELECT count(*)
                                FROM T_MIS_MONITOR_TASK_POINT p inner join T_MIS_MONITOR_SAMPLE_INFO s on p.ID = s.POINT_ID
                                where p.SUBTASK_ID='{0}' and p.IS_DEL='0' and  exists
                                (
                                select * from T_MIS_MONITOR_TASK_ITEM where  T_MIS_MONITOR_TASK_ITEM.TASK_POINT_ID= p.ID and 
                                exists
                                (
                                select * from T_BASE_ITEM_INFO where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是' and T_BASE_ITEM_INFO.ID =T_MIS_MONITOR_TASK_ITEM.ITEM_ID
                                )
                                )";
            strSQL = string.Format(strSQL, strSubTaksId);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        public DataTable getSamplingForSampleItemOne_MAS(string strSubTaksId, int intPageIndex, int intPageSize)
        {
            string strSQL = @"SELECT sample.* FROM
                                            T_MIS_MONITOR_SAMPLE_INFO sample
                                            INNER JOIN
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.ID = sample.SUBTASK_ID
                                            WHERE sample.QC_TYPE='0' and subtask.ID='{0}'";
            strSQL = string.Format(strSQL, strSubTaksId);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }
        public int getSamplingCountForSampleItemOne_MAS(string strSubTaksId)
        {
            string strSQL = @"SELECT COUNT(*) FROM
                                            T_MIS_MONITOR_SAMPLE_INFO sample
                                            INNER JOIN
                                            T_MIS_MONITOR_SUBTASK subtask ON subtask.ID = sample.SUBTASK_ID
                                            WHERE sample.QC_TYPE='0' and subtask.ID='{0}'";
            strSQL = string.Format(strSQL, strSubTaksId);
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 获取同一子任务下同一监测类别样品已编码数量
        /// </summary>
        /// <param name="tMisMonitorSampleInfo"></param>
        /// <param name="strMonitorId"></param>
        /// <returns></returns>
        public int GetMonitorTypeCountForSubTask(TMisMonitorSampleInfoVo tMisMonitorSampleInfo, string strMonitorId)
        {
            string strSQL = @"SELECT A.ID,A.SAMPLE_CODE,B.MONITOR_ID FROM dbo.T_MIS_MONITOR_SAMPLE_INFO A
LEFT JOIN dbo.T_MIS_MONITOR_SUBTASK B ON A.SUBTASK_ID=B.ID
WHERE 1=1";
            if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SUBTASK_ID))
            {
                strSQL += String.Format(" AND A.SUBTASK_ID='{0}'", tMisMonitorSampleInfo.SUBTASK_ID);
            }
            if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_CODE))
            {
                strSQL += String.Format(" AND A.SAMPLE_CODE='{0}'", tMisMonitorSampleInfo.SAMPLE_CODE);
            }
            if (String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_CODE))
            {
                strSQL += " AND A.SAMPLE_CODE!=' '";
            }

            if (!String.IsNullOrEmpty(strMonitorId))
            {
                strSQL += String.Format(" AND B.MONITOR_ID='{0}'", strMonitorId);
            }
            if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLECODE_CREATEDATE))
            {
                strSQL += String.Format(" AND CONVERT(DATETIME, CONVERT(VARCHAR(100), A.SAMPLECODE_CREATEDATE,23),111) ='{0}'", tMisMonitorSampleInfo.SAMPLECODE_CREATEDATE);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 创建原因：根据监测任务点位 获取环境质量当前年度当前月份的质控计划列表
        /// 创建人：胡方扬
        /// 创建日期：2013-06-26
        /// </summary>
        /// <param name="strTaskPointId"></param>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <returns></returns>
        public DataTable GetSampleInforForEnvQcSettingTable(string strTaskPointId, string strYear, string strMonth)
        {
            string strSQL = String.Format(@" SELECT A.ID,B.ID AS POINT_QC_ID,B.POINT_ID,B.POINT_NAME,C.ID AS POINTITEM_QC_ID,C.ITEM_ID,C.ITEM_NAME,C.QC_TYPE FROM dbo.T_MIS_MONITOR_TASK_POINT A
                                             LEFT JOIN dbo.T_MIS_POINT_QCSETTING B ON B.POINT_ID=A.POINT_ID AND B.YEAR='{0}' AND B.MONTH='{1}'
                                             LEFT JOIN dbo.T_MIS_POINTITEM_QCSETTING C ON C.POINT_QCSETTING_ID=B.ID WHERE A.ID='{2}'", strYear, strMonth, strTaskPointId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorSampleInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorSampleInfo)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorSampleInfo.ID.ToString()));
                }
                //监测子任务ID
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SUBTASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUBTASK_ID = '{0}'", tMisMonitorSampleInfo.SUBTASK_ID.ToString()));
                }
                //点位
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tMisMonitorSampleInfo.POINT_ID.ToString()));
                }
                //样品号
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_CODE = '{0}'", tMisMonitorSampleInfo.SAMPLE_CODE.ToString()));
                }
                //样品类型
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_TYPE = '{0}'", tMisMonitorSampleInfo.SAMPLE_TYPE.ToString()));
                }
                //样品名称
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_NAME = '{0}'", tMisMonitorSampleInfo.SAMPLE_NAME.ToString()));
                }
                //样品数量
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_COUNT = '{0}'", tMisMonitorSampleInfo.SAMPLE_COUNT.ToString()));
                }
                //状态
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STATUS = '{0}'", tMisMonitorSampleInfo.STATUS.ToString()));
                }
                //未采样
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.NOSAMPLE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NOSAMPLE = '{0}'", tMisMonitorSampleInfo.NOSAMPLE.ToString()));
                }
                //未采样说明
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.NOSAMPLEREMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NOSAMPLEREMARK = '{0}'", tMisMonitorSampleInfo.NOSAMPLEREMARK.ToString()));
                }
                //质控类型（现场空白、现场加标、现场平行、实验室密码平行，实验室空白、实验室加标、实验室明码平行、标准样）
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.QC_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_TYPE = '{0}'", tMisMonitorSampleInfo.QC_TYPE.ToString()));
                }
                //质控原始样ID
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.QC_SOURCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_SOURCE_ID = '{0}'", tMisMonitorSampleInfo.QC_SOURCE_ID.ToString()));
                }
                //特殊样说嘛
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SPECIALREMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SPECIALREMARK = '{0}'", tMisMonitorSampleInfo.SPECIALREMARK.ToString()));
                }
                //合并子样数量
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SUBSAMPLE_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUBSAMPLE_NUM = '{0}'", tMisMonitorSampleInfo.SUBSAMPLE_NUM.ToString()));
                }
                //样品编码生成日期
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLECODE_CREATEDATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLECODE_CREATEDATE = '{0}'", tMisMonitorSampleInfo.SAMPLECODE_CREATEDATE.ToString()));
                }
                // 样品接收时间/位置
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_ACCEPT_DATEORACC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ACCEPT_DATEORACC = '{0}'", tMisMonitorSampleInfo.SAMPLE_ACCEPT_DATEORACC.ToString()));
                }
                //样品原编号/名称
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SRC_CODEORNAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SRC_CODEORNAME = '{0}'", tMisMonitorSampleInfo.SRC_CODEORNAME.ToString()));
                }
                // 样品原始状态
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_STATUS = '{0}'", tMisMonitorSampleInfo.SAMPLE_STATUS.ToString()));
                }
                // 样品前处理说明
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_REMARK = '{0}'", tMisMonitorSampleInfo.SAMPLE_REMARK.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorSampleInfo.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorSampleInfo.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorSampleInfo.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorSampleInfo.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorSampleInfo.REMARK5.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.D_SOURCE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND D_SOURCE = '{0}'", tMisMonitorSampleInfo.D_SOURCE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.N_SOURCE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND N_SOURCE = '{0}'", tMisMonitorSampleInfo.N_SOURCE.ToString()));
                }
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.ENV_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENV_MONTH = '{0}'", tMisMonitorSampleInfo.ENV_MONTH.ToString()));
                }
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.ENV_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENV_DAY = '{0}'", tMisMonitorSampleInfo.ENV_DAY.ToString()));
                }
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.ENV_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENV_HOUR = '{0}'", tMisMonitorSampleInfo.ENV_HOUR.ToString()));
                }
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.ENV_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENV_MINUTE = '{0}'", tMisMonitorSampleInfo.ENV_MINUTE.ToString()));
                }
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.ENV_MONTH_END.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENV_MONTH_END = '{0}'", tMisMonitorSampleInfo.ENV_MONTH_END.ToString()));
                }
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.ENV_DAY_END.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENV_DAY_END = '{0}'", tMisMonitorSampleInfo.ENV_DAY_END.ToString()));
                }
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.ENV_HOUR_END.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENV_HOUR_END = '{0}'", tMisMonitorSampleInfo.ENV_HOUR_END.ToString()));
                }
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.ENV_MINUTE_END.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENV_MINUTE_END = '{0}'", tMisMonitorSampleInfo.ENV_MINUTE_END.ToString()));
                }
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_RAIN_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_RAIN_TYPE = '{0}'", tMisMonitorSampleInfo.SAMPLE_RAIN_TYPE.ToString()));
                }
                //CCFLOW_ID1
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.CCFLOW_ID1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID1 = '{0}'", tMisMonitorSampleInfo.CCFLOW_ID1.ToString()));
                }
                //CCFLOW_ID2
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.CCFLOW_ID2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID2 = '{0}'", tMisMonitorSampleInfo.CCFLOW_ID2.ToString()));
                }
                //CCFLOW_ID3
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.CCFLOW_ID3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CCFLOW_ID3 = '{0}'", tMisMonitorSampleInfo.CCFLOW_ID3.ToString()));
                }
                //样品条码
                if (!String.IsNullOrEmpty(tMisMonitorSampleInfo.SAMPLE_BARCODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_BARCODE = '{0}'", tMisMonitorSampleInfo.SAMPLE_BARCODE.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 获取计划任务的所有样品信息 Create By: weilin 2014-03-18
        /// </summary>
        /// <param name="strPlanID"></param>
        /// <returns></returns>
        public DataTable GetSampleInfoByPlanID(string strPlanID)
        {
            string strSQL = String.Format(@"select c.ID,a.PLAN_ID,c.SAMPLE_CODE,c.SAMPLE_NAME,b.MONITOR_ID,d.MONITOR_TYPE_NAME
                            from T_MIS_MONITOR_TASK a left join T_MIS_MONITOR_SUBTASK b on(a.ID=b.TASK_ID)
                            left join T_MIS_MONITOR_SAMPLE_INFO c on(b.ID=c.SUBTASK_ID)
                            left join T_BASE_MONITOR_TYPE_INFO d on(b.MONITOR_ID=d.ID)
                            where a.PLAN_ID='{0}'", strPlanID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 获取样品的监测项目信息 Create By: weilin 2014-03-18
        /// </summary>
        /// <param name="strSampleID"></param>
        /// <returns></returns>
        public DataTable GetItemInfoBySampleID(string strSampleID)
        {
            string strSQL = String.Format(@"select a.ID,a.ITEM_ID,b.ITEM_NAME
                            from T_MIS_MONITOR_RESULT a left join T_BASE_ITEM_INFO b on(a.ITEM_ID=b.ID)
                            where a.SAMPLE_ID='{0}'", strSampleID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        public bool UpdateSampleCell(string ID, string strCellName, string strCellValue)
        {
            string strSQL = String.Format(@"UPDATE T_MIS_MONITOR_SAMPLE_INFO SET {0}='{1}' WHERE ID='{2}'", strCellName, strCellValue, ID);
            return SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
        }

        public bool UpdateSetWhere(string strTableName, string strSet, string strWhere)
        {
            string strSQL = "UPDATE {0} SET {1} WHERE {2}";
            strSQL = string.Format(strSQL, strTableName, strSet, strWhere);
            return SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
        }

        public string GetPlanID(string strSampleID)
        {

            string strSQL = @"select D.PLAN_ID from T_MIS_MONITOR_SAMPLE_INFO as B
                        left join T_MIS_MONITOR_SUBTASK as C on (B.SUBTASK_ID = C.ID) 
                        left join T_MIS_MONITOR_TASK as D on (C.TASK_ID = D.ID) 
                        where B.ID ='{0}'";

            strSQL = String.Format(strSQL, strSampleID);

            DataTable dt = SqlHelper.ExecuteDataTable(strSQL);

            if (dt.Rows.Count != 1)
            {
                return "error";
            }
            else
            {
                return dt.Rows[0]["PLAN_ID"].ToString().Trim();
            }
        }

        /// <summary>
        /// 拼接监测项目查询条件_包含
        /// </summary>
        /// <param name="ItemCondition">监测项目条件(逗号隔开)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public string getItemCondition_WithIn(string ItemCondition)
        {
            string sqlItemCondition = "";
            foreach (string value in ItemCondition.Split(','))
            {
                string curCondition = "";
                switch (value)
                {
                    case "1": curCondition = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'"; break;
                    case "2": curCondition = "T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM='1'"; break;
                    default: break;
                }
                if (!string.IsNullOrEmpty(curCondition))
                    sqlItemCondition += (string.IsNullOrEmpty(sqlItemCondition) ? "" : " or ") + curCondition;
            }
            if (!string.IsNullOrEmpty(sqlItemCondition))
                sqlItemCondition = " and (" + sqlItemCondition + ") ";
            return sqlItemCondition;
        }
        /// <summary>
        /// 拼接监测项目查询条件_不包含
        /// </summary>
        /// <param name="ItemCondition">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public string getItemCondition_WithOut(string ItemCondition)
        {
            string sqlItemCondition = "";
            foreach (string value in ItemCondition.Split(','))
            {
                string curCondition = "";
                switch (value)
                {
                    case "1": curCondition = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT <> '是'"; break;
                    case "2": curCondition = "T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM <> '1'"; break;
                    default: break;
                }
                if (!string.IsNullOrEmpty(curCondition))
                    sqlItemCondition += " and " + curCondition;
            }
            return sqlItemCondition;
        }
    }
}
