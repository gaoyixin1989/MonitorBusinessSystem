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
    public class TMisMonitorDustattributePmAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_DUSTATTRIBUTE_PM " + this.BuildWhereStatement(tMisMonitorDustattributePm);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributePmVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTATTRIBUTE_PM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorDustattributePmVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributePmVo Details(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTATTRIBUTE_PM " + this.BuildWhereStatement(tMisMonitorDustattributePm));
            return SqlHelper.ExecuteObject(new TMisMonitorDustattributePmVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorDustattributePmVo> SelectByObject(TMisMonitorDustattributePmVo tMisMonitorDustattributePm, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_DUSTATTRIBUTE_PM " + this.BuildWhereStatement(tMisMonitorDustattributePm));
            return SqlHelper.ExecuteObjectList(tMisMonitorDustattributePm, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorDustattributePmVo tMisMonitorDustattributePm, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_DUSTATTRIBUTE_PM {0} order by id";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorDustattributePm));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorDustattributePm"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            string strSQL = "select * from T_MIS_MONITOR_DUSTATTRIBUTE_PM " + this.BuildWhereStatement(tMisMonitorDustattributePm) + " order by id";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象</param>
        /// <returns></returns>
        public TMisMonitorDustattributePmVo SelectByObject(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            string strSQL = "select * from T_MIS_MONITOR_DUSTATTRIBUTE_PM " + this.BuildWhereStatement(tMisMonitorDustattributePm);
            return SqlHelper.ExecuteObject(new TMisMonitorDustattributePmVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorDustattributePm, TMisMonitorDustattributePmVo.T_MIS_MONITOR_DUSTATTRIBUTE_PM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorDustattributePm, TMisMonitorDustattributePmVo.T_MIS_MONITOR_DUSTATTRIBUTE_PM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorDustattributePm.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattributePm_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorDustattributePm_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributePmVo tMisMonitorDustattributePm_UpdateSet, TMisMonitorDustattributePmVo tMisMonitorDustattributePm_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorDustattributePm_UpdateSet, TMisMonitorDustattributePmVo.T_MIS_MONITOR_DUSTATTRIBUTE_PM_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorDustattributePm_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_DUSTATTRIBUTE_PM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            string strSQL = "delete from T_MIS_MONITOR_DUSTATTRIBUTE_PM ";
            strSQL += this.BuildWhereStatement(tMisMonitorDustattributePm);

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
            string strSQL = String.Format(@" UPDATE dbo.T_MIS_MONITOR_DUSTATTRIBUTE_PM SET {0}='{1}' WHERE ID='{2}'", strCellName, strCellValue, strId);
            return SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorDustattributePm"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorDustattributePm)
            {

                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorDustattributePm.ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.BASEINFOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BASEINFOR_ID = '{0}'", tMisMonitorDustattributePm.BASEINFOR_ID.ToString()));
                }
                //采样序号
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.SAMPLE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_CODE = '{0}'", tMisMonitorDustattributePm.SAMPLE_CODE.ToString()));
                }
                //滤筒编号
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.FITER_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FITER_CODE = '{0}'", tMisMonitorDustattributePm.FITER_CODE.ToString()));
                }
                //采样开始日期
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.SAMPLE_BEGINDATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_BEGINDATE = '{0}'", tMisMonitorDustattributePm.SAMPLE_BEGINDATE.ToString()));
                }
                //采样结束日期
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.SAMPLE_ENDDATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ENDDATE = '{0}'", tMisMonitorDustattributePm.SAMPLE_ENDDATE.ToString()));
                }
                //采样累计时间
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.ACCTIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ACCTIME = '{0}'", tMisMonitorDustattributePm.ACCTIME.ToString()));
                }
                //采样体积
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.SAMPLE_L_STAND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_L_STAND = '{0}'", tMisMonitorDustattributePm.SAMPLE_L_STAND.ToString()));
                }
                //标况采样体积
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.L_STAND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND L_STAND = '{0}'", tMisMonitorDustattributePm.L_STAND.ToString()));
                }
                //标态流量
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.NM_SPEED.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NM_SPEED = '{0}'", tMisMonitorDustattributePm.NM_SPEED.ToString()));
                }
                //新品初重
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.SAMPLE_FWEIGHT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_FWEIGHT = '{0}'", tMisMonitorDustattributePm.SAMPLE_FWEIGHT.ToString()));
                }
                //样品终重
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.SAMPLE_EWEIGHT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_EWEIGHT = '{0}'", tMisMonitorDustattributePm.SAMPLE_EWEIGHT.ToString()));
                }
                //样品重量
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.SAMPLE_WEIGHT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_WEIGHT = '{0}'", tMisMonitorDustattributePm.SAMPLE_WEIGHT.ToString()));
                }
                //样品浓度
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.SAMPLE_CONCENT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_CONCENT = '{0}'", tMisMonitorDustattributePm.SAMPLE_CONCENT.ToString()));
                }
                //采样地点
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.SAMPLE_POINT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_POINT = '{0}'", tMisMonitorDustattributePm.SAMPLE_POINT.ToString()));
                }
                //采样介质编号
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.SAMPLE_MEDCODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_MEDCODE = '{0}'", tMisMonitorDustattributePm.SAMPLE_MEDCODE.ToString()));
                }
                //废气排放量
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.FQPFL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FQPFL = '{0}'", tMisMonitorDustattributePm.FQPFL.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorDustattributePm.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorDustattributePm.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisMonitorDustattributePm.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorDustattributePm.REMARK3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
