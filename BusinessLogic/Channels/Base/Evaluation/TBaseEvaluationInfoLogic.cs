using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Evaluation;
using i3.DataAccess.Channels.Base.Evaluation;

namespace i3.BusinessLogic.Channels.Base.Evaluation
{
    /// <summary>
    /// 功能：监测类别管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseEvaluationInfoLogic : LogicBase
    {

	TBaseEvaluationInfoVo tBaseEvaluationInfo = new TBaseEvaluationInfoVo();
    TBaseEvaluationInfoAccess access;
		
	public TBaseEvaluationInfoLogic()
	{
		  access = new TBaseEvaluationInfoAccess();  
	}
		
	public TBaseEvaluationInfoLogic(TBaseEvaluationInfoVo _tBaseEvaluationInfo)
	{
		tBaseEvaluationInfo = _tBaseEvaluationInfo;
		access = new TBaseEvaluationInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            return access.GetSelectResultCount(tBaseEvaluationInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseEvaluationInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseEvaluationInfoVo Details(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            return access.Details(tBaseEvaluationInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseEvaluationInfoVo> SelectByObject(TBaseEvaluationInfoVo tBaseEvaluationInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseEvaluationInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseEvaluationInfoVo tBaseEvaluationInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseEvaluationInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseEvaluationInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            return access.SelectByTable(tBaseEvaluationInfo);
        }


        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public TBaseEvaluationInfoVo SelectByObject(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            return access.SelectByObject(tBaseEvaluationInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            return access.Create(tBaseEvaluationInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            return access.Edit(tBaseEvaluationInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseEvaluationInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseEvaluationInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseEvaluationInfoVo tBaseEvaluationInfo_UpdateSet, TBaseEvaluationInfoVo tBaseEvaluationInfo_UpdateWhere)
        {
            return access.Edit(tBaseEvaluationInfo_UpdateSet, tBaseEvaluationInfo_UpdateWhere);
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
        public bool Delete(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            return access.Delete(tBaseEvaluationInfo);
        }

                /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseEvaluationInfoVo tBaseEvaluationInfo, int iIndex, int iCount)
        {
            return access.SelectDefinedTadble(tBaseEvaluationInfo,iIndex,iCount);
        }

                /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseEvaluationInfoVo tBaseEvaluationInfo)
        {
            return access.GetSelecDefinedtResultCount(tBaseEvaluationInfo);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseEvaluationInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //评价标准编号
	  if (tBaseEvaluationInfo.STANDARD_CODE.Trim() == "")
            {
                this.Tips.AppendLine("评价标准编号不能为空");
                return false;
            }
	  //评价标准名称
	  if (tBaseEvaluationInfo.STANDARD_NAME.Trim() == "")
            {
                this.Tips.AppendLine("评价标准名称不能为空");
                return false;
            }
	  //评价标准类别(国家标准、行业标准、地方标准、国际标准)
	  if (tBaseEvaluationInfo.STANDARD_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("评价标准类别(国家标准、行业标准、地方标准、国际标准)不能为空");
                return false;
            }
	  //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
	  if (tBaseEvaluationInfo.MONITOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）不能为空");
                return false;
            }
	  //生效日期
	  if (tBaseEvaluationInfo.EFFECTIVE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("生效日期不能为空");
                return false;
            }
	  //附件路径
	  if (tBaseEvaluationInfo.ATTACHMENT_URL.Trim() == "")
            {
                this.Tips.AppendLine("附件路径不能为空");
                return false;
            }
	  //评价标准描述
	  if (tBaseEvaluationInfo.DESCRIPTION.Trim() == "")
            {
                this.Tips.AppendLine("评价标准描述不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tBaseEvaluationInfo.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //备注1
	  if (tBaseEvaluationInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseEvaluationInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseEvaluationInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseEvaluationInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseEvaluationInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
