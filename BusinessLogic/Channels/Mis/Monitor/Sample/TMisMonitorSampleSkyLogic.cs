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
    /// 功能：天气情况表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleSkyLogic : LogicBase
    {

	TMisMonitorSampleSkyVo tMisMonitorSampleSky = new TMisMonitorSampleSkyVo();
    TMisMonitorSampleSkyAccess access;
		
	public TMisMonitorSampleSkyLogic()
	{
		  access = new TMisMonitorSampleSkyAccess();  
	}
		
	public TMisMonitorSampleSkyLogic(TMisMonitorSampleSkyVo _tMisMonitorSampleSky)
	{
		tMisMonitorSampleSky = _tMisMonitorSampleSky;
		access = new TMisMonitorSampleSkyAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            return access.GetSelectResultCount(tMisMonitorSampleSky);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleSkyVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleSkyVo Details(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            return access.Details(tMisMonitorSampleSky);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleSkyVo> SelectByObject(TMisMonitorSampleSkyVo tMisMonitorSampleSky, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorSampleSky, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleSkyVo tMisMonitorSampleSky, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorSampleSky, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleSky"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            return access.SelectByTable(tMisMonitorSampleSky);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleSky">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleSkyVo SelectByObject(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            return access.SelectByObject(tMisMonitorSampleSky);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            return access.Create(tMisMonitorSampleSky);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleSky">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            return access.Edit(tMisMonitorSampleSky);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleSky_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorSampleSky_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleSkyVo tMisMonitorSampleSky_UpdateSet, TMisMonitorSampleSkyVo tMisMonitorSampleSky_UpdateWhere)
        {
            return access.Edit(tMisMonitorSampleSky_UpdateSet, tMisMonitorSampleSky_UpdateWhere);
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
        public bool Delete(TMisMonitorSampleSkyVo tMisMonitorSampleSky)
        {
            return access.Delete(tMisMonitorSampleSky);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisMonitorSampleSky.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测子任务ID
	  if (tMisMonitorSampleSky.SUBTASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测子任务ID不能为空");
                return false;
            }
	  //天气项目
	  if (tMisMonitorSampleSky.WEATHER_ITEM.Trim() == "")
            {
                this.Tips.AppendLine("天气项目不能为空");
                return false;
            }
	  //天气信息
	  if (tMisMonitorSampleSky.WEATHER_INFO.Trim() == "")
            {
                this.Tips.AppendLine("天气信息不能为空");
                return false;
            }
	  //备注1
	  if (tMisMonitorSampleSky.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisMonitorSampleSky.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisMonitorSampleSky.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisMonitorSampleSky.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisMonitorSampleSky.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
