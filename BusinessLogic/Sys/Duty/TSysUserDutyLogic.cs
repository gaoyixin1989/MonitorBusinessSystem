using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.Duty;
using i3.DataAccess.Sys.Duty;

namespace i3.BusinessLogic.Sys.Duty
{
    /// <summary>
    /// 功能：上岗资质管理
    /// 创建日期：2012-11-12
    /// 创建人：胡方扬
    /// </summary>
    public class TSysUserDutyLogic : LogicBase
    {

        TSysUserDutyVo tSysUserDuty = new TSysUserDutyVo();
        TSysUserDutyAccess access;

        public TSysUserDutyLogic()
        {
            access = new TSysUserDutyAccess();
        }

        public TSysUserDutyLogic(TSysUserDutyVo _tSysUserDuty)
        {
            tSysUserDuty = _tSysUserDuty;
            access = new TSysUserDutyAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUserDuty">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserDutyVo tSysUserDuty)
        {
            return access.GetSelectResultCount(tSysUserDuty);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserDutyVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUserDuty">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserDutyVo Details(TSysUserDutyVo tSysUserDuty)
        {
            return access.Details(tSysUserDuty);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUserDuty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserDutyVo> SelectByObject(TSysUserDutyVo tSysUserDuty, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysUserDuty, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUserDuty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserDutyVo tSysUserDuty, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysUserDuty, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUserDuty"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserDutyVo tSysUserDuty)
        {
            return access.SelectByTable(tSysUserDuty);
        }

        /// <summary>
        /// 获取加载已经选择了的岗位职责项目
        /// </summary>
        /// <param name="MonitorId">监测类型ID</param>
        /// <param name="strUserId">用户ID</param>
        /// <param name="blDefaultAu">是否为默认负责人</param>
        /// <param name="blDefaultEx">是否为默认协同人</param>
        /// <returns></returns>
        public DataTable GetExistMonitor(string MonitorId, string strUserId, bool blDefaultAu, bool blDefaultEx)
        {
            return access.GetExistMonitor(MonitorId,strUserId,blDefaultAu,blDefaultEx);
        }
        /// <summary>
        /// 获取分析岗位职责中 对监测类别的设置数据的加载获取
        /// </summary>
        /// <param name="MonitorId"></param>
        /// <param name="strUserId"></param>
        /// <param name="blDefaultAu"></param>
        /// <param name="blDefaultEx"></param>
        /// <returns></returns>
        public DataTable GetExistMonitorType(string MonitorId, string strUserId, bool blDefaultAu, bool blDefaultEx)
        {
            return access.GetExistMonitorType(MonitorId, strUserId, blDefaultAu, blDefaultEx);
        }
        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUserDuty">对象</param>
        /// <returns></returns>
        public TSysUserDutyVo SelectByObject(TSysUserDutyVo tSysUserDuty)
        {
            return access.SelectByObject(tSysUserDuty);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserDutyVo tSysUserDuty)
        {
            return access.Create(tSysUserDuty);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserDuty">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserDutyVo tSysUserDuty)
        {
            return access.Edit(tSysUserDuty);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserDuty_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysUserDuty_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserDutyVo tSysUserDuty_UpdateSet, TSysUserDutyVo tSysUserDuty_UpdateWhere)
        {
            return access.Edit(tSysUserDuty_UpdateSet, tSysUserDuty_UpdateWhere);
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
        public bool Delete(TSysUserDutyVo tSysUserDuty)
        {
            return access.Delete(tSysUserDuty);
        }
 
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tSysUserDuty.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //用户表ID
            if (tSysUserDuty.USERID.Trim() == "")
            {
                this.Tips.AppendLine("用户表ID不能为空");
                return false;
            }
            //已关联岗位职责ID
            if (tSysUserDuty.DUTY_ID.Trim() == "")
            {
                this.Tips.AppendLine("已关联岗位职责ID不能为空");
                return false;
            }
            //是否默认负责人
            if (tSysUserDuty.IF_DEFAULT.Trim() == "")
            {
                this.Tips.AppendLine("是否默认负责人不能为空");
                return false;
            }
            //是否默认协同人
            if (tSysUserDuty.IF_DEFAULT_EX.Trim() == "")
            {
                this.Tips.AppendLine("是否默认协同人不能为空");
                return false;
            }
            //备注1
            if (tSysUserDuty.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysUserDuty.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysUserDuty.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tSysUserDuty.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tSysUserDuty.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }


        /// <summary>
        /// 更新默认负责人和协同人 Create By Castle(胡方扬) 2012-11-15
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="MonitorId">监测类别ID</param>
        /// <param name="MonitorItemId">监测项目ID</param>
        /// <param name="strDutyType">岗位职责类型</param>
        /// <param name="DefaultAuValue">是否默认负责人</param>
        /// <param name="DefaultExValue">是否默认协同人</param>
        /// <returns>返回值 true false</returns>
        public bool UpdateDefaultSet(string strUserId, string MonitorId, string[] MonitorItemId, string strDutyType, string DefaultAuValue, string DefaultExValue, bool isMonitorType)
        {
            return access.UpdateDefaultSet(strUserId, MonitorId, MonitorItemId, strDutyType, DefaultAuValue, DefaultExValue, isMonitorType);
        }

        /// <summary>
        /// 移除默认负责人和默认协同人 Create By Castle(胡方扬) 2012-11-15
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="MonitorId">监测类别ID</param>
        /// <param name="MonitorItemId">监测项目ID</param>
        /// <param name="strDutyType">岗位职责类型</param>
        /// <param name="MoveDefaultAu">移除默认负责人</param>
        /// <param name="MoveDefaultEx">移除默认协同人</param>
        /// <returns>返回值 true false</returns>
        public bool MoveDefaultSet(string strUserId, string MonitorId, string[] MonitorItemId, string strDutyType, bool MoveDefaultAu, bool MoveDefaultEx, bool isMonitorType)
        {
            return access.MoveDefaultSet(strUserId, MonitorId, MonitorItemId, strDutyType, MoveDefaultAu, MoveDefaultEx,isMonitorType);
        }

        /// <summary>
        /// 对象删除，删除移除的岗位职责用户关联的岗位职责  Create By Castle(胡方扬)  2012-11-14
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="MonitorId">监测类别ID</param>
        /// <param name="MonitorItemId">监测项目ID</param>
        /// <param name="strDutyType">岗位职责类型</param>
        /// <returns>是否成功</returns>
        public bool DeleteUserMonitorSet(string strUserId, string MonitorId, string[] MonitorItemId, string strDutyType, bool isMonitorType)
        {
            return access.DeleteUserMonitorSet(strUserId, MonitorId, MonitorItemId, strDutyType, isMonitorType);
        }
        /// <summary>
        /// 修改保存移除监测项目用户
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="MonitorId">监测项目ID</param>
        /// <param name="strDutyType">监测项目类别</param>
        /// <returns></returns>
        public bool DeleteUserMonitorSet(string[] strUserId, string MonitorId, string strDutyType)
        {
            return access.DeleteUserMonitorSet(strUserId, MonitorId, strDutyType);
        }

        public bool MoveDefaultSet(string[] strUserId, string MonitorId, string strDutyType, bool MoveDefaultAu, bool MoveDefaultEx)
        {
            return access.MoveDefaultSet(strUserId, MonitorId, strDutyType, MoveDefaultAu, MoveDefaultEx);
        }
        /// <summary>
        /// 获取采样用户和负责人协同人设置信息
        /// </summary>
        /// <param name="strMonitor"></param>
        /// <param name="strDutyType"></param>
        /// <returns></returns>
        public DataTable GetSamplingSetUser(string strMonitor, string strDutyType)
        {
            return access.GetSamplingSetUser(strMonitor, strDutyType);
        }
        /// <summary>
        /// 插入采样岗位资质
        /// </summary>
        /// <param name="strUserId"></param>
        /// <param name="DutyId"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public bool InsertSamplingUser( string[] strUserId, string DutyId, bool isDefault)
        {
            return access.InsertSamplingUser( strUserId, DutyId, isDefault);
        }
        /// <summary>
        /// 插入当前选择监测类别选择的用户
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="DutyId">岗位职责ID</param>
        /// <returns></returns>
        public bool InsertSelectedUser(string[] strUserId, string DutyId)
        {
            return access.InsertSelectedUser(strUserId,DutyId);
        }
        /// <summary>
        /// 对设置了默认负责人的监测项目，在重置默认负责人时，对默认负责人进行重置
        /// </summary>
        /// <param name="DutyId">岗位职责ID</param>
        /// <param name="isDefault">是否为默认负责人</param>
        /// <returns></returns>
        public bool SetDefaultUser(string DutyId, bool isDefault)
        {
            return access.SetDefaultUser(DutyId,isDefault);
        }
        /// <summary>
        /// 获取用户监测项目设置数据
        /// </summary>
        /// <param name="strDutyType">岗位类别</param>
        /// <param name="strMonitorId">监测项目</param>
        /// <returns></returns>
        public DataTable SelectUserDuty(string strDutyType, string strMonitorId)
        {
            return access.SelectUserDuty(strDutyType,strMonitorId);
        }

    }
}
