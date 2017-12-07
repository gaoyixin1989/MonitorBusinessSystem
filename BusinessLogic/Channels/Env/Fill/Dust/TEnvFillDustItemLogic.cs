using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Fill.Dust;
using i3.DataAccess.Channels.Env.Fill.Dust;

namespace i3.BusinessLogic.Channels.Env.Fill.Dust
{
    /// <summary>
    /// 功能：降尘填报监测项目
    /// 创建日期：2013-06-21
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillDustItemLogic : LogicBase
    {

        TEnvFillDustItemVo tEnvFillDustItem = new TEnvFillDustItemVo();
        TEnvFillDustItemAccess access;

        public TEnvFillDustItemLogic()
        {
            access = new TEnvFillDustItemAccess();
        }

        public TEnvFillDustItemLogic(TEnvFillDustItemVo _tEnvFillDustItem)
        {
            tEnvFillDustItem = _tEnvFillDustItem;
            access = new TEnvFillDustItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDustItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDustItemVo tEnvFillDustItem)
        {
            return access.GetSelectResultCount(tEnvFillDustItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDustItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDustItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDustItemVo Details(TEnvFillDustItemVo tEnvFillDustItem)
        {
            return access.Details(tEnvFillDustItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDustItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDustItemVo> SelectByObject(TEnvFillDustItemVo tEnvFillDustItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillDustItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDustItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDustItemVo tEnvFillDustItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillDustItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDustItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDustItemVo tEnvFillDustItem)
        {
            return access.SelectByTable(tEnvFillDustItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDustItem">对象</param>
        /// <returns></returns>
        public TEnvFillDustItemVo SelectByObject(TEnvFillDustItemVo tEnvFillDustItem)
        {
            return access.SelectByObject(tEnvFillDustItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDustItemVo tEnvFillDustItem)
        {
            return access.Create(tEnvFillDustItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDustItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDustItemVo tEnvFillDustItem)
        {
            return access.Edit(tEnvFillDustItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDustItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDustItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDustItemVo tEnvFillDustItem_UpdateSet, TEnvFillDustItemVo tEnvFillDustItem_UpdateWhere)
        {
            return access.Edit(tEnvFillDustItem_UpdateSet, tEnvFillDustItem_UpdateWhere);
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
        public bool Delete(TEnvFillDustItemVo tEnvFillDustItem)
        {
            return access.Delete(tEnvFillDustItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillDustItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillDustItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvFillDustItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvFillDustItem.ANALYSIS_METHOD_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillDustItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillDustItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillDustItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillDustItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillDustItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillDustItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillDustItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
