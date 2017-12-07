using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace i3.DataAccess.Channels.Env.Report
{
    public class TEnvReportAirAccess : SqlHelper
    {
        /// <summary>
        /// 获取空气数据
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public DataTable GetAirData(string strYear, string strMonth, string factor)
        {
            string strSQL = String.Format(@"SELECT  '130300' stcode
                                              ,YEAR ye
                                              ,POINT_CODE pcode
                                              ,POINT_NAME pname
                                              ,MONTH smo
                                              ,DAY sda
                                              ,'0' sho
                                              ,'0' smi
                                              ,MONTH smo
                                              ,DAY eda
                                              ,'23' sho
                                              ,'59' smi
                                              ,{2} value
                                          FROM T_ENV_FILL_AIR where YEAR='{0}' and Month='{1}'", strYear, strMonth,factor);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }
}
