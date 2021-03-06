using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Linq;
using i3.ValueObject.Channels.Env.Fill.Dust;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.Dust
{
    /// <summary>
    /// 功能：降尘数据填报表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改时间：2013-6-24
    /// 修改人：刘静楠
    /// </summary>
    public class TEnvFillDustAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDust">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDustVo tEnvFillDust)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_DUST " + this.BuildWhereStatement(tEnvFillDust);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDustVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DUST  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillDustVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDust">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDustVo Details(TEnvFillDustVo tEnvFillDust)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DUST " + this.BuildWhereStatement(tEnvFillDust));
            return SqlHelper.ExecuteObject(new TEnvFillDustVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDust">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDustVo> SelectByObject(TEnvFillDustVo tEnvFillDust, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_DUST " + this.BuildWhereStatement(tEnvFillDust));
            return SqlHelper.ExecuteObjectList(tEnvFillDust, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDust">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDustVo tEnvFillDust, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_DUST {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillDust));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDust"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDustVo tEnvFillDust)
        {
            string strSQL = "select * from T_ENV_FILL_DUST " + this.BuildWhereStatement(tEnvFillDust);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDust">对象</param>
        /// <returns></returns>
        public TEnvFillDustVo SelectByObject(TEnvFillDustVo tEnvFillDust)
        {
            string strSQL = "select * from T_ENV_FILL_DUST " + this.BuildWhereStatement(tEnvFillDust);
            return SqlHelper.ExecuteObject(new TEnvFillDustVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillDust">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDustVo tEnvFillDust)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillDust, TEnvFillDustVo.T_ENV_FILL_DUST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDust">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDustVo tEnvFillDust)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDust, TEnvFillDustVo.T_ENV_FILL_DUST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillDust.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDust_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDust_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDustVo tEnvFillDust_UpdateSet, TEnvFillDustVo tEnvFillDust_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDust_UpdateSet, TEnvFillDustVo.T_ENV_FILL_DUST_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillDust_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_DUST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillDustVo tEnvFillDust)
        {
            string strSQL = "delete from T_ENV_FILL_DUST ";
            strSQL += this.BuildWhereStatement(tEnvFillDust);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        

        #endregion

        #region// 构造填报表需要显示的信息(ljn)
        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("code", typeof(String));
            dt.Columns.Add("name", typeof(String));

            dr = dt.NewRow();
            dr["code"] = "YEAR";
            dr["name"] = "年份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "POINT_ID";
            dr["name"] = "监测点名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_MONTH";
            dr["name"] = "监测起始月";
            dt.Rows.Add(dr); 

            dr = dt.NewRow();
            dr["code"] = "BEGIN_DAY";
            dr["name"] = "监测起始日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_HOUR";
            dr["name"] = "监测起始时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_MINUTE";
            dr["name"] = "监测起始分";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_MONTH";
            dr["name"] = "监测结束月";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_DAY";
            dr["name"] = "监测结束日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_HOUR";
            dr["name"] = "监测结束时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_MINUTE";
            dr["name"] = "监测结束分";
            dt.Rows.Add(dr);

            return dt;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillDust"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillDustVo tEnvFillDust)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillDust)
            {
                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillDust.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillDust.ID.ToString()));
                }
                //降尘监测点ID，对应T_BAS_POINT_DUST表主键
                if (!String.IsNullOrEmpty(tEnvFillDust.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillDust.POINT_ID.ToString()));
                }
                //年
                if (!String.IsNullOrEmpty(tEnvFillDust.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillDust.YEAR.ToString()));
                }
                //月
                if (!String.IsNullOrEmpty(tEnvFillDust.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillDust.MONTH.ToString()));
                }
                //二月
                if (!String.IsNullOrEmpty(tEnvFillDust.BEGIN_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MONTH = '{0}'", tEnvFillDust.BEGIN_MONTH.ToString()));
                }
                //三月
                if (!String.IsNullOrEmpty(tEnvFillDust.BEGIN_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_DAY = '{0}'", tEnvFillDust.BEGIN_DAY.ToString()));
                }
                //四月
                if (!String.IsNullOrEmpty(tEnvFillDust.BEGIN_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_HOUR = '{0}'", tEnvFillDust.BEGIN_HOUR.ToString()));
                }
                //五月
                if (!String.IsNullOrEmpty(tEnvFillDust.BEGIN_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MINUTE = '{0}'", tEnvFillDust.BEGIN_MINUTE.ToString()));
                }
                //六月
                if (!String.IsNullOrEmpty(tEnvFillDust.END_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_MONTH = '{0}'", tEnvFillDust.END_MONTH.ToString()));
                }
                //七月
                if (!String.IsNullOrEmpty(tEnvFillDust.END_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_DAY = '{0}'", tEnvFillDust.END_DAY.ToString()));
                }
                //八月
                if (!String.IsNullOrEmpty(tEnvFillDust.END_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_HOUR = '{0}'", tEnvFillDust.END_HOUR.ToString()));
                }
                //九月
                if (!String.IsNullOrEmpty(tEnvFillDust.END_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_MINUTE = '{0}'", tEnvFillDust.END_MINUTE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillDust.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillDust.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillDust.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillDust.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillDust.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillDust.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillDust.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillDust.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillDust.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillDust.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
