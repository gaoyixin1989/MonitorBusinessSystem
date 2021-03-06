using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Point.Estuaries;
using i3.DataAccess.Channels.Env.Point.Estuaries;

namespace i3.BusinessLogic.Channels.Env.Point.Estuaries
{
    /// <summary>
    /// 功能：入海河口垂线表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointEstuariesVerticalLogic : LogicBase
    {

        TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical = new TEnvPointEstuariesVerticalVo();
        TEnvPointEstuariesVerticalAccess access;

        public TEnvPointEstuariesVerticalLogic()
        {
            access = new TEnvPointEstuariesVerticalAccess();
        }

        public TEnvPointEstuariesVerticalLogic(TEnvPointEstuariesVerticalVo _tEnvPointEstuariesVertical)
        {
            tEnvPointEstuariesVertical = _tEnvPointEstuariesVertical;
            access = new TEnvPointEstuariesVerticalAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            return access.GetSelectResultCount(tEnvPointEstuariesVertical);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVerticalVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVerticalVo Details(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            return access.Details(tEnvPointEstuariesVertical);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointEstuariesVerticalVo> SelectByObject(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPointEstuariesVertical, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPointEstuariesVertical, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            return access.SelectByTable(tEnvPointEstuariesVertical);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象</param>
        /// <returns></returns>
        public TEnvPointEstuariesVerticalVo SelectByObject(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            return access.SelectByObject(tEnvPointEstuariesVertical);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            return access.Create(tEnvPointEstuariesVertical);
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            return access.Edit(tEnvPointEstuariesVertical);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointEstuariesVertical_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical_UpdateSet, TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical_UpdateWhere)
        {
            return access.Edit(tEnvPointEstuariesVertical_UpdateSet, tEnvPointEstuariesVertical_UpdateWhere);
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
        public bool Delete(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            return access.Delete(tEnvPointEstuariesVertical);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvPointEstuariesVertical.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //入海河口监测点ID
            if (tEnvPointEstuariesVertical.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("入海河口监测点ID不能为空");
                return false;
            }
            //垂线名称
            if (tEnvPointEstuariesVertical.VERTICAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("垂线名称不能为空");
                return false;
            }
            //备注1
            if (tEnvPointEstuariesVertical.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPointEstuariesVertical.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPointEstuariesVertical.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPointEstuariesVertical.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPointEstuariesVertical.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }
            return true;
        }
    }
}
