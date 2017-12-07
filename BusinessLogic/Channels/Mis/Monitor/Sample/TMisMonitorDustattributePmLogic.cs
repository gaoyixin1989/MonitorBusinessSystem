using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.DataAccess.Channels.Mis.Monitor.Sample;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：PM10和悬浮颗粒物原始记录表
    /// 创建日期：2013-08-29
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorDustattributePmLogic : LogicBase
    {

        TMisMonitorDustattributePmVo tMisMonitorDustattributePm = new TMisMonitorDustattributePmVo();
        TMisMonitorDustattributePmAccess access;

        public TMisMonitorDustattributePmLogic()
        {
            access = new TMisMonitorDustattributePmAccess();
        }

        public TMisMonitorDustattributePmLogic(TMisMonitorDustattributePmVo _tMisMonitorDustattributePm)
        {
            tMisMonitorDustattributePm = _tMisMonitorDustattributePm;
            access = new TMisMonitorDustattributePmAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            return access.GetSelectResultCount(tMisMonitorDustattributePm);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributePmVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributePmVo Details(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            return access.Details(tMisMonitorDustattributePm);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorDustattributePmVo> SelectByObject(TMisMonitorDustattributePmVo tMisMonitorDustattributePm, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorDustattributePm, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorDustattributePmVo tMisMonitorDustattributePm, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorDustattributePm, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorDustattributePm"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            return access.SelectByTable(tMisMonitorDustattributePm);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">对象</param>
        /// <returns></returns>
        public TMisMonitorDustattributePmVo SelectByObject(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            return access.SelectByObject(tMisMonitorDustattributePm);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            return access.Create(tMisMonitorDustattributePm);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattributePm">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            return access.Edit(tMisMonitorDustattributePm);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattributePm_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorDustattributePm_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributePmVo tMisMonitorDustattributePm_UpdateSet, TMisMonitorDustattributePmVo tMisMonitorDustattributePm_UpdateWhere)
        {
            return access.Edit(tMisMonitorDustattributePm_UpdateSet, tMisMonitorDustattributePm_UpdateWhere);
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
        public bool Delete(TMisMonitorDustattributePmVo tMisMonitorDustattributePm)
        {
            return access.Delete(tMisMonitorDustattributePm);
        }

                /// <summary>
        /// 创建原因：更新属性数据列
        /// 创建人：胡方扬
        /// 创建日期：2013-07-05
        /// </summary>
        /// <param name="strId"></param>
        /// <param name="strCellName"></param>
        /// <param name="strCellValue"></param>
        /// <returns></returns>
        public bool UpdateCell(string strId, string strCellName, string strCellValue)
        {
            return access.UpdateCell(strId,strCellName,strCellValue);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tMisMonitorDustattributePm.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattributePm.BASEINFOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //采样序号
            if (tMisMonitorDustattributePm.SAMPLE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("采样序号不能为空");
                return false;
            }
            //滤筒编号
            if (tMisMonitorDustattributePm.FITER_CODE.Trim() == "")
            {
                this.Tips.AppendLine("滤筒编号不能为空");
                return false;
            }
            //采样开始日期
            if (tMisMonitorDustattributePm.SAMPLE_BEGINDATE.Trim() == "")
            {
                this.Tips.AppendLine("采样开始日期不能为空");
                return false;
            }
            //采样结束日期
            if (tMisMonitorDustattributePm.SAMPLE_ENDDATE.Trim() == "")
            {
                this.Tips.AppendLine("采样结束日期不能为空");
                return false;
            }
            //采样累计时间
            if (tMisMonitorDustattributePm.ACCTIME.Trim() == "")
            {
                this.Tips.AppendLine("采样累计时间不能为空");
                return false;
            }
            //采样体积
            if (tMisMonitorDustattributePm.SAMPLE_L_STAND.Trim() == "")
            {
                this.Tips.AppendLine("采样体积不能为空");
                return false;
            }
            //标况采样体积
            if (tMisMonitorDustattributePm.L_STAND.Trim() == "")
            {
                this.Tips.AppendLine("标况采样体积不能为空");
                return false;
            }
            //标态流量
            if (tMisMonitorDustattributePm.NM_SPEED.Trim() == "")
            {
                this.Tips.AppendLine("标态流量不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattributePm.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattributePm.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattributePm.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }
}
