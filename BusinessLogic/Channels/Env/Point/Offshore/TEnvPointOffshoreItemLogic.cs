using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Point.Offshore;
using i3.DataAccess.Channels.Env.Point.Offshore;

namespace i3.BusinessLogic.Channels.Env.Point.Offshore
{
    /// <summary>
    /// 功能：近岸直排监测项目表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    ///   /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointOffshoreItemLogic : LogicBase
    {

        TEnvPointOffshoreItemVo tEnvPointOffshoreItem = new TEnvPointOffshoreItemVo();
        TEnvPointOffshoreItemAccess access;

        public TEnvPointOffshoreItemLogic()
        {
            access = new TEnvPointOffshoreItemAccess();
        }

        public TEnvPointOffshoreItemLogic(TEnvPointOffshoreItemVo _tEnvPointOffshoreItem)
        {
            tEnvPointOffshoreItem = _tEnvPointOffshoreItem;
            access = new TEnvPointOffshoreItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            return access.GetSelectResultCount(tEnvPointOffshoreItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointOffshoreItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointOffshoreItemVo Details(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            return access.Details(tEnvPointOffshoreItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointOffshoreItemVo> SelectByObject(TEnvPointOffshoreItemVo tEnvPointOffshoreItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPointOffshoreItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointOffshoreItemVo tEnvPointOffshoreItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPointOffshoreItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointOffshoreItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            return access.SelectByTable(tEnvPointOffshoreItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">对象</param>
        /// <returns></returns>
        public TEnvPointOffshoreItemVo SelectByObject(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            return access.SelectByObject(tEnvPointOffshoreItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            return access.Create(tEnvPointOffshoreItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointOffshoreItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            return access.Edit(tEnvPointOffshoreItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointOffshoreItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointOffshoreItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointOffshoreItemVo tEnvPointOffshoreItem_UpdateSet, TEnvPointOffshoreItemVo tEnvPointOffshoreItem_UpdateWhere)
        {
            return access.Edit(tEnvPointOffshoreItem_UpdateSet, tEnvPointOffshoreItem_UpdateWhere);
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
        public bool Delete(TEnvPointOffshoreItemVo tEnvPointOffshoreItem)
        {
            return access.Delete(tEnvPointOffshoreItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvPointOffshoreItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //近岸直排监测点ID
            if (tEnvPointOffshoreItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("近岸直排监测点ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPointOffshoreItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //
            if (tEnvPointOffshoreItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //备注1
            if (tEnvPointOffshoreItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPointOffshoreItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPointOffshoreItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPointOffshoreItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPointOffshoreItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
