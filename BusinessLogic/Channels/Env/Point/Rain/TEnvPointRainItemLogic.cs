using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Point.Rain;
using i3.DataAccess.Channels.Env.Point.Rain;

namespace i3.BusinessLogic.Channels.Env.Point.Rain
{
    /// <summary>
    /// 功能：降水监测点监测项目表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointRainItemLogic : LogicBase
    {

        TEnvPointRainItemVo tEnvPointRainItem = new TEnvPointRainItemVo();
        TEnvPointRainItemAccess access;

        public TEnvPointRainItemLogic()
        {
            access = new TEnvPointRainItemAccess();
        }

        public TEnvPointRainItemLogic(TEnvPointRainItemVo _tEnvPointRainItem)
        {
            tEnvPointRainItem = _tEnvPointRainItem;
            access = new TEnvPointRainItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointRainItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointRainItemVo tEnvPointRainItem)
        {
            return access.GetSelectResultCount(tEnvPointRainItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointRainItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointRainItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointRainItemVo Details(TEnvPointRainItemVo tEnvPointRainItem)
        {
            return access.Details(tEnvPointRainItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointRainItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointRainItemVo> SelectByObject(TEnvPointRainItemVo tEnvPointRainItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPointRainItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointRainItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointRainItemVo tEnvPointRainItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPointRainItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointRainItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointRainItemVo tEnvPointRainItem)
        {
            return access.SelectByTable(tEnvPointRainItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointRainItem">对象</param>
        /// <returns></returns>
        public TEnvPointRainItemVo SelectByObject(TEnvPointRainItemVo tEnvPointRainItem)
        {
            return access.SelectByObject(tEnvPointRainItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointRainItemVo tEnvPointRainItem)
        {
            return access.Create(tEnvPointRainItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointRainItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointRainItemVo tEnvPointRainItem)
        {
            return access.Edit(tEnvPointRainItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointRainItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointRainItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointRainItemVo tEnvPointRainItem_UpdateSet, TEnvPointRainItemVo tEnvPointRainItem_UpdateWhere)
        {
            return access.Edit(tEnvPointRainItem_UpdateSet, tEnvPointRainItem_UpdateWhere);
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
        public bool Delete(TEnvPointRainItemVo tEnvPointRainItem)
        {
            return access.Delete(tEnvPointRainItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPointRainItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //降水监测点ID，对应T_BAS_POINT_RAIN表主键
            if (tEnvPointRainItem.POINT_ID.Trim() == "") 
            {
                this.Tips.AppendLine("降水监测点ID，对应T_BAS_POINT_RAIN表主键不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPointRainItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //
            if (tEnvPointRainItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //备注1
            if (tEnvPointRainItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPointRainItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPointRainItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPointRainItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPointRainItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
