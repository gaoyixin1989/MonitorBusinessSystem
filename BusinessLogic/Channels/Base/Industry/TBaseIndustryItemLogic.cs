using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Industry;
using i3.DataAccess.Channels.Base.Industry;

namespace i3.BusinessLogic.Channels.Base.Industry
{
    /// <summary>
    /// 功能：行业项目管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseIndustryItemLogic : LogicBase
    {

	TBaseIndustryItemVo tBaseIndustryItem = new TBaseIndustryItemVo();
    TBaseIndustryItemAccess access;
		
	public TBaseIndustryItemLogic()
	{
		  access = new TBaseIndustryItemAccess();  
	}
		
	public TBaseIndustryItemLogic(TBaseIndustryItemVo _tBaseIndustryItem)
	{
		tBaseIndustryItem = _tBaseIndustryItem;
		access = new TBaseIndustryItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseIndustryItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseIndustryItemVo tBaseIndustryItem)
        {
            return access.GetSelectResultCount(tBaseIndustryItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseIndustryItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseIndustryItem">对象条件</param>
        /// <returns>对象</returns>
        public TBaseIndustryItemVo Details(TBaseIndustryItemVo tBaseIndustryItem)
        {
            return access.Details(tBaseIndustryItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseIndustryItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseIndustryItemVo> SelectByObject(TBaseIndustryItemVo tBaseIndustryItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseIndustryItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseIndustryItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseIndustryItemVo tBaseIndustryItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseIndustryItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseIndustryItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseIndustryItemVo tBaseIndustryItem)
        {
            return access.SelectByTable(tBaseIndustryItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseIndustryItem">对象</param>
        /// <returns></returns>
        public TBaseIndustryItemVo SelectByObject(TBaseIndustryItemVo tBaseIndustryItem)
        {
            return access.SelectByObject(tBaseIndustryItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseIndustryItemVo tBaseIndustryItem)
        {
            return access.Create(tBaseIndustryItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseIndustryItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseIndustryItemVo tBaseIndustryItem)
        {
            return access.Edit(tBaseIndustryItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseIndustryItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseIndustryItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseIndustryItemVo tBaseIndustryItem_UpdateSet, TBaseIndustryItemVo tBaseIndustryItem_UpdateWhere)
        {
            return access.Edit(tBaseIndustryItem_UpdateSet, tBaseIndustryItem_UpdateWhere);
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
        public bool Delete(TBaseIndustryItemVo tBaseIndustryItem)
        {
            return access.Delete(tBaseIndustryItem);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseIndustryItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //行业ID
	  if (tBaseIndustryItem.INDUSTRY_ID.Trim() == "")
            {
                this.Tips.AppendLine("行业ID不能为空");
                return false;
            }
	  //监测项目ID
	  if (tBaseIndustryItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
	  //备注1
	  if (tBaseIndustryItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseIndustryItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseIndustryItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseIndustryItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseIndustryItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
