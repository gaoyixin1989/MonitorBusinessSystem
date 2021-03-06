using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Item;
using i3.DataAccess.Channels.Base.Item;

namespace i3.BusinessLogic.Channels.Base.Item
{
    /// <summary>
    /// 功能：监测项目子项管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemSubItemLogic : LogicBase
    {

	TBaseItemSubItemVo tBaseItemSubItem = new TBaseItemSubItemVo();
    TBaseItemSubItemAccess access;
		
	public TBaseItemSubItemLogic()
	{
		  access = new TBaseItemSubItemAccess();  
	}
		
	public TBaseItemSubItemLogic(TBaseItemSubItemVo _tBaseItemSubItem)
	{
		tBaseItemSubItem = _tBaseItemSubItem;
		access = new TBaseItemSubItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemSubItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseItemSubItemVo tBaseItemSubItem)
        {
            return access.GetSelectResultCount(tBaseItemSubItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseItemSubItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseItemSubItem">对象条件</param>
        /// <returns>对象</returns>
        public TBaseItemSubItemVo Details(TBaseItemSubItemVo tBaseItemSubItem)
        {
            return access.Details(tBaseItemSubItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseItemSubItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseItemSubItemVo> SelectByObject(TBaseItemSubItemVo tBaseItemSubItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseItemSubItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemSubItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseItemSubItemVo tBaseItemSubItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseItemSubItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseItemSubItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseItemSubItemVo tBaseItemSubItem)
        {
            return access.SelectByTable(tBaseItemSubItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseItemSubItem">对象</param>
        /// <returns></returns>
        public TBaseItemSubItemVo SelectByObject(TBaseItemSubItemVo tBaseItemSubItem)
        {
            return access.SelectByObject(tBaseItemSubItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseItemSubItemVo tBaseItemSubItem)
        {
            return access.Create(tBaseItemSubItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemSubItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemSubItemVo tBaseItemSubItem)
        {
            return access.Edit(tBaseItemSubItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemSubItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseItemSubItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemSubItemVo tBaseItemSubItem_UpdateSet, TBaseItemSubItemVo tBaseItemSubItem_UpdateWhere)
        {
            return access.Edit(tBaseItemSubItem_UpdateSet, tBaseItemSubItem_UpdateWhere);
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
        public bool Delete(TBaseItemSubItemVo tBaseItemSubItem)
        {
            return access.Delete(tBaseItemSubItem);
        }

        /// <summary>
        /// 获得父项ID
        /// </summary>
        /// <param name="strItemiD">子项ID</param>
        /// <returns>对象</returns>
        public TBaseItemSubItemVo getParentIDByItem(string strItemiD)
        {
            return access.getParentIDByItem(strItemiD);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseItemSubItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测项目ID
	  if (tBaseItemSubItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
	  //监测父项ID
	  if (tBaseItemSubItem.PARENT_ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测父项ID不能为空");
                return false;
            }
	  //备注1
	  if (tBaseItemSubItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseItemSubItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseItemSubItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseItemSubItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseItemSubItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
