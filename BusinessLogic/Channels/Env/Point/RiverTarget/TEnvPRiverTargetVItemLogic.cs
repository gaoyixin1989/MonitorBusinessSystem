using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverTarget;
using System.Data;
using i3.DataAccess.Channels.Env.Point.RiverTarget;

namespace i3.BusinessLogic.Channels.Env.Point.RiverTarget
{
    /// <summary>
    /// 功能：责任目标
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverTargetVItemLogic : LogicBase
    {

        TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem = new TEnvPRiverTargetVItemVo();
        TEnvPRiverTargetVItemAccess access;

        public TEnvPRiverTargetVItemLogic()
        {
            access = new TEnvPRiverTargetVItemAccess();
        }

        public TEnvPRiverTargetVItemLogic(TEnvPRiverTargetVItemVo _tEnvPRiverTargetVItem)
        {
            tEnvPRiverTargetVItem = _tEnvPRiverTargetVItem;
            access = new TEnvPRiverTargetVItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverTargetVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem)
        {
            return access.GetSelectResultCount(tEnvPRiverTargetVItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverTargetVItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverTargetVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverTargetVItemVo Details(TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem)
        {
            return access.Details(tEnvPRiverTargetVItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverTargetVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverTargetVItemVo> SelectByObject(TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiverTargetVItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverTargetVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiverTargetVItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverTargetVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem)
        {
            return access.SelectByTable(tEnvPRiverTargetVItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverTargetVItem">对象</param>
        /// <returns></returns>
        public TEnvPRiverTargetVItemVo SelectByObject(TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem)
        {
            return access.SelectByObject(tEnvPRiverTargetVItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem)
        {
            return access.Create(tEnvPRiverTargetVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverTargetVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem)
        {
            return access.Edit(tEnvPRiverTargetVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverTargetVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverTargetVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem_UpdateSet, TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem_UpdateWhere)
        {
            return access.Edit(tEnvPRiverTargetVItem_UpdateSet, tEnvPRiverTargetVItem_UpdateWhere);
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
        public bool Delete(TEnvPRiverTargetVItemVo tEnvPRiverTargetVItem)
        {
            return access.Delete(tEnvPRiverTargetVItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiverTargetVItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPRiverTargetVItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPRiverTargetVItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPRiverTargetVItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiverTargetVItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiverTargetVItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiverTargetVItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiverTargetVItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiverTargetVItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
