using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Method;
using i3.DataAccess.Channels.Base.Method;

namespace i3.BusinessLogic.Channels.Base.Method
{
    /// <summary>
    /// 功能：分析方法管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseMethodAnalysisLogic : LogicBase
    {

	TBaseMethodAnalysisVo tBaseMethodAnalysis = new TBaseMethodAnalysisVo();
    TBaseMethodAnalysisAccess access;
		
	public TBaseMethodAnalysisLogic()
	{
		  access = new TBaseMethodAnalysisAccess();  
	}
		
	public TBaseMethodAnalysisLogic(TBaseMethodAnalysisVo _tBaseMethodAnalysis)
	{
		tBaseMethodAnalysis = _tBaseMethodAnalysis;
		access = new TBaseMethodAnalysisAccess();
	}


    /// <summary>
    /// 获取对象DataTable
    /// </summary>
    /// <param name="strMethodName">方法依据名</param>
    /// <param name="iIndex">起始页码</param>
    /// <param name="iCount">每页数目</param>
    /// <returns>返回结果</returns>
    public DataTable SelectByTable_ForSelectMethod_inItem(string strMethodName, string strItemId, int iIndex, int iCount)
    {

        return access.SelectByTable_ForSelectMethod_inItem(strMethodName, strItemId, iIndex, iCount);
    }

    /// <summary>
    /// 获得查询结果总行数，用于分页
    /// </summary>
    /// <param name="tBaseMethodAnalysis">对象</param>
    /// <returns>返回行数</returns>
    public int GetSelectResultCount_ForSelectMethod_inItem(string strMethodName, string strItemId)
    {
        return access.GetSelectResultCount_ForSelectMethod_inItem(strMethodName, strItemId);
    }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            return access.GetSelectResultCount(tBaseMethodAnalysis);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseMethodAnalysisVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象条件</param>
        /// <returns>对象</returns>
        public TBaseMethodAnalysisVo Details(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            return access.Details(tBaseMethodAnalysis);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseMethodAnalysisVo> SelectByObject(TBaseMethodAnalysisVo tBaseMethodAnalysis, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseMethodAnalysis, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseMethodAnalysisVo tBaseMethodAnalysis, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseMethodAnalysis, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseMethodAnalysis"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            return access.SelectByTable(tBaseMethodAnalysis);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseMethodAnalysis">对象</param>
        /// <returns></returns>
        public TBaseMethodAnalysisVo SelectByObject(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            return access.SelectByObject(tBaseMethodAnalysis);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            return access.Create(tBaseMethodAnalysis);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMethodAnalysis">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            return access.Edit(tBaseMethodAnalysis);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMethodAnalysis_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseMethodAnalysis_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMethodAnalysisVo tBaseMethodAnalysis_UpdateSet, TBaseMethodAnalysisVo tBaseMethodAnalysis_UpdateWhere)
        {
            return access.Edit(tBaseMethodAnalysis_UpdateSet, tBaseMethodAnalysis_UpdateWhere);
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
        public bool Delete(TBaseMethodAnalysisVo tBaseMethodAnalysis)
        {
            return access.Delete(tBaseMethodAnalysis);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseMethodAnalysis.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //分析方法名称
	  if (tBaseMethodAnalysis.ANALYSIS_NAME.Trim() == "")
            {
                this.Tips.AppendLine("分析方法名称不能为空");
                return false;
            }
	  //分析方法描述
	  if (tBaseMethodAnalysis.DESCRIPTION.Trim() == "")
            {
                this.Tips.AppendLine("分析方法描述不能为空");
                return false;
            }
	  //方法依据ID
	  if (tBaseMethodAnalysis.METHOD_ID.Trim() == "")
            {
                this.Tips.AppendLine("方法依据ID不能为空");
                return false;
            }
	  //0为在使用、1为停用
      if (tBaseMethodAnalysis.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("0为在使用、1为停用不能为空");
                return false;
            }
	  //备注1
	  if (tBaseMethodAnalysis.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseMethodAnalysis.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseMethodAnalysis.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseMethodAnalysis.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseMethodAnalysis.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
