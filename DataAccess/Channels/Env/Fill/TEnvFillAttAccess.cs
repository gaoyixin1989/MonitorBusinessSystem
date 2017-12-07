using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill
{
    /// <summary>
    /// 功能：环境质量附件信息表
    /// 创建日期：2014-08-04
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillAttAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAtt">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAttVo tEnvFillAtt)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_ATT " + this.BuildWhereStatement(tEnvFillAtt);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAttVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ATT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAttVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAtt">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAttVo Details(TEnvFillAttVo tEnvFillAtt)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ATT " + this.BuildWhereStatement(tEnvFillAtt));
            return SqlHelper.ExecuteObject(new TEnvFillAttVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAttVo> SelectByObject(TEnvFillAttVo tEnvFillAtt, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_ATT " + this.BuildWhereStatement(tEnvFillAtt));
            return SqlHelper.ExecuteObjectList(tEnvFillAtt, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAttVo tEnvFillAtt, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_ATT {0} ";
            if (!string.IsNullOrEmpty(tEnvFillAtt.SORT_FIELD))
            {
                strSQL += " order by " + tEnvFillAtt.SORT_FIELD;
            }
            if (!string.IsNullOrEmpty(tEnvFillAtt.SORT_TYPE))
            {
                strSQL += " " + tEnvFillAtt.SORT_TYPE;
            }
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAtt));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAtt"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAttVo tEnvFillAtt)
        {
            string strSQL = "select * from T_ENV_FILL_ATT " + this.BuildWhereStatement(tEnvFillAtt);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAtt">对象</param>
        /// <returns></returns>
        public TEnvFillAttVo SelectByObject(TEnvFillAttVo tEnvFillAtt)
        {
            string strSQL = "select * from T_ENV_FILL_ATT " + this.BuildWhereStatement(tEnvFillAtt);
            return SqlHelper.ExecuteObject(new TEnvFillAttVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAtt">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAttVo tEnvFillAtt)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAtt, TEnvFillAttVo.T_ENV_FILL_ATT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAtt">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAttVo tEnvFillAtt)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAtt, TEnvFillAttVo.T_ENV_FILL_ATT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAtt.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAtt_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAtt_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAttVo tEnvFillAtt_UpdateSet, TEnvFillAttVo tEnvFillAtt_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAtt_UpdateSet, TEnvFillAttVo.T_ENV_FILL_ATT_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAtt_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_ATT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAttVo tEnvFillAtt)
        {
            string strSQL = "delete from T_ENV_FILL_ATT ";
            strSQL += this.BuildWhereStatement(tEnvFillAtt);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAtt"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAttVo tEnvFillAtt)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAtt)
            {

                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAtt.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillAtt.YEAR.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.SEASON.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SEASON = '{0}'", tEnvFillAtt.SEASON.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillAtt.MONTH.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillAtt.DAY.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.ENVTYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENVTYPE = '{0}'", tEnvFillAtt.ENVTYPE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tEnvFillAtt.REMARK.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAtt.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAtt.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAtt.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAtt.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvFillAtt.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAtt.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
