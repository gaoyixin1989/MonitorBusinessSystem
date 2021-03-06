using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.RPT;
using i3.ValueObject;

namespace i3.DataAccess.Channels.RPT
{
    /// <summary>
    /// 功能：模板标签表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptTemplateMarkAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptTemplateMark">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptTemplateMarkVo tRptTemplateMark)
        {
            string strSQL = "select Count(*) from T_RPT_TEMPLATE_MARK " + this.BuildWhereStatement(tRptTemplateMark);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptTemplateMarkVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_RPT_TEMPLATE_MARK  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TRptTemplateMarkVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptTemplateMark">对象条件</param>
        /// <returns>对象</returns>
        public TRptTemplateMarkVo Details(TRptTemplateMarkVo tRptTemplateMark)
        {
           string strSQL = String.Format("select * from  T_RPT_TEMPLATE_MARK " + this.BuildWhereStatement(tRptTemplateMark));
           return SqlHelper.ExecuteObject(new TRptTemplateMarkVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptTemplateMark">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptTemplateMarkVo> SelectByObject(TRptTemplateMarkVo tRptTemplateMark, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_RPT_TEMPLATE_MARK " + this.BuildWhereStatement(tRptTemplateMark));
            return SqlHelper.ExecuteObjectList(tRptTemplateMark, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptTemplateMark">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptTemplateMarkVo tRptTemplateMark, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_RPT_TEMPLATE_MARK {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tRptTemplateMark));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptTemplateMark"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptTemplateMarkVo tRptTemplateMark)
        {
            string strSQL = "select * from T_RPT_TEMPLATE_MARK " + this.BuildWhereStatement(tRptTemplateMark);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptTemplateMark">对象</param>
        /// <returns></returns>
        public TRptTemplateMarkVo SelectByObject(TRptTemplateMarkVo tRptTemplateMark)
        {
            string strSQL = "select * from T_RPT_TEMPLATE_MARK " + this.BuildWhereStatement(tRptTemplateMark);
            return SqlHelper.ExecuteObject(new TRptTemplateMarkVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tRptTemplateMark">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptTemplateMarkVo tRptTemplateMark)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tRptTemplateMark, TRptTemplateMarkVo.T_RPT_TEMPLATE_MARK_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptTemplateMark">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptTemplateMarkVo tRptTemplateMark)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptTemplateMark, TRptTemplateMarkVo.T_RPT_TEMPLATE_MARK_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tRptTemplateMark.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptTemplateMark_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tRptTemplateMark_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptTemplateMarkVo tRptTemplateMark_UpdateSet, TRptTemplateMarkVo tRptTemplateMark_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptTemplateMark_UpdateSet, TRptTemplateMarkVo.T_RPT_TEMPLATE_MARK_TABLE);
            strSQL += this.BuildWhereStatement(tRptTemplateMark_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_RPT_TEMPLATE_MARK where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TRptTemplateMarkVo tRptTemplateMark)
        {
            string strSQL = "delete from T_RPT_TEMPLATE_MARK ";
	    strSQL += this.BuildWhereStatement(tRptTemplateMark);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tRptTemplateMark"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TRptTemplateMarkVo tRptTemplateMark)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tRptTemplateMark)
            {
			    	
				//ID
				if (!String.IsNullOrEmpty(tRptTemplateMark.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tRptTemplateMark.ID.ToString()));
				}	
				//标签ID
				if (!String.IsNullOrEmpty(tRptTemplateMark.MARK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND MARK_ID = '{0}'", tRptTemplateMark.MARK_ID.ToString()));
				}	
				//模板ID
				if (!String.IsNullOrEmpty(tRptTemplateMark.TEMPLATE_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND TEMPLATE_ID = '{0}'", tRptTemplateMark.TEMPLATE_ID.ToString()));
				}	
				//备注
				if (!String.IsNullOrEmpty(tRptTemplateMark.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tRptTemplateMark.REMARK.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
