using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.MudRiver;
using System.Data;
using i3.DataAccess.Channels.Env.Point.MudRiver;

namespace i3.BusinessLogic.Channels.Env.Point.MudRiver
{
    /// <summary>
    /// 功能：沉积物（河流）
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPMudRiverLogic : LogicBase
    {

        TEnvPMudRiverVo tEnvPMudRiver = new TEnvPMudRiverVo();
        TEnvPMudRiverAccess access;

        public TEnvPMudRiverLogic()
        {
            access = new TEnvPMudRiverAccess();
        }

        public TEnvPMudRiverLogic(TEnvPMudRiverVo _tEnvPMudRiver)
        {
            tEnvPMudRiver = _tEnvPMudRiver;
            access = new TEnvPMudRiverAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPMudRiver">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPMudRiverVo tEnvPMudRiver)
        {
            return access.GetSelectResultCount(tEnvPMudRiver);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPMudRiverVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPMudRiver">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPMudRiverVo Details(TEnvPMudRiverVo tEnvPMudRiver)
        {
            return access.Details(tEnvPMudRiver);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPMudRiver">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPMudRiverVo> SelectByObject(TEnvPMudRiverVo tEnvPMudRiver, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPMudRiver, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPMudRiver">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPMudRiverVo tEnvPMudRiver, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPMudRiver, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPMudRiver"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPMudRiverVo tEnvPMudRiver)
        {
            return access.SelectByTable(tEnvPMudRiver);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPMudRiver">对象</param>
        /// <returns></returns>
        public TEnvPMudRiverVo SelectByObject(TEnvPMudRiverVo tEnvPMudRiver)
        {
            return access.SelectByObject(tEnvPMudRiver);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPMudRiverVo tEnvPMudRiver)
        {
            return access.Create(tEnvPMudRiver);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudRiver">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudRiverVo tEnvPMudRiver)
        {
            return access.Edit(tEnvPMudRiver);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudRiver_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPMudRiver_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudRiverVo tEnvPMudRiver_UpdateSet, TEnvPMudRiverVo tEnvPMudRiver_UpdateWhere)
        {
            return access.Edit(tEnvPMudRiver_UpdateSet, tEnvPMudRiver_UpdateWhere);
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
        public bool Delete(TEnvPMudRiverVo tEnvPMudRiver)
        {
            return access.Delete(tEnvPMudRiver);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPMudRiver.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPMudRiver.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPMudRiver.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPMudRiver.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //断面代码
            if (tEnvPMudRiver.SECTION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("断面代码不能为空");
                return false;
            }
            //断面名称
            if (tEnvPMudRiver.SECTION_NAME.Trim() == "")
            {
                this.Tips.AppendLine("断面名称不能为空");
                return false;
            }
            //所在地区ID（字典项）
            if (tEnvPMudRiver.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区ID（字典项）不能为空");
                return false;
            }
            //所属省份ID
            if (tEnvPMudRiver.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属省份ID不能为空");
                return false;
            }
            //控制级别ID
            if (tEnvPMudRiver.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID不能为空");
                return false;
            }
            //河流ID
            if (tEnvPMudRiver.RIVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("河流ID不能为空");
                return false;
            }
            //流域ID
            if (tEnvPMudRiver.VALLEY_ID.Trim() == "")
            {
                this.Tips.AppendLine("流域ID不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPMudRiver.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPMudRiver.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPMudRiver.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPMudRiver.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPMudRiver.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPMudRiver.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //条件项
            if (tEnvPMudRiver.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPMudRiver.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPMudRiver.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPMudRiver.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPMudRiver.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPMudRiver.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPMudRiver.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPMudRiver.REMARK5.Trim() == "")
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
        public bool Create(TEnvPMudRiverVo TEnvPMudRiver, string strSerial)
        {
            return access.Create(TEnvPMudRiver, strSerial);
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
