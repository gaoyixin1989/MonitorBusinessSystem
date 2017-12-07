using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseFun;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.NoiseFun;

namespace i3.BusinessLogic.Channels.Env.Fill.NoiseFun
{
    /// <summary>
    /// 功能：功能区噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseFunctionSummaryLogic : LogicBase
    {

        TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary = new TEnvFillNoiseFunctionSummaryVo();
        TEnvFillNoiseFunctionSummaryAccess access;

        public TEnvFillNoiseFunctionSummaryLogic()
        {
            access = new TEnvFillNoiseFunctionSummaryAccess();
        }

        public TEnvFillNoiseFunctionSummaryLogic(TEnvFillNoiseFunctionSummaryVo _tEnvFillNoiseFunctionSummary)
        {
            tEnvFillNoiseFunctionSummary = _tEnvFillNoiseFunctionSummary;
            access = new TEnvFillNoiseFunctionSummaryAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            return access.GetSelectResultCount(tEnvFillNoiseFunctionSummary);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionSummaryVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionSummaryVo Details(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            return access.Details(tEnvFillNoiseFunctionSummary);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseFunctionSummaryVo> SelectByObject(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillNoiseFunctionSummary, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillNoiseFunctionSummary, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            return access.SelectByTable(tEnvFillNoiseFunctionSummary);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseFunctionSummaryVo SelectByObject(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            return access.SelectByObject(tEnvFillNoiseFunctionSummary);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            return access.Create(tEnvFillNoiseFunctionSummary);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            return access.Edit(tEnvFillNoiseFunctionSummary);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionSummary_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseFunctionSummary_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary_UpdateSet, TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary_UpdateWhere)
        {
            return access.Edit(tEnvFillNoiseFunctionSummary_UpdateSet, tEnvFillNoiseFunctionSummary_UpdateWhere);
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
        public bool Delete(TEnvFillNoiseFunctionSummaryVo tEnvFillNoiseFunctionSummary)
        {
            return access.Delete(tEnvFillNoiseFunctionSummary);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvFillNoiseFunctionSummary.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //年度
            if (tEnvFillNoiseFunctionSummary.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //季度
            if (tEnvFillNoiseFunctionSummary.QUTER.Trim() == "")
            {
                this.Tips.AppendLine("季度不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillNoiseFunctionSummary.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //气象条件
            if (tEnvFillNoiseFunctionSummary.WEATHER.Trim() == "")
            {
                this.Tips.AppendLine("气象条件不能为空");
                return false;
            }
            //监测日期(月)
            if (tEnvFillNoiseFunctionSummary.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("监测日期(月)不能为空");
                return false;
            }
            //监测日期(日)
            if (tEnvFillNoiseFunctionSummary.DAY.Trim() == "")
            {
                this.Tips.AppendLine("监测日期(日)不能为空");
                return false;
            }
            //白昼均值
            if (tEnvFillNoiseFunctionSummary.LD.Trim() == "")
            {
                this.Tips.AppendLine("白昼均值不能为空");
                return false;
            }
            //夜间均值
            if (tEnvFillNoiseFunctionSummary.LN.Trim() == "")
            {
                this.Tips.AppendLine("夜间均值不能为空");
                return false;
            }
            //日均值
            if (tEnvFillNoiseFunctionSummary.LDN.Trim() == "")
            {
                this.Tips.AppendLine("日均值不能为空");
                return false;
            }
            //评价
            if (tEnvFillNoiseFunctionSummary.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillNoiseFunctionSummary.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillNoiseFunctionSummary.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillNoiseFunctionSummary.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillNoiseFunctionSummary.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillNoiseFunctionSummary.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新功能区噪声汇总表的昼均值、夜均值、日均值
        /// </summary>
        /// <param name="PointID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public int UpdateFunctionSummary(string PointID, string ItemID)
        {
            return access.UpdateFunctionSummary(PointID, ItemID);
        }

        /// <summary>
        /// 更新功能区噪声汇总表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ColName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int UpdateSummary(string ID, string ColName, string Value)
        {
            return access.UpdateSummary(ID, ColName, Value);
        }
    }

}
