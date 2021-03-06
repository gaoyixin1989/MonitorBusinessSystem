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
    /// 功能：监测任务点位项目明细表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskItemLogic : LogicBase
    {

        TMisMonitorTaskItemVo tMisMonitorTaskItem = new TMisMonitorTaskItemVo();
        TMisMonitorTaskItemAccess access;

        public TMisMonitorTaskItemLogic()
        {
            access = new TMisMonitorTaskItemAccess();
        }

        public TMisMonitorTaskItemLogic(TMisMonitorTaskItemVo _tMisMonitorTaskItem)
        {
            tMisMonitorTaskItem = _tMisMonitorTaskItem;
            access = new TMisMonitorTaskItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            return access.GetSelectResultCount(tMisMonitorTaskItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskItemVo Details(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            return access.Details(tMisMonitorTaskItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskItemVo> SelectByObject(TMisMonitorTaskItemVo tMisMonitorTaskItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorTaskItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskItemVo tMisMonitorTaskItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorTaskItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTaskItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            return access.SelectByTable(tMisMonitorTaskItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskItemVo SelectByObject(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            return access.SelectByObject(tMisMonitorTaskItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            return access.Create(tMisMonitorTaskItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            return access.Edit(tMisMonitorTaskItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorTaskItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskItemVo tMisMonitorTaskItem_UpdateSet, TMisMonitorTaskItemVo tMisMonitorTaskItem_UpdateWhere)
        {
            return access.Edit(tMisMonitorTaskItem_UpdateSet, tMisMonitorTaskItem_UpdateWhere);
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
        public bool Delete(TMisMonitorTaskItemVo tMisMonitorTaskItem)
        {
            return access.Delete(tMisMonitorTaskItem);
        }

        /// <summary>
        /// 功能描述：获得查询结果总行数，用于分页（报告编制）
        /// 创建时间：2012-12-6
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountForDetail(string strPointID)
        {
            return access.GetSelectResultCountForDetail(strPointID);
        }

        /// <summary>
        /// 功能描述：获取对象DataTable（报告编制）
        /// 创建时间：2012-12-6
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableForDetail(string strPointID, int iIndex, int iCount)
        {
            return access.SelectByTableForDetail(strPointID, iIndex, iCount);
        }

        /// <summary>
        /// 功能描述：获取项目详细信息
        /// 创建时间：2012-12-12
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strContractID">委托书ID</param>
        /// <returns>数据集</returns>
        public DataTable SelectByTableByContractID(string strContractID, string mItemTypeID)
        {
            return access.SelectByTableByContractID(strContractID, mItemTypeID);
        }

        /// <summary>
        /// 功能描述：取得样品对应的样品ID，样品号，项目名称，下限，上限，标准名，标准号
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strContractID">监测任务ID</param>
        /// <returns></returns>
        public DataTable SelectStandard_byTask(string strTaskID, string strItemTypeID)
        {
            return access.SelectStandard_byTask(strTaskID, strItemTypeID);
        }

        #region 手机版接口，请勿修改
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectResult_ByTable_forMobile(string strBasePointIdS, string strItemIDs, string strBeginDate, string strEndDate)
        {
            return access.SelectResult_ByTable_forMobile(strBasePointIdS, strItemIDs, strBeginDate, strEndDate);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_forMobile(string strBasePointIdS, int iIndex, int iCount)
        {
            return access.SelectByTable_forMobile(strBasePointIdS, iIndex, iCount);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_forMobile(string strBasePointIdS)
        {
            return access.GetSelectResultCount_forMobile(strBasePointIdS);
        }

        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectSQL_ByTable_forMobile(string strSQL, int iIndex, int iCount)
        {
            return access.SelectSQL_ByTable_forMobile(strSQL, iIndex, iCount);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectSQL_ResultCount_forMobile(string strSQL)
        {

            return access.GetSelectSQL_ResultCount_forMobile(strSQL);
        }
        #endregion

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorTaskItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //合同监测点ID
            if (tMisMonitorTaskItem.TASK_POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("合同监测点ID不能为空");
                return false;
            }
            //监测项目ID
            if (tMisMonitorTaskItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //已选条件项ID
            if (tMisMonitorTaskItem.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("已选条件项ID不能为空");
                return false;
            }
            //条件项类型（1，国标；2，行标；3，地标）
            if (tMisMonitorTaskItem.CONDITION_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("条件项类型（1，国标；2，行标；3，地标）不能为空");
                return false;
            }
            //国标上限
            if (tMisMonitorTaskItem.ST_UPPER.Trim() == "")
            {
                this.Tips.AppendLine("国标上限不能为空");
                return false;
            }
            //国标下限
            if (tMisMonitorTaskItem.ST_LOWER.Trim() == "")
            {
                this.Tips.AppendLine("国标下限不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorTaskItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorTaskItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorTaskItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorTaskItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorTaskItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
