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
    /// 功能：短信息接收
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoReceiveAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            string strSQL = "select Count(*) from T_OA_MESSAGE_INFO_RECEIVE " + this.BuildWhereStatement(tOaMessageInfoReceive);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageInfoReceiveVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_RECEIVE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaMessageInfoReceiveVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageInfoReceiveVo Details(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
           string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_RECEIVE " + this.BuildWhereStatement(tOaMessageInfoReceive));
           return SqlHelper.ExecuteObject(new TOaMessageInfoReceiveVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageInfoReceiveVo> SelectByObject(TOaMessageInfoReceiveVo tOaMessageInfoReceive, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_RECEIVE " + this.BuildWhereStatement(tOaMessageInfoReceive));
            return SqlHelper.ExecuteObjectList(tOaMessageInfoReceive, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageInfoReceiveVo tOaMessageInfoReceive, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_MESSAGE_INFO_RECEIVE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaMessageInfoReceive));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageInfoReceive"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            string strSQL = "select * from T_OA_MESSAGE_INFO_RECEIVE " + this.BuildWhereStatement(tOaMessageInfoReceive);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象</param>
        /// <returns></returns>
        public TOaMessageInfoReceiveVo SelectByObject(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            string strSQL = "select * from T_OA_MESSAGE_INFO_RECEIVE " + this.BuildWhereStatement(tOaMessageInfoReceive);
            return SqlHelper.ExecuteObject(new TOaMessageInfoReceiveVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaMessageInfoReceive, TOaMessageInfoReceiveVo.T_OA_MESSAGE_INFO_RECEIVE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoReceive">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageInfoReceive, TOaMessageInfoReceiveVo.T_OA_MESSAGE_INFO_RECEIVE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaMessageInfoReceive.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoReceive_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageInfoReceive_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoReceiveVo tOaMessageInfoReceive_UpdateSet, TOaMessageInfoReceiveVo tOaMessageInfoReceive_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageInfoReceive_UpdateSet, TOaMessageInfoReceiveVo.T_OA_MESSAGE_INFO_RECEIVE_TABLE);
            strSQL += this.BuildWhereStatement(tOaMessageInfoReceive_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_MESSAGE_INFO_RECEIVE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            string strSQL = "delete from T_OA_MESSAGE_INFO_RECEIVE ";
	    strSQL += this.BuildWhereStatement(tOaMessageInfoReceive);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaMessageInfoReceive"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaMessageInfoReceive)
            {
			    	
				//消息阅读编号
				if (!String.IsNullOrEmpty(tOaMessageInfoReceive.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaMessageInfoReceive.ID.ToString()));
				}	
				//消息编号
				if (!String.IsNullOrEmpty(tOaMessageInfoReceive.MESSAGE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MESSAGE_ID = '{0}'", tOaMessageInfoReceive.MESSAGE_ID.ToString()));
				}	
				//消息接收人
				if (!String.IsNullOrEmpty(tOaMessageInfoReceive.RECEIVER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND RECEIVER = '{0}'", tOaMessageInfoReceive.RECEIVER.ToString()));
				}	
				//是否已阅读
				if (!String.IsNullOrEmpty(tOaMessageInfoReceive.IS_READ.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_READ = '{0}'", tOaMessageInfoReceive.IS_READ.ToString()));
				}	
				//消息查阅时间
				if (!String.IsNullOrEmpty(tOaMessageInfoReceive.READ_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND READ_DATE = '{0}'", tOaMessageInfoReceive.READ_DATE.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaMessageInfoReceive.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaMessageInfoReceive.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaMessageInfoReceive.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaMessageInfoReceive.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaMessageInfoReceive.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaMessageInfoReceive.REMARK3.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
