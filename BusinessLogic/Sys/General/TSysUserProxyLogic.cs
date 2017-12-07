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
    /// 功能：出差代理
    /// 创建日期：2012-11-15
    /// 创建人：潘德军
    /// </summary>
    public class TSysUserProxyLogic : LogicBase
    {

        TSysUserProxyVo tSysUserProxy = new TSysUserProxyVo();
        TSysUserProxyAccess access;

        public TSysUserProxyLogic()
        {
            access = new TSysUserProxyAccess();
        }

        public TSysUserProxyLogic(TSysUserProxyVo _tSysUserProxy)
        {
            tSysUserProxy = _tSysUserProxy;
            access = new TSysUserProxyAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUserProxy">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserProxyVo tSysUserProxy)
        {
            return access.GetSelectResultCount(tSysUserProxy);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserProxyVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUserProxy">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserProxyVo Details(TSysUserProxyVo tSysUserProxy)
        {
            return access.Details(tSysUserProxy);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUserProxy">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserProxyVo> SelectByObject(TSysUserProxyVo tSysUserProxy, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysUserProxy, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUserProxy">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserProxyVo tSysUserProxy, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysUserProxy, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUserProxy"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserProxyVo tSysUserProxy)
        {
            return access.SelectByTable(tSysUserProxy);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUserProxy">对象</param>
        /// <returns></returns>
        public TSysUserProxyVo SelectByObject(TSysUserProxyVo tSysUserProxy)
        {
            return access.SelectByObject(tSysUserProxy);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserProxyVo tSysUserProxy)
        {
            return access.Create(tSysUserProxy);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserProxy">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserProxyVo tSysUserProxy)
        {
            return access.Edit(tSysUserProxy);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserProxy_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysUserProxy_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserProxyVo tSysUserProxy_UpdateSet, TSysUserProxyVo tSysUserProxy_UpdateWhere)
        {
            return access.Edit(tSysUserProxy_UpdateSet, tSysUserProxy_UpdateWhere);
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
        public bool Delete(TSysUserProxyVo tSysUserProxy)
        {
            return access.Delete(tSysUserProxy);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tSysUserProxy.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //用户编号
            if (tSysUserProxy.USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("用户编号不能为空");
                return false;
            }
            //被代理人ID
            if (tSysUserProxy.PROXY_USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("被代理人ID不能为空");
                return false;
            }
            //备注
            if (tSysUserProxy.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysUserProxy.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysUserProxy.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysUserProxy.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
