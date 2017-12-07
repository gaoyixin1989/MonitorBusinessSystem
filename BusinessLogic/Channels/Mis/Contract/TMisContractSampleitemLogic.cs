using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Contract;
using i3.DataAccess.Channels.Mis.Contract;

namespace i3.BusinessLogic.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书样品项目明细
    /// 创建日期：2012-11-27
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractSampleitemLogic : LogicBase
    {

        TMisContractSampleitemVo tMisContractSampleitem = new TMisContractSampleitemVo();
        TMisContractSampleitemAccess access;

        public TMisContractSampleitemLogic()
        {
            access = new TMisContractSampleitemAccess();
        }

        public TMisContractSampleitemLogic(TMisContractSampleitemVo _tMisContractSampleitem)
        {
            tMisContractSampleitem = _tMisContractSampleitem;
            access = new TMisContractSampleitemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractSampleitem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractSampleitemVo tMisContractSampleitem)
        {
            return access.GetSelectResultCount(tMisContractSampleitem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractSampleitemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractSampleitem">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractSampleitemVo Details(TMisContractSampleitemVo tMisContractSampleitem)
        {
            return access.Details(tMisContractSampleitem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractSampleitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractSampleitemVo> SelectByObject(TMisContractSampleitemVo tMisContractSampleitem, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractSampleitem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractSampleitem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractSampleitemVo tMisContractSampleitem, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractSampleitem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractSampleitem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractSampleitemVo tMisContractSampleitem)
        {
            return access.SelectByTable(tMisContractSampleitem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractSampleitem">对象</param>
        /// <returns></returns>
        public TMisContractSampleitemVo SelectByObject(TMisContractSampleitemVo tMisContractSampleitem)
        {
            return access.SelectByObject(tMisContractSampleitem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractSampleitemVo tMisContractSampleitem)
        {
            return access.Create(tMisContractSampleitem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSampleitem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSampleitemVo tMisContractSampleitem)
        {
            return access.Edit(tMisContractSampleitem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSampleitem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractSampleitem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSampleitemVo tMisContractSampleitem_UpdateSet, TMisContractSampleitemVo tMisContractSampleitem_UpdateWhere)
        {
            return access.Edit(tMisContractSampleitem_UpdateSet, tMisContractSampleitem_UpdateWhere);
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
        public bool Delete(TMisContractSampleitemVo tMisContractSampleitem)
        {
            return access.Delete(tMisContractSampleitem);
        }
        /// <summary>
        /// 获取自送样监测项目信息
        /// </summary>
        /// <param name="tMisContractSampleitem"></param>
        /// <param name="tMisContractSample"></param>
        /// <returns></returns>
        public DataTable SelectMonitorForSample(TMisContractSampleitemVo tMisContractSampleitem,TMisContractSampleVo tMisContractSample) 
        {
            return access.SelectMonitorForSample(tMisContractSampleitem, tMisContractSample);
        }

                /// <summary>
        /// 删除自送样委托书监测点位的监测项目 Create By Castle （胡方扬） 2012-12-20
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strMovePointItems"></param>
        /// <returns></returns>
        public bool DelMoveSampleItems(TMisContractSampleitemVo tMisContractPointitem, string[] strMovePointItems)
        {
            return access.DelMoveSampleItems(tMisContractPointitem, strMovePointItems);
        }

                /// <summary>
        /// 向自送样委托书监测项目中插入保存了且不存在的数据  Create By Castle （胡方扬） 2012-12-20
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <param name="strAddPointItems"></param>
        /// <returns></returns>
        public bool EditSampleItems(TMisContractSampleitemVo tMisContractSampleitem, string[] strAddPointItems)
        {
            return access.EditSampleItems(tMisContractSampleitem, strAddPointItems);
        }
         /// <summary>
        /// 获取样品监测项目 胡方扬 2013-04-28
        /// </summary>
        /// <param name="tMisContractSampleitem"></param>
        /// <returns></returns>
        public DataTable GetSampleItemsInfor(TMisContractSampleitemVo tMisContractSampleitem) {
            return access.GetSampleItemsInfor(tMisContractSampleitem);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisContractSampleitem.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //委托书样品ID
            if (tMisContractSampleitem.CONTRACT_SAMPLE_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书样品ID不能为空");
                return false;
            }
            //监测项目ID
            if (tMisContractSampleitem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //备注1
            if (tMisContractSampleitem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisContractSampleitem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisContractSampleitem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisContractSampleitem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisContractSampleitem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
