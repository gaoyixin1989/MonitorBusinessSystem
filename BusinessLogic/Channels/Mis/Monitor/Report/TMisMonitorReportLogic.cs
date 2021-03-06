using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Report;
using i3.DataAccess.Channels.Mis.Monitor.Report;
using i3.ValueObject.Channels.Mis.Monitor.Task;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Report
{
    /// <summary>
    /// 功能：监测报告表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorReportLogic : LogicBase
    {

        TMisMonitorReportVo tMisMonitorReport = new TMisMonitorReportVo();
        TMisMonitorReportAccess access;

        public TMisMonitorReportLogic()
        {
            access = new TMisMonitorReportAccess();
        }

        public TMisMonitorReportLogic(TMisMonitorReportVo _tMisMonitorReport)
        {
            tMisMonitorReport = _tMisMonitorReport;
            access = new TMisMonitorReportAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorReport">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorReportVo tMisMonitorReport)
        {
            return access.GetSelectResultCount(tMisMonitorReport);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorReportVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorReport">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorReportVo Details(TMisMonitorReportVo tMisMonitorReport)
        {
            return access.Details(tMisMonitorReport);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorReport">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorReportVo> SelectByObject(TMisMonitorReportVo tMisMonitorReport, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorReport, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorReport">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorReportVo tMisMonitorReport, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorReport, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorReport"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorReportVo tMisMonitorReport)
        {
            return access.SelectByTable(tMisMonitorReport);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorReport">对象</param>
        /// <returns></returns>
        public TMisMonitorReportVo SelectByObject(TMisMonitorReportVo tMisMonitorReport)
        {
            return access.SelectByObject(tMisMonitorReport);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorReportVo tMisMonitorReport)
        {
            return access.Create(tMisMonitorReport);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorReport">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorReportVo tMisMonitorReport)
        {
            return access.Edit(tMisMonitorReport);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorReport_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorReport_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorReportVo tMisMonitorReport_UpdateSet, TMisMonitorReportVo tMisMonitorReport_UpdateWhere)
        {
            return access.Edit(tMisMonitorReport_UpdateSet, tMisMonitorReport_UpdateWhere);
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
        public bool Delete(TMisMonitorReportVo tMisMonitorReport)
        {
            return access.Delete(tMisMonitorReport);
        }

        /// <summary>
        /// 报告领取信息 行数
        /// </summary>
        /// <param name="tMisMonitorReport">报告对象</param>
        /// <param name="tMisMonitorTask">监测任务对象</param>
        /// <returns>返回影响行数</returns>
        public int GetSelectResultCount(TMisMonitorReportVo tMisMonitorReport, TMisMonitorTaskVo tMisMonitorTask)
        {
            return access.GetSelectResultCount(tMisMonitorReport, tMisMonitorTask);
        }

        /// <summary>
        /// 报告领取信息
        /// </summary>
        /// <param name="tMisMonitorReport">报告对象</param>
        /// <param name="tMisMonitorTask">监测任务对象</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="pageSize">单页显示数</param>
        /// <returns>返回数据集</returns>
        public DataTable SelectByTableForManager(TMisMonitorReportVo tMisMonitorReport, TMisMonitorTaskVo tMisMonitorTask, int pageIndex, int pageSize)
        {
            return access.SelectByTableForManager(tMisMonitorReport, tMisMonitorTask, pageIndex, pageSize);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorReport.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //监测计划ID
            if (tMisMonitorReport.TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测计划ID不能为空");
                return false;
            }
            //报告单号
            if (tMisMonitorReport.REPORT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("报告单号不能为空");
                return false;
            }
            //报告完成日期
            if (tMisMonitorReport.REPORT_DATE.Trim() == "")
            {
                this.Tips.AppendLine("报告完成日期不能为空");
                return false;
            }
            //验收报告附件ID
            if (tMisMonitorReport.REPORT_EX_ATTACHE_ID.Trim() == "")
            {
                this.Tips.AppendLine("验收报告附件ID不能为空");
                return false;
            }
            //是否领取
            if (tMisMonitorReport.IF_GET.Trim() == "")
            {
                this.Tips.AppendLine("是否领取不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorReport.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorReport.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorReport.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorReport.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorReport.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
