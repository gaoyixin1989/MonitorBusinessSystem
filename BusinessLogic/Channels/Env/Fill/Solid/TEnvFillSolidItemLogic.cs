using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Solid;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.Solid;

namespace i3.BusinessLogic.Channels.Env.Fill.Solid
{
    /// <summary>
    /// 功能：固废数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSolidItemLogic : LogicBase
    {

        TEnvFillSolidItemVo tEnvFillSolidItem = new TEnvFillSolidItemVo();
        TEnvFillSolidItemAccess access;

        public TEnvFillSolidItemLogic()
        {
            access = new TEnvFillSolidItemAccess();
        }

        public TEnvFillSolidItemLogic(TEnvFillSolidItemVo _tEnvFillSolidItem)
        {
            tEnvFillSolidItem = _tEnvFillSolidItem;
            access = new TEnvFillSolidItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            return access.GetSelectResultCount(tEnvFillSolidItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSolidItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSolidItemVo Details(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            return access.Details(tEnvFillSolidItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSolidItemVo> SelectByObject(TEnvFillSolidItemVo tEnvFillSolidItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillSolidItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSolidItemVo tEnvFillSolidItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillSolidItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSolidItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            return access.SelectByTable(tEnvFillSolidItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSolidItem">对象</param>
        /// <returns></returns>
        public TEnvFillSolidItemVo SelectByObject(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            return access.SelectByObject(tEnvFillSolidItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            return access.Create(tEnvFillSolidItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSolidItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            return access.Edit(tEnvFillSolidItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSolidItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSolidItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSolidItemVo tEnvFillSolidItem_UpdateSet, TEnvFillSolidItemVo tEnvFillSolidItem_UpdateWhere)
        {
            return access.Edit(tEnvFillSolidItem_UpdateSet, tEnvFillSolidItem_UpdateWhere);
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
        public bool Delete(TEnvFillSolidItemVo tEnvFillSolidItem)
        {
            return access.Delete(tEnvFillSolidItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillSolidItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillSolidItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillSolidItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillSolidItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillSolidItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillSolidItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillSolidItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillSolidItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillSolidItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillSolidItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
