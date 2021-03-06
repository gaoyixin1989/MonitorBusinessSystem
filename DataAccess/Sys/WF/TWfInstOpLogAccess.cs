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
    /// 功能：流程操作记录表
    /// 创建日期：2012-11-07
    /// 创建人：石磊
    /// </summary>
    public class TWfInstOpLogAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstOpLog">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstOpLogVo tWfInstOpLog)
        {
            string strSQL = "select Count(*) from T_WF_INST_OP_LOG " + this.BuildWhereStatement(tWfInstOpLog);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstOpLogVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_INST_OP_LOG  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TWfInstOpLogVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstOpLog">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstOpLogVo Details(TWfInstOpLogVo tWfInstOpLog)
        {
            string strSQL = String.Format("select * from  T_WF_INST_OP_LOG " + this.BuildWhereStatement(tWfInstOpLog));
            return SqlHelper.ExecuteObject(new TWfInstOpLogVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstOpLog">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstOpLogVo> SelectByObject(TWfInstOpLogVo tWfInstOpLog, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_WF_INST_OP_LOG " + this.BuildWhereStatement(tWfInstOpLog));
            return SqlHelper.ExecuteObjectList(tWfInstOpLog, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstOpLog">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstOpLogVo tWfInstOpLog, int iIndex, int iCount)
        {

            string strSQL = " select * from T_WF_INST_OP_LOG {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfInstOpLog));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstOpLog"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstOpLogVo tWfInstOpLog)
        {
            string strSQL = "select * from T_WF_INST_OP_LOG " + this.BuildWhereStatement(tWfInstOpLog);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstOpLog">对象</param>
        /// <returns></returns>
        public TWfInstOpLogVo SelectByObject(TWfInstOpLogVo tWfInstOpLog)
        {
            string strSQL = "select * from T_WF_INST_OP_LOG " + this.BuildWhereStatement(tWfInstOpLog);
            return SqlHelper.ExecuteObject(new TWfInstOpLogVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfInstOpLog">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstOpLogVo tWfInstOpLog)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfInstOpLog, TWfInstOpLogVo.T_WF_INST_OP_LOG_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstOpLog">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstOpLogVo tWfInstOpLog)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstOpLog, TWfInstOpLogVo.T_WF_INST_OP_LOG_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfInstOpLog.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstOpLog_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstOpLog_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstOpLogVo tWfInstOpLog_UpdateSet, TWfInstOpLogVo tWfInstOpLog_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstOpLog_UpdateSet, TWfInstOpLogVo.T_WF_INST_OP_LOG_TABLE);
            strSQL += this.BuildWhereStatement(tWfInstOpLog_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_INST_OP_LOG where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfInstOpLogVo tWfInstOpLog)
        {
            string strSQL = "delete from T_WF_INST_OP_LOG ";
            strSQL += this.BuildWhereStatement(tWfInstOpLog);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfInstOpLog"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfInstOpLogVo tWfInstOpLog)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfInstOpLog)
            {

                //编号
                if (!String.IsNullOrEmpty(tWfInstOpLog.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfInstOpLog.ID.ToString()));
                }
                //流程实例编号
                if (!String.IsNullOrEmpty(tWfInstOpLog.WF_INST_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_INST_ID = '{0}'", tWfInstOpLog.WF_INST_ID.ToString()));
                }
                //环节实例编号
                if (!String.IsNullOrEmpty(tWfInstOpLog.WF_INST_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_INST_TASK_ID = '{0}'", tWfInstOpLog.WF_INST_TASK_ID.ToString()));
                }
                //操作用户
                if (!String.IsNullOrEmpty(tWfInstOpLog.OP_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OP_USER = '{0}'", tWfInstOpLog.OP_USER.ToString()));
                }
                //操作动作
                if (!String.IsNullOrEmpty(tWfInstOpLog.OP_ACTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OP_ACTION = '{0}'", tWfInstOpLog.OP_ACTION.ToString()));
                }
                //流程简码
                if (!String.IsNullOrEmpty(tWfInstOpLog.WF_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_ID = '{0}'", tWfInstOpLog.WF_ID.ToString()));
                }
                //环节简码
                if (!String.IsNullOrEmpty(tWfInstOpLog.WF_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_TASK_ID = '{0}'", tWfInstOpLog.WF_TASK_ID.ToString()));
                }
                //操作时间
                if (!String.IsNullOrEmpty(tWfInstOpLog.OP_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OP_TIME = '{0}'", tWfInstOpLog.OP_TIME.ToString()));
                }
                //操作描述
                if (!String.IsNullOrEmpty(tWfInstOpLog.OP_NOTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OP_NOTE = '{0}'", tWfInstOpLog.OP_NOTE.ToString()));
                }
                //是否代理
                if (!String.IsNullOrEmpty(tWfInstOpLog.IS_AGENT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_AGENT = '{0}'", tWfInstOpLog.IS_AGENT.ToString()));
                }
                //被代理人
                if (!String.IsNullOrEmpty(tWfInstOpLog.AGENT_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AGENT_USER = '{0}'", tWfInstOpLog.AGENT_USER.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
