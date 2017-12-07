using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Air30;
using i3.DataAccess.Channels.Env.Point.Air30;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.Air30
{
    /// <summary>
    /// 功能：双三十废气
    /// 创建日期：2013-06-17
    /// 创建人：魏林
    /// </summary>
    public class TEnvPAir30ItemLogic : LogicBase
    {

        TEnvPAir30ItemVo tEnvPAir30Item = new TEnvPAir30ItemVo();
        TEnvPAir30ItemAccess access;

        public TEnvPAir30ItemLogic()
        {
            access = new TEnvPAir30ItemAccess();
        }

        public TEnvPAir30ItemLogic(TEnvPAir30ItemVo _tEnvPAir30Item)
        {
            tEnvPAir30Item = _tEnvPAir30Item;
            access = new TEnvPAir30ItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPAir30Item">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            return access.GetSelectResultCount(tEnvPAir30Item);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPAir30ItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPAir30Item">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPAir30ItemVo Details(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            return access.Details(tEnvPAir30Item);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPAir30Item">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPAir30ItemVo> SelectByObject(TEnvPAir30ItemVo tEnvPAir30Item, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPAir30Item, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPAir30Item">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPAir30ItemVo tEnvPAir30Item, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPAir30Item, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPAir30Item"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            return access.SelectByTable(tEnvPAir30Item);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPAir30Item">对象</param>
        /// <returns></returns>
        public TEnvPAir30ItemVo SelectByObject(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            return access.SelectByObject(tEnvPAir30Item);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            return access.Create(tEnvPAir30Item);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir30Item">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            return access.Edit(tEnvPAir30Item);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir30Item_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPAir30Item_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAir30ItemVo tEnvPAir30Item_UpdateSet, TEnvPAir30ItemVo tEnvPAir30Item_UpdateWhere)
        {
            return access.Edit(tEnvPAir30Item_UpdateSet, tEnvPAir30Item_UpdateWhere);
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
        public bool Delete(TEnvPAir30ItemVo tEnvPAir30Item)
        {
            return access.Delete(tEnvPAir30Item);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvPAir30Item.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPAir30Item.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPAir30Item.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPAir30Item.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPAir30Item.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPAir30Item.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPAir30Item.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPAir30Item.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPAir30Item.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
