using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.DataAccess.Channels.Mis.Monitor.Result;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：结果数据可追溯性表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public partial class TMisMonitorResultLogLogic : LogicBase
    {

        TMisMonitorResultLogVo tMisMonitorResultLog = new TMisMonitorResultLogVo();
        TMisMonitorResultLogAccess access;

        public TMisMonitorResultLogLogic()
        {
            access = new TMisMonitorResultLogAccess();
        }

        public TMisMonitorResultLogLogic(TMisMonitorResultLogVo _tMisMonitorResultLog)
        {
            tMisMonitorResultLog = _tMisMonitorResultLog;
            access = new TMisMonitorResultLogAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            return access.GetSelectResultCount(tMisMonitorResultLog);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorResultLogVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorResultLogVo Details(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            return access.Details(tMisMonitorResultLog);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorResultLogVo> SelectByObject(TMisMonitorResultLogVo tMisMonitorResultLog, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorResultLog, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorResultLogVo tMisMonitorResultLog, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorResultLog, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResultLog"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            return access.SelectByTable(tMisMonitorResultLog);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorResultLog">对象</param>
        /// <returns></returns>
        public TMisMonitorResultLogVo SelectByObject(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            return access.SelectByObject(tMisMonitorResultLog);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            return access.Create(tMisMonitorResultLog);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultLog">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            return access.Edit(tMisMonitorResultLog);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultLog_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorResultLog_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultLogVo tMisMonitorResultLog_UpdateSet, TMisMonitorResultLogVo tMisMonitorResultLog_UpdateWhere)
        {
            return access.Edit(tMisMonitorResultLog_UpdateSet, tMisMonitorResultLog_UpdateWhere);
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
        public bool Delete(TMisMonitorResultLogVo tMisMonitorResultLog)
        {
            return access.Delete(tMisMonitorResultLog);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorResultLog.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //样品结果表ID
            if (tMisMonitorResultLog.RESULT_ID.Trim() == "")
            {
                this.Tips.AppendLine("样品结果表ID不能为空");
                return false;
            }
            //原结果数据
            if (tMisMonitorResultLog.OLD_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("原结果数据不能为空");
                return false;
            }
            //新结果数据
            if (tMisMonitorResultLog.NEW_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("新结果数据不能为空");
                return false;
            }
            //分析负责人员ID
            if (tMisMonitorResultLog.HEAD_USERID.Trim() == "")
            {
                this.Tips.AppendLine("分析负责人员ID不能为空");
                return false;
            }
            //
            if (tMisMonitorResultLog.ASSISTANT_USERID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //完成时间
            if (tMisMonitorResultLog.FINISH_DATE.Trim() == "")
            {
                this.Tips.AppendLine("完成时间不能为空");
                return false;
            }
            //校核人ID
            if (tMisMonitorResultLog.CHECK_USERID.Trim() == "")
            {
                this.Tips.AppendLine("校核人ID不能为空");
                return false;
            }
            //校核时间
            if (tMisMonitorResultLog.CHECK_DATE.Trim() == "")
            {
                this.Tips.AppendLine("校核时间不能为空");
                return false;
            }
            //校核意见
            if (tMisMonitorResultLog.CHECK_OPINION.Trim() == "")
            {
                this.Tips.AppendLine("校核意见不能为空");
                return false;
            }
            //复核人ID
            if (tMisMonitorResultLog.APPROVE_USERID.Trim() == "")
            {
                this.Tips.AppendLine("复核人ID不能为空");
                return false;
            }
            //复核时间
            if (tMisMonitorResultLog.APPROVE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("复核时间不能为空");
                return false;
            }
            //复核意见
            if (tMisMonitorResultLog.APPROVE_OPINION.Trim() == "")
            {
                this.Tips.AppendLine("复核意见不能为空");
                return false;
            }
            //质控手段审核人ID
            if (tMisMonitorResultLog.QC_APP_USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("质控手段审核人ID不能为空");
                return false;
            }
            //质控手段审核时间
            if (tMisMonitorResultLog.QC_APP_DATE.Trim() == "")
            {
                this.Tips.AppendLine("质控手段审核时间不能为空");
                return false;
            }
            //质控手段审核意见
            if (tMisMonitorResultLog.QC_APP_INFO.Trim() == "")
            {
                this.Tips.AppendLine("质控手段审核意见不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorResultLog.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorResultLog.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorResultLog.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorResultLog.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorResultLog.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }


        public string GetPlanID(string strResultID)
        {
            return access.GetPlanID(strResultID);
        }

    }
}
