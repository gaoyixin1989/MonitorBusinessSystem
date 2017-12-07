using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseArea;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.NoiseArea;

namespace i3.BusinessLogic.Channels.Env.Fill.NoiseArea
{
    /// <summary>
    /// 功能：区域环境噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseAreaItemLogic : LogicBase
    {

        TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem = new TEnvFillNoiseAreaItemVo();
        TEnvFillNoiseAreaItemAccess access;

        public TEnvFillNoiseAreaItemLogic()
        {
            access = new TEnvFillNoiseAreaItemAccess();
        }

        public TEnvFillNoiseAreaItemLogic(TEnvFillNoiseAreaItemVo _tEnvFillNoiseAreaItem)
        {
            tEnvFillNoiseAreaItem = _tEnvFillNoiseAreaItem;
            access = new TEnvFillNoiseAreaItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            return access.GetSelectResultCount(tEnvFillNoiseAreaItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaItemVo Details(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            return access.Details(tEnvFillNoiseAreaItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseAreaItemVo> SelectByObject(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillNoiseAreaItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillNoiseAreaItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            return access.SelectByTable(tEnvFillNoiseAreaItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseAreaItemVo SelectByObject(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            return access.SelectByObject(tEnvFillNoiseAreaItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            return access.Create(tEnvFillNoiseAreaItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            return access.Edit(tEnvFillNoiseAreaItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseAreaItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem_UpdateSet, TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem_UpdateWhere)
        {
            return access.Edit(tEnvFillNoiseAreaItem_UpdateSet, tEnvFillNoiseAreaItem_UpdateWhere);
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
        public bool Delete(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            return access.Delete(tEnvFillNoiseAreaItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillNoiseAreaItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillNoiseAreaItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvFillNoiseAreaItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvFillNoiseAreaItem.ANALYSIS_METHOD_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillNoiseAreaItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillNoiseAreaItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillNoiseAreaItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillNoiseAreaItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillNoiseAreaItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillNoiseAreaItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillNoiseAreaItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
