using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.DataAccess.Channels.Env.Point.Environment;
using i3.ValueObject.Channels.Env.Point.Environment;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.Environment
{
    /// <summary>
    /// 功能：环境空气
    /// 创建日期：2013-06-15
    /// 创建人：ljn
    /// </summary>
    public class TEnvPNoiseFunctionItemLogic : LogicBase
    {

        i3.ValueObject.Channels.Env.Point.Environment.TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem = new ValueObject.Channels.Env.Point.Environment.TEnvPNoiseFunctionItemVo();
        TEnvPNoiseFunctionItemAccess access;

        public TEnvPNoiseFunctionItemLogic()
        {
            access = new TEnvPNoiseFunctionItemAccess();
        }

        public TEnvPNoiseFunctionItemLogic(TEnvPNoiseFunctionItemVo _tEnvPNoiseFunctionItem)
        {
            tEnvPNoiseFunctionItem = _tEnvPNoiseFunctionItem;
            access = new TEnvPNoiseFunctionItemAccess();
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
        /// <param name="tEnvPNoiseFunctionItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            return access.GetSelectResultCount(tEnvPNoiseFunctionItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseFunctionItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseFunctionItemVo Details(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            return access.Details(tEnvPNoiseFunctionItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseFunctionItemVo> SelectByObject(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPNoiseFunctionItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPNoiseFunctionItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            return access.SelectByTable(tEnvPNoiseFunctionItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">对象</param>
        /// <returns></returns>
        public TEnvPNoiseFunctionItemVo SelectByObject(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            return access.SelectByObject(tEnvPNoiseFunctionItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            return access.Create(tEnvPNoiseFunctionItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            return access.Edit(tEnvPNoiseFunctionItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseFunctionItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem_UpdateSet, TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem_UpdateWhere)
        {
            return access.Edit(tEnvPNoiseFunctionItem_UpdateSet, tEnvPNoiseFunctionItem_UpdateWhere);
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
        public bool Delete(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            return access.Delete(tEnvPNoiseFunctionItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tEnvPNoiseFunctionItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPNoiseFunctionItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPNoiseFunctionItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //
            if (tEnvPNoiseFunctionItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //备注1
            if (tEnvPNoiseFunctionItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPNoiseFunctionItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPNoiseFunctionItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPNoiseFunctionItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPNoiseFunctionItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
