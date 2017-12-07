using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.RiverTarget;
using i3.DataAccess.Channels.Env.Fill.RiverTarget;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.RiverTarget
{
    /// <summary>
    /// 功能：责任目标
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverTargetLogic : LogicBase
    {

        TEnvFillRiverTargetVo tEnvFillRiverTarget = new TEnvFillRiverTargetVo();
        TEnvFillRiverTargetAccess access;

        public TEnvFillRiverTargetLogic()
        {
            access = new TEnvFillRiverTargetAccess();
        }

        public TEnvFillRiverTargetLogic(TEnvFillRiverTargetVo _tEnvFillRiverTarget)
        {
            tEnvFillRiverTarget = _tEnvFillRiverTarget;
            access = new TEnvFillRiverTargetAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverTarget">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverTargetVo tEnvFillRiverTarget)
        {
            return access.GetSelectResultCount(tEnvFillRiverTarget);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverTargetVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverTarget">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverTargetVo Details(TEnvFillRiverTargetVo tEnvFillRiverTarget)
        {
            return access.Details(tEnvFillRiverTarget);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverTarget">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverTargetVo> SelectByObject(TEnvFillRiverTargetVo tEnvFillRiverTarget, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiverTarget, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverTarget">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverTargetVo tEnvFillRiverTarget, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiverTarget, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverTarget"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverTargetVo tEnvFillRiverTarget)
        {
            return access.SelectByTable(tEnvFillRiverTarget);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverTarget">对象</param>
        /// <returns></returns>
        public TEnvFillRiverTargetVo SelectByObject(TEnvFillRiverTargetVo tEnvFillRiverTarget)
        {
            return access.SelectByObject(tEnvFillRiverTarget);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverTargetVo tEnvFillRiverTarget)
        {
            return access.Create(tEnvFillRiverTarget);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverTarget">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverTargetVo tEnvFillRiverTarget)
        {
            return access.Edit(tEnvFillRiverTarget);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverTarget_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverTarget_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverTargetVo tEnvFillRiverTarget_UpdateSet, TEnvFillRiverTargetVo tEnvFillRiverTarget_UpdateWhere)
        {
            return access.Edit(tEnvFillRiverTarget_UpdateSet, tEnvFillRiverTarget_UpdateWhere);
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
        public bool Delete(TEnvFillRiverTargetVo tEnvFillRiverTarget)
        {
            return access.Delete(tEnvFillRiverTarget);
        }
        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            return access.PointByTable(strYear, strMonth);
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
            if (tEnvFillRiverTarget.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvFillRiverTarget.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillRiverTarget.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillRiverTarget.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillRiverTarget.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillRiverTarget.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillRiverTarget.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillRiverTarget.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillRiverTarget.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillRiverTarget.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //评价
            if (tEnvFillRiverTarget.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillRiverTarget.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiverTarget.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiverTarget.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiverTarget.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiverTarget.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRiverTarget.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
