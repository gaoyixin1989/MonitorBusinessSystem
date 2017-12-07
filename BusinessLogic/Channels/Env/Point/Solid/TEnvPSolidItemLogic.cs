using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Solid;
using System.Data;
using i3.DataAccess.Channels.Env.Point.Solid;

namespace i3.BusinessLogic.Channels.Env.Point.Solid
{
    /// <summary>
    /// 功能：固废
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSolidItemLogic : LogicBase
    {

        TEnvPSolidItemVo tEnvPSolidItem = new TEnvPSolidItemVo();
        TEnvPSolidItemAccess access;

        public TEnvPSolidItemLogic()
        {
            access = new TEnvPSolidItemAccess();
        }

        public TEnvPSolidItemLogic(TEnvPSolidItemVo _tEnvPSolidItem)
        {
            tEnvPSolidItem = _tEnvPSolidItem;
            access = new TEnvPSolidItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSolidItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSolidItemVo tEnvPSolidItem)
        {
            return access.GetSelectResultCount(tEnvPSolidItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSolidItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSolidItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSolidItemVo Details(TEnvPSolidItemVo tEnvPSolidItem)
        {
            return access.Details(tEnvPSolidItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSolidItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSolidItemVo> SelectByObject(TEnvPSolidItemVo tEnvPSolidItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPSolidItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSolidItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSolidItemVo tEnvPSolidItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPSolidItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSolidItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSolidItemVo tEnvPSolidItem)
        {
            return access.SelectByTable(tEnvPSolidItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSolidItem">对象</param>
        /// <returns></returns>
        public TEnvPSolidItemVo SelectByObject(TEnvPSolidItemVo tEnvPSolidItem)
        {
            return access.SelectByObject(tEnvPSolidItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSolidItemVo tEnvPSolidItem)
        {
            return access.Create(tEnvPSolidItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSolidItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSolidItemVo tEnvPSolidItem)
        {
            return access.Edit(tEnvPSolidItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSolidItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSolidItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSolidItemVo tEnvPSolidItem_UpdateSet, TEnvPSolidItemVo tEnvPSolidItem_UpdateWhere)
        {
            return access.Edit(tEnvPSolidItem_UpdateSet, tEnvPSolidItem_UpdateWhere);
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
        public bool Delete(TEnvPSolidItemVo tEnvPSolidItem)
        {
            return access.Delete(tEnvPSolidItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPSolidItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPSolidItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPSolidItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPSolidItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPSolidItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPSolidItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPSolidItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPSolidItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPSolidItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
