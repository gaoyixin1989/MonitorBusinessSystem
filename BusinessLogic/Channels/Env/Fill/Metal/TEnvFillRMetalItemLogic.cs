using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.Metal;
using i3.DataAccess.Channels.Env.Fill.Metal;

namespace i3.BusinessLogic.Channels.Env.Fill.Metal
{
    /// <summary>
    /// 功能：底泥重金属数据填报监测项表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillRMetalItemLogic : LogicBase
    {

	TEnvFillRMetalItemVo tEnvFillRMetalItem = new TEnvFillRMetalItemVo();
    TEnvFillRMetalItemAccess access;
		
	public TEnvFillRMetalItemLogic()
	{
		  access = new TEnvFillRMetalItemAccess();  
	}
		
	public TEnvFillRMetalItemLogic(TEnvFillRMetalItemVo _tEnvFillRMetalItem)
	{
		tEnvFillRMetalItem = _tEnvFillRMetalItem;
		access = new TEnvFillRMetalItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRMetalItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRMetalItemVo tEnvFillRMetalItem)
        {
            return access.GetSelectResultCount(tEnvFillRMetalItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRMetalItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRMetalItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRMetalItemVo Details(TEnvFillRMetalItemVo tEnvFillRMetalItem)
        {
            return access.Details(tEnvFillRMetalItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRMetalItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRMetalItemVo> SelectByObject(TEnvFillRMetalItemVo tEnvFillRMetalItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRMetalItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRMetalItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRMetalItemVo tEnvFillRMetalItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRMetalItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRMetalItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRMetalItemVo tEnvFillRMetalItem)
        {
            return access.SelectByTable(tEnvFillRMetalItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRMetalItem">对象</param>
        /// <returns></returns>
        public TEnvFillRMetalItemVo SelectByObject(TEnvFillRMetalItemVo tEnvFillRMetalItem)
        {
            return access.SelectByObject(tEnvFillRMetalItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRMetalItemVo tEnvFillRMetalItem)
        {
            return access.Create(tEnvFillRMetalItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRMetalItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRMetalItemVo tEnvFillRMetalItem)
        {
            return access.Edit(tEnvFillRMetalItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRMetalItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillRMetalItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRMetalItemVo tEnvFillRMetalItem_UpdateSet, TEnvFillRMetalItemVo tEnvFillRMetalItem_UpdateWhere)
        {
            return access.Edit(tEnvFillRMetalItem_UpdateSet, tEnvFillRMetalItem_UpdateWhere);
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
        public bool Delete(TEnvFillRMetalItemVo tEnvFillRMetalItem)
        {
            return access.Delete(tEnvFillRMetalItem);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //主键ID
	  if (tEnvFillRMetalItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
	  //饮用水断面数据填报ID
	  if (tEnvFillRMetalItem.RIVER_METAL_FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("饮用水断面数据填报ID不能为空");
                return false;
            }
	  //监测项ID
	  if (tEnvFillRMetalItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
	  //监测值
	  if (tEnvFillRMetalItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
	  //备注1
	  if (tEnvFillRMetalItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tEnvFillRMetalItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tEnvFillRMetalItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tEnvFillRMetalItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tEnvFillRMetalItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
