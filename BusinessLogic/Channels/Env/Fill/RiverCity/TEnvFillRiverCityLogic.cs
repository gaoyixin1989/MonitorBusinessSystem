using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.RiverCity;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.RiverCity;

namespace i3.BusinessLogic.Channels.Env.Fill.RiverCity
{
    /// <summary>
    /// 功能：城考
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverCityLogic : LogicBase
    {

        TEnvFillRiverCityVo tEnvFillRiverCity = new TEnvFillRiverCityVo();
        TEnvFillRiverCityAccess access;

        public TEnvFillRiverCityLogic()
        {
            access = new TEnvFillRiverCityAccess();
        }

        public TEnvFillRiverCityLogic(TEnvFillRiverCityVo _tEnvFillRiverCity)
        {
            tEnvFillRiverCity = _tEnvFillRiverCity;
            access = new TEnvFillRiverCityAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            return access.GetSelectResultCount(tEnvFillRiverCity);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverCityVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverCityVo Details(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            return access.Details(tEnvFillRiverCity);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverCityVo> SelectByObject(TEnvFillRiverCityVo tEnvFillRiverCity, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiverCity, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverCityVo tEnvFillRiverCity, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiverCity, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverCity"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            return access.SelectByTable(tEnvFillRiverCity);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverCity">对象</param>
        /// <returns></returns>
        public TEnvFillRiverCityVo SelectByObject(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            return access.SelectByObject(tEnvFillRiverCity);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            return access.Create(tEnvFillRiverCity);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverCity">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            return access.Edit(tEnvFillRiverCity);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverCity_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverCity_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverCityVo tEnvFillRiverCity_UpdateSet, TEnvFillRiverCityVo tEnvFillRiverCity_UpdateWhere)
        {
            return access.Edit(tEnvFillRiverCity_UpdateSet, tEnvFillRiverCity_UpdateWhere);
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
        public bool Delete(TEnvFillRiverCityVo tEnvFillRiverCity)
        {
            return access.Delete(tEnvFillRiverCity);
        }


        #region //新增代码
        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            return access.PointByTable(strYear, strMonth);
        }
        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }
        #endregion

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRiverCity.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvFillRiverCity.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillRiverCity.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillRiverCity.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillRiverCity.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillRiverCity.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillRiverCity.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillRiverCity.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillRiverCity.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillRiverCity.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //评价
            if (tEnvFillRiverCity.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillRiverCity.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiverCity.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiverCity.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiverCity.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiverCity.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRiverCity.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
