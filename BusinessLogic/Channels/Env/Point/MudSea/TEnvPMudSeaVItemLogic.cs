using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.MudSea;
using System.Data;
using i3.DataAccess.Channels.Env.Point.MudSea;

namespace i3.BusinessLogic.Channels.Env.Point.MudSea
{
    /// <summary>
    /// 功能：沉积物（海水）
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPMudSeaVItemLogic : LogicBase
    {

        TEnvPMudSeaVItemVo tEnvPMudSeaVItem = new TEnvPMudSeaVItemVo();
        TEnvPMudSeaVItemAccess access;

        public TEnvPMudSeaVItemLogic()
        {
            access = new TEnvPMudSeaVItemAccess();
        }

        public TEnvPMudSeaVItemLogic(TEnvPMudSeaVItemVo _tEnvPMudSeaVItem)
        {
            tEnvPMudSeaVItem = _tEnvPMudSeaVItem;
            access = new TEnvPMudSeaVItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            return access.GetSelectResultCount(tEnvPMudSeaVItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPMudSeaVItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPMudSeaVItemVo Details(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            return access.Details(tEnvPMudSeaVItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPMudSeaVItemVo> SelectByObject(TEnvPMudSeaVItemVo tEnvPMudSeaVItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPMudSeaVItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPMudSeaVItemVo tEnvPMudSeaVItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPMudSeaVItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPMudSeaVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            return access.SelectByTable(tEnvPMudSeaVItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象</param>
        /// <returns></returns>
        public TEnvPMudSeaVItemVo SelectByObject(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            return access.SelectByObject(tEnvPMudSeaVItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            return access.Create(tEnvPMudSeaVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            return access.Edit(tEnvPMudSeaVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudSeaVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPMudSeaVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudSeaVItemVo tEnvPMudSeaVItem_UpdateSet, TEnvPMudSeaVItemVo tEnvPMudSeaVItem_UpdateWhere)
        {
            return access.Edit(tEnvPMudSeaVItem_UpdateSet, tEnvPMudSeaVItem_UpdateWhere);
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
        public bool Delete(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            return access.Delete(tEnvPMudSeaVItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPMudSeaVItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPMudSeaVItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPMudSeaVItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPMudSeaVItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPMudSeaVItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPMudSeaVItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPMudSeaVItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPMudSeaVItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPMudSeaVItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
