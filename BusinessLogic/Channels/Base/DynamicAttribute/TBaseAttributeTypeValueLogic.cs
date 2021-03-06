using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.DataAccess.Channels.Base.DynamicAttribute;

namespace i3.BusinessLogic.Channels.Base.DynamicAttribute
{
    /// <summary>
    /// 功能：属性类别配置表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttributeTypeValueLogic : LogicBase
    {

	TBaseAttributeTypeValueVo tBaseAttributeTypeValue = new TBaseAttributeTypeValueVo();
    TBaseAttributeTypeValueAccess access;
		
	public TBaseAttributeTypeValueLogic()
	{
		  access = new TBaseAttributeTypeValueAccess();  
	}
		
	public TBaseAttributeTypeValueLogic(TBaseAttributeTypeValueVo _tBaseAttributeTypeValue)
	{
		tBaseAttributeTypeValue = _tBaseAttributeTypeValue;
		access = new TBaseAttributeTypeValueAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            return access.GetSelectResultCount(tBaseAttributeTypeValue);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseAttributeTypeValueVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象条件</param>
        /// <returns>对象</returns>
        public TBaseAttributeTypeValueVo Details(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            return access.Details(tBaseAttributeTypeValue);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseAttributeTypeValueVo> SelectByObject(TBaseAttributeTypeValueVo tBaseAttributeTypeValue, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseAttributeTypeValue, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseAttributeTypeValueVo tBaseAttributeTypeValue, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseAttributeTypeValue, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseAttributeTypeValue"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            return access.SelectByTable(tBaseAttributeTypeValue);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">对象</param>
        /// <returns></returns>
        public TBaseAttributeTypeValueVo SelectByObject(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            return access.SelectByObject(tBaseAttributeTypeValue);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            return access.Create(tBaseAttributeTypeValue);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeTypeValue">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            return access.Edit(tBaseAttributeTypeValue);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeTypeValue_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseAttributeTypeValue_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeTypeValueVo tBaseAttributeTypeValue_UpdateSet, TBaseAttributeTypeValueVo tBaseAttributeTypeValue_UpdateWhere)
        {
            return access.Edit(tBaseAttributeTypeValue_UpdateSet, tBaseAttributeTypeValue_UpdateWhere);
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
        public bool Delete(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            return access.Delete(tBaseAttributeTypeValue);
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseAttributeTypeValueVo tBaseAttributeTypeValue, int iIndex, int iCount)
        {
            return access.SelectDefinedTadble(tBaseAttributeTypeValue, iIndex, iCount);
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseAttributeTypeValueVo tBaseAttributeTypeValue)
        {
            return access.GetSelecDefinedtResultCount(tBaseAttributeTypeValue);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseAttributeTypeValue.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测类别
	  if (tBaseAttributeTypeValue.ITEM_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("监测类别不能为空");
                return false;
            }
	  //排口点位类别
	  if (tBaseAttributeTypeValue.OUTLETPOINT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("排口点位类别不能为空");
                return false;
            }
	  //属性类别
	  if (tBaseAttributeTypeValue.ATTRIBUTE_TYPE_ID.Trim() == "")
            {
                this.Tips.AppendLine("属性类别不能为空");
                return false;
            }
	  //属性
	  if (tBaseAttributeTypeValue.ATTRIBUTE_ID.Trim() == "")
            {
                this.Tips.AppendLine("属性不能为空");
                return false;
            }
	  //排序
	  if (tBaseAttributeTypeValue.SN.Trim() == "")
            {
                this.Tips.AppendLine("排序不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tBaseAttributeTypeValue.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //备注1
	  if (tBaseAttributeTypeValue.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseAttributeTypeValue.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseAttributeTypeValue.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseAttributeTypeValue.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseAttributeTypeValue.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
