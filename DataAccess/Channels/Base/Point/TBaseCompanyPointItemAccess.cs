using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Point;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Point
{
    /// <summary>
    /// 功能：监测点项目明细表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseCompanyPointItemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            string strSQL = "select Count(*) from T_BASE_COMPANY_POINT_ITEM " + this.BuildWhereStatement(tBaseCompanyPointItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseCompanyPointItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_COMPANY_POINT_ITEM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseCompanyPointItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象条件</param>
        /// <returns>对象</returns>
        public TBaseCompanyPointItemVo Details(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
           string strSQL = String.Format("select * from  T_BASE_COMPANY_POINT_ITEM " + this.BuildWhereStatement(tBaseCompanyPointItem));
           return SqlHelper.ExecuteObject(new TBaseCompanyPointItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseCompanyPointItemVo> SelectByObject(TBaseCompanyPointItemVo tBaseCompanyPointItem, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_COMPANY_POINT_ITEM " + this.BuildWhereStatement(tBaseCompanyPointItem));
            return SqlHelper.ExecuteObjectList(tBaseCompanyPointItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseCompanyPointItemVo tBaseCompanyPointItem, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_COMPANY_POINT_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseCompanyPointItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseCompanyPointItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            string strSQL = "select * from T_BASE_COMPANY_POINT_ITEM " + this.BuildWhereStatement(tBaseCompanyPointItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象</param>
        /// <returns></returns>
        public TBaseCompanyPointItemVo SelectByObject(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            string strSQL = "select * from T_BASE_COMPANY_POINT_ITEM " + this.BuildWhereStatement(tBaseCompanyPointItem);
            return SqlHelper.ExecuteObject(new TBaseCompanyPointItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseCompanyPointItem, TBaseCompanyPointItemVo.T_BASE_COMPANY_POINT_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyPointItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseCompanyPointItem, TBaseCompanyPointItemVo.T_BASE_COMPANY_POINT_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseCompanyPointItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyPointItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseCompanyPointItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyPointItemVo tBaseCompanyPointItem_UpdateSet, TBaseCompanyPointItemVo tBaseCompanyPointItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseCompanyPointItem_UpdateSet, TBaseCompanyPointItemVo.T_BASE_COMPANY_POINT_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tBaseCompanyPointItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_COMPANY_POINT_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            string strSQL = "delete from T_BASE_COMPANY_POINT_ITEM ";
	    strSQL += this.BuildWhereStatement(tBaseCompanyPointItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 根据企业点位ID 获取当前点位下的所有监测项目
        /// </summary>
        /// <param name="tBaseCompanyPointItem"></param>
        /// <returns></returns>
        public DataTable SelectItemsForPoint(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            string strSQL = String.Format(@"SELECT A.POINT_ID,B.ID,B.ITEM_NAME,MONITOR_ID FROM T_BASE_COMPANY_POINT_ITEM A INNER JOIN T_BASE_ITEM_INFO B ON B.ID=A.ITEM_ID AND B.IS_DEL='0'");
            if (!String.IsNullOrEmpty(tBaseCompanyPointItem.POINT_ID)) {
                strSQL += String.Format(" AND A.POINT_ID='{0}'", tBaseCompanyPointItem.POINT_ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseCompanyPointItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseCompanyPointItem)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseCompanyPointItem.ID.ToString()));
				}	
				//监测点ID
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.POINT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tBaseCompanyPointItem.POINT_ID.ToString()));
				}	
				//监测项目ID
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.ITEM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tBaseCompanyPointItem.ITEM_ID.ToString()));
				}	
				//已选条件项ID
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.CONDITION_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tBaseCompanyPointItem.CONDITION_ID.ToString()));
				}	
				//条件项类型（1，国标；2，行标；3，地标）
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.CONDITION_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_TYPE = '{0}'", tBaseCompanyPointItem.CONDITION_TYPE.ToString()));
				}	
				//国标上限
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.ST_UPPER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ST_UPPER = '{0}'", tBaseCompanyPointItem.ST_UPPER.ToString()));
				}	
				//国标下限
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.ST_LOWER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ST_LOWER = '{0}'", tBaseCompanyPointItem.ST_LOWER.ToString()));
				}
                //删除标记
                if (!String.IsNullOrEmpty(tBaseCompanyPointItem.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseCompanyPointItem.IS_DEL.ToString()));
                }	
				//备注1
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseCompanyPointItem.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseCompanyPointItem.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseCompanyPointItem.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseCompanyPointItem.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseCompanyPointItem.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseCompanyPointItem.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
