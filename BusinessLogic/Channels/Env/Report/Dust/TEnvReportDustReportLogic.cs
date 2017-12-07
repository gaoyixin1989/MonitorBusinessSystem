using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.DataAccess.Channels.Env.Report;

namespace i3.BusinessLogic.Channels.Env.Report
{
    /// <summary>
    /// 功能描述：环境质量降尘报表
    /// 创建人：陈达峰
    /// 创建时间：2013-02-04
    /// </summary>
    public class TEnvReportDustReportLogic
    {
        TEnvReportDustReportAccess access = new TEnvReportDustReportAccess();
        /// <summary>
        /// 获取降尘原始数据
        /// </summary>
        /// <param name="strYear"></param>
        /// <returns></returns>
        public DataTable GetDustSourceData(string strYear)
        {
            return access.GetDustSourceData(strYear);
        }
    }
}
