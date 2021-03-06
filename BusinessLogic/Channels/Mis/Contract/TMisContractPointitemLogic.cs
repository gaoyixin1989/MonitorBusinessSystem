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
    /// 功能：委托书监测点项目明细
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPointitemLogic : LogicBase
    {

	TMisContractPointitemVo tMisContractPointitem = new TMisContractPointitemVo();
    TMisContractPointitemAccess access;
		
	public TMisContractPointitemLogic()
	{
		  access = new TMisContractPointitemAccess();  
	}
		
	public TMisContractPointitemLogic(TMisContractPointitemVo _tMisContractPointitem)
	{
		tMisContractPointitem = _tMisContractPointitem;
		access = new TMisContractPointitemAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPointitem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPointitemVo tMisContractPointitem)
        {
            return access.GetSelectResultCount(tMisContractPointitem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPointitemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPointitem">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPointitemVo Details(TMisContractPointitemVo tMisContractPointitem)
        {
            return access.Details(tMisContractPointitem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPointitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPointitemVo> SelectByObject(TMisContractPointitemVo tMisContractPointitem, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractPointitem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPointitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPointitemVo tMisContractPointitem, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractPointitem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPointitemVo tMisContractPointitem)
        {
            return access.SelectByTable(tMisContractPointitem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPointitem">对象</param>
        /// <returns></returns>
        public TMisContractPointitemVo SelectByObject(TMisContractPointitemVo tMisContractPointitem)
        {
            return access.SelectByObject(tMisContractPointitem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPointitemVo tMisContractPointitem)
        {
            return access.Create(tMisContractPointitem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPointitem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointitemVo tMisContractPointitem)
        {
            return access.Edit(tMisContractPointitem);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPointitem_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractPointitem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointitemVo tMisContractPointitem_UpdateSet, TMisContractPointitemVo tMisContractPointitem_UpdateWhere)
        {
            return access.Edit(tMisContractPointitem_UpdateSet, tMisContractPointitem_UpdateWhere);
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
        public bool Delete(TMisContractPointitemVo tMisContractPointitem)
        {
            return access.Delete(tMisContractPointitem);
        }
	
                /// <summary>
        /// 删除委托书监测点位的监测项目 Create By Castle （胡方扬） 2012-12-04
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strMovePointItems"></param>
        /// <returns></returns>
        public bool DelMoveItems(TMisContractPointitemVo tMisContractPointitem, string[] strMovePointItems)
        {
            return access.DelMoveItems(tMisContractPointitem, strMovePointItems);
        }
                /// <summary>
        /// 向委托书监测项目中插入保存了且不存在的数据  Create By Castle （胡方扬） 2012-12-04
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strAddPointItems"></param>
        /// <returns></returns>
        public bool EditItems(TMisContractPointitemVo tMisContractPointitem, string[] strAddPointItems)
        {
            return access.EditItems(tMisContractPointitem, strAddPointItems);
        }
        /// <summary>
        /// 获取点位排口下所有的监测项目信息
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public DataTable GetItemsForPoint(TMisContractPointitemVo tMisContractPointitem) {
            return access.GetItemsForPoint(tMisContractPointitem);
        }

         /// <summary>
        /// 插入环境质量点位及其相关点位项目
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public bool InsertEnvPointItems(TMisContractPointitemVo tMisContractPointitem)
        {
            return access.InsertEnvPointItems(tMisContractPointitem);
        }
        /// <summary>
        /// 插入环境质量点位及其相关点位项目 Create By:weilin 2014-11-16
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public bool InsertEnvPointItemsEx(TMisContractPointitemVo tMisContractPointitem)
        {
            return access.InsertEnvPointItemsEx(tMisContractPointitem);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisContractPointitem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //合同监测点ID
	  if (tMisContractPointitem.CONTRACT_POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("合同监测点ID不能为空");
                return false;
            }
	  //监测项目ID
	  if (tMisContractPointitem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
	  //已选条件项ID
	  if (tMisContractPointitem.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("已选条件项ID不能为空");
                return false;
            }
	  //条件项类型（1，国标；2，行标；3，地标）
	  if (tMisContractPointitem.CONDITION_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("条件项类型（1，国标；2，行标；3，地标）不能为空");
                return false;
            }
	  //国标上限
	  if (tMisContractPointitem.ST_UPPER.Trim() == "")
            {
                this.Tips.AppendLine("国标上限不能为空");
                return false;
            }
	  //国标下限
	  if (tMisContractPointitem.ST_LOWER.Trim() == "")
            {
                this.Tips.AppendLine("国标下限不能为空");
                return false;
            }
	  //备注1
	  if (tMisContractPointitem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisContractPointitem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisContractPointitem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisContractPointitem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisContractPointitem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
