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
    /// 功能：属性值表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttrbuteValueLogic : LogicBase
    {

	TBaseAttrbuteValueVo tBaseAttrbuteValue = new TBaseAttrbuteValueVo();
    TBaseAttrbuteValueAccess access;
		
	public TBaseAttrbuteValueLogic()
	{
		  access = new TBaseAttrbuteValueAccess();  
	}
		
	public TBaseAttrbuteValueLogic(TBaseAttrbuteValueVo _tBaseAttrbuteValue)
	{
		tBaseAttrbuteValue = _tBaseAttrbuteValue;
		access = new TBaseAttrbuteValueAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            return access.GetSelectResultCount(tBaseAttrbuteValue);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseAttrbuteValueVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象条件</param>
        /// <returns>对象</returns>
        public TBaseAttrbuteValueVo Details(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            return access.Details(tBaseAttrbuteValue);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseAttrbuteValueVo> SelectByObject(TBaseAttrbuteValueVo tBaseAttrbuteValue, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseAttrbuteValue, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseAttrbuteValueVo tBaseAttrbuteValue, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseAttrbuteValue, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseAttrbuteValue"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            return access.SelectByTable(tBaseAttrbuteValue);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseAttrbuteValue">对象</param>
        /// <returns></returns>
        public TBaseAttrbuteValueVo SelectByObject(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            return access.SelectByObject(tBaseAttrbuteValue);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            return access.Create(tBaseAttrbuteValue);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttrbuteValue">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            return access.Edit(tBaseAttrbuteValue);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttrbuteValue_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseAttrbuteValue_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttrbuteValueVo tBaseAttrbuteValue_UpdateSet, TBaseAttrbuteValueVo tBaseAttrbuteValue_UpdateWhere)
        {
            return access.Edit(tBaseAttrbuteValue_UpdateSet, tBaseAttrbuteValue_UpdateWhere);
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
        public bool Delete(TBaseAttrbuteValueVo tBaseAttrbuteValue)
        {
            return access.Delete(tBaseAttrbuteValue);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseAttrbuteValue.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //对象类型
	  if (tBaseAttrbuteValue.OBJECT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("对象类型不能为空");
                return false;
            }
	  //对象ID
	  if (tBaseAttrbuteValue.OBJECT_ID.Trim() == "")
            {
                this.Tips.AppendLine("对象ID不能为空");
                return false;
            }
	  //属性名称
	  if (tBaseAttrbuteValue.ATTRBUTE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("属性名称不能为空");
                return false;
            }
	  //属性值
	  if (tBaseAttrbuteValue.ATTRBUTE_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("属性值不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tBaseAttrbuteValue.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //备注1
	  if (tBaseAttrbuteValue.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseAttrbuteValue.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseAttrbuteValue.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseAttrbuteValue.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseAttrbuteValue.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
