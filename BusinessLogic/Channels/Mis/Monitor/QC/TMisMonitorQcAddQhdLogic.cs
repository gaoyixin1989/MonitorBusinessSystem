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
    /// 功能：空白加标(秦皇岛)
    /// 创建日期：2013-04-28
    /// 创建人：熊卫华
    /// </summary>
    public class TMisMonitorQcAddQhdLogic : LogicBase
    {

        TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd = new TMisMonitorQcAddQhdVo();
        TMisMonitorQcAddQhdAccess access;

        public TMisMonitorQcAddQhdLogic()
        {
            access = new TMisMonitorQcAddQhdAccess();
        }

        public TMisMonitorQcAddQhdLogic(TMisMonitorQcAddQhdVo _tMisMonitorQcAddQhd)
        {
            tMisMonitorQcAddQhd = _tMisMonitorQcAddQhd;
            access = new TMisMonitorQcAddQhdAccess();
        }
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            return access.GetSelectResultCount(tMisMonitorQcAddQhd);
        }
        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcAddQhdVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcAddQhdVo Details(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            return access.Details(tMisMonitorQcAddQhd);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcAddQhdVo> SelectByObject(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorQcAddQhd, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorQcAddQhd, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            return access.SelectByTable(tMisMonitorQcAddQhd);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">对象</param>
        /// <returns></returns>
        public TMisMonitorQcAddQhdVo SelectByObject(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            return access.SelectByObject(tMisMonitorQcAddQhd);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            return access.Create(tMisMonitorQcAddQhd);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            return access.Edit(tMisMonitorQcAddQhd);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcAddQhd_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorQcAddQhd_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd_UpdateSet, TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd_UpdateWhere)
        {
            return access.Edit(tMisMonitorQcAddQhd_UpdateSet, tMisMonitorQcAddQhd_UpdateWhere);
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
        public bool Delete(TMisMonitorQcAddQhdVo tMisMonitorQcAddQhd)
        {
            return access.Delete(tMisMonitorQcAddQhd);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorQcAddQhd.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //平行样分析结果 ID
            if (tMisMonitorQcAddQhd.RESULT_ID_ADD.Trim() == "")
            {
                this.Tips.AppendLine("平行样分析结果 ID不能为空");
                return false;
            }
            //加标量
            if (tMisMonitorQcAddQhd.QC_ADD.Trim() == "")
            {
                this.Tips.AppendLine("加标量不能为空");
                return false;
            }
            //原始测定值
            if (tMisMonitorQcAddQhd.SRC_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("原始测定值不能为空");
                return false;
            }
            //加标测定值
            if (tMisMonitorQcAddQhd.ADD_RESULT_EX.Trim() == "")
            {
                this.Tips.AppendLine("加标测定值不能为空");
                return false;
            }
            //加标回收率（%）
            if (tMisMonitorQcAddQhd.ADD_BACK.Trim() == "")
            {
                this.Tips.AppendLine("加标回收率（%）不能为空");
                return false;
            }
            //加标是否合格
            if (tMisMonitorQcAddQhd.ADD_ISOK.Trim() == "")
            {
                this.Tips.AppendLine("加标是否合格不能为空");
                return false;
            }
            //质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样 9、质控平行 10、空白加标）
            if (tMisMonitorQcAddQhd.QC_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样 9、质控平行 10、空白加标）不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorQcAddQhd.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorQcAddQhd.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorQcAddQhd.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorQcAddQhd.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorQcAddQhd.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}