using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Sediment;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.Sediment;

namespace i3.BusinessLogic.Channels.Env.Fill.Sediment
{
    /// <summary>
    /// 功能：底泥重金属填报
    /// 创建日期：2014-10-23
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSedimentItemLogic : LogicBase
    {

        TEnvFillSedimentItemVo tEnvFillSedimentItem = new TEnvFillSedimentItemVo();
        TEnvFillSedimentItemAccess access;

        public TEnvFillSedimentItemLogic()
        {
            access = new TEnvFillSedimentItemAccess();
        }

        public TEnvFillSedimentItemLogic(TEnvFillSedimentItemVo _tEnvFillSedimentItem)
        {
            tEnvFillSedimentItem = _tEnvFillSedimentItem;
            access = new TEnvFillSedimentItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            return access.GetSelectResultCount(tEnvFillSedimentItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSedimentItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSedimentItemVo Details(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            return access.Details(tEnvFillSedimentItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSedimentItemVo> SelectByObject(TEnvFillSedimentItemVo tEnvFillSedimentItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillSedimentItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSedimentItemVo tEnvFillSedimentItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillSedimentItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSedimentItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            return access.SelectByTable(tEnvFillSedimentItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSedimentItem">对象</param>
        /// <returns></returns>
        public TEnvFillSedimentItemVo SelectByObject(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            return access.SelectByObject(tEnvFillSedimentItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            return access.Create(tEnvFillSedimentItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSedimentItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            return access.Edit(tEnvFillSedimentItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSedimentItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSedimentItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSedimentItemVo tEnvFillSedimentItem_UpdateSet, TEnvFillSedimentItemVo tEnvFillSedimentItem_UpdateWhere)
        {
            return access.Edit(tEnvFillSedimentItem_UpdateSet, tEnvFillSedimentItem_UpdateWhere);
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
        public bool Delete(TEnvFillSedimentItemVo tEnvFillSedimentItem)
        {
            return access.Delete(tEnvFillSedimentItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillSedimentItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillSedimentItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillSedimentItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillSedimentItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillSedimentItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillSedimentItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillSedimentItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillSedimentItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillSedimentItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillSedimentItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
