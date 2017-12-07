using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.MudRiver;
using System.Data;
using i3.DataAccess.Channels.Env.Point.MudRiver;

namespace i3.BusinessLogic.Channels.Env.Point.MudRiver
{

    /// <summary>
    /// 功能：沉积物（河流）
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPMudRiverVLogic : LogicBase
    {

        TEnvPMudRiverVVo tEnvPMudRiverV = new TEnvPMudRiverVVo();
        TEnvPMudRiverVAccess access;

        public TEnvPMudRiverVLogic()
        {
            access = new TEnvPMudRiverVAccess();
        }

        public TEnvPMudRiverVLogic(TEnvPMudRiverVVo _tEnvPMudRiverV)
        {
            tEnvPMudRiverV = _tEnvPMudRiverV;
            access = new TEnvPMudRiverVAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            return access.GetSelectResultCount(tEnvPMudRiverV);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPMudRiverVVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPMudRiverVVo Details(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            return access.Details(tEnvPMudRiverV);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPMudRiverVVo> SelectByObject(TEnvPMudRiverVVo tEnvPMudRiverV, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPMudRiverV, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPMudRiverVVo tEnvPMudRiverV, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPMudRiverV, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPMudRiverV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            return access.SelectByTable(tEnvPMudRiverV);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象</param>
        /// <returns></returns>
        public TEnvPMudRiverVVo SelectByObject(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            return access.SelectByObject(tEnvPMudRiverV);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            return access.Create(tEnvPMudRiverV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudRiverV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            return access.Edit(tEnvPMudRiverV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudRiverV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPMudRiverV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudRiverVVo tEnvPMudRiverV_UpdateSet, TEnvPMudRiverVVo tEnvPMudRiverV_UpdateWhere)
        {
            return access.Edit(tEnvPMudRiverV_UpdateSet, tEnvPMudRiverV_UpdateWhere);
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
        public bool Delete(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            return access.Delete(tEnvPMudRiverV);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPMudRiverV.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvPMudRiverV.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //垂线名称
            if (tEnvPMudRiverV.VERTICAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("垂线名称不能为空");
                return false;
            }
            //备注1
            if (tEnvPMudRiverV.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPMudRiverV.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPMudRiverV.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPMudRiverV.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPMudRiverV.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
