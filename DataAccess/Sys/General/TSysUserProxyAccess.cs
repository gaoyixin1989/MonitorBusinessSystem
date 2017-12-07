using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.General;
using i3.ValueObject;

namespace i3.DataAccess.Sys.General
{
    /// <summary>
    /// 功能：出差代理
    /// 创建日期：2012-11-15
    /// 创建人：潘德军
    /// </summary>
    public class TSysUserProxyAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUserProxy">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserProxyVo tSysUserProxy)
        {
            string strSQL = "select Count(*) from T_SYS_USER_PROXY " + this.BuildWhereStatement(tSysUserProxy);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserProxyVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_USER_PROXY  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TSysUserProxyVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUserProxy">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserProxyVo Details(TSysUserProxyVo tSysUserProxy)
        {
            string strSQL = String.Format("select * from  T_SYS_USER_PROXY " + this.BuildWhereStatement(tSysUserProxy));
            return SqlHelper.ExecuteObject(new TSysUserProxyVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUserProxy">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserProxyVo> SelectByObject(TSysUserProxyVo tSysUserProxy, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_SYS_USER_PROXY " + this.BuildWhereStatement(tSysUserProxy));
            return SqlHelper.ExecuteObjectList(tSysUserProxy, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUserProxy">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserProxyVo tSysUserProxy, int iIndex, int iCount)
        {

            string strSQL = " select * from T_SYS_USER_PROXY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUserProxy));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUserProxy"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserProxyVo tSysUserProxy)
        {
            string strSQL = "select * from T_SYS_USER_PROXY " + this.BuildWhereStatement(tSysUserProxy);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUserProxy">对象</param>
        /// <returns></returns>
        public TSysUserProxyVo SelectByObject(TSysUserProxyVo tSysUserProxy)
        {
            string strSQL = "select * from T_SYS_USER_PROXY " + this.BuildWhereStatement(tSysUserProxy);
            return SqlHelper.ExecuteObject(new TSysUserProxyVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysUserProxy">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserProxyVo tSysUserProxy)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysUserProxy, TSysUserProxyVo.T_SYS_USER_PROXY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserProxy">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserProxyVo tSysUserProxy)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysUserProxy, TSysUserProxyVo.T_SYS_USER_PROXY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysUserProxy.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserProxy_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysUserProxy_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserProxyVo tSysUserProxy_UpdateSet, TSysUserProxyVo tSysUserProxy_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysUserProxy_UpdateSet, TSysUserProxyVo.T_SYS_USER_PROXY_TABLE);
            strSQL += this.BuildWhereStatement(tSysUserProxy_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_SYS_USER_PROXY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TSysUserProxyVo tSysUserProxy)
        {
            string strSQL = "delete from T_SYS_USER_PROXY ";
            strSQL += this.BuildWhereStatement(tSysUserProxy);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysUserProxy"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysUserProxyVo tSysUserProxy)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysUserProxy)
            {

                //编号
                if (!String.IsNullOrEmpty(tSysUserProxy.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysUserProxy.ID.ToString()));
                }
                //用户编号
                if (!String.IsNullOrEmpty(tSysUserProxy.USER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND USER_ID = '{0}'", tSysUserProxy.USER_ID.ToString()));
                }
                //被代理人ID
                if (!String.IsNullOrEmpty(tSysUserProxy.PROXY_USER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROXY_USER_ID = '{0}'", tSysUserProxy.PROXY_USER_ID.ToString()));
                }
                //备注
                if (!String.IsNullOrEmpty(tSysUserProxy.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysUserProxy.REMARK.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tSysUserProxy.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysUserProxy.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tSysUserProxy.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysUserProxy.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tSysUserProxy.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysUserProxy.REMARK3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
