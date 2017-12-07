using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace i3.DataAccess.Channels.Env.Report.Bottom
{
    /// <summary>
    /// 功能描述：河流底泥报表
    /// 创建人：钟杰华
    /// 创建日期：2013/02/26
    /// </summary>
    public class TEnvReportBottomAccess : SqlHelper
    {
        /// <summary>
        /// 获取河流底泥监测表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetBottomMonitorReportData(string month, string pointId)
        {
            string strSql = @"
                select 
	                max(RIVER_NAME) as RIVER_NAME,
	                RIVER_ID,
	                max([DATE]) as [DATE],
	                max(POINT_NAME) as POINT_NAME,
	                POINT_ID,
	                ITEM_ID,
	                cast(round(avg(cast(isnull(ITEM_VALUE,'0') as float)),2) as decimal(10,2)) as ITEM_VALUE,
	                max(ITEM_NAME) as ITEM_NAME,
	                [MONTH],
	                VERTICAL_ID
                from (
	                select
		                t6.DICT_TEXT as RIVER_NAME,
		                t6.DICT_CODE as RIVER_ID,
		                t1.[MONTH]+'月'+t1.SAMPLING_DAY+'日' as [DATE],
		                t4.SECTION_NAME+'('+t7.VERTICAL_NAME+')' as POINT_NAME,
		                t4.ID as POINT_ID,
		                t2.ITEM_ID,
		                t2.ITEM_VALUE,
		                t5.ITEM_NAME,
		                t1.[MONTH],
		                t1.VERTICAL_ID
	                from
		                T_ENV_FILL_RIVER_BOTTOM t1
	                left join
		                T_ENV_FILL_RIVER_BOTTOM_ITEM t2 on
		                t1.ID=t2.RIVER_SEDIMENT_FILL_ID and
		                t2.ITEM_ID in(select t3.ITEM_ID from T_ENV_POINT_BOTTOM_V_ITEM t3 where t3.VERTICAL_ID=t1.VERTICAL_ID)
	                left join
		                T_ENV_POINT_BOTTOM t4 on
		                t4.ID=t1.RIVER_SEDIMENT_POINT_ID and
		                t4.IS_DEL='0'
	                left join 
		                T_BASE_ITEM_INFO t5 on
		                t5.ID=t2.ITEM_ID and
		                t5.IS_DEL='0'
	                left join
		                T_SYS_DICT t6 on
		                t6.ID=t4.RIVER_ID
	                left join
		                T_ENV_POINT_BOTTOM_V t7 on
		                t7.ID=t1.VERTICAL_ID
	                where 
		                t1.[MONTH] in({0}) and 
		                t1.RIVER_SEDIMENT_POINT_ID in({1}) and
		                isnull(t2.ITEM_ID,'-999')<>'-999' and
		                isnull(t2.ITEM_VALUE,'')<>''
                ) t
                group by
	                RIVER_ID,
	                [MONTH],
	                VERTICAL_ID,
	                POINT_ID,
	                ITEM_ID
            ";

            strSql = string.Format(strSql, month, pointId);

            return ExecuteDataTable(strSql);
        }
    }
}
