using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Air30;
using i3.DataAccess.Channels.Env.Fill.Air30;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.Air30
{

    /// <summary>
    /// 功能：双30废气
    /// 创建日期：2013-06-25
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillAir30ItemLogic : LogicBase
    {

        TEnvFillAir30ItemVo tEnvFillAir30Item = new TEnvFillAir30ItemVo();
        TEnvFillAir30ItemAccess access;

        public TEnvFillAir30ItemLogic()
        {
            access = new TEnvFillAir30ItemAccess();
        }

        public TEnvFillAir30ItemLogic(TEnvFillAir30ItemVo _tEnvFillAir30Item)
        {
            tEnvFillAir30Item = _tEnvFillAir30Item;
            access = new TEnvFillAir30ItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAir30Item">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAir30ItemVo tEnvFillAir30Item)
        {
            return access.GetSelectResultCount(tEnvFillAir30Item);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAir30ItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAir30Item">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAir30ItemVo Details(TEnvFillAir30ItemVo tEnvFillAir30Item)
        {
            return access.Details(tEnvFillAir30Item);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAir30Item">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAir30ItemVo> SelectByObject(TEnvFillAir30ItemVo tEnvFillAir30Item, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAir30Item, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAir30Item">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAir30ItemVo tEnvFillAir30Item, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAir30Item, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAir30Item"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAir30ItemVo tEnvFillAir30Item)
        {
            return access.SelectByTable(tEnvFillAir30Item);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAir30Item">对象</param>
        /// <returns></returns>
        public TEnvFillAir30ItemVo SelectByObject(TEnvFillAir30ItemVo tEnvFillAir30Item)
        {
            return access.SelectByObject(tEnvFillAir30Item);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAir30ItemVo tEnvFillAir30Item)
        {
            return access.Create(tEnvFillAir30Item);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAir30Item">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAir30ItemVo tEnvFillAir30Item)
        {
            return access.Edit(tEnvFillAir30Item);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAir30Item_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAir30Item_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAir30ItemVo tEnvFillAir30Item_UpdateSet, TEnvFillAir30ItemVo tEnvFillAir30Item_UpdateWhere)
        {
            return access.Edit(tEnvFillAir30Item_UpdateSet, tEnvFillAir30Item_UpdateWhere);
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
        public bool Delete(TEnvFillAir30ItemVo tEnvFillAir30Item)
        {
            return access.Delete(tEnvFillAir30Item);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillAir30Item.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillAir30Item.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillAir30Item.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillAir30Item.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillAir30Item.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标倍数
            if (tEnvFillAir30Item.UP_DOUBLE.Trim() == "")
            {
                this.Tips.AppendLine("超标倍数不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAir30Item.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAir30Item.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAir30Item.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAir30Item.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAir30Item.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
