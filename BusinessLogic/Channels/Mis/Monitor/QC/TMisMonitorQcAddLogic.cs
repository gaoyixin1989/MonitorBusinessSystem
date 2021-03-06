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
    /// 功能：加标样结果表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorQcAddLogic : LogicBase
    {

        TMisMonitorQcAddVo tMisMonitorQcAdd = new TMisMonitorQcAddVo();
        TMisMonitorQcAddAccess access;

        public TMisMonitorQcAddLogic()
        {
            access = new TMisMonitorQcAddAccess();
        }

        public TMisMonitorQcAddLogic(TMisMonitorQcAddVo _tMisMonitorQcAdd)
        {
            tMisMonitorQcAdd = _tMisMonitorQcAdd;
            access = new TMisMonitorQcAddAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            return access.GetSelectResultCount(tMisMonitorQcAdd);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcAddVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcAddVo Details(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            return access.Details(tMisMonitorQcAdd);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcAddVo> SelectByObject(TMisMonitorQcAddVo tMisMonitorQcAdd, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorQcAdd, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcAddVo tMisMonitorQcAdd, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorQcAdd, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcAdd"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            return access.SelectByTable(tMisMonitorQcAdd);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcAdd">对象</param>
        /// <returns></returns>
        public TMisMonitorQcAddVo SelectByObject(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            return access.SelectByObject(tMisMonitorQcAdd);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            return access.Create(tMisMonitorQcAdd);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcAdd">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            return access.Edit(tMisMonitorQcAdd);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcAdd_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorQcAdd_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcAddVo tMisMonitorQcAdd_UpdateSet, TMisMonitorQcAddVo tMisMonitorQcAdd_UpdateWhere)
        {
            return access.Edit(tMisMonitorQcAdd_UpdateSet, tMisMonitorQcAdd_UpdateWhere);
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
        public bool Delete(TMisMonitorQcAddVo tMisMonitorQcAdd)
        {
            return access.Delete(tMisMonitorQcAdd);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorQcAdd.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //原始样分析结果 ID
            if (tMisMonitorQcAdd.RESULT_ID_SRC.Trim() == "")
            {
                this.Tips.AppendLine("原始样分析结果 ID不能为空");
                return false;
            }
            //平行样分析结果 ID
            if (tMisMonitorQcAdd.RESULT_ID_ADD.Trim() == "")
            {
                this.Tips.AppendLine("平行样分析结果 ID不能为空");
                return false;
            }
            //加标量
            if (tMisMonitorQcAdd.QC_ADD.Trim() == "")
            {
                this.Tips.AppendLine("加标量不能为空");
                return false;
            }
            //原始测定值
            if (tMisMonitorQcAdd.SRC_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("原始测定值不能为空");
                return false;
            }
            //加标测定值
            if (tMisMonitorQcAdd.ADD_RESULT_EX.Trim() == "")
            {
                this.Tips.AppendLine("加标测定值不能为空");
                return false;
            }
            //加标回收率（%）
            if (tMisMonitorQcAdd.ADD_BACK.Trim() == "")
            {
                this.Tips.AppendLine("加标回收率（%）不能为空");
                return false;
            }
            //加标是否合格
            if (tMisMonitorQcAdd.ADD_ISOK.Trim() == "")
            {
                this.Tips.AppendLine("加标是否合格不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorQcAdd.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorQcAdd.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorQcAdd.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorQcAdd.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorQcAdd.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
