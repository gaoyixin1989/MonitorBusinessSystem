using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.NoiseRoad;
using System.Data;
using i3.DataAccess.Channels.Env.Point.NoiseRoad;

namespace i3.BusinessLogic.Channels.Env.Point.NoiseRoad
{
    /// <summary>
    /// 功能：道路交通噪声
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPNoiseRoadItemLogic : LogicBase
    {

        TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem = new TEnvPNoiseRoadItemVo();
        TEnvPNoiseRoadItemAccess access;

        public TEnvPNoiseRoadItemLogic()
        {
            access = new TEnvPNoiseRoadItemAccess();
        }

        public TEnvPNoiseRoadItemLogic(TEnvPNoiseRoadItemVo _tEnvPNoiseRoadItem)
        {
            tEnvPNoiseRoadItem = _tEnvPNoiseRoadItem;
            access = new TEnvPNoiseRoadItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPNoiseRoadItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem)
        {
            return access.GetSelectResultCount(tEnvPNoiseRoadItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseRoadItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseRoadItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseRoadItemVo Details(TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem)
        {
            return access.Details(tEnvPNoiseRoadItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseRoadItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseRoadItemVo> SelectByObject(TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPNoiseRoadItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseRoadItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPNoiseRoadItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseRoadItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem)
        {
            return access.SelectByTable(tEnvPNoiseRoadItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseRoadItem">对象</param>
        /// <returns></returns>
        public TEnvPNoiseRoadItemVo SelectByObject(TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem)
        {
            return access.SelectByObject(tEnvPNoiseRoadItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem)
        {
            return access.Create(tEnvPNoiseRoadItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseRoadItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem)
        {
            return access.Edit(tEnvPNoiseRoadItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseRoadItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseRoadItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem_UpdateSet, TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem_UpdateWhere)
        {
            return access.Edit(tEnvPNoiseRoadItem_UpdateSet, tEnvPNoiseRoadItem_UpdateWhere);
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
        public bool Delete(TEnvPNoiseRoadItemVo tEnvPNoiseRoadItem)
        {
            return access.Delete(tEnvPNoiseRoadItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvPNoiseRoadItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPNoiseRoadItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPNoiseRoadItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPNoiseRoadItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPNoiseRoadItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPNoiseRoadItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPNoiseRoadItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPNoiseRoadItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPNoiseRoadItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
