using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using i3.DataAccess.Channels.Env.Point.PolluteRule;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.PolluteRule
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-29
    /// 创建人：
    /// </summary>
    public class TEnvPPolluteLogic : LogicBase
    {

        TEnvPPolluteVo tEnvPPollute = new TEnvPPolluteVo();
        TEnvPPolluteAccess access;

        public TEnvPPolluteLogic()
        {
            access = new TEnvPPolluteAccess();
        }

        public TEnvPPolluteLogic(TEnvPPolluteVo _tEnvPPollute)
        {
            tEnvPPollute = _tEnvPPollute;
            access = new TEnvPPolluteAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPPollute">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPPolluteVo tEnvPPollute)
        {
            return access.GetSelectResultCount(tEnvPPollute);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPPolluteVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPPollute">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPPolluteVo Details(TEnvPPolluteVo tEnvPPollute)
        {
            return access.Details(tEnvPPollute);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPPollute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPPolluteVo> SelectByObject(TEnvPPolluteVo tEnvPPollute, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPPollute, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPPollute">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPPolluteVo tEnvPPollute, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPPollute, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPPollute"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPPolluteVo tEnvPPollute)
        {
            return access.SelectByTable(tEnvPPollute);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPPollute">对象</param>
        /// <returns></returns>
        public TEnvPPolluteVo SelectByObject(TEnvPPolluteVo tEnvPPollute)
        {
            return access.SelectByObject(tEnvPPollute);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPolluteVo tEnvPPollute)
        {
            return access.Create(tEnvPPollute);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPollute">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteVo tEnvPPollute)
        {
            return access.Edit(tEnvPPollute);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPollute_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPPollute_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteVo tEnvPPollute_UpdateSet, TEnvPPolluteVo tEnvPPollute_UpdateWhere)
        {
            return access.Edit(tEnvPPollute_UpdateSet, tEnvPPollute_UpdateWhere);
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
        public bool Delete(TEnvPPolluteVo tEnvPPollute)
        {
            return access.Delete(tEnvPPollute);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool CreateInfo(TEnvPPolluteVo tEnvPPollute, string strSerial)
        {
            return access.CreateInfo(tEnvPPollute, strSerial);
        }

        public string GetType(TEnvPPolluteVo tEnvPPollute)
        {
            return access.GetType(tEnvPPollute);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvPPollute.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.TYPE_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.WATER_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.WATER_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.SEWERAGE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.EQUIPMENT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.MO_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.MO_CAPACITY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.MO_UOM.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.MO_DATE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.FUEL_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.FUEL_QTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.FUEL_MODEL.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.FUEL_TECH.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.IS_FUEL.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.DISCHARGE_WAY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.MO_HOUR_QTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.LOAD_MODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.POINT_TEMP.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.IS_RUN.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.MEASURED.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.WASTE_AIR_QTY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPollute.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }


    }

}
