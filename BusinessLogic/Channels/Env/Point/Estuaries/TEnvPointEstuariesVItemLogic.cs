using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Point.Estuaries;
using i3.DataAccess.Channels.Env.Point.Estuaries;

namespace i3.BusinessLogic.Channels.Env.Point.Estuaries
{
    /// <summary>
    /// 功能：入海河口监测点监测项目表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointEstuariesVItemLogic : LogicBase
    {

	TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem = new TEnvPointEstuariesVItemVo();
    TEnvPointEstuariesVItemAccess access;
		
	public TEnvPointEstuariesVItemLogic()
	{
		  access = new TEnvPointEstuariesVItemAccess();  
	}
		
	public TEnvPointEstuariesVItemLogic(TEnvPointEstuariesVItemVo _tEnvPointEstuariesVItem)
	{
		tEnvPointEstuariesVItem = _tEnvPointEstuariesVItem;
		access = new TEnvPointEstuariesVItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            return access.GetSelectResultCount(tEnvPointEstuariesVItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVItemVo Details(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            return access.Details(tEnvPointEstuariesVItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointEstuariesVItemVo> SelectByObject(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPointEstuariesVItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPointEstuariesVItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            return access.SelectByTable(tEnvPointEstuariesVItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">对象</param>
        /// <returns></returns>
        public TEnvPointEstuariesVItemVo SelectByObject(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            return access.SelectByObject(tEnvPointEstuariesVItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            return access.Create(tEnvPointEstuariesVItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            return access.Edit(tEnvPointEstuariesVItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuariesVItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvPointEstuariesVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem_UpdateSet, TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem_UpdateWhere)
        {
            return access.Edit(tEnvPointEstuariesVItem_UpdateSet, tEnvPointEstuariesVItem_UpdateWhere);
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
        public bool Delete(TEnvPointEstuariesVItemVo tEnvPointEstuariesVItem)
        {
            return access.Delete(tEnvPointEstuariesVItem);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tEnvPointEstuariesVItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //垂线ID
	  if (tEnvPointEstuariesVItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("垂线ID不能为空");
                return false;
            }
	  //监测项目ID
	  if (tEnvPointEstuariesVItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
	  //已选条件项ID
	  if (tEnvPointEstuariesVItem.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("已选条件项ID不能为空");
                return false;
            }
	  //条件项类型（1，国标；2，行标；3，地标）
	  if (tEnvPointEstuariesVItem.CONDITION_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("条件项类型（1，国标；2，行标；3，地标）不能为空");
                return false;
            }
	  //国标上限
	  if (tEnvPointEstuariesVItem.ST_UPPER.Trim() == "")
            {
                this.Tips.AppendLine("国标上限不能为空");
                return false;
            }
	  //国标下限
	  if (tEnvPointEstuariesVItem.ST_LOWER.Trim() == "")
            {
                this.Tips.AppendLine("国标下限不能为空");
                return false;
            }
	  //备注1
	  if (tEnvPointEstuariesVItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tEnvPointEstuariesVItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tEnvPointEstuariesVItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tEnvPointEstuariesVItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tEnvPointEstuariesVItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
