using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.PolluteAir;
using i3.DataAccess.Channels.Env.Fill.PolluteAir;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.PolluteAir
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-09-03
    /// 创建人：
    /// </summary>
    public class TEnvFillPolluteAirLogic : LogicBase
    {

        TEnvFillPolluteAirVo tEnvFillPolluteAir = new TEnvFillPolluteAirVo();
        TEnvFillPolluteAirAccess access;

        public TEnvFillPolluteAirLogic()
        {
            access = new TEnvFillPolluteAirAccess();
        }

        public TEnvFillPolluteAirLogic(TEnvFillPolluteAirVo _tEnvFillPolluteAir)
        {
            tEnvFillPolluteAir = _tEnvFillPolluteAir;
            access = new TEnvFillPolluteAirAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            return access.GetSelectResultCount(tEnvFillPolluteAir);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteAirVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPolluteAirVo Details(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            return access.Details(tEnvFillPolluteAir);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPolluteAirVo> SelectByObject(TEnvFillPolluteAirVo tEnvFillPolluteAir, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillPolluteAir, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPolluteAirVo tEnvFillPolluteAir, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillPolluteAir, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPolluteAir"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            return access.SelectByTable(tEnvFillPolluteAir);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPolluteAir">对象</param>
        /// <returns></returns>
        public TEnvFillPolluteAirVo SelectByObject(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            return access.SelectByObject(tEnvFillPolluteAir);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            return access.Create(tEnvFillPolluteAir);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteAir">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            return access.Edit(tEnvFillPolluteAir);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPolluteAir_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPolluteAir_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPolluteAirVo tEnvFillPolluteAir_UpdateSet, TEnvFillPolluteAirVo tEnvFillPolluteAir_UpdateWhere)
        {
            return access.Edit(tEnvFillPolluteAir_UpdateSet, tEnvFillPolluteAir_UpdateWhere);
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
        public bool Delete(TEnvFillPolluteAirVo tEnvFillPolluteAir)
        {
            return access.Delete(tEnvFillPolluteAir);
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

        public DataTable GetFillData(string strWhere, DataTable dtShow, string EnterInfo, string PolluteType, string Pollute, string PolluteItem, string FillTable, string FillItemTable, string FillSerial, string FillISerial)
        {
            return access.GetFillData(strWhere, dtShow, EnterInfo, PolluteType, Pollute, PolluteItem, FillTable, FillItemTable, FillSerial, FillISerial);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvFillPolluteAir.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.ENTERPRISE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.ENTERPRISE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.DAY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.SEASON.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.TIMES.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.OQTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.POLLUTEPER.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.POLLUTECALPER.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.IS_STANDARD.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.AIRQTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.MO_DATE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.FUEL_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.FUEL_QTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.FUEL_MODEL.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.FUEL_TECH.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.IS_FUEL.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.DISCHARGE_WAY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.MO_HOUR_QTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.LOAD_MODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.POINT_TEMP.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.IS_RUN.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.MEASURED.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.WASTE_AIR_QTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillPolluteAir.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }

}
