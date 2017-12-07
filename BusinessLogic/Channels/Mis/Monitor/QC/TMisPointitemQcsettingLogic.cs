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
    /// 功能：环境质量质控设置监测项目信息
    /// 创建日期：2013-06-25
    /// 创建人：胡方扬
    /// </summary>
    public class TMisPointitemQcsettingLogic : LogicBase
    {

        TMisPointitemQcsettingVo tMisPointitemQcsetting = new TMisPointitemQcsettingVo();
        TMisPointitemQcsettingAccess access;

        public TMisPointitemQcsettingLogic()
        {
            access = new TMisPointitemQcsettingAccess();
        }

        public TMisPointitemQcsettingLogic(TMisPointitemQcsettingVo _tMisPointitemQcsetting)
        {
            tMisPointitemQcsetting = _tMisPointitemQcsetting;
            access = new TMisPointitemQcsettingAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            return access.GetSelectResultCount(tMisPointitemQcsetting);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisPointitemQcsettingVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象条件</param>
        /// <returns>对象</returns>
        public TMisPointitemQcsettingVo Details(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            return access.Details(tMisPointitemQcsetting);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisPointitemQcsettingVo> SelectByObject(TMisPointitemQcsettingVo tMisPointitemQcsetting, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisPointitemQcsetting, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisPointitemQcsettingVo tMisPointitemQcsetting, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisPointitemQcsetting, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisPointitemQcsetting"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            return access.SelectByTable(tMisPointitemQcsetting);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisPointitemQcsetting">对象</param>
        /// <returns></returns>
        public TMisPointitemQcsettingVo SelectByObject(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            return access.SelectByObject(tMisPointitemQcsetting);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            return access.Create(tMisPointitemQcsetting);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisPointitemQcsetting">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            return access.Edit(tMisPointitemQcsetting);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisPointitemQcsetting_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisPointitemQcsetting_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisPointitemQcsettingVo tMisPointitemQcsetting_UpdateSet, TMisPointitemQcsettingVo tMisPointitemQcsetting_UpdateWhere)
        {
            return access.Edit(tMisPointitemQcsetting_UpdateSet, tMisPointitemQcsetting_UpdateWhere);
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
        public bool Delete(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            return access.Delete(tMisPointitemQcsetting);
        }

         /// <summary>
        /// 创建原因：插入环境质量质控设置点位监测项目表信息 
        /// 创建人：胡方扬
        /// 创建日期：2013-06-25
        /// </summary>
        /// <param name="tMisPointitemQcsetting">质控设置监测信息</param>
        /// <param name="strPointId">基础资料点位ID</param>
        /// <param name="strTableName">获取监测项目主表名称</param>
        /// <param name="strKeyColumns">获取监测项目主表条件</param>
        /// <returns></returns>
        public bool InsertQcSettingPointItems(TMisPointitemQcsettingVo tMisPointitemQcsetting, string strPointId,string strPointItemsName)
        {
            return access.InsertQcSettingPointItems(tMisPointitemQcsetting, strPointId, strPointItemsName);
        }

        /// <summary>
        /// 创建原因：动态获取环境质量监测点位的监测项目信息
        /// 创建人：胡方扬
        /// 创建日期：2013-06-25
        /// </summary>
        /// <param name="strTableName">主表名称</param>
        /// <param name="strKeyColumns">关键字名称</param>
        /// <param name="strPointId">关键字值</param>
        /// <returns></returns>
        public DataTable GetEnvPointItems(string strTableName, string strKeyColumns, string strPointId) {
            return access.GetEnvPointItems(strTableName, strKeyColumns, strPointId);
        }

                /// <summary>
        /// 创建原因：动态获取环境质量监测点位的监测项目信息 有父表的数据
        /// 创建人：胡方扬
        /// 创建日期：2013-06-25
        /// </summary>
        /// <param name="strTableName">主表名称</param>
        /// <param name="strKeyColumns">关键字名称</param>
        /// <param name="strPointId">关键字值</param>
        /// <returns></returns>
        public DataTable GetEnvPointItemsForFather(string strTableName, string strFatherTable, string strFatherKeyColumns, string strKeyColumns, string strPointId)
        {
            return access.GetEnvPointItemsForFather(strTableName, strFatherTable, strFatherKeyColumns, strKeyColumns, strPointId);
        }
                /// <summary>
        /// 创建原因：获取指定质控点位下的监测项目信息
        /// 创建人：胡方扬
        /// 创建日期：2013-06-25
        /// </summary>
        /// <param name="strTableName">主表名称</param>
        /// <param name="strKeyColumns">关键字名称</param>
        /// <param name="strPointId">关键字值</param>
        /// <returns></returns>
        public DataTable GetEnvPointItemsForQcSetting(TMisPointitemQcsettingVo tMisPointitemQcsetting)
        {
            return access.GetEnvPointItemsForQcSetting(tMisPointitemQcsetting);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisPointitemQcsetting.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //环境质量质控计划ID
            if (tMisPointitemQcsetting.POINT_QCSETTING_ID.Trim() == "")
            {
                this.Tips.AppendLine("环境质量质控计划ID不能为空");
                return false;
            }
            //监测项目ID
            if (tMisPointitemQcsetting.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //备注1
            if (tMisPointitemQcsetting.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisPointitemQcsetting.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisPointitemQcsetting.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisPointitemQcsetting.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisPointitemQcsetting.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
