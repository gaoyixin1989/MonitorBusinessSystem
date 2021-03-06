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
    /// 功能：流程操作记录表
    /// 创建日期：2012-11-07
    /// 创建人：石磊
    /// </summary>
    public class TWfInstOpLogLogic : LogicBase
    {

        TWfInstOpLogVo tWfInstOpLog = new TWfInstOpLogVo();
        TWfInstOpLogAccess access;

        public TWfInstOpLogLogic()
        {
            access = new TWfInstOpLogAccess();
        }

        public TWfInstOpLogLogic(TWfInstOpLogVo _tWfInstOpLog)
        {
            tWfInstOpLog = _tWfInstOpLog;
            access = new TWfInstOpLogAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstOpLog">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstOpLogVo tWfInstOpLog)
        {
            return access.GetSelectResultCount(tWfInstOpLog);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstOpLogVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstOpLog">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstOpLogVo Details(TWfInstOpLogVo tWfInstOpLog)
        {
            return access.Details(tWfInstOpLog);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstOpLog">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstOpLogVo> SelectByObject(TWfInstOpLogVo tWfInstOpLog, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfInstOpLog, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstOpLog">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstOpLogVo tWfInstOpLog, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfInstOpLog, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstOpLog"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstOpLogVo tWfInstOpLog)
        {
            return access.SelectByTable(tWfInstOpLog);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstOpLog">对象</param>
        /// <returns></returns>
        public TWfInstOpLogVo SelectByObject(TWfInstOpLogVo tWfInstOpLog)
        {
            return access.SelectByObject(tWfInstOpLog);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstOpLogVo tWfInstOpLog)
        {
            return access.Create(tWfInstOpLog);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstOpLog">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstOpLogVo tWfInstOpLog)
        {
            return access.Edit(tWfInstOpLog);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstOpLog_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstOpLog_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstOpLogVo tWfInstOpLog_UpdateSet, TWfInstOpLogVo tWfInstOpLog_UpdateWhere)
        {
            return access.Edit(tWfInstOpLog_UpdateSet, tWfInstOpLog_UpdateWhere);
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
        public bool Delete(TWfInstOpLogVo tWfInstOpLog)
        {
            return access.Delete(tWfInstOpLog);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfInstOpLog.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //流程实例编号
            if (tWfInstOpLog.WF_INST_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程实例编号不能为空");
                return false;
            }
            //环节实例编号
            if (tWfInstOpLog.WF_INST_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("环节实例编号不能为空");
                return false;
            }
            //操作用户
            if (tWfInstOpLog.OP_USER.Trim() == "")
            {
                this.Tips.AppendLine("操作用户不能为空");
                return false;
            }
            //操作动作
            if (tWfInstOpLog.OP_ACTION.Trim() == "")
            {
                this.Tips.AppendLine("操作动作不能为空");
                return false;
            }
            //流程简码
            if (tWfInstOpLog.WF_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程简码不能为空");
                return false;
            }
            //环节简码
            if (tWfInstOpLog.WF_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("环节简码不能为空");
                return false;
            }
            //操作时间
            if (tWfInstOpLog.OP_TIME.Trim() == "")
            {
                this.Tips.AppendLine("操作时间不能为空");
                return false;
            }
            //操作描述
            if (tWfInstOpLog.OP_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("操作描述不能为空");
                return false;
            }
            //是否代理
            if (tWfInstOpLog.IS_AGENT.Trim() == "")
            {
                this.Tips.AppendLine("是否代理不能为空");
                return false;
            }
            //被代理人
            if (tWfInstOpLog.AGENT_USER.Trim() == "")
            {
                this.Tips.AppendLine("被代理人不能为空");
                return false;
            }

            return true;
        }

    }
}
