using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.UnderDrinkSource;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.UnderDrinkSource;

namespace i3.BusinessLogic.Channels.Env.Fill.UnderDrinkSource
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-26
    /// 创建人：
    /// </summary>
    public class TEnvFillUnderdrinkSrcItemLogic : LogicBase
    {

        TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem = new TEnvFillUnderdrinkSrcItemVo();
        TEnvFillUnderdrinkSrcItemAccess access;

        public TEnvFillUnderdrinkSrcItemLogic()
        {
            access = new TEnvFillUnderdrinkSrcItemAccess();
        }

        public TEnvFillUnderdrinkSrcItemLogic(TEnvFillUnderdrinkSrcItemVo _tEnvFillUnderdrinkSrcItem)
        {
            tEnvFillUnderdrinkSrcItem = _tEnvFillUnderdrinkSrcItem;
            access = new TEnvFillUnderdrinkSrcItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            return access.GetSelectResultCount(tEnvFillUnderdrinkSrcItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillUnderdrinkSrcItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillUnderdrinkSrcItemVo Details(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            return access.Details(tEnvFillUnderdrinkSrcItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillUnderdrinkSrcItemVo> SelectByObject(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillUnderdrinkSrcItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillUnderdrinkSrcItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            return access.SelectByTable(tEnvFillUnderdrinkSrcItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">对象</param>
        /// <returns></returns>
        public TEnvFillUnderdrinkSrcItemVo SelectByObject(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            return access.SelectByObject(tEnvFillUnderdrinkSrcItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            return access.Create(tEnvFillUnderdrinkSrcItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            return access.Edit(tEnvFillUnderdrinkSrcItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrcItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillUnderdrinkSrcItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem_UpdateSet, TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem_UpdateWhere)
        {
            return access.Edit(tEnvFillUnderdrinkSrcItem_UpdateSet, tEnvFillUnderdrinkSrcItem_UpdateWhere);
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
        public bool Delete(TEnvFillUnderdrinkSrcItemVo tEnvFillUnderdrinkSrcItem)
        {
            return access.Delete(tEnvFillUnderdrinkSrcItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvFillUnderdrinkSrcItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrcItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrcItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrcItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrcItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrcItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrcItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrcItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrcItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrcItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }

}
