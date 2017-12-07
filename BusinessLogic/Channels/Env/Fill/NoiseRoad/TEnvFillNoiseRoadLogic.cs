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
    public class TEnvFillNoiseRoadLogic : LogicBase
    {

        TEnvFillNoiseRoadVo tEnvFillNoiseRoad = new TEnvFillNoiseRoadVo();
        TEnvFillNoiseRoadAccess access;

        public TEnvFillNoiseRoadLogic()
        {
            access = new TEnvFillNoiseRoadAccess();
        }

        public TEnvFillNoiseRoadLogic(TEnvFillNoiseRoadVo _tEnvFillNoiseRoad)
        {
            tEnvFillNoiseRoad = _tEnvFillNoiseRoad;
            access = new TEnvFillNoiseRoadAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseRoad">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseRoadVo tEnvFillNoiseRoad)
        {
            return access.GetSelectResultCount(tEnvFillNoiseRoad);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseRoadVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseRoad">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseRoadVo Details(TEnvFillNoiseRoadVo tEnvFillNoiseRoad)
        {
            return access.Details(tEnvFillNoiseRoad);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseRoad">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseRoadVo> SelectByObject(TEnvFillNoiseRoadVo tEnvFillNoiseRoad, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillNoiseRoad, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseRoad">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseRoadVo tEnvFillNoiseRoad, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillNoiseRoad, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseRoad"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseRoadVo tEnvFillNoiseRoad)
        {
            return access.SelectByTable(tEnvFillNoiseRoad);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseRoad">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseRoadVo SelectByObject(TEnvFillNoiseRoadVo tEnvFillNoiseRoad)
        {
            return access.SelectByObject(tEnvFillNoiseRoad);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseRoadVo tEnvFillNoiseRoad)
        {
            return access.Create(tEnvFillNoiseRoad);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseRoad">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseRoadVo tEnvFillNoiseRoad)
        {
            return access.Edit(tEnvFillNoiseRoad);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseRoad_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseRoad_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseRoadVo tEnvFillNoiseRoad_UpdateSet, TEnvFillNoiseRoadVo tEnvFillNoiseRoad_UpdateWhere)
        {
            return access.Edit(tEnvFillNoiseRoad_UpdateSet, tEnvFillNoiseRoad_UpdateWhere);
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
        public bool Delete(TEnvFillNoiseRoadVo tEnvFillNoiseRoad)
        {
            return access.Delete(tEnvFillNoiseRoad);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillNoiseRoad.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillNoiseRoad.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //季度
            if (tEnvFillNoiseRoad.QUARTER.Trim() == "")
            {
                this.Tips.AppendLine("季度不能为空");
                return false;
            }
            //测量时间
            if (tEnvFillNoiseRoad.MEASURE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("测量时间不能为空");
                return false;
            }
            //开始月
            if (tEnvFillNoiseRoad.BEGIN_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("开始月不能为空");
                return false;
            }
            //开始日
            if (tEnvFillNoiseRoad.BEGIN_DAY.Trim() == "")
            {
                this.Tips.AppendLine("开始日不能为空");
                return false;
            }
            //星期
            if (tEnvFillNoiseRoad.WEEK.Trim() == "")
            {
                this.Tips.AppendLine("星期不能为空");
                return false;
            }
            //开始时
            if (tEnvFillNoiseRoad.BEGIN_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("开始时不能为空");
                return false;
            }
            //开始分
            if (tEnvFillNoiseRoad.BEGIN_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("开始分不能为空");
                return false;
            }
            //评价
            if (tEnvFillNoiseRoad.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillNoiseRoad.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillNoiseRoad.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillNoiseRoad.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillNoiseRoad.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillNoiseRoad.REMARK5.Trim() == "")
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
