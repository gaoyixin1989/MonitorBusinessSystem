using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.DataAccess.Channels.Mis.Monitor.Sample;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：样品交接明细表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleHandsLogic : LogicBase
    {

	TMisMonitorSampleHandsVo tMisMonitorSampleHands = new TMisMonitorSampleHandsVo();
    TMisMonitorSampleHandsAccess access;
		
	public TMisMonitorSampleHandsLogic()
	{
		  access = new TMisMonitorSampleHandsAccess();  
	}
		
	public TMisMonitorSampleHandsLogic(TMisMonitorSampleHandsVo _tMisMonitorSampleHands)
	{
		tMisMonitorSampleHands = _tMisMonitorSampleHands;
		access = new TMisMonitorSampleHandsAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            return access.GetSelectResultCount(tMisMonitorSampleHands);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleHandsVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleHandsVo Details(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            return access.Details(tMisMonitorSampleHands);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleHandsVo> SelectByObject(TMisMonitorSampleHandsVo tMisMonitorSampleHands, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorSampleHands, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleHandsVo tMisMonitorSampleHands, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorSampleHands, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleHands"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            return access.SelectByTable(tMisMonitorSampleHands);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleHands">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleHandsVo SelectByObject(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            return access.SelectByObject(tMisMonitorSampleHands);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            return access.Create(tMisMonitorSampleHands);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleHands">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            return access.Edit(tMisMonitorSampleHands);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleHands_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorSampleHands_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleHandsVo tMisMonitorSampleHands_UpdateSet, TMisMonitorSampleHandsVo tMisMonitorSampleHands_UpdateWhere)
        {
            return access.Edit(tMisMonitorSampleHands_UpdateSet, tMisMonitorSampleHands_UpdateWhere);
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
        public bool Delete(TMisMonitorSampleHandsVo tMisMonitorSampleHands)
        {
            return access.Delete(tMisMonitorSampleHands);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisMonitorSampleHands.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测子任务ID
	  if (tMisMonitorSampleHands.HANDOVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测子任务ID不能为空");
                return false;
            }
	  //样品ID
	  if (tMisMonitorSampleHands.SAMPLE_ID.Trim() == "")
            {
                this.Tips.AppendLine("样品ID不能为空");
                return false;
            }
	  //样品数量
	  if (tMisMonitorSampleHands.SAMPLE_NUMBER.Trim() == "")
            {
                this.Tips.AppendLine("样品数量不能为空");
                return false;
            }
	  //是否移交
	  if (tMisMonitorSampleHands.IS_HANDOVER.Trim() == "")
            {
                this.Tips.AppendLine("是否移交不能为空");
                return false;
            }
	  //样品是否齐全完整
	  if (tMisMonitorSampleHands.IF_INTEGRITY.Trim() == "")
            {
                this.Tips.AppendLine("样品是否齐全完整不能为空");
                return false;
            }
	  //备注1
	  if (tMisMonitorSampleHands.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisMonitorSampleHands.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisMonitorSampleHands.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisMonitorSampleHands.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisMonitorSampleHands.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
