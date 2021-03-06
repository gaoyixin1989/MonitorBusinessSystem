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
    /// 功能：员工资格证书
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeQualificationAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            string strSQL = "select Count(*) from T_OA_EMPLOYE_QUALIFICATION " + this.BuildWhereStatement(tOaEmployeQualification);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeQualificationVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_QUALIFICATION  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaEmployeQualificationVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeQualification">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeQualificationVo Details(TOaEmployeQualificationVo tOaEmployeQualification)
        {
           string strSQL = String.Format("select * from  T_OA_EMPLOYE_QUALIFICATION " + this.BuildWhereStatement(tOaEmployeQualification));
           return SqlHelper.ExecuteObject(new TOaEmployeQualificationVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeQualificationVo> SelectByObject(TOaEmployeQualificationVo tOaEmployeQualification, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_EMPLOYE_QUALIFICATION " + this.BuildWhereStatement(tOaEmployeQualification));
            return SqlHelper.ExecuteObjectList(tOaEmployeQualification, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeQualificationVo tOaEmployeQualification, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_EMPLOYE_QUALIFICATION {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaEmployeQualification));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeQualification"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            string strSQL = "select * from T_OA_EMPLOYE_QUALIFICATION " + this.BuildWhereStatement(tOaEmployeQualification);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取证书信息及其附件信息
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <param name="strFileType">附件类型代码</param>
        /// <returns></returns>
        public DataTable SelectByUnionAttTable(TOaEmployeQualificationVo tOaEmployeQualification,string strFileType)
        {
            string strSQL = String.Format(" SELECT A.ID,A.EMPLOYEID,A.CERTITICATENAME,A.CERTITICATECODE,A.ISSUINGAUTHO,A.ISSUINDATE,A.ACTIVEDATE,B.ID AS ATTID,(B.ATTACH_NAME+B.ATTACH_TYPE) AS ATTFILE,B.UPLOAD_DATE FROM T_OA_EMPLOYE_QUALIFICATION  A");
            strSQL += String.Format(" LEFT JOIN T_OA_ATT B ON A.ID=B.BUSINESS_ID AND B.BUSINESS_TYPE='{0}'",strFileType);
            strSQL += String.Format(" WHERE 1=1 AND A.EMPLOYEID='{0}' ", tOaEmployeQualification.EMPLOYEID);
            if (!String.IsNullOrEmpty(tOaEmployeQualification.ID)) {
                strSQL += String.Format(" AND A.ID='{0}'", tOaEmployeQualification.ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <returns></returns>
        public TOaEmployeQualificationVo SelectByObject(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            string strSQL = "select * from T_OA_EMPLOYE_QUALIFICATION " + this.BuildWhereStatement(tOaEmployeQualification);
            return SqlHelper.ExecuteObject(new TOaEmployeQualificationVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaEmployeQualification, TOaEmployeQualificationVo.T_OA_EMPLOYE_QUALIFICATION_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeQualification">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeQualification, TOaEmployeQualificationVo.T_OA_EMPLOYE_QUALIFICATION_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaEmployeQualification.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeQualification_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeQualification_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeQualificationVo tOaEmployeQualification_UpdateSet, TOaEmployeQualificationVo tOaEmployeQualification_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaEmployeQualification_UpdateSet, TOaEmployeQualificationVo.T_OA_EMPLOYE_QUALIFICATION_TABLE);
            strSQL += this.BuildWhereStatement(tOaEmployeQualification_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_EMPLOYE_QUALIFICATION where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            string strSQL = "delete from T_OA_EMPLOYE_QUALIFICATION ";
	    strSQL += this.BuildWhereStatement(tOaEmployeQualification);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaEmployeQualification"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaEmployeQualification)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaEmployeQualification.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaEmployeQualification.ID.ToString()));
				}	
				//员工编号
				if (!String.IsNullOrEmpty(tOaEmployeQualification.EMPLOYEID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMPLOYEID = '{0}'", tOaEmployeQualification.EMPLOYEID.ToString()));
				}	
				//证书名称
				if (!String.IsNullOrEmpty(tOaEmployeQualification.CERTITICATENAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CERTITICATENAME = '{0}'", tOaEmployeQualification.CERTITICATENAME.ToString()));
				}	
				//证书编号
				if (!String.IsNullOrEmpty(tOaEmployeQualification.CERTITICATECODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CERTITICATECODE = '{0}'", tOaEmployeQualification.CERTITICATECODE.ToString()));
				}	
				//发证单位
				if (!String.IsNullOrEmpty(tOaEmployeQualification.ISSUINGAUTHO.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ISSUINGAUTHO = '{0}'", tOaEmployeQualification.ISSUINGAUTHO.ToString()));
				}	
				//发证时间
				if (!String.IsNullOrEmpty(tOaEmployeQualification.ISSUINDATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ISSUINDATE = '{0}'", tOaEmployeQualification.ISSUINDATE.ToString()));
				}	
				//有效日期
				if (!String.IsNullOrEmpty(tOaEmployeQualification.ACTIVEDATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ACTIVEDATE = '{0}'", tOaEmployeQualification.ACTIVEDATE.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaEmployeQualification.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaEmployeQualification.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaEmployeQualification.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaEmployeQualification.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaEmployeQualification.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaEmployeQualification.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaEmployeQualification.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaEmployeQualification.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaEmployeQualification.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaEmployeQualification.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
