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
    /// 功能：现场空白结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcEmptyOutLogic : LogicBase
    {

        TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut = new TMisMonitorQcEmptyOutVo();
        TMisMonitorQcEmptyOutAccess access;

        public TMisMonitorQcEmptyOutLogic()
        {
            access = new TMisMonitorQcEmptyOutAccess();
        }

        public TMisMonitorQcEmptyOutLogic(TMisMonitorQcEmptyOutVo _tMisMonitorQcEmptyOut)
        {
            tMisMonitorQcEmptyOut = _tMisMonitorQcEmptyOut;
            access = new TMisMonitorQcEmptyOutAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            return access.GetSelectResultCount(tMisMonitorQcEmptyOut);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcEmptyOutVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcEmptyOutVo Details(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            return access.Details(tMisMonitorQcEmptyOut);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcEmptyOutVo> SelectByObject(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorQcEmptyOut, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorQcEmptyOut, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            return access.SelectByTable(tMisMonitorQcEmptyOut);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">对象</param>
        /// <returns></returns>
        public TMisMonitorQcEmptyOutVo SelectByObject(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            return access.SelectByObject(tMisMonitorQcEmptyOut);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            return access.Create(tMisMonitorQcEmptyOut);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            return access.Edit(tMisMonitorQcEmptyOut);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcEmptyOut_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorQcEmptyOut_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut_UpdateSet, TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut_UpdateWhere)
        {
            return access.Edit(tMisMonitorQcEmptyOut_UpdateSet, tMisMonitorQcEmptyOut_UpdateWhere);
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
        public bool Delete(TMisMonitorQcEmptyOutVo tMisMonitorQcEmptyOut)
        {
            return access.Delete(tMisMonitorQcEmptyOut);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorQcEmptyOut.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //原始样分析结果 ID
            if (tMisMonitorQcEmptyOut.RESULT_ID_SRC.Trim() == "")
            {
                this.Tips.AppendLine("原始样分析结果 ID不能为空");
                return false;
            }
            //空白样分析结果 ID
            if (tMisMonitorQcEmptyOut.RESULT_ID_EMPTY.Trim() == "")
            {
                this.Tips.AppendLine("空白样分析结果 ID不能为空");
                return false;
            }
            //
            if (tMisMonitorQcEmptyOut.RESULT_EMPTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //相对偏差（%）
            if (tMisMonitorQcEmptyOut.EMPTY_OFFSET.Trim() == "")
            {
                this.Tips.AppendLine("相对偏差（%）不能为空");
                return false;
            }
            //空白是否合格
            if (tMisMonitorQcEmptyOut.EMPTY_ISOK.Trim() == "")
            {
                this.Tips.AppendLine("空白是否合格不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorQcEmptyOut.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorQcEmptyOut.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorQcEmptyOut.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorQcEmptyOut.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorQcEmptyOut.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
