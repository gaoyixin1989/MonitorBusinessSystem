using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.MudRiver;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.MudRiver;

namespace i3.BusinessLogic.Channels.Env.Fill.MudRiver
{
    /// <summary>
    /// 功能：沉积物（河流）数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillMudRiverItemLogic : LogicBase
    {

        TEnvFillMudRiverItemVo tEnvFillMudRiverItem = new TEnvFillMudRiverItemVo();
        TEnvFillMudRiverItemAccess access;

        public TEnvFillMudRiverItemLogic()
        {
            access = new TEnvFillMudRiverItemAccess();
        }

        public TEnvFillMudRiverItemLogic(TEnvFillMudRiverItemVo _tEnvFillMudRiverItem)
        {
            tEnvFillMudRiverItem = _tEnvFillMudRiverItem;
            access = new TEnvFillMudRiverItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            return access.GetSelectResultCount(tEnvFillMudRiverItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillMudRiverItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillMudRiverItemVo Details(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            return access.Details(tEnvFillMudRiverItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillMudRiverItemVo> SelectByObject(TEnvFillMudRiverItemVo tEnvFillMudRiverItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillMudRiverItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillMudRiverItemVo tEnvFillMudRiverItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillMudRiverItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillMudRiverItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            return access.SelectByTable(tEnvFillMudRiverItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">对象</param>
        /// <returns></returns>
        public TEnvFillMudRiverItemVo SelectByObject(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            return access.SelectByObject(tEnvFillMudRiverItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            return access.Create(tEnvFillMudRiverItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudRiverItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            return access.Edit(tEnvFillMudRiverItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudRiverItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillMudRiverItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudRiverItemVo tEnvFillMudRiverItem_UpdateSet, TEnvFillMudRiverItemVo tEnvFillMudRiverItem_UpdateWhere)
        {
            return access.Edit(tEnvFillMudRiverItem_UpdateSet, tEnvFillMudRiverItem_UpdateWhere);
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
        public bool Delete(TEnvFillMudRiverItemVo tEnvFillMudRiverItem)
        {
            return access.Delete(tEnvFillMudRiverItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillMudRiverItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillMudRiverItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillMudRiverItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillMudRiverItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillMudRiverItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillMudRiverItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillMudRiverItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillMudRiverItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillMudRiverItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillMudRiverItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
