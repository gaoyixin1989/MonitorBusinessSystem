using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.SubTask;
using i3.DataAccess.Channels.Mis.Monitor.SubTask;

namespace i3.BusinessLogic.Channels.Mis.Monitor.SubTask
{
    /// <summary>
    /// 功能：监测子任务审核表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSubtaskAppLogic : LogicBase
    {

        TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp = new TMisMonitorSubtaskAppVo();
        TMisMonitorSubtaskAppAccess access;

        public TMisMonitorSubtaskAppLogic()
        {
            access = new TMisMonitorSubtaskAppAccess();
        }

        public TMisMonitorSubtaskAppLogic(TMisMonitorSubtaskAppVo _tMisMonitorSubtaskApp)
        {
            tMisMonitorSubtaskApp = _tMisMonitorSubtaskApp;
            access = new TMisMonitorSubtaskAppAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            return access.GetSelectResultCount(tMisMonitorSubtaskApp);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSubtaskAppVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSubtaskAppVo Details(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            return access.Details(tMisMonitorSubtaskApp);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSubtaskAppVo> SelectByObject(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorSubtaskApp, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorSubtaskApp, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            return access.SelectByTable(tMisMonitorSubtaskApp);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">对象</param>
        /// <returns></returns>
        public TMisMonitorSubtaskAppVo SelectByObject(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            return access.SelectByObject(tMisMonitorSubtaskApp);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            return access.Create(tMisMonitorSubtaskApp);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            return access.Edit(tMisMonitorSubtaskApp);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubtaskApp_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorSubtaskApp_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp_UpdateSet, TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp_UpdateWhere)
        {
            return access.Edit(tMisMonitorSubtaskApp_UpdateSet, tMisMonitorSubtaskApp_UpdateWhere);
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
        public bool Delete(TMisMonitorSubtaskAppVo tMisMonitorSubtaskApp)
        {
            return access.Delete(tMisMonitorSubtaskApp);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorSubtaskApp.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //监测子任务ID
            if (tMisMonitorSubtaskApp.SUBTASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测子任务ID不能为空");
                return false;
            }
            //采样任务分配人
            if (tMisMonitorSubtaskApp.SAMPLE_ASSIGN_ID.Trim() == "")
            {
                this.Tips.AppendLine("采样任务分配人不能为空");
                return false;
            }
            //采样任务分配时间
            if (tMisMonitorSubtaskApp.SAMPLE_ASSIGN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("采样任务分配时间不能为空");
                return false;
            }
            //质控手段设置人
            if (tMisMonitorSubtaskApp.QC_USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("质控手段设置人不能为空");
                return false;
            }
            //质控手段设置时间
            if (tMisMonitorSubtaskApp.QC_DATE.Trim() == "")
            {
                this.Tips.AppendLine("质控手段设置时间不能为空");
                return false;
            }
            //分析任务分配人
            if (tMisMonitorSubtaskApp.ANALYSE_ASSIGN_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析任务分配人不能为空");
                return false;
            }
            //分析任务分配时间
            if (tMisMonitorSubtaskApp.ANALYSE_ASSIGN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("分析任务分配时间不能为空");
                return false;
            }
            //质控手段审核人ID
            if (tMisMonitorSubtaskApp.QC_APP_USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("质控手段审核人ID不能为空");
                return false;
            }
            //质控手段审核时间
            if (tMisMonitorSubtaskApp.QC_APP_DATE.Trim() == "")
            {
                this.Tips.AppendLine("质控手段审核时间不能为空");
                return false;
            }
            //质控手段审核意见
            if (tMisMonitorSubtaskApp.QC_APP_INFO.Trim() == "")
            {
                this.Tips.AppendLine("质控手段审核意见不能为空");
                return false;
            }
            //数据审核
            if (tMisMonitorSubtaskApp.RESULT_AUDIT.Trim() == "")
            {
                this.Tips.AppendLine("数据审核不能为空");
                return false;
            }
            //分析室主任审核
            if (tMisMonitorSubtaskApp.RESULT_CHECK.Trim() == "")
            {
                this.Tips.AppendLine("分析室主任审核不能为空");
                return false;
            }
            //分析室主任审核时间
            if (tMisMonitorSubtaskApp.RESULT_CHECK_DATE.Trim() == "")
            {
                this.Tips.AppendLine("分析室主任审核时间不能为空");
                return false;
            }
            //技术室审核
            if (tMisMonitorSubtaskApp.RESULT_QC_CHECK.Trim() == "")
            {
                this.Tips.AppendLine("技术室审核不能为空");
                return false;
            }
            //技术室审核时间
            if (tMisMonitorSubtaskApp.RESULT_QC_CHECK_DATE.Trim() == "")
            {
                this.Tips.AppendLine("技术室审核不能为空时间");
                return false;
            }
            //现场复核
            if (tMisMonitorSubtaskApp.SAMPLING_CHECK.Trim() == "")
            {
                this.Tips.AppendLine("现场复核不能为空");
                return false;
            }
            //现场审核
            if (tMisMonitorSubtaskApp.SAMPLING_QC_CHECK.Trim() == "")
            {
                this.Tips.AppendLine("现场审核不能为空");
                return false;
            }
            //采样后质控
            if (tMisMonitorSubtaskApp.SAMPLING_END_QC.Trim() == "")
            {
                this.Tips.AppendLine("采样后质控不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorSubtaskApp.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorSubtaskApp.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorSubtaskApp.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorSubtaskApp.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorSubtaskApp.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }


    }
}
