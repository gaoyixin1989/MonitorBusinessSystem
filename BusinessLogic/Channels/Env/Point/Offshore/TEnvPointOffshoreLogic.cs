using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Point.Offshore;
using i3.DataAccess.Channels.Env.Point.Offshore;

namespace i3.BusinessLogic.Channels.Env.Point.Offshore
{
    /// <summary>
    /// 功能：近岸直排监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointOffshoreLogic : LogicBase
    {

	TEnvPointOffshoreVo tEnvPointOffshore = new TEnvPointOffshoreVo();
    TEnvPointOffshoreAccess access;
		
	public TEnvPointOffshoreLogic()
	{
		  access = new TEnvPointOffshoreAccess();  
	}
		
	public TEnvPointOffshoreLogic(TEnvPointOffshoreVo _tEnvPointOffshore)
	{
		tEnvPointOffshore = _tEnvPointOffshore;
		access = new TEnvPointOffshoreAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            return access.GetSelectResultCount(tEnvPointOffshore);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointOffshoreVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointOffshore">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointOffshoreVo Details(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            return access.Details(tEnvPointOffshore);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointOffshoreVo> SelectByObject(TEnvPointOffshoreVo tEnvPointOffshore, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPointOffshore, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointOffshoreVo tEnvPointOffshore, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPointOffshore, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointOffshore"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            return access.SelectByTable(tEnvPointOffshore);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <returns></returns>
        public TEnvPointOffshoreVo SelectByObject(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            return access.SelectByObject(tEnvPointOffshore);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            return access.Create(tEnvPointOffshore);
        }
        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointOffshoreVo tEnvPointOffshore, string Number)
        {
            return access.Create(tEnvPointOffshore, Number);
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointOffshore">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            return access.Edit(tEnvPointOffshore);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointOffshore_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvPointOffshore_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointOffshoreVo tEnvPointOffshore_UpdateSet, TEnvPointOffshoreVo tEnvPointOffshore_UpdateWhere)
        {
            return access.Edit(tEnvPointOffshore_UpdateSet, tEnvPointOffshore_UpdateWhere);
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
        public bool Delete(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            return access.Delete(tEnvPointOffshore);
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TEnvPointOffshoreVo tEnvPointOffshore, int iIndex, int iCount)
        {
            return access.SelectDefinedTadble(tEnvPointOffshore, iIndex, iCount);
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            return access.GetSelecDefinedtResultCount(tEnvPointOffshore);
        }
        /// <summary>
        /// 批量保存监测项目数据[用于无垂线监测点]
        /// </summary>
        /// <param name="strTableName">数据表名</param>
        /// <param name="strColumnName">数据表列名</param>
        /// <param name="strSerialId">序列号</param>
        /// <param name="strPointId">监测点ID</param>
        /// <param name="strValue">监测项目值</param>
        /// <returns></returns>
        public bool SaveItemByTransaction(string strTableName, string strColumnName, string strSerialId, string strPointId, string strValue)
        {
            return access.SaveItemByTransaction(strTableName, strColumnName, strSerialId, strPointId, strValue);
        }
        
        //监测项目复制
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            return access.PasteItem(strFID, strTID, strSerial);
        }


        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvPointOffshore.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //年度
            if (tEnvPointOffshore.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //月度
            if (tEnvPointOffshore.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
            }
            //企业名称
            if (tEnvPointOffshore.COMPANY_NAME.Trim() == "")
            {
                this.Tips.AppendLine("企业名称不能为空");
                return false;
            }
            //功能属性
            if (tEnvPointOffshore.FUNCTION_ATTRIBUTE.Trim() == "")
            {
                this.Tips.AppendLine("功能属性不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPointOffshore.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPointOffshore.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPointOffshore.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPointOffshore.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPointOffshore.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPointOffshore.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //具体位置
            if (tEnvPointOffshore.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("具体位置不能为空");
                return false;
            }
            //条件项
            if (tEnvPointOffshore.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //使用状态(0为启用、1为停用)
            if (tEnvPointOffshore.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
            //序号
            if (tEnvPointOffshore.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPointOffshore.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPointOffshore.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPointOffshore.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPointOffshore.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPointOffshore.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }


    }
}
