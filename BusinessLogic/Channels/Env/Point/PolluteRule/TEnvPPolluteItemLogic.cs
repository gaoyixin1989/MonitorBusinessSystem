using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using i3.DataAccess.Channels.Env.Point.PolluteRule;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.PolluteRule
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-29
    /// 创建人：
    /// </summary>
    public class TEnvPPolluteItemLogic : LogicBase
    {

        TEnvPPolluteItemVo tEnvPPolluteItem = new TEnvPPolluteItemVo();
        TEnvPPolluteItemAccess access;

        public TEnvPPolluteItemLogic()
        {
            access = new TEnvPPolluteItemAccess();
        }

        public TEnvPPolluteItemLogic(TEnvPPolluteItemVo _tEnvPPolluteItem)
        {
            tEnvPPolluteItem = _tEnvPPolluteItem;
            access = new TEnvPPolluteItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            return access.GetSelectResultCount(tEnvPPolluteItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPPolluteItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPPolluteItemVo Details(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            return access.Details(tEnvPPolluteItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPPolluteItemVo> SelectByObject(TEnvPPolluteItemVo tEnvPPolluteItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPPolluteItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPPolluteItemVo tEnvPPolluteItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPPolluteItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPPolluteItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            return access.SelectByTable(tEnvPPolluteItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPPolluteItem">对象</param>
        /// <returns></returns>
        public TEnvPPolluteItemVo SelectByObject(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            return access.SelectByObject(tEnvPPolluteItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            return access.Create(tEnvPPolluteItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPolluteItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            return access.Edit(tEnvPPolluteItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPolluteItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPPolluteItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteItemVo tEnvPPolluteItem_UpdateSet, TEnvPPolluteItemVo tEnvPPolluteItem_UpdateWhere)
        {
            return access.Edit(tEnvPPolluteItem_UpdateSet, tEnvPPolluteItem_UpdateWhere);
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
        public bool Delete(TEnvPPolluteItemVo tEnvPPolluteItem)
        {
            return access.Delete(tEnvPPolluteItem);
        }

        //监测项目复制
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            return access.PasteItem(strFID, strTID, strSerial);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvPPolluteItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }
}
