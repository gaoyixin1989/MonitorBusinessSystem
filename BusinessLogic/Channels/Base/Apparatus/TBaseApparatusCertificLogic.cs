using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.DataAccess.Channels.Base.Apparatus;

namespace i3.BusinessLogic.Channels.Base.Apparatus
{
    /// <summary>
    /// 功能：仪器鉴定证书
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusCertificLogic : LogicBase
    {

	TBaseApparatusCertificVo tBaseApparatusCertific = new TBaseApparatusCertificVo();
    TBaseApparatusCertificAccess access;
		
	public TBaseApparatusCertificLogic()
	{
		  access = new TBaseApparatusCertificAccess();  
	}
		
	public TBaseApparatusCertificLogic(TBaseApparatusCertificVo _tBaseApparatusCertific)
	{
		tBaseApparatusCertific = _tBaseApparatusCertific;
		access = new TBaseApparatusCertificAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            return access.GetSelectResultCount(tBaseApparatusCertific);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseApparatusCertificVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象条件</param>
        /// <returns>对象</returns>
        public TBaseApparatusCertificVo Details(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            return access.Details(tBaseApparatusCertific);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseApparatusCertificVo> SelectByObject(TBaseApparatusCertificVo tBaseApparatusCertific, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseApparatusCertific, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusCertificVo tBaseApparatusCertific, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseApparatusCertific, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseApparatusCertific"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            return access.SelectByTable(tBaseApparatusCertific);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseApparatusCertific">对象</param>
        /// <returns></returns>
        public TBaseApparatusCertificVo SelectByObject(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            return access.SelectByObject(tBaseApparatusCertific);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            return access.Create(tBaseApparatusCertific);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusCertific">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            return access.Edit(tBaseApparatusCertific);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusCertific_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseApparatusCertific_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusCertificVo tBaseApparatusCertific_UpdateSet, TBaseApparatusCertificVo tBaseApparatusCertific_UpdateWhere)
        {
            return access.Edit(tBaseApparatusCertific_UpdateSet, tBaseApparatusCertific_UpdateWhere);
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
        public bool Delete(TBaseApparatusCertificVo tBaseApparatusCertific)
        {
            return access.Delete(tBaseApparatusCertific);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseApparatusCertific.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //检定名称
	  if (tBaseApparatusCertific.APPRAISAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("检定名称不能为空");
                return false;
            }
	  //仪器ID
	  if (tBaseApparatusCertific.APPARATUS_ID.Trim() == "")
            {
                this.Tips.AppendLine("仪器ID不能为空");
                return false;
            }
	  //仪器检定时间
	  if (tBaseApparatusCertific.APPRAISAL_DATE.Trim() == "")
            {
                this.Tips.AppendLine("仪器检定时间不能为空");
                return false;
            }
	  //检定证书路径
	  if (tBaseApparatusCertific.APPRAISAL_URL.Trim() == "")
            {
                this.Tips.AppendLine("检定证书路径不能为空");
                return false;
            }
	  //备注1
	  if (tBaseApparatusCertific.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseApparatusCertific.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseApparatusCertific.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseApparatusCertific.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseApparatusCertific.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
