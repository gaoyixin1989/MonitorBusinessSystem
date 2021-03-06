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
    /// 功能：流程配置主表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFlowAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingFlow">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingFlowVo tWfSettingFlow)
        {
            string strSQL = "select Count(*) from T_WF_SETTING_FLOW " + this.BuildWhereStatement(tWfSettingFlow);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingFlowVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_FLOW  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TWfSettingFlowVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingFlow">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingFlowVo Details(TWfSettingFlowVo tWfSettingFlow)
        {
           string strSQL = String.Format("select * from  T_WF_SETTING_FLOW " + this.BuildWhereStatement(tWfSettingFlow));
           return SqlHelper.ExecuteObject(new TWfSettingFlowVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingFlow">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingFlowVo> SelectByObject(TWfSettingFlowVo tWfSettingFlow, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_WF_SETTING_FLOW " + this.BuildWhereStatement(tWfSettingFlow));
            return SqlHelper.ExecuteObjectList(tWfSettingFlow, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingFlow">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingFlowVo tWfSettingFlow, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_WF_SETTING_FLOW {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfSettingFlow));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingFlow"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingFlowVo tWfSettingFlow)
        {
            string strSQL = "select * from T_WF_SETTING_FLOW " + this.BuildWhereStatement(tWfSettingFlow);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingFlow">对象</param>
        /// <returns></returns>
        public TWfSettingFlowVo SelectByObject(TWfSettingFlowVo tWfSettingFlow)
        {
            string strSQL = "select * from T_WF_SETTING_FLOW " + this.BuildWhereStatement(tWfSettingFlow);
            return SqlHelper.ExecuteObject(new TWfSettingFlowVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfSettingFlow">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingFlowVo tWfSettingFlow)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfSettingFlow, TWfSettingFlowVo.T_WF_SETTING_FLOW_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFlow">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFlowVo tWfSettingFlow)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingFlow, TWfSettingFlowVo.T_WF_SETTING_FLOW_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfSettingFlow.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFlow_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tWfSettingFlow_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFlowVo tWfSettingFlow_UpdateSet, TWfSettingFlowVo tWfSettingFlow_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingFlow_UpdateSet, TWfSettingFlowVo.T_WF_SETTING_FLOW_TABLE);
            strSQL += this.BuildWhereStatement(tWfSettingFlow_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_SETTING_FLOW where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfSettingFlowVo tWfSettingFlow)
        {
            string strSQL = "delete from T_WF_SETTING_FLOW ";
	    strSQL += this.BuildWhereStatement(tWfSettingFlow);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfSettingFlow"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfSettingFlowVo tWfSettingFlow)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfSettingFlow)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tWfSettingFlow.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfSettingFlow.ID.ToString()));
				}	
				//工作流编号
				if (!String.IsNullOrEmpty(tWfSettingFlow.WF_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_ID = '{0}'", tWfSettingFlow.WF_ID.ToString()));
				}	
				//工作流简称
				if (!String.IsNullOrEmpty(tWfSettingFlow.WF_CAPTION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_CAPTION = '{0}'", tWfSettingFlow.WF_CAPTION.ToString()));
				}	
				//类别归属
				if (!String.IsNullOrEmpty(tWfSettingFlow.WF_CLASS_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_CLASS_ID = '{0}'", tWfSettingFlow.WF_CLASS_ID.ToString()));
				}	
				//生成的版本
				if (!String.IsNullOrEmpty(tWfSettingFlow.WF_VERSION.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_VERSION = '{0}'", tWfSettingFlow.WF_VERSION.ToString()));
				}	
				//存在状态
				if (!String.IsNullOrEmpty(tWfSettingFlow.WF_STATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_STATE = '{0}'", tWfSettingFlow.WF_STATE.ToString()));
				}	
				//工作流描述
				if (!String.IsNullOrEmpty(tWfSettingFlow.WF_NOTE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_NOTE = '{0}'", tWfSettingFlow.WF_NOTE.ToString()));
				}	
				//主表单
				if (!String.IsNullOrEmpty(tWfSettingFlow.WF_FORM_MAIN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_FORM_MAIN = '{0}'", tWfSettingFlow.WF_FORM_MAIN.ToString()));
				}	
				//创建人
				if (!String.IsNullOrEmpty(tWfSettingFlow.CREATE_USER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CREATE_USER = '{0}'", tWfSettingFlow.CREATE_USER.ToString()));
				}	
				//创建日期
				if (!String.IsNullOrEmpty(tWfSettingFlow.CREATE_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND CREATE_DATE = '{0}'", tWfSettingFlow.CREATE_DATE.ToString()));
				}	
				//处理类型
				if (!String.IsNullOrEmpty(tWfSettingFlow.DEAL_TYPE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DEAL_TYPE = '{0}'", tWfSettingFlow.DEAL_TYPE.ToString()));
				}	
				//删除人
				if (!String.IsNullOrEmpty(tWfSettingFlow.DEAL_USER.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DEAL_USER = '{0}'", tWfSettingFlow.DEAL_USER.ToString()));
				}
                //删除日期 
				if (!String.IsNullOrEmpty(tWfSettingFlow.DEAL_DATE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND DEAL_DATE = '{0}'", tWfSettingFlow.DEAL_DATE.ToString()));
				}
                //首环节跳转页面
                if (!String.IsNullOrEmpty(tWfSettingFlow.FSTEP_RETURN_URL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FSTEP_RETURN_URL = '{0}'", tWfSettingFlow.FSTEP_RETURN_URL.ToString()));
                }	

				//备注
				if (!String.IsNullOrEmpty(tWfSettingFlow.REMARK.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tWfSettingFlow.REMARK.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
