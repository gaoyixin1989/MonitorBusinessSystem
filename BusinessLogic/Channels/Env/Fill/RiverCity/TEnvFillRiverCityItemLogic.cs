using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.RiverCity;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.RiverCity;

namespace i3.BusinessLogic.Channels.Env.Fill.RiverCity
{
    /// <summary>
    /// 功能：城考
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverCityItemLogic : LogicBase
    {

        TEnvFillRiverCityItemVo tEnvFillRiverCityItem = new TEnvFillRiverCityItemVo();
        TEnvFillRiverCityItemAccess access;

        public TEnvFillRiverCityItemLogic()
        {
            access = new TEnvFillRiverCityItemAccess();
        }

        public TEnvFillRiverCityItemLogic(TEnvFillRiverCityItemVo _tEnvFillRiverCityItem)
        {
            tEnvFillRiverCityItem = _tEnvFillRiverCityItem;
            access = new TEnvFillRiverCityItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            return access.GetSelectResultCount(tEnvFillRiverCityItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverCityItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverCityItemVo Details(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            return access.Details(tEnvFillRiverCityItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverCityItemVo> SelectByObject(TEnvFillRiverCityItemVo tEnvFillRiverCityItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiverCityItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverCityItemVo tEnvFillRiverCityItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiverCityItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverCityItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            return access.SelectByTable(tEnvFillRiverCityItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">对象</param>
        /// <returns></returns>
        public TEnvFillRiverCityItemVo SelectByObject(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            return access.SelectByObject(tEnvFillRiverCityItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            return access.Create(tEnvFillRiverCityItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverCityItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            return access.Edit(tEnvFillRiverCityItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverCityItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverCityItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverCityItemVo tEnvFillRiverCityItem_UpdateSet, TEnvFillRiverCityItemVo tEnvFillRiverCityItem_UpdateWhere)
        {
            return access.Edit(tEnvFillRiverCityItem_UpdateSet, tEnvFillRiverCityItem_UpdateWhere);
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
        public bool Delete(TEnvFillRiverCityItemVo tEnvFillRiverCityItem)
        {
            return access.Delete(tEnvFillRiverCityItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRiverCityItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillRiverCityItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillRiverCityItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillRiverCityItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillRiverCityItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiverCityItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiverCityItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiverCityItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiverCityItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRiverCityItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
