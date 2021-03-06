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
    /// 功能：流程节点集合表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingTaskAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingTaskVo tWfSettingTask)
        {
            string strSQL = "select Count(*) from T_WF_SETTING_TASK " + this.BuildWhereStatement(tWfSettingTask);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingTaskVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_TASK  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TWfSettingTaskVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingTask">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingTaskVo Details(TWfSettingTaskVo tWfSettingTask)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_TASK " + this.BuildWhereStatement(tWfSettingTask));
            return SqlHelper.ExecuteObject(new TWfSettingTaskVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskVo> SelectByObject(TWfSettingTaskVo tWfSettingTask, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_WF_SETTING_TASK " + this.BuildWhereStatement(tWfSettingTask));
            return SqlHelper.ExecuteObjectList(tWfSettingTask, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象List
        /// 此方法可用排序
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskVo> SelectByObjectList(TWfSettingTaskVo tWfSettingTask)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_TASK " + this.BuildWhereStatement(tWfSettingTask));
            if (!string.IsNullOrEmpty(tWfSettingTask.SORT_FIELD))
            {
                strSQL += " ORDER BY " + tWfSettingTask.SORT_FIELD + "" + tWfSettingTask.SORT_TYPE + " ";
            }
            return SqlHelper.ExecuteObjectList(tWfSettingTask, strSQL);

        }

        /// <summary>
        /// 根据WF_ID来获取对象List，主要为获取第一个其实节点设置
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskVo> SelectByObjectListForSetp(TWfSettingTaskVo tWfSettingTask)
        {
            string strSQL = String.Format("select * from  T_WF_SETTING_TASK " + this.BuildWhereStatement(tWfSettingTask));
            strSQL += " ORDER BY cast(task_order  as int)  asc ";
            return SqlHelper.ExecuteObjectList(tWfSettingTask, strSQL);

        }


        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingTaskVo tWfSettingTask, int iIndex, int iCount)
        {
            string strSQL = " select * from T_WF_SETTING_TASK {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfSettingTask));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(tWfSettingTask, strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingTask"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingTaskVo tWfSettingTask)
        {
            string strSQL = "select * from T_WF_SETTING_TASK " + this.BuildWhereStatement(tWfSettingTask);
            if (tWfSettingTask.SORT_FIELD.Trim() != "")
            {
                strSQL += string.Format(" ORDER BY  {0}  {1} ", tWfSettingTask.SORT_FIELD, tWfSettingTask.SORT_TYPE);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        //搜索指定流程的所有环节
        public DataTable SelectByTable_byWFID(string strWF_ID)
        {
            string strSQL = "select * from T_WF_SETTING_TASK Where WF_ID in (" + strWF_ID + ") order by WF_ID,task_order";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <returns></returns>
        public TWfSettingTaskVo SelectByObject(TWfSettingTaskVo tWfSettingTask)
        {
            string strSQL = "select * from T_WF_SETTING_TASK " + this.BuildWhereStatement(tWfSettingTask);
            return SqlHelper.ExecuteObject(new TWfSettingTaskVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingTaskVo tWfSettingTask)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfSettingTask, TWfSettingTaskVo.T_WF_SETTING_TASK_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTask">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskVo tWfSettingTask)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingTask, TWfSettingTaskVo.T_WF_SETTING_TASK_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfSettingTask.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTask_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingTask_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskVo tWfSettingTask_UpdateSet, TWfSettingTaskVo tWfSettingTask_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfSettingTask_UpdateSet, TWfSettingTaskVo.T_WF_SETTING_TASK_TABLE);
            strSQL += this.BuildWhereStatement(tWfSettingTask_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_SETTING_TASK where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfSettingTaskVo tWfSettingTask)
        {
            string strSQL = "delete from T_WF_SETTING_TASK ";
            strSQL += this.BuildWhereStatement(tWfSettingTask);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfSettingTask"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfSettingTaskVo tWfSettingTask)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfSettingTask)
            {

                //编号
                if (!String.IsNullOrEmpty(tWfSettingTask.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfSettingTask.ID.ToString()));
                }
                //节点编号
                if (!String.IsNullOrEmpty(tWfSettingTask.WF_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_TASK_ID = '{0}'", tWfSettingTask.WF_TASK_ID.ToString()));
                }
                //流程编号
                if (!String.IsNullOrEmpty(tWfSettingTask.WF_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_ID = '{0}'", tWfSettingTask.WF_ID.ToString()));
                }
                //节点类型
                if (!String.IsNullOrEmpty(tWfSettingTask.TASK_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_TYPE = '{0}'", tWfSettingTask.TASK_TYPE.ToString()));
                }
                //节点简称
                if (!String.IsNullOrEmpty(tWfSettingTask.TASK_CAPTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_CAPTION = '{0}'", tWfSettingTask.TASK_CAPTION.ToString()));
                }
                //节点描述
                if (!String.IsNullOrEmpty(tWfSettingTask.TASK_NOTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_NOTE = '{0}'", tWfSettingTask.TASK_NOTE.ToString()));
                }
                //节点与非类型
                if (!String.IsNullOrEmpty(tWfSettingTask.TASK_AND_OR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_AND_OR = '{0}'", tWfSettingTask.TASK_AND_OR.ToString()));
                }
                //操作人类型
                if (!String.IsNullOrEmpty(tWfSettingTask.OPER_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OPER_TYPE = '{0}'", tWfSettingTask.OPER_TYPE.ToString()));
                }
                //操作人值
                if (!String.IsNullOrEmpty(tWfSettingTask.OPER_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OPER_VALUE = '{0}'", tWfSettingTask.OPER_VALUE.ToString()));
                }
                //命令名称
                if (!String.IsNullOrEmpty(tWfSettingTask.COMMAND_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMMAND_NAME = '{0}'", tWfSettingTask.COMMAND_NAME.ToString()));
                }
                //附加功能
                if (!String.IsNullOrEmpty(tWfSettingTask.FUNCTION_LIST.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUNCTION_LIST = '{0}'", tWfSettingTask.FUNCTION_LIST.ToString()));
                }
                //节点排序
                if (!String.IsNullOrEmpty(tWfSettingTask.TASK_ORDER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_ORDER = '{0}'", tWfSettingTask.TASK_ORDER.ToString()));
                }
                //跳过自己
                if (!String.IsNullOrEmpty(tWfSettingTask.SELF_DEAL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SELF_DEAL = '{0}'", tWfSettingTask.SELF_DEAL.ToString()));
                }
                //绘图X坐标
                if (!String.IsNullOrEmpty(tWfSettingTask.POSITION_IX.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POSITION_IX = '{0}'", tWfSettingTask.POSITION_IX.ToString()));
                }
                //绘图Y坐标
                if (!String.IsNullOrEmpty(tWfSettingTask.POSITION_IY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POSITION_IY = '{0}'", tWfSettingTask.POSITION_IY.ToString()));
                }

            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
