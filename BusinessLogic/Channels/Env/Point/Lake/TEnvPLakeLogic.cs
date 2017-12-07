using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Lake;
using System.Data;
using i3.DataAccess.Channels.Env.Point.Lake;

namespace i3.BusinessLogic.Channels.Env.Point.Lake
{
    /// <summary>
    /// 功能：湖库
    /// 创建日期：2013-06-13
    /// 创建人：魏林
    /// </summary>
    public class TEnvPLakeLogic : LogicBase
    {

        TEnvPLakeVo tEnvPLake = new TEnvPLakeVo();
        TEnvPLakeAccess access;

        public TEnvPLakeLogic()
        {
            access = new TEnvPLakeAccess();
        }

        public TEnvPLakeLogic(TEnvPLakeVo _tEnvPLake)
        {
            tEnvPLake = _tEnvPLake;
            access = new TEnvPLakeAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPLake">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPLakeVo tEnvPLake)
        {
            return access.GetSelectResultCount(tEnvPLake);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPLakeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPLake">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPLakeVo Details(TEnvPLakeVo tEnvPLake)
        {
            return access.Details(tEnvPLake);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPLake">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPLakeVo> SelectByObject(TEnvPLakeVo tEnvPLake, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPLake, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPLake">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPLakeVo tEnvPLake, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPLake, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPLake"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPLakeVo tEnvPLake)
        {
            return access.SelectByTable(tEnvPLake);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPLake">对象</param>
        /// <returns></returns>
        public TEnvPLakeVo SelectByObject(TEnvPLakeVo tEnvPLake)
        {
            return access.SelectByObject(tEnvPLake);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPLakeVo tEnvPLake)
        {
            return access.Create(tEnvPLake);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPLake">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPLakeVo tEnvPLake)
        {
            return access.Edit(tEnvPLake);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPLake_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPLake_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPLakeVo tEnvPLake_UpdateSet, TEnvPLakeVo tEnvPLake_UpdateWhere)
        {
            return access.Edit(tEnvPLake_UpdateSet, tEnvPLake_UpdateWhere);
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
        public bool Delete(TEnvPLakeVo tEnvPLake)
        {
            return access.Delete(tEnvPLake);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPLake.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPLake.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPLake.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPLake.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //断面代码
            if (tEnvPLake.SECTION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("断面代码不能为空");
                return false;
            }
            //断面名称
            if (tEnvPLake.SECTION_NAME.Trim() == "")
            {
                this.Tips.AppendLine("断面名称不能为空");
                return false;
            }
            //所在地区ID（字典项）
            if (tEnvPLake.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区ID（字典项）不能为空");
                return false;
            }
            //所属省份ID（字典项）
            if (tEnvPLake.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属省份ID（字典项）不能为空");
                return false;
            }
            //控制级别ID（字典项）
            if (tEnvPLake.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID（字典项）不能为空");
                return false;
            }
            //河流ID（字典项）
            if (tEnvPLake.RIVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("河流ID（字典项）不能为空");
                return false;
            }
            //流域ID（字典项）
            if (tEnvPLake.VALLEY_ID.Trim() == "")
            {
                this.Tips.AppendLine("流域ID（字典项）不能为空");
                return false;
            }
            //水库ID（字典项）
            if (tEnvPLake.LAKE_ID.Trim() == "")
            {
                this.Tips.AppendLine("水库ID（字典项）不能为空");
                return false;
            }
            //水质目标ID（字典项）
            if (tEnvPLake.WATER_QUALITY_GOALS_ID.Trim() == "")
            {
                this.Tips.AppendLine("水质目标ID（字典项）不能为空");
                return false;
            }
            //每月监测、单月监测
            if (tEnvPLake.MONITOR_TIMES.Trim() == "")
            {
                this.Tips.AppendLine("每月监测、单月监测不能为空");
                return false;
            }
            //类别ID（字典项）
            if (tEnvPLake.CATEGORY_ID.Trim() == "")
            {
                this.Tips.AppendLine("类别ID（字典项）不能为空");
                return false;
            }
            //是否交接（0-否，1-是）
            if (tEnvPLake.IS_HANDOVER.Trim() == "")
            {
                this.Tips.AppendLine("是否交接（0-否，1-是）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPLake.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPLake.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPLake.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPLake.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPLake.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPLake.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //条件项
            if (tEnvPLake.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPLake.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //断面性质
            if (tEnvPLake.SECTION_PORPERTIES_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面性质不能为空");
                return false;
            }
            //序号
            if (tEnvPLake.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPLake.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPLake.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPLake.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPLake.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPLake.REMARK5.Trim() == "")
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
        public bool Create(TEnvPLakeVo TEnvPLake, string strSerial)
        {
            return access.Create(TEnvPLake, strSerial);
        }

        /// <summary>
        /// 湖库垂线监测项目的复制逻辑
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
