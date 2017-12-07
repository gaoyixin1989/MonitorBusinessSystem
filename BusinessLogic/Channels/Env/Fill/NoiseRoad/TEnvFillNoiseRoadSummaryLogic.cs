using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseRoad;
using i3.DataAccess.Channels.Env.Fill.NoiseRoad;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.NoiseRoad
{
    /// <summary>
    /// 功能：道路交通噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseRoadSummaryLogic : LogicBase
    {

        TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary = new TEnvFillNoiseRoadSummaryVo();
        TEnvFillNoiseRoadSummaryAccess access;

        public TEnvFillNoiseRoadSummaryLogic()
        {
            access = new TEnvFillNoiseRoadSummaryAccess();
        }

        public TEnvFillNoiseRoadSummaryLogic(TEnvFillNoiseRoadSummaryVo _tEnvFillNoiseRoadSummary)
        {
            tEnvFillNoiseRoadSummary = _tEnvFillNoiseRoadSummary;
            access = new TEnvFillNoiseRoadSummaryAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            return access.GetSelectResultCount(tEnvFillNoiseRoadSummary);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseRoadSummaryVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseRoadSummaryVo Details(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            return access.Details(tEnvFillNoiseRoadSummary);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseRoadSummaryVo> SelectByObject(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillNoiseRoadSummary, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillNoiseRoadSummary, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            return access.SelectByTable(tEnvFillNoiseRoadSummary);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseRoadSummaryVo SelectByObject(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            return access.SelectByObject(tEnvFillNoiseRoadSummary);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            return access.Create(tEnvFillNoiseRoadSummary);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            return access.Edit(tEnvFillNoiseRoadSummary);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseRoadSummary_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseRoadSummary_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary_UpdateSet, TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary_UpdateWhere)
        {
            return access.Edit(tEnvFillNoiseRoadSummary_UpdateSet, tEnvFillNoiseRoadSummary_UpdateWhere);
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
        public bool Delete(TEnvFillNoiseRoadSummaryVo tEnvFillNoiseRoadSummary)
        {
            return access.Delete(tEnvFillNoiseRoadSummary);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvFillNoiseRoadSummary.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //年度
            if (tEnvFillNoiseRoadSummary.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //道路交通干线数目
            if (tEnvFillNoiseRoadSummary.LINE_COUNT.Trim() == "")
            {
                this.Tips.AppendLine("道路交通干线数目不能为空");
                return false;
            }
            //有效测点数
            if (tEnvFillNoiseRoadSummary.VALID_COUNT.Trim() == "")
            {
                this.Tips.AppendLine("有效测点数不能为空");
                return false;
            }
            //监测起始月
            if (tEnvFillNoiseRoadSummary.BEGIN_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("监测起始月不能为空");
                return false;
            }
            //监测起始日
            if (tEnvFillNoiseRoadSummary.BEGIN_DAY.Trim() == "")
            {
                this.Tips.AppendLine("监测起始日不能为空");
                return false;
            }
            //监测结束月
            if (tEnvFillNoiseRoadSummary.END_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("监测结束月不能为空");
                return false;
            }
            //监测结束日
            if (tEnvFillNoiseRoadSummary.END_DAY.Trim() == "")
            {
                this.Tips.AppendLine("监测结束日不能为空");
                return false;
            }
            //机动车拥有量
            if (tEnvFillNoiseRoadSummary.TRAFFIC_COUNT.Trim() == "")
            {
                this.Tips.AppendLine("机动车拥有量不能为空");
                return false;
            }
            //评价
            if (tEnvFillNoiseRoadSummary.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillNoiseRoadSummary.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillNoiseRoadSummary.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillNoiseRoadSummary.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillNoiseRoadSummary.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillNoiseRoadSummary.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新道路交通噪声汇总表
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
        /// 统一运算某一年的道路交通噪声汇总表
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public int OperSummary(string Year)
        {
            return access.OperSummary(Year);
        }
    }

}
