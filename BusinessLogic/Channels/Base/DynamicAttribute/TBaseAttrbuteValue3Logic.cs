using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.DynamicAttribute;
using i3.DataAccess.Channels.Base.DynamicAttribute;

namespace i3.BusinessLogic.Channels.Base.DynamicAttribute
{
    /// <summary>
    /// 功能：属性值表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseAttrbuteValue3Logic : LogicBase
    {

        TBaseAttrbuteValue3Vo TBaseAttrbuteValue33 = new TBaseAttrbuteValue3Vo();
        TBaseAttrbuteValue3Access access;

        public TBaseAttrbuteValue3Logic()
        {
            access = new TBaseAttrbuteValue3Access();
        }

        public TBaseAttrbuteValue3Logic(TBaseAttrbuteValue3Vo _TBaseAttrbuteValue3)
        {
            TBaseAttrbuteValue33 = _TBaseAttrbuteValue3;
            access = new TBaseAttrbuteValue3Access();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="TBaseAttrbuteValue33">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseAttrbuteValue3Vo TBaseAttrbuteValue33)
        {
            return access.GetSelectResultCount(TBaseAttrbuteValue33);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseAttrbuteValue3Vo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="TBaseAttrbuteValue33">对象条件</param>
        /// <returns>对象</returns>
        public TBaseAttrbuteValue3Vo Details(TBaseAttrbuteValue3Vo TBaseAttrbuteValue33)
        {
            return access.Details(TBaseAttrbuteValue33);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="TBaseAttrbuteValue33">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseAttrbuteValue3Vo> SelectByObject(TBaseAttrbuteValue3Vo TBaseAttrbuteValue33, int iIndex, int iCount)
        {
            return access.SelectByObject(TBaseAttrbuteValue33, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="TBaseAttrbuteValue33">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseAttrbuteValue3Vo TBaseAttrbuteValue33, int iIndex, int iCount)
        {
            return access.SelectByTable(TBaseAttrbuteValue33, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="TBaseAttrbuteValue33"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseAttrbuteValue3Vo TBaseAttrbuteValue33)
        {
            return access.SelectByTable(TBaseAttrbuteValue33);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="TBaseAttrbuteValue33">对象</param>
        /// <returns></returns>
        public TBaseAttrbuteValue3Vo SelectByObject(TBaseAttrbuteValue3Vo TBaseAttrbuteValue33)
        {
            return access.SelectByObject(TBaseAttrbuteValue33);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseAttrbuteValue3Vo TBaseAttrbuteValue33)
        {
            return access.Create(TBaseAttrbuteValue33);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="TBaseAttrbuteValue33">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttrbuteValue3Vo TBaseAttrbuteValue33)
        {
            return access.Edit(TBaseAttrbuteValue33);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="TBaseAttrbuteValue3_UpdateSet">UpdateSet用户对象</param>
        /// <param name="TBaseAttrbuteValue3_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseAttrbuteValue3Vo TBaseAttrbuteValue3_UpdateSet, TBaseAttrbuteValue3Vo TBaseAttrbuteValue3_UpdateWhere)
        {
            return access.Edit(TBaseAttrbuteValue3_UpdateSet, TBaseAttrbuteValue3_UpdateWhere);
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
        public bool Delete(TBaseAttrbuteValue3Vo TBaseAttrbuteValue33)
        {
            return access.Delete(TBaseAttrbuteValue33);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (TBaseAttrbuteValue33.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //对象类型
            if (TBaseAttrbuteValue33.OBJECT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("对象类型不能为空");
                return false;
            }
            //对象ID
            if (TBaseAttrbuteValue33.OBJECT_ID.Trim() == "")
            {
                this.Tips.AppendLine("对象ID不能为空");
                return false;
            }
            //属性名称
            if (TBaseAttrbuteValue33.ATTRBUTE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("属性名称不能为空");
                return false;
            }
            //属性值
            if (TBaseAttrbuteValue33.ATTRBUTE_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("属性值不能为空");
                return false;
            }
            //使用状态(0为启用、1为停用)
            if (TBaseAttrbuteValue33.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
            //备注1
            if (TBaseAttrbuteValue33.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (TBaseAttrbuteValue33.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (TBaseAttrbuteValue33.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (TBaseAttrbuteValue33.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (TBaseAttrbuteValue33.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
