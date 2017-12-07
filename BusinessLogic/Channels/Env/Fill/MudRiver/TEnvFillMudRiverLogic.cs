using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.MudRiver;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.MudRiver;

namespace i3.BusinessLogic.Channels.Env.Fill.MudRiver
{
    /// <summary>
    /// 功能：沉积物（河流）数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillMudRiverLogic : LogicBase
    {

        TEnvFillMudRiverVo tEnvFillMudRiver = new TEnvFillMudRiverVo();
        TEnvFillMudRiverAccess access;

        public TEnvFillMudRiverLogic()
        {
            access = new TEnvFillMudRiverAccess();
        }

        public TEnvFillMudRiverLogic(TEnvFillMudRiverVo _tEnvFillMudRiver)
        {
            tEnvFillMudRiver = _tEnvFillMudRiver;
            access = new TEnvFillMudRiverAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            return access.GetSelectResultCount(tEnvFillMudRiver);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillMudRiverVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillMudRiverVo Details(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            return access.Details(tEnvFillMudRiver);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillMudRiverVo> SelectByObject(TEnvFillMudRiverVo tEnvFillMudRiver, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillMudRiver, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillMudRiverVo tEnvFillMudRiver, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillMudRiver, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillMudRiver"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            return access.SelectByTable(tEnvFillMudRiver);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象</param>
        /// <returns></returns>
        public TEnvFillMudRiverVo SelectByObject(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            return access.SelectByObject(tEnvFillMudRiver);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            return access.Create(tEnvFillMudRiver);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudRiver">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            return access.Edit(tEnvFillMudRiver);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudRiver_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillMudRiver_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudRiverVo tEnvFillMudRiver_UpdateSet, TEnvFillMudRiverVo tEnvFillMudRiver_UpdateWhere)
        {
            return access.Edit(tEnvFillMudRiver_UpdateSet, tEnvFillMudRiver_UpdateWhere);
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
        public bool Delete(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            return access.Delete(tEnvFillMudRiver);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillMudRiver.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvFillMudRiver.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillMudRiver.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillMudRiver.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillMudRiver.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillMudRiver.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillMudRiver.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillMudRiver.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillMudRiver.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillMudRiver.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //评价
            if (tEnvFillMudRiver.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillMudRiver.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillMudRiver.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillMudRiver.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillMudRiver.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillMudRiver.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillMudRiver.REMARK5.Trim() == "")
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
