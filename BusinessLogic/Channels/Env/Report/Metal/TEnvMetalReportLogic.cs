using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Report.Metal;
using System.Data;
// <summary>
/// 功能描述：河流底泥监测数据表
/// 创建人：陈达峰
/// 创建日期：2012-02-25
/// </summary>
namespace i3.BusinessLogic.Channels.Env.Report.Metal
{
    public class TEnvMetalReportLogic
    {
        TEnvMetalReportAccess access = new TEnvMetalReportAccess();

        /// <summary>
        /// 获取底泥重金属监测数据
        /// </summary>
        /// <param name="month"></param>
        /// <param name="strPoint"></param>
        /// <param name="statisType"></param>
        /// <returns></returns>
        public DataTable GetMonitorData(string strPoint,string statisType)
        {
            return access.GetMonitorData(strPoint, statisType);
        }
    }
}
