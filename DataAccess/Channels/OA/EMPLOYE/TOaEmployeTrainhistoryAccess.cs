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
    /// 功能：员工培训履历
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeTrainhistoryAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            string strSQL = "select Count(*) from T_OA_EMPLOYE_TRAINHISTORY " + this.BuildWhereStatement(tOaEmployeTrainhistory);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeTrainhistoryVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_TRAINHISTORY  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaEmployeTrainhistoryVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeTrainhistoryVo Details(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
           string strSQL = String.Format("select * from  T_OA_EMPLOYE_TRAINHISTORY " + this.BuildWhereStatement(tOaEmployeTrainhistory));
           return SqlHelper.ExecuteObject(new TOaEmployeTrainhistoryVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeTrainhistoryVo> SelectByObject(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_TRAINHISTORY " + this.BuildWhereStatement(tOaEmployeTrainhistory));
            return SqlHelper.ExecuteObjectList(tOaEmployeTrainhistory, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_EMPLOYE_TRAINHISTORY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaEmployeTrainhistory));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeTrainhistory"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            string strSQL = "select * from T_OA_EMPLOYE_TRAINHISTORY " + this.BuildWhereStatement(tOaEmployeTrainhistory);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取培训结果附件
        /// </summary>
        /// <param name="tOaEmployeTrainhistory"></param>
        /// <param name="strFileType"></param>
        /// <returns></returns>
        public DataTable SelectByTrainAttTable(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory, string strFileType)
        {
            string strSQL = String.Format(" SELECT A.ID,A.EMPLOYEID,A.ATT_NAME,A.ATT_URL,A.ATT_INFO,A.TRAIN_RESULT,A.BOOK_NUM,B.ID AS ATTID,(B.ATTACH_NAME+B.ATTACH_TYPE) AS ATTFILE,B.UPLOAD_DATE FROM T_OA_EMPLOYE_TRAINHISTORY  A");
            strSQL += String.Format(" LEFT JOIN T_OA_ATT B ON A.ID=B.BUSINESS_ID AND B.BUSINESS_TYPE='{0}'", strFileType);
            strSQL += String.Format(" WHERE 1=1 AND A.EMPLOYEID='{0}' ", tOaEmployeTrainhistory.EMPLOYEID);
            if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.ID))
            {
                strSQL += String.Format(" AND A.ID='{0}'", tOaEmployeTrainhistory.ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象</param>
        /// <returns></returns>
        public TOaEmployeTrainhistoryVo SelectByObject(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            string strSQL = "select * from T_OA_EMPLOYE_TRAINHISTORY " + this.BuildWhereStatement(tOaEmployeTrainhistory);
            return SqlHelper.ExecuteObject(new TOaEmployeTrainhistoryVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaEmployeTrainhistory, TOaEmployeTrainhistoryVo.T_OA_EMPLOYE_TRAINHISTORY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeTrainhistory, TOaEmployeTrainhistoryVo.T_OA_EMPLOYE_TRAINHISTORY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaEmployeTrainhistory.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeTrainhistory_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeTrainhistory_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory_UpdateSet, TOaEmployeTrainhistoryVo tOaEmployeTrainhistory_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeTrainhistory_UpdateSet, TOaEmployeTrainhistoryVo.T_OA_EMPLOYE_TRAINHISTORY_TABLE);
            strSQL += this.BuildWhereStatement(tOaEmployeTrainhistory_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_EMPLOYE_TRAINHISTORY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            string strSQL = "delete from T_OA_EMPLOYE_TRAINHISTORY ";
	    strSQL += this.BuildWhereStatement(tOaEmployeTrainhistory);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaEmployeTrainhistory"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaEmployeTrainhistory)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaEmployeTrainhistory.ID.ToString()));
				}	
				//员工编号
				if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.EMPLOYEID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMPLOYEID = '{0}'", tOaEmployeTrainhistory.EMPLOYEID.ToString()));
				}	
				//所在单位
				if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.ATT_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATT_NAME = '{0}'", tOaEmployeTrainhistory.ATT_NAME.ToString()));
				}	
				//附件路径
				if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.ATT_URL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATT_URL = '{0}'", tOaEmployeTrainhistory.ATT_URL.ToString()));
				}	
				//附件说明
				if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.ATT_INFO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ATT_INFO = '{0}'", tOaEmployeTrainhistory.ATT_INFO.ToString()));
				}	
				//培训结果
                if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.TRAIN_RESULT.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND TRAIN_RESULT = '{0}'", tOaEmployeTrainhistory.TRAIN_RESULT.ToString()));
				}	
				//证书号
                if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.BOOK_NUM.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND BOOK_NUM = '{0}'", tOaEmployeTrainhistory.BOOK_NUM.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaEmployeTrainhistory.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaEmployeTrainhistory.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaEmployeTrainhistory.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaEmployeTrainhistory.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaEmployeTrainhistory.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaEmployeTrainhistory.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
