using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Sediment;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.Sediment;

namespace i3.BusinessLogic.Channels.Env.Fill.Sediment
{
    /// <summary>
    /// 功能：底泥重金属填报
    /// 创建日期：2014-10-23
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSedimentLogic : LogicBase
    {

        TEnvFillSedimentVo tEnvFillSediment = new TEnvFillSedimentVo();
        TEnvFillSedimentAccess access;

        public TEnvFillSedimentLogic()
        {
            access = new TEnvFillSedimentAccess();
        }

        public TEnvFillSedimentLogic(TEnvFillSedimentVo _tEnvFillSediment)
        {
            tEnvFillSediment = _tEnvFillSediment;
            access = new TEnvFillSedimentAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSediment">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSedimentVo tEnvFillSediment)
        {
            return access.GetSelectResultCount(tEnvFillSediment);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSedimentVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSediment">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSedimentVo Details(TEnvFillSedimentVo tEnvFillSediment)
        {
            return access.Details(tEnvFillSediment);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSediment">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSedimentVo> SelectByObject(TEnvFillSedimentVo tEnvFillSediment, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillSediment, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSediment">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSedimentVo tEnvFillSediment, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillSediment, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSediment"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSedimentVo tEnvFillSediment)
        {
            return access.SelectByTable(tEnvFillSediment);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSediment">对象</param>
        /// <returns></returns>
        public TEnvFillSedimentVo SelectByObject(TEnvFillSedimentVo tEnvFillSediment)
        {
            return access.SelectByObject(tEnvFillSediment);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSedimentVo tEnvFillSediment)
        {
            return access.Create(tEnvFillSediment);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSediment">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSedimentVo tEnvFillSediment)
        {
            return access.Edit(tEnvFillSediment);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSediment_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSediment_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSedimentVo tEnvFillSediment_UpdateSet, TEnvFillSedimentVo tEnvFillSediment_UpdateWhere)
        {
            return access.Edit(tEnvFillSediment_UpdateSet, tEnvFillSediment_UpdateWhere);
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
        public bool Delete(TEnvFillSedimentVo tEnvFillSediment)
        {
            return access.Delete(tEnvFillSediment);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillSediment.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillSediment.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillSediment.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillSediment.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillSediment.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillSediment.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillSediment.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillSediment.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //评价
            if (tEnvFillSediment.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillSediment.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillSediment.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillSediment.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillSediment.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillSediment.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillSediment.REMARK5.Trim() == "")
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
