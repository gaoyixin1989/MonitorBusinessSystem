using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.DataAccess.Channels.Mis.Monitor.Task;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务企业信息表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskCompanyLogic : LogicBase
    {

	TMisMonitorTaskCompanyVo tMisMonitorTaskCompany = new TMisMonitorTaskCompanyVo();
    TMisMonitorTaskCompanyAccess access;
		
	public TMisMonitorTaskCompanyLogic()
	{
		  access = new TMisMonitorTaskCompanyAccess();  
	}
		
	public TMisMonitorTaskCompanyLogic(TMisMonitorTaskCompanyVo _tMisMonitorTaskCompany)
	{
		tMisMonitorTaskCompany = _tMisMonitorTaskCompany;
		access = new TMisMonitorTaskCompanyAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            return access.GetSelectResultCount(tMisMonitorTaskCompany);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskCompanyVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskCompanyVo Details(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            return access.Details(tMisMonitorTaskCompany);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskCompanyVo> SelectByObject(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorTaskCompany, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorTaskCompany, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTaskCompany"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            return access.SelectByTable(tMisMonitorTaskCompany);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskCompanyVo SelectByObject(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            return access.SelectByObject(tMisMonitorTaskCompany);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            return access.Create(tMisMonitorTaskCompany);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskCompany">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            return access.Edit(tMisMonitorTaskCompany);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskCompany_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tMisMonitorTaskCompany_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany_UpdateSet, TMisMonitorTaskCompanyVo tMisMonitorTaskCompany_UpdateWhere)
        {
            return access.Edit(tMisMonitorTaskCompany_UpdateSet, tMisMonitorTaskCompany_UpdateWhere);
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
        public bool Delete(TMisMonitorTaskCompanyVo tMisMonitorTaskCompany)
        {
            return access.Delete(tMisMonitorTaskCompany);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tMisMonitorTaskCompany.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //任务ID
	  if (tMisMonitorTaskCompany.TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("任务ID不能为空");
                return false;
            }
	  //企业ID
	  if (tMisMonitorTaskCompany.COMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("企业ID不能为空");
                return false;
            }
	  //企业法人代码
	  if (tMisMonitorTaskCompany.COMPANY_CODE.Trim() == "")
            {
                this.Tips.AppendLine("企业法人代码不能为空");
                return false;
            }
	  //企业名称
	  if (tMisMonitorTaskCompany.COMPANY_NAME.Trim() == "")
            {
                this.Tips.AppendLine("企业名称不能为空");
                return false;
            }
	  //拼音编码
	  if (tMisMonitorTaskCompany.PINYIN.Trim() == "")
            {
                this.Tips.AppendLine("拼音编码不能为空");
                return false;
            }
	  //主管部门
	  if (tMisMonitorTaskCompany.DIRECTOR_DEPT.Trim() == "")
            {
                this.Tips.AppendLine("主管部门不能为空");
                return false;
            }
	  //经济类型
	  if (tMisMonitorTaskCompany.ECONOMY_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("经济类型不能为空");
                return false;
            }
	  //所在区域
	  if (tMisMonitorTaskCompany.AREA.Trim() == "")
            {
                this.Tips.AppendLine("所在区域不能为空");
                return false;
            }
	  //企业规模
	  if (tMisMonitorTaskCompany.SIZE.Trim() == "")
            {
                this.Tips.AppendLine("企业规模不能为空");
                return false;
            }
	  //污染源类型
	  if (tMisMonitorTaskCompany.POLLUTE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("污染源类型不能为空");
                return false;
            }
	  //行业类别
	  if (tMisMonitorTaskCompany.INDUSTRY.Trim() == "")
            {
                this.Tips.AppendLine("行业类别不能为空");
                return false;
            }
	  //废气控制级别
	  if (tMisMonitorTaskCompany.GAS_LEAVEL.Trim() == "")
            {
                this.Tips.AppendLine("废气控制级别不能为空");
                return false;
            }
	  //废水控制级别
	  if (tMisMonitorTaskCompany.WATER_LEAVEL.Trim() == "")
            {
                this.Tips.AppendLine("废水控制级别不能为空");
                return false;
            }
	  //开业时间
	  if (tMisMonitorTaskCompany.PRACTICE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("开业时间不能为空");
                return false;
            }
	  //联系人
	  if (tMisMonitorTaskCompany.CONTACT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("联系人不能为空");
                return false;
            }
	  //联系部门
	  if (tMisMonitorTaskCompany.LINK_DEPT.Trim() == "")
            {
                this.Tips.AppendLine("联系部门不能为空");
                return false;
            }
	  //电子邮件
	  if (tMisMonitorTaskCompany.EMAIL.Trim() == "")
            {
                this.Tips.AppendLine("电子邮件不能为空");
                return false;
            }
	  //联系电话
	  if (tMisMonitorTaskCompany.LINK_PHONE.Trim() == "")
            {
                this.Tips.AppendLine("联系电话不能为空");
                return false;
            }
	  //委托代理人
	  if (tMisMonitorTaskCompany.FACTOR.Trim() == "")
            {
                this.Tips.AppendLine("委托代理人不能为空");
                return false;
            }
	  //办公电话
	  if (tMisMonitorTaskCompany.PHONE.Trim() == "")
            {
                this.Tips.AppendLine("办公电话不能为空");
                return false;
            }
	  //移动电话
	  if (tMisMonitorTaskCompany.MOBAIL_PHONE.Trim() == "")
            {
                this.Tips.AppendLine("移动电话不能为空");
                return false;
            }
	  //传真号码
	  if (tMisMonitorTaskCompany.FAX.Trim() == "")
            {
                this.Tips.AppendLine("传真号码不能为空");
                return false;
            }
	  //邮政编码
	  if (tMisMonitorTaskCompany.POST.Trim() == "")
            {
                this.Tips.AppendLine("邮政编码不能为空");
                return false;
            }
	  //联系地址
	  if (tMisMonitorTaskCompany.CONTACT_ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("联系地址不能为空");
                return false;
            }
	  //监测地址
	  if (tMisMonitorTaskCompany.MONITOR_ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("监测地址不能为空");
                return false;
            }
	  //企业网址
	  if (tMisMonitorTaskCompany.WEB_SITE.Trim() == "")
            {
                this.Tips.AppendLine("企业网址不能为空");
                return false;
            }
	  //经度
	  if (tMisMonitorTaskCompany.LONGITUDE.Trim() == "")
            {
                this.Tips.AppendLine("经度不能为空");
                return false;
            }
	  //纬度
	  if (tMisMonitorTaskCompany.LATITUDE.Trim() == "")
            {
                this.Tips.AppendLine("纬度不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tMisMonitorTaskCompany.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //备注1
	  if (tMisMonitorTaskCompany.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tMisMonitorTaskCompany.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tMisMonitorTaskCompany.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tMisMonitorTaskCompany.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tMisMonitorTaskCompany.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
