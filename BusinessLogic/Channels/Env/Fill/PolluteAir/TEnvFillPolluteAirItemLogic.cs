using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.PolluteAir;
using i3.DataAccess.Channels.Env.Fill.PolluteAir;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.PolluteAir
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-09-03
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteAirItemLogic : LogicBase
    {

        TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem = new TEnvFillPolluteAirItemVo();
        TEnvFillPolluteAirItemAccess access;

        public TEnvFillPolluteAirItemLogic()
        {
            access = new TEnvFillPolluteAirItemAccess();
        }

        public TEnvFillPolluteAirItemLogic(TEnvFillPolluteAirItemVo _tEnvFillPolluteAirItem)
        {
            tEnvFillPolluteAirItem = _tEnvFillPolluteAirItem;
            access = new TEnvFillPolluteAirItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            return access.GetSelectResultCount(tEnvFillPolluteAirItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteAirItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteAirItemVo Details(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            return access.Details(tEnvFillPolluteAirItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPolluteAirItemVo> SelectByObject(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillPolluteAirItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillPolluteAirItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            return access.SelectByTable(tEnvFillPolluteAirItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">对象</param>
        /// <returns></returns>
        public TEnvFillPolluteAirItemVo SelectByObject(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            return access.SelectByObject(tEnvFillPolluteAirItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            return access.Create(tEnvFillPolluteAirItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            return access.Edit(tEnvFillPolluteAirItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteAirItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPolluteAirItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem_UpdateSet, TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem_UpdateWhere)
        {
            return access.Edit(tEnvFillPolluteAirItem_UpdateSet, tEnvFillPolluteAirItem_UpdateWhere);
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
        public bool Delete(TEnvFillPolluteAirItemVo tEnvFillPolluteAirItem)
        {
            return access.Delete(tEnvFillPolluteAirItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvFillPolluteAirItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.UP_LINE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.DOWN_LINE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.UOM.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.STANDARD.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.OQTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.POLLUTEPER.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.POLLUTECALPER.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.IS_STANDARD.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.AIRQTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAirItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }

}
