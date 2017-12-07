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
    /// 功能：监测任务预约点位表
    /// 创建日期：2012-11-29
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPlanPointLogic : LogicBase
    {

        TMisContractPlanPointVo tMisContractPlanPoint = new TMisContractPlanPointVo();
        TMisContractPlanPointAccess access;

        public TMisContractPlanPointLogic()
        {
            access = new TMisContractPlanPointAccess();
        }

        public TMisContractPlanPointLogic(TMisContractPlanPointVo _tMisContractPlanPoint)
        {
            tMisContractPlanPoint = _tMisContractPlanPoint;
            access = new TMisContractPlanPointAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            return access.GetSelectResultCount(tMisContractPlanPoint);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPlanPointVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPlanPointVo Details(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            return access.Details(tMisContractPlanPoint);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPlanPointVo> SelectByObject(TMisContractPlanPointVo tMisContractPlanPoint, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractPlanPoint, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPlanPointVo tMisContractPlanPoint, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractPlanPoint, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPlanPoint"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            return access.SelectByTable(tMisContractPlanPoint);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <returns></returns>
        public TMisContractPlanPointVo SelectByObject(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            return access.SelectByObject(tMisContractPlanPoint);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            return access.Create(tMisContractPlanPoint);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlanPoint">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            return access.Edit(tMisContractPlanPoint);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlanPoint_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractPlanPoint_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanPointVo tMisContractPlanPoint_UpdateSet, TMisContractPlanPointVo tMisContractPlanPoint_UpdateWhere)
        {
            return access.Edit(tMisContractPlanPoint_UpdateSet, tMisContractPlanPoint_UpdateWhere);
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
        public bool Delete(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            return access.Delete(tMisContractPlanPoint);
        }

         /// <summary>
        /// 插入监测任务预约点位表信息  胡方扬 2012-12-18
        /// </summary>
        /// <param name="strFreqId"></param>
        /// <param name="strPointId"></param>
        /// <param name="strPlanId"></param>
        /// <returns></returns>
        public bool SavePlanPoint(string[] strFreqId, string[] strPointId, string strPlanId)
        {
            return access.SavePlanPoint(strFreqId, strPointId, strPlanId);
        }

        /// <summary>
        /// 保存选择的监测计划点位
        /// 创建时间：2013-06-07
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        public bool SaveSelectPlanPoint(string[] strFreqId, string[] strPointId, string strPlanId,bool isDo)
        {
            return access.SaveSelectPlanPoint(strFreqId, strPointId, strPlanId,isDo);
        }

        /// <summary>
        /// 插入监测任务预约点位表信息  胡方扬 2013-03-27 统一版本
        /// </summary>
        /// <param name="strFreqId"></param>
        /// <param name="strPointId"></param>
        /// <param name="strPlanId"></param>
        /// <returns></returns>
        public bool SavePlanPoint(string strContractId,string strPlanId)
        {
            return access.SavePlanPoint(strContractId,strPlanId);
        }
                /// <summary>
        /// 采样任务分配，取消采样任务后可从新预约 Create By 胡方扬 2012-02-04
        /// </summary>
        /// <param name="strSubTaskId">采样子任务点位ID</param>

        /// <returns></returns>
        public bool DelPlanPointForSampleDistr(string strSubTaskId) {
            return access.DelPlanPointForSampleDistr(strSubTaskId);
        }
                /// <summary>
        /// 获取指定监测计划的监测点位信息
        /// </summary>
        /// <param name="tMisContractPlanPoint"></param>
        /// <returns></returns>
        public DataTable GetPendingPlanPointDataTable(TMisContractPlanPointVo tMisContractPlanPoint) {
            return access.GetPendingPlanPointDataTable(tMisContractPlanPoint);
        }

                /// <summary>
        ///获取指定监测计划的监测类别信息
        /// </summary>
        /// <param name="tMisContractPlanPoint"></param>
        /// <returns></returns>
        public DataTable GetPendingPlanDistinctMonitorDataTable(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            return access.GetPendingPlanDistinctMonitorDataTable(tMisContractPlanPoint);
        }
          /// <summary>
        /// 对象添加,胡方扬  2013-05-15
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <returns>是否成功</returns>
        public bool CreateDefine(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            return access.CreateDefine(tMisContractPlanPoint);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisContractPlanPoint.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //监测任务预约表ID
            if (tMisContractPlanPoint.PLAN_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测任务预约表ID不能为空");
                return false;
            }
            //委托书监测点位ID
            if (tMisContractPlanPoint.CONTRACT_POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书监测点位ID不能为空");
                return false;
            }
            //委托书点位频次表ID
            if (tMisContractPlanPoint.POINT_FREQ_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书点位频次表ID不能为空");
                return false;
            }
            //REAMRK1
            if (tMisContractPlanPoint.REAMRK1.Trim() == "")
            {
                this.Tips.AppendLine("REAMRK1不能为空");
                return false;
            }
            //REAMRK2
            if (tMisContractPlanPoint.REAMRK2.Trim() == "")
            {
                this.Tips.AppendLine("REAMRK2不能为空");
                return false;
            }
            //REAMRK3
            if (tMisContractPlanPoint.REAMRK3.Trim() == "")
            {
                this.Tips.AppendLine("REAMRK3不能为空");
                return false;
            }
            //REAMRK4
            if (tMisContractPlanPoint.REAMRK4.Trim() == "")
            {
                this.Tips.AppendLine("REAMRK4不能为空");
                return false;
            }
            //REAMRK5
            if (tMisContractPlanPoint.REAMRK5.Trim() == "")
            {
                this.Tips.AppendLine("REAMRK5不能为空");
                return false;
            }

            return true;
        }

    }
}
