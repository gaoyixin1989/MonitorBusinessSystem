using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.RiverPlan;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.RiverPlan;

namespace i3.BusinessLogic.Channels.Env.Fill.RiverPlan
{
    /// <summary>
    /// 功能：规划断面
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverPlanLogic : LogicBase
    {

        TEnvFillRiverPlanVo tEnvFillRiverPlan = new TEnvFillRiverPlanVo();
        TEnvFillRiverPlanAccess access;

        public TEnvFillRiverPlanLogic()
        {
            access = new TEnvFillRiverPlanAccess();
        }

        public TEnvFillRiverPlanLogic(TEnvFillRiverPlanVo _tEnvFillRiverPlan)
        {
            tEnvFillRiverPlan = _tEnvFillRiverPlan;
            access = new TEnvFillRiverPlanAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            return access.GetSelectResultCount(tEnvFillRiverPlan);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverPlanVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverPlanVo Details(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            return access.Details(tEnvFillRiverPlan);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverPlanVo> SelectByObject(TEnvFillRiverPlanVo tEnvFillRiverPlan, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiverPlan, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverPlanVo tEnvFillRiverPlan, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiverPlan, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            return access.SelectByTable(tEnvFillRiverPlan);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverPlan">对象</param>
        /// <returns></returns>
        public TEnvFillRiverPlanVo SelectByObject(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            return access.SelectByObject(tEnvFillRiverPlan);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            return access.Create(tEnvFillRiverPlan);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverPlan">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            return access.Edit(tEnvFillRiverPlan);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverPlan_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverPlan_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverPlanVo tEnvFillRiverPlan_UpdateSet, TEnvFillRiverPlanVo tEnvFillRiverPlan_UpdateWhere)
        {
            return access.Edit(tEnvFillRiverPlan_UpdateSet, tEnvFillRiverPlan_UpdateWhere);
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
        public bool Delete(TEnvFillRiverPlanVo tEnvFillRiverPlan)
        {
            return access.Delete(tEnvFillRiverPlan);
        }

              /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            return access.PointByTable(strYear,strMonth);
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
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRiverPlan.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvFillRiverPlan.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillRiverPlan.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillRiverPlan.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillRiverPlan.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillRiverPlan.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillRiverPlan.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillRiverPlan.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillRiverPlan.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillRiverPlan.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //评价
            if (tEnvFillRiverPlan.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillRiverPlan.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiverPlan.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiverPlan.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiverPlan.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiverPlan.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRiverPlan.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
