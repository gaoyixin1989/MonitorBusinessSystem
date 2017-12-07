using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Report;
using System.Data;


/// <summary>
/// 功能描述：区域环境报表
/// 创建人：陈达峰
/// 创建时间：2013-02-04
/// </summary>
namespace i3.BusinessLogic.Channels.Env.Report
{
    public class TEnvReportAreaEnvLogic
    {
        TEnvReportAreaEnvAccess access = new TEnvReportAreaEnvAccess();

        public DataTable GetDbfReportData(string strSTime, string strETime)
        {
            return access.GetDbfReportData(strSTime, strETime);
        }
    }
}
