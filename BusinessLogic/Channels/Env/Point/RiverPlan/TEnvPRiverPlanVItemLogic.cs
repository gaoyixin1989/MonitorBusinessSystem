using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverPlan;
using System.Data;
using i3.DataAccess.Channels.Env.Point.RiverPlan;

namespace i3.BusinessLogic.Channels.Env.Point.RiverPlan
{
    /// <summary>
    /// 功能：规划断面
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverPlanVItemLogic : LogicBase
    {

        TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem = new TEnvPRiverPlanVItemVo();
        TEnvPRiverPlanVItemAccess access;

        public TEnvPRiverPlanVItemLogic()
        {
            access = new TEnvPRiverPlanVItemAccess();
        }

        public TEnvPRiverPlanVItemLogic(TEnvPRiverPlanVItemVo _tEnvPRiverPlanVItem)
        {
            tEnvPRiverPlanVItem = _tEnvPRiverPlanVItem;
            access = new TEnvPRiverPlanVItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            return access.GetSelectResultCount(tEnvPRiverPlanVItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverPlanVItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverPlanVItemVo Details(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            return access.Details(tEnvPRiverPlanVItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverPlanVItemVo> SelectByObject(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiverPlanVItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiverPlanVItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            return access.SelectByTable(tEnvPRiverPlanVItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">对象</param>
        /// <returns></returns>
        public TEnvPRiverPlanVItemVo SelectByObject(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            return access.SelectByObject(tEnvPRiverPlanVItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            return access.Create(tEnvPRiverPlanVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            return access.Edit(tEnvPRiverPlanVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverPlanVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverPlanVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem_UpdateSet, TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem_UpdateWhere)
        {
            return access.Edit(tEnvPRiverPlanVItem_UpdateSet, tEnvPRiverPlanVItem_UpdateWhere);
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
        public bool Delete(TEnvPRiverPlanVItemVo tEnvPRiverPlanVItem)
        {
            return access.Delete(tEnvPRiverPlanVItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiverPlanVItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPRiverPlanVItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPRiverPlanVItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPRiverPlanVItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiverPlanVItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiverPlanVItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiverPlanVItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiverPlanVItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiverPlanVItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
