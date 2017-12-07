using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.PayFor;
using System.Data;
using i3.DataAccess.Channels.Env.Point.PayFor;

namespace i3.BusinessLogic.Channels.Env.Point.PayFor
{
    /// <summary>
    /// 功能：生态补偿
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPPayforItemLogic : LogicBase
    {

        TEnvPPayforItemVo tEnvPPayforItem = new TEnvPPayforItemVo();
        TEnvPPayforItemAccess access;

        public TEnvPPayforItemLogic()
        {
            access = new TEnvPPayforItemAccess();
        }

        public TEnvPPayforItemLogic(TEnvPPayforItemVo _tEnvPPayforItem)
        {
            tEnvPPayforItem = _tEnvPPayforItem;
            access = new TEnvPPayforItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPPayforItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPPayforItemVo tEnvPPayforItem)
        {
            return access.GetSelectResultCount(tEnvPPayforItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPPayforItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPPayforItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPPayforItemVo Details(TEnvPPayforItemVo tEnvPPayforItem)
        {
            return access.Details(tEnvPPayforItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPPayforItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPPayforItemVo> SelectByObject(TEnvPPayforItemVo tEnvPPayforItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPPayforItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPPayforItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPPayforItemVo tEnvPPayforItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPPayforItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPPayforItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPPayforItemVo tEnvPPayforItem)
        {
            return access.SelectByTable(tEnvPPayforItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPPayforItem">对象</param>
        /// <returns></returns>
        public TEnvPPayforItemVo SelectByObject(TEnvPPayforItemVo tEnvPPayforItem)
        {
            return access.SelectByObject(tEnvPPayforItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPayforItemVo tEnvPPayforItem)
        {
            return access.Create(tEnvPPayforItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPayforItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPayforItemVo tEnvPPayforItem)
        {
            return access.Edit(tEnvPPayforItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPayforItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPPayforItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPayforItemVo tEnvPPayforItem_UpdateSet, TEnvPPayforItemVo tEnvPPayforItem_UpdateWhere)
        {
            return access.Edit(tEnvPPayforItem_UpdateSet, tEnvPPayforItem_UpdateWhere);
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
        public bool Delete(TEnvPPayforItemVo tEnvPPayforItem)
        {
            return access.Delete(tEnvPPayforItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPPayforItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //点位ID
            if (tEnvPPayforItem.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位ID不能为空");
                return false;
            }
            //监测项目ID
            if (tEnvPPayforItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }
            //分析方法ID
            if (tEnvPPayforItem.ANALYSIS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分析方法ID不能为空");
                return false;
            }
            //考核水质标准
            if (tEnvPPayforItem.STANDARD.Trim() == "")
            {
                this.Tips.AppendLine("考核水质标准不能为空");
                return false;
            }
            //备注1
            if (tEnvPPayforItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPPayforItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPPayforItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPPayforItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPPayforItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
