using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.DrinkUnder;
using i3.ValueObject.Channels.Env.Fill.DrinkUnder;

namespace i3.BusinessLogic.Channels.Env.Fill.DrinkUnder
{
    /// <summary>
    /// 功能：地下水填报
    /// 创建日期：2013-06-22
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillDrinkUnderItemLogic : LogicBase
    {

        TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem = new TEnvFillDrinkUnderItemVo();
        TEnvFillDrinkUnderItemAccess access;

        public TEnvFillDrinkUnderItemLogic()
        {
            access = new TEnvFillDrinkUnderItemAccess();
        }

        public TEnvFillDrinkUnderItemLogic(TEnvFillDrinkUnderItemVo _tEnvFillDrinkUnderItem)
        {
            tEnvFillDrinkUnderItem = _tEnvFillDrinkUnderItem;
            access = new TEnvFillDrinkUnderItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            return access.GetSelectResultCount(tEnvFillDrinkUnderItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkUnderItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkUnderItemVo Details(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            return access.Details(tEnvFillDrinkUnderItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDrinkUnderItemVo> SelectByObject(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillDrinkUnderItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillDrinkUnderItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            return access.SelectByTable(tEnvFillDrinkUnderItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">对象</param>
        /// <returns></returns>
        public TEnvFillDrinkUnderItemVo SelectByObject(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            return access.SelectByObject(tEnvFillDrinkUnderItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            return access.Create(tEnvFillDrinkUnderItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            return access.Edit(tEnvFillDrinkUnderItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkUnderItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDrinkUnderItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem_UpdateSet, TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem_UpdateWhere)
        {
            return access.Edit(tEnvFillDrinkUnderItem_UpdateSet, tEnvFillDrinkUnderItem_UpdateWhere);
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
        public bool Delete(TEnvFillDrinkUnderItemVo tEnvFillDrinkUnderItem)
        {
            return access.Delete(tEnvFillDrinkUnderItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillDrinkUnderItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillDrinkUnderItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillDrinkUnderItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillDrinkUnderItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillDrinkUnderItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillDrinkUnderItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillDrinkUnderItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillDrinkUnderItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillDrinkUnderItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillDrinkUnderItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
