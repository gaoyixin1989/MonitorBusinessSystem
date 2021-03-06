using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.PART;
using i3.DataAccess.Channels.OA.PART;

namespace i3.BusinessLogic.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料采购申请清单
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartBuyRequstLstLogic : LogicBase
    {

	TOaPartBuyRequstLstVo tOaPartBuyRequstLst = new TOaPartBuyRequstLstVo();
    TOaPartBuyRequstLstAccess access;
		
	public TOaPartBuyRequstLstLogic()
	{
		  access = new TOaPartBuyRequstLstAccess();  
	}
		
	public TOaPartBuyRequstLstLogic(TOaPartBuyRequstLstVo _tOaPartBuyRequstLst)
	{
		tOaPartBuyRequstLst = _tOaPartBuyRequstLst;
		access = new TOaPartBuyRequstLstAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            return access.GetSelectResultCount(tOaPartBuyRequstLst);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartBuyRequstLstVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartBuyRequstLstVo Details(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            return access.Details(tOaPartBuyRequstLst);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartBuyRequstLstVo> SelectByObject(TOaPartBuyRequstLstVo tOaPartBuyRequstLst, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaPartBuyRequstLst, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartBuyRequstLstVo tOaPartBuyRequstLst, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaPartBuyRequstLst, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartBuyRequstLst"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            return access.SelectByTable(tOaPartBuyRequstLst);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">对象</param>
        /// <returns></returns>
        public TOaPartBuyRequstLstVo SelectByObject(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            return access.SelectByObject(tOaPartBuyRequstLst);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            return access.Create(tOaPartBuyRequstLst);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartBuyRequstLst">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            return access.Edit(tOaPartBuyRequstLst);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartBuyRequstLst_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaPartBuyRequstLst_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartBuyRequstLstVo tOaPartBuyRequstLst_UpdateSet, TOaPartBuyRequstLstVo tOaPartBuyRequstLst_UpdateWhere)
        {
            return access.Edit(tOaPartBuyRequstLst_UpdateSet, tOaPartBuyRequstLst_UpdateWhere);
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

        public DataTable SelectRemarks(string ID)
        {
            return access.SelectRemarks(ID);
        }

        public DataTable SelectREQUSTID(string ID)
        {
            return access.SelectREQUSTID(ID);
        }

        public DataTable GetInfo(string ID)
        {
            return access.GetInfo(ID);
        }

	/// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaPartBuyRequstLstVo tOaPartBuyRequstLst)
        {
            return access.Delete(tOaPartBuyRequstLst);
        }

        public DataTable SelectUnionPartByTable(TOaPartBuyRequstLstVo tOaPartBuyRequstLst, TOaPartInfoVo tOaPartInfor, TOaPartBuyRequstVo tOaPartBuyRequest, int iIndex, int iCount)
        {
            return access.SelectUnionPartByTable(tOaPartBuyRequstLst, tOaPartInfor, tOaPartBuyRequest,iIndex, iCount);
        }
        public int GetSelectUnionPartByTableResult(TOaPartBuyRequstLstVo tOaPartBuyRequstLst, TOaPartInfoVo tOaPartInfor,TOaPartBuyRequstVo tOaPartBuyRequest)
        {
            return access.GetSelectUnionPartByTableResult(tOaPartBuyRequstLst, tOaPartInfor, tOaPartBuyRequest);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaPartBuyRequstLst.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //申请单ID
	  if (tOaPartBuyRequstLst.REQUST_ID.Trim() == "")
            {
                this.Tips.AppendLine("申请单ID不能为空");
                return false;
            }
	  //物料ID
	  if (tOaPartBuyRequstLst.PART_ID.Trim() == "")
            {
                this.Tips.AppendLine("物料ID不能为空");
                return false;
            }
	  //需求数量
	  if (tOaPartBuyRequstLst.NEED_QUANTITY.Trim() == "")
            {
                this.Tips.AppendLine("需求数量不能为空");
                return false;
            }
	  //采购用途
	  if (tOaPartBuyRequstLst.USERDO.Trim() == "")
            {
                this.Tips.AppendLine("采购用途不能为空");
                return false;
            }
	  //要求交货期限
	  if (tOaPartBuyRequstLst.DELIVERY_DATE.Trim() == "")
            {
                this.Tips.AppendLine("要求交货期限不能为空");
                return false;
            }
	  //计划资金
	  if (tOaPartBuyRequstLst.BUDGET_MONEY.Trim() == "")
            {
                this.Tips.AppendLine("计划资金不能为空");
                return false;
            }
	  //状态,1待审批，2待采购，3已采购
	  if (tOaPartBuyRequstLst.STATUS.Trim() == "")
            {
                this.Tips.AppendLine("状态,1待审批，2待采购，3已采购不能为空");
                return false;
            }
	  //备注1
	  if (tOaPartBuyRequstLst.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaPartBuyRequstLst.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaPartBuyRequstLst.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaPartBuyRequstLst.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaPartBuyRequstLst.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
