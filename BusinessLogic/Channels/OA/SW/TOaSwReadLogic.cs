using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.SW;
using i3.DataAccess.Channels.OA.SW;

namespace i3.BusinessLogic.Channels.OA.SW
{
    /// <summary>
    /// 功能：收文传阅
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSwReadLogic : LogicBase
    {

	TOaSwReadVo tOaSwRead = new TOaSwReadVo();
    TOaSwReadAccess access;
		
	public TOaSwReadLogic()
	{
		  access = new TOaSwReadAccess();  
	}
		
	public TOaSwReadLogic(TOaSwReadVo _tOaSwRead)
	{
		tOaSwRead = _tOaSwRead;
		access = new TOaSwReadAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSwRead">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaSwReadVo tOaSwRead)
        {
            return access.GetSelectResultCount(tOaSwRead);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaSwReadVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="swid">收文ID</param>
        /// <returns>返回结果</returns>
        public List<TOaSwReadVo> SelectByReadUser(string swid)
        {
            return access.SelectByReadUser(swid);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaSwRead">对象条件</param>
        /// <returns>对象</returns>
        public TOaSwReadVo Details(TOaSwReadVo tOaSwRead)
        {
            return access.Details(tOaSwRead);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaSwRead">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaSwReadVo> SelectByObject(TOaSwReadVo tOaSwRead, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaSwRead, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaSwRead">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaSwReadVo tOaSwRead, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaSwRead, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSwRead"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSwReadVo tOaSwRead)
        {
            return access.SelectByTable(tOaSwRead);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaSwRead">对象</param>
        /// <returns></returns>
        public TOaSwReadVo SelectByObject(TOaSwReadVo tOaSwRead)
        {
            return access.SelectByObject(tOaSwRead);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaSwReadVo tOaSwRead)
        {
            return access.Create(tOaSwRead);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwRead">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwReadVo tOaSwRead)
        {
            return access.Edit(tOaSwRead);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwRead_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaSwRead_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwReadVo tOaSwRead_UpdateSet, TOaSwReadVo tOaSwRead_UpdateWhere)
        {
            return access.Edit(tOaSwRead_UpdateSet, tOaSwRead_UpdateWhere);
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
        public bool Delete(TOaSwReadVo tOaSwRead)
        {
            return access.Delete(tOaSwRead);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tOaSwRead.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //收文ID
	  if (tOaSwRead.SW_ID.Trim() == "")
            {
                this.Tips.AppendLine("收文ID不能为空");
                return false;
            }
	  //传阅人ID
	  if (tOaSwRead.SW_PLAN_ID.Trim() == "")
            {
                this.Tips.AppendLine("传阅人ID不能为空");
                return false;
            }
	  //传阅日期
	  if (tOaSwRead.SW_PLAN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("传阅日期不能为空");
                return false;
            }
	  //是否已阅
	  if (tOaSwRead.IS_OK.Trim() == "")
            {
                this.Tips.AppendLine("是否已阅不能为空");
                return false;
            }
	  //备注1
	  if (tOaSwRead.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaSwRead.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaSwRead.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaSwRead.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaSwRead.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
