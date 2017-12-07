using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using i3.DataAccess.Channels.Env.Point.PolluteRule;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.PolluteRule
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-29
    /// 创建人：
    /// </summary>
    public class TEnvPEnterinfoLogic : LogicBase
    {

        TEnvPEnterinfoVo tEnvPEnterinfo = new TEnvPEnterinfoVo();
        TEnvPEnterinfoAccess access;

        public TEnvPEnterinfoLogic()
        {
            access = new TEnvPEnterinfoAccess();
        }

        public TEnvPEnterinfoLogic(TEnvPEnterinfoVo _tEnvPEnterinfo)
        {
            tEnvPEnterinfo = _tEnvPEnterinfo;
            access = new TEnvPEnterinfoAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            return access.GetSelectResultCount(tEnvPEnterinfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPEnterinfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPEnterinfoVo Details(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            return access.Details(tEnvPEnterinfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPEnterinfoVo> SelectByObject(TEnvPEnterinfoVo tEnvPEnterinfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPEnterinfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPEnterinfoVo tEnvPEnterinfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPEnterinfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPEnterinfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            return access.SelectByTable(tEnvPEnterinfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPEnterinfo">对象</param>
        /// <returns></returns>
        public TEnvPEnterinfoVo SelectByObject(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            return access.SelectByObject(tEnvPEnterinfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            return access.Create(tEnvPEnterinfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPEnterinfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            return access.Edit(tEnvPEnterinfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPEnterinfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPEnterinfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPEnterinfoVo tEnvPEnterinfo_UpdateSet, TEnvPEnterinfoVo tEnvPEnterinfo_UpdateWhere)
        {
            return access.Edit(tEnvPEnterinfo_UpdateSet, tEnvPEnterinfo_UpdateWhere);
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
        public bool Delete(TEnvPEnterinfoVo tEnvPEnterinfo)
        {
            return access.Delete(tEnvPEnterinfo);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvPEnterinfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.ENTER_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.ENTER_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.LEVEL1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.LEVEL2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.LEVEL3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPEnterinfo.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            return true;
        }

    }

}
