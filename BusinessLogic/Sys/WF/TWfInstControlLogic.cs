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
    /// 功能：工作流实例控制表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstControlLogic : LogicBase
    {

        TWfInstControlVo tWfInstControl = new TWfInstControlVo();
        TWfInstControlAccess access;

        public TWfInstControlLogic()
        {
            access = new TWfInstControlAccess();
        }

        public TWfInstControlLogic(TWfInstControlVo _tWfInstControl)
        {
            tWfInstControl = _tWfInstControl;
            access = new TWfInstControlAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstControlVo tWfInstControl)
        {
            return access.GetSelectResultCount(tWfInstControl);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstControlVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstControl">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstControlVo Details(TWfInstControlVo tWfInstControl)
        {
            return access.Details(tWfInstControl);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstControlVo> SelectByObject(TWfInstControlVo tWfInstControl, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfInstControl, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstControlVo tWfInstControl, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfInstControl, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable【主要用户销毁实例流程使用】
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableForClear(TWfInstControlVo tWfInstControl, int iIndex, int iCount)
        {
            return access.SelectByTableForClear(tWfInstControl, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstControl"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstControlVo tWfInstControl)
        {
            return access.SelectByTable(tWfInstControl);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstControl">对象</param>
        /// <returns></returns>
        public TWfInstControlVo SelectByObject(TWfInstControlVo tWfInstControl)
        {
            return access.SelectByObject(tWfInstControl);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstControlVo tWfInstControl)
        {
            return access.Create(tWfInstControl);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstControl">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstControlVo tWfInstControl)
        {
            return access.Edit(tWfInstControl);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstControl_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstControl_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstControlVo tWfInstControl_UpdateSet, TWfInstControlVo tWfInstControl_UpdateWhere)
        {
            return access.Edit(tWfInstControl_UpdateSet, tWfInstControl_UpdateWhere);
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
        public bool Delete(TWfInstControlVo tWfInstControl)
        {
            return access.Delete(tWfInstControl);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //流程实例编号
            if (tWfInstControl.ID.Trim() == "")
            {
                this.Tips.AppendLine("流程实例编号不能为空");
                return false;
            }
            //流程编号
            if (tWfInstControl.WF_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程编号不能为空");
                return false;
            }
            //流水号
            if (tWfInstControl.WF_SERIAL_NO.Trim() == "")
            {
                this.Tips.AppendLine("流水号不能为空");
                return false;
            }
            //当前环节编号
            if (tWfInstControl.WF_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("当前环节编号不能为空");
                return false;
            }
            //流程简述
            if (tWfInstControl.WF_CAPTION.Trim() == "")
            {
                this.Tips.AppendLine("流程简述不能为空");
                return false;
            }
            //流程备注
            if (tWfInstControl.WF_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("流程备注不能为空");
                return false;
            }
            //优先级
            if (tWfInstControl.WF_PRIORITY.Trim() == "")
            {
                this.Tips.AppendLine("优先级不能为空");
                return false;
            }
            //流程状态
            if (tWfInstControl.WF_STATE.Trim() == "")
            {
                this.Tips.AppendLine("流程状态不能为空");
                return false;
            }
            //开始时间
            if (tWfInstControl.WF_STARTTIME.Trim() == "")
            {
                this.Tips.AppendLine("开始时间不能为空");
                return false;
            }
            //约定结束时间
            if (tWfInstControl.WF_ENDTIME.Trim() == "")
            {
                this.Tips.AppendLine("约定结束时间不能为空");
                return false;
            }
            //挂起时间
            if (tWfInstControl.WF_SUSPEND_TIME.Trim() == "")
            {
                this.Tips.AppendLine("挂起时间不能为空");
                return false;
            }
            //挂起状态
            if (tWfInstControl.WF_SUSPEND_STATE.Trim() == "")
            {
                this.Tips.AppendLine("挂起状态不能为空");
                return false;
            }
            //挂起的结束时间
            if (tWfInstControl.WF_SUSPEND_ENDTIME.Trim() == "")
            {
                this.Tips.AppendLine("挂起的结束时间不能为空");
                return false;
            }
            //是否子流程
            if (tWfInstControl.IS_SUB_FLOW.Trim() == "")
            {
                this.Tips.AppendLine("是否子流程不能为空");
                return false;
            }
            //父流程实例编号
            if (tWfInstControl.PARENT_INST_FLOW_ID.Trim() == "")
            {
                this.Tips.AppendLine("父流程实例编号不能为空");
                return false;
            }
            //父流程编号
            if (tWfInstControl.PARENT_FLOW_ID.Trim() == "")
            {
                this.Tips.AppendLine("父流程编号不能为空");
                return false;
            }
            //父流程环节实例编号
            if (tWfInstControl.PARENT_INST_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("父流程环节实例编号不能为空");
                return false;
            }
            //父流程环节编号
            if (tWfInstControl.PARENT_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("父流程环节编号不能为空");
                return false;
            }
            //其他备注
            if (tWfInstControl.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("其他备注不能为空");
                return false;
            }

            return true;
        }

    }
}
