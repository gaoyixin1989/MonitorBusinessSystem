using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.DataAccess.Channels.Env.Report.River;

namespace i3.BusinessLogic.Channels.Env.Report.River
{
    /// <summary>
    /// 功能描述：江河水报表
    /// 创建人：钟杰华
    /// 创建日期：2013-02-25
    /// </summary>
    public class TEnvReportRiverLogic
    {
        TEnvReportRiverAccess access = new TEnvReportRiverAccess();

        /// <summary>
        /// 获取新三十重点县地表水监测表数据
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetNewDoubleThirtyReportData(string month, string pointId)
        {
            //处理pointId
            string pId = "";
            foreach (string pointIdTemp in pointId.Split(';'))
            {
                pId += "'" + pointIdTemp + "',";
            }
            pId = pId.TrimEnd(',');

            return access.GetNewDoubleThirtyReportData(month, pId);
        }
    }
}
