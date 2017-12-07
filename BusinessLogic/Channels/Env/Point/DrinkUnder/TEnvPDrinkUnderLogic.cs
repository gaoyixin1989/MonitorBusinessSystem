using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.DrinkUnder;
using System.Data;
using i3.DataAccess.Channels.Env.Point.DrinkUnder;

namespace i3.BusinessLogic.Channels.Env.Point.DrinkUnder
{
    /// <summary>
    /// 功能：地下饮用水
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPDrinkUnderLogic : LogicBase
    {

        TEnvPDrinkUnderVo tEnvPDrinkUnder = new TEnvPDrinkUnderVo();
        TEnvPDrinkUnderAccess access;

        public TEnvPDrinkUnderLogic()
        {
            access = new TEnvPDrinkUnderAccess();
        }

        public TEnvPDrinkUnderLogic(TEnvPDrinkUnderVo _tEnvPDrinkUnder)
        {
            tEnvPDrinkUnder = _tEnvPDrinkUnder;
            access = new TEnvPDrinkUnderAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            return access.GetSelectResultCount(tEnvPDrinkUnder);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDrinkUnderVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDrinkUnderVo Details(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            return access.Details(tEnvPDrinkUnder);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDrinkUnderVo> SelectByObject(TEnvPDrinkUnderVo tEnvPDrinkUnder, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPDrinkUnder, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDrinkUnderVo tEnvPDrinkUnder, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPDrinkUnder, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDrinkUnder"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            return access.SelectByTable(tEnvPDrinkUnder);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDrinkUnder">对象</param>
        /// <returns></returns>
        public TEnvPDrinkUnderVo SelectByObject(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            return access.SelectByObject(tEnvPDrinkUnder);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            return access.Create(tEnvPDrinkUnder);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkUnder">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            return access.Edit(tEnvPDrinkUnder);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkUnder_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDrinkUnder_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkUnderVo tEnvPDrinkUnder_UpdateSet, TEnvPDrinkUnderVo tEnvPDrinkUnder_UpdateWhere)
        {
            return access.Edit(tEnvPDrinkUnder_UpdateSet, tEnvPDrinkUnder_UpdateWhere);
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
        public bool Delete(TEnvPDrinkUnderVo tEnvPDrinkUnder)
        {
            return access.Delete(tEnvPDrinkUnder);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPDrinkUnder.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPDrinkUnder.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPDrinkUnder.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPDrinkUnder.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //点位代码
            if (tEnvPDrinkUnder.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("点位代码不能为空");
                return false;
            }
            //点位名称
            if (tEnvPDrinkUnder.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("点位名称不能为空");
                return false;
            }
            //控制级别ID（字典项）
            if (tEnvPDrinkUnder.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID（字典项）不能为空");
                return false;
            }
            //所在地区ID（字典项）
            if (tEnvPDrinkUnder.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区ID（字典项）不能为空");
                return false;
            }
            //所属省份ID（字典项）
            if (tEnvPDrinkUnder.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属省份ID（字典项）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPDrinkUnder.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPDrinkUnder.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPDrinkUnder.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPDrinkUnder.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPDrinkUnder.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPDrinkUnder.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //河流ID（字典项）
            if (tEnvPDrinkUnder.RIVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("河流ID（字典项）不能为空");
                return false;
            }
            //水质目标ID（字典项）
            if (tEnvPDrinkUnder.WATER_QUALITY_GOALS_ID.Trim() == "")
            {
                this.Tips.AppendLine("水质目标ID（字典项）不能为空");
                return false;
            }
            //类别ID（字典项）
            if (tEnvPDrinkUnder.CATEGORY_ID.Trim() == "")
            {
                this.Tips.AppendLine("类别ID（字典项）不能为空");
                return false;
            }
            //是否交接（0-否，1-是）
            if (tEnvPDrinkUnder.IS_HANDOVER.Trim() == "")
            {
                this.Tips.AppendLine("是否交接（0-否，1-是）不能为空");
                return false;
            }
            //水源地名称ID（字典项）
            if (tEnvPDrinkUnder.WATER_SOURCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("水源地名称ID（字典项）不能为空");
                return false;
            }
            //断面性质ID（字典项）
            if (tEnvPDrinkUnder.SECTION_PORPERTIES_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面性质ID（字典项）不能为空");
                return false;
            }
            //监测时段ID（字典项）
            if (tEnvPDrinkUnder.MONITORING_TIME_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测时段ID（字典项）不能为空");
                return false;
            }
            //条件项
            if (tEnvPDrinkUnder.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPDrinkUnder.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPDrinkUnder.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPDrinkUnder.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPDrinkUnder.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPDrinkUnder.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPDrinkUnder.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPDrinkUnder.REMARK5.Trim() == "")
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
        public bool Create(TEnvPDrinkUnderVo TEnvPDrinkUnder, string strSerial)
        {
            return access.Create(TEnvPDrinkUnder, strSerial);
        }

        /// <summary>
        /// 河流垂线监测项目的复制逻辑
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
    }

}
