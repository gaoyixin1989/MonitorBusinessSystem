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
    /// 功能：属性信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改日期：2012-11-12
    /// 修改人：潘德军
    /// 增加SelectByTableByJoin函数
    /// </summary>
    public class TBaseAttributeInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            string strSQL = "select Count(*) from T_BASE_ATTRIBUTE_INFO " + this.BuildWhereStatement(tBaseAttributeInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseAttributeInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_ATTRIBUTE_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseAttributeInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseAttributeInfoVo Details(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
           string strSQL = String.Format("select * from  T_BASE_ATTRIBUTE_INFO " + this.BuildWhereStatement(tBaseAttributeInfo));
           return SqlHelper.ExecuteObject(new TBaseAttributeInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseAttributeInfoVo> SelectByObject(TBaseAttributeInfoVo tBaseAttributeInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_ATTRIBUTE_INFO " + this.BuildWhereStatement(tBaseAttributeInfo));
            return SqlHelper.ExecuteObjectList(tBaseAttributeInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseAttributeInfoVo tBaseAttributeInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_ATTRIBUTE_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseAttributeInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="strOUTLETPOINT_TYPE">排口点位类别</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableByJoin()
        {

            string strSQL = " select tv.ATTRIBUTE_TYPE_ID,i.* from T_BASE_ATTRIBUTE_INFO i";
            strSQL += " join T_BASE_ATTRIBUTE_TYPE_VALUE tv on tv.ATTRIBUTE_ID=i.ID";
            strSQL += " where i.IS_DEL='0' and tv.IS_DEL='0'";
            strSQL += " order by cast(tv.SN as int)";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="strAttTypeID">属性类别ID</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableByJoin(string strAttTypeID)
        {

            string strSQL = " select tv.ATTRIBUTE_TYPE_ID,i.* from T_BASE_ATTRIBUTE_INFO i";
            strSQL += " join T_BASE_ATTRIBUTE_TYPE_VALUE tv on tv.ATTRIBUTE_ID=i.ID";
            strSQL += " where i.IS_DEL='0' and tv.IS_DEL='0'";
            if (strAttTypeID.Length > 0)
                strSQL += " and tv.ATTRIBUTE_TYPE_ID = '" + strAttTypeID + "'";
            strSQL += " order by cast(tv.SN as int)";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseAttributeInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            string strSQL = "select * from T_BASE_ATTRIBUTE_INFO " + this.BuildWhereStatement(tBaseAttributeInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <returns></returns>
        public TBaseAttributeInfoVo SelectByObject(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            string strSQL = "select * from T_BASE_ATTRIBUTE_INFO " + this.BuildWhereStatement(tBaseAttributeInfo);
            return SqlHelper.ExecuteObject(new TBaseAttributeInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseAttributeInfo, TBaseAttributeInfoVo.T_BASE_ATTRIBUTE_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseAttributeInfo, TBaseAttributeInfoVo.T_BASE_ATTRIBUTE_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseAttributeInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseAttributeInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeInfoVo tBaseAttributeInfo_UpdateSet, TBaseAttributeInfoVo tBaseAttributeInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseAttributeInfo_UpdateSet, TBaseAttributeInfoVo.T_BASE_ATTRIBUTE_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tBaseAttributeInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_ATTRIBUTE_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            string strSQL = "delete from T_BASE_ATTRIBUTE_INFO ";
	    strSQL += this.BuildWhereStatement(tBaseAttributeInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseAttributeInfoVo tBaseAttributeInfo, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT * FROM T_BASE_ATTRIBUTE_INFO WHERE IS_DEL='{0}'", tBaseAttributeInfo.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseAttributeInfo.ATTRIBUTE_NAME))
            {
                strSQL += String.Format("  AND ATTRIBUTE_NAME LIKE '%{0}%'", tBaseAttributeInfo.ATTRIBUTE_NAME);
            }
            if (!String.IsNullOrEmpty(tBaseAttributeInfo.CONTROL_NAME))
            {
                strSQL += String.Format("  AND CONTROL_NAME ='{0}'", tBaseAttributeInfo.CONTROL_NAME);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseAttributeInfoVo tBaseAttributeInfo)
        {

            string strSQL = String.Format("SELECT * FROM T_BASE_ATTRIBUTE_INFO WHERE IS_DEL='{0}'", tBaseAttributeInfo.IS_DEL);
            if (!String.IsNullOrEmpty(tBaseAttributeInfo.ATTRIBUTE_NAME))
            {
                strSQL += String.Format("  AND ATTRIBUTE_NAME LIKE '%{0}%'", tBaseAttributeInfo.ATTRIBUTE_NAME);
            }
            if (!String.IsNullOrEmpty(tBaseAttributeInfo.CONTROL_NAME))
            {
                strSQL += String.Format("  AND CONTROL_NAME ='{0}'", tBaseAttributeInfo.CONTROL_NAME);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion
        /// <summary>
        /// 获取动态属性数据 Create By weilin 2014-03-19
        /// </summary>
        /// <param name="strType_ID"></param>
        /// <returns></returns>
        public DataTable GetAttDate(string strType_ID)
        {
            string strSQL = String.Format(@"select i.ID,i.ATTRIBUTE_NAME,i.CONTROL_ID,i.CONTROL_NAME,i.DICTIONARY from T_BASE_ATTRIBUTE_INFO i
                                            join T_BASE_ATTRIBUTE_TYPE_VALUE tv on tv.ATTRIBUTE_ID=i.ID
                                            where i.IS_DEL='0' and tv.IS_DEL='0' and tv.Attribute_type_id in({0}) order by tv.ATTRIBUTE_TYPE_ID desc", strType_ID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 获取动态属性数据值 Create By weilin 2014-03-19
        /// </summary>
        /// <param name="strType_ID"></param>
        /// <param name="strPoint_ID"></param>
        /// <returns></returns>
        public DataTable GetAttValue(string strType_ID, string strPoint_ID)
        {
            string strSQL = String.Format(@"select i.ID,i.ATTRIBUTE_NAME,i.CONTROL_ID,i.CONTROL_NAME,i.DICTIONARY,av.ATTRBUTE_VALUE,
                                            case ISNULL(i.DICTIONARY,'') when '' then av.ATTRBUTE_VALUE else (select DICT_TEXT from T_SYS_DICT where DICT_TYPE=i.DICTIONARY and av.ATTRBUTE_VALUE=DICT_CODE) end 'ATTRBUTE_TEXT'
                                            from T_BASE_ATTRIBUTE_INFO i
                                            join T_BASE_ATTRIBUTE_TYPE_VALUE tv on tv.ATTRIBUTE_ID=i.ID
                                            left join T_BASE_ATTRBUTE_VALUE3 av on(i.ID=av.ATTRBUTE_CODE and av.OBJECT_ID='{0}' and av.IS_DEL='0')
                                            where i.IS_DEL='0' and tv.IS_DEL='0'  and tv.Attribute_type_id in({1})", strPoint_ID, strType_ID);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseAttributeInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseAttributeInfo)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseAttributeInfo.ID.ToString()));
				}	
				//属性名称
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.ATTRIBUTE_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATTRIBUTE_NAME = '{0}'", tBaseAttributeInfo.ATTRIBUTE_NAME.ToString()));
				}	
				//控件ID
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.CONTROL_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTROL_ID = '{0}'", tBaseAttributeInfo.CONTROL_ID.ToString()));
				}	
				//控件名称
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.CONTROL_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONTROL_NAME = '{0}'", tBaseAttributeInfo.CONTROL_NAME.ToString()));
				}	
				//控件宽度
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.WIDTH.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WIDTH = '{0}'", tBaseAttributeInfo.WIDTH.ToString()));
				}	
				//是否可编辑
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.ENABLE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ENABLE = '{0}'", tBaseAttributeInfo.ENABLE.ToString()));
				}	
				//可否为空
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.IS_NULL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_NULL = '{0}'", tBaseAttributeInfo.IS_NULL.ToString()));
				}	
				//最大长度
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.MAX_LENGTH.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MAX_LENGTH = '{0}'", tBaseAttributeInfo.MAX_LENGTH.ToString()));
				}	
				//字典项
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.DICTIONARY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DICTIONARY = '{0}'", tBaseAttributeInfo.DICTIONARY.ToString()));
				}	
				//数据库表名
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.TABLE_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TABLE_NAME = '{0}'", tBaseAttributeInfo.TABLE_NAME.ToString()));
				}	
				//文本字段
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.TEXT_FIELD.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TEXT_FIELD = '{0}'", tBaseAttributeInfo.TEXT_FIELD.ToString()));
				}	
				//值字段
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.VALUE_FIELD.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND VALUE_FIELD = '{0}'", tBaseAttributeInfo.VALUE_FIELD.ToString()));
				}	
				//排序
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.SN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SN = '{0}'", tBaseAttributeInfo.SN.ToString()));
				}	
				//使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tBaseAttributeInfo.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseAttributeInfo.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseAttributeInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseAttributeInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseAttributeInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseAttributeInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseAttributeInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseAttributeInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
