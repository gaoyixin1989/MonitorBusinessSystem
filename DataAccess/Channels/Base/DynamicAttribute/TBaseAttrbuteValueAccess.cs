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
    /// 功能：属性值表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttrbuteValueAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            string strSQL = "select Count(*) from T_BASE_ATTRBUTE_VALUE " + this.BuildWhereStatement(tBaseAttrbuteValue);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseAttrbuteValueVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_ATTRBUTE_VALUE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseAttrbuteValueVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象条件</param>
        /// <returns>对象</returns>
        public TBaseAttrbuteValueVo Details(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
           string strSQL = String.Format("select * from  T_BASE_ATTRBUTE_VALUE " + this.BuildWhereStatement(tBaseAttrbuteValue));
           return SqlHelper.ExecuteObject(new TBaseAttrbuteValueVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseAttrbuteValueVo> SelectByObject(TBaseAttrbuteValueVo tBaseAttrbuteValue, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_ATTRBUTE_VALUE " + this.BuildWhereStatement(tBaseAttrbuteValue));
            return SqlHelper.ExecuteObjectList(tBaseAttrbuteValue, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseAttrbuteValueVo tBaseAttrbuteValue, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_ATTRBUTE_VALUE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseAttrbuteValue));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseAttrbuteValue"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            string strSQL = "select * from T_BASE_ATTRBUTE_VALUE " + this.BuildWhereStatement(tBaseAttrbuteValue);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象</param>
        /// <returns></returns>
        public TBaseAttrbuteValueVo SelectByObject(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            string strSQL = "select * from T_BASE_ATTRBUTE_VALUE " + this.BuildWhereStatement(tBaseAttrbuteValue);
            return SqlHelper.ExecuteObject(new TBaseAttrbuteValueVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseAttrbuteValue, TBaseAttrbuteValueVo.T_BASE_ATTRBUTE_VALUE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttrbuteValue">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseAttrbuteValue, TBaseAttrbuteValueVo.T_BASE_ATTRBUTE_VALUE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseAttrbuteValue.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttrbuteValue_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseAttrbuteValue_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttrbuteValueVo tBaseAttrbuteValue_UpdateSet, TBaseAttrbuteValueVo tBaseAttrbuteValue_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseAttrbuteValue_UpdateSet, TBaseAttrbuteValueVo.T_BASE_ATTRBUTE_VALUE_TABLE);
            strSQL += this.BuildWhereStatement(tBaseAttrbuteValue_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_ATTRBUTE_VALUE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            string strSQL = "delete from T_BASE_ATTRBUTE_VALUE ";
	    strSQL += this.BuildWhereStatement(tBaseAttrbuteValue);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseAttrbuteValue"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseAttrbuteValue)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseAttrbuteValue.ID.ToString()));
				}	
				//对象类型
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.OBJECT_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND OBJECT_TYPE = '{0}'", tBaseAttrbuteValue.OBJECT_TYPE.ToString()));
				}	
				//对象ID
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.OBJECT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND OBJECT_ID = '{0}'", tBaseAttrbuteValue.OBJECT_ID.ToString()));
				}	
				//属性名称
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.ATTRBUTE_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATTRBUTE_CODE = '{0}'", tBaseAttrbuteValue.ATTRBUTE_CODE.ToString()));
				}	
				//属性值
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.ATTRBUTE_VALUE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATTRBUTE_VALUE = '{0}'", tBaseAttrbuteValue.ATTRBUTE_VALUE.ToString()));
				}	
				//使用状态(0为启用、1为停用)
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseAttrbuteValue.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseAttrbuteValue.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseAttrbuteValue.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseAttrbuteValue.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseAttrbuteValue.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseAttrbuteValue.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseAttrbuteValue.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
