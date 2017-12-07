using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.RiverPlan;
using i3.DataAccess.Channels.Env.Fill.RiverPlan;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.RiverPlan
{
    /// <summary>
    /// 功能：规划断面
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverPlanItemLogic : LogicBase
    {

        TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem = new TEnvFillRiverPlanItemVo();
        TEnvFillRiverPlanItemAccess access;

        public TEnvFillRiverPlanItemLogic()
        {
            access = new TEnvFillRiverPlanItemAccess();
        }

        public TEnvFillRiverPlanItemLogic(TEnvFillRiverPlanItemVo _tEnvFillRiverPlanItem)
        {
            tEnvFillRiverPlanItem = _tEnvFillRiverPlanItem;
            access = new TEnvFillRiverPlanItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            return access.GetSelectResultCount(tEnvFillRiverPlanItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverPlanItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverPlanItemVo Details(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            return access.Details(tEnvFillRiverPlanItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverPlanItemVo> SelectByObject(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiverPlanItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiverPlanItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            return access.SelectByTable(tEnvFillRiverPlanItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">对象</param>
        /// <returns></returns>
        public TEnvFillRiverPlanItemVo SelectByObject(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            return access.SelectByObject(tEnvFillRiverPlanItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            return access.Create(tEnvFillRiverPlanItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            return access.Edit(tEnvFillRiverPlanItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverPlanItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverPlanItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem_UpdateSet, TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem_UpdateWhere)
        {
            return access.Edit(tEnvFillRiverPlanItem_UpdateSet, tEnvFillRiverPlanItem_UpdateWhere);
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
        public bool Delete(TEnvFillRiverPlanItemVo tEnvFillRiverPlanItem)
        {
            return access.Delete(tEnvFillRiverPlanItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRiverPlanItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillRiverPlanItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillRiverPlanItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillRiverPlanItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillRiverPlanItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiverPlanItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiverPlanItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiverPlanItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiverPlanItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRiverPlanItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
