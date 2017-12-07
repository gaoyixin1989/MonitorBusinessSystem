using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.CodeRule;
using i3.DataAccess.Channels.Base.CodeRule;

namespace i3.BusinessLogic.Channels.Base.CodeRule
{
    /// <summary>
    /// 功能：编号规则
    /// 创建日期：2013-04-22
    /// 创建人：胡方扬
    /// </summary>
    public class TBaseSerialruleLogic : LogicBase
    {

        TBaseSerialruleVo tBaseSerialrule = new TBaseSerialruleVo();
        TBaseSerialruleAccess access;

        public TBaseSerialruleLogic()
        {
            access = new TBaseSerialruleAccess();
        }

        public TBaseSerialruleLogic(TBaseSerialruleVo _tBaseSerialrule)
        {
            tBaseSerialrule = _tBaseSerialrule;
            access = new TBaseSerialruleAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseSerialrule">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseSerialruleVo tBaseSerialrule)
        {
            return access.GetSelectResultCount(tBaseSerialrule);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseSerialruleVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseSerialrule">对象条件</param>
        /// <returns>对象</returns>
        public TBaseSerialruleVo Details(TBaseSerialruleVo tBaseSerialrule)
        {
            return access.Details(tBaseSerialrule);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseSerialrule">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseSerialruleVo> SelectByObject(TBaseSerialruleVo tBaseSerialrule, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseSerialrule, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseSerialrule">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseSerialruleVo tBaseSerialrule, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseSerialrule, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseSerialrule"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseSerialruleVo tBaseSerialrule)
        {
            return access.SelectByTable(tBaseSerialrule);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseSerialrule">对象</param>
        /// <returns></returns>
        public TBaseSerialruleVo SelectByObject(TBaseSerialruleVo tBaseSerialrule)
        {
            return access.SelectByObject(tBaseSerialrule);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseSerialruleVo tBaseSerialrule)
        {
            return access.Create(tBaseSerialrule);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseSerialrule">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseSerialruleVo tBaseSerialrule)
        {
            return access.Edit(tBaseSerialrule);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseSerialrule_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseSerialrule_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseSerialruleVo tBaseSerialrule_UpdateSet, TBaseSerialruleVo tBaseSerialrule_UpdateWhere)
        {
            return access.Edit(tBaseSerialrule_UpdateSet, tBaseSerialrule_UpdateWhere);
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
        public bool Delete(TBaseSerialruleVo tBaseSerialrule)
        {
            return access.Delete(tBaseSerialrule);
        }

                /// <summary>
        /// 验证是否跨年度、日度，如果跨年、日，则把符合条件的（启用了跨年、跨日重新编号的） 胡方扬 2013-04-24
        /// </summary>
        /// <param name="tBaseSerialrule"></param>
        /// <param name="strNowYear"></param>
        /// <param name="strNowDay"></param>
        /// <returns></returns>
        public bool UpdateInitStartNumForNewYear(TBaseSerialruleVo tBaseSerialrule, string strNowYear, string strNowDay)
        {
            return access.UpdateInitStartNumForNewYear(tBaseSerialrule, strNowYear,strNowDay);
        }
                /// <summary>
        /// 验证是否跨年度，如果跨年，则把符合条件的（启用了跨年重新编号的） 胡方扬 2013-04-24
        /// </summary>
        /// <param name="tBaseSerialrule"></param>
        /// <param name="strNowYear"></param>
        /// <returns></returns>
        public bool UpdateInitStartNumForNewYear(TBaseSerialruleVo tBaseSerialrule, string strNowYear)
        {
            return access.UpdateInitStartNumForNewYear(tBaseSerialrule, strNowYear);
        }

                /// <summary>
        /// 如果编号被使用则，立刻更新初始化编号  胡方扬 2013-04-24
        /// </summary>
        /// <param name="tBaseSerialrule"></param>
        /// <returns></returns>
        public bool UpdateSerialNum(TBaseSerialruleVo tBaseSerialrule)
        {
            return access.UpdateSerialNum(tBaseSerialrule);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tBaseSerialrule.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tBaseSerialrule.SERIAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //编码规则
            if (tBaseSerialrule.SERIAL_RULE.Trim() == "")
            {
                this.Tips.AppendLine("编码规则不能为空");
                return false;
            }
            //类型
            if (tBaseSerialrule.SERIAL_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("类型不能为空");
                return false;
            }
            //编码位数
            if (tBaseSerialrule.SERIAL_NUMBER_BIT.Trim() == "")
            {
                this.Tips.AppendLine("编码位数不能为空");
                return false;
            }
            //使用该编码的监测类别
            if (tBaseSerialrule.SERIAL_TYPE_ID.Trim() == "")
            {
                this.Tips.AppendLine("使用该编码的监测类别不能为空");
                return false;
            }
            //样品来源,1为抽样，2为自送样
            if (tBaseSerialrule.SAMPLE_SOURCE.Trim() == "")
            {
                this.Tips.AppendLine("样品来源,1为抽样，2为自送样不能为空");
                return false;
            }

            return true;
        }

    }
}
