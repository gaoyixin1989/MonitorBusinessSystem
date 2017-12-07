using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;


using i3.ValueObject.Channels.Mis.ProcessMgm;
using i3.DataAccess.Channels.Mis.ProcessMgm;


namespace i3.BusinessLogic.Channels.Mis.ProcessMgm
{
    /// <summary>
    /// 功能：
    /// 创建日期：2015-09-02
    /// 创建人：
    /// </summary>
    public class TMisMonitorProcessMgmLogic : LogicBase
    {

        TMisMonitorProcessMgmVo tMisMonitorProcessMgm = new TMisMonitorProcessMgmVo();
        TMisMonitorProcessMgmAccess access;

        public TMisMonitorProcessMgmLogic()
        {
            access = new TMisMonitorProcessMgmAccess();
        }

        public TMisMonitorProcessMgmLogic(TMisMonitorProcessMgmVo _tMisMonitorProcessMgm)
        {
            tMisMonitorProcessMgm = _tMisMonitorProcessMgm;
            access = new TMisMonitorProcessMgmAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            return access.GetSelectResultCount(tMisMonitorProcessMgm);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorProcessMgmVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorProcessMgmVo Details(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            return access.Details(tMisMonitorProcessMgm);
        }

        public TMisMonitorProcessMgmVo DetailsByCCFlowIDAndType(string ccflowID, string mgmType)
        {
            return access.DetailsByCCFlowIDAndType(ccflowID, mgmType);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorProcessMgmVo> SelectByObject(TMisMonitorProcessMgmVo tMisMonitorProcessMgm, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorProcessMgm, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorProcessMgmVo tMisMonitorProcessMgm, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorProcessMgm, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorProcessMgm"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            return access.SelectByTable(tMisMonitorProcessMgm);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">对象</param>
        /// <returns></returns>
        public TMisMonitorProcessMgmVo SelectByObject(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            return access.SelectByObject(tMisMonitorProcessMgm);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            return access.Create(tMisMonitorProcessMgm);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorProcessMgm">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorProcessMgmVo tMisMonitorProcessMgm)
        {
            return access.Edit(tMisMonitorProcessMgm);
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
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where">条件，每个条件间要用and/or来连接
        /// <returns></returns>
        public void DeleteByWhere(string where)
        {
            access.DeleteByWhere(where);
        }


        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tMisMonitorProcessMgm.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorProcessMgm.TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorProcessMgm.MONITOR_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorProcessMgm.MONITOR_TIME_START.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorProcessMgm.MONITOR_TIME_FINISH.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 根据条件返回集合
        /// </summary>
        /// <param name="where">自己拼接的条件，第个条件要用and/or连接</param>
        /// <returns></returns>
        public List<TMisMonitorProcessMgmVo> SelectByObject(string where)
        {
            return access.SelectByObject(where);
        }

        /// <summary>
        /// 根据条件返回DataTable
        /// </summary>
        /// <param name="where">自己拼接的条件，第个条件要用and/or连接</param>
        /// <returns></returns>
        public DataTable SelectByTable(string where)
        {
            return access.SelectByTable(where);
        }

        public DataTable SelectByTable_One(string where)
        {
            return access.SelectByTable_One(where);
        }


        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="model">条件实体</param>
        /// <returns></returns>
        public List<TMisMonitorProcessMgmVo> SelectByList(TMisMonitorProcessMgmVo model)
        {
            return access.SelectByList(model);
        }

        // 新建委托任务的时候，无法确定PLAN_ID
        public void UpdatePlanIDOfContractTask(string strContractID, string strPlanID)
        {
            access.UpdatePlanIDOfContractTask(strContractID, strPlanID);
        }
    }
}
