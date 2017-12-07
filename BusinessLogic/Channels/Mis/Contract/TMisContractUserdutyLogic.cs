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
    /// 功能：监测计划岗位信息
    /// 创建日期：2012-12-17
    /// 创建人：胡方扬
    /// </summary>
    public class TMisContractUserdutyLogic : LogicBase
    {

        TMisContractUserdutyVo tMisContractUserduty = new TMisContractUserdutyVo();
        TMisContractUserdutyAccess access;

        public TMisContractUserdutyLogic()
        {
            access = new TMisContractUserdutyAccess();
        }

        public TMisContractUserdutyLogic(TMisContractUserdutyVo _tMisContractUserduty)
        {
            tMisContractUserduty = _tMisContractUserduty;
            access = new TMisContractUserdutyAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractUserduty">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractUserdutyVo tMisContractUserduty)
        {
            return access.GetSelectResultCount(tMisContractUserduty);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractUserdutyVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractUserduty">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractUserdutyVo Details(TMisContractUserdutyVo tMisContractUserduty)
        {
            return access.Details(tMisContractUserduty);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractUserduty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractUserdutyVo> SelectByObject(TMisContractUserdutyVo tMisContractUserduty, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractUserduty, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractUserduty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractUserdutyVo tMisContractUserduty, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractUserduty, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractUserduty"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractUserdutyVo tMisContractUserduty)
        {
            return access.SelectByTable(tMisContractUserduty);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractUserduty">对象</param>
        /// <returns></returns>
        public TMisContractUserdutyVo SelectByObject(TMisContractUserdutyVo tMisContractUserduty)
        {
            return access.SelectByObject(tMisContractUserduty);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractUserdutyVo tMisContractUserduty)
        {
            return access.Create(tMisContractUserduty);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractUserduty">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractUserdutyVo tMisContractUserduty)
        {
            return access.Edit(tMisContractUserduty);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractUserduty_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractUserduty_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractUserdutyVo tMisContractUserduty_UpdateSet, TMisContractUserdutyVo tMisContractUserduty_UpdateWhere)
        {
            return access.Edit(tMisContractUserduty_UpdateSet, tMisContractUserduty_UpdateWhere);
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
        public bool Delete(TMisContractUserdutyVo tMisContractUserduty)
        {
            return access.Delete(tMisContractUserduty);
        }
        /// <summary>
        /// 保存监测计划岗位
        /// </summary>
        /// <param name="tMisContractUserduty"></param>
        /// <param name="strMonitorId"></param>
        /// <param name="strUserId"></param>
        /// <returns></returns>
        public bool SaveContractPlanDuty(TMisContractUserdutyVo tMisContractUserduty, string[] strMonitorId, string[] strUserId)
        {
            return access.SaveContractPlanDuty(tMisContractUserduty, strMonitorId, strUserId);
        }

        /// <summary>
        /// 保存环境质量监测计划岗位
        /// </summary>
        /// <param name="tMisContractUserduty"></param>
        /// <param name="strMonitorId"></param>
        /// <param name="strUserId"></param>
        /// <returns></returns>
        public bool SaveContractPlanDutyForEnv(TMisContractUserdutyVo tMisContractUserduty, string[] strMonitorId, string[] strUserId)
        {
            return access.SaveContractPlanDutyForEnv(tMisContractUserduty, strMonitorId, strUserId);
        }

                /// <summary>
        /// 获取监测计划负责人 胡方扬
        /// </summary>
        /// <param name="tMisContractUserduty"></param>
        /// <returns></returns>
        public DataTable SelectDutyUser(TMisContractUserdutyVo tMisContractUserduty)
        {
            return access.SelectDutyUser(tMisContractUserduty);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tMisContractUserduty.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //监测计划ID
            if (tMisContractUserduty.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测计划ID不能为空");
                return false;
            }
            //监测类别
            if (tMisContractUserduty.MONITOR_TYPE_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别不能为空");
                return false;
            }
            //默认负责人ID
            if (tMisContractUserduty.SAMPLING_MANAGER_ID.Trim() == "")
            {
                this.Tips.AppendLine("默认负责人ID不能为空");
                return false;
            }
            //默认协同人ID
            if (tMisContractUserduty.SAMPLING_ID.Trim() == "")
            {
                this.Tips.AppendLine("默认协同人ID不能为空");
                return false;
            }

            return true;
        }

    }
}
