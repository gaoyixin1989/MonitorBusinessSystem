using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Contract;
using i3.DataAccess.Channels.Mis.Contract;

namespace i3.BusinessLogic.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书企业信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractCompanyLogic : LogicBase
    {

	TMisContractCompanyVo tMisContractCompany = new TMisContractCompanyVo();
    TMisContractCompanyAccess access;
		
	public TMisContractCompanyLogic()
	{
		  access = new TMisContractCompanyAccess();  
	}
		
	public TMisContractCompanyLogic(TMisContractCompanyVo _tMisContractCompany)
	{
		tMisContractCompany = _tMisContractCompany;
		access = new TMisContractCompanyAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractCompany">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractCompanyVo tMisContractCompany)
        {
            return access.GetSelectResultCount(tMisContractCompany);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractCompanyVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractCompany">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractCompanyVo Details(TMisContractCompanyVo tMisContractCompany)
        {
            return access.Details(tMisContractCompany);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractCompany">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractCompanyVo> SelectByObject(TMisContractCompanyVo tMisContractCompany, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractCompany, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractCompany">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractCompanyVo tMisContractCompany, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractCompany, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractCompany"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractCompanyVo tMisContractCompany)
        {
            return access.SelectByTable(tMisContractCompany);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractCompany">对象</param>
        /// <returns></returns>
        public TMisContractCompanyVo SelectByObject(TMisContractCompanyVo tMisContractCompany)
        {
            return access.SelectByObject(tMisContractCompany);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractCompanyVo tMisContractCompany)
        {
            return access.Create(tMisContractCompany);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractCompany">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractCompanyVo tMisContractCompany)
        {
            return access.Edit(tMisContractCompany);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractCompany_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisContractCompany_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractCompanyVo tMisContractCompany_UpdateSet, TMisContractCompanyVo tMisContractCompany_UpdateWhere)
        {
            return access.Edit(tMisContractCompany_UpdateSet, tMisContractCompany_UpdateWhere);
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
        public bool Delete(TMisContractCompanyVo tMisContractCompany)
        {
            return access.Delete(tMisContractCompany);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisContractCompany.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //合同ID
	  if (tMisContractCompany.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("合同ID不能为空");
                return false;
            }
	  //企业ID
	  if (tMisContractCompany.COMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("企业ID不能为空");
                return false;
            }
	  //企业法人代码
	  if (tMisContractCompany.COMPANY_CODE.Trim() == "")
            {
                this.Tips.AppendLine("企业法人代码不能为空");
                return false;
            }
	  //企业名称
	  if (tMisContractCompany.COMPANY_NAME.Trim() == "")
            {
                this.Tips.AppendLine("企业名称不能为空");
                return false;
            }
	  //拼音编码
	  if (tMisContractCompany.PINYIN.Trim() == "")
            {
                this.Tips.AppendLine("拼音编码不能为空");
                return false;
            }
	  //主管部门
	  if (tMisContractCompany.DIRECTOR_DEPT.Trim() == "")
            {
                this.Tips.AppendLine("主管部门不能为空");
                return false;
            }
	  //经济类型
	  if (tMisContractCompany.ECONOMY_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("经济类型不能为空");
                return false;
            }
	  //所在区域
	  if (tMisContractCompany.AREA.Trim() == "")
            {
                this.Tips.AppendLine("所在区域不能为空");
                return false;
            }
	  //企业规模
	  if (tMisContractCompany.SIZE.Trim() == "")
            {
                this.Tips.AppendLine("企业规模不能为空");
                return false;
            }
	  //污染源类型
	  if (tMisContractCompany.POLLUTE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("污染源类型不能为空");
                return false;
            }
	  //行业类别
	  if (tMisContractCompany.INDUSTRY.Trim() == "")
            {
                this.Tips.AppendLine("行业类别不能为空");
                return false;
            }
	  //废气控制级别
	  if (tMisContractCompany.GAS_LEAVEL.Trim() == "")
            {
                this.Tips.AppendLine("废气控制级别不能为空");
                return false;
            }
	  //废水控制级别
	  if (tMisContractCompany.WATER_LEAVEL.Trim() == "")
            {
                this.Tips.AppendLine("废水控制级别不能为空");
                return false;
            }
	  //开业时间
	  if (tMisContractCompany.PRACTICE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("开业时间不能为空");
                return false;
            }
	  //联系人
	  if (tMisContractCompany.CONTACT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("联系人不能为空");
                return false;
            }
	  //联系部门
	  if (tMisContractCompany.LINK_DEPT.Trim() == "")
            {
                this.Tips.AppendLine("联系部门不能为空");
                return false;
            }
	  //电子邮件
	  if (tMisContractCompany.EMAIL.Trim() == "")
            {
                this.Tips.AppendLine("电子邮件不能为空");
                return false;
            }
	  //联系电话
	  if (tMisContractCompany.LINK_PHONE.Trim() == "")
            {
                this.Tips.AppendLine("联系电话不能为空");
                return false;
            }
	  //委托代理人
	  if (tMisContractCompany.FACTOR.Trim() == "")
            {
                this.Tips.AppendLine("委托代理人不能为空");
                return false;
            }
	  //办公电话
	  if (tMisContractCompany.PHONE.Trim() == "")
            {
                this.Tips.AppendLine("办公电话不能为空");
                return false;
            }
	  //移动电话
	  if (tMisContractCompany.MOBAIL_PHONE.Trim() == "")
            {
                this.Tips.AppendLine("移动电话不能为空");
                return false;
            }
	  //传真号码
	  if (tMisContractCompany.FAX.Trim() == "")
            {
                this.Tips.AppendLine("传真号码不能为空");
                return false;
            }
	  //邮政编码
	  if (tMisContractCompany.POST.Trim() == "")
            {
                this.Tips.AppendLine("邮政编码不能为空");
                return false;
            }
	  //联系地址
	  if (tMisContractCompany.CONTACT_ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("联系地址不能为空");
                return false;
            }
	  //监测地址
	  if (tMisContractCompany.MONITOR_ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("监测地址不能为空");
                return false;
            }
	  //企业网址
	  if (tMisContractCompany.WEB_SITE.Trim() == "")
            {
                this.Tips.AppendLine("企业网址不能为空");
                return false;
            }
	  //经度
	  if (tMisContractCompany.LONGITUDE.Trim() == "")
            {
                this.Tips.AppendLine("经度不能为空");
                return false;
            }
	  //纬度
	  if (tMisContractCompany.LATITUDE.Trim() == "")
            {
                this.Tips.AppendLine("纬度不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tMisContractCompany.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //备注1
	  if (tMisContractCompany.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisContractCompany.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisContractCompany.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisContractCompany.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisContractCompany.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
