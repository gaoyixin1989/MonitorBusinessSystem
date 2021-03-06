using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：现场空白结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcEmptyOutAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_QC_EMPTY_OUT " + this.BuildWhereStatement(tMisMonitorQcEmptyOut);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcEmptyOutVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_EMPTY_OUT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorQcEmptyOutVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcEmptyOutVo Details(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_EMPTY_OUT " + this.BuildWhereStatement(tMisMonitorQcEmptyOut));
            return SqlHelper.ExecuteObject(new TMisMonitorQcEmptyOutVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcEmptyOutVo> SelectByObject(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_EMPTY_OUT " + this.BuildWhereStatement(tMisMonitorQcEmptyOut));
            return SqlHelper.ExecuteObjectList(tMisMonitorQcEmptyOut, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_QC_EMPTY_OUT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorQcEmptyOut));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_EMPTY_OUT " + this.BuildWhereStatement(tMisMonitorQcEmptyOut);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象</param>
        /// <returns></returns>
        public TMisMonitorQcEmptyOutVo SelectByObject(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_EMPTY_OUT " + this.BuildWhereStatement(tMisMonitorQcEmptyOut);
            return SqlHelper.ExecuteObject(new TMisMonitorQcEmptyOutVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorQcEmptyOut, TMisMonitorQcEmptyOutVo.T_MIS_MONITOR_QC_EMPTY_OUT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcEmptyOut, TMisMonitorQcEmptyOutVo.T_MIS_MONITOR_QC_EMPTY_OUT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorQcEmptyOut.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorQcEmptyOut_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut_UpdateSet, TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcEmptyOut_UpdateSet, TMisMonitorQcEmptyOutVo.T_MIS_MONITOR_QC_EMPTY_OUT_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorQcEmptyOut_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_QC_EMPTY_OUT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            string strSQL = "delete from T_MIS_MONITOR_QC_EMPTY_OUT ";
            strSQL += this.BuildWhereStatement(tMisMonitorQcEmptyOut);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorQcEmptyOut)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorQcEmptyOut.ID.ToString()));
                }
                //原始样分析结果 ID
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.RESULT_ID_SRC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_SRC = '{0}'", tMisMonitorQcEmptyOut.RESULT_ID_SRC.ToString()));
                }
                //空白样分析结果 ID
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.RESULT_ID_EMPTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_EMPTY = '{0}'", tMisMonitorQcEmptyOut.RESULT_ID_EMPTY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.RESULT_EMPTY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_EMPTY = '{0}'", tMisMonitorQcEmptyOut.RESULT_EMPTY.ToString()));
                }
                //相对偏差（%）
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.EMPTY_OFFSET.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EMPTY_OFFSET = '{0}'", tMisMonitorQcEmptyOut.EMPTY_OFFSET.ToString()));
                }
                //空白是否合格
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.EMPTY_ISOK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EMPTY_ISOK = '{0}'", tMisMonitorQcEmptyOut.EMPTY_ISOK.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorQcEmptyOut.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorQcEmptyOut.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorQcEmptyOut.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorQcEmptyOut.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorQcEmptyOut.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorQcEmptyOut.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
