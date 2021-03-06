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
    /// 功能：评价标准条件项项目信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseEvaluationConItemAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            string strSQL = "select Count(*) from T_BASE_EVALUATION_CON_ITEM " + this.BuildWhereStatement(tBaseEvaluationConItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseEvaluationConItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_EVALUATION_CON_ITEM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseEvaluationConItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象条件</param>
        /// <returns>对象</returns>
        public TBaseEvaluationConItemVo Details(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
           string strSQL = String.Format("select * from  T_BASE_EVALUATION_CON_ITEM " + this.BuildWhereStatement(tBaseEvaluationConItem));
           return SqlHelper.ExecuteObject(new TBaseEvaluationConItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseEvaluationConItemVo> SelectByObject(TBaseEvaluationConItemVo tBaseEvaluationConItem, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_EVALUATION_CON_ITEM " + this.BuildWhereStatement(tBaseEvaluationConItem));
            return SqlHelper.ExecuteObjectList(tBaseEvaluationConItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseEvaluationConItemVo tBaseEvaluationConItem, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_EVALUATION_CON_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseEvaluationConItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseEvaluationConItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            string strSQL = "select * from T_BASE_EVALUATION_CON_ITEM " + this.BuildWhereStatement(tBaseEvaluationConItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象</param>
        /// <returns></returns>
        public TBaseEvaluationConItemVo SelectByObject(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            string strSQL = "select * from T_BASE_EVALUATION_CON_ITEM " + this.BuildWhereStatement(tBaseEvaluationConItem);
            return SqlHelper.ExecuteObject(new TBaseEvaluationConItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseEvaluationConItem, TBaseEvaluationConItemVo.T_BASE_EVALUATION_CON_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationConItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseEvaluationConItem, TBaseEvaluationConItemVo.T_BASE_EVALUATION_CON_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseEvaluationConItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationConItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseEvaluationConItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationConItemVo tBaseEvaluationConItem_UpdateSet, TBaseEvaluationConItemVo tBaseEvaluationConItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseEvaluationConItem_UpdateSet, TBaseEvaluationConItemVo.T_BASE_EVALUATION_CON_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tBaseEvaluationConItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_EVALUATION_CON_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            string strSQL = "delete from T_BASE_EVALUATION_CON_ITEM ";
	    strSQL += this.BuildWhereStatement(tBaseEvaluationConItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 创建原因：获取评价标准监测项目
        /// 创建人:胡方扬
        /// 创建时间:2013-07-18
        /// </summary>
        /// <param name="tBaseEvaluationConItem"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable GetEvaluItemDataDatable(TBaseEvaluationConItemVo tBaseEvaluationConItem,int iIndex,int iCount) {
            string strSQL = @"SELECT A.*,
       B.ITEM_NAME,
       C.MONITOR_TYPE_NAME,
       D.DICT_TEXT         AS UPPEROP,
       E.DICT_TEXT         AS LOWOP,
       F.DICT_TEXT         AS UNITNAME
  FROM T_BASE_EVALUATION_CON_ITEM A
  LEFT JOIN dbo.T_BASE_ITEM_INFO B
    ON B.ID = A.ITEM_ID
  LEFT JOIN dbo.T_BASE_MONITOR_TYPE_INFO C
    ON C.ID = A.MONITOR_ID
  LEFT JOIN dbo.T_SYS_DICT D
    ON D.DICT_CODE = A.UPPER_OPERATOR
   AND D.DICT_TYPE = 'logic_operator'
  LEFT JOIN dbo.T_SYS_DICT E
    ON E.DICT_CODE = A.LOWER_OPERATOR
   AND E.DICT_TYPE = 'logic_operator'
  LEFT JOIN dbo.T_SYS_DICT F
    ON F.DICT_CODE = A.UNIT
   AND F.DICT_TYPE = 'item_unit'
 Where 1 = 1";
            if (!String.IsNullOrEmpty(tBaseEvaluationConItem.IS_DEL)) {
                strSQL += String.Format(" AND A.IS_DEL='{0}'", tBaseEvaluationConItem.IS_DEL);
            }

            if (!String.IsNullOrEmpty(tBaseEvaluationConItem.CONDITION_ID))
            {
                strSQL += String.Format(" AND A.CONDITION_ID='{0}'", tBaseEvaluationConItem.CONDITION_ID);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }


        /// <summary>
        /// 创建原因：获取评价标准监测项目个数
        /// 创建人:胡方扬
        /// 创建时间:2013-07-18
        /// </summary>
        /// <param name="tBaseEvaluationConItem"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetEvaluItemDataDatableCount(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            string strSQL = @"SELECT A.*,
       B.ITEM_NAME,
       C.MONITOR_TYPE_NAME,
       D.DICT_TEXT         AS UPPEROP,
       E.DICT_TEXT         AS LOWOP,
       F.DICT_TEXT         AS UNITNAME
  FROM T_BASE_EVALUATION_CON_ITEM A
  LEFT JOIN dbo.T_BASE_ITEM_INFO B
    ON B.ID = A.ITEM_ID
  LEFT JOIN dbo.T_BASE_MONITOR_TYPE_INFO C
    ON C.ID = A.MONITOR_ID
  LEFT JOIN dbo.T_SYS_DICT D
    ON D.DICT_CODE = A.UPPER_OPERATOR
   AND D.DICT_TYPE = 'logic_operator'
  LEFT JOIN dbo.T_SYS_DICT E
    ON E.DICT_CODE = A.LOWER_OPERATOR
   AND E.DICT_TYPE = 'logic_operator'
  LEFT JOIN dbo.T_SYS_DICT F
    ON F.DICT_CODE = A.UNIT
   AND F.DICT_TYPE = 'item_unit'
 Where 1 = 1";
            if (!String.IsNullOrEmpty(tBaseEvaluationConItem.IS_DEL))
            {
                strSQL += String.Format(" AND A.IS_DEL='{0}'", tBaseEvaluationConItem.IS_DEL);
            }

            if (!String.IsNullOrEmpty(tBaseEvaluationConItem.CONDITION_ID))
            {
                strSQL += String.Format(" AND A.CONDITION_ID='{0}'", tBaseEvaluationConItem.CONDITION_ID);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseEvaluationConItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseEvaluationConItem)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseEvaluationConItem.ID.ToString()));
				}	
				//评价标准ID
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.STANDARD_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND STANDARD_ID = '{0}'", tBaseEvaluationConItem.STANDARD_ID.ToString()));
				}	
				//条件项ID
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.CONDITION_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tBaseEvaluationConItem.CONDITION_ID.ToString()));
				}
                //监测类型ID
                if (!String.IsNullOrEmpty(tBaseEvaluationConItem.MONITOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tBaseEvaluationConItem.MONITOR_ID.ToString()));
                }

                //监测值类型ID
                if (!String.IsNullOrEmpty(tBaseEvaluationConItem.MONITOR_VALUE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_VALUE_ID = '{0}'", tBaseEvaluationConItem.MONITOR_ID.ToString()));
                }	
				//监测项目ID
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.ITEM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tBaseEvaluationConItem.ITEM_ID.ToString()));
				}	
				//上限运算符
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.UPPER_OPERATOR.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UPPER_OPERATOR = '{0}'", tBaseEvaluationConItem.UPPER_OPERATOR.ToString()));
				}	
				//下限运算符
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.LOWER_OPERATOR.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LOWER_OPERATOR = '{0}'", tBaseEvaluationConItem.LOWER_OPERATOR.ToString()));
				}	
				//排放上限
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.DISCHARGE_UPPER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DISCHARGE_UPPER = '{0}'", tBaseEvaluationConItem.DISCHARGE_UPPER.ToString()));
				}	
				//排放下限
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.DISCHARGE_LOWER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DISCHARGE_LOWER = '{0}'", tBaseEvaluationConItem.DISCHARGE_LOWER.ToString()));
				}	
				//单位
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.UNIT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UNIT = '{0}'", tBaseEvaluationConItem.UNIT.ToString()));
				}	
				//0为在使用、1为停用
                if (!String.IsNullOrEmpty(tBaseEvaluationConItem.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseEvaluationConItem.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseEvaluationConItem.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseEvaluationConItem.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseEvaluationConItem.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseEvaluationConItem.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseEvaluationConItem.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseEvaluationConItem.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
