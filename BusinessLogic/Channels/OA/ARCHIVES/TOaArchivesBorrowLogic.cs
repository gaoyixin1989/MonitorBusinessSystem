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
    /// 功能：借阅管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaArchivesBorrowLogic : LogicBase
    {

        TOaArchivesBorrowVo tOaArchivesBorrow = new TOaArchivesBorrowVo();
        TOaArchivesBorrowAccess access;

        public TOaArchivesBorrowLogic()
        {
            access = new TOaArchivesBorrowAccess();
        }

        public TOaArchivesBorrowLogic(TOaArchivesBorrowVo _tOaArchivesBorrow)
        {
            tOaArchivesBorrow = _tOaArchivesBorrow;
            access = new TOaArchivesBorrowAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            return access.GetSelectResultCount(tOaArchivesBorrow);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesBorrowVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesBorrowVo Details(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            return access.Details(tOaArchivesBorrow);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaArchivesBorrowVo> SelectByObject(TOaArchivesBorrowVo tOaArchivesBorrow, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaArchivesBorrow, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaArchivesBorrowVo tOaArchivesBorrow, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaArchivesBorrow, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesBorrow"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            return access.SelectByTable(tOaArchivesBorrow);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <returns></returns>
        public TOaArchivesBorrowVo SelectByObject(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            return access.SelectByObject(tOaArchivesBorrow);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            return access.Create(tOaArchivesBorrow);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesBorrow">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            return access.Edit(tOaArchivesBorrow);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesBorrow_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesBorrow_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesBorrowVo tOaArchivesBorrow_UpdateSet, TOaArchivesBorrowVo tOaArchivesBorrow_UpdateWhere)
        {
            return access.Edit(tOaArchivesBorrow_UpdateSet, tOaArchivesBorrow_UpdateWhere);
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
        public bool Delete(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            return access.Delete(tOaArchivesBorrow);
        }

        /// <summary>
        /// 借阅情况统计
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <returns></returns>
        public int GetSelectResultCountForSearch(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            return access.GetSelectResultCountForSearch(tOaArchivesBorrow);
        }

        /// <summary>
        /// 借阅情况
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectByTableForSearch(TOaArchivesBorrowVo tOaArchivesBorrow, int intPageIndex, int intPageSize)
        {
            return access.SelectByTableForSearch(tOaArchivesBorrow, intPageIndex, intPageSize);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键
            if (tOaArchivesBorrow.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键不能为空");
                return false;
            }
            //文件ID
            if (tOaArchivesBorrow.DOCUMENT_ID.Trim() == "")
            {
                this.Tips.AppendLine("文件ID不能为空");
                return false;
            }
            //借出状态，0为未借出，1为已借出
            if (tOaArchivesBorrow.LENT_OUT_STATE.Trim() == "")
            {
                this.Tips.AppendLine("借出状态，0为未借出，1为已借出不能为空");
                return false;
            }
            //借阅人/归还人
            if (tOaArchivesBorrow.BORROWER.Trim() == "")
            {
                this.Tips.AppendLine("借阅人/归还人不能为空");
                return false;
            }
            //借阅天数
            if (tOaArchivesBorrow.HOLD_TIME.Trim() == "")
            {
                this.Tips.AppendLine("借阅天数不能为空");
                return false;
            }
            //借出时间/归还时间
            if (tOaArchivesBorrow.LOAN_TIME.Trim() == "")
            {
                this.Tips.AppendLine("借出时间/归还时间不能为空");
                return false;
            }
            //借出备注/归还备注
            if (tOaArchivesBorrow.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("借出备注/归还备注不能为空");
                return false;
            }
            //备注1
            if (tOaArchivesBorrow.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaArchivesBorrow.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaArchivesBorrow.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaArchivesBorrow.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaArchivesBorrow.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
