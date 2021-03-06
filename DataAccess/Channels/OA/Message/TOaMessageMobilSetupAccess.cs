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
    /// 功能：短信设置
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageMobilSetupAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            string strSQL = "select Count(*) from T_OA_MESSAGE_MOBIL_SETUP " + this.BuildWhereStatement(tOaMessageMobilSetup);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageMobilSetupVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_MESSAGE_MOBIL_SETUP  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaMessageMobilSetupVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageMobilSetupVo Details(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
           string strSQL = String.Format("select * from  T_OA_MESSAGE_MOBIL_SETUP " + this.BuildWhereStatement(tOaMessageMobilSetup));
           return SqlHelper.ExecuteObject(new TOaMessageMobilSetupVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageMobilSetupVo> SelectByObject(TOaMessageMobilSetupVo tOaMessageMobilSetup, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_MESSAGE_MOBIL_SETUP " + this.BuildWhereStatement(tOaMessageMobilSetup));
            return SqlHelper.ExecuteObjectList(tOaMessageMobilSetup, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageMobilSetupVo tOaMessageMobilSetup, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_MESSAGE_MOBIL_SETUP {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaMessageMobilSetup));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageMobilSetup"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            string strSQL = "select * from T_OA_MESSAGE_MOBIL_SETUP " + this.BuildWhereStatement(tOaMessageMobilSetup);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象</param>
        /// <returns></returns>
        public TOaMessageMobilSetupVo SelectByObject(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            string strSQL = "select * from T_OA_MESSAGE_MOBIL_SETUP " + this.BuildWhereStatement(tOaMessageMobilSetup);
            return SqlHelper.ExecuteObject(new TOaMessageMobilSetupVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaMessageMobilSetup, TOaMessageMobilSetupVo.T_OA_MESSAGE_MOBIL_SETUP_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageMobilSetup">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageMobilSetup, TOaMessageMobilSetupVo.T_OA_MESSAGE_MOBIL_SETUP_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaMessageMobilSetup.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageMobilSetup_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageMobilSetup_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageMobilSetupVo tOaMessageMobilSetup_UpdateSet, TOaMessageMobilSetupVo tOaMessageMobilSetup_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaMessageMobilSetup_UpdateSet, TOaMessageMobilSetupVo.T_OA_MESSAGE_MOBIL_SETUP_TABLE);
            strSQL += this.BuildWhereStatement(tOaMessageMobilSetup_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_MESSAGE_MOBIL_SETUP where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            string strSQL = "delete from T_OA_MESSAGE_MOBIL_SETUP ";
	    strSQL += this.BuildWhereStatement(tOaMessageMobilSetup);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaMessageMobilSetup"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaMessageMobilSetup)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tOaMessageMobilSetup.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaMessageMobilSetup.ID.ToString()));
				}	
				//手机短信适用模块编码（按系统分配的1.2.4.8）
				if (!String.IsNullOrEmpty(tOaMessageMobilSetup.MOBIL_SYS_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MOBIL_SYS_CODE = '{0}'", tOaMessageMobilSetup.MOBIL_SYS_CODE.ToString()));
				}	
				//备份1
				if (!String.IsNullOrEmpty(tOaMessageMobilSetup.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaMessageMobilSetup.REMARK1.ToString()));
				}	
				//备份2
				if (!String.IsNullOrEmpty(tOaMessageMobilSetup.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaMessageMobilSetup.REMARK2.ToString()));
				}	
				//备份3
				if (!String.IsNullOrEmpty(tOaMessageMobilSetup.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaMessageMobilSetup.REMARK3.ToString()));
				}	
				//备份4
				if (!String.IsNullOrEmpty(tOaMessageMobilSetup.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaMessageMobilSetup.REMARK4.ToString()));
				}	
				//备份5
				if (!String.IsNullOrEmpty(tOaMessageMobilSetup.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaMessageMobilSetup.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
