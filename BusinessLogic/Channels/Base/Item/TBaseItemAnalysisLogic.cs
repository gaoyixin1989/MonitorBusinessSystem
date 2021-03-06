using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Item;
using i3.DataAccess.Channels.Base.Item;

namespace i3.BusinessLogic.Channels.Base.Item
{
    /// <summary>
    /// 功能：监测项目分析方法管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemAnalysisLogic : LogicBase
    {

	TBaseItemAnalysisVo tBaseItemAnalysis = new TBaseItemAnalysisVo();
    TBaseItemAnalysisAccess access;
		
	public TBaseItemAnalysisLogic()
	{
		  access = new TBaseItemAnalysisAccess();  
	}
		
	public TBaseItemAnalysisLogic(TBaseItemAnalysisVo _tBaseItemAnalysis)
	{
		tBaseItemAnalysis = _tBaseItemAnalysis;
		access = new TBaseItemAnalysisAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            return access.GetSelectResultCount(tBaseItemAnalysis);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ByJoin(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            return access.GetSelectResultCount_ByJoin(tBaseItemAnalysis);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseItemAnalysisVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象条件</param>
        /// <returns>对象</returns>
        public TBaseItemAnalysisVo Details(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            return access.Details(tBaseItemAnalysis);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseItemAnalysisVo> SelectByObject(TBaseItemAnalysisVo tBaseItemAnalysis, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseItemAnalysis, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseItemAnalysisVo tBaseItemAnalysis, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseItemAnalysis, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ByJoin(TBaseItemAnalysisVo tBaseItemAnalysis, int iIndex, int iCount)
        {
            return access.SelectByTable_ByJoin(tBaseItemAnalysis, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable,不分页 by 熊卫华2012.11.04
        /// </summary>
        /// <param name="tBaseItemAnalysis">分析方法对象</param>
        /// <returns></returns>
        public DataTable SelectByTable_ByJoin(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            return access.SelectByTable_ByJoin(tBaseItemAnalysis);
        }
        public DataTable SelectByTable_ByJoin1(string ID)
        {
            return access.SelectByTable_ByJoin1(ID);
        }
        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseItemAnalysis"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            return access.SelectByTable(tBaseItemAnalysis);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns></returns>
        public TBaseItemAnalysisVo SelectByObject(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            return access.SelectByObject(tBaseItemAnalysis);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            return access.Create(tBaseItemAnalysis);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemAnalysis">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            return access.Edit(tBaseItemAnalysis);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemAnalysis_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseItemAnalysis_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemAnalysisVo tBaseItemAnalysis_UpdateSet, TBaseItemAnalysisVo tBaseItemAnalysis_UpdateWhere)
        {
            return access.Edit(tBaseItemAnalysis_UpdateSet, tBaseItemAnalysis_UpdateWhere);
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
        public bool Delete(TBaseItemAnalysisVo tBaseItemAnalysis)
        {
            return access.Delete(tBaseItemAnalysis);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseItemAnalysis.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测项目ID
	  if (tBaseItemAnalysis.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
	  //分析方法ID
	  if (tBaseItemAnalysis.ANALYSIS_METHOD_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
	  //监测仪器ID
	  if (tBaseItemAnalysis.INSTRUMENT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测仪器ID不能为空");
                return false;
            }
	  //单位
	  if (tBaseItemAnalysis.UNIT.Trim() == "")
            {
                this.Tips.AppendLine("单位不能为空");
                return false;
            }
	  //小数点精度
	  if (tBaseItemAnalysis.PRECISION.Trim() == "")
            {
                this.Tips.AppendLine("小数点精度不能为空");
                return false;
            }
	  //检测上限
	  if (tBaseItemAnalysis.UPPER_LIMIT.Trim() == "")
            {
                this.Tips.AppendLine("检测上限不能为空");
                return false;
            }
	  //检测下限
	  if (tBaseItemAnalysis.LOWER_LIMIT.Trim() == "")
            {
                this.Tips.AppendLine("检测下限不能为空");
                return false;
            }
	  //最低检出限
	  if (tBaseItemAnalysis.LOWER_CHECKOUT.Trim() == "")
            {
                this.Tips.AppendLine("最低检出限不能为空");
                return false;
            }
	  //0为不默认，1为默认
	  if (tBaseItemAnalysis.IS_DEFAULT.Trim() == "")
            {
                this.Tips.AppendLine("0为不默认，1为默认不能为空");
                return false;
            }
	  //0为在使用、1为停用
      if (tBaseItemAnalysis.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("0为在使用、1为停用不能为空");
                return false;
            }
	  //备注1
	  if (tBaseItemAnalysis.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseItemAnalysis.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseItemAnalysis.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseItemAnalysis.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseItemAnalysis.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
