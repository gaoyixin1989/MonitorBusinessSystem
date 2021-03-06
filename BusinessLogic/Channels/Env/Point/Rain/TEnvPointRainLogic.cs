using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Point.Rain;
using i3.DataAccess.Channels.Env.Point.Rain;

namespace i3.BusinessLogic.Channels.Env.Point.Rain
{
    /// <summary>
    /// 功能：降水监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointRainLogic : LogicBase
    {

        TEnvPointRainVo tEnvPointRain = new TEnvPointRainVo();
        TEnvPointRainAccess access;

        public TEnvPointRainLogic()
        {
            access = new TEnvPointRainAccess();
        }

        public TEnvPointRainLogic(TEnvPointRainVo _tEnvPointRain)
        {
            tEnvPointRain = _tEnvPointRain;
            access = new TEnvPointRainAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointRain">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointRainVo tEnvPointRain)
        {
            return access.GetSelectResultCount(tEnvPointRain);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointRainVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointRain">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointRainVo Details(TEnvPointRainVo tEnvPointRain)
        {
            return access.Details(tEnvPointRain);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointRain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointRainVo> SelectByObject(TEnvPointRainVo tEnvPointRain, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPointRain, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointRain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointRainVo tEnvPointRain, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPointRain, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointRain"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointRainVo tEnvPointRain)
        {
            return access.SelectByTable(tEnvPointRain);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointRain">对象</param>
        /// <returns></returns>
        public TEnvPointRainVo SelectByObject(TEnvPointRainVo tEnvPointRain)
        {
            return access.SelectByObject(tEnvPointRain);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointRainVo tEnvPointRain)
        {
            return access.Create(tEnvPointRain);
        }

        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointRainVo tEnvPointRain, string Number)
        {
            return access.Create(tEnvPointRain, Number);
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointRain">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointRainVo tEnvPointRain)
        {
            return access.Edit(tEnvPointRain);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointRain_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointRain_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointRainVo tEnvPointRain_UpdateSet, TEnvPointRainVo tEnvPointRain_UpdateWhere)
        {
            return access.Edit(tEnvPointRain_UpdateSet, tEnvPointRain_UpdateWhere);
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
        public bool Delete(TEnvPointRainVo tEnvPointRain)
        {
            return access.Delete(tEnvPointRain);
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
            if (tEnvPointRain.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPointRain.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvPointRain.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPointRain.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //测点编号
            if (tEnvPointRain.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("测点编号不能为空");
                return false;
            }
            //测点名称
            if (tEnvPointRain.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("测点名称不能为空");
                return false;
            }
            //行政区ID（字典项）
            if (tEnvPointRain.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("行政区ID（字典项）不能为空");
                return false;
            }
            //控制级别ID（字典项）
            if (tEnvPointRain.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别ID（字典项）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPointRain.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPointRain.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPointRain.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPointRain.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPointRain.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPointRain.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //具体位置
            if (tEnvPointRain.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("具体位置不能为空");
                return false;
            }
            //条件项
            if (tEnvPointRain.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //使用状态(0为启用、1为停用)
            if (tEnvPointRain.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
            //序号
            if (tEnvPointRain.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPointRain.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPointRain.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPointRain.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPointRain.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPointRain.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
