using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseArea;
using i3.DataAccess.Channels.Env.Fill.NoiseArea;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.NoiseArea
{
    /// <summary>
    /// 功能：区域环境噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseAreaSummaryLogic : LogicBase
    {

        TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary = new TEnvFillNoiseAreaSummaryVo();
        TEnvFillNoiseAreaSummaryAccess access;

        public TEnvFillNoiseAreaSummaryLogic()
        {
            access = new TEnvFillNoiseAreaSummaryAccess();
        }

        public TEnvFillNoiseAreaSummaryLogic(TEnvFillNoiseAreaSummaryVo _tEnvFillNoiseAreaSummary)
        {
            tEnvFillNoiseAreaSummary = _tEnvFillNoiseAreaSummary;
            access = new TEnvFillNoiseAreaSummaryAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            return access.GetSelectResultCount(tEnvFillNoiseAreaSummary);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaSummaryVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaSummaryVo Details(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            return access.Details(tEnvFillNoiseAreaSummary);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseAreaSummaryVo> SelectByObject(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillNoiseAreaSummary, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillNoiseAreaSummary, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            return access.SelectByTable(tEnvFillNoiseAreaSummary);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseAreaSummaryVo SelectByObject(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            return access.SelectByObject(tEnvFillNoiseAreaSummary);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            return access.Create(tEnvFillNoiseAreaSummary);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            return access.Edit(tEnvFillNoiseAreaSummary);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseAreaSummary_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseAreaSummary_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary_UpdateSet, TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary_UpdateWhere)
        {
            return access.Edit(tEnvFillNoiseAreaSummary_UpdateSet, tEnvFillNoiseAreaSummary_UpdateWhere);
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
        public bool Delete(TEnvFillNoiseAreaSummaryVo tEnvFillNoiseAreaSummary)
        {
            return access.Delete(tEnvFillNoiseAreaSummary);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvFillNoiseAreaSummary.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //年度
            if (tEnvFillNoiseAreaSummary.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //有效测点数
            if (tEnvFillNoiseAreaSummary.VALID_COUNT.Trim() == "")
            {
                this.Tips.AppendLine("有效测点数不能为空");
                return false;
            }
            //城区人口
            if (tEnvFillNoiseAreaSummary.PEOPLE_COUNT.Trim() == "")
            {
                this.Tips.AppendLine("城区人口不能为空");
                return false;
            }
            //监测起始月
            if (tEnvFillNoiseAreaSummary.BEGIN_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("监测起始月不能为空");
                return false;
            }
            //监测起始日
            if (tEnvFillNoiseAreaSummary.BEGIN_DAY.Trim() == "")
            {
                this.Tips.AppendLine("监测起始日不能为空");
                return false;
            }
            //监测结束月
            if (tEnvFillNoiseAreaSummary.END_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("监测结束月不能为空");
                return false;
            }
            //监测结束日
            if (tEnvFillNoiseAreaSummary.END_DAY.Trim() == "")
            {
                this.Tips.AppendLine("监测结束日不能为空");
                return false;
            }
            //城区人口密度
            if (tEnvFillNoiseAreaSummary.PEOPLE_DENSITY.Trim() == "")
            {
                this.Tips.AppendLine("城区人口密度不能为空");
                return false;
            }
            //建成区面积
            if (tEnvFillNoiseAreaSummary.AREA.Trim() == "")
            {
                this.Tips.AppendLine("建成区面积不能为空");
                return false;
            }
            //评价
            if (tEnvFillNoiseAreaSummary.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillNoiseAreaSummary.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillNoiseAreaSummary.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillNoiseAreaSummary.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillNoiseAreaSummary.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillNoiseAreaSummary.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新区域环境噪声汇总表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ColName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int UpdateSummary(string ID, string ColName, string Value)
        {
            return access.UpdateSummary(ID, ColName, Value);
        }
        /// <summary>
        /// 统一运算某一年的区域环境噪声汇总表
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public int OperSummary(string Year)
        {
            return access.OperSummary(Year);
        }
    }

}
