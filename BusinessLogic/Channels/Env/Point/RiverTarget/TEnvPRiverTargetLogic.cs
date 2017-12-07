using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverTarget;
using System.Data;
using i3.DataAccess.Channels.Env.Point.RiverTarget;

namespace i3.BusinessLogic.Channels.Env.Point.RiverTarget
{
    /// <summary>
    /// 功能：责任目标
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverTargetLogic : LogicBase
    {

        TEnvPRiverTargetVo tEnvPRiverTarget = new TEnvPRiverTargetVo();
        TEnvPRiverTargetAccess access;

        public TEnvPRiverTargetLogic()
        {
            access = new TEnvPRiverTargetAccess();
        }

        public TEnvPRiverTargetLogic(TEnvPRiverTargetVo _tEnvPRiverTarget)
        {
            tEnvPRiverTarget = _tEnvPRiverTarget;
            access = new TEnvPRiverTargetAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverTarget">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverTargetVo tEnvPRiverTarget)
        {
            return access.GetSelectResultCount(tEnvPRiverTarget);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverTargetVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverTarget">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverTargetVo Details(TEnvPRiverTargetVo tEnvPRiverTarget)
        {
            return access.Details(tEnvPRiverTarget);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverTarget">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverTargetVo> SelectByObject(TEnvPRiverTargetVo tEnvPRiverTarget, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiverTarget, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverTarget">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverTargetVo tEnvPRiverTarget, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiverTarget, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverTarget"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverTargetVo tEnvPRiverTarget)
        {
            return access.SelectByTable(tEnvPRiverTarget);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverTarget">对象</param>
        /// <returns></returns>
        public TEnvPRiverTargetVo SelectByObject(TEnvPRiverTargetVo tEnvPRiverTarget)
        {
            return access.SelectByObject(tEnvPRiverTarget);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverTargetVo tEnvPRiverTarget)
        {
            return access.Create(tEnvPRiverTarget);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverTarget">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverTargetVo tEnvPRiverTarget)
        {
            return access.Edit(tEnvPRiverTarget);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverTarget_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverTarget_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverTargetVo tEnvPRiverTarget_UpdateSet, TEnvPRiverTargetVo tEnvPRiverTarget_UpdateWhere)
        {
            return access.Edit(tEnvPRiverTarget_UpdateSet, tEnvPRiverTarget_UpdateWhere);
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
        public bool Delete(TEnvPRiverTargetVo tEnvPRiverTarget)
        {
            return access.Delete(tEnvPRiverTarget);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiverTarget.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPRiverTarget.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPRiverTarget.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPRiverTarget.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //断面代码
            if (tEnvPRiverTarget.SECTION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("断面代码不能为空");
                return false;
            }
            //断面名称
            if (tEnvPRiverTarget.SECTION_NAME.Trim() == "")
            {
                this.Tips.AppendLine("断面名称不能为空");
                return false;
            }
            //所在地区ID（字典项）
            if (tEnvPRiverTarget.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区ID（字典项）不能为空");
                return false;
            }
            //所属省份ID（字典项）
            if (tEnvPRiverTarget.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属省份ID（字典项）不能为空");
                return false;
            }
            //控制级别ID（字典项）
            if (tEnvPRiverTarget.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID（字典项）不能为空");
                return false;
            }
            //河流ID（字典项）
            if (tEnvPRiverTarget.RIVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("河流ID（字典项）不能为空");
                return false;
            }
            //流域ID（字典项）
            if (tEnvPRiverTarget.VALLEY_ID.Trim() == "")
            {
                this.Tips.AppendLine("流域ID（字典项）不能为空");
                return false;
            }
            //水质目标ID（字典项）
            if (tEnvPRiverTarget.WATER_QUALITY_GOALS_ID.Trim() == "")
            {
                this.Tips.AppendLine("水质目标ID（字典项）不能为空");
                return false;
            }
            //每月监测、单月监测
            if (tEnvPRiverTarget.MONITOR_TIMES.Trim() == "")
            {
                this.Tips.AppendLine("每月监测、单月监测不能为空");
                return false;
            }
            //类别ID（字典项）
            if (tEnvPRiverTarget.CATEGORY_ID.Trim() == "")
            {
                this.Tips.AppendLine("类别ID（字典项）不能为空");
                return false;
            }
            //是否交接（0-否，1-是）
            if (tEnvPRiverTarget.IS_HANDOVER.Trim() == "")
            {
                this.Tips.AppendLine("是否交接（0-否，1-是）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPRiverTarget.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPRiverTarget.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPRiverTarget.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPRiverTarget.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPRiverTarget.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPRiverTarget.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //条件项
            if (tEnvPRiverTarget.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPRiverTarget.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //断面性质
            if (tEnvPRiverTarget.SECTION_PORPERTIES_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面性质不能为空");
                return false;
            }
            //序号
            if (tEnvPRiverTarget.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiverTarget.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiverTarget.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiverTarget.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiverTarget.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiverTarget.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }
        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverTargetVo TEnvPRiverTarget, string strSerial)
        {
            return access.Create(TEnvPRiverTarget, strSerial);
        }
        /// <summary>
        /// 责任目标垂线监测项目的复制逻辑
        /// </summary>
        /// <param name="strFID"></param>
        /// <param name="strTID"></param>
        /// <param name="strSerial"></param>
        /// <returns></returns>
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            return access.PasteItem(strFID, strTID, strSerial);
        }
    }

}
