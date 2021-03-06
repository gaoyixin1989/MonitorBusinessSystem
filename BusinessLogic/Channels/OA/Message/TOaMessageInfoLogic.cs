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
    /// 功能：短消息信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoLogic : LogicBase
    {

	TOaMessageInfoVo tOaMessageInfo = new TOaMessageInfoVo();
    TOaMessageInfoAccess access;
		
	public TOaMessageInfoLogic()
	{
		  access = new TOaMessageInfoAccess();  
	}
		
	public TOaMessageInfoLogic(TOaMessageInfoVo _tOaMessageInfo)
	{
		tOaMessageInfo = _tOaMessageInfo;
		access = new TOaMessageInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageInfoVo tOaMessageInfo)
        {
            return access.GetSelectResultCount(tOaMessageInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageInfoVo Details(TOaMessageInfoVo tOaMessageInfo)
        {
            return access.Details(tOaMessageInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageInfoVo> SelectByObject(TOaMessageInfoVo tOaMessageInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaMessageInfo, iIndex, iCount);

        }

        /// <summary>
        /// 通过用户ID\部门ID获取对象DataTable
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strDeptCode">部门ID</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageInfoVo tOaMessageInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaMessageInfo, iIndex, iCount);
        }

        /// <summary>
        /// 通过用户ID\部门ID获取对象DataTable
        /// </summary>
        /// <param name="strUserID">用户ID</param>
        /// <param name="strDeptCode">部门ID</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public int GetSelectByUserIdAndDeptCount(string strUserID, string strDeptCode)
        {
            return access.GetSelectByUserIdAndDeptCount(strUserID, strDeptCode);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByUserIdAndDept(string strUserID, int iIndex, int iCount)
        {
            return access.SelectByUserIdAndDept(strUserID, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageInfoVo tOaMessageInfo)
        {
            return access.SelectByTable(tOaMessageInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageInfo">对象</param>
        /// <returns></returns>
        public TOaMessageInfoVo SelectByObject(TOaMessageInfoVo tOaMessageInfo)
        {
            return access.SelectByObject(tOaMessageInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageInfoVo tOaMessageInfo)
        {
            return access.Create(tOaMessageInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoVo tOaMessageInfo)
        {
            return access.Edit(tOaMessageInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoVo tOaMessageInfo_UpdateSet, TOaMessageInfoVo tOaMessageInfo_UpdateWhere)
        {
            return access.Edit(tOaMessageInfo_UpdateSet, tOaMessageInfo_UpdateWhere);
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
        public bool Delete(TOaMessageInfoVo tOaMessageInfo)
        {
            return access.Delete(tOaMessageInfo);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //消息编号
	  if (tOaMessageInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("消息编号不能为空");
                return false;
            }
	  //消息标题
	  if (tOaMessageInfo.MESSAGE_TITLE.Trim() == "")
            {
                this.Tips.AppendLine("消息标题不能为空");
                return false;
            }
	  //短消息内容
	  if (tOaMessageInfo.MESSAGE_CONTENT.Trim() == "")
            {
                this.Tips.AppendLine("短消息内容不能为空");
                return false;
            }
	  //消息发送人
	  if (tOaMessageInfo.SEND_BY.Trim() == "")
            {
                this.Tips.AppendLine("消息发送人不能为空");
                return false;
            }
	  //消息发送时间
	  if (tOaMessageInfo.SEND_DATE.Trim() == "")
            {
                this.Tips.AppendLine("消息发送时间不能为空");
                return false;
            }
	  //发送时分（时分秒）
	  if (tOaMessageInfo.SEND_TIME.Trim() == "")
            {
                this.Tips.AppendLine("发送时分（时分秒）不能为空");
                return false;
            }
	  //消息发送方式(4：定期，3：立即)，暂不支持周期循环发送，客户处暂未发现类似需求
	  if (tOaMessageInfo.SEND_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("消息发送方式(4：定期，3：立即)，暂不支持周期循环发送，客户处暂未发现类似需求不能为空");
                return false;
            }
	  //接收类别(1：全站；2：按人)
	  if (tOaMessageInfo.ACCEPT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("接收类别(1：全站；2：按人)不能为空");
                return false;
            }
	  //所有接收人id以中文逗号串联，仅为查询方便
	  if (tOaMessageInfo.ACCEPT_USERIDS.Trim() == "")
            {
                this.Tips.AppendLine("所有接收人id以中文逗号串联，仅为查询方便不能为空");
                return false;
            }
	  //所有接收人REALNAME以中文逗号串联，仅为查询方便
	  if (tOaMessageInfo.ACCEPT_REALNAMES.Trim() == "")
            {
                this.Tips.AppendLine("所有接收人REALNAME以中文逗号串联，仅为查询方便不能为空");
                return false;
            }
	  //所有接收部门id以中文逗号串联，仅为查询方便
	  if (tOaMessageInfo.ACCEPT_DEPTIDS.Trim() == "")
            {
                this.Tips.AppendLine("所有接收部门id以中文逗号串联，仅为查询方便不能为空");
                return false;
            }
	  //所有接收部门NAME以中文逗号串联串联，仅为查询方便
	  if (tOaMessageInfo.ACCEPT_DEPTNAMES.Trim() == "")
            {
                this.Tips.AppendLine("所有接收部门NAME以中文逗号串联串联，仅为查询方便不能为空");
                return false;
            }
	  //工作流任务id，将消息和任务关联，方便任务办理后消除消息，方便直接点消息办理任务
	  if (tOaMessageInfo.TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("工作流任务id，将消息和任务关联，方便任务办理后消除消息，方便直接点消息办理任务不能为空");
                return false;
            }
	  //备注1
	  if (tOaMessageInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaMessageInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaMessageInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
