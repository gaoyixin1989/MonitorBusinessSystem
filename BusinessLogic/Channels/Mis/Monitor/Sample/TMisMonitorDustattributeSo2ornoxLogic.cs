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
    public class TMisMonitorDustattributeSo2ornoxLogic : LogicBase
    {

        TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox = new TMisMonitorDustattributeSo2ornoxVo();
        TMisMonitorDustattributeSo2ornoxAccess access;

        public TMisMonitorDustattributeSo2ornoxLogic()
        {
            access = new TMisMonitorDustattributeSo2ornoxAccess();
        }

        public TMisMonitorDustattributeSo2ornoxLogic(TMisMonitorDustattributeSo2ornoxVo _tMisMonitorDustattributeSo2ornox)
        {
            tMisMonitorDustattributeSo2ornox = _tMisMonitorDustattributeSo2ornox;
            access = new TMisMonitorDustattributeSo2ornoxAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            return access.GetSelectResultCount(tMisMonitorDustattributeSo2ornox);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributeSo2ornoxVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributeSo2ornoxVo Details(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            return access.Details(tMisMonitorDustattributeSo2ornox);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorDustattributeSo2ornoxVo> SelectByObject(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorDustattributeSo2ornox, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorDustattributeSo2ornox, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            return access.SelectByTable(tMisMonitorDustattributeSo2ornox);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">对象</param>
        /// <returns></returns>
        public TMisMonitorDustattributeSo2ornoxVo SelectByObject(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            return access.SelectByObject(tMisMonitorDustattributeSo2ornox);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            return access.Create(tMisMonitorDustattributeSo2ornox);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            return access.Edit(tMisMonitorDustattributeSo2ornox);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattributeSo2ornox_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorDustattributeSo2ornox_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox_UpdateSet, TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox_UpdateWhere)
        {
            return access.Edit(tMisMonitorDustattributeSo2ornox_UpdateSet, tMisMonitorDustattributeSo2ornox_UpdateWhere);
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
        public bool Delete(TMisMonitorDustattributeSo2ornoxVo tMisMonitorDustattributeSo2ornox)
        {
            return access.Delete(tMisMonitorDustattributeSo2ornox);
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
            if (tMisMonitorDustattributeSo2ornox.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattributeSo2ornox.BASEINFOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //采样序号
            if (tMisMonitorDustattributeSo2ornox.SAMPLE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("采样序号不能为空");
                return false;
            }
            //滤筒编号
            if (tMisMonitorDustattributeSo2ornox.FITER_CODE.Trim() == "")
            {
                this.Tips.AppendLine("滤筒编号不能为空");
                return false;
            }
            //采样日期
            if (tMisMonitorDustattributeSo2ornox.SAMPLE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //烟气动压
            if (tMisMonitorDustattributeSo2ornox.SMOKE_MOVE_PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气动压不能为空");
                return false;
            }
            //烟气静压
            if (tMisMonitorDustattributeSo2ornox.SMOKE_STATIC_PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气静压不能为空");
                return false;
            }
            //烟气全压
            if (tMisMonitorDustattributeSo2ornox.SMOKE_ALL_PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气全压不能为空");
                return false;
            }
            //烟气计压
            if (tMisMonitorDustattributeSo2ornox.SMOKE_K_PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气计压不能为空");
                return false;
            }
            //烟气温度
            if (tMisMonitorDustattributeSo2ornox.SMOKE_TEMPERATURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气温度不能为空");
                return false;
            }
            //烟气含氧量
            if (tMisMonitorDustattributeSo2ornox.SMOKE_OXYGEN.Trim() == "")
            {
                this.Tips.AppendLine("烟气含氧量不能为空");
                return false;
            }
            //烟气流速
            if (tMisMonitorDustattributeSo2ornox.SMOKE_SPEED.Trim() == "")
            {
                this.Tips.AppendLine("烟气流速不能为空");
                return false;
            }
            //标态流量
            if (tMisMonitorDustattributeSo2ornox.NM_SPEED.Trim() == "")
            {
                this.Tips.AppendLine("标态流量不能为空");
                return false;
            }
            //SO2浓度
            if (tMisMonitorDustattributeSo2ornox.SO2_POTENCY.Trim() == "")
            {
                this.Tips.AppendLine("SO2浓度不能为空");
                return false;
            }
            //SO2折算浓度
            if (tMisMonitorDustattributeSo2ornox.SO2_PER_POTENCY.Trim() == "")
            {
                this.Tips.AppendLine("SO2折算浓度不能为空");
                return false;
            }
            //SO2排放量
            if (tMisMonitorDustattributeSo2ornox.SO2_DISCHARGE.Trim() == "")
            {
                this.Tips.AppendLine("SO2排放量不能为空");
                return false;
            }
            //NOX浓度
            if (tMisMonitorDustattributeSo2ornox.NOX_POTENCY.Trim() == "")
            {
                this.Tips.AppendLine("NOX浓度不能为空");
                return false;
            }
            //NOX折算浓度
            if (tMisMonitorDustattributeSo2ornox.NOX_PER_POTENCY.Trim() == "")
            {
                this.Tips.AppendLine("NOX折算浓度不能为空");
                return false;
            }
            //NOX排放量
            if (tMisMonitorDustattributeSo2ornox.NOX_DISCHARGE.Trim() == "")
            {
                this.Tips.AppendLine("NOX排放量不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattributeSo2ornox.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattributeSo2ornox.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattributeSo2ornox.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }
}
