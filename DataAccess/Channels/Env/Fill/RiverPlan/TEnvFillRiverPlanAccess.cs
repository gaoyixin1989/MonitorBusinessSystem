using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.RiverPlan;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.RiverPlan
{
    /// <summary>
    /// 功能：规划断面
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverPlanAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RIVER_PLAN " + this.BuildWhereStatement(tEnvFillRiverPlan);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverPlanVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_PLAN  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillRiverPlanVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverPlanVo Details(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_PLAN " + this.BuildWhereStatement(tEnvFillRiverPlan));
            return SqlHelper.ExecuteObject(new TEnvFillRiverPlanVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverPlanVo> SelectByObject(TEnvFillRiverPlanVo tEnvFillRiverPlan, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_PLAN " + this.BuildWhereStatement(tEnvFillRiverPlan));
            return SqlHelper.ExecuteObjectList(tEnvFillRiverPlan, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverPlanVo tEnvFillRiverPlan, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_RIVER_PLAN {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRiverPlan));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_PLAN " + this.BuildWhereStatement(tEnvFillRiverPlan);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象</param>
        /// <returns></returns>
        public TEnvFillRiverPlanVo SelectByObject(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_PLAN " + this.BuildWhereStatement(tEnvFillRiverPlan);
            return SqlHelper.ExecuteObject(new TEnvFillRiverPlanVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRiverPlan, TEnvFillRiverPlanVo.T_ENV_FILL_RIVER_PLAN_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverPlan">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverPlan, TEnvFillRiverPlanVo.T_ENV_FILL_RIVER_PLAN_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRiverPlan.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverPlan_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverPlan_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverPlanVo tEnvFillRiverPlan_UpdateSet, TEnvFillRiverPlanVo tEnvFillRiverPlan_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverPlan_UpdateSet, TEnvFillRiverPlanVo.T_ENV_FILL_RIVER_PLAN_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRiverPlan_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RIVER_PLAN where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            string strSQL = "delete from T_ENV_FILL_RIVER_PLAN ";
            strSQL += this.BuildWhereStatement(tEnvFillRiverPlan);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            string strSQL = "select a.ID,SECTION_NAME,VERTICAL_NAME from T_ENV_P_RIVER_PLAN a inner join T_ENV_P_RIVER_PLAN_V b on(a.ID=b.SECTION_ID) where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and a.IS_DEL='0'";
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
        /// <param name="tEnvFillRiverPlan"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRiverPlan)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRiverPlan.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvFillRiverPlan.SECTION_ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillRiverPlan.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillRiverPlan.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillRiverPlan.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillRiverPlan.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillRiverPlan.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillRiverPlan.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillRiverPlan.MINUTE.ToString()));
                }
                //枯水期、平水期、枯水期
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.KPF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KPF = '{0}'", tEnvFillRiverPlan.KPF.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillRiverPlan.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillRiverPlan.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRiverPlan.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRiverPlan.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRiverPlan.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRiverPlan.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRiverPlan.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRiverPlan.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
