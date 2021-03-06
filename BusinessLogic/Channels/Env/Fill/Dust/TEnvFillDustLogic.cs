using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.Dust;
using i3.DataAccess.Channels.Env.Fill.Dust;

namespace i3.BusinessLogic.Channels.Env.Fill.Dust
{
    /// <summary>
    /// 功能：降尘数据填报表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华 
    /// 修改人：刘静楠
    /// 修改时间：2013-06-24
    /// </summary>
    public class TEnvFillDustLogic : LogicBase
    {

	TEnvFillDustVo tEnvFillDust = new TEnvFillDustVo();
    TEnvFillDustAccess access;
		
	public TEnvFillDustLogic()
	{
		  access = new TEnvFillDustAccess();  
	}
		
	public TEnvFillDustLogic(TEnvFillDustVo _tEnvFillDust)
	{
		tEnvFillDust = _tEnvFillDust;
		access = new TEnvFillDustAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDust">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDustVo tEnvFillDust)
        {
            return access.GetSelectResultCount(tEnvFillDust);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDustVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDust">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDustVo Details(TEnvFillDustVo tEnvFillDust)
        {
            return access.Details(tEnvFillDust);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDust">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDustVo> SelectByObject(TEnvFillDustVo tEnvFillDust, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillDust, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDust">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDustVo tEnvFillDust, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillDust, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDust"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDustVo tEnvFillDust)
        {
            return access.SelectByTable(tEnvFillDust);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDust">对象</param>
        /// <returns></returns>
        public TEnvFillDustVo SelectByObject(TEnvFillDustVo tEnvFillDust)
        {
            return access.SelectByObject(tEnvFillDust);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDustVo tEnvFillDust)
        {
            return access.Create(tEnvFillDust);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDust">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDustVo tEnvFillDust)
        {
            return access.Edit(tEnvFillDust);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDust_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tEnvFillDust_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDustVo tEnvFillDust_UpdateSet, TEnvFillDustVo tEnvFillDust_UpdateWhere)
        {
            return access.Edit(tEnvFillDust_UpdateSet, tEnvFillDust_UpdateWhere);
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
        public bool Delete(TEnvFillDustVo tEnvFillDust)
        {
            return access.Delete(tEnvFillDust);
        } 

        #region//降尘数据填报，界面加载时调用(ljn，2013/6/19)
        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="year">年度</param>
        /// <returns></returns>
        //public DataTable GetDustFillData(string year, string month, ref DataTable dtAllItem, string pointId, string strSerial, string strSerialItem)
        //{
        //    DataTable dt = access.GetDustFillData(year, month, ref dtAllItem, pointId, strSerial, strSerialItem);

        //    return dt;// access.GetDustFillData(year, month, pointId);
        //}
        ///// <summary>
        ///// 保存数据
        ///// </summary>
        ///// <param name="dtData">填报数据</param>
        ///// <returns></returns>
        //public bool SaveDustFillData(DataTable dtData, string ItemName, string unSureMark, string strSerial, string strSerialItem)
        //{
        //    return access.SaveDustFillData(dtData, ItemName, unSureMark, strSerial, strSerialItem);
        //}

        #endregion
    
        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }

        #region// 合法性验证
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillDust.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillDust.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //年度
            if (tEnvFillDust.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvFillDust.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //监测起始月
            if (tEnvFillDust.BEGIN_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("监测起始月不能为空");
                return false;
            }
            //监测起始日
            if (tEnvFillDust.BEGIN_DAY.Trim() == "")
            {
                this.Tips.AppendLine("监测起始日不能为空");
                return false;
            }
            //监测起始时
            if (tEnvFillDust.BEGIN_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("监测起始时不能为空");
                return false;
            }
            //监测起始分
            if (tEnvFillDust.BEGIN_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("监测起始分不能为空");
                return false;
            }
            //监测结束月
            if (tEnvFillDust.END_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("监测结束月不能为空");
                return false;
            }
            //监测结束日
            if (tEnvFillDust.END_DAY.Trim() == "")
            {
                this.Tips.AppendLine("监测结束日不能为空");
                return false;
            }
            //监测结束时
            if (tEnvFillDust.END_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("监测结束时不能为空");
                return false;
            }
            //监测结束分
            if (tEnvFillDust.END_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("监测结束分不能为空");
                return false;
            }
            //备注1
            if (tEnvFillDust.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillDust.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillDust.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillDust.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillDust.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }
        #endregion
    }
}
