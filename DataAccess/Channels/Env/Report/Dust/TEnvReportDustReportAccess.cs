using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// 功能描述：降尘报表
/// 创建人：陈达峰
/// 创建时间：2013-01-30
/// </summary>
namespace i3.DataAccess.Channels.Env.Report
{
    public class TEnvReportDustReportAccess:SqlHelper
    {
        /// <summary>
        /// 获取降尘原始数据
        /// </summary>
        /// <param name="strYear"></param>
        /// <returns></returns>
        public DataTable GetDustSourceData(string strYear)
        {
            string strSQL = @"select t1.ID,t1.YEAR,t1.STATION_CODE,
                                               '降尘' as PROJECT_NAME,
                                               t1.POINT_CODE,
                                               t1.POINT_NAME,
                                               cast(t2.MONTH1 AS NUMERIC(10,2)) AS MONTH1,
                                               cast(t2.MONTH2 AS NUMERIC(10,2)) AS MONTH2,
                                               cast(t2.MONTH3 AS NUMERIC(10,2)) AS MONTH3,
                                               cast(t2.MONTH4 AS NUMERIC(10,2)) AS MONTH4,
                                               cast(t2.MONTH5 AS NUMERIC(10,2)) AS MONTH5,
                                               cast(t2.MONTH6 AS NUMERIC(10,2)) AS MONTH6,
                                               cast(t2.MONTH7 AS NUMERIC(10,2)) AS MONTH7,
                                               cast(t2.MONTH8 AS NUMERIC(10,2)) AS MONTH8,
                                               cast(t2.MONTH9 AS NUMERIC(10,2)) AS MONTH9,
                                               cast(t2.MONTH10 AS NUMERIC(10,2)) AS MONTH10,
                                               cast(t2.MONTH11 AS NUMERIC(10,2)) AS MONTH11,
                                               cast(t2.MONTH12 AS NUMERIC(10,2)) AS MONTH12,
                                                 (select isnull(cast(round(avg(value),2) as numeric(10,2)),0.00) from (
																															(
																																select (case cast(isnull(month1,'0') as float) when 0 then null else cast(month1 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month2,'0') as float) when 0 then null else cast(month2 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month3,'0') as float) when 0 then null else cast(month3 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month4,'0') as float) when 0 then null else cast(month4 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month5,'0') as float) when 0 then null else cast(month5 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month6,'0') as float) when 0 then null else cast(month6 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month7,'0') as float) when 0 then null else cast(month7 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month8,'0') as float) when 0 then null else cast(month8 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month9,'0') as float) when 0 then null else cast(month9 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month10,'0') as float) when 0 then null else cast(month10 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month11,'0') as float) when 0 then null else cast(month11 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																																union all
																																select (case cast(isnull(month12,'0') as float) when 0 then null else cast(month12 as float) end) value from t_Env_fill_dust tmfd2 where tmfd2.id=t2.id
																															
																															)
																														   ) ttt
												 ) as SUMAVG
                                          from (select ID,
                                                       YEAR,
                                                       STATION_CODE =
                                                       (select CONFIG_VALUE
                                                          from T_SYS_CONFIG
                                                         where CONFIG_CODE = 'IintRegionCode'),
                                                       POINT_CODE,
                                                       POINT_NAME
                                                  from dbo.T_ENV_POINT_DUST
                                                 where YEAR = '{0}'
                                                   and IS_DEL='0') t1
                                          left join T_Env_FILL_DUST t2
                                            on t1.ID = t2.DUST_POINT_ID";
            strSQL = string.Format(strSQL, strYear);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }
}
