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
    /// 功能：加标样结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcAddAccess : SqlHelper
    {
        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_QC_ADD " + this.BuildWhereStatement(tMisMonitorQcAdd);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcAddVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_ADD  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorQcAddVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcAddVo Details(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_ADD " + this.BuildWhereStatement(tMisMonitorQcAdd));
            return SqlHelper.ExecuteObject(new TMisMonitorQcAddVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcAddVo> SelectByObject(TMisMonitorQcAddVo tMisMonitorQcAdd, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_ADD " + this.BuildWhereStatement(tMisMonitorQcAdd));
            return SqlHelper.ExecuteObjectList(tMisMonitorQcAdd, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcAddVo tMisMonitorQcAdd, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_QC_ADD {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorQcAdd));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcAdd"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_ADD " + this.BuildWhereStatement(tMisMonitorQcAdd);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象</param>
        /// <returns></returns>
        public TMisMonitorQcAddVo SelectByObject(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_ADD " + this.BuildWhereStatement(tMisMonitorQcAdd);
            return SqlHelper.ExecuteObject(new TMisMonitorQcAddVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorQcAdd, TMisMonitorQcAddVo.T_MIS_MONITOR_QC_ADD_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcAdd">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcAdd, TMisMonitorQcAddVo.T_MIS_MONITOR_QC_ADD_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorQcAdd.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcAdd_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorQcAdd_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcAddVo tMisMonitorQcAdd_UpdateSet, TMisMonitorQcAddVo tMisMonitorQcAdd_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcAdd_UpdateSet, TMisMonitorQcAddVo.T_MIS_MONITOR_QC_ADD_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorQcAdd_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_QC_ADD where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            string strSQL = "delete from T_MIS_MONITOR_QC_ADD ";
            strSQL += this.BuildWhereStatement(tMisMonitorQcAdd);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorQcAdd"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorQcAdd)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorQcAdd.ID.ToString()));
                }
                //原始样分析结果 ID
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.RESULT_ID_SRC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_SRC = '{0}'", tMisMonitorQcAdd.RESULT_ID_SRC.ToString()));
                }
                //平行样分析结果 ID
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.RESULT_ID_ADD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_ADD = '{0}'", tMisMonitorQcAdd.RESULT_ID_ADD.ToString()));
                }
                //加标量
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.QC_ADD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_ADD = '{0}'", tMisMonitorQcAdd.QC_ADD.ToString()));
                }
                //原始测定值
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.SRC_RESULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SRC_RESULT = '{0}'", tMisMonitorQcAdd.SRC_RESULT.ToString()));
                }
                //加标测定值
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.ADD_RESULT_EX.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_RESULT_EX = '{0}'", tMisMonitorQcAdd.ADD_RESULT_EX.ToString()));
                }
                //加标回收率（%）
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.ADD_BACK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_BACK = '{0}'", tMisMonitorQcAdd.ADD_BACK.ToString()));
                }
                //加标是否合格
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.ADD_ISOK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_ISOK = '{0}'", tMisMonitorQcAdd.ADD_ISOK.ToString()));
                }
                //质控类别
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.QC_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_TYPE = '{0}'", tMisMonitorQcAdd.QC_TYPE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorQcAdd.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorQcAdd.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorQcAdd.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorQcAdd.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorQcAdd.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorQcAdd.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
