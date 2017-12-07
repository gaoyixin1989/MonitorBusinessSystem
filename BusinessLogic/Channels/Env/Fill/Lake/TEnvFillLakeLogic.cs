using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Lake;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.Lake;

namespace i3.BusinessLogic.Channels.Env.Fill.Lake
{
    /// <summary>
    /// 功能：湖库填报
    /// 创建日期：2013-06-22
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillLakeLogic : LogicBase
    {

        TEnvFillLakeVo tEnvFillLake = new TEnvFillLakeVo();
        TEnvFillLakeAccess access;

        public TEnvFillLakeLogic()
        {
            access = new TEnvFillLakeAccess();
        }

        public TEnvFillLakeLogic(TEnvFillLakeVo _tEnvFillLake)
        {
            tEnvFillLake = _tEnvFillLake;
            access = new TEnvFillLakeAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillLake">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillLakeVo tEnvFillLake)
        {
            return access.GetSelectResultCount(tEnvFillLake);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillLakeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillLake">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillLakeVo Details(TEnvFillLakeVo tEnvFillLake)
        {
            return access.Details(tEnvFillLake);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillLake">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillLakeVo> SelectByObject(TEnvFillLakeVo tEnvFillLake, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillLake, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillLake">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillLakeVo tEnvFillLake, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillLake, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillLake"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillLakeVo tEnvFillLake)
        {
            return access.SelectByTable(tEnvFillLake);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillLake">对象</param>
        /// <returns></returns>
        public TEnvFillLakeVo SelectByObject(TEnvFillLakeVo tEnvFillLake)
        {
            return access.SelectByObject(tEnvFillLake);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillLakeVo tEnvFillLake)
        {
            return access.Create(tEnvFillLake);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillLake">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillLakeVo tEnvFillLake)
        {
            return access.Edit(tEnvFillLake);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillLake_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillLake_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillLakeVo tEnvFillLake_UpdateSet, TEnvFillLakeVo tEnvFillLake_UpdateWhere)
        {
            return access.Edit(tEnvFillLake_UpdateSet, tEnvFillLake_UpdateWhere);
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
        public bool Delete(TEnvFillLakeVo tEnvFillLake)
        {
            return access.Delete(tEnvFillLake);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillLake.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvFillLake.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillLake.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillLake.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillLake.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillLake.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillLake.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillLake.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillLake.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillLake.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //评价
            if (tEnvFillLake.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillLake.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillLake.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillLake.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillLake.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillLake.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillLake.REMARK5.Trim() == "")
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
        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT_ZZ()
        {
            return access.CreateShowDT_ZZ();
        } 
    }

}
