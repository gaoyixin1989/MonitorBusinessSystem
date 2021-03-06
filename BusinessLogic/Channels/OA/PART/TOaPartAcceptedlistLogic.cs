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
    /// 功能：物料验收单申请单关联表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartAcceptedlistLogic : LogicBase
    {

	TOaPartAcceptedlistVo tOaPartAcceptedlist = new TOaPartAcceptedlistVo();
    TOaPartAcceptedlistAccess access;
		
	public TOaPartAcceptedlistLogic()
	{
		  access = new TOaPartAcceptedlistAccess();  
	}
		
	public TOaPartAcceptedlistLogic(TOaPartAcceptedlistVo _tOaPartAcceptedlist)
	{
		tOaPartAcceptedlist = _tOaPartAcceptedlist;
		access = new TOaPartAcceptedlistAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            return access.GetSelectResultCount(tOaPartAcceptedlist);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartAcceptedlistVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartAcceptedlistVo Details(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            return access.Details(tOaPartAcceptedlist);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartAcceptedlistVo> SelectByObject(TOaPartAcceptedlistVo tOaPartAcceptedlist, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaPartAcceptedlist, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartAcceptedlistVo tOaPartAcceptedlist, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaPartAcceptedlist, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartAcceptedlist"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            return access.SelectByTable(tOaPartAcceptedlist);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartAcceptedlist">对象</param>
        /// <returns></returns>
        public TOaPartAcceptedlistVo SelectByObject(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            return access.SelectByObject(tOaPartAcceptedlist);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            return access.Create(tOaPartAcceptedlist);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartAcceptedlist">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            return access.Edit(tOaPartAcceptedlist);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartAcceptedlist_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaPartAcceptedlist_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartAcceptedlistVo tOaPartAcceptedlist_UpdateSet, TOaPartAcceptedlistVo tOaPartAcceptedlist_UpdateWhere)
        {
            return access.Edit(tOaPartAcceptedlist_UpdateSet, tOaPartAcceptedlist_UpdateWhere);
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
        public bool Delete(TOaPartAcceptedlistVo tOaPartAcceptedlist)
        {
            return access.Delete(tOaPartAcceptedlist);
        }
        /// <summary>
        /// 返回符合条件的采购计划与验收清单
        /// </summary>
        /// <param name="tOaPartAcceptedlist"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectUnionByTable(TOaPartAcceptedlistVo tOaPartAcceptedlist,TOaPartInfoVo tOaPartInfor, int iIndex, int iCount) {
            return access.SelectUnionByTable(tOaPartAcceptedlist,tOaPartInfor, iIndex, iCount);
        }
        /// <summary>
        /// 返回符合条件的采购计划与验收清单总记录数
        /// </summary>
        /// <param name="tOaPartAcceptedlist"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectUnionByTableCount(TOaPartAcceptedlistVo tOaPartAcceptedlist, TOaPartInfoVo tOaPartInfor)
        {

            return access.GetSelectUnionByTableCount(tOaPartAcceptedlist, tOaPartInfor);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaPartAcceptedlist.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //验收清单ID
	  if (tOaPartAcceptedlist.REQUST_LST_ID.Trim() == "")
            {
                this.Tips.AppendLine("验收清单ID不能为空");
                return false;
            }
	  //采购申请清单ID
	  if (tOaPartAcceptedlist.ACCEPTED_ID.Trim() == "")
            {
                this.Tips.AppendLine("采购申请清单ID不能为空");
                return false;
            }
	  //备注1
	  if (tOaPartAcceptedlist.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaPartAcceptedlist.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaPartAcceptedlist.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaPartAcceptedlist.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaPartAcceptedlist.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
