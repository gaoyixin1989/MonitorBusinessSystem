using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.DataAccess.Channels.Env.Report.Drinking;

namespace i3.BusinessLogic.Channels.Env.Report.Drinking
{
    /// <summary>
    /// 功能描述：饮用水报表
    /// 创建人：钟杰华
    /// 创建日期：2013-02-28
    /// </summary>
    public class TEnvReportDrinkingLogic
    {
        TEnvReportDrinkingAccess access = new TEnvReportDrinkingAccess();

        /// <summary>
        /// 饮用水源地水质月报(全分析监测/33项)
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetDrinkingMonthReportData(string month, string pointId)
        {
            //处理pointId
            string pId = "";
            foreach (string pointIdTemp in pointId.Split(';'))
            {
                pId += "'" + pointIdTemp + "',";
            }
            pId = pId.TrimEnd(',');

            return access.GetDrinkingMonthReportData(month, pId);
        }

        /// <summary>
        /// 获取水库（饮用水源）水质监测结果填报表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetReservoirMonitorReportData(string month, string pointId)
        {
            //处理pointId
            string pId = "";
            foreach (string pointIdTemp in pointId.Split(';'))
            {
                pId += "'" + pointIdTemp + "',";
            }
            pId = pId.TrimEnd(',');

            return access.GetReservoirMonitorReportData(month, pId);
        }

        /// <summary>
        /// 获取饮用水源地全分析报表数据
        /// </summary>
        /// <param name="month"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public DataTable GetAllAnalysisReportData(string month, string pointId)
        {
            return access.GetAllAnalysisReportData(month, pointId);
        }
    }
}
