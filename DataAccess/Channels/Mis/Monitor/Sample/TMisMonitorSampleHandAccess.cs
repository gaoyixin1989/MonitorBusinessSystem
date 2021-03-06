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
    /// 功能：样品交接表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleHandAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            string strSQL = "select Count(*) from T_MIS_MONITOR_SAMPLE_HAND " + this.BuildWhereStatement(tMisMonitorSampleHand);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleHandVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_HAND  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TMisMonitorSampleHandVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleHandVo Details(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
           string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_HAND " + this.BuildWhereStatement(tMisMonitorSampleHand));
           return SqlHelper.ExecuteObject(new TMisMonitorSampleHandVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleHandVo> SelectByObject(TMisMonitorSampleHandVo tMisMonitorSampleHand, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_MIS_MONITOR_SAMPLE_HAND " + this.BuildWhereStatement(tMisMonitorSampleHand));
            return SqlHelper.ExecuteObjectList(tMisMonitorSampleHand, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleHandVo tMisMonitorSampleHand, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_MIS_MONITOR_SAMPLE_HAND {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisMonitorSampleHand));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleHand"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_HAND " + this.BuildWhereStatement(tMisMonitorSampleHand);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleHandVo SelectByObject(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            string strSQL = "select * from T_MIS_MONITOR_SAMPLE_HAND " + this.BuildWhereStatement(tMisMonitorSampleHand);
            return SqlHelper.ExecuteObject(new TMisMonitorSampleHandVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisMonitorSampleHand, TMisMonitorSampleHandVo.T_MIS_MONITOR_SAMPLE_HAND_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleHand">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleHand, TMisMonitorSampleHandVo.T_MIS_MONITOR_SAMPLE_HAND_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisMonitorSampleHand.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleHand_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorSampleHand_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleHandVo tMisMonitorSampleHand_UpdateSet, TMisMonitorSampleHandVo tMisMonitorSampleHand_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisMonitorSampleHand_UpdateSet, TMisMonitorSampleHandVo.T_MIS_MONITOR_SAMPLE_HAND_TABLE);
            strSQL += this.BuildWhereStatement(tMisMonitorSampleHand_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_MONITOR_SAMPLE_HAND where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            string strSQL = "delete from T_MIS_MONITOR_SAMPLE_HAND ";
	    strSQL += this.BuildWhereStatement(tMisMonitorSampleHand);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisMonitorSampleHand"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisMonitorSampleHand)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisMonitorSampleHand.ID.ToString()));
				}	
				//监测子任务ID
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.SUBTASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SUBTASK_ID = '{0}'", tMisMonitorSampleHand.SUBTASK_ID.ToString()));
				}	
				//交接单编号
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.HANDOVER_NO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND HANDOVER_NO = '{0}'", tMisMonitorSampleHand.HANDOVER_NO.ToString()));
				}	
				//编号类型
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.NO_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND NO_TYPE = '{0}'", tMisMonitorSampleHand.NO_TYPE.ToString()));
				}	
				//普通加急
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.IF_COMMON.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IF_COMMON = '{0}'", tMisMonitorSampleHand.IF_COMMON.ToString()));
				}	
				//样品类型
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.SAMPLE_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SAMPLE_TYPE = '{0}'", tMisMonitorSampleHand.SAMPLE_TYPE.ToString()));
				}	
				//单据类型编号
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.TAB_TYPE_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TAB_TYPE_CODE = '{0}'", tMisMonitorSampleHand.TAB_TYPE_CODE.ToString()));
				}	
				//是否移交
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.IS_HANDOVER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_HANDOVER = '{0}'", tMisMonitorSampleHand.IS_HANDOVER.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisMonitorSampleHand.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisMonitorSampleHand.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisMonitorSampleHand.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisMonitorSampleHand.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tMisMonitorSampleHand.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisMonitorSampleHand.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
