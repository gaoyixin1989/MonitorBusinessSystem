using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.DataAccess.Channels.Mis.Monitor.Task;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务点位信息表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskPointLogic : LogicBase
    {

        TMisMonitorTaskPointVo tMisMonitorTaskPoint = new TMisMonitorTaskPointVo();
        TMisMonitorTaskPointAccess access;

        public TMisMonitorTaskPointLogic()
        {
            access = new TMisMonitorTaskPointAccess();
        }

        public TMisMonitorTaskPointLogic(TMisMonitorTaskPointVo _tMisMonitorTaskPoint)
        {
            tMisMonitorTaskPoint = _tMisMonitorTaskPoint;
            access = new TMisMonitorTaskPointAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            return access.GetSelectResultCount(tMisMonitorTaskPoint);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskPointVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskPointVo Details(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            return access.Details(tMisMonitorTaskPoint);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskPointVo> SelectByObject(TMisMonitorTaskPointVo tMisMonitorTaskPoint, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorTaskPoint, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskPointVo tMisMonitorTaskPoint, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorTaskPoint, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTaskPoint"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            return access.SelectByTable(tMisMonitorTaskPoint);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskPointVo SelectByObject(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            return access.SelectByObject(tMisMonitorTaskPoint);
        }

        /// <summary>
        /// 根据监测子任务ID获取包含现场项目的点位信息
        /// </summary>
        /// <param name="strSubtaskID">子任务ID</param>
        /// <param name="ItemCondition">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptPoint(string strSubtaskID, string ItemCondition = "1", IList<string> sampleIdList = null)
        {
            return access.SelectSampleDeptPoint(strSubtaskID, ItemCondition,sampleIdList:sampleIdList);
        }

        /// <summary>
        /// 根据监测子任务ID获取包含现场项目的点位信息
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">对象</param>
        /// <returns></returns>
        public DataTable SelectSampleDeptPoint_QHD(string strSubtaskID)
        {
            return access.SelectSampleDeptPoint_QHD(strSubtaskID);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            return access.Create(tMisMonitorTaskPoint);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskPoint">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            return access.Edit(tMisMonitorTaskPoint);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskPoint_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorTaskPoint_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskPointVo tMisMonitorTaskPoint_UpdateSet, TMisMonitorTaskPointVo tMisMonitorTaskPoint_UpdateWhere)
        {
            return access.Edit(tMisMonitorTaskPoint_UpdateSet, tMisMonitorTaskPoint_UpdateWhere);
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
        public bool Delete(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            return access.Delete(tMisMonitorTaskPoint);
        }

        /// <summary>
        /// 功能描述：获取任务点位信息
        /// 创建时间：2012-12-5
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns></returns>
        public DataTable SelectByTableByTaskID(string strTaskID, string strItemType)
        {
            return access.SelectByTableByTaskID(strTaskID, strItemType);
        }

        /// <summary>
        /// 功能描述：获取任务点位信息（秦皇岛许可证）
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <param name="strItemType">监测类别</param>
        /// <returns></returns>
        public DataTable SelectByTableByTaskIDForLicense(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            return access.SelectByTableByTaskIDForLicense(tMisMonitorTaskPoint);
        }

        /// <summary>
        /// 功能描述：获取任务点位信息
        /// 创建时间：2012-12-5
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns></returns>
        public DataTable SelectAllQcTypePoint(string strTaskID, string strItemType)
        {
            return access.SelectAllQcTypePoint(strTaskID, strItemType);
        }

        /// <summary>
        /// 功能描述：获取点位项目信息
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorTaskPoint"></param>
        /// <returns></returns>
        public DataTable SelectTaskItemByPoint(TMisMonitorTaskPointVo tMisMonitorTaskPoint)
        {
            return access.SelectTaskItemByPoint(tMisMonitorTaskPoint);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorTaskPoint.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //任务ID
            if (tMisMonitorTaskPoint.TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("任务ID不能为空");
                return false;
            }
            //监测子任务ID
            if (tMisMonitorTaskPoint.SUBTASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测子任务ID不能为空");
                return false;
            }
            //所属企业ID
            if (tMisMonitorTaskPoint.COMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属企业ID不能为空");
                return false;
            }
            //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
            if (tMisMonitorTaskPoint.MONITOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）不能为空");
                return false;
            }
            //基础资料监测点ID
            if (tMisMonitorTaskPoint.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("基础资料监测点ID不能为空");
                return false;
            }
            //委托书监测点ID
            if (tMisMonitorTaskPoint.CONTRACT_POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书监测点ID不能为空");
                return false;
            }
            //监测点名称
            if (tMisMonitorTaskPoint.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("监测点名称不能为空");
                return false;
            }

            //动态属性ID,从静态数据表拷贝设备信息，必须拷贝动态属性信息
            if (tMisMonitorTaskPoint.DYNAMIC_ATTRIBUTE_ID.Trim() == "")
            {
                this.Tips.AppendLine("动态属性ID,从静态数据表拷贝设备信息，必须拷贝动态属性信息不能为空");
                return false;
            }
            //建成时间
            if (tMisMonitorTaskPoint.CREATE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("建成时间不能为空");
                return false;
            }
            //监测点位置
            if (tMisMonitorTaskPoint.ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("监测点位置不能为空");
                return false;
            }
            //经度
            if (tMisMonitorTaskPoint.LONGITUDE.Trim() == "")
            {
                this.Tips.AppendLine("经度不能为空");
                return false;
            }
            //纬度
            if (tMisMonitorTaskPoint.LATITUDE.Trim() == "")
            {
                this.Tips.AppendLine("纬度不能为空");
                return false;
            }
            //监测频次
            if (tMisMonitorTaskPoint.FREQ.Trim() == "")
            {
                this.Tips.AppendLine("监测频次不能为空");
                return false;
            }
            //点位描述
            if (tMisMonitorTaskPoint.DESCRIPTION.Trim() == "")
            {
                this.Tips.AppendLine("点位描述不能为空");
                return false;
            }
            //国标条件项
            if (tMisMonitorTaskPoint.NATIONAL_ST_CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("国标条件项不能为空");
                return false;
            }
            //行标条件项ID
            if (tMisMonitorTaskPoint.INDUSTRY_ST_CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("行标条件项ID不能为空");
                return false;
            }
            //地标条件项_ID
            if (tMisMonitorTaskPoint.LOCAL_ST_CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("地标条件项_ID不能为空");
                return false;
            }
            //是否删除
            if (tMisMonitorTaskPoint.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("是否删除不能为空");
                return false;
            }
            //序号
            if (tMisMonitorTaskPoint.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorTaskPoint.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorTaskPoint.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorTaskPoint.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorTaskPoint.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorTaskPoint.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }



    }
}
