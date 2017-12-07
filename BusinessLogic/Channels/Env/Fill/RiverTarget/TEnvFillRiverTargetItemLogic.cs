using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.RiverTarget;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.RiverTarget;

namespace i3.BusinessLogic.Channels.Env.Fill.RiverTarget
{
    /// <summary>
    /// 功能：责任目标
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverTargetItemLogic : LogicBase
    {

        TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem = new TEnvFillRiverTargetItemVo();
        TEnvFillRiverTargetItemAccess access;

        public TEnvFillRiverTargetItemLogic()
        {
            access = new TEnvFillRiverTargetItemAccess();
        }

        public TEnvFillRiverTargetItemLogic(TEnvFillRiverTargetItemVo _tEnvFillRiverTargetItem)
        {
            tEnvFillRiverTargetItem = _tEnvFillRiverTargetItem;
            access = new TEnvFillRiverTargetItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            return access.GetSelectResultCount(tEnvFillRiverTargetItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverTargetItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverTargetItemVo Details(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            return access.Details(tEnvFillRiverTargetItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverTargetItemVo> SelectByObject(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiverTargetItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiverTargetItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            return access.SelectByTable(tEnvFillRiverTargetItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">对象</param>
        /// <returns></returns>
        public TEnvFillRiverTargetItemVo SelectByObject(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            return access.SelectByObject(tEnvFillRiverTargetItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            return access.Create(tEnvFillRiverTargetItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            return access.Edit(tEnvFillRiverTargetItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverTargetItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverTargetItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem_UpdateSet, TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem_UpdateWhere)
        {
            return access.Edit(tEnvFillRiverTargetItem_UpdateSet, tEnvFillRiverTargetItem_UpdateWhere);
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
        public bool Delete(TEnvFillRiverTargetItemVo tEnvFillRiverTargetItem)
        {
            return access.Delete(tEnvFillRiverTargetItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRiverTargetItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillRiverTargetItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillRiverTargetItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillRiverTargetItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillRiverTargetItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiverTargetItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiverTargetItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiverTargetItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiverTargetItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRiverTargetItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
