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
    /// 功能：属性信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改日期：2012-11-12
    /// 修改人：潘德军
    /// 增加SelectByTableByJoin函数
    /// </summary>
    public class TBaseAttributeInfoLogic : LogicBase
    {

	TBaseAttributeInfoVo tBaseAttributeInfo = new TBaseAttributeInfoVo();
    TBaseAttributeInfoAccess access;
		
	public TBaseAttributeInfoLogic()
	{
		  access = new TBaseAttributeInfoAccess();  
	}
		
	public TBaseAttributeInfoLogic(TBaseAttributeInfoVo _tBaseAttributeInfo)
	{
		tBaseAttributeInfo = _tBaseAttributeInfo;
		access = new TBaseAttributeInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            return access.GetSelectResultCount(tBaseAttributeInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseAttributeInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseAttributeInfoVo Details(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            return access.Details(tBaseAttributeInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseAttributeInfoVo> SelectByObject(TBaseAttributeInfoVo tBaseAttributeInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseAttributeInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseAttributeInfoVo tBaseAttributeInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseAttributeInfo, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="strOUTLETPOINT_TYPE">排口点位类别</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableByJoin()
        {
            return access.SelectByTableByJoin();
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="strAttTypeID">属性类别ID</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableByJoin(string strAttTypeID)
        {
            return access.SelectByTableByJoin(strAttTypeID);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseAttributeInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            return access.SelectByTable(tBaseAttributeInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseAttributeInfo">对象</param>
        /// <returns></returns>
        public TBaseAttributeInfoVo SelectByObject(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            return access.SelectByObject(tBaseAttributeInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            return access.Create(tBaseAttributeInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            return access.Edit(tBaseAttributeInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseAttributeInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseAttributeInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttributeInfoVo tBaseAttributeInfo_UpdateSet, TBaseAttributeInfoVo tBaseAttributeInfo_UpdateWhere)
        {
            return access.Edit(tBaseAttributeInfo_UpdateSet, tBaseAttributeInfo_UpdateWhere);
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
        public bool Delete(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            return access.Delete(tBaseAttributeInfo);
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseAttributeInfoVo tBaseAttributeInfo, int iIndex, int iCount)
        {
            return access.SelectDefinedTadble(tBaseAttributeInfo, iIndex, iCount);
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseAttributeInfoVo tBaseAttributeInfo)
        {
            return access.GetSelecDefinedtResultCount(tBaseAttributeInfo);
        }

        /// <summary>
        /// 获取动态属性数据 Create By weilin 2014-03-19
        /// </summary>
        /// <param name="strType_ID"></param>
        /// <returns></returns>
        public DataTable GetAttDate(string strType_ID)
        {
            return access.GetAttDate(strType_ID);
        }
        /// <summary>
        /// 获取动态属性数据值 Create By weilin 2014-03-19
        /// </summary>
        /// <param name="strType_ID"></param>
        /// <param name="strPoint_ID"></param>
        /// <returns></returns>
        public DataTable GetAttValue(string strType_ID, string strPoint_ID)
        {
            return access.GetAttValue(strType_ID, strPoint_ID);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseAttributeInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //属性名称
	  if (tBaseAttributeInfo.ATTRIBUTE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("属性名称不能为空");
                return false;
            }
	  //控件ID
	  if (tBaseAttributeInfo.CONTROL_ID.Trim() == "")
            {
                this.Tips.AppendLine("控件ID不能为空");
                return false;
            }
	  //控件名称
	  if (tBaseAttributeInfo.CONTROL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("控件名称不能为空");
                return false;
            }
	  //控件宽度
	  if (tBaseAttributeInfo.WIDTH.Trim() == "")
            {
                this.Tips.AppendLine("控件宽度不能为空");
                return false;
            }
	  //是否可编辑
	  if (tBaseAttributeInfo.ENABLE.Trim() == "")
            {
                this.Tips.AppendLine("是否可编辑不能为空");
                return false;
            }
	  //可否为空
	  if (tBaseAttributeInfo.IS_NULL.Trim() == "")
            {
                this.Tips.AppendLine("可否为空不能为空");
                return false;
            }
	  //最大长度
	  if (tBaseAttributeInfo.MAX_LENGTH.Trim() == "")
            {
                this.Tips.AppendLine("最大长度不能为空");
                return false;
            }
	  //字典项
	  if (tBaseAttributeInfo.DICTIONARY.Trim() == "")
            {
                this.Tips.AppendLine("字典项不能为空");
                return false;
            }
	  //数据库表名
	  if (tBaseAttributeInfo.TABLE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("数据库表名不能为空");
                return false;
            }
	  //文本字段
	  if (tBaseAttributeInfo.TEXT_FIELD.Trim() == "")
            {
                this.Tips.AppendLine("文本字段不能为空");
                return false;
            }
	  //值字段
	  if (tBaseAttributeInfo.VALUE_FIELD.Trim() == "")
            {
                this.Tips.AppendLine("值字段不能为空");
                return false;
            }
	  //排序
	  if (tBaseAttributeInfo.SN.Trim() == "")
            {
                this.Tips.AppendLine("排序不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tBaseAttributeInfo.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //备注1
	  if (tBaseAttributeInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseAttributeInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseAttributeInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseAttributeInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseAttributeInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
