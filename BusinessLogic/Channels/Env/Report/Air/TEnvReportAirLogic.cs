using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Report;
using System.Data;

/// <summary>
/// 功能描述：环境质量空气报表
/// 创建人：陈达峰
/// 创建时间：2013-02-04
/// </summary>
namespace i3.BusinessLogic.Channels.Env.Report
{
    public class TEnvReportAirLogic
    {
        TEnvReportAirAccess access = new TEnvReportAirAccess();
        

        public DataTable GetAirData(string strYear, string strMonth, string factor)
        {
            return access.GetAirData(strYear, strMonth, factor);
        }
    }
}
