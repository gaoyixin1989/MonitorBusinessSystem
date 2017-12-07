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
    public class TEnvFillNoiseAreaLogic : LogicBase
    {

        TEnvFillNoiseAreaVo tEnvFillNoiseArea = new TEnvFillNoiseAreaVo();
        TEnvFillNoiseAreaAccess access;

        public TEnvFillNoiseAreaLogic()
        {
            access = new TEnvFillNoiseAreaAccess();
        }

        public TEnvFillNoiseAreaLogic(TEnvFillNoiseAreaVo _tEnvFillNoiseArea)
        {
            tEnvFillNoiseArea = _tEnvFillNoiseArea;
            access = new TEnvFillNoiseAreaAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            return access.GetSelectResultCount(tEnvFillNoiseArea);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaVo Details(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            return access.Details(tEnvFillNoiseArea);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseAreaVo> SelectByObject(TEnvFillNoiseAreaVo tEnvFillNoiseArea, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillNoiseArea, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaVo tEnvFillNoiseArea, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillNoiseArea, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseArea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            return access.SelectByTable(tEnvFillNoiseArea);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseArea">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseAreaVo SelectByObject(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            return access.SelectByObject(tEnvFillNoiseArea);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            return access.Create(tEnvFillNoiseArea);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseArea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            return access.Edit(tEnvFillNoiseArea);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseArea_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseArea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaVo tEnvFillNoiseArea_UpdateSet, TEnvFillNoiseAreaVo tEnvFillNoiseArea_UpdateWhere)
        {
            return access.Edit(tEnvFillNoiseArea_UpdateSet, tEnvFillNoiseArea_UpdateWhere);
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
        public bool Delete(TEnvFillNoiseAreaVo tEnvFillNoiseArea)
        {
            return access.Delete(tEnvFillNoiseArea);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillNoiseArea.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillNoiseArea.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //测量时间
            if (tEnvFillNoiseArea.MEASURE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("测量时间不能为空");
                return false;
            }
            //开始月
            if (tEnvFillNoiseArea.BEGIN_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("开始月不能为空");
                return false;
            }
            //开始日
            if (tEnvFillNoiseArea.BEGIN_DAY.Trim() == "")
            {
                this.Tips.AppendLine("开始日不能为空");
                return false;
            }
            //星期
            if (tEnvFillNoiseArea.WEEK.Trim() == "")
            {
                this.Tips.AppendLine("星期不能为空");
                return false;
            }
            //开始时
            if (tEnvFillNoiseArea.BEGIN_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("开始时不能为空");
                return false;
            }
            //开始分
            if (tEnvFillNoiseArea.BEGIN_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("开始分不能为空");
                return false;
            }
            //评价
            if (tEnvFillNoiseArea.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillNoiseArea.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillNoiseArea.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillNoiseArea.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillNoiseArea.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillNoiseArea.REMARK5.Trim() == "")
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
