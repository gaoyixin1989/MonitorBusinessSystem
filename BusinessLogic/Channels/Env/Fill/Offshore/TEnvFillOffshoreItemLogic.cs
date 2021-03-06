using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.Offshore;
using i3.DataAccess.Channels.Env.Fill.Offshore;

namespace i3.BusinessLogic.Channels.Env.Fill.Offshore
{
    /// <summary>
    /// 功能：近岸直排数据填报监测项
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// /// 修改人：刘静楠
    /// 修改时间：2013-6-25
    public class TEnvFillOffshoreItemLogic : LogicBase
    {

	TEnvFillOffshoreItemVo tEnvFillOffshoreItem = new TEnvFillOffshoreItemVo();
    TEnvFillOffshoreItemAccess access;
		
	public TEnvFillOffshoreItemLogic()
	{
		  access = new TEnvFillOffshoreItemAccess();  
	}
		
	public TEnvFillOffshoreItemLogic(TEnvFillOffshoreItemVo _tEnvFillOffshoreItem)
	{
		tEnvFillOffshoreItem = _tEnvFillOffshoreItem;
		access = new TEnvFillOffshoreItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            return access.GetSelectResultCount(tEnvFillOffshoreItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillOffshoreItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillOffshoreItemVo Details(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            return access.Details(tEnvFillOffshoreItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillOffshoreItemVo> SelectByObject(TEnvFillOffshoreItemVo tEnvFillOffshoreItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillOffshoreItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillOffshoreItemVo tEnvFillOffshoreItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillOffshoreItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillOffshoreItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            return access.SelectByTable(tEnvFillOffshoreItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">对象</param>
        /// <returns></returns>
        public TEnvFillOffshoreItemVo SelectByObject(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            return access.SelectByObject(tEnvFillOffshoreItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            return access.Create(tEnvFillOffshoreItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillOffshoreItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            return access.Edit(tEnvFillOffshoreItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillOffshoreItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillOffshoreItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillOffshoreItemVo tEnvFillOffshoreItem_UpdateSet, TEnvFillOffshoreItemVo tEnvFillOffshoreItem_UpdateWhere)
        {
            return access.Edit(tEnvFillOffshoreItem_UpdateSet, tEnvFillOffshoreItem_UpdateWhere);
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
        public bool Delete(TEnvFillOffshoreItemVo tEnvFillOffshoreItem)
        {
            return access.Delete(tEnvFillOffshoreItem);
        }


        #region//合法性验证
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillOffshoreItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillOffshoreItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillOffshoreItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillOffshoreItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillOffshoreItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillOffshoreItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillOffshoreItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillOffshoreItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillOffshoreItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillOffshoreItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }
        #endregion

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
