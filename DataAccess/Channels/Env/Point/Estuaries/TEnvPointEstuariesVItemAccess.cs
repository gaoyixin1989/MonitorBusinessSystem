using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Point.Estuaries;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Point.Estuaries
{
    /// <summary>
    /// 功能：入海河口监测点监测项目表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointEstuariesVItemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            string strSQL = "select Count(*) from  T_ENV_P_ESTUARIES_V_ITEM " + this.BuildWhereStatement(tEnvPointEstuariesVItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVItemVo Details(string id)
        {
            string strSQL = String.Format("select * from   T_ENV_P_ESTUARIES_V_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPointEstuariesVItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVItemVo Details(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            string strSQL = String.Format("select * from   T_ENV_P_ESTUARIES_V_ITEM " + this.BuildWhereStatement(tEnvPointEstuariesVItem));
           return SqlHelper.ExecuteObject(new TEnvPointEstuariesVItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointEstuariesVItemVo> SelectByObject(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from   T_ENV_P_ESTUARIES_V_ITEM " + this.BuildWhereStatement(tEnvPointEstuariesVItem));
            return SqlHelper.ExecuteObjectList(tEnvPointEstuariesVItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem, int iIndex, int iCount)
        {
            string strSQL = " select * from  T_ENV_P_ESTUARIES_V_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPointEstuariesVItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            string strSQL = "select * from  T_ENV_P_ESTUARIES_V_ITEM " + this.BuildWhereStatement(tEnvPointEstuariesVItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象</param>
        /// <returns></returns>
        public TEnvPointEstuariesVItemVo SelectByObject(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            string strSQL = "select * from  T_ENV_P_ESTUARIES_V_ITEM " + this.BuildWhereStatement(tEnvPointEstuariesVItem);
            return SqlHelper.ExecuteObject(new TEnvPointEstuariesVItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPointEstuariesVItem, TEnvPointEstuariesVItemVo.T_ENV_POINT_ESTUARIES_V_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointEstuariesVItem, TEnvPointEstuariesVItemVo.T_ENV_POINT_ESTUARIES_V_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPointEstuariesVItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvPointEstuariesVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem_UpdateSet, TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointEstuariesVItem_UpdateSet, TEnvPointEstuariesVItemVo.T_ENV_POINT_ESTUARIES_V_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPointEstuariesVItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from  T_ENV_P_ESTUARIES_V_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            string strSQL = "delete from  T_ENV_P_ESTUARIES_V_ITEM ";
	    strSQL += this.BuildWhereStatement(tEnvPointEstuariesVItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointEstuariesVItem)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointEstuariesVItem.ID.ToString()));
				}	
				//垂线ID
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.POINT_ID.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPointEstuariesVItem.POINT_ID.ToString()));
				}	
				//监测项目ID
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.ITEM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPointEstuariesVItem.ITEM_ID.ToString()));
				}	
				//已选条件项ID
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.CONDITION_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPointEstuariesVItem.CONDITION_ID.ToString()));
				}	
				//条件项类型（1，国标；2，行标；3，地标）
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.CONDITION_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_TYPE = '{0}'", tEnvPointEstuariesVItem.CONDITION_TYPE.ToString()));
				}	
				//国标上限
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.ST_UPPER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ST_UPPER = '{0}'", tEnvPointEstuariesVItem.ST_UPPER.ToString()));
				}	
				//国标下限
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.ST_LOWER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ST_LOWER = '{0}'", tEnvPointEstuariesVItem.ST_LOWER.ToString()));
				}
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPointEstuariesVItem.IS_DEL.ToString()));
                }	
				//备注1
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointEstuariesVItem.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointEstuariesVItem.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointEstuariesVItem.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointEstuariesVItem.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tEnvPointEstuariesVItem.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointEstuariesVItem.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
