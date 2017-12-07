using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Fill.FillQry;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.FillQry
{
    /// <summary>
    /// 功能：统计填报数据
    /// 创建日期：2013-07-05
    /// 创建人：魏林
    /// </summary>
    public class FillQryLogic : LogicBase
    {
        public override bool Validate()
        {
            throw new NotImplementedException();
        }

        FillQryAccess access;

        public FillQryLogic()
        {
            access = new FillQryAccess();
        }

        /// <summary>
        /// 获取点位信息
        /// </summary>
        /// <param name="Select">获取的信息</param>
        /// <param name="TableName">表的来源</param>
        /// <param name="Where">条件</param>
        /// <returns></returns>
        public DataTable GetPointInfo(string Select, string TableName, string Where)
        {
            return access.GetPointInfo(Select, TableName, Where);
        }

        /// <summary>
        /// 获取填报统计数据
        /// </summary>
        /// <param name="type">类别</param>
        /// <param name="year">年份</param>
        /// <param name="months">月份</param>
        /// <param name="point">测点编号</param>
        /// <returns></returns>
        public DataTable GetDataInfo(string type, string year, string months, string point, string temp)
        {
            return access.GetDataInfo(type, year, months, point, temp);
        }

        /// <summary>
        /// 获取综合评价统计报表信息
        /// </summary>
        /// <param name="type">填报类型</param>
        /// <param name="year">年份</param>
        /// <param name="months">月份</param>
        /// <param name="point_code">监测点CODE</param>
        /// <param name="river_id">河流ID</param>
        /// <returns></returns>
        public DataTable GetFillEvalData(string type, string year, string months, string point_code, string river_id)
        {
            return access.GetFillEvalData(type, year, months, point_code, river_id);
        }

        /// <summary>
        /// 获取全市空气均值统计报表信息
        /// </summary>
        /// <param name="strDateS"></param>
        /// <param name="strDateE"></param>
        /// <returns></returns>
        public DataTable GetAirAvgData(string strDateS, string strDateE)
        {
            return access.GetAirAvgData(strDateS, strDateE);
        }

        /// <summary>
        /// 获取空气监测点均值情况统计报表信息
        /// </summary>
        /// <param name="strDateS"></param>
        /// <param name="strDateE"></param>
        /// <returns></returns>
        public DataTable GetAirPointAvgData(string strDateS, string strDateE, string strPoint)
        {
            return access.GetAirPointAvgData(strDateS, strDateE, strPoint);
        }

        /// <summary>
        /// 获取全市空气首要污染物指数统计报表信息
        /// </summary>
        /// <param name="strDateS"></param>
        /// <param name="strDateE"></param>
        /// <returns></returns>
        public DataTable GetAirPullutionData(string strDateS, string strDateE)
        {
            return access.GetAirPullutionData(strDateS, strDateE);
        }

        /// <summary>
        /// 获取空气监测点首要污染物指数统计报表信息
        /// </summary>
        /// <param name="strDateS"></param>
        /// <param name="strDateE"></param>
        /// <returns></returns>
        public DataTable GetAirPointPullutionData(string strDateS, string strDateE, string strPointCode)
        {
            return access.GetAirPointPullutionData(strDateS, strDateE, strPointCode);
        }
    }
}
