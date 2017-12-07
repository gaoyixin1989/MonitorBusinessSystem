using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.DrinkUnder;
using System.Data;
using i3.DataAccess.Channels.Env.Point.DrinkUnder;

namespace i3.BusinessLogic.Channels.Env.Point.DrinkUnder
{
    /// <summary>
    /// 功能：地下饮用水
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPDrinkUnderItemLogic : LogicBase
    {

        TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem = new TEnvPDrinkUnderItemVo();
        TEnvPDrinkUnderItemAccess access;

        public TEnvPDrinkUnderItemLogic()
        {
            access = new TEnvPDrinkUnderItemAccess();
        }

        public TEnvPDrinkUnderItemLogic(TEnvPDrinkUnderItemVo _tEnvPDrinkUnderItem)
        {
            tEnvPDrinkUnderItem = _tEnvPDrinkUnderItem;
            access = new TEnvPDrinkUnderItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            return access.GetSelectResultCount(tEnvPDrinkUnderItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDrinkUnderItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDrinkUnderItemVo Details(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            return access.Details(tEnvPDrinkUnderItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDrinkUnderItemVo> SelectByObject(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPDrinkUnderItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPDrinkUnderItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            return access.SelectByTable(tEnvPDrinkUnderItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象</param>
        /// <returns></returns>
        public TEnvPDrinkUnderItemVo SelectByObject(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            return access.SelectByObject(tEnvPDrinkUnderItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            return access.Create(tEnvPDrinkUnderItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            return access.Edit(tEnvPDrinkUnderItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDrinkUnderItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem_UpdateSet, TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem_UpdateWhere)
        {
            return access.Edit(tEnvPDrinkUnderItem_UpdateSet, tEnvPDrinkUnderItem_UpdateWhere);
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
        public bool Delete(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            return access.Delete(tEnvPDrinkUnderItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPDrinkUnderItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPDrinkUnderItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPDrinkUnderItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPDrinkUnderItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPDrinkUnderItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPDrinkUnderItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPDrinkUnderItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPDrinkUnderItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPDrinkUnderItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }
        
    }

}
