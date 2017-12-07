using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.DrinkUnder;
using i3.ValueObject.Channels.Env.Fill.DrinkUnder;

namespace i3.BusinessLogic.Channels.Env.Fill.DrinkUnder
{
    /// <summary>
    /// 功能：地下水填报
    /// 创建日期：2013-06-22
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillDrinkUnderLogic : LogicBase
    {

        TEnvFillDrinkUnderVo tEnvFillDrinkUnder = new TEnvFillDrinkUnderVo();
        TEnvFillDrinkUnderAccess access;

        public TEnvFillDrinkUnderLogic()
        {
            access = new TEnvFillDrinkUnderAccess();
        }

        public TEnvFillDrinkUnderLogic(TEnvFillDrinkUnderVo _tEnvFillDrinkUnder)
        {
            tEnvFillDrinkUnder = _tEnvFillDrinkUnder;
            access = new TEnvFillDrinkUnderAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            return access.GetSelectResultCount(tEnvFillDrinkUnder);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkUnderVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillDrinkUnderVo Details(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            return access.Details(tEnvFillDrinkUnder);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillDrinkUnderVo> SelectByObject(TEnvFillDrinkUnderVo tEnvFillDrinkUnder, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillDrinkUnder, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillDrinkUnderVo tEnvFillDrinkUnder, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillDrinkUnder, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillDrinkUnder"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            return access.SelectByTable(tEnvFillDrinkUnder);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">对象</param>
        /// <returns></returns>
        public TEnvFillDrinkUnderVo SelectByObject(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            return access.SelectByObject(tEnvFillDrinkUnder);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            return access.Create(tEnvFillDrinkUnder);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkUnder">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            return access.Edit(tEnvFillDrinkUnder);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillDrinkUnder_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillDrinkUnder_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillDrinkUnderVo tEnvFillDrinkUnder_UpdateSet, TEnvFillDrinkUnderVo tEnvFillDrinkUnder_UpdateWhere)
        {
            return access.Edit(tEnvFillDrinkUnder_UpdateSet, tEnvFillDrinkUnder_UpdateWhere);
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
        public bool Delete(TEnvFillDrinkUnderVo tEnvFillDrinkUnder)
        {
            return access.Delete(tEnvFillDrinkUnder);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillDrinkUnder.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillDrinkUnder.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillDrinkUnder.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillDrinkUnder.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillDrinkUnder.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillDrinkUnder.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillDrinkUnder.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillDrinkUnder.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //评价
            if (tEnvFillDrinkUnder.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillDrinkUnder.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillDrinkUnder.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillDrinkUnder.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillDrinkUnder.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillDrinkUnder.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillDrinkUnder.REMARK5.Trim() == "")
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
