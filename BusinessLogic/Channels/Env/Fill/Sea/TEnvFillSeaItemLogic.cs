using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.Sea;
using i3.DataAccess.Channels.Env.Fill.Sea;

namespace i3.BusinessLogic.Channels.Env.Fill.Sea
{
    /// <summary>
    /// 功能：近海海域数据填报监测项
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-25
    /// </summary>
    public class TEnvFillSeaItemLogic : LogicBase
    {

	TEnvFillSeaItemVo tEnvFillSeaItem = new TEnvFillSeaItemVo();
    TEnvFillSeaItemAccess access;
		
	public TEnvFillSeaItemLogic()
	{
		  access = new TEnvFillSeaItemAccess();  
	}
		
	public TEnvFillSeaItemLogic(TEnvFillSeaItemVo _tEnvFillSeaItem)
	{
		tEnvFillSeaItem = _tEnvFillSeaItem;
		access = new TEnvFillSeaItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            return access.GetSelectResultCount(tEnvFillSeaItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSeaItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSeaItemVo Details(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            return access.Details(tEnvFillSeaItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSeaItemVo> SelectByObject(TEnvFillSeaItemVo tEnvFillSeaItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillSeaItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSeaItemVo tEnvFillSeaItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillSeaItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSeaItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            return access.SelectByTable(tEnvFillSeaItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSeaItem">对象</param>
        /// <returns></returns>
        public TEnvFillSeaItemVo SelectByObject(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            return access.SelectByObject(tEnvFillSeaItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            return access.Create(tEnvFillSeaItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeaItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            return access.Edit(tEnvFillSeaItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeaItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillSeaItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeaItemVo tEnvFillSeaItem_UpdateSet, TEnvFillSeaItemVo tEnvFillSeaItem_UpdateWhere)
        {
            return access.Edit(tEnvFillSeaItem_UpdateSet, tEnvFillSeaItem_UpdateWhere);
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
        public bool Delete(TEnvFillSeaItemVo tEnvFillSeaItem)
        {
            return access.Delete(tEnvFillSeaItem);
        }

        //  /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillSeaItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillSeaItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillSeaItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillSeaItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillSeaItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillSeaItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillSeaItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillSeaItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillSeaItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillSeaItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
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
    }
}
