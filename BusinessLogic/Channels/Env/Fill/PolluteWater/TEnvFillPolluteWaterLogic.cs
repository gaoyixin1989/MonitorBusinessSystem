using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.PolluteWater;
using i3.DataAccess.Channels.Env.Fill.PolluteWater;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.PolluteWater
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-09-02
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteWaterLogic : LogicBase
    {

        TEnvFillPolluteWaterVo tEnvFillPolluteWater = new TEnvFillPolluteWaterVo();
        TEnvFillPolluteWaterAccess access;

        public TEnvFillPolluteWaterLogic()
        {
            access = new TEnvFillPolluteWaterAccess();
        }

        public TEnvFillPolluteWaterLogic(TEnvFillPolluteWaterVo _tEnvFillPolluteWater)
        {
            tEnvFillPolluteWater = _tEnvFillPolluteWater;
            access = new TEnvFillPolluteWaterAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            return access.GetSelectResultCount(tEnvFillPolluteWater);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteWaterVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteWaterVo Details(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            return access.Details(tEnvFillPolluteWater);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPolluteWaterVo> SelectByObject(TEnvFillPolluteWaterVo tEnvFillPolluteWater, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillPolluteWater, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPolluteWaterVo tEnvFillPolluteWater, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillPolluteWater, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPolluteWater"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            return access.SelectByTable(tEnvFillPolluteWater);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPolluteWater">对象</param>
        /// <returns></returns>
        public TEnvFillPolluteWaterVo SelectByObject(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            return access.SelectByObject(tEnvFillPolluteWater);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            return access.Create(tEnvFillPolluteWater);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteWater">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            return access.Edit(tEnvFillPolluteWater);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteWater_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPolluteWater_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteWaterVo tEnvFillPolluteWater_UpdateSet, TEnvFillPolluteWaterVo tEnvFillPolluteWater_UpdateWhere)
        {
            return access.Edit(tEnvFillPolluteWater_UpdateSet, tEnvFillPolluteWater_UpdateWhere);
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
        public bool Delete(TEnvFillPolluteWaterVo tEnvFillPolluteWater)
        {
            return access.Delete(tEnvFillPolluteWater);
        }
        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            return access.PointByTable(strYear, strMonth);
        }
        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }
        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public bool Calculate(TEnvFillPolluteWaterVo Fill_ID)
        {
            return access.Calculate(Fill_ID);
        } 

        public DataTable GetFillData(string strWhere, DataTable dtShow, string EnterInfo, string PolluteType, string Pollute,string PolluteItem, string FillTable, string FillItemTable, string FillSerial, string FillISerial)
        {
            return access.GetFillData(strWhere, dtShow, EnterInfo, PolluteType, Pollute,PolluteItem, FillTable, FillItemTable, FillSerial, FillISerial);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvFillPolluteWater.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.ENTERPRISE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.ENTERPRISE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.DAY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.SEASON.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.TIMES.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.WATERQTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.IS_RUN.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.LOAD_MODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.IN_WATER_QTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.IS_EVALUATE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            if (tEnvFillPolluteWater.WATER_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteWater.WATER_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            return true;
        }

    }

}
