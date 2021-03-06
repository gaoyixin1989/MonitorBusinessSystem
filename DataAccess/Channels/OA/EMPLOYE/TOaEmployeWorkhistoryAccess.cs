using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.EMPLOYE;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.EMPLOYE
{
    /// <summary>
    /// 功能：员工工作经历
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeWorkhistoryAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            string strSQL = "select Count(*) from T_OA_EMPLOYE_WORKHISTORY " + this.BuildWhereStatement(tOaEmployeWorkhistory);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeWorkhistoryVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_WORKHISTORY  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaEmployeWorkhistoryVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeWorkhistoryVo Details(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
           string strSQL = String.Format("select * from  T_OA_EMPLOYE_WORKHISTORY " + this.BuildWhereStatement(tOaEmployeWorkhistory));
           return SqlHelper.ExecuteObject(new TOaEmployeWorkhistoryVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeWorkhistoryVo> SelectByObject(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_WORKHISTORY " + this.BuildWhereStatement(tOaEmployeWorkhistory));
            return SqlHelper.ExecuteObjectList(tOaEmployeWorkhistory, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_EMPLOYE_WORKHISTORY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaEmployeWorkhistory));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeWorkhistory"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            string strSQL = "select * from T_OA_EMPLOYE_WORKHISTORY " + this.BuildWhereStatement(tOaEmployeWorkhistory);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象</param>
        /// <returns></returns>
        public TOaEmployeWorkhistoryVo SelectByObject(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            string strSQL = "select * from T_OA_EMPLOYE_WORKHISTORY " + this.BuildWhereStatement(tOaEmployeWorkhistory);
            return SqlHelper.ExecuteObject(new TOaEmployeWorkhistoryVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaEmployeWorkhistory, TOaEmployeWorkhistoryVo.T_OA_EMPLOYE_WORKHISTORY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeWorkhistory, TOaEmployeWorkhistoryVo.T_OA_EMPLOYE_WORKHISTORY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaEmployeWorkhistory.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeWorkhistory_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeWorkhistory_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory_UpdateSet, TOaEmployeWorkhistoryVo tOaEmployeWorkhistory_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeWorkhistory_UpdateSet, TOaEmployeWorkhistoryVo.T_OA_EMPLOYE_WORKHISTORY_TABLE);
            strSQL += this.BuildWhereStatement(tOaEmployeWorkhistory_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_EMPLOYE_WORKHISTORY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            string strSQL = "delete from T_OA_EMPLOYE_WORKHISTORY ";
	    strSQL += this.BuildWhereStatement(tOaEmployeWorkhistory);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaEmployeWorkhistory"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaEmployeWorkhistory)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaEmployeWorkhistory.ID.ToString()));
				}	
				//员工编号
				if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.EMPLOYEID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMPLOYEID = '{0}'", tOaEmployeWorkhistory.EMPLOYEID.ToString()));
				}	
				//所在单位
				if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.WORKCOMPANY.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WORKCOMPANY = '{0}'", tOaEmployeWorkhistory.WORKCOMPANY.ToString()));
				}	
				//开始时间
				if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.WORKBEGINDATE.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND WORKBEGINDATE = '{0}'", tOaEmployeWorkhistory.WORKBEGINDATE.ToString()));
				}
                //截止时间
                if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.WORKENDDATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WORKENDDATE = '{0}'", tOaEmployeWorkhistory.WORKENDDATE.ToString()));
                }	
				//岗位
				if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.POSITION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POSITION = '{0}'", tOaEmployeWorkhistory.POSITION.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaEmployeWorkhistory.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaEmployeWorkhistory.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaEmployeWorkhistory.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaEmployeWorkhistory.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaEmployeWorkhistory.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaEmployeWorkhistory.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
