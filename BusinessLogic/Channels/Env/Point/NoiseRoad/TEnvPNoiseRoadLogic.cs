using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.NoiseRoad;
using System.Data;
using i3.DataAccess.Channels.Env.Point.NoiseRoad;

namespace i3.BusinessLogic.Channels.Env.Point.NoiseRoad
{
    /// <summary>
    /// 功能：道路交通噪声
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPNoiseRoadLogic : LogicBase
    {

        TEnvPNoiseRoadVo tEnvPNoiseRoad = new TEnvPNoiseRoadVo();
        TEnvPNoiseRoadAccess access;

        public TEnvPNoiseRoadLogic()
        {
            access = new TEnvPNoiseRoadAccess();
        }

        public TEnvPNoiseRoadLogic(TEnvPNoiseRoadVo _tEnvPNoiseRoad)
        {
            tEnvPNoiseRoad = _tEnvPNoiseRoad;
            access = new TEnvPNoiseRoadAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            return access.GetSelectResultCount(tEnvPNoiseRoad);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseRoadVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseRoadVo Details(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            return access.Details(tEnvPNoiseRoad);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseRoadVo> SelectByObject(TEnvPNoiseRoadVo tEnvPNoiseRoad, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPNoiseRoad, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseRoadVo tEnvPNoiseRoad, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPNoiseRoad, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseRoad"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            return access.SelectByTable(tEnvPNoiseRoad);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseRoad">对象</param>
        /// <returns></returns>
        public TEnvPNoiseRoadVo SelectByObject(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            return access.SelectByObject(tEnvPNoiseRoad);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            return access.Create(tEnvPNoiseRoad);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseRoad">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            return access.Edit(tEnvPNoiseRoad);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseRoad_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseRoad_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseRoadVo tEnvPNoiseRoad_UpdateSet, TEnvPNoiseRoadVo tEnvPNoiseRoad_UpdateWhere)
        {
            return access.Edit(tEnvPNoiseRoad_UpdateSet, tEnvPNoiseRoad_UpdateWhere);
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
        public bool Delete(TEnvPNoiseRoadVo tEnvPNoiseRoad)
        {
            return access.Delete(tEnvPNoiseRoad);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPNoiseRoad.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPNoiseRoad.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPNoiseRoad.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPNoiseRoad.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //路段名称
            if (tEnvPNoiseRoad.ROAD_NAME.Trim() == "")
            {
                this.Tips.AppendLine("路段名称不能为空");
                return false;
            }
            //测点编号
            if (tEnvPNoiseRoad.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("测点编号不能为空");
                return false;
            }
            //测点名称
            if (tEnvPNoiseRoad.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("测点名称不能为空");
                return false;
            }
            //路段长度
            if (tEnvPNoiseRoad.ROAD_LENGTH.Trim() == "")
            {
                this.Tips.AppendLine("路段长度不能为空");
                return false;
            }
            //路段宽度
            if (tEnvPNoiseRoad.ROAD_WIDTH.Trim() == "")
            {
                this.Tips.AppendLine("路段宽度不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPNoiseRoad.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPNoiseRoad.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPNoiseRoad.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPNoiseRoad.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPNoiseRoad.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPNoiseRoad.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //测点位置
            if (tEnvPNoiseRoad.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("测点位置不能为空");
                return false;
            }
            //删除标记
            if (tEnvPNoiseRoad.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPNoiseRoad.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPNoiseRoad.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPNoiseRoad.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPNoiseRoad.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPNoiseRoad.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPNoiseRoad.REMARK5.Trim() == "")
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
        public bool Create(TEnvPNoiseRoadVo TEnvPNoiseRoad, string strSerial)
        {
            return access.Create(TEnvPNoiseRoad, strSerial);
        }

        /// <summary>
        /// 监测项目的复制逻辑
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
