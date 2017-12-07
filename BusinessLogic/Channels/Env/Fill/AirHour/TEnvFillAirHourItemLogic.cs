using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Fill.AirHour;
using i3.DataAccess.Channels.Env.Fill.AirHour;


namespace i3.BusinessLogic.Channels.Env.Fill.AirHour
{

    /// <summary>
    /// 功能：环境空气填报（小时）监测项目
    /// 创建日期：2013-06-27
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillAirhourItemLogic : LogicBase
    {

        TEnvFillAirhourItemVo tEnvFillAirhourItem = new TEnvFillAirhourItemVo();
        TEnvFillAirhourItemAccess access;

        public TEnvFillAirhourItemLogic()
        {
            access = new TEnvFillAirhourItemAccess();
        }

        public TEnvFillAirhourItemLogic(TEnvFillAirhourItemVo _tEnvFillAirhourItem)
        {
            tEnvFillAirhourItem = _tEnvFillAirhourItem;
            access = new TEnvFillAirhourItemAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            return access.GetSelectResultCount(tEnvFillAirhourItem);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirhourItemVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirhourItemVo Details(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            return access.Details(tEnvFillAirhourItem);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirhourItemVo> SelectByObject(TEnvFillAirhourItemVo tEnvFillAirhourItem, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAirhourItem, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirhourItemVo tEnvFillAirhourItem, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAirhourItem, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAirhourItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            return access.SelectByTable(tEnvFillAirhourItem);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAirhourItem">对象</param>
        /// <returns></returns>
        public TEnvFillAirhourItemVo SelectByObject(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            return access.SelectByObject(tEnvFillAirhourItem);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            return access.Create(tEnvFillAirhourItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirhourItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            return access.Edit(tEnvFillAirhourItem);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirhourItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAirhourItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirhourItemVo tEnvFillAirhourItem_UpdateSet, TEnvFillAirhourItemVo tEnvFillAirhourItem_UpdateWhere)
        {
            return access.Edit(tEnvFillAirhourItem_UpdateSet, tEnvFillAirhourItem_UpdateWhere);
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
        public bool Delete(TEnvFillAirhourItemVo tEnvFillAirhourItem)
        {
            return access.Delete(tEnvFillAirhourItem);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillAirhourItem.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillAirhourItem.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillAirhourItem.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillAirhourItem.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //超标倍数
            if (tEnvFillAirhourItem.UP_DOUBLE.Trim() == "")
            {
                this.Tips.AppendLine("超标倍数不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAirhourItem.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAirhourItem.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAirhourItem.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAirhourItem.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAirhourItem.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
