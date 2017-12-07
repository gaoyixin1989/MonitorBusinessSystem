using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.AIR30;
using i3.DataAccess.Channels.Env.Fill.AIR30;

namespace i3.BusinessLogic.Channels.Env.Fill.AIR30
{
    /// <summary>
    /// 功能：双三十废气填报表
    /// 创建日期：2013-05-08
    /// 创建人：潘德军
    /// modify ：刘静楠
    /// time : 2013-6-25
    /// </summary>
    public class TEnvFillAir30Logic : LogicBase
    {

        TEnvFillAir30Vo tEnvFillAir30 = new TEnvFillAir30Vo();
        TEnvFillAir30Access access;

        public TEnvFillAir30Logic()
        {
            access = new TEnvFillAir30Access();
        }

        public TEnvFillAir30Logic(TEnvFillAir30Vo _tEnvFillAir30)
        {
            tEnvFillAir30 = _tEnvFillAir30;
            access = new TEnvFillAir30Access();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAir30">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAir30Vo tEnvFillAir30)
        {
            return access.GetSelectResultCount(tEnvFillAir30);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAir30Vo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAir30">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAir30Vo Details(TEnvFillAir30Vo tEnvFillAir30)
        {
            return access.Details(tEnvFillAir30);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAir30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAir30Vo> SelectByObject(TEnvFillAir30Vo tEnvFillAir30, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAir30, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAir30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAir30Vo tEnvFillAir30, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAir30, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAir30"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAir30Vo tEnvFillAir30)
        {
            return access.SelectByTable(tEnvFillAir30);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAir30">对象</param>
        /// <returns></returns>
        public TEnvFillAir30Vo SelectByObject(TEnvFillAir30Vo tEnvFillAir30)
        {
            return access.SelectByObject(tEnvFillAir30);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAir30Vo tEnvFillAir30)
        {
            return access.Create(tEnvFillAir30);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAir30">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAir30Vo tEnvFillAir30)
        {
            return access.Edit(tEnvFillAir30);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAir30_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAir30_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAir30Vo tEnvFillAir30_UpdateSet, TEnvFillAir30Vo tEnvFillAir30_UpdateWhere)
        {
            return access.Edit(tEnvFillAir30_UpdateSet, tEnvFillAir30_UpdateWhere);
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
        public bool Delete(TEnvFillAir30Vo tEnvFillAir30)
        {
            return access.Delete(tEnvFillAir30);
        }
        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }

        #region//合法性验证
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillAir30.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillAir30.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //年度
            if (tEnvFillAir30.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月
            if (tEnvFillAir30.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月不能为空");
                return false;
            }
            //日
            if (tEnvFillAir30.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //周
            if (tEnvFillAir30.WEEK.Trim() == "")
            {
                this.Tips.AppendLine("周不能为空");
                return false;
            }
            //测点号
            if (tEnvFillAir30.POINT_NUM.Trim() == "")
            {
                this.Tips.AppendLine("测点号不能为空");
                return false;
            }
            //测点名称
            if (tEnvFillAir30.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("测点名称不能为空");
                return false;
            }
            //气温
            if (tEnvFillAir30.TEMPERATRUE.Trim() == "")
            {
                this.Tips.AppendLine("气温不能为空");
                return false;
            }
            //气压
            if (tEnvFillAir30.PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("气压不能为空");
                return false;
            }
            //风速
            if (tEnvFillAir30.WIND_SPEED.Trim() == "")
            {
                this.Tips.AppendLine("风速不能为空");
                return false;
            }
            //风向
            if (tEnvFillAir30.WIND_DIRECTION.Trim() == "")
            {
                this.Tips.AppendLine("风向不能为空");
                return false;
            }
            //API指数
            if (tEnvFillAir30.API_CODE.Trim() == "")
            {
                this.Tips.AppendLine("API指数不能为空");
                return false;
            }
            //空气质量指数
            if (tEnvFillAir30.AQI_CODE.Trim() == "")
            {
                this.Tips.AppendLine("空气质量指数不能为空");
                return false;
            }
            //空气级别
            if (tEnvFillAir30.AIR_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("空气级别不能为空");
                return false;
            }
            //空气质量状况
            if (tEnvFillAir30.AIR_STATE.Trim() == "")
            {
                this.Tips.AppendLine("空气质量状况不能为空");
                return false;
            }
            //主要污染物
            if (tEnvFillAir30.MAIN_AIR.Trim() == "")
            {
                this.Tips.AppendLine("主要污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAir30.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAir30.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAir30.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAir30.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAir30.REMARK5.Trim() == "")
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
        public DataTable GetAir30FillData(string year, string month, string areacode)
        {
            DataTable dt = access.GetAir30FillData(year, month, areacode);

            //排序
            if (dt.Rows.Count > 0)
                dt = dt.AsEnumerable().OrderBy(c => c["DAY"].ToString().PadLeft(2,'0')).CopyToDataTable();

            return dt;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="dtData">数据集</param>
        /// <returns></returns>
        public string SaveAir30FillData(DataTable dtData, string areaCode)
        {
            return access.SaveAir30FillData(dtData, areaCode);
        }
    }
}
