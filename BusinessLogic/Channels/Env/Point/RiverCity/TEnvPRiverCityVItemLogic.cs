using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverCity;
using System.Data;
using i3.DataAccess.Channels.Env.Point.RiverCity;

namespace i3.BusinessLogic.Channels.Env.Point.RiverCity
{
    /// <summary>
    /// 功能：城考
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverCityVItemLogic : LogicBase
    {

        TEnvPRiverCityVItemVo tEnvPRiverCityVItem = new TEnvPRiverCityVItemVo();
        TEnvPRiverCityVItemAccess access;

        public TEnvPRiverCityVItemLogic()
        {
            access = new TEnvPRiverCityVItemAccess();
        }

        public TEnvPRiverCityVItemLogic(TEnvPRiverCityVItemVo _tEnvPRiverCityVItem)
        {
            tEnvPRiverCityVItem = _tEnvPRiverCityVItem;
            access = new TEnvPRiverCityVItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            return access.GetSelectResultCount(tEnvPRiverCityVItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverCityVItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverCityVItemVo Details(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            return access.Details(tEnvPRiverCityVItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverCityVItemVo> SelectByObject(TEnvPRiverCityVItemVo tEnvPRiverCityVItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiverCityVItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverCityVItemVo tEnvPRiverCityVItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiverCityVItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverCityVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            return access.SelectByTable(tEnvPRiverCityVItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">对象</param>
        /// <returns></returns>
        public TEnvPRiverCityVItemVo SelectByObject(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            return access.SelectByObject(tEnvPRiverCityVItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            return access.Create(tEnvPRiverCityVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverCityVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            return access.Edit(tEnvPRiverCityVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverCityVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverCityVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverCityVItemVo tEnvPRiverCityVItem_UpdateSet, TEnvPRiverCityVItemVo tEnvPRiverCityVItem_UpdateWhere)
        {
            return access.Edit(tEnvPRiverCityVItem_UpdateSet, tEnvPRiverCityVItem_UpdateWhere);
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
        public bool Delete(TEnvPRiverCityVItemVo tEnvPRiverCityVItem)
        {
            return access.Delete(tEnvPRiverCityVItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiverCityVItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPRiverCityVItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPRiverCityVItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPRiverCityVItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiverCityVItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiverCityVItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiverCityVItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiverCityVItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiverCityVItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
