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
    /// 功能：角色菜单
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysMenuRoleLogic : LogicBase
    {

        TSysMenuRoleVo tSysMenuRole = new TSysMenuRoleVo();
        TSysMenuRoleAccess access;

        public TSysMenuRoleLogic()
        {
            access = new TSysMenuRoleAccess();
        }

        public TSysMenuRoleLogic(TSysMenuRoleVo _tSysMenuRole)
        {
            tSysMenuRole = _tSysMenuRole;
            access = new TSysMenuRoleAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysMenuRole">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysMenuRoleVo tSysMenuRole)
        {
            return access.GetSelectResultCount(tSysMenuRole);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysMenuRoleVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysMenuRole">对象条件</param>
        /// <returns>对象</returns>
        public TSysMenuRoleVo Details(TSysMenuRoleVo tSysMenuRole)
        {
            return access.Details(tSysMenuRole);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysMenuRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysMenuRoleVo> SelectByObject(TSysMenuRoleVo tSysMenuRole, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysMenuRole, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysMenuRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysMenuRoleVo tSysMenuRole, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysMenuRole, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysMenuRole"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysMenuRoleVo tSysMenuRole)
        {
            return access.SelectByTable(tSysMenuRole);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysMenuRole">对象</param>
        /// <returns></returns>
        public TSysMenuRoleVo SelectByObject(TSysMenuRoleVo tSysMenuRole)
        {
            return access.SelectByObject(tSysMenuRole);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysMenuRoleVo tSysMenuRole)
        {
            return access.Create(tSysMenuRole);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysMenuRole">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysMenuRoleVo tSysMenuRole)
        {
            return access.Edit(tSysMenuRole);
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
        /// <param name="Id">RoleID</param>
        /// <returns>是否成功</returns>
        public bool DeleteByRoleID(string Id)
        {
            return access.DeleteByRoleID(Id);
        }
        

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tSysMenuRole.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //角色编号
            if (tSysMenuRole.ROLE_ID.Trim() == "")
            {
                this.Tips.AppendLine("角色编号不能为空");
                return false;
            }
            //菜单编号
            if (tSysMenuRole.MENU_ID.Trim() == "")
            {
                this.Tips.AppendLine("菜单编号不能为空");
                return false;
            }
            //备注
            if (tSysMenuRole.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysMenuRole.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysMenuRole.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysMenuRole.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
