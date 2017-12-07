using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.WF;
using i3.DataAccess.Sys.WF;

namespace i3.BusinessLogic.Sys.WF
{
    /// <summary>
    /// 功能：纸质数据审核记录
    /// 创建日期：2013-05-03
    /// 创建人：潘德军
    /// </summary>
    public class TWfSettingTaskHasappLogic : LogicBase
    {

        TWfSettingTaskHasappVo tWfSettingTaskHasapp = new TWfSettingTaskHasappVo();
        TWfSettingTaskHasappAccess access;

        public TWfSettingTaskHasappLogic()
        {
            access = new TWfSettingTaskHasappAccess();
        }

        public TWfSettingTaskHasappLogic(TWfSettingTaskHasappVo _tWfSettingTaskHasapp)
        {
            tWfSettingTaskHasapp = _tWfSettingTaskHasapp;
            access = new TWfSettingTaskHasappAccess();
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForApp(string strUserId)
        {
            return access.GetSelectResultCount_ForApp(strUserId);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForApp(string strUserId, int iIndex, int iCount)
        {
            return access.SelectByTable_ForApp(strUserId, iIndex, iCount);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            return access.GetSelectResultCount(tWfSettingTaskHasapp);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingTaskHasappVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingTaskHasappVo Details(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            return access.Details(tWfSettingTaskHasapp);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskHasappVo> SelectByObject(TWfSettingTaskHasappVo tWfSettingTaskHasapp, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfSettingTaskHasapp, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingTaskHasappVo tWfSettingTaskHasapp, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfSettingTaskHasapp, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingTaskHasapp"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            return access.SelectByTable(tWfSettingTaskHasapp);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">对象</param>
        /// <returns></returns>
        public TWfSettingTaskHasappVo SelectByObject(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            return access.SelectByObject(tWfSettingTaskHasapp);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            return access.Create(tWfSettingTaskHasapp);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskHasapp">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            return access.Edit(tWfSettingTaskHasapp);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskHasapp_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingTaskHasapp_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskHasappVo tWfSettingTaskHasapp_UpdateSet, TWfSettingTaskHasappVo tWfSettingTaskHasapp_UpdateWhere)
        {
            return access.Edit(tWfSettingTaskHasapp_UpdateSet, tWfSettingTaskHasapp_UpdateWhere);
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
        public bool Delete(TWfSettingTaskHasappVo tWfSettingTaskHasapp)
        {
            return access.Delete(tWfSettingTaskHasapp);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //Id
            if (tWfSettingTaskHasapp.ID.Trim() == "")
            {
                this.Tips.AppendLine("Id不能为空");
                return false;
            }
            //环节ID
            if (tWfSettingTaskHasapp.WF_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("环节ID不能为空");
                return false;
            }
            //环节名
            if (tWfSettingTaskHasapp.WF_TASK_NAME.Trim() == "")
            {
                this.Tips.AppendLine("环节名不能为空");
                return false;
            }
            //流程ID或者流程类别（监测分析用流程类别）
            if (tWfSettingTaskHasapp.WF_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程ID或者流程类别（监测分析用流程类别）不能为空");
                return false;
            }
            //实例ID
            if (tWfSettingTaskHasapp.TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("实例ID不能为空");
                return false;
            }
            //是否已审核
            if (tWfSettingTaskHasapp.HAS_APP.Trim() == "")
            {
                this.Tips.AppendLine("是否已审核不能为空");
                return false;
            }

            return true;
        }

    }
}
