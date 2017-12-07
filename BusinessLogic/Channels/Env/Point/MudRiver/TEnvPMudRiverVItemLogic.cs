using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.MudRiver;
using System.Data;
using i3.DataAccess.Channels.Env.Point.MudRiver;

namespace i3.BusinessLogic.Channels.Env.Point.MudRiver
{
    /// <summary>
    /// 功能：沉积物（河流）
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPMudRiverVItemLogic : LogicBase
    {

        TEnvPMudRiverVItemVo tEnvPMudRiverVItem = new TEnvPMudRiverVItemVo();
        TEnvPMudRiverVItemAccess access;

        public TEnvPMudRiverVItemLogic()
        {
            access = new TEnvPMudRiverVItemAccess();
        }

        public TEnvPMudRiverVItemLogic(TEnvPMudRiverVItemVo _tEnvPMudRiverVItem)
        {
            tEnvPMudRiverVItem = _tEnvPMudRiverVItem;
            access = new TEnvPMudRiverVItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPMudRiverVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPMudRiverVItemVo tEnvPMudRiverVItem)
        {
            return access.GetSelectResultCount(tEnvPMudRiverVItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPMudRiverVItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPMudRiverVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPMudRiverVItemVo Details(TEnvPMudRiverVItemVo tEnvPMudRiverVItem)
        {
            return access.Details(tEnvPMudRiverVItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPMudRiverVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPMudRiverVItemVo> SelectByObject(TEnvPMudRiverVItemVo tEnvPMudRiverVItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPMudRiverVItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPMudRiverVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPMudRiverVItemVo tEnvPMudRiverVItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPMudRiverVItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPMudRiverVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPMudRiverVItemVo tEnvPMudRiverVItem)
        {
            return access.SelectByTable(tEnvPMudRiverVItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPMudRiverVItem">对象</param>
        /// <returns></returns>
        public TEnvPMudRiverVItemVo SelectByObject(TEnvPMudRiverVItemVo tEnvPMudRiverVItem)
        {
            return access.SelectByObject(tEnvPMudRiverVItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPMudRiverVItemVo tEnvPMudRiverVItem)
        {
            return access.Create(tEnvPMudRiverVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudRiverVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudRiverVItemVo tEnvPMudRiverVItem)
        {
            return access.Edit(tEnvPMudRiverVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudRiverVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPMudRiverVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudRiverVItemVo tEnvPMudRiverVItem_UpdateSet, TEnvPMudRiverVItemVo tEnvPMudRiverVItem_UpdateWhere)
        {
            return access.Edit(tEnvPMudRiverVItem_UpdateSet, tEnvPMudRiverVItem_UpdateWhere);
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
        public bool Delete(TEnvPMudRiverVItemVo tEnvPMudRiverVItem)
        {
            return access.Delete(tEnvPMudRiverVItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPMudRiverVItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPMudRiverVItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPMudRiverVItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPMudRiverVItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPMudRiverVItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPMudRiverVItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPMudRiverVItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPMudRiverVItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPMudRiverVItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
