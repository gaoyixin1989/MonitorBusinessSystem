using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.General;
using i3.DataAccess.Sys.General;

namespace i3.BusinessLogic.Sys.General
{
    /// <summary>
    /// 功能：用户职位表
    /// 创建日期：2012-10-25
    /// 创建人：潘德军
    /// </summary>
    public class TSysUserPostLogic : LogicBase
    {

	TSysUserPostVo tSysUserPost = new TSysUserPostVo();
    TSysUserPostAccess access;
		
	public TSysUserPostLogic()
	{
		  access = new TSysUserPostAccess();  
	}
		
	public TSysUserPostLogic(TSysUserPostVo _tSysUserPost)
	{
		tSysUserPost = _tSysUserPost;
		access = new TSysUserPostAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUserPost">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserPostVo tSysUserPost)
        {
            return access.GetSelectResultCount(tSysUserPost);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserPostVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUserPost">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserPostVo Details(TSysUserPostVo tSysUserPost)
        {
            return access.Details(tSysUserPost);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUserPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserPostVo> SelectByObject(TSysUserPostVo tSysUserPost, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysUserPost, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUserPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserPostVo tSysUserPost, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysUserPost, iIndex, iCount);
        }

        /// <summary>
        /// 获得指定用户的科室主任ID或者直接上级ID，如果该用户属于多个科室，则出现多个主任
        /// </summary>
        /// <param name="strUserId"></param>
        /// <returns></returns>
        public DataTable SelectDeptAdmin_byTable(string strUserId)
        {
            return access.SelectDeptAdmin_byTable(strUserId);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUserPost"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserPostVo tSysUserPost)
        {
            return access.SelectByTable(tSysUserPost);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUserPost">对象</param>
        /// <returns></returns>
        public TSysUserPostVo SelectByObject(TSysUserPostVo tSysUserPost)
        {
            return access.SelectByObject(tSysUserPost);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserPostVo tSysUserPost)
        {
            return access.Create(tSysUserPost);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserPost">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserPostVo tSysUserPost)
        {
            return access.Edit(tSysUserPost);
        }

	    /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserPost_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tSysUserPost_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserPostVo tSysUserPost_UpdateSet, TSysUserPostVo tSysUserPost_UpdateWhere)
        {
            return access.Edit(tSysUserPost_UpdateSet, tSysUserPost_UpdateWhere);
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
        public bool Delete(TSysUserPostVo tSysUserPost)
        {
            return access.Delete(tSysUserPost);
        }

        public bool UpdateUserPostInfor(string strUserId, string[] strPostId)
        {
            return access.UpdateUserPostInfor(strUserId,strPostId);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tSysUserPost.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //用户编号
	  if (tSysUserPost.USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("用户编号不能为空");
                return false;
            }
	  //角色编号
	  if (tSysUserPost.POST_ID.Trim() == "")
            {
                this.Tips.AppendLine("角色编号不能为空");
                return false;
            }
	  //备注
	  if (tSysUserPost.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
	  //备注1
	  if (tSysUserPost.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tSysUserPost.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tSysUserPost.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
