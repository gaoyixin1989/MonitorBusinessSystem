using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill;
using i3.DataAccess.Channels.Env.Fill;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill
{
    /// <summary>
    /// 功能：环境质量附件信息表
    /// 创建日期：2014-08-04
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillAttLogic : LogicBase
    {

        TEnvFillAttVo tEnvFillAtt = new TEnvFillAttVo();
        TEnvFillAttAccess access;

        public TEnvFillAttLogic()
        {
            access = new TEnvFillAttAccess();
        }

        public TEnvFillAttLogic(TEnvFillAttVo _tEnvFillAtt)
        {
            tEnvFillAtt = _tEnvFillAtt;
            access = new TEnvFillAttAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAtt">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAttVo tEnvFillAtt)
        {
            return access.GetSelectResultCount(tEnvFillAtt);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAttVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAtt">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAttVo Details(TEnvFillAttVo tEnvFillAtt)
        {
            return access.Details(tEnvFillAtt);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAttVo> SelectByObject(TEnvFillAttVo tEnvFillAtt, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAtt, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAttVo tEnvFillAtt, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAtt, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAtt"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAttVo tEnvFillAtt)
        {
            return access.SelectByTable(tEnvFillAtt);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAtt">对象</param>
        /// <returns></returns>
        public TEnvFillAttVo SelectByObject(TEnvFillAttVo tEnvFillAtt)
        {
            return access.SelectByObject(tEnvFillAtt);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAttVo tEnvFillAtt)
        {
            return access.Create(tEnvFillAtt);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAtt">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAttVo tEnvFillAtt)
        {
            return access.Edit(tEnvFillAtt);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAtt_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAtt_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAttVo tEnvFillAtt_UpdateSet, TEnvFillAttVo tEnvFillAtt_UpdateWhere)
        {
            return access.Edit(tEnvFillAtt_UpdateSet, tEnvFillAtt_UpdateWhere);
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
        public bool Delete(TEnvFillAttVo tEnvFillAtt)
        {
            return access.Delete(tEnvFillAtt);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvFillAtt.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.SEASON.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.DAY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.ENVTYPE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvFillAtt.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }

}
