using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Item;
using i3.DataAccess.Channels.Base.Item;

namespace i3.BusinessLogic.Channels.Base.Item
{
    /// <summary>
    /// 功能：采样仪器管理
    /// 创建日期：2013-06-25
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemSamplingInstrumentLogic : LogicBase
    {

        TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument = new TBaseItemSamplingInstrumentVo();
        TBaseItemSamplingInstrumentAccess access;

        public TBaseItemSamplingInstrumentLogic()
        {
            access = new TBaseItemSamplingInstrumentAccess();
        }

        public TBaseItemSamplingInstrumentLogic(TBaseItemSamplingInstrumentVo _tBaseItemSamplingInstrument)
        {
            tBaseItemSamplingInstrument = _tBaseItemSamplingInstrument;
            access = new TBaseItemSamplingInstrumentAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            return access.GetSelectResultCount(tBaseItemSamplingInstrument);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseItemSamplingInstrumentVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象条件</param>
        /// <returns>对象</returns>
        public TBaseItemSamplingInstrumentVo Details(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            return access.Details(tBaseItemSamplingInstrument);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseItemSamplingInstrumentVo> SelectByObject(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseItemSamplingInstrument, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseItemSamplingInstrument, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            return access.SelectByTable(tBaseItemSamplingInstrument);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">对象</param>
        /// <returns></returns>
        public TBaseItemSamplingInstrumentVo SelectByObject(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            return access.SelectByObject(tBaseItemSamplingInstrument);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            return access.Create(tBaseItemSamplingInstrument);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            return access.Edit(tBaseItemSamplingInstrument);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemSamplingInstrument_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseItemSamplingInstrument_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument_UpdateSet, TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument_UpdateWhere)
        {
            return access.Edit(tBaseItemSamplingInstrument_UpdateSet, tBaseItemSamplingInstrument_UpdateWhere);
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
        public bool Delete(TBaseItemSamplingInstrumentVo tBaseItemSamplingInstrument)
        {
            return access.Delete(tBaseItemSamplingInstrument);
        }

        /// <summary>
        /// 创建原因：返回指定监测项目ID的采样仪器，如果有默认则返回默认仪器，如果没有默认，但是有采样仪器记录则返回第一条
        /// 创建人：胡方扬
        /// 创建日期：2013-07-04
        /// </summary>
        /// <param name="strItemID">监测项目ID</param>
        /// <returns>返回 DataTable</returns>
        public DataTable GetItemSamplingInstrument(string strItemID)
        {
            return access.GetItemSamplingInstrument(strItemID);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tBaseItemSamplingInstrument.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //监测项目ID
            if (tBaseItemSamplingInstrument.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //0为不默认，1为默认
            if (tBaseItemSamplingInstrument.IS_DEFAULT.Trim() == "")
            {
                this.Tips.AppendLine("0为不默认，1为默认不能为空");
                return false;
            }
            //采样仪器名称
            if (tBaseItemSamplingInstrument.INSTRUMENT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("采样仪器名称不能为空");
                return false;
            }
            //删除标记
            if (tBaseItemSamplingInstrument.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //备注1
            if (tBaseItemSamplingInstrument.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tBaseItemSamplingInstrument.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tBaseItemSamplingInstrument.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tBaseItemSamplingInstrument.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tBaseItemSamplingInstrument.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}