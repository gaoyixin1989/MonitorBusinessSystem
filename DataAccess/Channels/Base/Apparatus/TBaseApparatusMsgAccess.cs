using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Apparatus
{
    /// <summary>
    /// 功能：仪器定期维护自动提醒
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusMsgAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            string strSQL = "select Count(*) from T_BASE_APPARATUS_MSG " + this.BuildWhereStatement(tBaseApparatusMsg);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseApparatusMsgVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_APPARATUS_MSG  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseApparatusMsgVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象条件</param>
        /// <returns>对象</returns>
        public TBaseApparatusMsgVo Details(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
           string strSQL = String.Format("select * from  T_BASE_APPARATUS_MSG " + this.BuildWhereStatement(tBaseApparatusMsg));
           return SqlHelper.ExecuteObject(new TBaseApparatusMsgVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseApparatusMsgVo> SelectByObject(TBaseApparatusMsgVo tBaseApparatusMsg, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_APPARATUS_MSG " + this.BuildWhereStatement(tBaseApparatusMsg));
            return SqlHelper.ExecuteObjectList(tBaseApparatusMsg, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusMsgVo tBaseApparatusMsg, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_APPARATUS_MSG {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseApparatusMsg));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseApparatusMsg"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            string strSQL = "select * from T_BASE_APPARATUS_MSG " + this.BuildWhereStatement(tBaseApparatusMsg);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象</param>
        /// <returns></returns>
        public TBaseApparatusMsgVo SelectByObject(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            string strSQL = "select * from T_BASE_APPARATUS_MSG " + this.BuildWhereStatement(tBaseApparatusMsg);
            return SqlHelper.ExecuteObject(new TBaseApparatusMsgVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseApparatusMsg, TBaseApparatusMsgVo.T_BASE_APPARATUS_MSG_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusMsg">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseApparatusMsg, TBaseApparatusMsgVo.T_BASE_APPARATUS_MSG_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseApparatusMsg.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusMsg_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseApparatusMsg_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusMsgVo tBaseApparatusMsg_UpdateSet, TBaseApparatusMsgVo tBaseApparatusMsg_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseApparatusMsg_UpdateSet, TBaseApparatusMsgVo.T_BASE_APPARATUS_MSG_TABLE);
            strSQL += this.BuildWhereStatement(tBaseApparatusMsg_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_APPARATUS_MSG where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            string strSQL = "delete from T_BASE_APPARATUS_MSG ";
	    strSQL += this.BuildWhereStatement(tBaseApparatusMsg);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseApparatusMsg"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseApparatusMsg)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseApparatusMsg.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseApparatusMsg.ID.ToString()));
				}	
				//消息ID
				if (!String.IsNullOrEmpty(tBaseApparatusMsg.MSG_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MSG_ID = '{0}'", tBaseApparatusMsg.MSG_ID.ToString()));
				}	
				//仪器ID
				if (!String.IsNullOrEmpty(tBaseApparatusMsg.APPARATUS_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPARATUS_ID = '{0}'", tBaseApparatusMsg.APPARATUS_ID.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseApparatusMsg.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseApparatusMsg.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseApparatusMsg.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseApparatusMsg.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseApparatusMsg.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseApparatusMsg.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseApparatusMsg.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseApparatusMsg.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseApparatusMsg.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseApparatusMsg.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
