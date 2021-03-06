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
    /// 功能：委托书附加费用明细
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractAttfeeLogic : LogicBase
    {

	TMisContractAttfeeVo tMisContractAttfee = new TMisContractAttfeeVo();
    TMisContractAttfeeAccess access;
		
	public TMisContractAttfeeLogic()
	{
		  access = new TMisContractAttfeeAccess();  
	}
		
	public TMisContractAttfeeLogic(TMisContractAttfeeVo _tMisContractAttfee)
	{
		tMisContractAttfee = _tMisContractAttfee;
		access = new TMisContractAttfeeAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractAttfee">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractAttfeeVo tMisContractAttfee)
        {
            return access.GetSelectResultCount(tMisContractAttfee);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractAttfeeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractAttfee">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractAttfeeVo Details(TMisContractAttfeeVo tMisContractAttfee)
        {
            return access.Details(tMisContractAttfee);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractAttfee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractAttfeeVo> SelectByObject(TMisContractAttfeeVo tMisContractAttfee, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractAttfee, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractAttfee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractAttfeeVo tMisContractAttfee, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractAttfee, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractAttfee"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractAttfeeVo tMisContractAttfee)
        {
            return access.SelectByTable(tMisContractAttfee);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractAttfee">对象</param>
        /// <returns></returns>
        public TMisContractAttfeeVo SelectByObject(TMisContractAttfeeVo tMisContractAttfee)
        {
            return access.SelectByObject(tMisContractAttfee);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractAttfeeVo tMisContractAttfee)
        {
            return access.Create(tMisContractAttfee);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractAttfee">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractAttfeeVo tMisContractAttfee)
        {
            return access.Edit(tMisContractAttfee);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractAttfee_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractAttfee_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractAttfeeVo tMisContractAttfee_UpdateSet, TMisContractAttfeeVo tMisContractAttfee_UpdateWhere)
        {
            return access.Edit(tMisContractAttfee_UpdateSet, tMisContractAttfee_UpdateWhere);
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
        public bool Delete(TMisContractAttfeeVo tMisContractAttfee)
        {
            return access.Delete(tMisContractAttfee);
        }
        /// <summary>
        /// 删除委托书监测点位的附加项目 Create By Castle （胡方扬） 2012-12-04
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strMovePointItems"></param>
        /// <returns></returns>
        public bool DelMoveAttItems(TMisContractAttfeeVo tMisContractAttfee, string[] strMovePointItems)
        {
            return access.DelMoveAttItems(tMisContractAttfee, strMovePointItems);
        }
        /// <summary>
        /// 向增加新的附加项目 Create By Castle （胡方扬） 2012-12-04
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strAddPointItems"></param>
        /// <returns></returns>
        public bool EditAttItems(TMisContractAttfeeVo tMisContractAttfee, string[] strAddPointItems)
        {
            return access.EditAttItems(tMisContractAttfee, strAddPointItems);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisContractAttfee.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //委托书ID
	  if (tMisContractAttfee.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书ID不能为空");
                return false;
            }
	  //频次次数
	  if (tMisContractAttfee.ATT_FEE_ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("频次次数不能为空");
                return false;
            }
	  //实际附加费用
	  if (tMisContractAttfee.FEE.Trim() == "")
            {
                this.Tips.AppendLine("实际附加费用不能为空");
                return false;
            }
	  //备注1
	  if (tMisContractAttfee.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisContractAttfee.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisContractAttfee.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisContractAttfee.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisContractAttfee.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
