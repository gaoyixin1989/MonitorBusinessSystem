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
    /// 功能：员工考核历史
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeExaminehistoryLogic : LogicBase
    {

	TOaEmployeExaminehistoryVo tOaEmployeExaminehistory = new TOaEmployeExaminehistoryVo();
    TOaEmployeExaminehistoryAccess access;
		
	public TOaEmployeExaminehistoryLogic()
	{
		  access = new TOaEmployeExaminehistoryAccess();  
	}
		
	public TOaEmployeExaminehistoryLogic(TOaEmployeExaminehistoryVo _tOaEmployeExaminehistory)
	{
		tOaEmployeExaminehistory = _tOaEmployeExaminehistory;
		access = new TOaEmployeExaminehistoryAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            return access.GetSelectResultCount(tOaEmployeExaminehistory);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeExaminehistoryVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeExaminehistoryVo Details(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            return access.Details(tOaEmployeExaminehistory);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeExaminehistoryVo> SelectByObject(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaEmployeExaminehistory, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaEmployeExaminehistory, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeExaminehistory"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            return access.SelectByTable(tOaEmployeExaminehistory);
        }

        /// <summary>
        /// 获取证书信息及其附件信息
        /// </summary>
        /// <param name="tOaEmployeQualification"></param>
        /// <param name="strFileType"></param>
        /// <returns></returns>
        public DataTable SelectByUnionAttTable(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory, string strFileType)
        {
            return access.SelectByUnionAttTable(tOaEmployeExaminehistory, strFileType);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">对象</param>
        /// <returns></returns>
        public TOaEmployeExaminehistoryVo SelectByObject(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            return access.SelectByObject(tOaEmployeExaminehistory);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            return access.Create(tOaEmployeExaminehistory);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeExaminehistory">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            return access.Edit(tOaEmployeExaminehistory);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeExaminehistory_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeExaminehistory_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory_UpdateSet, TOaEmployeExaminehistoryVo tOaEmployeExaminehistory_UpdateWhere)
        {
            return access.Edit(tOaEmployeExaminehistory_UpdateSet, tOaEmployeExaminehistory_UpdateWhere);
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
        public bool Delete(TOaEmployeExaminehistoryVo tOaEmployeExaminehistory)
        {
            return access.Delete(tOaEmployeExaminehistory);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaEmployeExaminehistory.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //员工编号
	  if (tOaEmployeExaminehistory.EMPLOYEID.Trim() == "")
            {
                this.Tips.AppendLine("员工编号不能为空");
                return false;
            }
	  //所在单位
	  if (tOaEmployeExaminehistory.ATT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("所在单位不能为空");
                return false;
            }
	  //附件路径
	  if (tOaEmployeExaminehistory.ATT_URL.Trim() == "")
            {
                this.Tips.AppendLine("附件路径不能为空");
                return false;
            }
	  //附件说明
	  if (tOaEmployeExaminehistory.ATT_INFO.Trim() == "")
            {
                this.Tips.AppendLine("附件说明不能为空");
                return false;
            }
	  //备注1
	  if (tOaEmployeExaminehistory.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaEmployeExaminehistory.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaEmployeExaminehistory.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaEmployeExaminehistory.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaEmployeExaminehistory.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
