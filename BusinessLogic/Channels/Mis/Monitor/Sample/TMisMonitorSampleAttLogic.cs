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
    /// 功能：采样原始数据附件表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleAttLogic : LogicBase
    {

	TMisMonitorSampleAttVo tMisMonitorSampleAtt = new TMisMonitorSampleAttVo();
    TMisMonitorSampleAttAccess access;
		
	public TMisMonitorSampleAttLogic()
	{
		  access = new TMisMonitorSampleAttAccess();  
	}
		
	public TMisMonitorSampleAttLogic(TMisMonitorSampleAttVo _tMisMonitorSampleAtt)
	{
		tMisMonitorSampleAtt = _tMisMonitorSampleAtt;
		access = new TMisMonitorSampleAttAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            return access.GetSelectResultCount(tMisMonitorSampleAtt);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleAttVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleAttVo Details(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            return access.Details(tMisMonitorSampleAtt);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleAttVo> SelectByObject(TMisMonitorSampleAttVo tMisMonitorSampleAtt, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorSampleAtt, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleAttVo tMisMonitorSampleAtt, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorSampleAtt, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleAtt"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            return access.SelectByTable(tMisMonitorSampleAtt);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleAttVo SelectByObject(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            return access.SelectByObject(tMisMonitorSampleAtt);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            return access.Create(tMisMonitorSampleAtt);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleAtt">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            return access.Edit(tMisMonitorSampleAtt);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleAtt_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorSampleAtt_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleAttVo tMisMonitorSampleAtt_UpdateSet, TMisMonitorSampleAttVo tMisMonitorSampleAtt_UpdateWhere)
        {
            return access.Edit(tMisMonitorSampleAtt_UpdateSet, tMisMonitorSampleAtt_UpdateWhere);
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
        public bool Delete(TMisMonitorSampleAttVo tMisMonitorSampleAtt)
        {
            return access.Delete(tMisMonitorSampleAtt);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisMonitorSampleAtt.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测子任务ID
	  if (tMisMonitorSampleAtt.SUBTASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测子任务ID不能为空");
                return false;
            }
	  //原始数据附件ID
	  if (tMisMonitorSampleAtt.ATTACH_ID.Trim() == "")
            {
                this.Tips.AppendLine("原始数据附件ID不能为空");
                return false;
            }
	  //备注1
	  if (tMisMonitorSampleAtt.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisMonitorSampleAtt.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisMonitorSampleAtt.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisMonitorSampleAtt.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisMonitorSampleAtt.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
