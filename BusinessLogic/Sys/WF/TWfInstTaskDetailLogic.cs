using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.WF;
using i3.DataAccess.Sys.WF;
using i3.ValueObject.Sys.General;

namespace i3.BusinessLogic.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例环节明细表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstTaskDetailLogic : LogicBase
    {

        TWfInstTaskDetailVo tWfInstTaskDetail = new TWfInstTaskDetailVo();
        TWfInstTaskDetailAccess access;



        #region 基础获取ID和时间的方法
        /// <summary>
        /// 获得GUID编号
        /// </summary>
        /// <returns></returns>
        private string GetGUID()
        {
            long i = 1;
            Guid guid = Guid.NewGuid();
            foreach (byte b in guid.ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks).ToUpper();
        }
        /// <summary>
        /// 获得标准时间格式字符串
        /// </summary>
        /// <returns></returns>
        private string GetDateTimeToStanString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 获得20121105182451098的时间形式字符
        /// </summary>
        /// <returns></returns>
        private string GetDateTimeToStringFor17()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString().PadLeft(3, '0');
        }

        /// <summary>
        /// 获得121105182451098的时间形式字符
        /// </summary>
        /// <returns></returns>
        private string GetDateTimeToStringFor15()
        {
            return DateTime.Now.ToString("yyMMddHHmmss") + DateTime.Now.Millisecond.ToString().PadLeft(3, '0');
        }

        /// <summary>
        /// 指定用GUID方式或者时间方式来生成编号，0为16位GUID，1为15位时间编码,2为17位时间编码，其他情况返回""
        /// </summary>
        /// <param name="strType">输入类别</param>
        /// <returns></returns>
        public string CreatInstFlowID(string strType)
        {
            if (strType == "0")
                return this.GetGUID();
            else if (strType == "1")
                return this.GetDateTimeToStringFor15();
            else if (strType == "2")
                return this.GetDateTimeToStringFor17();
            return "";
        }


        #endregion



        public TWfInstTaskDetailLogic()
        {
            access = new TWfInstTaskDetailAccess();
        }

        public TWfInstTaskDetailLogic(TWfInstTaskDetailVo _tWfInstTaskDetail)
        {
            tWfInstTaskDetail = _tWfInstTaskDetail;
            access = new TWfInstTaskDetailAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstTaskDetail">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            return access.GetSelectResultCount(tWfInstTaskDetail);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstTaskDetailVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstTaskDetail">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstTaskDetailVo Details(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            return access.Details(tWfInstTaskDetail);
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
            return access.SelectByObject(tWfInstTaskDetail, iIndex, iCount);

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
            return access.SelectAllByObject(tWfInstTaskDetail);
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
            return access.SelectByTable(tWfInstTaskDetail, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstTaskDetail"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            return access.SelectByTable(tWfInstTaskDetail);
        }

        /// <summary>
        /// 根据用户ID查询此用户下所有的待处理任务
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strType">2A,2B</param>
        /// <returns></returns>
        public DataTable SelectByTableForUserTaskList(string strUserID, string strType, int iIndex, int iCount)
        {
            return access.SelectByTableForUserTaskList(strUserID, strType, iIndex, iCount);
        }

        /// <summary>
        /// 根据用户ID查询此用户下所有的待处理任务的数量  【和SelectByTableForUserTaskList搭配使用】
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strType">2A表示正常，2B表示已完成</param>
        /// <returns></returns>
        public int GetSelectResultCountForUserTaskList(string strUserID, string strType)
        {
            return access.GetSelectResultCountForUserTaskList(strUserID, strType);
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
            return access.SelectByTableForUserTaskList(strUserID, strType, strWF_SERVICE_NAME, strSRC_USER, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to, iIndex, iCount);
        }

        /// <summary>
        /// 根据用户ID查询此用户下所有的待处理任务的数量  【和SelectByTableForUserTaskList搭配使用】
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
            return access.GetSelectResultCountForUserTaskList(strUserID, strType, strWF_SERVICE_NAME, strSRC_USER, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to);
        }
        /// <summary>
        /// 获取待办任务信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable Get_Waiting_TaskList(string UserID, string Status)
        {
            return access.Get_Waiting_TaskList(UserID, Status);
        }
        /// <summary>
        /// 发文待办
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable Get_FW_WaitTaskList(string UserID, string Status)
        {
            return access.Get_FW_WaitTaskList(UserID, Status);
        }
        /// <summary>
        /// 发文退回
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="back"></param>
        /// <returns></returns>
        public DataTable Get_FW_BackTaskList(string UserID, string back)
        {
            return access.Get_FW_BackTaskList(UserID, back);
        }
              /// <summary>
        /// 获取退回任务信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable Get_Return_TaskList(string UserID, string back)
        {
            return access.Get_Return_TaskList(UserID, back);
        }
                /// <summary>
        /// 获取发文待办数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string Get_FW_Count(string userID, string strType)
        {
            return access.Get_FW_Count(userID, strType);
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
            return access.SelectByTableForUserTaskListEx(strUserID, strType, strWF_SERVICE_NAME, strSRC_USER, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to, iIndex, iCount);
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
            return access.GetSelectResultCountForUserTaskListEx(strUserID, strType, strWF_SERVICE_NAME, strSRC_USER, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to);
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
        public DataTable SelectByTableForUserTaskListEx_Mobile(string strUserID, string strWFID, string strType, string strWF_SERVICE_NAME, string strSRC_USER, string strINST_TASK_STARTTIME_from, string strINST_TASK_STARTTIME_to, int iIndex, int iCount)
        {
            return access.SelectByTableForUserTaskListEx_Mobile(strUserID, strWFID, strType, strWF_SERVICE_NAME, strSRC_USER, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to, iIndex, iCount);
        }

        public int GetSelectResultCountForUserTaskListEx_Mobile(string strUserID, string strWFID, string strType, string strWF_SERVICE_NAME, string strSRC_USER, string strINST_TASK_STARTTIME_from, string strINST_TASK_STARTTIME_to)
        {
            return access.GetSelectResultCountForUserTaskListEx_Mobile(strUserID, strWFID, strType, strWF_SERVICE_NAME, strSRC_USER, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to);
        }

        /// <summary>
        /// 获取指定流程实例的所有环节数据【主要用户判断是否为路由节点使用】
        /// </summary>
        /// <param name="strWFInstID"></param>
        /// <returns></returns>
        public DataTable SelectByTableForAndOr(string strWFInstID)
        {
            return access.SelectByTableForAndOr(strWFInstID);
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
            return access.SelectByTableForUserDealing(strUserID, strType, iIndex, iCount);
        }

        /// <summary>
        /// 查询用户正在进行中的流程以及实例的数量 【和SelectByTableForUserDealing搭配使用】
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public int GetSelectResultCountForUserDealing(string strUserID, string strType)
        {
            return access.GetSelectResultCountForUserDealing(strUserID, strType);

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
            return access.SelectByTableForUserDealing_OA(strType, iIndex, iCount);
        }
        /// <summary>
        /// 查询所有的站务管理待处理任务所在的流程实例的数量 【和SelectByTableForUserDealing_OA搭配使用】
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public int GetSelectResultCountForUserDealing_OA(string strType)
        {
            return access.GetSelectResultCountForUserDealing_OA( strType);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstTaskDetail">对象</param>
        /// <returns></returns>
        public TWfInstTaskDetailVo SelectByObject(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            return access.SelectByObject(tWfInstTaskDetail);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            return access.Create(tWfInstTaskDetail);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskDetail">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            return access.Edit(tWfInstTaskDetail);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskDetail_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstTaskDetail_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskDetailVo tWfInstTaskDetail_UpdateSet, TWfInstTaskDetailVo tWfInstTaskDetail_UpdateWhere)
        {
            return access.Edit(tWfInstTaskDetail_UpdateSet, tWfInstTaskDetail_UpdateWhere);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            return access.Delete(Id);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfInstTaskDetailVo tWfInstTaskDetail)
        {
            return access.Delete(tWfInstTaskDetail);
        }


        /// <summary>
        /// 根据实例环节的编号，获取前一环节实例对象【前一环节】
        /// </summary>
        /// <param name="strInstTaskID"></param>
        /// <returns></returns>
        public TWfInstTaskDetailVo GetPreInstTask(string strInstTaskID)
        {
            return access.Details(access.Details(strInstTaskID).PRE_INST_TASK_ID);
        }

        /// <summary>
        /// 根据实例环节的编号，获取后一环节实例对象【后一环节】
        /// </summary>
        /// <param name="strInstTaskID"></param>
        /// <returns></returns>
        public TWfInstTaskDetailVo GetNextInstTask(string strInstTaskID)
        {
            return access.Details(new TWfInstTaskDetailVo() { PRE_INST_TASK_ID = strInstTaskID });
        }


        /// <summary>
        /// 根据实例环节获取环节配置数据，再查看其是否为起始环节【根据实例环节编号间接判断是否为起始环节】
        /// </summary>
        /// <param name="strInstStepID">实例环节ID</param>
        /// <returns></returns>
        public bool IsStartStep(string strInstStepID)
        {
            TWfSettingTaskLogic logic = new TWfSettingTaskLogic();
            TWfInstTaskDetailVo detail = Details(strInstStepID);
            return logic.IsStartStep(detail.WF_ID, detail.WF_TASK_ID);
        }

        /// <summary>
        /// 根据实例环节获取环节配置数据，再查看其是否为结束环节【根据实例环节编号间接判断是否为结束环节】
        /// </summary>
        /// <param name="strInstStepID">实例环节ID</param>
        /// <returns></returns>
        public bool IsEndStep(string strInstStepID)
        {
            TWfSettingTaskLogic logic = new TWfSettingTaskLogic();
            TWfInstTaskDetailVo detail = Details(strInstStepID);
            return logic.IsEndStep(detail.WF_ID, detail.WF_TASK_ID);
        }

        /// <summary>
        /// 获取一个List的执行人的用户ID
        /// </summary>
        /// <param name="strInstStepID">实例环节ID</param>
        /// <returns></returns>
        public List<string> GetObjectUserList(string strInstStepID)
        {
            string strList = Details(strInstStepID).OBJECT_USER;

            string[] list = strList.Split(',');
            List<string> UserList = new List<string>();
            foreach (string strTemp in list)
                UserList.Add(strTemp);
            return UserList;

        }

        /// <summary>
        /// 创建实例流程（包括未处理的第一个环节）数据接口
        /// </summary>
        /// <param name="strWFID">流程代码</param>
        /// <param name="strObjectUser">目标处理人用户ID</param>
        /// <param name="strCreateUser">生成人员ID</param>
        /// <param name="strServiceCode">业务类型编码</param>
        /// <param name="strServiceName">业务类型名称</param>
        /// <returns></returns>
        public bool CreateInstWFAndFirstStep(string strWFID, string strObjectUser, string strCreateUser, string strServiceCode, string strServiceName)
        {
            TWfInstTaskDetailVo detail = new TWfInstTaskDetailVo();
            TWfInstControlVo control = new TWfInstControlVo();
            TWfSettingTaskVo task = new TWfSettingTaskLogic().GetFirstStep(strWFID);
            TWfSettingFlowVo flow = new TWfSettingFlowLogic().Details(new TWfSettingFlowVo() { WF_ID = strWFID });

            //如果不存在数据，则返回
            if (string.IsNullOrEmpty(task.ID) || string.IsNullOrEmpty(flow.ID))
                return false;

            detail.ID = GetGUID();
            detail.INST_NOTE = task.TASK_NOTE;
            detail.INST_TASK_CAPTION = task.TASK_CAPTION;
            detail.INST_TASK_STARTTIME = GetDateTimeToStanString();
            detail.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
            detail.OBJECT_USER = strObjectUser;
            detail.SRC_USER = strCreateUser;
            detail.WF_ID = strWFID;
            detail.WF_INST_ID = GetGUID();
            detail.WF_SERIAL_NO = GetDateTimeToStringFor15();
            detail.WF_TASK_ID = task.WF_TASK_ID;

            control.ID = detail.WF_INST_ID;
            control.IS_SUB_FLOW = "0";
            control.WF_CAPTION = flow.WF_CAPTION;
            control.WF_ID = strWFID;
            control.WF_INST_TASK_ID = detail.ID;
            control.WF_NOTE = flow.WF_NOTE;
            control.WF_PRIORITY = TWfCommDict.WfPriority.Priority_1;
            control.WF_SERIAL_NO = detail.WF_SERIAL_NO;
            control.WF_SERVICE_CODE = strServiceCode;
            control.WF_SERVICE_NAME = strServiceName;
            control.WF_STARTTIME = detail.INST_TASK_STARTTIME;
            control.WF_STATE = TWfCommDict.WfState.StateNormal;
            control.WF_TASK_ID = detail.WF_TASK_ID;

            bool bIsDetail = this.Create(detail);
            bool bIsControl = new TWfInstControlLogic().Create(control);

            return bIsDetail & bIsControl;
        }

        /// <summary>
        /// 功能描述：获取指定时间范围内的任务
        /// 创建时间：2012-12-23
        /// 创建人：邵世卓
        /// <param name="tWfInstTaskDetail">任务对象</param>
        /// <param name="strINST_TASK_STARTTIME_from">时间上限</param>
        /// <param name="strINST_TASK_STARTTIME_to">时间下限</param>
        /// <returns></returns>
        public int GetSelectResultCountForDayTaskList(TWfInstTaskDetailVo tWfInstTaskDetail, string strINST_TASK_STARTTIME_from, string strINST_TASK_STARTTIME_to)
        {
            return access.GetSelectResultCountForDayTaskList(tWfInstTaskDetail, strINST_TASK_STARTTIME_from, strINST_TASK_STARTTIME_to);
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
            return access.GetWFDetailByBusinessInfo(tWfInstTaskDetail, strServiceName, strServiceValue);
        }
            #region//短信自动提醒功能（郑州）
              /// <summary>
        /// 获取待处理任务人员
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public DataTable GetUserList(TSysUserVo userInfo)
        {
            return access.GetUserList(userInfo);
        }
                /// <summary>
        /// 获取未处理的流程
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnTreatInfo(TSysUserVo userInfo)
        {
            return access.GetUnTreatInfo( userInfo);
        }
        //代办收文
        public DataTable GetUnTreatSW()
        {
            return access.GetUnTreatSW();
        }
        //采样任务列表
        public DataTable SamplingList()
        {
            return access.SamplingList();
        }
            #endregion
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //环节实例编号
            if (tWfInstTaskDetail.ID.Trim() == "")
            {
                this.Tips.AppendLine("环节实例编号不能为空");
                return false;
            }
            //流程实例编号
            if (tWfInstTaskDetail.WF_INST_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程实例编号不能为空");
                return false;
            }
            //流水号
            if (tWfInstTaskDetail.WF_SERIAL_NO.Trim() == "")
            {
                this.Tips.AppendLine("流水号不能为空");
                return false;
            }
            //流程编号
            if (tWfInstTaskDetail.WF_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程编号不能为空");
                return false;
            }
            //环节编号
            if (tWfInstTaskDetail.WF_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("环节编号不能为空");
                return false;
            }
            //上环节实例编号
            if (tWfInstTaskDetail.PRE_INST_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("上环节实例编号不能为空");
                return false;
            }
            //上环节编号
            if (tWfInstTaskDetail.PRE_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("上环节编号不能为空");
                return false;
            }
            //此环节简述
            if (tWfInstTaskDetail.INST_TASK_CAPTION.Trim() == "")
            {
                this.Tips.AppendLine("此环节简述不能为空");
                return false;
            }
            //此节点详细描述
            if (tWfInstTaskDetail.INST_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("此节点详细描述不能为空");
                return false;
            }
            //此环节开始时间
            if (tWfInstTaskDetail.INST_TASK_STARTTIME.Trim() == "")
            {
                this.Tips.AppendLine("此环节开始时间不能为空");
                return false;
            }
            //此环节结束时间
            if (tWfInstTaskDetail.INST_TASK_ENDTIME.Trim() == "")
            {
                this.Tips.AppendLine("此环节结束时间不能为空");
                return false;
            }
            //此环节状态
            if (tWfInstTaskDetail.INST_TASK_STATE.Trim() == "")
            {
                this.Tips.AppendLine("此环节状态不能为空");
                return false;
            }
            //目标操作人
            if (tWfInstTaskDetail.OBJECT_USER.Trim() == "")
            {
                this.Tips.AppendLine("目标操作人不能为空");
                return false;
            }
            //实际操作人
            if (tWfInstTaskDetail.REAL_USER.Trim() == "")
            {
                this.Tips.AppendLine("实际操作人不能为空");
                return false;
            }
            //环节提示信息
            if (tWfInstTaskDetail.INST_TASK_MSG.Trim() == "")
            {
                this.Tips.AppendLine("环节提示信息不能为空");
                return false;
            }
            //是否超时
            if (tWfInstTaskDetail.IS_OVERTIME.Trim() == "")
            {
                this.Tips.AppendLine("是否超时不能为空");
                return false;
            }
            //是否提醒
            if (tWfInstTaskDetail.IS_REMIND.Trim() == "")
            {
                this.Tips.AppendLine("是否提醒不能为空");
                return false;
            }
            //确认时间
            if (tWfInstTaskDetail.CFM_TIME.Trim() == "")
            {
                this.Tips.AppendLine("确认时间不能为空");
                return false;
            }
            //确认人
            if (tWfInstTaskDetail.CFM_USER.Trim() == "")
            {
                this.Tips.AppendLine("确认人不能为空");
                return false;
            }
            //撤销时间
            if (tWfInstTaskDetail.CFM_UNTIME.Trim() == "")
            {
                this.Tips.AppendLine("撤销时间不能为空");
                return false;
            }

            return true;
        }

    }
}
