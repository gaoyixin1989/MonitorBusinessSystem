using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.DrinkSource;
using i3.DataAccess.Channels.Env.Point.DrinkSource;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.DrinkSource
{
    /// <summary>
    /// 功能：魏林
    /// 创建日期：2013-06-07
    /// 创建人：饮用水源地（湖库、河流）
    /// </summary>
    public class TEnvPDrinkSrcLogic : LogicBase
    {

        TEnvPDrinkSrcVo tEnvPDrinkSrc = new TEnvPDrinkSrcVo();
        TEnvPDrinkSrcAccess access;

        public TEnvPDrinkSrcLogic()
        {
            access = new TEnvPDrinkSrcAccess();
        }

        public TEnvPDrinkSrcLogic(TEnvPDrinkSrcVo _tEnvPDrinkSrc)
        {
            tEnvPDrinkSrc = _tEnvPDrinkSrc;
            access = new TEnvPDrinkSrcAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            return access.GetSelectResultCount(tEnvPDrinkSrc);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVo Details(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            return access.Details(tEnvPDrinkSrc);
        }
        

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDrinkSrcVo> SelectByObject(TEnvPDrinkSrcVo tEnvPDrinkSrc, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPDrinkSrc, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVo tEnvPDrinkSrc, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPDrinkSrc, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDrinkSrc"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            return access.SelectByTable(tEnvPDrinkSrc);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <returns></returns>
        public TEnvPDrinkSrcVo SelectByObject(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            return access.SelectByObject(tEnvPDrinkSrc);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            return access.Create(tEnvPDrinkSrc);
        }
        

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrc">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            return access.Edit(tEnvPDrinkSrc);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrc_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDrinkSrc_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVo tEnvPDrinkSrc_UpdateSet, TEnvPDrinkSrcVo tEnvPDrinkSrc_UpdateWhere)
        {
            return access.Edit(tEnvPDrinkSrc_UpdateSet, tEnvPDrinkSrc_UpdateWhere);
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
        public bool Delete(TEnvPDrinkSrcVo tEnvPDrinkSrc)
        {
            return access.Delete(tEnvPDrinkSrc);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPDrinkSrc.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPDrinkSrc.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPDrinkSrc.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPDrinkSrc.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //断面代码
            if (tEnvPDrinkSrc.SECTION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("断面代码不能为空");
                return false;
            }
            //断面名称
            if (tEnvPDrinkSrc.SECTION_NAME.Trim() == "")
            {
                this.Tips.AppendLine("断面名称不能为空");
                return false;
            }
            //所在地区ID（字典项）
            if (tEnvPDrinkSrc.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区ID（字典项）不能为空");
                return false;
            }
            //河流ID（字典项）
            if (tEnvPDrinkSrc.RIVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("河流ID（字典项）不能为空");
                return false;
            }
            //控制级别ID（字典项）
            if (tEnvPDrinkSrc.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID（字典项）不能为空");
                return false;
            }
            //所属省份ID（字典项）
            if (tEnvPDrinkSrc.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属省份ID（字典项）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPDrinkSrc.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPDrinkSrc.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPDrinkSrc.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPDrinkSrc.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPDrinkSrc.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPDrinkSrc.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //水质目标ID（字典项）
            if (tEnvPDrinkSrc.WATER_QUALITY_GOALS_ID.Trim() == "")
            {
                this.Tips.AppendLine("水质目标ID（字典项）不能为空");
                return false;
            }
            //类别ID（字典项）
            if (tEnvPDrinkSrc.CATEGORY_ID.Trim() == "")
            {
                this.Tips.AppendLine("类别ID（字典项）不能为空");
                return false;
            }
            //是否交接（0-否，1-是）
            if (tEnvPDrinkSrc.IS_HANDOVER.Trim() == "")
            {
                this.Tips.AppendLine("是否交接（0-否，1-是）不能为空");
                return false;
            }
            //水源地名称ID（字典项）
            if (tEnvPDrinkSrc.WATER_SOURCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("水源地名称ID（字典项）不能为空");
                return false;
            }
            //水期
            if (tEnvPDrinkSrc.WATER_PERIOD.Trim() == "")
            {
                this.Tips.AppendLine("水期不能为空");
                return false;
            }
            //所属水系
            if (tEnvPDrinkSrc.RIVER_SYSTEM.Trim() == "")
            {
                this.Tips.AppendLine("所属水系不能为空");
                return false;
            }
            //水源地性质
            if (tEnvPDrinkSrc.WATER_PROPERTY.Trim() == "")
            {
                this.Tips.AppendLine("水源地性质不能为空");
                return false;
            }
            //断面性质ID（字典项）
            if (tEnvPDrinkSrc.SECTION_PORPERTIES_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面性质ID（字典项）不能为空");
                return false;
            }
            //监测时段ID（字典项）
            if (tEnvPDrinkSrc.MONITORING_TIME_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测时段ID（字典项）不能为空");
                return false;
            }
            //条件项
            if (tEnvPDrinkSrc.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPDrinkSrc.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPDrinkSrc.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPDrinkSrc.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPDrinkSrc.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPDrinkSrc.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPDrinkSrc.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPDrinkSrc.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取监测项目信息
        /// </summary>
        /// <param name="strID">监测项目ID</param>
        /// <returns></returns>
        public DataTable getItemInfo(string strID)
        {
            return access.getItemInfo(strID);
        }
        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkSrcVo tEnvPDrinkSrc, string strSerial)
        {
            return access.Create(tEnvPDrinkSrc, strSerial);
        }

        /// <summary>
        /// 饮用水源地垂线监测项目的复制逻辑
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
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            return access.PointByTable(strYear, strMonth);
        }
        #region//郑州
        /// <summary>
        /// 根据年份和月份获取监测点信息(地表饮用水)
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable_Sur(string strYear, string strMonth)
        {
            return access.PointByTable_Sur(strYear, strMonth);
        }
        public DataTable GetFillData(string strWhere, DataTable dtShow, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string mark)
        {
            return access.GetFillData(strWhere, dtShow, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
        }
        #endregion
    }

}
