using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using i3.ValueObject.Sys.WF;
using i3.ValueObject.Sys;
using i3.ValueObject;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.WF;


namespace i3.View
{
    /// <summary>
    /// 工作流处理基类，从PageBase继承
    /// </summary>
    public class PageBaseForWF : PageBase
    {
        public const string WF_INST_ID_FIELD = "WF_INST_ID";
        public const string WF_INST_TASK_ID_FIELD = "WF_INST_TASK_ID";
        public const string WF_INST_TASK_SERVICE_FIELD = "WF_INST_TASK_SERVICE";

        public const string USER_INFO_TABLE_FIELD = "USER_INFO_TABLE";

        public const string WF_VERSION_FIELD = "I3_LIMS";

        public PageBaseForWF()
        {

        }

        /// <summary>
        /// 获取全局变量 用户信息表
        /// </summary>
        protected DataTable GetUserInfoTable
        {
            get
            {
                if (null == Application[USER_INFO_TABLE_FIELD])
                    Application[USER_INFO_TABLE_FIELD] = new TSysUserLogic().SelectByTable(new TSysUserVo() { IS_DEL = "0", IS_USE = "1" });
                return (DataTable)Application[USER_INFO_TABLE_FIELD];
            }
        }


        /// <summary>
        /// 获得GUID编号
        /// </summary>
        /// <returns></returns>
        public string GetGUID()
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
        public string GetDateTimeToStanString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 获得20121105182451098的时间形式字符
        /// </summary>
        /// <returns></returns>
        public string GetDateTimeToStringFor17()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString().PadLeft(3, '0');
        }


        #region 工作流初始化属性
        /// <summary>
        /// 获取节点命令名称
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public string GetCMDNameFromCode(string strCode)
        {
            switch (strCode)
            {
                case "01":
                    return "发送";
                case "02":
                    return "转发";
                case "03":
                    return "挂起";
                case "04":
                    return "恢复";
                case "05":
                    return "暂停";
                case "06":
                    return "返元";
                case "08":
                    return "跳转";
                case "09":
                    return "销毁";
                case "00":
                    return "退回";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取附加功能名称
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public string GetFUNCTIONNameFromCode(string strCode)
        {
            switch (strCode)
            {
                case "31":
                    return "选择人员";
                case "32":
                    return "上传附件";
                case "33":
                    return "审批评论";
                case "34":
                    return "发表意见";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 根据指定的流程编号，获取起始节点编号【用于启动流程第一个环节使用】
        /// </summary>
        /// <param name="strWFID">流程编号（简码）</param>
        /// <returns>第一个环节的环节ID</returns>
        public static string GetFirstStepIDFromWFID(string strWFID)
        {
            List<TWfSettingTaskVo> tempList = new TWfSettingTaskLogic().SelectByObjectListForSetp(new TWfSettingTaskVo() { WF_ID = strWFID });
            if (tempList.Count > 0)
                return tempList[0].WF_TASK_ID;
            else
                return "";
        }



        #endregion

        /// <summary>
        /// 根据环节状态值获取状态名称
        /// </summary>
        /// <param name="strStepState"></param>
        /// <returns></returns>
        public string GetStepStateName(string strStepState)
        {
            switch (strStepState)
            {
                case "2A":
                    return "待处理";
                case "2B":
                    return "已处理";
                case "2C":
                    return "已保存";
                case "2D":
                    return "已领取";
                default:
                    return "";
            }

        }

        /// <summary>
        /// 根据环节状态值获取状态名称
        /// </summary>
        /// <param name="strStepState"></param>
        /// <returns></returns>
        public string GetStepStateName(string strStepState, string strStepDealState)
        {
            switch (strStepState)
            {
                case TWfCommDict.StepState.StateNormal:
                    return "待处理";
                case TWfCommDict.StepState.StateDown:
                    {
                        switch (strStepDealState)
                        {
                            case TWfCommDict.StepDealState.ForBack:
                                return "已退回";
                            case TWfCommDict.StepDealState.ForCallBack:
                                return "已回收";
                            case TWfCommDict.StepDealState.ForJump:
                                return "已跳转";
                            case TWfCommDict.StepDealState.ForToZero:
                                return "已返元";
                            case TWfCommDict.StepDealState.ForZSend:
                                return "已转发";
                            default:
                                return "已处理";
                        }
                    }
                case TWfCommDict.StepState.StateSave:
                    return "已保存";
                case TWfCommDict.StepState.StateConfirm:
                    return "已确认";
                default:
                    return "";
            }

        }
        /// <summary>
        /// 根据环节状态值获取状态名称
        /// </summary>
        /// <param name="strStepState"></param>
        /// <returns></returns>
        public string GetStepStateColor(string strStepState)
        {
            switch (strStepState)
            {
                case "2A":
                    return "Orange";
                case "2B":
                    return "Green";
                case "2C":
                    return "Gray";
                case "2D":
                    return "Orange";
                default:
                    return "White";
            }
        }

        /// <summary>
        /// 根据用户ID来检索用户实际名称
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="bIsReal">是否真实名称，true返回真实名称， false 返回登录名称</param>
        /// <returns>用户名称[实际名称和登录名都可以]</returns>
        public string GetUserNameFromID(string strUserID, bool bIsReal)
        {
            DataTable dtUser = GetUserInfoTable as DataTable;
            foreach (DataRow dr in dtUser.Rows)
            {
                if (dr[TSysUserVo.ID_FIELD].ToString() == strUserID && bIsReal)
                    return dr[TSysUserVo.REAL_NAME_FIELD].ToString();
                else if (dr[TSysUserVo.ID_FIELD].ToString() == strUserID && (!bIsReal))
                    return dr[TSysUserVo.USER_NAME_FIELD].ToString();
                else
                    continue;
            }
            return "";
        }

        /// <summary>
        /// 根据流程状态编码获取流程状态名称
        /// </summary>
        /// <param name="strWFState"></param>
        /// <returns></returns>
        public string GetWFStateName(string strWFState)
        {
            switch (strWFState)
            {
                case "1A":
                    return "保存草稿";
                case "1B":
                    return "正常运行";
                case "1C":
                    return "流转暂停";
                case "1D":
                    return "流程挂起";
                case "1E":
                    return "流程退回";
                case "1F":
                    return "流程终止";
                case "1G":
                    return "流转完成";
                case "1H":
                    return "备份完成";
                default:
                    return "";

            }
        }

        /// <summary>
        /// 根流程状态，返回流程所标示的颜色
        /// </summary>
        /// <param name="strWFState"></param>
        /// <returns></returns>
        public string GetWFStateColor(string strWFState)
        {
            switch (strWFState)
            {
                case "1A"://保存草稿
                    return "Lime";
                case "1B"://正常运行
                    return "Yellow";
                case "1C"://暂停流转
                    return "Gray";
                case "1D"://挂起流程
                    return "Gray";
                case "1E"://流程退回
                    return "Blue";
                case "1F"://终止流程
                    return "Red";
                case "1G"://完成流转
                    return "Green";
                case "1H"://备份完成
                    return "Lime";
                default:
                    return "White";

            }
        }

        /// <summary>
        /// 挂起指定实例流程
        /// </summary>
        /// <param name="strInstWFID">实例流程的ID</param>
        public bool WFOperateForHold(string strID)
        {
            TWfInstControlVo ticv = new TWfInstControlVo() { ID = strID, WF_STATE = TWfCommDict.WfState.StateHold, WF_SUSPEND_STATE = "1", WF_SUSPEND_TIME = this.GetDateTimeToStanString() };
            return new TWfInstControlLogic().Edit(ticv);
        }

        /// <summary>
        /// 恢复指定流程的状态为正常
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public bool WFOperateForReNormal(string strID)
        {
            TWfInstControlVo ticv = new TWfInstControlVo() { ID = strID, WF_STATE = TWfCommDict.WfState.StateNormal, WF_SUSPEND_STATE = "0", WF_SUSPEND_ENDTIME = this.GetDateTimeToStanString() };
            return new TWfInstControlLogic().Edit(ticv);
        }

        /// <summary>
        /// 销毁指定流程，并且置所有的环节为已处理状态
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public bool WFOperateForKill(string strID)
        {
            new TWfInstControlLogic().Edit(new TWfInstControlVo()
            {
                ID = strID,
                WF_STATE = TWfCommDict.WfState.StateKill,
                WF_SUSPEND_STATE = "2",
                WF_SUSPEND_ENDTIME = this.GetDateTimeToStanString()
            });
            //然后将目前环节的处理结果置为已处理，保证不被任务列表搜索到

            List<TWfInstTaskDetailVo> taskInstList = new TWfInstTaskDetailLogic().SelectByObject(new TWfInstTaskDetailVo() { WF_INST_ID = strID, INST_TASK_STATE = TWfCommDict.StepState.StateNormal }, 0, 100);
            //所有的未处理节点，都需要置为已处理
            foreach (TWfInstTaskDetailVo temp in taskInstList)
            {
                new TWfInstTaskDetailLogic().Edit(new TWfInstTaskDetailVo()
                {
                    ID = temp.ID,
                    INST_TASK_ENDTIME = this.GetDateTimeToStanString(),
                    INST_TASK_STATE = TWfCommDict.StepState.StateDown,
                    INST_TASK_DEAL_STATE = TWfCommDict.StepDealState.ForKill
                });
            }
            return true;
        }

        /// <summary>
        /// 销毁指定流程，并且置所有的环节为已处理状态
        /// 修改目的：强行结束流程
        /// 修改时间：2013-5-16
        /// 修改人：邵世卓
        /// </summary>
        /// <param name="strID">TWfInstControlVo.ID</param>
        /// <returns></returns>
        public bool WFOperateForKillBySSZ(string strID)
        {
            new TWfInstControlLogic().Edit(new TWfInstControlVo()
            {
                ID = strID,
                WF_STATE = TWfCommDict.WfState.StateFinish,
                WF_SUSPEND_STATE = "2",
                WF_SUSPEND_ENDTIME = this.GetDateTimeToStanString()
            });
            //然后将目前环节的处理结果置为已处理，保证不被任务列表搜索到
            List<TWfInstTaskDetailVo> taskInstList = new TWfInstTaskDetailLogic().SelectByObject(new TWfInstTaskDetailVo() { WF_INST_ID = strID }, 0, 0);
            //所有的置为已处理（包括未处理的）
            foreach (TWfInstTaskDetailVo temp in taskInstList)
            {
                new TWfInstTaskDetailLogic().Edit(new TWfInstTaskDetailVo()
                {
                    ID = temp.ID,
                    INST_TASK_ENDTIME = this.GetDateTimeToStanString(),
                    INST_TASK_STATE = TWfCommDict.StepState.StateDown,
                    INST_TASK_DEAL_STATE = TWfCommDict.StepDealState.ForKill
                });
            }
            return true;
        }

        /// <summary>
        ///  流程实例返元的处理方法
        /// </summary>
        /// <param name="strID">实例流程的编号</param>
        /// <returns></returns>
        protected bool WFOperateGoStart(string strID)
        { //具有返元权限的操作人员可以做
            TWfInstTaskDetailLogic instTaskLogic = new TWfInstTaskDetailLogic();
            List<TWfInstTaskDetailVo> taskInstList = instTaskLogic.SelectByObject(new TWfInstTaskDetailVo() { WF_INST_ID = strID }, 0, 100);
            //如果指定的流程无实际的节点，则返回
            if (taskInstList.Count < 1)
                return false;
            TWfInstTaskDetailVo taskInstFirst = taskInstList[0];
            TWfInstTaskDetailVo taskInstLast = taskInstList[taskInstList.Count - 1];
            List<TWfSettingTaskVo> taskSettingList = new TWfSettingTaskLogic().SelectByObjectListForSetp(new TWfSettingTaskVo() { WF_ID = taskInstFirst.WF_ID });
            TWfInstControlVo wfInst = new TWfInstControlLogic().Details(taskInstFirst.WF_INST_ID);
            //如果配置信息没有任何节点，则返回
            if (taskSettingList.Count < 1)
                return false;

            TWfInstTaskDetailVo taskNew = new TWfInstTaskDetailVo();
            taskNew.ID = this.GetGUID();
            taskNew.INST_NOTE = taskSettingList[0].TASK_NOTE;
            taskNew.INST_TASK_CAPTION = taskSettingList[0].TASK_CAPTION;
            taskNew.INST_TASK_STARTTIME = this.GetDateTimeToStanString();
            taskNew.INST_TASK_STATE = TWfCommDict.StepState.StateNormal;
            taskNew.OBJECT_USER = taskInstFirst.OBJECT_USER;//使用上环节的目标处理人
            taskNew.PRE_INST_TASK_ID = taskInstLast.ID;//上一个环节的编号，将成为本环节的上环节编号
            //返元的所有新节点的前一个节点肯定是空的，直接置空即可
            //taskNew.PRE_TASK_ID = i3.ValueObject.ConstValues.SpecialCharacter.EmptyValuesFillChar;
            taskNew.WF_ID = taskSettingList[0].WF_ID;
            taskNew.WF_INST_ID = taskInstFirst.WF_INST_ID;
            taskNew.WF_SERIAL_NO = taskInstFirst.WF_SERIAL_NO;
            taskNew.WF_TASK_ID = taskSettingList[0].ID;

            //将原环节表的标志位更新为完成
            taskInstLast.INST_TASK_ENDTIME = this.GetDateTimeToStanString();
            taskInstLast.INST_TASK_STATE = TWfCommDict.StepState.StateDown;
            taskInstLast.INST_TASK_DEAL_STATE = TWfCommDict.StepDealState.ForToZero;
            taskInstLast.REAL_USER = (this.Page as PageBase).LogInfo.UserInfo.ID;

            //环节表更新完毕，接着更新控制表
            //更新控制表信息
            //退回时要把附件和评论信息放入数据库，业务数据也要全部退回
            //写入流程产生的新数据

            instTaskLogic.Create(taskNew);
            instTaskLogic.Edit(new TWfInstTaskDetailVo()
            {
                ID = taskInstLast.ID,
                INST_TASK_ENDTIME = taskInstLast.INST_TASK_ENDTIME,
                INST_TASK_STATE = taskInstLast.INST_TASK_STATE,
                INST_TASK_DEAL_STATE = taskInstLast.INST_TASK_DEAL_STATE,
                REAL_USER = taskInstLast.REAL_USER
            });
            TWfInstControlLogic instWFLogic = new TWfInstControlLogic();
            instWFLogic.Edit(new TWfInstControlVo()
            {
                ID = wfInst.ID,
                WF_INST_TASK_ID = taskNew.ID,
                WF_TASK_ID = taskNew.WF_TASK_ID
            });

            //附件和评论都无效

            //业务数据直接copy第一个节点时的数据即可
            TWfInstTaskServiceLogic serviceLogic = new TWfInstTaskServiceLogic();
            List<TWfInstTaskServiceVo> serviceList = new List<TWfInstTaskServiceVo>();
            List<TWfInstTaskServiceVo> serviceOldList = new TWfInstTaskServiceLogic().SelectByObject(new TWfInstTaskServiceVo() { WF_INST_ID = taskInstFirst.WF_INST_ID, WF_INST_TASK_ID = taskInstFirst.ID }, 0, 100);
            if (null != serviceOldList)
                foreach (TWfInstTaskServiceVo service in serviceOldList)
                {
                    //增加ID，流程实例编号、环节实例编号等内容，业务代码，Key和Value由业务系统自己处理
                    TWfInstTaskServiceVo stemp = new TWfInstTaskServiceVo();
                    stemp.ID = this.GetGUID();
                    stemp.WF_INST_ID = wfInst.ID;
                    stemp.WF_INST_TASK_ID = taskNew.ID;
                    stemp.SERVICE_NAME = service.SERVICE_NAME;
                    stemp.SERVICE_KEY_NAME = service.SERVICE_KEY_NAME;
                    stemp.SERVICE_KEY_VALUE = service.SERVICE_KEY_VALUE;
                    stemp.SERVICE_ROW_SIGN = service.SERVICE_ROW_SIGN;
                    serviceList.Add(stemp);
                }

            foreach (TWfInstTaskServiceVo serviceTemp in serviceList)
                serviceLogic.Create(serviceTemp);

            return true;
        }
    }
}
