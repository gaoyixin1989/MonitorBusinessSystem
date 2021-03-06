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
    /// 功能：工作流实例控制表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstControlAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstControlVo tWfInstControl)
        {
            string strSQL = "select Count(*) from T_WF_INST_CONTROL " + this.BuildWhereStatement(tWfInstControl);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstControlVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_INST_CONTROL  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TWfInstControlVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstControl">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstControlVo Details(TWfInstControlVo tWfInstControl)
        {
            string strSQL = String.Format("select * from  T_WF_INST_CONTROL " + this.BuildWhereStatement(tWfInstControl));
            return SqlHelper.ExecuteObject(new TWfInstControlVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstControlVo> SelectByObject(TWfInstControlVo tWfInstControl, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_WF_INST_CONTROL " + this.BuildWhereStatement(tWfInstControl));
            return SqlHelper.ExecuteObjectList(tWfInstControl, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstControlVo tWfInstControl, int iIndex, int iCount)
        {

            string strSQL = " select * from T_WF_INST_CONTROL {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfInstControl));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable【主要用户销毁实例流程使用】
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableForClear(TWfInstControlVo tWfInstControl, int iIndex, int iCount)
        {
            //不等于1G和1H的所有流程都可以销毁
            string strSQL = " select * from T_WF_INST_CONTROL " + BuildWhereStatement(tWfInstControl) + " and  wf_state <> '1G'  and wf_state <> '1H'  and wf_state <> '1F'  ";

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }


        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstControl"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstControlVo tWfInstControl)
        {
            string strSQL = "select * from T_WF_INST_CONTROL " + this.BuildWhereStatement(tWfInstControl);
            return SqlHelper.ExecuteDataTable(strSQL);
        }



        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <returns></returns>
        public TWfInstControlVo SelectByObject(TWfInstControlVo tWfInstControl)
        {
            string strSQL = "select * from T_WF_INST_CONTROL " + this.BuildWhereStatement(tWfInstControl);
            return SqlHelper.ExecuteObject(new TWfInstControlVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstControlVo tWfInstControl)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfInstControl, TWfInstControlVo.T_WF_INST_CONTROL_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstControl">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstControlVo tWfInstControl)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstControl, TWfInstControlVo.T_WF_INST_CONTROL_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfInstControl.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstControl_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstControl_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstControlVo tWfInstControl_UpdateSet, TWfInstControlVo tWfInstControl_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstControl_UpdateSet, TWfInstControlVo.T_WF_INST_CONTROL_TABLE);
            strSQL += this.BuildWhereStatement(tWfInstControl_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_INST_CONTROL where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfInstControlVo tWfInstControl)
        {
            string strSQL = "delete from T_WF_INST_CONTROL ";
            strSQL += this.BuildWhereStatement(tWfInstControl);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfInstControl"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfInstControlVo tWfInstControl)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfInstControl)
            {

                //流程实例编号
                if (!String.IsNullOrEmpty(tWfInstControl.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfInstControl.ID.ToString()));
                }
                //流程编号
                if (!String.IsNullOrEmpty(tWfInstControl.WF_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_ID = '{0}'", tWfInstControl.WF_ID.ToString()));
                }
                //流水号
                if (!String.IsNullOrEmpty(tWfInstControl.WF_SERIAL_NO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_SERIAL_NO = '{0}'", tWfInstControl.WF_SERIAL_NO.ToString()));
                }
                //当前环节编号
                if (!String.IsNullOrEmpty(tWfInstControl.WF_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_TASK_ID = '{0}'", tWfInstControl.WF_TASK_ID.ToString()));
                }
                //当前实例环节编号
                if (!String.IsNullOrEmpty(tWfInstControl.WF_INST_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_INST_TASK_ID = '{0}'", tWfInstControl.WF_INST_TASK_ID.ToString()));
                }
                //流程简述
                if (!String.IsNullOrEmpty(tWfInstControl.WF_CAPTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_CAPTION = '{0}'", tWfInstControl.WF_CAPTION.ToString()));
                }
                //流程备注
                if (!String.IsNullOrEmpty(tWfInstControl.WF_NOTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_NOTE = '{0}'", tWfInstControl.WF_NOTE.ToString()));
                }

                //业务编码
                if (!String.IsNullOrEmpty(tWfInstControl.WF_SERVICE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_SERVICE_CODE = '{0}'", tWfInstControl.WF_SERVICE_CODE.ToString()));
                }
                //业务名称
                if (!String.IsNullOrEmpty(tWfInstControl.WF_SERVICE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_SERVICE_NAME = '{0}'", tWfInstControl.WF_SERVICE_NAME.ToString()));
                }
                //优先级
                if (!String.IsNullOrEmpty(tWfInstControl.WF_PRIORITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_PRIORITY = '{0}'", tWfInstControl.WF_PRIORITY.ToString()));
                }
                //流程状态
                if (!String.IsNullOrEmpty(tWfInstControl.WF_STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_STATE = '{0}'", tWfInstControl.WF_STATE.ToString()));
                }
                //开始时间
                if (!String.IsNullOrEmpty(tWfInstControl.WF_STARTTIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_STARTTIME = '{0}'", tWfInstControl.WF_STARTTIME.ToString()));
                }
                //约定结束时间
                if (!String.IsNullOrEmpty(tWfInstControl.WF_ENDTIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_ENDTIME = '{0}'", tWfInstControl.WF_ENDTIME.ToString()));
                }
                //挂起时间
                if (!String.IsNullOrEmpty(tWfInstControl.WF_SUSPEND_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_SUSPEND_TIME = '{0}'", tWfInstControl.WF_SUSPEND_TIME.ToString()));
                }
                //挂起状态
                if (!String.IsNullOrEmpty(tWfInstControl.WF_SUSPEND_STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_SUSPEND_STATE = '{0}'", tWfInstControl.WF_SUSPEND_STATE.ToString()));
                }
                //挂起的结束时间
                if (!String.IsNullOrEmpty(tWfInstControl.WF_SUSPEND_ENDTIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_SUSPEND_ENDTIME = '{0}'", tWfInstControl.WF_SUSPEND_ENDTIME.ToString()));
                }
                //是否子流程
                if (!String.IsNullOrEmpty(tWfInstControl.IS_SUB_FLOW.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_SUB_FLOW = '{0}'", tWfInstControl.IS_SUB_FLOW.ToString()));
                }
                //父流程实例编号
                if (!String.IsNullOrEmpty(tWfInstControl.PARENT_INST_FLOW_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PARENT_INST_FLOW_ID = '{0}'", tWfInstControl.PARENT_INST_FLOW_ID.ToString()));
                }
                //父流程编号
                if (!String.IsNullOrEmpty(tWfInstControl.PARENT_FLOW_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PARENT_FLOW_ID = '{0}'", tWfInstControl.PARENT_FLOW_ID.ToString()));
                }
                //父流程环节实例编号
                if (!String.IsNullOrEmpty(tWfInstControl.PARENT_INST_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PARENT_INST_TASK_ID = '{0}'", tWfInstControl.PARENT_INST_TASK_ID.ToString()));
                }
                //父流程环节编号
                if (!String.IsNullOrEmpty(tWfInstControl.PARENT_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PARENT_TASK_ID = '{0}'", tWfInstControl.PARENT_TASK_ID.ToString()));
                }
                //其他备注
                if (!String.IsNullOrEmpty(tWfInstControl.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tWfInstControl.REMARK.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
