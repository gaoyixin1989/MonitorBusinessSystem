using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Solid;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.Solid;

namespace i3.BusinessLogic.Channels.Env.Fill.Solid
{
    /// <summary>
    /// 功能：固废数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSolidLogic : LogicBase
    {

        TEnvFillSolidVo tEnvFillSolid = new TEnvFillSolidVo();
        TEnvFillSolidAccess access;

        public TEnvFillSolidLogic()
        {
            access = new TEnvFillSolidAccess();
        }

        public TEnvFillSolidLogic(TEnvFillSolidVo _tEnvFillSolid)
        {
            tEnvFillSolid = _tEnvFillSolid;
            access = new TEnvFillSolidAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSolid">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSolidVo tEnvFillSolid)
        {
            return access.GetSelectResultCount(tEnvFillSolid);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSolidVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSolid">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSolidVo Details(TEnvFillSolidVo tEnvFillSolid)
        {
            return access.Details(tEnvFillSolid);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSolid">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSolidVo> SelectByObject(TEnvFillSolidVo tEnvFillSolid, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillSolid, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSolid">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSolidVo tEnvFillSolid, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillSolid, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSolid"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSolidVo tEnvFillSolid)
        {
            return access.SelectByTable(tEnvFillSolid);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSolid">对象</param>
        /// <returns></returns>
        public TEnvFillSolidVo SelectByObject(TEnvFillSolidVo tEnvFillSolid)
        {
            return access.SelectByObject(tEnvFillSolid);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSolidVo tEnvFillSolid)
        {
            return access.Create(tEnvFillSolid);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSolid">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSolidVo tEnvFillSolid)
        {
            return access.Edit(tEnvFillSolid);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSolid_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSolid_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSolidVo tEnvFillSolid_UpdateSet, TEnvFillSolidVo tEnvFillSolid_UpdateWhere)
        {
            return access.Edit(tEnvFillSolid_UpdateSet, tEnvFillSolid_UpdateWhere);
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
        public bool Delete(TEnvFillSolidVo tEnvFillSolid)
        {
            return access.Delete(tEnvFillSolid);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillSolid.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillSolid.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillSolid.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillSolid.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillSolid.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillSolid.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillSolid.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillSolid.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //评价
            if (tEnvFillSolid.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillSolid.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillSolid.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillSolid.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillSolid.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillSolid.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillSolid.REMARK5.Trim() == "")
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
