using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.DataAccess.Channels.Mis.Monitor.Sample;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：颗粒物原始记录表-基本信息表
    /// 创建日期：2013-07-09
    /// 创建人：胡方扬
    /// </summary>
    public class TMisMonitorDustinforLogic : LogicBase
    {

        TMisMonitorDustinforVo tMisMonitorDustinfor = new TMisMonitorDustinforVo();
        TMisMonitorDustinforAccess access;

        public TMisMonitorDustinforLogic()
        {
            access = new TMisMonitorDustinforAccess();
        }

        public TMisMonitorDustinforLogic(TMisMonitorDustinforVo _tMisMonitorDustinfor)
        {
            tMisMonitorDustinfor = _tMisMonitorDustinfor;
            access = new TMisMonitorDustinforAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            return access.GetSelectResultCount(tMisMonitorDustinfor);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorDustinforVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorDustinforVo Details(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            return access.Details(tMisMonitorDustinfor);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorDustinforVo> SelectByObject(TMisMonitorDustinforVo tMisMonitorDustinfor, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorDustinfor, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorDustinforVo tMisMonitorDustinfor, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorDustinfor, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorDustinfor"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            return access.SelectByTable(tMisMonitorDustinfor);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorDustinfor">对象</param>
        /// <returns></returns>
        public TMisMonitorDustinforVo SelectByObject(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            return access.SelectByObject(tMisMonitorDustinfor);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            return access.Create(tMisMonitorDustinfor);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustinfor">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            return access.Edit(tMisMonitorDustinfor);
        }

        /// <summary>
        /// 对象编辑，空值都可以修改 黄进军20141106
        /// </summary>
        /// <param name="tMisMonitorDustinfor">用户对象</param>
        /// <returns>是否成功</returns>
        public bool ObjEditNull(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            return access.ObjEditNull(tMisMonitorDustinfor);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorDustinfor_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorDustinfor_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorDustinforVo tMisMonitorDustinfor_UpdateSet, TMisMonitorDustinforVo tMisMonitorDustinfor_UpdateWhere)
        {
            return access.Edit(tMisMonitorDustinfor_UpdateSet, tMisMonitorDustinfor_UpdateWhere);
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
        public bool Delete(TMisMonitorDustinforVo tMisMonitorDustinfor)
        {
            return access.Delete(tMisMonitorDustinfor);
        }

        public DataTable SelectTableByID(string strIDs)
        {
            return access.SelectTableByID(strIDs);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tMisMonitorDustinfor.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisMonitorDustinfor.SUBTASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //监测项目ID
            if (tMisMonitorDustinfor.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //
            if (tMisMonitorDustinfor.PURPOSE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //采样日期
            if (tMisMonitorDustinfor.SAMPLE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //锅炉（炉窑）名称/蒸吨
            if (tMisMonitorDustinfor.BOILER_NAME.Trim() == "")
            {
                this.Tips.AppendLine("锅炉（炉窑）名称/蒸吨不能为空");
                return false;
            }
            //燃料种类
            if (tMisMonitorDustinfor.FUEL_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("燃料种类不能为空");
                return false;
            }
            //烟囱高度M
            if (tMisMonitorDustinfor.HEIGHT.Trim() == "")
            {
                this.Tips.AppendLine("烟囱高度M不能为空");
                return false;
            }
            //采样位置
            if (tMisMonitorDustinfor.POSITION.Trim() == "")
            {
                this.Tips.AppendLine("采样位置不能为空");
                return false;
            }
            //断面直径
            if (tMisMonitorDustinfor.SECTION_DIAMETER.Trim() == "")
            {
                this.Tips.AppendLine("断面直径不能为空");
                return false;
            }
            //断面面积
            if (tMisMonitorDustinfor.SECTION_AREA.Trim() == "")
            {
                this.Tips.AppendLine("断面面积不能为空");
                return false;
            }
            //治理措施
            if (tMisMonitorDustinfor.GOVERM_METHOLD.Trim() == "")
            {
                this.Tips.AppendLine("治理措施不能为空");
                return false;
            }
            //风机风量
            if (tMisMonitorDustinfor.MECHIE_WIND_MEASURE.Trim() == "")
            {
                this.Tips.AppendLine("风机风量不能为空");
                return false;
            }
            //烟气含湿量
            if (tMisMonitorDustinfor.HUMIDITY_MEASURE.Trim() == "")
            {
                this.Tips.AppendLine("烟气含湿量不能为空");
                return false;
            }
            //折算系数
            if (tMisMonitorDustinfor.MODUL_NUM.Trim() == "")
            {
                this.Tips.AppendLine("折算系数不能为空");
                return false;
            }
            //仪器型号
            if (tMisMonitorDustinfor.MECHIE_MODEL.Trim() == "")
            {
                this.Tips.AppendLine("仪器型号不能为空");
                return false;
            }
            //仪器编码
            if (tMisMonitorDustinfor.MECHIE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("仪器编码不能为空");
                return false;
            }
            //采样嘴直径
            if (tMisMonitorDustinfor.SAMPLE_POSITION_DIAMETER.Trim() == "")
            {
                this.Tips.AppendLine("采样嘴直径不能为空");
                return false;
            }
            //环境温度
            if (tMisMonitorDustinfor.ENV_TEMPERATURE.Trim() == "")
            {
                this.Tips.AppendLine("环境温度不能为空");
                return false;
            }
            //大气压力
            if (tMisMonitorDustinfor.AIR_PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("大气压力不能为空");
                return false;
            }

            return true;
        }

    }
}
