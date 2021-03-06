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
    /// 功能：员工工作经历
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeWorkhistoryLogic : LogicBase
    {

	TOaEmployeWorkhistoryVo tOaEmployeWorkhistory = new TOaEmployeWorkhistoryVo();
    TOaEmployeWorkhistoryAccess access;
		
	public TOaEmployeWorkhistoryLogic()
	{
		  access = new TOaEmployeWorkhistoryAccess();  
	}
		
	public TOaEmployeWorkhistoryLogic(TOaEmployeWorkhistoryVo _tOaEmployeWorkhistory)
	{
		tOaEmployeWorkhistory = _tOaEmployeWorkhistory;
		access = new TOaEmployeWorkhistoryAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            return access.GetSelectResultCount(tOaEmployeWorkhistory);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeWorkhistoryVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeWorkhistoryVo Details(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            return access.Details(tOaEmployeWorkhistory);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeWorkhistoryVo> SelectByObject(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaEmployeWorkhistory, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaEmployeWorkhistory, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeWorkhistory"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            return access.SelectByTable(tOaEmployeWorkhistory);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">对象</param>
        /// <returns></returns>
        public TOaEmployeWorkhistoryVo SelectByObject(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            return access.SelectByObject(tOaEmployeWorkhistory);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            return access.Create(tOaEmployeWorkhistory);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeWorkhistory">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            return access.Edit(tOaEmployeWorkhistory);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeWorkhistory_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeWorkhistory_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory_UpdateSet, TOaEmployeWorkhistoryVo tOaEmployeWorkhistory_UpdateWhere)
        {
            return access.Edit(tOaEmployeWorkhistory_UpdateSet, tOaEmployeWorkhistory_UpdateWhere);
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
        public bool Delete(TOaEmployeWorkhistoryVo tOaEmployeWorkhistory)
        {
            return access.Delete(tOaEmployeWorkhistory);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaEmployeWorkhistory.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //员工编号
	  if (tOaEmployeWorkhistory.EMPLOYEID.Trim() == "")
            {
                this.Tips.AppendLine("员工编号不能为空");
                return false;
            }
	  //所在单位
	  if (tOaEmployeWorkhistory.WORKCOMPANY.Trim() == "")
            {
                this.Tips.AppendLine("所在单位不能为空");
                return false;
            }
	  //时间
	  if (tOaEmployeWorkhistory.WORKBEGINDATE.Trim() == "")
            {
                this.Tips.AppendLine("开始时间不能为空");
                return false;
            }
      //时间
      if (tOaEmployeWorkhistory.WORKENDDATE.Trim() == "")
      {
          this.Tips.AppendLine("截止时间不能为空");
          return false;
      }
	  //岗位
	  if (tOaEmployeWorkhistory.POSITION.Trim() == "")
            {
                this.Tips.AppendLine("岗位不能为空");
                return false;
            }
	  //备注1
	  if (tOaEmployeWorkhistory.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaEmployeWorkhistory.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaEmployeWorkhistory.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaEmployeWorkhistory.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaEmployeWorkhistory.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
