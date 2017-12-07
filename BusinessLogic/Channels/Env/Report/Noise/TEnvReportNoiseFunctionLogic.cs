using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.DataAccess.Channels.Env.Report;

namespace i3.BusinessLogic.Channels.Env.Report
{
    /// <summary>
    /// 功能描述：环境质量功能区噪声报表
    /// 创建人：钟杰华
    /// 创建时间：2013-01-30
    /// </summary>
    public class TEnvReportNoiseFunctionLogic
    {
        TEnvReportNoiseFunctionAccess access = new TEnvReportNoiseFunctionAccess();

        /// <summary>
        /// 获取功能区噪声监测数据表数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="season">季度</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetFunctionNoiseDataReportData(string year, string season, string pointId)
        {
            return access.GetFunctionNoiseDataReportData(year, season, pointId);
        }
        /// <summary>
        /// 获取功能区噪声dbf文件数据
        /// </summary>
        /// <param name="strSTime"></param>
        /// <param name="strETime"></param>
        /// <returns></returns>
        public DataTable GetDbfData(string strYear, string strMonth,string strDay)
        {
            return access.GetDbfData(strYear, strMonth, strDay);
        }
    }
}
