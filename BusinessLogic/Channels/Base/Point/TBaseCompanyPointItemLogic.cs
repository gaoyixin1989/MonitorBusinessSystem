using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Point;
using i3.DataAccess.Channels.Base.Point;

namespace i3.BusinessLogic.Channels.Base.Point
{
    /// <summary>
    /// 功能：监测点项目明细表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseCompanyPointItemLogic : LogicBase
    {

	TBaseCompanyPointItemVo tBaseCompanyPointItem = new TBaseCompanyPointItemVo();
    TBaseCompanyPointItemAccess access;
		
	public TBaseCompanyPointItemLogic()
	{
		  access = new TBaseCompanyPointItemAccess();  
	}
		
	public TBaseCompanyPointItemLogic(TBaseCompanyPointItemVo _tBaseCompanyPointItem)
	{
		tBaseCompanyPointItem = _tBaseCompanyPointItem;
		access = new TBaseCompanyPointItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            return access.GetSelectResultCount(tBaseCompanyPointItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseCompanyPointItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象条件</param>
        /// <returns>对象</returns>
        public TBaseCompanyPointItemVo Details(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            return access.Details(tBaseCompanyPointItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseCompanyPointItemVo> SelectByObject(TBaseCompanyPointItemVo tBaseCompanyPointItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseCompanyPointItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseCompanyPointItemVo tBaseCompanyPointItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseCompanyPointItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseCompanyPointItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            return access.SelectByTable(tBaseCompanyPointItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseCompanyPointItem">对象</param>
        /// <returns></returns>
        public TBaseCompanyPointItemVo SelectByObject(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            return access.SelectByObject(tBaseCompanyPointItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            return access.Create(tBaseCompanyPointItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyPointItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            return access.Edit(tBaseCompanyPointItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyPointItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseCompanyPointItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyPointItemVo tBaseCompanyPointItem_UpdateSet, TBaseCompanyPointItemVo tBaseCompanyPointItem_UpdateWhere)
        {
            return access.Edit(tBaseCompanyPointItem_UpdateSet, tBaseCompanyPointItem_UpdateWhere);
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
        public bool Delete(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            return access.Delete(tBaseCompanyPointItem);
        }
	

                /// <summary>
        /// 根据企业点位ID 获取当前点位下的所有监测项目
        /// </summary>
        /// <param name="tBaseCompanyPointItem"></param>
        /// <returns></returns>
        public DataTable SelectItemsForPoint(TBaseCompanyPointItemVo tBaseCompanyPointItem)
        {
            return access.SelectItemsForPoint(tBaseCompanyPointItem);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseCompanyPointItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测点ID
	  if (tBaseCompanyPointItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
	  //监测项目ID
	  if (tBaseCompanyPointItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
	  //已选条件项ID
	  if (tBaseCompanyPointItem.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("已选条件项ID不能为空");
                return false;
            }
	  //条件项类型（1，国标；2，行标；3，地标）
	  if (tBaseCompanyPointItem.CONDITION_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("条件项类型（1，国标；2，行标；3，地标）不能为空");
                return false;
            }
	  //国标上限
	  if (tBaseCompanyPointItem.ST_UPPER.Trim() == "")
            {
                this.Tips.AppendLine("国标上限不能为空");
                return false;
            }
	  //国标下限
	  if (tBaseCompanyPointItem.ST_LOWER.Trim() == "")
            {
                this.Tips.AppendLine("国标下限不能为空");
                return false;
            }
	  //备注1
	  if (tBaseCompanyPointItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseCompanyPointItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseCompanyPointItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseCompanyPointItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseCompanyPointItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
