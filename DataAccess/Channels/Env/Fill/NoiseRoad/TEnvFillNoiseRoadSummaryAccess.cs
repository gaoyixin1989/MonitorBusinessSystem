using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseRoad;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.NoiseRoad
{
    /// <summary>
    /// 功能：道路交通噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseRoadSummaryAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_NOISE_ROAD_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseRoadSummary);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseRoadSummaryVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_ROAD_SUMMARY  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillNoiseRoadSummaryVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseRoadSummaryVo Details(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_ROAD_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseRoadSummary));
            return SqlHelper.ExecuteObject(new TEnvFillNoiseRoadSummaryVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseRoadSummaryVo> SelectByObject(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_ROAD_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseRoadSummary));
            return SqlHelper.ExecuteObjectList(tEnvFillNoiseRoadSummary, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_NOISE_ROAD_SUMMARY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillNoiseRoadSummary));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_ROAD_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseRoadSummary);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseRoadSummaryVo SelectByObject(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_ROAD_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseRoadSummary);
            return SqlHelper.ExecuteObject(new TEnvFillNoiseRoadSummaryVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillNoiseRoadSummary, TEnvFillNoiseRoadSummaryVo.T_ENV_FILL_NOISE_ROAD_SUMMARY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseRoadSummary, TEnvFillNoiseRoadSummaryVo.T_ENV_FILL_NOISE_ROAD_SUMMARY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillNoiseRoadSummary.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseRoadSummary_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary_UpdateSet, TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseRoadSummary_UpdateSet, TEnvFillNoiseRoadSummaryVo.T_ENV_FILL_NOISE_ROAD_SUMMARY_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillNoiseRoadSummary_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_NOISE_ROAD_SUMMARY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            string strSQL = "delete from T_ENV_FILL_NOISE_ROAD_SUMMARY ";
            strSQL += this.BuildWhereStatement(tEnvFillNoiseRoadSummary);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillNoiseRoadSummary)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillNoiseRoadSummary.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillNoiseRoadSummary.YEAR.ToString()));
                }
                //道路交通干线数目
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.LINE_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LINE_COUNT = '{0}'", tEnvFillNoiseRoadSummary.LINE_COUNT.ToString()));
                }
                //有效测点数
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.VALID_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VALID_COUNT = '{0}'", tEnvFillNoiseRoadSummary.VALID_COUNT.ToString()));
                }
                //监测起始月
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.BEGIN_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MONTH = '{0}'", tEnvFillNoiseRoadSummary.BEGIN_MONTH.ToString()));
                }
                //监测起始日
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.BEGIN_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_DAY = '{0}'", tEnvFillNoiseRoadSummary.BEGIN_DAY.ToString()));
                }
                //监测结束月
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.END_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_MONTH = '{0}'", tEnvFillNoiseRoadSummary.END_MONTH.ToString()));
                }
                //监测结束日
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.END_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_DAY = '{0}'", tEnvFillNoiseRoadSummary.END_DAY.ToString()));
                }
                //机动车拥有量
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.TRAFFIC_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TRAFFIC_COUNT = '{0}'", tEnvFillNoiseRoadSummary.TRAFFIC_COUNT.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillNoiseRoadSummary.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillNoiseRoadSummary.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillNoiseRoadSummary.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillNoiseRoadSummary.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillNoiseRoadSummary.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadSummary.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillNoiseRoadSummary.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 单个更新道路交通噪声汇总表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ColName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int UpdateSummary(string ID, string ColName, string Value)
        {
            string sql = "";

            sql = "update " + TEnvFillNoiseRoadSummaryVo.T_ENV_FILL_NOISE_ROAD_SUMMARY_TABLE + " set " + ColName + "='" + Value + "' where ID='" + ID + "'";

            return ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 统一运算某一年的道路交通噪声汇总表
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public int OperSummary(string Year)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("update T_ENV_FILL_NOISE_ROAD_SUMMARY set ");
            sql.Append("LINE_COUNT=(select COUNT(distinct(ROAD_NAME)) from T_ENV_P_NOISE_ROAD where YEAR='"+Year+"' and IS_DEL='0'),");
            sql.Append("VALID_COUNT=(select COUNT(1) from T_ENV_P_NOISE_ROAD where YEAR='"+Year+"' and IS_DEL='0'),");
            sql.Append("BEGIN_MONTH=(select MONTH(MIN(cast(YEAR+'-'+BEGIN_MONTH+'-'+isnull(BEGIN_DAY,'1') as datetime))) from T_ENV_FILL_NOISE_ROAD where YEAR='"+Year+"'),");
            sql.Append("BEGIN_DAY=(select DAY(MIN(cast(YEAR+'-'+BEGIN_MONTH+'-'+isnull(BEGIN_DAY,'1') as datetime))) from T_ENV_FILL_NOISE_ROAD where YEAR='"+Year+"'),");
            sql.Append("END_MONTH=(select MONTH(MAX(cast(YEAR+'-'+BEGIN_MONTH+'-'+isnull(BEGIN_DAY,'1') as datetime))) from T_ENV_FILL_NOISE_ROAD where YEAR='"+Year+"'),");
            sql.Append("END_DAY=(select DAY(MAX(cast(YEAR+'-'+BEGIN_MONTH+'-'+isnull(BEGIN_DAY,'1') as datetime))) from T_ENV_FILL_NOISE_ROAD where YEAR='"+Year+"')");
            sql.Append("where YEAR='"+Year+"'");

            return ExecuteNonQuery(sql.ToString());
        }
    }

}
