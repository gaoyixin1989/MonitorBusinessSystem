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
    /// 功能：属性类别表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttributeTypeAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseAttributeTypeVo tBaseAttributeType)
        {
            string strSQL = "select Count(*) from T_BASE_ATTRIBUTE_TYPE " + this.BuildWhereStatement(tBaseAttributeType);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseAttributeTypeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_ATTRIBUTE_TYPE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseAttributeTypeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseAttributeType">对象条件</param>
        /// <returns>对象</returns>
        public TBaseAttributeTypeVo Details(TBaseAttributeTypeVo tBaseAttributeType)
        {
           string strSQL = String.Format("select * from  T_BASE_ATTRIBUTE_TYPE " + this.BuildWhereStatement(tBaseAttributeType));
           return SqlHelper.ExecuteObject(new TBaseAttributeTypeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseAttributeTypeVo> SelectByObject(TBaseAttributeTypeVo tBaseAttributeType, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_ATTRIBUTE_TYPE " + this.BuildWhereStatement(tBaseAttributeType));
            return SqlHelper.ExecuteObjectList(tBaseAttributeType, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseAttributeTypeVo tBaseAttributeType, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_ATTRIBUTE_TYPE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseAttributeType));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseAttributeType"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseAttributeTypeVo tBaseAttributeType)
        {
            string strSQL = "select * from T_BASE_ATTRIBUTE_TYPE " + this.BuildWhereStatement(tBaseAttributeType);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <returns></returns>
        public TBaseAttributeTypeVo SelectByObject(TBaseAttributeTypeVo tBaseAttributeType)
        {
            string strSQL = "select * from T_BASE_ATTRIBUTE_TYPE " + this.BuildWhereStatement(tBaseAttributeType);
            return SqlHelper.ExecuteObject(new TBaseAttributeTypeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseAttributeTypeVo tBaseAttributeType)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseAttributeType, TBaseAttributeTypeVo.T_BASE_ATTRIBUTE_TYPE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeType">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeTypeVo tBaseAttributeType)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseAttributeType, TBaseAttributeTypeVo.T_BASE_ATTRIBUTE_TYPE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseAttributeType.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeType_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseAttributeType_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeTypeVo tBaseAttributeType_UpdateSet, TBaseAttributeTypeVo tBaseAttributeType_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseAttributeType_UpdateSet, TBaseAttributeTypeVo.T_BASE_ATTRIBUTE_TYPE_TABLE);
            strSQL += this.BuildWhereStatement(tBaseAttributeType_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_ATTRIBUTE_TYPE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseAttributeTypeVo tBaseAttributeType)
        {
            string strSQL = "delete from T_BASE_ATTRIBUTE_TYPE ";
	    strSQL += this.BuildWhereStatement(tBaseAttributeType);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseAttributeTypeVo tBaseAttributeType, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT * FROM T_BASE_ATTRIBUTE_TYPE WHERE IS_DEL='{0}'", tBaseAttributeType.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseAttributeType.SORT_NAME))
            {
                strSQL += String.Format("  AND SORT_NAME LIKE '%{0}%'", tBaseAttributeType.SORT_NAME);
            }
            if (!String.IsNullOrEmpty(tBaseAttributeType.MONITOR_ID))
            {
                strSQL += String.Format("  AND MONITOR_ID ='{0}'", tBaseAttributeType.MONITOR_ID);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseAttributeTypeVo tBaseAttributeType)
        {

            string strSQL = String.Format("SELECT * FROM T_BASE_ATTRIBUTE_TYPE WHERE IS_DEL='{0}'", tBaseAttributeType.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseAttributeType.SORT_NAME))
            {
                strSQL += String.Format("  AND SORT_NAME LIKE '%{0}%'", tBaseAttributeType.SORT_NAME);
            }
            if (!String.IsNullOrEmpty(tBaseAttributeType.MONITOR_ID))
            {
                strSQL += String.Format("  AND MONITOR_ID ='{0}'", tBaseAttributeType.MONITOR_ID);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseAttributeType"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseAttributeTypeVo tBaseAttributeType)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseAttributeType)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseAttributeType.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseAttributeType.ID.ToString()));
				}	
				//监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
				if (!String.IsNullOrEmpty(tBaseAttributeType.MONITOR_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tBaseAttributeType.MONITOR_ID.ToString()));
				}	
				//类别名称
				if (!String.IsNullOrEmpty(tBaseAttributeType.SORT_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SORT_NAME = '{0}'", tBaseAttributeType.SORT_NAME.ToString()));
				}	
				//添加人
				if (!String.IsNullOrEmpty(tBaseAttributeType.INSERT_BY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INSERT_BY = '{0}'", tBaseAttributeType.INSERT_BY.ToString()));
				}	
				//添加时间
				if (!String.IsNullOrEmpty(tBaseAttributeType.INSERT_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INSERT_DATE = '{0}'", tBaseAttributeType.INSERT_DATE.ToString()));
				}	
				//使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tBaseAttributeType.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseAttributeType.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseAttributeType.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseAttributeType.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseAttributeType.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseAttributeType.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseAttributeType.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseAttributeType.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseAttributeType.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseAttributeType.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseAttributeType.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseAttributeType.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
