using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Contract;
using i3.DataAccess.Channels.Mis.Contract;

namespace i3.BusinessLogic.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书缴费表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractFeeLogic : LogicBase
    {

	TMisContractFeeVo tMisContractFee = new TMisContractFeeVo();
    TMisContractFeeAccess access;
		
	public TMisContractFeeLogic()
	{
		  access = new TMisContractFeeAccess();  
	}
		
	public TMisContractFeeLogic(TMisContractFeeVo _tMisContractFee)
	{
		tMisContractFee = _tMisContractFee;
		access = new TMisContractFeeAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractFeeVo tMisContractFee)
        {
            return access.GetSelectResultCount(tMisContractFee);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// 何海亮修改
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountByHHL(TMisContractFeeVo tMisContractFee)
        {
            return access.GetSelectResultCountByHHL(tMisContractFee);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractFeeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractFee">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractFeeVo Details(TMisContractFeeVo tMisContractFee)
        {
            return access.Details(tMisContractFee);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractFeeVo> SelectByObject(TMisContractFeeVo tMisContractFee, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractFee, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractFeeVo tMisContractFee, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractFee, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable
        /// 何海亮修改
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableCreateByHHL(TMisContractFeeVo tMisContractFee, int iIndex, int iCount)
        {
            return access.SelectByTableCreateByHHL(tMisContractFee, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractFee"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractFeeVo tMisContractFee)
        {
            return access.SelectByTable(tMisContractFee);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractFee">对象</param>
        /// <returns></returns>
        public TMisContractFeeVo SelectByObject(TMisContractFeeVo tMisContractFee)
        {
            return access.SelectByObject(tMisContractFee);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractFeeVo tMisContractFee)
        {
            return access.Create(tMisContractFee);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractFee">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractFeeVo tMisContractFee)
        {
            return access.Edit(tMisContractFee);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractFee_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractFee_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractFeeVo tMisContractFee_UpdateSet, TMisContractFeeVo tMisContractFee_UpdateWhere)
        {
            return access.Edit(tMisContractFee_UpdateSet, tMisContractFee_UpdateWhere);
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
        public bool Delete(TMisContractFeeVo tMisContractFee)
        {
            return access.Delete(tMisContractFee);
        }


        /// <summary>
        /// 获取统计缴费信息
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="tMisContractFee"></param>
        /// <returns></returns>
        public DataTable SelectTableForContractFree(TMisContractVo tMisContract, TMisContractFeeVo tMisContractFee,bool flag)
        {
            return access.SelectTableForContractFree(tMisContract,tMisContractFee,flag);
        }

        /// <summary>
        /// 获取统计缴费信息记录数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="tMisContractFee"></param>
        /// <returns></returns>
        public DataTable SelectTableForContractFreeCount(TMisContractVo tMisContract, TMisContractFeeVo tMisContractFee)
        {
            return access.SelectTableForContractFreeCount(tMisContract, tMisContractFee);
        }

        /// <summary>
        /// 获取统计缴费金额信息记录数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="tMisContractFee"></param>
        /// <returns></returns>
        public DataTable SelectTableForContractFreeSum(TMisContractVo tMisContract, TMisContractFeeVo tMisContractFee)
        {
            return access.SelectTableForContractFreeSum(tMisContract, tMisContractFee);
        }

        /// <summary>
        /// 获取统计缴费金额信息记录数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <param name="tMisContractFee"></param>
        /// <returns></returns>
        public DataTable SelectGetCompayFreeDetailTable(TMisContractVo tMisContract,int iIndex,int iCount)
        {
            return access.SelectGetCompayFreeDetailTable(tMisContract,iIndex,iCount);
        }

                /// <summary>
        /// 返回企业缴费信息明细记录数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public int SelectGetCompayFreeDetailTableCount(TMisContractVo tMisContract)
        {
            return access.SelectGetCompayFreeDetailTableCount(tMisContract);
        }
                /// <summary>
        /// 获取企业收费记录的明细记录列表
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable SelectGetCompanyDetailListInfor(TMisContractVo tMisContract, int iIndex, int iCount) {
            return access.SelectGetCompanyDetailListInfor(tMisContract,iIndex,iCount);
        }

         /// <summary>
        /// 获取企业收费记录的明细记录总数
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public int SelectGetCompanyDetailListInforCount(TMisContractVo tMisContract)
        {
            return access.SelectGetCompanyDetailListInforCount(tMisContract);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisContractFee.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //委托书ID
	  if (tMisContractFee.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书ID不能为空");
                return false;
            }
	  //监测费用，监测费用明细总和
	  if (tMisContractFee.TEST_FEE.Trim() == "")
            {
                this.Tips.AppendLine("监测费用，监测费用明细总和不能为空");
                return false;
            }
	  //附加总费用，附加费用明细总和
	  if (tMisContractFee.ATT_FEE.Trim() == "")
            {
                this.Tips.AppendLine("附加总费用，附加费用明细总和不能为空");
                return false;
            }
	  //预算总费用，监测总费用+附加总费用
	  if (tMisContractFee.BUDGET.Trim() == "")
            {
                this.Tips.AppendLine("预算总费用，监测总费用+附加总费用不能为空");
                return false;
            }
	  //实际收费
	  if (tMisContractFee.INCOME.Trim() == "")
            {
                this.Tips.AppendLine("实际收费不能为空");
                return false;
            }
	  //是否缴费
	  if (tMisContractFee.IF_PAY.Trim() == "")
            {
                this.Tips.AppendLine("是否缴费不能为空");
                return false;
            }
	  //备注1
	  if (tMisContractFee.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisContractFee.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisContractFee.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisContractFee.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisContractFee.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
