using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.River;
using System.Data;
using i3.DataAccess.Channels.Env.Point.River;

namespace i3.BusinessLogic.Channels.Env.Point.River
{
    /// <summary>
    /// 功能：河流
    /// 创建日期：2013-06-13
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverVLogic : LogicBase
    {

        TEnvPRiverVVo tEnvPRiverV = new TEnvPRiverVVo();
        TEnvPRiverVAccess access;

        public TEnvPRiverVLogic()
        {
            access = new TEnvPRiverVAccess();
        }

        public TEnvPRiverVLogic(TEnvPRiverVVo _tEnvPRiverV)
        {
            tEnvPRiverV = _tEnvPRiverV;
            access = new TEnvPRiverVAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverVVo tEnvPRiverV)
        {
            return access.GetSelectResultCount(tEnvPRiverV);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverVVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverVVo Details(TEnvPRiverVVo tEnvPRiverV)
        {
            return access.Details(tEnvPRiverV);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverVVo> SelectByObject(TEnvPRiverVVo tEnvPRiverV, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiverV, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverVVo tEnvPRiverV, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiverV, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverVVo tEnvPRiverV)
        {
            return access.SelectByTable(tEnvPRiverV);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverV">对象</param>
        /// <returns></returns>
        public TEnvPRiverVVo SelectByObject(TEnvPRiverVVo tEnvPRiverV)
        {
            return access.SelectByObject(tEnvPRiverV);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverVVo tEnvPRiverV)
        {
            return access.Create(tEnvPRiverV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverVVo tEnvPRiverV)
        {
            return access.Edit(tEnvPRiverV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverVVo tEnvPRiverV_UpdateSet, TEnvPRiverVVo tEnvPRiverV_UpdateWhere)
        {
            return access.Edit(tEnvPRiverV_UpdateSet, tEnvPRiverV_UpdateWhere);
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
        public bool Delete(TEnvPRiverVVo tEnvPRiverV)
        {
            return access.Delete(tEnvPRiverV);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiverV.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //河流断面ID
            if (tEnvPRiverV.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("河流断面ID不能为空");
                return false;
            }
            //垂线名称
            if (tEnvPRiverV.VERTICAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("垂线名称不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiverV.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiverV.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiverV.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiverV.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiverV.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
