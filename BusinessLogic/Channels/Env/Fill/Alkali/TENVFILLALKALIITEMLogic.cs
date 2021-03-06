﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Fill.Alkali;
using System.Data;
using i3.ValueObject.Channels.Env.Fill.Alkali;

namespace i3.BusinessLogic.Channels.Env.Fill.Alkali
{
    /// <summary>
    /// 功能：硫酸盐化速率填报监测项目 
    /// 修改日期：2013-06-21
    /// 修改人：刘静楠
    /// </summary>
    public class TEnvFillAlkaliItemLogic : LogicBase
    {

        TEnvFillAlkaliItemVo tEnvFillAlkaliItem = new TEnvFillAlkaliItemVo();
        TEnvFillAlkaliItemAccess access;

        public TEnvFillAlkaliItemLogic()
        {
            access = new TEnvFillAlkaliItemAccess();
        }

        public TEnvFillAlkaliItemLogic(TEnvFillAlkaliItemVo _tEnvFillAlkaliItem)
        {
            tEnvFillAlkaliItem = _tEnvFillAlkaliItem;
            access = new TEnvFillAlkaliItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            return access.GetSelectResultCount(tEnvFillAlkaliItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAlkaliItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAlkaliItemVo Details(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            return access.Details(tEnvFillAlkaliItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAlkaliItemVo> SelectByObject(TEnvFillAlkaliItemVo tEnvFillAlkaliItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAlkaliItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAlkaliItemVo tEnvFillAlkaliItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAlkaliItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAlkaliItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            return access.SelectByTable(tEnvFillAlkaliItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">对象</param>
        /// <returns></returns>
        public TEnvFillAlkaliItemVo SelectByObject(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            return access.SelectByObject(tEnvFillAlkaliItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            return access.Create(tEnvFillAlkaliItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlkaliItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            return access.Edit(tEnvFillAlkaliItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlkaliItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAlkaliItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlkaliItemVo tEnvFillAlkaliItem_UpdateSet, TEnvFillAlkaliItemVo tEnvFillAlkaliItem_UpdateWhere)
        {
            return access.Edit(tEnvFillAlkaliItem_UpdateSet, tEnvFillAlkaliItem_UpdateWhere);
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
        public bool Delete(TEnvFillAlkaliItemVo tEnvFillAlkaliItem)
        {
            return access.Delete(tEnvFillAlkaliItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillAlkaliItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillAlkaliItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillAlkaliItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillAlkaliItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillAlkaliItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAlkaliItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAlkaliItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAlkaliItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAlkaliItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAlkaliItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
