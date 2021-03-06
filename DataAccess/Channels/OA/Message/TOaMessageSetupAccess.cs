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
    /// 功能：个人短消息接收设置表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageSetupAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageSetup">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageSetupVo tOaMessageSetup)
        {
            string strSQL = "select Count(*) from T_OA_MESSAGE_SETUP " + this.BuildWhereStatement(tOaMessageSetup);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageSetupVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_MESSAGE_SETUP  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaMessageSetupVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageSetup">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageSetupVo Details(TOaMessageSetupVo tOaMessageSetup)
        {
           string strSQL = String.Format("select * from  T_OA_MESSAGE_SETUP " + this.BuildWhereStatement(tOaMessageSetup));
           return SqlHelper.ExecuteObject(new TOaMessageSetupVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageSetup">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageSetupVo> SelectByObject(TOaMessageSetupVo tOaMessageSetup, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_MESSAGE_SETUP " + this.BuildWhereStatement(tOaMessageSetup));
            return SqlHelper.ExecuteObjectList(tOaMessageSetup, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageSetup">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageSetupVo tOaMessageSetup, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_MESSAGE_SETUP {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaMessageSetup));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageSetup"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageSetupVo tOaMessageSetup)
        {
            string strSQL = "select * from T_OA_MESSAGE_SETUP " + this.BuildWhereStatement(tOaMessageSetup);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageSetup">对象</param>
        /// <returns></returns>
        public TOaMessageSetupVo SelectByObject(TOaMessageSetupVo tOaMessageSetup)
        {
            string strSQL = "select * from T_OA_MESSAGE_SETUP " + this.BuildWhereStatement(tOaMessageSetup);
            return SqlHelper.ExecuteObject(new TOaMessageSetupVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaMessageSetup">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageSetupVo tOaMessageSetup)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaMessageSetup, TOaMessageSetupVo.T_OA_MESSAGE_SETUP_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageSetup">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageSetupVo tOaMessageSetup)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageSetup, TOaMessageSetupVo.T_OA_MESSAGE_SETUP_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaMessageSetup.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageSetup_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageSetup_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageSetupVo tOaMessageSetup_UpdateSet, TOaMessageSetupVo tOaMessageSetup_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageSetup_UpdateSet, TOaMessageSetupVo.T_OA_MESSAGE_SETUP_TABLE);
            strSQL += this.BuildWhereStatement(tOaMessageSetup_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_MESSAGE_SETUP where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaMessageSetupVo tOaMessageSetup)
        {
            string strSQL = "delete from T_OA_MESSAGE_SETUP ";
	    strSQL += this.BuildWhereStatement(tOaMessageSetup);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaMessageSetup"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaMessageSetupVo tOaMessageSetup)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaMessageSetup)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tOaMessageSetup.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaMessageSetup.ID.ToString()));
				}	
				//
				if (!String.IsNullOrEmpty(tOaMessageSetup.IF_RE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IF_RE = '{0}'", tOaMessageSetup.IF_RE.ToString()));
				}	
				//提醒方式，1弹出窗口，2短信，3图标闪烁（3可以暂时不实现）
				if (!String.IsNullOrEmpty(tOaMessageSetup.UDS_REMINDTYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UDS_REMINDTYPE = '{0}'", tOaMessageSetup.UDS_REMINDTYPE.ToString()));
				}	
				//提醒时间，即刷新间隔
				if (!String.IsNullOrEmpty(tOaMessageSetup.UDS_REFRESHTIME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UDS_REFRESHTIME = '{0}'", tOaMessageSetup.UDS_REFRESHTIME.ToString()));
				}	
				//用户ID
				if (!String.IsNullOrEmpty(tOaMessageSetup.USER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND USER_ID = '{0}'", tOaMessageSetup.USER_ID.ToString()));
				}	
				//备份1
				if (!String.IsNullOrEmpty(tOaMessageSetup.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaMessageSetup.REMARK1.ToString()));
				}	
				//备份2
				if (!String.IsNullOrEmpty(tOaMessageSetup.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaMessageSetup.REMARK2.ToString()));
				}	
				//备份3
				if (!String.IsNullOrEmpty(tOaMessageSetup.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaMessageSetup.REMARK3.ToString()));
				}	
				//备份4
				if (!String.IsNullOrEmpty(tOaMessageSetup.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaMessageSetup.REMARK4.ToString()));
				}	
				//备份5
				if (!String.IsNullOrEmpty(tOaMessageSetup.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaMessageSetup.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
