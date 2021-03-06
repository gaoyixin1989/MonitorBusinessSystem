using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.PART;
using i3.DataAccess.Channels.OA.PART;

namespace i3.BusinessLogic.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料基础信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartInfoLogic : LogicBase
    {

	TOaPartInfoVo tOaPartInfo = new TOaPartInfoVo();
    TOaPartInfoAccess access;
		
	public TOaPartInfoLogic()
	{
		  access = new TOaPartInfoAccess();  
	}
		
	public TOaPartInfoLogic(TOaPartInfoVo _tOaPartInfo)
	{
		tOaPartInfo = _tOaPartInfo;
		access = new TOaPartInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartInfoVo tOaPartInfo)
        {
            return access.GetSelectResultCount(tOaPartInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartInfoVo Details(TOaPartInfoVo tOaPartInfo)
        {
            return access.Details(tOaPartInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartInfoVo> SelectByObject(TOaPartInfoVo tOaPartInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaPartInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartInfoVo tOaPartInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaPartInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartInfoVo tOaPartInfo)
        {
            return access.SelectByTable(tOaPartInfo);
        }

        public DataTable GetLineInfo(string PART_NAME, string StartTime, string EndTime)
        {
            return access.GetLineInfo(PART_NAME, StartTime, EndTime);
        }
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public DataTable SelectByTable_ByJoin(TOaPartInfoVo tOaPartInfo, int iIndex, int iCount)
        {
            return access.SelectByTable_ByJoin(tOaPartInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartInfo">对象</param>
        /// <returns></returns>
        public TOaPartInfoVo SelectByObject(TOaPartInfoVo tOaPartInfo)
        {
            return access.SelectByObject(tOaPartInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartInfoVo tOaPartInfo)
        {
            return access.Create(tOaPartInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartInfoVo tOaPartInfo)
        {
            return access.Edit(tOaPartInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaPartInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartInfoVo tOaPartInfo_UpdateSet, TOaPartInfoVo tOaPartInfo_UpdateWhere)
        {
            return access.Edit(tOaPartInfo_UpdateSet, tOaPartInfo_UpdateWhere);
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
        public bool Delete(TOaPartInfoVo tOaPartInfo)
        {
            return access.Delete(tOaPartInfo);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaPartInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //物料编码
	  if (tOaPartInfo.PART_CODE.Trim() == "")
            {
                this.Tips.AppendLine("物料编码不能为空");
                return false;
            }
	  //物料类别
	  if (tOaPartInfo.PART_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("物料类别不能为空");
                return false;
            }
	  //物料名称
	  if (tOaPartInfo.PART_NAME.Trim() == "")
            {
                this.Tips.AppendLine("物料名称不能为空");
                return false;
            }
	  //单位
	  if (tOaPartInfo.UNIT.Trim() == "")
            {
                this.Tips.AppendLine("单位不能为空");
                return false;
            }
	  //规格型号
	  if (tOaPartInfo.MODELS.Trim() == "")
            {
                this.Tips.AppendLine("规格型号不能为空");
                return false;
            }
	  //库存量
	  if (tOaPartInfo.INVENTORY.Trim() == "")
            {
                this.Tips.AppendLine("库存量不能为空");
                return false;
            }
	  //介质/基体
	  if (tOaPartInfo.MEDIUM.Trim() == "")
            {
                this.Tips.AppendLine("介质/基体不能为空");
                return false;
            }
	  //分析纯/化学纯
	  if (tOaPartInfo.PURE.Trim() == "")
            {
                this.Tips.AppendLine("分析纯/化学纯不能为空");
                return false;
            }
	  //报警值
	  if (tOaPartInfo.ALARM.Trim() == "")
            {
                this.Tips.AppendLine("报警值不能为空");
                return false;
            }
	  //用途
	  if (tOaPartInfo.USEING.Trim() == "")
            {
                this.Tips.AppendLine("用途不能为空");
                return false;
            }
	  //技术要求
	  if (tOaPartInfo.REQUEST.Trim() == "")
            {
                this.Tips.AppendLine("技术要求不能为空");
                return false;
            }
	  //性质说明
	  if (tOaPartInfo.NARURE.Trim() == "")
            {
                this.Tips.AppendLine("性质说明不能为空");
                return false;
            }
	  //备注1
	  if (tOaPartInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaPartInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaPartInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaPartInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaPartInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
