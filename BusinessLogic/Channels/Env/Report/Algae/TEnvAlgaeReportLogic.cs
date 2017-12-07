using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Report.Algae;
using System.Data;

/// <summary>
/// 功能描述：蓝藻水华
/// 创建人：陈达峰
/// 创建时间：2013-02-26
/// </summary>
namespace i3.BusinessLogic.Channels.Env.Report.Algae
{
    public class TEnvAlgaeReportLogic
    {
        TEnvAlgaeReportAccess access = new TEnvAlgaeReportAccess();
        /// <summary>
        /// 获取蓝藻水华监测数据
        /// </summary>
        /// <param name="month"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public DataTable GetMonitorData(string month, string pointId)
        {
            return access.GetMonitorData(month, pointId);
        }
    }
}
