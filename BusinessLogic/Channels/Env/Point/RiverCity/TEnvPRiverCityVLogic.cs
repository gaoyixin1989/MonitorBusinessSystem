using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverCity;
using System.Data;
using i3.DataAccess.Channels.Env.Point.RiverCity;

namespace i3.BusinessLogic.Channels.Env.Point.RiverCity
{
    /// <summary>
    /// 功能：城考
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverCityVLogic : LogicBase
    {

        TEnvPRiverCityVVo tEnvPRiverCityV = new TEnvPRiverCityVVo();
        TEnvPRiverCityVAccess access;

        public TEnvPRiverCityVLogic()
        {
            access = new TEnvPRiverCityVAccess();
        }

        public TEnvPRiverCityVLogic(TEnvPRiverCityVVo _tEnvPRiverCityV)
        {
            tEnvPRiverCityV = _tEnvPRiverCityV;
            access = new TEnvPRiverCityVAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            return access.GetSelectResultCount(tEnvPRiverCityV);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverCityVVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverCityVVo Details(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            return access.Details(tEnvPRiverCityV);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverCityVVo> SelectByObject(TEnvPRiverCityVVo tEnvPRiverCityV, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPRiverCityV, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverCityVVo tEnvPRiverCityV, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPRiverCityV, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverCityV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            return access.SelectByTable(tEnvPRiverCityV);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象</param>
        /// <returns></returns>
        public TEnvPRiverCityVVo SelectByObject(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            return access.SelectByObject(tEnvPRiverCityV);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            return access.Create(tEnvPRiverCityV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverCityV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            return access.Edit(tEnvPRiverCityV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverCityV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverCityV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverCityVVo tEnvPRiverCityV_UpdateSet, TEnvPRiverCityVVo tEnvPRiverCityV_UpdateWhere)
        {
            return access.Edit(tEnvPRiverCityV_UpdateSet, tEnvPRiverCityV_UpdateWhere);
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
        public bool Delete(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            return access.Delete(tEnvPRiverCityV);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPRiverCityV.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //城考断面ID
            if (tEnvPRiverCityV.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("城考断面ID不能为空");
                return false;
            }
            //垂线名称
            if (tEnvPRiverCityV.VERTICAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("垂线名称不能为空");
                return false;
            }
            //备注1
            if (tEnvPRiverCityV.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPRiverCityV.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPRiverCityV.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPRiverCityV.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPRiverCityV.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
