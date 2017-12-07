using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// 功能描述：区域环境报表
/// 创建人：陈达峰
/// 创建时间：2013-02-26
/// </summary>
namespace i3.DataAccess.Channels.Env.Report
{
    public class TEnvReportAreaEnvAccess:SqlHelper
    {
        /// <summary>
        /// 获取区域环境噪声
        /// </summary>
        /// <param name="strSTime"></param>
        /// <param name="strETime"></param>
        /// <returns></returns>
        public DataTable GetDbfReportData(string strSTime, string strETime)
        {
            string strYear =DateTime.Parse(strSTime).Year.ToString();
            string strSMonth = DateTime.Parse(strSTime).Month.ToString();
            string strSDay = DateTime.Parse(strSTime).Day.ToString();
            string strEMonth = DateTime.Parse(strETime).Month.ToString();
            string strEDay = DateTime.Parse(strETime).Day.ToString();
            string strSQL = String.Format(@"select 
                                                '130300' Stcode,
                                                t2.year Ye,
                                                t2.grid_size_x Gridl,
                                                t2.grid_size_y Gridw,
                                                t2.point_name Gdname,
                                                t2.point_code Gdcode,
	                                            t2.function_area_id Ndisc,
	                                            t2.sound_source_id Nsc,
	                                            '-1' Gpopul,
	                                            t1.begin_month Mon,
	                                            t1.begin_day Da,
	                                            t1.begin_hour Hor,
	                                            t1.begin_minute Mi,
	                                            t1.leq Leqa,
	                                            t1.l10 L10a,
	                                            t1.l50 L50a,
	                                            t1.l90 L90a
                                            from
	                                            t_env_fill_noise_area t1
                                            left join
	                                            t_env_point_noise_area t2 on t1.area_point_id=t2.id
	                                            where t1.begin_month>='{0}' and t1.begin_day>='{1}' 
                                                    and t1.begin_month<='{2}' and t1.begin_day<='{3}'
                                                    and t2.year='{4}'",strSMonth,strSDay,strEMonth,strEDay,strYear);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }
}
