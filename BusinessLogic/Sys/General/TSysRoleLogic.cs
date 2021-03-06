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
    /// 功能：角色管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysRoleLogic : LogicBase
    {

        TSysRoleVo tSysRole = new TSysRoleVo();
        TSysRoleAccess access;

        public TSysRoleLogic()
        {
            access = new TSysRoleAccess();
        }

        public TSysRoleLogic(TSysRoleVo _tSysRole)
        {
            tSysRole = _tSysRole;
            access = new TSysRoleAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysRole">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysRoleVo tSysRole)
        {
            return access.GetSelectResultCount(tSysRole);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysRoleVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysRole">对象条件</param>
        /// <returns>对象</returns>
        public TSysRoleVo Details(TSysRoleVo tSysRole)
        {
            return access.Details(tSysRole);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysRoleVo> SelectByObject(TSysRoleVo tSysRole, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysRole, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysRole">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysRoleVo tSysRole, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysRole, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysRole"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysRoleVo tSysRole)
        {
            return access.SelectByTable(tSysRole);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysRole">对象</param>
        /// <returns></returns>
        public TSysRoleVo SelectByObject(TSysRoleVo tSysRole)
        {
            return access.SelectByObject(tSysRole);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysRoleVo tSysRole)
        {
            return access.Create(tSysRole);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysRole">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysRoleVo tSysRole)
        {
            return access.Edit(tSysRole);
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
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //是否为用户唯一
            if (tSysRole.USER_ONLY.Trim() == "")
            {
                this.Tips.AppendLine("是否为用户唯一不能为空");
                return false;
            }
            //启用标记,1为启用
            if (tSysRole.IS_USE.Trim() == "")
            {
                this.Tips.AppendLine("启用标记,1为启用不能为空");
                return false;
            }
            //删除标记,1为删除
            if (tSysRole.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记,1为删除不能为空");
                return false;
            }
            //备注
            if (tSysRole.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysRole.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysRole.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysRole.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tSysRole.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tSysRole.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }
            //角色编号
            if (tSysRole.ID.Trim() == "")
            {
                this.Tips.AppendLine("角色编号不能为空");
                return false;
            }
            //角色名称
            if (tSysRole.ROLE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("角色名称不能为空");
                return false;
            }
            //角色类型
            if (tSysRole.ROLE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("角色类型不能为空");
                return false;
            }
            //角色说明
            if (tSysRole.ROLE_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("角色说明不能为空");
                return false;
            }
            //创建人ID
            if (tSysRole.CREATE_ID.Trim() == "")
            {
                this.Tips.AppendLine("创建人ID不能为空");
                return false;
            }
            //创建时间
            if (tSysRole.CREATE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("创建时间不能为空");
                return false;
            }

            return true;
        }

    }
}
