using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.DrinkSource;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.DrinkSource
{
    /// <summary>
    /// 功能：饮用水源地数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillDrinkSrcAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_DRINK_SRC " + this.BuildWhereStatement(tEnvFillDrinkSrc);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkSrcVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_SRC  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillDrinkSrcVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkSrcVo Details(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_SRC " + this.BuildWhereStatement(tEnvFillDrinkSrc));
            return SqlHelper.ExecuteObject(new TEnvFillDrinkSrcVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDrinkSrcVo> SelectByObject(TEnvFillDrinkSrcVo tEnvFillDrinkSrc, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_DRINK_SRC " + this.BuildWhereStatement(tEnvFillDrinkSrc));
            return SqlHelper.ExecuteObjectList(tEnvFillDrinkSrc, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDrinkSrcVo tEnvFillDrinkSrc, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_DRINK_SRC {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillDrinkSrc));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDrinkSrc"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            string strSQL = "select * from T_ENV_FILL_DRINK_SRC " + this.BuildWhereStatement(tEnvFillDrinkSrc);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象</param>
        /// <returns></returns>
        public TEnvFillDrinkSrcVo SelectByObject(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            string strSQL = "select * from T_ENV_FILL_DRINK_SRC " + this.BuildWhereStatement(tEnvFillDrinkSrc);
            return SqlHelper.ExecuteObject(new TEnvFillDrinkSrcVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillDrinkSrc, TEnvFillDrinkSrcVo.T_ENV_FILL_DRINK_SRC_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDrinkSrc, TEnvFillDrinkSrcVo.T_ENV_FILL_DRINK_SRC_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillDrinkSrc.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkSrc_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDrinkSrc_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkSrcVo tEnvFillDrinkSrc_UpdateSet, TEnvFillDrinkSrcVo tEnvFillDrinkSrc_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillDrinkSrc_UpdateSet, TEnvFillDrinkSrcVo.T_ENV_FILL_DRINK_SRC_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillDrinkSrc_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_DRINK_SRC where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            string strSQL = "delete from T_ENV_FILL_DRINK_SRC ";
            strSQL += this.BuildWhereStatement(tEnvFillDrinkSrc);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillDrinkSrc"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillDrinkSrc)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillDrinkSrc.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvFillDrinkSrc.SECTION_ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillDrinkSrc.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillDrinkSrc.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillDrinkSrc.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillDrinkSrc.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillDrinkSrc.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillDrinkSrc.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillDrinkSrc.MINUTE.ToString()));
                }
                //枯水期、平水期、枯水期
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.KPF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KPF = '{0}'", tEnvFillDrinkSrc.KPF.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillDrinkSrc.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillDrinkSrc.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillDrinkSrc.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillDrinkSrc.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillDrinkSrc.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillDrinkSrc.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillDrinkSrc.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillDrinkSrc.REMARK5.ToString()));
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
            dr["code"] = "SECTION_ID";
            dr["name"] = "断面名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "POINT_ID";
            dr["name"] = "垂线名称";
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
            dr["code"] = "KPF";
            dr["name"] = "水期";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "OVERPROOF";
            dr["name"] = "超标污染物";
            dt.Rows.Add(dr);

            return dt;
        }
    }

}
