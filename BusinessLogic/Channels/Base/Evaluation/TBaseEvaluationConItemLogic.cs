using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Evaluation;
using i3.DataAccess.Channels.Base.Evaluation;

namespace i3.BusinessLogic.Channels.Base.Evaluation
{
    /// <summary>
    /// 功能：评价标准条件项项目信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseEvaluationConItemLogic : LogicBase
    {

	TBaseEvaluationConItemVo tBaseEvaluationConItem = new TBaseEvaluationConItemVo();
    TBaseEvaluationConItemAccess access;
		
	public TBaseEvaluationConItemLogic()
	{
		  access = new TBaseEvaluationConItemAccess();  
	}
		
	public TBaseEvaluationConItemLogic(TBaseEvaluationConItemVo _tBaseEvaluationConItem)
	{
		tBaseEvaluationConItem = _tBaseEvaluationConItem;
		access = new TBaseEvaluationConItemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            return access.GetSelectResultCount(tBaseEvaluationConItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseEvaluationConItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象条件</param>
        /// <returns>对象</returns>
        public TBaseEvaluationConItemVo Details(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            return access.Details(tBaseEvaluationConItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseEvaluationConItemVo> SelectByObject(TBaseEvaluationConItemVo tBaseEvaluationConItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseEvaluationConItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseEvaluationConItemVo tBaseEvaluationConItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseEvaluationConItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseEvaluationConItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            return access.SelectByTable(tBaseEvaluationConItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseEvaluationConItem">对象</param>
        /// <returns></returns>
        public TBaseEvaluationConItemVo SelectByObject(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            return access.SelectByObject(tBaseEvaluationConItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            return access.Create(tBaseEvaluationConItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationConItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            return access.Edit(tBaseEvaluationConItem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationConItem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseEvaluationConItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationConItemVo tBaseEvaluationConItem_UpdateSet, TBaseEvaluationConItemVo tBaseEvaluationConItem_UpdateWhere)
        {
            return access.Edit(tBaseEvaluationConItem_UpdateSet, tBaseEvaluationConItem_UpdateWhere);
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
        public bool Delete(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            return access.Delete(tBaseEvaluationConItem);
        }
	
                /// <summary>
        /// 创建原因：获取评价标准监测项目
        /// 创建人:胡方扬
        /// 创建时间:2013-07-18
        /// </summary>
        /// <param name="tBaseEvaluationConItem"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable GetEvaluItemDataDatable(TBaseEvaluationConItemVo tBaseEvaluationConItem, int iIndex, int iCount) {
            return access.GetEvaluItemDataDatable(tBaseEvaluationConItem,iIndex,iCount);
        }

                /// <summary>
        /// 创建原因：获取评价标准监测项目个数
        /// 创建人:胡方扬
        /// 创建时间:2013-07-18
        /// </summary>
        /// <param name="tBaseEvaluationConItem"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetEvaluItemDataDatableCount(TBaseEvaluationConItemVo tBaseEvaluationConItem)
        {
            return access.GetEvaluItemDataDatableCount(tBaseEvaluationConItem);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseEvaluationConItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //评价标准ID
	  if (tBaseEvaluationConItem.STANDARD_ID.Trim() == "")
            {
                this.Tips.AppendLine("评价标准ID不能为空");
                return false;
            }
	  //条件项ID
	  if (tBaseEvaluationConItem.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项ID不能为空");
                return false;
            }
	  //监测项目ID
	  if (tBaseEvaluationConItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
      //监测项目ID
      if (tBaseEvaluationConItem.MONITOR_VALUE_ID.Trim() == "")
      {
          this.Tips.AppendLine("监测值类型ID不能为空");
          return false;
      }
	  //上限运算符
	  if (tBaseEvaluationConItem.UPPER_OPERATOR.Trim() == "")
            {
                this.Tips.AppendLine("上限运算符不能为空");
                return false;
            }
	  //下限运算符
	  if (tBaseEvaluationConItem.LOWER_OPERATOR.Trim() == "")
            {
                this.Tips.AppendLine("下限运算符不能为空");
                return false;
            }
	  //排放上限
	  if (tBaseEvaluationConItem.DISCHARGE_UPPER.Trim() == "")
            {
                this.Tips.AppendLine("排放上限不能为空");
                return false;
            }
	  //排放下限
	  if (tBaseEvaluationConItem.DISCHARGE_LOWER.Trim() == "")
            {
                this.Tips.AppendLine("排放下限不能为空");
                return false;
            }
	  //单位
	  if (tBaseEvaluationConItem.UNIT.Trim() == "")
            {
                this.Tips.AppendLine("单位不能为空");
                return false;
            }
	  //0为在使用、1为停用
      if (tBaseEvaluationConItem.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("0为在使用、1为停用不能为空");
                return false;
            }
	  //备注1
	  if (tBaseEvaluationConItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseEvaluationConItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseEvaluationConItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseEvaluationConItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseEvaluationConItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
