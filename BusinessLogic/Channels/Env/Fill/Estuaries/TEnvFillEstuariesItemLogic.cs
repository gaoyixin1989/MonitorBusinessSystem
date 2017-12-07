using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Estuaries;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.Estuaries;

namespace i3.BusinessLogic.Channels.Env.Fill.Estuaries
{
    /// <summary>
    /// 功能：入海河口数据填报
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
   /// 修改人：刘静楠
    /// 修改时间：2013-6-25
    public class TEnvFillEstuariesItemLogic : LogicBase
    {

        TEnvFillEstuariesItemVo tEnvFillEstuariesItem = new TEnvFillEstuariesItemVo();
        TEnvFillEstuariesItemAccess access;

        public TEnvFillEstuariesItemLogic()
        {
            access = new TEnvFillEstuariesItemAccess();
        }

        public TEnvFillEstuariesItemLogic(TEnvFillEstuariesItemVo _tEnvFillEstuariesItem)
        {
            tEnvFillEstuariesItem = _tEnvFillEstuariesItem;
            access = new TEnvFillEstuariesItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            return access.GetSelectResultCount(tEnvFillEstuariesItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillEstuariesItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillEstuariesItemVo Details(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            return access.Details(tEnvFillEstuariesItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillEstuariesItemVo> SelectByObject(TEnvFillEstuariesItemVo tEnvFillEstuariesItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillEstuariesItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillEstuariesItemVo tEnvFillEstuariesItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillEstuariesItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillEstuariesItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            return access.SelectByTable(tEnvFillEstuariesItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">对象</param>
        /// <returns></returns>
        public TEnvFillEstuariesItemVo SelectByObject(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            return access.SelectByObject(tEnvFillEstuariesItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            return access.Create(tEnvFillEstuariesItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillEstuariesItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            return access.Edit(tEnvFillEstuariesItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillEstuariesItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillEstuariesItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillEstuariesItemVo tEnvFillEstuariesItem_UpdateSet, TEnvFillEstuariesItemVo tEnvFillEstuariesItem_UpdateWhere)
        {
            return access.Edit(tEnvFillEstuariesItem_UpdateSet, tEnvFillEstuariesItem_UpdateWhere);
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
        public bool Delete(TEnvFillEstuariesItemVo tEnvFillEstuariesItem)
        {
            return access.Delete(tEnvFillEstuariesItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillEstuariesItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillEstuariesItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillEstuariesItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillEstuariesItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //评价
            if (tEnvFillEstuariesItem.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //备注1
            if (tEnvFillEstuariesItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillEstuariesItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillEstuariesItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillEstuariesItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillEstuariesItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
