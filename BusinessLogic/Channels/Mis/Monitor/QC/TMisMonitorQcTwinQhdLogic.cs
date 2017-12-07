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
    /// 功能：质控平行(秦皇岛)
    /// 创建日期：2013-04-28
    /// 创建人：熊卫华
    /// </summary>
    public class TMisMonitorQcTwinQhdLogic : LogicBase
    {

        TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd = new TMisMonitorQcTwinQhdVo();
        TMisMonitorQcTwinQhdAccess access;

        public TMisMonitorQcTwinQhdLogic()
        {
            access = new TMisMonitorQcTwinQhdAccess();
        }

        public TMisMonitorQcTwinQhdLogic(TMisMonitorQcTwinQhdVo _tMisMonitorQcTwinQhd)
        {
            tMisMonitorQcTwinQhd = _tMisMonitorQcTwinQhd;
            access = new TMisMonitorQcTwinQhdAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            return access.GetSelectResultCount(tMisMonitorQcTwinQhd);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorQcTwinQhdVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorQcTwinQhdVo Details(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            return access.Details(tMisMonitorQcTwinQhd);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorQcTwinQhdVo> SelectByObject(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorQcTwinQhd, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorQcTwinQhd, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            return access.SelectByTable(tMisMonitorQcTwinQhd);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">对象</param>
        /// <returns></returns>
        public TMisMonitorQcTwinQhdVo SelectByObject(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            return access.SelectByObject(tMisMonitorQcTwinQhd);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            return access.Create(tMisMonitorQcTwinQhd);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            return access.Edit(tMisMonitorQcTwinQhd);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorQcTwinQhd_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorQcTwinQhd_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd_UpdateSet, TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd_UpdateWhere)
        {
            return access.Edit(tMisMonitorQcTwinQhd_UpdateSet, tMisMonitorQcTwinQhd_UpdateWhere);
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
        public bool Delete(TMisMonitorQcTwinQhdVo tMisMonitorQcTwinQhd)
        {
            return access.Delete(tMisMonitorQcTwinQhd);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorQcTwinQhd.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //原始样分析结果 ID
            if (tMisMonitorQcTwinQhd.RESULT_ID_SRC.Trim() == "")
            {
                this.Tips.AppendLine("原始样分析结果 ID不能为空");
                return false;
            }
            //平行样分析结果ID1
            if (tMisMonitorQcTwinQhd.RESULT_ID_TWIN1.Trim() == "")
            {
                this.Tips.AppendLine("平行样分析结果ID1不能为空");
                return false;
            }
            //平行样分析结果ID2
            if (tMisMonitorQcTwinQhd.RESULT_ID_TWIN2.Trim() == "")
            {
                this.Tips.AppendLine("平行样分析结果ID2不能为空");
                return false;
            }
            //平行样测定值1
            if (tMisMonitorQcTwinQhd.TWIN_RESULT1.Trim() == "")
            {
                this.Tips.AppendLine("平行样测定值1不能为空");
                return false;
            }
            //平行样测定值2
            if (tMisMonitorQcTwinQhd.TWIN_RESULT2.Trim() == "")
            {
                this.Tips.AppendLine("平行样测定值2不能为空");
                return false;
            }
            //平行测定均值
            if (tMisMonitorQcTwinQhd.TWIN_AVG.Trim() == "")
            {
                this.Tips.AppendLine("平行测定均值不能为空");
                return false;
            }
            //相对偏差（%）
            if (tMisMonitorQcTwinQhd.TWIN_OFFSET.Trim() == "")
            {
                this.Tips.AppendLine("相对偏差（%）不能为空");
                return false;
            }
            //是否合格
            if (tMisMonitorQcTwinQhd.TWIN_ISOK.Trim() == "")
            {
                this.Tips.AppendLine("是否合格不能为空");
                return false;
            }
            //质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样  9、质控平行 10、空白加标）
            if (tMisMonitorQcTwinQhd.QC_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("质控类型（0、原始样；1、现场空白；2、现场加标；3、现场平行；4、实验室密码平行；5、实验室空白；6、实验室加标；7、实验室明码平行；8、标准样  9、质控平行 10、空白加标）不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorQcTwinQhd.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorQcTwinQhd.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorQcTwinQhd.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorQcTwinQhd.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorQcTwinQhd.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
