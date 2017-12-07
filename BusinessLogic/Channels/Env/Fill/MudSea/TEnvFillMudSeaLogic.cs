using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.MudSea;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.MudSea;

namespace i3.BusinessLogic.Channels.Env.Fill.MudSea
{
    /// <summary>
    /// 功能：沉积物（海水）数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillMudSeaLogic : LogicBase
    {

        TEnvFillMudSeaVo tEnvFillMudSea = new TEnvFillMudSeaVo();
        TEnvFillMudSeaAccess access;

        public TEnvFillMudSeaLogic()
        {
            access = new TEnvFillMudSeaAccess();
        }

        public TEnvFillMudSeaLogic(TEnvFillMudSeaVo _tEnvFillMudSea)
        {
            tEnvFillMudSea = _tEnvFillMudSea;
            access = new TEnvFillMudSeaAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillMudSea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            return access.GetSelectResultCount(tEnvFillMudSea);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillMudSeaVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillMudSea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillMudSeaVo Details(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            return access.Details(tEnvFillMudSea);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillMudSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillMudSeaVo> SelectByObject(TEnvFillMudSeaVo tEnvFillMudSea, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillMudSea, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillMudSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillMudSeaVo tEnvFillMudSea, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillMudSea, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillMudSea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            return access.SelectByTable(tEnvFillMudSea);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillMudSea">对象</param>
        /// <returns></returns>
        public TEnvFillMudSeaVo SelectByObject(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            return access.SelectByObject(tEnvFillMudSea);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            return access.Create(tEnvFillMudSea);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudSea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            return access.Edit(tEnvFillMudSea);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudSea_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillMudSea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudSeaVo tEnvFillMudSea_UpdateSet, TEnvFillMudSeaVo tEnvFillMudSea_UpdateWhere)
        {
            return access.Edit(tEnvFillMudSea_UpdateSet, tEnvFillMudSea_UpdateWhere);
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
        public bool Delete(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            return access.Delete(tEnvFillMudSea);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillMudSea.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvFillMudSea.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillMudSea.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillMudSea.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillMudSea.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillMudSea.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillMudSea.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillMudSea.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillMudSea.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillMudSea.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //评价
            if (tEnvFillMudSea.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillMudSea.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillMudSea.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillMudSea.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillMudSea.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillMudSea.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillMudSea.REMARK5.Trim() == "")
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
