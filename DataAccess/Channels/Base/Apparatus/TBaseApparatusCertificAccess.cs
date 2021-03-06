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
    /// 功能：仪器鉴定证书
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusCertificAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            string strSQL = "select Count(*) from T_BASE_APPARATUS_CERTIFIC " + this.BuildWhereStatement(tBaseApparatusCertific);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseApparatusCertificVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_APPARATUS_CERTIFIC  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseApparatusCertificVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象条件</param>
        /// <returns>对象</returns>
        public TBaseApparatusCertificVo Details(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
           string strSQL = String.Format("select * from  T_BASE_APPARATUS_CERTIFIC " + this.BuildWhereStatement(tBaseApparatusCertific));
           return SqlHelper.ExecuteObject(new TBaseApparatusCertificVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseApparatusCertificVo> SelectByObject(TBaseApparatusCertificVo tBaseApparatusCertific, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_APPARATUS_CERTIFIC " + this.BuildWhereStatement(tBaseApparatusCertific));
            return SqlHelper.ExecuteObjectList(tBaseApparatusCertific, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusCertificVo tBaseApparatusCertific, int iIndex, int iCount)
        {

            string strSQL = @" select *,(select APPARATUS_CODE from T_BASE_APPARATUS_INFO where ID=APPARATUS_ID) as APPARATUS_CODE,
		                         (select NAME from T_BASE_APPARATUS_INFO where ID=APPARATUS_ID) as APPARATUS_NAME from T_BASE_APPARATUS_CERTIFIC {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseApparatusCertific));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseApparatusCertific"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            string strSQL = "select * from T_BASE_APPARATUS_CERTIFIC " + this.BuildWhereStatement(tBaseApparatusCertific);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象</param>
        /// <returns></returns>
        public TBaseApparatusCertificVo SelectByObject(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            string strSQL = "select * from T_BASE_APPARATUS_CERTIFIC " + this.BuildWhereStatement(tBaseApparatusCertific);
            return SqlHelper.ExecuteObject(new TBaseApparatusCertificVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseApparatusCertific, TBaseApparatusCertificVo.T_BASE_APPARATUS_CERTIFIC_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusCertific">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseApparatusCertific, TBaseApparatusCertificVo.T_BASE_APPARATUS_CERTIFIC_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseApparatusCertific.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusCertific_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseApparatusCertific_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusCertificVo tBaseApparatusCertific_UpdateSet, TBaseApparatusCertificVo tBaseApparatusCertific_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseApparatusCertific_UpdateSet, TBaseApparatusCertificVo.T_BASE_APPARATUS_CERTIFIC_TABLE);
            strSQL += this.BuildWhereStatement(tBaseApparatusCertific_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_APPARATUS_CERTIFIC where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            string strSQL = "delete from T_BASE_APPARATUS_CERTIFIC ";
	    strSQL += this.BuildWhereStatement(tBaseApparatusCertific);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseApparatusCertific"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseApparatusCertific)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseApparatusCertific.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseApparatusCertific.ID.ToString()));
				}	
				//检定名称
				if (!String.IsNullOrEmpty(tBaseApparatusCertific.APPRAISAL_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPRAISAL_NAME = '{0}'", tBaseApparatusCertific.APPRAISAL_NAME.ToString()));
				}	
				//仪器ID
				if (!String.IsNullOrEmpty(tBaseApparatusCertific.APPARATUS_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPARATUS_ID = '{0}'", tBaseApparatusCertific.APPARATUS_ID.ToString()));
				}	
				//仪器检定时间
				if (!String.IsNullOrEmpty(tBaseApparatusCertific.APPRAISAL_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPRAISAL_DATE = '{0}'", tBaseApparatusCertific.APPRAISAL_DATE.ToString()));
				}	
				//检定证书路径
				if (!String.IsNullOrEmpty(tBaseApparatusCertific.APPRAISAL_URL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APPRAISAL_URL = '{0}'", tBaseApparatusCertific.APPRAISAL_URL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseApparatusCertific.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseApparatusCertific.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseApparatusCertific.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseApparatusCertific.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseApparatusCertific.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseApparatusCertific.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseApparatusCertific.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseApparatusCertific.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseApparatusCertific.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseApparatusCertific.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
