using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using System.Linq;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.ValueObject;
using i3.ValueObject.Channels.Mis.Monitor.Sample;
using System.Text.RegularExpressions;

namespace i3.DataAccess.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：分析结果表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public partial class TMisMonitorResultAccess : SqlHelper
    {
        #region 分析环节公共方法

        /// <summary>
        /// 根据环节代码、监测类型、监测项目查找有权限的用户信息 by 熊卫华 2012.11.29
        /// </summary>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strMonitorType">监测类型代码，如废水、废气等</param>
        /// <param name="strItemId">监测项目代码</param>
        /// <returns></returns>
        public DataTable getUsersInfo(string strFlowCode, string strMonitorType, string strItemId)
        {

            string strSql = @"select REMARK1 from T_BASE_MONITOR_TYPE_INFO where ID='{0}'";
            strSql = string.Format(strSql, strMonitorType);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            if (objTable.Rows.Count > 0 && strFlowCode != "duty_sampling")
            {
                if (objTable.Rows[0]["REMARK1"] != null && objTable.Rows[0]["REMARK1"].ToString() != "")
                    strMonitorType = objTable.Rows[0]["REMARK1"].ToString();
            }

            string strWhere = "1=1";
            if (strFlowCode != "")
                strWhere += " and T_SYS_DUTY.DICT_CODE='" + strFlowCode + "'";
            if (strMonitorType != "")
                strWhere += " and T_SYS_DUTY.MONITOR_TYPE_ID='" + strMonitorType + "'";
            if (strItemId != "")
                strWhere += " and T_SYS_DUTY.MONITOR_ITEM_ID='" + strItemId + "'";

            strSql = @"select duty.*, userinfo.USER_NAME, userinfo.REAL_NAME
                                  from (select distinct USERID, IF_DEFAULT, IF_DEFAULT_EX
                                          from T_SYS_USER_DUTY
                                         where exists (select *
                                                  from T_SYS_DUTY
                                                 where 1 = 1
                                                   and {0}
                                                   and T_SYS_USER_DUTY.DUTY_ID = T_SYS_DUTY.ID)) duty,
                                       T_SYS_USER userinfo
                                 where userinfo.IS_DEL = '0'
                                   and duty.USERID = userinfo.ID";
            strSql = string.Format(strSql, strWhere);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        #endregion

        #region 外控管理方法
        /// <summary>
        /// 获取现场加标质控家标量数据方法
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strQcType">质控类型</param>
        /// <param name="strItemId">监测项目ID</param>
        /// <returns></returns>
        public DataTable getQcAddValue(string strSampleId, string strQcType, string strItemId)
        {
            string strSql = @"select QC_ADD,REMARK1,REMARK2,REMARK3
                                          from T_MIS_MONITOR_QC_ADD
                                         where exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT RESULT_SUM
                                                 where RESULT_SUM.SOURCE_ID = T_MIS_MONITOR_QC_ADD.RESULT_ID_SRC
                                                   and RESULT_SUM.ID = T_MIS_MONITOR_QC_ADD.RESULT_ID_ADD
                                                   and RESULT_SUM.SOURCE_ID in
                                                       (select ID
                                                          from T_MIS_MONITOR_RESULT RESULT
                                                         where RESULT.SAMPLE_ID = '{0}'
                                                           and RESULT.QC_TYPE = '0')
                                                   and RESULT_SUM.QC_TYPE = '{1}'
                                                   and RESULT_SUM.ITEM_ID = '{2}')";
            strSql = string.Format(strSql, strSampleId, strQcType, strItemId);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 删除样品质控信息以及样品信息
        /// </summary>
        /// <param name="strSampleId">样品编号</param>
        /// <returns></returns>
        public bool deleteSampleInfo(string strSampleId)
        {
            ArrayList arrVo = new ArrayList();
            deleteQcInfo(strSampleId, "");
            //删除样品信息
            string strSql = @"delete from T_MIS_MONITOR_SAMPLE_INFO where ID='{0}'";
            strSql = string.Format(strSql, strSampleId);
            arrVo.Add(strSql);
            strSql = @"delete from T_MIS_MONITOR_RESULT
                         where not exists
                         (select *
                                  from T_MIS_MONITOR_SAMPLE_INFO
                                 where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID)";
            arrVo.Add(strSql);
            return ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 删除质控信息，如果质控类型为空则删除该样品下的所有质控信息
        /// </summary>
        /// <param name="strSampleId">样品编号</param>
        /// <param name="strQcType">质控类型</param>
        /// <returns></returns>
        public bool deleteQcInfo(string strSampleId, string strQcType)
        {
            ArrayList arrVo = new ArrayList();
            string strSql = "";
            //现场空白
            if (strQcType == "1")
            {
                //删除现场空白结果表【T_MIS_MONITOR_QC_EMPTY_OUT】表信息
                strSql = @"delete from T_MIS_MONITOR_QC_EMPTY_OUT
                                     where QC_TYPE = '{1}'
                                       and RESULT_ID_SRC in (select ID
                                                               from T_MIS_MONITOR_RESULT RESULT
                                                              where RESULT.SAMPLE_ID = '{0}'
                                                                and RESULT.QC_TYPE = '0')";
                strSql = string.Format(strSql, strSampleId, strQcType);
                arrVo.Add(strSql);
            }
            //现场加标
            if (strQcType == "2")
            {
                //删除加标样结果表【T_MIS_MONITOR_QC_ADD】表信息
                strSql = @"delete from T_MIS_MONITOR_QC_ADD
                                 where QC_TYPE = '{1}'
                                   and RESULT_ID_SRC in (select ID
                                                           from T_MIS_MONITOR_RESULT RESULT
                                                          where RESULT.SAMPLE_ID = '{0}'
                                                            and RESULT.QC_TYPE = '0')";
                strSql = string.Format(strSql, strSampleId, strQcType);
                arrVo.Add(strSql);
            }
            //现场平行、实验室密码平行、实验室明码平行
            if (strQcType == "3" || strQcType == "4" || strQcType == "7")
            {
                //删除平行结果【T_MIS_MONITOR_QC_TWIN】表信息
                strSql = @"delete from T_MIS_MONITOR_QC_TWIN
                                 where QC_TYPE = '{1}'
                                   and RESULT_ID_SRC in (select ID
                                                           from T_MIS_MONITOR_RESULT RESULT
                                                          where RESULT.SAMPLE_ID = '{0}'
                                                            and RESULT.QC_TYPE = '0')";
                strSql = string.Format(strSql, strSampleId, strQcType);
                arrVo.Add(strSql);
            }
            //质控平行【秦皇岛】
            if (strQcType == "9")
            {
                //删除质控平行结果【T_MIS_MONITOR_QC_TWIN_QHD】表信息
                strSql = @"delete from T_MIS_MONITOR_QC_TWIN_QHD
                                 where QC_TYPE = '{1}'
                                   and RESULT_ID_SRC in (select ID
                                                           from T_MIS_MONITOR_RESULT RESULT
                                                          where RESULT.SAMPLE_ID = '{0}'
                                                            and RESULT.QC_TYPE = '{1}')";
                strSql = string.Format(strSql, strSampleId, strQcType);
                arrVo.Add(strSql);
            }
            //空白加标【秦皇岛】
            if (strQcType == "10")
            {
                //删除空白加标结果【T_MIS_MONITOR_QC_ADD_QHD】表信息
                strSql = @"delete from T_MIS_MONITOR_QC_ADD_QHD
                             where QC_TYPE = '{1}'
                               and RESULT_ID_ADD in (select ID
                                                       from T_MIS_MONITOR_RESULT RESULT
                                                      where RESULT.SAMPLE_ID = '{0}'
                                                        and RESULT.QC_TYPE = '{1}')";
                strSql = string.Format(strSql, strSampleId, strQcType);
                arrVo.Add(strSql);
            }

            //如果是要删除样品下的所有质控信息
            if (strQcType == "")
            {
                //删除现场空白结果表【T_MIS_MONITOR_QC_EMPTY_OUT】表信息
                strSql = @"delete from T_MIS_MONITOR_QC_EMPTY_OUT
                                 where RESULT_ID_SRC in (select ID
                                                           from T_MIS_MONITOR_RESULT RESULT
                                                          where RESULT.SAMPLE_ID = '{0}'
                                                            and RESULT.QC_TYPE = '0')";
                strSql = string.Format(strSql, strSampleId);
                arrVo.Add(strSql);

                //删除加标样结果表【T_MIS_MONITOR_QC_ADD】表信息
                strSql = @"delete from T_MIS_MONITOR_QC_ADD
                                 where RESULT_ID_SRC in (select ID
                                                           from T_MIS_MONITOR_RESULT RESULT
                                                          where RESULT.SAMPLE_ID = '{0}'
                                                            and RESULT.QC_TYPE = '0')";
                strSql = string.Format(strSql, strSampleId);
                arrVo.Add(strSql);

                //删除平行结果【T_MIS_MONITOR_QC_TWIN】表信息
                strSql = @"delete from T_MIS_MONITOR_QC_TWIN
                                 where RESULT_ID_SRC in (select ID
                                                           from T_MIS_MONITOR_RESULT RESULT
                                                          where RESULT.SAMPLE_ID = '{0}'
                                                            and RESULT.QC_TYPE = '0')";
                strSql = string.Format(strSql, strSampleId);
                arrVo.Add(strSql);

                //删除质控平行结果【T_MIS_MONITOR_QC_TWIN_QHD】表信息
                strSql = @"delete from T_MIS_MONITOR_QC_TWIN_QHD
                                 where RESULT_ID_SRC in (select ID
                                                           from T_MIS_MONITOR_RESULT RESULT
                                                          where RESULT.SAMPLE_ID = '{0}'
                                                            and RESULT.QC_TYPE = '9')";
                strSql = string.Format(strSql, strSampleId);
                arrVo.Add(strSql);

                //删除空白加标结果【T_MIS_MONITOR_QC_ADD_QHD】表信息
                strSql = @"delete from T_MIS_MONITOR_QC_ADD_QHD
                             where RESULT_ID_ADD in (select ID
                                                       from T_MIS_MONITOR_RESULT RESULT
                                                      where RESULT.SAMPLE_ID = '{0}'
                                                        and RESULT.QC_TYPE = '10')";
                strSql = string.Format(strSql, strSampleId);
                arrVo.Add(strSql);
            }

            //删除结果分析执行表【T_MIS_MONITOR_RESULT_APP】的信息
            strSql = @" delete from T_MIS_MONITOR_RESULT_APP
                             where RESULT_ID in (select ID
                                                   from T_MIS_MONITOR_RESULT RESULT2
                                                  where RESULT2.SOURCE_ID in
                                                        (select ID
                                                           from T_MIS_MONITOR_RESULT RESULT1
                                                          where RESULT1.SAMPLE_ID = '{0}'
                                                            and RESULT1.QC_TYPE = '0')
                                                    and RESULT2.QC_TYPE = '{1}')";
            strSql = string.Format(strSql, strSampleId, strQcType);
            arrVo.Add(strSql);
            //删除分析结果表【T_MIS_MONITOR_RESULT】的信息
            strSql = @" delete from T_MIS_MONITOR_RESULT
                         where QC_TYPE = '{1}'
                           and SOURCE_ID in (select ID
                                               from T_MIS_MONITOR_RESULT RESULT1
                                              where RESULT1.SAMPLE_ID = '{0}'
                                                and RESULT1.QC_TYPE = '0')";
            strSql = string.Format(strSql, strSampleId, strQcType);
            arrVo.Add(strSql);
            //删除样品表【T_MIS_MONITOR_SAMPLE_INFO】相关的样品信息
            strSql = @"delete from T_MIS_MONITOR_SAMPLE_INFO
                         where QC_SOURCE_ID = '{0}'
                           and QC_TYPE = '{1}'";
            strSql = string.Format(strSql, strSampleId, strQcType);
            arrVo.Add(strSql);
            return ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 根据样品号删除该样品底下的现场空白信息
        /// </summary>
        /// <param name="strSampleId">样品号</param>
        /// <param name="strQcType"></param>
        /// <returns></returns>
        public bool deleteQcEmptyInfo(string strSampleId, string strQcType)
        {
            ArrayList arrVo = new ArrayList();
            //删除现场空白结果表【T_MIS_MONITOR_QC_EMPTY_OUT】表信息
            string strSql = @"delete from T_MIS_MONITOR_QC_EMPTY_OUT
                                 where QC_TYPE = '{1}'
                                   and RESULT_ID_SRC in (select ID
                                                           from T_MIS_MONITOR_RESULT RESULT
                                                          where RESULT.SAMPLE_ID = '{0}'
                                                            and RESULT.QC_TYPE = '0')";
            strSql = string.Format(strSql, strSampleId, strQcType);
            arrVo.Add(strSql);
            //删除结果分析执行表【T_MIS_MONITOR_RESULT_APP】的信息
            strSql = @" delete from T_MIS_MONITOR_RESULT_APP
                             where RESULT_ID in (select ID
                                                   from T_MIS_MONITOR_RESULT RESULT2
                                                  where RESULT2.SOURCE_ID in
                                                        (select ID
                                                           from T_MIS_MONITOR_RESULT RESULT1
                                                          where RESULT1.SAMPLE_ID = '{0}'
                                                            and RESULT1.QC_TYPE = '0')
                                                    and RESULT2.QC_TYPE = '{1}')";
            strSql = string.Format(strSql, strSampleId, strQcType);
            arrVo.Add(strSql);
            //删除分析结果表【T_MIS_MONITOR_RESULT】的信息
            strSql = @" delete from T_MIS_MONITOR_RESULT
                         where QC_TYPE = '{1}'
                           and SOURCE_ID in (select ID
                                               from T_MIS_MONITOR_RESULT RESULT1
                                              where RESULT1.SAMPLE_ID = '{0}'
                                                and RESULT1.QC_TYPE = '0')";
            strSql = string.Format(strSql, strSampleId, strQcType);
            arrVo.Add(strSql);

            return ExecuteSQLByTransaction(arrVo);
        }
        #endregion

        #region 采样前质控【秦皇岛】
        /// <summary>
        /// 获取采样前质控任务信息
        /// </summary>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数量</param>
        /// <returns></returns>
        public DataTable getSamplingBeginQcTaskInfo(string strQcStatus, string strSubTaskStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                          from T_MIS_MONITOR_TASK
                                         where exists (select *
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID) and QC_STATUS='{1}'";
            strSql = string.Format(strSql, strSubTaskStatus, strQcStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 获取采样前质控任务数量
        /// </summary>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <returns></returns>
        public int getSamplingBeginQcTaskCount(string strQcStatus, string strSubTaskStatus)
        {
            string strSql = @"select count(*)
                                              from T_MIS_MONITOR_TASK
                                             where exists (select *
                                                      from T_MIS_MONITOR_SUBTASK
                                                     where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                                       and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID) and QC_STATUS='{1}'";
            strSql = string.Format(strSql, strSubTaskStatus, strQcStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 获取采样前质控监测类别
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <returns></returns>
        public DataTable getSamplingBeginQcItemTypeInfo(string strTaskId, string strSubTaskStatus)
        {
            string strSql = @"select ID, MONITOR_ID
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'
                                           and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{1}'";
            strSql = string.Format(strSql, strTaskId, strSubTaskStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取采样前质控样品信息
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <returns></returns>
        public DataTable getSamplingBeginQcSampleInfo(string strSubTaskId)
        {
            string strSql = @"select *
                              from T_MIS_MONITOR_SAMPLE_INFO
                             where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                               and SUBTASK_ID = '{0}'";
            strSql = string.Format(strSql, strSubTaskId);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取采样前质控监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <returns></returns>
        public DataTable getSamplingBeginQcItemInfo(string strSampleId)
        {
            string strSql = @"select * from T_MIS_MONITOR_RESULT where SAMPLE_ID = '{0}'";
            strSql = string.Format(strSql, strSampleId);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strSubTaskNextStatus">下一环节状态</param>
        /// <returns></returns>
        public bool sendSamplingBeginQcTaskToNext(string strSampleId, string strSubTaskNextStatus)
        {
            ArrayList arrVo = new ArrayList();
            string strSql = "update T_MIS_MONITOR_TASK set QC_STATUS='{1}' where ID='{0}'";
            strSql = string.Format(strSql, strSampleId, strSubTaskNextStatus);
            arrVo.Add(strSql);
            return ExecuteSQLByTransaction(arrVo);
        }
        #endregion

        #region 质控平行、空白加标方法【秦皇岛】

        /// <summary>
        /// 获取质控平行【9】，空白加标【10】监测项目信息
        /// </summary>
        /// <param name="strSubTaskId">子任务编号</param>
        /// <param name="strQcType">质控类型</param>
        /// <returns></returns>
        public DataTable getQcItemInfo_QHD(string strSubTaskId, string strQcType)
        {
            string strSql = @"select *
                                          from T_BASE_ITEM_INFO
                                         where IS_SAMPLEDEPT = '否'
                                           and exists (select *
                                                  from T_MIS_MONITOR_RESULT
                                                 where QC_TYPE = '{1}'
                                                   and exists (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where ID = '{0}'
                                                           and T_MIS_MONITOR_SAMPLE_INFO.ID =
                                                               T_MIS_MONITOR_RESULT.SAMPLE_ID)
                                                   and T_MIS_MONITOR_RESULT.ITEM_ID =
                                                       T_BASE_ITEM_INFO.ID)";
            strSql = string.Format(strSql, strSubTaskId, strQcType);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        #endregion

        #region 标准盲样【郑州】

        /// <summary>
        /// 根据样品ID获取标准盲样信息
        /// </summary>
        /// <param name="strampleId">样品ID</param>
        /// <returns></returns>
        public string getBlindString(string strSampleId)
        {
            string strSql = @"select t1.RESULT_ID,
                                           (select ITEM_NAME from T_BASE_ITEM_INFO where ID = t1.ITEM_ID) ITEM_NAME,
                                           t2.STANDARD_VALUE,
                                           t2.UNCETAINTY
                                      from (select ID as RESULT_ID, ITEM_ID
                                              from T_MIS_MONITOR_RESULT
                                             where exists (select *
                                                      from T_MIS_MONITOR_SAMPLE_INFO
                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                           T_MIS_MONITOR_SAMPLE_INFO.ID
                                                       and T_MIS_MONITOR_SAMPLE_INFO.ID = '{0}')) t1
                                      left join T_MIS_MONITOR_QC_BLIND_ZZ t2
                                        on t1.RESULT_ID = t2.RESULT_ID";
            strSql = string.Format(strSql, strSampleId);
            string strBlindQc = "";

            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            if (objTable.Rows.Count > 0)
            {
                string strItemName = objTable.Rows[0]["ITEM_NAME"] == null ? "" : objTable.Rows[0]["ITEM_NAME"].ToString();
                string strStandValue = objTable.Rows[0]["STANDARD_VALUE"] == null ? "" : objTable.Rows[0]["STANDARD_VALUE"].ToString();
                string strUncetainty = objTable.Rows[0]["UNCETAINTY"] == null ? "" : objTable.Rows[0]["UNCETAINTY"].ToString();

                strBlindQc = strItemName + " 标准值：" + strStandValue + "；不确定度：" + strUncetainty;
            }
            return strBlindQc;
        }
        #endregion

        #region 采样【郑州】
        /// <summary>
        /// 获取采样任务退回信息
        /// </summary>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strQcStatus">采样环节状态</param>
        /// <returns></returns>
        public DataTable getSamplingBackInfo_ZZ(string strSubTaskStatus, string strQcStatus)
        {
            string strSql = @"select A.*,
                               E.PROJECT_NAME,
                               E.ASKING_DATE,
                               E.CONTRACT_CODE,
                               E.TICKET_NUM,
                               C.COMPANY_NAME,
                               D.DICT_TEXT AS AREA_NAME,
                               E.ID as TASK_ID
                          from T_MIS_CONTRACT_PLAN A
                          LEFT JOIN dbo.T_MIS_MONITOR_TASK E
                            ON E.PLAN_ID = A.ID
                           and E.ID in
                               (select TASK_ID from T_MIS_MONITOR_SUBTASK where TASK_STATUS = '{0}')
                          LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY C
                            ON E.TESTED_COMPANY_ID = C.ID
                          LEFT JOIN dbo.T_MIS_MONITOR_TASK_COMPANY G
                            ON E.CLIENT_COMPANY_ID = G.ID
                          LEFT JOIN dbo.T_SYS_DICT D
                            ON D.DICT_TYPE = 'administrative_area'
                           AND C.AREA = D.DICT_CODE
                         WHERE 1 = 1
                           AND A.HAS_DONE = '1'
                           AND (E.TASK_TYPE IS NULL OR E.TASK_TYPE IN ('0', '1'))
                           AND E.QC_STATUS = '{1}'
                         order by A.ID ASC";
            strSql = string.Format(strSql, strSubTaskStatus, strQcStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        #endregion

        #region 分析任务分配

        /// <summary>
        /// 获取任务信息 by 熊卫华 2012.11.29
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskInfo(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select distinct T_MIS_MONITOR_TASK.* --,T_MIS_MONITOR_SUBTASK.SAMPLE_ASK_DATE   --,T_MIS_MONITOR_SUBTASK.ID subTaskId
                                          from T_MIS_MONITOR_TASK
                                          INNER JOIN T_MIS_MONITOR_SUBTASK ON(T_MIS_MONITOR_SUBTASK.TASK_ID=T_MIS_MONITOR_TASK.ID)
                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                           and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                               T_MIS_MONITOR_SUBTASK.ID
                                                           and exists (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                   and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID))
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS in({1})))
                                                                   order by T_MIS_MONITOR_TASK.CREATE_DATE desc";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpressEx(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 获取任务信息 by weilin 2014.05.05
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskInfo_QHD(string strTaskStatus, string strResultStatus, string strSampleStatus, string iSample, int iIndex, int iCount)
        {
            string strResultWhere = "";
            string strValue = getSampleItem();
            if (iSample == "0")
            {
                strResultWhere = "(T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002') and T_MIS_MONITOR_RESULT.ITEM_ID not in(" + strValue + "))";
                strResultWhere += " or (T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002'))";
            }
            else if (iSample == "1")
            {
                strResultWhere = "(T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002') and T_MIS_MONITOR_RESULT.ITEM_ID in(" + strValue + "))";
                strResultWhere += " or (T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002') and 1=2)";
            }
            else
            {
                strResultWhere = "1=1";
            }

            string strSql = @"select distinct T_MIS_MONITOR_TASK.*,T_MIS_MONITOR_SUBTASK.SAMPLE_ASK_DATE   --,T_MIS_MONITOR_SUBTASK.ID subTaskId
                                          from T_MIS_MONITOR_TASK
                                          INNER JOIN T_MIS_MONITOR_SUBTASK ON(T_MIS_MONITOR_SUBTASK.TASK_ID=T_MIS_MONITOR_TASK.ID)
                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({0})
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                           and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                               T_MIS_MONITOR_SUBTASK.ID
                                                           and exists (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                   and ({3})
                                                                   and exists (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID)
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS in({1})))
                                                                   order by T_MIS_MONITOR_TASK.CREATE_DATE desc";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus, strSampleStatus, strResultWhere);
            return SqlHelper.ExecuteDataTable(BuildPagerExpressEx(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 获取任务信息总记录数量 by 熊卫华 2012.11.29
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getTaskInfoCount(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select count(*)
                                              from T_MIS_MONITOR_TASK
                                             where exists (select *
                                                      from T_MIS_MONITOR_SUBTASK
                                                     where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                                       and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                       and exists
                                                     (select *
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                               and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                                   T_MIS_MONITOR_SUBTASK.ID
                                                               and exists (select *
                                                                      from T_MIS_MONITOR_RESULT
                                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                           T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                                              from T_BASE_ITEM_INFO
                                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                               and T_BASE_ITEM_INFO.ID =
                                                                                   T_MIS_MONITOR_RESULT.ITEM_ID))
                                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS in ({1}))))";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        public DataTable getTaskInfo_MAS(string strTaskID, string strSubTaskID, int iIndex, int iCount)
        {
            string strSql = @"select distinct T_MIS_MONITOR_TASK.* --,T_MIS_MONITOR_SUBTASK.SAMPLE_ASK_DATE   --,T_MIS_MONITOR_SUBTASK.ID subTaskId
                                          from T_MIS_MONITOR_TASK
                                          INNER JOIN T_MIS_MONITOR_SUBTASK ON(T_MIS_MONITOR_SUBTASK.TASK_ID=T_MIS_MONITOR_TASK.ID)
                                         where {0} and {1}
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                               T_MIS_MONITOR_SUBTASK.ID
                                                           and exists (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                   and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID))
                                                                   ))
                                                                   order by T_MIS_MONITOR_TASK.CREATE_DATE desc";
            strSql = string.Format(strSql, strTaskID == "" ? "1=1" : "T_MIS_MONITOR_SUBTASK.TASK_ID='" + strTaskID + "'", strSubTaskID == "" ? "1=1" : "T_MIS_MONITOR_SUBTASK.ID='" + strSubTaskID + "'");
            return SqlHelper.ExecuteDataTable(BuildPagerExpressEx(strSql, iIndex, iCount));
        }
        public int getTaskInfoCount_MAS(string strTaskID, string strSubTaskID)
        {
            string strSql = @"select count(*)
                                              from T_MIS_MONITOR_TASK
                                             where exists (select *
                                                      from T_MIS_MONITOR_SUBTASK
                                                     where {0} and {1}
                                                       and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                       and exists
                                                     (select *
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                                   T_MIS_MONITOR_SUBTASK.ID
                                                               and exists (select *
                                                                      from T_MIS_MONITOR_RESULT
                                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                           T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                                              from T_BASE_ITEM_INFO
                                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                               and T_BASE_ITEM_INFO.ID =
                                                                                   T_MIS_MONITOR_RESULT.ITEM_ID))
                                                                       )))";
            strSql = string.Format(strSql, strTaskID == "" ? "1=1" : "T_MIS_MONITOR_SUBTASK.TASK_ID='" + strTaskID + "'", strSubTaskID == "" ? "1=1" : "T_MIS_MONITOR_SUBTASK.ID='" + strSubTaskID + "'");
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        //huangjinjun add 2015-12-21 承德数据汇总节点
        public DataTable getTaskInfo_CD(string strTaskID, string strSubTaskID, int iIndex, int iCount)
        {
            string strSql = @"select distinct T_MIS_MONITOR_TASK.* 
                                          from T_MIS_MONITOR_TASK
                                          INNER JOIN T_MIS_MONITOR_SUBTASK ON(T_MIS_MONITOR_SUBTASK.TASK_ID=T_MIS_MONITOR_TASK.ID)
                                         where {0} and {1}";
            strSql = string.Format(strSql, strTaskID == "" ? "1=1" : "T_MIS_MONITOR_SUBTASK.TASK_ID='" + strTaskID + "'", strSubTaskID == "" ? "1=1" : "T_MIS_MONITOR_SUBTASK.ID='" + strSubTaskID + "'");
            return SqlHelper.ExecuteDataTable(BuildPagerExpressEx(strSql, iIndex, iCount));
        }

        /// <summary>
        /// 获取任务信息总记录数量 by weilin 2014.05.05
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getTaskInfoCount_QHD(string strTaskStatus, string strResultStatus, string strSampleStatus, string iSample)
        {
            string strResultWhere = "";
            string strValue = getSampleItem();
            if (iSample == "0")
            {
                strResultWhere = "(T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002') and T_MIS_MONITOR_RESULT.ITEM_ID not in(" + strValue + "))";
                strResultWhere += " or (T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002'))";
            }
            else if (iSample == "1")
            {
                strResultWhere = "(T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002') and T_MIS_MONITOR_RESULT.ITEM_ID in(" + strValue + "))";
                strResultWhere += " or (T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002') and 1=2)";
            }
            else
            {
                strResultWhere = "1=1";
            }

            string strSql = @"select count(*)
                                              from T_MIS_MONITOR_TASK
                                             where exists (select *
                                                      from T_MIS_MONITOR_SUBTASK
                                                     where T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({0})
                                                       and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                       and exists
                                                     (select *
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                               and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                                   T_MIS_MONITOR_SUBTASK.ID
                                                               and exists (select *
                                                                      from T_MIS_MONITOR_RESULT
                                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                           T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                       and ({3})
                                                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                                              from T_BASE_ITEM_INFO
                                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                               --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                               and T_BASE_ITEM_INFO.ID =
                                                                                   T_MIS_MONITOR_RESULT.ITEM_ID))
                                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS in ({1}))))";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus, strSampleStatus, strResultWhere);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 获取监测类别信息 by 熊卫华 2012.11.30
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskId">监测任务Id</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getItemTypeInfo(string strCurrentUserId, string strFlowCode, string strTaskId, string strTaskStatus, string strResultStatus)
        {
            //设置默认分析完成时间
            /*
            string strSql = @"update T_MIS_MONITOR_RESULT_APP
                                               set ASKING_DATE = '{2}'
                                             where exists
                                             (select *
                                                      from T_MIS_MONITOR_RESULT
                                                     where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS in ({1})
                                                       and exists (select *
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where T_MIS_MONITOR_SAMPLE_INFO.ID =
                                                                   T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                               and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                               and exists (select *
                                                                      from T_MIS_MONITOR_SUBTASK
                                                                     where T_MIS_MONITOR_SUBTASK.ID =
                                                                           T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                       and T_MIS_MONITOR_SUBTASK.TASK_ID =
                                                                           '{0}')))
                                               and ASKING_DATE is null";
            string strAskingDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + "23:59:59";
            strSql = string.Format(strSql, strTaskId, strResultStatus, strAskingDate);
            */
            //            string strSql = @"UPDATE APP SET ASKING_DATE=convert(varchar(10),dateadd(d,1,getdate()),120) from T_MIS_MONITOR_RESULT_APP APP 
            //                    left join T_MIS_MONITOR_RESULT RESULT on(APP.RESULT_ID=RESULT.ID)
            //                    left join T_MIS_MONITOR_SAMPLE_INFO SAMPLEINFO on(SAMPLEINFO.ID=RESULT.SAMPLE_ID)
            //                    left join T_MIS_MONITOR_SUBTASK SUBTASK on(SUBTASK.ID=SAMPLEINFO.SUBTASK_ID)
            //                    left join T_MIS_MONITOR_TASK TASK on(TASK.ID=SUBTASK.TASK_ID)
            //                    where TASK.ID='{0}' and RESULT.RESULT_STATUS in ({1})";
            //            strSql = string.Format(strSql, strTaskId, strResultStatus);

            //            SqlHelper.ExecuteNonQuery(strSql);
            string strSql = "";

            strSql = @"select ID, MONITOR_ID, SAMPLE_FINISH_DATE, SAMPLING_MANAGER_ID
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'
                                           and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{1}'
                                           and exists (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.ID
                                                   and exists (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS in ({2})))";
            strSql = string.Format(strSql, strTaskId, strTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        public DataTable getItemTypeInfo_MAS(string strTaskID, string strSubTaskID)
        {
            string strSql = "";

            strSql = @"select ID, MONITOR_ID, SAMPLE_FINISH_DATE, SAMPLING_MANAGER_ID
                                          from T_MIS_MONITOR_SUBTASK
                                         where {0}
                                           and {1}
                                           and exists (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.ID
                                                   and exists (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))
                                                           ))";
            strSql = string.Format(strSql, strTaskID == "" ? "1=1" : "T_MIS_MONITOR_SUBTASK.TASK_ID='" + strTaskID + "'", strSubTaskID == "" ? "1=1" : "T_MIS_MONITOR_SUBTASK.ID='" + strSubTaskID + "'");
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取监测类别信息 by weilin 2014.05.05
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskId">监测任务Id</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getItemTypeInfo_QHD(string strTaskId, string strTaskStatus, string strResultStatus, string strSampleStatus, bool b)
        {
            string strSql = "";
            if (b)
            {
                strSql = @"UPDATE APP SET ASKING_DATE=convert(varchar(10),dateadd(d,1,getdate()),120) from T_MIS_MONITOR_RESULT_APP APP 
                    left join T_MIS_MONITOR_RESULT RESULT on(APP.RESULT_ID=RESULT.ID)
                    left join T_MIS_MONITOR_SAMPLE_INFO SAMPLEINFO on(SAMPLEINFO.ID=RESULT.SAMPLE_ID)
                    left join T_MIS_MONITOR_SUBTASK SUBTASK on(SUBTASK.ID=SAMPLEINFO.SUBTASK_ID)
                    left join T_MIS_MONITOR_TASK TASK on(TASK.ID=SUBTASK.TASK_ID)
                    where TASK.ID='{0}' and RESULT.RESULT_STATUS in ({1}) and SAMPLEINFO.NOSAMPLE='{2}'";
                strSql = string.Format(strSql, strTaskId, strResultStatus, strSampleStatus);
                SqlHelper.ExecuteNonQuery(strSql);
            }

            strSql = @"select ID, MONITOR_ID, SAMPLE_FINISH_DATE
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'
                                           and T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({1})
                                           and exists (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{3}'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.ID
                                                   and exists (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS in ({2})))";
            strSql = string.Format(strSql, strTaskId, strTaskStatus, strResultStatus, strSampleStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取监测项目信息 by 熊卫华 2012.11.30
        /// </summary>
        /// <param name="strSubTaskId">子任务Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getItemInfo(string strSubTaskId, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select distinct ITEM_ID
                                              from T_MIS_MONITOR_RESULT
                                             where RESULT_STATUS = '{1}'
                                               and exists
                                             (select *
                                                      from T_MIS_MONITOR_SAMPLE_INFO
                                                     where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID = '{0}'
                                                       and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                       and T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID)
                                               and exists
                                             (select *
                                                      from T_BASE_ITEM_INFO
                                                     where T_MIS_MONITOR_RESULT.ITEM_ID = T_BASE_ITEM_INFO.ID
                                                       and T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                       and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否')";
            strSql = string.Format(strSql, strSubTaskId, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 获取监测项目数量 by 熊卫华 2012.11.30
        /// </summary>
        /// <param name="strSubTaskId">子任务Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getItemInfoCount(string strSubTaskId, string strResultStatus)
        {
            string strSql = @"select count(*)
                                          from (select distinct ITEM_ID
                                                  from T_MIS_MONITOR_RESULT
                                                 where RESULT_STATUS = '{1}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID = '{0}'
                                                           and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                           and T_MIS_MONITOR_SAMPLE_INFO.ID =
                                                               T_MIS_MONITOR_RESULT.SAMPLE_ID)
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_MIS_MONITOR_RESULT.ITEM_ID = T_BASE_ITEM_INFO.ID
                                                           and T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'))";
            strSql = string.Format(strSql, strSubTaskId, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 获取监测项目附加信息,如分析负责人、分析协同人，分析完成时间 by 熊卫华 2013.04.23
        /// </summary>
        /// <param name="strResultId">监测结果ID</param>
        /// <returns></returns>
        public DataTable getItemExInfo(string strResultId)
        {
            string strSql = @"select HEAD_USERID, ASSISTANT_USERID, ASKING_DATE
                                      from T_MIS_MONITOR_RESULT_APP
                                            where RESULT_ID = '{0}'";
            strSql = string.Format(strSql, strResultId);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 保存监测项目附加信息数据，如分析负责人、分析协同人，分析完成时间  by 熊卫华 2013.04.23
        /// </summary>
        /// <param name="strResultId">监测结果ID</param>
        /// <param name="strValue">需要设置的值</param>
        /// <param name="strColumnName">列名</param>
        /// <returns></returns>
        public bool SaveItemExInfo(string strResultId, string strValue, string strColumnName)
        {
            string strSql = @"update T_MIS_MONITOR_RESULT_APP set {1} = '{2}' where RESULT_ID = '{0}'";
            strSql = string.Format(strSql, strResultId, strColumnName, strValue);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        /// <summary>
        /// 保存监测项目附加信息数据，如分析负责人、分析协同人，分析完成时间  by weilin 2014.05.06
        /// </summary>
        /// <returns></returns>
        public bool SaveItemExInfo_QHD(string strSampleIDs, string strItemID, string strValue, string strColumnName)
        {
            string strSql = @"update T_MIS_MONITOR_RESULT_APP set {0}='{1}' where RESULT_ID in(
                                select ID from T_MIS_MONITOR_RESULT
                                where SAMPLE_ID in('{2}') and ITEM_ID='{3}')";
            strSql = string.Format(strSql, strColumnName, strValue, strSampleIDs, strItemID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        /// <summary>
        /// 保存监测项目附加信息数据，如:分析完成时间  by weilin 2014.09.19
        /// </summary>
        /// <returns></returns>
        public bool SaveItemExInfo_QY(string strSampleID, string strStatus, string strValue, string strColumnName)
        {
            string strSql = @"update T_MIS_MONITOR_RESULT_APP set {0}='{1}' where RESULT_ID in(
                                select ID from T_MIS_MONITOR_RESULT
                                where SAMPLE_ID in('{2}') and RESULT_STATUS in('{3}')
                                and exists(select 1 from T_BASE_ITEM_INFO where T_MIS_MONITOR_RESULT.ITEM_ID=ID and IS_SAMPLEDEPT='否'))";
            strSql = string.Format(strSql, strColumnName, strValue, strSampleID, strStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        /// <summary>
        /// 保存监测项目附加信息数据，如:分析完成时间  by weilin 2015.01.21
        /// </summary>
        /// <returns></returns>
        public bool SaveItemExInfo_MAS(string strSampleID, string strValue, string strColumnName)
        {
            string strSql = @"update T_MIS_MONITOR_RESULT_APP set {0}='{1}' where RESULT_ID in(
                                select ID from T_MIS_MONITOR_RESULT
                                where SAMPLE_ID in('{2}')
                                and exists(select 1 from T_BASE_ITEM_INFO where T_MIS_MONITOR_RESULT.ITEM_ID=ID and IS_SAMPLEDEPT='否'))";
            strSql = string.Format(strSql, strColumnName, strValue, strSampleID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }

        /// <summary>
        /// 获取已分配任务的默认负责人信息
        /// </summary>
        /// <param name="strTaskId">总任务Id</param>
        /// <returns></returns>
        public DataTable getAssignedDefaultUser(string strTaskId)
        {
            string strSql = @"select USER_INFO.ID as USER_ID, USER_INFO.REAL_NAME
                                                  from (select distinct HEAD_USERID
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT_APP.HEAD_USERID is not null
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT_APP.RESULT_ID =
                                                                       T_MIS_MONITOR_RESULT.ID
                                                                   and exists (select *
                                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                                         where T_MIS_MONITOR_SAMPLE_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                                           and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                                           and exists
                                                                         (select *
                                                                                  from T_MIS_MONITOR_SUBTASK
                                                                                 where T_MIS_MONITOR_SUBTASK.ID =
                                                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))
                                                                   and exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID))) DEFAULT_USER
                                                  left join T_SYS_USER USER_INFO
                                                    on DEFAULT_USER.HEAD_USERID = USER_INFO.ID";
            strSql = string.Format(strSql, strTaskId);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取已分配任务的负责项目
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strDefaultUser">默认负责人ID</param>
        /// <returns></returns>
        public DataTable getAssignedDefaultItem(string strTaskId, string strDefaultUser)
        {
            string strSql = @"select DEFAULT_ITEM.ITEM_ID, T_BASE_ITEM_INFO.ITEM_NAME
                                                  from (select distinct ITEM_ID
                                                          from T_MIS_MONITOR_RESULT
                                                         where exists (select *
                                                                  from T_MIS_MONITOR_RESULT_APP
                                                                 where T_MIS_MONITOR_RESULT.ID =
                                                                       T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                                 where T_MIS_MONITOR_SAMPLE_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                                   and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                                   and exists
                                                                 (select *
                                                                          from T_MIS_MONITOR_SUBTASK
                                                                         where T_MIS_MONITOR_SUBTASK.ID =
                                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))) DEFAULT_ITEM
                                                  left join T_BASE_ITEM_INFO
                                                    on DEFAULT_ITEM.ITEM_ID = T_BASE_ITEM_INFO.ID";
            strSql = string.Format(strSql, strTaskId, strDefaultUser);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取已分配任务的协同项目
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strDefaultUser">默认协同人ID</param>
        /// <returns></returns>
        public DataTable getAssignedDefaultItemEx(string strTaskId, string strDefaultUser)
        {
            string strSql = @"select DEFAULT_ITEM.ITEM_ID, T_BASE_ITEM_INFO.ITEM_NAME
                                          from (select distinct ITEM_ID
                                                  from T_MIS_MONITOR_RESULT
                                                 where exists (select *
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT.ID =
                                                               T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                           and T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID like '%{1}%' )
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                                   and exists (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.ID =
                                                               T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                           and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                           and exists (select *
                                                                  from T_MIS_MONITOR_SUBTASK
                                                                 where T_MIS_MONITOR_SUBTASK.ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID =
                                                                       '{0}'))) DEFAULT_ITEM
                                          left join T_BASE_ITEM_INFO
                                            on DEFAULT_ITEM.ITEM_ID = T_BASE_ITEM_INFO.ID";
            strSql = string.Format(strSql, strTaskId, strDefaultUser);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取已分配任务的样品号
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strDefaultUser">默认协同人ID</param>
        /// <returns></returns>
        public DataTable getAssignedSampleCode(string strTaskId, string strDefaultUser)
        {
            string strSql = @"select distinct SAMPLE_CODE
                                          from T_MIS_MONITOR_SAMPLE_INFO
                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                           and exists (select *
                                                  from T_MIS_MONITOR_RESULT
                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =T_MIS_MONITOR_SAMPLE_INFO.ID
                                                          and exists (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID =
                                                               T_MIS_MONITOR_RESULT.ITEM_ID)
                                                   and exists (select *
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT.ID =
                                                               T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                           and T_MIS_MONITOR_RESULT_APP.HEAD_USERID =
                                                               '{1}'))
                                           and exists (select *
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where T_MIS_MONITOR_SUBTASK.ID =
                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}')";
            strSql = string.Format(strSql, strTaskId, strDefaultUser);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 判断任务是否可以返回
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public bool IsCanGoToBack(string strTaskId, string strCurrentUserId, string strFlowCode, string strResultStatus)
        {
            string strSql = @"select *
                                          from T_MIS_MONITOR_RESULT
                                         where T_MIS_MONITOR_RESULT.RESULT_STATUS not in ({1})
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
                                                   and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            strSql = string.Format(strSql, strTaskId, strResultStatus);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            return dt.Rows.Count > 0 ? false : true;
        }

        /// <summary>
        /// 子任务返回
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strBackStatus">回退的状态</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public bool subTaskGoToBack(string strTaskId, string strCurrentUserId, string strBackStatus, string strFlowCode)
        {
            string strSql = @"update T_MIS_MONITOR_SUBTASK set TASK_STATUS='{1}',TASK_TYPE='退回'
                                         where TASK_ID = '{0}'";
            strSql = string.Format(strSql, strTaskId, strBackStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }

        /// <summary>
        /// 子任务返回
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strBackStatus">回退的状态</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public bool subTaskGoToBack_QHD(string strTaskId, string strSubTaskStatus, string strBackStatus, string strSampleStatus, string strResultStatus)
        {
            string strSql = @"update a set a.TASK_STATUS='{1}',a.TASK_TYPE='退回'
                              from T_MIS_MONITOR_SUBTASK a left join T_MIS_MONITOR_SAMPLE_INFO b on(a.ID=b.SUBTASK_ID)
                              left join T_MIS_MONITOR_RESULT c on(b.ID=c.SAMPLE_ID)
                                         where a.TASK_ID = '{0}' and a.TASK_STATUS='{2}' and b.NOSAMPLE='{3}' and c.RESULT_STATUS='{4}'";
            strSql = string.Format(strSql, strTaskId, strBackStatus, strSubTaskStatus, strSampleStatus, strResultStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        /// <summary>
        /// 样品退回
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="strSampleStatus"></param>
        /// <param name="strBackStatus"></param>
        /// <param name="strResultStatus"></param>
        /// <returns></returns>
        public bool SampleGoToBack_QHD(string strTaskId, string strSampleStatus, string strBackStatus, string strResultStatus)
        {
            string strSql = @"update b set b.NOSAMPLE='{1}'
                              from T_MIS_MONITOR_SUBTASK a left join T_MIS_MONITOR_SAMPLE_INFO b on(a.ID=b.SUBTASK_ID)
                              left join T_MIS_MONITOR_RESULT c on(b.ID=c.SAMPLE_ID)
                                         where a.TASK_ID = '{0}' and b.NOSAMPLE='{2}' and c.RESULT_STATUS='{3}'";
            strSql = string.Format(strSql, strTaskId, strBackStatus, strSampleStatus, strResultStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }

        /// <summary>
        /// 子任务返回 Create By weilin 2013-11-12
        /// </summary>
        public bool subTaskGoToBackEx(string strTaskId, string strCurrentStatus, string strCurrentUserId, string strBackStatus, string strFlowCode)
        {
            string strSql = @"update T_MIS_MONITOR_SUBTASK set TASK_STATUS='{1}',TASK_TYPE='退回'
                                         where TASK_ID = '{0}' and TASK_STATUS= '{2}'";
            strSql = string.Format(strSql, strTaskId, strBackStatus, strCurrentStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }

        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strCurrenteResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public bool sendToNextFlow(string strTaskId, string strCurrentUserId, string strFlowCode, string strSubTaskStatus, string strCurrenteResultStatus, string strNextResultStatus)
        {
            ArrayList arrVo = new ArrayList();

            //判断发送之前默认负责人是否已经选择，如果没有选择则发送不成功
            string strSql = @"select *
                                  from dbo.T_MIS_MONITOR_RESULT_APP
                                 where (T_MIS_MONITOR_RESULT_APP.HEAD_USERID is null or
                                       T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '')
                                   and exists
                                 (select *
                                          from T_MIS_MONITOR_RESULT
                                         where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                           and RESULT_STATUS in ({2})
                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                         (select *
                                                  from T_BASE_ITEM_INFO
                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.ID =
                                                       T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                   and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                           and TASK_STATUS = '{1}'
                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}')))";
            strSql = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus);
            DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
            if (objTable.Rows.Count > 0)
                return false;

            strSql = @"update T_MIS_MONITOR_RESULT
                                               set T_MIS_MONITOR_RESULT.RESULT_STATUS = '{3}',TASK_TYPE='发送'
                                             where RESULT_STATUS in ({2})
                                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                       from T_BASE_ITEM_INFO
                                                      where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                        and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                        and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                                and exists
                                              (select *
                                                       from T_MIS_MONITOR_SAMPLE_INFO
                                                      where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                        and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                        and exists
                                                      (select *
                                                               from T_MIS_MONITOR_SUBTASK
                                                              where T_MIS_MONITOR_SUBTASK.ID =
                                                                    T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                    and TASK_STATUS='{1}'
                                                                and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            strSql = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strNextResultStatus);
            arrVo.Add(strSql);
            //判断T_MIS_MONITOR_SUBTASK表中的ANALYSE_ASSIGN_DATE字段是否填写，如果没有填写则写入
            strSql = @"select distinct ANALYSE_ASSIGN_DATE
                                  from T_MIS_MONITOR_SUBTASK
                                 where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'";
            strSql = string.Format(strSql, strTaskId);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ANALYSE_ASSIGN_DATE"] == null || dt.Rows[0]["ANALYSE_ASSIGN_DATE"].ToString() == "")
                {
                    strSql = @"update T_MIS_MONITOR_SUBTASK set ANALYSE_ASSIGN_DATE='{1}'
                                         where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'";
                    strSql = string.Format(strSql, strTaskId, DateTime.Now.ToString("yyyy-MM-dd"));
                    arrVo.Add(strSql);
                }
            }
            //将分析任务分配信息写入监测子任务表(T_MIS_MONITOR_SUBTASK)
            strSql = @"select ID
                                  from T_MIS_MONITOR_SUBTASK
                                 where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'";
            strSql = string.Format(strSql, strTaskId);
            dt = SqlHelper.ExecuteDataTable(strSql);
            string strDateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            foreach (DataRow row in dt.Rows)
            {
                string strId = row["ID"].ToString();
                strSql = @"update T_MIS_MONITOR_SUBTASK_APP set ANALYSE_ASSIGN_ID='{0}',ANALYSE_ASSIGN_DATE='{1}' where SUBTASK_ID='{2}'";
                strSql = string.Format(strSql, strCurrentUserId, strDateTimeNow, strId);
                arrVo.Add(strSql);
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strCurrenteResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public bool sendToNextFlow_QHD(string strTaskId, string strCurrentUserId, string strSubTaskStatus, string strSampleStatus, string strCurrenteResultStatus, string strNextResultStatus, string iSample)
        {
            ArrayList arrVo = new ArrayList();

            //判断发送之前默认负责人是否已经选择，如果没有选择则发送不成功
            string strSql = @"select *
                                  from dbo.T_MIS_MONITOR_RESULT_APP
                                 where (T_MIS_MONITOR_RESULT_APP.HEAD_USERID is null or
                                       T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '')
                                   and exists
                                 (select *
                                          from T_MIS_MONITOR_RESULT
                                         where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                           and RESULT_STATUS = '{2}'
                                           {5}
                                           and exists
                                         (select *
                                                  from T_BASE_ITEM_INFO
                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                   --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.ID =
                                                       T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                   and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{3}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                           and TASK_STATUS in ({1})
                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'
                                                           {4}) ))";
            string strSql0 = "";
            string strSql1 = "";
            string strValue = getSampleItem();
            if (iSample == "0")
            {
                strSql0 = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strSampleStatus, " and T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002')", " and ITEM_ID not in(" + strValue + ")");
                strSql1 = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strSampleStatus, " and T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002')", "");
            }
            else
            {
                strSql0 = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strSampleStatus, " and T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002')", " and ITEM_ID in(" + strValue + ")");
                strSql1 = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strSampleStatus, " and T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002')", " and 1=2");
            }

            DataTable objTable = SqlHelper.ExecuteDataTable(strSql0);
            DataTable objTable1 = SqlHelper.ExecuteDataTable(strSql1);
            if (objTable.Rows.Count > 0 || objTable1.Rows.Count > 0)
                return false;

            strSql = @"update T_MIS_MONITOR_RESULT
                                               set T_MIS_MONITOR_RESULT.RESULT_STATUS = '{3}',TASK_TYPE='发送'
                                             where RESULT_STATUS in ({2})
                                                       {6}
                                                       and exists (select *
                                                       from T_BASE_ITEM_INFO
                                                      where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                        --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                        and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                                and exists
                                              (select *
                                                       from T_MIS_MONITOR_SAMPLE_INFO
                                                      where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                        and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{4}'
                                                        and exists
                                                      (select *
                                                               from T_MIS_MONITOR_SUBTASK
                                                              where T_MIS_MONITOR_SUBTASK.ID =
                                                                    T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                    and TASK_STATUS in({1})
                                                                and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}' {5}))";
            //strSql = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strNextResultStatus, strSampleStatus);
            if (iSample == "0")
            {
                strSql0 = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strNextResultStatus, strSampleStatus, " and T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002')", " and ITEM_ID not in(" + strValue + ")");
                strSql1 = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strNextResultStatus, strSampleStatus, " and T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002')", "");
            }
            else
            {
                strSql0 = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strNextResultStatus, strSampleStatus, " and T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002')", " and ITEM_ID in(" + strValue + ")");
                strSql1 = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strNextResultStatus, strSampleStatus, " and T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002')", " and 1=2");
            }

            arrVo.Add(strSql0);
            arrVo.Add(strSql1);
            //判断T_MIS_MONITOR_SUBTASK表中的ANALYSE_ASSIGN_DATE字段是否填写，如果没有填写则写入
            strSql = @"select distinct ANALYSE_ASSIGN_DATE
                                  from T_MIS_MONITOR_SUBTASK
                                 where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'";
            strSql = string.Format(strSql, strTaskId);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ANALYSE_ASSIGN_DATE"] == null || dt.Rows[0]["ANALYSE_ASSIGN_DATE"].ToString() == "")
                {
                    strSql = @"update T_MIS_MONITOR_SUBTASK set ANALYSE_ASSIGN_DATE='{1}'
                                         where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'";
                    strSql = string.Format(strSql, strTaskId, DateTime.Now.ToString("yyyy-MM-dd"));
                    arrVo.Add(strSql);
                }
            }
            //将分析任务分配信息写入监测子任务表(T_MIS_MONITOR_SUBTASK)
            strSql = @"select ID
                                  from T_MIS_MONITOR_SUBTASK
                                 where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'";
            strSql = string.Format(strSql, strTaskId);
            dt = SqlHelper.ExecuteDataTable(strSql);
            string strDateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            foreach (DataRow row in dt.Rows)
            {
                string strId = row["ID"].ToString();
                strSql = @"update T_MIS_MONITOR_SUBTASK_APP set ANALYSE_ASSIGN_ID='{0}',ANALYSE_ASSIGN_DATE='{1}' where SUBTASK_ID='{2}'";
                strSql = string.Format(strSql, strCurrentUserId, strDateTimeNow, strId);
                arrVo.Add(strSql);
            }
            return ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 获取监测结果所属的监测类别
        /// </summary>
        /// <param name="strResultId">监测结果ID</param>
        /// <returns></returns>
        public DataTable getMonitorTypeByResultId(string strResultId)
        {
            string strSql = @"select MONITOR_ID
                                  from T_MIS_MONITOR_SUBTASK
                                 where exists (select *
                                          from T_MIS_MONITOR_SAMPLE_INFO
                                         where T_MIS_MONITOR_SUBTASK.ID =
                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                           and exists (select *
                                                  from T_MIS_MONITOR_RESULT
                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                   and ID = '{0}'))";
            strSql = string.Format(strSql, strResultId);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            return dt;
        }
        #endregion

        #region 分析任务分配【秦皇岛】
        /// <summary>
        /// 获取分析任务分配环节中的样品号 by 熊卫华 2013.01.15
        /// </summary>
        /// <param name="strSubTaskId">子任务Id</param>
        /// <param name="strCurrentUserId">当前用户Id</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable getSimpleCodeInAlloction_QHD(string strSubTaskId, string strCurrentUserId, string strResultStatus, string strSampleStatus, string iSample, string strMonitorID)
        {
            string strItemWhere = "";
            string strSAMPLEDEPT = "";
            if (iSample == "0" || iSample == "1")
            {
                if (strMonitorID == "000000001" || strMonitorID == "000000002")
                {
                    string strValue = getSampleItem();
                    if (iSample == "0")
                    {
                        strItemWhere = " and T_MIS_MONITOR_RESULT.ITEM_ID not in(" + strValue + ")";
                    }
                    else
                    {
                        strItemWhere = " and T_MIS_MONITOR_RESULT.ITEM_ID in(" + strValue + ")";
                    }
                }
                else
                {
                    if (iSample == "1")
                    {
                        strItemWhere = " and 1=2";
                    }
                }
                strSAMPLEDEPT = "1=1";
            }
            else
            {
                strSAMPLEDEPT = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'";
            }
            string strSql = @"select ID, SAMPLE_CODE
                              from T_MIS_MONITOR_SAMPLE_INFO
                             where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{3}'
                               and SUBTASK_ID = '{0}'
                               and exists
                             (select *
                                      from T_MIS_MONITOR_RESULT
                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS in ({2})
                                       {4}
                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                     (select *
                                              from T_BASE_ITEM_INFO
                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                               and {5}
                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)))
                             order by T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE";
            strSql = string.Format(strSql, strSubTaskId, strCurrentUserId, strResultStatus, strSampleStatus, strItemWhere, strSAMPLEDEPT);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 根据子任务ID获取需要分析任务分配的样品信息 Create By :weilin 2015-01-22
        /// </summary>
        /// <param name="strSubTaskId"></param>
        /// <returns></returns>
        public DataTable getSampleCodeInAlloction_MAS(string strTaskId, string strSubTaskId)
        {
            string strSql = @"select T_MIS_MONITOR_SAMPLE_INFO.ID, T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE, T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_BARCODE
                              from T_MIS_MONITOR_SAMPLE_INFO
                              left join T_MIS_MONITOR_SUBTASK on(T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID=T_MIS_MONITOR_SUBTASK.ID)
                             where {0} and {1}
                               and exists
                             (select *
                                      from T_MIS_MONITOR_RESULT
                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                      and exists
                                     (select *
                                              from T_BASE_ITEM_INFO
                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                             order by T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE";
            strSql = string.Format(strSql, strTaskId == "" ? "1=1" : "T_MIS_MONITOR_SUBTASK.TASK_ID='" + strTaskId + "'", strSubTaskId == "" ? "1=1" : "T_MIS_MONITOR_SUBTASK.ID='" + strSubTaskId + "'");
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 根据子任务ID获取相关信息
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="b">true:分析项目 false:现场项目</param>
        /// <returns></returns>
        public DataTable getItemInfoBySubTaskID_MAS(string strTaskId, string strSubTaskId, bool b)
        {
            string strSql = @"select a.ID SUBTASKID,c.ID RESULTID,e.ID ITEM_ID,e.IS_ANYSCENE_ITEM,d.HEAD_USERID,f.USER_NAME ,c.SAMPLE_ID,b.ID SampleID
                                from T_MIS_MONITOR_SUBTASK a left join T_MIS_MONITOR_SAMPLE_INFO b on(a.ID=b.SUBTASK_ID)
                                left join T_MIS_MONITOR_RESULT c on(b.ID=c.SAMPLE_ID)
                                left join T_MIS_MONITOR_RESULT_APP d on(c.ID=d.RESULT_ID)
                                left join T_BASE_ITEM_INFO e on(c.ITEM_ID=e.ID)
                                left join T_SYS_USER f on(d.HEAD_USERID=f.ID)
                                where {0} and {1} and e.HAS_SUB_ITEM='0' and e.IS_SAMPLEDEPT='{2}'";
            strSql = string.Format(strSql, strTaskId == "" ? "1=1" : "a.TASK_ID='" + strTaskId + "'", strSubTaskId == "" ? "1=1" : "a.ID='" + strSubTaskId + "'", b ? "否" : "是");
            return SqlHelper.ExecuteDataTable(strSql);



        }

        public string getSampleItem()
        {
            string strSql = @"select ID from T_BASE_ITEM_INFO 
                            where ITEM_NAME like '%pH%'
                            or ITEM_NAME like '%悬浮物%'
                            or ITEM_NAME like '%化学需氧量%'
                            or ITEM_NAME like '%生化需氧量%'
                            or ITEM_NAME like '%色度%'
                            or ITEM_NAME like '%浊度%'
                            or ITEM_NAME like '%石油类%'
                            or ITEM_NAME like '%动植物油%'
                            or ITEM_NAME like '%硫化物%'
                            or ITEM_NAME like '%氨氮%'
                            or ITEM_NAME = '磷酸盐'
                            or ITEM_NAME like '%总磷%'
                            or ITEM_NAME like '%总氮%'
                            or ITEM_NAME like '%氟化物%'
                            or ITEM_NAME like '%粪大肠菌群%'
                            or ITEM_NAME like '%总余氯%'
                            or ITEM_NAME like '%甲醛%'
                            or ITEM_NAME like '%烟尘%'
                            or ITEM_NAME like '%油烟%'
                            or ITEM_NAME like '%颗粒物%'
                            or ITEM_NAME = '氨'";
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            string strValue = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strValue += dt.Rows[i]["ID"].ToString() + ",";
            }
            return strValue.TrimEnd(',');
        }

        /// <summary>
        /// 获取监测项目信息 by 熊卫华 2013.01.15
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getItemInfoInAlloction_QHD(string strSampleId, string strResultStatus, string strSampleStatus, string iSample, string strMonitorID, int iIndex, int iCount)
        {
            string strItemWhere = "";
            string strSAMPLEDEPT = "";
            if (iSample == "0" || iSample == "1")
            {
                if (strMonitorID == "000000001" || strMonitorID == "000000002")
                {
                    string strValue = getSampleItem();
                    if (iSample == "0")
                    {
                        strItemWhere = " and ITEM_ID not in(" + strValue + ")";
                    }
                    else
                    {
                        strItemWhere = " and ITEM_ID in(" + strValue + ")";
                    }
                }
                else
                {
                    if (iSample == "1")
                    {
                        strItemWhere = " and 1=2";
                    }
                }
                strSAMPLEDEPT = "1=1";
            }
            else
            {
                strSAMPLEDEPT = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'";
            }
            string strSql = @"select   *
                                              from T_MIS_MONITOR_RESULT
                                             where RESULT_STATUS in ({1})
					                           and SAMPLE_ID='{0}'
                                               {3}
                                               and exists
                                             (select *
                                                      from T_MIS_MONITOR_SAMPLE_INFO
                                                     where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                       and T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID)
                                               and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                             (select *
                                                      from T_BASE_ITEM_INFO
                                                     where T_MIS_MONITOR_RESULT.ITEM_ID = T_BASE_ITEM_INFO.ID
                                                       and T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                       and {4}))";
            strSql = string.Format(strSql, strSampleId, strResultStatus, strSampleStatus, strItemWhere, strSAMPLEDEPT);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }

        /// <summary>
        /// 获取监测项目数量 by 熊卫华 2013.01.15
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getItemInfoCountInAlloction_QHD(string strSampleId, string strResultStatus, string strSampleStatus, string iSample, string strMonitorID)
        {
            string strItemWhere = "";
            if (iSample == "0" || iSample == "1")
            {
                if (strMonitorID == "000000001" || strMonitorID == "000000002")
                {
                    string strValue = getSampleItem();
                    if (iSample == "0")
                    {
                        strItemWhere = " and ITEM_ID not in(" + strValue + ")";
                    }
                    else
                    {
                        strItemWhere = " and ITEM_ID in(" + strValue + ")";
                    }
                }
                else
                {
                    if (iSample == "1")
                    {
                        strItemWhere = " and 1=2";
                    }
                }
            }
            string strSql = @"select count(*)
                                          from (select distinct ITEM_ID
                                                  from T_MIS_MONITOR_RESULT
                                                 where RESULT_STATUS in ({1})
                                                   and SAMPLE_ID='{0}'
                                                   {3}
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                           and T_MIS_MONITOR_SAMPLE_INFO.ID =
                                                               T_MIS_MONITOR_RESULT.SAMPLE_ID)
                                                   and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_MIS_MONITOR_RESULT.ITEM_ID = T_BASE_ITEM_INFO.ID
                                                           and T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否')))";
            strSql = string.Format(strSql, strSampleId, strResultStatus, strSampleStatus, strItemWhere);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 获取分析监测项目信息 Create By : weilin 2015-01-22
        /// </summary>
        /// <param name="strSampleId"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <param name="ItemCondition_WithOut">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public DataTable getItemInfoInAlloction_MAS(string strSampleId, int iIndex, int iCount, string ItemCondition_WithOut = "1")
        {
            string sqlItemCondition = getItemCondition_WithOut(ItemCondition_WithOut);
            string strSql = @"select   *
                                              from T_MIS_MONITOR_RESULT
                                             where SAMPLE_ID='{0}'
                                               and exists
                                             (select *
                                                      from T_BASE_ITEM_INFO
                                                     where T_MIS_MONITOR_RESULT.ITEM_ID = T_BASE_ITEM_INFO.ID
                                                       and T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' {1}
                                                       --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                )";
            strSql = string.Format(strSql, strSampleId, sqlItemCondition);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 获取分析监测项目数量 Create By : weilin 2015-01-22
        /// </summary>
        /// <param name="strSampleId"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <param name="ItemCondition_WithOut">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public int getItemInfoCountInAlloction_MAS(string strSampleId, string ItemCondition_WithOut = "1")
        {
            string sqlItemCondition = getItemCondition_WithOut(ItemCondition_WithOut);
            string strSql = @"select   count(*)
                                              from T_MIS_MONITOR_RESULT
                                             where SAMPLE_ID='{0}'
                                               and exists
                                             (select *
                                                      from T_BASE_ITEM_INFO
                                                     where T_MIS_MONITOR_RESULT.ITEM_ID = T_BASE_ITEM_INFO.ID
                                                       and T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0' {1}
                                                       --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                        )";
            strSql = string.Format(strSql, strSampleId, sqlItemCondition);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 将监测结果发送至下一个环节
        /// </summary>
        /// <param name="strResultId">监测结果Id</param>
        /// <param name="strNextResultStatus">下一环节结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool SendResultToNext_QHD(string strResultId, string strNextResultStatus)
        {
            bool isSuccess = false;
            try
            {
                string strSql = @"select * from  T_MIS_MONITOR_RESULT
                                    where ID='{0}' and (ITEM_RESULT is null or ITEM_RESULT='')";
                strSql = string.Format(strSql, strResultId);
                DataTable dt = SqlHelper.ExecuteDataTable(strSql);
                if (dt.Rows.Count > 0)
                    return false;

                strSql = @"update T_MIS_MONITOR_RESULT
                                                   set RESULT_STATUS = '{1}' where ID='{0}'";
                strSql = string.Format(strSql, strResultId, strNextResultStatus);
                isSuccess = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
            }
            catch (Exception ex) { }
            return isSuccess;
        }
        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strTaskStatus">任务状态 分析环节：03</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strCurrResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool SendTaskCheckToNextFlow_QHD(string strTaskId, string strCurrentUserId, string strTaskStatus, string strFlowCode, string strSampleStatus, string strCurrResultStatus, string strNextResultStatus, bool b, string iSample)
        {
            ArrayList arrVo = new ArrayList();
            string strSql0 = "";
            string strSql1 = "";
            string strValue = getSampleItem();
            string strSql = @"update T_MIS_MONITOR_RESULT
                                               set T_MIS_MONITOR_RESULT.RESULT_STATUS = '{3}'
                                             where T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}' {7}
                                               and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                             (select *
                                                      from T_BASE_ITEM_INFO
                                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                       and {5}
                                                       and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                               and exists
                                             (select *
                                                      from T_MIS_MONITOR_SAMPLE_INFO
                                                     where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                       and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{4}'
                                                       and exists
                                                     (select *
                                                              from T_MIS_MONITOR_SUBTASK
                                                             where T_MIS_MONITOR_SUBTASK.ID =
                                                                   T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                               and T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({1})
                                                               and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}' {6}))";
            if (b)
            {
                if (iSample == "0")
                {
                    strSql0 = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strSampleStatus, "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'", " and T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002')", " and T_MIS_MONITOR_RESULT.ITEM_ID not in(" + strValue + ")");
                    strSql1 = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strSampleStatus, "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'", " and T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002')", "");
                    arrVo.Add(strSql0);
                    arrVo.Add(strSql1);
                }
                else if (iSample == "1")
                {
                    strSql0 = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strSampleStatus, "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'", " and T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002')", " and T_MIS_MONITOR_RESULT.ITEM_ID in(" + strValue + ")");
                    strSql1 = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strSampleStatus, "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'", " and T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002')", " and 1=2");
                    arrVo.Add(strSql0);
                    arrVo.Add(strSql1);
                }
                else
                {
                    strSql = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strSampleStatus, "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'", "", "");
                    arrVo.Add(strSql);
                }
            }
            else
            {
                if (iSample == "0")
                {
                    strSql0 = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strSampleStatus, "1=1", " and T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002')", " and T_MIS_MONITOR_RESULT.ITEM_ID not in(" + strValue + ")");
                    strSql1 = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strSampleStatus, "1=1", " and T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002')", "");
                    arrVo.Add(strSql0);
                    arrVo.Add(strSql1);
                }
                else if (iSample == "1")
                {
                    strSql0 = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strSampleStatus, "1=1", " and T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002')", " and T_MIS_MONITOR_RESULT.ITEM_ID in(" + strValue + ")");
                    strSql1 = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strSampleStatus, "1=1", " and T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002')", " and 1=2");
                    arrVo.Add(strSql0);
                    arrVo.Add(strSql1);
                }
                else
                {
                    strSql = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, strSampleStatus, "1=1", "", "");
                    arrVo.Add(strSql);
                }
            }

            return ExecuteSQLByTransaction(arrVo);
        }
        public bool SendTaskSampleCheckToNext_QHD(string strTaskID, string strCurrResultStatus, string strNextResultStatus)
        {
            string strSql = @"update c set c.RESULT_STATUS='{2}',c.TASK_TYPE='发送'
                              from T_MIS_MONITOR_SUBTASK a left join T_MIS_MONITOR_SAMPLE_INFO b on(a.ID=b.SUBTASK_ID)
                              left join T_MIS_MONITOR_RESULT c on(b.ID=c.SAMPLE_ID)
                              left join T_BASE_ITEM_INFO d on(c.ITEM_ID=d.ID)
                              where a.TASK_ID='{0}'  and c.RESULT_STATUS='{1}' and d.HAS_SUB_ITEM = '0' and d.IS_SAMPLEDEPT = '是'";
            strSql = string.Format(strSql, strTaskID, strCurrResultStatus, strNextResultStatus);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        /// <summary>
        /// 判断一个任务所有分析类项目是否已经全部发送到同一环节 Create By :weilin 2014-04-24
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public bool isAllSend(string strTaskID, string strStatus, bool b)
        {
            string strSQL = @"select 1 from T_MIS_MONITOR_SUBTASK a
                            left join T_MIS_MONITOR_SAMPLE_INFO b on(b.SUBTASK_ID=a.ID)
                            left join T_MIS_MONITOR_RESULT c on(c.SAMPLE_ID=b.ID)
                            left join T_BASE_ITEM_INFO d on(d.ID=c.ITEM_ID)
                            where a.TASK_ID='{0}' and c.RESULT_STATUS<>'{1}' and {2}";
            if (b)
                strSQL = string.Format(strSQL, strTaskID, strStatus, "d.IS_SAMPLEDEPT='否'");
            else
                strSQL = string.Format(strSQL, strTaskID, strStatus, "1=1");

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count > 0 ? false : true;
        }
        #endregion

        #region 分析结果录入方法
        /// <summary>
        /// 分析录入环节获取样品号
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getSimpleCodeInResult(string strCurrentUserId, string strResultStatus, string strSampleStatus)
        {
            string strSql = @"select distinct T_MIS_MONITOR_SAMPLE_INFO.*,T_MIS_MONITOR_TASK.TICKET_NUM
                                          from T_MIS_MONITOR_SAMPLE_INFO 
                                          left join T_MIS_MONITOR_SUBTASK 
                                          on T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID = T_MIS_MONITOR_SUBTASK.ID 
                                          left join T_MIS_MONITOR_TASK on
                                          T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
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
                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT.ID =
                                                               T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                           and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{0}')
                                                   and (T_MIS_MONITOR_RESULT.REMARK_4='1' or exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)))
                                         order by T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE";
            strSql = string.Format(strSql, strCurrentUserId, strResultStatus, strSampleStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取分析录入环节获取样品数量
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getSimpleCodeInResultCount(string strCurrentUserId, string strResultStatus, string strSampleStatus)
        {
            string strSql = @"select count(*)
                                          from (select ID, SAMPLE_CODE
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT_APP
                                                                 where T_MIS_MONITOR_RESULT.ID =
                                                                       T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{0}')
                                                           and (T_MIS_MONITOR_RESULT.REMARK_4='1' or exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID)))) record";
            strSql = string.Format(strSql, strCurrentUserId, strResultStatus, strSampleStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 分析录入环节获取样品号
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getSimpleCodeInResult_QHD(string strCurrentUserId, string strResultStatus, string strSampleStatus)
        {
            string strSql = @"select distinct T_MIS_MONITOR_SAMPLE_INFO.*,T_MIS_MONITOR_TASK.TICKET_NUM
                                          from T_MIS_MONITOR_SAMPLE_INFO 
                                          left join T_MIS_MONITOR_SUBTASK 
                                          on T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID = T_MIS_MONITOR_SUBTASK.ID 
                                          left join T_MIS_MONITOR_TASK on
                                          T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
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
                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT.ID =
                                                               T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                           and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{0}')
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                         order by T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE";
            strSql = string.Format(strSql, strCurrentUserId, strResultStatus, strSampleStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取分析录入环节获取样品数量
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getSimpleCodeInResultCount_QHD(string strCurrentUserId, string strResultStatus, string strSampleStatus)
        {
            string strSql = @"select count(*)
                                          from (select ID, SAMPLE_CODE
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT_APP
                                                                 where T_MIS_MONITOR_RESULT.ID =
                                                                       T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{0}')
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))) record";
            strSql = string.Format(strSql, strCurrentUserId, strResultStatus, strSampleStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 分析录入环节获取样品号(不受用户限制)
        /// </summary>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public DataTable getSimpleCodeInResultEx(string strResultStatus)
        {
            string strSql = @"select *
                                          from T_MIS_MONITOR_SAMPLE_INFO
                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
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
                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{0}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT.ID =
                                                               T_MIS_MONITOR_RESULT_APP.RESULT_ID)
                                                   and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)))
                                         order by T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE";
            strSql = string.Format(strSql, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取分析录入环节获取样品数量(不受用户限制)
        /// </summary>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getSimpleCodeInResultCountEx(string strResultStatus)
        {
            string strSql = @"select count(*)
                                          from (select ID, SAMPLE_CODE
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
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
                                                                 where T_MIS_MONITOR_RESULT.ID =
                                                                       T_MIS_MONITOR_RESULT_APP.RESULT_ID)
                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID)))) record";
            strSql = string.Format(strSql, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 获取分析结果录入环节监测项目信息
        /// </summary>
        /// <param name="strSimpleId">样品Id</param>
        /// <param name="strCurrentUserId">当前登陆用户</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getResultInResultFlow(string strSimpleId, string strCurrentUserId, string strResultStatus)
        {
            string strSql = @"select sum_record.*,
                                               APPARATUS_INFO.APPARATUS_CODE,
                                               APPARATUS_INFO.APPARATUS_NAME,
                                               APPARATUS_INFO.LOWER_CHECKOUT,
                                               (CASE
                                                 WHEN sum_record.ITEM_RESULT = '1' THEN
                                                  '完成'
                                                 ELSE
                                                  '未完成'
                                               END) as REMARK1
                                          from (select record.*,
                                                       T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                       T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                       T_MIS_MONITOR_RESULT_APP.ASKING_DATE
                                                  from (select T_MIS_MONITOR_RESULT.ID,
                                                               T_MIS_MONITOR_RESULT.ITEM_ID,
                                                               T_SYS_DICT.DICT_TEXT as UNIT,
                                                               ITEM_RESULT,
                                                               QC,
                                                               T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                               T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                               STANDARD_ID,
                                                               T_BASE_METHOD_INFO.METHOD_CODE,
                                                               APPARTUS_START_TIME,
                                                               APPARTUS_END_TIME,
                                                               APPARTUS_TIME_USED,
                                                               REMARK_1,
                                                               REMARK_2
                                                          from T_MIS_MONITOR_RESULT
                                                          left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                          left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                          left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                          left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                           and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT_APP
                                                                 where T_MIS_MONITOR_RESULT.ID =
                                                                       T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')
                                                           and (T_MIS_MONITOR_RESULT.REMARK_4='1' or exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))) record
                                                  left join T_MIS_MONITOR_RESULT_APP
                                                    on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                          left join (select (select APPARATUS_CODE
                                                               from T_BASE_APPARATUS_INFO
                                                              where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                            (select NAME
                                                               from T_BASE_APPARATUS_INFO
                                                              where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                            LOWER_CHECKOUT,
                                                            ITEM_ID,
                                                            ANALYSIS_METHOD_ID,ID
                                                       from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                            on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                           and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取分析结果录入环节监测项目信息
        /// </summary>
        /// <param name="strSimpleId">样品Id</param>
        /// <param name="strCurrentUserId">当前登陆用户</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getResultInResultFlow_QHD(string strSimpleId, string strCurrentUserId, string strResultStatus)
        {
            string strSql = @"select sum_record.*,
                                               APPARATUS_INFO.APPARATUS_CODE,
                                               APPARATUS_INFO.APPARATUS_NAME,
                                               APPARATUS_INFO.LOWER_CHECKOUT,
                                               (CASE
                                                 WHEN sum_record.ITEM_RESULT = '1' THEN
                                                  '完成'
                                                 ELSE
                                                  '未完成'
                                               END) as REMARK1
                                          from (select record.*,
                                                       T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                       T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                       T_MIS_MONITOR_RESULT_APP.ASKING_DATE
                                                  from (select T_MIS_MONITOR_RESULT.ID,
                                                               T_MIS_MONITOR_RESULT.ITEM_ID,
                                                               T_SYS_DICT.DICT_TEXT as UNIT,
                                                               ITEM_RESULT,
                                                               QC,
                                                               T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                               T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                               STANDARD_ID,
                                                               T_BASE_METHOD_INFO.METHOD_CODE,
                                                               APPARTUS_START_TIME,
                                                               APPARTUS_END_TIME,
                                                               APPARTUS_TIME_USED,
                                                               REMARK_1,
                                                               REMARK_2
                                                          from T_MIS_MONITOR_RESULT
                                                          left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                          left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                          left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                          left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                           and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT_APP
                                                                 where T_MIS_MONITOR_RESULT.ID =
                                                                       T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID)) record
                                                  left join T_MIS_MONITOR_RESULT_APP
                                                    on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                          left join (select (select APPARATUS_CODE
                                                               from T_BASE_APPARATUS_INFO
                                                              where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                            (select NAME
                                                               from T_BASE_APPARATUS_INFO
                                                              where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                            LOWER_CHECKOUT,
                                                            ITEM_ID,
                                                            ANALYSIS_METHOD_ID,ID
                                                       from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                            on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                           and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取分析结果录入环节监测项目信息记录数
        /// </summary>
        /// <param name="strSimpleId">样品Id</param>
        /// <param name="strCurrentUserId">当前登陆用户</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getResultInResultFlowCount(string strSimpleId, string strCurrentUserId, string strResultStatus)
        {
            string strSql = @"select count(*)
                                              from (select sum_record.*,
                                                           APPARATUS_INFO.APPARATUS_NAME,
                                                           APPARATUS_INFO.LOWER_CHECKOUT
                                                      from (select record.*,
                                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE
                                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                                           T_MIS_MONITOR_RESULT.ITEM_ID,
                                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                                           ITEM_RESULT,
                                                                           QC,
                                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                                           STANDARD_ID
                                                                      from T_MIS_MONITOR_RESULT
                                                                      left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                                      left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                                      left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                                      left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                                       and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                                                       and exists (select *
                                                                              from T_MIS_MONITOR_RESULT_APP
                                                                             where T_MIS_MONITOR_RESULT.ID =
                                                                                   T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                                               and T_MIS_MONITOR_RESULT_APP.HEAD_USERID =
                                                                                   '{1}')
                                                                       and exists
                                                                     (select *
                                                                              from T_BASE_ITEM_INFO
                                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                               and T_BASE_ITEM_INFO.ID =
                                                                                   T_MIS_MONITOR_RESULT.ITEM_ID)) record
                                                              left join T_MIS_MONITOR_RESULT_APP
                                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                                      left join (select (select NAME
                                                                          from T_BASE_APPARATUS_INFO
                                                                         where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                                       LOWER_CHECKOUT,
                                                                       ITEM_ID,
                                                                       ANALYSIS_METHOD_ID,ID
                                                                  from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                                       and APPARATUS_INFO.ID =
                                                           sum_record.ANALYSIS_METHOD_ID) sum";
            strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 获取分析结果录入环节监测项目信息(不受用户限制)
        /// </summary>
        /// <param name="strSimpleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getResultInResultFlowEx(string strSimpleId, string strResultStatus)
        {
            string strSql = @"select sum_record.*,
                                               APPARATUS_INFO.APPARATUS_CODE,
                                               APPARATUS_INFO.APPARATUS_NAME,
                                               APPARATUS_INFO.LOWER_CHECKOUT,
                                               (CASE
                                                 WHEN sum_record.ITEM_RESULT = '1' THEN
                                                  '完成'
                                                 ELSE
                                                  '未完成'
                                               END) as REMARK1
                                          from (select record.*,
                                                       T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                       T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                       T_MIS_MONITOR_RESULT_APP.ASKING_DATE
                                                  from (select T_MIS_MONITOR_RESULT.ID,
                                                               T_MIS_MONITOR_RESULT.ITEM_ID,
                                                               T_SYS_DICT.DICT_TEXT as UNIT,
                                                               ITEM_RESULT,
                                                               QC,
                                                               T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                               T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                               STANDARD_ID,
                                                               T_BASE_METHOD_INFO.METHOD_CODE,
                                                               APPARTUS_START_TIME,
                                                               APPARTUS_END_TIME,
                                                               APPARTUS_TIME_USED,
                                                               REMARK_1,
                                                               REMARK_2
                                                          from T_MIS_MONITOR_RESULT
                                                          left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                          left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                          left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                          left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                           and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                               T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT_APP
                                                                 where T_MIS_MONITOR_RESULT.ID =
                                                                       T_MIS_MONITOR_RESULT_APP.RESULT_ID)
                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))) record
                                                  left join T_MIS_MONITOR_RESULT_APP
                                                    on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                          left join (select (select APPARATUS_CODE
                                                               from T_BASE_APPARATUS_INFO
                                                              where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                            (select NAME
                                                               from T_BASE_APPARATUS_INFO
                                                              where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                            LOWER_CHECKOUT,
                                                            ITEM_ID,
                                                            ANALYSIS_METHOD_ID,ID
                                                       from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                            on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                           and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSimpleId, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 根据监测项目获取分析方法信息
        /// </summary>
        /// <param name="strItemId">监测项目ID</param>
        /// <returns></returns>
        public DataTable getAnalysisByItemId(string strItemId)
        {
            string strSql = @"select ANALYSIS_METHOD.*,T_BASE_METHOD_INFO.METHOD_CODE,
                                               T_BASE_APPARATUS_INFO.APPARATUS_CODE,
                                               T_BASE_APPARATUS_INFO.NAME as INSTRUMENT_NAME
                                          from (select T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID as ID,
                                                       T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                       T_BASE_ITEM_ANALYSIS.LOWER_CHECKOUT,
                                                       T_BASE_ITEM_ANALYSIS.INSTRUMENT_ID,
                                                       T_BASE_METHOD_ANALYSIS.METHOD_ID
                                                  from T_BASE_METHOD_ANALYSIS, T_BASE_ITEM_ANALYSIS
                                                 where T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID =
                                                       T_BASE_METHOD_ANALYSIS.ID
                                                   and T_BASE_METHOD_ANALYSIS.IS_DEL = '0'
                                                   and T_BASE_ITEM_ANALYSIS.ITEM_ID = '{0}') ANALYSIS_METHOD
                                          left join T_BASE_APPARATUS_INFO
                                            on ANALYSIS_METHOD.INSTRUMENT_ID = T_BASE_APPARATUS_INFO.ID
                                          left join T_BASE_METHOD_INFO ON(ANALYSIS_METHOD.METHOD_ID=T_BASE_METHOD_INFO.ID)";
            strSql = string.Format(strSql, strItemId);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取质控详细信息
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strQcType">质控类型 外控：0，内控1</param>
        /// <returns></returns>
        public DataTable getQcDetailInfo(string strResultId, string strQcType)
        {
            string strSql = "";
            DataTable dt = new DataTable();
            if (strQcType == "0")
            {
                strSql = @"select QC_RECORD.*, T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE
                                          from (select ID, SAMPLE_ID, ITEM_ID, QC_TYPE, ITEM_RESULT, '' as REMARK,'' as SCOPE,'' as IS_OK
                                                  from T_MIS_MONITOR_RESULT
                                                 where QC_SOURCE_ID = '{0}'
                                                   and (T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '4')) QC_RECORD
                                          left join T_MIS_MONITOR_SAMPLE_INFO
                                            on QC_RECORD.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID";
                strSql = string.Format(strSql, strResultId);
                dt = SqlHelper.ExecuteDataTable(strSql);
                string strNote = "";
                foreach (DataRow row in dt.Rows)
                {
                    string strSubResultId = row["ID"].ToString();
                    string strSubQcType = row["QC_TYPE"].ToString();
                    //现场空白
                    if (strSubQcType == "1")
                    {
                        strNote = "";
                        strSql = @"select *
                                          from T_MIS_MONITOR_QC_EMPTY_OUT where RESULT_ID_SRC='{0}' and RESULT_ID_EMPTY='{1}'";
                        strSql = string.Format(strSql, strSubResultId, strSubQcType);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                            strNote = "空白值:" + dtTemp.Rows[0]["RESULT_EMPTY"].ToString();
                        row["REMARK"] = strNote;
                        row["IS_OK"] = "1";
                        row["SCOPE"] = "";
                    }
                    //现场加标
                    if (strSubQcType == "2")
                    {
                        strNote = "";
                        strSql = "select * from T_MIS_MONITOR_QC_ADD where RESULT_ID_SRC='{0}' and RESULT_ID_ADD='{1}'";
                        strSql = string.Format(strSql, strResultId, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                        {
                            strNote = "加标测定值:" + dtTemp.Rows[0]["ADD_RESULT_EX"].ToString() + ";加标量:" + dtTemp.Rows[0]["QC_ADD"].ToString() + ";加标回收率:" + dtTemp.Rows[0]["ADD_BACK"].ToString() + "%";
                            row["IS_OK"] = dtTemp.Rows[0]["ADD_ISOK"].ToString();
                        }
                        row["REMARK"] = strNote;

                        //获取监测项目ID
                        string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                        //根据监测项目获取加标上限和加标下限
                        ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);
                        string strAddMax = TBaseItemInfoVo.ADD_MAX;
                        string strAddMin = TBaseItemInfoVo.ADD_MIN;

                        row["SCOPE"] = "大于等于" + strAddMin + "，小于等于" + strAddMax;
                    }
                    //现场平行,实验室密码平行
                    if (strSubQcType == "3" || strSubQcType == "4")
                    {
                        strNote = "";
                        strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and RESULT_ID_TWIN1='{1}'";
                        strSql = string.Format(strSql, strResultId, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                        {
                            strNote = "测定值" + dtTemp.Rows[0]["TWIN_RESULT1"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                            row["REMARK"] = strNote;

                            row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                            //获取监测项目ID
                            string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                            //根据监测项目获取加标上限和加标下限
                            string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                            row["SCOPE"] = "小于等于" + strTwinValue;
                        }
                        else
                        {
                            strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and RESULT_ID_TWIN2='{1}'";
                            strSql = string.Format(strSql, strResultId, strSubResultId);
                            dtTemp = SqlHelper.ExecuteDataTable(strSql);
                            if (dtTemp.Rows.Count > 0)
                            {
                                strNote = "测定值" + dtTemp.Rows[0]["TWIN_RESULT2"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                                row["REMARK"] = strNote;
                                row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                                //获取监测项目ID
                                string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                                //根据监测项目获取加标上限和加标下限
                                string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                                row["SCOPE"] = "小于等于" + strTwinValue;
                            }
                        }
                    }
                }
            }
            if (strQcType == "1")
            {
                string strItemId = "";
                string strNote = "";
                strSql = "select '' as ITEM_ID,'' as QC_TYPE,'' as ITEM_RESULT,'' as REMARK,'' as SCOPE,'' as IS_OK";
                dt = SqlHelper.ExecuteDataTable(strSql);
                dt.Rows.Clear();
                strSql = "select ITEM_ID from T_MIS_MONITOR_RESULT where ID='{0}'";
                strSql = string.Format(strSql, strResultId);
                DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                    strItemId = dtTemp.Rows[0]["ITEM_ID"].ToString();
                //实验室空白
                strNote = "";
                strSql = @"select *
                                          from T_MIS_MONITOR_QC_EMPTY_BAT
                                         where exists (select *
                                                  from T_MIS_MONITOR_RESULT
                                                 where id = '{0}'
                                                   and T_MIS_MONITOR_QC_EMPTY_BAT.ID =
                                                       T_MIS_MONITOR_RESULT.EMPTY_IN_BAT_ID)";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    strNote = "空白值:" + dtTemp.Rows[0]["QC_EMPTY_IN_RESULT"].ToString() + ";空白个数:" + dtTemp.Rows[0]["QC_EMPTY_IN_COUNT"].ToString();
                    DataRow row = dt.NewRow();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "5";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["QC_EMPTY_IN_RESULT"].ToString();
                    row["REMARK"] = strNote;
                    row["IS_OK"] = "1";
                    row["SCOPE"] = "";
                    dt.Rows.Add(row);
                }
                //实验室加标
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_ADD where RESULT_ID_SRC='{0}' and QC_TYPE='6'";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    strNote = "加标测定值:" + dtTemp.Rows[0]["ADD_RESULT_EX"].ToString() + ";加标量:" + dtTemp.Rows[0]["QC_ADD"].ToString() + ";加标回收率:" + dtTemp.Rows[0]["ADD_BACK"].ToString() + "%";
                    DataRow row = dt.NewRow();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "6";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["ADD_RESULT_EX"].ToString();
                    row["REMARK"] = strNote;

                    row["IS_OK"] = dtTemp.Rows[0]["ADD_ISOK"].ToString();

                    //根据监测项目获取加标上限和加标下限
                    ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);
                    string strAddMax = TBaseItemInfoVo.ADD_MAX;
                    string strAddMin = TBaseItemInfoVo.ADD_MIN;
                    row["SCOPE"] = "大于等于" + strAddMin + "，小于等于" + strAddMax;

                    dt.Rows.Add(row);
                }
                //实验室明码平行
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and QC_TYPE='7'";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    strNote = "测定值:" + dtTemp.Rows[0]["TWIN_RESULT1"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                    DataRow row = dt.NewRow();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "7";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["TWIN_RESULT1"].ToString();
                    row["REMARK"] = strNote;

                    row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                    //根据监测项目获取加标上限和加标下限
                    string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                    row["SCOPE"] = "小于等于" + strTwinValue;

                    dt.Rows.Add(row);
                }
                //标准样
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_ST  where RESULT_ID_SRC='{0}'";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    strNote = "标准值:" + dtTemp.Rows[0]["SRC_RESULT"].ToString() + ";测定值:" + dtTemp.Rows[0]["ST_RESULT"].ToString();
                    DataRow row = dt.NewRow();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "8";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["ST_RESULT"].ToString();
                    row["REMARK"] = strNote;
                    row["IS_OK"] = "1";
                    row["SCOPE"] = "";
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        /// <summary>
        /// 根据样品获取任务
        /// </summary>
        /// <param name="strSubtask"></param>
        /// <returns></returns>
        public DataTable getTaskID(string strSubtask)
        {
            string strSql = "";
            DataTable dt = new DataTable();
            strSql = @"select * from T_MIS_MONITOR_SUBTASK WHERE ID IN(
                            select SUBTASK_ID from T_MIS_MONITOR_SAMPLE_INFO where ID IN('{0}'))";
            strSql = string.Format(strSql, strSubtask);
            dt = SqlHelper.ExecuteDataTable(strSql);
            return dt;
        }
        public DataTable getItemId(string strSubtask)
        {
            string strSql = "";
            DataTable dt = new DataTable();
            strSql = @"select ITEM_ID from T_MIS_MONITOR_RESULT where ID IN('{0}')";
            strSql = string.Format(strSql, strSubtask);
            dt = SqlHelper.ExecuteDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取质控详细信息 
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strQcType">质控类型 外控：0，内控1</param>
        /// <returns></returns>
        public DataTable getQcDetailInfo_QY(string strResultId, string strQcType, string strSubTaskID, string strItemID)
        {
            string strSql = "";
            DataTable dt = new DataTable();
            if (strQcType == "0")
            {
                strSql = @"select QC_RECORD.*, T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE
                                          from (select ID, SAMPLE_ID, ITEM_ID, QC_TYPE, ITEM_RESULT, '' as REMARK,'' as SCOPE,'' as IS_OK
                                                  from T_MIS_MONITOR_RESULT
                                                 where QC_SOURCE_ID = '{0}'
                                                   and (T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '4')
                                                 union all
                                                 select a.ID, a.SAMPLE_ID, a.ITEM_ID, a.QC_TYPE, a.ITEM_RESULT, '' as REMARK,'' as SCOPE,'' as IS_OK 
                                                    from T_MIS_MONITOR_RESULT a left join T_MIS_MONITOR_SAMPLE_INFO b on(a.SAMPLE_ID=b.ID)
                                                    where a.QC_TYPE='11' and b.SUBTASK_ID='{1}' and a.ITEM_ID='{2}' 
                                           ) QC_RECORD
                                          left join T_MIS_MONITOR_SAMPLE_INFO
                                            on QC_RECORD.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID";
                strSql = string.Format(strSql, strResultId, strSubTaskID, strItemID);
                dt = SqlHelper.ExecuteDataTable(strSql);
                string strNote = "";
                foreach (DataRow row in dt.Rows)
                {
                    string strSubResultId = row["ID"].ToString();
                    string strSubQcType = row["QC_TYPE"].ToString();
                    //现场空白
                    if (strSubQcType == "1")
                    {
                        strNote = "";
                        strSql = @"select *
                                          from T_MIS_MONITOR_QC_EMPTY_OUT where RESULT_ID_EMPTY='{0}'";
                        strSql = string.Format(strSql, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                            strNote = "空白值:" + dtTemp.Rows[0]["RESULT_EMPTY"].ToString();
                        row["REMARK"] = strNote;
                        row["IS_OK"] = "1";
                        row["SCOPE"] = "";
                    }
                    //现场加标
                    if (strSubQcType == "2")
                    {
                        strNote = "";
                        strSql = "select * from T_MIS_MONITOR_QC_ADD where RESULT_ID_SRC='{0}' and RESULT_ID_ADD='{1}'";
                        strSql = string.Format(strSql, strResultId, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                        {
                            strNote = "加标测定值:" + dtTemp.Rows[0]["ADD_RESULT_EX"].ToString() + ";加标量:" + dtTemp.Rows[0]["QC_ADD"].ToString() + ";加标回收率:" + dtTemp.Rows[0]["ADD_BACK"].ToString() + "%";
                            row["IS_OK"] = dtTemp.Rows[0]["ADD_ISOK"].ToString();
                        }
                        row["REMARK"] = strNote;

                        //获取监测项目ID
                        string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                        //根据监测项目获取加标上限和加标下限
                        ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);
                        string strAddMax = TBaseItemInfoVo.ADD_MAX;
                        string strAddMin = TBaseItemInfoVo.ADD_MIN;

                        row["SCOPE"] = "大于等于" + strAddMin + "，小于等于" + strAddMax;
                    }
                    //现场平行,实验室密码平行
                    if (strSubQcType == "3" || strSubQcType == "4")
                    {
                        strNote = "";
                        strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and RESULT_ID_TWIN1='{1}'";
                        strSql = string.Format(strSql, strResultId, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                        {
                            strNote = "测定值" + dtTemp.Rows[0]["TWIN_RESULT1"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                            row["REMARK"] = strNote;

                            row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                            //获取监测项目ID
                            string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                            //根据监测项目获取加标上限和加标下限
                            string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                            row["SCOPE"] = "小于等于" + strTwinValue;
                        }
                        else
                        {
                            strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and RESULT_ID_TWIN2='{1}'";
                            strSql = string.Format(strSql, strResultId, strSubResultId);
                            dtTemp = SqlHelper.ExecuteDataTable(strSql);
                            if (dtTemp.Rows.Count > 0)
                            {
                                strNote = "测定值" + dtTemp.Rows[0]["TWIN_RESULT2"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                                row["REMARK"] = strNote;
                                row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                                //获取监测项目ID
                                string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                                //根据监测项目获取加标上限和加标下限
                                string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                                row["SCOPE"] = "小于等于" + strTwinValue;
                            }
                        }
                    }
                    //密码盲样
                    if (strSubQcType == "11")
                    {
                        strNote = "";
                        strSql = @"select *
                                          from T_MIS_MONITOR_QC_BLIND_ZZ where RESULT_ID='{0}' and QC_TYPE='11'";
                        strSql = string.Format(strSql, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                        {
                            strNote = "标准值:" + dtTemp.Rows[0]["STANDARD_VALUE"].ToString() + ";不确定值:" + dtTemp.Rows[0]["UNCETAINTY"].ToString();// + ";偏移量:" + dtTemp.Rows[0]["OFFSET"].ToString() + "%";
                            row["IS_OK"] = dtTemp.Rows[0]["BLIND_ISOK"].ToString();
                        }
                        row["REMARK"] = strNote;
                        row["SCOPE"] = "";
                    }
                }
            }
            if (strQcType == "1")
            {
                string strItemId = "";
                string strNote = "";
                strSql = "select '' as ID,'' as ITEM_ID,'' as QC_TYPE,'' as ITEM_RESULT,'' as REMARK,'' as SCOPE,'' as IS_OK";
                dt = SqlHelper.ExecuteDataTable(strSql);
                dt.Rows.Clear();
                strSql = "select ITEM_ID from T_MIS_MONITOR_RESULT where ID='{0}'";
                strSql = string.Format(strSql, strResultId);
                DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                    strItemId = dtTemp.Rows[0]["ITEM_ID"].ToString();
                //实验室空白
                strNote = "";
                strSql = @"select *
                                          from T_MIS_MONITOR_QC_EMPTY_BAT
                                         where exists (select *
                                                  from T_MIS_MONITOR_RESULT
                                                 where id = '{0}'
                                                   and T_MIS_MONITOR_QC_EMPTY_BAT.ID =
                                                       T_MIS_MONITOR_RESULT.EMPTY_IN_BAT_ID)";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    strNote = "空白值:" + dtTemp.Rows[0]["QC_EMPTY_IN_RESULT"].ToString() + ";空白个数:" + dtTemp.Rows[0]["QC_EMPTY_IN_COUNT"].ToString();
                    DataRow row = dt.NewRow();
                    row["ID"] = dtTemp.Rows[0]["ID"].ToString();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "5";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["QC_EMPTY_IN_RESULT"].ToString();
                    row["REMARK"] = strNote;
                    row["IS_OK"] = "1";
                    row["SCOPE"] = "";
                    dt.Rows.Add(row);
                }
                //实验室加标
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_ADD where RESULT_ID_SRC='{0}' and QC_TYPE='6'";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    strNote = "加标测定值:" + dtTemp.Rows[0]["ADD_RESULT_EX"].ToString() + ";加标量:" + dtTemp.Rows[0]["QC_ADD"].ToString() + ";加标回收率:" + dtTemp.Rows[0]["ADD_BACK"].ToString() + "%";
                    DataRow row = dt.NewRow();
                    row["ID"] = dtTemp.Rows[0]["ID"].ToString();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "6";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["ADD_RESULT_EX"].ToString();
                    row["REMARK"] = strNote;

                    row["IS_OK"] = dtTemp.Rows[0]["ADD_ISOK"].ToString();

                    //根据监测项目获取加标上限和加标下限
                    ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);
                    string strAddMax = TBaseItemInfoVo.ADD_MAX;
                    string strAddMin = TBaseItemInfoVo.ADD_MIN;
                    row["SCOPE"] = "大于等于" + strAddMin + "，小于等于" + strAddMax;

                    dt.Rows.Add(row);
                }
                //实验室明码平行
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and QC_TYPE='7'";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    strNote = "测定值1:" + dtTemp.Rows[0]["TWIN_RESULT1"].ToString() + ";测定值2:" + dtTemp.Rows[0]["TWIN_RESULT2"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                    DataRow row = dt.NewRow();
                    row["ID"] = dtTemp.Rows[0]["ID"].ToString();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "7";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["TWIN_AVG"].ToString();
                    row["REMARK"] = strNote;

                    row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                    //根据监测项目获取加标上限和加标下限
                    string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                    row["SCOPE"] = "小于等于" + strTwinValue;

                    dt.Rows.Add(row);
                }
                //标准样
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_ST  where RESULT_ID_SRC='{0}'";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    //strNote = "标准值:" + dtTemp.Rows[0]["SRC_RESULT"].ToString() + ";标准个数:" + dtTemp.Rows[0]["ST_RESULT"].ToString();
                    strNote = "";
                    if (dtTemp.Rows[0]["REMARK1"].ToString() != "" && dtTemp.Rows[0]["REMARK1"].ToString() != "0")
                        strNote += "标准值1：" + dtTemp.Rows[0]["REMARK1"].ToString() + ";";
                    if (dtTemp.Rows[0]["REMARK2"].ToString() != "" && dtTemp.Rows[0]["REMARK2"].ToString() != "0")
                        strNote += "标准值2：" + dtTemp.Rows[0]["REMARK2"].ToString() + ";";
                    if (dtTemp.Rows[0]["REMARK3"].ToString() != "" && dtTemp.Rows[0]["REMARK3"].ToString() != "0")
                        strNote += "标准值3：" + dtTemp.Rows[0]["REMARK3"].ToString() + ";";
                    DataRow row = dt.NewRow();
                    row["ID"] = dtTemp.Rows[0]["ID"].ToString();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "8";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["ST_RESULT"].ToString();
                    row["REMARK"] = strNote.TrimEnd(';');
                    row["IS_OK"] = "1";
                    row["SCOPE"] = "";
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取质控详细信息(由于在清远监测分析环节的实验室空白需求添加多个样，所以获取数据需要扩展)
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strQcType">质控类型 外控：0，内控1</param>
        /// <returns></returns>
        public DataTable getQcDetailInfoEx(string strResultId, string strQcType)
        {
            string strSql = "";
            DataTable dt = new DataTable();
            if (strQcType == "0")
            {
                strSql = @"select QC_RECORD.*, T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE
                                          from (select ID, SAMPLE_ID, ITEM_ID, QC_TYPE, ITEM_RESULT, '' as REMARK,'' as SCOPE,'' as IS_OK
                                                  from T_MIS_MONITOR_RESULT
                                                 where QC_SOURCE_ID = '{0}'
                                                   and (T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '4')) QC_RECORD
                                          left join T_MIS_MONITOR_SAMPLE_INFO
                                            on QC_RECORD.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID";
                strSql = string.Format(strSql, strResultId);
                dt = SqlHelper.ExecuteDataTable(strSql);
                string strNote = "";
                foreach (DataRow row in dt.Rows)
                {
                    string strSubResultId = row["ID"].ToString();
                    string strSubQcType = row["QC_TYPE"].ToString();
                    //现场空白
                    if (strSubQcType == "1")
                    {
                        strNote = "";
                        strSql = @"select *
                                          from T_MIS_MONITOR_QC_EMPTY_OUT where RESULT_ID_SRC='{0}' and RESULT_ID_EMPTY='{1}'";
                        strSql = string.Format(strSql, strSubResultId, strSubQcType);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                            strNote = "空白值:" + dtTemp.Rows[0]["RESULT_EMPTY"].ToString();
                        row["REMARK"] = strNote;
                        row["IS_OK"] = "1";
                        row["SCOPE"] = "";
                    }
                    //现场加标
                    if (strSubQcType == "2")
                    {
                        strNote = "";
                        strSql = "select * from T_MIS_MONITOR_QC_ADD where RESULT_ID_SRC='{0}' and RESULT_ID_ADD='{1}'";
                        strSql = string.Format(strSql, strResultId, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                            strNote = "加标测定值:" + dtTemp.Rows[0]["ADD_RESULT_EX"].ToString() + ";加标量:" + dtTemp.Rows[0]["QC_ADD"].ToString() + ";加标回收率:" + dtTemp.Rows[0]["ADD_BACK"].ToString() + "%";
                        row["REMARK"] = strNote;
                        row["IS_OK"] = dtTemp.Rows[0]["ADD_ISOK"].ToString();

                        //获取监测项目ID
                        string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                        //根据监测项目获取加标上限和加标下限
                        ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);
                        string strAddMax = TBaseItemInfoVo.ADD_MAX;
                        string strAddMin = TBaseItemInfoVo.ADD_MIN;

                        row["SCOPE"] = "大于等于" + strAddMin + "，小于等于" + strAddMax;
                    }
                    //现场平行,实验室密码平行
                    if (strSubQcType == "3" || strSubQcType == "4")
                    {
                        strNote = "";
                        strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and RESULT_ID_TWIN1='{1}'";
                        strSql = string.Format(strSql, strResultId, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                        {
                            strNote = "测定值" + dtTemp.Rows[0]["TWIN_RESULT1"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                            row["REMARK"] = strNote;

                            row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                            //获取监测项目ID
                            string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                            //根据监测项目获取加标上限和加标下限
                            string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                            row["SCOPE"] = "小于等于" + strTwinValue;
                        }
                        else
                        {
                            strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and RESULT_ID_TWIN2='{1}'";
                            strSql = string.Format(strSql, strResultId, strSubResultId);
                            dtTemp = SqlHelper.ExecuteDataTable(strSql);
                            if (dtTemp.Rows.Count > 0)
                            {
                                strNote = "测定值" + dtTemp.Rows[0]["TWIN_RESULT2"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                                row["REMARK"] = strNote;
                                row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                                //获取监测项目ID
                                string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                                //根据监测项目获取加标上限和加标下限
                                string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                                row["SCOPE"] = "小于等于" + strTwinValue;
                            }
                        }
                    }
                }
            }
            if (strQcType == "1")
            {
                string strItemId = "";
                string strNote = "";
                strSql = "select '' ID, '' as ITEM_ID,'' as QC_TYPE,'' as ITEM_RESULT,'' as REMARK,'' as SCOPE,'' as IS_OK";
                dt = SqlHelper.ExecuteDataTable(strSql);
                dt.Rows.Clear();
                strSql = "select ITEM_ID from T_MIS_MONITOR_RESULT where ID='{0}'";
                strSql = string.Format(strSql, strResultId);
                DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                    strItemId = dtTemp.Rows[0]["ITEM_ID"].ToString();
                //实验室空白
                strNote = "";
                strSql = @"select * from T_MIS_MONITOR_QC_EMPTY_BAT WHERE REMARK1='{0}'";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        strNote = "空白值:" + dtTemp.Rows[i]["QC_EMPTY_IN_RESULT"].ToString() + ";空白个数:" + dtTemp.Rows[i]["QC_EMPTY_IN_COUNT"].ToString();
                        DataRow row = dt.NewRow();
                        row["ID"] = dtTemp.Rows[i]["ID"].ToString();
                        row["ITEM_ID"] = strItemId;
                        row["QC_TYPE"] = "5";
                        row["ITEM_RESULT"] = dtTemp.Rows[i]["QC_EMPTY_IN_RESULT"].ToString();
                        row["REMARK"] = strNote;
                        row["IS_OK"] = "1";
                        row["SCOPE"] = "";
                        dt.Rows.Add(row);
                    }

                }
                //实验室加标
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_ADD where RESULT_ID_SRC='{0}' and QC_TYPE='6'";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    strNote = "加标测定值:" + dtTemp.Rows[0]["ADD_RESULT_EX"].ToString() + ";加标量:" + dtTemp.Rows[0]["QC_ADD"].ToString() + ";加标回收率:" + dtTemp.Rows[0]["ADD_BACK"].ToString() + "%";
                    DataRow row = dt.NewRow();
                    row["ID"] = dtTemp.Rows[0]["ID"].ToString();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "6";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["ADD_RESULT_EX"].ToString();
                    row["REMARK"] = strNote;

                    row["IS_OK"] = dtTemp.Rows[0]["ADD_ISOK"].ToString();

                    //根据监测项目获取加标上限和加标下限
                    ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);
                    string strAddMax = TBaseItemInfoVo.ADD_MAX;
                    string strAddMin = TBaseItemInfoVo.ADD_MIN;
                    row["SCOPE"] = "大于等于" + strAddMin + "，小于等于" + strAddMax;

                    dt.Rows.Add(row);
                }
                //实验室明码平行
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and QC_TYPE='7'";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    strNote = "测定值:" + dtTemp.Rows[0]["TWIN_RESULT1"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                    DataRow row = dt.NewRow();
                    row["ID"] = dtTemp.Rows[0]["ID"].ToString();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "7";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["TWIN_RESULT1"].ToString();
                    row["REMARK"] = strNote;

                    row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                    //根据监测项目获取加标上限和加标下限
                    string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                    row["SCOPE"] = "小于等于" + strTwinValue;

                    dt.Rows.Add(row);
                }
                //标准样
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_ST  where RESULT_ID_SRC='{0}'";
                strSql = string.Format(strSql, strResultId);
                dtTemp = SqlHelper.ExecuteDataTable(strSql);
                if (dtTemp.Rows.Count > 0)
                {
                    strNote = "标准值:" + dtTemp.Rows[0]["SRC_RESULT"].ToString() + ";测定值:" + dtTemp.Rows[0]["ST_RESULT"].ToString();
                    DataRow row = dt.NewRow();
                    row["ID"] = dtTemp.Rows[0]["ID"].ToString();
                    row["ITEM_ID"] = strItemId;
                    row["QC_TYPE"] = "8";
                    row["ITEM_RESULT"] = dtTemp.Rows[0]["ST_RESULT"].ToString();
                    row["REMARK"] = strNote;
                    row["IS_OK"] = "1";
                    row["SCOPE"] = "";
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        /// <summary>
        /// 删除实验室质控样 Create By：weilin 2013-12-13
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="strQcType">实验室质控类型</param>
        /// <returns></returns>
        public bool deleteQcAnalysis(string strID, string strQcType)
        {
            string strSql = string.Empty;
            switch (strQcType)
            {
                //实验室空白
                case "5":
                    strSql = "delete from T_MIS_MONITOR_QC_EMPTY_BAT where id='{0}'";
                    break;
                //实验室加标
                case "6":
                    strSql = "delete from T_MIS_MONITOR_QC_ADD where id='{0}'";
                    break;
                //实验室明码平行
                case "7":
                    strSql = "delete from T_MIS_MONITOR_QC_TWIN where id='{0}'";
                    break;
                //标准样
                case "8":
                    strSql = "delete from T_MIS_MONITOR_QC_ST where id='{0}'";
                    break;
                default:
                    break;
            }
            strSql = string.Format(strSql, strID);

            return SqlHelper.ExecuteNonQuery(strSql) > 0 ? true : false;
        }

        /// <summary>
        /// 将实验室质控数据保存至数据库
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="chkQcEmpty">实验室空白</param>
        /// <param name="strEmptyValue">空白值</param>
        /// <param name="strEmptyCount">空白个数</param>
        /// <param name="chkQcSt">标准样</param>
        /// <param name="strSrcResult">标准值</param>
        /// <param name="strStResult">测定值</param>
        /// <param name="chkQcAdd">实验室加标</param>
        /// <param name="strAddResultEx">测定值</param>
        /// <param name="strQcAdd">加标量</param>
        /// <param name="strAddBack">回收率</param>
        /// <param name="chkQcTwin">实验室明码平行</param>
        /// <param name="strTwinResult1">测定值1</param>
        /// <param name="strTwinResult2">测定值2</param>
        /// <param name="strTwinAvg">测定均值</param>
        /// <param name="strTwinOffSet">相对偏差</param>
        /// <returns></returns>
        public bool saveQcValue(string strResultId, string chkQcEmpty, string strEmptyValue, string strEmptyCount, string chkQcSt,
                                            string strSrcResult, string strStResult, string chkQcAdd, string strAddResultEx, string strQcAdd, string strAddBack,
                                            string chkQcTwin, string strTwinResult1, string strTwinResult2, string strTwinAvg, string strTwinOffSet)
        {
            ArrayList arrVo = new ArrayList();

            //获取空白批次ID
            string strEmptyBatId = new TMisMonitorResultAccess().Details(strResultId).EMPTY_IN_BAT_ID;
            //获取最初原始样ID
            string strSourceId = new TMisMonitorResultAccess().Details(strResultId).SOURCE_ID;
            //获取样品ID
            string strSimpleId = new TMisMonitorResultAccess().Details(strResultId).SAMPLE_ID;
            //获取监测项目ID
            string strItemId = new TMisMonitorResultAccess().Details(strResultId).ITEM_ID;
            //分析方法ID
            string strAnalysisMethodId = new TMisMonitorResultAccess().Details(strResultId).ANALYSIS_METHOD_ID;
            //方法依据ID
            string strStandId = new TMisMonitorResultAccess().Details(strResultId).STANDARD_ID;
            //质控类型
            string strQcType = "";

            string strSql = "";
            //实验室空白
            if (chkQcEmpty == "on")
            {
                //空白批次ID
                string strQcEmptyBatTemp = GetSerialNumber("QC_EMPTY_BAT");
                //将质控信息加到质控表中
                strSql = @"insert into T_MIS_MONITOR_QC_EMPTY_BAT(ID,QC_EMPTY_IN_DATE,QC_EMPTY_IN_COUNT,QC_EMPTY_IN_RESULT)
                                                                                                                          values('{0}','{1}','{2}','{3}')";
                strSql = string.Format(strSql, strQcEmptyBatTemp, DateTime.Now.ToString("yyyy-MM-dd"), strEmptyCount, strEmptyValue);
                arrVo.Add(strSql);
                strSql = "update T_MIS_MONITOR_RESULT set EMPTY_IN_BAT_ID='{0}' where ID='{1}'";
                strSql = string.Format(strSql, strQcEmptyBatTemp, strResultId);
                arrVo.Add(strSql);
                strQcType = "5";
            }
            else
            {
                strSql = "update T_MIS_MONITOR_RESULT set EMPTY_IN_BAT_ID='{0}' where ID='{1}'";
                strSql = string.Format(strSql, "", strResultId);
                arrVo.Add(strSql);
            }
            //标准样
            if (chkQcSt == "on")
            {
                string strQcStandId = GetSerialNumber("QcStandId");
                //删除标准样结果表中的数据
                strSql = @"delete from T_MIS_MONITOR_QC_ST where RESULT_ID_SRC = '{0}'";
                strSql = string.Format(strSql, strResultId);
                arrVo.Add(strSql);
                //将质控信息存储到标准样质控表中
                strSql = @"insert into T_MIS_MONITOR_QC_ST(ID,RESULT_ID_SRC,SRC_RESULT,ST_RESULT) values('{0}','{1}','{2}','{3}')";
                strSql = string.Format(strSql, strQcStandId, strResultId, strSrcResult, strStResult);
                arrVo.Add(strSql);
                if (strQcType != "")
                    strQcType = strQcType + ",8";
                else
                    strQcType = "8";
            }
            else
            {
                //删除标准样结果表中的数据
                strSql = @"delete from T_MIS_MONITOR_QC_ST  where RESULT_ID_SRC = '{0}'";
                strSql = string.Format(strSql, strResultId);
                arrVo.Add(strSql);
            }
            //实验室加标
            if (chkQcAdd == "on")
            {
                string strQcAddId = GetSerialNumber("QcAddId");
                strSql = @"delete from T_MIS_MONITOR_QC_ADD
                                             where RESULT_ID_SRC = '{0}' and QC_TYPE = '6'";
                strSql = string.Format(strSql, strResultId);
                arrVo.Add(strSql);

                //根据监测项目获取加标上限和加标下限
                ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);

                string strAddMax = TBaseItemInfoVo.ADD_MAX;
                string strAddMin = TBaseItemInfoVo.ADD_MIN;

                decimal dAddMin = 0;
                decimal dAddMax = 0;

                //加标回收率合格标志，默认不合格
                string isOk = "0";

                if (strAddMin.Trim() != "" && strAddMax.Trim() != "")
                {
                    bool isSuccess = decimal.TryParse(strAddMin, out dAddMin);
                    isSuccess = decimal.TryParse(strAddMax, out dAddMax);
                    //如果字符类型成功的话，判断加标回收率是否在允许的范围之内
                    if (isSuccess)
                    {
                        if (decimal.Parse(strAddBack) >= dAddMin && decimal.Parse(strAddBack) <= dAddMax)
                            isOk = "1";
                    }
                }

                strSql = @"insert into T_MIS_MONITOR_QC_ADD(ID,RESULT_ID_SRC,ADD_RESULT_EX,QC_ADD,ADD_BACK,QC_TYPE,ADD_ISOK) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                strSql = string.Format(strSql, strQcAddId, strResultId, strAddResultEx, strQcAdd, strAddBack, "6", isOk);
                arrVo.Add(strSql);

                if (strQcType != "")
                    strQcType = strQcType + ",6";
                else
                    strQcType = "6";
            }
            else
            {
                strSql = @"delete from T_MIS_MONITOR_QC_ADD
                                             where RESULT_ID_SRC = '{0}' and QC_TYPE = '6'";
                strSql = string.Format(strSql, strResultId);
                arrVo.Add(strSql);
            }
            //实验室明码平行
            if (chkQcTwin == "on")
            {
                string strQcTwinId = GetSerialNumber("QcTwinId");
                strSql = @"delete from T_MIS_MONITOR_QC_TWIN
                                             where RESULT_ID_SRC = '{0}' and QC_TYPE = '7'";
                strSql = string.Format(strSql, strResultId);
                arrVo.Add(strSql);

                //根据监测项目获取平行上限
                string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;

                decimal dTwinValue = 0;
                //相对偏差合格标志，默认不合格
                string isOk = "0";

                if (strTwinValue.Trim() != "")
                {
                    bool isSuccess = decimal.TryParse(strTwinValue, out dTwinValue);
                    //如果字符类型成功的话，判断相对偏差是否在允许的范围之内
                    if (isSuccess)
                    {
                        if (decimal.Parse(strTwinOffSet) <= dTwinValue)
                            isOk = "1";
                    }
                }


                strSql = @"insert into T_MIS_MONITOR_QC_TWIN
                                                  (ID,
                                                   RESULT_ID_SRC,
                                                   TWIN_RESULT1,
                                                   TWIN_RESULT2,
                                                   TWIN_AVG,
                                                   TWIN_OFFSET,QC_TYPE,TWIN_ISOK)
                                                values
                                                  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}','{7}')";
                strSql = string.Format(strSql, strQcTwinId, strResultId, strTwinResult1, strTwinResult2, strTwinAvg, strTwinOffSet, "7", isOk);
                arrVo.Add(strSql);

                if (strQcType != "")
                    strQcType = strQcType + ",7";
                else
                    strQcType = "7";
            }
            else
            {
                strSql = @"delete from T_MIS_MONITOR_QC_TWIN
                                             where RESULT_ID_SRC = '{0}' and QC_TYPE = '7'";
                strSql = string.Format(strSql, strResultId);
                arrVo.Add(strSql);
            }
            strSql = "update T_MIS_MONITOR_RESULT set QC='{0}' where id='{1}'";
            strSql = string.Format(strSql, strQcType, strResultId);
            arrVo.Add(strSql);

            return ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 将实验室质控数据保存至数据库 Create By：weilin 2013-12-13
        /// </summary>
        /// <param name="strResultIds">结果ID(用于空白样和标准样)</param>
        /// <param name="chkQcEmpty">实验室空白</param>
        /// <param name="strEmptyValue">空白值</param>
        /// <param name="strEmptyCount">空白个数</param>
        /// <param name="chkQcSt">标准样</param>
        /// <param name="strSrcResult">标准值</param>
        /// <param name="strStResult">测定值</param>
        /// <param name="strAddResultId">加标样的结果ID</param>
        /// <param name="chkQcAdd">实验室加标</param>
        /// <param name="strAddResultEx">测定值</param>
        /// <param name="strQcAdd">加标量</param>
        /// <param name="strAddBack">回收率</param>
        /// <param name="strTwinResultId">明码平行的结果ID</param>
        /// <param name="chkQcTwin">实验室明码平行</param>
        /// <param name="strTwinResult1">测定值1</param>
        /// <param name="strTwinResult2">测定值2</param>
        /// <param name="strTwinAvg">测定均值</param>
        /// <param name="strTwinOffSet">相对偏差</param>
        /// <returns></returns>
        public bool saveQcValueEx(string strResultIds, string chkQcEmpty, string strEmptyValue1, string strEmptyValue2, string strEmptyValue3, string strEmptyValue, string strEmptyCount, string chkQcSt,
                                            string strSrcResult, string strStResult, string strSRC_IN_VALUE1, string strSRC_IN_VALUE2, string strSRC_IN_VALUE3, string strAddResultId, string chkQcAdd, string strAddResultEx, string strQcAdd, string strAddBack,
                                            string strTwinResultId, string chkQcTwin, string strTwinResult1, string strTwinResult2, string strTwinAvg, string strTwinOffSet)
        {
            ArrayList arrVo = new ArrayList();

            ////获取空白批次ID
            //string strEmptyBatId = new TMisMonitorResultAccess().Details(strResultId).EMPTY_IN_BAT_ID;
            ////获取最初原始样ID
            //string strSourceId = new TMisMonitorResultAccess().Details(strResultId).SOURCE_ID;
            ////获取样品ID
            //string strSimpleId = new TMisMonitorResultAccess().Details(strResultId).SAMPLE_ID;
            ////获取监测项目ID
            //string strItemId = new TMisMonitorResultAccess().Details(strResultId).ITEM_ID;
            ////分析方法ID
            //string strAnalysisMethodId = new TMisMonitorResultAccess().Details(strResultId).ANALYSIS_METHOD_ID;
            ////方法依据ID
            //string strStandId = new TMisMonitorResultAccess().Details(strResultId).STANDARD_ID;
            //质控类型
            string strQcType1 = "";
            string strQcType2 = "";
            string strQcType3 = "";

            string strSql = "";
            string[] ArrResultId = strResultIds.Split(',');
            //实验室空白
            if (chkQcEmpty == "on")
            {
                for (int i = 0; i < ArrResultId.Length; i++)
                {
                    //空白批次ID
                    string strQcEmptyBatTemp = GetSerialNumber("QC_EMPTY_BAT");
                    //将质控信息加到质控表中
                    strSql = @"insert into T_MIS_MONITOR_QC_EMPTY_BAT(ID,QC_EMPTY_IN_DATE,QC_EMPTY_IN_COUNT,QC_EMPTY_IN_RESULT, REMARK1, REMARK2, REMARK3)
                                                                                                                          values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                    strSql = string.Format(strSql, strQcEmptyBatTemp, DateTime.Now.ToString("yyyy-MM-dd"), strEmptyCount, strEmptyValue, strEmptyValue1, strEmptyValue2, strEmptyValue3);
                    arrVo.Add(strSql);
                    strSql = "update T_MIS_MONITOR_RESULT set EMPTY_IN_BAT_ID='{0}' where ID='{1}'";
                    strSql = string.Format(strSql, strQcEmptyBatTemp, ArrResultId[i].ToString());
                    arrVo.Add(strSql);
                }

                strQcType1 = "5";
            }
            else
            {
                strSql = "update T_MIS_MONITOR_RESULT set EMPTY_IN_BAT_ID='{0}' where ID in({1})";
                strSql = string.Format(strSql, "", strResultIds);
                arrVo.Add(strSql);
            }
            //标准样
            if (chkQcSt == "on")
            {
                for (int i = 0; i < ArrResultId.Length; i++)
                {
                    string strQcStandId = GetSerialNumber("QcStandId");
                    //删除标准样结果表中的数据
                    strSql = @"delete from T_MIS_MONITOR_QC_ST where RESULT_ID_SRC = '{0}'";
                    strSql = string.Format(strSql, ArrResultId[i].ToString());
                    arrVo.Add(strSql);
                    //将质控信息存储到标准样质控表中
                    strSql = @"insert into T_MIS_MONITOR_QC_ST(ID,RESULT_ID_SRC,SRC_RESULT,ST_RESULT,REMARK1,REMARK2,REMARK3) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                    strSql = string.Format(strSql, strQcStandId, ArrResultId[i].ToString(), strSrcResult, strStResult, strSRC_IN_VALUE1, strSRC_IN_VALUE2, strSRC_IN_VALUE3);
                    arrVo.Add(strSql);
                }

                if (strQcType1 != "")
                    strQcType1 = strQcType1 + ",8";
                else
                    strQcType1 = "8";
            }
            else
            {
                //删除标准样结果表中的数据
                strSql = @"delete from T_MIS_MONITOR_QC_ST  where RESULT_ID_SRC in ({0})";
                strSql = string.Format(strSql, strResultIds);
                arrVo.Add(strSql);
            }
            //实验室加标
            if (chkQcAdd == "on")
            {
                string strQcAddId = GetSerialNumber("QcAddId");
                strSql = @"delete from T_MIS_MONITOR_QC_ADD
                                             where RESULT_ID_SRC = '{0}' and QC_TYPE = '6'";
                strSql = string.Format(strSql, strAddResultId);
                arrVo.Add(strSql);

                //获取监测项目ID
                string strItemId = new TMisMonitorResultAccess().Details(strAddResultId).ITEM_ID;
                //根据监测项目获取加标上限和加标下限
                ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);

                string strAddMax = TBaseItemInfoVo.ADD_MAX;
                string strAddMin = TBaseItemInfoVo.ADD_MIN;

                decimal dAddMin = 0;
                decimal dAddMax = 0;

                //加标回收率合格标志，默认不合格
                string isOk = "0";

                if (strAddMin.Trim() != "" && strAddMax.Trim() != "")
                {
                    bool isSuccess = decimal.TryParse(strAddMin, out dAddMin);
                    isSuccess = decimal.TryParse(strAddMax, out dAddMax);
                    //如果字符类型成功的话，判断加标回收率是否在允许的范围之内
                    if (isSuccess)
                    {
                        if (decimal.Parse(strAddBack) >= dAddMin && decimal.Parse(strAddBack) <= dAddMax)
                            isOk = "1";
                    }
                }

                strSql = @"insert into T_MIS_MONITOR_QC_ADD(ID,RESULT_ID_SRC,ADD_RESULT_EX,QC_ADD,ADD_BACK,QC_TYPE,ADD_ISOK) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                strSql = string.Format(strSql, strQcAddId, strAddResultId, strAddResultEx, strQcAdd, strAddBack, "6", isOk);
                arrVo.Add(strSql);

                if (strQcType1 != "")
                    strQcType2 = strQcType1 + ",6";
                else
                    strQcType2 = "6";
            }
            else
            {
                strSql = @"delete from T_MIS_MONITOR_QC_ADD
                                             where RESULT_ID_SRC = '{0}' and QC_TYPE = '6'";
                strSql = string.Format(strSql, strAddResultId);
                arrVo.Add(strSql);
            }
            //实验室明码平行
            if (chkQcTwin == "on")
            {
                string strQcTwinId = GetSerialNumber("QcTwinId");
                strSql = @"delete from T_MIS_MONITOR_QC_TWIN
                                             where RESULT_ID_SRC = '{0}' and QC_TYPE = '7'";
                strSql = string.Format(strSql, strTwinResultId);
                arrVo.Add(strSql);

                //获取监测项目ID
                string strItemId = new TMisMonitorResultAccess().Details(strAddResultId).ITEM_ID;
                //根据监测项目获取平行上限
                string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;

                decimal dTwinValue = 0;
                //相对偏差合格标志，默认不合格
                string isOk = "0";

                if (strTwinValue.Trim() != "")
                {
                    bool isSuccess = decimal.TryParse(strTwinValue, out dTwinValue);
                    //如果字符类型成功的话，判断相对偏差是否在允许的范围之内
                    if (isSuccess)
                    {
                        if (decimal.Parse(strTwinOffSet) <= dTwinValue)
                            isOk = "1";
                    }
                }


                strSql = @"insert into T_MIS_MONITOR_QC_TWIN
                                                  (ID,
                                                   RESULT_ID_SRC,
                                                   TWIN_RESULT1,
                                                   TWIN_RESULT2,
                                                   TWIN_AVG,
                                                   TWIN_OFFSET,QC_TYPE,TWIN_ISOK)
                                                values
                                                  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}','{7}')";
                strSql = string.Format(strSql, strQcTwinId, strTwinResultId, strTwinResult1, strTwinResult2, strTwinAvg, strTwinOffSet, "7", isOk);
                arrVo.Add(strSql);

                strSql = "update T_MIS_MONITOR_RESULT set ITEM_RESULT='{0}' where ID='{1}'";
                strSql = string.Format(strSql, strTwinAvg, strTwinResultId);
                arrVo.Add(strSql);

                if (strQcType1 != "")
                    strQcType3 = strQcType1 + ",7";
                else
                    strQcType3 = "7";
            }
            else
            {
                strSql = @"delete from T_MIS_MONITOR_QC_TWIN
                                             where RESULT_ID_SRC = '{0}' and QC_TYPE = '7'";
                strSql = string.Format(strSql, strTwinResultId);
                arrVo.Add(strSql);
            }
            strSql = "update T_MIS_MONITOR_RESULT set QC='{0}' where id in({1})";
            strSql = string.Format(strSql, strQcType1, strResultIds);
            arrVo.Add(strSql);

            if (strAddResultId != "")
            {
                strSql = "update T_MIS_MONITOR_RESULT set QC='{0}' where id in({1})";
                strSql = string.Format(strSql, strQcType2, strAddResultId);
                arrVo.Add(strSql);
            }
            if (strTwinResultId != "")
            {
                strSql = "update T_MIS_MONITOR_RESULT set QC='{0}' where id in({1})";
                strSql = string.Format(strSql, strQcType3, strTwinResultId);
                arrVo.Add(strSql);
            }

            return ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 将分析数据录入发送至下一个环节
        /// </summary>
        /// <param name="strSimpleId">样品id</param>
        /// <param name="strCurrentUserId">当前登录用户的ID</param>
        /// <param name="strCurrResultStatus">当前结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">当前结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool ResultSendToNext(string strSimpleId, string strCurrentUserId, string strCurrResultStatus, string strNextResultStatus)
        {
            bool isSuccess = false;
            try
            {
                string strSql = @"select *
                                              from T_MIS_MONITOR_RESULT
                                             where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                               and (T_MIS_MONITOR_RESULT.ITEM_RESULT is null or
                                                   T_MIS_MONITOR_RESULT.ITEM_RESULT = '')
                                               and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                               and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                               and exists
                                             (select *
                                                      from T_MIS_MONITOR_RESULT_APP
                                                     where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                       and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')
                                               and exists
                                             (select *
                                                      from T_BASE_ITEM_INFO
                                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                       and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                       and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)";
                strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strCurrResultStatus);

                DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
                if (objTable.Rows.Count > 0) return false;

                ReStatisticsQc(strSimpleId, strCurrentUserId, strCurrResultStatus);

                strSql = @"update T_MIS_MONITOR_RESULT
                                       set RESULT_STATUS = '{3}', TASK_TYPE = '发送'
                                     where SAMPLE_ID = '{0}'
                                       and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_RESULT_APP
                                             where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                               and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')
                                       and exists
                                     (select *
                                              from T_BASE_ITEM_INFO
                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)";
                strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strCurrResultStatus, strNextResultStatus);
                isSuccess = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
            }
            catch (Exception ex) { }
            return isSuccess;
        }

        /// <summary>
        /// 将分析数据录入发送至下一个环节
        /// </summary>
        /// <param name="strSimpleId">样品id</param>
        /// <param name="strCurrentUserId">当前登录用户的ID</param>
        /// <param name="strCurrResultStatus">当前结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">当前结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public string ResultSendToNext_QHD(string strSimpleIds, string strCurrentUserId, string strCurrResultStatus, string strNextResultStatus)
        {
            bool isSuccess = false;
            string strMsg = "";
            string SampleIDs = strSimpleIds.Replace(",", "','");
            string[] objSampleID = strSimpleIds.Split(',');
            try
            {
                string strSql = @"select T_MIS_MONITOR_RESULT.ID,T_BASE_ITEM_INFO.ITEM_NAME,T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE
                                              from T_MIS_MONITOR_RESULT
                                              left join T_MIS_MONITOR_SAMPLE_INFO on(T_MIS_MONITOR_RESULT.SAMPLE_ID=T_MIS_MONITOR_SAMPLE_INFO.ID)
                                              left join T_BASE_ITEM_INFO on(T_MIS_MONITOR_RESULT.ITEM_ID=T_BASE_ITEM_INFO.ID)
                                             where T_MIS_MONITOR_RESULT.SAMPLE_ID in ('{0}')
                                               and (T_MIS_MONITOR_RESULT.ITEM_RESULT is null or
                                                   T_MIS_MONITOR_RESULT.ITEM_RESULT = '')
                                               and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                               and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                               and exists
                                             (select *
                                                      from T_MIS_MONITOR_RESULT_APP
                                                     where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                       and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')";
                strSql = string.Format(strSql, SampleIDs, strCurrentUserId, strCurrResultStatus);

                DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
                if (objTable.Rows.Count > 0)
                {
                    strMsg = "样品【" + objTable.Rows[0]["SAMPLE_CODE"].ToString() + "】中的【" + objTable.Rows[0]["ITEM_NAME"].ToString() + "】还没完成，请检查";
                    return strMsg;
                }

                for (int i = 0; i < objSampleID.Length; i++)
                {
                    ReStatisticsQc(objSampleID[i].ToString(), strCurrentUserId, strCurrResultStatus);
                }

                strSql = @"update T_MIS_MONITOR_RESULT
                                       set RESULT_STATUS = '{3}', TASK_TYPE = '发送'
                                     where SAMPLE_ID in ('{0}')
                                       and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_RESULT_APP
                                             where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                               and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')
                                       and exists
                                     (select *
                                              from T_BASE_ITEM_INFO
                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                               --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)";
                strSql = string.Format(strSql, SampleIDs, strCurrentUserId, strCurrResultStatus, strNextResultStatus);
                isSuccess = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
            }
            catch (Exception ex) { }
            if (isSuccess)
                strMsg = "";
            else
                strMsg = "发送失败，请联系管理员";
            return strMsg;
        }

        /// <summary>
        /// 质控信息重新统计
        /// </summary>
        /// <param name="strSimpleId">样品ID</param>
        /// <param name="strCurrentUserId">当前用户ＩＤ</param>
        /// <param name="strResultStatus">结果状态</param>
        public void ReStatisticsQc(string strSimpleId, string strCurrentUserId, string strResultStatus)
        {
            //现场加标
            ReStatisticsQcAdd(strSimpleId, strCurrentUserId, "2", strResultStatus);
            //现场平行
            ReStatisticsQcTwin(strSimpleId, strCurrentUserId, "3", strResultStatus);
            //实验室密码平行
            ReStatisticsQcTwin(strSimpleId, strCurrentUserId, "4", strResultStatus);
            //实验室加标
            ReStatisticsQcAdd(strSimpleId, strCurrentUserId, "6", strResultStatus);
            //实验室明码平行
            ReStatisticsQcTwin(strSimpleId, strCurrentUserId, "7", strResultStatus);
            //标准盲样计算
            ReStatisticsQcBlind(strSimpleId, strCurrentUserId, "11", strResultStatus);
        }
        /// <summary>
        /// 质控信息重新统计 
        /// </summary>
        /// <param name="strSimpleId">样品ID</param>
        /// <param name="strCurrentUserId">当前用户ＩＤ</param>
        /// <param name="strResultStatus">结果状态</param>
        public void ReStatisticsQc_QY(string strSimpleId, string strCurrentUserId, string strResultStatus)
        {
            //现场加标
            ReStatisticsQcAdd_QY(strSimpleId, strCurrentUserId, "2", strResultStatus);
            //现场平行
            ReStatisticsQcTwin_QY(strSimpleId, strCurrentUserId, "3", strResultStatus);
            //实验室密码平行
            ReStatisticsQcTwin(strSimpleId, strCurrentUserId, "4", strResultStatus);
            //实验室加标
            ReStatisticsQcAdd_QY(strSimpleId, strCurrentUserId, "6", strResultStatus);
            //实验室明码平行
            ReStatisticsQcTwin_QY(strSimpleId, strCurrentUserId, "7", strResultStatus);
            //现场密码计算
            ReStatisticsQcBlind_QY(strSimpleId, strCurrentUserId, "11", strResultStatus);
        }
        /// <summary>
        /// 重新统计加标
        /// </summary>
        /// <param name="strSimpleId">样品ID</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strQcType">质控类型 现场加标：2；实验室加标：6</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public bool ReStatisticsQcAdd(string strSimpleId, string strCurrentUserId, string strQcType, string strResultStatus)
        {
            ArrayList arrVo = new ArrayList();
            //计算现场加标的回收率，现场平行的均值与相对偏差
            string strSql = @"select *
                                          from T_MIS_MONITOR_RESULT RESULT1
                                         where QC_TYPE = '0'
                                           and RESULT_STATUS = '{3}'
                                           and SAMPLE_ID = '{0}'
                                           and exists (select *
                                                  from T_MIS_MONITOR_RESULT RESULT2
                                                 where RESULT2.QC_SOURCE_ID = RESULT1.ID
                                                   and RESULT2.QC_TYPE = '{2}')
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT_APP
                                                 where RESULT1.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')";
            strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strQcType, strResultStatus);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow rows in dt.Rows)
            {
                //原始结果ID
                string strRootResultId = rows["ID"].ToString();
                //原始测定值
                string strRootResult = GetNumber(rows["ITEM_RESULT"].ToString()).ToString();
                //计算加标样
                strSql = "select * from T_MIS_MONITOR_RESULT where QC_SOURCE_ID='{0}' and QC_TYPE='{1}'";
                strSql = string.Format(strSql, strRootResultId, strQcType);
                DataTable addTableResult = SqlHelper.ExecuteDataTable(strSql);
                foreach (DataRow addRow in addTableResult.Rows)
                {
                    //加标样结果ID
                    string strResultId = addRow["ID"].ToString();
                    //加标样测定值
                    string strResultValue = GetNumber(addRow["ITEM_RESULT"].ToString()).ToString();
                    //加标量
                    string strQcAdd = "0";
                    strSql = "select QC_ADD from T_MIS_MONITOR_QC_ADD where RESULT_ID_SRC='{0}' and RESULT_ID_ADD='{1}' and QC_TYPE='{2}'";
                    strSql = string.Format(strSql, strRootResultId, strResultId, strQcType);
                    DataTable addTable = SqlHelper.ExecuteDataTable(strSql);
                    if (addTable.Rows.Count > 0)
                    {
                        if (addTable.Rows[0]["QC_ADD"] != null && addTable.Rows[0]["QC_ADD"].ToString() != "")
                            strQcAdd = addTable.Rows[0]["QC_ADD"].ToString();
                    }
                    //计算回收率
                    decimal AddBack = 0;
                    if (strQcAdd != "0")
                        AddBack = Math.Abs(decimal.Parse(strResultValue) - decimal.Parse(strRootResult)) / decimal.Parse(strQcAdd) * 100;
                    string strAddBack = Math.Round(AddBack, 1).ToString();

                    //获取监测项目ID
                    string strItemId = new TMisMonitorResultAccess().Details(strResultId).ITEM_ID;
                    //根据监测项目获取加标上限和加标下限
                    ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);

                    string strAddMax = TBaseItemInfoVo.ADD_MAX;
                    string strAddMin = TBaseItemInfoVo.ADD_MIN;

                    decimal dAddMin = 0;
                    decimal dAddMax = 0;

                    //加标回收率合格标志，默认不合格
                    string isOk = "0";

                    if (strAddMin.Trim() != "" && strAddMax.Trim() != "")
                    {
                        bool isSuccess = decimal.TryParse(strAddMin, out dAddMin);
                        isSuccess = decimal.TryParse(strAddMax, out dAddMax);
                        //如果字符类型成功的话，判断加标回收率是否在允许的范围之内
                        if (isSuccess)
                        {
                            if (Math.Round(AddBack, 1) >= dAddMin && Math.Round(AddBack, 1) <= dAddMax)
                                isOk = "1";
                        }
                    }
                    //将数据更新至加标结果表中
                    strSql = @"update T_MIS_MONITOR_QC_ADD
                                                   set SRC_RESULT = '{2}', ADD_RESULT_EX = '{3}', ADD_BACK = '{4}',ADD_ISOK='{6}'
                                                 where RESULT_ID_SRC = '{0}'
                                                   and RESULT_ID_ADD = '{1}' and QC_TYPE='{5}'";
                    strSql = string.Format(strSql, strRootResultId, strResultId, strRootResult, strResultValue, strAddBack, strQcType, isOk);
                    arrVo.Add(strSql);
                }
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 重新统计加标 Create By :weilin 2014-06-24
        /// </summary>
        /// <param name="strSimpleId">样品ID</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strQcType">质控类型 现场加标：2；实验室加标：6</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public bool ReStatisticsQcAdd_QY(string strSimpleId, string strCurrentUserId, string strQcType, string strResultStatus)
        {
            ArrayList arrVo = new ArrayList();
            //计算现场加标的回收率
            string strSql = @"select *
                                          from T_MIS_MONITOR_RESULT RESULT1
                                         where QC_TYPE = '{2}'
                                           and RESULT_STATUS = '{3}'
                                           and SAMPLE_ID = '{0}'
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT_APP
                                                 where RESULT1.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')";
            strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strQcType, strResultStatus);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow rows in dt.Rows)
            {
                //加标样结果ID
                string strResultId = rows["ID"].ToString();
                //加标样测定值
                string strResult = GetNumber(rows["ITEM_RESULT"].ToString()).ToString();
                //计算加标样
                strSql = "select * from T_MIS_MONITOR_RESULT where ID='{0}' and QC_TYPE='0'";   //获取原样结果信息
                strSql = string.Format(strSql, rows["QC_SOURCE_ID"].ToString());
                DataTable addTableResult = SqlHelper.ExecuteDataTable(strSql);
                foreach (DataRow addRow in addTableResult.Rows)
                {
                    //原样结果ID
                    string strRootResultId = addRow["ID"].ToString();
                    //原样测定值
                    string strRootResult = GetNumber(addRow["ITEM_RESULT"].ToString()).ToString();
                    //加标量
                    string strQcAdd = "0";
                    strSql = "select QC_ADD from T_MIS_MONITOR_QC_ADD where RESULT_ID_SRC='{0}' and RESULT_ID_ADD='{1}' and QC_TYPE='{2}'";
                    strSql = string.Format(strSql, strRootResultId, strResultId, strQcType);
                    DataTable addTable = SqlHelper.ExecuteDataTable(strSql);
                    if (addTable.Rows.Count > 0)
                    {
                        if (addTable.Rows[0]["QC_ADD"] != null && addTable.Rows[0]["QC_ADD"].ToString() != "")
                            strQcAdd = addTable.Rows[0]["QC_ADD"].ToString();
                    }
                    //计算回收率
                    decimal AddBack = 0;
                    if (strQcAdd != "0")
                        AddBack = Math.Abs(decimal.Parse(strResult) - decimal.Parse(strRootResult)) / decimal.Parse(strQcAdd) * 100;
                    string strAddBack = Math.Round(AddBack, 1).ToString();

                    //获取监测项目ID
                    string strItemId = new TMisMonitorResultAccess().Details(strResultId).ITEM_ID;
                    //根据监测项目获取加标上限和加标下限
                    ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);

                    string strAddMax = TBaseItemInfoVo.ADD_MAX;
                    string strAddMin = TBaseItemInfoVo.ADD_MIN;

                    decimal dAddMin = 0;
                    decimal dAddMax = 0;

                    //加标回收率合格标志，默认不合格
                    string isOk = "0";

                    if (strAddMin.Trim() != "" && strAddMax.Trim() != "")
                    {
                        bool isSuccess = decimal.TryParse(strAddMin, out dAddMin);
                        isSuccess = decimal.TryParse(strAddMax, out dAddMax);
                        //如果字符类型成功的话，判断加标回收率是否在允许的范围之内
                        if (isSuccess)
                        {
                            if (Math.Round(AddBack, 1) >= dAddMin && Math.Round(AddBack, 1) <= dAddMax)
                                isOk = "1";
                        }
                    }
                    //将数据更新至加标结果表中
                    strSql = @"update T_MIS_MONITOR_QC_ADD
                                                   set SRC_RESULT = '{2}', ADD_RESULT_EX = '{3}', ADD_BACK = '{4}',ADD_ISOK='{6}'
                                                 where RESULT_ID_SRC = '{0}'
                                                   and RESULT_ID_ADD = '{1}' and QC_TYPE='{5}'";
                    strSql = string.Format(strSql, strRootResultId, strResultId, strRootResult, strResult, strAddBack, strQcType, "");
                    arrVo.Add(strSql);
                }
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 平行样质控信息重新统计
        /// </summary>
        /// <param name="strSimpleId">样品ID</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strQcType">质控信息 现场平行：3，实验室密码平行：4，实验室明码平行：7</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public bool ReStatisticsQcTwin(string strSimpleId, string strCurrentUserId, string strQcType, string strResultStatus)
        {
            ArrayList arrVo = new ArrayList();
            //计算现场加标的回收率，现场平行的均值与相对偏差
            string strSql = @"select *
                                          from T_MIS_MONITOR_RESULT RESULT1
                                         where QC_TYPE = '0'
                                           and RESULT_STATUS = '{3}'
                                           and SAMPLE_ID = '{0}'
                                           and exists (select *
                                                  from T_MIS_MONITOR_RESULT RESULT2
                                                 where RESULT2.QC_SOURCE_ID = RESULT1.ID
                                                   and RESULT2.QC_TYPE = '{2}')
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT_APP
                                                 where RESULT1.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')";
            strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strQcType, strResultStatus);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);

            foreach (DataRow rows in dt.Rows)
            {
                //原始结果ID
                string strRootResultId = rows["ID"].ToString();
                //原始测定值
                string strRootResult = GetNumber(rows["ITEM_RESULT"].ToString()).ToString();

                //获取监测项目ID
                string strItemId = new TMisMonitorResultAccess().Details(strRootResultId).ITEM_ID;
                //根据监测项目获取平行上限
                string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;


                //获取平行样
                strSql = "select * from T_MIS_MONITOR_RESULT where QC_SOURCE_ID='{0}' and QC_TYPE='{1}'";
                strSql = string.Format(strSql, strRootResultId, strQcType);
                DataTable twinTableResult = SqlHelper.ExecuteDataTable(strSql);
                //如果只有一个平行样
                if (twinTableResult.Rows.Count == 1)
                {
                    //平行样结果Id1
                    string strResultId1 = twinTableResult.Rows[0]["ID"].ToString();
                    //平行结果1
                    string strResult1 = GetNumber(twinTableResult.Rows[0]["ITEM_RESULT"].ToString()).ToString();
                    string strValue = getQcTwinValue(strRootResultId, strResult1, "");
                    //平均值
                    string strAvgValue = "";
                    //相对偏差
                    string strTwinOffset = "";
                    if (strValue != "")
                    {
                        strAvgValue = strValue.Split(',')[0].ToString();
                        strTwinOffset = strValue.Split(',')[1].ToString();
                    }

                    decimal dTwinValue = 0;
                    //相对偏差合格标志，默认不合格
                    string isOk = "0";

                    if (strTwinValue.Trim() != "")
                    {
                        bool isSuccess = decimal.TryParse(strTwinValue, out dTwinValue);
                        //如果字符类型成功的话，判断相对偏差是否在允许的范围之内
                        if (isSuccess)
                        {
                            if (decimal.Parse(strTwinOffset) <= dTwinValue)
                                isOk = "1";
                        }
                    }


                    //更新标准样结果表中的数据
                    strSql = @"update T_MIS_MONITOR_QC_TWIN
                                                                       set RESULT_ID_TWIN1 = '{1}',
                                                                           TWIN_RESULT1    = '{2}',
                                                                           TWIN_AVG        = '{3}',
                                                                           TWIN_OFFSET     = '{4}',
                                                                           TWIN_ISOK       = '{6}'
                                                                     where RESULT_ID_SRC = '{0}'
                                                                       and QC_TYPE = '{5}'";
                    strSql = string.Format(strSql, strRootResultId, strResultId1, strResult1, strAvgValue, strTwinOffset, strQcType, isOk);
                    arrVo.Add(strSql);
                }
                //如果存在两个平行样
                if (twinTableResult.Rows.Count == 2)
                {
                    //平行样结果id1
                    string strResultId1 = twinTableResult.Rows[0]["ID"].ToString();
                    //平行样结果id2
                    string strResultId2 = twinTableResult.Rows[1]["ID"].ToString();
                    //平行结果1
                    string strResult1 = "";
                    if (twinTableResult.Rows[0]["ITEM_RESULT"] == null || string.IsNullOrEmpty(twinTableResult.Rows[0]["ITEM_RESULT"].ToString()))
                        strResult1 = "0";
                    else
                        strResult1 = GetNumber(twinTableResult.Rows[0]["ITEM_RESULT"].ToString()).ToString();

                    //平行结果2
                    string strResult2 = "";
                    if (twinTableResult.Rows[1]["ITEM_RESULT"] == null || string.IsNullOrEmpty(twinTableResult.Rows[1]["ITEM_RESULT"].ToString()))
                        strResult2 = "0";
                    else
                        strResult2 = GetNumber(twinTableResult.Rows[1]["ITEM_RESULT"].ToString()).ToString();
                    string strValue = getQcTwinValue(strRootResultId, strResult1, strResult2);
                    //平均值
                    string strAvgValue = "";
                    //相对偏差
                    string strTwinOffset = "";
                    if (strValue != "")
                    {
                        strAvgValue = strValue.Split(',')[0].ToString();
                        strTwinOffset = strValue.Split(',')[1].ToString();
                    }

                    decimal dTwinValue = 0;
                    //相对偏差合格标志，默认不合格
                    string isOk = "0";

                    if (strTwinValue.Trim() != "")
                    {
                        bool isSuccess = decimal.TryParse(strTwinValue, out dTwinValue);
                        //如果字符类型成功的话，判断相对偏差是否在允许的范围之内
                        if (isSuccess)
                        {
                            if (decimal.Parse(strTwinOffset) <= dTwinValue)
                                isOk = "1";
                        }
                    }

                    //更新标准样结果表中的数据
                    strSql = @"update T_MIS_MONITOR_QC_TWIN
                                                                       set RESULT_ID_TWIN1 = '{1}',
                                                                           RESULT_ID_TWIN2 = '{2}',
                                                                           TWIN_RESULT1    = '{3}',
                                                                           TWIN_RESULT2    = '{4}',
                                                                           TWIN_AVG        = '{5}',
                                                                           TWIN_OFFSET     = '{6}',
                                                                           TWIN_ISOK       = '{8}'
                                                                     where RESULT_ID_SRC = '{0}'
                                                                       and QC_TYPE = '{7}'";
                    strSql = string.Format(strSql, strRootResultId, strResultId1, strResultId2, strResult1, strResult2, strAvgValue, strTwinOffset, strQcType, isOk);
                    arrVo.Add(strSql);
                }
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 平行样质控信息重新统计 Create By :weilin 2014-06-24
        /// </summary>
        /// <param name="strSimpleId">样品ID</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strQcType">质控信息 现场平行：3，实验室密码平行：4，实验室明码平行：7</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public bool ReStatisticsQcTwin_QY(string strSimpleId, string strCurrentUserId, string strQcType, string strResultStatus)
        {
            ArrayList arrVo = new ArrayList();
            //现场平行的均值与相对偏差
            string strSql = @"select *
                                          from T_MIS_MONITOR_RESULT RESULT1
                                         where QC_TYPE = '{2}'
                                           and RESULT_STATUS = '{3}'
                                           and SAMPLE_ID = '{0}'
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT_APP
                                                 where RESULT1.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')";
            strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strQcType, strResultStatus);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);

            foreach (DataRow rows in dt.Rows)
            {
                //平行样结果ID
                string strResultId = rows["ID"].ToString();
                //平行样测定值
                string strResult = GetNumber(rows["ITEM_RESULT"].ToString()).ToString();

                //获取监测项目ID
                string strItemId = new TMisMonitorResultAccess().Details(strResultId).ITEM_ID;
                //根据监测项目获取平行上限
                string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;


                //获取原始样
                strSql = "select * from T_MIS_MONITOR_RESULT where ID='{0}' and QC_TYPE='0'";
                strSql = string.Format(strSql, rows["QC_SOURCE_ID"].ToString(), strQcType);
                DataTable twinTableResult = SqlHelper.ExecuteDataTable(strSql);

                if (twinTableResult.Rows.Count > 0)
                {
                    //原始样结果Id
                    string strRootResultId = twinTableResult.Rows[0]["ID"].ToString();
                    //原始结果1
                    string strRootResult = GetNumber(twinTableResult.Rows[0]["ITEM_RESULT"].ToString()).ToString();
                    string strValue = getQcTwinValueEx(strRootResultId, strRootResult, strResult);
                    //平均值
                    string strAvgValue = "";
                    //相对偏差
                    string strTwinOffset = "";
                    if (strValue != "")
                    {
                        strAvgValue = strValue.Split(',')[0].ToString();
                        strTwinOffset = strValue.Split(',')[1].ToString();
                    }

                    decimal dTwinValue = 0;
                    //相对偏差合格标志，默认不合格
                    string isOk = "0";

                    if (strTwinValue.Trim() != "")
                    {
                        bool isSuccess = decimal.TryParse(strTwinValue, out dTwinValue);
                        //如果字符类型成功的话，判断相对偏差是否在允许的范围之内
                        if (isSuccess)
                        {
                            if (decimal.Parse(strTwinOffset) <= dTwinValue)
                                isOk = "1";
                        }
                    }


                    //更新标准样结果表中的数据
                    strSql = @"update T_MIS_MONITOR_QC_TWIN
                                                                       set RESULT_ID_TWIN1 = '{1}',
                                                                           TWIN_RESULT1    = '{2}',
                                                                           TWIN_AVG        = '{3}',
                                                                           TWIN_OFFSET     = '{4}',
                                                                           TWIN_ISOK       = '{6}'
                                                                     where RESULT_ID_SRC = '{0}'
                                                                       and QC_TYPE = '{5}'";
                    strSql = string.Format(strSql, strRootResultId, strResultId, strResult, strAvgValue, strTwinOffset, strQcType, "");
                    arrVo.Add(strSql);
                }

            }
            return ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 标准盲样的重新统计计算
        /// </summary>
        /// <param name="strSimpleId"></param>
        /// <param name="strCurrentUserId"></param>
        /// <param name="strQcType"></param>
        /// <param name="strResultStatus"></param>
        /// <returns></returns>
        public bool ReStatisticsQcBlind(string strSimpleId, string strCurrentUserId, string strQcType, string strResultStatus)
        {
            ArrayList arrVo = new ArrayList();
            //计算偏移量与是否合格判断 偏移量=|测定值-标准值|/标准值*100%
            string strSql = @"select *
                                          from T_MIS_MONITOR_RESULT
                                         where SAMPLE_ID = '{0}'
                                           and QC_TYPE = '{2}'
                                           and RESULT_STATUS = '{3}'
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT_APP
                                                 where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')";
            strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strQcType, strResultStatus);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow rows in dt.Rows)
            {
                //监测结果ID
                string strResultId = rows["ID"].ToString();
                //测定值
                string strResultValue = GetNumber(rows["ITEM_RESULT"].ToString()).ToString();
                //根据结果值获取标准盲样的标准值和不确定度
                decimal decOffset = 0;
                string strStandardValue = "";
                string strUncetaintyValue = "";
                string IS_OK = "0";

                strSql = @"select ID,STANDARD_VALUE,UNCETAINTY from T_MIS_MONITOR_QC_BLIND_ZZ where RESULT_ID='{0}'";
                strSql = string.Format(strSql, strResultId);
                DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
                if (objTable.Rows.Count > 0)
                {
                    strStandardValue = objTable.Rows[0]["STANDARD_VALUE"] == null ? "" : objTable.Rows[0]["STANDARD_VALUE"].ToString();
                    strUncetaintyValue = objTable.Rows[0]["UNCETAINTY"] == null ? "" : objTable.Rows[0]["UNCETAINTY"].ToString();
                    if (strStandardValue != "")
                    {
                        //偏移量 = |测定值-标准值|/标准值*100
                        decOffset = Math.Abs(decimal.Parse(strResultValue) - decimal.Parse(strStandardValue)) / decimal.Parse(strStandardValue) * 100;
                        decOffset = Math.Round(decOffset, 2);
                        //与不确定度对比
                        if (strUncetaintyValue != "")
                        {
                            if (decOffset <= decimal.Parse(strUncetaintyValue))
                                IS_OK = "1";
                        }
                    }
                    //将结果更新到数据库表中
                    string strId = objTable.Rows[0]["ID"].ToString();
                    strSql = "update T_MIS_MONITOR_QC_BLIND_ZZ set BLIND_VALUE='{1}',OFFSET='{2}',BLIND_ISOK='{3}' where ID='{0}'";
                    strSql = string.Format(strSql, strId, strResultValue, decOffset, IS_OK);
                    arrVo.Add(strSql);
                }
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 现场密码的重新统计计算 Create By ：weilin 2014-06-24
        /// </summary>
        /// <param name="strSimpleId"></param>
        /// <param name="strCurrentUserId"></param>
        /// <param name="strQcType"></param>
        /// <param name="strResultStatus"></param>
        /// <returns></returns>
        public bool ReStatisticsQcBlind_QY(string strSimpleId, string strCurrentUserId, string strQcType, string strResultStatus)
        {
            ArrayList arrVo = new ArrayList();
            //计算是否合格：结果值如果在范围之间表示合格（范围：标准值+不确定值\标准值-不确定值）
            string strSql = @"select *
                                          from T_MIS_MONITOR_RESULT
                                         where SAMPLE_ID = '{0}'
                                           and QC_TYPE = '{2}'
                                           and RESULT_STATUS = '{3}'
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT_APP
                                                 where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')";
            strSql = string.Format(strSql, strSimpleId, strCurrentUserId, strQcType, strResultStatus);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            foreach (DataRow rows in dt.Rows)
            {
                //监测结果ID
                string strResultId = rows["ID"].ToString();
                //测定值
                string strResultValue = GetNumber(rows["ITEM_RESULT"].ToString()).ToString();
                //根据结果值获取标准盲样的标准值和不确定度
                //decimal decOffset = 0;
                string strStandardValue = "";
                string strUncetaintyValue = "";
                decimal dMax = 0;
                decimal dMin = 0;
                string IS_OK = "0";

                strSql = @"select ID,STANDARD_VALUE,UNCETAINTY from T_MIS_MONITOR_QC_BLIND_ZZ where RESULT_ID='{0}'";
                strSql = string.Format(strSql, strResultId);
                DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
                if (objTable.Rows.Count > 0)
                {
                    strStandardValue = objTable.Rows[0]["STANDARD_VALUE"].ToString() == "" ? "0" : objTable.Rows[0]["STANDARD_VALUE"].ToString();
                    strUncetaintyValue = objTable.Rows[0]["UNCETAINTY"].ToString() == "" ? "0" : objTable.Rows[0]["UNCETAINTY"].ToString();
                    if (strStandardValue != "")
                    {
                        //上限、下限
                        dMax = Math.Abs(decimal.Parse(strStandardValue) + decimal.Parse(strUncetaintyValue));
                        dMin = Math.Abs(decimal.Parse(strStandardValue) - decimal.Parse(strUncetaintyValue));
                        //与不确定度对比
                        if (strUncetaintyValue != "")
                        {
                            if (decimal.Parse(strResultValue) <= dMax && decimal.Parse(strResultValue) >= dMin)
                                IS_OK = "1";
                        }
                    }
                    //将结果更新到数据库表中
                    string strId = objTable.Rows[0]["ID"].ToString();
                    strSql = "update T_MIS_MONITOR_QC_BLIND_ZZ set BLIND_VALUE='{1}',OFFSET='{2}',BLIND_ISOK='{3}' where ID='{0}'";
                    strSql = string.Format(strSql, strId, strResultValue, "", IS_OK);
                    arrVo.Add(strSql);
                }
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 计算平行样均值与相对偏差
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strValue1">测定值1</param>
        /// <param name="strValue2">测定值2</param>
        /// <returns></returns>
        public string getQcTwinValue(string strResultId, string strValue1, string strValue2)
        {
            string strReturnValue = "";
            //根据结果ID获取监测项目ID、结果值、分析方法ID
            string strSql = "select ITEM_ID,ITEM_RESULT,ANALYSIS_METHOD_ID from T_MIS_MONITOR_RESULT where ID='{0}' ";
            strSql = string.Format(strSql, strResultId);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 0) return "";

            string strItemId = dt.Rows[0]["ITEM_ID"].ToString();
            string strResultValue = GetNumber(dt.Rows[0]["ITEM_RESULT"].ToString()).ToString();
            if (strResultValue == "")
                return strReturnValue;
            string strAnalysisMethodId = dt.Rows[0]["ANALYSIS_METHOD_ID"].ToString();

            //根据监测项目、分析方法获取监测项目小数位数和最低检出限
            strSql = "select PRECISION,LOWER_CHECKOUT from T_BASE_ITEM_ANALYSIS where ITEM_ID='{0}' and ID='{1}'";
            strSql = string.Format(strSql, strItemId, strAnalysisMethodId);
            dt = SqlHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 0) return "";

            string strPrecision = dt.Rows[0]["PRECISION"].ToString();
            string strLowerCheckOut = dt.Rows[0]["LOWER_CHECKOUT"].ToString();

            //如果小数精度没有设置，则默认保留一位小数
            if (strPrecision == null || string.IsNullOrEmpty(strPrecision))
                strPrecision = "1";

            //计算平均数,如果只有一个平行的情况
            if (strValue2 == "")
            {
                decimal dResultValue = decimal.Parse(strResultValue);
                decimal dValue1 = decimal.Parse(strValue1);

                decimal dAvgSaveValue = Math.Round((dResultValue + dValue1) / 2, int.Parse(strPrecision), MidpointRounding.ToEven);

                //如果最低检出限没有设置，则按照正常方式测算平均值
                if (strLowerCheckOut != null && !string.IsNullOrEmpty(strLowerCheckOut))
                {
                    decimal dLowerCheckOut = decimal.Parse(strLowerCheckOut);

                    if (dValue1 < dLowerCheckOut)
                        dValue1 = dLowerCheckOut;
                    if (dResultValue < dLowerCheckOut)
                        dResultValue = dLowerCheckOut;

                    dAvgSaveValue = Math.Round((dResultValue + dValue1) / 2, int.Parse(strPrecision), MidpointRounding.ToEven);
                }
                decimal dAvgRealValue = (dResultValue + dValue1) / 2;
                //计算相对偏差
                decimal dTwinOffset = Math.Abs(dValue1 - dAvgRealValue) / dAvgRealValue * 100;
                strReturnValue = dAvgSaveValue.ToString() + "," + Math.Round(dTwinOffset, 1).ToString();
            }
            if (strValue2 != "")
            {
                decimal dResultValue = decimal.Parse(strResultValue);
                decimal dValue1 = decimal.Parse(strValue1);
                decimal dValue2 = decimal.Parse(strValue2);
                decimal dAvgSaveValue = Math.Round((dResultValue + dValue1 + dValue2) / 3, int.Parse(strPrecision), MidpointRounding.ToEven);

                //如果最低检出限没有设置，则按照正常方式测算平均值
                if (strLowerCheckOut != null && !string.IsNullOrEmpty(strLowerCheckOut))
                {
                    decimal dLowerCheckOut = decimal.Parse(strLowerCheckOut);

                    if (dValue1 < dLowerCheckOut)
                        dValue1 = dLowerCheckOut;
                    if (dValue2 < dLowerCheckOut)
                        dValue2 = dLowerCheckOut;
                    if (dResultValue < dLowerCheckOut)
                        dResultValue = dLowerCheckOut;

                    dAvgSaveValue = Math.Round((dResultValue + dValue1 + dValue2) / 3, int.Parse(strPrecision), MidpointRounding.ToEven);
                }
                decimal dAvgRealValue = (dResultValue + dValue1 + dValue2) / 3;
                //计算相对偏差
                decimal dTwinOffset = Math.Abs(dResultValue - dAvgRealValue) / dAvgRealValue * 100;
                strReturnValue = dAvgSaveValue.ToString() + "," + Math.Round(dTwinOffset, 1).ToString();
            }
            return strReturnValue;
        }

        /// <summary>
        /// 计算平行样均值与相对偏差(清远) Create By weilin 2014-03-20
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strValue1">测定值1</param>
        /// <param name="strValue2">测定值2</param>
        /// <returns></returns>
        public string getQcTwinValueEx(string strResultId, string strValue1, string strValue2)
        {
            string strReturnValue = "";
            //根据结果ID获取监测项目ID、结果值、分析方法ID
            string strSql = "select ITEM_ID,ITEM_RESULT,ANALYSIS_METHOD_ID from T_MIS_MONITOR_RESULT where ID='{0}' ";
            strSql = string.Format(strSql, strResultId);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 0) return ",";

            string strItemId = dt.Rows[0]["ITEM_ID"].ToString();
            string strResultValue = GetNumber(dt.Rows[0]["ITEM_RESULT"].ToString()).ToString();

            string strAnalysisMethodId = dt.Rows[0]["ANALYSIS_METHOD_ID"].ToString();

            //根据监测项目、分析方法获取监测项目小数位数和最低检出限
            strSql = "select PRECISION,LOWER_CHECKOUT from T_BASE_ITEM_ANALYSIS where ITEM_ID='{0}' and ID='{1}'";
            strSql = string.Format(strSql, strItemId, strAnalysisMethodId);
            dt = SqlHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 0) return ",";

            string strPrecision = dt.Rows[0]["PRECISION"].ToString();
            string strLowerCheckOut = dt.Rows[0]["LOWER_CHECKOUT"].ToString();

            //如果小数精度没有设置，则默认保留一位小数  huangjinjun update
            //if (strPrecision == null || strPrecision=="0" || string.IsNullOrEmpty(strPrecision))
            //strPrecision = "1";

            //huangjinjun add 20141029
            int val = 0;
            val = strValue1.Contains('.') ? strValue1.Split('.')[1].Length : 0;
            strPrecision = val.ToString();


            //计算平均数,如果只有一个平行的情况
            decimal dValue1 = decimal.Parse(strValue1 == "" ? "0" : strValue1);
            decimal dValue2 = decimal.Parse(strValue2 == "" ? "0" : strValue2);

            decimal dAvgSaveValue = Math.Round((dValue1 + dValue2) / 2, int.Parse(strPrecision), MidpointRounding.ToEven);

            //如果最低检出限没有设置，则按照正常方式测算平均值
            if (strLowerCheckOut != null && !string.IsNullOrEmpty(strLowerCheckOut))
            {
                decimal dLowerCheckOut = decimal.Parse(strLowerCheckOut);

                if (dValue1 < dLowerCheckOut)
                    dValue1 = dLowerCheckOut;
                if (dValue2 < dLowerCheckOut)
                    dValue2 = dLowerCheckOut;

                dAvgSaveValue = Math.Round((dValue1 + dValue2) / 2, int.Parse(strPrecision), MidpointRounding.ToEven);
            }

            //计算相对偏差
            decimal dTwinOffset = Math.Abs(dValue1 - dValue2) / (dValue1 + dValue2) * 100;
            strReturnValue = dAvgSaveValue.ToString() + "," + Math.Round(dTwinOffset, 1).ToString();


            return strReturnValue;
        }

        /// <summary>
        /// 获取结果表现场项目信息
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strQcType">质控类型 外控：0，内控1</param>
        /// <param name="ItemCondition_WithIn">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptResult(string strSubtaskID, string strTaskPointID, string ItemCondition_WithIn = "1")
        {
            string sqlItemCondition = getItemCondition_WithIn(ItemCondition_WithIn);

            string strSQL = @"SELECT a.IS_SAMPLEDEPT,a.IS_ANYSCENE_ITEM, t.*  FROM T_MIS_MONITOR_RESULT t
                              left join T_BASE_ITEM_INFO a on a.ID = t.ITEM_ID	
                                         where exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.POINT_ID = '{1}'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID = '{0}'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.ID = t.SAMPLE_ID
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where 1=1 {2} 
                                                           and T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.ID = t.ITEM_ID))";
            strSQL = string.Format(strSQL, strSubtaskID, strTaskPointID, sqlItemCondition);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 获取结果表现场项目信息
        /// </summary>
        /// <param name="strResultId">结果ID</param>
        /// <param name="strQcType">质控类型 外控：0，内控1</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptResult_QHD(string strSubtaskID, string strTaskPointID)
        {
            string strSQL = @"SELECT *
                                          FROM T_MIS_MONITOR_RESULT
                                         where exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.ID = '{1}'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID = '{0}'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))";
            strSQL = string.Format(strSQL, strSubtaskID, strTaskPointID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取结果表现场项目信息
        /// </summary>
        /// <param name="strSubtaskID">子任务ID</param>
        /// <param name="strSAMPLEID">样品ID</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptResultEx(string strSubtaskID, string strSAMPLEID)
        {
            string strSQL = @"SELECT *
                                          FROM T_MIS_MONITOR_RESULT
                                         where exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.ID = '{1}'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID = '{0}'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))";
            strSQL = string.Format(strSQL, strSubtaskID, strSAMPLEID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        #endregion

        #region 分析结果录入方法【清远】

        /// <summary>
        /// 获取分析结果录入环节监测项目信息
        /// </summary>
        /// <param name="strCurrentUserId">当前登陆用户</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getResultInResultFlow_QY(string strCurrentUserId, string strResultStatus)
        {
            string strSql = @"select sum_record.*
                                          from (select distinct ITEM_ID
                                                  from T_MIS_MONITOR_RESULT
                                                 where (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT_APP
                                                         where T_MIS_MONITOR_RESULT.ID =
                                                               T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                           and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{0}')
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)) sum_record";
            strSql = string.Format(strSql, strCurrentUserId, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取分析结果录入环节监测项目信息记录数
        /// </summary>
        /// <param name="strCurrentUserId">当前登陆用户</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public int getResultInResultFlowCount_QY(string strCurrentUserId, string strResultStatus)
        {
            string strSql = @"select count(*)
                                              from (select sum_record.*
                                                      from (select distinct ITEM_ID
                                                              from T_MIS_MONITOR_RESULT
                                                             where (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                               and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                               and exists
                                                             (select *
                                                                      from T_MIS_MONITOR_RESULT_APP
                                                                     where T_MIS_MONITOR_RESULT.ID =
                                                                           T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                                       and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{0}')
                                                               and exists
                                                             (select *
                                                                      from T_BASE_ITEM_INFO
                                                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                       and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                       and T_BASE_ITEM_INFO.ID =
                                                                           T_MIS_MONITOR_RESULT.ITEM_ID)) sum_record) sum";
            strSql = string.Format(strSql, strCurrentUserId, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 根据监测项目获取样品信息
        /// </summary>
        /// <param name="strItemId">监测项目ID</param>
        /// <param name="strCurrentUserId">当前用户ＩＤ</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public DataTable getSimpleCodeInResult_QY(string strItemId, string strCurrentUserId, string strResultStatus)
        {
            string strSql = @"select sum_record.*,
                                           APPARATUS_INFO.APPARATUS_CODE,
                                           APPARATUS_INFO.APPARATUS_NAME,
                                           APPARATUS_INFO.LOWER_CHECKOUT,
                                           APPARATUS_INFO.ID as LOWER_CHECKOUT_ID
                                      from (select record.*,
                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                   CONVERT(VARCHAR(10),T_MIS_MONITOR_RESULT_APP.FINISH_DATE,120) FINISH_DATE
                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                           SAMPLE_ID,
                                                           T_MIS_MONITOR_RESULT.ITEM_ID,
                                                           T_MIS_MONITOR_SUBTASK.MONITOR_ID,
                                                           REMARK_2,REMARK_1,
                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                           (select SAMPLE_CODE
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SAMPLE_CODE,
                                                           (select SPECIALREMARK
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SPECIALREMARK,
                                                           (select SAMPLE_REMARK
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SAMPLE_REMARK,
                                                           ITEM_RESULT, REMARK_3,REMARK_5,
                                                           QC,
                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                           STANDARD_ID,
                                                           T_BASE_METHOD_INFO.METHOD_CODE,
                                                           T_MIS_MONITOR_RESULT.RESULT_CHECKOUT
                                                      from T_MIS_MONITOR_RESULT
                                                     left join T_MIS_MONITOR_SAMPLE_INFO on(T_MIS_MONITOR_RESULT.SAMPLE_ID=T_MIS_MONITOR_SAMPLE_INFO.ID)
                                                     left join T_MIS_MONITOR_SUBTASK on(T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID=T_MIS_MONITOR_SUBTASK.ID)
                                                     left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                     left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                     left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                     left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                     where T_MIS_MONITOR_RESULT.ITEM_ID = '{0}'
                                                       and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                                       and exists
                                                     (select *
                                                              from T_MIS_MONITOR_RESULT_APP
                                                             where T_MIS_MONITOR_RESULT.ID =
                                                                   T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                               and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')
                                                       and exists
                                                     (select *
                                                              from T_BASE_ITEM_INFO
                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                               and T_BASE_ITEM_INFO.ID =
                                                                   T_MIS_MONITOR_RESULT.ITEM_ID)) record
                                              left join T_MIS_MONITOR_RESULT_APP
                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                      left join (select (select APPARATUS_CODE
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                        (select NAME
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                        LOWER_CHECKOUT,
                                                        ITEM_ID,
                                                        ANALYSIS_METHOD_ID,
                                                        ID
                                                   from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                       and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strItemId, strCurrentUserId, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 根据监测项目获取样品信息
        /// </summary>
        /// <param name="strItemId">监测项目ID</param>
        /// <param name="strCurrentUserId">当前用户ＩＤ</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public DataTable getSampleCodeInResult_MAS(string strItemId)
        {
            string strSql = @"select sum_record.*,
                                           APPARATUS_INFO.APPARATUS_CODE,
                                           APPARATUS_INFO.APPARATUS_NAME,
                                           APPARATUS_INFO.LOWER_CHECKOUT,
                                           APPARATUS_INFO.ID as LOWER_CHECKOUT_ID
                                      from (select record.*,
                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                   CONVERT(VARCHAR(10),T_MIS_MONITOR_RESULT_APP.FINISH_DATE,120) FINISH_DATE
                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                           SAMPLE_ID,
                                                           T_MIS_MONITOR_RESULT.ITEM_ID,
                                                           T_MIS_MONITOR_SUBTASK.MONITOR_ID,
                                                           REMARK_2,REMARK_1,
                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                           (select SAMPLE_CODE
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SAMPLE_CODE,
                                                           (select SPECIALREMARK
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SPECIALREMARK,
                                                           (select SAMPLE_REMARK
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SAMPLE_REMARK,
                                                           (select SAMPLE_BARCODE
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SAMPLE_BARCODE,
                                                           ITEM_RESULT, REMARK_3,REMARK_5,
                                                           QC,
                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                           STANDARD_ID,
                                                           T_BASE_METHOD_INFO.METHOD_CODE,
                                                           T_MIS_MONITOR_RESULT.RESULT_CHECKOUT
                                                      from T_MIS_MONITOR_RESULT
                                                     left join T_MIS_MONITOR_SAMPLE_INFO on(T_MIS_MONITOR_RESULT.SAMPLE_ID=T_MIS_MONITOR_SAMPLE_INFO.ID)
                                                     left join T_MIS_MONITOR_SUBTASK on(T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID=T_MIS_MONITOR_SUBTASK.ID)
                                                     left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                     left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                     left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                     left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                     where T_MIS_MONITOR_RESULT.ID = '{0}'
                                                       ) record
                                              left join T_MIS_MONITOR_RESULT_APP
                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                      left join (select (select APPARATUS_CODE
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                        (select NAME
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                        LOWER_CHECKOUT,
                                                        ITEM_ID,
                                                        ANALYSIS_METHOD_ID,
                                                        ID
                                                   from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                       and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strItemId);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 根据监测项目获取样品信息  (批量处理)  黄飞20150916
        /// </summary>
        /// <param name="strOneGridId">监测项目ID</param>
        /// <param name="strResultID">批量处理ID</param>
        /// <returns></returns>
        public DataTable getSampleCodeInResult_MAS_Batch(string strOneGridId, string strResultID)
        {
            string strSql = @"select sum_record.*,
                                           APPARATUS_INFO.APPARATUS_CODE,
                                           APPARATUS_INFO.APPARATUS_NAME,
                                           APPARATUS_INFO.LOWER_CHECKOUT,
                                           APPARATUS_INFO.ID as LOWER_CHECKOUT_ID
                                      from (select record.*,
                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                   CONVERT(VARCHAR(10),T_MIS_MONITOR_RESULT_APP.FINISH_DATE,120) FINISH_DATE
                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                           SAMPLE_ID,
                                                           T_MIS_MONITOR_RESULT.ITEM_ID,
                                                           T_MIS_MONITOR_SUBTASK.MONITOR_ID,
                                                           REMARK_2,REMARK_1,
                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                           (select SAMPLE_CODE
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SAMPLE_CODE,
                                                           (select SPECIALREMARK
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SPECIALREMARK,
                                                           (select SAMPLE_REMARK
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SAMPLE_REMARK,
                                                           (select SAMPLE_BARCODE
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where ID = SAMPLE_ID) as SAMPLE_BARCODE,
                                                           ITEM_RESULT, REMARK_3,REMARK_5,
                                                           QC,
                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                           STANDARD_ID,
                                                           T_BASE_METHOD_INFO.METHOD_CODE,
                                                           T_MIS_MONITOR_RESULT.RESULT_CHECKOUT
                                                      from T_MIS_MONITOR_RESULT
                                                     left join T_MIS_MONITOR_SAMPLE_INFO on(T_MIS_MONITOR_RESULT.SAMPLE_ID=T_MIS_MONITOR_SAMPLE_INFO.ID)
                                                     left join T_MIS_MONITOR_SUBTASK on(T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID=T_MIS_MONITOR_SUBTASK.ID)
                                                     left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                     left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                     left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                     left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                     where T_MIS_MONITOR_RESULT.ITEM_ID = '{0}' AND T_MIS_MONITOR_RESULT.ID IN({1})
                                                       ) record
                                              left join T_MIS_MONITOR_RESULT_APP
                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                      left join (select (select APPARATUS_CODE
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                        (select NAME
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                        LOWER_CHECKOUT,
                                                        ITEM_ID,
                                                        ANALYSIS_METHOD_ID,
                                                        ID
                                                   from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                       and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strOneGridId, strResultID);
            return SqlHelper.ExecuteDataTable(strSql);
        }







        /// <summary>
        /// 将监测结果发送至下一个环节
        /// </summary>
        /// <param name="strSumResultId">监测结果ＩＤ总和</param>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strNextFlowUserId">要发送的用户ＩＤ</param>
        /// <param name="strNextResultStatus">下一环节结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public bool SendResultToNext_QY(string strSumResultId, string strCurrentUserId, string strNextFlowUserId, string strNextResultStatus)
        {
            bool isSuccess = false;
            try
            {
                ArrayList arrVo = new ArrayList();
                if (strSumResultId == "") return false;

                foreach (string strResultId in strSumResultId.Split(','))
                {
                    //根据监测结果ID获取样品ID
                    string strSampleId = new TMisMonitorResultAccess().Details(strResultId).SAMPLE_ID;
                    //做重新统计
                    ReStatisticsQc_QY(strSampleId, strCurrentUserId, "20");

                    string strSql = @"update T_MIS_MONITOR_RESULT
                                                   set REMARK_1='{1}',RESULT_STATUS = '{2}',TASK_TYPE='发送' where ID='{0}'";
                    strSql = string.Format(strSql, strResultId, strNextFlowUserId, strNextResultStatus);
                    arrVo.Add(strSql);

                    strSql = @"update T_MIS_MONITOR_RESULT_APP
                                                   set FINISH_DATE='{1}' where RESULT_ID='{0}'";
                    strSql = string.Format(strSql, strResultId, DateTime.Now.ToString());
                    arrVo.Add(strSql);
                }
                isSuccess = ExecuteSQLByTransaction(arrVo);
            }
            catch (Exception ex) { }
            return isSuccess;
        }

        /// <summary>
        /// 创建原因：按列更新列数据值
        /// 创建人：胡方扬
        /// 创建时间：2013-07-11
        /// </summary>
        /// <param name="strCellName"></param>
        /// <param name="strCellValue"></param>
        /// <param name="strInforId"></param>
        /// <returns></returns>
        public bool UpdateCellValue(string strCellName, string strCellValue, string strInforId)
        {
            string strSQL = @" UPDATE dbo.T_MIS_MONITOR_RESULT SET {0}='{1}' WHERE ID='{2}'";
            strSQL = string.Format(strSQL, strCellName, strCellValue, strInforId);
            return SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
        }
        #endregion

        #region 分析结果录入方法【郑州】

        /// <summary>
        /// 设置郑州样品已经领取方法
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public bool setReceiveSample_ZZ(string strSampleId, string strCurrentUserId, string strResultStatus)
        {
            ArrayList arrVo = new ArrayList();
            string strSql = @"update T_MIS_MONITOR_RESULT
                                           SET REMARK_1 = '1'
                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                           and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                               T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT_APP
                                                 where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                                   and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')
                                           and exists
                                         (select *
                                                  from T_BASE_ITEM_INFO
                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)";
            strSql = string.Format(strSql, strSampleId, strCurrentUserId, strResultStatus);
            arrVo.Add(strSql);

            strSql = @"update T_MIS_MONITOR_SAMPLE_INFO set REMARK1 = '1' where ID = '{0}'";
            strSql = string.Format(strSql, strSampleId);
            arrVo.Add(strSql);

            return ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 获取样品已领取状态
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public bool getReceiveSampleStatus(string strSampleId, string strCurrentUserId, string strResultStatus)
        {
            string strSql = @"select *
                                      from T_MIS_MONITOR_RESULT
                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                       and REMARK_1 = '1'
                                       and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_RESULT_APP
                                             where T_MIS_MONITOR_RESULT.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID
                                               and T_MIS_MONITOR_RESULT_APP.HEAD_USERID = '{1}')
                                       and exists
                                     (select *
                                              from T_BASE_ITEM_INFO
                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)";
            strSql = string.Format(strSql, strSampleId, strCurrentUserId, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql).Rows.Count > 0 ? true : false;
        }

        /// <summary>
        /// 获取默认负责人信息
        /// </summary>
        /// <param name="strSampleId">样品编号</param>
        /// <returns></returns>
        public string getEntire_QC(string strSampleId)
        {
            string strSql = @"select ALLQC_STATUS
                                  from T_MIS_MONITOR_TASK
                                 where exists (select *
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.ID
                                                   and T_MIS_MONITOR_SAMPLE_INFO.ID = '{0}')
        
                                        )";
            strSql = string.Format(strSql, strSampleId);
            object obj = SqlHelper.ExecuteScalar(strSql);
            return obj == null ? "" : obj.ToString();
        }
        /// <summary>
        /// 将结果发送至下一个环节
        /// </summary>
        /// <param name="strResultId">结果ID集</param>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strCurrentResultStatus">当前环节结果状态 01:分析任务分配 02：分析结果填报 03：分析主任复核 04：质量科审核 05：质量负责人审核</param>
        /// <param name="strNextResultStatus">下一环节结果状态 01:分析任务分配 02：分析结果填报 03：分析主任复核 04：质量科审核 05：质量负责人审核</param>
        /// <returns></returns>
        public bool SendResultToNext_ZZ(string strResultId, string strCurrentUserId, string strCurrentResultStatus, string strNextResultStatus)
        {
            bool isSuccess = false;
            ArrayList list = new ArrayList();
            try
            {
                string strSql = "";
                //                strSql = @"select * from  T_MIS_MONITOR_RESULT
                //                                    where ID in({0}) and (ITEM_RESULT is null or ITEM_RESULT='')";
                //                strSql = string.Format(strSql, strResultId);
                //                DataTable dt = SqlHelper.ExecuteDataTable(strSql);
                //                if (dt.Rows.Count > 0)
                //                    return false;

                if (strCurrentResultStatus == "20")
                {
                    //根据结果ID获取样品ID
                    string strSampleId = "";
                    strSql = @"select SAMPLE_ID from T_MIS_MONITOR_RESULT where ID  in({0})";
                    DataTable objTable = SqlHelper.ExecuteDataTable(strSql);
                    if (objTable.Rows.Count > 0)
                        strSampleId = objTable.Rows[0]["SAMPLE_ID"] == null ? "" : objTable.Rows[0]["SAMPLE_ID"].ToString();
                    if (strSampleId != "")
                        ReStatisticsQc(strSampleId, strCurrentUserId, strCurrentResultStatus);
                }

                strSql = @"update T_MIS_MONITOR_RESULT set RESULT_STATUS = '{1}' where ID in({0})";
                strSql = string.Format(strSql, strResultId, strNextResultStatus);
                list.Add(strSql);

                strSql = @"update T_MIS_MONITOR_RESULT_APP set HEAD_USERID='{0}' where RESULT_ID in({1})";
                strSql = string.Format(strSql, strCurrentUserId, strResultId);
                list.Add(strSql);

                isSuccess = SqlHelper.ExecuteSQLByTransaction(list);
            }
            catch (Exception ex) { }
            return isSuccess;
        }
        #endregion

        #region 分析校核方法

        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2012.12.10
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskCheckInfo(string strCurrentUserId, string strFlowCode, string strTaskStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                              from T_MIS_MONITOR_TASK
                                             where exists
                                             (select *
                                                      from T_MIS_MONITOR_SUBTASK
                                                     where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                                       and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                       and exists
                                                     (select *
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                               and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                                   T_MIS_MONITOR_SUBTASK.ID
                                                               and exists
                                                             (select *
                                                                      from T_MIS_MONITOR_RESULT
                                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                           T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                       and exists
                                                                     (select *
                                                                              from T_BASE_ITEM_INFO
                                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                               and T_BASE_ITEM_INFO.ID =
                                                                                   T_MIS_MONITOR_RESULT.ITEM_ID)
                                                                       and (T_MIS_MONITOR_RESULT.RESULT_STATUS = '02' or
                                                                           T_MIS_MONITOR_RESULT.RESULT_STATUS = '03'))))";
            strSql = string.Format(strSql, strTaskStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2012.12.10
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public int getTaskInfoCheckCount(string strCurrentUserId, string strFlowCode, string strTaskStatus)
        {
            string strSql = @"select count(*)
                                                  from T_MIS_MONITOR_TASK
                                                 where exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                                       T_MIS_MONITOR_SUBTASK.ID
                                                                   and exists
                                                                 (select *
                                                                          from T_MIS_MONITOR_RESULT
                                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                           and exists
                                                                         (select *
                                                                                  from T_BASE_ITEM_INFO
                                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                                   and T_BASE_ITEM_INFO.ID =
                                                                                       T_MIS_MONITOR_RESULT.ITEM_ID)
                                                                           and (T_MIS_MONITOR_RESULT.RESULT_STATUS = '02' or
                                                                               T_MIS_MONITOR_RESULT.RESULT_STATUS = '03'))))";
            strSql = string.Format(strSql, strTaskStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 获取分析结果校核环节的样品信息
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleCheckInfo(string strTaskId, string strCurrentUserId, string strFlowCode, int iIndex, int iCount)
        {
            string strSql = @"select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                           and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '03'
		                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID='{0}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                                           and (T_MIS_MONITOR_RESULT.RESULT_STATUS = '02' or
                                                               T_MIS_MONITOR_RESULT.RESULT_STATUS = '03'))";
            strSql = string.Format(strSql, strTaskId);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }

        /// <summary>
        /// 获取分析结果校核环节的样品信息数量
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public int getTaskSimpleCheckInfoCount(string strTaskId, string strCurrentUserId, string strFlowCode)
        {
            string strSql = @"select count(*)
                                                  from (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_SUBTASK
                                                                 where T_MIS_MONITOR_SUBTASK.ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                   and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '03'
                                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                   and exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID)
                                                                   and (T_MIS_MONITOR_RESULT.RESULT_STATUS = '02' or
                                                                       T_MIS_MONITOR_RESULT.RESULT_STATUS = '03'))) record";
            strSql = string.Format(strSql, strTaskId);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 获取分析结果校核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strResultStatus">结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable getTaskItemCheckInfo(string strUserId, string strSampleId, string strResultStatus, string iSample, string strMonitorID)
        {
            string strItemWhere = "";
            string strSAMPLEDEPT = "";
            if (iSample == "0" || iSample == "1")
            {
                if (strMonitorID == "000000001" || strMonitorID == "000000002")
                {
                    string strValue = getSampleItem();
                    if (iSample == "0")
                    {
                        strItemWhere = " and T_MIS_MONITOR_RESULT.ITEM_ID not in(" + strValue + ")";
                    }
                    else
                    {
                        strItemWhere = " and T_MIS_MONITOR_RESULT.ITEM_ID in(" + strValue + ")";
                    }
                }
                else
                {
                    if (iSample == "1")
                    {
                        strItemWhere = " and 1=2";
                    }
                }
                strSAMPLEDEPT = "1=1";
            }
            else
            {
                strSAMPLEDEPT = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'";
            }
            string strSql = @"select sum_record.*,
                                                   APPARATUS_INFO.APPARATUS_NAME,
                                                   APPARATUS_INFO.LOWER_CHECKOUT
                                              from (select record.*,
                                                           T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                           T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                           T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                           T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                                      from (select T_MIS_MONITOR_RESULT.ID,
                                                                   T_MIS_MONITOR_RESULT.ITEM_ID,
                                                                    T_SYS_DICT.DICT_TEXT as UNIT,
                                                                   ITEM_RESULT,
                                                                   QC,
                                                                   T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                                   T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                                   STANDARD_ID,
                                                                   RESULT_STATUS,
                                                                   REMARK_1,
                                                                   REMARK_2
                                                              from T_MIS_MONITOR_RESULT
                                                              left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                              left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                              left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                             where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}' --and T_MIS_MONITOR_RESULT.REMARK_1 = '{2}'
                                                               {3}
                                                               and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '10'or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                               and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                               and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                             (select *
                                                                      from T_BASE_ITEM_INFO
                                                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                       and {4}
                                                                       and T_BASE_ITEM_INFO.ID =
                                                                           T_MIS_MONITOR_RESULT.ITEM_ID))) record
                                                      left join T_MIS_MONITOR_RESULT_APP
                                                        on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                              left join (select (select NAME
                                                                   from T_BASE_APPARATUS_INFO
                                                                  where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                                LOWER_CHECKOUT,
                                                                ITEM_ID,
                                                                ANALYSIS_METHOD_ID,ID
                                                           from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                                on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                               and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSampleId, strResultStatus, strUserId, strItemWhere, strSAMPLEDEPT);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取分析结果校核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strResultStatus">结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable getTaskItemCheckInfo_QY(string strUserId, string strSampleId, string strResultStatus)
        {
            string strSql = @"select sum_record.*,
                                                   APPARATUS_INFO.APPARATUS_NAME,
                                                   APPARATUS_INFO.LOWER_CHECKOUT
                                              from (select record.*,
                                                           T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                           T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                           T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                           T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                                      from (select T_MIS_MONITOR_RESULT.ID,
                                                                   T_MIS_MONITOR_RESULT.ITEM_ID,
                                                                    T_SYS_DICT.DICT_TEXT as UNIT,
                                                                   ITEM_RESULT,
                                                                   QC,
                                                                   T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                                   T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                                   STANDARD_ID,
                                                                   RESULT_STATUS,
                                                                   REMARK_1,
                                                                   REMARK_2,REMARK_5,T_MIS_MONITOR_RESULT.RESULT_CHECKOUT
                                                              from T_MIS_MONITOR_RESULT
                                                              left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                              left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                              left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                             where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}' and T_MIS_MONITOR_RESULT.REMARK_1 = '{2}'
                                                               and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '10'or
                                                                   T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                                               and T_MIS_MONITOR_RESULT.RESULT_STATUS in ('{1}')
                                                               and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                             (select *
                                                                      from T_BASE_ITEM_INFO
                                                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                       and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                       and T_BASE_ITEM_INFO.ID =
                                                                           T_MIS_MONITOR_RESULT.ITEM_ID))) record
                                                      left join T_MIS_MONITOR_RESULT_APP
                                                        on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                              left join (select (select NAME
                                                                   from T_BASE_APPARATUS_INFO
                                                                  where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                                LOWER_CHECKOUT,
                                                                ITEM_ID,
                                                                ANALYSIS_METHOD_ID,ID
                                                           from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                                on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                               and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSampleId, strResultStatus, strUserId);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取分析结果校核环节的监测项目数量
        /// </summary>
        /// <param name="strSampleId">样品ID</param>
        /// <param name="strResultStatus">结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getTaskItemCheckInfoCount(string strSampleId, string strResultStatus)
        {
            string strSql = @"select count(*)
                                          from (select sum_record.*,
                                                       APPARATUS_INFO.APPARATUS_NAME,
                                                       APPARATUS_INFO.LOWER_CHECKOUT
                                                  from (select record.*,
                                                               T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                               T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                               T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                               T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                                          from (select T_MIS_MONITOR_RESULT.ID,
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID,
                                                                       T_SYS_DICT.DICT_TEXT as UNIT,
                                                                       ITEM_RESULT,
                                                                       QC,
                                                                       T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                                       T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                                       STANDARD_ID,
                                                                       RESULT_STATUS
                                                                  from T_MIS_MONITOR_RESULT
                                                                  left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                                  left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                                  left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                                  left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
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
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID)) record
                                                          left join T_MIS_MONITOR_RESULT_APP
                                                            on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                                  left join (select (select NAME
                                                                      from T_BASE_APPARATUS_INFO
                                                                     where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                                   LOWER_CHECKOUT,
                                                                   ITEM_ID,
                                                                   ANALYSIS_METHOD_ID,ID
                                                              from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                                    on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                                   and APPARATUS_INFO.ID =
                                                       sum_record.ANALYSIS_METHOD_ID) record_count";
            strSql = string.Format(strSql, strSampleId, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 判断任务是否可以发送
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public bool IsCanSendTaskCheckToNextFlow(string strTaskId, string strCurrentUserId, string strFlowCode, string strResultStatus)
        {
            string strSql = @"select *
                                          from T_MIS_MONITOR_RESULT
                                         where T_MIS_MONITOR_RESULT.RESULT_STATUS <> '{1}'
                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                         (select *
                                                  from T_BASE_ITEM_INFO
                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                   and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            strSql = string.Format(strSql, strTaskId, strResultStatus);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            return dt.Rows.Count > 0 ? false : true;
        }
        /// <summary>
        /// 判断一个任务中的分析类现场项目的状态情况 Create By : weilin 2014-06-19
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="strCurrentUserId"></param>
        /// <param name="strFlowCode"></param>
        /// <param name="strResultStatus"></param>
        /// <returns></returns>
        public bool IsAnalySampleItemFlow(string strTaskId, string strResultStatus)
        {
            string strSql = @"select 1
                                          from T_MIS_MONITOR_RESULT
                                         where T_MIS_MONITOR_RESULT.RESULT_STATUS <> '{1}'
                                           and (exists
                                         (select *
                                                  from T_BASE_ITEM_INFO
                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                   and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM = '1'
                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                   and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            strSql = string.Format(strSql, strTaskId, strResultStatus);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            return dt.Rows.Count > 0 ? false : true;
        }
        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strTaskStatus">子任务状态，质控审核:04</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public bool SendTaskCheckToNextFlow(string strTaskId, string strCurrentUserId, string strTaskStatus, string strFlowCode)
        {
            string strSql = @"update T_MIS_MONITOR_SUBTASK set TASK_STATUS='{1}'
                                         where TASK_ID = '{0}'
                                         ";
            strSql = string.Format(strSql, strTaskId, strTaskStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        #endregion

        #region 分析数据审核（分析室主任审核）方法【秦皇岛】

        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，数据审核：duty_other_audit，分析室主任审核：duty_other_analyse_result，技术室主任审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskCheckInfo_QHD(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strSampleStatus, string strResultStatus, string iSample, int iIndex, int iCount)
        {
            string strResultWhere = "";
            string strSAMPLEDEPT = "";
            string strValue = getSampleItem();
            if (iSample == "0")
            {
                strResultWhere = "(T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002') and T_MIS_MONITOR_RESULT.ITEM_ID not in(" + strValue + "))";
                strResultWhere += " or (T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002'))";
                strSAMPLEDEPT = "1=1";
            }
            else if (iSample == "1")
            {
                strResultWhere = "(T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002') and T_MIS_MONITOR_RESULT.ITEM_ID in(" + strValue + "))";
                strResultWhere += " or (T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002') and 1=2)";
                strSAMPLEDEPT = "1=1";
            }
            else
            {
                strResultWhere = "1=1";
                strSAMPLEDEPT = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'";
            }

            string strSql = @"select *
                                              from T_MIS_MONITOR_TASK
                                             where exists
                                             (select *
                                                      from T_MIS_MONITOR_SUBTASK
                                                     where T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({0})
                                                       and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                       and exists
                                                     (select *
                                                              from T_MIS_MONITOR_SAMPLE_INFO
                                                             where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                                   T_MIS_MONITOR_SUBTASK.ID
                                                               and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                               and exists
                                                             (select *
                                                                      from T_MIS_MONITOR_RESULT
                                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                           T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                       and ({3})
                                                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                                     (select *
                                                                              from T_BASE_ITEM_INFO
                                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                               and {4}
                                                                               and T_BASE_ITEM_INFO.ID =
                                                                                   T_MIS_MONITOR_RESULT.ITEM_ID))
                                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')))";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus, strSampleStatus, strResultWhere, strSAMPLEDEPT);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，数据审核：duty_other_audit，分析室主任审核：duty_other_analyse_result，技术室主任审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getTaskInfoCheckCount_QHD(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strSampleStatus, string strResultStatus, string iSample)
        {
            string strResultWhere = "";
            string strSAMPLEDEPT = "";
            string strValue = getSampleItem();
            if (iSample == "0")
            {
                strResultWhere = "(T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002') and T_MIS_MONITOR_RESULT.ITEM_ID not in(" + strValue + "))";
                strResultWhere += " or (T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002'))";
                strSAMPLEDEPT = "1=1";
            }
            else if (iSample == "1")
            {
                strResultWhere = "(T_MIS_MONITOR_SUBTASK.MONITOR_ID in('000000001','000000002') and T_MIS_MONITOR_RESULT.ITEM_ID in(" + strValue + "))";
                strResultWhere += " or (T_MIS_MONITOR_SUBTASK.MONITOR_ID not in('000000001','000000002') and 1=2)";
                strSAMPLEDEPT = "1=1";
            }
            else
            {
                strResultWhere = "1=1";
                strSAMPLEDEPT = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'";
            }

            string strSql = @"select count(*)
                                                  from T_MIS_MONITOR_TASK
                                                 where exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({0})
                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                                 where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                                       T_MIS_MONITOR_SUBTASK.ID
                                                                   and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                                   and exists
                                                                 (select *
                                                                          from T_MIS_MONITOR_RESULT
                                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                           and ({3})
                                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                                         (select *
                                                                                  from T_BASE_ITEM_INFO
                                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                                   and {4}
                                                                                   and T_BASE_ITEM_INFO.ID =
                                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))
                                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')))";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus, strSampleStatus, strResultWhere, strSAMPLEDEPT);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        public DataTable getSampleTaskCheck_QHD(string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select distinct a.* from T_MIS_MONITOR_TASK a
                              left join T_MIS_MONITOR_SUBTASK b on(a.ID=b.TASK_ID)
                              left join T_MIS_MONITOR_SAMPLE_INFO c on(b.ID=c.SUBTASK_ID)
                              left join T_MIS_MONITOR_RESULT d on(c.ID=d.SAMPLE_ID)
                              left join T_BASE_ITEM_INFO e on(d.ITEM_ID=e.ID)
                              where e.HAS_SUB_ITEM = '0' and e.IS_SAMPLEDEPT = '是' and b.TASK_STATUS in({0}) and d.RESULT_STATUS='{1}'";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        public int getSampleTaskCheckCount_QHD(string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select distinct a.* from T_MIS_MONITOR_TASK a
                              left join T_MIS_MONITOR_SUBTASK b on(a.ID=b.TASK_ID)
                              left join T_MIS_MONITOR_SAMPLE_INFO c on(b.ID=c.SUBTASK_ID)
                              left join T_MIS_MONITOR_RESULT d on(c.ID=d.SAMPLE_ID)
                              left join T_BASE_ITEM_INFO e on(d.ITEM_ID=e.ID)
                              where e.HAS_SUB_ITEM = '0' and e.IS_SAMPLEDEPT = '是' and b.TASK_STATUS in({0}) and d.RESULT_STATUS='{1}'";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 获取监测类别信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，数据审核：duty_other_audit，分析室主任审核：duty_other_analyse_result，技术室主任审核：duty_other_analyse_control</param>
        /// <param name="strTaskId">监测任务Id</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable geResultChecktItemTypeInfo_QHD(string strCurrentUserId, string strFlowCode, string strTaskId, string strTaskStatus, string strSampleStatus, string strResultStatus)
        {
            string strSql = @"select ID, MONITOR_ID, SAMPLE_FINISH_DATE
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'
                                           and T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({1})
                                           and exists (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{3}'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.ID
                                                   and exists (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS =  '{2}'))";
            strSql = string.Format(strSql, strTaskId, strTaskStatus, strResultStatus, strSampleStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取监测类别信息 
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，数据审核：duty_other_audit，分析室主任审核：duty_other_analyse_result，技术室主任审核：duty_other_analyse_control</param>
        /// <param name="strTaskId">监测任务Id</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public DataTable geResultChecktItemTypeInfoEx_QHD(string strCurrentUserId, string strFlowCode, string strTaskId, string strTaskStatus, string strSampleStatus, string strResultStatus)
        {
            string strSql = @"select ID, MONITOR_ID, SAMPLE_FINISH_DATE
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'
                                           and T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({1})
                                           and exists (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{3}'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.ID
                                                   and exists (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS =  '{2}'))";
            strSql = string.Format(strSql, strTaskId, strTaskStatus, strResultStatus, strSampleStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        public DataTable getSampleItemCheckMonitorType_QHD(string strTaskId, string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select distinct a.ID, a.MONITOR_ID, a.SAMPLE_FINISH_DATE
                              from T_MIS_MONITOR_SUBTASK a left join T_MIS_MONITOR_SAMPLE_INFO b on(a.ID=b.SUBTASK_ID)
                              left join T_MIS_MONITOR_RESULT c on(b.ID=c.SAMPLE_ID)
                              left join T_BASE_ITEM_INFO d on(c.ITEM_ID=d.ID)
                              where a.TASK_ID='{0}' and a.TASK_STATUS in({1}) and c.RESULT_STATUS='{2}' and d.HAS_SUB_ITEM = '0' and d.IS_SAMPLEDEPT = '是'";
            strSql = string.Format(strSql, strTaskId, strTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取分析结果校核环节的样品信息
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleCheckInfo_QHD(string strSubTaskId, string strSampleStatus, string strResultStatus, int iIndex, int iCount, string iSample, string strMonitorID)
        {
            string strItemWhere = "";
            string strSAMPLEDEPT = "";
            if (iSample == "0" || iSample == "1")
            {
                if (strMonitorID == "000000001" || strMonitorID == "000000002")
                {
                    string strValue = getSampleItem();
                    if (iSample == "0")
                    {
                        strItemWhere = " and T_MIS_MONITOR_RESULT.ITEM_ID not in(" + strValue + ")";
                    }
                    else
                    {
                        strItemWhere = " and T_MIS_MONITOR_RESULT.ITEM_ID in(" + strValue + ")";
                    }
                }
                else
                {
                    if (iSample == "1")
                    {
                        strItemWhere = " and 1=2";
                    }
                }
                strSAMPLEDEPT = "1=1";
            }
            else
            {
                strSAMPLEDEPT = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'";
            }

            string strSql = @"select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                   and SUBTASK_ID='{0}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           {3}
                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and {4}
                                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}') ORDER BY SAMPLE_CODE";
            strSql = string.Format(strSql, strSubTaskId, strResultStatus, strSampleStatus, strItemWhere, strSAMPLEDEPT);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 获取分析结果校核环节的样品信息数量
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getTaskSimpleCheckInfoCount_QHD(string strSubTaskId, string strSampleStatus, string strResultStatus, string iSample, string strMonitorID)
        {
            string strItemWhere = "";
            string strSAMPLEDEPT = "";
            if (iSample == "0" || iSample == "1")
            {
                if (strMonitorID == "000000001" || strMonitorID == "000000002")
                {
                    string strValue = getSampleItem();
                    if (iSample == "0")
                    {
                        strItemWhere = " and T_MIS_MONITOR_RESULT.ITEM_ID not in(" + strValue + ")";
                    }
                    else
                    {
                        strItemWhere = " and T_MIS_MONITOR_RESULT.ITEM_ID in(" + strValue + ")";
                    }
                }
                else
                {
                    if (iSample == "1")
                    {
                        strItemWhere = " and 1=2";
                    }
                }
                strSAMPLEDEPT = "1=1";
            }
            else
            {
                strSAMPLEDEPT = "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'";
            }

            string strSql = @"select count(*)
                                                  from (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                          and SUBTASK_ID='{0}'
                                                          and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                       {3}
                                                                   and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and {4}
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID))
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')) record";
            strSql = string.Format(strSql, strSubTaskId, strResultStatus, strSampleStatus, strItemWhere, strSAMPLEDEPT);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        public DataTable getSampleCheckInfo_QHD(string strSubTaskId, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select distinct a.* from T_MIS_MONITOR_SAMPLE_INFO a
                              left join T_MIS_MONITOR_RESULT b on(a.ID=b.SAMPLE_ID)
                              left join T_BASE_ITEM_INFO c on(b.ITEM_ID=c.ID)
                              where c.HAS_SUB_ITEM = '0' and c.IS_SAMPLEDEPT = '是' and a.SUBTASK_ID='{0}' and b.RESULT_STATUS='{1}'";
            strSql = string.Format(strSql, strSubTaskId, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        public int getSampleCheckInfoCount_QHD(string strSubTaskId, string strResultStatus)
        {
            string strSql = @"select distinct a.* from T_MIS_MONITOR_SAMPLE_INFO a
                              left join T_MIS_MONITOR_RESULT b on(a.ID=b.SAMPLE_ID)
                              left join T_BASE_ITEM_INFO c on(b.ITEM_ID=c.ID)
                              where c.HAS_SUB_ITEM = '0' and c.IS_SAMPLEDEPT = '是' and a.SUBTASK_ID='{0}' and b.RESULT_STATUS='{1}'";
            strSql = string.Format(strSql, strSubTaskId, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        #endregion

        #region 分析数据审核方法【清远】

        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskCheckInfo_QY(string strCurrentUserId, string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                  from T_MIS_MONITOR_TASK
                                 where exists
                                 (select *
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS in ('{1}')
                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.ID
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and T_MIS_MONITOR_RESULT.REMARK_1 = '{0}'
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID)
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS in ('{2}'))))
                                                           order by CREATE_DATE desc";
            strSql = string.Format(strSql, strCurrentUserId, strTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public int getTaskInfoCheckCount_QY(string strCurrentUserId, string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select count(*)
                                  from T_MIS_MONITOR_TASK
                                 where exists (select *
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS in ('{1}')
                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.ID
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and T_MIS_MONITOR_RESULT.REMARK_1 = '{0}'
                                                           and exists (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID)
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS in
                                                               ('{2}'))))";
            strSql = string.Format(strSql, strCurrentUserId, strTaskStatus, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 获取监测类别信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strTaskId">监测任务Id</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，质控审核：40 分析室主任审核：50</param>
        /// <returns></returns>
        public DataTable geResultChecktItemTypeInfo_QY(string strCurrentUserId, string strTaskId, string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select ID, MONITOR_ID
                                      from T_MIS_MONITOR_SUBTASK
                                     where T_MIS_MONITOR_SUBTASK.TASK_ID = '{1}'
                                       and T_MIS_MONITOR_SUBTASK.TASK_STATUS in ('{2}')
      
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_SAMPLE_INFO
                                             where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                               and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                   T_MIS_MONITOR_SUBTASK.ID
                                               and exists
                                             (select *
                                                      from T_MIS_MONITOR_RESULT
                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                           T_MIS_MONITOR_SAMPLE_INFO.ID
                                                       and REMARK_1 = '{0}'
                                                       and exists
                                                     (select *
                                                              from T_BASE_ITEM_INFO
                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                               and T_BASE_ITEM_INFO.ID =
                                                                   T_MIS_MONITOR_RESULT.ITEM_ID)
                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS in ('{3}')))";
            strSql = string.Format(strSql, strCurrentUserId, strTaskId, strTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取分析结果校核环节的样品信息
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleCheckInfo_QY(string strSubTaskId, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and SUBTASK_ID='{0}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS in ('{1}'))";
            strSql = string.Format(strSql, strSubTaskId, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 获取分析结果校核环节的样品信息数量
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getTaskSimpleCheckInfoCount_QY(string strSubTaskId, string strResultStatus)
        {
            string strSql = @"select count(*)
                                                  from (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                          and SUBTASK_ID='{0}'
                                                          and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                   and exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID)
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS in ('{1}'))) record";
            strSql = string.Format(strSql, strSubTaskId, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 判断任务是否有外控样品
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <returns></returns>
        public bool CheckTaskHasOuterQcSample(string strTaskId, string strSubTaskID)
        {
            string strSql = @"select *
                                  from T_MIS_MONITOR_RESULT
                                 where QC_TYPE <> '0'
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
                                           and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where T_MIS_MONITOR_SUBTASK.ID =
                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                   and {0} and {1}))";
            strSql = string.Format(strSql, (strTaskId != "" ? "T_MIS_MONITOR_SUBTASK.TASK_ID = '" + strTaskId + "'" : "1=1"), (strSubTaskID != "" ? "T_MIS_MONITOR_SUBTASK.ID = '" + strSubTaskID + "'" : "1=1"));
            return SqlHelper.ExecuteDataTable(strSql).Rows.Count > 0 ? true : false;
        }
        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strTaskStatus">任务状态 分析环节：03</param>
        /// <param name="strCurrResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool SendTaskCheckToNextFlow_QY(string strTaskId, string strCurrentUserId, string strTaskStatus, string strCurrResultStatus, string strNextResultStatus)
        {
            string strSql = @"update T_MIS_MONITOR_RESULT
                                   set T_MIS_MONITOR_RESULT.RESULT_STATUS = '{4}',T_MIS_MONITOR_RESULT.TASK_TYPE = '发送',APPARTUS_TIME_USED = CONVERT(VARCHAR(19),GETDATE(),120)
                                 where T_MIS_MONITOR_RESULT.RESULT_STATUS = '{3}'
                                   and T_MIS_MONITOR_RESULT.REMARK_1 = '{1}'
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
                                           and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where T_MIS_MONITOR_SUBTASK.ID =
                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                   and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{2}'
                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            strSql = string.Format(strSql, strTaskId, strCurrentUserId, strTaskStatus, strCurrResultStatus, strNextResultStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        #endregion

        #region 分析结果质控审核
        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskQcCheckInfo(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strSampleStatus, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                from T_MIS_MONITOR_TASK
                                where exists
                                (select *
                                        from T_MIS_MONITOR_SUBTASK
                                        where T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({0})
                                        and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                        and exists
                                        (select *
                                                from T_MIS_MONITOR_SAMPLE_INFO
                                                where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                    T_MIS_MONITOR_SUBTASK.ID
                                                and exists
                                                (select *
                                                        from T_MIS_MONITOR_RESULT
                                                        where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                            T_MIS_MONITOR_SAMPLE_INFO.ID
                                                            and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                        and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                        (select *
                                                                from T_BASE_ITEM_INFO
                                                                where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                and T_BASE_ITEM_INFO.ID =
                                                                    T_MIS_MONITOR_RESULT.ITEM_ID))
                                                        and  T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')))";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus, strSampleStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskInfoQcCheckCount(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strSampleStatus, string strResultStatus)
        {
            string strSql = @"select count(*)
                                                  from T_MIS_MONITOR_TASK
                                                 where exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS in ({0})
                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{2}'
                                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                                       T_MIS_MONITOR_SUBTASK.ID
                                                                   and exists
                                                                 (select *
                                                                          from T_MIS_MONITOR_RESULT
                                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                                and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists 
                                                                         (select *
                                                                                  from T_BASE_ITEM_INFO
                                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                                   and T_BASE_ITEM_INFO.ID =
                                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))
                                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')))";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus, strSampleStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        public DataTable getTaskQcCheckInfo_QHD(string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                from T_MIS_MONITOR_TASK
                                where exists
                                (select *
                                        from T_MIS_MONITOR_SUBTASK
                                        where T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                        and T_MIS_MONITOR_SUBTASK.TASK_STATUS in({1})
                                        and exists
                                        (select *
                                                from T_MIS_MONITOR_SAMPLE_INFO
                                                where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                    T_MIS_MONITOR_SUBTASK.ID
                                                and exists
                                                (select *
                                                        from T_MIS_MONITOR_RESULT
                                                        where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                            T_MIS_MONITOR_SAMPLE_INFO.ID
                                                            and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                        and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                        (select *
                                                                from T_BASE_ITEM_INFO
                                                                where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                and T_BASE_ITEM_INFO.ID =
                                                                    T_MIS_MONITOR_RESULT.ITEM_ID))
                                                        and  T_MIS_MONITOR_RESULT.RESULT_STATUS = '{0}')))";
            strSql = string.Format(strSql, strResultStatus, strTaskStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        public int getTaskQcCheckInfoCount_QHD(string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select count(*)
                                from T_MIS_MONITOR_TASK
                                where exists
                                (select *
                                        from T_MIS_MONITOR_SUBTASK
                                        where T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                        and T_MIS_MONITOR_SUBTASK.TASK_STATUS in({1})
                                        and exists
                                        (select *
                                                from T_MIS_MONITOR_SAMPLE_INFO
                                                where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                    T_MIS_MONITOR_SUBTASK.ID
                                                and exists
                                                (select *
                                                        from T_MIS_MONITOR_RESULT
                                                        where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                            T_MIS_MONITOR_SAMPLE_INFO.ID
                                                            and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                        and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                        (select *
                                                                from T_BASE_ITEM_INFO
                                                                where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                and T_BASE_ITEM_INFO.ID =
                                                                    T_MIS_MONITOR_RESULT.ITEM_ID))
                                                        and  T_MIS_MONITOR_RESULT.RESULT_STATUS = '{0}')))";
            strSql = string.Format(strSql, strResultStatus, strTaskStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        public DataTable getMonitorQcCheckInfo_QHD(string strTaskID, string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select ID,MONITOR_ID
                                        from T_MIS_MONITOR_SUBTASK
                                        where T_MIS_MONITOR_SUBTASK.TASK_ID = '{2}'
                                        and T_MIS_MONITOR_SUBTASK.TASK_STATUS in({1})
                                        and exists
                                        (select *
                                                from T_MIS_MONITOR_SAMPLE_INFO
                                                where T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                    T_MIS_MONITOR_SUBTASK.ID
                                                and exists
                                                (select *
                                                        from T_MIS_MONITOR_RESULT
                                                        where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                            T_MIS_MONITOR_SAMPLE_INFO.ID
                                                            and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                        and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                        (select *
                                                                from T_BASE_ITEM_INFO
                                                                where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                and T_BASE_ITEM_INFO.ID =
                                                                    T_MIS_MONITOR_RESULT.ITEM_ID))
                                                        and  T_MIS_MONITOR_RESULT.RESULT_STATUS = '{0}'))";
            strSql = string.Format(strSql, strResultStatus, strTaskStatus, strTaskID);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取分析结果质控审核环节的样品信息
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleQcCheckInfo(string strTaskId, string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                           and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{1}'
		                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID='{0}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                                            and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                            and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or exists 
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM<>'1'
                                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                                           and  T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'))";
            strSql = string.Format(strSql, strTaskId, strTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }


        /// <summary>
        /// 获取分析结果质控审核环节的样品信息数量
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strCurrentUserId">当前用户</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskSimpleQcCheckInfoCount(string strTaskId, string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select count(*)
                                                  from (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_SUBTASK
                                                                 where T_MIS_MONITOR_SUBTASK.ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                   and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{1}'
                                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}' 
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                        and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                                   and (T_MIS_MONITOR_RESULT.reamrk_4='1' or exists 
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM<>'1'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID))
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}')) record";
            strSql = string.Format(strSql, strTaskId, strTaskStatus, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        public DataTable getTaskSampleInfo(string strTaskID)
        {
            string strSql = @"select sample.* 
                            from T_MIS_MONITOR_SAMPLE_INFO sample 
                            left join T_MIS_MONITOR_SUBTASK subtask on(sample.SUBTASK_ID=subtask.ID)
                            where subtask.TASK_ID='{0}' and sample.QC_TYPE='0'";
            strSql = string.Format(strSql, strTaskID);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        public DataTable getTaskSampleInfo_One(string strTaskID)
        {
            string strSql = @"select sample.* 
                            from T_MIS_MONITOR_SAMPLE_INFO sample 
                            left join T_MIS_MONITOR_SUBTASK subtask on(sample.SUBTASK_ID=subtask.ID)
                            where subtask.TASK_ID in({0}) and sample.QC_TYPE='0'";
            strSql = string.Format(strSql, strTaskID);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取分析结果质控审核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskItemQcCheckInfo(string strSampleId, string strResultStatus)
        {
            string strSql = @"select sum_record.*,
                                           APPARATUS_INFO.APPARATUS_CODE,
                                           APPARATUS_INFO.APPARATUS_NAME,
                                           APPARATUS_INFO.LOWER_CHECKOUT
                                      from (select record.*,
                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                   T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                           T_MIS_MONITOR_RESULT.ITEM_ID,SAMPLE_ID,
                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                           ITEM_RESULT,
                                                           QC,
                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                           STANDARD_ID,
                                                           T_BASE_METHOD_INFO.METHOD_CODE,
                                                           RESULT_STATUS,REMARK_2,REMARK_5,T_MIS_MONITOR_RESULT.RESULT_CHECKOUT
                                                      from T_MIS_MONITOR_RESULT
                                                      left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                      left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                       and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                                     (select *
                                                              from T_BASE_ITEM_INFO
                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                               and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM<>'1'
                                                               and T_BASE_ITEM_INFO.ID =
                                                                   T_MIS_MONITOR_RESULT.ITEM_ID))) record
                                              left join T_MIS_MONITOR_RESULT_APP
                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                      left join (select (select APPARATUS_CODE
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                        (select NAME
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                        LOWER_CHECKOUT,
                                                        ITEM_ID,
                                                        ANALYSIS_METHOD_ID,ID
                                                   from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                       and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSampleId, strResultStatus);
            //return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
            return SqlHelper.ExecuteDataTable(strSql);
        }
        public DataTable getTaskItemQcCheckInfo_MAS(string strResultID)
        {
            string strSql = @"select sum_record.*,
                                           APPARATUS_INFO.APPARATUS_CODE,
                                           APPARATUS_INFO.APPARATUS_NAME,
                                           APPARATUS_INFO.LOWER_CHECKOUT
                                      from (select record.*,
                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                   T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                           T_MIS_MONITOR_RESULT.ITEM_ID,SAMPLE_ID,
                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                           ITEM_RESULT,
                                                           QC,
                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                           STANDARD_ID,
                                                           T_BASE_METHOD_INFO.METHOD_CODE,
                                                           RESULT_STATUS,REMARK_2,REMARK_5,T_MIS_MONITOR_RESULT.RESULT_CHECKOUT
                                                      from T_MIS_MONITOR_RESULT
                                                      left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                      left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                     where T_MIS_MONITOR_RESULT.ID = '{0}'
                                                       and T_MIS_MONITOR_RESULT.QC_TYPE in('0','1','2','3','4','5','6')
                                                       ) record
                                              left join T_MIS_MONITOR_RESULT_APP
                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                      left join (select (select APPARATUS_CODE
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                        (select NAME
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                        LOWER_CHECKOUT,
                                                        ITEM_ID,
                                                        ANALYSIS_METHOD_ID,ID
                                                   from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                       and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strResultID);

            return SqlHelper.ExecuteDataTable(strSql);
        }

        public DataTable getTaskItemQcCheckInfo(string strSampleID)
        {
            string strSql = @"select sum_record.*,
                                           APPARATUS_INFO.APPARATUS_CODE,
                                           APPARATUS_INFO.APPARATUS_NAME,
                                           APPARATUS_INFO.LOWER_CHECKOUT
                                      from (select record.*,
                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                   T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                           T_MIS_MONITOR_RESULT.ITEM_ID,SAMPLE_ID,
                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                           ITEM_RESULT,
                                                           QC,
                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                           STANDARD_ID,
                                                           T_BASE_METHOD_INFO.METHOD_CODE,
                                                           RESULT_STATUS,REMARK_2,REMARK_5,T_MIS_MONITOR_RESULT.RESULT_CHECKOUT
                                                      from T_MIS_MONITOR_RESULT
                                                      left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                      left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                       and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                       ) record
                                              left join T_MIS_MONITOR_RESULT_APP
                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                      left join (select (select APPARATUS_CODE
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                        (select NAME
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                        LOWER_CHECKOUT,
                                                        ITEM_ID,
                                                        ANALYSIS_METHOD_ID,ID
                                                   from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                       and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSampleID);

            return SqlHelper.ExecuteDataTable(strSql);
        }

        public DataTable getTaskItemQcCheckInfo_One(string strSampleID)
        {
            string strSql = @"select sum_record.*,
                                           APPARATUS_INFO.APPARATUS_CODE,
                                           APPARATUS_INFO.APPARATUS_NAME,
                                           APPARATUS_INFO.LOWER_CHECKOUT
                                      from (select record.*,
                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                   T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                           T_MIS_MONITOR_RESULT.ITEM_ID,SAMPLE_ID,
                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                           ITEM_RESULT,
                                                           QC,
                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                           STANDARD_ID,
                                                           T_BASE_METHOD_INFO.METHOD_CODE,
                                                           RESULT_STATUS,REMARK_2,REMARK_5,T_MIS_MONITOR_RESULT.RESULT_CHECKOUT
                                                      from T_MIS_MONITOR_RESULT
                                                      left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                      left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID  in ({0})
                                                       and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                       ) record
                                              left join T_MIS_MONITOR_RESULT_APP
                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                      left join (select (select APPARATUS_CODE
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                        (select NAME
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                        LOWER_CHECKOUT,
                                                        ITEM_ID,
                                                        ANALYSIS_METHOD_ID,ID
                                                   from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                       and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSampleID);

            return SqlHelper.ExecuteDataTable(strSql);
        }


        public DataTable getTaskItemQcCheckInfo_MAS_ONE(string strResultID, string strSampleId)
        {
            string strSql = @"select sum_record.*,
                                           APPARATUS_INFO.APPARATUS_CODE,
                                           APPARATUS_INFO.APPARATUS_NAME,
                                           APPARATUS_INFO.LOWER_CHECKOUT
                                      from (select record.*,
                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                   T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                           T_MIS_MONITOR_RESULT.ITEM_ID,SAMPLE_ID,
                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                           ITEM_RESULT,
                                                           QC,
                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                           STANDARD_ID,
                                                           T_BASE_METHOD_INFO.METHOD_CODE,
                                                           RESULT_STATUS,REMARK_2,REMARK_5,T_MIS_MONITOR_RESULT.RESULT_CHECKOUT
                                                      from T_MIS_MONITOR_RESULT
                                                      left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                      left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                    where T_MIS_MONITOR_RESULT.ID in( {0})
                                                       and T_MIS_MONITOR_RESULT.QC_TYPE in('0','1','2','3','4','5','6') and T_MIS_MONITOR_RESULT.SAMPLE_ID='{1}'
                                                       ) record
                                              left join T_MIS_MONITOR_RESULT_APP
                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                      left join (select (select APPARATUS_CODE
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                        (select NAME
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                        LOWER_CHECKOUT,
                                                        ITEM_ID,
                                                        ANALYSIS_METHOD_ID,ID
                                                   from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                       and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strResultID, strSampleId);

            return SqlHelper.ExecuteDataTable(strSql);
        }



        /// <summary>
        /// 获取分析结果质控审核环节的监测项目数量
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskItemCheckQcInfoCount(string strSampleId, string strResultStatus)
        {
            string strSql = @"select count(*)
                                          from (select sum_record.*,
                                                       APPARATUS_INFO.APPARATUS_CODE,
                                                       APPARATUS_INFO.APPARATUS_NAME,
                                                       APPARATUS_INFO.LOWER_CHECKOUT
                                                  from (select record.*,
                                                               T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                               T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                               T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                               T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                                          from (select T_MIS_MONITOR_RESULT.ID,
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID,
                                                                       T_SYS_DICT.DICT_TEXT as UNIT,
                                                                       ITEM_RESULT,
                                                                       QC,
                                                                       T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                                       T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                                       STANDARD_ID,
                                                                       T_BASE_METHOD_INFO.METHOD_CODE,
                                                                       RESULT_STATUS
                                                                  from T_MIS_MONITOR_RESULT
                                                                  left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                                  left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                                  left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                                  left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                                   and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                                   and exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID)) record
                                                          left join T_MIS_MONITOR_RESULT_APP
                                                            on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                                  left join (select (select APPARATUS_CODE
                                                                      from T_BASE_APPARATUS_INFO
                                                                     where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                                   (select NAME
                                                                      from T_BASE_APPARATUS_INFO
                                                                     where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                                   LOWER_CHECKOUT,
                                                                   ITEM_ID,
                                                                   ANALYSIS_METHOD_ID,ID
                                                              from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                                    on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                                   and APPARATUS_INFO.ID =
                                                       sum_record.ANALYSIS_METHOD_ID) record_count";
            strSql = string.Format(strSql, strSampleId, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 发送任务至下一环节
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strSendStatus">发送至下一环节状态</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public bool SendQcTaskToNextFlow(string strTaskId, string strCurrentUserId, string strSendStatus, string strFlowCode)
        {
            string strSql = @"update T_MIS_MONITOR_SUBTASK set TASK_STATUS='{1}',TASK_TYPE='发送'
                                         where TASK_ID = '{0}' and TASK_STATUS='03'
                                         ";
            strSql = string.Format(strSql, strTaskId, strSendStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        #endregion

        #region 分析结果质控审核【清远】
        /// <summary>
        /// 发送任务至下一环节 update by ssz
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strSendStatus">发送至下一环节状态</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：duty_other_analyse_control</param>
        /// <returns></returns>
        public bool SendQcTaskToNextFlowForQy(string strTaskId, string strCurrentUserId, string strSendStatus, string strFlowCode)
        {
            string strSql = @"update T_MIS_MONITOR_SUBTASK set TASK_STATUS='{1}',TASK_TYPE='发送'
                                         where TASK_ID = '{0}' AND TASK_STATUS<>'022' AND TASK_STATUS<>'023' AND TASK_STATUS<>'24' AND TASK_STATUS<>'02'
                                          ";
            strSql = string.Format(strSql, strTaskId, strSendStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }

        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：analysis_result_qc_check,分析室主任审核：analysis_result_check</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskQcCheckInfo_QY(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                  from T_MIS_MONITOR_TASK
                                 where exists
                                 (select *
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.ID
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM<>'1'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID)
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')))/*
                                   and ID not in
                                       (select ID
                                          from T_MIS_MONITOR_TASK
                                         where exists
                                         (select *
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                           and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                               T_MIS_MONITOR_SUBTASK.ID
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                   and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                                   and exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM<>'1'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID)
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS <> '{1}'))))*/
                                                                   order by CREATE_DATE desc";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，分析结果校核：duty_other_analyse_result，分析结果质控审核：analysis_result_qc_check,分析室主任审核：analysis_result_check</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03</param>
        /// <param name="strResultStatus">分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public int getTaskInfoQcCheckCount_QY(string strCurrentUserId, string strFlowCode, string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select count(*)
                                  from T_MIS_MONITOR_TASK
                                 where exists
                                 (select *
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.ID
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM<>'1'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID)
                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')))
                                   and ID not in
                                       (select ID
                                          from T_MIS_MONITOR_TASK
                                         where exists
                                         (select *
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                           and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                               T_MIS_MONITOR_SUBTASK.ID
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                   and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                                   and exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM<>'1'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID)
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS <> '{1}'))))";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 将质控审核环节数据发送至下一环节
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strTaskStatus">任务状态</param>
        /// <param name="strCurrResultStatus">当前环节。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">下一环节。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strMark">0: 无限制更新；1：只更新分析类现场项目；2：只更新分析类项目</param>
        /// <returns></returns>
        public bool sendTaskQcCheckInfoToNext_QY(string strTaskId, string strTaskStatus, string strCurrResultStatus, string strNextResultStatus, string strType, string strMark)
        {
            string strSql = @"update T_MIS_MONITOR_RESULT
                               set T_MIS_MONITOR_RESULT.RESULT_STATUS = '{3}',T_MIS_MONITOR_RESULT.TASK_TYPE = '{5}'
                             where T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                               and exists
                             (select *
                                      from T_BASE_ITEM_INFO
                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                       and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                       and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID {4})
                               and exists
                             (select *
                                      from T_MIS_MONITOR_SAMPLE_INFO
                                     where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                       and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                       and exists
                                     (select *
                                              from T_MIS_MONITOR_SUBTASK
                                             where (T_MIS_MONITOR_SUBTASK.ID =
                                                   T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID or T_MIS_MONITOR_SUBTASK.REMARK1 =
                                                   T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)
                                               and T_MIS_MONITOR_SUBTASK.TASK_STATUS in ('{1}')
                                               and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            if (strMark == "0")
            {
                strSql = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, "", strType);
            }
            else if (strMark == "1")
            {
                strSql = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, " and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM='1'", strType);
            }
            else
            {
                strSql = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus, " and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM='0'", strType);
            }

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        #endregion

        #region 技术室主任审核【秦皇岛】

        /// <summary>
        /// 获取分析结果质控审核环节的样品信息
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleQcCheckInfo_QHD(string strSubTaskId, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where  exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                           and T_MIS_MONITOR_SUBTASK.ID='{0}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                                           and  T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')) ORDER BY SAMPLE_CODE";
            strSql = string.Format(strSql, strSubTaskId, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }

        /// <summary>
        /// 获取分析结果质控审核环节的样品信息数量
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskSimpleQcCheckInfoCount_QHD(string strSubTaskId, string strResultStatus)
        {
            string strSql = @"select count(*)
                                                  from (select *
                                                          from T_MIS_MONITOR_SAMPLE_INFO
                                                         where exists
                                                         (select *
                                                                  from T_MIS_MONITOR_SUBTASK
                                                                 where T_MIS_MONITOR_SUBTASK.ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                   and  T_MIS_MONITOR_SUBTASK.ID = '{0}' 
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_RESULT
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                       T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                   and exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID)
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')) record";
            strSql = string.Format(strSql, strSubTaskId, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 获取分析结果质控审核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskItemQcCheckInfo_QHD(string strSampleId, string strResultStatus)
        {
            string strSql = @"select sum_record.*,
                                           APPARATUS_INFO.APPARATUS_CODE,
                                           APPARATUS_INFO.APPARATUS_NAME,
                                           APPARATUS_INFO.LOWER_CHECKOUT
                                      from (select record.*,
                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                   T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                           T_MIS_MONITOR_RESULT.ITEM_ID,
                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                           ITEM_RESULT,
                                                           QC,
                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                           STANDARD_ID,
                                                           T_BASE_METHOD_INFO.METHOD_CODE,
                                                           RESULT_STATUS,REMARK_2
                                                      from T_MIS_MONITOR_RESULT
                                                      left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                      left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                      left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                       --and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                       and exists
                                                     (select *
                                                              from T_BASE_ITEM_INFO
                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                               and T_BASE_ITEM_INFO.ID =
                                                                   T_MIS_MONITOR_RESULT.ITEM_ID)) record
                                              left join T_MIS_MONITOR_RESULT_APP
                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                      left join (select (select APPARATUS_CODE
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                        (select NAME
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                        LOWER_CHECKOUT,
                                                        ITEM_ID,
                                                        ANALYSIS_METHOD_ID,ID
                                                   from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                       and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSampleId, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 判断任务是否可以发送至下环节
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strResultStatus">分析结果状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool TaskCanSendInQcCheck_QHD(string strTaskId, string strCurrentUserId, string strResultStatus, bool b)
        {
            string strSql = @"select * from  T_MIS_MONITOR_RESULT
                                               where T_MIS_MONITOR_RESULT.RESULT_STATUS <> '{1}'
                                                 and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                               (select *
                                                        from T_BASE_ITEM_INFO
                                                       where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                         and {2}
                                                         and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                                 and exists
                                               (select *
                                                        from T_MIS_MONITOR_SAMPLE_INFO
                                                       where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                         --and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                         and exists
                                                       (select *
                                                                from T_MIS_MONITOR_SUBTASK
                                                               where T_MIS_MONITOR_SUBTASK.ID =
                                                                     T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                 and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            if (b)
                strSql = string.Format(strSql, strTaskId, strResultStatus, "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'");
            else
                strSql = string.Format(strSql, strTaskId, strResultStatus, "1=1");
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            return dt.Rows.Count > 0 ? false : true;
        }
        /// <summary>
        /// 判断任务是否可以发送到下一环节（状态不一致无法发送） Create By：weilin 2014-09-22
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="strResultStatus"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsCanSendToNext(string strTaskId, string strResultStatus, bool b)
        {
            string strSql = @"select * from  T_MIS_MONITOR_RESULT
                                               where T_MIS_MONITOR_RESULT.RESULT_STATUS <> '{1}'
                                                 and (T_MIS_MONITOR_RESULT.remark_4='1' or exists
                                               (select *
                                                        from T_BASE_ITEM_INFO
                                                       where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                         and {2}
                                                         and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                                 and exists
                                               (select *
                                                        from T_MIS_MONITOR_SAMPLE_INFO
                                                       where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                         --and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                         and exists
                                                       (select *
                                                                from T_MIS_MONITOR_SUBTASK
                                                               where T_MIS_MONITOR_SUBTASK.ID =
                                                                     T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                 and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            if (b)
                strSql = string.Format(strSql, strTaskId, strResultStatus, "T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否' and T_BASE_ITEM_INFO.IS_ANYSCENE_ITEM='0'");
            else
                strSql = string.Format(strSql, strTaskId, strResultStatus, "1=1");
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            return dt.Rows.Count > 0 ? false : true;
        }

        /// <summary>
        /// 子任务返回
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strCurrentUserId">当前登录系统的用户ID</param>
        /// <param name="strFlowCode">环节代码，分析任务分配：duty_other_analyse，数据审核：duty_other_audit，分析室主任审核：duty_other_analyse_result，技术室主任审核：duty_other_analyse_control</param>
        /// <param name="strCurrResultStatus">当前结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strBackResultStatus">上一环节结果数据状态 分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool TaskGoBackInQcCheck_QHD(string strTaskId, string strCurrentUserId, string strFlowCode, string strSampleStatus, string strCurrResultStatus, string strBackResultStatus)
        {
            string strSql = @"update T_MIS_MONITOR_RESULT
                                             set T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                           where T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                             and (T_MIS_MONITOR_RESULT.remark_4='1' or exists 
                                           (select *
                                                    from T_BASE_ITEM_INFO
                                                   where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                     and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                     and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                             and exists
                                           (select *
                                                    from T_MIS_MONITOR_SAMPLE_INFO
                                                   where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                     and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '{3}'
                                                     and exists
                                                   (select *
                                                            from T_MIS_MONITOR_SUBTASK
                                                           where T_MIS_MONITOR_SUBTASK.ID =
                                                                 T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                             and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            strSql = string.Format(strSql, strTaskId, strCurrResultStatus, strBackResultStatus, strSampleStatus);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        //审核意见
        public bool Update_Info(string strTaskId, string Rtn_Content)
        {
            string sql = "update T_MIS_MONITOR_TASK set REMARK3='" + Rtn_Content + "' where ID='" + strTaskId + "'";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql) > 0 ? true : false;
        }
        #endregion

        #region 采样环节现场监测项目审核方法【郑州】


        /// <summary>
        /// 根据现场监测项目获取采样环节的任务信息
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strFlowCode">环节代码：sample_result_qccheck</param>
        /// <param name="strTaskStatus">任务状态</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页条码</param>
        /// <returns></returns>
        public DataTable getTaskInfoByDeptItem(string strCurrentUserId, string strFlowCode, string strTaskStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                  from T_MIS_MONITOR_TASK
                                 where exists
                                 (select *
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.REMARK1
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and (T_MIS_MONITOR_RESULT.REMARK_4 is null or T_MIS_MONITOR_RESULT.REMARK_4 <>'1')
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))))";
            strSql = string.Format(strSql, strTaskStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 根据现场监测项目获取采样环节的任务信息数量
        /// </summary>
        /// <param name="strCurrentUserId">当前用户ID</param>
        /// <param name="strFlowCode">环节代码：sample_result_qccheck</param>
        /// <param name="strTaskStatus">任务状态</param>
        /// <returns></returns>
        public int getTaskInfoByDeptItemCount(string strCurrentUserId, string strFlowCode, string strTaskStatus)
        {
            string strSql = @"select count(*)
                                  from T_MIS_MONITOR_TASK
                                 where exists
                                 (select *
                                          from T_MIS_MONITOR_SUBTASK
                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                       T_MIS_MONITOR_SUBTASK.REMARK1
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                           and (T_MIS_MONITOR_RESULT.REMARK_4 is null or T_MIS_MONITOR_RESULT.REMARK_4 <>'1')
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'
                                                                   and T_BASE_ITEM_INFO.ID =
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID))))";
            strSql = string.Format(strSql, strTaskStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 根据现场监测项目获取采样环节的样品信息
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strFlowCode">环节代码：sample_result_qccheck</param>
        /// <param name="strCurrentUserId">任务状态</param>
        /// <returns></returns>
        public DataTable getSampleInfoByDeptItem(string strTaskId, string strFlowCode, string strCurrentUserId)
        {
            string strSql = @"select *
                                          from T_MIS_MONITOR_SAMPLE_INFO
                                         where QC_TYPE = '0'
                                           and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                           and exists (select *
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where T_MIS_MONITOR_SUBTASK.ID =
                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                      --and T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                      --    T_MIS_MONITOR_SUBTASK.REMARK1
                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}')
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_RESULT
                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                                   and (T_MIS_MONITOR_RESULT.REMARK_4 is null or T_MIS_MONITOR_RESULT.REMARK_4 <>'1')
                                                   and exists
                                                 (select *
                                                          from T_BASE_ITEM_INFO
                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                           and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'
                                                           and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))";
            strSql = string.Format(strSql, strTaskId);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取现场项目审核环节中的现场项目信息
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <returns></returns>
        public DataTable getItemInfoByDeptItem(string strSampleId)
        {
            string strSql = @"select T_MIS_MONITOR_RESULT.ID,
                                   T_MIS_MONITOR_RESULT.ITEM_ID,
                                   ITEM_NAME =
                                   (select ITEM_NAME from T_BASE_ITEM_INFO where ID = T_MIS_MONITOR_RESULT.ITEM_ID),
                                   T_SYS_DICT.DICT_TEXT as UNIT,
                                   ITEM_RESULT,
                                   SAMPLING_MANAGER_NAME =
                                   (select top 1 REAL_NAME
                                      from T_SYS_USER
                                     where ID in
                                           (select SAMPLING_MANAGER_ID
                                              from T_MIS_MONITOR_SUBTASK
                                             where T_MIS_MONITOR_SUBTASK.ID in
                                                   (select SUBTASK_ID
                                                      from T_MIS_MONITOR_SAMPLE_INFO
                                                     where T_MIS_MONITOR_SAMPLE_INFO.ID in
                                                           (T_MIS_MONITOR_RESULT.SAMPLE_ID)))),
                                   SAMPLE_ACCESS_NAME =
                                   (select top 1 SAMPLING_MAN
                                      from T_MIS_MONITOR_SUBTASK
                                     where T_MIS_MONITOR_SUBTASK.ID in
                                           (select SUBTASK_ID
                                              from T_MIS_MONITOR_SAMPLE_INFO
                                             where T_MIS_MONITOR_SAMPLE_INFO.ID in
                                                   (T_MIS_MONITOR_RESULT.SAMPLE_ID))),
                                   SAMPLE_ACCESS_DATE =
                                   (select top 1 SAMPLE_ACCESS_DATE
                                      from T_MIS_MONITOR_SUBTASK
                                     where T_MIS_MONITOR_SUBTASK.ID in
                                           (select SUBTASK_ID
                                              from T_MIS_MONITOR_SAMPLE_INFO
                                             where T_MIS_MONITOR_SAMPLE_INFO.ID in
                                                   (T_MIS_MONITOR_RESULT.SAMPLE_ID)))
                              from T_MIS_MONITOR_RESULT
                              left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                              left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                             where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                               and (T_MIS_MONITOR_RESULT.REMARK_4 is null or T_MIS_MONITOR_RESULT.REMARK_4 <>'1')
                               and exists
                             (select *
                                      from T_BASE_ITEM_INFO
                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                       and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'
                                       and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)";
            strSql = string.Format(strSql, strSampleId);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 将现场项目审核任务发送【退回】给下环节【上环节】
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <param name="strCurrentSubTaskStatus"></param>
        /// <param name="strNextSubTaskStatus"></param>
        /// <returns></returns>
        public bool TaskGoBackInSampleResultQcCheck(string strTaskId, string strCurrentSubTaskStatus, string strNextSubTaskStatus)
        {
            string strSql = @"update T_MIS_MONITOR_SUBTASK set TASK_STATUS='{2}' where TASK_ID='{0}' and TASK_STATUS='{1}'";
            strSql = string.Format(strSql, strTaskId, strCurrentSubTaskStatus, strNextSubTaskStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        /// <summary>
        /// 根据任务设置现场采样项目在分析结果表中的状态
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strResultStatus">要设置的结果状态</param>
        /// <returns></returns>
        public bool SetSampleResultStatus(string strTaskId, string strSubTaskStatus, string strResultStatus)
        {
            string strSql = @"update T_MIS_MONITOR_RESULT
                                           set RESULT_STATUS = '{2}'
                                         where (T_MIS_MONITOR_RESULT.remark_4<>'1' or T_MIS_MONITOR_RESULT.remark_4 is null)
                                                and  exists (select *
                                                  from T_BASE_ITEM_INFO
                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'
                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{1}'
                                                           and T_MIS_MONITOR_SUBTASK.REMARK1 =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            strSql = string.Format(strSql, strTaskId, strSubTaskStatus, strResultStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        #endregion

        #region 质量负责人审核【郑州】
        /// <summary>
        /// 分析结果校核环节获取任务信息 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskQcCheckInfo_ZZ(string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                from T_MIS_MONITOR_TASK
                                where exists
                                (select *
                                        from T_MIS_MONITOR_SUBTASK
                                        where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                        and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                        and exists
                                        (select *
                                                from T_MIS_MONITOR_SAMPLE_INFO
                                                where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                and (T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                    T_MIS_MONITOR_SUBTASK.ID or T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                    T_MIS_MONITOR_SUBTASK.REMARK1)
                                                and exists
                                                (select *
                                                        from T_MIS_MONITOR_RESULT
                                                        where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                            T_MIS_MONITOR_SAMPLE_INFO.ID
                                                            and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                        and exists
                                                        (select *
                                                                from T_BASE_ITEM_INFO
                                                                where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                and T_BASE_ITEM_INFO.ID =
                                                                    T_MIS_MONITOR_RESULT.ITEM_ID)
                                                        and  T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')))";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }
        /// <summary>
        /// 分析结果校核环节获取任务信息总记录数量 by 熊卫华 2013.01.16
        /// </summary>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskInfoQcCheckCount_ZZ(string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select count(*)
                                                  from T_MIS_MONITOR_TASK
                                                 where exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}'
                                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                                           and exists
                                                         (select *
                                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                                   and (T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                                    T_MIS_MONITOR_SUBTASK.ID or T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID =
                                                                    T_MIS_MONITOR_SUBTASK.REMARK1)
                                                                   and exists
                                                                 (select *
                                                                          from T_MIS_MONITOR_RESULT
                                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                                               T_MIS_MONITOR_SAMPLE_INFO.ID
                                                                                and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                                           and exists
                                                                         (select *
                                                                                  from T_BASE_ITEM_INFO
                                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                                   --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                                   and T_BASE_ITEM_INFO.ID =
                                                                                       T_MIS_MONITOR_RESULT.ITEM_ID)
                                                                           and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}')))";
            strSql = string.Format(strSql, strTaskStatus, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }

        /// <summary>
        /// 获取分析结果质控审核环节的样品信息
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskSimpleQcCheckInfo_ZZ(string strTaskId, string strTaskStatus, string strResultStatus, int iIndex, int iCount)
        {
            string strSql = @"select *
                                                  from T_MIS_MONITOR_SAMPLE_INFO
                                                 where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_SUBTASK
                                                         where (T_MIS_MONITOR_SUBTASK.ID =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID or T_MIS_MONITOR_SUBTASK.REMARK1 =
                                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)
                                                           and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{1}'
		                                                   and T_MIS_MONITOR_SUBTASK.TASK_ID='{0}'
                                                   and exists
                                                 (select *
                                                          from T_MIS_MONITOR_RESULT
                                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID
                                                            and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                            and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                           and exists
                                                         (select *
                                                                  from T_BASE_ITEM_INFO
                                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                   --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                                           and  T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'))";
            strSql = string.Format(strSql, strTaskId, strTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSql, iIndex, iCount));
        }

        /// <summary>
        /// 获取分析结果质控审核环节的样品信息数量
        /// </summary>
        /// <param name="strTaskId">任务ＩＤ</param>
        /// <param name="strTaskStatus">任务状态。分析环节：03,质控审核：04</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskSimpleQcCheckInfoCount_ZZ(string strTaskId, string strTaskStatus, string strResultStatus)
        {
            string strSql = @"select count(*)
                                    from (select *
                                            from T_MIS_MONITOR_SAMPLE_INFO
                                            where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                            and exists
                                            (select *
                                                    from T_MIS_MONITOR_SUBTASK
                                                    where （T_MIS_MONITOR_SUBTASK.ID =
                                                        T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID or T_MIS_MONITOR_SUBTASK.REMARK1 =
                                                        T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID)
                                                    and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{1}'
                                                    and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}' 
                                            and exists
                                            (select *
                                                    from T_MIS_MONITOR_RESULT
                                                    where T_MIS_MONITOR_RESULT.SAMPLE_ID =
                                                        T_MIS_MONITOR_SAMPLE_INFO.ID
                                                        and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                    and exists
                                                    (select *
                                                            from T_BASE_ITEM_INFO
                                                            where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                            --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                            and T_BASE_ITEM_INFO.ID =
                                                                T_MIS_MONITOR_RESULT.ITEM_ID)
                                                    and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}')) record";
            strSql = string.Format(strSql, strTaskId, strTaskStatus, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 获取分析结果质控审核环节的监测项目信息
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns></returns>
        public DataTable getTaskItemQcCheckInfo_ZZ(string strSampleId, string strResultStatus)
        {
            string strSql = @"select sum_record.*,
                                           APPARATUS_INFO.APPARATUS_CODE,
                                           APPARATUS_INFO.APPARATUS_NAME,
                                           APPARATUS_INFO.LOWER_CHECKOUT
                                      from (select record.*,
                                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                   T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                              from (select T_MIS_MONITOR_RESULT.ID,
                                                           T_MIS_MONITOR_RESULT.ITEM_ID,
                                                           T_SYS_DICT.DICT_TEXT as UNIT,
                                                           ITEM_RESULT,
                                                           QC,
                                                           T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                           T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                           STANDARD_ID,
                                                           T_BASE_METHOD_INFO.METHOD_CODE,
                                                           RESULT_STATUS,REMARK_2
                                                      from T_MIS_MONITOR_RESULT
                                                          left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                          left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                          left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                          left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                       and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                       and exists
                                                     (select *
                                                              from T_BASE_ITEM_INFO
                                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                               --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                               and T_BASE_ITEM_INFO.ID =
                                                                   T_MIS_MONITOR_RESULT.ITEM_ID)) record
                                              left join T_MIS_MONITOR_RESULT_APP
                                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                      left join (select (select APPARATUS_CODE
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                        (select NAME
                                                           from T_BASE_APPARATUS_INFO
                                                          where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                        LOWER_CHECKOUT,
                                                        ITEM_ID,
                                                        ANALYSIS_METHOD_ID,ID
                                                   from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                        on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                       and APPARATUS_INFO.ID = sum_record.ANALYSIS_METHOD_ID";
            strSql = string.Format(strSql, strSampleId, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取分析结果质控审核环节的监测项目数量
        /// </summary>
        /// <param name="strSampleId">样品Id</param>
        /// <param name="strResultStatus">分析结果状态 04：技术室质控审核</param>
        /// <returns></returns>
        public int getTaskItemCheckQcInfoCount_ZZ(string strSampleId, string strResultStatus)
        {
            string strSql = @"select count(*)
                                          from (select sum_record.*,
                                                       APPARATUS_INFO.APPARATUS_CODE,
                                                       APPARATUS_INFO.APPARATUS_NAME,
                                                       APPARATUS_INFO.LOWER_CHECKOUT
                                                  from (select record.*,
                                                               T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                                               T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                                               T_MIS_MONITOR_RESULT_APP.ASKING_DATE,
                                                               T_MIS_MONITOR_RESULT_APP.FINISH_DATE
                                                          from (select T_MIS_MONITOR_RESULT.ID,
                                                                       T_MIS_MONITOR_RESULT.ITEM_ID,
                                                                       T_SYS_DICT.DICT_TEXT as UNIT,
                                                                       ITEM_RESULT,
                                                                       QC,
                                                                       T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID,
                                                                       T_BASE_METHOD_ANALYSIS.ANALYSIS_NAME,
                                                                       STANDARD_ID,
                                                                       T_BASE_METHOD_INFO.METHOD_CODE,
                                                                       RESULT_STATUS
                                                                  from T_MIS_MONITOR_RESULT
                                                                  left join T_BASE_ITEM_ANALYSIS on(T_MIS_MONITOR_RESULT.ANALYSIS_METHOD_ID=T_BASE_ITEM_ANALYSIS.ID)
                                                                  left join T_BASE_METHOD_ANALYSIS on(T_BASE_ITEM_ANALYSIS.ANALYSIS_METHOD_ID=T_BASE_METHOD_ANALYSIS.ID)
                                                                  left join T_BASE_METHOD_INFO on(T_BASE_METHOD_ANALYSIS.METHOD_ID=T_BASE_METHOD_INFO.ID)
                                                                  left join T_SYS_DICT on(T_SYS_DICT.DICT_TYPE='item_unit' and T_BASE_ITEM_ANALYSIS.UNIT=T_SYS_DICT.DICT_CODE)
                                                                 where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                                                   and T_MIS_MONITOR_RESULT.QC_TYPE = '0'
                                                                   and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                                                   and exists
                                                                 (select *
                                                                          from T_BASE_ITEM_INFO
                                                                         where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                                           --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                                           and T_BASE_ITEM_INFO.ID =
                                                                               T_MIS_MONITOR_RESULT.ITEM_ID)) record
                                                          left join T_MIS_MONITOR_RESULT_APP
                                                            on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID) sum_record
                                                  left join (select (select APPARATUS_CODE
                                                                      from T_BASE_APPARATUS_INFO
                                                                     where ID = INSTRUMENT_ID) as APPARATUS_CODE,
                                                                   (select NAME
                                                                      from T_BASE_APPARATUS_INFO
                                                                     where ID = INSTRUMENT_ID) as APPARATUS_NAME,
                                                                   LOWER_CHECKOUT,
                                                                   ITEM_ID,
                                                                   ANALYSIS_METHOD_ID,ID
                                                              from T_BASE_ITEM_ANALYSIS) APPARATUS_INFO
                                                    on APPARATUS_INFO.ITEM_ID = sum_record.ITEM_ID
                                                   and APPARATUS_INFO.ANALYSIS_METHOD_ID =
                                                       sum_record.ID) record_count";
            strSql = string.Format(strSql, strSampleId, strResultStatus);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 在质量负责人审核环节判断任务是否可以发送
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strResultStatus">结果状态 01:分析任务分配 02：分析结果填报 03：分析主任复核 04：质量科审核 05：质量负责人审核</param>
        /// <returns></returns>
        public bool TaskCanSendInQcManagerAudit_ZZ(string strTaskId, string strResultStatus)
        {
            string strSql = @"select * from  T_MIS_MONITOR_RESULT
                                               where T_MIS_MONITOR_RESULT.RESULT_STATUS <> '{1}'
                                                 and exists
                                               (select *
                                                        from T_BASE_ITEM_INFO
                                                       where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                         --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                         and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                                 and exists
                                               (select *
                                                        from T_MIS_MONITOR_SAMPLE_INFO
                                                       where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                         and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                         and exists
                                                       (select *
                                                                from T_MIS_MONITOR_SUBTASK
                                                               where T_MIS_MONITOR_SUBTASK.ID =
                                                                     T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                 and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            strSql = string.Format(strSql, strTaskId, strResultStatus);
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            return dt.Rows.Count > 0 ? false : true;
        }
        /// <summary>
        /// 质量负责人审核环节判断现场采样项目审核环节和分析流程是否已经全部完毕
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strSubTaskStatus">现场项目审核完毕之后子任务状态【24】</param>
        /// <param name="strResultStatus">分析环节完毕之后结果状态【50】</param>
        /// <returns></returns>
        public bool allTaskIsFinish_ZZ(string strTaskId, string strSubTaskStatus, string strResultStatus)
        {
            //判断结果是否已经全部提交完毕
            string strSql = @"select *
                                  from T_MIS_MONITOR_RESULT
                                 where T_MIS_MONITOR_RESULT.RESULT_STATUS <> '{1}'
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
                                           and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                           and exists
                                         (select *
                                                  from T_MIS_MONITOR_SUBTASK
                                                 where TASK_ID = '{0}'
                                                   and T_MIS_MONITOR_SUBTASK.ID =
                                                       T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID))";
            strSql = string.Format(strSql, strTaskId, strResultStatus);
            bool isResultFinish = SqlHelper.ExecuteDataTable(strSql).Rows.Count > 0 ? false : true;

            //判断子任务是否已经全部提交完毕
            strSql = @"select * from T_MIS_MONITOR_SUBTASK 
	                    where TASK_ID = '{0}' and TASK_STATUS<>'09' and TASK_STATUS<>'{1}'";
            strSql = string.Format(strSql, strTaskId, strSubTaskStatus);
            bool isSubTaskFinish = SqlHelper.ExecuteDataTable(strSql).Rows.Count > 0 ? false : true;

            bool isFinish = isResultFinish && isSubTaskFinish;
            return isFinish;
        }
        /// <summary>
        /// 将任务发送至下一环节
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <param name="strTaskStatus">任务状态 分析环节：03</param>
        /// <param name="strCurrResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果录入：20，数据审核：30，分析室主任审核：40，技术室主任审核：50</param>
        /// <returns></returns>
        public bool SendTaskCheckToNextFlow_ZZ(string strTaskId, string strTaskStatus, string strCurrResultStatus, string strNextResultStatus)
        {
            string strSql = @"update T_MIS_MONITOR_RESULT
                                               set T_MIS_MONITOR_RESULT.RESULT_STATUS = '{3}'
                                             where T_MIS_MONITOR_RESULT.RESULT_STATUS = '{2}'
                                               and exists
                                             (select *
                                                      from T_BASE_ITEM_INFO
                                                     where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                       --and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                       and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)
                                               and exists
                                             (select *
                                                      from T_MIS_MONITOR_SAMPLE_INFO
                                                     where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                       and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                       and exists
                                                     (select *
                                                              from T_MIS_MONITOR_SUBTASK
                                                             where T_MIS_MONITOR_SUBTASK.ID =
                                                                   T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                               and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{1}'
                                                               and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            strSql = string.Format(strSql, strTaskId, strTaskStatus, strCurrResultStatus, strNextResultStatus);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }
        #endregion

        #region 样品分析通知单打印功能【郑州】
        /// <summary>
        /// 获取郑州样品分析通知单信息
        /// </summary>
        /// <param name="strSubTaskStatus1">样品交接状态</param>
        /// <param name="strSubTaskStatus2">分析环节状态</param>
        /// <returns></returns>
        public DataTable getSamplesOrderInfo_ZZ(string strSubTaskStatus1, string strSubTaskStatus2)
        {
            string strSql = @"select t1.*
                                  from (select T_MIS_MONITOR_SAMPLE_INFO.ID,
                                               T_MIS_MONITOR_TASK.CREATE_DATE as TASK_DATE,
                                               T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE,
                                               (select COMPANY_NAME
                                                  from T_MIS_MONITOR_TASK_COMPANY
                                                 where ID = T_MIS_MONITOR_TASK.TESTED_COMPANY_ID) as COMPANY_NAME,
                                               SAMPLE_NAME,
                                               T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_TYPE,
                                               dbo.getItemNameBySampleId(T_MIS_MONITOR_SAMPLE_INFO.ID) as ITEM_NAME,
                                               T_MIS_MONITOR_TASK.ASKING_DATE,
                                               (select REAL_NAME
                                                  from T_SYS_USER
                                                 where ID = T_MIS_MONITOR_TASK.PROJECT_ID) as PROJECT_NAME,
                                               T_MIS_MONITOR_SAMPLE_INFO.SAMPLES_ORDER_ISPRINTED
                                          from T_MIS_MONITOR_SAMPLE_INFO,
                                               T_MIS_MONITOR_SUBTASK,
                                               T_MIS_MONITOR_TASK
                                         where (T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}' or
                                               T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{1}')
                                           and T_MIS_MONITOR_SUBTASK.ID =
                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                           and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0') t1
                                 where exists
                                 (select *
                                          from T_MIS_MONITOR_RESULT
                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = t1.ID
                                           and not exists
                                         (select *
                                                  from T_BASE_ITEM_INFO
                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'
                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                 order by t1.SAMPLES_ORDER_ISPRINTED, t1.SAMPLE_CODE";
            strSql = string.Format(strSql, strSubTaskStatus1, strSubTaskStatus2);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取样品交接环节样品分析通知单信息
        /// </summary>
        /// <param name="strTaskId">任务ID</param>
        /// <returns></returns>
        public DataTable getSampleOrderInfo_ZZ(string strTaskId)
        {
            string strSql = @"select t1.*
                                  from (select T_MIS_MONITOR_SAMPLE_INFO.ID,
                                               T_MIS_MONITOR_TASK.CREATE_DATE as TASK_DATE,
                                               T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE,
                                               (select COMPANY_NAME
                                                  from T_MIS_MONITOR_TASK_COMPANY
                                                 where ID = T_MIS_MONITOR_TASK.TESTED_COMPANY_ID) as COMPANY_NAME,
                                               SAMPLE_NAME,
                                               T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_TYPE,
                                               dbo.getItemNameBySampleId(T_MIS_MONITOR_SAMPLE_INFO.ID) as ITEM_NAME,
                                               T_MIS_MONITOR_TASK.ASKING_DATE,
                                               (select REAL_NAME
                                                  from T_SYS_USER
                                                 where ID = T_MIS_MONITOR_TASK.PROJECT_ID) as PROJECT_NAME,
                                               T_MIS_MONITOR_SAMPLE_INFO.SAMPLES_ORDER_ISPRINTED
                                          from T_MIS_MONITOR_SAMPLE_INFO,
                                               T_MIS_MONITOR_SUBTASK,
                                               T_MIS_MONITOR_TASK
                                         where T_MIS_MONITOR_TASK.ID = '{0}'
                                           and T_MIS_MONITOR_SUBTASK.ID =
                                               T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                           and T_MIS_MONITOR_SUBTASK.TASK_ID = T_MIS_MONITOR_TASK.ID
                                           and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0') t1

                                 where exists
                                 (select *
                                          from T_MIS_MONITOR_RESULT
                                         where T_MIS_MONITOR_RESULT.SAMPLE_ID = t1.ID
                                           and (T_MIS_MONITOR_RESULT.remark_4='1' or (not exists
                                         (select *
                                                  from T_BASE_ITEM_INFO
                                                 where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                   and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '是'
                                                   and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))))

                                 order by t1.SAMPLE_CODE";
            strSql = string.Format(strSql, strTaskId);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        #endregion

        #region 任务状态查看【郑州】

        /// <summary>
        /// 获取分析任务分配情况状态情况(样品)
        /// </summary>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public DataTable getAnalysisTaskAllocationViewSampleInfo(string strSubTaskStatus, string strResultStatus)
        {
            string strSql = @"select *
                              from T_MIS_MONITOR_SAMPLE_INFO
                             where T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                               and exists (select *
                                      from T_MIS_MONITOR_SUBTASK
                                     where T_MIS_MONITOR_SUBTASK.ID =
                                           T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                       and T_MIS_MONITOR_SUBTASK.TASK_STATUS = '{0}')
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
                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                       and exists
                                     (select *
                                              from T_BASE_ITEM_INFO
                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                             order by SAMPLE_CODE";
            strSql = string.Format(strSql, strSubTaskStatus, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取分析任务分配情况状态情况（监测项目）
        /// </summary>
        /// <param name="strSampeleId">样品ID</param>
        /// <param name="strResultStatus">结果状态</param>
        /// <returns></returns>
        public DataTable getAnalysisTaskAllocationViewResultInfo(string strSampeleId, string strResultStatus)
        {
            string strSql = @"select record.*,
                                   T_MIS_MONITOR_RESULT_APP.HEAD_USERID,
                                   T_MIS_MONITOR_RESULT_APP.ASSISTANT_USERID,
                                   T_MIS_MONITOR_RESULT_APP.ASKING_DATE
                              from (select *
                                      from T_MIS_MONITOR_RESULT
                                     where T_MIS_MONITOR_RESULT.SAMPLE_ID = '{0}'
                                       and (T_MIS_MONITOR_RESULT.QC_TYPE = '0' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '4' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '9' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '10' or
                                           T_MIS_MONITOR_RESULT.QC_TYPE = '11')
                                       and T_MIS_MONITOR_RESULT.RESULT_STATUS = '{1}'
                                       and exists
                                     (select *
                                              from T_BASE_ITEM_INFO
                                             where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                               and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                               and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID)) record
                              left join T_MIS_MONITOR_RESULT_APP
                                on record.ID = T_MIS_MONITOR_RESULT_APP.RESULT_ID";
            strSql = string.Format(strSql, strSampeleId, strResultStatus);
            return SqlHelper.ExecuteDataTable(strSql);
        }
        #endregion

        /// <summary>
        /// 样品交接发送到监测分析环节【郑州】
        /// </summary>
        /// <param name="strTaskId">总任务ID</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strCurrenteResultStatus">当前环节环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <param name="strNextResultStatus">下一环节分析结果状态。分析任务分配：01，分析结果填报：02，分析结果校核：03</param>
        /// <returns></returns>
        public bool sendToAnalysis(string strTaskId, string strSubTaskStatus, string strCurrenteResultStatus, string strNextResultStatus)
        {
            ArrayList arrVo = new ArrayList();

            string strSql = "";

            strSql = @"update T_MIS_MONITOR_RESULT
                                               set T_MIS_MONITOR_RESULT.RESULT_STATUS = '{3}',TASK_TYPE='发送'
                                             where RESULT_STATUS in ({2})
                                                       and (T_MIS_MONITOR_RESULT.remark_4='1' or exists (select *
                                                       from T_BASE_ITEM_INFO
                                                      where T_BASE_ITEM_INFO.HAS_SUB_ITEM = '0'
                                                        and T_BASE_ITEM_INFO.IS_SAMPLEDEPT = '否'
                                                        and T_BASE_ITEM_INFO.ID = T_MIS_MONITOR_RESULT.ITEM_ID))
                                                and exists
                                              (select *
                                                       from T_MIS_MONITOR_SAMPLE_INFO
                                                      where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                        and T_MIS_MONITOR_SAMPLE_INFO.NOSAMPLE = '0'
                                                        and exists
                                                      (select *
                                                               from T_MIS_MONITOR_SUBTASK
                                                              where T_MIS_MONITOR_SUBTASK.ID =
                                                                    T_MIS_MONITOR_SAMPLE_INFO.SUBTASK_ID
                                                                    and TASK_STATUS='{1}'
                                                                and T_MIS_MONITOR_SUBTASK.TASK_ID = '{0}'))";
            strSql = string.Format(strSql, strTaskId, strSubTaskStatus, strCurrenteResultStatus, strNextResultStatus);
            arrVo.Add(strSql);

            return ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 获取现场质控和实验质控的详细信息 Create By weilin 2014-03-21
        /// </summary>
        /// <param name="strSubTaskID"></param>
        /// <param name="strResultID"></param>
        /// <param name="strItemID"></param>
        /// <param name="strMark">0: 现场质控与实验质控 1：实验质控</param>
        /// <returns></returns>
        public DataTable GetQcInfo_QY(string strSubTaskID, string strResultID, string strItemID, string strMark)
        {
            string strSql = "";
            DataTable dt = new DataTable();
            DataTable dtTempEx = new DataTable();
            string strResultIDs = strResultID;
            string strNote = "";
            strSql = "select '' as ID, '' as SAMPLE_ID, '' as SAMPLE_CODE,'' as ITEM_ID,'' as QC_TYPE,'' as ITEM_RESULT,'' as REMARK,'' as SCOPE,'' as IS_OK";
            dt = SqlHelper.ExecuteDataTable(strSql);
            dt.Rows.Clear();

            if (strMark == "0")
            {
                //现场质控信息
                strSql = @"select QC_RECORD.*, T_MIS_MONITOR_SAMPLE_INFO.SAMPLE_CODE
                                          from (select ID, SAMPLE_ID, ITEM_ID, QC_TYPE, ITEM_RESULT, '' as REMARK,'' as SCOPE,'' as IS_OK
                                                  from T_MIS_MONITOR_RESULT
                                                 where QC_SOURCE_ID = '{0}'
                                                   and (T_MIS_MONITOR_RESULT.QC_TYPE = '1' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '2' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '3' or
                                                       T_MIS_MONITOR_RESULT.QC_TYPE = '4')
                                                 union all
                                                 select a.ID, a.SAMPLE_ID, a.ITEM_ID, a.QC_TYPE, a.ITEM_RESULT, '' as REMARK,'' as SCOPE,'' as IS_OK 
                                                    from T_MIS_MONITOR_RESULT a left join T_MIS_MONITOR_SAMPLE_INFO b on(a.SAMPLE_ID=b.ID)
                                                    where a.QC_TYPE='11' and b.SUBTASK_ID='{1}' and a.ITEM_ID='{2}' 
                                           ) QC_RECORD
                                          left join T_MIS_MONITOR_SAMPLE_INFO
                                            on QC_RECORD.SAMPLE_ID = T_MIS_MONITOR_SAMPLE_INFO.ID";
                strSql = string.Format(strSql, strResultID, strSubTaskID, strItemID);
                dtTempEx = SqlHelper.ExecuteDataTable(strSql);

                foreach (DataRow row in dtTempEx.Rows)
                {
                    strResultIDs += "," + row["ID"].ToString();
                    string strSubResultId = row["ID"].ToString();
                    string strSubQcType = row["QC_TYPE"].ToString();
                    //现场空白
                    if (strSubQcType == "1")
                    {
                        strNote = "";
                        strSql = @"select *
                                          from T_MIS_MONITOR_QC_EMPTY_OUT where RESULT_ID_EMPTY='{0}'";
                        strSql = string.Format(strSql, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                            strNote = "空白值:" + dtTemp.Rows[0]["RESULT_EMPTY"].ToString();
                        row["REMARK"] = strNote;
                        row["IS_OK"] = "1";
                        row["SCOPE"] = "";
                    }
                    //现场加标
                    if (strSubQcType == "2")
                    {
                        strNote = "";
                        strSql = "select * from T_MIS_MONITOR_QC_ADD where RESULT_ID_SRC='{0}' and RESULT_ID_ADD='{1}'";
                        strSql = string.Format(strSql, strResultID, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);

                        if (dtTemp.Rows.Count > 0)
                        {
                            TMisMonitorResultVo ResultVo = new TMisMonitorResultAccess().Details(strResultID);
                            strNote = "原测定值:" + ResultVo.ITEM_RESULT + ";加标测定值:" + dtTemp.Rows[0]["ADD_RESULT_EX"].ToString() + ";加标量:" + dtTemp.Rows[0]["QC_ADD"].ToString() + ";加标回收率:" + dtTemp.Rows[0]["ADD_BACK"].ToString() + "%";
                            row["IS_OK"] = dtTemp.Rows[0]["ADD_ISOK"].ToString();
                        }
                        row["REMARK"] = strNote;

                        //获取监测项目ID
                        string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                        //根据监测项目获取加标上限和加标下限
                        ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId);
                        string strAddMax = TBaseItemInfoVo.ADD_MAX;
                        string strAddMin = TBaseItemInfoVo.ADD_MIN;

                        row["SCOPE"] = "大于等于" + strAddMin + "，小于等于" + strAddMax;
                    }
                    //现场平行,实验室密码平行
                    if (strSubQcType == "3" || strSubQcType == "4")
                    {
                        strNote = "";
                        strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and RESULT_ID_TWIN1='{1}'";
                        strSql = string.Format(strSql, strResultID, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                        {
                            TMisMonitorResultVo ResultVo = new TMisMonitorResultAccess().Details(strResultID);
                            strNote = "原测定值:" + ResultVo.ITEM_RESULT + ";测定值" + dtTemp.Rows[0]["TWIN_RESULT1"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                            row["REMARK"] = strNote;

                            row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                            //获取监测项目ID
                            string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                            //根据监测项目获取加标上限和加标下限
                            string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                            row["SCOPE"] = "小于等于" + strTwinValue;
                        }
                        else
                        {
                            strSql = "select * from T_MIS_MONITOR_QC_TWIN where RESULT_ID_SRC='{0}' and RESULT_ID_TWIN2='{1}'";
                            strSql = string.Format(strSql, strResultID, strSubResultId);
                            dtTemp = SqlHelper.ExecuteDataTable(strSql);
                            if (dtTemp.Rows.Count > 0)
                            {
                                strNote = "测定值" + dtTemp.Rows[0]["TWIN_RESULT2"].ToString() + ";测定均值:" + dtTemp.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTemp.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                                row["REMARK"] = strNote;
                                row["IS_OK"] = dtTemp.Rows[0]["TWIN_ISOK"].ToString();
                                //获取监测项目ID
                                string strItemId = new TMisMonitorResultAccess().Details(strSubResultId).ITEM_ID;
                                //根据监测项目获取加标上限和加标下限
                                string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemId).TWIN_VALUE;
                                row["SCOPE"] = "小于等于" + strTwinValue;
                            }
                        }
                    }
                    //密码盲样
                    if (strSubQcType == "11")
                    {
                        strNote = "";
                        strSql = @"select *
                                          from T_MIS_MONITOR_QC_BLIND_ZZ where RESULT_ID='{0}' and QC_TYPE='11'";
                        strSql = string.Format(strSql, strSubResultId);
                        DataTable dtTemp = SqlHelper.ExecuteDataTable(strSql);
                        if (dtTemp.Rows.Count > 0)
                        {
                            strNote = "标准值:" + dtTemp.Rows[0]["STANDARD_VALUE"].ToString() + ";不确定值:" + dtTemp.Rows[0]["UNCETAINTY"].ToString();// + ";偏移量:" + dtTemp.Rows[0]["OFFSET"].ToString() + "%";
                            row["IS_OK"] = dtTemp.Rows[0]["BLIND_ISOK"].ToString();
                        }
                        row["REMARK"] = strNote;
                        row["SCOPE"] = "";
                    }

                    dt.ImportRow(row);
                }
            }
            //实验质控信息
            string[] objResultID = strResultIDs.Split(',');
            string strSampleID = "";
            string strSampleCode = "";
            string strBat = "0";
            string strSt = "0";
            for (int i = 0; i < objResultID.Length; i++)
            {
                strResultID = objResultID[i].ToString();
                strSql = "select a.SAMPLE_ID,b.SAMPLE_CODE from T_MIS_MONITOR_RESULT a left join T_MIS_MONITOR_SAMPLE_INFO b on(a.SAMPLE_ID=b.ID) where a.ID='{0}'";
                strSql = string.Format(strSql, strResultID);
                dtTempEx = SqlHelper.ExecuteDataTable(strSql);
                if (dtTempEx.Rows.Count > 0)
                {
                    strSampleID = dtTempEx.Rows[0]["SAMPLE_ID"].ToString();
                    strSampleCode = dtTempEx.Rows[0]["SAMPLE_CODE"].ToString();
                }

                //实验室空白
                strNote = "";
                strSql = @"select *
                                          from T_MIS_MONITOR_QC_EMPTY_BAT
                                         where exists (select *
                                                  from T_MIS_MONITOR_RESULT
                                                 where id = '{0}'
                                                   and T_MIS_MONITOR_QC_EMPTY_BAT.ID =
                                                       T_MIS_MONITOR_RESULT.EMPTY_IN_BAT_ID)";
                strSql = string.Format(strSql, strResultID);
                dtTempEx = SqlHelper.ExecuteDataTable(strSql);
                if (dtTempEx.Rows.Count > 0)
                {
                    if (strBat == "0")
                    {
                        strNote = "空白值:" + dtTempEx.Rows[0]["QC_EMPTY_IN_RESULT"].ToString() + ";空白个数:" + dtTempEx.Rows[0]["QC_EMPTY_IN_COUNT"].ToString();
                        DataRow row = dt.NewRow();
                        row["ID"] = dtTempEx.Rows[0]["ID"].ToString();
                        row["SAMPLE_ID"] = "";
                        row["SAMPLE_CODE"] = "";
                        row["ITEM_ID"] = strItemID;
                        row["QC_TYPE"] = "5";
                        row["ITEM_RESULT"] = dtTempEx.Rows[0]["QC_EMPTY_IN_RESULT"].ToString();
                        row["REMARK"] = strNote;
                        row["IS_OK"] = "1";
                        row["SCOPE"] = "";
                        dt.Rows.Add(row);

                        strBat = "1";
                    }
                }
                //实验室加标
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_ADD  where RESULT_ID_SRC='{0}' and QC_TYPE='6'";
                strSql = string.Format(strSql, strResultID);
                dtTempEx = SqlHelper.ExecuteDataTable(strSql);
                if (dtTempEx.Rows.Count > 0)
                {
                    strNote = "加标测定值:" + dtTempEx.Rows[0]["ADD_RESULT_EX"].ToString() + ";加标量:" + dtTempEx.Rows[0]["QC_ADD"].ToString() + ";加标回收率:" + dtTempEx.Rows[0]["ADD_BACK"].ToString() + "%";
                    DataRow row = dt.NewRow();
                    row["ID"] = dtTempEx.Rows[0]["ID"].ToString();
                    row["SAMPLE_ID"] = strSampleID;
                    row["SAMPLE_CODE"] = strSampleCode;
                    row["ITEM_ID"] = strItemID;
                    row["QC_TYPE"] = "6";
                    row["ITEM_RESULT"] = dtTempEx.Rows[0]["ADD_RESULT_EX"].ToString();
                    row["REMARK"] = strNote;
                    row["IS_OK"] = dtTempEx.Rows[0]["ADD_ISOK"].ToString();

                    //根据监测项目获取加标上限和加标下限
                    ValueObject.Channels.Base.Item.TBaseItemInfoVo TBaseItemInfoVo = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemID);
                    string strAddMax = TBaseItemInfoVo.ADD_MAX;
                    string strAddMin = TBaseItemInfoVo.ADD_MIN;
                    row["SCOPE"] = "大于等于" + strAddMin + "，小于等于" + strAddMax;

                    dt.Rows.Add(row);
                }
                //实验室明码平行
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_TWIN  where RESULT_ID_SRC='{0}' and QC_TYPE='7'";
                strSql = string.Format(strSql, strResultID);
                dtTempEx = SqlHelper.ExecuteDataTable(strSql);
                if (dtTempEx.Rows.Count > 0)
                {
                    strNote = "测定值1:" + dtTempEx.Rows[0]["TWIN_RESULT1"].ToString() + ";测定值2:" + dtTempEx.Rows[0]["TWIN_RESULT2"].ToString() + ";测定均值:" + dtTempEx.Rows[0]["TWIN_AVG"].ToString() + ";相对偏差:" + dtTempEx.Rows[0]["TWIN_OFFSET"].ToString() + "%";
                    DataRow row = dt.NewRow();
                    row["ID"] = dtTempEx.Rows[0]["ID"].ToString();
                    row["SAMPLE_ID"] = strSampleID;
                    row["SAMPLE_CODE"] = strSampleCode;
                    row["ITEM_ID"] = strItemID;
                    row["QC_TYPE"] = "7";
                    row["ITEM_RESULT"] = dtTempEx.Rows[0]["TWIN_AVG"].ToString();
                    row["REMARK"] = strNote;

                    row["IS_OK"] = dtTempEx.Rows[0]["TWIN_ISOK"].ToString();
                    //根据监测项目获取加标上限和加标下限
                    string strTwinValue = new i3.DataAccess.Channels.Base.Item.TBaseItemInfoAccess().Details(strItemID).TWIN_VALUE;
                    row["SCOPE"] = "小于等于" + strTwinValue;

                    dt.Rows.Add(row);
                }

                //标准样
                strNote = "";
                strSql = "select * from T_MIS_MONITOR_QC_ST  where RESULT_ID_SRC='{0}'";
                strSql = string.Format(strSql, strResultID);
                dtTempEx = SqlHelper.ExecuteDataTable(strSql);
                if (dtTempEx.Rows.Count > 0)
                {
                    if (strSt == "0")
                    {
                        //strNote = "标准值:" + dtTempEx.Rows[0]["SRC_RESULT"].ToString() + ";标准个数:" + dtTempEx.Rows[0]["ST_RESULT"].ToString();
                        strNote = "";
                        if (dtTempEx.Rows[0]["REMARK1"].ToString() != "" && dtTempEx.Rows[0]["REMARK1"].ToString() != "0")
                            strNote += "标准值1：" + dtTempEx.Rows[0]["REMARK1"].ToString() + ";";
                        if (dtTempEx.Rows[0]["REMARK2"].ToString() != "" && dtTempEx.Rows[0]["REMARK2"].ToString() != "0")
                            strNote += "标准值2：" + dtTempEx.Rows[0]["REMARK2"].ToString() + ";";
                        if (dtTempEx.Rows[0]["REMARK3"].ToString() != "" && dtTempEx.Rows[0]["REMARK3"].ToString() != "0")
                            strNote += "标准值3：" + dtTempEx.Rows[0]["REMARK3"].ToString() + ";";
                        DataRow row = dt.NewRow();
                        row["ID"] = dtTempEx.Rows[0]["ID"].ToString();
                        row["SAMPLE_ID"] = "";
                        row["SAMPLE_CODE"] = "";
                        row["ITEM_ID"] = strItemID;
                        row["QC_TYPE"] = "8";
                        row["ITEM_RESULT"] = dtTempEx.Rows[0]["ST_RESULT"].ToString();
                        row["REMARK"] = strNote;
                        row["IS_OK"] = "1";
                        row["SCOPE"] = "";
                        dt.Rows.Add(row);

                        strSt = "1";
                    }
                }
            }

            return dt;
        }
        /// <summary>
        /// 获取字符串中的数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>数字</returns>
        public static decimal GetNumber(string str)
        {
            decimal result = 0;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.）
                str = Regex.Replace(str, @"[^\d.\d]", "");

                // 如果是数字，则转换为decimal类型
                if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                {
                    result = decimal.Parse(str);
                }
            }

            return result;
        }
        /// <summary>
        /// 更新某些样品的监测项目的结果值 Create By :weilin 2014-8-3
        /// </summary>
        /// <param name="strSampleIDs"></param>
        /// <param name="strItemID"></param>
        /// <param name="strItemResult"></param>
        /// <returns></returns>
        public bool FinishAllItem(string strSampleIDs, string strItemID, string strItemResult)
        {
            string strSql = "";
            strSql = @"update T_MIS_MONITOR_RESULT set ITEM_RESULT = '{0}'
                                             where ITEM_ID='{1}'
                                                and exists
                                              (select *
                                                       from T_MIS_MONITOR_SAMPLE_INFO
                                                      where T_MIS_MONITOR_SAMPLE_INFO.ID = T_MIS_MONITOR_RESULT.SAMPLE_ID
                                                      and T_MIS_MONITOR_SAMPLE_INFO.ID in({2})
                                                        )";
            strSql = string.Format(strSql, strItemResult, strItemID, strSampleIDs);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql) > 0 ? true : false;
        }

        /// <summary>
        /// 实验室质控时获取相关信息 Create By:weilin
        /// </summary>
        /// <param name="strResultIDs"></param>
        /// <returns></returns>
        public DataTable getItemInfoForQC(string strResultIDs)
        {
            string strSql = "";
            strSql = @"select a.ID ResultID,b.SAMPLE_CODE SampleCode,a.ITEM_RESULT ItemResult from T_MIS_MONITOR_RESULT a left join T_MIS_MONITOR_SAMPLE_INFO b 
                    on(a.SAMPLE_ID=b.ID)
                    where a.ID in({0})";
            strSql = string.Format(strSql, strResultIDs);

            return SqlHelper.ExecuteDataTable(strSql);
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