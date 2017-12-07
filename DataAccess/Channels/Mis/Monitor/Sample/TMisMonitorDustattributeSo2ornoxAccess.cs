using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：PM10和悬浮颗粒物原始记录表
    /// 创建日期：2013-08-29
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorDustattributeSo2ornoxAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX " + this.BuildWhereStatement(tMisMonitorDustattributeSo2ornox);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributeSo2ornoxVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorDustattributeSo2ornoxVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributeSo2ornoxVo Details(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX " + this.BuildWhereStatement(tMisMonitorDustattributeSo2ornox));
            return SqlHelper.ExecuteObject(new TMisMonitorDustattributeSo2ornoxVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorDustattributeSo2ornoxVo> SelectByObject(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX " + this.BuildWhereStatement(tMisMonitorDustattributeSo2ornox));
            return SqlHelper.ExecuteObjectList(tMisMonitorDustattributeSo2ornox, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX {0} " + " order by id";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorDustattributeSo2ornox));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            string strSQL = "select * from T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX " + this.BuildWhereStatement(tMisMonitorDustattributeSo2ornox) + " order by id";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象</param>
        /// <returns></returns>
        public TMisMonitorDustattributeSo2ornoxVo SelectByObject(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            string strSQL = "select * from T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX " + this.BuildWhereStatement(tMisMonitorDustattributeSo2ornox);
            return SqlHelper.ExecuteObject(new TMisMonitorDustattributeSo2ornoxVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorDustattributeSo2ornox, TMisMonitorDustattributeSo2ornoxVo.T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorDustattributeSo2ornox, TMisMonitorDustattributeSo2ornoxVo.T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorDustattributeSo2ornox.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorDustattributeSo2ornox_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox_UpdateSet, TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorDustattributeSo2ornox_UpdateSet, TMisMonitorDustattributeSo2ornoxVo.T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorDustattributeSo2ornox_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            string strSQL = "delete from T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX ";
            strSQL += this.BuildWhereStatement(tMisMonitorDustattributeSo2ornox);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 创建原因：更新属性数据列
        /// 创建人：胡方扬
        /// 创建日期：2013-07-05
        /// </summary>
        /// <param name="strId"></param>
        /// <param name="strCellName"></param>
        /// <param name="strCellValue"></param>
        /// <returns></returns>
        public bool UpdateCell(string strId, string strCellName, string strCellValue)
        {
            string strSQL = String.Format(@" UPDATE dbo.T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX SET {0}='{1}' WHERE ID='{2}'", strCellName, strCellValue, strId);
            return SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorDustattributeSo2ornox)
            {

                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorDustattributeSo2ornox.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.BASEINFOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BASEINFOR_ID = '{0}'", tMisMonitorDustattributeSo2ornox.BASEINFOR_ID.ToString()));
                }
                //采样序号
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SAMPLE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_CODE = '{0}'", tMisMonitorDustattributeSo2ornox.SAMPLE_CODE.ToString()));
                }
                //滤筒编号
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.FITER_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FITER_CODE = '{0}'", tMisMonitorDustattributeSo2ornox.FITER_CODE.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SAMPLE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_DATE = '{0}'", tMisMonitorDustattributeSo2ornox.SAMPLE_DATE.ToString()));
                }
                //烟气动压
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SMOKE_MOVE_PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_MOVE_PRESSURE = '{0}'", tMisMonitorDustattributeSo2ornox.SMOKE_MOVE_PRESSURE.ToString()));
                }
                //烟气静压
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SMOKE_STATIC_PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_STATIC_PRESSURE = '{0}'", tMisMonitorDustattributeSo2ornox.SMOKE_STATIC_PRESSURE.ToString()));
                }
                //烟气全压
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SMOKE_ALL_PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_ALL_PRESSURE = '{0}'", tMisMonitorDustattributeSo2ornox.SMOKE_ALL_PRESSURE.ToString()));
                }
                //烟气计压
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SMOKE_K_PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_K_PRESSURE = '{0}'", tMisMonitorDustattributeSo2ornox.SMOKE_K_PRESSURE.ToString()));
                }
                //烟气温度
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SMOKE_TEMPERATURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_TEMPERATURE = '{0}'", tMisMonitorDustattributeSo2ornox.SMOKE_TEMPERATURE.ToString()));
                }
                //烟气含氧量
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SMOKE_OXYGEN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_OXYGEN = '{0}'", tMisMonitorDustattributeSo2ornox.SMOKE_OXYGEN.ToString()));
                }
                //烟气流速
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SMOKE_SPEED.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_SPEED = '{0}'", tMisMonitorDustattributeSo2ornox.SMOKE_SPEED.ToString()));
                }
                //标态流量
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.NM_SPEED.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NM_SPEED = '{0}'", tMisMonitorDustattributeSo2ornox.NM_SPEED.ToString()));
                }
                //SO2浓度
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SO2_POTENCY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SO2_POTENCY = '{0}'", tMisMonitorDustattributeSo2ornox.SO2_POTENCY.ToString()));
                }
                //SO2折算浓度
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SO2_PER_POTENCY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SO2_PER_POTENCY = '{0}'", tMisMonitorDustattributeSo2ornox.SO2_PER_POTENCY.ToString()));
                }
                //SO2排放量
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.SO2_DISCHARGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SO2_DISCHARGE = '{0}'", tMisMonitorDustattributeSo2ornox.SO2_DISCHARGE.ToString()));
                }
                //NOX浓度
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.NOX_POTENCY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NOX_POTENCY = '{0}'", tMisMonitorDustattributeSo2ornox.NOX_POTENCY.ToString()));
                }
                //NOX折算浓度
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.NOX_PER_POTENCY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NOX_PER_POTENCY = '{0}'", tMisMonitorDustattributeSo2ornox.NOX_PER_POTENCY.ToString()));
                }
                //NOX排放量
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.NOX_DISCHARGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NOX_DISCHARGE = '{0}'", tMisMonitorDustattributeSo2ornox.NOX_DISCHARGE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorDustattributeSo2ornox.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorDustattributeSo2ornox.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattributeSo2ornox.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorDustattributeSo2ornox.REMARK3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
