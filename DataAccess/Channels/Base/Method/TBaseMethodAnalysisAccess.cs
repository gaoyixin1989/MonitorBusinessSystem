using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Method;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Method
{
    /// <summary>
    /// 功能：分析方法管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseMethodAnalysisAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="strMethodName">方法依据名</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForSelectMethod_inItem(string strMethodName, string strItemId, int iIndex, int iCount)
        {

            string strSQL = " select a.ID,a.ANALYSIS_NAME,m.METHOD_CODE,m.id as MethodID from T_BASE_METHOD_ANALYSIS a ";
            strSQL += " join T_BASE_METHOD_INFO m on m.id=METHOD_ID ";
            strSQL += " where m.IS_DEL='0' and a.IS_DEL='0' and m.MONITOR_ID in (select MONITOR_ID from T_BASE_ITEM_INFO where id='" + strItemId + "')";
            if (strMethodName.Length > 0)
                strSQL += " and m.METHOD_CODE like '%" + strMethodName + "%'";
            strSQL += " order by m.METHOD_CODE desc";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForSelectMethod_inItem(string strMethodName,string strItemId)
        {
            string strSQL = " select a.ID,a.ANALYSIS_NAME,m.METHOD_CODE,m.id as MethodID from T_BASE_METHOD_ANALYSIS a ";
            strSQL += " join T_BASE_METHOD_INFO m on m.id=METHOD_ID ";
            strSQL += " where m.IS_DEL='0' and a.IS_DEL='0' and m.MONITOR_ID in (select MONITOR_ID from T_BASE_ITEM_INFO where id='" + strItemId + "')";
            if (strMethodName.Length > 0)
                strSQL += " and m.METHOD_CODE like '%" + strMethodName + "%'";

            strSQL = "select Count(*) from (" + strSQL + ")t";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            string strSQL = "select Count(*) from T_BASE_METHOD_ANALYSIS " + this.BuildWhereStatement(tBaseMethodAnalysis);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseMethodAnalysisVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_METHOD_ANALYSIS  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseMethodAnalysisVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象条件</param>
        /// <returns>对象</returns>
        public TBaseMethodAnalysisVo Details(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
           string strSQL = String.Format("select * from  T_BASE_METHOD_ANALYSIS " + this.BuildWhereStatement(tBaseMethodAnalysis));
           return SqlHelper.ExecuteObject(new TBaseMethodAnalysisVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseMethodAnalysisVo> SelectByObject(TBaseMethodAnalysisVo tBaseMethodAnalysis, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_METHOD_ANALYSIS " + this.BuildWhereStatement(tBaseMethodAnalysis));
            return SqlHelper.ExecuteObjectList(tBaseMethodAnalysis, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseMethodAnalysisVo tBaseMethodAnalysis, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_METHOD_ANALYSIS {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseMethodAnalysis));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseMethodAnalysis"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            string strSQL = "select * from T_BASE_METHOD_ANALYSIS " + this.BuildWhereStatement(tBaseMethodAnalysis);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象</param>
        /// <returns></returns>
        public TBaseMethodAnalysisVo SelectByObject(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            string strSQL = "select * from T_BASE_METHOD_ANALYSIS " + this.BuildWhereStatement(tBaseMethodAnalysis);
            return SqlHelper.ExecuteObject(new TBaseMethodAnalysisVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseMethodAnalysis, TBaseMethodAnalysisVo.T_BASE_METHOD_ANALYSIS_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMethodAnalysis">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseMethodAnalysis, TBaseMethodAnalysisVo.T_BASE_METHOD_ANALYSIS_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseMethodAnalysis.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMethodAnalysis_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseMethodAnalysis_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMethodAnalysisVo tBaseMethodAnalysis_UpdateSet, TBaseMethodAnalysisVo tBaseMethodAnalysis_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseMethodAnalysis_UpdateSet, TBaseMethodAnalysisVo.T_BASE_METHOD_ANALYSIS_TABLE);
            strSQL += this.BuildWhereStatement(tBaseMethodAnalysis_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_METHOD_ANALYSIS where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            string strSQL = "delete from T_BASE_METHOD_ANALYSIS ";
	    strSQL += this.BuildWhereStatement(tBaseMethodAnalysis);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseMethodAnalysis"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseMethodAnalysis)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseMethodAnalysis.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseMethodAnalysis.ID.ToString()));
				}	
				//分析方法名称
				if (!String.IsNullOrEmpty(tBaseMethodAnalysis.ANALYSIS_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ANALYSIS_NAME = '{0}'", tBaseMethodAnalysis.ANALYSIS_NAME.ToString()));
				}	
				//分析方法描述
				if (!String.IsNullOrEmpty(tBaseMethodAnalysis.DESCRIPTION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DESCRIPTION = '{0}'", tBaseMethodAnalysis.DESCRIPTION.ToString()));
				}	
				//方法依据ID
				if (!String.IsNullOrEmpty(tBaseMethodAnalysis.METHOD_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND METHOD_ID = '{0}'", tBaseMethodAnalysis.METHOD_ID.ToString()));
				}	
				//0为在使用、1为停用
                if (!String.IsNullOrEmpty(tBaseMethodAnalysis.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseMethodAnalysis.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseMethodAnalysis.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseMethodAnalysis.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseMethodAnalysis.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseMethodAnalysis.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseMethodAnalysis.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseMethodAnalysis.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseMethodAnalysis.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseMethodAnalysis.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseMethodAnalysis.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseMethodAnalysis.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
