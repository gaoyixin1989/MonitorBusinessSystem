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
    /// 功能：工作流实例环节附属业务明细表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstTaskServiceAccess : SqlHelper 
    {

         #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstTaskService">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstTaskServiceVo tWfInstTaskService)
        {
            string strSQL = "select Count(*) from T_WF_INST_TASK_SERVICE " + this.BuildWhereStatement(tWfInstTaskService);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstTaskServiceVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_INST_TASK_SERVICE  where id='{0}'",id);

            return SqlHelper.ExecuteObject(new TWfInstTaskServiceVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstTaskService">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstTaskServiceVo Details(TWfInstTaskServiceVo tWfInstTaskService)
        {
           string strSQL = String.Format("select * from  T_WF_INST_TASK_SERVICE " + this.BuildWhereStatement(tWfInstTaskService));
           return SqlHelper.ExecuteObject(new TWfInstTaskServiceVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstTaskService">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstTaskServiceVo> SelectByObject(TWfInstTaskServiceVo tWfInstTaskService, int iIndex, int iCount)
        {
            
            string strSQL = String.Format("select * from  T_WF_INST_TASK_SERVICE " + this.BuildWhereStatement(tWfInstTaskService));
            return SqlHelper.ExecuteObjectList(tWfInstTaskService, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstTaskService">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstTaskServiceVo tWfInstTaskService, int iIndex, int iCount)
        {
            
            string strSQL = " select * from T_WF_INST_TASK_SERVICE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfInstTaskService));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstTaskService"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstTaskServiceVo tWfInstTaskService)
        {
            string strSQL = "select * from T_WF_INST_TASK_SERVICE " + this.BuildWhereStatement(tWfInstTaskService);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstTaskService">对象</param>
        /// <returns></returns>
        public TWfInstTaskServiceVo SelectByObject(TWfInstTaskServiceVo tWfInstTaskService)
        {
            string strSQL = "select * from T_WF_INST_TASK_SERVICE " + this.BuildWhereStatement(tWfInstTaskService);
            return SqlHelper.ExecuteObject(new TWfInstTaskServiceVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfInstTaskService">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstTaskServiceVo tWfInstTaskService)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfInstTaskService, TWfInstTaskServiceVo.T_WF_INST_TASK_SERVICE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskService">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskServiceVo tWfInstTaskService)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstTaskService, TWfInstTaskServiceVo.T_WF_INST_TASK_SERVICE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfInstTaskService.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskService_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tWfInstTaskService_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskServiceVo tWfInstTaskService_UpdateSet, TWfInstTaskServiceVo tWfInstTaskService_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstTaskService_UpdateSet, TWfInstTaskServiceVo.T_WF_INST_TASK_SERVICE_TABLE);
            strSQL += this.BuildWhereStatement(tWfInstTaskService_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_INST_TASK_SERVICE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfInstTaskServiceVo tWfInstTaskService)
        {
            string strSQL = "delete from T_WF_INST_TASK_SERVICE ";
	    strSQL += this.BuildWhereStatement(tWfInstTaskService);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfInstTaskService"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfInstTaskServiceVo tWfInstTaskService)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfInstTaskService)
            {
			    	
				//编号
				if (!String.IsNullOrEmpty(tWfInstTaskService.ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfInstTaskService.ID.ToString()));
				}	
				//环节实例编号
				if (!String.IsNullOrEmpty(tWfInstTaskService.WF_INST_TASK_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_INST_TASK_ID = '{0}'", tWfInstTaskService.WF_INST_TASK_ID.ToString()));
				}	
				//流程实例编号
				if (!String.IsNullOrEmpty(tWfInstTaskService.WF_INST_ID.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND WF_INST_ID = '{0}'", tWfInstTaskService.WF_INST_ID.ToString()));
				}	
				//业务编号
				if (!String.IsNullOrEmpty(tWfInstTaskService.SERVICE_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SERVICE_NAME = '{0}'", tWfInstTaskService.SERVICE_NAME.ToString()));
				}	
				//业务单类型
				if (!String.IsNullOrEmpty(tWfInstTaskService.SERVICE_KEY_NAME.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SERVICE_KEY_NAME = '{0}'", tWfInstTaskService.SERVICE_KEY_NAME.ToString()));
				}	
				//业务单主键值
				if (!String.IsNullOrEmpty(tWfInstTaskService.SERVICE_KEY_VALUE.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SERVICE_KEY_VALUE = '{0}'", tWfInstTaskService.SERVICE_KEY_VALUE.ToString()));
				}	
				//联合单据分组
				if (!String.IsNullOrEmpty(tWfInstTaskService.SERVICE_ROW_SIGN.ToString().Trim()))
				{
					strWhereStatement.Append(string.Format(" AND SERVICE_ROW_SIGN = '{0}'", tWfInstTaskService.SERVICE_ROW_SIGN.ToString()));
				}
			}
			return strWhereStatement.ToString();
        }

        #endregion
    }
}
