using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.WF;
using i3.ValueObject;
using i3.ValueObject.Sys.General;

namespace i3.DataAccess.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例环节明细表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstTaskDetailAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstTaskDetail">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            string strSQL = "select Count(*) from T_WF_INST_TASK_DETAIL " + this.BuildWhereStatement(tWfInstTaskDetail);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstTaskDetailVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_INST_TASK_DETAIL  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TWfInstTaskDetailVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstTaskDetail">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstTaskDetailVo Details(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            string strSQL = String.Format("select * from  T_WF_INST_TASK_DETAIL " + this.BuildWhereStatement(tWfInstTaskDetail));
            return SqlHelper.ExecuteObject(new TWfInstTaskDetailVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstTaskDetail">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstTaskDetailVo> SelectByObject(TWfInstTaskDetailVo tWfInstTaskDetail, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_WF_INST_TASK_DETAIL " + this.BuildWhereStatement(tWfInstTaskDetail));
            //增加对排序的支持，流程排序一致以正序排列
            if (!string.IsNullOrEmpty(tWfInstTaskDetail.SORT_FIELD))
                strSQL += (" ORDER BY " + tWfInstTaskDetail.SORT_FIELD + " " + (string.IsNullOrEmpty(tWfInstTaskDetail.SORT_TYPE) ? " ASC " : tWfInstTaskDetail.SORT_TYPE));
            return SqlHelper.ExecuteObjectList(tWfInstTaskDetail, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象List
        /// 【大数据量慎用】
        /// </summary>
        /// <param name="tWfInstTaskDetail">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstTaskDetailVo> SelectAllByObject(TWfInstTaskDetailVo tWfInstTaskDetail)
        {

            string strSQL = String.Format("select * from  T_WF_INST_TASK_DETAIL " + this.BuildWhereStatement(tWfInstTaskDetail));

            return SqlHelper.ExecuteObjectList(tWfInstTaskDetail, strSQL);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstTaskDetail">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstTaskDetailVo tWfInstTaskDetail, int iIndex, int iCount)
        {

            string strSQL = " select * from T_WF_INST_TASK_DETAIL {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfInstTaskDetail));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstTaskDetail"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            string strSQL = "select * from T_WF_INST_TASK_DETAIL " + this.BuildWhereStatement(tWfInstTaskDetail);
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 获取指定流程实例的所有环节数据【主要用户判断是否为路由节点使用】
        /// </summary>
        /// <param name="strWFInstID"></param>
        /// <returns></returns>
        public DataTable SelectByTableForAndOr(string strWFInstID)
        {
            string strSQL = "select a.*,b.task_and_or,b.task_order from t_wf_inst_task_detail a left join t_wf_setting_task  b on a.wf_task_id = b.id where WF_INST_ID='{0}' ";
            strSQL = string.Format(strSQL, strWFInstID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据用户ID查询此用户下所有的待处理任务所在的流程实例
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strType">2A表示正常，2B表示已完成</param>
        /// <param name="iIndex">页码</param>
        /// <param name="iCount">每页数量</param>
        /// <returns></returns>
        public DataTable SelectByTableForUserDealing(string strUserID, string strType, int iIndex, int iCount)
        {
            string strSQL = "select  c.*,d.INST_TASK_CAPTION,d.INST_NOTE,d.INST_TASK_STARTTIME,d.INST_TASK_STATE,d.OBJECT_USER,D.SRC_USER from t_wf_inst_control c left join t_wf_inst_task_detail d on c.wf_task_id = d.wf_task_id and c.wf_serial_no = d.wf_serial_no where 1=1 and d.object_user like '%" + strUserID + "%' and d.inst_task_state ='" + strType + "' ";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 查询用户正在进行中的流程以及实例的数量 【和SelectByTableForUserDealing搭配使用】
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public int GetSelectResultCountForUserDealing(string strUserID, string strType)
        {
            string strSQL = "select  count(*) from t_wf_inst_control c left join t_wf_inst_task_detail d on c.wf_task_id = d.wf_task_id and c.wf_serial_no = d.wf_serial_no where 1=1 and d.object_user like '%" + strUserID + "%' and d.inst_task_state ='" + strType + "' ";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 查询所有的站务管理待处理任务所在的流程实例
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strType">2A表示正常，2B表示已完成</param>
        /// <param name="iIndex">页码</param>
        /// <param name="iCount">每页数量</param>
        /// <returns></returns>
        public DataTable SelectByTableForUserDealing_OA(string strType, int iIndex, int iCount)
        {
            string strWfType = "'WF_TEST','RPT','WF_A','SAMPLE_WT','WT_FLOW','RPT_QHD','WT_QFLOW'";
            string strSQL = @"select  c.*,d.INST_TASK_CAPTION,d.INST_NOTE,d.INST_TASK_STARTTIME,d.INST_TASK_STATE,d.OBJECT_USER,D.SRC_USER
                 from t_wf_inst_control c
                 left join t_wf_inst_task_detail d on c.wf_task_id = d.wf_task_id and c.wf_serial_no = d.wf_serial_no
                 where 1=1  and d.inst_task_state ='" + strType + "'  and c.WF_ID not in (" + strWfType + ")";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 查询所有的站务管理待处理任务所在的流程实例的数量 【和SelectByTableForUserDealing_OA搭配使用】
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public int GetSelectResultCountForUserDealing_OA(string strType)
        {
            string strWfType = "'WF_TEST','RPT','WF_A','SAMPLE_WT','WT_FLOW','RPT_QHD','WT_QFLOW'";
            string strSQL = @"select  count(*) from t_wf_inst_control c
             left join t_wf_inst_task_detail d on c.wf_task_id = d.wf_task_id and c.wf_serial_no = d.wf_serial_no
             where 1=1  and d.inst_task_state ='" + strType + "' and c.WF_ID not in (" + strWfType + ")";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 根据用户ID查询此用户下所有的待处理任务
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strType">2A表示正常，2B表示已完成</param>
        /// <returns></returns>
        public DataTable SelectByTableForUserTaskList(string strUserID, string strType, int iIndex, int iCount)
        {

            string strSQL = "select d.*,c.WF_SERVICE_NAME from T_WF_INST_TASK_DETAIL d left join  t_wf_inst_control c on d.wf_inst_id = c.id where 1=1 and d.INST_TASK_STATE='" + strType + "' and d.object_user like '%" + strUserID + "%' order by d.INST_TASK_STARTTIME ";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据用户ID查询此用户下所有的待处理任务的数量  【和SelectByTableForUserTaskList搭配使用】
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strType">2A表示正常，2B表示已完成</param>
        /// <returns></returns>
        public int GetSelectResultCountForUserTaskList(string strUserID, string strType)
        {

            string strSQL = "select count(1) from T_WF_INST_TASK_DETAIL d left join  t_wf_inst_control c on d.wf_inst_id = c.id where 1=1 and d.INST_TASK_STATE='" + strType + "' and d.object_user like '%" + strUserID + "%' ";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 根据用户ID查询此用户下所有的待处理任务
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strType">2A表示正常，2B表示已完成</param>
        /// <param name="strWF_SERVICE_NAME">任务名称</param>
        /// <param name="strSRC_USER">用户</param>
        /// <param name="strINST_TASK_STARTTIME_from">起始时间</param>
        /// <param name="strINST_TASK_STARTTIME_to">结束时间</param>
        /// <returns></returns>
        public DataTable SelectByTableForUserTaskList(string strUserID, string strType, string strWF_SERVICE_NAME, string strSRC_USER, string strINST_TASK_STARTTIME_from, string strINST_TASK_STARTTIME_to, int iIndex, int iCount)
        {

            string strSQL = "select d.*,c.WF_SERVICE_NAME,c.WF_SERVICE_CODE from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id where 1=1 and d.INST_TASK_STATE='" + strType + "' and d.object_user like '%" + strUserID + "%'  ";
            if (strWF_SERVICE_NAME.Length > 0)
                strSQL += " and c.WF_SERVICE_NAME like '%" + strWF_SERVICE_NAME + "%'";
            if (strSRC_USER.Length > 0)
                strSQL += " and d.SRC_USER in (select ID from T_SYS_USER where REAL_NAME like '%" + strSRC_USER + "%')";
            if (strINST_TASK_STARTTIME_from.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME >= '" + strINST_TASK_STARTTIME_from + " 0:00:00'";
            if (strINST_TASK_STARTTIME_to.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME <= '" + strINST_TASK_STARTTIME_to + " 23:59:59'";
            strSQL += " order by d.INST_TASK_STARTTIME DESC";

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据用户ID查询此用户下所有的待处理任务的数量(不包含退回任务)  【和SelectByTableForUserTaskList搭配使用】
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strType">2A表示正常，2B表示已完成</param>
        /// <param name="strWF_SERVICE_NAME">任务名称</param>
        /// <param name="strSRC_USER">用户</param>
        /// <param name="strINST_TASK_STARTTIME_from">起始时间</param>
        /// <param name="strINST_TASK_STARTTIME_to">结束时间</param>
        /// <returns></returns>
        public int GetSelectResultCountForUserTaskList(string strUserID, string strType, string strWF_SERVICE_NAME, string strSRC_USER, string strINST_TASK_STARTTIME_from, string strINST_TASK_STARTTIME_to)
        {

            string strSQL = "select count(1) from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id where 1=1 and isnull(d.inst_task_deal_state,'')<>'00' and d.wf_id not  in ('fw','sw')  and d.INST_TASK_STATE='" + strType + "' and d.object_user like '%" + strUserID + "%' ";
            if (strWF_SERVICE_NAME.Length > 0)
                strSQL += " and c.WF_SERVICE_NAME like '%" + strWF_SERVICE_NAME + "%'";
            if (strSRC_USER.Length > 0)
                strSQL += " and d.SRC_USER in (select ID from T_SYS_USER where REAL_NAME like '%" + strSRC_USER + "%')";
            if (strINST_TASK_STARTTIME_from.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME >= '" + strINST_TASK_STARTTIME_from + " 0:00:00'";
            if (strINST_TASK_STARTTIME_to.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME <= '" + strINST_TASK_STARTTIME_to + " 23:59:59'";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }
        /// <summary>
        /// 获取待办任务信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable Get_Waiting_TaskList(string UserID,string Status)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select d.*,c.WF_SERVICE_NAME,c.WF_SERVICE_CODE,c.WF_CAPTION from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id ");
            sb.Append(" where 1=1  and d.wf_id not  in ('fw','sw')  and d.INST_TASK_STATE='" + Status + "' and d.object_user like '%" + UserID + "%' ");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }
        /// <summary>
        /// 获取退回任务信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable Get_Return_TaskList(string UserID, string back)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select d.*,c.WF_SERVICE_NAME,c.WF_SERVICE_CODE,c.WF_CAPTION from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id ");
            sb.Append(" where 1=1  and d.wf_id not  in ('fw','sw')  and d.INST_TASK_DEAL_STATE='" + back + "' and d.object_user ='" + UserID + "' ");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }
        /// <summary>
        /// 发文待办
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable Get_FW_WaitTaskList(string UserID, string Status)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select d.*,c.WF_SERVICE_NAME,c.WF_SERVICE_CODE,c.WF_CAPTION from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id ");
            sb.Append(" where 1=1  and d.wf_id  in ('fw')  and d.INST_TASK_STATE='" + Status + "' and d.object_user like '%" + UserID + "%' ");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }
        /// <summary>
        /// 发文退回
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="back"></param>
        /// <returns></returns>
        public DataTable Get_FW_BackTaskList(string UserID, string back)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select d.*,c.WF_SERVICE_NAME,c.WF_SERVICE_CODE,c.WF_CAPTION from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id ");
            sb.Append(" where 1=1  and d.wf_id  in ('fw')  and d.INST_TASK_DEAL_STATE='" + back + "' and d.object_user ='" + UserID + "' ");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }
        /// <summary>
        /// 获取发文待办数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string Get_FW_Count(string userID, string strType)
        {
            string strSQL = "select count(*) from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id where 1=1 and d.INST_TASK_STATE='" + strType + "'  and d.wf_id   in ('fw') and d.object_user='" + userID + "'";
            return SqlHelper.ExecuteScalar(strSQL).ToString();
        }
        /// <summary>
        /// 根据用户ID【如果是上级领导用户，则可查询其下属】查询此用户下所有的非待处理，非需领取任务的数量（就是一个非2A和2D的集合）
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strWF_SERVICE_NAME">任务名称</param>
        /// <param name="strSRC_USER">用户</param>
        /// <param name="strINST_TASK_STARTTIME_from">起始时间</param>
        /// <param name="strINST_TASK_STARTTIME_to">结束时间</param>
        /// <returns></returns>
        public DataTable SelectByTableForUserTaskListEx(string strUserID, string strType, string strWF_SERVICE_NAME, string strSRC_USER, string strINST_TASK_STARTTIME_from, string strINST_TASK_STARTTIME_to, int iIndex, int iCount)
        {
            string strTypeSql = "";
            string strUserSQL = "";
            if (string.IsNullOrEmpty(strType))
            {
                strTypeSql = " not in ('2A','2D') ";
            }
            else if (strType == "XX")
            {
                strTypeSql = " in ('2A','2D') ";
            }
            else
                strTypeSql = (" = '" + strType + "'");

            //判断下属用户，由潘德军完善
            //strUserSQL = " in (" + strUserID + ") ";
            if (strUserID.Length > 0)
            {
                strUserSQL = " and (d.object_user  in ('" + strUserID + "') or d.OBJECT_USER in(select USER_ID from T_SYS_USER_PROXY where PROXY_USER_ID='" + strUserID + "'))";
            }
            //如果需要包含下属，则需要更改为：
            //strUserSQL = " in (select ID from T_SYS_USER WHERE )";

            string strSQL = "select d.*,c.WF_SERVICE_NAME,c.WF_SERVICE_CODE,c.WF_CAPTION from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id where 1=1 and d.INST_TASK_STATE " + strTypeSql + "  " + strUserSQL;
            if (strWF_SERVICE_NAME.Length > 0)
                strSQL += " and c.WF_SERVICE_NAME like '%" + strWF_SERVICE_NAME + "%'";
            if (strSRC_USER.Length > 0)
                strSQL += " and d.SRC_USER in (select ID from T_SYS_USER where REAL_NAME like '%" + strSRC_USER + "%')";
            if (strINST_TASK_STARTTIME_from.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME >= '" + strINST_TASK_STARTTIME_from + " 0:00:00'";
            if (strINST_TASK_STARTTIME_to.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME <= '" + strINST_TASK_STARTTIME_to + " 23:59:59'";
            strSQL += " order by d.INST_TASK_STARTTIME DESC";

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据用户ID【如果是上级领导用户，则可查询其下属】查询此用户下所有的非待处理，非需领取任务的数量（就是一个非2A和2D的集合）  【和SelectByTableForUserTaskListEx搭配使用】
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strWF_SERVICE_NAME">任务名称</param>
        /// <param name="strSRC_USER">用户</param>
        /// <param name="strINST_TASK_STARTTIME_from">起始时间</param>
        /// <param name="strINST_TASK_STARTTIME_to">结束时间</param>
        /// <returns></returns>
        public int GetSelectResultCountForUserTaskListEx(string strUserID, string strType, string strWF_SERVICE_NAME, string strSRC_USER, string strINST_TASK_STARTTIME_from, string strINST_TASK_STARTTIME_to)
        {
            string strTypeSql = "";
            string strUserSQL = "";
            if (string.IsNullOrEmpty(strType))
            {
                strTypeSql = " not in ('2A','2D') ";
            }
            else if (strType == "XX")
            {
                strTypeSql = " in ('2A','2D') ";
            }
            else
                strTypeSql = (" = '" + strType + "'");

            //判断下属用户，由潘德军完善
            //strUserSQL = " like '%" + strUserID + "%' ";
            if (strUserID.Length > 0)
            {
                strUserSQL = " and d.object_user  in (" + strUserID + ") ";
            }
            //如果需要包含下属，则需要更改为：
            //strUserSQL = " in (select ID from T_SYS_USER WHERE )";

            string strSQL = "select count(1) from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id where 1=1 and d.INST_TASK_STATE " + strTypeSql + " " + strUserSQL;
            if (strWF_SERVICE_NAME.Length > 0)
                strSQL += " and c.WF_SERVICE_NAME like '%" + strWF_SERVICE_NAME + "%'";
            if (strSRC_USER.Length > 0)
                strSQL += " and d.SRC_USER in (select ID from T_SYS_USER where REAL_NAME like '%" + strSRC_USER + "%')";
            if (strINST_TASK_STARTTIME_from.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME >= '" + strINST_TASK_STARTTIME_from + " 0:00:00'";
            if (strINST_TASK_STARTTIME_to.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME <= '" + strINST_TASK_STARTTIME_to + " 23:59:59'";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 根据用户ID【如果是上级领导用户，则可查询其下属】查询此用户下所有的非待处理，非需领取任务的数量（就是一个非2A和2D的集合）
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strWF_SERVICE_NAME">任务名称</param>
        /// <param name="strSRC_USER">用户</param>
        /// <param name="strINST_TASK_STARTTIME_from">起始时间</param>
        /// <param name="strINST_TASK_STARTTIME_to">结束时间</param>
        /// <returns></returns>
        public DataTable SelectByTableForUserTaskListEx_Mobile(string strUserID,string strWFID, string strType, string strWF_SERVICE_NAME, string strSRC_USER, string strINST_TASK_STARTTIME_from, string strINST_TASK_STARTTIME_to, int iIndex, int iCount)
        {
            string strTypeSql = "";
            string strUserSQL = "";
            if (string.IsNullOrEmpty(strType))
            {
                strTypeSql = " not in ('2A','2D') ";
            }
            else if (strType == "XX")
            {
                strTypeSql = " in ('2A','2D') ";
            }
            else
                strTypeSql = (" = '" + strType + "'");

            //判断下属用户，由潘德军完善
            //strUserSQL = " in (" + strUserID + ") ";
            if (strUserID.Length > 0)
            {
                strUserSQL = " and d.object_user = '" + strUserID + "' ";
            }
            //如果需要包含下属，则需要更改为：
            //strUserSQL = " in (select ID from T_SYS_USER WHERE )";

            string strSQL = "select d.ID,d.WF_INST_ID,d.WF_ID, d.WF_TASK_ID,c.WF_SERVICE_NAME,d.INST_TASK_STARTTIME as sendTime,u.REAL_NAME as sendUser";
            strSQL += " from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id left join t_sys_user u on u.id=d.SRC_USER where 1=1 and d.INST_TASK_STATE " + strTypeSql + "  " + strUserSQL;
            if (strWF_SERVICE_NAME.Length > 0)
                strSQL += " and c.WF_SERVICE_NAME like '%" + strWF_SERVICE_NAME + "%'";
            if (strSRC_USER.Length > 0)
                strSQL += " and d.SRC_USER in (select ID from T_SYS_USER where REAL_NAME like '%" + strSRC_USER + "%')";
            if (strINST_TASK_STARTTIME_from.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME >= '" + strINST_TASK_STARTTIME_from + " 0:00:00'";
            if (strINST_TASK_STARTTIME_to.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME <= '" + strINST_TASK_STARTTIME_to + " 23:59:59'";
            if (strWFID.Length > 0)
            {
                if (strWFID.Contains("|"))//最多支持2个
                {
                    string[] arrWfID = strWFID.Split('|');
                    string strSqlSub = "";
                    for (int i = 0; i < arrWfID.Length; i++)
                    {
                        strSqlSub += (i > 0 ? " or " : "") + "d.WF_ID = '" + arrWfID[i] + "'";
                    }
                    if (strSqlSub.Length>0)
                        strSQL += " and (" + strSqlSub + ")";
                }
                else
                    strSQL += " and d.WF_ID = '" + strWFID + "'";
            }
            strSQL += " order by d.INST_TASK_STARTTIME DESC";

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据用户ID【如果是上级领导用户，则可查询其下属】查询此用户下所有的非待处理，非需领取任务的数量（就是一个非2A和2D的集合）  【和SelectByTableForUserTaskListEx搭配使用】
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strWF_SERVICE_NAME">任务名称</param>
        /// <param name="strSRC_USER">用户</param>
        /// <param name="strINST_TASK_STARTTIME_from">起始时间</param>
        /// <param name="strINST_TASK_STARTTIME_to">结束时间</param>
        /// <returns></returns>
        public int GetSelectResultCountForUserTaskListEx_Mobile(string strUserID, string strWFID, string strType, string strWF_SERVICE_NAME, string strSRC_USER, string strINST_TASK_STARTTIME_from, string strINST_TASK_STARTTIME_to)
        {
            string strTypeSql = "";
            string strUserSQL = "";
            if (string.IsNullOrEmpty(strType))
            {
                strTypeSql = " not in ('2A','2D') ";
            }
            else if (strType == "XX")
            {
                strTypeSql = " in ('2A','2D') ";
            }
            else
                strTypeSql = (" = '" + strType + "'");

            //判断下属用户，由潘德军完善
            //strUserSQL = " in (" + strUserID + ") ";
            if (strUserID.Length > 0)
            {
                strUserSQL = " and d.object_user ='" + strUserID + "' ";
            }
            //如果需要包含下属，则需要更改为：
            //strUserSQL = " in (select ID from T_SYS_USER WHERE )";

            string strSQL = "select count(*)";
            strSQL += " from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id left join t_sys_user u on u.id=d.SRC_USER where 1=1 and d.INST_TASK_STATE " + strTypeSql + "  " + strUserSQL;
            if (strWF_SERVICE_NAME.Length > 0)
                strSQL += " and c.WF_SERVICE_NAME like '%" + strWF_SERVICE_NAME + "%'";
            if (strSRC_USER.Length > 0)
                strSQL += " and d.SRC_USER in (select ID from T_SYS_USER where REAL_NAME like '%" + strSRC_USER + "%')";
            if (strINST_TASK_STARTTIME_from.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME >= '" + strINST_TASK_STARTTIME_from + " 0:00:00'";
            if (strINST_TASK_STARTTIME_to.Length > 0)
                strSQL += " and d.INST_TASK_STARTTIME <= '" + strINST_TASK_STARTTIME_to + " 23:59:59'";
            if (strWFID.Length > 0)
            {
                if (strWFID.Contains("|"))//最多支持2个
                {
                    string[] arrWfID = strWFID.Split('|');
                    string strSqlSub = "";
                    for (int i = 0; i < arrWfID.Length; i++)
                    {
                        strSqlSub += (i > 0 ? " or " : "") + "d.WF_ID = '" + arrWfID[i] + "'";
                    }
                    if (strSqlSub.Length > 0)
                        strSQL += " and (" + strSqlSub + ")";
                }
                else
                    strSQL += " and d.WF_ID = '" + strWFID + "'";
            }
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstTaskDetail">对象</param>
        /// <returns></returns>
        public TWfInstTaskDetailVo SelectByObject(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            string strSQL = "select * from T_WF_INST_TASK_DETAIL " + this.BuildWhereStatement(tWfInstTaskDetail);
            return SqlHelper.ExecuteObject(new TWfInstTaskDetailVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfInstTaskDetail">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfInstTaskDetail, TWfInstTaskDetailVo.T_WF_INST_TASK_DETAIL_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskDetail">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstTaskDetail, TWfInstTaskDetailVo.T_WF_INST_TASK_DETAIL_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfInstTaskDetail.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskDetail_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstTaskDetail_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskDetailVo tWfInstTaskDetail_UpdateSet, TWfInstTaskDetailVo tWfInstTaskDetail_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstTaskDetail_UpdateSet, TWfInstTaskDetailVo.T_WF_INST_TASK_DETAIL_TABLE);
            strSQL += this.BuildWhereStatement(tWfInstTaskDetail_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_INST_TASK_DETAIL where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            string strSQL = "delete from T_WF_INST_TASK_DETAIL ";
            strSQL += this.BuildWhereStatement(tWfInstTaskDetail);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 功能描述：获取指定时间范围内的任务
        /// 创建时间：2012-12-23
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tWfInstTaskDetail">任务对象</param>
        /// <param name="strINST_TASK_STARTTIME_from">时间上限</param>
        /// <param name="strINST_TASK_STARTTIME_to">时间下限</param>
        /// <returns></returns>
        public int GetSelectResultCountForDayTaskList(TWfInstTaskDetailVo tWfInstTaskDetail, string strINST_TASK_STARTTIME_from, string strINST_TASK_STARTTIME_to)
        {
            string strSQL = "select Count(*) from T_WF_INST_TASK_DETAIL " + this.BuildWhereStatement(tWfInstTaskDetail);

            if (strINST_TASK_STARTTIME_from.Length > 0)
                strSQL += " and INST_TASK_STARTTIME >= '" + strINST_TASK_STARTTIME_from + " 0:00:00'";
            if (strINST_TASK_STARTTIME_to.Length > 0)
                strSQL += " and INST_TASK_STARTTIME <= '" + strINST_TASK_STARTTIME_to + " 23:59:59'";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 功能描述：获取某一业务单对应所有相关的工作流属性
        /// 包括ID:WF_INST_DETAIL的ID，CONTROL_ID:WF_INST_CONTROL的ID（同一业务流程所有环节相同）,OBJECT_USER:接收人,REAL_USER:实际处理人,SRC_USER:发送人,WF_ID:流程名,INST_TASK_CAPTION:环节名称
        /// INST_TASK_STARTTIME:环节开始时间,INST_TASK_ENDTIME：环节结束时间，INST_TASK_STATE：环节执行状态（2A：待办，2B：已办）
        /// TASK_ORDER:环节顺序（开始环节取最小数，结束环节取最大数）
        /// 创建时间：2013-1-8
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tWfInstTaskDetail">实例对象</param>
        /// <param name="strServiceName">业务相关名称，如Task_ID</param>
        /// <param name="strServiceValue">业务相关值，如委托书ID或监测任务ID</param>
        /// <returns></returns>
        public DataTable GetWFDetailByBusinessInfo(TWfInstTaskDetailVo tWfInstTaskDetail, string strServiceName, string strServiceValue)
        {
            string strSQL = @"select control.ID as CONTROL_ID,detail.ID,detail.OBJECT_USER,detail.REAL_USER,detail.SRC_USER,detail.WF_ID,detail.INST_TASK_CAPTION,
                                                detail.INST_TASK_STARTTIME,detail.INST_TASK_ENDTIME,detail.INST_TASK_STATE,task.TASK_ORDER
                                                from ({0}) detail
                                                JOIN T_WF_SETTING_TASK task ON detail.WF_TASK_ID=task.ID
                                                JOIN T_WF_INST_CONTROL control ON detail.WF_INST_ID=control.ID";

            string strDetail = "select * from T_WF_INST_TASK_DETAIL ";
            strDetail += BuildWhereStatement(tWfInstTaskDetail);
            if (!string.IsNullOrEmpty(strServiceName) && !string.IsNullOrEmpty(strServiceValue))
            {
                strDetail += string.Format(" and ID in (select WF_INST_TASK_ID from T_WF_INST_TASK_SERVICE where SERVICE_KEY_NAME='{0}' and SERVICE_KEY_VALUE='{1}')", strServiceName, strServiceValue);
            }

            strSQL = string.Format(strSQL, strDetail);
            strSQL = string.Format(strSQL, BuildWhereStatement(tWfInstTaskDetail));
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region//短信自动提醒功能（郑州）
        /// <summary>
        /// 获取待处理任务人员
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public DataTable GetUserList(TSysUserVo userInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select d.object_user,a.real_name");
            sb.Append(" from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id left join T_SYS_USER a on d.object_user=a.iD");
            sb.Append(" where 1=1 and d.INST_TASK_STATE = '2A' and  d.object_user !='null' and a.IS_DEL='" + userInfo.IS_DEL + "' and a.IS_USE ='" + userInfo.IS_USE + "'   ");
            sb.Append(" group by d.object_user,a.real_name ");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }
        /// <summary>
        /// 获取未处理的流程
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnTreatInfo(TSysUserVo userInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select d.object_user,a.real_name,c.wf_service_name");
            sb.Append(" from T_WF_INST_TASK_DETAIL d join  t_wf_inst_control c on d.wf_inst_id = c.id left join T_SYS_USER a on d.object_user=a.iD");
            sb.Append(" where 1=1 and d.INST_TASK_STATE = '2A' and c.wf_service_name is not null and  d.object_user !='null' and a.IS_DEL='" + userInfo.IS_DEL + "' and a.IS_USE ='" + userInfo.IS_USE + "'   ");
            sb.Append(" group by d.object_user,a.real_name,c.wf_service_name ");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }
        //代办收文
        public DataTable GetUnTreatSW()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select bb.ID,SEND_USER,TASK_NAME,TASK_STATUS ");
            sb.Append("  from(select a.ID,'SW'+replace(replace(replace(CONVERT(varchar, a.SW_REG_DATE, 120 ),'-',''),' ',''),':','') TASK_CODE,''''+a.SW_TITLE+'''公文收取' TASK_NAME,a.SW_STATUS ");
            sb.Append(" TASK_STATUS,c.REAL_NAME SEND_USER,b.STR_DATE SEND_DATE from T_OA_SW_INFO a inner join T_OA_SW_HANDLE b on(a.ID=b.SW_ID and a.SW_STATUS=b.");
            sb.Append(" SW_HANDER) left join T_SYS_USER c on(b.STR_USERID=c.ID) where (b.IS_OK='0' or b.IS_OK='2')  ) aa left join  dbo.T_SYS_USER bb on aa.SEND_USER=bb.REAL_NAME");
            sb.Append("   where 1=1  and send_user!=''");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }
        //采样任务列表
        public DataTable SamplingList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT   t1.SAMPLING_MANAGER_ID as SAMPLING_MANAGER_ID,bb.REAL_NAME, t2.PROJECT_NAME ");
            sb.Append(" FROM  T_MIS_MONITOR_SUBTASK AS t1 INNER JOIN  T_MIS_MONITOR_TASK AS t2 ON t1.TASK_ID = t2.ID ");
            sb.Append(" INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO TM ON TM.ID=t1.MONITOR_ID ");
            sb.Append("  left join dbo.T_SYS_USER bb on t1.SAMPLING_MANAGER_ID=bb.ID ");
            sb.Append("  where 1=1  and t1.TASK_STATUS='02' and t1.SAMPLING_MANAGER_ID !='' ");
            return SqlHelper.ExecuteDataTable(sb.ToString());
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfInstTaskDetail"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfInstTaskDetail)
            {

                //环节实例编号
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfInstTaskDetail.ID.ToString()));
                }
                //流程实例编号
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.WF_INST_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_INST_ID = '{0}'", tWfInstTaskDetail.WF_INST_ID.ToString()));
                }
                //流水号
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.WF_SERIAL_NO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_SERIAL_NO = '{0}'", tWfInstTaskDetail.WF_SERIAL_NO.ToString()));
                }
                //流程编号
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.WF_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_ID = '{0}'", tWfInstTaskDetail.WF_ID.ToString()));
                }
                //环节编号
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.WF_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_TASK_ID = '{0}'", tWfInstTaskDetail.WF_TASK_ID.ToString()));
                }
                //上环节实例编号
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.PRE_INST_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PRE_INST_TASK_ID = '{0}'", tWfInstTaskDetail.PRE_INST_TASK_ID.ToString()));
                }
                //上环节编号
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.PRE_TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PRE_TASK_ID = '{0}'", tWfInstTaskDetail.PRE_TASK_ID.ToString()));
                }
                //此环节简述
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.INST_TASK_CAPTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INST_TASK_CAPTION = '{0}'", tWfInstTaskDetail.INST_TASK_CAPTION.ToString()));
                }
                //此节点详细描述
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.INST_NOTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INST_NOTE = '{0}'", tWfInstTaskDetail.INST_NOTE.ToString()));
                }
                //此环节开始时间
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.INST_TASK_STARTTIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INST_TASK_STARTTIME = '{0}'", tWfInstTaskDetail.INST_TASK_STARTTIME.ToString()));
                }
                //此环节结束时间
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.INST_TASK_ENDTIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INST_TASK_ENDTIME = '{0}'", tWfInstTaskDetail.INST_TASK_ENDTIME.ToString()));
                }
                //此环节状态 
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.INST_TASK_STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INST_TASK_STATE = '{0}'", tWfInstTaskDetail.INST_TASK_STATE.ToString()));
                }
                //环节处理状态
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.INST_TASK_DEAL_STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INST_TASK_DEAL_STATE = '{0}'", tWfInstTaskDetail.INST_TASK_DEAL_STATE.ToString()));
                }
                //目标操作人
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.OBJECT_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OBJECT_USER = '{0}'", tWfInstTaskDetail.OBJECT_USER.ToString()));
                }
                //实际操作人
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.REAL_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAL_USER = '{0}'", tWfInstTaskDetail.REAL_USER.ToString()));
                }
                //发送人
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.SRC_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SRC_USER = '{0}'", tWfInstTaskDetail.SRC_USER.ToString()));
                }
                //环节提示信息
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.INST_TASK_MSG.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INST_TASK_MSG = '{0}'", tWfInstTaskDetail.INST_TASK_MSG.ToString()));
                }
                //是否超时
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.IS_OVERTIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_OVERTIME = '{0}'", tWfInstTaskDetail.IS_OVERTIME.ToString()));
                }
                //是否提醒
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.IS_REMIND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_REMIND = '{0}'", tWfInstTaskDetail.IS_REMIND.ToString()));
                }

                //确认人
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.CFM_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CFM_USER = '{0}'", tWfInstTaskDetail.CFM_USER.ToString()));
                }
                //确认时间
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.CFM_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CFM_TIME = '{0}'", tWfInstTaskDetail.CFM_TIME.ToString()));
                }
                //撤销时间
                if (!String.IsNullOrEmpty(tWfInstTaskDetail.CFM_UNTIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CFM_UNTIME = '{0}'", tWfInstTaskDetail.CFM_UNTIME.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
