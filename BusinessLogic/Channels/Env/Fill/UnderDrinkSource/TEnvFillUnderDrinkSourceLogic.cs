using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.UnderDrinkSource;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.UnderDrinkSource;

namespace i3.BusinessLogic.Channels.Env.Fill.UnderDrinkSource
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-26
    /// 创建人：
    /// </summary>
    public class TEnvFillUnderdrinkSrcLogic : LogicBase
    {

        TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc = new TEvnFillUnderDrinkSourceVo();
        TEnvFillUnderdrinkSrcAccess access;

        public TEnvFillUnderdrinkSrcLogic()
        {
            access = new TEnvFillUnderdrinkSrcAccess();
        }

        public TEnvFillUnderdrinkSrcLogic(TEvnFillUnderDrinkSourceVo _tEnvFillUnderdrinkSrc)
        {
            tEnvFillUnderdrinkSrc = _tEnvFillUnderdrinkSrc;
            access = new TEnvFillUnderdrinkSrcAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            return access.GetSelectResultCount(tEnvFillUnderdrinkSrc);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEvnFillUnderDrinkSourceVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象条件</param>
        /// <returns>对象</returns>
        public TEvnFillUnderDrinkSourceVo Details(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            return access.Details(tEnvFillUnderdrinkSrc);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEvnFillUnderDrinkSourceVo> SelectByObject(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillUnderdrinkSrc, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillUnderdrinkSrc, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            return access.SelectByTable(tEnvFillUnderdrinkSrc);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">对象</param>
        /// <returns></returns>
        public TEvnFillUnderDrinkSourceVo SelectByObject(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            return access.SelectByObject(tEnvFillUnderdrinkSrc);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            return access.Create(tEnvFillUnderdrinkSrc);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            return access.Edit(tEnvFillUnderdrinkSrc);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillUnderdrinkSrc_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillUnderdrinkSrc_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc_UpdateSet, TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc_UpdateWhere)
        {
            return access.Edit(tEnvFillUnderdrinkSrc_UpdateSet, tEnvFillUnderdrinkSrc_UpdateWhere);
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
        public bool Delete(TEvnFillUnderDrinkSourceVo tEnvFillUnderdrinkSrc)
        {
            return access.Delete(tEnvFillUnderdrinkSrc);
        }
        /// <summary>
        /// 根据年份和月份获取监测点信息(地下饮用水)
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable_Und(string strYear, string strMonth)
        {
            return access.PointByTable_Und(strYear, strMonth);
        }
        public DataTable GetFillData(string strWhere, DataTable dtShow, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string mark)
        {
            return access.GetFillData(strWhere, dtShow, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvFillUnderdrinkSrc.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.DAY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.KPF.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillUnderdrinkSrc.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }

} 
