using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Dust;
using i3.DataAccess.Channels.Env.Point.Dust;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.Dust
{
    /// <summary>
    /// 功能：降尘监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    ///     /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointDustItemLogic : LogicBase
    {
        TEnvPDustItemVo tEnvPDustItem = new TEnvPDustItemVo();
        TEnvPDustItemAccess access;

        public TEnvPointDustItemLogic()
        {
            access = new TEnvPDustItemAccess();
        }

        public TEnvPointDustItemLogic(TEnvPDustItemVo _tEnvPDustItem)
        {
            tEnvPDustItem = _tEnvPDustItem;
            access = new TEnvPDustItemAccess();
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDustItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDustItemVo tEnvPDustItem)
        {
            return access.GetSelectResultCount(tEnvPDustItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDustItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDustItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDustItemVo Details(TEnvPDustItemVo tEnvPDustItem)
        {
            return access.Details(tEnvPDustItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDustItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDustItemVo> SelectByObject(TEnvPDustItemVo tEnvPDustItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPDustItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDustItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDustItemVo tEnvPDustItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPDustItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDustItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDustItemVo tEnvPDustItem)
        {
            return access.SelectByTable(tEnvPDustItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDustItem">对象</param>
        /// <returns></returns>
        public TEnvPDustItemVo SelectByObject(TEnvPDustItemVo tEnvPDustItem)
        {
            return access.SelectByObject(tEnvPDustItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDustItemVo tEnvPDustItem)
        {
            return access.Create(tEnvPDustItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDustItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDustItemVo tEnvPDustItem)
        {
            return access.Edit(tEnvPDustItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDustItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDustItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDustItemVo tEnvPDustItem_UpdateSet, TEnvPDustItemVo tEnvPDustItem_UpdateWhere)
        {
            return access.Edit(tEnvPDustItem_UpdateSet, tEnvPDustItem_UpdateWhere);
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
        public bool Delete(TEnvPDustItemVo tEnvPDustItem)
        {
            return access.Delete(tEnvPDustItem);
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
        //监测项目复制
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            return access.PasteItem(strFID, strTID, strSerial);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPDustItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPDustItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPDustItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //备注1
            if (tEnvPDustItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPDustItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPDustItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPDustItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPDustItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
