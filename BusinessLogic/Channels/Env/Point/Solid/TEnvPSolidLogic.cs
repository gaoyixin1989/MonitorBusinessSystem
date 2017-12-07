using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Solid;
using System.Data;
using i3.DataAccess.Channels.Env.Point.Solid;

namespace i3.BusinessLogic.Channels.Env.Point.Solid
{
    /// <summary>
    /// 功能：固废
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSolidLogic : LogicBase
    {

        TEnvPSolidVo tEnvPSolid = new TEnvPSolidVo();
        TEnvPSolidAccess access;

        public TEnvPSolidLogic()
        {
            access = new TEnvPSolidAccess();
        }

        public TEnvPSolidLogic(TEnvPSolidVo _tEnvPSolid)
        {
            tEnvPSolid = _tEnvPSolid;
            access = new TEnvPSolidAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSolid">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSolidVo tEnvPSolid)
        {
            return access.GetSelectResultCount(tEnvPSolid);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSolidVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSolid">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSolidVo Details(TEnvPSolidVo tEnvPSolid)
        {
            return access.Details(tEnvPSolid);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSolid">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSolidVo> SelectByObject(TEnvPSolidVo tEnvPSolid, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPSolid, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSolid">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSolidVo tEnvPSolid, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPSolid, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSolid"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSolidVo tEnvPSolid)
        {
            return access.SelectByTable(tEnvPSolid);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSolid">对象</param>
        /// <returns></returns>
        public TEnvPSolidVo SelectByObject(TEnvPSolidVo tEnvPSolid)
        {
            return access.SelectByObject(tEnvPSolid);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSolidVo tEnvPSolid)
        {
            return access.Create(tEnvPSolid);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSolid">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSolidVo tEnvPSolid)
        {
            return access.Edit(tEnvPSolid);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSolid_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSolid_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSolidVo tEnvPSolid_UpdateSet, TEnvPSolidVo tEnvPSolid_UpdateWhere)
        {
            return access.Edit(tEnvPSolid_UpdateSet, tEnvPSolid_UpdateWhere);
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
        public bool Delete(TEnvPSolidVo tEnvPSolid)
        {
            return access.Delete(tEnvPSolid);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPSolid.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPSolid.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPSolid.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPSolid.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //点位代码
            if (tEnvPSolid.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("点位代码不能为空");
                return false;
            }
            //点位名称
            if (tEnvPSolid.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("点位名称不能为空");
                return false;
            }
            //所在地区ID（字典项）
            if (tEnvPSolid.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区ID（字典项）不能为空");
                return false;
            }
            //所属省份ID
            if (tEnvPSolid.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属省份ID不能为空");
                return false;
            }
            //控制级别ID
            if (tEnvPSolid.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPSolid.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPSolid.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPSolid.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPSolid.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPSolid.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPSolid.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //条件项
            if (tEnvPSolid.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPSolid.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPSolid.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPSolid.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPSolid.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPSolid.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPSolid.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPSolid.REMARK5.Trim() == "")
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
        public bool Create(TEnvPSolidVo TEnvPSolid, string strSerial)
        {
            return access.Create(TEnvPSolid, strSerial);
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
