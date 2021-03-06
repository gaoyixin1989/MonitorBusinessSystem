using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Item;
using i3.DataAccess.Channels.Base.Item;

namespace i3.BusinessLogic.Channels.Base.Item
{
    /// <summary>
    /// 功能：监测项目管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemInfoLogic : LogicBase
    {

	TBaseItemInfoVo tBaseItemInfo = new TBaseItemInfoVo();
    TBaseItemInfoAccess access;
		
	public TBaseItemInfoLogic()
	{
		  access = new TBaseItemInfoAccess();  
	}
		
	public TBaseItemInfoLogic(TBaseItemInfoVo _tBaseItemInfo)
	{
		tBaseItemInfo = _tBaseItemInfo;
		access = new TBaseItemInfoAccess();
	}

        
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="strSrhMONITOR_ID">监测类别</param>
        /// <param name="strSrhItemName">监测项目</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForSelectItem_inBag(string strSrhMONITOR_ID,string strSrhItemName, int iIndex, int iCount)
        {
            return access.SelectByTable_ForSelectItem_inBag(strSrhMONITOR_ID,strSrhItemName, iIndex, iCount);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="strSrhMONITOR_ID">监测类别</param>
        /// <param name="strSrhItemName">监测项目</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForSelectItem_inBag(string strSrhMONITOR_ID, string strSrhItemName)
        {
            return access.GetSelectResultCount_ForSelectItem_inBag(strSrhMONITOR_ID, strSrhItemName);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseItemInfoVo tBaseItemInfo)
        {
            return access.GetSelectResultCount(tBaseItemInfo);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ItemOfBag(string strItemBagID)
        {
            return access.GetSelectResultCount_ItemOfBag(strItemBagID);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseItemInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseItemInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseItemInfoVo Details(TBaseItemInfoVo tBaseItemInfo)
        {
            return access.Details(tBaseItemInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseItemInfoVo> SelectByObject(TBaseItemInfoVo tBaseItemInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseItemInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseItemInfoVo tBaseItemInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseItemInfo, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ByJoinMonitorType(TBaseItemInfoVo tBaseItemInfo, int iIndex, int iCount)
        {
            return access.SelectByTable_ByJoinMonitorType(tBaseItemInfo, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ItemOfBag(string strItemBagID, int iIndex, int iCount)
        {
            return access.SelectByTable_ItemOfBag(strItemBagID, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseItemInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseItemInfoVo tBaseItemInfo)
        {
            return access.SelectByTable(tBaseItemInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <returns></returns>
        public TBaseItemInfoVo SelectByObject(TBaseItemInfoVo tBaseItemInfo)
        {
            return access.SelectByObject(tBaseItemInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseItemInfoVo tBaseItemInfo)
        {
            return access.Create(tBaseItemInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemInfoVo tBaseItemInfo)
        {
            return access.Edit(tBaseItemInfo);
        }

        /// <summary>
        /// ph值、溶解氧、电导率，现场项目转变成实验室项目
        /// </summary>
        /// huangjinjun add 2016.1.25
        /// <returns>是否成功</returns>
        public bool EditItemTypeFX()
        {
            return access.EditItemTypeFX();
        }

        /// <summary>
        /// ph值、溶解氧、电导率，实验室项目转变成现场项目
        /// </summary>
        /// huangjinjun add 2016.1.25
        /// <returns>是否成功</returns>
        public bool EditItemTypeXC()
        {
            return access.EditItemTypeXC();
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseItemInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemInfoVo tBaseItemInfo_UpdateSet, TBaseItemInfoVo tBaseItemInfo_UpdateWhere)
        {
            return access.Edit(tBaseItemInfo_UpdateSet, tBaseItemInfo_UpdateWhere);
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
        public bool Delete(TBaseItemInfoVo tBaseItemInfo)
        {
            return access.Delete(tBaseItemInfo);
        }

        /// <summary>
        /// 根据样品ID(质控类型)查找项目-非现场项目
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public DataTable SelectItemForQC(string strSampleID, string strQcType)
        {
            return access.SelectItemForQC(strSampleID, strQcType);
        }

        /// <summary>
        /// 获取样品的现场加标信息
        /// </summary>
        /// <param name="strSampleID"></param>
        /// <param name="strQcType"></param>
        /// <returns></returns>
        public DataTable SelectQcAddItem(string strSampleID, string strQcType)
        {
            return access.SelectQcAddItem(strSampleID, strQcType);
        }
        /// <summary>
        /// 获取样品的密码盲样信息
        /// </summary>
        /// <param name="strSampleID"></param>
        /// <param name="strQcType"></param>
        /// <returns></returns>
        public DataTable SelectQcBlindItem(string strSampleID, string strQcType)
        {
            return access.SelectQcBlindItem(strSampleID, strQcType);
        }

        /// <summary>
        /// 采样时，修改现场加标和盲样的方法
        /// </summary>
        /// <param name="strTable"></param>
        /// <param name="strUpdateCell"></param>
        /// <param name="strUpdateCellValue"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        public bool UpdateQcInfo(string strTable, string strUpdateCell, string strUpdateCellValue, string strID)
        {
            return access.UpdateQcInfo(strTable, strUpdateCell, strUpdateCellValue, strID);
        }

        /// <summary>
        /// 为监测类别复制监测项目
        /// </summary>
        /// <param name="tBaseItemInfo"></param>
        /// <returns></returns>
        public bool CopySameMonitorItemInfor(TBaseItemInfoVo tBaseItemInfo, string strToId)
        {
            return access.CopySameMonitorItemInfor(tBaseItemInfo, strToId);
        }
         /// <summary>
        /// 设置默认现场采样仪器
        /// </summary>
        /// <param name="strItemId">监测项目ID</param>
        /// <param name="strSamplingInstrumentId">现场采样仪器ID</param>
        /// <returns></returns>
        public bool setItemSamplingInstrumentDefault(string strItemId, string strSamplingInstrumentId)
        {
            return access.setItemSamplingInstrumentDefault(strItemId, strSamplingInstrumentId);
        }
                /// <summary>
        /// 获取监测项目要用到的原始记录表的表名称和表ID
        /// </summary>
        /// <param name="strLikeWhere"></param>
        /// <returns></returns>
        public DataTable GetSysDutyCataLogTableInfor(string strLikeWhere) {
            return access.GetSysDutyCataLogTableInfor(strLikeWhere);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseItemInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测项目名称
	  if (tBaseItemInfo.ITEM_NAME.Trim() == "")
            {
                this.Tips.AppendLine("监测项目名称不能为空");
                return false;
            }
	  //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
	  if (tBaseItemInfo.MONITOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）不能为空");
                return false;
            }
	  //废水类，现场室填写项目
	  if (tBaseItemInfo.IS_SAMPLEDEPT.Trim() == "")
            {
                this.Tips.AppendLine("废水类，现场室填写项目不能为空");
                return false;
            }
	  //是否包含监测子项
	  if (tBaseItemInfo.HAS_SUB_ITEM.Trim() == "")
            {
                this.Tips.AppendLine("是否包含监测子项不能为空");
                return false;
            }
	  //是否是监测子项
	  if (tBaseItemInfo.IS_SUB.Trim() == "")
            {
                this.Tips.AppendLine("是否是监测子项不能为空");
                return false;
            }
	  //监测单价
	  if (tBaseItemInfo.CHARGE.Trim() == "")
            {
                this.Tips.AppendLine("监测单价不能为空");
                return false;
            }
	  //开机费用
	  if (tBaseItemInfo.TEST_POWER_FEE.Trim() == "")
            {
                this.Tips.AppendLine("开机费用不能为空");
                return false;
            }
	  //实验室认可
	  if (tBaseItemInfo.LAB_CERTIFICATE.Trim() == "")
            {
                this.Tips.AppendLine("实验室认可不能为空");
                return false;
            }
	  //计量认可
	  if (tBaseItemInfo.MEASURE_CERTIFICATE.Trim() == "")
            {
                this.Tips.AppendLine("计量认可不能为空");
                return false;
            }
	  //平行上限
	  if (tBaseItemInfo.TWIN_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("平行上限不能为空");
                return false;
            }
	  //加标下限
	  if (tBaseItemInfo.ADD_MIN.Trim() == "")
            {
                this.Tips.AppendLine("加标下限不能为空");
                return false;
            }
	  //加标上限
	  if (tBaseItemInfo.ADD_MAX.Trim() == "")
            {
                this.Tips.AppendLine("加标上限不能为空");
                return false;
            }
	  //序号
	  if (tBaseItemInfo.ORDER_NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
	  //0为在使用、1为停用
      if (tBaseItemInfo.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("0为在使用、1为停用不能为空");
                return false;
            }
	  //备注1
	  if (tBaseItemInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseItemInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseItemInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseItemInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseItemInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
