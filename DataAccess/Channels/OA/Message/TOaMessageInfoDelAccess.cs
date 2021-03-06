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
    /// 功能：短消息已阅表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoDelAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            string strSQL = "select Count(*) from T_OA_MESSAGE_INFO_DEL " + this.BuildWhereStatement(tOaMessageInfoDel);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageInfoDelVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_DEL  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaMessageInfoDelVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageInfoDelVo Details(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
           string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_DEL " + this.BuildWhereStatement(tOaMessageInfoDel));
           return SqlHelper.ExecuteObject(new TOaMessageInfoDelVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageInfoDelVo> SelectByObject(TOaMessageInfoDelVo tOaMessageInfoDel, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_MESSAGE_INFO_DEL " + this.BuildWhereStatement(tOaMessageInfoDel));
            return SqlHelper.ExecuteObjectList(tOaMessageInfoDel, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageInfoDelVo tOaMessageInfoDel, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_MESSAGE_INFO_DEL {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaMessageInfoDel));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageInfoDel"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            string strSQL = "select * from T_OA_MESSAGE_INFO_DEL " + this.BuildWhereStatement(tOaMessageInfoDel);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象</param>
        /// <returns></returns>
        public TOaMessageInfoDelVo SelectByObject(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            string strSQL = "select * from T_OA_MESSAGE_INFO_DEL " + this.BuildWhereStatement(tOaMessageInfoDel);
            return SqlHelper.ExecuteObject(new TOaMessageInfoDelVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaMessageInfoDel, TOaMessageInfoDelVo.T_OA_MESSAGE_INFO_DEL_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoDel">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageInfoDel, TOaMessageInfoDelVo.T_OA_MESSAGE_INFO_DEL_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaMessageInfoDel.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoDel_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageInfoDel_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoDelVo tOaMessageInfoDel_UpdateSet, TOaMessageInfoDelVo tOaMessageInfoDel_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageInfoDel_UpdateSet, TOaMessageInfoDelVo.T_OA_MESSAGE_INFO_DEL_TABLE);
            strSQL += this.BuildWhereStatement(tOaMessageInfoDel_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_MESSAGE_INFO_DEL where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            string strSQL = "delete from T_OA_MESSAGE_INFO_DEL ";
	    strSQL += this.BuildWhereStatement(tOaMessageInfoDel);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaMessageInfoDel"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaMessageInfoDel)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tOaMessageInfoDel.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaMessageInfoDel.ID.ToString()));
				}	
				//消息表ID、消息接收表ID，具体对哪个id进行清除，根据接收发送类型决定
				if (!String.IsNullOrEmpty(tOaMessageInfoDel.MESSAGE_INFO_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MESSAGE_INFO_ID = '{0}'", tOaMessageInfoDel.MESSAGE_INFO_ID.ToString()));
				}	
				//接收发送类型（发送、接收）
				if (!String.IsNullOrEmpty(tOaMessageInfoDel.SEND_OR_ACCEPT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SEND_OR_ACCEPT = '{0}'", tOaMessageInfoDel.SEND_OR_ACCEPT.ToString()));
				}	
				//清除标识
				if (!String.IsNullOrEmpty(tOaMessageInfoDel.IS_DEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tOaMessageInfoDel.IS_DEL.ToString()));
				}	
				//备份1
				if (!String.IsNullOrEmpty(tOaMessageInfoDel.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaMessageInfoDel.REMARK1.ToString()));
				}	
				//备份2
				if (!String.IsNullOrEmpty(tOaMessageInfoDel.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaMessageInfoDel.REMARK2.ToString()));
				}	
				//备份3
				if (!String.IsNullOrEmpty(tOaMessageInfoDel.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaMessageInfoDel.REMARK3.ToString()));
				}	
				//备份4
				if (!String.IsNullOrEmpty(tOaMessageInfoDel.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaMessageInfoDel.REMARK4.ToString()));
				}	
				//备份5
				if (!String.IsNullOrEmpty(tOaMessageInfoDel.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaMessageInfoDel.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
