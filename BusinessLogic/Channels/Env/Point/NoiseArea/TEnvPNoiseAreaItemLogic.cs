using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.NoiseArea;
using System.Data;
using i3.DataAccess.Channels.Env.Point.NoiseArea;

namespace i3.BusinessLogic.Channels.Env.Point.NoiseArea
{
    /// <summary>
    /// 功能：区域环境噪声
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPNoiseAreaItemLogic : LogicBase
    {

        TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem = new TEnvPNoiseAreaItemVo();
        TEnvPNoiseAreaItemAccess access;

        public TEnvPNoiseAreaItemLogic()
        {
            access = new TEnvPNoiseAreaItemAccess();
        }

        public TEnvPNoiseAreaItemLogic(TEnvPNoiseAreaItemVo _tEnvPNoiseAreaItem)
        {
            tEnvPNoiseAreaItem = _tEnvPNoiseAreaItem;
            access = new TEnvPNoiseAreaItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            return access.GetSelectResultCount(tEnvPNoiseAreaItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseAreaItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseAreaItemVo Details(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            return access.Details(tEnvPNoiseAreaItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseAreaItemVo> SelectByObject(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPNoiseAreaItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPNoiseAreaItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            return access.SelectByTable(tEnvPNoiseAreaItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象</param>
        /// <returns></returns>
        public TEnvPNoiseAreaItemVo SelectByObject(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            return access.SelectByObject(tEnvPNoiseAreaItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            return access.Create(tEnvPNoiseAreaItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            return access.Edit(tEnvPNoiseAreaItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseAreaItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem_UpdateSet, TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem_UpdateWhere)
        {
            return access.Edit(tEnvPNoiseAreaItem_UpdateSet, tEnvPNoiseAreaItem_UpdateWhere);
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
        public bool Delete(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            return access.Delete(tEnvPNoiseAreaItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvPNoiseAreaItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPNoiseAreaItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPNoiseAreaItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPNoiseAreaItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPNoiseAreaItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPNoiseAreaItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPNoiseAreaItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPNoiseAreaItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPNoiseAreaItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
