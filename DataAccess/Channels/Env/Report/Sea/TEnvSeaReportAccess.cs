using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// 功能描述：近岸海域报表
/// 创建人：陈达峰
/// 创建日期：2013-02-28
/// </summary>
namespace i3.DataAccess.Channels.Env.Report.Sea
{
    public class TEnvSeaReportAccess:SqlHelper
    {
        /// <summary>
        /// 近岸海域水质监测结果
        /// </summary>
        /// <param name="month"></param>
        /// <param name="strPoint"></param>
        /// <returns></returns>
        public DataTable GetMonitorData(string month, string strPoint)
        {
            string strSql = String.Format(@"select
                                            t6.id as point_code,
                                            t6.sea_name as station_code,
                                            '' as sea_area,
                                            '' as pollution_sea_area,
                                            '' as sea_dept,
                                            t6.year as year,
                                            t1.month as month,
                                            t1.sampling_day as day,
                                            '' spk,
                                            '' season,
                                            '' cengci,
                                            t3.item_value,
                                            t4.item_name+isnull(t8.dict_text,'') as item_name
                                             from t_env_fill_sea t1
	                                            left join t_env_point_sea_item t2
		                                            on t1.sea_point_id=t2.sea_point_id
	                                            left join t_env_fill_sea_item t3 
		                                            on t3.sea_fill_id=t1.id and t3.item_id=t2.item_id
	                                            left join t_base_item_info t4 
		                                            on t4.id=t3.item_id
	                                            left join t_env_point_sea t6
		                                            on t6.id=t1.sea_point_id
	                                            left join t_base_item_analysis t7
		                                            on t7.item_id=t2.item_id and t7.analysis_method_id=t2.analysis_id
	                                            left join t_sys_dict t8 
		                                            on t8.dict_code=t7.unit and dict_type='item_unit'
		                                            where 
		                                            t1.sea_point_id in ({0}) 
		                                            and t1.month='{1}' and t3.item_id is not null", strPoint, month);
            return SqlHelper.ExecuteDataTable(strSql);
        }
    }
}
