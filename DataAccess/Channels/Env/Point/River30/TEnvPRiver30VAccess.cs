using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.River30;

namespace i3.DataAccess.Channels.Env.Point.River30
{
    /// <summary>
    /// 功能：双三十废水
    /// 创建日期：2013-06-17
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiver30VAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiver30V">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiver30VVo tEnvPRiver30V)
        {
            string strSQL = "select Count(*) from T_ENV_P_RIVER30_V " + this.BuildWhereStatement(tEnvPRiver30V);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiver30VVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER30_V  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPRiver30VVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiver30V">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiver30VVo Details(TEnvPRiver30VVo tEnvPRiver30V)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER30_V " + this.BuildWhereStatement(tEnvPRiver30V));
            return SqlHelper.ExecuteObject(new TEnvPRiver30VVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiver30V">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiver30VVo> SelectByObject(TEnvPRiver30VVo tEnvPRiver30V, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_RIVER30_V " + this.BuildWhereStatement(tEnvPRiver30V));
            return SqlHelper.ExecuteObjectList(tEnvPRiver30V, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiver30V">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiver30VVo tEnvPRiver30V, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_RIVER30_V {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPRiver30V));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiver30V"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiver30VVo tEnvPRiver30V)
        {
            string strSQL = "select * from T_ENV_P_RIVER30_V " + this.BuildWhereStatement(tEnvPRiver30V);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiver30V">对象</param>
        /// <returns></returns>
        public TEnvPRiver30VVo SelectByObject(TEnvPRiver30VVo tEnvPRiver30V)
        {
            string strSQL = "select * from T_ENV_P_RIVER30_V " + this.BuildWhereStatement(tEnvPRiver30V);
            return SqlHelper.ExecuteObject(new TEnvPRiver30VVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPRiver30V">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiver30VVo tEnvPRiver30V)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPRiver30V, TEnvPRiver30VVo.T_ENV_P_RIVER30_V_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiver30V">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiver30VVo tEnvPRiver30V)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiver30V, TEnvPRiver30VVo.T_ENV_P_RIVER30_V_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPRiver30V.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiver30V_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiver30V_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiver30VVo tEnvPRiver30V_UpdateSet, TEnvPRiver30VVo tEnvPRiver30V_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiver30V_UpdateSet, TEnvPRiver30VVo.T_ENV_P_RIVER30_V_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPRiver30V_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_RIVER30_V where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPRiver30VVo tEnvPRiver30V)
        {
            string strSQL = "delete from T_ENV_P_RIVER30_V ";
            strSQL += this.BuildWhereStatement(tEnvPRiver30V);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPRiver30V"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPRiver30VVo tEnvPRiver30V)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPRiver30V)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPRiver30V.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPRiver30V.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvPRiver30V.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvPRiver30V.SECTION_ID.ToString()));
                }
                //垂线名称
                if (!String.IsNullOrEmpty(tEnvPRiver30V.VERTICAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_NAME = '{0}'", tEnvPRiver30V.VERTICAL_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPRiver30V.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPRiver30V.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPRiver30V.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPRiver30V.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPRiver30V.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPRiver30V.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPRiver30V.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPRiver30V.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPRiver30V.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPRiver30V.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
