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
    /// 功能：实验室空白批次表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcEmptyBatLogic : LogicBase
    {

	TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat = new TMisMonitorQcEmptyBatVo();
    TMisMonitorQcEmptyBatAccess access;
		
	public TMisMonitorQcEmptyBatLogic()
	{
		  access = new TMisMonitorQcEmptyBatAccess();  
	}
		
	public TMisMonitorQcEmptyBatLogic(TMisMonitorQcEmptyBatVo _tMisMonitorQcEmptyBat)
	{
		tMisMonitorQcEmptyBat = _tMisMonitorQcEmptyBat;
		access = new TMisMonitorQcEmptyBatAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            return access.GetSelectResultCount(tMisMonitorQcEmptyBat);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcEmptyBatVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcEmptyBatVo Details(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            return access.Details(tMisMonitorQcEmptyBat);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcEmptyBatVo> SelectByObject(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorQcEmptyBat, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorQcEmptyBat, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            return access.SelectByTable(tMisMonitorQcEmptyBat);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">对象</param>
        /// <returns></returns>
        public TMisMonitorQcEmptyBatVo SelectByObject(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            return access.SelectByObject(tMisMonitorQcEmptyBat);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            return access.Create(tMisMonitorQcEmptyBat);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            return access.Edit(tMisMonitorQcEmptyBat);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcEmptyBat_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorQcEmptyBat_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat_UpdateSet, TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat_UpdateWhere)
        {
            return access.Edit(tMisMonitorQcEmptyBat_UpdateSet, tMisMonitorQcEmptyBat_UpdateWhere);
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
        public bool Delete(TMisMonitorQcEmptyBatVo tMisMonitorQcEmptyBat)
        {
            return access.Delete(tMisMonitorQcEmptyBat);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisMonitorQcEmptyBat.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //空白批次
	  if (tMisMonitorQcEmptyBat.QC_EMPTY_IN_NUM.Trim() == "")
            {
                this.Tips.AppendLine("空白批次不能为空");
                return false;
            }
	  //空白测试日期
	  if (tMisMonitorQcEmptyBat.QC_EMPTY_IN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("空白测试日期不能为空");
                return false;
            }
	  //实验室空白个数
	  if (tMisMonitorQcEmptyBat.QC_EMPTY_IN_COUNT.Trim() == "")
            {
                this.Tips.AppendLine("实验室空白个数不能为空");
                return false;
            }
	  //实验室空白值
	  if (tMisMonitorQcEmptyBat.QC_EMPTY_IN_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("实验室空白值不能为空");
                return false;
            }
	  //相对偏差（%）
	  if (tMisMonitorQcEmptyBat.QC_EMPTY_OFFSET.Trim() == "")
            {
                this.Tips.AppendLine("相对偏差（%）不能为空");
                return false;
            }
	  //空白是否合格
	  if (tMisMonitorQcEmptyBat.QC_EMPTY_ISOK.Trim() == "")
            {
                this.Tips.AppendLine("空白是否合格不能为空");
                return false;
            }
	  //备注1
	  if (tMisMonitorQcEmptyBat.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisMonitorQcEmptyBat.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisMonitorQcEmptyBat.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisMonitorQcEmptyBat.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisMonitorQcEmptyBat.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
