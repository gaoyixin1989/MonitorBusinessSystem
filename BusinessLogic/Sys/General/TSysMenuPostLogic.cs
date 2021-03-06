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
    /// 功能：权限管理
    /// 创建日期：2012-10-25
    /// 创建人：潘德军
    /// </summary>
    public class TSysMenuPostLogic : LogicBase
    {

	TSysMenuPostVo tSysMenuPost = new TSysMenuPostVo();
    TSysMenuPostAccess access;
		
	public TSysMenuPostLogic()
	{
		  access = new TSysMenuPostAccess();  
	}
		
	public TSysMenuPostLogic(TSysMenuPostVo _tSysMenuPost)
	{
		tSysMenuPost = _tSysMenuPost;
		access = new TSysMenuPostAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysMenuPost">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysMenuPostVo tSysMenuPost)
        {
            return access.GetSelectResultCount(tSysMenuPost);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysMenuPostVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysMenuPost">对象条件</param>
        /// <returns>对象</returns>
        public TSysMenuPostVo Details(TSysMenuPostVo tSysMenuPost)
        {
            return access.Details(tSysMenuPost);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysMenuPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysMenuPostVo> SelectByObject(TSysMenuPostVo tSysMenuPost, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysMenuPost, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysMenuPost">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysMenuPostVo tSysMenuPost, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysMenuPost, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysMenuPost"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysMenuPostVo tSysMenuPost)
        {
            return access.SelectByTable(tSysMenuPost);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysMenuPost">对象</param>
        /// <returns></returns>
        public TSysMenuPostVo SelectByObject(TSysMenuPostVo tSysMenuPost)
        {
            return access.SelectByObject(tSysMenuPost);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysMenuPostVo tSysMenuPost)
        {
            return access.Create(tSysMenuPost);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysMenuPost">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysMenuPostVo tSysMenuPost)
        {
            return access.Edit(tSysMenuPost);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysMenuPost_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tSysMenuPost_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysMenuPostVo tSysMenuPost_UpdateSet, TSysMenuPostVo tSysMenuPost_UpdateWhere)
        {
            return access.Edit(tSysMenuPost_UpdateSet, tSysMenuPost_UpdateWhere);
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
        public bool Delete(TSysMenuPostVo tSysMenuPost)
        {
            return access.Delete(tSysMenuPost);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tSysMenuPost.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //人员ID
	  if (tSysMenuPost.POST_ID.Trim() == "")
            {
                this.Tips.AppendLine("人员ID不能为空");
                return false;
            }
	  //人员职位类型,1,人员，2，职位
	  if (tSysMenuPost.RIGHT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("人员职位类型,1,人员，2，职位不能为空");
                return false;
            }
	  //菜单编号
	  if (tSysMenuPost.MENU_ID.Trim() == "")
            {
                this.Tips.AppendLine("菜单编号不能为空");
                return false;
            }
	  //备注
	  if (tSysMenuPost.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
	  //备注1
	  if (tSysMenuPost.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tSysMenuPost.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tSysMenuPost.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
