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
    /// 功能：收文阅办
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSwHandleLogic : LogicBase
    {

	TOaSwHandleVo tOaSwHandle = new TOaSwHandleVo();
    TOaSwHandleAccess access;
		
	public TOaSwHandleLogic()
	{
		  access = new TOaSwHandleAccess();  
	}
		
	public TOaSwHandleLogic(TOaSwHandleVo _tOaSwHandle)
	{
		tOaSwHandle = _tOaSwHandle;
		access = new TOaSwHandleAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSwHandle">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaSwHandleVo tOaSwHandle)
        {
            return access.GetSelectResultCount(tOaSwHandle);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaSwHandleVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaSwHandle">对象条件</param>
        /// <returns>对象</returns>
        public TOaSwHandleVo Details(TOaSwHandleVo tOaSwHandle)
        {
            return access.Details(tOaSwHandle);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaSwHandle">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaSwHandleVo> SelectByObject(TOaSwHandleVo tOaSwHandle, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaSwHandle, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaSwHandle">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaSwHandleVo tOaSwHandle, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaSwHandle, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSwHandle"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSwHandleVo tOaSwHandle)
        {
            return access.SelectByTable(tOaSwHandle);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaSwHandle">对象</param>
        /// <returns></returns>
        public TOaSwHandleVo SelectByObject(TOaSwHandleVo tOaSwHandle)
        {
            return access.SelectByObject(tOaSwHandle);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaSwHandleVo tOaSwHandle)
        {
            return access.Create(tOaSwHandle);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwHandle">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwHandleVo tOaSwHandle)
        {
            return access.Edit(tOaSwHandle);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwHandle_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaSwHandle_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwHandleVo tOaSwHandle_UpdateSet, TOaSwHandleVo tOaSwHandle_UpdateWhere)
        {
            return access.Edit(tOaSwHandle_UpdateSet, tOaSwHandle_UpdateWhere);
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
        public bool Delete(TOaSwHandleVo tOaSwHandle)
        {
            return access.Delete(tOaSwHandle);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tOaSwHandle.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //收文ID
	  if (tOaSwHandle.SW_ID.Trim() == "")
            {
                this.Tips.AppendLine("收文ID不能为空");
                return false;
            }
	  //拟办人ID
	  if (tOaSwHandle.SW_PLAN_ID.Trim() == "")
            {
                this.Tips.AppendLine("拟办人ID不能为空");
                return false;
            }
	  //拟办日期
	  if (tOaSwHandle.SW_PLAN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("拟办日期不能为空");
                return false;
            }
	  //批办意见
	  if (tOaSwHandle.SW_PLAN_APP_INFO.Trim() == "")
            {
                this.Tips.AppendLine("批办意见不能为空");
                return false;
            }
	  //是否已办
	  if (tOaSwHandle.IS_OK.Trim() == "")
            {
                this.Tips.AppendLine("是否已办不能为空");
                return false;
            }
	  //备注1
	  if (tOaSwHandle.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaSwHandle.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaSwHandle.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaSwHandle.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaSwHandle.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }
      //
      if (tOaSwHandle.SW_HANDER.Trim() == "")
      {
          this.Tips.AppendLine("处理人标志不能为空");
          return false;
      }//
      if (tOaSwHandle.STR_USERID.Trim() == "")
      {
          this.Tips.AppendLine("发送人ID不能为空");
          return false;
      }
      if (tOaSwHandle.STR_DATE.Trim() == "")
      {
          this.Tips.AppendLine("发送时间不能为空");
          return false;
      }
            return true;
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSwHandle"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSwHandleVo tOaSwHandle, bool b)
        {
            return access.SelectByTable(tOaSwHandle, b);
        }

    }
}
