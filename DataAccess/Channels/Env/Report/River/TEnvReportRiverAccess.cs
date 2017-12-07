using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace i3.DataAccess.Channels.Env.Report.River
{
    /// <summary>
    /// 功能描述：江河水报表
    /// 创建人：钟杰华
    /// 创建日期：2013-02-25
    /// </summary>
    public class TEnvReportRiverAccess:SqlHelper
    {
        /// <summary>
        /// 获取新三十重点县地表水监测表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetNewDoubleThirtyReportData(string month, string pointId)
        {
            string sqlStr = @"
                select 
	                max(CITY_NAME) as CITY_NAME,
	                POINT_ID,
	                max(POINT_AREA) as POINT_AREA,
	                max(POINT_AREA_ID) as POINT_AREA_ID,
	                max(POINT_NAME) as POINT_NAME,
	                max([DATE]) as [DATE],
	                ITEM_ID,
	                cast(round(avg(cast(isnull(ITEM_VALUE,'0') as float)),2) as decimal(10,2)) as ITEM_VALUE,
	                max(ITEM_NAME) as ITEM_NAME,
	                max(UNIT) as UNIT
                from (
	                select 
		                (select DICT_TEXT from T_SYS_DICT where DICT_TYPE='local' and DICT_CODE='curLocal') as CITY_NAME,
		                t4.ID as POINT_ID,
		                t5.DICT_TEXT as POINT_AREA,
		                t5.DICT_CODE as POINT_AREA_ID,
		                t4.POINT_NAME,
		                t4.[YEAR]+'/'+t1.[MONTH]+'/'+t1.SAMPLING_DAY as [DATE],
		                t2.ITEM_ID,
		                t2.ITEM_VALUE,
		                t6.ITEM_NAME,
		                t9.DICT_TEXT as UNIT
	                from 
		                T_ENV_FILL_RIVER t1
	                left join 
		                T_ENV_FILL_RIVER_ITEM t2 on 
		                t2.RIVER_FILL_ID=t1.ID and 
		                t2.ITEM_ID in(select t3.ITEM_ID from T_ENV_POINT_R_V_ITEM t3 where t3.VERTICAL_ID=t1.VERTICAL_ID)
	                left join
		                T_ENV_POINT_RIVER t4 on
		                t4.ID=t1.RIVER_POINT_ID and
		                t4.IS_DEL='0'
	                left join
		                T_SYS_DICT t5 on
		                t5.DICT_TYPE='administrative_area' and
		                t5.DICT_CODE=t4.POINT_AREA
	                left join 
		                T_BASE_ITEM_INFO t6 on
		                t6.ID=t2.ITEM_ID and
                        t6.IS_DEL='0'
	                left join 
		                T_ENV_POINT_R_V_ITEM t7 on
		                t7.VERTICAL_ID=t1.VERTICAL_ID and
		                t7.ITEM_ID=t6.ID
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
		                t1.[month]='{0}' and 
		                t1.river_point_id in({1})  and
		                isnull(t2.ITEM_ID,'-999')<>'-999' and
		                isnull(t2.ITEM_VALUE,'')<>''
                ) t
                group by
	                POINT_ID,
	                ITEM_ID
            ";

            sqlStr = string.Format(sqlStr, month, pointId);

            return ExecuteDataTable(sqlStr);
        }
    }
}
