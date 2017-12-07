using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Contract;
using i3.DataAccess.Channels.Mis.Contract;

namespace i3.BusinessLogic.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书点位频次表
    /// 创建日期：2012-11-29
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPointFreqLogic : LogicBase
    {

        TMisContractPointFreqVo tMisContractPointFreq = new TMisContractPointFreqVo();
        TMisContractPointFreqAccess access;

        public TMisContractPointFreqLogic()
        {
            access = new TMisContractPointFreqAccess();
        }

        public TMisContractPointFreqLogic(TMisContractPointFreqVo _tMisContractPointFreq)
        {
            tMisContractPointFreq = _tMisContractPointFreq;
            access = new TMisContractPointFreqAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPointFreq">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPointFreqVo tMisContractPointFreq)
        {
            return access.GetSelectResultCount(tMisContractPointFreq);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPointFreqVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPointFreq">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPointFreqVo Details(TMisContractPointFreqVo tMisContractPointFreq)
        {
            return access.Details(tMisContractPointFreq);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPointFreq">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPointFreqVo> SelectByObject(TMisContractPointFreqVo tMisContractPointFreq, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractPointFreq, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPointFreq">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPointFreqVo tMisContractPointFreq, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractPointFreq, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPointFreqVo tMisContractPointFreq)
        {
            return access.SelectByTable(tMisContractPointFreq);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPointFreq">对象</param>
        /// <returns></returns>
        public TMisContractPointFreqVo SelectByObject(TMisContractPointFreqVo tMisContractPointFreq)
        {
            return access.SelectByObject(tMisContractPointFreq);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPointFreqVo tMisContractPointFreq)
        {
            return access.Create(tMisContractPointFreq);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPointFreq">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointFreqVo tMisContractPointFreq)
        {
            return access.Edit(tMisContractPointFreq);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPointFreq_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractPointFreq_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointFreqVo tMisContractPointFreq_UpdateSet, TMisContractPointFreqVo tMisContractPointFreq_UpdateWhere)
        {
            return access.Edit(tMisContractPointFreq_UpdateSet, tMisContractPointFreq_UpdateWhere);
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
        public bool Delete(TMisContractPointFreqVo tMisContractPointFreq)
        {
            return access.Delete(tMisContractPointFreq);
        }
        /// <summary>
        /// 插入委托书监测点位频次信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtDict"></param>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public bool CreatePlanPoint(DataTable dt, DataTable dtDict, string task_id) 
        {
            return access.CreatePlanPoint(dt, dtDict,task_id);
        }

        /// <summary>
        /// 插入委托书监测点位频次,采样频次信息 胡方扬 2013-03-27 统一版本使用
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtDict"></param>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public bool CreatePlanPoint(DataTable dt, string task_id)
        {
            return access.CreatePlanPoint(dt,task_id);
        }
                /// <summary>
        /// 插入委托书环境质量类监测点位频次,采样频次信息 胡方扬 2013-04-02 统一版本使用 --质量类监测点位频次
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtDict"></param>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public bool CreatePlanPointForEvn(DataTable dt, string task_id)
        {
            return access.CreatePlanPointForEvn(dt, task_id);
        }
        /// <summary>
        /// 快捷方式生成委托书信息 插入委托书监测点位频次信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtDict"></param>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public bool CreatePlanPointQuck(DataTable dt,  string task_id)
        {
            return access.CreatePlanPointQuck(dt, task_id);
        }
               /// <summary>
        /// 创建人：插入指令性任务监测点位频次信息，并更新对应监测计划对应点位的POINT_FREQ_ID
        /// 创建人：胡方扬
        /// 创建日期：2013-07-04
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtDict"></param>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public bool CreateUnContractPlanPoint(DataTable dt, string strPlanId)
        {
            return access.CreateUnContractPlanPoint(dt,strPlanId);
        }
        public DataTable SelectContractPlanInfor(TMisContractPointFreqVo tMisContractPointFreq,TMisContractVo tMisContract,TMisContractCompanyVo tMisContractCompany,int iIndex,int iCount)
        {
            return access.SelectContractPlanInfor(tMisContractPointFreq,tMisContract,tMisContractCompany,iIndex,iCount);
        }
        public int SelectContractPlanInforCount(TMisContractPointFreqVo tMisContractPointFreq, TMisContractVo tMisContract, TMisContractCompanyVo tMisContractCompany)
        {
            return access.SelectContractPlanInforCount(tMisContractPointFreq, tMisContract, tMisContractCompany);
        }
                /// <summary>
        /// 获取委托书监测点位频次信息
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable GetPointInfor(TMisContractPointFreqVo tMisContractPointFreq)
        {
            return access.GetPointInfor(tMisContractPointFreq);
        }
        
        /// <summary>
        /// 获取当前即将进行的委托书监测点位频次信息 清远需求 胡方扬 2013-03-13
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable GetPointInforForFreq(TMisContractPointFreqVo tMisContractPointFreq)
        {
            return access.GetPointInforForFreq(tMisContractPointFreq);
        }
                /// <summary>
        /// 获取委托书已完成预约的监测点位频次信息
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable GetPlanFinishedPointInfor(TMisContractPointFreqVo tMisContractPointFreq)
        {
            return access.GetPlanFinishedPointInfor(tMisContractPointFreq);
        }
                /// <summary>
        /// 获取委托书下下有监测点位的所有监测类别 Create By Castle (胡方扬) 2012-12-21
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable GetPointMonitorInfor(TMisContractPointFreqVo tMisContractPointFreq,string strPlanId)
        {
            return access.GetPointMonitorInfor(tMisContractPointFreq,strPlanId);
        }

                /// <summary>
        /// 获取委托书下当前委托计划下监测点位的监测类别 Create By Castle (胡方扬) 2013-04-01
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable GetPointMonitorInforForPlan(TMisContractPointFreqVo tMisContractPointFreq, string strPlanId)
        {
            return access.GetPointMonitorInforForPlan(tMisContractPointFreq, strPlanId);
        }
        /// <summary>
        /// 获取委托书下预约办理环节的监测类别 Create　By weilin 2014-04-09
        /// </summary>
        /// <param name="strContractID"></param>
        /// <param name="strPLanID"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public DataTable GetMonitorInfoForPlan(string strContractID, string strPLanID, string strStatus)
        {
            return access.GetMonitorInfoForPlan(strContractID, strPLanID, strStatus);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisContractPointFreq.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //委托书ID
            if (tMisContractPointFreq.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书ID不能为空");
                return false;
            }
            //委托书监测点位ID
            if (tMisContractPointFreq.CONTRACT_POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书监测点位ID不能为空");
                return false;
            }
            //监测频次
            if (tMisContractPointFreq.FREQ.Trim() == "")
            {
                this.Tips.AppendLine("监测频次不能为空");
                return false;
            }
            //执行序号
            if (tMisContractPointFreq.NUM.Trim() == "")
            {
                this.Tips.AppendLine("执行序号不能为空");
                return false;
            }
            //是否已预约
            if (tMisContractPointFreq.IF_PLAN.Trim() == "")
            {
                this.Tips.AppendLine("是否已预约不能为空");
                return false;
            }
            //备注1
            if (tMisContractPointFreq.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisContractPointFreq.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisContractPointFreq.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisContractPointFreq.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisContractPointFreq.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
