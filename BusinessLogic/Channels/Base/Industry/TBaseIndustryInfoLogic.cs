using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Industry;
using i3.DataAccess.Channels.Base.Industry;

namespace i3.BusinessLogic.Channels.Base.Industry
{
    /// <summary>
    /// 功能：行业信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseIndustryInfoLogic : LogicBase
    {

	TBaseIndustryInfoVo tBaseIndustryInfo = new TBaseIndustryInfoVo();
    TBaseIndustryInfoAccess access;
		
	public TBaseIndustryInfoLogic()
	{
		  access = new TBaseIndustryInfoAccess();  
	}
		
	public TBaseIndustryInfoLogic(TBaseIndustryInfoVo _tBaseIndustryInfo)
	{
		tBaseIndustryInfo = _tBaseIndustryInfo;
		access = new TBaseIndustryInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            return access.GetSelectResultCount(tBaseIndustryInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseIndustryInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseIndustryInfoVo Details(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            return access.Details(tBaseIndustryInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseIndustryInfoVo> SelectByObject(TBaseIndustryInfoVo tBaseIndustryInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseIndustryInfo, iIndex, iCount);

        }

          /// <summary>
        ///获取指定行业类别的监测项目 胡方扬 2013-03-14
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByObjectForIndustry(TBaseIndustryInfoVo tBaseIndustryInfo, string strMonitorId,int iIndex, int iCount)
        {
            return access.SelectByObjectForIndustry(tBaseIndustryInfo, strMonitorId,iIndex, iCount);
        }

         /// <summary>
        /// 根据点位，行业类别，确定企业已选的监测项目 胡方扬 2013-03-14
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByObjectForFinishedIndustry(TBaseIndustryInfoVo tBaseIndustryInfo, string strMonitorId, string strPointId, int iIndex, int iCount)
        {
            return access.SelectByObjectForFinishedIndustry(tBaseIndustryInfo, strPointId,strMonitorId, iIndex, iCount);
        }
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseIndustryInfoVo tBaseIndustryInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseIndustryInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseIndustryInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            return access.SelectByTable(tBaseIndustryInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseIndustryInfo">对象</param>
        /// <returns></returns>
        public TBaseIndustryInfoVo SelectByObject(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            return access.SelectByObject(tBaseIndustryInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            return access.Create(tBaseIndustryInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseIndustryInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            return access.Edit(tBaseIndustryInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseIndustryInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseIndustryInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseIndustryInfoVo tBaseIndustryInfo_UpdateSet, TBaseIndustryInfoVo tBaseIndustryInfo_UpdateWhere)
        {
            return access.Edit(tBaseIndustryInfo_UpdateSet, tBaseIndustryInfo_UpdateWhere);
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
        public bool Delete(TBaseIndustryInfoVo tBaseIndustryInfo)
        {
            return access.Delete(tBaseIndustryInfo);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseIndustryInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //行业代码
	  if (tBaseIndustryInfo.INDUSTRY_CODE.Trim() == "")
            {
                this.Tips.AppendLine("行业代码不能为空");
                return false;
            }
	  //行业名称
	  if (tBaseIndustryInfo.INDUSTRY_NAME.Trim() == "")
            {
                this.Tips.AppendLine("行业名称不能为空");
                return false;
            }
	  //备注1
	  if (tBaseIndustryInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseIndustryInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseIndustryInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseIndustryInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseIndustryInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
