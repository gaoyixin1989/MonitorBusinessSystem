using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseArea;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.NoiseArea
{
    /// <summary>
    /// 功能：区域环境噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseAreaSummaryAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_NOISE_AREA_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseAreaSummary);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaSummaryVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_AREA_SUMMARY  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillNoiseAreaSummaryVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaSummaryVo Details(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_AREA_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseAreaSummary));
            return SqlHelper.ExecuteObject(new TEnvFillNoiseAreaSummaryVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseAreaSummaryVo> SelectByObject(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_AREA_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseAreaSummary));
            return SqlHelper.ExecuteObjectList(tEnvFillNoiseAreaSummary, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_NOISE_AREA_SUMMARY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillNoiseAreaSummary));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_AREA_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseAreaSummary);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseAreaSummaryVo SelectByObject(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_AREA_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseAreaSummary);
            return SqlHelper.ExecuteObject(new TEnvFillNoiseAreaSummaryVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillNoiseAreaSummary, TEnvFillNoiseAreaSummaryVo.T_ENV_FILL_NOISE_AREA_SUMMARY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseAreaSummary, TEnvFillNoiseAreaSummaryVo.T_ENV_FILL_NOISE_AREA_SUMMARY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillNoiseAreaSummary.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseAreaSummary_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary_UpdateSet, TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseAreaSummary_UpdateSet, TEnvFillNoiseAreaSummaryVo.T_ENV_FILL_NOISE_AREA_SUMMARY_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillNoiseAreaSummary_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_NOISE_AREA_SUMMARY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            string strSQL = "delete from T_ENV_FILL_NOISE_AREA_SUMMARY ";
            strSQL += this.BuildWhereStatement(tEnvFillNoiseAreaSummary);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillNoiseAreaSummary)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillNoiseAreaSummary.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillNoiseAreaSummary.YEAR.ToString()));
                }
                //有效测点数
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.VALID_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VALID_COUNT = '{0}'", tEnvFillNoiseAreaSummary.VALID_COUNT.ToString()));
                }
                //城区人口
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.PEOPLE_COUNT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PEOPLE_COUNT = '{0}'", tEnvFillNoiseAreaSummary.PEOPLE_COUNT.ToString()));
                }
                //监测起始月
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.BEGIN_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MONTH = '{0}'", tEnvFillNoiseAreaSummary.BEGIN_MONTH.ToString()));
                }
                //监测起始日
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.BEGIN_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_DAY = '{0}'", tEnvFillNoiseAreaSummary.BEGIN_DAY.ToString()));
                }
                //监测结束月
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.END_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_MONTH = '{0}'", tEnvFillNoiseAreaSummary.END_MONTH.ToString()));
                }
                //监测结束日
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.END_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_DAY = '{0}'", tEnvFillNoiseAreaSummary.END_DAY.ToString()));
                }
                //城区人口密度
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.PEOPLE_DENSITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PEOPLE_DENSITY = '{0}'", tEnvFillNoiseAreaSummary.PEOPLE_DENSITY.ToString()));
                }
                //建成区面积
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.AREA.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA = '{0}'", tEnvFillNoiseAreaSummary.AREA.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillNoiseAreaSummary.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillNoiseAreaSummary.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillNoiseAreaSummary.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillNoiseAreaSummary.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillNoiseAreaSummary.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaSummary.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillNoiseAreaSummary.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 单个更新区域环境噪声汇总表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ColName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int UpdateSummary(string ID, string ColName, string Value)
        {
            string sql = "";

            sql = "update " + TEnvFillNoiseAreaSummaryVo.T_ENV_FILL_NOISE_AREA_SUMMARY_TABLE + " set " + ColName + "='" + Value + "' where ID='" + ID + "'";

            return ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 统一运算某一年的区域噪声汇总表
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public int OperSummary(string Year)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("update T_ENV_FILL_NOISE_AREA_SUMMARY set ");
            sql.Append("VALID_COUNT=(select COUNT(1) from T_ENV_P_NOISE_AREA where YEAR='" + Year + "' and IS_DEL='0'),");
            sql.Append("BEGIN_MONTH=(select MONTH(MIN(cast(YEAR+'-'+BEGIN_MONTH+'-'+isnull(BEGIN_DAY,'1') as datetime))) from T_ENV_FILL_NOISE_AREA where YEAR='" + Year + "'),");
            sql.Append("BEGIN_DAY=(select DAY(MIN(cast(YEAR+'-'+BEGIN_MONTH+'-'+isnull(BEGIN_DAY,'1') as datetime))) from T_ENV_FILL_NOISE_AREA where YEAR='" + Year + "'),");
            sql.Append("END_MONTH=(select MONTH(MAX(cast(YEAR+'-'+BEGIN_MONTH+'-'+isnull(BEGIN_DAY,'1') as datetime))) from T_ENV_FILL_NOISE_AREA where YEAR='" + Year + "'),");
            sql.Append("END_DAY=(select DAY(MAX(cast(YEAR+'-'+BEGIN_MONTH+'-'+isnull(BEGIN_DAY,'1') as datetime))) from T_ENV_FILL_NOISE_AREA where YEAR='" + Year + "')");
            sql.Append("where YEAR='" + Year + "'");

            return ExecuteNonQuery(sql.ToString());
        }
    }

}
