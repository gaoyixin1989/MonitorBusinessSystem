using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Report.Sea;
using System.Data;
/// <summary>
/// 功能描述：近岸海域报表
/// 创建人：陈达峰
/// 创建日期：2013-02-28
/// </summary>
namespace i3.BusinessLogic.Channels.Env.Report.Sea
{
    public class TEnvSeaReportLogic
    {
        TEnvSeaReportAccess access = new TEnvSeaReportAccess();

        /// <summary>
        /// 近岸海域水质监测结果
        /// </summary>
        /// <param name="month"></param>
        /// <param name="strPoint"></param>
        /// <returns></returns>
        public DataTable GetMonitorData(string month, string strPoint)
        {
            return access.GetMonitorData(month, strPoint);
        }
    }
}
