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
    /// 功能：短消息已阅表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoDelLogic : LogicBase
    {

	TOaMessageInfoDelVo tOaMessageInfoDel = new TOaMessageInfoDelVo();
    TOaMessageInfoDelAccess access;
		
	public TOaMessageInfoDelLogic()
	{
		  access = new TOaMessageInfoDelAccess();  
	}
		
	public TOaMessageInfoDelLogic(TOaMessageInfoDelVo _tOaMessageInfoDel)
	{
		tOaMessageInfoDel = _tOaMessageInfoDel;
		access = new TOaMessageInfoDelAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            return access.GetSelectResultCount(tOaMessageInfoDel);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageInfoDelVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageInfoDelVo Details(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            return access.Details(tOaMessageInfoDel);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageInfoDelVo> SelectByObject(TOaMessageInfoDelVo tOaMessageInfoDel, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaMessageInfoDel, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageInfoDelVo tOaMessageInfoDel, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaMessageInfoDel, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageInfoDel"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            return access.SelectByTable(tOaMessageInfoDel);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageInfoDel">对象</param>
        /// <returns></returns>
        public TOaMessageInfoDelVo SelectByObject(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            return access.SelectByObject(tOaMessageInfoDel);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            return access.Create(tOaMessageInfoDel);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoDel">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            return access.Edit(tOaMessageInfoDel);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoDel_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageInfoDel_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoDelVo tOaMessageInfoDel_UpdateSet, TOaMessageInfoDelVo tOaMessageInfoDel_UpdateWhere)
        {
            return access.Edit(tOaMessageInfoDel_UpdateSet, tOaMessageInfoDel_UpdateWhere);
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
        public bool Delete(TOaMessageInfoDelVo tOaMessageInfoDel)
        {
            return access.Delete(tOaMessageInfoDel);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tOaMessageInfoDel.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //消息表ID、消息接收表ID，具体对哪个id进行清除，根据接收发送类型决定
	  if (tOaMessageInfoDel.MESSAGE_INFO_ID.Trim() == "")
            {
                this.Tips.AppendLine("消息表ID、消息接收表ID，具体对哪个id进行清除，根据接收发送类型决定不能为空");
                return false;
            }
	  //接收发送类型（发送、接收）
	  if (tOaMessageInfoDel.SEND_OR_ACCEPT.Trim() == "")
            {
                this.Tips.AppendLine("接收发送类型（发送、接收）不能为空");
                return false;
            }
	  //清除标识
	  if (tOaMessageInfoDel.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("清除标识不能为空");
                return false;
            }
	  //备份1
	  if (tOaMessageInfoDel.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备份1不能为空");
                return false;
            }
	  //备份2
	  if (tOaMessageInfoDel.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备份2不能为空");
                return false;
            }
	  //备份3
	  if (tOaMessageInfoDel.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备份3不能为空");
                return false;
            }
	  //备份4
	  if (tOaMessageInfoDel.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备份4不能为空");
                return false;
            }
	  //备份5
	  if (tOaMessageInfoDel.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备份5不能为空");
                return false;
            }

            return true;
        }

    }
}
