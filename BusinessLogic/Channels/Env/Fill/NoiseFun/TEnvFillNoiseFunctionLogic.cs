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
    public class TEnvFillNoiseFunctionLogic : LogicBase
    {

        TEnvFillNoiseFunctionVo tEnvFillNoiseFunction = new TEnvFillNoiseFunctionVo();
        TEnvFillNoiseFunctionAccess access;

        public TEnvFillNoiseFunctionLogic()
        {
            access = new TEnvFillNoiseFunctionAccess();
        }

        public TEnvFillNoiseFunctionLogic(TEnvFillNoiseFunctionVo _tEnvFillNoiseFunction)
        {
            tEnvFillNoiseFunction = _tEnvFillNoiseFunction;
            access = new TEnvFillNoiseFunctionAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            return access.GetSelectResultCount(tEnvFillNoiseFunction);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionVo Details(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            return access.Details(tEnvFillNoiseFunction);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseFunctionVo> SelectByObject(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillNoiseFunction, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillNoiseFunction, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseFunction"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            return access.SelectByTable(tEnvFillNoiseFunction);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseFunctionVo SelectByObject(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            return access.SelectByObject(tEnvFillNoiseFunction);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            return access.Create(tEnvFillNoiseFunction);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunction">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            return access.Edit(tEnvFillNoiseFunction);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunction_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseFunction_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction_UpdateSet, TEnvFillNoiseFunctionVo tEnvFillNoiseFunction_UpdateWhere)
        {
            return access.Edit(tEnvFillNoiseFunction_UpdateSet, tEnvFillNoiseFunction_UpdateWhere);
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
        public bool Delete(TEnvFillNoiseFunctionVo tEnvFillNoiseFunction)
        {
            return access.Delete(tEnvFillNoiseFunction);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillNoiseFunction.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillNoiseFunction.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //季度
            if (tEnvFillNoiseFunction.QUARTER.Trim() == "")
            {
                this.Tips.AppendLine("季度不能为空");
                return false;
            }
            //测量时间
            if (tEnvFillNoiseFunction.MEASURE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("测量时间不能为空");
                return false;
            }
            //年度
            if (tEnvFillNoiseFunction.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //开始月
            if (tEnvFillNoiseFunction.BEGIN_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("开始月不能为空");
                return false;
            }
            //开始日
            if (tEnvFillNoiseFunction.BEGIN_DAY.Trim() == "")
            {
                this.Tips.AppendLine("开始日不能为空");
                return false;
            }
            //开始时
            if (tEnvFillNoiseFunction.BEGIN_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("开始时不能为空");
                return false;
            }
            //开始分
            if (tEnvFillNoiseFunction.BEGIN_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("开始分不能为空");
                return false;
            }
            //评价
            if (tEnvFillNoiseFunction.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillNoiseFunction.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillNoiseFunction.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillNoiseFunction.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillNoiseFunction.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillNoiseFunction.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }

        public DataTable SelectFill_ForSummary(string strYear, string strMonth, string strPoint)
        {
            return access.SelectFill_ForSummary(strYear, strMonth, strPoint);
        }

        public DataTable SelectResult_ForSummary(string strYear, string strMonth, string strPoint)
        {
            return access.SelectResult_ForSummary(strYear, strMonth, strPoint);
        }
    }

}
