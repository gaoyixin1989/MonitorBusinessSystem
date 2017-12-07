using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Point.Environment;
using i3.ValueObject.Channels.Env.Point.Environment;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.Environment
{
    /// <summary>
    /// 功能：环境空气
    /// 创建日期：2013-06-15
    /// 创建人：ljn
    /// </summary>
    public class T_ENV_P_AIR : LogicBase
    {
        TEnvPAirVo tEnvPAir = new TEnvPAirVo();
        TEnvPAirAccess access;

        public T_ENV_P_AIR()
        {
            access = new TEnvPAirAccess();
        }

        public T_ENV_P_AIR(TEnvPAirVo _tEnvPAir)
        {
            tEnvPAir = _tEnvPAir;
            access = new TEnvPAirAccess();
        }
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPAirVo tEnvPAir)
        {
            return access.GetSelectResultCount(tEnvPAir);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPAirVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPAir">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPAirVo Details(TEnvPAirVo tEnvPAir)
        {
            return access.Details(tEnvPAir);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPAirVo> SelectByObject(TEnvPAirVo tEnvPAir, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPAir, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPAirVo tEnvPAir, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPAir, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPAir"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPAirVo tEnvPAir)
        {
            return access.SelectByTable(tEnvPAir);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns></returns>
        public TEnvPAirVo SelectByObject(TEnvPAirVo tEnvPAir)
        {
            return access.SelectByObject(tEnvPAir);
        }

        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAirVo tEnvPAir, string Number)
        {
            return access.Create(tEnvPAir, Number);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAirVo tEnvPAir)
        {
            return access.Edit(tEnvPAir);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPAir_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAirVo tEnvPAir_UpdateSet, TEnvPAirVo tEnvPAir_UpdateWhere)
        {
            return access.Edit(tEnvPAir_UpdateSet, tEnvPAir_UpdateWhere);
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
        public bool Delete(TEnvPAirVo tEnvPAir)
        {
            return access.Delete(tEnvPAir);
        }

        
        //监测项目复制
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
            if (tEnvPAir.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPAir.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPAir.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站
            if (tEnvPAir.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站不能为空");
                return false;
            }
            //测点编号
            if (tEnvPAir.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("测点编号不能为空");
                return false;
            }
            //测点名称
            if (tEnvPAir.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("测点名称不能为空");
                return false;
            }
            //行政区ID（字典项）
            if (tEnvPAir.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("行政区ID（字典项）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPAir.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPAir.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPAir.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPAir.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPAir.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPAir.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //测点位置
            if (tEnvPAir.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("测点位置不能为空");
                return false;
            }
            //删除标记
            if (tEnvPAir.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPAir.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPAir.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPAir.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPAir.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPAir.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPAir.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
