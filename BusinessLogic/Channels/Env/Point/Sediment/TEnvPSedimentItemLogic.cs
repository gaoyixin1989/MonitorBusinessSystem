using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Sediment;
using System.Data;
using i3.DataAccess.Channels.Env.Point.Sediment;

namespace i3.BusinessLogic.Channels.Env.Point.Sediment
{
    /// <summary>
    /// 功能：底泥重金属
    /// 创建日期：2014-10-23
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSedimentItemLogic : LogicBase
    {

        TEnvPSedimentItemVo tEnvPSedimentItem = new TEnvPSedimentItemVo();
        TEnvPSedimentItemAccess access;

        public TEnvPSedimentItemLogic()
        {
            access = new TEnvPSedimentItemAccess();
        }

        public TEnvPSedimentItemLogic(TEnvPSedimentItemVo _tEnvPSedimentItem)
        {
            tEnvPSedimentItem = _tEnvPSedimentItem;
            access = new TEnvPSedimentItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            return access.GetSelectResultCount(tEnvPSedimentItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSedimentItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSedimentItemVo Details(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            return access.Details(tEnvPSedimentItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSedimentItemVo> SelectByObject(TEnvPSedimentItemVo tEnvPSedimentItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPSedimentItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSedimentItemVo tEnvPSedimentItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPSedimentItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSedimentItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            return access.SelectByTable(tEnvPSedimentItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象</param>
        /// <returns></returns>
        public TEnvPSedimentItemVo SelectByObject(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            return access.SelectByObject(tEnvPSedimentItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            return access.Create(tEnvPSedimentItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSedimentItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            return access.Edit(tEnvPSedimentItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSedimentItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSedimentItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSedimentItemVo tEnvPSedimentItem_UpdateSet, TEnvPSedimentItemVo tEnvPSedimentItem_UpdateWhere)
        {
            return access.Edit(tEnvPSedimentItem_UpdateSet, tEnvPSedimentItem_UpdateWhere);
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
        public bool Delete(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            return access.Delete(tEnvPSedimentItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPSedimentItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPSedimentItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPSedimentItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPSedimentItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPSedimentItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPSedimentItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPSedimentItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPSedimentItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPSedimentItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
