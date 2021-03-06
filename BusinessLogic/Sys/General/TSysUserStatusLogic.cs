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
    /// 功能：在线用户管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysUserStatusLogic : LogicBase
    {

        TSysUserStatusVo tSysUserStatus = new TSysUserStatusVo();
        TSysUserStatusAccess access;

        public TSysUserStatusLogic()
        {
            access = new TSysUserStatusAccess();
        }

        public TSysUserStatusLogic(TSysUserStatusVo _tSysUserStatus)
        {
            tSysUserStatus = _tSysUserStatus;
            access = new TSysUserStatusAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserStatusVo tSysUserStatus)
        {
            return access.GetSelectResultCount(tSysUserStatus);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserStatusVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUserStatus">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserStatusVo Details(TSysUserStatusVo tSysUserStatus)
        {
            return access.Details(tSysUserStatus);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserStatusVo> SelectByObject(TSysUserStatusVo tSysUserStatus, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysUserStatus, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserStatusVo tSysUserStatus, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysUserStatus, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUserStatus"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserStatusVo tSysUserStatus)
        {
            return access.SelectByTable(tSysUserStatus);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <returns></returns>
        public TSysUserStatusVo SelectByObject(TSysUserStatusVo tSysUserStatus)
        {
            return access.SelectByObject(tSysUserStatus);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserStatusVo tSysUserStatus)
        {
            return access.Create(tSysUserStatus);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserStatus">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserStatusVo tSysUserStatus)
        {
            return access.Edit(tSysUserStatus);
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
        /// 扩展方法
        /// 获得在线用户列表,通过时间
        /// 创建日期：2011-04-20 17:10
        /// 创建人  ：郑义
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <param name="dateTime">时间</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableEx(TSysUserStatusVo tSysUserStatus, int iIndex, int iCount, DateTime dateTime,string strRealName)
        {
            return access.SelectByTableEx(tSysUserStatus, iIndex, iCount, dateTime, strRealName);
        }
        /// <summary>
        /// 扩展方法
        /// 获得在线用户查询结果总行数，用于分页
        /// 创建日期：2011-04-20 18:30
        /// 创建人  ：郑义
        /// </summary>
        /// <param name="tSysUserStatus">对象</param>
        /// <param name="dateTime">时间</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountEx(TSysUserStatusVo tSysUserStatus, DateTime dateTime, string strRealName)
        {
            return access.GetSelectResultCountEx(tSysUserStatus, dateTime, strRealName);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tSysUserStatus.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //用户编号
            if (tSysUserStatus.USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("用户编号不能为空");
                return false;
            }
            //最后一次访问时间
            if (tSysUserStatus.LAST_OPTIME.Trim() == "")
            {
                this.Tips.AppendLine("最后一次访问时间不能为空");
                return false;
            }
            //最后一次登陆IP
            if (tSysUserStatus.LAST_LOGIN_IP.Trim() == "")
            {
                this.Tips.AppendLine("最后一次登陆IP不能为空");
                return false;
            }
            //最后一次访问页面
            if (tSysUserStatus.LAST_PAGE.Trim() == "")
            {
                this.Tips.AppendLine("最后一次访问页面不能为空");
                return false;
            }
            //最后一次操作记录
            if (tSysUserStatus.LAST_OPERATION.Trim() == "")
            {
                this.Tips.AppendLine("最后一次操作记录不能为空");
                return false;
            }
            //备注
            if (tSysUserStatus.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysUserStatus.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysUserStatus.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysUserStatus.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
