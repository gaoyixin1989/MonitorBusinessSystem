using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.SW;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.SW
{
    /// <summary>
    /// 功能：收文阅办
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSwHandleAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSwHandle">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaSwHandleVo tOaSwHandle)
        {
            string strSQL = "select Count(*) from T_OA_SW_HANDLE " + this.BuildWhereStatement(tOaSwHandle);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaSwHandleVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_SW_HANDLE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaSwHandleVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaSwHandle">对象条件</param>
        /// <returns>对象</returns>
        public TOaSwHandleVo Details(TOaSwHandleVo tOaSwHandle)
        {
           string strSQL = String.Format("select * from  T_OA_SW_HANDLE " + this.BuildWhereStatement(tOaSwHandle));
           return SqlHelper.ExecuteObject(new TOaSwHandleVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaSwHandle">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaSwHandleVo> SelectByObject(TOaSwHandleVo tOaSwHandle, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_SW_HANDLE " + this.BuildWhereStatement(tOaSwHandle));
            return SqlHelper.ExecuteObjectList(tOaSwHandle, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaSwHandle">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaSwHandleVo tOaSwHandle, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_SW_HANDLE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaSwHandle));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSwHandle"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSwHandleVo tOaSwHandle)
        {
            string strSQL = "select * from T_OA_SW_HANDLE " + this.BuildWhereStatement(tOaSwHandle);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaSwHandle">对象</param>
        /// <returns></returns>
        public TOaSwHandleVo SelectByObject(TOaSwHandleVo tOaSwHandle)
        {
            string strSQL = "select * from T_OA_SW_HANDLE " + this.BuildWhereStatement(tOaSwHandle);
            return SqlHelper.ExecuteObject(new TOaSwHandleVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaSwHandle">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaSwHandleVo tOaSwHandle)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaSwHandle, TOaSwHandleVo.T_OA_SW_HANDLE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwHandle">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwHandleVo tOaSwHandle)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaSwHandle, TOaSwHandleVo.T_OA_SW_HANDLE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaSwHandle.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwHandle_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaSwHandle_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwHandleVo tOaSwHandle_UpdateSet, TOaSwHandleVo tOaSwHandle_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaSwHandle_UpdateSet, TOaSwHandleVo.T_OA_SW_HANDLE_TABLE);
            strSQL += this.BuildWhereStatement(tOaSwHandle_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_SW_HANDLE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaSwHandleVo tOaSwHandle)
        {
            string strSQL = "delete from T_OA_SW_HANDLE ";
	    strSQL += this.BuildWhereStatement(tOaSwHandle);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaSwHandle"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaSwHandleVo tOaSwHandle)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaSwHandle)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tOaSwHandle.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaSwHandle.ID.ToString()));
				}	
				//收文ID
				if (!String.IsNullOrEmpty(tOaSwHandle.SW_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SW_ID = '{0}'", tOaSwHandle.SW_ID.ToString()));
				}	
				//拟办人ID
				if (!String.IsNullOrEmpty(tOaSwHandle.SW_PLAN_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SW_PLAN_ID = '{0}'", tOaSwHandle.SW_PLAN_ID.ToString()));
				}	
				//拟办日期
				if (!String.IsNullOrEmpty(tOaSwHandle.SW_PLAN_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SW_PLAN_DATE = '{0}'", tOaSwHandle.SW_PLAN_DATE.ToString()));
				}	
				//批办意见
				if (!String.IsNullOrEmpty(tOaSwHandle.SW_PLAN_APP_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SW_PLAN_APP_INFO = '{0}'", tOaSwHandle.SW_PLAN_APP_INFO.ToString()));
				}	
				//是否已办
				if (!String.IsNullOrEmpty(tOaSwHandle.IS_OK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_OK = '{0}'", tOaSwHandle.IS_OK.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaSwHandle.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaSwHandle.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaSwHandle.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaSwHandle.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaSwHandle.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaSwHandle.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaSwHandle.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaSwHandle.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaSwHandle.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaSwHandle.REMARK5.ToString()));
				}
                //处理人标志
                if (!String.IsNullOrEmpty(tOaSwHandle.SW_HANDER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SW_HANDER = '{0}'", tOaSwHandle.SW_HANDER.ToString()));
                }
                //发送人ID
                if (!String.IsNullOrEmpty(tOaSwHandle.STR_USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STR_USERID = '{0}'", tOaSwHandle.STR_USERID.ToString()));
                }
                //发送时间
                if (!String.IsNullOrEmpty(tOaSwHandle.STR_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STR_DATE = '{0}'", tOaSwHandle.STR_DATE.ToString()));
                }
			}
			return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSwHandle"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSwHandleVo tOaSwHandle, bool b)
        {
            string strSQL = "select b.REAL_NAME UserName,a.SW_PLAN_DATE PlanDate,a.SW_PLAN_APP_INFO Suggion from T_OA_SW_HANDLE a left join T_SYS_USER b on(a.SW_PLAN_ID=b.ID)" + this.BuildWhereStatement(tOaSwHandle);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }
}
