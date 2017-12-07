using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Task;
using i3.DataAccess.Channels.Mis.Monitor.Task;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Task
{
    /// <summary>
    /// 功能：监测任务属性值表
    /// 创建日期：2012-11-27
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorTaskAttrbuteLogic : LogicBase
    {

        TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute = new TMisMonitorTaskAttrbuteVo();
        TMisMonitorTaskAttrbuteAccess access;

        public TMisMonitorTaskAttrbuteLogic()
        {
            access = new TMisMonitorTaskAttrbuteAccess();
        }

        public TMisMonitorTaskAttrbuteLogic(TMisMonitorTaskAttrbuteVo _tMisMonitorTaskAttrbute)
        {
            tMisMonitorTaskAttrbute = _tMisMonitorTaskAttrbute;
            access = new TMisMonitorTaskAttrbuteAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            return access.GetSelectResultCount(tMisMonitorTaskAttrbute);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskAttrbuteVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorTaskAttrbuteVo Details(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            return access.Details(tMisMonitorTaskAttrbute);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorTaskAttrbuteVo> SelectByObject(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorTaskAttrbute, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorTaskAttrbute, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            return access.SelectByTable(tMisMonitorTaskAttrbute);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">对象</param>
        /// <returns></returns>
        public TMisMonitorTaskAttrbuteVo SelectByObject(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            return access.SelectByObject(tMisMonitorTaskAttrbute);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            return access.Create(tMisMonitorTaskAttrbute);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            return access.Edit(tMisMonitorTaskAttrbute);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorTaskAttrbute_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorTaskAttrbute_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute_UpdateSet, TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute_UpdateWhere)
        {
            return access.Edit(tMisMonitorTaskAttrbute_UpdateSet, tMisMonitorTaskAttrbute_UpdateWhere);
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
        public bool Delete(TMisMonitorTaskAttrbuteVo tMisMonitorTaskAttrbute)
        {
            return access.Delete(tMisMonitorTaskAttrbute);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorTaskAttrbute.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //对象类型
            if (tMisMonitorTaskAttrbute.OBJECT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("对象类型不能为空");
                return false;
            }
            //对象ID
            if (tMisMonitorTaskAttrbute.OBJECT_ID.Trim() == "")
            {
                this.Tips.AppendLine("对象ID不能为空");
                return false;
            }
            //属性名称
            if (tMisMonitorTaskAttrbute.ATTRBUTE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("属性名称不能为空");
                return false;
            }
            //属性值
            if (tMisMonitorTaskAttrbute.ATTRBUTE_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("属性值不能为空");
                return false;
            }
            //是否删除
            if (tMisMonitorTaskAttrbute.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("是否删除不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorTaskAttrbute.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorTaskAttrbute.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorTaskAttrbute.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorTaskAttrbute.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorTaskAttrbute.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
