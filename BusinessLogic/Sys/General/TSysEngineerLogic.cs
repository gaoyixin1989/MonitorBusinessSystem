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
    /// 功能：维护工程师管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysEngineerLogic : LogicBase
    {

        TSysEngineerVo tSysEngineer = new TSysEngineerVo();
        TSysEngineerAccess access;

        public TSysEngineerLogic()
        {
            access = new TSysEngineerAccess();
        }

        public TSysEngineerLogic(TSysEngineerVo _tSysEngineer)
        {
            tSysEngineer = _tSysEngineer;
            access = new TSysEngineerAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysEngineer">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysEngineerVo tSysEngineer)
        {
            return access.GetSelectResultCount(tSysEngineer);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysEngineerVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysEngineer">对象条件</param>
        /// <returns>对象</returns>
        public TSysEngineerVo Details(TSysEngineerVo tSysEngineer)
        {
            return access.Details(tSysEngineer);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysEngineer">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysEngineerVo> SelectByObject(TSysEngineerVo tSysEngineer, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysEngineer, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysEngineer">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysEngineerVo tSysEngineer, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysEngineer, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysEngineer"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysEngineerVo tSysEngineer)
        {
            return access.SelectByTable(tSysEngineer);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysEngineer">对象</param>
        /// <returns></returns>
        public TSysEngineerVo SelectByObject(TSysEngineerVo tSysEngineer)
        {
            return access.SelectByObject(tSysEngineer);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysEngineerVo tSysEngineer)
        {
            return access.Create(tSysEngineer);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysEngineer">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysEngineerVo tSysEngineer)
        {
            return access.Edit(tSysEngineer);
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
            //编号
            if (tSysEngineer.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //工程师编码
            if (tSysEngineer.ENGINEER_CODE.Trim() == "")
            {
                this.Tips.AppendLine("工程师编码不能为空");
                return false;
            }
            //真实姓名
            if (tSysEngineer.ENGINEERL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("真实姓名不能为空");
                return false;
            }
            //手机号码
            if (tSysEngineer.PHONE_MOBILE.Trim() == "")
            {
                this.Tips.AppendLine("手机号码不能为空");
                return false;
            }
            //办公电话
            if (tSysEngineer.PHONE_OFFICE.Trim() == "")
            {
                this.Tips.AppendLine("办公电话不能为空");
                return false;
            }
            //家庭电话
            if (tSysEngineer.PHONE_HOME.Trim() == "")
            {
                this.Tips.AppendLine("家庭电话不能为空");
                return false;
            }
            //业务代码
            if (tSysEngineer.BUSINESS_CODE.Trim() == "")
            {
                this.Tips.AppendLine("业务代码不能为空");
                return false;
            }
            //单位编码
            if (tSysEngineer.UNITS_CODE.Trim() == "")
            {
                this.Tips.AppendLine("单位编码不能为空");
                return false;
            }
            //地区编码
            if (tSysEngineer.REGION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("地区编码不能为空");
                return false;
            }
            //职务编码
            if (tSysEngineer.DUTY_CODE.Trim() == "")
            {
                this.Tips.AppendLine("职务编码不能为空");
                return false;
            }
            //启用标记,1启用,0停用
            if (tSysEngineer.IS_USE.Trim() == "")
            {
                this.Tips.AppendLine("启用标记,1启用,0停用不能为空");
                return false;
            }
            //删除标记,1为删除,
            if (tSysEngineer.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记,1为删除,不能为空");
                return false;
            }
            //创建人ID
            if (tSysEngineer.CREATE_ID.Trim() == "")
            {
                this.Tips.AppendLine("创建人ID不能为空");
                return false;
            }
            //创建时间
            if (tSysEngineer.CREATE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("创建时间不能为空");
                return false;
            }
            //备注
            if (tSysEngineer.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysEngineer.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysEngineer.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysEngineer.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
