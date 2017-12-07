using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Soil;
using System.Data;
using i3.DataAccess.Channels.Env.Point.Soil;

namespace i3.BusinessLogic.Channels.Env.Point.Soil
{
    /// <summary>
    /// 功能：土壤
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSoilLogic : LogicBase
    {

        TEnvPSoilVo tEnvPSoil = new TEnvPSoilVo();
        TEnvPSoilAccess access;

        public TEnvPSoilLogic()
        {
            access = new TEnvPSoilAccess();
        }

        public TEnvPSoilLogic(TEnvPSoilVo _tEnvPSoil)
        {
            tEnvPSoil = _tEnvPSoil;
            access = new TEnvPSoilAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSoil">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSoilVo tEnvPSoil)
        {
            return access.GetSelectResultCount(tEnvPSoil);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSoilVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSoil">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSoilVo Details(TEnvPSoilVo tEnvPSoil)
        {
            return access.Details(tEnvPSoil);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSoil">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSoilVo> SelectByObject(TEnvPSoilVo tEnvPSoil, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPSoil, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSoil">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSoilVo tEnvPSoil, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPSoil, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSoil"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSoilVo tEnvPSoil)
        {
            return access.SelectByTable(tEnvPSoil);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSoil">对象</param>
        /// <returns></returns>
        public TEnvPSoilVo SelectByObject(TEnvPSoilVo tEnvPSoil)
        {
            return access.SelectByObject(tEnvPSoil);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSoilVo tEnvPSoil)
        {
            return access.Create(tEnvPSoil);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSoil">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSoilVo tEnvPSoil)
        {
            return access.Edit(tEnvPSoil);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSoil_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSoil_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSoilVo tEnvPSoil_UpdateSet, TEnvPSoilVo tEnvPSoil_UpdateWhere)
        {
            return access.Edit(tEnvPSoil_UpdateSet, tEnvPSoil_UpdateWhere);
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
        public bool Delete(TEnvPSoilVo tEnvPSoil)
        {
            return access.Delete(tEnvPSoil);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPSoil.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPSoil.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPSoil.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPSoil.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //点位代码
            if (tEnvPSoil.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("点位代码不能为空");
                return false;
            }
            //点位名称
            if (tEnvPSoil.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("点位名称不能为空");
                return false;
            }
            //所在地区ID（字典项）
            if (tEnvPSoil.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区ID（字典项）不能为空");
                return false;
            }
            //所属省份ID
            if (tEnvPSoil.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属省份ID不能为空");
                return false;
            }
            //控制级别ID
            if (tEnvPSoil.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPSoil.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPSoil.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPSoil.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPSoil.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPSoil.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPSoil.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //条件项
            if (tEnvPSoil.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPSoil.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPSoil.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPSoil.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPSoil.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPSoil.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPSoil.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPSoil.REMARK5.Trim() == "")
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
        public bool Create(TEnvPSoilVo TEnvPSoil, string strSerial)
        {
            return access.Create(TEnvPSoil, strSerial);
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
