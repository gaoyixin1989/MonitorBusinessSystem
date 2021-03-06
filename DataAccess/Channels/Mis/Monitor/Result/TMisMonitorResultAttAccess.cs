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
    /// 功能：分析原始数据附件表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorResultAttAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_RESULT_ATT " + this.BuildWhereStatement(tMisMonitorResultAtt);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorResultAttVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT_ATT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorResultAttVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorResultAttVo Details(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT_ATT " + this.BuildWhereStatement(tMisMonitorResultAtt));
            return SqlHelper.ExecuteObject(new TMisMonitorResultAttVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorResultAttVo> SelectByObject(TMisMonitorResultAttVo tMisMonitorResultAtt, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_RESULT_ATT " + this.BuildWhereStatement(tMisMonitorResultAtt));
            return SqlHelper.ExecuteObjectList(tMisMonitorResultAtt, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorResultAttVo tMisMonitorResultAtt, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_RESULT_ATT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorResultAtt));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResultAtt"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            string strSQL = "select * from T_MIS_MONITOR_RESULT_ATT " + this.BuildWhereStatement(tMisMonitorResultAtt);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象</param>
        /// <returns></returns>
        public TMisMonitorResultAttVo SelectByObject(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            string strSQL = "select * from T_MIS_MONITOR_RESULT_ATT " + this.BuildWhereStatement(tMisMonitorResultAtt);
            return SqlHelper.ExecuteObject(new TMisMonitorResultAttVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorResultAtt, TMisMonitorResultAttVo.T_MIS_MONITOR_RESULT_ATT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultAtt">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorResultAtt, TMisMonitorResultAttVo.T_MIS_MONITOR_RESULT_ATT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorResultAtt.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultAtt_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorResultAtt_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultAttVo tMisMonitorResultAtt_UpdateSet, TMisMonitorResultAttVo tMisMonitorResultAtt_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorResultAtt_UpdateSet, TMisMonitorResultAttVo.T_MIS_MONITOR_RESULT_ATT_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorResultAtt_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_RESULT_ATT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            string strSQL = "delete from T_MIS_MONITOR_RESULT_ATT ";
            strSQL += this.BuildWhereStatement(tMisMonitorResultAtt);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorResultAtt"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorResultAtt)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorResultAtt.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorResultAtt.ID.ToString()));
                }
                //样品结果ID
                if (!String.IsNullOrEmpty(tMisMonitorResultAtt.RESULT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID = '{0}'", tMisMonitorResultAtt.RESULT_ID.ToString()));
                }
                //附件ID
                if (!String.IsNullOrEmpty(tMisMonitorResultAtt.FILE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILE_ID = '{0}'", tMisMonitorResultAtt.FILE_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorResultAtt.REMARK_1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK_1 = '{0}'", tMisMonitorResultAtt.REMARK_1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorResultAtt.REMARK_2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK_2 = '{0}'", tMisMonitorResultAtt.REMARK_2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorResultAtt.REMARK_3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK_3 = '{0}'", tMisMonitorResultAtt.REMARK_3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorResultAtt.REMARK_4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK_4 = '{0}'", tMisMonitorResultAtt.REMARK_4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorResultAtt.REMARK_5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK_5 = '{0}'", tMisMonitorResultAtt.REMARK_5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
