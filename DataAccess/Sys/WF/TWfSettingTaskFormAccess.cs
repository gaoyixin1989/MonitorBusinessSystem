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
    /// 功能：工作流节点表单集
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingTaskFormAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            string strSQL = "select Count(*) from T_WF_SETTING_TASK_FORM " + this.BuildWhereStatement(tWfSettingTaskForm);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingTaskFormVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_TASK_FORM  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TWfSettingTaskFormVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingTaskFormVo Details(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
           string strSQL = String.Format("select * from  T_WF_SETTING_TASK_FORM " + this.BuildWhereStatement(tWfSettingTaskForm));
           return SqlHelper.ExecuteObject(new TWfSettingTaskFormVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskFormVo> SelectByObject(TWfSettingTaskFormVo tWfSettingTaskForm, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_WF_SETTING_TASK_FORM " + this.BuildWhereStatement(tWfSettingTaskForm));
            return SqlHelper.ExecuteObjectList(tWfSettingTaskForm, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingTaskFormVo tWfSettingTaskForm, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_WF_SETTING_TASK_FORM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfSettingTaskForm));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingTaskForm"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            string strSQL = "select * from T_WF_SETTING_TASK_FORM " + this.BuildWhereStatement(tWfSettingTaskForm);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象</param>
        /// <returns></returns>
        public TWfSettingTaskFormVo SelectByObject(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            string strSQL = "select * from T_WF_SETTING_TASK_FORM " + this.BuildWhereStatement(tWfSettingTaskForm);
            return SqlHelper.ExecuteObject(new TWfSettingTaskFormVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfSettingTaskForm, TWfSettingTaskFormVo.T_WF_SETTING_TASK_FORM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskForm">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingTaskForm, TWfSettingTaskFormVo.T_WF_SETTING_TASK_FORM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfSettingTaskForm.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskForm_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tWfSettingTaskForm_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskFormVo tWfSettingTaskForm_UpdateSet, TWfSettingTaskFormVo tWfSettingTaskForm_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingTaskForm_UpdateSet, TWfSettingTaskFormVo.T_WF_SETTING_TASK_FORM_TABLE);
            strSQL += this.BuildWhereStatement(tWfSettingTaskForm_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_SETTING_TASK_FORM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            string strSQL = "delete from T_WF_SETTING_TASK_FORM ";
	    strSQL += this.BuildWhereStatement(tWfSettingTaskForm);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfSettingTaskForm"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfSettingTaskForm)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tWfSettingTaskForm.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfSettingTaskForm.ID.ToString()));
				}	
				//表单内编号
				if (!String.IsNullOrEmpty(tWfSettingTaskForm.WF_TF_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_TF_ID = '{0}'", tWfSettingTaskForm.WF_TF_ID.ToString()));
				}	
				//流程编号
				if (!String.IsNullOrEmpty(tWfSettingTaskForm.WF_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_ID = '{0}'", tWfSettingTaskForm.WF_ID.ToString()));
				}	
				//节点编号
				if (!String.IsNullOrEmpty(tWfSettingTaskForm.WF_TASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_TASK_ID = '{0}'", tWfSettingTaskForm.WF_TASK_ID.ToString()));
				}	
				//主表单编号
				if (!String.IsNullOrEmpty(tWfSettingTaskForm.UCM_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCM_ID = '{0}'", tWfSettingTaskForm.UCM_ID.ToString()));
				}	
				//主表单类型
				if (!String.IsNullOrEmpty(tWfSettingTaskForm.UCM_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND UCM_TYPE = '{0}'", tWfSettingTaskForm.UCM_TYPE.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
