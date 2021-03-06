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
    /// 功能：消息接收人
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoManAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            string strSQL = "select Count(*) from T_OA_MESSAGE_INFO_MAN " + this.BuildWhereStatement(tOaMessageInfoMan);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageInfoManVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_MAN  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaMessageInfoManVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageInfoManVo Details(TOaMessageInfoManVo tOaMessageInfoMan)
        {
           string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_MAN " + this.BuildWhereStatement(tOaMessageInfoMan));
           return SqlHelper.ExecuteObject(new TOaMessageInfoManVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageInfoManVo> SelectByObject(TOaMessageInfoManVo tOaMessageInfoMan, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_MAN " + this.BuildWhereStatement(tOaMessageInfoMan));
            return SqlHelper.ExecuteObjectList(tOaMessageInfoMan, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageInfoManVo tOaMessageInfoMan, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_MESSAGE_INFO_MAN {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaMessageInfoMan));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageInfoMan"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            string strSQL = "select * from T_OA_MESSAGE_INFO_MAN " + this.BuildWhereStatement(tOaMessageInfoMan);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象</param>
        /// <returns></returns>
        public TOaMessageInfoManVo SelectByObject(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            string strSQL = "select * from T_OA_MESSAGE_INFO_MAN " + this.BuildWhereStatement(tOaMessageInfoMan);
            return SqlHelper.ExecuteObject(new TOaMessageInfoManVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaMessageInfoMan, TOaMessageInfoManVo.T_OA_MESSAGE_INFO_MAN_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoMan">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageInfoMan, TOaMessageInfoManVo.T_OA_MESSAGE_INFO_MAN_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaMessageInfoMan.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoMan_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageInfoMan_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoManVo tOaMessageInfoMan_UpdateSet, TOaMessageInfoManVo tOaMessageInfoMan_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageInfoMan_UpdateSet, TOaMessageInfoManVo.T_OA_MESSAGE_INFO_MAN_TABLE);
            strSQL += this.BuildWhereStatement(tOaMessageInfoMan_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_MESSAGE_INFO_MAN where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            string strSQL = "delete from T_OA_MESSAGE_INFO_MAN ";
	    strSQL += this.BuildWhereStatement(tOaMessageInfoMan);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaMessageInfoMan"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaMessageInfoMan)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tOaMessageInfoMan.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaMessageInfoMan.ID.ToString()));
				}	
				//消息编号
				if (!String.IsNullOrEmpty(tOaMessageInfoMan.MESSAGE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MESSAGE_ID = '{0}'", tOaMessageInfoMan.MESSAGE_ID.ToString()));
				}	
				//消息接收人
				if (!String.IsNullOrEmpty(tOaMessageInfoMan.RECEIVER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND RECEIVER_ID = '{0}'", tOaMessageInfoMan.RECEIVER_ID.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaMessageInfoMan.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaMessageInfoMan.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaMessageInfoMan.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaMessageInfoMan.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaMessageInfoMan.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaMessageInfoMan.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaMessageInfoMan.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaMessageInfoMan.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaMessageInfoMan.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaMessageInfoMan.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
