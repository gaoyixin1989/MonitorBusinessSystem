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
    /// 功能：颗粒物原始记录表-属性表
    /// 创建日期：2013-07-09
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorDustattributeLogic : LogicBase
    {

        TMisMonitorDustattributeVo tMisMonitorDustattribute = new TMisMonitorDustattributeVo();
        TMisMonitorDustattributeAccess access;

        public TMisMonitorDustattributeLogic()
        {
            access = new TMisMonitorDustattributeAccess();
        }

        public TMisMonitorDustattributeLogic(TMisMonitorDustattributeVo _tMisMonitorDustattribute)
        {
            tMisMonitorDustattribute = _tMisMonitorDustattribute;
            access = new TMisMonitorDustattributeAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            return access.GetSelectResultCount(tMisMonitorDustattribute);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorDustattributeVo Details(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            return access.Details(tMisMonitorDustattribute);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorDustattributeVo> SelectByObject(TMisMonitorDustattributeVo tMisMonitorDustattribute, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorDustattribute, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorDustattributeVo tMisMonitorDustattribute, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorDustattribute, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorDustattribute"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            return access.SelectByTable(tMisMonitorDustattribute);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorDustattribute">对象</param>
        /// <returns></returns>
        public TMisMonitorDustattributeVo SelectByObject(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            return access.SelectByObject(tMisMonitorDustattribute);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            return access.Create(tMisMonitorDustattribute);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattribute">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            return access.Edit(tMisMonitorDustattribute);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustattribute_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorDustattribute_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustattributeVo tMisMonitorDustattribute_UpdateSet, TMisMonitorDustattributeVo tMisMonitorDustattribute_UpdateWhere)
        {
            return access.Edit(tMisMonitorDustattribute_UpdateSet, tMisMonitorDustattribute_UpdateWhere);
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
        public bool Delete(TMisMonitorDustattributeVo tMisMonitorDustattribute)
        {
            return access.Delete(tMisMonitorDustattribute);
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
            if (tMisMonitorDustattribute.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattribute.BASEINFOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //采样序号
            if (tMisMonitorDustattribute.SAMPLE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("采样序号不能为空");
                return false;
            }
            //滤筒编号
            if (tMisMonitorDustattribute.FITER_CODE.Trim() == "")
            {
                this.Tips.AppendLine("滤筒编号不能为空");
                return false;
            }
            //采样日期
            if (tMisMonitorDustattribute.SAMPLE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //烟气动压
            if (tMisMonitorDustattribute.SMOKE_MOVE_PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气动压不能为空");
                return false;
            }
            //烟气静压
            if (tMisMonitorDustattribute.SMOKE_STATIC_PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气静压不能为空");
                return false;
            }
            //烟气全压
            if (tMisMonitorDustattribute.SMOKE_ALL_PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气全压不能为空");
                return false;
            }
            //烟气计压
            if (tMisMonitorDustattribute.SMOKE_K_PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气计压不能为空");
                return false;
            }
            //烟气温度
            if (tMisMonitorDustattribute.SMOKE_TEMPERATURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气温度不能为空");
                return false;
            }
            //烟气含氧量
            if (tMisMonitorDustattribute.SMOKE_OXYGEN.Trim() == "")
            {
                this.Tips.AppendLine("烟气含氧量不能为空");
                return false;
            }
            //烟气流速
            if (tMisMonitorDustattribute.SMOKE_SPEED.Trim() == "")
            {
                this.Tips.AppendLine("烟气流速不能为空");
                return false;
            }
            //标态流量
            if (tMisMonitorDustattribute.NM_SPEED.Trim() == "")
            {
                this.Tips.AppendLine("标态流量不能为空");
                return false;
            }
            //标况体积
            if (tMisMonitorDustattribute.L_STAND.Trim() == "")
            {
                this.Tips.AppendLine("标况体积不能为空");
                return false;
            }
            //滤筒初重
            if (tMisMonitorDustattribute.FITER_BEGIN_WEIGHT.Trim() == "")
            {
                this.Tips.AppendLine("滤筒初重不能为空");
                return false;
            }
            //滤筒终重
            if (tMisMonitorDustattribute.FITER_AFTER_WEIGHT.Trim() == "")
            {
                this.Tips.AppendLine("滤筒终重不能为空");
                return false;
            }
            //样品重量
            if (tMisMonitorDustattribute.SAMPLE_WEIGHT.Trim() == "")
            {
                this.Tips.AppendLine("样品重量不能为空");
                return false;
            }
            //烟尘浓度
            if (tMisMonitorDustattribute.SMOKE_POTENCY.Trim() == "")
            {
                this.Tips.AppendLine("烟尘浓度不能为空");
                return false;
            }
            //烟尘折算浓度
            if (tMisMonitorDustattribute.SMOKE_POTENCY2.Trim() == "")
            {
                this.Tips.AppendLine("烟尘折算浓度不能为空");
                return false;
            }
            //烟尘排放量
            if (tMisMonitorDustattribute.SMOKE_DISCHARGE.Trim() == "")
            {
                this.Tips.AppendLine("烟尘排放量不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattribute.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattribute.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorDustattribute.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }
}
