using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.PolluteRule;
using i3.DataAccess.Channels.Env.Point.PolluteRule;

namespace i3.BusinessLogic.Channels.Env.Point.PolluteRule
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-08-29
    /// 创建人：
    /// </summary>
    public class TEnvPPolluteTypeLogic : LogicBase
    {

        TEnvPPolluteTypeVo tEnvPPolluteType = new TEnvPPolluteTypeVo();
        TEnvPPolluteTypeAccess access;

        public TEnvPPolluteTypeLogic()
        {
            access = new TEnvPPolluteTypeAccess();
        }

        public TEnvPPolluteTypeLogic(TEnvPPolluteTypeVo _tEnvPPolluteType)
        {
            tEnvPPolluteType = _tEnvPPolluteType;
            access = new TEnvPPolluteTypeAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPPolluteType">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            return access.GetSelectResultCount(tEnvPPolluteType);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPPolluteTypeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPPolluteType">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPPolluteTypeVo Details(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            return access.Details(tEnvPPolluteType);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPPolluteType">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPPolluteTypeVo> SelectByObject(TEnvPPolluteTypeVo tEnvPPolluteType, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPPolluteType, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPPolluteType">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPPolluteTypeVo tEnvPPolluteType, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPPolluteType, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPPolluteType"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            return access.SelectByTable(tEnvPPolluteType);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPPolluteType">对象</param>
        /// <returns></returns>
        public TEnvPPolluteTypeVo SelectByObject(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            return access.SelectByObject(tEnvPPolluteType);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            return access.Create(tEnvPPolluteType);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPolluteType">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            return access.Edit(tEnvPPolluteType);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPolluteType_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPPolluteType_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPolluteTypeVo tEnvPPolluteType_UpdateSet, TEnvPPolluteTypeVo tEnvPPolluteType_UpdateWhere)
        {
            return access.Edit(tEnvPPolluteType_UpdateSet, tEnvPPolluteType_UpdateWhere);
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
        public bool Delete(TEnvPPolluteTypeVo tEnvPPolluteType)
        {
            return access.Delete(tEnvPPolluteType);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tEnvPPolluteType.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteType.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteType.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteType.TYPE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteType.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteType.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteType.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteType.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tEnvPPolluteType.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }

}
