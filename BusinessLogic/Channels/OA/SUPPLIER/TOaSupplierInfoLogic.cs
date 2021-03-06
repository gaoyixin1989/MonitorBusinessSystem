using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.SUPPLIER;
using i3.DataAccess.Channels.OA.SUPPLIER;

namespace i3.BusinessLogic.Channels.OA.SUPPLIER
{
    /// <summary>
    /// 功能：供应商信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSupplierInfoLogic : LogicBase
    {

	TOaSupplierInfoVo tOaSupplierInfo = new TOaSupplierInfoVo();
    TOaSupplierInfoAccess access;
		
	public TOaSupplierInfoLogic()
	{
		  access = new TOaSupplierInfoAccess();  
	}
		
	public TOaSupplierInfoLogic(TOaSupplierInfoVo _tOaSupplierInfo)
	{
		tOaSupplierInfo = _tOaSupplierInfo;
		access = new TOaSupplierInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSupplierInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaSupplierInfoVo tOaSupplierInfo)
        {
            return access.GetSelectResultCount(tOaSupplierInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaSupplierInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaSupplierInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaSupplierInfoVo Details(TOaSupplierInfoVo tOaSupplierInfo)
        {
            return access.Details(tOaSupplierInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaSupplierInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaSupplierInfoVo> SelectByObject(TOaSupplierInfoVo tOaSupplierInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaSupplierInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaSupplierInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaSupplierInfoVo tOaSupplierInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaSupplierInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSupplierInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSupplierInfoVo tOaSupplierInfo)
        {
            return access.SelectByTable(tOaSupplierInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaSupplierInfo">对象</param>
        /// <returns></returns>
        public TOaSupplierInfoVo SelectByObject(TOaSupplierInfoVo tOaSupplierInfo)
        {
            return access.SelectByObject(tOaSupplierInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaSupplierInfoVo tOaSupplierInfo)
        {
            return access.Create(tOaSupplierInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSupplierInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSupplierInfoVo tOaSupplierInfo)
        {
            return access.Edit(tOaSupplierInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSupplierInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaSupplierInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSupplierInfoVo tOaSupplierInfo_UpdateSet, TOaSupplierInfoVo tOaSupplierInfo_UpdateWhere)
        {
            return access.Edit(tOaSupplierInfo_UpdateSet, tOaSupplierInfo_UpdateWhere);
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
        public bool Delete(TOaSupplierInfoVo tOaSupplierInfo)
        {
            return access.Delete(tOaSupplierInfo);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaSupplierInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //供应商名称
	  if (tOaSupplierInfo.SUPPLIER_NAME.Trim() == "")
            {
                this.Tips.AppendLine("供应商名称不能为空");
                return false;
            }
	  //供应物质类别
	  if (tOaSupplierInfo.SUPPLIER_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("供应物质类别不能为空");
                return false;
            }
	  //经营范围
	  if (tOaSupplierInfo.PRODUCTS.Trim() == "")
            {
                this.Tips.AppendLine("经营范围不能为空");
                return false;
            }
	  //质量体系认证
	  if (tOaSupplierInfo.QUANTITYSYSTEM.Trim() == "")
            {
                this.Tips.AppendLine("质量体系认证不能为空");
                return false;
            }
	  //联系人
	  if (tOaSupplierInfo.LINK_MAN.Trim() == "")
            {
                this.Tips.AppendLine("联系人不能为空");
                return false;
            }
	  //地址
	  if (tOaSupplierInfo.ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("地址不能为空");
                return false;
            }
	  //电话
	  if (tOaSupplierInfo.TEL.Trim() == "")
            {
                this.Tips.AppendLine("电话不能为空");
                return false;
            }
	  //传真
	  if (tOaSupplierInfo.FAX.Trim() == "")
            {
                this.Tips.AppendLine("传真不能为空");
                return false;
            }
	  //邮件
	  if (tOaSupplierInfo.EMAIL.Trim() == "")
            {
                this.Tips.AppendLine("邮件不能为空");
                return false;
            }
	  //邮政编码
	  if (tOaSupplierInfo.POST_CODE.Trim() == "")
            {
                this.Tips.AppendLine("邮政编码不能为空");
                return false;
            }
	  //开户行
	  if (tOaSupplierInfo.BANK.Trim() == "")
            {
                this.Tips.AppendLine("开户行不能为空");
                return false;
            }
	  //帐号
	  if (tOaSupplierInfo.ACCOUNT_FOR.Trim() == "")
            {
                this.Tips.AppendLine("帐号不能为空");
                return false;
            }
	  //备注1
	  if (tOaSupplierInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaSupplierInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaSupplierInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaSupplierInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaSupplierInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
