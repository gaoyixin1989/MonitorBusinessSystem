using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.Algae;
using i3.DataAccess.Channels.Env.Fill.Algae;

namespace i3.BusinessLogic.Channels.Env.Fill.Algae
{
    /// <summary>
    /// 功能：蓝藻水华数据填报监测项表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillAlgaeItemLogic : LogicBase
    {

	TEnvFillAlgaeItemVo tEnvFillAlgaeItem = new TEnvFillAlgaeItemVo();
    TEnvFillAlgaeItemAccess access;
		
	public TEnvFillAlgaeItemLogic()
	{
		  access = new TEnvFillAlgaeItemAccess();  
	}
		
	public TEnvFillAlgaeItemLogic(TEnvFillAlgaeItemVo _tEnvFillAlgaeItem)
	{
		tEnvFillAlgaeItem = _tEnvFillAlgaeItem;
		access = new TEnvFillAlgaeItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            return access.GetSelectResultCount(tEnvFillAlgaeItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAlgaeItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAlgaeItemVo Details(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            return access.Details(tEnvFillAlgaeItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAlgaeItemVo> SelectByObject(TEnvFillAlgaeItemVo tEnvFillAlgaeItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAlgaeItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAlgaeItemVo tEnvFillAlgaeItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAlgaeItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAlgaeItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            return access.SelectByTable(tEnvFillAlgaeItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">对象</param>
        /// <returns></returns>
        public TEnvFillAlgaeItemVo SelectByObject(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            return access.SelectByObject(tEnvFillAlgaeItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            return access.Create(tEnvFillAlgaeItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlgaeItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            return access.Edit(tEnvFillAlgaeItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlgaeItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillAlgaeItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlgaeItemVo tEnvFillAlgaeItem_UpdateSet, TEnvFillAlgaeItemVo tEnvFillAlgaeItem_UpdateWhere)
        {
            return access.Edit(tEnvFillAlgaeItem_UpdateSet, tEnvFillAlgaeItem_UpdateWhere);
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
        public bool Delete(TEnvFillAlgaeItemVo tEnvFillAlgaeItem)
        {
            return access.Delete(tEnvFillAlgaeItem);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //主键ID
	  if (tEnvFillAlgaeItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
	  //饮用水断面数据填报ID
	  if (tEnvFillAlgaeItem.ALGAE_FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("饮用水断面数据填报ID不能为空");
                return false;
            }
	  //监测项ID
	  if (tEnvFillAlgaeItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
	  //监测值
	  if (tEnvFillAlgaeItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
	  //备注1
	  if (tEnvFillAlgaeItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tEnvFillAlgaeItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tEnvFillAlgaeItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tEnvFillAlgaeItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tEnvFillAlgaeItem.REMARK5.Trim() == "")
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
