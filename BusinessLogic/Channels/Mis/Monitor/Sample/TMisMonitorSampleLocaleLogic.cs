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
    /// 功能：现场信息表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleLocaleLogic : LogicBase
    {

	TMisMonitorSampleLocaleVo tMisMonitorSampleLocale = new TMisMonitorSampleLocaleVo();
    TMisMonitorSampleLocaleAccess access;
		
	public TMisMonitorSampleLocaleLogic()
	{
		  access = new TMisMonitorSampleLocaleAccess();  
	}
		
	public TMisMonitorSampleLocaleLogic(TMisMonitorSampleLocaleVo _tMisMonitorSampleLocale)
	{
		tMisMonitorSampleLocale = _tMisMonitorSampleLocale;
		access = new TMisMonitorSampleLocaleAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            return access.GetSelectResultCount(tMisMonitorSampleLocale);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleLocaleVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleLocaleVo Details(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            return access.Details(tMisMonitorSampleLocale);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleLocaleVo> SelectByObject(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorSampleLocale, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorSampleLocale, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleLocale"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            return access.SelectByTable(tMisMonitorSampleLocale);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleLocaleVo SelectByObject(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            return access.SelectByObject(tMisMonitorSampleLocale);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            return access.Create(tMisMonitorSampleLocale);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleLocale">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            return access.Edit(tMisMonitorSampleLocale);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleLocale_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorSampleLocale_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale_UpdateSet, TMisMonitorSampleLocaleVo tMisMonitorSampleLocale_UpdateWhere)
        {
            return access.Edit(tMisMonitorSampleLocale_UpdateSet, tMisMonitorSampleLocale_UpdateWhere);
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
        public bool Delete(TMisMonitorSampleLocaleVo tMisMonitorSampleLocale)
        {
            return access.Delete(tMisMonitorSampleLocale);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisMonitorSampleLocale.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测子任务ID
	  if (tMisMonitorSampleLocale.SUBTASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测子任务ID不能为空");
                return false;
            }
	  //企业工况
	  if (tMisMonitorSampleLocale.WORK_CONDITION.Trim() == "")
            {
                this.Tips.AppendLine("企业工况不能为空");
                return false;
            }
	  //环保设施运行情况
	  if (tMisMonitorSampleLocale.ENVT_EQUT_STATUS.Trim() == "")
            {
                this.Tips.AppendLine("环保设施运行情况不能为空");
                return false;
            }
	  //备注1
	  if (tMisMonitorSampleLocale.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisMonitorSampleLocale.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisMonitorSampleLocale.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisMonitorSampleLocale.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisMonitorSampleLocale.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
