using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Seabath;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Seabath
{
  
    /// <summary>
    /// 功能：海水浴场填报
    /// 创建日期：2013-06-25
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillSeabathAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSeabath">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSeabathVo tEnvFillSeabath)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_SEABATH " + this.BuildWhereStatement(tEnvFillSeabath);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSeabathVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEABATH  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillSeabathVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSeabath">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSeabathVo Details(TEnvFillSeabathVo tEnvFillSeabath)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEABATH " + this.BuildWhereStatement(tEnvFillSeabath));
            return SqlHelper.ExecuteObject(new TEnvFillSeabathVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSeabath">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSeabathVo> SelectByObject(TEnvFillSeabathVo tEnvFillSeabath, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_SEABATH " + this.BuildWhereStatement(tEnvFillSeabath));
            return SqlHelper.ExecuteObjectList(tEnvFillSeabath, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSeabath">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSeabathVo tEnvFillSeabath, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_SEABATH {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillSeabath));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSeabath"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSeabathVo tEnvFillSeabath)
        {
            string strSQL = "select * from T_ENV_FILL_SEABATH " + this.BuildWhereStatement(tEnvFillSeabath);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSeabath">对象</param>
        /// <returns></returns>
        public TEnvFillSeabathVo SelectByObject(TEnvFillSeabathVo tEnvFillSeabath)
        {
            string strSQL = "select * from T_ENV_FILL_SEABATH " + this.BuildWhereStatement(tEnvFillSeabath);
            return SqlHelper.ExecuteObject(new TEnvFillSeabathVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillSeabath">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSeabathVo tEnvFillSeabath)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillSeabath, TEnvFillSeabathVo.T_ENV_FILL_SEABATH_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeabath">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeabathVo tEnvFillSeabath)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSeabath, TEnvFillSeabathVo.T_ENV_FILL_SEABATH_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillSeabath.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeabath_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSeabath_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeabathVo tEnvFillSeabath_UpdateSet, TEnvFillSeabathVo tEnvFillSeabath_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSeabath_UpdateSet, TEnvFillSeabathVo.T_ENV_FILL_SEABATH_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillSeabath_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_SEABATH where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillSeabathVo tEnvFillSeabath)
        {
            string strSQL = "delete from T_ENV_FILL_SEABATH ";
            strSQL += this.BuildWhereStatement(tEnvFillSeabath);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region// 构造填报表需要显示的信息
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
            dr["code"] = "POINT_ID";
            dr["name"] = "监测点名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "YEAR";
            dr["name"] = "年份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "SAMPLING_DAY";
            dr["name"] = "采样日期";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DAY";
            dr["name"] = "日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "HOUR";
            dr["name"] = "时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MINUTE";
            dr["name"] = "分";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "JUDGE";
            dr["name"] = "评价";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "OVERPROOF";
            dr["name"] = "超标污染物";
            dt.Rows.Add(dr);

            return dt;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillSeabath"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillSeabathVo tEnvFillSeabath)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillSeabath)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillSeabath.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillSeabath.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillSeabath.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillSeabath.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillSeabath.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillSeabath.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillSeabath.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillSeabath.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillSeabath.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillSeabath.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillSeabath.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillSeabath.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillSeabath.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillSeabath.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillSeabath.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillSeabath.MINUTE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillSeabath.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillSeabath.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillSeabath.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillSeabath.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillSeabath.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillSeabath.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillSeabath.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillSeabath.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillSeabath.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillSeabath.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillSeabath.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillSeabath.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillSeabath.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillSeabath.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
