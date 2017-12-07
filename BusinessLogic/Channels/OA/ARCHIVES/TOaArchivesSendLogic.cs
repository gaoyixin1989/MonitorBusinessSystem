using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.ARCHIVES;
using i3.DataAccess.Channels.OA.ARCHIVES;

namespace i3.BusinessLogic.Channels.OA.ARCHIVES
{
    /// <summary>
    /// 功能：档案文件分发回收
    /// 创建日期：2013-01-31
    /// 创建人：邵世卓
    /// </summary>
    public class TOaArchivesSendLogic : LogicBase
    {

        TOaArchivesSendVo tOaArchivesSend = new TOaArchivesSendVo();
        TOaArchivesSendAccess access;

        public TOaArchivesSendLogic()
        {
            access = new TOaArchivesSendAccess();
        }

        public TOaArchivesSendLogic(TOaArchivesSendVo _tOaArchivesSend)
        {
            tOaArchivesSend = _tOaArchivesSend;
            access = new TOaArchivesSendAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesSend">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesSendVo tOaArchivesSend)
        {
            return access.GetSelectResultCount(tOaArchivesSend);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesSendVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesSend">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesSendVo Details(TOaArchivesSendVo tOaArchivesSend)
        {
            return access.Details(tOaArchivesSend);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaArchivesSend">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaArchivesSendVo> SelectByObject(TOaArchivesSendVo tOaArchivesSend, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaArchivesSend, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaArchivesSend">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaArchivesSendVo tOaArchivesSend, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaArchivesSend, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesSend"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesSendVo tOaArchivesSend)
        {
            return access.SelectByTable(tOaArchivesSend);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesSend">对象</param>
        /// <returns></returns>
        public TOaArchivesSendVo SelectByObject(TOaArchivesSendVo tOaArchivesSend)
        {
            return access.SelectByObject(tOaArchivesSend);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesSendVo tOaArchivesSend)
        {
            return access.Create(tOaArchivesSend);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesSend">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesSendVo tOaArchivesSend)
        {
            return access.Edit(tOaArchivesSend);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesSend_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesSend_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesSendVo tOaArchivesSend_UpdateSet, TOaArchivesSendVo tOaArchivesSend_UpdateWhere)
        {
            return access.Edit(tOaArchivesSend_UpdateSet, tOaArchivesSend_UpdateWhere);
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
        public bool Delete(TOaArchivesSendVo tOaArchivesSend)
        {
            return access.Delete(tOaArchivesSend);
        }

        /// <summary>
        /// 分发情况统计
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <returns></returns>
        public int GetSelectResultCountForSearch(TOaArchivesSendVo tOaArchivesSend)
        {
            return access.GetSelectResultCountForSearch(tOaArchivesSend);
        }

        /// <summary>
        /// 分发情况
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectByTableForSearch(TOaArchivesSendVo tOaArchivesSend, int intPageIndex, int intPageSize)
        {
            return access.SelectByTableForSearch(tOaArchivesSend, intPageIndex, intPageSize);
        }

        /// <summary>
        /// 批量增加分发记录
        /// </summary>
        /// <param name="tOaArchivesSend">对象</param>
        /// <returns></returns>
        public bool CreateTrans(TOaArchivesSendVo tOaArchivesSend)
        {
            return access.CreateTrans(tOaArchivesSend);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键
            if (tOaArchivesSend.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键不能为空");
                return false;
            }
            //文件ID
            if (tOaArchivesSend.DOCUMENT_ID.Trim() == "")
            {
                this.Tips.AppendLine("文件ID不能为空");
                return false;
            }
            //分发状态，0为回收，1为分发
            if (tOaArchivesSend.LENT_OUT_STATE.Trim() == "")
            {
                this.Tips.AppendLine("分发状态，0为回收，1为分发不能为空");
                return false;
            }
            //接收人/回收人
            if (tOaArchivesSend.BORROWER.Trim() == "")
            {
                this.Tips.AppendLine("接收人/回收人不能为空");
                return false;
            }
            //份数
            if (tOaArchivesSend.HOLD_TIME.Trim() == "")
            {
                this.Tips.AppendLine("份数不能为空");
                return false;
            }
            //分发时间/回收时间
            if (tOaArchivesSend.LOAN_TIME.Trim() == "")
            {
                this.Tips.AppendLine("分发时间/回收时间不能为空");
                return false;
            }
            //借出备注/归还备注
            if (tOaArchivesSend.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("借出备注/归还备注不能为空");
                return false;
            }
            //备注1
            if (tOaArchivesSend.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaArchivesSend.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaArchivesSend.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaArchivesSend.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaArchivesSend.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
