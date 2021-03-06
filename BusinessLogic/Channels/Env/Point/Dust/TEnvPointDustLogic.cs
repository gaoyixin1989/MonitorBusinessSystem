using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Point.Dust;
using i3.DataAccess.Channels.Env.Point.Dust;

namespace i3.BusinessLogic.Channels.Env.Point.Dust
{
    /// <summary>
    /// 功能：降尘监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    ///     /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointDustLogic : LogicBase
    {

	TEnvPointDustVo tEnvPointDust = new TEnvPointDustVo();
    TEnvPointDustAccess access;
		
	public TEnvPointDustLogic()
	{
		  access = new TEnvPointDustAccess();  
	}
		
	public TEnvPointDustLogic(TEnvPointDustVo _tEnvPointDust)
	{
		tEnvPointDust = _tEnvPointDust;
		access = new TEnvPointDustAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointDust">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointDustVo tEnvPointDust)
        {
            return access.GetSelectResultCount(tEnvPointDust);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointDustVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointDust">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointDustVo Details(TEnvPointDustVo tEnvPointDust)
        {
            return access.Details(tEnvPointDust);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointDust">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointDustVo> SelectByObject(TEnvPointDustVo tEnvPointDust, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPointDust, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointDust">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointDustVo tEnvPointDust, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPointDust, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointDust"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointDustVo tEnvPointDust)
        {
            return access.SelectByTable(tEnvPointDust);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointDust">对象</param>
        /// <returns></returns>
        public TEnvPointDustVo SelectByObject(TEnvPointDustVo tEnvPointDust)
        {
            return access.SelectByObject(tEnvPointDust);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointDustVo tEnvPointDust)
        {
            return access.Create(tEnvPointDust);
        }
        /// <summary>
        /// 对象添加(ljn.2013/6/18)
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointDustVo tEnvPointDust, string Number)
        {
            return access.Create(tEnvPointDust, Number);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointDust">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointDustVo tEnvPointDust)
        {
            return access.Edit(tEnvPointDust);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointDust_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvPointDust_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointDustVo tEnvPointDust_UpdateSet, TEnvPointDustVo tEnvPointDust_UpdateWhere)
        {
            return access.Edit(tEnvPointDust_UpdateSet, tEnvPointDust_UpdateWhere);
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
        public bool Delete(TEnvPointDustVo tEnvPointDust)
        {
            return access.Delete(tEnvPointDust);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //主键ID
	  if (tEnvPointDust.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
	  //年度
	  if (tEnvPointDust.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
	  //测站ID（字典项）
	  if (tEnvPointDust.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
	  //测点编号
	  if (tEnvPointDust.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("测点编号不能为空");
                return false;
            }
	  //测点名称
	  if (tEnvPointDust.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("测点名称不能为空");
                return false;
            }
	  //行政区ID（字典项）
	  if (tEnvPointDust.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("行政区ID（字典项）不能为空");
                return false;
            }
	  //控制级别ID（字典项）
	  if (tEnvPointDust.CONTRAL_LEVEL_ID.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID（字典项）不能为空");
                return false;
            }
	  //经度（度）
	  if (tEnvPointDust.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
	  //经度（分）
	  if (tEnvPointDust.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
	  //经度（秒）
	  if (tEnvPointDust.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
	  //纬度（度）
	  if (tEnvPointDust.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
	  //纬度（分）
	  if (tEnvPointDust.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
	  //纬度（秒）
	  if (tEnvPointDust.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
	  //具体位置
	  if (tEnvPointDust.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("具体位置不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tEnvPointDust.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //序号
	  if (tEnvPointDust.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
	  //备注1
	  if (tEnvPointDust.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tEnvPointDust.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tEnvPointDust.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tEnvPointDust.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tEnvPointDust.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
