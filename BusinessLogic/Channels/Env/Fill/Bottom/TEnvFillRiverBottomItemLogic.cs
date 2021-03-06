using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.Bottom;
using i3.DataAccess.Channels.Env.Fill.Bottom;

namespace i3.BusinessLogic.Channels.Env.Fill.Bottom
{
    /// <summary>
    /// 功能：河流底泥数据填报监测项表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillRiverBottomItemLogic : LogicBase
    {

	TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem = new TEnvFillRiverBottomItemVo();
    TEnvFillRiverBottomItemAccess access;
		
	public TEnvFillRiverBottomItemLogic()
	{
		  access = new TEnvFillRiverBottomItemAccess();  
	}
		
	public TEnvFillRiverBottomItemLogic(TEnvFillRiverBottomItemVo _tEnvFillRiverBottomItem)
	{
		tEnvFillRiverBottomItem = _tEnvFillRiverBottomItem;
		access = new TEnvFillRiverBottomItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverBottomItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem)
        {
            return access.GetSelectResultCount(tEnvFillRiverBottomItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverBottomItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverBottomItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverBottomItemVo Details(TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem)
        {
            return access.Details(tEnvFillRiverBottomItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverBottomItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverBottomItemVo> SelectByObject(TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiverBottomItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverBottomItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiverBottomItem, iIndex, iCount);
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
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverBottomItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem)
        {
            return access.SelectByTable(tEnvFillRiverBottomItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverBottomItem">对象</param>
        /// <returns></returns>
        public TEnvFillRiverBottomItemVo SelectByObject(TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem)
        {
            return access.SelectByObject(tEnvFillRiverBottomItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem)
        {
            return access.Create(tEnvFillRiverBottomItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverBottomItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem)
        {
            return access.Edit(tEnvFillRiverBottomItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverBottomItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillRiverBottomItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem_UpdateSet, TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem_UpdateWhere)
        {
            return access.Edit(tEnvFillRiverBottomItem_UpdateSet, tEnvFillRiverBottomItem_UpdateWhere);
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
        public bool Delete(TEnvFillRiverBottomItemVo tEnvFillRiverBottomItem)
        {
            return access.Delete(tEnvFillRiverBottomItem);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //主键ID
	  if (tEnvFillRiverBottomItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
	  //饮用水断面数据填报ID
	  if (tEnvFillRiverBottomItem.RIVER_SEDIMENT_FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("饮用水断面数据填报ID不能为空");
                return false;
            }
	  //监测项ID
	  if (tEnvFillRiverBottomItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
	  //监测值
	  if (tEnvFillRiverBottomItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
	  //备注1
	  if (tEnvFillRiverBottomItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tEnvFillRiverBottomItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tEnvFillRiverBottomItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tEnvFillRiverBottomItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tEnvFillRiverBottomItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
