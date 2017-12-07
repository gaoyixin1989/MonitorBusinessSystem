using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Report.Rmetal;
using System.Data;

/// <summary>
/// 功能描述：地表水重金属报表
/// 创建人：陈达峰
/// 创建日期：2013-02-28
/// </summary>
namespace i3.BusinessLogic.Channels.Env.Report.Rmetal
{
    public class TEnvRMetalReportLogic
    {
        TEnvRMetalReportAccess access = new TEnvRMetalReportAccess();

        /// <summary>
        /// 地表水重金属监测数据表
        /// </summary>
        /// <param name="strPoint"></param>
        /// <param name="statisType"></param>
        /// <returns></returns>
        public DataTable GetMonitorData(string strPoint, string statisType)
        {
            return access.GetMonitorData(strPoint, statisType);
        }
    }
}
