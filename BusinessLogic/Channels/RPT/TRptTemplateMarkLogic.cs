using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.RPT;
using i3.DataAccess.Channels.RPT;

namespace i3.BusinessLogic.Channels.RPT
{
    /// <summary>
    /// 功能：模板标签表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptTemplateMarkLogic : LogicBase
    {

	TRptTemplateMarkVo tRptTemplateMark = new TRptTemplateMarkVo();
    TRptTemplateMarkAccess access;
		
	public TRptTemplateMarkLogic()
	{
		  access = new TRptTemplateMarkAccess();  
	}
		
	public TRptTemplateMarkLogic(TRptTemplateMarkVo _tRptTemplateMark)
	{
		tRptTemplateMark = _tRptTemplateMark;
		access = new TRptTemplateMarkAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptTemplateMark">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptTemplateMarkVo tRptTemplateMark)
        {
            return access.GetSelectResultCount(tRptTemplateMark);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptTemplateMarkVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptTemplateMark">对象条件</param>
        /// <returns>对象</returns>
        public TRptTemplateMarkVo Details(TRptTemplateMarkVo tRptTemplateMark)
        {
            return access.Details(tRptTemplateMark);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptTemplateMark">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptTemplateMarkVo> SelectByObject(TRptTemplateMarkVo tRptTemplateMark, int iIndex, int iCount)
        {
            return access.SelectByObject(tRptTemplateMark, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptTemplateMark">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptTemplateMarkVo tRptTemplateMark, int iIndex, int iCount)
        {
            return access.SelectByTable(tRptTemplateMark, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptTemplateMark"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptTemplateMarkVo tRptTemplateMark)
        {
            return access.SelectByTable(tRptTemplateMark);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptTemplateMark">对象</param>
        /// <returns></returns>
        public TRptTemplateMarkVo SelectByObject(TRptTemplateMarkVo tRptTemplateMark)
        {
            return access.SelectByObject(tRptTemplateMark);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptTemplateMarkVo tRptTemplateMark)
        {
            return access.Create(tRptTemplateMark);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptTemplateMark">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptTemplateMarkVo tRptTemplateMark)
        {
            return access.Edit(tRptTemplateMark);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptTemplateMark_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tRptTemplateMark_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptTemplateMarkVo tRptTemplateMark_UpdateSet, TRptTemplateMarkVo tRptTemplateMark_UpdateWhere)
        {
            return access.Edit(tRptTemplateMark_UpdateSet, tRptTemplateMark_UpdateWhere);
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
        public bool Delete(TRptTemplateMarkVo tRptTemplateMark)
        {
            return access.Delete(tRptTemplateMark);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tRptTemplateMark.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //标签ID
	  if (tRptTemplateMark.MARK_ID.Trim() == "")
            {
                this.Tips.AppendLine("标签ID不能为空");
                return false;
            }
	  //模板ID
	  if (tRptTemplateMark.TEMPLATE_ID.Trim() == "")
            {
                this.Tips.AppendLine("模板ID不能为空");
                return false;
            }
	  //备注
	  if (tRptTemplateMark.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }

            return true;
        }

    }
}
