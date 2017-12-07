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
    /// 功能：环境质量质控设置点位信息
    /// 创建日期：2013-06-25
    /// 创建人：胡方扬
    /// </summary>
    public class TMisPointQcsettingLogic : LogicBase
    {

        TMisPointQcsettingVo tMisPointQcsetting = new TMisPointQcsettingVo();
        TMisPointQcsettingAccess access;

        public TMisPointQcsettingLogic()
        {
            access = new TMisPointQcsettingAccess();
        }

        public TMisPointQcsettingLogic(TMisPointQcsettingVo _tMisPointQcsetting)
        {
            tMisPointQcsetting = _tMisPointQcsetting;
            access = new TMisPointQcsettingAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisPointQcsetting">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisPointQcsettingVo tMisPointQcsetting)
        {
            return access.GetSelectResultCount(tMisPointQcsetting);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisPointQcsettingVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisPointQcsetting">对象条件</param>
        /// <returns>对象</returns>
        public TMisPointQcsettingVo Details(TMisPointQcsettingVo tMisPointQcsetting)
        {
            return access.Details(tMisPointQcsetting);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisPointQcsetting">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisPointQcsettingVo> SelectByObject(TMisPointQcsettingVo tMisPointQcsetting, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisPointQcsetting, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisPointQcsetting">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisPointQcsettingVo tMisPointQcsetting, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisPointQcsetting, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisPointQcsetting"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisPointQcsettingVo tMisPointQcsetting)
        {
            return access.SelectByTable(tMisPointQcsetting);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisPointQcsetting">对象</param>
        /// <returns></returns>
        public TMisPointQcsettingVo SelectByObject(TMisPointQcsettingVo tMisPointQcsetting)
        {
            return access.SelectByObject(tMisPointQcsetting);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisPointQcsettingVo tMisPointQcsetting)
        {
            return access.Create(tMisPointQcsetting);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisPointQcsetting">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisPointQcsettingVo tMisPointQcsetting)
        {
            return access.Edit(tMisPointQcsetting);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisPointQcsetting_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisPointQcsetting_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisPointQcsettingVo tMisPointQcsetting_UpdateSet, TMisPointQcsettingVo tMisPointQcsetting_UpdateWhere)
        {
            return access.Edit(tMisPointQcsetting_UpdateSet, tMisPointQcsetting_UpdateWhere);
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
        public bool Delete(TMisPointQcsettingVo tMisPointQcsetting)
        {
            return access.Delete(tMisPointQcsetting);
        }

                /// <summary>
        /// 创建原因：插入环境质量质控设置点位表信息 
        /// 创建人：胡方扬
        /// 创建日期：2013-06-25
        /// </summary>
        /// <param name="tMisPointQcsetting">质控设置其他信息</param>
        /// <param name="strPointId">点位数组</param>
        /// <param name="strPointName">点位名称数组</param>
        /// <returns></returns>
        public bool InsertPointInforForArry(TMisPointQcsettingVo tMisPointQcsetting, string[] strPointId, string[] strPointName)
        {
            return access.InsertPointInforForArry(tMisPointQcsetting, strPointId, strPointName);
        }


        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisPointQcsetting.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //监测点ID
            if (tMisPointQcsetting.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //监测点名称
            if (tMisPointQcsetting.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("监测点名称不能为空");
                return false;
            }
            //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
            if (tMisPointQcsetting.MONITOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）不能为空");
                return false;
            }
            //监测年度
            if (tMisPointQcsetting.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("监测年度不能为空");
                return false;
            }
            //监测月份
            if (tMisPointQcsetting.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("监测月份不能为空");
                return false;
            }
            //质控计划名称
            if (tMisPointQcsetting.PROJECT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("质控计划名称不能为空");
                return false;
            }
            //备注1
            if (tMisPointQcsetting.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisPointQcsetting.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisPointQcsetting.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisPointQcsetting.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisPointQcsetting.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
