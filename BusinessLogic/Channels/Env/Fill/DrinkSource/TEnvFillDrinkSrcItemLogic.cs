using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.DrinkSource;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.DrinkSource;

namespace i3.BusinessLogic.Channels.Env.Fill.DrinkSource
{
    /// <summary>
    /// 功能：饮用水源地数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillDrinkSrcItemLogic : LogicBase
    {

        TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem = new TEnvFillDrinkSrcItemVo();
        TEnvFillDrinkSrcItemAccess access;

        public TEnvFillDrinkSrcItemLogic()
        {
            access = new TEnvFillDrinkSrcItemAccess();
        }

        public TEnvFillDrinkSrcItemLogic(TEnvFillDrinkSrcItemVo _tEnvFillDrinkSrcItem)
        {
            tEnvFillDrinkSrcItem = _tEnvFillDrinkSrcItem;
            access = new TEnvFillDrinkSrcItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            return access.GetSelectResultCount(tEnvFillDrinkSrcItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkSrcItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkSrcItemVo Details(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            return access.Details(tEnvFillDrinkSrcItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDrinkSrcItemVo> SelectByObject(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillDrinkSrcItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillDrinkSrcItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            return access.SelectByTable(tEnvFillDrinkSrcItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">对象</param>
        /// <returns></returns>
        public TEnvFillDrinkSrcItemVo SelectByObject(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            return access.SelectByObject(tEnvFillDrinkSrcItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            return access.Create(tEnvFillDrinkSrcItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            return access.Edit(tEnvFillDrinkSrcItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkSrcItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDrinkSrcItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem_UpdateSet, TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem_UpdateWhere)
        {
            return access.Edit(tEnvFillDrinkSrcItem_UpdateSet, tEnvFillDrinkSrcItem_UpdateWhere);
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
        public bool Delete(TEnvFillDrinkSrcItemVo tEnvFillDrinkSrcItem)
        {
            return access.Delete(tEnvFillDrinkSrcItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillDrinkSrcItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillDrinkSrcItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillDrinkSrcItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillDrinkSrcItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillDrinkSrcItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillDrinkSrcItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillDrinkSrcItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillDrinkSrcItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillDrinkSrcItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillDrinkSrcItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
