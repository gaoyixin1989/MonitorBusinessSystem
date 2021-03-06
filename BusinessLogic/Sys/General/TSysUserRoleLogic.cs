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
    /// 功能：用户角色
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysUserRoleLogic : LogicBase
    {

        TSysUserRoleVo tSysUserRole = new TSysUserRoleVo();
        TSysUserRoleAccess access;

        public TSysUserRoleLogic()
        {
            access = new TSysUserRoleAccess();
        }

        public TSysUserRoleLogic(TSysUserRoleVo _tSysUserRole)
        {
            tSysUserRole = _tSysUserRole;
            access = new TSysUserRoleAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUserRole">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserRoleVo tSysUserRole)
        {
            return access.GetSelectResultCount(tSysUserRole);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserRoleVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUserRole">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserRoleVo Details(TSysUserRoleVo tSysUserRole)
        {
            return access.Details(tSysUserRole);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUserRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserRoleVo> SelectByObject(TSysUserRoleVo tSysUserRole, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysUserRole, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUserRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserRoleVo tSysUserRole, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysUserRole, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUserRole"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserRoleVo tSysUserRole)
        {
            return access.SelectByTable(tSysUserRole);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUserRole">对象</param>
        /// <returns></returns>
        public TSysUserRoleVo SelectByObject(TSysUserRoleVo tSysUserRole)
        {
            return access.SelectByObject(tSysUserRole);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserRoleVo tSysUserRole)
        {
            return access.Create(tSysUserRole);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserRole">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserRoleVo tSysUserRole)
        {
            return access.Edit(tSysUserRole);
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
        /// <param name="Id">UserId</param>
        /// <returns>是否成功</returns>
        public bool DeleteByUserId(string UserId)
        {
            return access.DeleteByUserId(UserId);
        }
        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">RoleId</param>
        /// <returns>是否成功</returns>
        public bool DeleteByRoleId(string RoleId)
        {
            return access.DeleteByRoleId(RoleId);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tSysUserRole.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //用户编号
            if (tSysUserRole.USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("用户编号不能为空");
                return false;
            }
            //角色编号
            if (tSysUserRole.ROLE_ID.Trim() == "")
            {
                this.Tips.AppendLine("角色编号不能为空");
                return false;
            }
            //备注
            if (tSysUserRole.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysUserRole.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysUserRole.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysUserRole.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
