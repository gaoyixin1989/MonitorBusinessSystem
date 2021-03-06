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
    /// 功能：消息接收人
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoManLogic : LogicBase
    {

	TOaMessageInfoManVo tOaMessageInfoMan = new TOaMessageInfoManVo();
    TOaMessageInfoManAccess access;
		
	public TOaMessageInfoManLogic()
	{
		  access = new TOaMessageInfoManAccess();  
	}
		
	public TOaMessageInfoManLogic(TOaMessageInfoManVo _tOaMessageInfoMan)
	{
		tOaMessageInfoMan = _tOaMessageInfoMan;
		access = new TOaMessageInfoManAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            return access.GetSelectResultCount(tOaMessageInfoMan);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageInfoManVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageInfoManVo Details(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            return access.Details(tOaMessageInfoMan);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageInfoManVo> SelectByObject(TOaMessageInfoManVo tOaMessageInfoMan, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaMessageInfoMan, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageInfoManVo tOaMessageInfoMan, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaMessageInfoMan, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageInfoMan"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            return access.SelectByTable(tOaMessageInfoMan);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageInfoMan">对象</param>
        /// <returns></returns>
        public TOaMessageInfoManVo SelectByObject(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            return access.SelectByObject(tOaMessageInfoMan);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            return access.Create(tOaMessageInfoMan);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoMan">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            return access.Edit(tOaMessageInfoMan);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoMan_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageInfoMan_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoManVo tOaMessageInfoMan_UpdateSet, TOaMessageInfoManVo tOaMessageInfoMan_UpdateWhere)
        {
            return access.Edit(tOaMessageInfoMan_UpdateSet, tOaMessageInfoMan_UpdateWhere);
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
        public bool Delete(TOaMessageInfoManVo tOaMessageInfoMan)
        {
            return access.Delete(tOaMessageInfoMan);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tOaMessageInfoMan.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //消息编号
	  if (tOaMessageInfoMan.MESSAGE_ID.Trim() == "")
            {
                this.Tips.AppendLine("消息编号不能为空");
                return false;
            }
	  //消息接收人
	  if (tOaMessageInfoMan.RECEIVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("消息接收人不能为空");
                return false;
            }
	  //备注1
	  if (tOaMessageInfoMan.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaMessageInfoMan.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaMessageInfoMan.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaMessageInfoMan.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaMessageInfoMan.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
