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
    /// 功能：颗粒物原始记录表-属性表
    /// 创建日期：2013-07-09
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorDustattributeAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_DUSTATTRIBUTE " + this.BuildWhereStatement(tMisMonitorDustattribute);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTATTRIBUTE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorDustattributeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributeVo Details(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTATTRIBUTE " + this.BuildWhereStatement(tMisMonitorDustattribute));
            return SqlHelper.ExecuteObject(new TMisMonitorDustattributeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorDustattributeVo> SelectByObject(TMisMonitorDustattributeVo tMisMonitorDustattribute, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTATTRIBUTE " + this.BuildWhereStatement(tMisMonitorDustattribute));
            return SqlHelper.ExecuteObjectList(tMisMonitorDustattribute, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorDustattributeVo tMisMonitorDustattribute, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_DUSTATTRIBUTE {0} order by id";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorDustattribute));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorDustattribute"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            string strSQL = "select * from T_MIS_MONITOR_DUSTATTRIBUTE " + this.BuildWhereStatement(tMisMonitorDustattribute) + " order by id";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象</param>
        /// <returns></returns>
        public TMisMonitorDustattributeVo SelectByObject(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            string strSQL = "select * from T_MIS_MONITOR_DUSTATTRIBUTE " + this.BuildWhereStatement(tMisMonitorDustattribute);
            return SqlHelper.ExecuteObject(new TMisMonitorDustattributeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorDustattribute, TMisMonitorDustattributeVo.T_MIS_MONITOR_DUSTATTRIBUTE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattribute">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorDustattribute, TMisMonitorDustattributeVo.T_MIS_MONITOR_DUSTATTRIBUTE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorDustattribute.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattribute_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorDustattribute_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributeVo tMisMonitorDustattribute_UpdateSet, TMisMonitorDustattributeVo tMisMonitorDustattribute_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorDustattribute_UpdateSet, TMisMonitorDustattributeVo.T_MIS_MONITOR_DUSTATTRIBUTE_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorDustattribute_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_DUSTATTRIBUTE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            string strSQL = "delete from T_MIS_MONITOR_DUSTATTRIBUTE ";
            strSQL += this.BuildWhereStatement(tMisMonitorDustattribute);

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
        public bool UpdateCell(string strId,string strCellName, string strCellValue) 
        {
            string strSQL = String.Format(@" UPDATE dbo.T_MIS_MONITOR_DUSTATTRIBUTE SET {0}='{1}' WHERE ID='{2}'", strCellName, strCellValue, strId);
            return SqlHelper.ExecuteNonQuery(strSQL)>0?true:false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorDustattribute"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorDustattribute)
            {

                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorDustattribute.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.BASEINFOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BASEINFOR_ID = '{0}'", tMisMonitorDustattribute.BASEINFOR_ID.ToString()));
                }
                //采样序号
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SAMPLE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_CODE = '{0}'", tMisMonitorDustattribute.SAMPLE_CODE.ToString()));
                }
                //滤筒编号
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.FITER_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FITER_CODE = '{0}'", tMisMonitorDustattribute.FITER_CODE.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SAMPLE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_DATE = '{0}'", tMisMonitorDustattribute.SAMPLE_DATE.ToString()));
                }
                //烟气动压
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SMOKE_MOVE_PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_MOVE_PRESSURE = '{0}'", tMisMonitorDustattribute.SMOKE_MOVE_PRESSURE.ToString()));
                }
                //烟气静压
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SMOKE_STATIC_PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_STATIC_PRESSURE = '{0}'", tMisMonitorDustattribute.SMOKE_STATIC_PRESSURE.ToString()));
                }
                //烟气全压
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SMOKE_ALL_PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_ALL_PRESSURE = '{0}'", tMisMonitorDustattribute.SMOKE_ALL_PRESSURE.ToString()));
                }
                //烟气计压
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SMOKE_K_PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_K_PRESSURE = '{0}'", tMisMonitorDustattribute.SMOKE_K_PRESSURE.ToString()));
                }
                //烟气温度
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SMOKE_TEMPERATURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_TEMPERATURE = '{0}'", tMisMonitorDustattribute.SMOKE_TEMPERATURE.ToString()));
                }
                //烟气含氧量
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SMOKE_OXYGEN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_OXYGEN = '{0}'", tMisMonitorDustattribute.SMOKE_OXYGEN.ToString()));
                }
                //烟气流速
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SMOKE_SPEED.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_SPEED = '{0}'", tMisMonitorDustattribute.SMOKE_SPEED.ToString()));
                }
                //标态流量
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.NM_SPEED.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NM_SPEED = '{0}'", tMisMonitorDustattribute.NM_SPEED.ToString()));
                }
                //标况体积
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.L_STAND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND L_STAND = '{0}'", tMisMonitorDustattribute.L_STAND.ToString()));
                }
                //滤筒初重
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.FITER_BEGIN_WEIGHT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FITER_BEGIN_WEIGHT = '{0}'", tMisMonitorDustattribute.FITER_BEGIN_WEIGHT.ToString()));
                }
                //滤筒终重
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.FITER_AFTER_WEIGHT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FITER_AFTER_WEIGHT = '{0}'", tMisMonitorDustattribute.FITER_AFTER_WEIGHT.ToString()));
                }
                //样品重量
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SAMPLE_WEIGHT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_WEIGHT = '{0}'", tMisMonitorDustattribute.SAMPLE_WEIGHT.ToString()));
                }
                //烟尘浓度
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SMOKE_POTENCY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_POTENCY = '{0}'", tMisMonitorDustattribute.SMOKE_POTENCY.ToString()));
                }
                //烟尘折算浓度
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SMOKE_POTENCY2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_POTENCY2 = '{0}'", tMisMonitorDustattribute.SMOKE_POTENCY2.ToString()));
                }
                //烟尘排放量
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.SMOKE_DISCHARGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SMOKE_DISCHARGE = '{0}'", tMisMonitorDustattribute.SMOKE_DISCHARGE.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorDustattribute.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorDustattribute.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattribute.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorDustattribute.REMARK3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
