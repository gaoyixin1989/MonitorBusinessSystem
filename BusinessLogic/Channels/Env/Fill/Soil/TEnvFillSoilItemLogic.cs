using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Soil;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.Soil;

namespace i3.BusinessLogic.Channels.Env.Fill.Soil
{
    /// <summary>
    /// 功能：土壤数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSoilItemLogic : LogicBase
    {

        TEnvFillSoilItemVo tEnvFillSoilItem = new TEnvFillSoilItemVo();
        TEnvFillSoilItemAccess access;

        public TEnvFillSoilItemLogic()
        {
            access = new TEnvFillSoilItemAccess();
        }

        public TEnvFillSoilItemLogic(TEnvFillSoilItemVo _tEnvFillSoilItem)
        {
            tEnvFillSoilItem = _tEnvFillSoilItem;
            access = new TEnvFillSoilItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            return access.GetSelectResultCount(tEnvFillSoilItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSoilItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSoilItemVo Details(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            return access.Details(tEnvFillSoilItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSoilItemVo> SelectByObject(TEnvFillSoilItemVo tEnvFillSoilItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillSoilItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSoilItemVo tEnvFillSoilItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillSoilItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSoilItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            return access.SelectByTable(tEnvFillSoilItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSoilItem">对象</param>
        /// <returns></returns>
        public TEnvFillSoilItemVo SelectByObject(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            return access.SelectByObject(tEnvFillSoilItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            return access.Create(tEnvFillSoilItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSoilItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            return access.Edit(tEnvFillSoilItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSoilItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSoilItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSoilItemVo tEnvFillSoilItem_UpdateSet, TEnvFillSoilItemVo tEnvFillSoilItem_UpdateWhere)
        {
            return access.Edit(tEnvFillSoilItem_UpdateSet, tEnvFillSoilItem_UpdateWhere);
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
        public bool Delete(TEnvFillSoilItemVo tEnvFillSoilItem)
        {
            return access.Delete(tEnvFillSoilItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillSoilItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //填报ID
            if (tEnvFillSoilItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillSoilItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillSoilItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillSoilItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillSoilItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillSoilItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillSoilItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillSoilItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillSoilItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
