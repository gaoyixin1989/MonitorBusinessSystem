using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Evaluation;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Evaluation
{
    /// <summary>
    /// 功能：评价标准条件项
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseEvaluationConInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            string strSQL = "select Count(*) from T_BASE_EVALUATION_CON_INFO " + this.BuildWhereStatement(tBaseEvaluationConInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseEvaluationConInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_EVALUATION_CON_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseEvaluationConInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseEvaluationConInfoVo Details(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
           string strSQL = String.Format("select * from  T_BASE_EVALUATION_CON_INFO " + this.BuildWhereStatement(tBaseEvaluationConInfo));
           return SqlHelper.ExecuteObject(new TBaseEvaluationConInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseEvaluationConInfoVo> SelectByObject(TBaseEvaluationConInfoVo tBaseEvaluationConInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_EVALUATION_CON_INFO " + this.BuildWhereStatement(tBaseEvaluationConInfo));
            return SqlHelper.ExecuteObjectList(tBaseEvaluationConInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseEvaluationConInfoVo tBaseEvaluationConInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_EVALUATION_CON_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseEvaluationConInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseEvaluationConInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            string strSQL = "select * from T_BASE_EVALUATION_CON_INFO " + this.BuildWhereStatement(tBaseEvaluationConInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象</param>
        /// <returns></returns>
        public TBaseEvaluationConInfoVo SelectByObject(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            string strSQL = "select * from T_BASE_EVALUATION_CON_INFO " + this.BuildWhereStatement(tBaseEvaluationConInfo);
            return SqlHelper.ExecuteObject(new TBaseEvaluationConInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseEvaluationConInfo, TBaseEvaluationConInfoVo.T_BASE_EVALUATION_CON_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseEvaluationConInfo, TBaseEvaluationConInfoVo.T_BASE_EVALUATION_CON_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseEvaluationConInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationConInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseEvaluationConInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationConInfoVo tBaseEvaluationConInfo_UpdateSet, TBaseEvaluationConInfoVo tBaseEvaluationConInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseEvaluationConInfo_UpdateSet, TBaseEvaluationConInfoVo.T_BASE_EVALUATION_CON_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tBaseEvaluationConInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_EVALUATION_CON_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            string strSQL = "delete from T_BASE_EVALUATION_CON_INFO ";
	    strSQL += this.BuildWhereStatement(tBaseEvaluationConInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 排序（Sort） 批量更新数据库 Castle（胡方扬 2012-11-05）
        /// </summary>
        /// <param name="strValue">数据</param>
        /// <returns></returns>
        public bool UpdateSortByTransaction(string strValue)
        {
            ArrayList arrVo = new ArrayList();
            string[] values = strValue.Split(',');
            foreach (string valueTemp in values)
            {
                string strOrderBy = valueTemp.Split('|')[0];
                string strId = valueTemp.Split('|')[1];
                string strParentId = valueTemp.Split('|')[2];
                string strsql = "UPDATE T_BASE_EVALUATION_CON_INFO SET  PARENT_ID='" + strParentId + "' WHERE ID = '" + strId + "'";
                arrVo.Add(strsql);
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseEvaluationConInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseEvaluationConInfo)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseEvaluationConInfo.ID.ToString()));
				}	
				//评价标准ID
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.STANDARD_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND STANDARD_ID = '{0}'", tBaseEvaluationConInfo.STANDARD_ID.ToString()));
				}	
				//条件项编号
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.CONDITION_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_CODE = '{0}'", tBaseEvaluationConInfo.CONDITION_CODE.ToString()));
				}	
				//父节点ID，如果为根节点，则父节点为“0”
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.PARENT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PARENT_ID = '{0}'", tBaseEvaluationConInfo.PARENT_ID.ToString()));
				}	
				//条件项名称
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.CONDITION_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_NAME = '{0}'", tBaseEvaluationConInfo.CONDITION_NAME.ToString()));
				}	
				//条件项说明
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.CONDITION_REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_REMARK = '{0}'", tBaseEvaluationConInfo.CONDITION_REMARK.ToString()));
				}	
				//0为在使用、1为停用
                if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseEvaluationConInfo.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseEvaluationConInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseEvaluationConInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseEvaluationConInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseEvaluationConInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseEvaluationConInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseEvaluationConInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
