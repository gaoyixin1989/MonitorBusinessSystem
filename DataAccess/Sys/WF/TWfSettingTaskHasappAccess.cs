using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.WF;
using i3.ValueObject;

namespace i3.DataAccess.Sys.WF
{
    /// <summary>
    /// 功能：纸质数据审核记录
    /// 创建日期：2013-05-03
    /// 创建人：潘德军
    /// </summary>
    public class TWfSettingTaskHasappAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForApp(string strUserId)
        {
            string strContractApp = @"select distinct d.INST_TASK_ENDTIME,d.REAL_USER,d.INST_NOTE,d.WF_TASK_ID,d.WF_ID,s.SERVICE_KEY_VALUE,
                    c.CONTRACT_CODE,'' as TICKET_NUM,'' as REPORT_CODE,C.CONTRACT_YEAR,c.PROJECT_NAME, dict.DICT_TEXT as CONTRACT_TYPE
                    from T_MIS_CONTRACT c 
                        join T_WF_INST_TASK_SERVICE s on s.SERVICE_KEY_VALUE=c.ID and (s.SERVICE_NAME='委托书ID' or s.SERVICE_NAME='委托书(自送样)ID' or s.SERVICE_NAME='验收委托')
                            join T_WF_INST_TASK_DETAIL d on d.ID=s.WF_INST_TASK_ID and d.INST_TASK_STATE='2B'
                                join T_SYS_DICT dict on dict.DICT_TYPE='Contract_Type' and dict.DICT_CODE=c.CONTRACT_TYPE
                    where d.WF_TASK_ID in (select WF_TASK_ID  from T_WF_SETTING_TASK_ISAPP where IS_APP='1')
                        and not exists (select 1 from T_WF_SETTING_TASK_HASAPP h where h.WF_TASK_ID=d.WF_TASK_ID and h.TASK_ID=c.ID and h.WF_ID=d.WF_ID and h.HAS_APP='1')
                            and d.REAL_USER='{0}'";
            string strRptApp = @"select distinct d.INST_TASK_ENDTIME,d.REAL_USER,d.INST_NOTE,d.WF_TASK_ID,d.WF_ID,s.SERVICE_KEY_VALUE,
                    c.CONTRACT_CODE,c.TICKET_NUM,rp.REPORT_CODE,C.CONTRACT_YEAR,c.PROJECT_NAME,dict.DICT_TEXT as CONTRACT_TYPE
                    from T_MIS_MONITOR_TASK c 
                        join T_WF_INST_TASK_SERVICE s on s.SERVICE_KEY_VALUE=c.ID and s.SERVICE_NAME='报告流程'
                            join T_WF_INST_TASK_DETAIL d on d.ID=s.WF_INST_TASK_ID and d.INST_TASK_STATE='2B'
                                join T_MIS_MONITOR_REPORT rp on rp.TASK_ID=c.ID
                                    join T_SYS_DICT dict on dict.DICT_TYPE='Contract_Typek' and dict.DICT_CODE=c.CONTRACT_TYPE 
                    where d.WF_TASK_ID in (select WF_TASK_ID  from T_WF_SETTING_TASK_ISAPP where IS_APP='1')
                        and not exists (select 1 from T_WF_SETTING_TASK_HASAPP h where h.WF_TASK_ID=d.WF_TASK_ID and h.TASK_ID=c.ID and h.WF_ID=d.WF_ID and h.HAS_APP='1')
                            and d.REAL_USER='{0}'";
            string strSQL = " select count(*) from (({0}) union ({1}))t";
            strSQL = string.Format(strSQL, strContractApp, strRptApp);
            strSQL = string.Format(strSQL, strUserId);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForApp(string strUserId, int iIndex, int iCount)
        {
            string strContractApp = @"select distinct d.INST_TASK_ENDTIME,d.REAL_USER,d.INST_NOTE,d.WF_TASK_ID,d.WF_ID,s.SERVICE_KEY_VALUE,
                    c.CONTRACT_CODE,'' as TICKET_NUM,'' as REPORT_CODE,C.CONTRACT_YEAR,c.PROJECT_NAME, dict.DICT_TEXT as CONTRACT_TYPE
                    from T_MIS_CONTRACT c 
                        join T_WF_INST_TASK_SERVICE s on s.SERVICE_KEY_VALUE=c.ID and (s.SERVICE_NAME='委托书ID' or s.SERVICE_NAME='委托书(自送样)ID' or s.SERVICE_NAME='验收委托')
                            join T_WF_INST_TASK_DETAIL d on d.ID=s.WF_INST_TASK_ID and d.INST_TASK_STATE='2B'
                                join T_SYS_DICT dict on dict.DICT_TYPE='Contract_Type' and dict.DICT_CODE=c.CONTRACT_TYPE
                    where d.WF_TASK_ID in (select WF_TASK_ID  from T_WF_SETTING_TASK_ISAPP where IS_APP='1')
                        and not exists (select 1 from T_WF_SETTING_TASK_HASAPP h where h.WF_TASK_ID=d.WF_TASK_ID and h.TASK_ID=c.ID and h.WF_ID=d.WF_ID and h.HAS_APP='1')
                            and d.REAL_USER='{0}'";
            string strRptApp = @"select distinct d.INST_TASK_ENDTIME,d.REAL_USER,d.INST_NOTE,d.WF_TASK_ID,d.WF_ID,s.SERVICE_KEY_VALUE,
                    c.CONTRACT_CODE,c.TICKET_NUM,rp.REPORT_CODE,C.CONTRACT_YEAR,c.PROJECT_NAME,dict.DICT_TEXT as CONTRACT_TYPE
                    from T_MIS_MONITOR_TASK c 
                        join T_WF_INST_TASK_SERVICE s on s.SERVICE_KEY_VALUE=c.ID and s.SERVICE_NAME='报告流程'
                            join T_WF_INST_TASK_DETAIL d on d.ID=s.WF_INST_TASK_ID and d.INST_TASK_STATE='2B'
                                join T_MIS_MONITOR_REPORT rp on rp.TASK_ID=c.ID
                                    join T_SYS_DICT dict on dict.DICT_TYPE='Contract_Typek' and dict.DICT_CODE=c.CONTRACT_TYPE 
                    where d.WF_TASK_ID in (select WF_TASK_ID  from T_WF_SETTING_TASK_ISAPP where IS_APP='1')
                        and not exists (select 1 from T_WF_SETTING_TASK_HASAPP h where h.WF_TASK_ID=d.WF_TASK_ID and h.TASK_ID=c.ID and h.WF_ID=d.WF_ID and h.HAS_APP='1')
                            and d.REAL_USER='{0}'";
            string strSQL = " select * from (({0}) union ({1}))t order by INST_TASK_ENDTIME";
            strSQL = string.Format(strSQL, strContractApp, strRptApp);
            strSQL = string.Format(strSQL, strUserId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            string strSQL = "select Count(*) from T_WF_SETTING_TASK_HASAPP " + this.BuildWhereStatement(tWfSettingTaskHasapp);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingTaskHasappVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_TASK_HASAPP  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TWfSettingTaskHasappVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingTaskHasappVo Details(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_TASK_HASAPP " + this.BuildWhereStatement(tWfSettingTaskHasapp));
            return SqlHelper.ExecuteObject(new TWfSettingTaskHasappVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskHasappVo> SelectByObject(TWfSettingTaskHasappVo tWfSettingTaskHasapp, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_WF_SETTING_TASK_HASAPP " + this.BuildWhereStatement(tWfSettingTaskHasapp));
            return SqlHelper.ExecuteObjectList(tWfSettingTaskHasapp, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingTaskHasappVo tWfSettingTaskHasapp, int iIndex, int iCount)
        {

            string strSQL = " select * from T_WF_SETTING_TASK_HASAPP {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfSettingTaskHasapp));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingTaskHasapp"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            string strSQL = "select * from T_WF_SETTING_TASK_HASAPP " + this.BuildWhereStatement(tWfSettingTaskHasapp);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <returns></returns>
        public TWfSettingTaskHasappVo SelectByObject(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            string strSQL = "select * from T_WF_SETTING_TASK_HASAPP " + this.BuildWhereStatement(tWfSettingTaskHasapp);
            return SqlHelper.ExecuteObject(new TWfSettingTaskHasappVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfSettingTaskHasapp, TWfSettingTaskHasappVo.T_WF_SETTING_TASK_HASAPP_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingTaskHasapp, TWfSettingTaskHasappVo.T_WF_SETTING_TASK_HASAPP_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfSettingTaskHasapp.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskHasapp_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingTaskHasapp_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskHasappVo tWfSettingTaskHasapp_UpdateSet, TWfSettingTaskHasappVo tWfSettingTaskHasapp_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingTaskHasapp_UpdateSet, TWfSettingTaskHasappVo.T_WF_SETTING_TASK_HASAPP_TABLE);
            strSQL += this.BuildWhereStatement(tWfSettingTaskHasapp_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_SETTING_TASK_HASAPP where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            string strSQL = "delete from T_WF_SETTING_TASK_HASAPP ";
            strSQL += this.BuildWhereStatement(tWfSettingTaskHasapp);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfSettingTaskHasapp"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfSettingTaskHasapp)
            {

                //Id
                if (!String.IsNullOrEmpty(tWfSettingTaskHasapp.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfSettingTaskHasapp.ID.ToString()));
                }
                //环节ID
                if (!String.IsNullOrEmpty(tWfSettingTaskHasapp.WF_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_TASK_ID = '{0}'", tWfSettingTaskHasapp.WF_TASK_ID.ToString()));
                }
                //环节名
                if (!String.IsNullOrEmpty(tWfSettingTaskHasapp.WF_TASK_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_TASK_NAME = '{0}'", tWfSettingTaskHasapp.WF_TASK_NAME.ToString()));
                }
                //流程ID或者流程类别（监测分析用流程类别）
                if (!String.IsNullOrEmpty(tWfSettingTaskHasapp.WF_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_ID = '{0}'", tWfSettingTaskHasapp.WF_ID.ToString()));
                }
                //实例ID
                if (!String.IsNullOrEmpty(tWfSettingTaskHasapp.TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_ID = '{0}'", tWfSettingTaskHasapp.TASK_ID.ToString()));
                }
                //是否已审核
                if (!String.IsNullOrEmpty(tWfSettingTaskHasapp.HAS_APP.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HAS_APP = '{0}'", tWfSettingTaskHasapp.HAS_APP.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
