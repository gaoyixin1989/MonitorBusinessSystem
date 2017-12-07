using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverPlan;
using System.Data;
using i3.DataAccess.Channels.Env.Point.RiverPlan;

namespace i3.BusinessLogic.Channels.Env.Point.RiverPlan
{
    /// <summary>
    /// 功能：规划断面
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverPlanVLogic : LogicBase
    {

        TEnvPRiverPlanVVo tEnvPRiverPlanV = new TEnvPRiverPlanVVo();
        TEnvPRiverPlanVAccess access;

        public TEnvPRiverPlanVLogic()
        {
            access = new TEnvPRiverPlanVAccess();
        }

        public TEnvPRiverPlanVLogic(TEnvPRiverPlanVVo _tEnvPRiverPlanV)
        {
            tEnvPRiverPlanV = _tEnvPRiverPlanV;
            access = new TEnvPRiverPlanVAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            return access.GetSelectResultCount(tEnvPRiverPlanV);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverPlanVVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverPlanVVo Details(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            return access.Details(tEnvPRiverPlanV);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverPlanVVo> SelectByObject(TEnvPRiverPlanVVo tEnvPRiverPlanV, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiverPlanV, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverPlanVVo tEnvPRiverPlanV, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiverPlanV, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverPlanV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            return access.SelectByTable(tEnvPRiverPlanV);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象</param>
        /// <returns></returns>
        public TEnvPRiverPlanVVo SelectByObject(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            return access.SelectByObject(tEnvPRiverPlanV);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            return access.Create(tEnvPRiverPlanV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverPlanV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            return access.Edit(tEnvPRiverPlanV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverPlanV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverPlanV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverPlanVVo tEnvPRiverPlanV_UpdateSet, TEnvPRiverPlanVVo tEnvPRiverPlanV_UpdateWhere)
        {
            return access.Edit(tEnvPRiverPlanV_UpdateSet, tEnvPRiverPlanV_UpdateWhere);
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
        public bool Delete(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            return access.Delete(tEnvPRiverPlanV);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiverPlanV.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //规划断面ID
            if (tEnvPRiverPlanV.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("规划断面ID不能为空");
                return false;
            }
            //垂线名称
            if (tEnvPRiverPlanV.VERTICAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("垂线名称不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiverPlanV.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiverPlanV.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiverPlanV.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiverPlanV.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiverPlanV.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
