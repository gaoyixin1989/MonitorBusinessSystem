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
    /// 功能：河流底泥数据填报监测项表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillMetalItemLogic : LogicBase
    {

	TEnvFillMetalItemVo tEnvFillMetalItem = new TEnvFillMetalItemVo();
    TEnvFillMetalItemAccess access;
		
	public TEnvFillMetalItemLogic()
	{
		  access = new TEnvFillMetalItemAccess();  
	}
		
	public TEnvFillMetalItemLogic(TEnvFillMetalItemVo _tEnvFillMetalItem)
	{
		tEnvFillMetalItem = _tEnvFillMetalItem;
		access = new TEnvFillMetalItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            return access.GetSelectResultCount(tEnvFillMetalItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillMetalItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillMetalItemVo Details(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            return access.Details(tEnvFillMetalItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillMetalItemVo> SelectByObject(TEnvFillMetalItemVo tEnvFillMetalItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillMetalItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillMetalItemVo tEnvFillMetalItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillMetalItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillMetalItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            return access.SelectByTable(tEnvFillMetalItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillMetalItem">对象</param>
        /// <returns></returns>
        public TEnvFillMetalItemVo SelectByObject(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            return access.SelectByObject(tEnvFillMetalItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            return access.Create(tEnvFillMetalItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMetalItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            return access.Edit(tEnvFillMetalItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMetalItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillMetalItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMetalItemVo tEnvFillMetalItem_UpdateSet, TEnvFillMetalItemVo tEnvFillMetalItem_UpdateWhere)
        {
            return access.Edit(tEnvFillMetalItem_UpdateSet, tEnvFillMetalItem_UpdateWhere);
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
        public bool Delete(TEnvFillMetalItemVo tEnvFillMetalItem)
        {
            return access.Delete(tEnvFillMetalItem);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //主键ID
	  if (tEnvFillMetalItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
	  //饮用水断面数据填报ID
	  if (tEnvFillMetalItem.SEDIMENT_METAL_FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("饮用水断面数据填报ID不能为空");
                return false;
            }
	  //监测项ID
	  if (tEnvFillMetalItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
	  //监测值
	  if (tEnvFillMetalItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
	  //备注1
	  if (tEnvFillMetalItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tEnvFillMetalItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tEnvFillMetalItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tEnvFillMetalItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tEnvFillMetalItem.REMARK5.Trim() == "")
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
