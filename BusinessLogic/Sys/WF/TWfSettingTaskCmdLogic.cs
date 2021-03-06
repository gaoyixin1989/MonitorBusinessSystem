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
    /// 功能：流程节点命令集合
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingTaskCmdLogic : LogicBase
    {

        TWfSettingTaskCmdVo tWfSettingTaskCmd = new TWfSettingTaskCmdVo();
        TWfSettingTaskCmdAccess access;

        public TWfSettingTaskCmdLogic()
        {
            access = new TWfSettingTaskCmdAccess();
        }

        public TWfSettingTaskCmdLogic(TWfSettingTaskCmdVo _tWfSettingTaskCmd)
        {
            tWfSettingTaskCmd = _tWfSettingTaskCmd;
            access = new TWfSettingTaskCmdAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            return access.GetSelectResultCount(tWfSettingTaskCmd);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingTaskCmdVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingTaskCmdVo Details(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            return access.Details(tWfSettingTaskCmd);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskCmdVo> SelectByObject(TWfSettingTaskCmdVo tWfSettingTaskCmd, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfSettingTaskCmd, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingTaskCmdVo tWfSettingTaskCmd, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfSettingTaskCmd, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingTaskCmd"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            return access.SelectByTable(tWfSettingTaskCmd);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingTaskCmd">对象</param>
        /// <returns></returns>
        public TWfSettingTaskCmdVo SelectByObject(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            return access.SelectByObject(tWfSettingTaskCmd);
        }

        /// <summary>
        /// List对象添加 
        /// </summary>
        /// <param name="tWfSettingTaskCmd">List对象</param>
        /// <returns>是否成功</returns>
        public bool Create(List<TWfSettingTaskCmdVo> tWfSettingTaskCmdList)
        {
            return access.Create(tWfSettingTaskCmdList);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            return access.Create(tWfSettingTaskCmd);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskCmd">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            return access.Edit(tWfSettingTaskCmd);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskCmd_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingTaskCmd_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskCmdVo tWfSettingTaskCmd_UpdateSet, TWfSettingTaskCmdVo tWfSettingTaskCmd_UpdateWhere)
        {
            return access.Edit(tWfSettingTaskCmd_UpdateSet, tWfSettingTaskCmd_UpdateWhere);
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
        public bool Delete(TWfSettingTaskCmdVo tWfSettingTaskCmd)
        {
            return access.Delete(tWfSettingTaskCmd);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfSettingTaskCmd.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //命令编号
            if (tWfSettingTaskCmd.WF_CMD_ID.Trim() == "")
            {
                this.Tips.AppendLine("命令编号不能为空");
                return false;
            }
            //流程编号
            if (tWfSettingTaskCmd.WF_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程编号不能为空");
                return false;
            }
            //节点编号
            if (tWfSettingTaskCmd.WF_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("节点编号不能为空");
                return false;
            }
            //命令名称
            if (tWfSettingTaskCmd.CMD_NAME.Trim() == "")
            {
                this.Tips.AppendLine("命令名称不能为空");
                return false;
            }
            //命令描述
            if (tWfSettingTaskCmd.CMD_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("命令描述不能为空");
                return false;
            }

            return true;
        }

    }
}
