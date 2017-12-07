using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseFun;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.NoiseFun;

namespace i3.BusinessLogic.Channels.Env.Fill.NoiseFun
{
    /// <summary>
    /// 功能：功能区噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseFunctionItemLogic : LogicBase
    {

        TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem = new TEnvFillNoiseFunctionItemVo();
        TEnvFillNoiseFunctionItemAccess access;

        public TEnvFillNoiseFunctionItemLogic()
        {
            access = new TEnvFillNoiseFunctionItemAccess();
        }

        public TEnvFillNoiseFunctionItemLogic(TEnvFillNoiseFunctionItemVo _tEnvFillNoiseFunctionItem)
        {
            tEnvFillNoiseFunctionItem = _tEnvFillNoiseFunctionItem;
            access = new TEnvFillNoiseFunctionItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            return access.GetSelectResultCount(tEnvFillNoiseFunctionItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionItemVo Details(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            return access.Details(tEnvFillNoiseFunctionItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseFunctionItemVo> SelectByObject(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillNoiseFunctionItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillNoiseFunctionItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            return access.SelectByTable(tEnvFillNoiseFunctionItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseFunctionItemVo SelectByObject(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            return access.SelectByObject(tEnvFillNoiseFunctionItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            return access.Create(tEnvFillNoiseFunctionItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            return access.Edit(tEnvFillNoiseFunctionItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseFunctionItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem_UpdateSet, TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem_UpdateWhere)
        {
            return access.Edit(tEnvFillNoiseFunctionItem_UpdateSet, tEnvFillNoiseFunctionItem_UpdateWhere);
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
        public bool Delete(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            return access.Delete(tEnvFillNoiseFunctionItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillNoiseFunctionItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillNoiseFunctionItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvFillNoiseFunctionItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvFillNoiseFunctionItem.ANALYSIS_METHOD_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillNoiseFunctionItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillNoiseFunctionItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillNoiseFunctionItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillNoiseFunctionItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillNoiseFunctionItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillNoiseFunctionItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillNoiseFunctionItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
