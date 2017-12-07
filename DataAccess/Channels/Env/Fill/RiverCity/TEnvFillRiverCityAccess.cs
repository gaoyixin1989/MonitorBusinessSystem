using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.RiverCity;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.RiverCity
{
    /// <summary>
    /// 功能：城考
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverCityAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RIVER_CITY " + this.BuildWhereStatement(tEnvFillRiverCity);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverCityVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_CITY  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillRiverCityVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverCityVo Details(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_CITY " + this.BuildWhereStatement(tEnvFillRiverCity));
            return SqlHelper.ExecuteObject(new TEnvFillRiverCityVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverCityVo> SelectByObject(TEnvFillRiverCityVo tEnvFillRiverCity, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_CITY " + this.BuildWhereStatement(tEnvFillRiverCity));
            return SqlHelper.ExecuteObjectList(tEnvFillRiverCity, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverCityVo tEnvFillRiverCity, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_RIVER_CITY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRiverCity));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverCity"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_CITY " + this.BuildWhereStatement(tEnvFillRiverCity);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象</param>
        /// <returns></returns>
        public TEnvFillRiverCityVo SelectByObject(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_CITY " + this.BuildWhereStatement(tEnvFillRiverCity);
            return SqlHelper.ExecuteObject(new TEnvFillRiverCityVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRiverCity, TEnvFillRiverCityVo.T_ENV_FILL_RIVER_CITY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverCity">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverCity, TEnvFillRiverCityVo.T_ENV_FILL_RIVER_CITY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRiverCity.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverCity_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverCity_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverCityVo tEnvFillRiverCity_UpdateSet, TEnvFillRiverCityVo tEnvFillRiverCity_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverCity_UpdateSet, TEnvFillRiverCityVo.T_ENV_FILL_RIVER_CITY_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRiverCity_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RIVER_CITY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            string strSQL = "delete from T_ENV_FILL_RIVER_CITY ";
            strSQL += this.BuildWhereStatement(tEnvFillRiverCity);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region //新增代码
        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            string strSQL = "select a.ID,SECTION_NAME,VERTICAL_NAME from T_ENV_P_RIVER_CITY a inner join T_ENV_P_RIVER_CITY_V b on(a.ID=b.SECTION_ID) where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and a.IS_DEL='0'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
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
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillRiverCity"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRiverCity)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRiverCity.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvFillRiverCity.SECTION_ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillRiverCity.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillRiverCity.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillRiverCity.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillRiverCity.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillRiverCity.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillRiverCity.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillRiverCity.MINUTE.ToString()));
                }
                //枯水期、平水期、枯水期
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.KPF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KPF = '{0}'", tEnvFillRiverCity.KPF.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillRiverCity.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillRiverCity.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRiverCity.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRiverCity.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRiverCity.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRiverCity.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRiverCity.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRiverCity.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
