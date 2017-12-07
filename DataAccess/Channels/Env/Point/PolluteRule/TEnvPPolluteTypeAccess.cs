using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.PolluteRule;

namespace i3.DataAccess.Channels.Env.Point.PolluteRule
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-29
    /// 创建人：
    /// </summary>
    public class TEnvPPolluteTypeAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPPolluteType">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            string strSQL = "select Count(*) from T_ENV_P_POLLUTE_TYPE " + this.BuildWhereStatement(tEnvPPolluteType);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPPolluteTypeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_POLLUTE_TYPE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPPolluteTypeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPPolluteType">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPPolluteTypeVo Details(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            string strSQL = String.Format("select * from  T_ENV_P_POLLUTE_TYPE " + this.BuildWhereStatement(tEnvPPolluteType));
            return SqlHelper.ExecuteObject(new TEnvPPolluteTypeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPPolluteType">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPPolluteTypeVo> SelectByObject(TEnvPPolluteTypeVo tEnvPPolluteType, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_POLLUTE_TYPE " + this.BuildWhereStatement(tEnvPPolluteType));
            return SqlHelper.ExecuteObjectList(tEnvPPolluteType, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPPolluteType">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPPolluteTypeVo tEnvPPolluteType, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_POLLUTE_TYPE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPPolluteType));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPPolluteType"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            string strSQL = "select * from T_ENV_P_POLLUTE_TYPE " + this.BuildWhereStatement(tEnvPPolluteType);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPPolluteType">对象</param>
        /// <returns></returns>
        public TEnvPPolluteTypeVo SelectByObject(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            string strSQL = "select * from T_ENV_P_POLLUTE_TYPE " + this.BuildWhereStatement(tEnvPPolluteType);
            return SqlHelper.ExecuteObject(new TEnvPPolluteTypeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPPolluteType">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPPolluteType, TEnvPPolluteTypeVo.T_ENV_P_POLLUTE_TYPE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPolluteType">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPPolluteType, TEnvPPolluteTypeVo.T_ENV_P_POLLUTE_TYPE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPPolluteType.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPolluteType_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPPolluteType_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteTypeVo tEnvPPolluteType_UpdateSet, TEnvPPolluteTypeVo tEnvPPolluteType_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPPolluteType_UpdateSet, TEnvPPolluteTypeVo.T_ENV_P_POLLUTE_TYPE_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPPolluteType_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_POLLUTE_TYPE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            string strSQL = "delete from T_ENV_P_POLLUTE_TYPE ";
            strSQL += this.BuildWhereStatement(tEnvPPolluteType);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPPolluteType"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPPolluteType)
            {
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteType.IS_DEL.ToString().Trim())) 
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPPolluteType.IS_DEL.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteType.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPPolluteType.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteType.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPPolluteType.SATAIONS_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteType.TYPE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TYPE_NAME = '{0}'", tEnvPPolluteType.TYPE_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteType.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPPolluteType.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteType.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPPolluteType.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteType.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPPolluteType.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteType.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPPolluteType.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPPolluteType.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPPolluteType.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
