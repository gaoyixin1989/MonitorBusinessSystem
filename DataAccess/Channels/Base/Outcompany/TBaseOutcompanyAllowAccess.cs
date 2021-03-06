using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Outcompany;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Base.Outcompany
{
    /// <summary>
    /// 功能：分包单位资质
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseOutcompanyAllowAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            string strSQL = "select Count(*) from T_BASE_OUTCOMPANY_ALLOW " + this.BuildWhereStatement(tBaseOutcompanyAllow);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseOutcompanyAllowVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_OUTCOMPANY_ALLOW  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseOutcompanyAllowVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象条件</param>
        /// <returns>对象</returns>
        public TBaseOutcompanyAllowVo Details(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
           string strSQL = String.Format("select * from  T_BASE_OUTCOMPANY_ALLOW " + this.BuildWhereStatement(tBaseOutcompanyAllow));
           return SqlHelper.ExecuteObject(new TBaseOutcompanyAllowVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseOutcompanyAllowVo> SelectByObject(TBaseOutcompanyAllowVo tBaseOutcompanyAllow, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_OUTCOMPANY_ALLOW " + this.BuildWhereStatement(tBaseOutcompanyAllow));
            return SqlHelper.ExecuteObjectList(tBaseOutcompanyAllow, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseOutcompanyAllowVo tBaseOutcompanyAllow, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_OUTCOMPANY_ALLOW {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseOutcompanyAllow));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseOutcompanyAllow"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            string strSQL = "select * from T_BASE_OUTCOMPANY_ALLOW " + this.BuildWhereStatement(tBaseOutcompanyAllow);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public DataTable SelectByTable_ByJoin(TBaseOutcompanyAllowVo tBaseOutcompanyAllow, int iIndex, int iCount)
        {
            string strSQL1 = " select * from T_BASE_OUTCOMPANY_ALLOW {0} ";
            strSQL1 = String.Format(strSQL1, BuildWhereStatement(tBaseOutcompanyAllow));
            string strSQL = "select a.COMPANY_NAME,i.* from (" + strSQL1 + ")i";
            strSQL += " join T_BASE_OUTCOMPANY_INFO a on a.id=i.OUTCOMPANY_ID";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象</param>
        /// <returns></returns>
        public TBaseOutcompanyAllowVo SelectByObject(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            string strSQL = "select * from T_BASE_OUTCOMPANY_ALLOW " + this.BuildWhereStatement(tBaseOutcompanyAllow);
            return SqlHelper.ExecuteObject(new TBaseOutcompanyAllowVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseOutcompanyAllow, TBaseOutcompanyAllowVo.T_BASE_OUTCOMPANY_ALLOW_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseOutcompanyAllow, TBaseOutcompanyAllowVo.T_BASE_OUTCOMPANY_ALLOW_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseOutcompanyAllow.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseOutcompanyAllow_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseOutcompanyAllow_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseOutcompanyAllowVo tBaseOutcompanyAllow_UpdateSet, TBaseOutcompanyAllowVo tBaseOutcompanyAllow_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseOutcompanyAllow_UpdateSet, TBaseOutcompanyAllowVo.T_BASE_OUTCOMPANY_ALLOW_TABLE);
            strSQL += this.BuildWhereStatement(tBaseOutcompanyAllow_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_OUTCOMPANY_ALLOW where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            string strSQL = "delete from T_BASE_OUTCOMPANY_ALLOW ";
	    strSQL += this.BuildWhereStatement(tBaseOutcompanyAllow);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseOutcompanyAllow"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseOutcompanyAllow)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseOutcompanyAllow.ID.ToString()));
				}	
				//分包单位ID
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.OUTCOMPANY_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND OUTCOMPANY_ID = '{0}'", tBaseOutcompanyAllow.OUTCOMPANY_ID.ToString()));
				}	
				//资质变化情况
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.QUALIFICATIONS_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QUALIFICATIONS_INFO = '{0}'", tBaseOutcompanyAllow.QUALIFICATIONS_INFO.ToString()));
				}	
				//主要项目情况
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.PROJECT_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PROJECT_INFO = '{0}'", tBaseOutcompanyAllow.PROJECT_INFO.ToString()));
				}	
				//质保体系情况(1,符合国标；2，符合地标)
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.QC_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QC_INFO = '{0}'", tBaseOutcompanyAllow.QC_INFO.ToString()));
				}	
				//经办人
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.CHECK_USER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CHECK_USER_ID = '{0}'", tBaseOutcompanyAllow.CHECK_USER_ID.ToString()));
				}	
				//经办日期
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.CHECK_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CHECK_DATE = '{0}'", tBaseOutcompanyAllow.CHECK_DATE.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND INFO = '{0}'", tBaseOutcompanyAllow.INFO.ToString()));
				}	
				//附件ID
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.ATT_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATT_ID = '{0}'", tBaseOutcompanyAllow.ATT_ID.ToString()));
				}	
				//委托完成情况
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.COMPLETE_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPLETE_INFO = '{0}'", tBaseOutcompanyAllow.COMPLETE_INFO.ToString()));
				}	
				//是否通过评审
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.IS_OK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND IS_OK = '{0}'", tBaseOutcompanyAllow.IS_OK.ToString()));
				}	
				//评审意见
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.APP_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_INFO = '{0}'", tBaseOutcompanyAllow.APP_INFO.ToString()));
				}	
				//评审人ID
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.APP_USER_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_USER_ID = '{0}'", tBaseOutcompanyAllow.APP_USER_ID.ToString()));
				}	
				//评审时间
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.APP_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APP_DATE = '{0}'", tBaseOutcompanyAllow.APP_DATE.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseOutcompanyAllow.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseOutcompanyAllow.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseOutcompanyAllow.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseOutcompanyAllow.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseOutcompanyAllow.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseOutcompanyAllow.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
