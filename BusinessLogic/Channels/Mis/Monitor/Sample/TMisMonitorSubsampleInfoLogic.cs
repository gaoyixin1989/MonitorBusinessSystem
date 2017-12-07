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
    /// 功能：采样样品子样
    /// 创建日期：2013-04-08
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorSubsampleInfoLogic : LogicBase
    {

        TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo = new TMisMonitorSubsampleInfoVo();
        TMisMonitorSubsampleInfoAccess access;

        public TMisMonitorSubsampleInfoLogic()
        {
            access = new TMisMonitorSubsampleInfoAccess();
        }

        public TMisMonitorSubsampleInfoLogic(TMisMonitorSubsampleInfoVo _tMisMonitorSubsampleInfo)
        {
            tMisMonitorSubsampleInfo = _tMisMonitorSubsampleInfo;
            access = new TMisMonitorSubsampleInfoAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            return access.GetSelectResultCount(tMisMonitorSubsampleInfo);
        }
                /// <summary>
        /// 插入子样品数据
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo"></param>
        /// <param name="strCode"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        public bool InsertSubSample(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo, string strCode, int Number) {
            return access.InsertSubSample(tMisMonitorSubsampleInfo, strCode, Number);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSubsampleInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSubsampleInfoVo Details(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            return access.Details(tMisMonitorSubsampleInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSubsampleInfoVo> SelectByObject(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorSubsampleInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorSubsampleInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            return access.SelectByTable(tMisMonitorSubsampleInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">对象</param>
        /// <returns></returns>
        public TMisMonitorSubsampleInfoVo SelectByObject(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            return access.SelectByObject(tMisMonitorSubsampleInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            return access.Create(tMisMonitorSubsampleInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            return access.Edit(tMisMonitorSubsampleInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSubsampleInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorSubsampleInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo_UpdateSet, TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo_UpdateWhere)
        {
            return access.Edit(tMisMonitorSubsampleInfo_UpdateSet, tMisMonitorSubsampleInfo_UpdateWhere);
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
        public bool Delete(TMisMonitorSubsampleInfoVo tMisMonitorSubsampleInfo)
        {
            return access.Delete(tMisMonitorSubsampleInfo);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tMisMonitorSubsampleInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorSubsampleInfo.SUBSAMPLE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorSubsampleInfo.SAMPLEID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorSubsampleInfo.ACTIONDATE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }
}
