using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.DataAccess.Channels.Base.DynamicAttribute;

namespace i3.BusinessLogic.Channels.Base.DynamicAttribute
{
    /// <summary>
    /// 功能：属性类别表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttributeTypeLogic : LogicBase
    {

	TBaseAttributeTypeVo tBaseAttributeType = new TBaseAttributeTypeVo();
    TBaseAttributeTypeAccess access;
		
	public TBaseAttributeTypeLogic()
	{
		  access = new TBaseAttributeTypeAccess();  
	}
		
	public TBaseAttributeTypeLogic(TBaseAttributeTypeVo _tBaseAttributeType)
	{
		tBaseAttributeType = _tBaseAttributeType;
		access = new TBaseAttributeTypeAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseAttributeTypeVo tBaseAttributeType)
        {
            return access.GetSelectResultCount(tBaseAttributeType);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseAttributeTypeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseAttributeType">对象条件</param>
        /// <returns>对象</returns>
        public TBaseAttributeTypeVo Details(TBaseAttributeTypeVo tBaseAttributeType)
        {
            return access.Details(tBaseAttributeType);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseAttributeTypeVo> SelectByObject(TBaseAttributeTypeVo tBaseAttributeType, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseAttributeType, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseAttributeTypeVo tBaseAttributeType, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseAttributeType, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseAttributeType"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseAttributeTypeVo tBaseAttributeType)
        {
            return access.SelectByTable(tBaseAttributeType);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseAttributeType">对象</param>
        /// <returns></returns>
        public TBaseAttributeTypeVo SelectByObject(TBaseAttributeTypeVo tBaseAttributeType)
        {
            return access.SelectByObject(tBaseAttributeType);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseAttributeTypeVo tBaseAttributeType)
        {
            return access.Create(tBaseAttributeType);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeType">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeTypeVo tBaseAttributeType)
        {
            return access.Edit(tBaseAttributeType);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeType_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseAttributeType_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeTypeVo tBaseAttributeType_UpdateSet, TBaseAttributeTypeVo tBaseAttributeType_UpdateWhere)
        {
            return access.Edit(tBaseAttributeType_UpdateSet, tBaseAttributeType_UpdateWhere);
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
        public bool Delete(TBaseAttributeTypeVo tBaseAttributeType)
        {
            return access.Delete(tBaseAttributeType);
        }
        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseAttributeTypeVo tBaseAttributeType, int iIndex, int iCount)
        {
            return access.SelectDefinedTadble(tBaseAttributeType, iIndex, iCount);
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseAttributeTypeVo tBaseAttributeType)
        {
            return access.GetSelecDefinedtResultCount(tBaseAttributeType);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseAttributeType.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
	  if (tBaseAttributeType.MONITOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）不能为空");
                return false;
            }
	  //类别名称
	  if (tBaseAttributeType.SORT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("类别名称不能为空");
                return false;
            }
	  //添加人
	  if (tBaseAttributeType.INSERT_BY.Trim() == "")
            {
                this.Tips.AppendLine("添加人不能为空");
                return false;
            }
	  //添加时间
	  if (tBaseAttributeType.INSERT_DATE.Trim() == "")
            {
                this.Tips.AppendLine("添加时间不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tBaseAttributeType.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //备注1
	  if (tBaseAttributeType.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseAttributeType.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseAttributeType.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseAttributeType.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseAttributeType.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
