using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Soil;
using System.Data;
using i3.DataAccess.Channels.Env.Point.Soil;

namespace i3.BusinessLogic.Channels.Env.Point.Soil
{
    /// <summary>
    /// 功能：土壤
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSoilItemLogic : LogicBase
    {

        TEnvPSoilItemVo tEnvPSoilItem = new TEnvPSoilItemVo();
        TEnvPSoilItemAccess access;

        public TEnvPSoilItemLogic()
        {
            access = new TEnvPSoilItemAccess();
        }

        public TEnvPSoilItemLogic(TEnvPSoilItemVo _tEnvPSoilItem)
        {
            tEnvPSoilItem = _tEnvPSoilItem;
            access = new TEnvPSoilItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSoilItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSoilItemVo tEnvPSoilItem)
        {
            return access.GetSelectResultCount(tEnvPSoilItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSoilItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSoilItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSoilItemVo Details(TEnvPSoilItemVo tEnvPSoilItem)
        {
            return access.Details(tEnvPSoilItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSoilItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSoilItemVo> SelectByObject(TEnvPSoilItemVo tEnvPSoilItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPSoilItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSoilItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSoilItemVo tEnvPSoilItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPSoilItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSoilItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSoilItemVo tEnvPSoilItem)
        {
            return access.SelectByTable(tEnvPSoilItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSoilItem">对象</param>
        /// <returns></returns>
        public TEnvPSoilItemVo SelectByObject(TEnvPSoilItemVo tEnvPSoilItem)
        {
            return access.SelectByObject(tEnvPSoilItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSoilItemVo tEnvPSoilItem)
        {
            return access.Create(tEnvPSoilItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSoilItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSoilItemVo tEnvPSoilItem)
        {
            return access.Edit(tEnvPSoilItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSoilItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSoilItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSoilItemVo tEnvPSoilItem_UpdateSet, TEnvPSoilItemVo tEnvPSoilItem_UpdateWhere)
        {
            return access.Edit(tEnvPSoilItem_UpdateSet, tEnvPSoilItem_UpdateWhere);
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
        public bool Delete(TEnvPSoilItemVo tEnvPSoilItem)
        {
            return access.Delete(tEnvPSoilItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPSoilItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPSoilItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPSoilItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPSoilItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPSoilItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPSoilItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPSoilItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPSoilItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPSoilItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
