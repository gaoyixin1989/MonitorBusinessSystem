using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Contract;
using i3.DataAccess.Channels.Mis.Contract;
using i3.ValueObject.Channels.Mis.Monitor.Task;

namespace i3.BusinessLogic.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书监测预约表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPlanLogic : LogicBase
    {

        TMisContractPlanVo tMisContractPlan = new TMisContractPlanVo();
        TMisContractPlanAccess access;

        public TMisContractPlanLogic()
        {
            access = new TMisContractPlanAccess();
        }

        public TMisContractPlanLogic(TMisContractPlanVo _tMisContractPlan)
        {
            tMisContractPlan = _tMisContractPlan;
            access = new TMisContractPlanAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPlan">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPlanVo tMisContractPlan)
        {
            return access.GetSelectResultCount(tMisContractPlan);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPlanVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPlan">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPlanVo Details(TMisContractPlanVo tMisContractPlan)
        {
            return access.Details(tMisContractPlan);
        }

        public TMisContractPlanVo DetailsByCCFlowID(string ccflowID)
        {
            return access.DetailsByCCFlowID(ccflowID);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPlanVo> SelectByObject(TMisContractPlanVo tMisContractPlan, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractPlan, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPlanVo tMisContractPlan, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractPlan, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPlanVo tMisContractPlan)
        {
            return access.SelectByTable(tMisContractPlan);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPlan">对象</param>
        /// <returns></returns>
        public TMisContractPlanVo SelectByObject(TMisContractPlanVo tMisContractPlan)
        {
            return access.SelectByObject(tMisContractPlan);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPlanVo tMisContractPlan)
        {
            return access.Create(tMisContractPlan);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlan">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanVo tMisContractPlan)
        {
            return access.Edit(tMisContractPlan);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlan_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractPlan_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanVo tMisContractPlan_UpdateSet, TMisContractPlanVo tMisContractPlan_UpdateWhere)
        {
            return access.Edit(tMisContractPlan_UpdateSet, tMisContractPlan_UpdateWhere);
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
        public bool Delete(TMisContractPlanVo tMisContractPlan)
        {
            return access.Delete(tMisContractPlan);
        }
        /// <summary>
        /// 获取指定条件委托书的监测任务计划（快速录入） 胡方扬 2013-02-25
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableContractPlanForQuickly(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.SelectByTableContractPlanForQuickly(tMisContractPlan, tMisContract, tMisMonitorTask);
        }
        /// <summary>
        /// 获取指定条件委托书的监测任务计划（快速录入） 记录数 胡方扬 2013-02-25
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public int GetContractPlanForQuicklyCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract,TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.GetContractPlanForQuicklyCount(tMisContractPlan, tMisContract, tMisMonitorTask);
        }
        /// <summary>
        /// 获取指定条件下委托书监测计划任务列表（待预约的，已预约的） 胡方扬 2013-04-01
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableContractPlanForPending(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType, int iIndex, int iCount)
        {
            return access.SelectByTableContractPlanForPending(tMisContractPlan, tMisContract, tMisMonitorTask, strType, iIndex, iCount);
        }
        /// <summary>
        /// 获取指定条件下委托书监测计划任务列表（待预约的，已预约的） 胡方扬 2013-04-24
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableContractPlanForPending(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract)
        {
            return access.SelectByTableContractPlanForPending(tMisContractPlan, tMisContract);
        }

        /// <summary>
        /// 获取指定条件下委托书监测计划任务列表（待预约的，已预约的） 胡方扬 2013-04-24
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableContractPlanForPending(TMisContractPlanVo tMisContractPlan)
        {
            return access.SelectByTableContractPlanForPending(tMisContractPlan);
        }
        /// <summary>
        /// 获取指定条件下委托书监测计划任务列表数目（待预约的，已预约的） 胡方扬 2013-04-01
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public int SelectByTableContractPlanForPendingCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract,TMisMonitorTaskVo tMisMonitorTask,bool strType)
        {
            return access.SelectByTableContractPlanForPendingCount(tMisContractPlan, tMisContract,tMisMonitorTask, strType);
        }
        
        /// <summary>
        /// 完全根据任务去获取待预约的任务
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectByTablePlanForTask(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType, int iIndex, int iCount)
        {
            return access.SelectByTablePlanForTask(tMisContractPlan, tMisContract, tMisMonitorTask, strType, iIndex, iCount);
        }
        
        /// <summary>
        /// 完全根据任务去获取待预约的任务的总条数
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectByTablePlanForTaskCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType)
        {
            return access.GetSelectByTablePlanForTaskCount(tMisContractPlan, tMisContract, tMisMonitorTask, strType);
        }

        /// <summary>
        /// 完全根据任务去获取预约办理的任务
        /// 创建时间：2013-06-08
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectByTablePlanForDoTask(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType, int iIndex, int iCount)
        {
            return access.SelectByTablePlanForDoTask(tMisContractPlan, tMisContract, tMisMonitorTask, strType, iIndex, iCount);
        }

        /// <summary>
        /// 完全根据任务去获取预约办理的任务的总条数
        /// 创建时间：2013-06-08
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectByTablePlanForDoTaskCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType)
        {
            return access.GetSelectByTablePlanForDoTaskCount(tMisContractPlan, tMisContract, tMisMonitorTask, strType);
        }

        /// <summary>
        /// 获取已预约办理的任务
        /// 创建时间：2014-09-30
        /// 创建人：魏林
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        public DataTable SelectByTablePlanForDoTask_Done(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, int iIndex, int iCount)
        {
            return access.SelectByTablePlanForDoTask_Done(tMisContractPlan, tMisContract, tMisMonitorTask, iIndex, iCount);
        }

        /// <summary>
        /// 获取已预约办理的任务的总条数
        /// 创建时间：2014-09-30
        /// 创建人：魏林
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        public int GetSelectByTablePlanForDoTaskCount_Done(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.GetSelectByTablePlanForDoTaskCount_Done(tMisContractPlan, tMisContract, tMisMonitorTask);
        }

        /// <summary>
        /// 完全根据任务去获取指令性已经完成任务下达任务列表
        /// 创建时间：2013-06-08
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectByTablePlanForDoOrderTask(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType, int iIndex, int iCount)
        {
            return access.SelectByTablePlanForDoOrderTask(tMisContractPlan, tMisContract, tMisMonitorTask, strType, iIndex, iCount);
        }

        /// <summary>
        /// 完全根据任务去获取指令性已经完成任务下达任务列表的总条数
        /// 创建时间：2013-06-08
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectByTablePlanForDoOrderTaskCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType)
        {
            return access.GetSelectByTablePlanForDoOrderTaskCount(tMisContractPlan, tMisContract, tMisMonitorTask, strType);
        }


        /// <summary>
        /// 完全根据任务去获取任务计划信息，不做TASK_STATUS限定
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectByTablePlanForTaskAnyTaskStatus(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType, int iIndex, int iCount)
        {
            return access.SelectByTablePlanForTaskAnyTaskStatus(tMisContractPlan, tMisContract, tMisMonitorTask, strType, iIndex, iCount);
        }

        /// <summary>
        /// 完全根据任务去获取任务计划信息，不做TASK_STATUS限定的总条数
        /// 创建时间：2013-06-06
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <param name="tMisContract"></param>
        /// <param name="tMisMonitorTask"></param>
        /// <param name="strType"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int SelectByTablePlanForTaskAnyTaskStatusCount(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, TMisMonitorTaskVo tMisMonitorTask, bool strType)
        {
            return access.SelectByTablePlanForTaskAnyTaskStatusCount(tMisContractPlan, tMisContract, tMisMonitorTask, strType);
        }
        

        /// <summary>
        /// 获取指定日期监测计划
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableContractPlan(TMisContractPlanVo tMisContractPlan)
        {
            return access.SelectByTableContractPlan(tMisContractPlan);
        }
        /// <summary>
        /// 获取月份监测计划
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectTableContractByMonth(TMisContractPlanVo tMisContractPlan)
        {
            return access.SelectTableContractByMonth(tMisContractPlan);
        }

        /// <summary>
        /// 获取周监测计划
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectTableContractByWeek(TMisContractPlanVo tMisContractPlan)
        {
            return access.SelectTableContractByWeek(tMisContractPlan);
        }

        /// <summary>
        /// 获取符合条件委托书的采样计划顺序
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectMaxPlanNum(TMisContractPlanVo tMisContractPlan)
        {
            return access.SelectMaxPlanNum(tMisContractPlan);
        }
        /// <summary>
        /// 获取具体监测预约计划
        /// </summary>
        /// <param name="tMisContractPlan">预约计划ID</param>
        /// <returns></returns>
        public DataTable GetPlanPointSetted(TMisContractPlanVo tMisContractPlan)
        {
            return access.GetPlanPointSetted(tMisContractPlan);
        }

        /// <summary>
        /// 获取指定监测预约计划的委托书信息
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable GetContractInfor(TMisContractPlanVo tMisContractPlan)
        {
            return access.GetContractInfor(tMisContractPlan);
        }

        /// <summary>
        /// 功能描述：获取指定条件下预监测任务列表（已预约的）
        /// 创建时间：2013-5-7
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTableForPlanTask(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, string strQcStatus, bool strType, string strTaskCode, int iIndex, int iCount)
        {
            return access.SelectByTableForPlanTask(tMisContractPlan, tMisContract, strQcStatus, strType, strTaskCode, iIndex, iCount);
        }

        /// <summary>
        /// 功能描述：获取指定条件下预监测任务列表总数（已预约的）
        /// 创建时间：2013-5-7
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisContractPlan"></param>
        /// <returns></returns>
        public int SelectCountForPlanTask(TMisContractPlanVo tMisContractPlan, TMisContractVo tMisContract, string strQcStatus, bool strType, string strTaskCode)
        {
            return access.SelectCountForPlanTask(tMisContractPlan, tMisContract, strQcStatus, strType, strTaskCode);
        }

        /// <summary>
        /// 获取指定监测计划的监测点位
        /// 创建时间：2013-06-06 
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="strPlanId"></param>
        /// <param name="strIfPlan"></param>
        public DataTable GetPointInforsForPlan(string strPlanId, string strIfPlan)
        {
            return access.GetPointInforsForPlan(strPlanId,strIfPlan);
        }
                /// <summary>
        /// 获取指定监测计划的监测点位个数
        /// 创建时间：2013-06-06 
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        /// <param name="strPlanId"></param>
        /// <param name="strIfPlan"></param>
        public int GetPointInforsForPlanCount(string strPlanId, string strIfPlan)
        {
            return access.GetPointInforsForPlanCount(strPlanId, strIfPlan);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisContractPlan.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //委托书ID
            if (tMisContractPlan.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书ID不能为空");
                return false;
            }
            //受检企业ID
            if (tMisContractPlan.CONTRACT_COMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("受检企业ID不能为空");
                return false;
            }
            //年度
            if (tMisContractPlan.PLAN_YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tMisContractPlan.PLAN_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日期
            if (tMisContractPlan.PLAN_DAY.Trim() == "")
            {
                this.Tips.AppendLine("日期不能为空");
                return false;
            }

            //是否已执行
            if (tMisContractPlan.HAS_DONE.Trim() == "")
            {
                this.Tips.AppendLine("是否已执行不能为空");
                return false;
            }
            //执行日期
            if (tMisContractPlan.DONE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("执行日期不能为空");
                return false;
            }
            //REAMRK1
            if (tMisContractPlan.REAMRK1.Trim() == "")
            {
                this.Tips.AppendLine("REAMRK1不能为空");
                return false;
            }
            //REAMRK2
            if (tMisContractPlan.REAMRK2.Trim() == "")
            {
                this.Tips.AppendLine("REAMRK2不能为空");
                return false;
            }
            //REAMRK3
            if (tMisContractPlan.REAMRK3.Trim() == "")
            {
                this.Tips.AppendLine("REAMRK3不能为空");
                return false;
            }
            //REAMRK4
            if (tMisContractPlan.REAMRK4.Trim() == "")
            {
                this.Tips.AppendLine("REAMRK4不能为空");
                return false;
            }
            //REAMRK5
            if (tMisContractPlan.REAMRK5.Trim() == "")
            {
                this.Tips.AppendLine("REAMRK5不能为空");
                return false;
            }

            return true;
        }

    }
}
