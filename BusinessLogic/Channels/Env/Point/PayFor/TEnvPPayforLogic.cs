using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.PayFor;
using i3.DataAccess.Channels.Env.Point.PayFor;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.PayFor
{
    /// <summary>
    /// 功能：生态补偿
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPPayforLogic : LogicBase
    {

        TEnvPPayforVo tEnvPPayfor = new TEnvPPayforVo();
        TEnvPPayforAccess access;

        public TEnvPPayforLogic()
        {
            access = new TEnvPPayforAccess();
        }

        public TEnvPPayforLogic(TEnvPPayforVo _tEnvPPayfor)
        {
            tEnvPPayfor = _tEnvPPayfor;
            access = new TEnvPPayforAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPPayfor">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPPayforVo tEnvPPayfor)
        {
            return access.GetSelectResultCount(tEnvPPayfor);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPPayforVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPPayfor">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPPayforVo Details(TEnvPPayforVo tEnvPPayfor)
        {
            return access.Details(tEnvPPayfor);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPPayfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPPayforVo> SelectByObject(TEnvPPayforVo tEnvPPayfor, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPPayfor, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPPayfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPPayforVo tEnvPPayfor, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPPayfor, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPPayfor"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPPayforVo tEnvPPayfor)
        {
            return access.SelectByTable(tEnvPPayfor);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPPayfor">对象</param>
        /// <returns></returns>
        public TEnvPPayforVo SelectByObject(TEnvPPayforVo tEnvPPayfor)
        {
            return access.SelectByObject(tEnvPPayfor);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPayforVo tEnvPPayfor)
        {
            return access.Create(tEnvPPayfor);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPayfor">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPayforVo tEnvPPayfor)
        {
            return access.Edit(tEnvPPayfor);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPayfor_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPPayfor_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPayforVo tEnvPPayfor_UpdateSet, TEnvPPayforVo tEnvPPayfor_UpdateWhere)
        {
            return access.Edit(tEnvPPayfor_UpdateSet, tEnvPPayfor_UpdateWhere);
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
        public bool Delete(TEnvPPayforVo tEnvPPayfor)
        {
            return access.Delete(tEnvPPayfor);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPPayfor.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPPayfor.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPPayfor.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPPayfor.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //断面代码
            if (tEnvPPayfor.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("断面代码不能为空");
                return false;
            }
            //断面名称
            if (tEnvPPayfor.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("断面名称不能为空");
                return false;
            }
            //上游断面
            if (tEnvPPayfor.UP_POINT.Trim() == "")
            {
                this.Tips.AppendLine("上游断面不能为空");
                return false;
            }
            //所在地区ID（字典项）
            if (tEnvPPayfor.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区ID（字典项）不能为空");
                return false;
            }
            //所属省份ID（字典项）
            if (tEnvPPayfor.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属省份ID（字典项）不能为空");
                return false;
            }
            //控制级别ID（字典项）
            if (tEnvPPayfor.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID（字典项）不能为空");
                return false;
            }
            //河流ID（字典项）
            if (tEnvPPayfor.RIVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("河流ID（字典项）不能为空");
                return false;
            }
            //流域ID（字典项）
            if (tEnvPPayfor.VALLEY_ID.Trim() == "")
            {
                this.Tips.AppendLine("流域ID（字典项）不能为空");
                return false;
            }
            //水质目标ID（字典项）
            if (tEnvPPayfor.WATER_QUALITY_GOALS_ID.Trim() == "")
            {
                this.Tips.AppendLine("水质目标ID（字典项）不能为空");
                return false;
            }
            //每月监测、单月监测
            if (tEnvPPayfor.MONITOR_TIMES.Trim() == "")
            {
                this.Tips.AppendLine("每月监测、单月监测不能为空");
                return false;
            }
            //类别ID（字典项）
            if (tEnvPPayfor.CATEGORY_ID.Trim() == "")
            {
                this.Tips.AppendLine("类别ID（字典项）不能为空");
                return false;
            }
            //是否交接（0-否，1-是）
            if (tEnvPPayfor.IS_HANDOVER.Trim() == "")
            {
                this.Tips.AppendLine("是否交接（0-否，1-是）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPPayfor.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPPayfor.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPPayfor.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPPayfor.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPPayfor.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPPayfor.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //条件项
            if (tEnvPPayfor.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPPayfor.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //断面性质
            if (tEnvPPayfor.SECTION_PORPERTIES_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面性质不能为空");
                return false;
            }
            //序号
            if (tEnvPPayfor.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPPayfor.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPPayfor.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPPayfor.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPPayfor.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPPayfor.REMARK5.Trim() == "")
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
        public bool Create(TEnvPPayforVo TEnvPPayfor, string strSerial)
        {
            return access.Create(TEnvPPayfor, strSerial);
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
        /// 保存监测项目的考核标准值
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SaveItemStData(DataTable dt)
        {
            return access.SaveItemStData(dt);
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
