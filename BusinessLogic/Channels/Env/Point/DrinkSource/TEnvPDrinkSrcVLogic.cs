using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.DrinkSource;
using i3.DataAccess.Channels.Env.Point.DrinkSource;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.DrinkSource
{
    /// <summary>
    /// 功能：魏林
    /// 创建日期：2013-06-07
    /// 创建人：饮用水源地（湖库、河流）
    /// </summary>
    public class TEnvPDrinkSrcVLogic : LogicBase
    {

        TEnvPDrinkSrcVVo tEnvPDrinkSrcV = new TEnvPDrinkSrcVVo();
        TEnvPDrinkSrcVAccess access;

        public TEnvPDrinkSrcVLogic()
        {
            access = new TEnvPDrinkSrcVAccess();
        }

        public TEnvPDrinkSrcVLogic(TEnvPDrinkSrcVVo _tEnvPDrinkSrcV)
        {
            tEnvPDrinkSrcV = _tEnvPDrinkSrcV;
            access = new TEnvPDrinkSrcVAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            return access.GetSelectResultCount(tEnvPDrinkSrcV);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDrinkSrcVVo Details(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            return access.Details(tEnvPDrinkSrcV);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDrinkSrcVVo> SelectByObject(TEnvPDrinkSrcVVo tEnvPDrinkSrcV, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPDrinkSrcV, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVVo tEnvPDrinkSrcV, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPDrinkSrcV, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDrinkSrcV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            return access.SelectByTable(tEnvPDrinkSrcV);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">对象</param>
        /// <returns></returns>
        public TEnvPDrinkSrcVVo SelectByObject(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            return access.SelectByObject(tEnvPDrinkSrcV);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            return access.Create(tEnvPDrinkSrcV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrcV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            return access.Edit(tEnvPDrinkSrcV);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkSrcV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDrinkSrcV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkSrcVVo tEnvPDrinkSrcV_UpdateSet, TEnvPDrinkSrcVVo tEnvPDrinkSrcV_UpdateWhere)
        {
            return access.Edit(tEnvPDrinkSrcV_UpdateSet, tEnvPDrinkSrcV_UpdateWhere);
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
        public bool Delete(TEnvPDrinkSrcVVo tEnvPDrinkSrcV)
        {
            return access.Delete(tEnvPDrinkSrcV);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPDrinkSrcV.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvPDrinkSrcV.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //垂线名称
            if (tEnvPDrinkSrcV.VERTICAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("垂线名称不能为空");
                return false;
            }
            //备注1
            if (tEnvPDrinkSrcV.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPDrinkSrcV.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPDrinkSrcV.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPDrinkSrcV.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPDrinkSrcV.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
