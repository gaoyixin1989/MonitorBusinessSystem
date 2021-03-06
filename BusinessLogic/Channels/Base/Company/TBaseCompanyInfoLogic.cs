using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Company;
using i3.DataAccess.Channels.Base.Company;

namespace i3.BusinessLogic.Channels.Base.Company
{
    /// <summary>
    /// 功能：企业信息管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseCompanyInfoLogic : LogicBase
    {

	TBaseCompanyInfoVo tBaseCompanyInfo = new TBaseCompanyInfoVo();
    TBaseCompanyInfoAccess access;
		
	public TBaseCompanyInfoLogic()
	{
		  access = new TBaseCompanyInfoAccess();  
	}
		
	public TBaseCompanyInfoLogic(TBaseCompanyInfoVo _tBaseCompanyInfo)
	{
		tBaseCompanyInfo = _tBaseCompanyInfo;
		access = new TBaseCompanyInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            return access.GetSelectResultCount(tBaseCompanyInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseCompanyInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseCompanyInfoVo Details(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            return access.Details(tBaseCompanyInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseCompanyInfoVo> SelectByObject(TBaseCompanyInfoVo tBaseCompanyInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseCompanyInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseCompanyInfoVo tBaseCompanyInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseCompanyInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseCompanyInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            return access.SelectByTable(tBaseCompanyInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseCompanyInfo">对象</param>
        /// <returns></returns>
        public TBaseCompanyInfoVo SelectByObject(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            return access.SelectByObject(tBaseCompanyInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            return access.Create(tBaseCompanyInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            return access.Edit(tBaseCompanyInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseCompanyInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyInfoVo tBaseCompanyInfo_UpdateSet, TBaseCompanyInfoVo tBaseCompanyInfo_UpdateWhere)
        {
            return access.Edit(tBaseCompanyInfo_UpdateSet, tBaseCompanyInfo_UpdateWhere);
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
        public bool Delete(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            return access.Delete(tBaseCompanyInfo);
        }


        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseCompanyInfoVo tBaseCompanyInfo, int iIndex, int iCount)
        {
            return access.SelectDefinedTadble(tBaseCompanyInfo, iIndex, iCount);
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseCompanyInfoVo tBaseCompanyInfo)
        {
            return access.GetSelecDefinedtResultCount(tBaseCompanyInfo);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseCompanyInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //企业法人代码
	  if (tBaseCompanyInfo.COMPANY_CODE.Trim() == "")
            {
                this.Tips.AppendLine("企业法人代码不能为空");
                return false;
            }
	  //企业名称
	  if (tBaseCompanyInfo.COMPANY_NAME.Trim() == "")
            {
                this.Tips.AppendLine("企业名称不能为空");
                return false;
            }
	  //拼音编码
	  if (tBaseCompanyInfo.PINYIN.Trim() == "")
            {
                this.Tips.AppendLine("拼音编码不能为空");
                return false;
            }
	  //主管部门
	  if (tBaseCompanyInfo.DIRECTOR_DEPT.Trim() == "")
            {
                this.Tips.AppendLine("主管部门不能为空");
                return false;
            }
	  //经济类型
	  if (tBaseCompanyInfo.ECONOMY_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("经济类型不能为空");
                return false;
            }
	  //所在区域
	  if (tBaseCompanyInfo.AREA.Trim() == "")
            {
                this.Tips.AppendLine("所在区域不能为空");
                return false;
            }
	  //企业规模
	  if (tBaseCompanyInfo.SIZE.Trim() == "")
            {
                this.Tips.AppendLine("企业规模不能为空");
                return false;
            }
	  //污染源类型
	  if (tBaseCompanyInfo.POLLUTE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("污染源类型不能为空");
                return false;
            }
	  //行业类别
	  if (tBaseCompanyInfo.INDUSTRY.Trim() == "")
            {
                this.Tips.AppendLine("行业类别不能为空");
                return false;
            }
	  //废气控制级别
	  if (tBaseCompanyInfo.GAS_LEAVEL.Trim() == "")
            {
                this.Tips.AppendLine("废气控制级别不能为空");
                return false;
            }
	  //废水控制级别
	  if (tBaseCompanyInfo.WATER_LEAVEL.Trim() == "")
            {
                this.Tips.AppendLine("废水控制级别不能为空");
                return false;
            }
	  //开业时间
	  if (tBaseCompanyInfo.PRACTICE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("开业时间不能为空");
                return false;
            }
	  //联系人
	  if (tBaseCompanyInfo.CONTACT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("联系人不能为空");
                return false;
            }
	  //联系部门
	  if (tBaseCompanyInfo.LINK_DEPT.Trim() == "")
            {
                this.Tips.AppendLine("联系部门不能为空");
                return false;
            }
	  //电子邮件
	  if (tBaseCompanyInfo.EMAIL.Trim() == "")
            {
                this.Tips.AppendLine("电子邮件不能为空");
                return false;
            }
	  //联系电话
	  if (tBaseCompanyInfo.LINK_PHONE.Trim() == "")
            {
                this.Tips.AppendLine("联系电话不能为空");
                return false;
            }
	  //委托代理人
	  if (tBaseCompanyInfo.FACTOR.Trim() == "")
            {
                this.Tips.AppendLine("委托代理人不能为空");
                return false;
            }
	  //办公电话
	  if (tBaseCompanyInfo.PHONE.Trim() == "")
            {
                this.Tips.AppendLine("办公电话不能为空");
                return false;
            }
	  //移动电话
	  if (tBaseCompanyInfo.MOBAIL_PHONE.Trim() == "")
            {
                this.Tips.AppendLine("移动电话不能为空");
                return false;
            }
	  //传真号码
	  if (tBaseCompanyInfo.FAX.Trim() == "")
            {
                this.Tips.AppendLine("传真号码不能为空");
                return false;
            }
	  //邮政编码
	  if (tBaseCompanyInfo.POST.Trim() == "")
            {
                this.Tips.AppendLine("邮政编码不能为空");
                return false;
            }
	  //联系地址
	  if (tBaseCompanyInfo.CONTACT_ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("联系地址不能为空");
                return false;
            }
	  //监测地址
	  if (tBaseCompanyInfo.MONITOR_ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("监测地址不能为空");
                return false;
            }
	  //企业网址
	  if (tBaseCompanyInfo.WEB_SITE.Trim() == "")
            {
                this.Tips.AppendLine("企业网址不能为空");
                return false;
            }
	  //经度
	  if (tBaseCompanyInfo.LONGITUDE.Trim() == "")
            {
                this.Tips.AppendLine("经度不能为空");
                return false;
            }
	  //纬度
	  if (tBaseCompanyInfo.LATITUDE.Trim() == "")
            {
                this.Tips.AppendLine("纬度不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tBaseCompanyInfo.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //备注1
	  if (tBaseCompanyInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseCompanyInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseCompanyInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseCompanyInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseCompanyInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
