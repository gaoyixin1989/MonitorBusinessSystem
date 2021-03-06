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
    /// 功能：员工工作成果与事故
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeResultorfaultLogic : LogicBase
    {

	TOaEmployeResultorfaultVo tOaEmployeResultorfault = new TOaEmployeResultorfaultVo();
    TOaEmployeResultorfaultAccess access;
		
	public TOaEmployeResultorfaultLogic()
	{
		  access = new TOaEmployeResultorfaultAccess();  
	}
		
	public TOaEmployeResultorfaultLogic(TOaEmployeResultorfaultVo _tOaEmployeResultorfault)
	{
		tOaEmployeResultorfault = _tOaEmployeResultorfault;
		access = new TOaEmployeResultorfaultAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            return access.GetSelectResultCount(tOaEmployeResultorfault);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeResultorfaultVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeResultorfaultVo Details(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            return access.Details(tOaEmployeResultorfault);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeResultorfaultVo> SelectByObject(TOaEmployeResultorfaultVo tOaEmployeResultorfault, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaEmployeResultorfault, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeResultorfaultVo tOaEmployeResultorfault, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaEmployeResultorfault, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeResultorfault"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            return access.SelectByTable(tOaEmployeResultorfault);
        }

        /// <summary>
        /// 获取员工工作成果及其附件
        /// </summary>
        /// <param name="tOaEmployeResultorfault"></param>
        /// <param name="strFileType"></param>
        /// <returns></returns>
        public DataTable SelectByWorkResultTable(TOaEmployeResultorfaultVo tOaEmployeResultorfault,string strFileType)
        {
            return access.SelectByWorkResultTable(tOaEmployeResultorfault, strFileType);
        }
        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeResultorfault">对象</param>
        /// <returns></returns>
        public TOaEmployeResultorfaultVo SelectByObject(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            return access.SelectByObject(tOaEmployeResultorfault);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            return access.Create(tOaEmployeResultorfault);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeResultorfault">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            return access.Edit(tOaEmployeResultorfault);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeResultorfault_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeResultorfault_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeResultorfaultVo tOaEmployeResultorfault_UpdateSet, TOaEmployeResultorfaultVo tOaEmployeResultorfault_UpdateWhere)
        {
            return access.Edit(tOaEmployeResultorfault_UpdateSet, tOaEmployeResultorfault_UpdateWhere);
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
        public bool Delete(TOaEmployeResultorfaultVo tOaEmployeResultorfault)
        {
            return access.Delete(tOaEmployeResultorfault);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaEmployeResultorfault.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //员工编号
	  if (tOaEmployeResultorfault.EMPLOYEID.Trim() == "")
            {
                this.Tips.AppendLine("员工编号不能为空");
                return false;
            }
	  //工作成果
	  if (tOaEmployeResultorfault.WORKRESULT.Trim() == "")
            {
                this.Tips.AppendLine("工作成果不能为空");
                return false;
            }
	  //质量事故
	  if (tOaEmployeResultorfault.ACCIDENTS.Trim() == "")
            {
                this.Tips.AppendLine("质量事故不能为空");
                return false;
            }
	  //成果或事故，1成果，2事故
	  if (tOaEmployeResultorfault.RESULT_OR_ACCIDENT.Trim() == "")
            {
                this.Tips.AppendLine("成果或事故，1成果，2事故不能为空");
                return false;
            }
	  //备注1
	  if (tOaEmployeResultorfault.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaEmployeResultorfault.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaEmployeResultorfault.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaEmployeResultorfault.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaEmployeResultorfault.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
