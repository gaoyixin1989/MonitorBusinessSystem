using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.Rain;
using i3.DataAccess.Channels.Env.Fill.Rain;

namespace i3.BusinessLogic.Channels.Env.Fill.Rain
{
    /// <summary>
    /// 功能：降水数据填报监测项表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-25
    /// </summary>
    public class TEnvFillRainItemLogic : LogicBase
    {

	TEnvFillRainItemVo tEnvFillRainItem = new TEnvFillRainItemVo();
    TEnvFillRainItemAccess access;
		
	public TEnvFillRainItemLogic()
	{
		  access = new TEnvFillRainItemAccess();  
	}
		
	public TEnvFillRainItemLogic(TEnvFillRainItemVo _tEnvFillRainItem)
	{
		tEnvFillRainItem = _tEnvFillRainItem;
		access = new TEnvFillRainItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRainItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRainItemVo tEnvFillRainItem)
        {
            return access.GetSelectResultCount(tEnvFillRainItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRainItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRainItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRainItemVo Details(TEnvFillRainItemVo tEnvFillRainItem)
        {
            return access.Details(tEnvFillRainItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRainItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRainItemVo> SelectByObject(TEnvFillRainItemVo tEnvFillRainItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRainItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRainItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRainItemVo tEnvFillRainItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRainItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRainItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRainItemVo tEnvFillRainItem)
        {
            return access.SelectByTable(tEnvFillRainItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRainItem">对象</param>
        /// <returns></returns>
        public TEnvFillRainItemVo SelectByObject(TEnvFillRainItemVo tEnvFillRainItem)
        {
            return access.SelectByObject(tEnvFillRainItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRainItemVo tEnvFillRainItem)
        {
            return access.Create(tEnvFillRainItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRainItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRainItemVo tEnvFillRainItem)
        {
            return access.Edit(tEnvFillRainItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRainItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillRainItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRainItemVo tEnvFillRainItem_UpdateSet, TEnvFillRainItemVo tEnvFillRainItem_UpdateWhere)
        {
            return access.Edit(tEnvFillRainItem_UpdateSet, tEnvFillRainItem_UpdateWhere);
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
        public bool Delete(TEnvFillRainItemVo tEnvFillRainItem)
        {
            return access.Delete(tEnvFillRainItem);
        }

        #region//合法性验证
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRainItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillRainItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvFillRainItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvFillRainItem.ANALYSIS_METHOD_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillRainItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillRainItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRainItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRainItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRainItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRainItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRainItem.REMARK5.Trim() == "")
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
