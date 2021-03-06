using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.SUPPLIER;
using i3.DataAccess.Channels.OA.SUPPLIER;

namespace i3.BusinessLogic.Channels.OA.SUPPLIER
{
    /// <summary>
    /// 功能：供应商产品评价
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSupplierJudgeLogic : LogicBase
    {

	TOaSupplierJudgeVo tOaSupplierJudge = new TOaSupplierJudgeVo();
    TOaSupplierJudgeAccess access;
		
	public TOaSupplierJudgeLogic()
	{
		  access = new TOaSupplierJudgeAccess();  
	}
		
	public TOaSupplierJudgeLogic(TOaSupplierJudgeVo _tOaSupplierJudge)
	{
		tOaSupplierJudge = _tOaSupplierJudge;
		access = new TOaSupplierJudgeAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSupplierJudge">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            return access.GetSelectResultCount(tOaSupplierJudge);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaSupplierJudgeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaSupplierJudge">对象条件</param>
        /// <returns>对象</returns>
        public TOaSupplierJudgeVo Details(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            return access.Details(tOaSupplierJudge);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaSupplierJudge">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaSupplierJudgeVo> SelectByObject(TOaSupplierJudgeVo tOaSupplierJudge, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaSupplierJudge, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaSupplierJudge">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaSupplierJudgeVo tOaSupplierJudge, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaSupplierJudge, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSupplierJudge"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            return access.SelectByTable(tOaSupplierJudge);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public DataTable SelectByTable_ByJoin(TOaSupplierJudgeVo tOaSupplierJudge, int iIndex, int iCount)
        {
            return access.SelectByTable_ByJoin(tOaSupplierJudge, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaSupplierJudge">对象</param>
        /// <returns></returns>
        public TOaSupplierJudgeVo SelectByObject(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            return access.SelectByObject(tOaSupplierJudge);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            return access.Create(tOaSupplierJudge);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSupplierJudge">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            return access.Edit(tOaSupplierJudge);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSupplierJudge_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaSupplierJudge_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSupplierJudgeVo tOaSupplierJudge_UpdateSet, TOaSupplierJudgeVo tOaSupplierJudge_UpdateWhere)
        {
            return access.Edit(tOaSupplierJudge_UpdateSet, tOaSupplierJudge_UpdateWhere);
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
        public bool Delete(TOaSupplierJudgeVo tOaSupplierJudge)
        {
            return access.Delete(tOaSupplierJudge);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaSupplierJudge.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //供应商ID
	  if (tOaSupplierJudge.SUPPLIER_ID.Trim() == "")
            {
                this.Tips.AppendLine("供应商ID不能为空");
                return false;
            }
	  //物料名称
	  if (tOaSupplierJudge.PARTNAME.Trim() == "")
            {
                this.Tips.AppendLine("物料名称不能为空");
                return false;
            }
	  //规格型号
	  if (tOaSupplierJudge.MODEL.Trim() == "")
            {
                this.Tips.AppendLine("规格型号不能为空");
                return false;
            }
	  //参考价
	  if (tOaSupplierJudge.REFERENCEPRICE.Trim() == "")
            {
                this.Tips.AppendLine("参考价不能为空");
                return false;
            }
	  //产品标准
	  if (tOaSupplierJudge.PRODUCTSTANDARD.Trim() == "")
            {
                this.Tips.AppendLine("产品标准不能为空");
                return false;
            }
	  //最短供货期
	  if (tOaSupplierJudge.DELIVERYPERIOD.Trim() == "")
            {
                this.Tips.AppendLine("最短供货期不能为空");
                return false;
            }
	  //供货数量
	  if (tOaSupplierJudge.QUANTITY.Trim() == "")
            {
                this.Tips.AppendLine("供货数量不能为空");
                return false;
            }
	  //供应商编码
	  if (tOaSupplierJudge.ENTERPRISECODE.Trim() == "")
            {
                this.Tips.AppendLine("供应商编码不能为空");
                return false;
            }
	  //质量保证体系
	  if (tOaSupplierJudge.QUATITYSYSTEM.Trim() == "")
            {
                this.Tips.AppendLine("质量保证体系不能为空");
                return false;
            }
	  //合同信守情况
	  if (tOaSupplierJudge.SINCERITY.Trim() == "")
            {
                this.Tips.AppendLine("合同信守情况不能为空");
                return false;
            }
	  //备注1
	  if (tOaSupplierJudge.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaSupplierJudge.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaSupplierJudge.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaSupplierJudge.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaSupplierJudge.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
