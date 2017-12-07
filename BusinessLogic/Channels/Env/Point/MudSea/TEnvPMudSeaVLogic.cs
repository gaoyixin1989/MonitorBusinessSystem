using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.MudSea;
using System.Data;
using i3.DataAccess.Channels.Env.Point.MudSea;

namespace i3.BusinessLogic.Channels.Env.Point.MudSea
{
    /// <summary>
    /// 功能：沉积物（海水）
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPMudSeaVLogic : LogicBase
    {

        TEnvPMudSeaVVo tEnvPMudSeaV = new TEnvPMudSeaVVo();
        TEnvPMudSeaVAccess access;

        public TEnvPMudSeaVLogic()
        {
            access = new TEnvPMudSeaVAccess();
        }

        public TEnvPMudSeaVLogic(TEnvPMudSeaVVo _tEnvPMudSeaV)
        {
            tEnvPMudSeaV = _tEnvPMudSeaV;
            access = new TEnvPMudSeaVAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            return access.GetSelectResultCount(tEnvPMudSeaV);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPMudSeaVVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPMudSeaVVo Details(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            return access.Details(tEnvPMudSeaV);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPMudSeaVVo> SelectByObject(TEnvPMudSeaVVo tEnvPMudSeaV, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPMudSeaV, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPMudSeaVVo tEnvPMudSeaV, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPMudSeaV, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPMudSeaV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            return access.SelectByTable(tEnvPMudSeaV);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPMudSeaV">对象</param>
        /// <returns></returns>
        public TEnvPMudSeaVVo SelectByObject(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            return access.SelectByObject(tEnvPMudSeaV);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            return access.Create(tEnvPMudSeaV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudSeaV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            return access.Edit(tEnvPMudSeaV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudSeaV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPMudSeaV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudSeaVVo tEnvPMudSeaV_UpdateSet, TEnvPMudSeaVVo tEnvPMudSeaV_UpdateWhere)
        {
            return access.Edit(tEnvPMudSeaV_UpdateSet, tEnvPMudSeaV_UpdateWhere);
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
        public bool Delete(TEnvPMudSeaVVo tEnvPMudSeaV)
        {
            return access.Delete(tEnvPMudSeaV);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPMudSeaV.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvPMudSeaV.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //垂线名称
            if (tEnvPMudSeaV.VERTICAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("垂线名称不能为空");
                return false;
            }
            //备注1
            if (tEnvPMudSeaV.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPMudSeaV.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPMudSeaV.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPMudSeaV.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPMudSeaV.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
