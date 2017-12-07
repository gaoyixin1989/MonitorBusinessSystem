using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.MudSea;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.MudSea;

namespace i3.BusinessLogic.Channels.Env.Fill.MudSea
{
    /// <summary>
    /// 功能：沉积物（海水）数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillMudSeaItemLogic : LogicBase
    {

        TEnvFillMudSeaItemVo tEnvFillMudSeaItem = new TEnvFillMudSeaItemVo();
        TEnvFillMudSeaItemAccess access;

        public TEnvFillMudSeaItemLogic()
        {
            access = new TEnvFillMudSeaItemAccess();
        }

        public TEnvFillMudSeaItemLogic(TEnvFillMudSeaItemVo _tEnvFillMudSeaItem)
        {
            tEnvFillMudSeaItem = _tEnvFillMudSeaItem;
            access = new TEnvFillMudSeaItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            return access.GetSelectResultCount(tEnvFillMudSeaItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillMudSeaItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillMudSeaItemVo Details(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            return access.Details(tEnvFillMudSeaItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillMudSeaItemVo> SelectByObject(TEnvFillMudSeaItemVo tEnvFillMudSeaItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillMudSeaItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillMudSeaItemVo tEnvFillMudSeaItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillMudSeaItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillMudSeaItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            return access.SelectByTable(tEnvFillMudSeaItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">对象</param>
        /// <returns></returns>
        public TEnvFillMudSeaItemVo SelectByObject(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            return access.SelectByObject(tEnvFillMudSeaItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            return access.Create(tEnvFillMudSeaItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudSeaItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            return access.Edit(tEnvFillMudSeaItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudSeaItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillMudSeaItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudSeaItemVo tEnvFillMudSeaItem_UpdateSet, TEnvFillMudSeaItemVo tEnvFillMudSeaItem_UpdateWhere)
        {
            return access.Edit(tEnvFillMudSeaItem_UpdateSet, tEnvFillMudSeaItem_UpdateWhere);
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
        public bool Delete(TEnvFillMudSeaItemVo tEnvFillMudSeaItem)
        {
            return access.Delete(tEnvFillMudSeaItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillMudSeaItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillMudSeaItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillMudSeaItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillMudSeaItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillMudSeaItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillMudSeaItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillMudSeaItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillMudSeaItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillMudSeaItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillMudSeaItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
