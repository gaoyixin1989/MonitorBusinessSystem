using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.DataAccess.Channels.Env.Report.Estuaries;

namespace i3.BusinessLogic.Channels.Env.Report.Estuaries
{
    /// <summary>
    /// 功能描述：入海河口报表
    /// 创建人：钟杰华
    /// 创建日期：2013-02-27
    /// </summary>
    public class TEnvReportEstuariesLogic
    {
        TEnvReportEstuariesAccess access = new TEnvReportEstuariesAccess();

        /// <summary>
        /// 获取入海河流浓度监测数据报表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetEstuariesMonitorConcentrationReportData(string month, string pointId)
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

            return access.GetEstuariesMonitorConcentrationReportData(m, pId);
        }

        /// <summary>
        /// 获取入海河流总量监测数据报表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetEstuariesMonitorTotalReportData(string month, string pointId)
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

            return access.GetEstuariesMonitorTotalReportData(m, pId);
        }
    }
}
