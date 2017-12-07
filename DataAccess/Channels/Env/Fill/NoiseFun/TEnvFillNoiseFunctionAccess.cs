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
    public class TEnvFillNoiseFunctionAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_NOISE_FUNCTION " + this.BuildWhereStatement(tEnvFillNoiseFunction);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_FUNCTION  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillNoiseFunctionVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionVo Details(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_FUNCTION " + this.BuildWhereStatement(tEnvFillNoiseFunction));
            return SqlHelper.ExecuteObject(new TEnvFillNoiseFunctionVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseFunctionVo> SelectByObject(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_FUNCTION " + this.BuildWhereStatement(tEnvFillNoiseFunction));
            return SqlHelper.ExecuteObjectList(tEnvFillNoiseFunction, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_NOISE_FUNCTION {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillNoiseFunction));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseFunction"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_FUNCTION " + this.BuildWhereStatement(tEnvFillNoiseFunction);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseFunctionVo SelectByObject(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_FUNCTION " + this.BuildWhereStatement(tEnvFillNoiseFunction);
            return SqlHelper.ExecuteObject(new TEnvFillNoiseFunctionVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillNoiseFunction, TEnvFillNoiseFunctionVo.T_ENV_FILL_NOISE_FUNCTION_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseFunction, TEnvFillNoiseFunctionVo.T_ENV_FILL_NOISE_FUNCTION_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillNoiseFunction.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunction_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseFunction_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction_UpdateSet, TEnvFillNoiseFunctionVo tEnvFillNoiseFunction_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseFunction_UpdateSet, TEnvFillNoiseFunctionVo.T_ENV_FILL_NOISE_FUNCTION_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillNoiseFunction_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_NOISE_FUNCTION where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            string strSQL = "delete from T_ENV_FILL_NOISE_FUNCTION ";
            strSQL += this.BuildWhereStatement(tEnvFillNoiseFunction);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillNoiseFunction"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillNoiseFunction)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillNoiseFunction.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillNoiseFunction.POINT_ID.ToString()));
                }
                //季度
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.QUARTER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QUARTER = '{0}'", tEnvFillNoiseFunction.QUARTER.ToString()));
                }
                //测量时间
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.MEASURE_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MEASURE_TIME = '{0}'", tEnvFillNoiseFunction.MEASURE_TIME.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillNoiseFunction.YEAR.ToString()));
                }
                //开始月
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.BEGIN_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MONTH = '{0}'", tEnvFillNoiseFunction.BEGIN_MONTH.ToString()));
                }
                //开始日
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.BEGIN_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_DAY = '{0}'", tEnvFillNoiseFunction.BEGIN_DAY.ToString()));
                }
                //开始时
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.BEGIN_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_HOUR = '{0}'", tEnvFillNoiseFunction.BEGIN_HOUR.ToString()));
                }
                //开始分
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.BEGIN_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MINUTE = '{0}'", tEnvFillNoiseFunction.BEGIN_MINUTE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillNoiseFunction.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillNoiseFunction.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillNoiseFunction.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillNoiseFunction.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillNoiseFunction.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunction.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillNoiseFunction.REMARK5.ToString()));
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
            dr["code"] = "QUARTER";
            dr["name"] = "季度";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_HOUR";
            dr["name"] = "小时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_DAY";
            dr["name"] = "开始日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MEASURE_TIME";
            dr["name"] = "测量时间";
            dt.Rows.Add(dr);


            return dt;
        }

        public DataTable SelectFill_ForSummary(string strYear, string strMonth, string strPoint)
        {
            string strSQL = @"select f.ID,f.POINT_ID,p.POINT_CODE as 测点编号,p.POINT_NAME,f.MEASURE_TIME as 测量时间,
                f.YEAR+'-'+f.MONTH+'-'+f.BEGIN_DAY as DATE,
                f.BEGIN_HOUR+':00:00' as TIME 
                 from T_ENV_FILL_NOISE_FUNCTION f
                 join T_ENV_P_NOISE_FUNCTION p on p.id=f.POINT_ID 
                 where 1=1";
            if (strYear.Length > 0)
                strSQL += string.Format(" and f.YEAR='{0}'", strYear);
            if (strMonth.Length > 0)
            {
                string strMonthEx = strMonth.StartsWith("0") ? strMonth.Replace("0", "") : strMonth;
                strSQL += string.Format(" and (f.MONTH='{0}' or f.MONTH='{1}')", strMonth, strMonthEx);
            }
            if (strPoint.Length > 0)
                strSQL += string.Format(" and f.POINT_ID='{0}'", strPoint);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        public DataTable SelectResult_ForSummary(string strYear, string strMonth, string strPoint)
        {
            string strSQL = @"select r.ID,r.FILL_ID,r.ITEM_ID,i.ITEM_NAME,r.ITEM_VALUE,r.JUDGE 
                 from T_ENV_FILL_NOISE_FUNCTION_ITEM r
                 join T_BASE_ITEM_INFO i on i.ID=r.ITEM_ID
                 where r.FILL_ID in (select ID 
                     from T_ENV_FILL_NOISE_FUNCTION  
                     where 1=1";
            if (strYear.Length > 0)
                strSQL += string.Format(" and YEAR='{0}'", strYear);
            if (strMonth.Length > 0)
            {
                string strMonthEx = strMonth.StartsWith("0") ? strMonth.Replace("0", "") : strMonth;
                strSQL += string.Format(" and (MONTH='{0}' or MONTH='{1}')", strMonth, strMonthEx);
            }
            if (strPoint.Length > 0)
                strSQL += string.Format(" and POINT_ID='{0}'", strPoint);
            strSQL += ")";

            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }

}
