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
    /// 功能：标准样结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcStAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_QC_ST " + this.BuildWhereStatement(tMisMonitorQcSt);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcStVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_ST  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisMonitorQcStVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcStVo Details(TMisMonitorQcStVo tMisMonitorQcSt)
        {
           string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_ST " + this.BuildWhereStatement(tMisMonitorQcSt));
           return SqlHelper.ExecuteObject(new TMisMonitorQcStVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcStVo> SelectByObject(TMisMonitorQcStVo tMisMonitorQcSt, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_ST " + this.BuildWhereStatement(tMisMonitorQcSt));
            return SqlHelper.ExecuteObjectList(tMisMonitorQcSt, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcStVo tMisMonitorQcSt, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_MONITOR_QC_ST {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorQcSt));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcSt"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_ST " + this.BuildWhereStatement(tMisMonitorQcSt);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象</param>
        /// <returns></returns>
        public TMisMonitorQcStVo SelectByObject(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_ST " + this.BuildWhereStatement(tMisMonitorQcSt);
            return SqlHelper.ExecuteObject(new TMisMonitorQcStVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorQcSt, TMisMonitorQcStVo.T_MIS_MONITOR_QC_ST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcSt">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcSt, TMisMonitorQcStVo.T_MIS_MONITOR_QC_ST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorQcSt.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcSt_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorQcSt_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcStVo tMisMonitorQcSt_UpdateSet, TMisMonitorQcStVo tMisMonitorQcSt_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcSt_UpdateSet, TMisMonitorQcStVo.T_MIS_MONITOR_QC_ST_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorQcSt_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_QC_ST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            string strSQL = "delete from T_MIS_MONITOR_QC_ST ";
	    strSQL += this.BuildWhereStatement(tMisMonitorQcSt);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorQcSt"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorQcSt)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorQcSt.ID.ToString()));
				}	
				//原始样分析结果 ID
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.RESULT_ID_SRC.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND RESULT_ID_SRC = '{0}'", tMisMonitorQcSt.RESULT_ID_SRC.ToString()));
				}	
				//空白样分析结果 ID
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.RESULT_ID_ST.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND RESULT_ID_ST = '{0}'", tMisMonitorQcSt.RESULT_ID_ST.ToString()));
				}	
				//实验室标准样标准值
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.SRC_RESULT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SRC_RESULT = '{0}'", tMisMonitorQcSt.SRC_RESULT.ToString()));
				}	
				//不确定度
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.UNCERTAINTY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UNCERTAINTY = '{0}'", tMisMonitorQcSt.UNCERTAINTY.ToString()));
				}	
				//实验室标准样结果值
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.ST_RESULT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ST_RESULT = '{0}'", tMisMonitorQcSt.ST_RESULT.ToString()));
				}	
				//空白是否合格
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.ST_ISOK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ST_ISOK = '{0}'", tMisMonitorQcSt.ST_ISOK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorQcSt.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorQcSt.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorQcSt.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorQcSt.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisMonitorQcSt.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorQcSt.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
