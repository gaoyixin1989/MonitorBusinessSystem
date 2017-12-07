using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Fill.DrinkUnder;

namespace i3.DataAccess.Channels.Env.Fill.DrinkUnder
{
    /// <summary>
    /// 功能：地下水填报
    /// 创建日期：2013-06-22
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillDrinkUnderAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_DRINK_UNDER " + this.BuildWhereStatement(tEnvFillDrinkUnder);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkUnderVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_UNDER  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillDrinkUnderVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkUnderVo Details(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_UNDER " + this.BuildWhereStatement(tEnvFillDrinkUnder));
            return SqlHelper.ExecuteObject(new TEnvFillDrinkUnderVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDrinkUnderVo> SelectByObject(TEnvFillDrinkUnderVo tEnvFillDrinkUnder, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_UNDER " + this.BuildWhereStatement(tEnvFillDrinkUnder));
            return SqlHelper.ExecuteObjectList(tEnvFillDrinkUnder, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDrinkUnderVo tEnvFillDrinkUnder, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_DRINK_UNDER {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillDrinkUnder));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDrinkUnder"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            string strSQL = "select * from T_ENV_FILL_DRINK_UNDER " + this.BuildWhereStatement(tEnvFillDrinkUnder);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象</param>
        /// <returns></returns>
        public TEnvFillDrinkUnderVo SelectByObject(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            string strSQL = "select * from T_ENV_FILL_DRINK_UNDER " + this.BuildWhereStatement(tEnvFillDrinkUnder);
            return SqlHelper.ExecuteObject(new TEnvFillDrinkUnderVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillDrinkUnder, TEnvFillDrinkUnderVo.T_ENV_FILL_DRINK_UNDER_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDrinkUnder, TEnvFillDrinkUnderVo.T_ENV_FILL_DRINK_UNDER_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillDrinkUnder.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkUnder_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDrinkUnder_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkUnderVo tEnvFillDrinkUnder_UpdateSet, TEnvFillDrinkUnderVo tEnvFillDrinkUnder_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDrinkUnder_UpdateSet, TEnvFillDrinkUnderVo.T_ENV_FILL_DRINK_UNDER_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillDrinkUnder_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_DRINK_UNDER where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            string strSQL = "delete from T_ENV_FILL_DRINK_UNDER ";
            strSQL += this.BuildWhereStatement(tEnvFillDrinkUnder);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillDrinkUnder"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillDrinkUnder)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillDrinkUnder.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillDrinkUnder.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillDrinkUnder.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillDrinkUnder.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillDrinkUnder.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillDrinkUnder.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillDrinkUnder.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillDrinkUnder.MINUTE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillDrinkUnder.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillDrinkUnder.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillDrinkUnder.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillDrinkUnder.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillDrinkUnder.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillDrinkUnder.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillDrinkUnder.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillDrinkUnder.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

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
            dr["name"] = "测点名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DAY";
            dr["name"] = "日期";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "OVERPROOF";
            dr["name"] = "超标污染物";
            dt.Rows.Add(dr);

            return dt;
        }
    }

}
