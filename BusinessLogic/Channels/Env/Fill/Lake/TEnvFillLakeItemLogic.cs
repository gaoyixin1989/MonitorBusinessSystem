using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Lake;
using i3.DataAccess.Channels.Env.Fill.Lake;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.Lake
{
    /// <summary>
    /// 功能：湖库填报
    /// 创建日期：2013-06-22
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillLakeItemLogic : LogicBase
    {

        TEnvFillLakeItemVo tEnvFillLakeItem = new TEnvFillLakeItemVo();
        TEnvFillLakeItemAccess access;

        public TEnvFillLakeItemLogic()
        {
            access = new TEnvFillLakeItemAccess();
        }

        public TEnvFillLakeItemLogic(TEnvFillLakeItemVo _tEnvFillLakeItem)
        {
            tEnvFillLakeItem = _tEnvFillLakeItem;
            access = new TEnvFillLakeItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            return access.GetSelectResultCount(tEnvFillLakeItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillLakeItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillLakeItemVo Details(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            return access.Details(tEnvFillLakeItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillLakeItemVo> SelectByObject(TEnvFillLakeItemVo tEnvFillLakeItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillLakeItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillLakeItemVo tEnvFillLakeItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillLakeItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillLakeItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            return access.SelectByTable(tEnvFillLakeItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillLakeItem">对象</param>
        /// <returns></returns>
        public TEnvFillLakeItemVo SelectByObject(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            return access.SelectByObject(tEnvFillLakeItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            return access.Create(tEnvFillLakeItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillLakeItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            return access.Edit(tEnvFillLakeItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillLakeItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillLakeItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillLakeItemVo tEnvFillLakeItem_UpdateSet, TEnvFillLakeItemVo tEnvFillLakeItem_UpdateWhere)
        {
            return access.Edit(tEnvFillLakeItem_UpdateSet, tEnvFillLakeItem_UpdateWhere);
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
        public bool Delete(TEnvFillLakeItemVo tEnvFillLakeItem)
        {
            return access.Delete(tEnvFillLakeItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillLakeItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillLakeItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillLakeItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillLakeItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillLakeItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillLakeItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillLakeItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillLakeItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillLakeItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillLakeItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
