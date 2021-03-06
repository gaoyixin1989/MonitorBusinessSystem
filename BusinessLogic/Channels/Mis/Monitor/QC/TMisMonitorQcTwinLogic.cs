using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.QC;
using i3.DataAccess.Channels.Mis.Monitor.QC;

namespace i3.BusinessLogic.Channels.Mis.Monitor.QC
{
    /// <summary>
    /// 功能：平行样结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcTwinLogic : LogicBase
    {

        TMisMonitorQcTwinVo tMisMonitorQcTwin = new TMisMonitorQcTwinVo();
        TMisMonitorQcTwinAccess access;

        public TMisMonitorQcTwinLogic()
        {
            access = new TMisMonitorQcTwinAccess();
        }

        public TMisMonitorQcTwinLogic(TMisMonitorQcTwinVo _tMisMonitorQcTwin)
        {
            tMisMonitorQcTwin = _tMisMonitorQcTwin;
            access = new TMisMonitorQcTwinAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            return access.GetSelectResultCount(tMisMonitorQcTwin);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcTwinVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcTwinVo Details(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            return access.Details(tMisMonitorQcTwin);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcTwinVo> SelectByObject(TMisMonitorQcTwinVo tMisMonitorQcTwin, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorQcTwin, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcTwinVo tMisMonitorQcTwin, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorQcTwin, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcTwin"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            return access.SelectByTable(tMisMonitorQcTwin);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcTwin">对象</param>
        /// <returns></returns>
        public TMisMonitorQcTwinVo SelectByObject(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            return access.SelectByObject(tMisMonitorQcTwin);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            return access.Create(tMisMonitorQcTwin);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcTwin">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            return access.Edit(tMisMonitorQcTwin);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcTwin_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorQcTwin_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcTwinVo tMisMonitorQcTwin_UpdateSet, TMisMonitorQcTwinVo tMisMonitorQcTwin_UpdateWhere)
        {
            return access.Edit(tMisMonitorQcTwin_UpdateSet, tMisMonitorQcTwin_UpdateWhere);
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
        public bool Delete(TMisMonitorQcTwinVo tMisMonitorQcTwin)
        {
            return access.Delete(tMisMonitorQcTwin);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorQcTwin.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //原始样分析结果 ID
            if (tMisMonitorQcTwin.RESULT_ID_SRC.Trim() == "")
            {
                this.Tips.AppendLine("原始样分析结果 ID不能为空");
                return false;
            }
            //平行样分析结果ID1
            if (tMisMonitorQcTwin.RESULT_ID_TWIN1.Trim() == "")
            {
                this.Tips.AppendLine("平行样分析结果ID1不能为空");
                return false;
            }
            //平行样分析结果ID2
            if (tMisMonitorQcTwin.RESULT_ID_TWIN2.Trim() == "")
            {
                this.Tips.AppendLine("平行样分析结果ID2不能为空");
                return false;
            }
            //平行样测定值1
            if (tMisMonitorQcTwin.TWIN_RESULT1.Trim() == "")
            {
                this.Tips.AppendLine("平行样测定值1不能为空");
                return false;
            }
            //平行样测定值2
            if (tMisMonitorQcTwin.TWIN_RESULT2.Trim() == "")
            {
                this.Tips.AppendLine("平行样测定值2不能为空");
                return false;
            }
            //平行测定均值
            if (tMisMonitorQcTwin.TWIN_AVG.Trim() == "")
            {
                this.Tips.AppendLine("平行测定均值不能为空");
                return false;
            }
            //相对偏差（%）
            if (tMisMonitorQcTwin.TWIN_OFFSET.Trim() == "")
            {
                this.Tips.AppendLine("相对偏差（%）不能为空");
                return false;
            }
            //是否合格
            if (tMisMonitorQcTwin.TWIN_ISOK.Trim() == "")
            {
                this.Tips.AppendLine("是否合格不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorQcTwin.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorQcTwin.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorQcTwin.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorQcTwin.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorQcTwin.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
