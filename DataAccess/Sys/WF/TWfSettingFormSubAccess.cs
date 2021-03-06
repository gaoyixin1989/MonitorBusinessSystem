using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.WF;
using i3.ValueObject;

namespace i3.DataAccess.Sys.WF
{
    /// <summary>
    /// 功能：流程子表单配置
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFormSubAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingFormSub">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingFormSubVo tWfSettingFormSub)
        {
            string strSQL = "select Count(*) from T_WF_SETTING_FORM_SUB " + this.BuildWhereStatement(tWfSettingFormSub);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingFormSubVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_FORM_SUB  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TWfSettingFormSubVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingFormSub">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingFormSubVo Details(TWfSettingFormSubVo tWfSettingFormSub)
        {
           string strSQL = String.Format("select * from  T_WF_SETTING_FORM_SUB " + this.BuildWhereStatement(tWfSettingFormSub));
           return SqlHelper.ExecuteObject(new TWfSettingFormSubVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingFormSub">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingFormSubVo> SelectByObject(TWfSettingFormSubVo tWfSettingFormSub, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_WF_SETTING_FORM_SUB " + this.BuildWhereStatement(tWfSettingFormSub));
            return SqlHelper.ExecuteObjectList(tWfSettingFormSub, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingFormSub">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingFormSubVo tWfSettingFormSub, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_WF_SETTING_FORM_SUB {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfSettingFormSub));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingFormSub"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingFormSubVo tWfSettingFormSub)
        {
            string strSQL = "select * from T_WF_SETTING_FORM_SUB " + this.BuildWhereStatement(tWfSettingFormSub);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingFormSub">对象</param>
        /// <returns></returns>
        public TWfSettingFormSubVo SelectByObject(TWfSettingFormSubVo tWfSettingFormSub)
        {
            string strSQL = "select * from T_WF_SETTING_FORM_SUB " + this.BuildWhereStatement(tWfSettingFormSub);
            return SqlHelper.ExecuteObject(new TWfSettingFormSubVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfSettingFormSub">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingFormSubVo tWfSettingFormSub)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfSettingFormSub, TWfSettingFormSubVo.T_WF_SETTING_FORM_SUB_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormSub">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormSubVo tWfSettingFormSub)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingFormSub, TWfSettingFormSubVo.T_WF_SETTING_FORM_SUB_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfSettingFormSub.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormSub_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tWfSettingFormSub_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormSubVo tWfSettingFormSub_UpdateSet, TWfSettingFormSubVo tWfSettingFormSub_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingFormSub_UpdateSet, TWfSettingFormSubVo.T_WF_SETTING_FORM_SUB_TABLE);
            strSQL += this.BuildWhereStatement(tWfSettingFormSub_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_SETTING_FORM_SUB where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfSettingFormSubVo tWfSettingFormSub)
        {
            string strSQL = "delete from T_WF_SETTING_FORM_SUB ";
	    strSQL += this.BuildWhereStatement(tWfSettingFormSub);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfSettingFormSub"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfSettingFormSubVo tWfSettingFormSub)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfSettingFormSub)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tWfSettingFormSub.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfSettingFormSub.ID.ToString()));
				}	
				//子表单编号
				if (!String.IsNullOrEmpty(tWfSettingFormSub.UCS_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCS_ID = '{0}'", tWfSettingFormSub.UCS_ID.ToString()));
				}	
				//子表单简称
				if (!String.IsNullOrEmpty(tWfSettingFormSub.UCS_CAPTION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCS_CAPTION = '{0}'", tWfSettingFormSub.UCS_CAPTION.ToString()));
				}	
				//子表单类型
				if (!String.IsNullOrEmpty(tWfSettingFormSub.UCS_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCS_TYPE = '{0}'", tWfSettingFormSub.UCS_TYPE.ToString()));
				}	
				//相对路径
				if (!String.IsNullOrEmpty(tWfSettingFormSub.UCS_PATH.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCS_PATH = '{0}'", tWfSettingFormSub.UCS_PATH.ToString()));
				}	
				//子表单全名
				if (!String.IsNullOrEmpty(tWfSettingFormSub.UCS_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCS_NAME = '{0}'", tWfSettingFormSub.UCS_NAME.ToString()));
				}	
				//子表单内编码
				if (!String.IsNullOrEmpty(tWfSettingFormSub.UCS_NOTE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCS_NOTE = '{0}'", tWfSettingFormSub.UCS_NOTE.ToString()));
				}	
				//子表单描述
				if (!String.IsNullOrEmpty(tWfSettingFormSub.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tWfSettingFormSub.REMARK.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
