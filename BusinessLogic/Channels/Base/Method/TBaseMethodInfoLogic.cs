using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Method;
using i3.DataAccess.Channels.Base.Method;

namespace i3.BusinessLogic.Channels.Base.Method
{
    /// <summary>
    /// 功能：方法依据管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseMethodInfoLogic : LogicBase
    {

	TBaseMethodInfoVo tBaseMethodInfo = new TBaseMethodInfoVo();
    TBaseMethodInfoAccess access;
		
	public TBaseMethodInfoLogic()
	{
		  access = new TBaseMethodInfoAccess();  
	}
		
	public TBaseMethodInfoLogic(TBaseMethodInfoVo _tBaseMethodInfo)
	{
		tBaseMethodInfo = _tBaseMethodInfo;
		access = new TBaseMethodInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseMethodInfoVo tBaseMethodInfo)
        {
            return access.GetSelectResultCount(tBaseMethodInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseMethodInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseMethodInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseMethodInfoVo Details(TBaseMethodInfoVo tBaseMethodInfo)
        {
            return access.Details(tBaseMethodInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseMethodInfoVo> SelectByObject(TBaseMethodInfoVo tBaseMethodInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseMethodInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseMethodInfoVo tBaseMethodInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseMethodInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseMethodInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseMethodInfoVo tBaseMethodInfo)
        {
            return access.SelectByTable(tBaseMethodInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <returns></returns>
        public TBaseMethodInfoVo SelectByObject(TBaseMethodInfoVo tBaseMethodInfo)
        {
            return access.SelectByObject(tBaseMethodInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseMethodInfoVo tBaseMethodInfo)
        {
            return access.Create(tBaseMethodInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMethodInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMethodInfoVo tBaseMethodInfo)
        {
            return access.Edit(tBaseMethodInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseMethodInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseMethodInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseMethodInfoVo tBaseMethodInfo_UpdateSet, TBaseMethodInfoVo tBaseMethodInfo_UpdateWhere)
        {
            return access.Edit(tBaseMethodInfo_UpdateSet, tBaseMethodInfo_UpdateWhere);
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
        public bool Delete(TBaseMethodInfoVo tBaseMethodInfo)
        {
            return access.Delete(tBaseMethodInfo);
        }


        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseMethodInfoVo tBaseMethodInfo, int iIndex, int iCount)
        {
            return access.SelectDefinedTadble(tBaseMethodInfo, iIndex, iCount);
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseMethodInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseMethodInfoVo tBaseMethodInfo)
        {
            return access.GetSelecDefinedtResultCount(tBaseMethodInfo);
        }

        /// <summary>
        ///创建原因： 复制分析方法依据和分析方法
        /// 创建人：胡方扬
        /// 创建日期：2013-08-13  
        /// </summary>
        /// <param name="strSourceTypeId"></param>
        /// <param name="strToTypeId"></param>
        /// <returns></returns>
        public bool CopyInfor(string strSourceTypeId, string strToTypeId)
        {
            return access.CopyInfor(strSourceTypeId, strToTypeId);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseMethodInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //方法依据编号
	  if (tBaseMethodInfo.METHOD_CODE.Trim() == "")
            {
                this.Tips.AppendLine("方法依据编号不能为空");
                return false;
            }
	  //方法依据名称
	  if (tBaseMethodInfo.METHOD_NAME.Trim() == "")
            {
                this.Tips.AppendLine("方法依据名称不能为空");
                return false;
            }
	  //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
	  if (tBaseMethodInfo.MONITOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）不能为空");
                return false;
            }
	  //方法依据描述
	  if (tBaseMethodInfo.DESCRIPTION.Trim() == "")
            {
                this.Tips.AppendLine("方法依据描述不能为空");
                return false;
            }
	  //0为在使用、1为停用
      if (tBaseMethodInfo.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("0为在使用、1为停用不能为空");
                return false;
            }
	  //备注1
	  if (tBaseMethodInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseMethodInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseMethodInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseMethodInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseMethodInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
