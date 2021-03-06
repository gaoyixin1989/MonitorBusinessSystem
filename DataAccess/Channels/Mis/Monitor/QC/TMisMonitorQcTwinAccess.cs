using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：平行样结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcTwinAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_QC_TWIN " + this.BuildWhereStatement(tMisMonitorQcTwin);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcTwinVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_TWIN  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisMonitorQcTwinVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcTwinVo Details(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_TWIN " + this.BuildWhereStatement(tMisMonitorQcTwin));
            return SqlHelper.ExecuteObject(new TMisMonitorQcTwinVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcTwinVo> SelectByObject(TMisMonitorQcTwinVo tMisMonitorQcTwin, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_TWIN " + this.BuildWhereStatement(tMisMonitorQcTwin));
            return SqlHelper.ExecuteObjectList(tMisMonitorQcTwin, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcTwinVo tMisMonitorQcTwin, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_MONITOR_QC_TWIN {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorQcTwin));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcTwin"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_TWIN " + this.BuildWhereStatement(tMisMonitorQcTwin);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象</param>
        /// <returns></returns>
        public TMisMonitorQcTwinVo SelectByObject(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_TWIN " + this.BuildWhereStatement(tMisMonitorQcTwin);
            return SqlHelper.ExecuteObject(new TMisMonitorQcTwinVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorQcTwin, TMisMonitorQcTwinVo.T_MIS_MONITOR_QC_TWIN_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcTwin">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcTwin, TMisMonitorQcTwinVo.T_MIS_MONITOR_QC_TWIN_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorQcTwin.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcTwin_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorQcTwin_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcTwinVo tMisMonitorQcTwin_UpdateSet, TMisMonitorQcTwinVo tMisMonitorQcTwin_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcTwin_UpdateSet, TMisMonitorQcTwinVo.T_MIS_MONITOR_QC_TWIN_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorQcTwin_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_QC_TWIN where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            string strSQL = "delete from T_MIS_MONITOR_QC_TWIN ";
            strSQL += this.BuildWhereStatement(tMisMonitorQcTwin);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorQcTwin"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorQcTwin)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorQcTwin.ID.ToString()));
                }
                //原始样分析结果 ID
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.RESULT_ID_SRC.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_SRC = '{0}'", tMisMonitorQcTwin.RESULT_ID_SRC.ToString()));
                }
                //平行样分析结果ID1
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.RESULT_ID_TWIN1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_TWIN1 = '{0}'", tMisMonitorQcTwin.RESULT_ID_TWIN1.ToString()));
                }
                //平行样分析结果ID2
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.RESULT_ID_TWIN2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID_TWIN2 = '{0}'", tMisMonitorQcTwin.RESULT_ID_TWIN2.ToString()));
                }
                //平行样测定值1
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.TWIN_RESULT1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_RESULT1 = '{0}'", tMisMonitorQcTwin.TWIN_RESULT1.ToString()));
                }
                //平行样测定值2
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.TWIN_RESULT2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_RESULT2 = '{0}'", tMisMonitorQcTwin.TWIN_RESULT2.ToString()));
                }
                //平行测定均值
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.TWIN_AVG.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_AVG = '{0}'", tMisMonitorQcTwin.TWIN_AVG.ToString()));
                }
                //相对偏差（%）
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.TWIN_OFFSET.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_OFFSET = '{0}'", tMisMonitorQcTwin.TWIN_OFFSET.ToString()));
                }
                //是否合格
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.TWIN_ISOK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_ISOK = '{0}'", tMisMonitorQcTwin.TWIN_ISOK.ToString()));
                }
                //质控类别
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.QC_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND QC_TYPE = '{0}'", tMisMonitorQcTwin.QC_TYPE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorQcTwin.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorQcTwin.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorQcTwin.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorQcTwin.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisMonitorQcTwin.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorQcTwin.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
