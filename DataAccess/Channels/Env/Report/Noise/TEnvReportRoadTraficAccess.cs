using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// 功能描述：道路交通噪声上报报表
/// 创建人：陈达峰
/// 创建时间：2013-02-04
/// </summary>
namespace i3.DataAccess.Channels.Env.Report.Noise
{
    public class TEnvReportRoadTraficAccess : SqlHelper
    {
        /// <summary>
        /// 获取道路交通噪声
        /// </summary>
        /// <param name="strSTime"></param>
        /// <param name="strETime"></param>
        /// <returns></returns>
        public DataTable GetDbfReportData(string strSTime, string strETime)
        {
            string strYear = DateTime.Parse(strSTime).Year.ToString();
            string strSMonth = DateTime.Parse(strSTime).Month.ToString();
            string strSDay = DateTime.Parse(strSTime).Day.ToString();
            string strEMonth = DateTime.Parse(strETime).Month.ToString();
            string strEDay = DateTime.Parse(strETime).Day.ToString();
            string strSQL = String.Format(@"select 
                                                '130300' Stcode,
                                                t2.year Ye,
                                                t2.road_length Rdlen,
                                                t2.road_width Rdwid,
                                                t2.road_name Rdname,
                                                t2.point_code Rdcode,
                                                t1.traffic_veh Rdtrafic,
	                                            t1.begin_month Mon,
	                                            t1.begin_day Da,
	                                            t1.begin_hour Hor,
	                                            t1.begin_minute Mi,
	                                            t1.leq Leqa,
	                                            t1.l10 L10a,
	                                            t1.l50 L50a,
	                                            t1.l90 L90a
                                            from
	                                            t_env_fill_noise_road t1
                                            left join
	                                            t_env_point_noise_road t2 on t1.road_point_id=t2.id
	                                            where t1.begin_month>='{0}' and t1.begin_day>='{1}' 
                                                    and t1.begin_month<='{2}' and t1.begin_day<='{3}'
                                                    and t2.year='{4}'", strSMonth, strSDay, strEMonth, strEDay, strYear);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }
}
