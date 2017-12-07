using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.River30;
using System.Data;
using i3.DataAccess.Channels.Env.Point.River30;

namespace i3.BusinessLogic.Channels.Env.Point.River30
{
    /// <summary>
    /// 功能：双三十废水
    /// 创建日期：2013-06-17
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiver30VItemLogic : LogicBase
    {

        TEnvPRiver30VItemVo tEnvPRiver30VItem = new TEnvPRiver30VItemVo();
        TEnvPRiver30VItemAccess access;

        public TEnvPRiver30VItemLogic()
        {
            access = new TEnvPRiver30VItemAccess();
        }

        public TEnvPRiver30VItemLogic(TEnvPRiver30VItemVo _tEnvPRiver30VItem)
        {
            tEnvPRiver30VItem = _tEnvPRiver30VItem;
            access = new TEnvPRiver30VItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiver30VItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiver30VItemVo tEnvPRiver30VItem)
        {
            return access.GetSelectResultCount(tEnvPRiver30VItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiver30VItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiver30VItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiver30VItemVo Details(TEnvPRiver30VItemVo tEnvPRiver30VItem)
        {
            return access.Details(tEnvPRiver30VItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiver30VItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiver30VItemVo> SelectByObject(TEnvPRiver30VItemVo tEnvPRiver30VItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiver30VItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiver30VItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiver30VItemVo tEnvPRiver30VItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiver30VItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiver30VItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiver30VItemVo tEnvPRiver30VItem)
        {
            return access.SelectByTable(tEnvPRiver30VItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiver30VItem">对象</param>
        /// <returns></returns>
        public TEnvPRiver30VItemVo SelectByObject(TEnvPRiver30VItemVo tEnvPRiver30VItem)
        {
            return access.SelectByObject(tEnvPRiver30VItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiver30VItemVo tEnvPRiver30VItem)
        {
            return access.Create(tEnvPRiver30VItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiver30VItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiver30VItemVo tEnvPRiver30VItem)
        {
            return access.Edit(tEnvPRiver30VItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiver30VItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiver30VItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiver30VItemVo tEnvPRiver30VItem_UpdateSet, TEnvPRiver30VItemVo tEnvPRiver30VItem_UpdateWhere)
        {
            return access.Edit(tEnvPRiver30VItem_UpdateSet, tEnvPRiver30VItem_UpdateWhere);
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
        public bool Delete(TEnvPRiver30VItemVo tEnvPRiver30VItem)
        {
            return access.Delete(tEnvPRiver30VItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiver30VItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPRiver30VItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPRiver30VItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPRiver30VItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiver30VItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiver30VItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiver30VItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiver30VItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiver30VItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
