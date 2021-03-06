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
    /// 功能：委托书监测费用明细
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractTestfeeLogic : LogicBase
    {

	TMisContractTestfeeVo tMisContractTestfee = new TMisContractTestfeeVo();
    TMisContractTestfeeAccess access;
		
	public TMisContractTestfeeLogic()
	{
		  access = new TMisContractTestfeeAccess();  
	}
		
	public TMisContractTestfeeLogic(TMisContractTestfeeVo _tMisContractTestfee)
	{
		tMisContractTestfee = _tMisContractTestfee;
		access = new TMisContractTestfeeAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractTestfee">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractTestfeeVo tMisContractTestfee)
        {
            return access.GetSelectResultCount(tMisContractTestfee);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractTestfeeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractTestfee">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractTestfeeVo Details(TMisContractTestfeeVo tMisContractTestfee)
        {
            return access.Details(tMisContractTestfee);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractTestfee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractTestfeeVo> SelectByObject(TMisContractTestfeeVo tMisContractTestfee, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractTestfee, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractTestfee">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractTestfeeVo tMisContractTestfee, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractTestfee, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractTestfee"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractTestfeeVo tMisContractTestfee)
        {
            return access.SelectByTable(tMisContractTestfee);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractTestfee">对象</param>
        /// <returns></returns>
        public TMisContractTestfeeVo SelectByObject(TMisContractTestfeeVo tMisContractTestfee)
        {
            return access.SelectByObject(tMisContractTestfee);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractTestfeeVo tMisContractTestfee)
        {
            return access.Create(tMisContractTestfee);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractTestfee">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractTestfeeVo tMisContractTestfee)
        {
            return access.Edit(tMisContractTestfee);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractTestfee_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractTestfee_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractTestfeeVo tMisContractTestfee_UpdateSet, TMisContractTestfeeVo tMisContractTestfee_UpdateWhere)
        {
            return access.Edit(tMisContractTestfee_UpdateSet, tMisContractTestfee_UpdateWhere);
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
        public bool Delete(TMisContractTestfeeVo tMisContractTestfee)
        {
            return access.Delete(tMisContractTestfee);
        }
	
                /// <summary>
        /// 获取指定委托监测费用明细 胡方扬 2013-04-11
        /// </summary>
        /// <param name="tMisContractTestfee"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable GetContractConstFeeDetail(TMisContractTestfeeVo tMisContractTestfee, int iIndex, int iCount)
        {
            return access.GetContractConstFeeDetail(tMisContractTestfee, iIndex, iCount);
        }

        /// <summary>
        /// 获取指定委托监测费用明细总记录 胡方扬 2013-04-11
        /// </summary>
        /// <param name="tMisContractTestfee"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetContractConstFeeDetailCount(TMisContractTestfeeVo tMisContractTestfee)
        {
            return access.GetContractConstFeeDetailCount(tMisContractTestfee);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisContractTestfee.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //委托书ID
	  if (tMisContractTestfee.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书ID不能为空");
                return false;
            }
	  //频次次数，合同分几次执行
      if (tMisContractTestfee.PLAN_ID.Trim() == "")
            {
                this.Tips.AppendLine("频次次数，合同分几次执行不能为空");
                return false;
            }
	  //监测项目ID
      if (tMisContractTestfee.CONTRACT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
	  //样品数，实际就是点位数
      if (tMisContractTestfee.TICKET_NUM.Trim() == "")
            {
                this.Tips.AppendLine("样品数，实际就是点位数不能为空");
                return false;
            }
	  //分析单价
      if (tMisContractTestfee.CONTRACT_YEAR.Trim() == "")
            {
                this.Tips.AppendLine("分析单价不能为空");
                return false;
            }
	  //分析费用，分析单价×频次×样品数
      if (tMisContractTestfee.PROJECT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("分析费用，分析单价×频次×样品数不能为空");
                return false;
            }
	  //开机费用单价
      if (tMisContractTestfee.CONTRACT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("开机费用单价不能为空");
                return false;
            }
	  //开机总费用，开机费用单价×频次
      if (tMisContractTestfee.TEST_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("开机总费用，开机费用单价×频次不能为空");
                return false;
            }
	  //小计，分析总费用+开机总费用
      if (tMisContractTestfee.TEST_PURPOSE.Trim() == "")
            {
                this.Tips.AppendLine("小计，分析总费用+开机总费用不能为空");
                return false;
            }
	  //备注1
	  if (tMisContractTestfee.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisContractTestfee.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisContractTestfee.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisContractTestfee.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisContractTestfee.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
