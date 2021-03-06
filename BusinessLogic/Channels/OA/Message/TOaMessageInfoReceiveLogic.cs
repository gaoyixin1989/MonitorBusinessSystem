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
    /// 功能：短信息接收
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoReceiveLogic : LogicBase
    {

	TOaMessageInfoReceiveVo tOaMessageInfoReceive = new TOaMessageInfoReceiveVo();
    TOaMessageInfoReceiveAccess access;
		
	public TOaMessageInfoReceiveLogic()
	{
		  access = new TOaMessageInfoReceiveAccess();  
	}
		
	public TOaMessageInfoReceiveLogic(TOaMessageInfoReceiveVo _tOaMessageInfoReceive)
	{
		tOaMessageInfoReceive = _tOaMessageInfoReceive;
		access = new TOaMessageInfoReceiveAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            return access.GetSelectResultCount(tOaMessageInfoReceive);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageInfoReceiveVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageInfoReceiveVo Details(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            return access.Details(tOaMessageInfoReceive);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageInfoReceiveVo> SelectByObject(TOaMessageInfoReceiveVo tOaMessageInfoReceive, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaMessageInfoReceive, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageInfoReceiveVo tOaMessageInfoReceive, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaMessageInfoReceive, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageInfoReceive"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            return access.SelectByTable(tOaMessageInfoReceive);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageInfoReceive">对象</param>
        /// <returns></returns>
        public TOaMessageInfoReceiveVo SelectByObject(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            return access.SelectByObject(tOaMessageInfoReceive);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            return access.Create(tOaMessageInfoReceive);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoReceive">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            return access.Edit(tOaMessageInfoReceive);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoReceive_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageInfoReceive_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoReceiveVo tOaMessageInfoReceive_UpdateSet, TOaMessageInfoReceiveVo tOaMessageInfoReceive_UpdateWhere)
        {
            return access.Edit(tOaMessageInfoReceive_UpdateSet, tOaMessageInfoReceive_UpdateWhere);
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
        public bool Delete(TOaMessageInfoReceiveVo tOaMessageInfoReceive)
        {
            return access.Delete(tOaMessageInfoReceive);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //消息阅读编号
	  if (tOaMessageInfoReceive.ID.Trim() == "")
            {
                this.Tips.AppendLine("消息阅读编号不能为空");
                return false;
            }
	  //消息编号
	  if (tOaMessageInfoReceive.MESSAGE_ID.Trim() == "")
            {
                this.Tips.AppendLine("消息编号不能为空");
                return false;
            }
	  //消息接收人
	  if (tOaMessageInfoReceive.RECEIVER.Trim() == "")
            {
                this.Tips.AppendLine("消息接收人不能为空");
                return false;
            }
	  //是否已阅读
	  if (tOaMessageInfoReceive.IS_READ.Trim() == "")
            {
                this.Tips.AppendLine("是否已阅读不能为空");
                return false;
            }
	  //消息查阅时间
	  if (tOaMessageInfoReceive.READ_DATE.Trim() == "")
            {
                this.Tips.AppendLine("消息查阅时间不能为空");
                return false;
            }
	  //备注1
	  if (tOaMessageInfoReceive.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaMessageInfoReceive.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaMessageInfoReceive.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
