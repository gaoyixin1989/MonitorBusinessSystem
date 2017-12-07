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
    public class TEnvPEnterinfoAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            string strSQL = "select Count(*) from T_ENV_P_ENTERINFO " + this.BuildWhereStatement(tEnvPEnterinfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPEnterinfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_ENTERINFO  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPEnterinfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPEnterinfoVo Details(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            string strSQL = String.Format("select * from  T_ENV_P_ENTERINFO " + this.BuildWhereStatement(tEnvPEnterinfo));
            return SqlHelper.ExecuteObject(new TEnvPEnterinfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPEnterinfoVo> SelectByObject(TEnvPEnterinfoVo tEnvPEnterinfo, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_ENTERINFO " + this.BuildWhereStatement(tEnvPEnterinfo));
            return SqlHelper.ExecuteObjectList(tEnvPEnterinfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPEnterinfoVo tEnvPEnterinfo, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_ENTERINFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPEnterinfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPEnterinfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            string strSQL = "select * from T_ENV_P_ENTERINFO " + this.BuildWhereStatement(tEnvPEnterinfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象</param>
        /// <returns></returns>
        public TEnvPEnterinfoVo SelectByObject(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            string strSQL = "select * from T_ENV_P_ENTERINFO " + this.BuildWhereStatement(tEnvPEnterinfo);
            return SqlHelper.ExecuteObject(new TEnvPEnterinfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPEnterinfo, TEnvPEnterinfoVo.T_ENV_P_ENTERINFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPEnterinfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPEnterinfo, TEnvPEnterinfoVo.T_ENV_P_ENTERINFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPEnterinfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPEnterinfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPEnterinfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPEnterinfoVo tEnvPEnterinfo_UpdateSet, TEnvPEnterinfoVo tEnvPEnterinfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPEnterinfo_UpdateSet, TEnvPEnterinfoVo.T_ENV_P_ENTERINFO_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPEnterinfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_ENTERINFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            string strSQL = "delete from T_ENV_P_ENTERINFO ";
            strSQL += this.BuildWhereStatement(tEnvPEnterinfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPEnterinfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPEnterinfo)
            {
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.IS_DEL.ToString().Trim())) 
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPEnterinfo.IS_DEL.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPEnterinfo.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.ENTER_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENTER_NAME = '{0}'", tEnvPEnterinfo.ENTER_NAME.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.ENTER_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ENTER_CODE = '{0}'", tEnvPEnterinfo.ENTER_CODE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPEnterinfo.PROVINCE_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPEnterinfo.AREA_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.LEVEL1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LEVEL1 = '{0}'", tEnvPEnterinfo.LEVEL1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.LEVEL2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LEVEL2 = '{0}'", tEnvPEnterinfo.LEVEL2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.LEVEL3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LEVEL3 = '{0}'", tEnvPEnterinfo.LEVEL3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPEnterinfo.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPEnterinfo.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPEnterinfo.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPEnterinfo.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPEnterinfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPEnterinfo.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
