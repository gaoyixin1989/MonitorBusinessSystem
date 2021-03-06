using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Point.Sea;
using i3.DataAccess.Channels.Env.Point.Sea;

namespace i3.BusinessLogic.Channels.Env.Point.Sea
{
    /// <summary>
    /// 功能：近海海域监测项目表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointSeaItemLogic : LogicBase
    {

        TEnvPointSeaItemVo tEnvPointSeaItem = new TEnvPointSeaItemVo();
        TEnvPointSeaItemAccess access;

        public TEnvPointSeaItemLogic()
        {
            access = new TEnvPointSeaItemAccess();
        }

        public TEnvPointSeaItemLogic(TEnvPointSeaItemVo _tEnvPointSeaItem)
        {
            tEnvPointSeaItem = _tEnvPointSeaItem;
            access = new TEnvPointSeaItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            return access.GetSelectResultCount(tEnvPointSeaItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointSeaItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointSeaItemVo Details(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            return access.Details(tEnvPointSeaItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointSeaItemVo> SelectByObject(TEnvPointSeaItemVo tEnvPointSeaItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPointSeaItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointSeaItemVo tEnvPointSeaItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPointSeaItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointSeaItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            return access.SelectByTable(tEnvPointSeaItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointSeaItem">对象</param>
        /// <returns></returns>
        public TEnvPointSeaItemVo SelectByObject(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            return access.SelectByObject(tEnvPointSeaItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            return access.Create(tEnvPointSeaItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointSeaItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            return access.Edit(tEnvPointSeaItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointSeaItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointSeaItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointSeaItemVo tEnvPointSeaItem_UpdateSet, TEnvPointSeaItemVo tEnvPointSeaItem_UpdateWhere)
        {
            return access.Edit(tEnvPointSeaItem_UpdateSet, tEnvPointSeaItem_UpdateWhere);
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
        public bool Delete(TEnvPointSeaItemVo tEnvPointSeaItem)
        {
            return access.Delete(tEnvPointSeaItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvPointSeaItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //近海海域监测点ID
            if (tEnvPointSeaItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("近海海域监测点ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPointSeaItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //
            if (tEnvPointSeaItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //备注1
            if (tEnvPointSeaItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPointSeaItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPointSeaItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPointSeaItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPointSeaItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
