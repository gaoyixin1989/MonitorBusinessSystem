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
    /// 功能：用户管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// 功能：删除几个无用函数（指向用户组的函数）、修改查询用户语句（去除Oracle有关）
    /// 功能：增加SelectSubPost(获取指定职位的所有下属职位)、SelectUnderling_OfPost（获取指定职位的所有下属人员）、SelectUnderling_OfUser（获取指定用户的所有下属人员）
    /// 修改日期：2012-11-16
    /// 修改人：潘德军
    /// </summary>
    public class TSysUserLogic : LogicBase
    {

        TSysUserVo tSysUser = new TSysUserVo();
        TSysUserAccess access;

        public TSysUserLogic()
        {
            access = new TSysUserAccess();
        }

        public TSysUserLogic(TSysUserVo _tSysUser)
        {
            tSysUser = _tSysUser;
            access = new TSysUserAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserVo tSysUser)
        {
            return access.GetSelectResultCount(tSysUser);
        }
        
        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUser">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserVo Details(TSysUserVo tSysUser)
        {
            return access.Details(tSysUser);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserVo> SelectByObject(TSysUserVo tSysUser, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysUser, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserVo tSysUser, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysUser, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable Create By :weilin 
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableEx(TSysUserVo tSysUser, int iIndex, int iCount)
        {
            return access.SelectByTableEx(tSysUser, iIndex, iCount);
        }

         /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ByDept(TSysUserVo tSysUser)
        {
            return access.GetSelectResultCount_ByDept(tSysUser);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ByDept(TSysUserVo tSysUser, int iIndex, int iCount)
        {
            return access.SelectByTable_ByDept(tSysUser, iIndex, iCount);
        }

        /// <summary>
        /// 获取指定职位的所有下属职位
        /// </summary>
        /// <param name="strPostID"></param>
        /// <returns></returns>
        public DataTable SelectSubPost(string strPostID)
        {
            return access.SelectSubPost(strPostID);
        }

        /// <summary>
        /// 获取指定职位的所有下属人员
        /// </summary>
        /// <param name="strPostID"></param>
        /// <returns></returns>
        public DataTable SelectUnderling_OfPost(string strPostID)
        {
            return access.SelectUnderling_OfPost(strPostID);
        }

        /// <summary>
        /// 获取指定用户的所有下属人员
        /// </summary>
        /// <param name="strPostID"></param>
        /// <returns></returns>
        public DataTable SelectUnderling_OfUser(string strUserID)
        {
            return access.SelectUnderling_OfUser(strUserID);
        }
        
        /// <summary>
        /// 获取对象DataTable,查询指定部门的用户
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableUnderDept(string strDeptID, int iIndex, int iCount)
        {
            return access.SelectByTableUnderDept(strDeptID, iIndex, iCount);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页,查询指定部门的用户
        /// </summary>
        /// <param name="tSysPost">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectByTableUnderDept(string strDeptID)
        {
            return access.GetSelectByTableUnderDept(strDeptID);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUser"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserVo tSysUser)
        {
            return access.SelectByTable(tSysUser);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUser">对象</param>
        /// <returns></returns>
        public TSysUserVo SelectByObject(TSysUserVo tSysUser)
        {
            return access.SelectByObject(tSysUser);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserVo tSysUser)
        {
            return access.Create(tSysUser);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUser">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserVo tSysUser)
        {
            return access.Edit(tSysUser);
        }
        /// <summary>
        /// 对象编辑通过名称
        /// </summary>
        /// <param name="tSysUser">用户对象</param>
        /// <returns>是否成功</returns>
        public bool EditByName(TSysUserVo tSysUser)
        {
            return access.EditByName(tSysUser);
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
        /// 获取符合条件的用户
        /// </summary>
        /// <param name="tSysUser"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectUnionByDefineTable(TSysUserVo tSysUser, int iIndex, int iCount)
        {
            return access.SelectUnionByDefineTable(tSysUser, iIndex, iCount);
        }

        /// <summary>
        /// 获取符合条件的用户记录
        /// </summary>
        /// <param name="tSysUser"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetSelectUnionByDefineTableCount(TSysUserVo tSysUser)
        {
            return access.GetSelectUnionByDefineTableCount(tSysUser);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //用户编号
            if (tSysUser.ID.Trim() == "")
            {
                this.Tips.AppendLine("用户编号不能为空");
                return false;
            }
            //用户登录名
            if (tSysUser.USER_NAME.Trim() == "")
            {
                this.Tips.AppendLine("用户登录名不能为空");
                return false;
            }
            //真实姓名
            if (tSysUser.REAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("真实姓名不能为空");
                return false;
            }
            //名次排序
            if (tSysUser.ORDER_ID.Trim() == "")
            {
                this.Tips.AppendLine("名次排序不能为空");
                return false;
            }
            //用户类型
            if (tSysUser.USER_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("用户类型不能为空");
                return false;
            }
            //登录密码
            if (tSysUser.USER_PWD.Trim() == "")
            {
                this.Tips.AppendLine("登录密码不能为空");
                return false;
            }
            //单位编码
            if (tSysUser.UNITS_CODE.Trim() == "")
            {
                this.Tips.AppendLine("单位编码不能为空");
                return false;
            }
            //地区编码
            if (tSysUser.REGION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("地区编码不能为空");
                return false;
            }
            //职务编码
            if (tSysUser.DUTY_CODE.Trim() == "")
            {
                this.Tips.AppendLine("职务编码不能为空");
                return false;
            }
            //业务代码
            if (tSysUser.BUSINESS_CODE.Trim() == "")
            {
                this.Tips.AppendLine("业务代码不能为空");
                return false;
            }
            //性别
            if (tSysUser.SEX.Trim() == "")
            {
                this.Tips.AppendLine("性别不能为空");
                return false;
            }
            //出生日期
            if (tSysUser.BIRTHDAY.Trim() == "")
            {
                this.Tips.AppendLine("出生日期不能为空");
                return false;
            }
            //邮件地址
            if (tSysUser.EMAIL.Trim() == "")
            {
                this.Tips.AppendLine("邮件地址不能为空");
                return false;
            }
            //详细地址
            if (tSysUser.ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("详细地址不能为空");
                return false;
            }
            //邮编
            if (tSysUser.POSTCODE.Trim() == "")
            {
                this.Tips.AppendLine("邮编不能为空");
                return false;
            }
            //手机号码
            if (tSysUser.PHONE_MOBILE.Trim() == "")
            {
                this.Tips.AppendLine("手机号码不能为空");
                return false;
            }
            //家庭电话
            if (tSysUser.PHONE_HOME.Trim() == "")
            {
                this.Tips.AppendLine("家庭电话不能为空");
                return false;
            }
            //办公电话
            if (tSysUser.PHONE_OFFICE.Trim() == "")
            {
                this.Tips.AppendLine("办公电话不能为空");
                return false;
            }
            //限定登录IP
            if (tSysUser.ALLOW_IP.Trim() == "")
            {
                this.Tips.AppendLine("限定登录IP不能为空");
                return false;
            }
            //启用标记,1启用,0停用
            if (tSysUser.IS_USE.Trim() == "")
            {
                this.Tips.AppendLine("启用标记,1启用,0停用不能为空");
                return false;
            }
            //删除标记,1为删除,
            if (tSysUser.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记,1为删除,不能为空");
                return false;
            }
            //创建人ID
            if (tSysUser.CREATE_ID.Trim() == "")
            {
                this.Tips.AppendLine("创建人ID不能为空");
                return false;
            }
            //创建时间
            if (tSysUser.CREATE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("创建时间不能为空");
                return false;
            }
            //备注
            if (tSysUser.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysUser.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysUser.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysUser.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tSysUser.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tSysUser.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
