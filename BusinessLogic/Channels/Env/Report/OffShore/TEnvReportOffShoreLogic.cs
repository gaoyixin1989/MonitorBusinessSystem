using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.DataAccess.Channels.Env.Report.OffShore;

namespace i3.BusinessLogic.Channels.Env.Report.OffShore
{
    /// <summary>
    /// 功能描述：直排入海报表
    /// 创建人：钟杰华
    /// 创建日期：2013/02/26
    /// </summary>
    public class TEnvReportOffShoreLogic
    {
        TEnvReportOffShoreAccess access = new TEnvReportOffShoreAccess();

        /// <summary>
        /// 获取直排海污染源监测浓度数据上报表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetOffShoreMonitorConcentrationReportData(string month, string pointId)
        {
            string m = "";
            foreach (string monthTemp in month.Split(';'))
            {
                m += "'" + monthTemp + "',";
            }
            m = m.TrimEnd(',');

            string pId = "";
            foreach (string pointIdTemp in pointId.Split(';'))
            {
                pId += "'" + pointIdTemp + "',";
            }
            pId = pId.TrimEnd(',');

            return access.GetOffShoreMonitorConcentrationReportData(m, pId);
        }

        /// <summary>
        /// 获取直排海污染源监测总量数据上报表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetOffShoreMonitorTotalReportData(string month, string pointId)
        {
            string m = "";
            foreach (string monthTemp in month.Split(';'))
            {
                m += "'" + monthTemp + "',";
            }
            m = m.TrimEnd(',');

            string pId = "";
            foreach (string pointIdTemp in pointId.Split(';'))
            {
                pId += "'" + pointIdTemp + "',";
            }
            pId = pId.TrimEnd(',');

            return access.GetOffShoreMonitorTotalReportData(m, pId);
        }
    }
}
