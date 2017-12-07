using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace i3.DataAccess.Channels.Env.Report.OffShore
{
    /// <summary>
    /// 功能描述：直排入海报表
    /// 创建人：钟杰华
    /// 创建日期：2013/02/26
    /// </summary>
    public class TEnvReportOffShoreAccess:SqlHelper
    {
        /// <summary>
        /// 获取直排海污染源监测浓度数据上报表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetOffShoreMonitorConcentrationReportData(string month, string pointId)
        {
            string sqlStr = @"
                select
	                [YEAR],
	                [MONTH],
	                max(SAMPLING_DAY) as SAMPLING_DAY,
	                max(COMPANY_NAME) as COMPANY_NAME,
	                max(ITEM_NAME) as ITEM_NAME,
	                cast(round(avg(cast(isnull(ITEM_VALUE,'0') as float)),2) as decimal(10,2)) as ITEM_VALUE,
	                POINT_ID,
	                ITEM_ID
                from (
	                select
		                t4.[YEAR],
		                t1.[MONTH],
		                t1.[SAMPLING_DAY],
		                t4.COMPANY_NAME,
		                t5.ITEM_NAME,
		                t2.ITEM_VALUE,
		                t4.ID as POINT_ID,
		                t5.ID as ITEM_ID
	                from
		                T_ENV_FILL_OFFSHORE t1
	                left join 
		                T_ENV_FILL_OFFSHORE_ITEM t2 on t2.OFFSHORE_FILL_ID=t1.ID and
		                t2.ITEM_ID in(select ITEM_ID from T_ENV_POINT_OFFSHORE_ITEM t3 where t3.OFFSHORE_POINT_ID=t1.OFFSHORE_POINT_ID)
	                left join
		                T_ENV_POINT_OFFSHORE t4 on t4.ID=t1.OFFSHORE_POINT_ID and
		                t4.IS_DEL='0'
	                left join 
		                T_BASE_ITEM_INFO t5 on
		                t5.ID=t2.ITEM_ID and
		                t5.IS_DEL='0'
	                where
		                t1.[MONTH] in({0}) and
		                t1.OFFSHORE_POINT_ID in({1}) and
		                isnull(t2.ITEM_ID,'-999')<>'-999' and
		                isnull(t2.ITEM_VALUE,'')<>''
                ) t
                group by
	                [YEAR],
	                [MONTH],
	                POINT_ID,
	                ITEM_ID	
            ";

            sqlStr = string.Format(sqlStr, month, pointId);

            return ExecuteDataTable(sqlStr);
        }

        /// <summary>
        /// 获取直排海污染源监测浓度数据上报表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetOffShoreMonitorTotalReportData(string month, string pointId)
        {
            string sqlStr = @"
                select
	                [YEAR],
	                [MONTH],
	                max(SAMPLING_DAY) as SAMPLING_DAY,
	                max(COMPANY_NAME) as COMPANY_NAME,
	                max(ITEM_NAME) as ITEM_NAME,
	                cast(round(avg(cast(isnull(ITEM_VALUE,'0') as float)),2) as decimal(10,2)) as ITEM_VALUE,
	                POINT_ID,
	                ITEM_ID
                from (
	                select
		                t4.[YEAR],
		                t1.[MONTH],
		                t1.[SAMPLING_DAY],
		                t4.COMPANY_NAME,
		                t5.ITEM_NAME,
		                t2.ITEM_VALUE,
		                t4.ID as POINT_ID,
		                t5.ID as ITEM_ID
	                from
		                T_ENV_FILL_OFFSHORE t1
	                left join 
		                T_ENV_FILL_OFFSHORE_ITEM t2 on t2.OFFSHORE_FILL_ID=t1.ID and
		                t2.ITEM_ID in(select ITEM_ID from T_ENV_POINT_OFFSHORE_ITEM t3 where t3.OFFSHORE_POINT_ID=t1.OFFSHORE_POINT_ID)
	                left join
		                T_ENV_POINT_OFFSHORE t4 on t4.ID=t1.OFFSHORE_POINT_ID and
		                t4.IS_DEL='0'
	                left join 
		                T_BASE_ITEM_INFO t5 on
		                t5.ID=t2.ITEM_ID and
		                t5.IS_DEL='0'
	                where
		                t1.[MONTH] in({0}) and
		                t1.OFFSHORE_POINT_ID in({1}) and
		                isnull(t2.ITEM_ID,'-999')<>'-999' and
		                isnull(t2.ITEM_VALUE,'')<>''
                ) t
                group by
	                [YEAR],
	                [MONTH],
	                POINT_ID,
	                ITEM_ID	
            ";

            sqlStr = string.Format(sqlStr, month, pointId);

            return ExecuteDataTable(sqlStr);
        }
    }
}
