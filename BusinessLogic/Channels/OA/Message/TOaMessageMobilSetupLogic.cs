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
    /// 功能：短信设置
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaMessageMobilSetupLogic : LogicBase
    {

	TOaMessageMobilSetupVo tOaMessageMobilSetup = new TOaMessageMobilSetupVo();
    TOaMessageMobilSetupAccess access;
		
	public TOaMessageMobilSetupLogic()
	{
		  access = new TOaMessageMobilSetupAccess();  
	}
		
	public TOaMessageMobilSetupLogic(TOaMessageMobilSetupVo _tOaMessageMobilSetup)
	{
		tOaMessageMobilSetup = _tOaMessageMobilSetup;
		access = new TOaMessageMobilSetupAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            return access.GetSelectResultCount(tOaMessageMobilSetup);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaMessageMobilSetupVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象条件</param>
        /// <returns>对象</returns>
        public TOaMessageMobilSetupVo Details(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            return access.Details(tOaMessageMobilSetup);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaMessageMobilSetupVo> SelectByObject(TOaMessageMobilSetupVo tOaMessageMobilSetup, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaMessageMobilSetup, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaMessageMobilSetupVo tOaMessageMobilSetup, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaMessageMobilSetup, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaMessageMobilSetup"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            return access.SelectByTable(tOaMessageMobilSetup);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaMessageMobilSetup">对象</param>
        /// <returns></returns>
        public TOaMessageMobilSetupVo SelectByObject(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            return access.SelectByObject(tOaMessageMobilSetup);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            return access.Create(tOaMessageMobilSetup);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageMobilSetup">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            return access.Edit(tOaMessageMobilSetup);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaMessageMobilSetup_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaMessageMobilSetup_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaMessageMobilSetupVo tOaMessageMobilSetup_UpdateSet, TOaMessageMobilSetupVo tOaMessageMobilSetup_UpdateWhere)
        {
            return access.Edit(tOaMessageMobilSetup_UpdateSet, tOaMessageMobilSetup_UpdateWhere);
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
        public bool Delete(TOaMessageMobilSetupVo tOaMessageMobilSetup)
        {
            return access.Delete(tOaMessageMobilSetup);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tOaMessageMobilSetup.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //手机短信适用模块编码（按系统分配的1.2.4.8）
	  if (tOaMessageMobilSetup.MOBIL_SYS_CODE.Trim() == "")
            {
                this.Tips.AppendLine("手机短信适用模块编码（按系统分配的1.2.4.8）不能为空");
                return false;
            }
	  //备份1
	  if (tOaMessageMobilSetup.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备份1不能为空");
                return false;
            }
	  //备份2
	  if (tOaMessageMobilSetup.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备份2不能为空");
                return false;
            }
	  //备份3
	  if (tOaMessageMobilSetup.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备份3不能为空");
                return false;
            }
	  //备份4
	  if (tOaMessageMobilSetup.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备份4不能为空");
                return false;
            }
	  //备份5
	  if (tOaMessageMobilSetup.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备份5不能为空");
                return false;
            }

            return true;
        }

    }
}
