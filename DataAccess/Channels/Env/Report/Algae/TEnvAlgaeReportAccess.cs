using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// 功能描述：蓝藻水华报表
/// 创建人：陈达峰
/// 创建时间：2013-02-26
/// </summary>
namespace i3.DataAccess.Channels.Env.Report.Algae
{
    public class TEnvAlgaeReportAccess:SqlHelper
    {
        public DataTable GetMonitorData(string month, string pointId)
        {
            string strSQL = string.Format(@"select
		                                    (select dict_text from t_sys_dict where dict_code =t3.area and dict_type='administrative_area') area,
		                                    t3.point_name,
		                                    (select dict_text from t_sys_dict where dict_code=t3.function_attribute and dict_type='function_attribute') function_attr,
		                                    t3.year+'.'+t1.month+'.'+t1.sampling_day rec_time,
		                                    t2.item_value,
		                                    t5.Item_name+isnull(t7.dict_text,'') item_name
                                        from t_env_fill_algae t1 
                                      left join T_ENV_POINT_ALGAE t3 on
			                                    t3.id=t1.algae_point_id
                                      left join T_ENV_POINT_ALGAE_Item t4 on
			                                    t4.algae_point_id=t1.algae_point_id
                                      left join t_env_fill_algae_item t2 on
			                                t1.id=t2.algae_fill_id and t2.item_id=t4.item_id
                                      left join t_base_item_info t5 on
			                                    t5.id=t2.item_id
                                      left join T_BASE_ITEM_ANALYSIS t6 on
			                                    t6.item_id=t4.item_id and t6.analysis_method_id=t4.analysis_id
                                      left join t_sys_dict t7 on 
                                      t7.dict_code=t6.unit and dict_type='item_unit'
			                                    where t1.algae_point_id in({0}) and t1.month='{1}'", pointId,month);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }
}
