using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseRoad;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.NoiseRoad;

namespace i3.BusinessLogic.Channels.Env.Fill.NoiseRoad
{
    /// <summary>
    /// 功能：道路交通噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseRoadItemLogic : LogicBase
    {

        TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem = new TEnvFillNoiseRoadItemVo();
        TEnvFillNoiseRoadItemAccess access;

        public TEnvFillNoiseRoadItemLogic()
        {
            access = new TEnvFillNoiseRoadItemAccess();
        }

        public TEnvFillNoiseRoadItemLogic(TEnvFillNoiseRoadItemVo _tEnvFillNoiseRoadItem)
        {
            tEnvFillNoiseRoadItem = _tEnvFillNoiseRoadItem;
            access = new TEnvFillNoiseRoadItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            return access.GetSelectResultCount(tEnvFillNoiseRoadItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseRoadItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseRoadItemVo Details(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            return access.Details(tEnvFillNoiseRoadItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseRoadItemVo> SelectByObject(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillNoiseRoadItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillNoiseRoadItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            return access.SelectByTable(tEnvFillNoiseRoadItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseRoadItemVo SelectByObject(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            return access.SelectByObject(tEnvFillNoiseRoadItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            return access.Create(tEnvFillNoiseRoadItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            return access.Edit(tEnvFillNoiseRoadItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseRoadItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem_UpdateSet, TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem_UpdateWhere)
        {
            return access.Edit(tEnvFillNoiseRoadItem_UpdateSet, tEnvFillNoiseRoadItem_UpdateWhere);
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
        public bool Delete(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            return access.Delete(tEnvFillNoiseRoadItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillNoiseRoadItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillNoiseRoadItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvFillNoiseRoadItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvFillNoiseRoadItem.ANALYSIS_METHOD_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillNoiseRoadItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillNoiseRoadItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillNoiseRoadItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillNoiseRoadItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillNoiseRoadItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillNoiseRoadItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillNoiseRoadItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
