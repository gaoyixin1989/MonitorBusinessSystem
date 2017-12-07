using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.River30;
using i3.DataAccess.Channels.Env.Fill.River30;

namespace i3.BusinessLogic.Channels.Env.Fill.River30
{
    /// <summary>
    /// 功能：双三十水断面数据填报监测项表
    /// 创建日期：2013-05-08
    /// 创建人：潘德军
    /// 修改人：刘静楠
    /// 修改时间：2013-6-25
    /// </summary>
    public class TEnvFillRiver30ItemLogic : LogicBase
    {

        TEnvFillRiver30ItemVo tEnvFillRiver30Item = new TEnvFillRiver30ItemVo();
        TEnvFillRiver30ItemAccess access;

        public TEnvFillRiver30ItemLogic()
        {
            access = new TEnvFillRiver30ItemAccess();
        }

        public TEnvFillRiver30ItemLogic(TEnvFillRiver30ItemVo _tEnvFillRiver30Item)
        {
            tEnvFillRiver30Item = _tEnvFillRiver30Item;
            access = new TEnvFillRiver30ItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            return access.GetSelectResultCount(tEnvFillRiver30Item);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiver30ItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiver30ItemVo Details(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            return access.Details(tEnvFillRiver30Item);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiver30ItemVo> SelectByObject(TEnvFillRiver30ItemVo tEnvFillRiver30Item, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiver30Item, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiver30ItemVo tEnvFillRiver30Item, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiver30Item, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiver30Item"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            return access.SelectByTable(tEnvFillRiver30Item);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable SelectByTable(string where)
        {
            return access.SelectByTable(where);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiver30Item">对象</param>
        /// <returns></returns>
        public TEnvFillRiver30ItemVo SelectByObject(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            return access.SelectByObject(tEnvFillRiver30Item);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            return access.Create(tEnvFillRiver30Item);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiver30Item">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            return access.Edit(tEnvFillRiver30Item);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiver30Item_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiver30Item_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiver30ItemVo tEnvFillRiver30Item_UpdateSet, TEnvFillRiver30ItemVo tEnvFillRiver30Item_UpdateWhere)
        {
            return access.Edit(tEnvFillRiver30Item_UpdateSet, tEnvFillRiver30Item_UpdateWhere);
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
        public bool Delete(TEnvFillRiver30ItemVo tEnvFillRiver30Item)
        {
            return access.Delete(tEnvFillRiver30Item);
        }




        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRiver30Item.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillRiver30Item.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillRiver30Item.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillRiver30Item.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillRiver30Item.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiver30Item.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiver30Item.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiver30Item.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiver30Item.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRiver30Item.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }


    }
}
