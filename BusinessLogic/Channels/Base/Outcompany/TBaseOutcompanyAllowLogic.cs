using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Outcompany;
using i3.DataAccess.Channels.Base.Outcompany;

namespace i3.BusinessLogic.Channels.Base.Outcompany
{
    /// <summary>
    /// 功能：分包单位资质
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseOutcompanyAllowLogic : LogicBase
    {

	TBaseOutcompanyAllowVo tBaseOutcompanyAllow = new TBaseOutcompanyAllowVo();
    TBaseOutcompanyAllowAccess access;
		
	public TBaseOutcompanyAllowLogic()
	{
		  access = new TBaseOutcompanyAllowAccess();  
	}
		
	public TBaseOutcompanyAllowLogic(TBaseOutcompanyAllowVo _tBaseOutcompanyAllow)
	{
		tBaseOutcompanyAllow = _tBaseOutcompanyAllow;
		access = new TBaseOutcompanyAllowAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            return access.GetSelectResultCount(tBaseOutcompanyAllow);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseOutcompanyAllowVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象条件</param>
        /// <returns>对象</returns>
        public TBaseOutcompanyAllowVo Details(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            return access.Details(tBaseOutcompanyAllow);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseOutcompanyAllowVo> SelectByObject(TBaseOutcompanyAllowVo tBaseOutcompanyAllow, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseOutcompanyAllow, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseOutcompanyAllowVo tBaseOutcompanyAllow, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseOutcompanyAllow, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseOutcompanyAllow"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            return access.SelectByTable(tBaseOutcompanyAllow);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemAnalysis">对象</param>
        /// <returns>返回行数</returns>
        public DataTable SelectByTable_ByJoin(TBaseOutcompanyAllowVo tBaseOutcompanyAllow, int iIndex, int iCount)
        {
            return access.SelectByTable_ByJoin(tBaseOutcompanyAllow, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">对象</param>
        /// <returns></returns>
        public TBaseOutcompanyAllowVo SelectByObject(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            return access.SelectByObject(tBaseOutcompanyAllow);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            return access.Create(tBaseOutcompanyAllow);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseOutcompanyAllow">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            return access.Edit(tBaseOutcompanyAllow);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseOutcompanyAllow_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseOutcompanyAllow_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseOutcompanyAllowVo tBaseOutcompanyAllow_UpdateSet, TBaseOutcompanyAllowVo tBaseOutcompanyAllow_UpdateWhere)
        {
            return access.Edit(tBaseOutcompanyAllow_UpdateSet, tBaseOutcompanyAllow_UpdateWhere);
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
        public bool Delete(TBaseOutcompanyAllowVo tBaseOutcompanyAllow)
        {
            return access.Delete(tBaseOutcompanyAllow);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseOutcompanyAllow.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //分包单位ID
	  if (tBaseOutcompanyAllow.OUTCOMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("分包单位ID不能为空");
                return false;
            }
	  //资质变化情况
	  if (tBaseOutcompanyAllow.QUALIFICATIONS_INFO.Trim() == "")
            {
                this.Tips.AppendLine("资质变化情况不能为空");
                return false;
            }
	  //主要项目情况
	  if (tBaseOutcompanyAllow.PROJECT_INFO.Trim() == "")
            {
                this.Tips.AppendLine("主要项目情况不能为空");
                return false;
            }
	  //质保体系情况(1,符合国标；2，符合地标)
	  if (tBaseOutcompanyAllow.QC_INFO.Trim() == "")
            {
                this.Tips.AppendLine("质保体系情况(1,符合国标；2，符合地标)不能为空");
                return false;
            }
	  //经办人
	  if (tBaseOutcompanyAllow.CHECK_USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("经办人不能为空");
                return false;
            }
	  //经办日期
	  if (tBaseOutcompanyAllow.CHECK_DATE.Trim() == "")
            {
                this.Tips.AppendLine("经办日期不能为空");
                return false;
            }
	  //备注
	  if (tBaseOutcompanyAllow.INFO.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
	  //附件ID
	  if (tBaseOutcompanyAllow.ATT_ID.Trim() == "")
            {
                this.Tips.AppendLine("附件ID不能为空");
                return false;
            }
	  //委托完成情况
	  if (tBaseOutcompanyAllow.COMPLETE_INFO.Trim() == "")
            {
                this.Tips.AppendLine("委托完成情况不能为空");
                return false;
            }
	  //是否通过评审
	  if (tBaseOutcompanyAllow.IS_OK.Trim() == "")
            {
                this.Tips.AppendLine("是否通过评审不能为空");
                return false;
            }
	  //评审意见
	  if (tBaseOutcompanyAllow.APP_INFO.Trim() == "")
            {
                this.Tips.AppendLine("评审意见不能为空");
                return false;
            }
	  //评审人ID
	  if (tBaseOutcompanyAllow.APP_USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("评审人ID不能为空");
                return false;
            }
	  //评审时间
	  if (tBaseOutcompanyAllow.APP_DATE.Trim() == "")
            {
                this.Tips.AppendLine("评审时间不能为空");
                return false;
            }
	  //备注1
	  if (tBaseOutcompanyAllow.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseOutcompanyAllow.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseOutcompanyAllow.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseOutcompanyAllow.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseOutcompanyAllow.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
