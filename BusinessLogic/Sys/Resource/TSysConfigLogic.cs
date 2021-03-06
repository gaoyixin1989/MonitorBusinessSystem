using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.Resource;
using i3.DataAccess.Sys.Resource;

namespace i3.BusinessLogic.Sys.Resource
{
    /// <summary>
    /// 功能：系统配置管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysConfigLogic : LogicBase
    {

	TSysConfigVo tSysConfig = new TSysConfigVo();
        TSysConfigAccess access;
		
	public TSysConfigLogic()
	{
		  access = new TSysConfigAccess();  
	}
		
	public TSysConfigLogic(TSysConfigVo _tSysConfig)
	{
		tSysConfig = _tSysConfig;
		access = new TSysConfigAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysConfig">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysConfigVo tSysConfig)
        {
            return access.GetSelectResultCount(tSysConfig);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysConfigVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysConfig">对象条件</param>
        /// <returns>对象</returns>
        public TSysConfigVo Details(TSysConfigVo tSysConfig)
        {
            return access.Details(tSysConfig);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysConfig">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysConfigVo> SelectByObject(TSysConfigVo tSysConfig, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysConfig, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysConfig">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysConfigVo tSysConfig, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysConfig, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysConfig"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysConfigVo tSysConfig)
        {
            return access.SelectByTable(tSysConfig);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysConfig">对象</param>
        /// <returns></returns>
        public TSysConfigVo SelectByObject(TSysConfigVo tSysConfig)
        {
            return access.SelectByObject(tSysConfig);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysConfigVo tSysConfig)
        {
            return access.Create(tSysConfig);
        }

        /// <summary>
        /// 常规过滤配置
        /// </summary>
        /// <param name="tSysConfig">配置对象</param>
        /// <param name="strUpLimit">上限</param>
        /// <param name="strDownLimit">下限</param>
        /// <returns></returns>
        public bool CreateConfig(TSysConfigVo tSysConfig, TSysConfigVo objUpLimit, TSysConfigVo objDownLimit)
        {
            return access.CreateConfig(tSysConfig, objUpLimit, objDownLimit);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysConfig">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysConfigVo tSysConfig)
        {
            return access.Edit(tSysConfig);
        }

        /// <summary>
        /// 常规过滤配置
        /// </summary>
        /// <param name="strUpLimit">上限</param>
        /// <param name="strDownLimit">下限</param>
        /// <returns></returns>
        public bool EditConfig(TSysConfigVo objUpLimit, TSysConfigVo objDownLimit)
        {
            return access.EditConfig(objUpLimit, objDownLimit);
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
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            	  //编号
	  if (tSysConfig.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //配置编码
	  if (tSysConfig.CONFIG_CODE.Trim() == "")
            {
                this.Tips.AppendLine("配置编码不能为空");
                return false;
            }
	  //配置值
	  if (tSysConfig.CONFIG_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("配置值不能为空");
                return false;
            }
	  //配置类型
	  if (tSysConfig.CONFIG_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("配置类型不能为空");
                return false;
            }
	  //创建人ID
	  if (tSysConfig.CREATE_ID.Trim() == "")
            {
                this.Tips.AppendLine("创建人ID不能为空");
                return false;
            }
	  //创建时间
	  if (tSysConfig.CREATE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("创建时间不能为空");
                return false;
            }
	  //备注
	  if (tSysConfig.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
	  //备注1
	  if (tSysConfig.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tSysConfig.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tSysConfig.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
