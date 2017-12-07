using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Sediment;
using System.Data;
using i3.DataAccess.Channels.Env.Point.Sediment;

namespace i3.BusinessLogic.Channels.Env.Point.Sediment
{
    /// <summary>
    /// 功能：底泥重金属
    /// 创建日期：2014-10-23
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSedimentLogic : LogicBase
    {

        TEnvPSedimentVo tEnvPSediment = new TEnvPSedimentVo();
        TEnvPSedimentAccess access;

        public TEnvPSedimentLogic()
        {
            access = new TEnvPSedimentAccess();
        }

        public TEnvPSedimentLogic(TEnvPSedimentVo _tEnvPSediment)
        {
            tEnvPSediment = _tEnvPSediment;
            access = new TEnvPSedimentAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSediment">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSedimentVo tEnvPSediment)
        {
            return access.GetSelectResultCount(tEnvPSediment);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSedimentVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSediment">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSedimentVo Details(TEnvPSedimentVo tEnvPSediment)
        {
            return access.Details(tEnvPSediment);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSediment">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSedimentVo> SelectByObject(TEnvPSedimentVo tEnvPSediment, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPSediment, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSediment">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSedimentVo tEnvPSediment, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPSediment, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSediment"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSedimentVo tEnvPSediment)
        {
            return access.SelectByTable(tEnvPSediment);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSediment">对象</param>
        /// <returns></returns>
        public TEnvPSedimentVo SelectByObject(TEnvPSedimentVo tEnvPSediment)
        {
            return access.SelectByObject(tEnvPSediment);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSedimentVo tEnvPSediment)
        {
            return access.Create(tEnvPSediment);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSedimentVo TEnvPSedimentVo, string strSerial)
        {
            return access.Create(TEnvPSedimentVo, strSerial);
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSediment">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSedimentVo tEnvPSediment)
        {
            return access.Edit(tEnvPSediment);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSediment_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSediment_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSedimentVo tEnvPSediment_UpdateSet, TEnvPSedimentVo tEnvPSediment_UpdateWhere)
        {
            return access.Edit(tEnvPSediment_UpdateSet, tEnvPSediment_UpdateWhere);
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
        public bool Delete(TEnvPSedimentVo tEnvPSediment)
        {
            return access.Delete(tEnvPSediment);
        }

        /// <summary>
        /// 底泥重金属监测项目的复制逻辑
        /// </summary>
        /// <param name="strFID"></param>
        /// <param name="strTID"></param>
        /// <param name="strSerial"></param>
        /// <returns></returns>
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            return access.PasteItem(strFID, strTID, strSerial);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPSediment.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPSediment.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPSediment.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPSediment.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //点位代码
            if (tEnvPSediment.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("点位代码不能为空");
                return false;
            }
            //点位名称
            if (tEnvPSediment.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("点位名称不能为空");
                return false;
            }
            //控制级别ID（字典项）
            if (tEnvPSediment.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID（字典项）不能为空");
                return false;
            }
            //所在地区ID（字典项）
            if (tEnvPSediment.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区ID（字典项）不能为空");
                return false;
            }
            //所属省份ID（字典项）
            if (tEnvPSediment.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属省份ID（字典项）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPSediment.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPSediment.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPSediment.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPSediment.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPSediment.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPSediment.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //河流ID（字典项）
            if (tEnvPSediment.RIVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("河流ID（字典项）不能为空");
                return false;
            }
            //水质目标ID（字典项）
            if (tEnvPSediment.WATER_QUALITY_GOALS_ID.Trim() == "")
            {
                this.Tips.AppendLine("水质目标ID（字典项）不能为空");
                return false;
            }
            //类别ID（字典项）
            if (tEnvPSediment.CATEGORY_ID.Trim() == "")
            {
                this.Tips.AppendLine("类别ID（字典项）不能为空");
                return false;
            }
            //是否交接（0-否，1-是）
            if (tEnvPSediment.IS_HANDOVER.Trim() == "")
            {
                this.Tips.AppendLine("是否交接（0-否，1-是）不能为空");
                return false;
            }
            //水源地名称ID（字典项）
            if (tEnvPSediment.WATER_SOURCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("水源地名称ID（字典项）不能为空");
                return false;
            }
            //断面性质ID（字典项）
            if (tEnvPSediment.SECTION_PORPERTIES_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面性质ID（字典项）不能为空");
                return false;
            }
            //监测时段ID（字典项）
            if (tEnvPSediment.MONITORING_TIME_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测时段ID（字典项）不能为空");
                return false;
            }
            //条件项
            if (tEnvPSediment.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPSediment.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPSediment.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPSediment.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPSediment.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPSediment.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPSediment.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPSediment.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }
        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            return access.PointByTable(strYear, strMonth);
        }

    }

}
