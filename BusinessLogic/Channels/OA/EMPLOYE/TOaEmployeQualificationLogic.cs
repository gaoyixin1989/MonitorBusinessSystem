using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.EMPLOYE;
using i3.DataAccess.Channels.OA.EMPLOYE;

namespace i3.BusinessLogic.Channels.OA.EMPLOYE
{
    /// <summary>
    /// 功能：员工资格证书
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeQualificationLogic : LogicBase
    {

	TOaEmployeQualificationVo tOaEmployeQualification = new TOaEmployeQualificationVo();
    TOaEmployeQualificationAccess access;
		
	public TOaEmployeQualificationLogic()
	{
		  access = new TOaEmployeQualificationAccess();  
	}
		
	public TOaEmployeQualificationLogic(TOaEmployeQualificationVo _tOaEmployeQualification)
	{
		tOaEmployeQualification = _tOaEmployeQualification;
		access = new TOaEmployeQualificationAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            return access.GetSelectResultCount(tOaEmployeQualification);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeQualificationVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeQualification">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeQualificationVo Details(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            return access.Details(tOaEmployeQualification);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeQualificationVo> SelectByObject(TOaEmployeQualificationVo tOaEmployeQualification, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaEmployeQualification, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeQualificationVo tOaEmployeQualification, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaEmployeQualification, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeQualification"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            return access.SelectByTable(tOaEmployeQualification);
        }

        /// <summary>
        /// 获取证书信息及其附件信息
        /// </summary>
        /// <param name="tOaEmployeQualification"></param>
        /// <param name="strFileType"></param>
        /// <returns></returns>
        public DataTable SelectByUnionAttTable(TOaEmployeQualificationVo tOaEmployeQualification, string strFileType)
        {
            return access.SelectByUnionAttTable(tOaEmployeQualification,strFileType);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeQualification">对象</param>
        /// <returns></returns>
        public TOaEmployeQualificationVo SelectByObject(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            return access.SelectByObject(tOaEmployeQualification);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            return access.Create(tOaEmployeQualification);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeQualification">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            return access.Edit(tOaEmployeQualification);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeQualification_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeQualification_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeQualificationVo tOaEmployeQualification_UpdateSet, TOaEmployeQualificationVo tOaEmployeQualification_UpdateWhere)
        {
            return access.Edit(tOaEmployeQualification_UpdateSet, tOaEmployeQualification_UpdateWhere);
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
        public bool Delete(TOaEmployeQualificationVo tOaEmployeQualification)
        {
            return access.Delete(tOaEmployeQualification);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaEmployeQualification.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //员工编号
	  if (tOaEmployeQualification.EMPLOYEID.Trim() == "")
            {
                this.Tips.AppendLine("员工编号不能为空");
                return false;
            }
	  //证书名称
	  if (tOaEmployeQualification.CERTITICATENAME.Trim() == "")
            {
                this.Tips.AppendLine("证书名称不能为空");
                return false;
            }
	  //证书编号
	  if (tOaEmployeQualification.CERTITICATECODE.Trim() == "")
            {
                this.Tips.AppendLine("证书编号不能为空");
                return false;
            }
	  //发证单位
	  if (tOaEmployeQualification.ISSUINGAUTHO.Trim() == "")
            {
                this.Tips.AppendLine("发证单位不能为空");
                return false;
            }
	  //发证时间
	  if (tOaEmployeQualification.ISSUINDATE.Trim() == "")
            {
                this.Tips.AppendLine("发证时间不能为空");
                return false;
            }
	  //有效日期
	  if (tOaEmployeQualification.ACTIVEDATE.Trim() == "")
            {
                this.Tips.AppendLine("有效日期不能为空");
                return false;
            }
	  //备注1
	  if (tOaEmployeQualification.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaEmployeQualification.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaEmployeQualification.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaEmployeQualification.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaEmployeQualification.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
