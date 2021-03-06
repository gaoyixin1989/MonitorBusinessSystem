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
    /// 功能：短消息信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageInfoVo tOaMessageInfo)
        {
            string strSQL = "select Count(*) from T_OA_MESSAGE_INFO " + this.BuildWhereStatement(tOaMessageInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaMessageInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageInfoVo Details(TOaMessageInfoVo tOaMessageInfo)
        {
           string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO " + this.BuildWhereStatement(tOaMessageInfo));
           return SqlHelper.ExecuteObject(new TOaMessageInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageInfoVo> SelectByObject(TOaMessageInfoVo tOaMessageInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO " + this.BuildWhereStatement(tOaMessageInfo));
            return SqlHelper.ExecuteObjectList(tOaMessageInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageInfoVo tOaMessageInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_MESSAGE_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaMessageInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
         

        /// <summary>
        /// 通过用户ID\部门ID获取对象DataTable
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strDeptCode">部门ID</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public int GetSelectByUserIdAndDeptCount(string strUserID, string strDeptCode)
        {
            string strSQL = " select Count(*) from T_OA_MESSAGE_INFO where ACCEPT_USERIDS like '%" + strUserID + "%' ";
            for (int i = 0; i < strDeptCode.Split(',').Length; i++)
            {
                strSQL += " or ACCEPT_DEPTIDS like '%" + strDeptCode.Split(',')[i] + "%' ";
            }
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 通过用户ID\部门ID获取对象DataTable
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strDeptCode">部门ID</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByUserIdAndDept(string strUserID, int iIndex, int iCount)
        {
            //begin yinchengyi 2014-10-22 清远开启短信功能
//            string strSQL = @"select t1.ID,t1.MESSAGE_TITLE,t1.MESSAGE_CONTENT, t1.SEND_BY, t1.SEND_DATE, t1.SEND_TIME, t1.SEND_TYPE, t1.ACCEPT_TYPE, t1.ACCEPT_USERIDS, t1.ACCEPT_REALNAMES, 
//                                t1.ACCEPT_DEPTIDS, t1.ACCEPT_DEPTNAMES, t1.TASK_ID, t2.IS_READ as REMARK1, t1.REMARK2, t1.REMARK3 
//                                from T_OA_MESSAGE_INFO AS t1
//                                INNER JOIN T_OA_MESSAGE_INFO_RECEIVE T2  ON t1.ID = t2.MESSAGE_ID and t2.IS_READ='0' and t2.RECEIVER = '{0}'
//                                where ACCEPT_USERIDS like '%{0}%' ";

            string strSQL = @"select t1.ID,t1.MESSAGE_TITLE,t1.MESSAGE_CONTENT, t1.SEND_BY, t1.SEND_DATE, t1.SEND_TIME, t1.SEND_TYPE, t1.ACCEPT_TYPE, t1.ACCEPT_USERIDS, t1.ACCEPT_REALNAMES, 
                                t1.ACCEPT_DEPTIDS, t1.ACCEPT_DEPTNAMES, t1.TASK_ID,t2.RECEIVER , t2.IS_READ as REMARK1, t1.REMARK2, t1.REMARK3 
                                from T_OA_MESSAGE_INFO AS t1
                                JOIN T_OA_MESSAGE_INFO_RECEIVE T2  ON t1.ID = t2.MESSAGE_ID
                                where t2.RECEIVER = '{0}' ";

            strSQL = string.Format(strSQL, strUserID);

            // by yinchengyi 不必选择部门。因为在发送信息时，如果选择了按部门发送部门，则T_OA_MESSAGE_INFO_RECEIVE表中会按照部门人数插入等量的记录
            //for (int i = 0; i < strDeptCode.Split(',').Length; i++)
            //{
            //    strSQL += " or ACCEPT_DEPTIDS like '%" + strDeptCode.Split(',')[i] + "%' ";
            //}

            // end yinchengyi
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageInfoVo tOaMessageInfo)
        {
            string strSQL = "select * from T_OA_MESSAGE_INFO " + this.BuildWhereStatement(tOaMessageInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageInfo">对象</param>
        /// <returns></returns>
        public TOaMessageInfoVo SelectByObject(TOaMessageInfoVo tOaMessageInfo)
        {
            string strSQL = "select * from T_OA_MESSAGE_INFO " + this.BuildWhereStatement(tOaMessageInfo);
            return SqlHelper.ExecuteObject(new TOaMessageInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaMessageInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageInfoVo tOaMessageInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaMessageInfo, TOaMessageInfoVo.T_OA_MESSAGE_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoVo tOaMessageInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageInfo, TOaMessageInfoVo.T_OA_MESSAGE_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaMessageInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoVo tOaMessageInfo_UpdateSet, TOaMessageInfoVo tOaMessageInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageInfo_UpdateSet, TOaMessageInfoVo.T_OA_MESSAGE_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tOaMessageInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_MESSAGE_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaMessageInfoVo tOaMessageInfo)
        {
            string strSQL = "delete from T_OA_MESSAGE_INFO ";
	    strSQL += this.BuildWhereStatement(tOaMessageInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaMessageInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaMessageInfoVo tOaMessageInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaMessageInfo)
            {
			    	
				//消息编号
				if (!String.IsNullOrEmpty(tOaMessageInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaMessageInfo.ID.ToString()));
				}	
				//消息标题
				if (!String.IsNullOrEmpty(tOaMessageInfo.MESSAGE_TITLE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MESSAGE_TITLE = '{0}'", tOaMessageInfo.MESSAGE_TITLE.ToString()));
				}	
				//短消息内容
				if (!String.IsNullOrEmpty(tOaMessageInfo.MESSAGE_CONTENT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MESSAGE_CONTENT = '{0}'", tOaMessageInfo.MESSAGE_CONTENT.ToString()));
				}	
				//消息发送人
				if (!String.IsNullOrEmpty(tOaMessageInfo.SEND_BY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SEND_BY = '{0}'", tOaMessageInfo.SEND_BY.ToString()));
				}	
				//消息发送时间
				if (!String.IsNullOrEmpty(tOaMessageInfo.SEND_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SEND_DATE = '{0}'", tOaMessageInfo.SEND_DATE.ToString()));
				}	
				//发送时分（时分秒）
				if (!String.IsNullOrEmpty(tOaMessageInfo.SEND_TIME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SEND_TIME = '{0}'", tOaMessageInfo.SEND_TIME.ToString()));
				}	
				//消息发送方式(4：定期，3：立即)，暂不支持周期循环发送，客户处暂未发现类似需求
				if (!String.IsNullOrEmpty(tOaMessageInfo.SEND_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SEND_TYPE = '{0}'", tOaMessageInfo.SEND_TYPE.ToString()));
				}	
				//接收类别(1：全站；2：按人)
				if (!String.IsNullOrEmpty(tOaMessageInfo.ACCEPT_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ACCEPT_TYPE = '{0}'", tOaMessageInfo.ACCEPT_TYPE.ToString()));
				}	
				//所有接收人id以中文逗号串联，仅为查询方便
				if (!String.IsNullOrEmpty(tOaMessageInfo.ACCEPT_USERIDS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ACCEPT_USERIDS = '{0}'", tOaMessageInfo.ACCEPT_USERIDS.ToString()));
				}	
				//所有接收人REALNAME以中文逗号串联，仅为查询方便
				if (!String.IsNullOrEmpty(tOaMessageInfo.ACCEPT_REALNAMES.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ACCEPT_REALNAMES = '{0}'", tOaMessageInfo.ACCEPT_REALNAMES.ToString()));
				}	
				//所有接收部门id以中文逗号串联，仅为查询方便
				if (!String.IsNullOrEmpty(tOaMessageInfo.ACCEPT_DEPTIDS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ACCEPT_DEPTIDS = '{0}'", tOaMessageInfo.ACCEPT_DEPTIDS.ToString()));
				}	
				//所有接收部门NAME以中文逗号串联串联，仅为查询方便
				if (!String.IsNullOrEmpty(tOaMessageInfo.ACCEPT_DEPTNAMES.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ACCEPT_DEPTNAMES = '{0}'", tOaMessageInfo.ACCEPT_DEPTNAMES.ToString()));
				}	
				//工作流任务id，将消息和任务关联，方便任务办理后消除消息，方便直接点消息办理任务
				if (!String.IsNullOrEmpty(tOaMessageInfo.TASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TASK_ID = '{0}'", tOaMessageInfo.TASK_ID.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaMessageInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaMessageInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaMessageInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaMessageInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaMessageInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaMessageInfo.REMARK3.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
