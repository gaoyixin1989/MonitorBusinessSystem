using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace i3.DataAccess.Channels.Env.Report
{
    public class TEnvReportRainAccess : SqlHelper
    {
        public DataTable GetRainSourceRecordReportData(string Month, string Point)
        {
            string strSQL = string.Format(@"select row_number()over(order by fr_id )as id,
                                            t3.*,t4.ITEM_ID AS mi_id,(select ITEM_NAME from T_BASE_ITEM_INFO where ID=t4.ITEM_ID) AS mi_name,
                                            t4.ITEM_VALUE AS mi_value
                                                from (SELECT t1.ID AS fr_id,
                                                    t2.POINT_NAME AS point_name,
                                                    t2.POINT_CODE AS point_code,
                                                    (t1.BEGIN_MONTH+'.'+t1.BEGIN_DAY+' '+t1.BEGIN_HOUR+':'+t1.BEGIN_MINUTE) AS begin_date,
                                                    (t1.END_MONTH+'.'+t1.END_DAY+' '+t1.END_HOUR+':'+t1.END_MINUTE) AS end_date,
                                                    t1.rain_type,
                                                    t1.precipitation
                                                FROM 
                                                    T_ENV_FILL_RAIN t1 
                                                LEFT JOIN
                                                    T_ENV_POINT_RAIN t2 ON t2.ID=t1.RAIN_POINT_ID
                                                WHERE  t1.BEGIN_MONTH='{0}' and t2.ID='{1}'
                                                ) t3 left join dbo.T_ENV_FILL_RAIN_ITEM t4 
                                                    on t4.rain_fill_id =t3.fr_id
                                                        ",Month,Point);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }
}
