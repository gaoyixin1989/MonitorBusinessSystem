using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.Sulfate;
using i3.DataAccess.Channels.Env.Point.Sulfate;

namespace i3.BusinessLogic.Channels.Env.Point.Sulfate
{
    /// <summary>
    /// 功能：硫酸盐化速率监测点监测项目表
    /// 创建日期：2013-06-15
    /// 创建人：ljn
    /// </summary>
    public class TEnvPAlkaliItemLogic : LogicBase
    {

        TEnvPAlkaliItemVo tEnvPAlkaliItem = new TEnvPAlkaliItemVo();
        TEnvPAlkaliItemAccess access;

        public TEnvPAlkaliItemLogic()
        {
            access = new TEnvPAlkaliItemAccess();
        }

        public TEnvPAlkaliItemLogic(TEnvPAlkaliItemVo _tEnvPAlkaliItem)
        {
            tEnvPAlkaliItem = _tEnvPAlkaliItem;
            access = new TEnvPAlkaliItemAccess();
        }

        /// <summary>
        /// 批量保存监测项目数据[用于无垂线监测点](ljn, 2013/6/15)
        /// </summary>
        /// <param name="strTableName">数据表名</param>
        /// <param name="strColumnName">数据表列名</param>
        /// <param name="strSerialId">序列号</param>
        /// <param name="strPointId">监测点ID</param>
        /// <param name="strValue">监测项目值</param>
        /// <returns></returns>
        public bool SaveItemByTransaction(string strTableName, string strColumnName, string strSerialId, string strPointId, string strValue)
        {
            return access.SaveItemByTransaction(strTableName, strColumnName, strSerialId, strPointId, strValue);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            return access.GetSelectResultCount(tEnvPAlkaliItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPAlkaliItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPAlkaliItemVo Details(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            return access.Details(tEnvPAlkaliItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPAlkaliItemVo> SelectByObject(TEnvPAlkaliItemVo tEnvPAlkaliItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPAlkaliItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPAlkaliItemVo tEnvPAlkaliItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPAlkaliItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPAlkaliItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            return access.SelectByTable(tEnvPAlkaliItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象</param>
        /// <returns></returns>
        public TEnvPAlkaliItemVo SelectByObject(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            return access.SelectByObject(tEnvPAlkaliItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            return access.Create(tEnvPAlkaliItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAlkaliItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            return access.Edit(tEnvPAlkaliItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAlkaliItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPAlkaliItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAlkaliItemVo tEnvPAlkaliItem_UpdateSet, TEnvPAlkaliItemVo tEnvPAlkaliItem_UpdateWhere)
        {
            return access.Edit(tEnvPAlkaliItem_UpdateSet, tEnvPAlkaliItem_UpdateWhere);
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
        public bool Delete(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            return access.Delete(tEnvPAlkaliItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvPAlkaliItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPAlkaliItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPAlkaliItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPAlkaliItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPAlkaliItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPAlkaliItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPAlkaliItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPAlkaliItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPAlkaliItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
