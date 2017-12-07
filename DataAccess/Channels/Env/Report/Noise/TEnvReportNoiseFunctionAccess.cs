using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace i3.DataAccess.Channels.Env.Report
{
    /// <summary>
    /// 功能描述：环境质量功能区噪声报表
    /// 创建人：钟杰华
    /// 创建时间：2013-01-30
    /// </summary>
    public class TEnvReportNoiseFunctionAccess : SqlHelper
    {
        /// <summary>
        /// 获取功能区噪声监测数据表数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="season">季度</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetFunctionNoiseDataReportData(string year, string season, string pointId)
        {
            string sqlStr = @"select 
                                                t1.id,
	                                            '{0}' as [year],
	                                            t1.begin_month,
	                                            t1.begin_day,
	                                            t1.begin_hour,
	                                            t2.point_code,
	                                            t2.point_name,
	                                            t2.function_area_id,
	                                            t1.leq,
	                                            t1.l10,
	                                            t1.l50,
	                                            t1.l90,
	                                            t1.sd,
	                                            t1.traffic_veh
                                            from
	                                            t_env_fill_noise_function t1
                                            left join
	                                            t_env_point_noise_function t2 on t1.function_point_id=t2.id
                                            where
	                                            t1.function_point_id in({1}) and
	                                            t1.[quarter]='{2}'
                                            order by
	                                            cast(t2.id as integer) asc,
	                                            cast(t1.begin_month as integer) asc,
	                                            cast(t1.begin_day as integer) asc,
	                                            cast(t1.begin_hour as integer) asc";
            sqlStr = string.Format(sqlStr, year, pointId, season);

            return ExecuteDataTable(sqlStr);
        }
        /// <summary>
        /// 获取功能区噪声dbf文件数据
        /// </summary>
        /// <param name="strSTime"></param>
        /// <param name="strETime"></param>
        /// <returns></returns>
        public DataTable GetDbfData(string strYear, string strMonth,string strDay)
        {
            string strSQL = String.Format(@"select 
                                                '130300' Stcode,
                                                t2.year Ye,
                                                t2.point_name Poname,
                                                t2.point_code Pocode,
	                                            t2.function_area_id Ndisc,
	                                            t1.begin_month Mon,
	                                            t1.begin_day Da,
	                                            t1.begin_hour Hor,
	                                            t1.leq Leqa,
	                                            t1.l10 L10a,
	                                            t1.l50 L50a,
	                                            t1.l90 L90a
                                            from
	                                            t_env_fill_noise_function t1
                                            left join
	                                            t_env_point_noise_function t2 on t1.function_point_id=t2.id
	                                            where t1.begin_month='{0}' and t1.begin_day='{1}' and t2.year='{2}'",strMonth,strDay,strYear);
            return ExecuteDataTable(strSQL);
        }
    }
}
