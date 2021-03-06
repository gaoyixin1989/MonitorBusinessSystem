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
    /// 功能：监测任务外包单位表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskOutLogic : LogicBase
    {

	TMisMonitorTaskOutVo tMisMonitorTaskOut = new TMisMonitorTaskOutVo();
    TMisMonitorTaskOutAccess access;
		
	public TMisMonitorTaskOutLogic()
	{
		  access = new TMisMonitorTaskOutAccess();  
	}
		
	public TMisMonitorTaskOutLogic(TMisMonitorTaskOutVo _tMisMonitorTaskOut)
	{
		tMisMonitorTaskOut = _tMisMonitorTaskOut;
		access = new TMisMonitorTaskOutAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            return access.GetSelectResultCount(tMisMonitorTaskOut);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskOutVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskOutVo Details(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            return access.Details(tMisMonitorTaskOut);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskOutVo> SelectByObject(TMisMonitorTaskOutVo tMisMonitorTaskOut, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorTaskOut, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskOutVo tMisMonitorTaskOut, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorTaskOut, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTaskOut"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            return access.SelectByTable(tMisMonitorTaskOut);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTaskOut">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskOutVo SelectByObject(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            return access.SelectByObject(tMisMonitorTaskOut);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            return access.Create(tMisMonitorTaskOut);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskOut">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            return access.Edit(tMisMonitorTaskOut);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskOut_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorTaskOut_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskOutVo tMisMonitorTaskOut_UpdateSet, TMisMonitorTaskOutVo tMisMonitorTaskOut_UpdateWhere)
        {
            return access.Edit(tMisMonitorTaskOut_UpdateSet, tMisMonitorTaskOut_UpdateWhere);
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
        public bool Delete(TMisMonitorTaskOutVo tMisMonitorTaskOut)
        {
            return access.Delete(tMisMonitorTaskOut);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisMonitorTaskOut.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测计划ID
	  if (tMisMonitorTaskOut.TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测计划ID不能为空");
                return false;
            }
	  //外包ID
	  if (tMisMonitorTaskOut.OUTCOMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("外包ID不能为空");
                return false;
            }
	  //备注
	  if (tMisMonitorTaskOut.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }

            return true;
        }

    }
}
