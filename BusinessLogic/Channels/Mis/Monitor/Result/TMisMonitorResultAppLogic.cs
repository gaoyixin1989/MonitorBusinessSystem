using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.DataAccess.Channels.Mis.Monitor.Result;
using i3.ValueObject.Channels.Mis.Contract;
namespace i3.BusinessLogic.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：结果分析执行表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorResultAppLogic : LogicBase
    {

        TMisMonitorResultAppVo tMisMonitorResultApp = new TMisMonitorResultAppVo();
        TMisMonitorResultAppAccess access;

        public TMisMonitorResultAppLogic()
        {
            access = new TMisMonitorResultAppAccess();
        }

        public TMisMonitorResultAppLogic(TMisMonitorResultAppVo _tMisMonitorResultApp)
        {
            tMisMonitorResultApp = _tMisMonitorResultApp;
            access = new TMisMonitorResultAppAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            return access.GetSelectResultCount(tMisMonitorResultApp);
        }
 

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorResultAppVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorResultAppVo Details(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            return access.Details(tMisMonitorResultApp);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorResultAppVo> SelectByObject(TMisMonitorResultAppVo tMisMonitorResultApp, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorResultApp, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorResultAppVo tMisMonitorResultApp, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorResultApp, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResultApp"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            return access.SelectByTable(tMisMonitorResultApp);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorResultApp">对象</param>
        /// <returns></returns>
        public TMisMonitorResultAppVo SelectByObject(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            return access.SelectByObject(tMisMonitorResultApp);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            return access.Create(tMisMonitorResultApp);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultApp">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            return access.Edit(tMisMonitorResultApp);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultApp_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorResultApp_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultAppVo tMisMonitorResultApp_UpdateSet, TMisMonitorResultAppVo tMisMonitorResultApp_UpdateWhere)
        {
            return access.Edit(tMisMonitorResultApp_UpdateSet, tMisMonitorResultApp_UpdateWhere);
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
        public bool Delete(TMisMonitorResultAppVo tMisMonitorResultApp)
        {
            return access.Delete(tMisMonitorResultApp);
        }
        /// <summary>
        /// 获得分析执行表信息
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable SelectByTableByTaskID(string strTaskID, string strItemType)
        {
            return access.SelectByTableByTaskID(strTaskID, strItemType);
        }

        /// <summary>
        /// 获取完成及时率列表
        /// </summary>
        /// <param name="type">true,h获取超时的，false获取正常完成的</param>
        /// <returns></returns>
        public DataTable GetAnalyseResultFinished(TMisMonitorResultAppVo tMisMonitorResultApp, string strContract_Code, string strDept, string strUserName, bool type)
        {
            return access.GetAnalyseResultFinished(tMisMonitorResultApp, strContract_Code, strDept, strUserName, type);
        }

        /// <summary>
        /// 获取完成及时率列表
        /// </summary>
        /// <param name="type">true,h获取超时的，false获取正常完成的</param>
        /// <returns></returns>
        public DataTable GetAnalyseResultFinishedCount(TMisMonitorResultAppVo tMisMonitorResultApp, string strContract_Code, string strDept, string strUserName)
        {
            return access.GetAnalyseResultFinishedCount(tMisMonitorResultApp, strContract_Code, strDept, strUserName);
        }
        /// <summary>
        /// 获取指定年份、企业、监测类别、点位、监测项目等数据，构造曲线图表 胡方扬 2013-03-07
        /// </summary>
        /// <param name="tMisMonitorResultApp"></param>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable GetPollutantSourceReport(TMisMonitorResultAppVo tMisMonitorResultApp, TMisContractVo tMisContract)
        {
            return access.GetPollutantSourceReport(tMisMonitorResultApp, tMisContract);
        }
        public int GetResultCount(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime)
        {
            return access.GetResultCount(StartTime, EndTime, HEAD_USERID, TICKET_NUM, OverTime);
        }
        public DataTable SearchDataEx(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime, int iIndex, int iCount)
        {
            return access.SearchDataEx(StartTime, EndTime, HEAD_USERID, TICKET_NUM, OverTime,iIndex,iCount);
        }
        public int GetUseRecordCount(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime, string NAME)
        {
            return access.GetUseRecordCount(StartTime, EndTime, HEAD_USERID, TICKET_NUM, OverTime, NAME);
        }
        public DataTable SearchUseReocrdData(string StartTime, string EndTime, string HEAD_USERID, string TICKET_NUM, string OverTime,string NAME, int iIndex, int iCount)
        {
            return access.SearchUseReocrdData(StartTime, EndTime, HEAD_USERID, TICKET_NUM, OverTime, NAME,iIndex, iCount);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorResultApp.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //样品结果表ID
            if (tMisMonitorResultApp.RESULT_ID.Trim() == "")
            {
                this.Tips.AppendLine("样品结果表ID不能为空");
                return false;
            }
            //分析负责人员ID
            if (tMisMonitorResultApp.HEAD_USERID.Trim() == "")
            {
                this.Tips.AppendLine("分析负责人员ID不能为空");
                return false;
            }
            //
            if (tMisMonitorResultApp.ASSISTANT_USERID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //执行人接受确认
            if (tMisMonitorResultApp.TEST_CORFIRM.Trim() == "")
            {
                this.Tips.AppendLine("执行人接受确认不能为空");
                return false;
            }
            //要求时间
            if (tMisMonitorResultApp.ASKING_DATE.Trim() == "")
            {
                this.Tips.AppendLine("要求时间不能为空");
                return false;
            }
            //完成时间
            if (tMisMonitorResultApp.FINISH_DATE.Trim() == "")
            {
                this.Tips.AppendLine("完成时间不能为空");
                return false;
            }
            //校核人ID
            if (tMisMonitorResultApp.CHECK_USERID.Trim() == "")
            {
                this.Tips.AppendLine("校核人ID不能为空");
                return false;
            }
            //校核时间
            if (tMisMonitorResultApp.CHECK_DATE.Trim() == "")
            {
                this.Tips.AppendLine("校核时间不能为空");
                return false;
            }
            //校核意见
            if (tMisMonitorResultApp.CHECK_OPINION.Trim() == "")
            {
                this.Tips.AppendLine("校核意见不能为空");
                return false;
            }
            //复核人ID
            if (tMisMonitorResultApp.APPROVE_USERID.Trim() == "")
            {
                this.Tips.AppendLine("复核人ID不能为空");
                return false;
            }
            //复核时间
            if (tMisMonitorResultApp.APPROVE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("复核时间不能为空");
                return false;
            }
            //复核意见
            if (tMisMonitorResultApp.APPROVE_OPINION.Trim() == "")
            {
                this.Tips.AppendLine("复核意见不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorResultApp.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorResultApp.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorResultApp.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorResultApp.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorResultApp.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
