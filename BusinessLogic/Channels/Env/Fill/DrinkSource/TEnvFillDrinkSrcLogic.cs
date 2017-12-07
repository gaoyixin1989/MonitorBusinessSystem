using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.DrinkSource;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.DrinkSource;

namespace i3.BusinessLogic.Channels.Env.Fill.DrinkSource
{
    /// <summary>
    /// 功能：饮用水源地数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillDrinkSrcLogic : LogicBase
    {

        TEnvFillDrinkSrcVo tEnvFillDrinkSrc = new TEnvFillDrinkSrcVo();
        TEnvFillDrinkSrcAccess access;

        public TEnvFillDrinkSrcLogic()
        {
            access = new TEnvFillDrinkSrcAccess();
        }

        public TEnvFillDrinkSrcLogic(TEnvFillDrinkSrcVo _tEnvFillDrinkSrc)
        {
            tEnvFillDrinkSrc = _tEnvFillDrinkSrc;
            access = new TEnvFillDrinkSrcAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            return access.GetSelectResultCount(tEnvFillDrinkSrc);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkSrcVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkSrcVo Details(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            return access.Details(tEnvFillDrinkSrc);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDrinkSrcVo> SelectByObject(TEnvFillDrinkSrcVo tEnvFillDrinkSrc, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillDrinkSrc, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDrinkSrcVo tEnvFillDrinkSrc, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillDrinkSrc, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDrinkSrc"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            return access.SelectByTable(tEnvFillDrinkSrc);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">对象</param>
        /// <returns></returns>
        public TEnvFillDrinkSrcVo SelectByObject(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            return access.SelectByObject(tEnvFillDrinkSrc);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            return access.Create(tEnvFillDrinkSrc);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkSrc">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            return access.Edit(tEnvFillDrinkSrc);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkSrc_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDrinkSrc_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkSrcVo tEnvFillDrinkSrc_UpdateSet, TEnvFillDrinkSrcVo tEnvFillDrinkSrc_UpdateWhere)
        {
            return access.Edit(tEnvFillDrinkSrc_UpdateSet, tEnvFillDrinkSrc_UpdateWhere);
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
        public bool Delete(TEnvFillDrinkSrcVo tEnvFillDrinkSrc)
        {
            return access.Delete(tEnvFillDrinkSrc);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillDrinkSrc.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvFillDrinkSrc.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillDrinkSrc.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillDrinkSrc.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillDrinkSrc.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillDrinkSrc.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillDrinkSrc.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillDrinkSrc.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillDrinkSrc.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillDrinkSrc.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //评价
            if (tEnvFillDrinkSrc.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillDrinkSrc.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillDrinkSrc.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillDrinkSrc.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillDrinkSrc.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillDrinkSrc.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillDrinkSrc.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }
    }

}
