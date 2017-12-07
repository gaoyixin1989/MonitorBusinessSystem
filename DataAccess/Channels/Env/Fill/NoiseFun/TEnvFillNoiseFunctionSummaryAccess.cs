using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseFun;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.NoiseFun
{
    /// <summary>
    /// 功能：功能区噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseFunctionSummaryAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_NOISE_FUNCTION_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseFunctionSummary);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionSummaryVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_FUNCTION_SUMMARY  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillNoiseFunctionSummaryVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionSummaryVo Details(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_FUNCTION_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseFunctionSummary));
            return SqlHelper.ExecuteObject(new TEnvFillNoiseFunctionSummaryVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseFunctionSummaryVo> SelectByObject(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_FUNCTION_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseFunctionSummary));
            return SqlHelper.ExecuteObjectList(tEnvFillNoiseFunctionSummary, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_NOISE_FUNCTION_SUMMARY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillNoiseFunctionSummary));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_FUNCTION_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseFunctionSummary);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseFunctionSummaryVo SelectByObject(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_FUNCTION_SUMMARY " + this.BuildWhereStatement(tEnvFillNoiseFunctionSummary);
            return SqlHelper.ExecuteObject(new TEnvFillNoiseFunctionSummaryVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillNoiseFunctionSummary, TEnvFillNoiseFunctionSummaryVo.T_ENV_FILL_NOISE_FUNCTION_SUMMARY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseFunctionSummary, TEnvFillNoiseFunctionSummaryVo.T_ENV_FILL_NOISE_FUNCTION_SUMMARY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillNoiseFunctionSummary.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseFunctionSummary_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary_UpdateSet, TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseFunctionSummary_UpdateSet, TEnvFillNoiseFunctionSummaryVo.T_ENV_FILL_NOISE_FUNCTION_SUMMARY_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillNoiseFunctionSummary_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_NOISE_FUNCTION_SUMMARY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            string strSQL = "delete from T_ENV_FILL_NOISE_FUNCTION_SUMMARY ";
            strSQL += this.BuildWhereStatement(tEnvFillNoiseFunctionSummary);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillNoiseFunctionSummary)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillNoiseFunctionSummary.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillNoiseFunctionSummary.YEAR.ToString()));
                }
                //季度
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.QUTER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QUTER = '{0}'", tEnvFillNoiseFunctionSummary.QUTER.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvFillNoiseFunctionSummary.POINT_CODE.ToString()));
                }
                //气象条件
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.WEATHER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WEATHER = '{0}'", tEnvFillNoiseFunctionSummary.WEATHER.ToString()));
                }
                //监测日期(月)
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillNoiseFunctionSummary.MONTH.ToString()));
                }
                //监测日期(日)
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillNoiseFunctionSummary.DAY.ToString()));
                }
                //白昼均值
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.LD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LD = '{0}'", tEnvFillNoiseFunctionSummary.LD.ToString()));
                }
                //夜间均值
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.LN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LN = '{0}'", tEnvFillNoiseFunctionSummary.LN.ToString()));
                }
                //日均值
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.LDN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LDN = '{0}'", tEnvFillNoiseFunctionSummary.LDN.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillNoiseFunctionSummary.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillNoiseFunctionSummary.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillNoiseFunctionSummary.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillNoiseFunctionSummary.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillNoiseFunctionSummary.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionSummary.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillNoiseFunctionSummary.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 更新功能区噪声汇总表的昼均值、夜均值、日均值
        /// </summary>
        /// <param name="PointID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public int UpdateFunctionSummary(string PointID, string ItemID)
        {
            StringBuilder sql = new StringBuilder();
            List<string> list = new List<string>();

            list = getLEQ(PointID, ItemID);

            sql.Append("update T_ENV_FILL_NOISE_FUNCTION_SUMMARY ");
            sql.Append("set LD='" + list[0] + "',");
            sql.Append("LDN='" + list[1] + "',");
            sql.Append("LN='" + list[2] + "' ");
            sql.Append("where POINT_CODE='" + PointID + "'");

            return ExecuteNonQuery(sql.ToString());
        }

        /// <summary>
        /// 更新功能区噪声汇总表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ColName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int UpdateSummary(string ID, string ColName, string Value)
        {
            string sql = "";

            sql = "update " + TEnvFillNoiseFunctionSummaryVo.T_ENV_FILL_NOISE_FUNCTION_SUMMARY_TABLE + " set " + ColName + "='" + Value + "' where ID='" + ID + "'";

            return ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 计算LEQ的等效声级
        /// </summary>
        /// <param name="PointID"></param>
        /// <param name="ItemID"></param>
        public List<string> getLEQ(string PointID, string ItemID)
        {
            List<string> list = new List<string>();
            DataTable dt = new DataTable();
            double dLd = 0;   //昼间等效声级
            double dLn = 0;   //夜间等效声级
            double dLdn = 0;  //昼夜等效声级
            double dLdSum = 0;
            double dLnSum = 0;
            double dLdnSum = 0;
            int iN = 0;
            string strSql = "";
            //计算昼间等效声级
            strSql = "select b.ITEM_VALUE from T_ENV_FILL_NOISE_FUNCTION a left join T_ENV_FILL_NOISE_FUNCTION_ITEM b on(a.ID=b.FILL_ID) where a.POINT_ID='{0}' and b.ITEM_ID='{1}' and cast(a.BEGIN_HOUR as int) between 7 and 22";
            strSql = string.Format(strSql, PointID, ItemID);
            dt = SqlHelper.ExecuteDataTable(strSql);
            iN = dt.Rows.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dLdSum += Math.Pow(10, (0.1 * double.Parse(dt.Rows[i]["ITEM_VALUE"].ToString() == "" ? "0" : dt.Rows[i]["ITEM_VALUE"].ToString())));
            }
            dLd = Math.Round(10 * Math.Log10(dLdSum / iN), 2);
            list.Add(dLd.ToString());

            //计算夜间等效声级
            strSql = "select b.ITEM_VALUE from T_ENV_FILL_NOISE_FUNCTION a left join T_ENV_FILL_NOISE_FUNCTION_ITEM b on(a.ID=b.FILL_ID) where a.POINT_ID='{0}' and b.ITEM_ID='{1}' and (cast(a.BEGIN_HOUR as int) between 0 and 6 or a.BEGIN_HOUR='23')";
            strSql = string.Format(strSql, PointID, ItemID);
            dt = SqlHelper.ExecuteDataTable(strSql);
            iN = dt.Rows.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dLnSum += Math.Pow(10, (0.1 * double.Parse(dt.Rows[i]["ITEM_VALUE"].ToString() == "" ? "0" : dt.Rows[i]["ITEM_VALUE"].ToString())));
                dLdnSum += Math.Pow(10, (0.1 * (double.Parse(dt.Rows[i]["ITEM_VALUE"].ToString() == "" ? "0" : dt.Rows[i]["ITEM_VALUE"].ToString()) + 10)));
            }
            dLn = Math.Round(10 * Math.Log10(dLnSum / iN), 2);
            list.Add(dLn.ToString());

            //计算昼夜等效声级
            dLdnSum += dLdSum;
            dLdn = Math.Round(10 * Math.Log10(dLdnSum / 24), 2);
            list.Add(dLdn.ToString());

            return list;
        }
    }

}
