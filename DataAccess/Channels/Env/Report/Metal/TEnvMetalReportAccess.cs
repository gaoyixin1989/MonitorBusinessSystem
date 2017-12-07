using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
// <summary>
/// 功能描述：河流底泥监测数据表
/// 创建人：陈达峰
/// 创建日期：2012-02-25
/// </summary>
namespace i3.DataAccess.Channels.Env.Report.Metal
{
    public class TEnvMetalReportAccess:SqlHelper
    {
        /// <summary>
        /// 获取底泥重金属监测数据
        /// </summary>
        /// <param name="month"></param>
        /// <param name="strPoint"></param>
        /// <param name="statisType"></param>
        /// <returns></returns>
        public DataTable GetMonitorData(string strPoint, string statisType)
        {
            string vertical_name = "+'('+t6.vertical_name+')'";
            string strGroup = "t.vertical_id";
            if (statisType == "avg")
            {
                strGroup = "t.section_id";
                vertical_name = "";
            }
            string strSQL = String.Format(@"select 
	                                        '' section_id,
                                            '' section_name,
                                            '' vertical_id,
	                                        '' province,
	                                        '' river,
	                                        '' valley,
	                                         t1.condition_name,t2.item_id,
	                                        (select item_name from t_base_item_info where id=t2.item_id) item_name,
	                                         t2.Discharge_upper item_value 
	                                          from T_Base_Evaluation_Con_info t1
		                                        left join t_base_evaluation_con_item t2
			                                        on t1.id=t2.condition_id
			                                        where t1.standard_id =
			                                        (
				                                        select distinct standard_id from  T_Base_Evaluation_Con_ITEM  where condition_id
				                                        in (select condition_id from t_env_point_metal)
			                                        ) 
			                                        and t1.condition_code is not null
			                                        and (
				                                        t2.item_id in(
						                                        select item_id from t_env_point_metal_v_item where vertical_id in 
						                                        (
							                                        select id from T_ENV_POINT_METAL_V
			 				                                        where sediment_metal_id in({0})
						                                        )
					                                        )
				                                        )
                                        union all
                                        select 
                                            max(t.section_id) as section_id,
                                            max(t.section_name) as section_name,
                                            max(t.vertical_id) as vertical_id,
                                            max(t.station_name) as station_name,
                                            max(t.province) as province,
                                            max(t.river) as river,
                                            max(t.valley) as valley,
                                            max(t.condition) as condition,
                                            max(t.item_id) as item_id,
                                            max(t.item_name) as item_name,
                                            cast(Round(avg(cast(t.item_value as float)),2) as decimal(10,2)) as item_value
                                            from(
                                                    select
                                                        t5.id section_id,
			                                            t1.vertical_id,
	                                                    t5.section_name{2} section_name, 
	                                                    (select dict_text from t_sys_dict where dict_code=t5.STATION_ID and dict_type='SATAIONS') station_name,
	                                                    (select dict_text from t_sys_dict where dict_code=t5.province_id and dict_type='province') province,
	                                                    (select dict_text from t_sys_dict where id=t5.river_id) river,
	                                                    (select dict_text from t_sys_dict where id=t5.valley_id) valley,
	                                                    '' condition,
	                                                    t4.id item_id,
	                                                    t4.item_name,
	                                                    t3.item_value
                                                        from t_env_fill_metal t1
	                                                    left join t_env_point_metal_v_item t2 on
		                                                    t2.vertical_id=t1.vertical_id 
	                                                    left join t_env_fill_metal_item t3 on
	                                                        t1.id=t3.sediment_metal_fill_id and t3.item_id=t2.item_id
	                                                    left join t_base_item_info t4 on 
	                                                        t4.id=t3.item_id
	                                                    left join t_env_point_metal t5 on
	                                                        t5.id=t1.sediment_metal_point_id
	                                                    left join t_env_point_metal_v t6 on 
	                                                    t6.id=t1.vertical_id
	                                                    where t1.sediment_metal_point_id in({0}) and t3.item_id is not null
                                                            and isnull(item_value,'')<>''
                                              )t group by {1},t.item_id", strPoint,strGroup, vertical_name);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }
}
