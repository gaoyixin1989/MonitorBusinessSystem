using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace i3.DataAccess.Channels.Env.Report.Estuaries
{
    /// <summary>
    /// 功能描述：入海河口报表
    /// 创建人：钟杰华
    /// 创建日期：2013-02-27
    /// </summary>
    public class TEnvReportEstuariesAccess : SqlHelper
    {
        /// <summary>
        /// 获取入海河流浓度监测数据报表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetEstuariesMonitorConcentrationReportData(string month, string pointId)
        {
            string sqlStr = @"
                select
	                max([YEAR]) as [YEAR],
	                max(RIVER_NAME) as RIVER_NAME,
	                max(POINT_NAME) as POINT_NAME,
	                max(POINT_CODE) as POINT_CODE,
	                [MONTH],
	                max(ITEM_NAME) as ITEM_NAME,
	                cast(round(avg(cast(isnull(ITEM_VALUE,'0') as float)),2) as decimal(10,2)) as ITEM_VALUE,
	                POINT_ID,
	                ITEM_ID,
	                max(UNIT) as UNIT
                from (
	                select
		                t4.[YEAR],
		                t5.DICT_TEXT as RIVER_NAME,
		                t4.SECTION_NAME as POINT_NAME,
		                t4.SECTION_CODE as POINT_CODE,
		                t1.[MONTH],
		                t6.ITEM_NAME,
		                t2.ITEM_VALUE,
		                t4.ID as POINT_ID,
		                t2.ITEM_ID,
		                t9.DICT_TEXT as UNIT
	                from
		                T_ENV_FILL_ESTUARIES t1
	                left join
		                T_ENV_FILL_ESTUARIES_ITEM t2 on
		                t2.RIVER_METAL_FILL_ID=t1.ID and
		                t2.ITEM_ID in(select t3.ITEM_ID from T_ENV_POINT_ESTUARIES_V_ITEM t3 where t3.VERTICAL_ID=t1.VERTICAL_ID)
	                left join
		                T_ENV_POINT_ESTUARIES t4 on 
		                t4.ID=T1.RIVER_METAL_POINT_ID and
		                t4.IS_DEL='0'
	                left join
		                T_SYS_DICT t5 on
		                t5.ID=t4.RIVER_ID
	                left join
		                T_BASE_ITEM_INFO t6 on
		                t6.ID=t2.ITEM_ID and
		                t6.IS_DEL='0'
	                left join 
	                    T_ENV_POINT_ESTUARIES_V_ITEM t7 on
	                    t7.VERTICAL_ID=t1.VERTICAL_ID --and
	                    --t7.ITEM_ID=t6.ID
	                left join
		                T_BASE_ITEM_ANALYSIS t8 on
		                t8.ITEM_ID=t6.ID and
		                t8.ANALYSIS_METHOD_ID=t7.ANALYSIS_ID and
                        t8.IS_DEL='0'
                    left join
		                T_SYS_DICT t9 on
		                t9.DICT_CODE=t8.UNIT and
		                t9.DICT_TYPE='item_unit'
	                where 
		                t1.[month] in({0}) and 
		                t1.RIVER_METAL_POINT_ID in({1})  and
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
        /// 获取入海河流浓度监测数据报表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetEstuariesMonitorTotalReportData(string month, string pointId)
        {
            string sqlStr = @"
                select
	                max([YEAR]) as [YEAR],
	                max(RIVER_NAME) as RIVER_NAME,
	                max(POINT_NAME) as POINT_NAME,
	                max(POINT_CODE) as POINT_CODE,
	                [MONTH],
	                max(ITEM_NAME) as ITEM_NAME,
	                cast(round(avg(cast(isnull(ITEM_VALUE,'0') as float)),2) as decimal(10,2)) as ITEM_VALUE,
	                POINT_ID,
	                ITEM_ID,
	                't' as UNIT
                from (
	                select
		                t4.[YEAR],
		                t5.DICT_TEXT as RIVER_NAME,
		                t4.SECTION_NAME as POINT_NAME,
		                t4.SECTION_CODE as POINT_CODE,
		                t1.[MONTH],
		                t6.ITEM_NAME,
		                t2.ITEM_VALUE,
		                t4.ID as POINT_ID,
		                t2.ITEM_ID,
		                t9.DICT_TEXT as UNIT
	                from
		                T_ENV_FILL_ESTUARIES t1
	                left join
		                T_ENV_FILL_ESTUARIES_ITEM t2 on
		                t2.RIVER_METAL_FILL_ID=t1.ID and
		                t2.ITEM_ID in(select t3.ITEM_ID from T_ENV_POINT_ESTUARIES_V_ITEM t3 where t3.VERTICAL_ID=t1.VERTICAL_ID)
	                left join
		                T_ENV_POINT_ESTUARIES t4 on 
		                t4.ID=T1.RIVER_METAL_POINT_ID and
		                t4.IS_DEL='0'
	                left join
		                T_SYS_DICT t5 on
		                t5.ID=t4.RIVER_ID
	                left join
		                T_BASE_ITEM_INFO t6 on
		                t6.ID=t2.ITEM_ID and
		                t6.IS_DEL='0'
	                left join 
	                    T_ENV_POINT_ESTUARIES_V_ITEM t7 on
	                    t7.VERTICAL_ID=t1.VERTICAL_ID --and
	                    --t7.ITEM_ID=t6.ID
	                left join
		                T_BASE_ITEM_ANALYSIS t8 on
		                t8.ITEM_ID=t6.ID and
		                t8.ANALYSIS_METHOD_ID=t7.ANALYSIS_ID and
                        t8.IS_DEL='0'
                    left join
		                T_SYS_DICT t9 on
		                t9.DICT_CODE=t8.UNIT and
		                t9.DICT_TYPE='item_unit'
	                where 
		                t1.[month] in({0}) and 
		                t1.RIVER_METAL_POINT_ID in({1})  and
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
    }
}
