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
    /// 功能：样品交接表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleHandLogic : LogicBase
    {

	TMisMonitorSampleHandVo tMisMonitorSampleHand = new TMisMonitorSampleHandVo();
    TMisMonitorSampleHandAccess access;
		
	public TMisMonitorSampleHandLogic()
	{
		  access = new TMisMonitorSampleHandAccess();  
	}
		
	public TMisMonitorSampleHandLogic(TMisMonitorSampleHandVo _tMisMonitorSampleHand)
	{
		tMisMonitorSampleHand = _tMisMonitorSampleHand;
		access = new TMisMonitorSampleHandAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            return access.GetSelectResultCount(tMisMonitorSampleHand);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleHandVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleHandVo Details(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            return access.Details(tMisMonitorSampleHand);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleHandVo> SelectByObject(TMisMonitorSampleHandVo tMisMonitorSampleHand, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorSampleHand, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleHandVo tMisMonitorSampleHand, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorSampleHand, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleHand"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            return access.SelectByTable(tMisMonitorSampleHand);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleHand">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleHandVo SelectByObject(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            return access.SelectByObject(tMisMonitorSampleHand);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            return access.Create(tMisMonitorSampleHand);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleHand">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            return access.Edit(tMisMonitorSampleHand);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleHand_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorSampleHand_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleHandVo tMisMonitorSampleHand_UpdateSet, TMisMonitorSampleHandVo tMisMonitorSampleHand_UpdateWhere)
        {
            return access.Edit(tMisMonitorSampleHand_UpdateSet, tMisMonitorSampleHand_UpdateWhere);
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
        public bool Delete(TMisMonitorSampleHandVo tMisMonitorSampleHand)
        {
            return access.Delete(tMisMonitorSampleHand);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisMonitorSampleHand.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测子任务ID
	  if (tMisMonitorSampleHand.SUBTASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测子任务ID不能为空");
                return false;
            }
	  //交接单编号
	  if (tMisMonitorSampleHand.HANDOVER_NO.Trim() == "")
            {
                this.Tips.AppendLine("交接单编号不能为空");
                return false;
            }
	  //编号类型
	  if (tMisMonitorSampleHand.NO_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("编号类型不能为空");
                return false;
            }
	  //普通加急
	  if (tMisMonitorSampleHand.IF_COMMON.Trim() == "")
            {
                this.Tips.AppendLine("普通加急不能为空");
                return false;
            }
	  //样品类型
	  if (tMisMonitorSampleHand.SAMPLE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("样品类型不能为空");
                return false;
            }
	  //单据类型编号
	  if (tMisMonitorSampleHand.TAB_TYPE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("单据类型编号不能为空");
                return false;
            }
	  //是否移交
	  if (tMisMonitorSampleHand.IS_HANDOVER.Trim() == "")
            {
                this.Tips.AppendLine("是否移交不能为空");
                return false;
            }
	  //备注1
	  if (tMisMonitorSampleHand.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisMonitorSampleHand.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisMonitorSampleHand.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisMonitorSampleHand.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisMonitorSampleHand.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
