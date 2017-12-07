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
    public class TEnvFillNoiseAreaAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_NOISE_AREA " + this.BuildWhereStatement(tEnvFillNoiseArea);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_AREA  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillNoiseAreaVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaVo Details(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_AREA " + this.BuildWhereStatement(tEnvFillNoiseArea));
            return SqlHelper.ExecuteObject(new TEnvFillNoiseAreaVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseAreaVo> SelectByObject(TEnvFillNoiseAreaVo tEnvFillNoiseArea, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_AREA " + this.BuildWhereStatement(tEnvFillNoiseArea));
            return SqlHelper.ExecuteObjectList(tEnvFillNoiseArea, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaVo tEnvFillNoiseArea, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_NOISE_AREA {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillNoiseArea));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseArea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_AREA " + this.BuildWhereStatement(tEnvFillNoiseArea);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseAreaVo SelectByObject(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_AREA " + this.BuildWhereStatement(tEnvFillNoiseArea);
            return SqlHelper.ExecuteObject(new TEnvFillNoiseAreaVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillNoiseArea, TEnvFillNoiseAreaVo.T_ENV_FILL_NOISE_AREA_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseArea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseArea, TEnvFillNoiseAreaVo.T_ENV_FILL_NOISE_AREA_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillNoiseArea.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseArea_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseArea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaVo tEnvFillNoiseArea_UpdateSet, TEnvFillNoiseAreaVo tEnvFillNoiseArea_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseArea_UpdateSet, TEnvFillNoiseAreaVo.T_ENV_FILL_NOISE_AREA_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillNoiseArea_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_NOISE_AREA where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            string strSQL = "delete from T_ENV_FILL_NOISE_AREA ";
            strSQL += this.BuildWhereStatement(tEnvFillNoiseArea);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillNoiseArea"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillNoiseArea)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillNoiseArea.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillNoiseArea.POINT_ID.ToString()));
                }
                //测量时间
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.MEASURE_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MEASURE_TIME = '{0}'", tEnvFillNoiseArea.MEASURE_TIME.ToString()));
                }
                //开始月
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.BEGIN_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MONTH = '{0}'", tEnvFillNoiseArea.BEGIN_MONTH.ToString()));
                }
                //开始日
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.BEGIN_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_DAY = '{0}'", tEnvFillNoiseArea.BEGIN_DAY.ToString()));
                }
                //星期
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.WEEK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WEEK = '{0}'", tEnvFillNoiseArea.WEEK.ToString()));
                }
                //开始时
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.BEGIN_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_HOUR = '{0}'", tEnvFillNoiseArea.BEGIN_HOUR.ToString()));
                }
                //开始分
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.BEGIN_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MINUTE = '{0}'", tEnvFillNoiseArea.BEGIN_MINUTE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillNoiseArea.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillNoiseArea.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillNoiseArea.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillNoiseArea.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillNoiseArea.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillNoiseArea.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillNoiseArea.REMARK5.ToString()));
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
            dr["code"] = "BEGIN_DAY";
            dr["name"] = "开始日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_HOUR";
            dr["name"] = "开始时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_MINUTE";
            dr["name"] = "开始分";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "WEEK";
            //dr["name"] = "星期";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "MEASURE_TIME";
            //dr["name"] = "测量时间";
            //dt.Rows.Add(dr);

            return dt;
        }

        public DataTable SelectFill_ForSummary(string strYear, string strMonth, string strPoint)
        {
            string strSQL = @"select f.ID,f.POINT_ID,p.POINT_CODE as 测点编号,p.POINT_NAME,f.MEASURE_TIME as 测量时间,
                f.YEAR+'-'+f.MONTH+'-'+f.BEGIN_DAY as DATE,
                f.BEGIN_HOUR+':'+f.BEGIN_MINUTE+':00' as TIME,
                p.SOUND_SOURCE_ID as 声源代码   
                 from T_ENV_FILL_NOISE_AREA f
                 join T_ENV_P_NOISE_AREA p on p.id=f.POINT_ID 
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
                 from T_ENV_FILL_NOISE_AREA_ITEM r
                 join T_BASE_ITEM_INFO i on i.ID=r.ITEM_ID
                 where r.FILL_ID in (select ID 
                     from T_ENV_FILL_NOISE_AREA  
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
