using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.DataAccess.Channels.Env.Report.Noise;

/// <summary>
/// 功能描述：道路交通噪声上报报表
/// 创建人：陈达峰
/// 创建时间：2013-02-26
/// </summary>
namespace i3.BusinessLogic.Channels.Env.Report.Noise
{
    public class TEnvReportRoadTraficLogic
    {
        TEnvReportRoadTraficAccess access = new TEnvReportRoadTraficAccess();
        public DataTable GetDbfReportData(string strSTime, string strETime)
        {
            return access.GetDbfReportData(strSTime, strETime);
        }
    }
}
