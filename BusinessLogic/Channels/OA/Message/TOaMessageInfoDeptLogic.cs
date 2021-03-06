using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.Message;
using i3.DataAccess.Channels.OA.Message;

namespace i3.BusinessLogic.Channels.OA.Message
{
    /// <summary>
    /// 功能：消息接收部门
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageInfoDeptLogic : LogicBase
    {

	TOaMessageInfoDeptVo tOaMessageInfoDept = new TOaMessageInfoDeptVo();
    TOaMessageInfoDeptAccess access;
		
	public TOaMessageInfoDeptLogic()
	{
		  access = new TOaMessageInfoDeptAccess();  
	}
		
	public TOaMessageInfoDeptLogic(TOaMessageInfoDeptVo _tOaMessageInfoDept)
	{
		tOaMessageInfoDept = _tOaMessageInfoDept;
		access = new TOaMessageInfoDeptAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            return access.GetSelectResultCount(tOaMessageInfoDept);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageInfoDeptVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageInfoDeptVo Details(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            return access.Details(tOaMessageInfoDept);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageInfoDeptVo> SelectByObject(TOaMessageInfoDeptVo tOaMessageInfoDept, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaMessageInfoDept, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageInfoDeptVo tOaMessageInfoDept, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaMessageInfoDept, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageInfoDept"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            return access.SelectByTable(tOaMessageInfoDept);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageInfoDept">对象</param>
        /// <returns></returns>
        public TOaMessageInfoDeptVo SelectByObject(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            return access.SelectByObject(tOaMessageInfoDept);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            return access.Create(tOaMessageInfoDept);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoDept">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            return access.Edit(tOaMessageInfoDept);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageInfoDept_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageInfoDept_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageInfoDeptVo tOaMessageInfoDept_UpdateSet, TOaMessageInfoDeptVo tOaMessageInfoDept_UpdateWhere)
        {
            return access.Edit(tOaMessageInfoDept_UpdateSet, tOaMessageInfoDept_UpdateWhere);
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
        public bool Delete(TOaMessageInfoDeptVo tOaMessageInfoDept)
        {
            return access.Delete(tOaMessageInfoDept);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tOaMessageInfoDept.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //消息编号
	  if (tOaMessageInfoDept.MESSAGE_ID.Trim() == "")
            {
                this.Tips.AppendLine("消息编号不能为空");
                return false;
            }
	  //部门ID
	  if (tOaMessageInfoDept.DEPT_ID.Trim() == "")
            {
                this.Tips.AppendLine("部门ID不能为空");
                return false;
            }
	  //备注1
	  if (tOaMessageInfoDept.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaMessageInfoDept.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaMessageInfoDept.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaMessageInfoDept.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaMessageInfoDept.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
