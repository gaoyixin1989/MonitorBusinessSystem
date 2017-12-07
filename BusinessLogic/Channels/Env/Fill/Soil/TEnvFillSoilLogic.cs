using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Soil;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.Soil;

namespace i3.BusinessLogic.Channels.Env.Fill.Soil
{
    /// <summary>
    /// 功能：土壤数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSoilLogic : LogicBase
    {

        TEnvFillSoilVo tEnvFillSoil = new TEnvFillSoilVo();
        TEnvFillSoilAccess access;

        public TEnvFillSoilLogic()
        {
            access = new TEnvFillSoilAccess();
        }

        public TEnvFillSoilLogic(TEnvFillSoilVo _tEnvFillSoil)
        {
            tEnvFillSoil = _tEnvFillSoil;
            access = new TEnvFillSoilAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSoil">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSoilVo tEnvFillSoil)
        {
            return access.GetSelectResultCount(tEnvFillSoil);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSoilVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSoil">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSoilVo Details(TEnvFillSoilVo tEnvFillSoil)
        {
            return access.Details(tEnvFillSoil);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSoil">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSoilVo> SelectByObject(TEnvFillSoilVo tEnvFillSoil, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillSoil, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSoil">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSoilVo tEnvFillSoil, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillSoil, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSoil"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSoilVo tEnvFillSoil)
        {
            return access.SelectByTable(tEnvFillSoil);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSoil">对象</param>
        /// <returns></returns>
        public TEnvFillSoilVo SelectByObject(TEnvFillSoilVo tEnvFillSoil)
        {
            return access.SelectByObject(tEnvFillSoil);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSoilVo tEnvFillSoil)
        {
            return access.Create(tEnvFillSoil);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSoil">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSoilVo tEnvFillSoil)
        {
            return access.Edit(tEnvFillSoil);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSoil_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSoil_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSoilVo tEnvFillSoil_UpdateSet, TEnvFillSoilVo tEnvFillSoil_UpdateWhere)
        {
            return access.Edit(tEnvFillSoil_UpdateSet, tEnvFillSoil_UpdateWhere);
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
        public bool Delete(TEnvFillSoilVo tEnvFillSoil)
        {
            return access.Delete(tEnvFillSoil);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillSoil.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillSoil.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillSoil.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillSoil.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillSoil.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillSoil.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillSoil.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillSoil.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //评价
            if (tEnvFillSoil.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillSoil.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillSoil.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillSoil.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillSoil.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillSoil.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillSoil.REMARK5.Trim() == "")
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
