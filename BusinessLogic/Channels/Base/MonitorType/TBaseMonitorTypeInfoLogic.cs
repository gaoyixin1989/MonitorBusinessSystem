using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.MonitorType;
using i3.DataAccess.Channels.Base.MonitorType;

namespace i3.BusinessLogic.Channels.Base.MonitorType
{
    /// <summary>
    /// 功能：监测类别管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseMonitorTypeInfoLogic : LogicBase
    {

        TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo = new TBaseMonitorTypeInfoVo();
        TBaseMonitorTypeInfoAccess access;

        public TBaseMonitorTypeInfoLogic()
        {
            access = new TBaseMonitorTypeInfoAccess();
        }

        public TBaseMonitorTypeInfoLogic(TBaseMonitorTypeInfoVo _tBaseMonitorTypeInfo)
        {
            tBaseMonitorTypeInfo = _tBaseMonitorTypeInfo;
            access = new TBaseMonitorTypeInfoAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            return access.GetSelectResultCount(tBaseMonitorTypeInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseMonitorTypeInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseMonitorTypeInfoVo Details(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            return access.Details(tBaseMonitorTypeInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseMonitorTypeInfoVo> SelectByObject(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseMonitorTypeInfo, iIndex, iCount);

        }

        /// <summary>
        /// select通用查询函数
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strWhere">where语句</param>
        /// <param name="strIdfield">id 列名</param>
        /// <param name="strTextField">Text 列名</param>
        /// <param name="strDistinct">是否distinct</param>
        /// <returns></returns>
        public DataTable SelectByTable(string strTableName, string strWhere, string strIdfield, string strTextField, string strDistinct, string strOrder)
        {
            return access.SelectByTable(strTableName, strWhere, strIdfield, strTextField, strDistinct, strOrder);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseMonitorTypeInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            return access.SelectByTable(tBaseMonitorTypeInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">对象</param>
        /// <returns></returns>
        public TBaseMonitorTypeInfoVo SelectByObject(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            return access.SelectByObject(tBaseMonitorTypeInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            return access.Create(tBaseMonitorTypeInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            return access.Edit(tBaseMonitorTypeInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMonitorTypeInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseMonitorTypeInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo_UpdateSet, TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo_UpdateWhere)
        {
            return access.Edit(tBaseMonitorTypeInfo_UpdateSet, tBaseMonitorTypeInfo_UpdateWhere);
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
        public bool Delete(TBaseMonitorTypeInfoVo tBaseMonitorTypeInfo)
        {
            return access.Delete(tBaseMonitorTypeInfo);
        }

        /// <summary>
        /// 功能描述：获得任务所监测类别(List)
        /// 创建时间：2012-12-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemTypeByTask(string strTaskID)
        {
            return access.getItemTypeByTask(strTaskID);
        }
        // <summary>
        /// 功能描述：获取监测类别环境质量除外
        /// 创建时间：2014-11-21
        /// 创建人：魏林
        /// </summary>
        /// <returns>数据集</returns>
        public DataTable getMonitorType()
        {
            return access.getMonitorType();
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tBaseMonitorTypeInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //监测类别名称
            if (tBaseMonitorTypeInfo.MONITOR_TYPE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("监测类别名称不能为空");
                return false;
            }
            //监测类别描述
            if (tBaseMonitorTypeInfo.DESCRIPTION.Trim() == "")
            {
                this.Tips.AppendLine("监测类别描述不能为空");
                return false;
            }
            //使用状态(0为启用、1为停用)
            if (tBaseMonitorTypeInfo.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
            //备注1
            if (tBaseMonitorTypeInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tBaseMonitorTypeInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tBaseMonitorTypeInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tBaseMonitorTypeInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tBaseMonitorTypeInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
