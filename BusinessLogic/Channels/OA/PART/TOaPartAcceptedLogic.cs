using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.PART;
using i3.DataAccess.Channels.OA.PART;

namespace i3.BusinessLogic.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料验收清单
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartAcceptedLogic : LogicBase
    {

        TOaPartAcceptedVo tOaPartAccepted = new TOaPartAcceptedVo();
        TOaPartAcceptedAccess access;

        public TOaPartAcceptedLogic()
        {
            access = new TOaPartAcceptedAccess();
        }

        public TOaPartAcceptedLogic(TOaPartAcceptedVo _tOaPartAccepted)
        {
            tOaPartAccepted = _tOaPartAccepted;
            access = new TOaPartAcceptedAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartAcceptedVo tOaPartAccepted)
        {
            return access.GetSelectResultCount(tOaPartAccepted);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartAcceptedVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartAccepted">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartAcceptedVo Details(TOaPartAcceptedVo tOaPartAccepted)
        {
            return access.Details(tOaPartAccepted);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartAcceptedVo> SelectByObject(TOaPartAcceptedVo tOaPartAccepted, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaPartAccepted, iIndex, iCount);

        }

        public DataTable SelectByTableEx(TOaPartAcceptedVo tOaPartAccepted, int iIndex, int iCount)
        {
            return access.SelectByTableEx(tOaPartAccepted, iIndex, iCount);
        }

        public int GetSelectByTableExCount(TOaPartAcceptedVo tOaPartAccepted)
        {
            return access.GetSelectByTableExCount(tOaPartAccepted);
        }

        //获取物料时间段 黄进军添加20141028
        public DataTable SelectByTimeList(TOaPartAcceptedVo tOaPartAccepted, string startTime, string endTime, int iIndex, int iCount)
        {
            return access.SelectByTimeList(tOaPartAccepted, startTime, endTime, iIndex, iCount);
        }

        //获取物料时间段分页 黄进军添加20141028
        public int GetSelectByTimeListCount(TOaPartAcceptedVo tOaPartAccepted, string startTime, string endTime)
        {
            return access.GetSelectByTimeListCount(tOaPartAccepted, startTime, endTime);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartAcceptedVo tOaPartAccepted, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaPartAccepted, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartAccepted"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartAcceptedVo tOaPartAccepted)
        {
            return access.SelectByTable(tOaPartAccepted);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartAccepted">对象</param>
        /// <returns></returns>
        public TOaPartAcceptedVo SelectByObject(TOaPartAcceptedVo tOaPartAccepted)
        {
            return access.SelectByObject(tOaPartAccepted);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartAcceptedVo tOaPartAccepted)
        {
            return access.Create(tOaPartAccepted);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartAccepted">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartAcceptedVo tOaPartAccepted)
        {
            return access.Edit(tOaPartAccepted);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartAccepted_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaPartAccepted_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartAcceptedVo tOaPartAccepted_UpdateSet, TOaPartAcceptedVo tOaPartAccepted_UpdateWhere)
        {
            return access.Edit(tOaPartAccepted_UpdateSet, tOaPartAccepted_UpdateWhere);
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
        public bool Delete(TOaPartAcceptedVo tOaPartAccepted)
        {
            return access.Delete(tOaPartAccepted);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tOaPartAccepted.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //物料ID
            if (tOaPartAccepted.PART_ID.Trim() == "")
            {
                this.Tips.AppendLine("物料ID不能为空");
                return false;
            }
            //需求数量
            if (tOaPartAccepted.NEED_QUANTITY.Trim() == "")
            {
                this.Tips.AppendLine("需求数量不能为空");
                return false;
            }
            //用途
            if (tOaPartAccepted.USERDO.Trim() == "")
            {
                this.Tips.AppendLine("用途不能为空");
                return false;
            }
            //供应商名称
            if (tOaPartAccepted.ENTERPRISE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("供应商名称不能为空");
                return false;
            }
            //浓度范围
            if (tOaPartAccepted.RANGE.Trim() == "")
            {
                this.Tips.AppendLine("浓度范围不能为空");
                return false;
            }
            //标准值/不确定度
            if (tOaPartAccepted.STANDARD.Trim() == "")
            {
                this.Tips.AppendLine("标准值/不确定度不能为空");
                return false;
            }
            //稀释倍数
            if (tOaPartAccepted.RATIO.Trim() == "")
            {
                this.Tips.AppendLine("稀释倍数不能为空");
                return false;
            }
            //单价
            if (tOaPartAccepted.PRICE.Trim() == "")
            {
                this.Tips.AppendLine("单价不能为空");
                return false;
            }
            //金额
            if (tOaPartAccepted.AMOUNT.Trim() == "")
            {
                this.Tips.AppendLine("金额不能为空");
                return false;
            }
            //收货日期
            if (tOaPartAccepted.RECIVEPART_DATE.Trim() == "")
            {
                this.Tips.AppendLine("收货日期不能为空");
                return false;
            }
            //检验日期
            if (tOaPartAccepted.CHECK_DATE.Trim() == "")
            {
                this.Tips.AppendLine("检验日期不能为空");
                return false;
            }
            //验收情况
            if (tOaPartAccepted.CHECK_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("验收情况不能为空");
                return false;
            }
            //验收人ID
            if (tOaPartAccepted.CHECK_USERID.Trim() == "")
            {
                this.Tips.AppendLine("验收人ID不能为空");
                return false;
            }
            //标识
            if (tOaPartAccepted.FLAG.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
            }
            //备注1
            if (tOaPartAccepted.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaPartAccepted.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaPartAccepted.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaPartAccepted.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaPartAccepted.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
