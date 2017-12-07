using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.River;
using System.Data;
using i3.DataAccess.Channels.Env.Point.River;

namespace i3.BusinessLogic.Channels.Env.Point.River
{
    /// <summary>
    /// 功能：河流
    /// 创建日期：2013-06-13
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverVItemLogic : LogicBase
    {

        TEnvPRiverVItemVo tEnvPRiverVItem = new TEnvPRiverVItemVo();
        TEnvPRiverVItemAccess access;

        public TEnvPRiverVItemLogic()
        {
            access = new TEnvPRiverVItemAccess();
        }

        public TEnvPRiverVItemLogic(TEnvPRiverVItemVo _tEnvPRiverVItem)
        {
            tEnvPRiverVItem = _tEnvPRiverVItem;
            access = new TEnvPRiverVItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverVItemVo tEnvPRiverVItem)
        {
            return access.GetSelectResultCount(tEnvPRiverVItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverVItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverVItemVo Details(TEnvPRiverVItemVo tEnvPRiverVItem)
        {
            return access.Details(tEnvPRiverVItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverVItemVo> SelectByObject(TEnvPRiverVItemVo tEnvPRiverVItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiverVItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverVItemVo tEnvPRiverVItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiverVItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverVItemVo tEnvPRiverVItem)
        {
            return access.SelectByTable(tEnvPRiverVItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverVItem">对象</param>
        /// <returns></returns>
        public TEnvPRiverVItemVo SelectByObject(TEnvPRiverVItemVo tEnvPRiverVItem)
        {
            return access.SelectByObject(tEnvPRiverVItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverVItemVo tEnvPRiverVItem)
        {
            return access.Create(tEnvPRiverVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverVItemVo tEnvPRiverVItem)
        {
            return access.Edit(tEnvPRiverVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverVItemVo tEnvPRiverVItem_UpdateSet, TEnvPRiverVItemVo tEnvPRiverVItem_UpdateWhere)
        {
            return access.Edit(tEnvPRiverVItem_UpdateSet, tEnvPRiverVItem_UpdateWhere);
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
        public bool Delete(TEnvPRiverVItemVo tEnvPRiverVItem)
        {
            return access.Delete(tEnvPRiverVItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiverVItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPRiverVItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPRiverVItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPRiverVItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiverVItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiverVItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiverVItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiverVItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiverVItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
