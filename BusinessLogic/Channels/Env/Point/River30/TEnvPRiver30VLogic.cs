using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.River30;
using System.Data;
using i3.DataAccess.Channels.Env.Point.River30;

namespace i3.BusinessLogic.Channels.Env.Point.River30
{
    /// <summary>
    /// 功能：双三十废水
    /// 创建日期：2013-06-17
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiver30VLogic : LogicBase
    {

        TEnvPRiver30VVo tEnvPRiver30V = new TEnvPRiver30VVo();
        TEnvPRiver30VAccess access;

        public TEnvPRiver30VLogic()
        {
            access = new TEnvPRiver30VAccess();
        }

        public TEnvPRiver30VLogic(TEnvPRiver30VVo _tEnvPRiver30V)
        {
            tEnvPRiver30V = _tEnvPRiver30V;
            access = new TEnvPRiver30VAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiver30V">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiver30VVo tEnvPRiver30V)
        {
            return access.GetSelectResultCount(tEnvPRiver30V);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiver30VVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiver30V">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiver30VVo Details(TEnvPRiver30VVo tEnvPRiver30V)
        {
            return access.Details(tEnvPRiver30V);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiver30V">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiver30VVo> SelectByObject(TEnvPRiver30VVo tEnvPRiver30V, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiver30V, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiver30V">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiver30VVo tEnvPRiver30V, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiver30V, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiver30V"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiver30VVo tEnvPRiver30V)
        {
            return access.SelectByTable(tEnvPRiver30V);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiver30V">对象</param>
        /// <returns></returns>
        public TEnvPRiver30VVo SelectByObject(TEnvPRiver30VVo tEnvPRiver30V)
        {
            return access.SelectByObject(tEnvPRiver30V);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiver30VVo tEnvPRiver30V)
        {
            return access.Create(tEnvPRiver30V);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiver30V">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiver30VVo tEnvPRiver30V)
        {
            return access.Edit(tEnvPRiver30V);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiver30V_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiver30V_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiver30VVo tEnvPRiver30V_UpdateSet, TEnvPRiver30VVo tEnvPRiver30V_UpdateWhere)
        {
            return access.Edit(tEnvPRiver30V_UpdateSet, tEnvPRiver30V_UpdateWhere);
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
        public bool Delete(TEnvPRiver30VVo tEnvPRiver30V)
        {
            return access.Delete(tEnvPRiver30V);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiver30V.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvPRiver30V.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //垂线名称
            if (tEnvPRiver30V.VERTICAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("垂线名称不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiver30V.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiver30V.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiver30V.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiver30V.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiver30V.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
