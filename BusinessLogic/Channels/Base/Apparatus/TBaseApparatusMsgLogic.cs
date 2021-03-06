using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.DataAccess.Channels.Base.Apparatus;

namespace i3.BusinessLogic.Channels.Base.Apparatus
{
    /// <summary>
    /// 功能：仪器定期维护自动提醒
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusMsgLogic : LogicBase
    {

	TBaseApparatusMsgVo tBaseApparatusMsg = new TBaseApparatusMsgVo();
    TBaseApparatusMsgAccess access;
		
	public TBaseApparatusMsgLogic()
	{
		  access = new TBaseApparatusMsgAccess();  
	}
		
	public TBaseApparatusMsgLogic(TBaseApparatusMsgVo _tBaseApparatusMsg)
	{
		tBaseApparatusMsg = _tBaseApparatusMsg;
		access = new TBaseApparatusMsgAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            return access.GetSelectResultCount(tBaseApparatusMsg);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseApparatusMsgVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象条件</param>
        /// <returns>对象</returns>
        public TBaseApparatusMsgVo Details(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            return access.Details(tBaseApparatusMsg);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseApparatusMsgVo> SelectByObject(TBaseApparatusMsgVo tBaseApparatusMsg, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseApparatusMsg, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusMsgVo tBaseApparatusMsg, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseApparatusMsg, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseApparatusMsg"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            return access.SelectByTable(tBaseApparatusMsg);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseApparatusMsg">对象</param>
        /// <returns></returns>
        public TBaseApparatusMsgVo SelectByObject(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            return access.SelectByObject(tBaseApparatusMsg);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            return access.Create(tBaseApparatusMsg);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusMsg">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            return access.Edit(tBaseApparatusMsg);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusMsg_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseApparatusMsg_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusMsgVo tBaseApparatusMsg_UpdateSet, TBaseApparatusMsgVo tBaseApparatusMsg_UpdateWhere)
        {
            return access.Edit(tBaseApparatusMsg_UpdateSet, tBaseApparatusMsg_UpdateWhere);
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
        public bool Delete(TBaseApparatusMsgVo tBaseApparatusMsg)
        {
            return access.Delete(tBaseApparatusMsg);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseApparatusMsg.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //消息ID
	  if (tBaseApparatusMsg.MSG_ID.Trim() == "")
            {
                this.Tips.AppendLine("消息ID不能为空");
                return false;
            }
	  //仪器ID
	  if (tBaseApparatusMsg.APPARATUS_ID.Trim() == "")
            {
                this.Tips.AppendLine("仪器ID不能为空");
                return false;
            }
	  //备注1
	  if (tBaseApparatusMsg.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseApparatusMsg.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseApparatusMsg.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseApparatusMsg.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseApparatusMsg.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
