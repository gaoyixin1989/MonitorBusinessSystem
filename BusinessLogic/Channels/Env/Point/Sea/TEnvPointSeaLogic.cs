using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Point.Sea;
using i3.DataAccess.Channels.Env.Point.Sea;

namespace i3.BusinessLogic.Channels.Env.Point.Sea
{
    /// <summary>
    /// 功能：近海海域监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
  /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointSeaLogic : LogicBase
    {

	TEnvPointSeaVo tEnvPointSea = new TEnvPointSeaVo();
    TEnvPointSeaAccess access;
		
	public TEnvPointSeaLogic()
	{
		  access = new TEnvPointSeaAccess();  
	}
		
	public TEnvPointSeaLogic(TEnvPointSeaVo _tEnvPointSea)
	{
		tEnvPointSea = _tEnvPointSea;
		access = new TEnvPointSeaAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointSeaVo tEnvPointSea)
        {
            return access.GetSelectResultCount(tEnvPointSea);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointSeaVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointSea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointSeaVo Details(TEnvPointSeaVo tEnvPointSea)
        {
            return access.Details(tEnvPointSea);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointSeaVo> SelectByObject(TEnvPointSeaVo tEnvPointSea, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPointSea, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointSeaVo tEnvPointSea, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPointSea, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointSea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointSeaVo tEnvPointSea)
        {
            return access.SelectByTable(tEnvPointSea);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointSea">对象</param>
        /// <returns></returns>
        public TEnvPointSeaVo SelectByObject(TEnvPointSeaVo tEnvPointSea)
        {
            return access.SelectByObject(tEnvPointSea);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointSeaVo tEnvPointSea)
        {
            return access.Create(tEnvPointSea);
        }
        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointSeaVo tEnvPointSea, string Number)
        {
            return access.Create(tEnvPointSea, Number);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointSea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointSeaVo tEnvPointSea)
        {
            return access.Edit(tEnvPointSea);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointSea_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvPointSea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointSeaVo tEnvPointSea_UpdateSet, TEnvPointSeaVo tEnvPointSea_UpdateWhere)
        {
            return access.Edit(tEnvPointSea_UpdateSet, tEnvPointSea_UpdateWhere);
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
        public bool Delete(TEnvPointSeaVo tEnvPointSea)
        {
            return access.Delete(tEnvPointSea);
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TEnvPointSeaVo tEnvPointSea, int iIndex, int iCount)
        {
            return access.SelectDefinedTadble(tEnvPointSea, iIndex, iCount);
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TEnvPointSeaVo tEnvPointSea)
        {
            return access.GetSelecDefinedtResultCount(tEnvPointSea);
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
            if (tEnvPointSea.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //年份
            if (tEnvPointSea.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //月度
            if (tEnvPointSea.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //监测点编码
            if (tEnvPointSea.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //监测点名称
            if (tEnvPointSea.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("监测点名称不能为空");
                return false;
            }
            //功能区代码
            if (tEnvPointSea.FUNCTION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("功能区代码不能为空");
                return false;
            }
            //国家编号
            if (tEnvPointSea.COUNTRY_CODE.Trim() == "")
            {
                this.Tips.AppendLine("国家编号不能为空");
                return false;
            }
            //省份编号
            if (tEnvPointSea.PROVINCE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("省份编号不能为空");
                return false;
            }
            //点位类别
            if (tEnvPointSea.POINT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("点位类别不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPointSea.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPointSea.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPointSea.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPointSea.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPointSea.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPointSea.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //具体位置
            if (tEnvPointSea.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("具体位置不能为空");
                return false;
            }
            //条件项
            if (tEnvPointSea.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //使用状态(0为启用、1为停用)
            if (tEnvPointSea.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
            //序号
            if (tEnvPointSea.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPointSea.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPointSea.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPointSea.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPointSea.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPointSea.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }


    }
}
