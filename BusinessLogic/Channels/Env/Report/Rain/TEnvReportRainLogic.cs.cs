using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Report;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Report
{
    /// <summary>
    /// 功能描述：环境质量降水报表
    /// 创建人：陈达峰
    /// 创建时间：2013-02-04
    /// </summary>
    public class TEnvReportRainLogic
    {
        TEnvReportRainAccess access = new TEnvReportRainAccess();
        public DataTable GetRainSourceRecordReportData(string Month, string Point)
        {
            return access.GetRainSourceRecordReportData(Month, Point);
        }
    }
}
