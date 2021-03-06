using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.SUPPLIER;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.SUPPLIER
{
    /// <summary>
    /// 功能：供应商信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSupplierInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSupplierInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaSupplierInfoVo tOaSupplierInfo)
        {
            string strSQL = "select Count(*) from T_OA_SUPPLIER_INFO " + this.BuildWhereStatement(tOaSupplierInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaSupplierInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_SUPPLIER_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TOaSupplierInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaSupplierInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaSupplierInfoVo Details(TOaSupplierInfoVo tOaSupplierInfo)
        {
           string strSQL = String.Format("select * from  T_OA_SUPPLIER_INFO " + this.BuildWhereStatement(tOaSupplierInfo));
           return SqlHelper.ExecuteObject(new TOaSupplierInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaSupplierInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaSupplierInfoVo> SelectByObject(TOaSupplierInfoVo tOaSupplierInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_OA_SUPPLIER_INFO " + this.BuildWhereStatement(tOaSupplierInfo));
            return SqlHelper.ExecuteObjectList(tOaSupplierInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaSupplierInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaSupplierInfoVo tOaSupplierInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_OA_SUPPLIER_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaSupplierInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSupplierInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSupplierInfoVo tOaSupplierInfo)
        {
            string strSQL = "select * from T_OA_SUPPLIER_INFO " + this.BuildWhereStatement(tOaSupplierInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaSupplierInfo">对象</param>
        /// <returns></returns>
        public TOaSupplierInfoVo SelectByObject(TOaSupplierInfoVo tOaSupplierInfo)
        {
            string strSQL = "select * from T_OA_SUPPLIER_INFO " + this.BuildWhereStatement(tOaSupplierInfo);
            return SqlHelper.ExecuteObject(new TOaSupplierInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaSupplierInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaSupplierInfoVo tOaSupplierInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaSupplierInfo, TOaSupplierInfoVo.T_OA_SUPPLIER_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSupplierInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSupplierInfoVo tOaSupplierInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaSupplierInfo, TOaSupplierInfoVo.T_OA_SUPPLIER_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaSupplierInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSupplierInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaSupplierInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSupplierInfoVo tOaSupplierInfo_UpdateSet, TOaSupplierInfoVo tOaSupplierInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaSupplierInfo_UpdateSet, TOaSupplierInfoVo.T_OA_SUPPLIER_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tOaSupplierInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_SUPPLIER_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaSupplierInfoVo tOaSupplierInfo)
        {
            string strSQL = "delete from T_OA_SUPPLIER_INFO ";
	    strSQL += this.BuildWhereStatement(tOaSupplierInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaSupplierInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaSupplierInfoVo tOaSupplierInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaSupplierInfo)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tOaSupplierInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaSupplierInfo.ID.ToString()));
				}	
				//供应商名称
				if (!String.IsNullOrEmpty(tOaSupplierInfo.SUPPLIER_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SUPPLIER_NAME like '%{0}%'", tOaSupplierInfo.SUPPLIER_NAME.ToString()));
				}	
				//供应物质类别
				if (!String.IsNullOrEmpty(tOaSupplierInfo.SUPPLIER_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SUPPLIER_TYPE = '{0}'", tOaSupplierInfo.SUPPLIER_TYPE.ToString()));
				}	
				//经营范围
				if (!String.IsNullOrEmpty(tOaSupplierInfo.PRODUCTS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PRODUCTS = '{0}'", tOaSupplierInfo.PRODUCTS.ToString()));
				}	
				//质量体系认证
				if (!String.IsNullOrEmpty(tOaSupplierInfo.QUANTITYSYSTEM.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND QUANTITYSYSTEM = '{0}'", tOaSupplierInfo.QUANTITYSYSTEM.ToString()));
				}	
				//联系人
				if (!String.IsNullOrEmpty(tOaSupplierInfo.LINK_MAN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LINK_MAN = '{0}'", tOaSupplierInfo.LINK_MAN.ToString()));
				}	
				//地址
				if (!String.IsNullOrEmpty(tOaSupplierInfo.ADDRESS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ADDRESS = '{0}'", tOaSupplierInfo.ADDRESS.ToString()));
				}	
				//电话
				if (!String.IsNullOrEmpty(tOaSupplierInfo.TEL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TEL = '{0}'", tOaSupplierInfo.TEL.ToString()));
				}	
				//传真
				if (!String.IsNullOrEmpty(tOaSupplierInfo.FAX.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND FAX = '{0}'", tOaSupplierInfo.FAX.ToString()));
				}	
				//邮件
				if (!String.IsNullOrEmpty(tOaSupplierInfo.EMAIL.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND EMAIL = '{0}'", tOaSupplierInfo.EMAIL.ToString()));
				}	
				//邮政编码
				if (!String.IsNullOrEmpty(tOaSupplierInfo.POST_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST_CODE = '{0}'", tOaSupplierInfo.POST_CODE.ToString()));
				}	
				//开户行
				if (!String.IsNullOrEmpty(tOaSupplierInfo.BANK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND BANK = '{0}'", tOaSupplierInfo.BANK.ToString()));
				}	
				//帐号
				if (!String.IsNullOrEmpty(tOaSupplierInfo.ACCOUNT_FOR.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ACCOUNT_FOR = '{0}'", tOaSupplierInfo.ACCOUNT_FOR.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tOaSupplierInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaSupplierInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tOaSupplierInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaSupplierInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tOaSupplierInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaSupplierInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tOaSupplierInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaSupplierInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tOaSupplierInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaSupplierInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
