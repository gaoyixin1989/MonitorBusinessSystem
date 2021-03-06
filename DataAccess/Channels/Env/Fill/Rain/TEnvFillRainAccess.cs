using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Rain;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.Rain
{
    /// <summary>
    /// 功能：降水数据填报表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvFillRainAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRain">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRainVo tEnvFillRain)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RAIN " + this.BuildWhereStatement(tEnvFillRain);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRainVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RAIN  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillRainVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRain">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRainVo Details(TEnvFillRainVo tEnvFillRain)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RAIN " + this.BuildWhereStatement(tEnvFillRain));
            return SqlHelper.ExecuteObject(new TEnvFillRainVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRainVo> SelectByObject(TEnvFillRainVo tEnvFillRain, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_RAIN " + this.BuildWhereStatement(tEnvFillRain));
            return SqlHelper.ExecuteObjectList(tEnvFillRain, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRainVo tEnvFillRain, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_RAIN {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRain));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRain"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRainVo tEnvFillRain)
        {
            string strSQL = "select * from T_ENV_FILL_RAIN " + this.BuildWhereStatement(tEnvFillRain);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRain">对象</param>
        /// <returns></returns>
        public TEnvFillRainVo SelectByObject(TEnvFillRainVo tEnvFillRain)
        {
            string strSQL = "select * from T_ENV_FILL_RAIN " + this.BuildWhereStatement(tEnvFillRain);
            return SqlHelper.ExecuteObject(new TEnvFillRainVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRain">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRainVo tEnvFillRain)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRain, TEnvFillRainVo.T_ENV_FILL_RAIN_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRain">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRainVo tEnvFillRain)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRain, TEnvFillRainVo.T_ENV_FILL_RAIN_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRain.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRain_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRain_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRainVo tEnvFillRain_UpdateSet, TEnvFillRainVo tEnvFillRain_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRain_UpdateSet, TEnvFillRainVo.T_ENV_FILL_RAIN_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRain_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RAIN where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRainVo tEnvFillRain)
        {
            string strSQL = "delete from T_ENV_FILL_RAIN ";
            strSQL += this.BuildWhereStatement(tEnvFillRain);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region// 构造填报表需要显示的信息
        /// <summary>
        /// 构造填报表需要显示的信息（刘静楠/ 2013/6/24）
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
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr); 

            dr = dt.NewRow();
            dr["code"] = "POINT_ID";
            dr["name"] = "监测点名称";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "BEGIN_MONTH";
            //dr["name"] = "起始月";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_DAY";
            dr["name"] = "起始日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_HOUR";
            dr["name"] = "起始时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_MINUTE";
            dr["name"] = "起始分";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "END_MONTH";
            //dr["name"] = "结束月";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_DAY";
            dr["name"] = "结束日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_HOUR";
            dr["name"] = "结束时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_MINUTE";
            dr["name"] = "结束分";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "RAIN_TYPE";
            dr["name"] = "降水类型";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "PRECIPITATION";
            //dr["name"] = "降水量";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "JUDGE";
            //dr["name"] = "评价"; 
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "OVERPROOF";
            //dr["name"] = "超标污染物";
            //dt.Rows.Add(dr);
            return dt;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillRain"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRainVo tEnvFillRain)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRain)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRain.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRain.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillRain.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillRain.POINT_ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillRain.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillRain.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvFillRain.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillRain.MONTH.ToString()));
                }	
                //开始月
                if (!String.IsNullOrEmpty(tEnvFillRain.BEGIN_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MONTH = '{0}'", tEnvFillRain.BEGIN_MONTH.ToString()));
                }
                //开始日
                if (!String.IsNullOrEmpty(tEnvFillRain.BEGIN_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_DAY = '{0}'", tEnvFillRain.BEGIN_DAY.ToString()));
                }
                //开始时
                if (!String.IsNullOrEmpty(tEnvFillRain.BEGIN_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_HOUR = '{0}'", tEnvFillRain.BEGIN_HOUR.ToString()));
                }
                //开始分
                if (!String.IsNullOrEmpty(tEnvFillRain.BEGIN_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MINUTE = '{0}'", tEnvFillRain.BEGIN_MINUTE.ToString()));
                }
                //结束月
                if (!String.IsNullOrEmpty(tEnvFillRain.END_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_MONTH = '{0}'", tEnvFillRain.END_MONTH.ToString()));
                }
                //结束日
                if (!String.IsNullOrEmpty(tEnvFillRain.END_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_DAY = '{0}'", tEnvFillRain.END_DAY.ToString()));
                }
                //结束时
                if (!String.IsNullOrEmpty(tEnvFillRain.END_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_HOUR = '{0}'", tEnvFillRain.END_HOUR.ToString()));
                }
                //结束分
                if (!String.IsNullOrEmpty(tEnvFillRain.END_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_MINUTE = '{0}'", tEnvFillRain.END_MINUTE.ToString()));
                }
                //降水类型
                if (!String.IsNullOrEmpty(tEnvFillRain.RAIN_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RAIN_TYPE = '{0}'", tEnvFillRain.RAIN_TYPE.ToString()));
                }
                //降水量
                if (!String.IsNullOrEmpty(tEnvFillRain.PRECIPITATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PRECIPITATION = '{0}'", tEnvFillRain.PRECIPITATION.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillRain.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillRain.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillRain.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillRain.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRain.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRain.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRain.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRain.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRain.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRain.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRain.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRain.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRain.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRain.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="pointId">监测点ID</param>
        /// <param name="month">月份</param>
        /// <param name="day">日期</param>
        /// <returns></returns>
        public DataTable GetRainFillData(string pointId, string month, string day)
        {
            string sqlStr = @"select 
	                                            t1.id as point_id,
                                                t2.id as fill_id,
	                                            t1.[year],
	                                            t1.point_name,
	                                            '{0}' as begin_month,
	                                            {1},
	                                            t2.begin_hour,
	                                            t2.begin_minute,
	                                            t2.end_month,
	                                            t2.end_day,
	                                            t2.end_hour,
	                                            t2.end_minute,
	                                            t2.rain_type,
	                                            t2.precipitation
                                            from 
	                                            T_ENV_POINT_RAIN t1
	                                        left join 
												T_ENV_FILL_RAIN t2 on t2.RAIN_POINT_ID=t1.ID and t2.begin_month='{0}' {2}
                                            where
	                                            t1.id='{3}'";

            string fieldDay = "";
            string filterDay = "";
            if (!string.IsNullOrEmpty(day))
            {
                fieldDay = "'" + day + "' as begin_day";
                filterDay = " and t2.begin_day='" + day + "'";
            }
            else
            {
                fieldDay = "t2.begin_day";
            }

            sqlStr += " order by cast(t2.begin_month as int) desc,cast(t2.begin_day as int) desc";

            sqlStr = string.Format(sqlStr, month, fieldDay, filterDay, pointId);

            return ExecuteDataTable(sqlStr);
        }

        /// <summary>
        /// 获取要填报的监测项
        /// </summary>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetFillItem(string pointId)
        {
            string strSql = @"select 
	                                            t2.id,
	                                            t2.item_name
                                            from 
                                            (
	                                            select 
		                                            item_id 
	                                            from 
		                                            T_ENV_POINT_RAIN_ITEM
	                                            where
		                                            RAIN_POINT_ID in({0})
	                                            group by
		                                            item_id
                                            ) t1
                                            left join 
	                                            T_BASE_ITEM_INFO t2 on t1.item_id=t2.id
                                            where
	                                            t2.is_del='0'";
            strSql = string.Format(strSql, pointId);

            return ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 保存填报数据
        /// </summary>
        /// <param name="dtData">填报数据</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public bool SaveRainFillData(DataTable dtData, string pointId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("begin Transaction T\n");
            strSql.Append("declare @errorCount as int\n");
            strSql.Append("set @errorCount=0\n");

            string beginMonth = dtData.Rows[0]["begin_month"].ToString(); //月份

            //删除原有数据
            strSql.AppendFormat("delete from T_ENV_FILL_RAIN_ITEM where RAIN_FILL_ID in(select id from T_ENV_FILL_RAIN where RAIN_POINT_ID='{0}' and BEGIN_MONTH='{1}')\n", pointId, beginMonth);
            strSql.AppendFormat("set @errorCount=@errorCount+@@error\n");
            strSql.AppendFormat("delete from T_ENV_FILL_RAIN where RAIN_POINT_ID='{0}' and BEGIN_MONTH='{1}'\n", pointId, beginMonth);
            strSql.AppendFormat("set @errorCount=@errorCount+@@error\n");

            foreach (DataRow drData in dtData.Rows)
            {
                string beginDay = drData["begin_day"].ToString();
                string beginHour = drData["begin_hour"].ToString();
                string beginMinute = drData["begin_minute"].ToString();
                string endMonth = drData["end_month"].ToString();
                string endDay = drData["end_day"].ToString();
                string endHour = drData["end_hour"].ToString();
                string endMinute = drData["end_minute"].ToString();
                string rainType = drData["rain_type"].ToString();
                string precipitation = drData["precipitation"].ToString();
                string fillId = new TSysSerialAccess().GetSerialNumber("rain_fill_id");

                //基础项
                strSql.AppendFormat("insert into T_ENV_FILL_RAIN(id,RAIN_POINT_ID,begin_month,begin_day,begin_hour,begin_minute,end_month,end_day,end_hour,end_minute,rain_type,precipitation) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')\n", fillId, pointId, beginMonth, beginDay, beginHour, beginMinute, endMonth, endDay, endHour, endMinute, rainType, precipitation);
                strSql.AppendFormat("set @errorCount=@errorCount+@@error\n");

                //监测项
                foreach (DataColumn dcData in dtData.Columns)
                {
                    if (dcData.ColumnName.Contains("_id_unSure"))
                    {
                        string itemId = drData[dcData].ToString();
                        string itemValue = drData[dcData.ColumnName.Replace("_id", "")].ToString();
                        string fillItemId = new TSysSerialAccess().GetSerialNumber("rain_fill_item_id");

                        strSql.AppendFormat("insert into T_ENV_FILL_RAIN_ITEM(id,RAIN_FILL_ID,item_id,item_value) values('{0}','{1}','{2}','{3}')\n", fillItemId, fillId, itemId, itemValue);
                        strSql.AppendFormat("set @errorCount=@errorCount+@@error\n");
                    }
                }
            }

            strSql.Append("IF @errorCount <> 0\n");
            strSql.Append("begin\n");
            strSql.Append("select 'fail'\n");
            strSql.Append("RollBack Transaction T\n");
            strSql.Append("end\n");
            strSql.Append("else\n");
            strSql.Append("begin\n");
            strSql.Append("select 'success'\n");
            strSql.Append("COMMIT Transaction T\n");
            strSql.Append("end\n");

            DataTable dtResult = ExecuteDataTable(strSql.ToString());
            bool result = (dtResult.Rows[0][0].ToString() == "success" ? true : false);

            return result;
        }
    }
}
