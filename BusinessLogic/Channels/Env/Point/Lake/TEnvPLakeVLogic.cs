using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Lake;
using System.Data;
using i3.DataAccess.Channels.Env.Point.Lake;

namespace i3.BusinessLogic.Channels.Env.Point.Lake
{
    /// <summary>
    /// 功能：湖库
    /// 创建日期：2013-06-13
    /// 创建人：魏林
    /// </summary>
    public class TEnvPLakeVLogic : LogicBase
    {

        TEnvPLakeVVo tEnvPLakeV = new TEnvPLakeVVo();
        TEnvPLakeVAccess access;

        public TEnvPLakeVLogic()
        {
            access = new TEnvPLakeVAccess();
        }

        public TEnvPLakeVLogic(TEnvPLakeVVo _tEnvPLakeV)
        {
            tEnvPLakeV = _tEnvPLakeV;
            access = new TEnvPLakeVAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPLakeV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPLakeVVo tEnvPLakeV)
        {
            return access.GetSelectResultCount(tEnvPLakeV);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPLakeVVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPLakeV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPLakeVVo Details(TEnvPLakeVVo tEnvPLakeV)
        {
            return access.Details(tEnvPLakeV);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPLakeV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPLakeVVo> SelectByObject(TEnvPLakeVVo tEnvPLakeV, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPLakeV, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPLakeV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPLakeVVo tEnvPLakeV, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPLakeV, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPLakeV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPLakeVVo tEnvPLakeV)
        {
            return access.SelectByTable(tEnvPLakeV);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPLakeV">对象</param>
        /// <returns></returns>
        public TEnvPLakeVVo SelectByObject(TEnvPLakeVVo tEnvPLakeV)
        {
            return access.SelectByObject(tEnvPLakeV);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPLakeVVo tEnvPLakeV)
        {
            return access.Create(tEnvPLakeV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPLakeV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPLakeVVo tEnvPLakeV)
        {
            return access.Edit(tEnvPLakeV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPLakeV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPLakeV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPLakeVVo tEnvPLakeV_UpdateSet, TEnvPLakeVVo tEnvPLakeV_UpdateWhere)
        {
            return access.Edit(tEnvPLakeV_UpdateSet, tEnvPLakeV_UpdateWhere);
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
        public bool Delete(TEnvPLakeVVo tEnvPLakeV)
        {
            return access.Delete(tEnvPLakeV);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPLakeV.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //湖库断面ID
            if (tEnvPLakeV.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("湖库断面ID不能为空");
                return false;
            }
            //垂线名称
            if (tEnvPLakeV.VERTICAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("垂线名称不能为空");
                return false;
            }
            //备注1
            if (tEnvPLakeV.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPLakeV.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPLakeV.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPLakeV.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPLakeV.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
