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
    /// 功能：仪器定期维护自动提醒人员
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusMsgUserLogic : LogicBase
    {

	TBaseApparatusMsgUserVo tBaseApparatusMsgUser = new TBaseApparatusMsgUserVo();
    TBaseApparatusMsgUserAccess access;
		
	public TBaseApparatusMsgUserLogic()
	{
		  access = new TBaseApparatusMsgUserAccess();  
	}
		
	public TBaseApparatusMsgUserLogic(TBaseApparatusMsgUserVo _tBaseApparatusMsgUser)
	{
		tBaseApparatusMsgUser = _tBaseApparatusMsgUser;
		access = new TBaseApparatusMsgUserAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            return access.GetSelectResultCount(tBaseApparatusMsgUser);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseApparatusMsgUserVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象条件</param>
        /// <returns>对象</returns>
        public TBaseApparatusMsgUserVo Details(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            return access.Details(tBaseApparatusMsgUser);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseApparatusMsgUserVo> SelectByObject(TBaseApparatusMsgUserVo tBaseApparatusMsgUser, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseApparatusMsgUser, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusMsgUserVo tBaseApparatusMsgUser, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseApparatusMsgUser, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseApparatusMsgUser"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            return access.SelectByTable(tBaseApparatusMsgUser);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">对象</param>
        /// <returns></returns>
        public TBaseApparatusMsgUserVo SelectByObject(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            return access.SelectByObject(tBaseApparatusMsgUser);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            return access.Create(tBaseApparatusMsgUser);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusMsgUser">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            return access.Edit(tBaseApparatusMsgUser);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusMsgUser_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseApparatusMsgUser_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusMsgUserVo tBaseApparatusMsgUser_UpdateSet, TBaseApparatusMsgUserVo tBaseApparatusMsgUser_UpdateWhere)
        {
            return access.Edit(tBaseApparatusMsgUser_UpdateSet, tBaseApparatusMsgUser_UpdateWhere);
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
        public bool Delete(TBaseApparatusMsgUserVo tBaseApparatusMsgUser)
        {
            return access.Delete(tBaseApparatusMsgUser);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseApparatusMsgUser.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //USERID
	  if (tBaseApparatusMsgUser.USERID.Trim() == "")
            {
                this.Tips.AppendLine("USERID不能为空");
                return false;
            }
	  //备份1
	  if (tBaseApparatusMsgUser.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备份1不能为空");
                return false;
            }
	  //备份2
	  if (tBaseApparatusMsgUser.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备份2不能为空");
                return false;
            }
	  //备份3
	  if (tBaseApparatusMsgUser.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备份3不能为空");
                return false;
            }
	  //备份4
	  if (tBaseApparatusMsgUser.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备份4不能为空");
                return false;
            }
	  //备份5
	  if (tBaseApparatusMsgUser.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备份5不能为空");
                return false;
            }

            return true;
        }

    }
}
