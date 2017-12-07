using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.AirKS;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.AirKS;

namespace i3.BusinessLogic.Channels.Env.Fill.AirKS
{

    /// <summary>
    /// 功能：环境空气(科室)填报监测项目
    /// 创建日期：2013-07-03
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillAirksItemLogic : LogicBase
    {

        TEnvFillAirksItemVo tEnvFillAirksItem = new TEnvFillAirksItemVo();
        TEnvFillAirksItemAccess access;

        public TEnvFillAirksItemLogic()
        {
            access = new TEnvFillAirksItemAccess();
        }

        public TEnvFillAirksItemLogic(TEnvFillAirksItemVo _tEnvFillAirksItem)
        {
            tEnvFillAirksItem = _tEnvFillAirksItem;
            access = new TEnvFillAirksItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            return access.GetSelectResultCount(tEnvFillAirksItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirksItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirksItemVo Details(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            return access.Details(tEnvFillAirksItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirksItemVo> SelectByObject(TEnvFillAirksItemVo tEnvFillAirksItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAirksItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirksItemVo tEnvFillAirksItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAirksItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAirksItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            return access.SelectByTable(tEnvFillAirksItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAirksItem">对象</param>
        /// <returns></returns>
        public TEnvFillAirksItemVo SelectByObject(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            return access.SelectByObject(tEnvFillAirksItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            return access.Create(tEnvFillAirksItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirksItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            return access.Edit(tEnvFillAirksItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirksItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAirksItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirksItemVo tEnvFillAirksItem_UpdateSet, TEnvFillAirksItemVo tEnvFillAirksItem_UpdateWhere)
        {
            return access.Edit(tEnvFillAirksItem_UpdateSet, tEnvFillAirksItem_UpdateWhere);
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
        public bool Delete(TEnvFillAirksItemVo tEnvFillAirksItem)
        {
            return access.Delete(tEnvFillAirksItem);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillAirksItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillAirksItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvFillAirksItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvFillAirksItem.ANALYSIS_METHOD_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillAirksItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillAirksItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAirksItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAirksItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAirksItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAirksItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAirksItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
