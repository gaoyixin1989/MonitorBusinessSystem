using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Outcompany;
using i3.DataAccess.Channels.Base.Outcompany;

namespace i3.BusinessLogic.Channels.Base.Outcompany
{
    /// <summary>
    /// 功能：分包单位
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseOutcompanyInfoLogic : LogicBase
    {

	TBaseOutcompanyInfoVo tBaseOutcompanyInfo = new TBaseOutcompanyInfoVo();
    TBaseOutcompanyInfoAccess access;
		
	public TBaseOutcompanyInfoLogic()
	{
		  access = new TBaseOutcompanyInfoAccess();  
	}
		
	public TBaseOutcompanyInfoLogic(TBaseOutcompanyInfoVo _tBaseOutcompanyInfo)
	{
		tBaseOutcompanyInfo = _tBaseOutcompanyInfo;
		access = new TBaseOutcompanyInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            return access.GetSelectResultCount(tBaseOutcompanyInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseOutcompanyInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseOutcompanyInfoVo Details(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            return access.Details(tBaseOutcompanyInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseOutcompanyInfoVo> SelectByObject(TBaseOutcompanyInfoVo tBaseOutcompanyInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseOutcompanyInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseOutcompanyInfoVo tBaseOutcompanyInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseOutcompanyInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseOutcompanyInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            return access.SelectByTable(tBaseOutcompanyInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">对象</param>
        /// <returns></returns>
        public TBaseOutcompanyInfoVo SelectByObject(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            return access.SelectByObject(tBaseOutcompanyInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            return access.Create(tBaseOutcompanyInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseOutcompanyInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            return access.Edit(tBaseOutcompanyInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseOutcompanyInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseOutcompanyInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseOutcompanyInfoVo tBaseOutcompanyInfo_UpdateSet, TBaseOutcompanyInfoVo tBaseOutcompanyInfo_UpdateWhere)
        {
            return access.Edit(tBaseOutcompanyInfo_UpdateSet, tBaseOutcompanyInfo_UpdateWhere);
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
        public bool Delete(TBaseOutcompanyInfoVo tBaseOutcompanyInfo)
        {
            return access.Delete(tBaseOutcompanyInfo);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseOutcompanyInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //公司法人代码
	  if (tBaseOutcompanyInfo.COMPANY_CODE.Trim() == "")
            {
                this.Tips.AppendLine("公司法人代码不能为空");
                return false;
            }
	  //公司名称
	  if (tBaseOutcompanyInfo.COMPANY_NAME.Trim() == "")
            {
                this.Tips.AppendLine("公司名称不能为空");
                return false;
            }
	  //拼音编码
	  if (tBaseOutcompanyInfo.PINYIN.Trim() == "")
            {
                this.Tips.AppendLine("拼音编码不能为空");
                return false;
            }
	  //联系人
	  if (tBaseOutcompanyInfo.LINK_MAN.Trim() == "")
            {
                this.Tips.AppendLine("联系人不能为空");
                return false;
            }
	  //联系
	  if (tBaseOutcompanyInfo.PHONE.Trim() == "")
            {
                this.Tips.AppendLine("联系不能为空");
                return false;
            }
	  //邮编
	  if (tBaseOutcompanyInfo.POST.Trim() == "")
            {
                this.Tips.AppendLine("邮编不能为空");
                return false;
            }
	  //详细地址
	  if (tBaseOutcompanyInfo.ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("详细地址不能为空");
                return false;
            }
	  //外包公司资质
	  if (tBaseOutcompanyInfo.APTITUDE.Trim() == "")
            {
                this.Tips.AppendLine("外包公司资质不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tBaseOutcompanyInfo.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //备注1
	  if (tBaseOutcompanyInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseOutcompanyInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseOutcompanyInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseOutcompanyInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseOutcompanyInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
