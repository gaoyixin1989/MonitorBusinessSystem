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
    /// 功能：员工工作成果与事故
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeResultorfaultAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            string strSQL = "select Count(*) from T_OA_EMPLOYE_RESULTORFAULT " + this.BuildWhereStatement(tOaEmployeResultorfault);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeResultorfaultVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_RESULTORFAULT  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaEmployeResultorfaultVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeResultorfaultVo Details(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
           string strSQL = String.Format("select * from  T_OA_EMPLOYE_RESULTORFAULT " + this.BuildWhereStatement(tOaEmployeResultorfault));
           return SqlHelper.ExecuteObject(new TOaEmployeResultorfaultVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeResultorfaultVo> SelectByObject(TOaEmployeResultorfaultVo tOaEmployeResultorfault, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_RESULTORFAULT " + this.BuildWhereStatement(tOaEmployeResultorfault));
            return SqlHelper.ExecuteObjectList(tOaEmployeResultorfault, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeResultorfaultVo tOaEmployeResultorfault, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_EMPLOYE_RESULTORFAULT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaEmployeResultorfault));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeResultorfault"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            string strSQL = "select * from T_OA_EMPLOYE_RESULTORFAULT " + this.BuildWhereStatement(tOaEmployeResultorfault);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 获取员工工作成果及其附件
        /// </summary>
        /// <param name="tOaEmployeResultorfault"></param>
        /// <param name="strFileType"></param>
        /// <returns></returns>
        public DataTable SelectByWorkResultTable(TOaEmployeResultorfaultVo tOaEmployeResultorfault, string strFileType)
        {
            string strSQL = String.Format("  SELECT A.ID,A.EMPLOYEID,A.WORKRESULT,A.ACCIDENTS,A.RESULT_OR_ACCIDENT,A.ACCIDENTHAPPENDATE,B.ID AS ATTID,(B.ATTACH_NAME+B.ATTACH_TYPE) AS ATTFILE,B.UPLOAD_DATE  FROM dbo.T_OA_EMPLOYE_RESULTORFAULT A ");
            strSQL += String.Format(" LEFT JOIN T_OA_ATT B ON A.ID=B.BUSINESS_ID AND B.BUSINESS_TYPE='{0}'",strFileType);
            strSQL += String.Format(" WHERE 1=1 AND A.EMPLOYEID='{0}'  AND A.RESULT_OR_ACCIDENT='{1}' ", tOaEmployeResultorfault.EMPLOYEID,tOaEmployeResultorfault.RESULT_OR_ACCIDENT);
            if (!String.IsNullOrEmpty(tOaEmployeResultorfault.ID))
            {
                strSQL += String.Format(" AND A.ID='{0}'", tOaEmployeResultorfault.ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象</param>
        /// <returns></returns>
        public TOaEmployeResultorfaultVo SelectByObject(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            string strSQL = "select * from T_OA_EMPLOYE_RESULTORFAULT " + this.BuildWhereStatement(tOaEmployeResultorfault);
            return SqlHelper.ExecuteObject(new TOaEmployeResultorfaultVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaEmployeResultorfault, TOaEmployeResultorfaultVo.T_OA_EMPLOYE_RESULTORFAULT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeResultorfault">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeResultorfault, TOaEmployeResultorfaultVo.T_OA_EMPLOYE_RESULTORFAULT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaEmployeResultorfault.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeResultorfault_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeResultorfault_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeResultorfaultVo tOaEmployeResultorfault_UpdateSet, TOaEmployeResultorfaultVo tOaEmployeResultorfault_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeResultorfault_UpdateSet, TOaEmployeResultorfaultVo.T_OA_EMPLOYE_RESULTORFAULT_TABLE);
            strSQL += this.BuildWhereStatement(tOaEmployeResultorfault_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_EMPLOYE_RESULTORFAULT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            string strSQL = "delete from T_OA_EMPLOYE_RESULTORFAULT ";
	    strSQL += this.BuildWhereStatement(tOaEmployeResultorfault);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaEmployeResultorfault"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaEmployeResultorfault)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaEmployeResultorfault.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaEmployeResultorfault.ID.ToString()));
				}	
				//员工编号
				if (!String.IsNullOrEmpty(tOaEmployeResultorfault.EMPLOYEID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMPLOYEID = '{0}'", tOaEmployeResultorfault.EMPLOYEID.ToString()));
				}	
				//工作成果
				if (!String.IsNullOrEmpty(tOaEmployeResultorfault.WORKRESULT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WORKRESULT = '{0}'", tOaEmployeResultorfault.WORKRESULT.ToString()));
				}	
				//质量事故
				if (!String.IsNullOrEmpty(tOaEmployeResultorfault.ACCIDENTS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ACCIDENTS = '{0}'", tOaEmployeResultorfault.ACCIDENTS.ToString()));
				}	
				//成果或事故，1成果，2事故
				if (!String.IsNullOrEmpty(tOaEmployeResultorfault.RESULT_OR_ACCIDENT.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND RESULT_OR_ACCIDENT = '{0}'", tOaEmployeResultorfault.RESULT_OR_ACCIDENT.ToString()));
				}
                //成果或事故，1成果，2事故
                if (!String.IsNullOrEmpty(tOaEmployeResultorfault.ACCIDENTHAPPENDATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ACCIDENTHAPPENDATE = '{0}'", tOaEmployeResultorfault.ACCIDENTHAPPENDATE.ToString()));
                }	
				//备注1
				if (!String.IsNullOrEmpty(tOaEmployeResultorfault.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaEmployeResultorfault.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaEmployeResultorfault.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaEmployeResultorfault.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaEmployeResultorfault.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaEmployeResultorfault.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaEmployeResultorfault.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaEmployeResultorfault.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaEmployeResultorfault.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaEmployeResultorfault.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
