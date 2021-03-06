using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.Air;
using i3.DataAccess.Channels.Env.Fill.Air;

namespace i3.BusinessLogic.Channels.Env.Fill.Air
{
    /// <summary>
    /// 功能：空气数据填报表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改时间：2013-06-24
    /// 修改人：刘静楠
    /// </summary>
    public class TEnvFillAirLogic : LogicBase
    {

        TEnvFillAirVo tEnvFillAir = new TEnvFillAirVo();
        TEnvFillAirAccess access;

        public TEnvFillAirLogic()
        {
            access = new TEnvFillAirAccess();
        }

        public TEnvFillAirLogic(TEnvFillAirVo _tEnvFillAir)
        {
            tEnvFillAir = _tEnvFillAir;
            access = new TEnvFillAirAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAir">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirVo tEnvFillAir)
        {
            return access.GetSelectResultCount(tEnvFillAir);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAir">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirVo Details(TEnvFillAirVo tEnvFillAir)
        {
            return access.Details(tEnvFillAir);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirVo> SelectByObject(TEnvFillAirVo tEnvFillAir, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAir, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirVo tEnvFillAir, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAir, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAir"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirVo tEnvFillAir)
        {
            return access.SelectByTable(tEnvFillAir);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAir">对象</param>
        /// <returns></returns>
        public TEnvFillAirVo SelectByObject(TEnvFillAirVo tEnvFillAir)
        {
            return access.SelectByObject(tEnvFillAir);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirVo tEnvFillAir)
        {
            return access.Create(tEnvFillAir);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAir">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirVo tEnvFillAir)
        {
            return access.Edit(tEnvFillAir);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAir_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAir_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirVo tEnvFillAir_UpdateSet, TEnvFillAirVo tEnvFillAir_UpdateWhere)
        {
            return access.Edit(tEnvFillAir_UpdateSet, tEnvFillAir_UpdateWhere);
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
        public bool Delete(TEnvFillAirVo tEnvFillAir)
        {
            return access.Delete(tEnvFillAir);
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
        /// 获取环境质量数据填报数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// /// <param name="dtShow">填报主表显示的列表信息 格式：有两列，第一列是字段名，第二列是中文名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <returns></returns>
        public DataTable GetFillData(string strWhere, DataTable dtShow,string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            return access.GetFillData(strWhere, dtShow, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial);
        }
        /// <summary>
        /// 根据监测值计算出空气分指数、空气指数、空气质量级别、空气质量状况和主要污染物
        /// </summary>
        /// <param name="Fill_ID">环境空气的行ID</param>
        /// <param name="ItemVaule">监测值</param>
        /// <param name="ItemName">监测值得列名(规则:表明@监测值ID,"T_ENV_FILL_DUST_ITEM@000000232";)</param>
        public bool UpdateAirValue(string Fill_ID, string ItemVaule, string ItemName)
        {
            return access.UpdateAirValue(Fill_ID, ItemVaule, ItemName);
        }

        public int ExcelOutCal(string year, string month, string ItemName)
        {
            return access.BeforeOutUpdate(year, month, ItemName);
        }

        public string  EnvInsertData(DataSet ds, string AirFillTable, string AirFillItemTable, string FillSerial, string FillISerial)
        {
           // return access.EnvInsertData(ds, AirFillTable, AirFillItemTable, FillSerial, FillISerial);
            return access.EnvUpdateData(ds, AirFillTable, AirFillItemTable, FillSerial, FillISerial); 
        }

        #region//合法性验证
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillAir.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillAir.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //年度
            if (tEnvFillAir.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillAir.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillAir.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillAir.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillAir.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //气温
            if (tEnvFillAir.TEMPERATRUE.Trim() == "")
            {
                this.Tips.AppendLine("气温不能为空");
                return false;
            }
            //气压
            if (tEnvFillAir.PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("气压不能为空");
                return false;
            }
            //风速
            if (tEnvFillAir.WIND_SPEED.Trim() == "")
            {
                this.Tips.AppendLine("风速不能为空");
                return false;
            }
            //风向
            if (tEnvFillAir.WIND_DIRECTION.Trim() == "")
            {
                this.Tips.AppendLine("风向不能为空");
                return false;
            }
            //API指数
            if (tEnvFillAir.API_CODE.Trim() == "")
            {
                this.Tips.AppendLine("API指数不能为空");
                return false;
            }
            //空气质量指数
            if (tEnvFillAir.AQI_CODE.Trim() == "")
            {
                this.Tips.AppendLine("空气质量指数不能为空");
                return false;
            }
            //空气级别
            if (tEnvFillAir.AIR_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("空气级别不能为空");
                return false;
            }
            //空气质量状况
            if (tEnvFillAir.AIR_STATE.Trim() == "")
            {
                this.Tips.AppendLine("空气质量状况不能为空");
                return false;
            }
            //主要污染物
            if (tEnvFillAir.MAIN_AIR.Trim() == "")
            {
                this.Tips.AppendLine("主要污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAir.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAir.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAir.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAir.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAir.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public DataTable GetAirFillData(string year, string month)
        {
            DataTable dt = access.GetAirFillData(year, month);

            //排序
            if (dt.Rows.Count > 0)
                dt = dt.AsEnumerable().OrderBy(c => c["POINT_CODE"].ToString()).ThenBy(c => Convert.ToInt32(!string.IsNullOrEmpty(c["DAY"].ToString()) ? c["DAY"].ToString() : "0")).CopyToDataTable();

            //为空时补一行
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["year"] = year;
                dr["month"] = month;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="dtData">数据集</param>
        /// <returns></returns>
        public string SaveAirFillData(DataTable dtData)
        {
            return access.SaveAirFillData(dtData);
        }
    }
}
