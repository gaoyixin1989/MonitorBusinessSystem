using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.River30;
using System.Data;
using i3.DataAccess.Channels.Env.Point.River30;

namespace i3.BusinessLogic.Channels.Env.Point.River30
{
    /// <summary>
    /// 功能：双三十废水
    /// 创建日期：2013-06-17
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiver30Logic : LogicBase
    {

        TEnvPRiver30Vo tEnvPRiver30 = new TEnvPRiver30Vo();
        TEnvPRiver30Access access;

        public TEnvPRiver30Logic()
        {
            access = new TEnvPRiver30Access();
        }

        public TEnvPRiver30Logic(TEnvPRiver30Vo _tEnvPRiver30)
        {
            tEnvPRiver30 = _tEnvPRiver30;
            access = new TEnvPRiver30Access();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiver30">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiver30Vo tEnvPRiver30)
        {
            return access.GetSelectResultCount(tEnvPRiver30);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiver30Vo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiver30">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiver30Vo Details(TEnvPRiver30Vo tEnvPRiver30)
        {
            return access.Details(tEnvPRiver30);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiver30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiver30Vo> SelectByObject(TEnvPRiver30Vo tEnvPRiver30, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiver30, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiver30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiver30Vo tEnvPRiver30, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiver30, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiver30"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiver30Vo tEnvPRiver30)
        {
            return access.SelectByTable(tEnvPRiver30);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiver30">对象</param>
        /// <returns></returns>
        public TEnvPRiver30Vo SelectByObject(TEnvPRiver30Vo tEnvPRiver30)
        {
            return access.SelectByObject(tEnvPRiver30);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiver30Vo tEnvPRiver30)
        {
            return access.Create(tEnvPRiver30);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiver30">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiver30Vo tEnvPRiver30)
        {
            return access.Edit(tEnvPRiver30);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiver30_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiver30_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiver30Vo tEnvPRiver30_UpdateSet, TEnvPRiver30Vo tEnvPRiver30_UpdateWhere)
        {
            return access.Edit(tEnvPRiver30_UpdateSet, tEnvPRiver30_UpdateWhere);
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
        public bool Delete(TEnvPRiver30Vo tEnvPRiver30)
        {
            return access.Delete(tEnvPRiver30);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiver30.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPRiver30.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPRiver30.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPRiver30.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //断面代码
            if (tEnvPRiver30.SECTION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("断面代码不能为空");
                return false;
            }
            //断面名称
            if (tEnvPRiver30.SECTION_NAME.Trim() == "")
            {
                this.Tips.AppendLine("断面名称不能为空");
                return false;
            }
            //所在地区ID（字典项）
            if (tEnvPRiver30.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区ID（字典项）不能为空");
                return false;
            }
            //所属省份ID（字典项）
            if (tEnvPRiver30.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属省份ID（字典项）不能为空");
                return false;
            }
            //控制级别ID（字典项）
            if (tEnvPRiver30.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID（字典项）不能为空");
                return false;
            }
            //河流ID（字典项）
            if (tEnvPRiver30.RIVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("河流ID（字典项）不能为空");
                return false;
            }
            //流域ID（字典项）
            if (tEnvPRiver30.VALLEY_ID.Trim() == "")
            {
                this.Tips.AppendLine("流域ID（字典项）不能为空");
                return false;
            }
            //水质目标ID（字典项）
            if (tEnvPRiver30.WATER_QUALITY_GOALS_ID.Trim() == "")
            {
                this.Tips.AppendLine("水质目标ID（字典项）不能为空");
                return false;
            }
            //每月监测、单月监测
            if (tEnvPRiver30.MONITOR_TIMES.Trim() == "")
            {
                this.Tips.AppendLine("每月监测、单月监测不能为空");
                return false;
            }
            //类别ID（字典项）
            if (tEnvPRiver30.CATEGORY_ID.Trim() == "")
            {
                this.Tips.AppendLine("类别ID（字典项）不能为空");
                return false;
            }
            //是否交接（0-否，1-是）
            if (tEnvPRiver30.IS_HANDOVER.Trim() == "")
            {
                this.Tips.AppendLine("是否交接（0-否，1-是）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPRiver30.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPRiver30.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPRiver30.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPRiver30.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPRiver30.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPRiver30.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //条件项
            if (tEnvPRiver30.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPRiver30.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //断面性质
            if (tEnvPRiver30.SECTION_PORPERTIES_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面性质不能为空");
                return false;
            }
            //序号
            if (tEnvPRiver30.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiver30.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiver30.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiver30.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiver30.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiver30.REMARK5.Trim() == "")
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
        public bool Create(TEnvPRiver30Vo TEnvPRiver30, string strSerial)
        {
            return access.Create(TEnvPRiver30, strSerial);
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
    }

}
