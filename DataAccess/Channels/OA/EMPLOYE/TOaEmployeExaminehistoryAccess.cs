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
    /// 功能：员工考核历史
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeExaminehistoryAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            string strSQL = "select Count(*) from T_OA_EMPLOYE_EXAMINEHISTORY " + this.BuildWhereStatement(tOaEmployeExaminehistory);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeExaminehistoryVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_EXAMINEHISTORY  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaEmployeExaminehistoryVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeExaminehistoryVo Details(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
           string strSQL = String.Format("select * from  T_OA_EMPLOYE_EXAMINEHISTORY " + this.BuildWhereStatement(tOaEmployeExaminehistory));
           return SqlHelper.ExecuteObject(new TOaEmployeExaminehistoryVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeExaminehistoryVo> SelectByObject(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_EXAMINEHISTORY " + this.BuildWhereStatement(tOaEmployeExaminehistory));
            return SqlHelper.ExecuteObjectList(tOaEmployeExaminehistory, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_EMPLOYE_EXAMINEHISTORY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaEmployeExaminehistory));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeExaminehistory"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            string strSQL = "select * from T_OA_EMPLOYE_EXAMINEHISTORY " + this.BuildWhereStatement(tOaEmployeExaminehistory);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取证书信息及其附件信息
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <param name="strFileType">附件类型代码</param>
        /// <returns></returns>
        public DataTable SelectByUnionAttTable(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory, string strFileType)
        {
            string strSQL = String.Format(" SELECT A.ID,A.EMPLOYEID,A.EX_YEAR,A.EX_INFO,B.ID AS ATTID,(B.ATTACH_NAME+B.ATTACH_TYPE) AS ATTFILE,B.UPLOAD_DATE FROM T_OA_EMPLOYE_EXAMINEHISTORY  A");
            strSQL += String.Format(" LEFT JOIN T_OA_ATT B ON A.ID=B.BUSINESS_ID AND B.BUSINESS_TYPE='{0}'", strFileType);
            strSQL += String.Format(" WHERE 1=1 AND A.EMPLOYEID='{0}' ", tOaEmployeExaminehistory.EMPLOYEID);
            if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.ID))
            {
                strSQL += String.Format(" AND A.ID='{0}'", tOaEmployeExaminehistory.ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象</param>
        /// <returns></returns>
        public TOaEmployeExaminehistoryVo SelectByObject(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            string strSQL = "select * from T_OA_EMPLOYE_EXAMINEHISTORY " + this.BuildWhereStatement(tOaEmployeExaminehistory);
            return SqlHelper.ExecuteObject(new TOaEmployeExaminehistoryVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaEmployeExaminehistory, TOaEmployeExaminehistoryVo.T_OA_EMPLOYE_EXAMINEHISTORY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeExaminehistory, TOaEmployeExaminehistoryVo.T_OA_EMPLOYE_EXAMINEHISTORY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaEmployeExaminehistory.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeExaminehistory_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeExaminehistory_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory_UpdateSet, TOaEmployeExaminehistoryVo tOaEmployeExaminehistory_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeExaminehistory_UpdateSet, TOaEmployeExaminehistoryVo.T_OA_EMPLOYE_EXAMINEHISTORY_TABLE);
            strSQL += this.BuildWhereStatement(tOaEmployeExaminehistory_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_EMPLOYE_EXAMINEHISTORY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            string strSQL = "delete from T_OA_EMPLOYE_EXAMINEHISTORY ";
	    strSQL += this.BuildWhereStatement(tOaEmployeExaminehistory);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaEmployeExaminehistory"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaEmployeExaminehistory)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaEmployeExaminehistory.ID.ToString()));
				}	
				//员工编号
				if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.EMPLOYEID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMPLOYEID = '{0}'", tOaEmployeExaminehistory.EMPLOYEID.ToString()));
				}
                //年度
                if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.EX_YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EX_YEAR = '{0}'", tOaEmployeExaminehistory.EX_YEAR.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.EX_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND EX_INFO = '{0}'", tOaEmployeExaminehistory.EX_INFO.ToString()));
                }	
				//所在单位
				if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.ATT_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATT_NAME = '{0}'", tOaEmployeExaminehistory.ATT_NAME.ToString()));
				}	
				//附件路径
				if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.ATT_URL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATT_URL = '{0}'", tOaEmployeExaminehistory.ATT_URL.ToString()));
				}	
				//附件说明
				if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.ATT_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATT_INFO = '{0}'", tOaEmployeExaminehistory.ATT_INFO.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaEmployeExaminehistory.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaEmployeExaminehistory.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaEmployeExaminehistory.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaEmployeExaminehistory.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaEmployeExaminehistory.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaEmployeExaminehistory.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
