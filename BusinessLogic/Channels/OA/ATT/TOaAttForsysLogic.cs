using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.ATT;
using i3.DataAccess.Channels.OA.ATT;

namespace i3.BusinessLogic.Channels.OA.ATT
{
    /// <summary>
    /// 功能：附件业务登记
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaAttForsysLogic : LogicBase
    {

	TOaAttForsysVo tOaAttForsys = new TOaAttForsysVo();
    TOaAttForsysAccess access;
		
	public TOaAttForsysLogic()
	{
		  access = new TOaAttForsysAccess();  
	}
		
	public TOaAttForsysLogic(TOaAttForsysVo _tOaAttForsys)
	{
		tOaAttForsys = _tOaAttForsys;
		access = new TOaAttForsysAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaAttForsys">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaAttForsysVo tOaAttForsys)
        {
            return access.GetSelectResultCount(tOaAttForsys);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaAttForsysVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaAttForsys">对象条件</param>
        /// <returns>对象</returns>
        public TOaAttForsysVo Details(TOaAttForsysVo tOaAttForsys)
        {
            return access.Details(tOaAttForsys);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaAttForsys">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaAttForsysVo> SelectByObject(TOaAttForsysVo tOaAttForsys, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaAttForsys, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaAttForsys">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaAttForsysVo tOaAttForsys, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaAttForsys, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaAttForsys"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaAttForsysVo tOaAttForsys)
        {
            return access.SelectByTable(tOaAttForsys);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaAttForsys">对象</param>
        /// <returns></returns>
        public TOaAttForsysVo SelectByObject(TOaAttForsysVo tOaAttForsys)
        {
            return access.SelectByObject(tOaAttForsys);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaAttForsysVo tOaAttForsys)
        {
            return access.Create(tOaAttForsys);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaAttForsys">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaAttForsysVo tOaAttForsys)
        {
            return access.Edit(tOaAttForsys);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaAttForsys_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaAttForsys_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaAttForsysVo tOaAttForsys_UpdateSet, TOaAttForsysVo tOaAttForsys_UpdateWhere)
        {
            return access.Edit(tOaAttForsys_UpdateSet, tOaAttForsys_UpdateWhere);
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
        public bool Delete(TOaAttForsysVo tOaAttForsys)
        {
            return access.Delete(tOaAttForsys);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaAttForsys.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //业务ID
	  if (tOaAttForsys.BUSINESSID.Trim() == "")
            {
                this.Tips.AppendLine("业务ID不能为空");
                return false;
            }
	  //附件名称
	  if (tOaAttForsys.ATTACHEMNTNAME.Trim() == "")
            {
                this.Tips.AppendLine("附件名称不能为空");
                return false;
            }
	  //附件路径
	  if (tOaAttForsys.ATTACHPATH.Trim() == "")
            {
                this.Tips.AppendLine("附件路径不能为空");
                return false;
            }
	  //备注
	  if (tOaAttForsys.REMARKS.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }

            return true;
        }

    }
}
