using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.Message;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.Message
{
    /// <summary>
    /// 功能：消息接收部门
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoDeptAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            string strSQL = "select Count(*) from T_OA_MESSAGE_INFO_DEPT " + this.BuildWhereStatement(tOaMessageInfoDept);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageInfoDeptVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_DEPT  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaMessageInfoDeptVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageInfoDeptVo Details(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
           string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_DEPT " + this.BuildWhereStatement(tOaMessageInfoDept));
           return SqlHelper.ExecuteObject(new TOaMessageInfoDeptVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageInfoDeptVo> SelectByObject(TOaMessageInfoDeptVo tOaMessageInfoDept, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_DEPT " + this.BuildWhereStatement(tOaMessageInfoDept));
            return SqlHelper.ExecuteObjectList(tOaMessageInfoDept, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageInfoDeptVo tOaMessageInfoDept, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_MESSAGE_INFO_DEPT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaMessageInfoDept));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageInfoDept"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            string strSQL = "select * from T_OA_MESSAGE_INFO_DEPT " + this.BuildWhereStatement(tOaMessageInfoDept);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象</param>
        /// <returns></returns>
        public TOaMessageInfoDeptVo SelectByObject(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            string strSQL = "select * from T_OA_MESSAGE_INFO_DEPT " + this.BuildWhereStatement(tOaMessageInfoDept);
            return SqlHelper.ExecuteObject(new TOaMessageInfoDeptVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaMessageInfoDept, TOaMessageInfoDeptVo.T_OA_MESSAGE_INFO_DEPT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoDept">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageInfoDept, TOaMessageInfoDeptVo.T_OA_MESSAGE_INFO_DEPT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaMessageInfoDept.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoDept_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageInfoDept_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoDeptVo tOaMessageInfoDept_UpdateSet, TOaMessageInfoDeptVo tOaMessageInfoDept_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageInfoDept_UpdateSet, TOaMessageInfoDeptVo.T_OA_MESSAGE_INFO_DEPT_TABLE);
            strSQL += this.BuildWhereStatement(tOaMessageInfoDept_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_MESSAGE_INFO_DEPT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            string strSQL = "delete from T_OA_MESSAGE_INFO_DEPT ";
	    strSQL += this.BuildWhereStatement(tOaMessageInfoDept);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaMessageInfoDept"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaMessageInfoDept)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tOaMessageInfoDept.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaMessageInfoDept.ID.ToString()));
				}	
				//消息编号
				if (!String.IsNullOrEmpty(tOaMessageInfoDept.MESSAGE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MESSAGE_ID = '{0}'", tOaMessageInfoDept.MESSAGE_ID.ToString()));
				}	
				//部门ID
				if (!String.IsNullOrEmpty(tOaMessageInfoDept.DEPT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DEPT_ID = '{0}'", tOaMessageInfoDept.DEPT_ID.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaMessageInfoDept.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaMessageInfoDept.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaMessageInfoDept.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaMessageInfoDept.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaMessageInfoDept.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaMessageInfoDept.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaMessageInfoDept.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaMessageInfoDept.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaMessageInfoDept.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaMessageInfoDept.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
