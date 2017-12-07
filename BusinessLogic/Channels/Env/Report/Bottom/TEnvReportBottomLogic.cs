using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.DataAccess.Channels.Env.Report.Bottom;

namespace i3.BusinessLogic.Channels.Env.Report.Bottom
{
    /// <summary>
    /// 功能描述：河流底泥报表
    /// 创建人：钟杰华
    /// 创建日期：2013/02/26
    /// </summary>
    public class TEnvReportBottomLogic
    {
        TEnvReportBottomAccess access = new TEnvReportBottomAccess();

        /// <summary>
        /// 获取河流底泥监测表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetBottomMonitorReportData(string month, string pointId)
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

            return access.GetBottomMonitorReportData(m, pId);
        }
    }
}
