using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.Message;
using i3.DataAccess.Channels.OA.Message;

namespace i3.BusinessLogic.Channels.OA.Message
{
    /// <summary>
    /// 功能：个人短消息接收设置表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageSetupLogic : LogicBase
    {

	TOaMessageSetupVo tOaMessageSetup = new TOaMessageSetupVo();
    TOaMessageSetupAccess access;
		
	public TOaMessageSetupLogic()
	{
		  access = new TOaMessageSetupAccess();  
	}
		
	public TOaMessageSetupLogic(TOaMessageSetupVo _tOaMessageSetup)
	{
		tOaMessageSetup = _tOaMessageSetup;
		access = new TOaMessageSetupAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageSetup">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageSetupVo tOaMessageSetup)
        {
            return access.GetSelectResultCount(tOaMessageSetup);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageSetupVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageSetup">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageSetupVo Details(TOaMessageSetupVo tOaMessageSetup)
        {
            return access.Details(tOaMessageSetup);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageSetup">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageSetupVo> SelectByObject(TOaMessageSetupVo tOaMessageSetup, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaMessageSetup, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageSetup">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageSetupVo tOaMessageSetup, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaMessageSetup, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageSetup"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageSetupVo tOaMessageSetup)
        {
            return access.SelectByTable(tOaMessageSetup);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageSetup">对象</param>
        /// <returns></returns>
        public TOaMessageSetupVo SelectByObject(TOaMessageSetupVo tOaMessageSetup)
        {
            return access.SelectByObject(tOaMessageSetup);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageSetupVo tOaMessageSetup)
        {
            return access.Create(tOaMessageSetup);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageSetup">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageSetupVo tOaMessageSetup)
        {
            return access.Edit(tOaMessageSetup);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageSetup_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageSetup_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageSetupVo tOaMessageSetup_UpdateSet, TOaMessageSetupVo tOaMessageSetup_UpdateWhere)
        {
            return access.Edit(tOaMessageSetup_UpdateSet, tOaMessageSetup_UpdateWhere);
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
        public bool Delete(TOaMessageSetupVo tOaMessageSetup)
        {
            return access.Delete(tOaMessageSetup);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tOaMessageSetup.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //
	  if (tOaMessageSetup.IF_RE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
	  //提醒方式，1弹出窗口，2短信，3图标闪烁（3可以暂时不实现）
	  if (tOaMessageSetup.UDS_REMINDTYPE.Trim() == "")
            {
                this.Tips.AppendLine("提醒方式，1弹出窗口，2短信，3图标闪烁（3可以暂时不实现）不能为空");
                return false;
            }
	  //提醒时间，即刷新间隔
	  if (tOaMessageSetup.UDS_REFRESHTIME.Trim() == "")
            {
                this.Tips.AppendLine("提醒时间，即刷新间隔不能为空");
                return false;
            }
	  //用户ID
	  if (tOaMessageSetup.USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("用户ID不能为空");
                return false;
            }
	  //备份1
	  if (tOaMessageSetup.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备份1不能为空");
                return false;
            }
	  //备份2
	  if (tOaMessageSetup.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备份2不能为空");
                return false;
            }
	  //备份3
	  if (tOaMessageSetup.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备份3不能为空");
                return false;
            }
	  //备份4
	  if (tOaMessageSetup.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备份4不能为空");
                return false;
            }
	  //备份5
	  if (tOaMessageSetup.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备份5不能为空");
                return false;
            }

            return true;
        }

    }
}
