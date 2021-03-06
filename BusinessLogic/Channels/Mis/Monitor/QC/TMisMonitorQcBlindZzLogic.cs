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
    /// 功能：标准盲样
    /// 创建日期：2013-07-02
    /// 创建人：熊卫华
    /// </summary>
    public class TMisMonitorQcBlindZzLogic : LogicBase
    {

	TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz = new TMisMonitorQcBlindZzVo();
    TMisMonitorQcBlindZzAccess access;
		
	public TMisMonitorQcBlindZzLogic()
	{
		  access = new TMisMonitorQcBlindZzAccess();  
	}
		
	public TMisMonitorQcBlindZzLogic(TMisMonitorQcBlindZzVo _tMisMonitorQcBlindZz)
	{
		tMisMonitorQcBlindZz = _tMisMonitorQcBlindZz;
		access = new TMisMonitorQcBlindZzAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            return access.GetSelectResultCount(tMisMonitorQcBlindZz);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcBlindZzVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcBlindZzVo Details(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            return access.Details(tMisMonitorQcBlindZz);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcBlindZzVo> SelectByObject(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorQcBlindZz, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorQcBlindZz, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            return access.SelectByTable(tMisMonitorQcBlindZz);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">对象</param>
        /// <returns></returns>
        public TMisMonitorQcBlindZzVo SelectByObject(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            return access.SelectByObject(tMisMonitorQcBlindZz);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            return access.Create(tMisMonitorQcBlindZz);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            return access.Edit(tMisMonitorQcBlindZz);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcBlindZz_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorQcBlindZz_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz_UpdateSet, TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz_UpdateWhere)
        {
            return access.Edit(tMisMonitorQcBlindZz_UpdateSet, tMisMonitorQcBlindZz_UpdateWhere);
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
        public bool Delete(TMisMonitorQcBlindZzVo tMisMonitorQcBlindZz)
        {
            return access.Delete(tMisMonitorQcBlindZz);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisMonitorQcBlindZz.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //平行样分析结果 ID
	  if (tMisMonitorQcBlindZz.RESULT_ID.Trim() == "")
            {
                this.Tips.AppendLine("平行样分析结果 ID不能为空");
                return false;
            }
	  //标准值
	  if (tMisMonitorQcBlindZz.STANDARD_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("标准值不能为空");
                return false;
            }
	  //不确定度
	  if (tMisMonitorQcBlindZz.UNCETAINTY.Trim() == "")
            {
                this.Tips.AppendLine("不确定度不能为空");
                return false;
            }
	  //测定值
	  if (tMisMonitorQcBlindZz.BLIND_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("测定值不能为空");
                return false;
            }
	  //偏移量（%）
	  if (tMisMonitorQcBlindZz.OFFSET.Trim() == "")
            {
                this.Tips.AppendLine("偏移量（%）不能为空");
                return false;
            }
	  //是否合格
	  if (tMisMonitorQcBlindZz.BLIND_ISOK.Trim() == "")
            {
                this.Tips.AppendLine("是否合格不能为空");
                return false;
            }
	  //质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样 9、质控平行 10、空白加标 11、标准盲样）
	  if (tMisMonitorQcBlindZz.QC_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样 9、质控平行 10、空白加标 11、标准盲样）不能为空");
                return false;
            }
	  //备注1
	  if (tMisMonitorQcBlindZz.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisMonitorQcBlindZz.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisMonitorQcBlindZz.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisMonitorQcBlindZz.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisMonitorQcBlindZz.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
