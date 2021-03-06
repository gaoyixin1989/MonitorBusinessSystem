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
    /// 功能：实验室空白批次表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcEmptyBatAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_QC_EMPTY_BAT " + this.BuildWhereStatement(tMisMonitorQcEmptyBat);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcEmptyBatVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_EMPTY_BAT  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisMonitorQcEmptyBatVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcEmptyBatVo Details(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
           string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_EMPTY_BAT " + this.BuildWhereStatement(tMisMonitorQcEmptyBat));
           return SqlHelper.ExecuteObject(new TMisMonitorQcEmptyBatVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcEmptyBatVo> SelectByObject(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_MONITOR_QC_EMPTY_BAT " + this.BuildWhereStatement(tMisMonitorQcEmptyBat));
            return SqlHelper.ExecuteObjectList(tMisMonitorQcEmptyBat, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_MONITOR_QC_EMPTY_BAT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorQcEmptyBat));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_EMPTY_BAT " + this.BuildWhereStatement(tMisMonitorQcEmptyBat);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象</param>
        /// <returns></returns>
        public TMisMonitorQcEmptyBatVo SelectByObject(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            string strSQL = "select * from T_MIS_MONITOR_QC_EMPTY_BAT " + this.BuildWhereStatement(tMisMonitorQcEmptyBat);
            return SqlHelper.ExecuteObject(new TMisMonitorQcEmptyBatVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorQcEmptyBat, TMisMonitorQcEmptyBatVo.T_MIS_MONITOR_QC_EMPTY_BAT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcEmptyBat, TMisMonitorQcEmptyBatVo.T_MIS_MONITOR_QC_EMPTY_BAT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorQcEmptyBat.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorQcEmptyBat_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat_UpdateSet, TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorQcEmptyBat_UpdateSet, TMisMonitorQcEmptyBatVo.T_MIS_MONITOR_QC_EMPTY_BAT_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorQcEmptyBat_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_QC_EMPTY_BAT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            string strSQL = "delete from T_MIS_MONITOR_QC_EMPTY_BAT ";
	    strSQL += this.BuildWhereStatement(tMisMonitorQcEmptyBat);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorQcEmptyBat)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorQcEmptyBat.ID.ToString()));
				}	
				//空白批次
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.QC_EMPTY_IN_NUM.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QC_EMPTY_IN_NUM = '{0}'", tMisMonitorQcEmptyBat.QC_EMPTY_IN_NUM.ToString()));
				}	
				//空白测试日期
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.QC_EMPTY_IN_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QC_EMPTY_IN_DATE = '{0}'", tMisMonitorQcEmptyBat.QC_EMPTY_IN_DATE.ToString()));
				}	
				//实验室空白个数
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.QC_EMPTY_IN_COUNT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QC_EMPTY_IN_COUNT = '{0}'", tMisMonitorQcEmptyBat.QC_EMPTY_IN_COUNT.ToString()));
				}	
				//实验室空白值
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.QC_EMPTY_IN_RESULT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QC_EMPTY_IN_RESULT = '{0}'", tMisMonitorQcEmptyBat.QC_EMPTY_IN_RESULT.ToString()));
				}	
				//相对偏差（%）
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.QC_EMPTY_OFFSET.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QC_EMPTY_OFFSET = '{0}'", tMisMonitorQcEmptyBat.QC_EMPTY_OFFSET.ToString()));
				}	
				//空白是否合格
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.QC_EMPTY_ISOK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QC_EMPTY_ISOK = '{0}'", tMisMonitorQcEmptyBat.QC_EMPTY_ISOK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorQcEmptyBat.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorQcEmptyBat.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorQcEmptyBat.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorQcEmptyBat.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisMonitorQcEmptyBat.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorQcEmptyBat.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
