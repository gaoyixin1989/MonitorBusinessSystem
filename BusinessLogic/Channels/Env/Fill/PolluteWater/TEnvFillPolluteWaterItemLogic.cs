using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.PolluteWater;
using i3.DataAccess.Channels.Env.Fill.PolluteWater;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.PolluteWater
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-09-02
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteWaterItemLogic : LogicBase
    {

        TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem = new TEnvFillPolluteWaterItemVo();
        TEnvFillPolluteWaterItemAccess access;

        public TEnvFillPolluteWaterItemLogic()
        {
            access = new TEnvFillPolluteWaterItemAccess();
        }

        public TEnvFillPolluteWaterItemLogic(TEnvFillPolluteWaterItemVo _tEnvFillPolluteWaterItem)
        {
            tEnvFillPolluteWaterItem = _tEnvFillPolluteWaterItem;
            access = new TEnvFillPolluteWaterItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            return access.GetSelectResultCount(tEnvFillPolluteWaterItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteWaterItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteWaterItemVo Details(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            return access.Details(tEnvFillPolluteWaterItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPolluteWaterItemVo> SelectByObject(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillPolluteWaterItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillPolluteWaterItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            return access.SelectByTable(tEnvFillPolluteWaterItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">对象</param>
        /// <returns></returns>
        public TEnvFillPolluteWaterItemVo SelectByObject(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            return access.SelectByObject(tEnvFillPolluteWaterItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            return access.Create(tEnvFillPolluteWaterItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            return access.Edit(tEnvFillPolluteWaterItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteWaterItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPolluteWaterItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem_UpdateSet, TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem_UpdateWhere)
        {
            return access.Edit(tEnvFillPolluteWaterItem_UpdateSet, tEnvFillPolluteWaterItem_UpdateWhere);
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
        public bool Delete(TEnvFillPolluteWaterItemVo tEnvFillPolluteWaterItem)
        {
            return access.Delete(tEnvFillPolluteWaterItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvFillPolluteWaterItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.UPLINE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.DOWNLINE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.UOM.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.STANDARD.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.IS_STANDARD.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.WATER_PER.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWaterItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }

}
