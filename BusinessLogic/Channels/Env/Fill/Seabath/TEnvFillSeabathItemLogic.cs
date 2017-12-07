using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Seabath;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.Seabath;

namespace i3.BusinessLogic.Channels.Env.Fill.Seabath
{
    /// <summary>
    /// 功能：海水浴场填报监测项目
    /// 创建日期：2013-06-25
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillSeabathItemLogic : LogicBase
    {

        TEnvFillSeabathItemVo tEnvFillSeabathItem = new TEnvFillSeabathItemVo();
        TEnvFillSeabathItemAccess access;

        public TEnvFillSeabathItemLogic()
        {
            access = new TEnvFillSeabathItemAccess();
        }

        public TEnvFillSeabathItemLogic(TEnvFillSeabathItemVo _tEnvFillSeabathItem)
        {
            tEnvFillSeabathItem = _tEnvFillSeabathItem;
            access = new TEnvFillSeabathItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            return access.GetSelectResultCount(tEnvFillSeabathItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSeabathItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSeabathItemVo Details(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            return access.Details(tEnvFillSeabathItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSeabathItemVo> SelectByObject(TEnvFillSeabathItemVo tEnvFillSeabathItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillSeabathItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSeabathItemVo tEnvFillSeabathItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillSeabathItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSeabathItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            return access.SelectByTable(tEnvFillSeabathItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSeabathItem">对象</param>
        /// <returns></returns>
        public TEnvFillSeabathItemVo SelectByObject(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            return access.SelectByObject(tEnvFillSeabathItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            return access.Create(tEnvFillSeabathItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeabathItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            return access.Edit(tEnvFillSeabathItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeabathItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSeabathItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeabathItemVo tEnvFillSeabathItem_UpdateSet, TEnvFillSeabathItemVo tEnvFillSeabathItem_UpdateWhere)
        {
            return access.Edit(tEnvFillSeabathItem_UpdateSet, tEnvFillSeabathItem_UpdateWhere);
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
        public bool Delete(TEnvFillSeabathItemVo tEnvFillSeabathItem)
        {
            return access.Delete(tEnvFillSeabathItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillSeabathItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillSeabathItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillSeabathItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillSeabathItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillSeabathItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillSeabathItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillSeabathItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillSeabathItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillSeabathItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillSeabathItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
