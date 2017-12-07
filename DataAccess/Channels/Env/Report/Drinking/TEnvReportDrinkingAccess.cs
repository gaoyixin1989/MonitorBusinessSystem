using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace i3.DataAccess.Channels.Env.Report.Drinking
{
    /// <summary>
    /// 功能描述：饮用水报表
    /// 创建人：钟杰华
    /// 创建日期：2013-02-28
    /// </summary>
    public class TEnvReportDrinkingAccess:SqlHelper
    {
        /// <summary>
        /// 饮用水源地水质月报(全分析监测)
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetDrinkingMonthReportData(string month, string pointId)
        {
            string sqlStr = @"
                select
	                max(POINT_NAME) as POINT_NAME,
	                max(POINT_CODE) as POINT_CODE,
	                max(RIVER_NAME) as RIVER_NAME,
	                max(SECTION_PORPERTIES) as SECTION_PORPERTIES,
	                max(LAT_LONG) as LAT_LONG,
	                max(SAMPLING_DAY) as SAMPLING_DAY,
	                max(KPF) as KPF,
	                max(ITEM_NAME) as ITEM_NAME,
	                cast(round(avg(cast(isnull(ITEM_VALUE,'0') as float)),2) as decimal(10,2)) as ITEM_VALUE,
	                POINT_ID,
	                [MONTH],
	                ITEM_ID
                from (
	                select
		                t4.SECTION_NAME as POINT_NAME,
		                t4.SECTION_CODE as POINT_CODE,
		                t5.DICT_TEXT as RIVER_NAME,
		                t6.DICT_TEXT as SECTION_PORPERTIES,
		                t4.LONGITUDE_DEGREE+'°'+t4.LONGITUDE_MINUTE+'′'+t4.LONGITUDE_SECOND+','+t4.LATITUDE_DEGREE+'°'+t4.LATITUDE_MINUTE+'′'+t4.LATITUDE_SECOND as LAT_LONG,
		                t1.SAMPLING_DAY,
		                t1.KPF,
		                t7.ITEM_NAME,
		                t2.ITEM_VALUE,
		                t4.ID as POINT_ID,
		                t1.[MONTH],
		                t2.ITEM_ID
	                from
		                T_ENV_FILL_DRINKING t1
	                left join
		                T_ENV_FILL_DRINKING_ITEM t2 on
		                t2.DRINKING_FILL_ID=t1.ID and
		                t2.ITEM_ID in(select t3.ITEM_ID from T_ENV_POINT_DRINKING_ITEM t3 where t3.VERTICAL_ID=t1.VERTICAL_ID)
	                left join
		                T_ENV_POINT_DRINKING t4 on
		                t4.ID=t1.DRINKING_POINT_ID and
		                t4.IS_DEL='0'
	                left join
		                T_SYS_DICT t5 on
		                t5.ID=t4.RIVER_ID
	                left join
		                T_SYS_DICT t6 on
		                t6.DICT_CODE=t4.SECTION_PORPERTIES_ID and
		                t6.DICT_TYPE='SECTION_PORPERTIES_ID'
	                left join
		                T_BASE_ITEM_INFO t7 on
		                t7.ID=t2.ITEM_ID and
		                t7.IS_DEL='0'
	                where
		                t1.[MONTH]='{0}' and
		                t1.DRINKING_POINT_ID in({1}) and
		                isnull(t2.ITEM_ID,'-999')<>'-999' and
		                isnull(t2.ITEM_VALUE,'')<>''
                ) t
                group by
	                POINT_ID,
	                [MONTH],
	                ITEM_ID
            ";

            sqlStr = string.Format(sqlStr, month, pointId);

            return ExecuteDataTable(sqlStr);
        }

        /// <summary>
        /// 获取水库（饮用水源）水质监测结果填报表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetReservoirMonitorReportData(string month, string pointId)
        {
            string sqlStr = @"
                select
	                max(RESERVOIR_NAME) as RESERVOIR_NAME,
	                max(POINT_NAME) as POINT_NAME,
	                max(POINT_CODE) as POINT_CODE,
	                max(SAMPLING_DAY) as SAMPLING_DAY,
	                max(ITEM_NAME) as ITEM_NAME,
	                cast(round(avg(cast(isnull(ITEM_VALUE,'0') as float)),2) as decimal(10,2)) as ITEM_VALUE,
	                POINT_ID,
	                [MONTH],
	                ITEM_ID
                from (
	                select
		                t4.WATER_SOURCE_ID as RESERVOIR_NAME,
		                t4.SECTION_NAME as POINT_NAME,
		                t4.SECTION_CODE as POINT_CODE,
		                t1.SAMPLING_DAY,
		                t7.ITEM_NAME,
		                t2.ITEM_VALUE,
		                t1.DRINKING_POINT_ID as POINT_ID,
		                t1.[MONTH],
		                t2.ITEM_ID
	                from
		                T_ENV_FILL_DRINKING t1
	                left join
		                T_ENV_FILL_DRINKING_ITEM t2 on
		                t2.DRINKING_FILL_ID=t1.ID and
		                t2.ITEM_ID in(select t3.ITEM_ID from T_ENV_POINT_DRINKING_ITEM t3 where t3.VERTICAL_ID=t1.VERTICAL_ID)
	                left join
		                T_ENV_POINT_DRINKING t4 on
		                t4.ID=t1.DRINKING_POINT_ID and
		                t4.IS_DEL='0'
	                left join
		                T_SYS_DICT t5 on
		                t5.ID=t4.RIVER_ID
	                left join
		                T_SYS_DICT t6 on
		                t6.DICT_CODE=t4.SECTION_PORPERTIES_ID and
		                t6.DICT_TYPE='SECTION_PORPERTIES_ID'
	                left join
		                T_BASE_ITEM_INFO t7 on
		                t7.ID=t2.ITEM_ID and
		                t7.IS_DEL='0'
	                where
		                t1.[MONTH]='{0}' and
		                t1.DRINKING_POINT_ID in({1}) and
		                isnull(t2.ITEM_ID,'-999')<>'-999' and
		                isnull(t2.ITEM_VALUE,'')<>''
                ) t
                group by
	                POINT_ID,
	                [MONTH],
	                ITEM_ID
            ";

            sqlStr = string.Format(sqlStr, month, pointId);

            return ExecuteDataTable(sqlStr);
        }

        /// <summary>
        /// 获取饮用水源地全分析
        /// </summary>
        /// <param name="month"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public DataTable GetAllAnalysisReportData(string month, string pointId)
        {
            string strSQL = @"select
	                max(POINT_NAME) as POINT_NAME,
	                max(POINT_CODE) as POINT_CODE,
	                max(ITEM_NAME) as ITEM_NAME,
	                cast(round(avg(cast(isnull(ITEM_VALUE,'0') as float)),2) as decimal(10,2)) as ITEM_VALUE,
	                POINT_ID,
	                ITEM_ID,
	                max(upper_operator) as UPPER_VALUE
                from (
	                select
		                t4.SECTION_NAME as POINT_NAME,
		                t4.SECTION_CODE as POINT_CODE,
		                t7.ITEM_NAME,
		                t2.ITEM_VALUE,
		                t4.ID as POINT_ID,
		                t1.[MONTH],
		                t2.ITEM_ID,
		                t8.upper_operator as upper_operator
	                from
		                T_ENV_FILL_DRINKING t1
	                left join
		                T_ENV_FILL_DRINKING_ITEM t2 on
		                t2.DRINKING_FILL_ID=t1.ID and
		                t2.ITEM_ID in(select t3.ITEM_ID from T_ENV_POINT_DRINKING_ITEM t3 where t3.VERTICAL_ID=t1.VERTICAL_ID)
	                left join
		                T_ENV_POINT_DRINKING t4 on
		                t4.ID=t1.DRINKING_POINT_ID and
		                t4.IS_DEL='0'
	                left join
		                T_SYS_DICT t5 on
		                t5.ID=t4.RIVER_ID
	                left join
		                T_SYS_DICT t6 on
		                t6.DICT_CODE=t4.SECTION_PORPERTIES_ID and
		                t6.DICT_TYPE='SECTION_PORPERTIES_ID'
	                left join
		                T_BASE_ITEM_INFO t7 on
		                t7.ID=t2.ITEM_ID and
		                t7.IS_DEL='0'
		            left join 
						t_base_evaluation_con_item t8 on
						t8.condition_id=t4.condition_id and 
						t8.item_id=t2.item_id
	                where
		                t1.[MONTH]='{0}' and
		                t1.DRINKING_POINT_ID in({1})
                ) t
                group by
	                POINT_ID,
	                [MONTH],
	                ITEM_ID";
            strSQL = string.Format(strSQL, month, pointId);

            return ExecuteDataTable(strSQL);
        }
    }
}
