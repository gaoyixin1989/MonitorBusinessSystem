using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Lake;
using System.Data;
using i3.DataAccess.Channels.Env.Point.Lake;

namespace i3.BusinessLogic.Channels.Env.Point.Lake
{
    /// <summary>
    /// 功能：湖库
    /// 创建日期：2013-06-13
    /// 创建人：魏林
    /// </summary>
    public class TEnvPLakeVItemLogic : LogicBase
    {

        TEnvPLakeVItemVo tEnvPLakeVItem = new TEnvPLakeVItemVo();
        TEnvPLakeVItemAccess access;

        public TEnvPLakeVItemLogic()
        {
            access = new TEnvPLakeVItemAccess();
        }

        public TEnvPLakeVItemLogic(TEnvPLakeVItemVo _tEnvPLakeVItem)
        {
            tEnvPLakeVItem = _tEnvPLakeVItem;
            access = new TEnvPLakeVItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPLakeVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPLakeVItemVo tEnvPLakeVItem)
        {
            return access.GetSelectResultCount(tEnvPLakeVItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPLakeVItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPLakeVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPLakeVItemVo Details(TEnvPLakeVItemVo tEnvPLakeVItem)
        {
            return access.Details(tEnvPLakeVItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPLakeVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPLakeVItemVo> SelectByObject(TEnvPLakeVItemVo tEnvPLakeVItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPLakeVItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPLakeVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPLakeVItemVo tEnvPLakeVItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPLakeVItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPLakeVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPLakeVItemVo tEnvPLakeVItem)
        {
            return access.SelectByTable(tEnvPLakeVItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPLakeVItem">对象</param>
        /// <returns></returns>
        public TEnvPLakeVItemVo SelectByObject(TEnvPLakeVItemVo tEnvPLakeVItem)
        {
            return access.SelectByObject(tEnvPLakeVItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPLakeVItemVo tEnvPLakeVItem)
        {
            return access.Create(tEnvPLakeVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPLakeVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPLakeVItemVo tEnvPLakeVItem)
        {
            return access.Edit(tEnvPLakeVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPLakeVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPLakeVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPLakeVItemVo tEnvPLakeVItem_UpdateSet, TEnvPLakeVItemVo tEnvPLakeVItem_UpdateWhere)
        {
            return access.Edit(tEnvPLakeVItem_UpdateSet, tEnvPLakeVItem_UpdateWhere);
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
        public bool Delete(TEnvPLakeVItemVo tEnvPLakeVItem)
        {
            return access.Delete(tEnvPLakeVItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPLakeVItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPLakeVItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPLakeVItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPLakeVItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPLakeVItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPLakeVItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPLakeVItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPLakeVItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPLakeVItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
