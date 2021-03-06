using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.DataAccess.Channels.Mis.Monitor.QC;

namespace i3.BusinessLogic.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：标准样结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcStLogic : LogicBase
    {

	TMisMonitorQcStVo tMisMonitorQcSt = new TMisMonitorQcStVo();
    TMisMonitorQcStAccess access;
		
	public TMisMonitorQcStLogic()
	{
		  access = new TMisMonitorQcStAccess();  
	}
		
	public TMisMonitorQcStLogic(TMisMonitorQcStVo _tMisMonitorQcSt)
	{
		tMisMonitorQcSt = _tMisMonitorQcSt;
		access = new TMisMonitorQcStAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            return access.GetSelectResultCount(tMisMonitorQcSt);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcStVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcStVo Details(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            return access.Details(tMisMonitorQcSt);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcStVo> SelectByObject(TMisMonitorQcStVo tMisMonitorQcSt, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorQcSt, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcStVo tMisMonitorQcSt, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorQcSt, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcSt"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            return access.SelectByTable(tMisMonitorQcSt);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcSt">对象</param>
        /// <returns></returns>
        public TMisMonitorQcStVo SelectByObject(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            return access.SelectByObject(tMisMonitorQcSt);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            return access.Create(tMisMonitorQcSt);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcSt">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            return access.Edit(tMisMonitorQcSt);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcSt_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorQcSt_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcStVo tMisMonitorQcSt_UpdateSet, TMisMonitorQcStVo tMisMonitorQcSt_UpdateWhere)
        {
            return access.Edit(tMisMonitorQcSt_UpdateSet, tMisMonitorQcSt_UpdateWhere);
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
        public bool Delete(TMisMonitorQcStVo tMisMonitorQcSt)
        {
            return access.Delete(tMisMonitorQcSt);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisMonitorQcSt.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //原始样分析结果 ID
	  if (tMisMonitorQcSt.RESULT_ID_SRC.Trim() == "")
            {
                this.Tips.AppendLine("原始样分析结果 ID不能为空");
                return false;
            }
	  //空白样分析结果 ID
	  if (tMisMonitorQcSt.RESULT_ID_ST.Trim() == "")
            {
                this.Tips.AppendLine("空白样分析结果 ID不能为空");
                return false;
            }
	  //实验室标准样标准值
	  if (tMisMonitorQcSt.SRC_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("实验室标准样标准值不能为空");
                return false;
            }
	  //不确定度
	  if (tMisMonitorQcSt.UNCERTAINTY.Trim() == "")
            {
                this.Tips.AppendLine("不确定度不能为空");
                return false;
            }
	  //实验室标准样结果值
	  if (tMisMonitorQcSt.ST_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("实验室标准样结果值不能为空");
                return false;
            }
	  //空白是否合格
	  if (tMisMonitorQcSt.ST_ISOK.Trim() == "")
            {
                this.Tips.AppendLine("空白是否合格不能为空");
                return false;
            }
	  //备注1
	  if (tMisMonitorQcSt.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisMonitorQcSt.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisMonitorQcSt.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisMonitorQcSt.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisMonitorQcSt.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
