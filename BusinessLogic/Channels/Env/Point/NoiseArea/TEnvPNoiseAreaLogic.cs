using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.NoiseArea;
using System.Data;
using i3.DataAccess.Channels.Env.Point.NoiseArea;

namespace i3.BusinessLogic.Channels.Env.Point.NoiseArea
{
    /// <summary>
    /// 功能：区域环境噪声
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPNoiseAreaLogic : LogicBase
    {

        TEnvPNoiseAreaVo tEnvPNoiseArea = new TEnvPNoiseAreaVo();
        TEnvPNoiseAreaAccess access;

        public TEnvPNoiseAreaLogic()
        {
            access = new TEnvPNoiseAreaAccess();
        }

        public TEnvPNoiseAreaLogic(TEnvPNoiseAreaVo _tEnvPNoiseArea)
        {
            tEnvPNoiseArea = _tEnvPNoiseArea;
            access = new TEnvPNoiseAreaAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            return access.GetSelectResultCount(tEnvPNoiseArea);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseAreaVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseAreaVo Details(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            return access.Details(tEnvPNoiseArea);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseAreaVo> SelectByObject(TEnvPNoiseAreaVo tEnvPNoiseArea, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPNoiseArea, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseAreaVo tEnvPNoiseArea, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPNoiseArea, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseArea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            return access.SelectByTable(tEnvPNoiseArea);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseArea">对象</param>
        /// <returns></returns>
        public TEnvPNoiseAreaVo SelectByObject(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            return access.SelectByObject(tEnvPNoiseArea);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            return access.Create(tEnvPNoiseArea);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseArea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            return access.Edit(tEnvPNoiseArea);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseArea_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseArea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseAreaVo tEnvPNoiseArea_UpdateSet, TEnvPNoiseAreaVo tEnvPNoiseArea_UpdateWhere)
        {
            return access.Edit(tEnvPNoiseArea_UpdateSet, tEnvPNoiseArea_UpdateWhere);
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
        public bool Delete(TEnvPNoiseAreaVo tEnvPNoiseArea)
        {
            return access.Delete(tEnvPNoiseArea);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPNoiseArea.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPNoiseArea.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPNoiseArea.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPNoiseArea.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //功能区ID（字典项）
            if (tEnvPNoiseArea.FUNCTION_AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("功能区ID（字典项）不能为空");
                return false;
            }
            //测点编号
            if (tEnvPNoiseArea.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("测点编号不能为空");
                return false;
            }
            //测点名称
            if (tEnvPNoiseArea.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("测点名称不能为空");
                return false;
            }
            //行政区ID（字典项）
            if (tEnvPNoiseArea.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("行政区ID（字典项）不能为空");
                return false;
            }
            //声源类型ID（字典项）
            if (tEnvPNoiseArea.SOUND_SOURCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("声源类型ID（字典项）不能为空");
                return false;
            }
            //覆盖面积
            if (tEnvPNoiseArea.COVER_AREA.Trim() == "")
            {
                this.Tips.AppendLine("覆盖面积不能为空");
                return false;
            }
            //覆盖人口
            if (tEnvPNoiseArea.COVER_PUPILATION.Trim() == "")
            {
                this.Tips.AppendLine("覆盖人口不能为空");
                return false;
            }
            //网格大小（X）
            if (tEnvPNoiseArea.GRID_SIZE_X.Trim() == "")
            {
                this.Tips.AppendLine("网格大小（X）不能为空");
                return false;
            }
            //网格大小（Y）
            if (tEnvPNoiseArea.GRID_SIZE_Y.Trim() == "")
            {
                this.Tips.AppendLine("网格大小（Y）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPNoiseArea.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPNoiseArea.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPNoiseArea.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPNoiseArea.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPNoiseArea.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPNoiseArea.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //测点位置
            if (tEnvPNoiseArea.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("测点位置不能为空");
                return false;
            }
            //删除标记
            if (tEnvPNoiseArea.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPNoiseArea.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPNoiseArea.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPNoiseArea.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPNoiseArea.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPNoiseArea.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPNoiseArea.REMARK5.Trim() == "")
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
        public bool Create(TEnvPNoiseAreaVo TEnvPNoiseArea, string strSerial)
        {
            return access.Create(TEnvPNoiseArea, strSerial);
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
