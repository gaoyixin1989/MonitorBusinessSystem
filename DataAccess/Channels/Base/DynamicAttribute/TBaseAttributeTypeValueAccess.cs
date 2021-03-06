using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.DynamicAttribute
{
    /// <summary>
    /// 功能：属性类别配置表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttributeTypeValueAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            string strSQL = "select Count(*) from T_BASE_ATTRIBUTE_TYPE_VALUE " + this.BuildWhereStatement(tBaseAttributeTypeValue);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseAttributeTypeValueVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_ATTRIBUTE_TYPE_VALUE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseAttributeTypeValueVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象条件</param>
        /// <returns>对象</returns>
        public TBaseAttributeTypeValueVo Details(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
           string strSQL = String.Format("select * from  T_BASE_ATTRIBUTE_TYPE_VALUE " + this.BuildWhereStatement(tBaseAttributeTypeValue));
           return SqlHelper.ExecuteObject(new TBaseAttributeTypeValueVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseAttributeTypeValueVo> SelectByObject(TBaseAttributeTypeValueVo tBaseAttributeTypeValue, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_ATTRIBUTE_TYPE_VALUE " + this.BuildWhereStatement(tBaseAttributeTypeValue));
            return SqlHelper.ExecuteObjectList(tBaseAttributeTypeValue, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseAttributeTypeValueVo tBaseAttributeTypeValue, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_ATTRIBUTE_TYPE_VALUE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseAttributeTypeValue));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseAttributeTypeValue"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            string strSQL = "select * from T_BASE_ATTRIBUTE_TYPE_VALUE " + this.BuildWhereStatement(tBaseAttributeTypeValue);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <returns></returns>
        public TBaseAttributeTypeValueVo SelectByObject(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            string strSQL = "select * from T_BASE_ATTRIBUTE_TYPE_VALUE " + this.BuildWhereStatement(tBaseAttributeTypeValue);
            return SqlHelper.ExecuteObject(new TBaseAttributeTypeValueVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseAttributeTypeValue, TBaseAttributeTypeValueVo.T_BASE_ATTRIBUTE_TYPE_VALUE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseAttributeTypeValue, TBaseAttributeTypeValueVo.T_BASE_ATTRIBUTE_TYPE_VALUE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseAttributeTypeValue.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeTypeValue_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseAttributeTypeValue_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeTypeValueVo tBaseAttributeTypeValue_UpdateSet, TBaseAttributeTypeValueVo tBaseAttributeTypeValue_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseAttributeTypeValue_UpdateSet, TBaseAttributeTypeValueVo.T_BASE_ATTRIBUTE_TYPE_VALUE_TABLE);
            strSQL += this.BuildWhereStatement(tBaseAttributeTypeValue_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_ATTRIBUTE_TYPE_VALUE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            string strSQL = "delete from T_BASE_ATTRIBUTE_TYPE_VALUE ";
	    strSQL += this.BuildWhereStatement(tBaseAttributeTypeValue);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseAttributeTypeValueVo tBaseAttributeTypeValue, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT * FROM T_BASE_ATTRIBUTE_TYPE_VALUE WHERE IS_DEL='{0}'", tBaseAttributeTypeValue.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.ITEM_TYPE))
            {
                strSQL += String.Format("  AND ITEM_TYPE  ='{0}'", tBaseAttributeTypeValue.ITEM_TYPE);
            }
            if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.ATTRIBUTE_ID))
            {
                strSQL += String.Format("  AND ATTRIBUTE_ID ='{0}'", tBaseAttributeTypeValue.ATTRIBUTE_ID);
            }
            if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.ATTRIBUTE_TYPE_ID))
            {
                strSQL += String.Format("  AND ATTRIBUTE_TYPE_ID ='{0}'", tBaseAttributeTypeValue.ATTRIBUTE_TYPE_ID);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {

            string strSQL = String.Format("SELECT * FROM T_BASE_ATTRIBUTE_TYPE_VALUE WHERE IS_DEL='{0}'", tBaseAttributeTypeValue.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.ITEM_TYPE))
            {
                strSQL += String.Format("  AND ITEM_TYPE  ='{0}'", tBaseAttributeTypeValue.ITEM_TYPE);
            }
            if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.ATTRIBUTE_ID))
            {
                strSQL += String.Format("  AND ATTRIBUTE_ID ='{0}'", tBaseAttributeTypeValue.ATTRIBUTE_ID);
            }
            if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.ATTRIBUTE_TYPE_ID))
            {
                strSQL += String.Format("  AND ATTRIBUTE_TYPE_ID ='{0}'", tBaseAttributeTypeValue.ATTRIBUTE_TYPE_ID);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseAttributeTypeValue"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseAttributeTypeValue)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseAttributeTypeValue.ID.ToString()));
				}	
				//监测类别
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.ITEM_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_TYPE = '{0}'", tBaseAttributeTypeValue.ITEM_TYPE.ToString()));
				}	
				//排口点位类别
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.OUTLETPOINT_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND OUTLETPOINT_TYPE = '{0}'", tBaseAttributeTypeValue.OUTLETPOINT_TYPE.ToString()));
				}	
				//属性类别
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.ATTRIBUTE_TYPE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATTRIBUTE_TYPE_ID = '{0}'", tBaseAttributeTypeValue.ATTRIBUTE_TYPE_ID.ToString()));
				}	
				//属性
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.ATTRIBUTE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATTRIBUTE_ID = '{0}'", tBaseAttributeTypeValue.ATTRIBUTE_ID.ToString()));
				}	
				//排序
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.SN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SN = '{0}'", tBaseAttributeTypeValue.SN.ToString()));
				}	
				//使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseAttributeTypeValue.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseAttributeTypeValue.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseAttributeTypeValue.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseAttributeTypeValue.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseAttributeTypeValue.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseAttributeTypeValue.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseAttributeTypeValue.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
