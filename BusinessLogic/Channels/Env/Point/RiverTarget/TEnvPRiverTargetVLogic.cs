using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverTarget;
using System.Data;
using i3.DataAccess.Channels.Env.Point.RiverTarget;

namespace i3.BusinessLogic.Channels.Env.Point.RiverTarget
{
    /// <summary>
    /// 功能：责任目标
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverTargetVLogic : LogicBase
    {

        TEnvPRiverTargetVVo tEnvPRiverTargetV = new TEnvPRiverTargetVVo();
        TEnvPRiverTargetVAccess access;

        public TEnvPRiverTargetVLogic()
        {
            access = new TEnvPRiverTargetVAccess();
        }

        public TEnvPRiverTargetVLogic(TEnvPRiverTargetVVo _tEnvPRiverTargetV)
        {
            tEnvPRiverTargetV = _tEnvPRiverTargetV;
            access = new TEnvPRiverTargetVAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            return access.GetSelectResultCount(tEnvPRiverTargetV);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverTargetVVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverTargetVVo Details(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            return access.Details(tEnvPRiverTargetV);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverTargetVVo> SelectByObject(TEnvPRiverTargetVVo tEnvPRiverTargetV, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiverTargetV, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverTargetVVo tEnvPRiverTargetV, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiverTargetV, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverTargetV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            return access.SelectByTable(tEnvPRiverTargetV);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverTargetV">对象</param>
        /// <returns></returns>
        public TEnvPRiverTargetVVo SelectByObject(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            return access.SelectByObject(tEnvPRiverTargetV);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            return access.Create(tEnvPRiverTargetV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverTargetV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            return access.Edit(tEnvPRiverTargetV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverTargetV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverTargetV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverTargetVVo tEnvPRiverTargetV_UpdateSet, TEnvPRiverTargetVVo tEnvPRiverTargetV_UpdateWhere)
        {
            return access.Edit(tEnvPRiverTargetV_UpdateSet, tEnvPRiverTargetV_UpdateWhere);
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
        public bool Delete(TEnvPRiverTargetVVo tEnvPRiverTargetV)
        {
            return access.Delete(tEnvPRiverTargetV);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiverTargetV.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //责任目标断面ID
            if (tEnvPRiverTargetV.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("责任目标断面ID不能为空");
                return false;
            }
            //垂线名称
            if (tEnvPRiverTargetV.VERTICAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("垂线名称不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiverTargetV.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiverTargetV.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiverTargetV.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiverTargetV.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiverTargetV.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
