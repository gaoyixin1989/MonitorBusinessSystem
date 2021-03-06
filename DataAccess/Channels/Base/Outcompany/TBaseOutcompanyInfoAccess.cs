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
    /// 功能：分包单位
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseOutcompanyInfoAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            string strSQL = "select Count(*) from T_BASE_OUTCOMPANY_INFO " + this.BuildWhereStatement(tBaseOutcompanyInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseOutcompanyInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_OUTCOMPANY_INFO  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TBaseOutcompanyInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseOutcompanyInfoVo Details(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
           string strSQL = String.Format("select * from  T_BASE_OUTCOMPANY_INFO " + this.BuildWhereStatement(tBaseOutcompanyInfo));
           return SqlHelper.ExecuteObject(new TBaseOutcompanyInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseOutcompanyInfoVo> SelectByObject(TBaseOutcompanyInfoVo tBaseOutcompanyInfo, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_BASE_OUTCOMPANY_INFO " + this.BuildWhereStatement(tBaseOutcompanyInfo));
            return SqlHelper.ExecuteObjectList(tBaseOutcompanyInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseOutcompanyInfoVo tBaseOutcompanyInfo, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_BASE_OUTCOMPANY_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseOutcompanyInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseOutcompanyInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            string strSQL = "select * from T_BASE_OUTCOMPANY_INFO " + this.BuildWhereStatement(tBaseOutcompanyInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象</param>
        /// <returns></returns>
        public TBaseOutcompanyInfoVo SelectByObject(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            string strSQL = "select * from T_BASE_OUTCOMPANY_INFO " + this.BuildWhereStatement(tBaseOutcompanyInfo);
            return SqlHelper.ExecuteObject(new TBaseOutcompanyInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseOutcompanyInfo, TBaseOutcompanyInfoVo.T_BASE_OUTCOMPANY_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseOutcompanyInfo, TBaseOutcompanyInfoVo.T_BASE_OUTCOMPANY_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseOutcompanyInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseOutcompanyInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseOutcompanyInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseOutcompanyInfoVo tBaseOutcompanyInfo_UpdateSet, TBaseOutcompanyInfoVo tBaseOutcompanyInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseOutcompanyInfo_UpdateSet, TBaseOutcompanyInfoVo.T_BASE_OUTCOMPANY_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tBaseOutcompanyInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_OUTCOMPANY_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            string strSQL = "delete from T_BASE_OUTCOMPANY_INFO ";
	    strSQL += this.BuildWhereStatement(tBaseOutcompanyInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseOutcompanyInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseOutcompanyInfo)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseOutcompanyInfo.ID.ToString()));
				}	
				//公司法人代码
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.COMPANY_CODE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPANY_CODE = '{0}'", tBaseOutcompanyInfo.COMPANY_CODE.ToString()));
				}	
				//公司名称
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.COMPANY_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND COMPANY_NAME = '{0}'", tBaseOutcompanyInfo.COMPANY_NAME.ToString()));
				}	
				//拼音编码
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.PINYIN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PINYIN = '{0}'", tBaseOutcompanyInfo.PINYIN.ToString()));
				}	
				//联系人
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.LINK_MAN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND LINK_MAN = '{0}'", tBaseOutcompanyInfo.LINK_MAN.ToString()));
				}	
				//联系
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.PHONE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND PHONE = '{0}'", tBaseOutcompanyInfo.PHONE.ToString()));
				}	
				//邮编
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.POST.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND POST = '{0}'", tBaseOutcompanyInfo.POST.ToString()));
				}	
				//详细地址
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.ADDRESS.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ADDRESS = '{0}'", tBaseOutcompanyInfo.ADDRESS.ToString()));
				}	
				//外包公司资质
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.APTITUDE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND APTITUDE = '{0}'", tBaseOutcompanyInfo.APTITUDE.ToString()));
				}	
				//使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.IS_DEL.ToString().Trim()))
				{
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseOutcompanyInfo.IS_DEL.ToString()));
				}	
				//备注1
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.REMARK1.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseOutcompanyInfo.REMARK1.ToString()));
				}	
				//备注2
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.REMARK2.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseOutcompanyInfo.REMARK2.ToString()));
				}	
				//备注3
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.REMARK3.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseOutcompanyInfo.REMARK3.ToString()));
				}	
				//备注4
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.REMARK4.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseOutcompanyInfo.REMARK4.ToString()));
				}	
				//备注5
				if (!String.IsNullOrEmpty(tBaseOutcompanyInfo.REMARK5.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseOutcompanyInfo.REMARK5.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
