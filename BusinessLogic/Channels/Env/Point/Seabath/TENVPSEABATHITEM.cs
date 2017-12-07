using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.Seabath;
using i3.DataAccess.Channels.Env.Point.Seabath;

namespace i3.BusinessLogic.Channels.Env.Point.Seabath
{
    /// <summary>
    /// 功能：海水浴场
    /// 创建日期：2013-06-18
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvPSeabathItemLogic : LogicBase
    {

        TEnvPSeabathItemVo tEnvPSeabathItem = new TEnvPSeabathItemVo();
        TEnvPSeabathItemAccess access;

        public TEnvPSeabathItemLogic()
        {
            access = new TEnvPSeabathItemAccess();
        }

        public TEnvPSeabathItemLogic(TEnvPSeabathItemVo _tEnvPSeabathItem)
        {
            tEnvPSeabathItem = _tEnvPSeabathItem;
            access = new TEnvPSeabathItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            return access.GetSelectResultCount(tEnvPSeabathItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSeabathItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSeabathItemVo Details(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            return access.Details(tEnvPSeabathItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSeabathItemVo> SelectByObject(TEnvPSeabathItemVo tEnvPSeabathItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPSeabathItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSeabathItemVo tEnvPSeabathItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPSeabathItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSeabathItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            return access.SelectByTable(tEnvPSeabathItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象</param>
        /// <returns></returns>
        public TEnvPSeabathItemVo SelectByObject(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            return access.SelectByObject(tEnvPSeabathItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            return access.Create(tEnvPSeabathItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSeabathItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            return access.Edit(tEnvPSeabathItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSeabathItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSeabathItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSeabathItemVo tEnvPSeabathItem_UpdateSet, TEnvPSeabathItemVo tEnvPSeabathItem_UpdateWhere)
        {
            return access.Edit(tEnvPSeabathItem_UpdateSet, tEnvPSeabathItem_UpdateWhere);
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
        public bool Delete(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            return access.Delete(tEnvPSeabathItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvPSeabathItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPSeabathItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPSeabathItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPSeabathItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPSeabathItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPSeabathItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPSeabathItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPSeabathItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPSeabathItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
