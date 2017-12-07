using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.DrinkSource;
using i3.DataAccess.Channels.Env.Point.DrinkSource;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.DrinkSource
{
    /// <summary>
    /// 功能：魏林
    /// 创建日期：2013-06-07
    /// 创建人：饮用水源地（湖库、河流）
    /// </summary>
    public class TEnvPDrinkSrcVItemLogic : LogicBase
    {

        TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem = new TEnvPDrinkSrcVItemVo();
        TEnvPDrinkSrcVItemAccess access;

        public TEnvPDrinkSrcVItemLogic()
        {
            access = new TEnvPDrinkSrcVItemAccess();
        }

        public TEnvPDrinkSrcVItemLogic(TEnvPDrinkSrcVItemVo _tEnvPDrinkSrcVItem)
        {
            tEnvPDrinkSrcVItem = _tEnvPDrinkSrcVItem;
            access = new TEnvPDrinkSrcVItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            return access.GetSelectResultCount(tEnvPDrinkSrcVItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVItemVo Details(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            return access.Details(tEnvPDrinkSrcVItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDrinkSrcVItemVo> SelectByObject(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPDrinkSrcVItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPDrinkSrcVItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            return access.SelectByTable(tEnvPDrinkSrcVItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">对象</param>
        /// <returns></returns>
        public TEnvPDrinkSrcVItemVo SelectByObject(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            return access.SelectByObject(tEnvPDrinkSrcVItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            return access.Create(tEnvPDrinkSrcVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            return access.Edit(tEnvPDrinkSrcVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrcVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDrinkSrcVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem_UpdateSet, TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem_UpdateWhere)
        {
            return access.Edit(tEnvPDrinkSrcVItem_UpdateSet, tEnvPDrinkSrcVItem_UpdateWhere);
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
        public bool Delete(TEnvPDrinkSrcVItemVo tEnvPDrinkSrcVItem)
        {
            return access.Delete(tEnvPDrinkSrcVItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPDrinkSrcVItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPDrinkSrcVItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPDrinkSrcVItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPDrinkSrcVItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPDrinkSrcVItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPDrinkSrcVItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPDrinkSrcVItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPDrinkSrcVItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPDrinkSrcVItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
