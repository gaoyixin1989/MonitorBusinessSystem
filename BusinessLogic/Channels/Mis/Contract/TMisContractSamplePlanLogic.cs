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
    /// 功能：自送样预约表
    /// 创建日期：2012-11-29
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractSamplePlanLogic : LogicBase
    {

        TMisContractSamplePlanVo tMisContractSamplePlan = new TMisContractSamplePlanVo();
        TMisContractSamplePlanAccess access;

        public TMisContractSamplePlanLogic()
        {
            access = new TMisContractSamplePlanAccess();
        }

        public TMisContractSamplePlanLogic(TMisContractSamplePlanVo _tMisContractSamplePlan)
        {
            tMisContractSamplePlan = _tMisContractSamplePlan;
            access = new TMisContractSamplePlanAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            return access.GetSelectResultCount(tMisContractSamplePlan);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractSamplePlanVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractSamplePlanVo Details(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            return access.Details(tMisContractSamplePlan);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractSamplePlanVo> SelectByObject(TMisContractSamplePlanVo tMisContractSamplePlan, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractSamplePlan, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractSamplePlanVo tMisContractSamplePlan, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractSamplePlan, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractSamplePlan"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            return access.SelectByTable(tMisContractSamplePlan);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象</param>
        /// <returns></returns>
        public TMisContractSamplePlanVo SelectByObject(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            return access.SelectByObject(tMisContractSamplePlan);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            return access.Create(tMisContractSamplePlan);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSamplePlan">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            return access.Edit(tMisContractSamplePlan);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSamplePlan_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractSamplePlan_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSamplePlanVo tMisContractSamplePlan_UpdateSet, TMisContractSamplePlanVo tMisContractSamplePlan_UpdateWhere)
        {
            return access.Edit(tMisContractSamplePlan_UpdateSet, tMisContractSamplePlan_UpdateWhere);
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
        public bool Delete(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            return access.Delete(tMisContractSamplePlan);
        }
        /// <summary>
        /// 自送样流程结束自动插入采样计划数据 胡方扬 2012-12-29
        /// </summary>
        /// <param name="task_id"></param>
        /// <param name="strFreq"></param>
        /// <returns></returns>
        public bool CreateSamplePlan(string task_id, string strFreq) {
            return access.CreateSamplePlan(task_id, strFreq);
        }

        public DataTable GetSamplePlanMonitor(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            return access.GetSamplePlanMonitor(tMisContractSamplePlan);
        }

                /// <summary>
        /// 获取自送样计划样品监测项目 胡方扬 2012-12-29
        /// </summary>
        /// <param name="tMisContractSamplePlan"></param>
        /// <returns></returns>
        public DataTable GetSamplePlanItems(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            return access.GetSamplePlanItems(tMisContractSamplePlan);
        }

                /// <summary>
        /// 自送样插入采样计划数据
        /// </summary>
        /// <param name="task_id"></param>
        /// <param name="strFreq"></param>
        /// <returns></returns>
        public bool CreateSamplePlan(TMisContractSamplePlanVo tMisContractSamplePlan, string strCompany_Name)
        {
            return access.CreateSamplePlan(tMisContractSamplePlan, strCompany_Name );
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisContractSamplePlan.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //委托书ID
            if (tMisContractSamplePlan.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书ID不能为空");
                return false;
            }
            //监测频次
            if (tMisContractSamplePlan.FREQ.Trim() == "")
            {
                this.Tips.AppendLine("监测频次不能为空");
                return false;
            }
            //执行序号
            if (tMisContractSamplePlan.NUM.Trim() == "")
            {
                this.Tips.AppendLine("执行序号不能为空");
                return false;
            }
            //是否已预约
            if (tMisContractSamplePlan.IF_PLAN.Trim() == "")
            {
                this.Tips.AppendLine("是否已预约不能为空");
                return false;
            }
            //备注1
            if (tMisContractSamplePlan.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisContractSamplePlan.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisContractSamplePlan.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisContractSamplePlan.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisContractSamplePlan.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
