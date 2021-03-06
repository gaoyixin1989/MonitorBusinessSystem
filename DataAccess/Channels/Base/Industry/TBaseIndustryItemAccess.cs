using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Industry;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Industry
{
    /// <summary>
    /// 功能：行业项目管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseIndustryItemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseIndustryItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseIndustryItemVo tBaseIndustryItem)
        {
            string strSQL = "select Count(*) from T_BASE_INDUSTRY_ITEM " + this.BuildWhereStatement(tBaseIndustryItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseIndustryItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_INDUSTRY_ITEM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseIndustryItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseIndustryItem">对象条件</param>
        /// <returns>对象</returns>
        public TBaseIndustryItemVo Details(TBaseIndustryItemVo tBaseIndustryItem)
        {
           string strSQL = String.Format("select * from  T_BASE_INDUSTRY_ITEM " + this.BuildWhereStatement(tBaseIndustryItem));
           return SqlHelper.ExecuteObject(new TBaseIndustryItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseIndustryItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseIndustryItemVo> SelectByObject(TBaseIndustryItemVo tBaseIndustryItem, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_INDUSTRY_ITEM " + this.BuildWhereStatement(tBaseIndustryItem));
            return SqlHelper.ExecuteObjectList(tBaseIndustryItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseIndustryItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseIndustryItemVo tBaseIndustryItem, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_INDUSTRY_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseIndustryItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseIndustryItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseIndustryItemVo tBaseIndustryItem)
        {
            string strSQL = "select * from T_BASE_INDUSTRY_ITEM " + this.BuildWhereStatement(tBaseIndustryItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseIndustryItem">对象</param>
        /// <returns></returns>
        public TBaseIndustryItemVo SelectByObject(TBaseIndustryItemVo tBaseIndustryItem)
        {
            string strSQL = "select * from T_BASE_INDUSTRY_ITEM " + this.BuildWhereStatement(tBaseIndustryItem);
            return SqlHelper.ExecuteObject(new TBaseIndustryItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseIndustryItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseIndustryItemVo tBaseIndustryItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseIndustryItem, TBaseIndustryItemVo.T_BASE_INDUSTRY_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseIndustryItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseIndustryItemVo tBaseIndustryItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseIndustryItem, TBaseIndustryItemVo.T_BASE_INDUSTRY_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseIndustryItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseIndustryItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseIndustryItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseIndustryItemVo tBaseIndustryItem_UpdateSet, TBaseIndustryItemVo tBaseIndustryItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseIndustryItem_UpdateSet, TBaseIndustryItemVo.T_BASE_INDUSTRY_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tBaseIndustryItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_INDUSTRY_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseIndustryItemVo tBaseIndustryItem)
        {
            string strSQL = "delete from T_BASE_INDUSTRY_ITEM ";
	    strSQL += this.BuildWhereStatement(tBaseIndustryItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseIndustryItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseIndustryItemVo tBaseIndustryItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseIndustryItem)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseIndustryItem.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseIndustryItem.ID.ToString()));
				}	
				//行业ID
				if (!String.IsNullOrEmpty(tBaseIndustryItem.INDUSTRY_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INDUSTRY_ID = '{0}'", tBaseIndustryItem.INDUSTRY_ID.ToString()));
				}	
				//监测项目ID
				if (!String.IsNullOrEmpty(tBaseIndustryItem.ITEM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tBaseIndustryItem.ITEM_ID.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseIndustryItem.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseIndustryItem.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseIndustryItem.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseIndustryItem.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseIndustryItem.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseIndustryItem.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseIndustryItem.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseIndustryItem.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseIndustryItem.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseIndustryItem.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
