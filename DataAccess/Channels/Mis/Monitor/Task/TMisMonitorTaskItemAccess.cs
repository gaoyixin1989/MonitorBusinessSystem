using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务点位项目明细表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_TASK_ITEM " + this.BuildWhereStatement(tMisMonitorTaskItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorTaskItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskItemVo Details(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_ITEM " + this.BuildWhereStatement(tMisMonitorTaskItem));
            return SqlHelper.ExecuteObject(new TMisMonitorTaskItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskItemVo> SelectByObject(TMisMonitorTaskItemVo tMisMonitorTaskItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_TASK_ITEM " + this.BuildWhereStatement(tMisMonitorTaskItem));
            return SqlHelper.ExecuteObjectList(tMisMonitorTaskItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskItemVo tMisMonitorTaskItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_TASK_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorTaskItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTaskItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK_ITEM " + this.BuildWhereStatement(tMisMonitorTaskItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskItemVo SelectByObject(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            string strSQL = "select * from T_MIS_MONITOR_TASK_ITEM " + this.BuildWhereStatement(tMisMonitorTaskItem);
            return SqlHelper.ExecuteObject(new TMisMonitorTaskItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorTaskItem, TMisMonitorTaskItemVo.T_MIS_MONITOR_TASK_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTaskItem, TMisMonitorTaskItemVo.T_MIS_MONITOR_TASK_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorTaskItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorTaskItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskItemVo tMisMonitorTaskItem_UpdateSet, TMisMonitorTaskItemVo tMisMonitorTaskItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorTaskItem_UpdateSet, TMisMonitorTaskItemVo.T_MIS_MONITOR_TASK_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorTaskItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_TASK_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            string strSQL = "delete from T_MIS_MONITOR_TASK_ITEM ";
            strSQL += this.BuildWhereStatement(tMisMonitorTaskItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 功能描述：获得查询结果总行数，用于分页（报告编制）
        /// 创建时间：2012-12-6
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountForDetail(string strPointID)
        {
            string strSQL = "select count(*) from T_MIS_MONITOR_TASK_ITEM where TASK_POINT_ID='{0}'";
            strSQL = string.Format(strSQL, strPointID);

            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 功能描述：获取对象DataTable（报告编制）
        /// 创建时间：2012-12-6
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strPointID">任务点位ID</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableForDetail(string strPointID, int iIndex, int iCount)
        {
            string strSQL = " select sampleinfo.*,(case when item.ST_LOWER is not null then item.ST_LOWER+" +
                                        " (case when item.ST_UPPER is not null then '~'+item.ST_UPPER else '' end) else item.ST_UPPER end) as STANDARD_VALUE " +
                                        " from " +
                                        " (select * from T_MIS_MONITOR_TASK_ITEM where TASK_POINT_ID='{0}') item" +//监测任务点位项目明细
                                        " join (select distinct sample.POINT_ID,info.ITEM_NAME,result.ITEM_ID,result.ITEM_RESULT,result.QC,u.REAL_NAME as HEAD_USER,(analysis.ANALYSIS_NAME+method.METHOD_CODE) as METHOD_NAME,apparatus.NAME as APPARATUS_NAME " +
                                        " from T_MIS_MONITOR_SAMPLE_INFO sample" +//关联样品表
                                        " left join T_MIS_MONITOR_RESULT result on result.QC_TYPE='0' and result.SAMPLE_ID=sample.ID" +// 关联样品结果表
                                        " left join T_MIS_MONITOR_RESULT_APP app on result.ID = app.RESULT_ID" +//关联分析执行表
                                        " left join T_SYS_USER u on u.IS_DEL='0' and u.IS_USE='1' and u.ID=app.HEAD_USERID" +//关联分析负责人
                                        " left join T_BASE_METHOD_ANALYSIS analysis on analysis.IS_DEL='0' and result.ANALYSIS_METHOD_ID=analysis.ID" +//关联分析方法表
                                        " left join T_BASE_METHOD_INFO method on method.IS_DEL='0' and result.STANDARD_ID=method.ID" +//关联方法依据表
                                        " left join T_BASE_ITEM_ANALYSIS ia on ia.IS_DEL='0' and ia.ITEM_ID=result.ITEM_ID and ia.ANALYSIS_METHOD_ID=analysis.ID" +//关联监测项目分析方法管理表
                                        " left join T_BASE_APPARATUS_INFO apparatus on apparatus.IS_DEL='0' and apparatus.ID = ia.INSTRUMENT_ID" +//关联仪器信息表
                                        " left join T_BASE_ITEM_INFO info on info.IS_DEL='0' and info.ID=result.ITEM_ID" +//关联项目表
                                        " where sample.POINT_ID='{0}') sampleinfo on item.TASK_POINT_ID=sampleinfo.POINT_ID and item.ITEM_ID=sampleinfo.ITEM_ID" +
                                        " order by sampleinfo.ITEM_ID";
            strSQL = string.Format(strSQL, strPointID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 功能描述：获取项目详细信息
        /// 创建时间：2012-12-12
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strContractID">委托书ID</param>
        /// <returns>数据集</returns>
        public DataTable SelectByTableByContractID(string strContractID, string mItemTypeID)
        {
            string strSQL = @"select task_item.ITEM_ID,info.ITEM_NAME from 
                                            (select distinct ITEM_ID from T_MIS_MONITOR_TASK_ITEM  
                                                where TASK_POINT_ID in (select ID from T_MIS_MONITOR_TASK_POINT where IS_DEL='0' and MONITOR_ID='{1}' and TASK_ID in 
                                            (select ID from T_MIS_MONITOR_TASK where CONTRACT_ID='{0}')))task_item
                                            left join T_BASE_ITEM_INFO info on info.IS_DEL='0' and task_item.ITEM_ID=info.ID";
            strSQL = string.Format(strSQL, strContractID, mItemTypeID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：取得样品对应的样品ID，样品号，项目名称，下限，上限，标准名，标准号
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strContractID">监测任务ID</param>
        /// <param name="strItemTypeID">监测类别ID</param>
        /// <returns></returns>
        public DataTable SelectStandard_byTask(string strTaskID, string strItemTypeID)
        {
            string strSQL = @"SELECT si.ID, si.SAMPLE_CODE, i.ITEM_NAME, oid.ST_UPPER, oid.ST_LOWER, s.STANDARD_NAME, s.STANDARD_CODE
                                             FROM T_MIS_MONITOR_SAMPLE_INFO AS si
                                             LEFT OUTER JOIN  T_MIS_MONITOR_TASK_ITEM AS oid ON oid.TASK_POINT_ID = si.POINT_ID 
                                             LEFT OUTER JOIN T_BASE_ITEM_INFO AS i ON i.ID = oid.ITEM_ID
                                             LEFT OUTER JOIN T_BASE_EVALUATION_CON_INFO AS t ON t.ID = oid.CONDITION_ID 
                                             LEFT OUTER JOIN T_BASE_EVALUATION_INFO AS s ON s.ID = t.STANDARD_ID
                                             WHERE  (si.SUBTASK_ID in 
                                            (select ID from T_MIS_MONITOR_SUBTASK where TASK_ID='{0}' and MONITOR_ID='{1}')) and s.STANDARD_CODE is not null";

            strSQL = string.Format(strSQL, strTaskID, strItemTypeID);

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, 0, 0));
        }

        #region 手机版接口，请勿修改
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectResult_ByTable_forMobile(string strBasePointIdS,string strItemIDs,string strBeginDate,string strEndDate)
        {
            strBasePointIdS = strBasePointIdS.Replace(",", "','");
            strBasePointIdS = "'" + strBasePointIdS + "'";
            strItemIDs = strItemIDs.Replace(",", "','");
            strItemIDs = "'" + strItemIDs + "'";

            string strSQL = @"SELECT     r.ID, p.POINT_NAME, i.ITEM_NAME, r.ITEM_RESULT, isnull(d.DICT_TEXT,'') as unit,st.SAMPLE_FINISH_DATE 
                  FROM         T_MIS_MONITOR_RESULT AS r  
                  JOIN  T_MIS_MONITOR_SAMPLE_INFO s ON s.ID = r.SAMPLE_ID  
				  left join T_MIS_MONITOR_TASK_POINT p on p.ID=s.POINT_ID
                  JOIN  T_BASE_ITEM_INFO i ON i.ID = r.ITEM_ID
				  left join T_BASE_ITEM_ANALYSIS ia on ia.ITEM_ID=r.Item_ID and ia.ANALYSIS_METHOD_ID=r.ANALYSIS_METHOD_ID
				  left join T_SYS_DICT d on d.DICT_TYPE='' and d.DICT_CODE=ia.UNIT 
				  join T_MIS_MONITOR_SUBTASK st on st.ID=s.SUBTASK_ID 
				  join T_MIS_MONITOR_TASK t on t.ID=st.TASK_ID 
                    WHERE     r.QC_TYPE = '0' --and (t.TASK_STATUS='09' or t.TASK_STATUS='11') 
                         and  r.Item_ID in ({1})  
                        and s.POINT_ID in (SELECT ID FROM T_MIS_MONITOR_TASK_POINT WHERE POINT_ID IN ({0}))
                        and st.SAMPLE_FINISH_DATE>='{2}' and st.SAMPLE_FINISH_DATE<'{3}'
                  order by p.POINT_NAME,i.ITEM_NAME,st.SAMPLE_FINISH_DATE ";
            strSQL = String.Format(strSQL, strBasePointIdS, strItemIDs, strBeginDate, strEndDate);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_forMobile(string strBasePointIdS, int iIndex, int iCount)
        {
            strBasePointIdS = strBasePointIdS.Replace(",", "','");
            strBasePointIdS = "'" + strBasePointIdS + "'";
            string strSQL = @"SELECT distinct    tpi.ITEM_ID ID,i.ITEM_Name 
                     FROM T_MIS_MONITOR_TASK_ITEM tpi 
                     join T_BASE_ITEM_INFO i on i.ID=tpi.ITEM_ID 
                     WHERE     TASK_POINT_ID in (SELECT ID  FROM T_MIS_MONITOR_TASK_POINT WHERE     POINT_ID IN ({0})) 
                     and tpi.is_DEL='0'  ";
            strSQL = String.Format(strSQL, strBasePointIdS);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_forMobile(string strBasePointIdS)
        {
            strBasePointIdS = strBasePointIdS.Replace(",", "','");
            strBasePointIdS = "'" + strBasePointIdS + "'";
            string strSQL = @"SELECT count(1)  
                     FROM T_MIS_MONITOR_TASK_ITEM tpi 
                     join T_BASE_ITEM_INFO i on i.ID=tpi.ITEM_ID 
                     WHERE     TASK_POINT_ID in (SELECT ID  FROM T_MIS_MONITOR_TASK_POINT WHERE     POINT_ID IN ({0})) 
                     and tpi.is_DEL='0'  ";
            strSQL = String.Format(strSQL, strBasePointIdS);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectSQL_ByTable_forMobile(string strSQL, int iIndex, int iCount)
        {
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectSQL_ResultCount_forMobile(string strSQL)
        {
            
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
        #endregion
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorTaskItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorTaskItem)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorTaskItem.ID.ToString()));
                }
                //合同监测点ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.TASK_POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_POINT_ID = '{0}'", tMisMonitorTaskItem.TASK_POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tMisMonitorTaskItem.ITEM_ID.ToString()));
                }
                //已选条件项ID
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tMisMonitorTaskItem.CONDITION_ID.ToString()));
                }
                //条件项类型（1，国标；2，行标；3，地标）
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.CONDITION_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_TYPE = '{0}'", tMisMonitorTaskItem.CONDITION_TYPE.ToString()));
                }
                //国标上限
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.ST_UPPER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ST_UPPER = '{0}'", tMisMonitorTaskItem.ST_UPPER.ToString()));
                }
                //国标下限
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.ST_LOWER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ST_LOWER = '{0}'", tMisMonitorTaskItem.ST_LOWER.ToString()));
                }
                //是否删除
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tMisMonitorTaskItem.IS_DEL.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorTaskItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorTaskItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorTaskItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorTaskItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorTaskItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorTaskItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
