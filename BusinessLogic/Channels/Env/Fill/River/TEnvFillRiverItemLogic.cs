using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.River;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.River;

namespace i3.BusinessLogic.Channels.Env.Fill.River
{
    /// <summary>
    /// 功能：河流填报
    /// 创建日期：2013-06-18
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverItemLogic : LogicBase
    {

        TEnvFillRiverItemVo tEnvFillRiverItem = new TEnvFillRiverItemVo();
        TEnvFillRiverItemAccess access;

        public TEnvFillRiverItemLogic()
        {
            access = new TEnvFillRiverItemAccess();
        }

        public TEnvFillRiverItemLogic(TEnvFillRiverItemVo _tEnvFillRiverItem)
        {
            tEnvFillRiverItem = _tEnvFillRiverItem;
            access = new TEnvFillRiverItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            return access.GetSelectResultCount(tEnvFillRiverItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverItemVo Details(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            return access.Details(tEnvFillRiverItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverItemVo> SelectByObject(TEnvFillRiverItemVo tEnvFillRiverItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiverItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverItemVo tEnvFillRiverItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiverItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            return access.SelectByTable(tEnvFillRiverItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverItem">对象</param>
        /// <returns></returns>
        public TEnvFillRiverItemVo SelectByObject(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            return access.SelectByObject(tEnvFillRiverItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            return access.Create(tEnvFillRiverItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            return access.Edit(tEnvFillRiverItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverItemVo tEnvFillRiverItem_UpdateSet, TEnvFillRiverItemVo tEnvFillRiverItem_UpdateWhere)
        {
            return access.Edit(tEnvFillRiverItem_UpdateSet, tEnvFillRiverItem_UpdateWhere);
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
        public bool Delete(TEnvFillRiverItemVo tEnvFillRiverItem)
        {
            return access.Delete(tEnvFillRiverItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRiverItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillRiverItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillRiverItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillRiverItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillRiverItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiverItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiverItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiverItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiverItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRiverItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
