using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Fill.Alkali;
using i3.DataAccess.Channels.Env.Fill.Alkali;

namespace i3.BusinessLogic.Channels.Env.Fill.Alkali
{
    /// <summary>
    /// 功能：硫酸盐化速率填报
    /// 创建日期：2013-05-08
    /// 创建人：潘德军
    /// 修改日期：2013-06-21
    /// 修改人：刘静楠
    /// </summary>
    public class TEnvFillAlkaliLogic : LogicBase
    {

        TEnvFillAlkaliVo tEnvFillAlkali = new TEnvFillAlkaliVo();
        TEnvFillAlkaliAccess access;

        public TEnvFillAlkaliLogic()
        {
            access = new TEnvFillAlkaliAccess();
        }

        public TEnvFillAlkaliLogic(TEnvFillAlkaliVo _tEnvFillAlkali)
        {
            tEnvFillAlkali = _tEnvFillAlkali;
            access = new TEnvFillAlkaliAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAlkali">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            return access.GetSelectResultCount(tEnvFillAlkali);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAlkaliVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAlkali">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAlkaliVo Details(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            return access.Details(tEnvFillAlkali);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAlkali">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAlkaliVo> SelectByObject(TEnvFillAlkaliVo tEnvFillAlkali, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAlkali, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAlkali">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAlkaliVo tEnvFillAlkali, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAlkali, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAlkali"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            return access.SelectByTable(tEnvFillAlkali);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAlkali">对象</param>
        /// <returns></returns>
        public TEnvFillAlkaliVo SelectByObject(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            return access.SelectByObject(tEnvFillAlkali);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            return access.Create(tEnvFillAlkali);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlkali">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            return access.Edit(tEnvFillAlkali);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlkali_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAlkali_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlkaliVo tEnvFillAlkali_UpdateSet, TEnvFillAlkaliVo tEnvFillAlkali_UpdateWhere)
        {
            return access.Edit(tEnvFillAlkali_UpdateSet, tEnvFillAlkali_UpdateWhere);
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
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }
        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            return access.Delete(tEnvFillAlkali);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillAlkali.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvFillAlkali.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvFillAlkali.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillAlkali.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //开始月
            if (tEnvFillAlkali.BEGIN_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("开始月不能为空");
                return false;
            }
            //开始日
            if (tEnvFillAlkali.BEGIN_DAY.Trim() == "")
            {
                this.Tips.AppendLine("开始日不能为空");
                return false;
            }
            //开始时
            if (tEnvFillAlkali.BEGIN_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("开始时不能为空");
                return false;
            }
            //开始分
            if (tEnvFillAlkali.BEGIN_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("开始分不能为空");
                return false;
            }
            //结束月
            if (tEnvFillAlkali.END_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("结束月不能为空");
                return false;
            }
            //结束日
            if (tEnvFillAlkali.END_DAY.Trim() == "")
            {
                this.Tips.AppendLine("结束日不能为空");
                return false;
            }
            //结束时
            if (tEnvFillAlkali.END_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("结束时不能为空");
                return false;
            }
            //结束分
            if (tEnvFillAlkali.END_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("结束分不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAlkali.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAlkali.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAlkali.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAlkali.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAlkali.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }


        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public DataTable GetAlkaliFillData(string year, string month)
        {
            DataTable dt = access.GetAlkaliFillData(year, month);

            //排序
            if (dt.Rows.Count > 0)
                dt = dt.AsEnumerable().OrderBy(c => c["POINT_NUM"].ToString()).ThenBy(c => Convert.ToInt32(c["SDAY"].ToString())).CopyToDataTable();

            //为空时补一行
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["year"] = year;
                dr["SMONTH"] = month;
                dr["EMONTH"] = month;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="dtData">数据集</param>
        /// <returns></returns>
        public string SaveAlkaliFillData(DataTable dtData)
        {
            return access.SaveAlkaliFillData(dtData);
        }
    }
}
