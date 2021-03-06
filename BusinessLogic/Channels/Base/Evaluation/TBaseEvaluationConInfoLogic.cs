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
    /// 功能：评价标准条件项
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseEvaluationConInfoLogic : LogicBase
    {

	TBaseEvaluationConInfoVo tBaseEvaluationConInfo = new TBaseEvaluationConInfoVo();
    TBaseEvaluationConInfoAccess access;
		
	public TBaseEvaluationConInfoLogic()
	{
		  access = new TBaseEvaluationConInfoAccess();  
	}
		
	public TBaseEvaluationConInfoLogic(TBaseEvaluationConInfoVo _tBaseEvaluationConInfo)
	{
		tBaseEvaluationConInfo = _tBaseEvaluationConInfo;
		access = new TBaseEvaluationConInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            return access.GetSelectResultCount(tBaseEvaluationConInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseEvaluationConInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseEvaluationConInfoVo Details(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            return access.Details(tBaseEvaluationConInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseEvaluationConInfoVo> SelectByObject(TBaseEvaluationConInfoVo tBaseEvaluationConInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseEvaluationConInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseEvaluationConInfoVo tBaseEvaluationConInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseEvaluationConInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseEvaluationConInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            return access.SelectByTable(tBaseEvaluationConInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">对象</param>
        /// <returns></returns>
        public TBaseEvaluationConInfoVo SelectByObject(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            return access.SelectByObject(tBaseEvaluationConInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            return access.Create(tBaseEvaluationConInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationConInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            return access.Edit(tBaseEvaluationConInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationConInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseEvaluationConInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationConInfoVo tBaseEvaluationConInfo_UpdateSet, TBaseEvaluationConInfoVo tBaseEvaluationConInfo_UpdateWhere)
        {
            return access.Edit(tBaseEvaluationConInfo_UpdateSet, tBaseEvaluationConInfo_UpdateWhere);
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
        public bool Delete(TBaseEvaluationConInfoVo tBaseEvaluationConInfo)
        {
            return access.Delete(tBaseEvaluationConInfo);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseEvaluationConInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //评价标准ID
	  if (tBaseEvaluationConInfo.STANDARD_ID.Trim() == "")
            {
                this.Tips.AppendLine("评价标准ID不能为空");
                return false;
            }
	  //条件项编号
	  if (tBaseEvaluationConInfo.CONDITION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("条件项编号不能为空");
                return false;
            }
	  //父节点ID，如果为根节点，则父节点为“0”
	  if (tBaseEvaluationConInfo.PARENT_ID.Trim() == "")
            {
                this.Tips.AppendLine("父节点ID，如果为根节点，则父节点为“0”不能为空");
                return false;
            }
	  //条件项名称
	  if (tBaseEvaluationConInfo.CONDITION_NAME.Trim() == "")
            {
                this.Tips.AppendLine("条件项名称不能为空");
                return false;
            }
	  //条件项说明
	  if (tBaseEvaluationConInfo.CONDITION_REMARK.Trim() == "")
            {
                this.Tips.AppendLine("条件项说明不能为空");
                return false;
            }
	  //0为在使用、1为停用
      if (tBaseEvaluationConInfo.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("0为在使用、1为停用不能为空");
                return false;
            }
	  //备注1
	  if (tBaseEvaluationConInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseEvaluationConInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseEvaluationConInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseEvaluationConInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseEvaluationConInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新排序 Castle （胡方扬）2012-11-05
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool UpdateSortByTransaction(string strValue)
        {
            return access.UpdateSortByTransaction(strValue);
        }

    }
}
