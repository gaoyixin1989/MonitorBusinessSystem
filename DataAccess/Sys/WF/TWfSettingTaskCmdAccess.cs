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
    /// 功能：流程节点命令集合
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingTaskCmdAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            string strSQL = "select Count(*) from T_WF_SETTING_TASK_CMD " + this.BuildWhereStatement(tWfSettingTaskCmd);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingTaskCmdVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_TASK_CMD  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TWfSettingTaskCmdVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingTaskCmdVo Details(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_TASK_CMD " + this.BuildWhereStatement(tWfSettingTaskCmd));
            return SqlHelper.ExecuteObject(new TWfSettingTaskCmdVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskCmdVo> SelectByObject(TWfSettingTaskCmdVo tWfSettingTaskCmd, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_WF_SETTING_TASK_CMD " + this.BuildWhereStatement(tWfSettingTaskCmd));
            return SqlHelper.ExecuteObjectList(tWfSettingTaskCmd, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingTaskCmdVo tWfSettingTaskCmd, int iIndex, int iCount)
        {

            string strSQL = " select * from T_WF_SETTING_TASK_CMD {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfSettingTaskCmd));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingTaskCmd"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            string strSQL = "select * from T_WF_SETTING_TASK_CMD " + this.BuildWhereStatement(tWfSettingTaskCmd);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象</param>
        /// <returns></returns>
        public TWfSettingTaskCmdVo SelectByObject(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            string strSQL = "select * from T_WF_SETTING_TASK_CMD " + this.BuildWhereStatement(tWfSettingTaskCmd);
            return SqlHelper.ExecuteObject(new TWfSettingTaskCmdVo(), strSQL);
        }

        /// <summary>
        /// List对象添加 
        /// </summary>
        /// <param name="tWfSettingTaskCmd">List对象</param>
        /// <returns>是否成功</returns>
        public bool Create(List<TWfSettingTaskCmdVo> tWfSettingTaskCmdList)
        {
            ArrayList strSQLList = new ArrayList();
            foreach (TWfSettingTaskCmdVo temp in tWfSettingTaskCmdList)
            {
                string strSQL = SqlHelper.BuildInsertExpress(temp, TWfSettingTaskCmdVo.T_WF_SETTING_TASK_CMD_TABLE);
                strSQLList.Add(strSQL);
            }
            return SqlHelper.ExecuteSQLByTransaction(strSQLList);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfSettingTaskCmd, TWfSettingTaskCmdVo.T_WF_SETTING_TASK_CMD_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskCmd">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingTaskCmd, TWfSettingTaskCmdVo.T_WF_SETTING_TASK_CMD_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfSettingTaskCmd.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskCmd_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingTaskCmd_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskCmdVo tWfSettingTaskCmd_UpdateSet, TWfSettingTaskCmdVo tWfSettingTaskCmd_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingTaskCmd_UpdateSet, TWfSettingTaskCmdVo.T_WF_SETTING_TASK_CMD_TABLE);
            strSQL += this.BuildWhereStatement(tWfSettingTaskCmd_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_SETTING_TASK_CMD where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            string strSQL = "delete from T_WF_SETTING_TASK_CMD ";
            strSQL += this.BuildWhereStatement(tWfSettingTaskCmd);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfSettingTaskCmd"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfSettingTaskCmd)
            {

                //编号
                if (!String.IsNullOrEmpty(tWfSettingTaskCmd.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfSettingTaskCmd.ID.ToString()));
                }
                //命令编号
                if (!String.IsNullOrEmpty(tWfSettingTaskCmd.WF_CMD_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_CMD_ID = '{0}'", tWfSettingTaskCmd.WF_CMD_ID.ToString()));
                }
                //流程编号
                if (!String.IsNullOrEmpty(tWfSettingTaskCmd.WF_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_ID = '{0}'", tWfSettingTaskCmd.WF_ID.ToString()));
                }
                //节点编号
                if (!String.IsNullOrEmpty(tWfSettingTaskCmd.WF_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_TASK_ID = '{0}'", tWfSettingTaskCmd.WF_TASK_ID.ToString()));
                }
                //命令名称
                if (!String.IsNullOrEmpty(tWfSettingTaskCmd.CMD_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CMD_NAME = '{0}'", tWfSettingTaskCmd.CMD_NAME.ToString()));
                }
                //命令描述
                if (!String.IsNullOrEmpty(tWfSettingTaskCmd.CMD_NOTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CMD_NOTE = '{0}'", tWfSettingTaskCmd.CMD_NOTE.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
