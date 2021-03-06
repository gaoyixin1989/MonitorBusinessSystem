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
    /// 功能：委托书附加费用单价
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractAttfeeitemLogic : LogicBase
    {

	TMisContractAttfeeitemVo tMisContractAttfeeitem = new TMisContractAttfeeitemVo();
    TMisContractAttfeeitemAccess access;
		
	public TMisContractAttfeeitemLogic()
	{
		  access = new TMisContractAttfeeitemAccess();  
	}
		
	public TMisContractAttfeeitemLogic(TMisContractAttfeeitemVo _tMisContractAttfeeitem)
	{
		tMisContractAttfeeitem = _tMisContractAttfeeitem;
		access = new TMisContractAttfeeitemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            return access.GetSelectResultCount(tMisContractAttfeeitem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractAttfeeitemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractAttfeeitemVo Details(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            return access.Details(tMisContractAttfeeitem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractAttfeeitemVo> SelectByObject(TMisContractAttfeeitemVo tMisContractAttfeeitem, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractAttfeeitem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractAttfeeitemVo tMisContractAttfeeitem, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractAttfeeitem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractAttfeeitem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            return access.SelectByTable(tMisContractAttfeeitem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractAttfeeitem">对象</param>
        /// <returns></returns>
        public TMisContractAttfeeitemVo SelectByObject(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            return access.SelectByObject(tMisContractAttfeeitem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            return access.Create(tMisContractAttfeeitem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractAttfeeitem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            return access.Edit(tMisContractAttfeeitem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractAttfeeitem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractAttfeeitem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractAttfeeitemVo tMisContractAttfeeitem_UpdateSet, TMisContractAttfeeitemVo tMisContractAttfeeitem_UpdateWhere)
        {
            return access.Edit(tMisContractAttfeeitem_UpdateSet, tMisContractAttfeeitem_UpdateWhere);
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
        public bool Delete(TMisContractAttfeeitemVo tMisContractAttfeeitem)
        {
            return access.Delete(tMisContractAttfeeitem);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //
	  if (tMisContractAttfeeitem.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
	  //附加项目
	  if (tMisContractAttfeeitem.ATT_FEE_ITEM.Trim() == "")
            {
                this.Tips.AppendLine("附加项目不能为空");
                return false;
            }
	  //费用单价
	  if (tMisContractAttfeeitem.PRICE.Trim() == "")
            {
                this.Tips.AppendLine("费用单价不能为空");
                return false;
            }
	  //费用描述
	  if (tMisContractAttfeeitem.INFO.Trim() == "")
            {
                this.Tips.AppendLine("费用描述不能为空");
                return false;
            }
	  //备注1
	  if (tMisContractAttfeeitem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisContractAttfeeitem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisContractAttfeeitem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisContractAttfeeitem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisContractAttfeeitem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
