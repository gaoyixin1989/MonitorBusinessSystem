using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Contract;
using i3.DataAccess.Channels.Mis.Contract;

namespace i3.BusinessLogic.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：点位停产
    /// 创建日期：2013-03-13
    /// 创建人：胡方扬
    /// </summary>
    public class TMisContractPlanPointstopLogic : LogicBase
    {

        TMisContractPlanPointstopVo tMisContractPlanPointstop = new TMisContractPlanPointstopVo();
        TMisContractPlanPointstopAccess access;

        public TMisContractPlanPointstopLogic()
        {
            access = new TMisContractPlanPointstopAccess();
        }

        public TMisContractPlanPointstopLogic(TMisContractPlanPointstopVo _tMisContractPlanPointstop)
        {
            tMisContractPlanPointstop = _tMisContractPlanPointstop;
            access = new TMisContractPlanPointstopAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            return access.GetSelectResultCount(tMisContractPlanPointstop);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPlanPointstopVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPlanPointstopVo Details(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            return access.Details(tMisContractPlanPointstop);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPlanPointstopVo> SelectByObject(TMisContractPlanPointstopVo tMisContractPlanPointstop, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractPlanPointstop, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPlanPointstopVo tMisContractPlanPointstop, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractPlanPointstop, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPlanPointstop"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            return access.SelectByTable(tMisContractPlanPointstop);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPlanPointstop">对象</param>
        /// <returns></returns>
        public TMisContractPlanPointstopVo SelectByObject(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            return access.SelectByObject(tMisContractPlanPointstop);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            return access.Create(tMisContractPlanPointstop);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlanPointstop">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            return access.Edit(tMisContractPlanPointstop);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlanPointstop_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractPlanPointstop_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanPointstopVo tMisContractPlanPointstop_UpdateSet, TMisContractPlanPointstopVo tMisContractPlanPointstop_UpdateWhere)
        {
            return access.Edit(tMisContractPlanPointstop_UpdateSet, tMisContractPlanPointstop_UpdateWhere);
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
        public bool Delete(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            return access.Delete(tMisContractPlanPointstop);
        }
                /// <summary>
        /// 记录停产原因时间等信息
        /// </summary>
        /// <param name="strSubTaskArrId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool InsertStopPointForSampleItems(string strSubTaskArrId, TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            return access.InsertStopPointForSampleItems(strSubTaskArrId,tMisContractPlanPointstop);
        }
         /// <summary>
        /// 获取停产的点位列表
        /// </summary>
        /// <param name="tMisContractPlanPointstop"></param>
        /// <returns></returns>
        public DataTable GetStopPointForSampleList(TMisContractPlanPointstopVo tMisContractPlanPointstop, int iIndex, int iCount)
        {
            return access.GetStopPointForSampleList(tMisContractPlanPointstop,iIndex,iCount);
        }

         /// <summary>
        /// 获取停产的点位列表总记录数
        /// </summary>
        /// <param name="tMisContractPlanPointstop"></param>
        /// <returns></returns>
        public int GetStopPointForSampleListCount(TMisContractPlanPointstopVo tMisContractPlanPointstop)
        {
            return access.GetStopPointForSampleListCount(tMisContractPlanPointstop);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tMisContractPlanPointstop.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.CONTRACT_POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.CONTRACT_COMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.STOPRESON.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.ACTIONDATE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.ACTION_USERID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisContractPlanPointstop.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }
}
