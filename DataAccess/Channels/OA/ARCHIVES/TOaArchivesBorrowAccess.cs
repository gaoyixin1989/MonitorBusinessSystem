using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.ARCHIVES;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.ARCHIVES
{
    /// <summary>
    /// 功能：借阅管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaArchivesBorrowAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            string strSQL = "select Count(*) from T_OA_ARCHIVES_BORROW " + this.BuildWhereStatement(tOaArchivesBorrow);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesBorrowVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_BORROW  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaArchivesBorrowVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesBorrowVo Details(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_BORROW " + this.BuildWhereStatement(tOaArchivesBorrow));
            return SqlHelper.ExecuteObject(new TOaArchivesBorrowVo(), strSQL);
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

            string strSQL = String.Format("select * from  T_OA_ARCHIVES_BORROW " + this.BuildWhereStatement(tOaArchivesBorrow));
            return SqlHelper.ExecuteObjectList(tOaArchivesBorrow, BuildPagerExpress(strSQL, iIndex, iCount));

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

            string strSQL = " select * from T_OA_ARCHIVES_BORROW {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesBorrow));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesBorrow"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            string strSQL = "select * from T_OA_ARCHIVES_BORROW " + this.BuildWhereStatement(tOaArchivesBorrow);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <returns></returns>
        public TOaArchivesBorrowVo SelectByObject(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            string strSQL = "select * from T_OA_ARCHIVES_BORROW " + this.BuildWhereStatement(tOaArchivesBorrow);
            return SqlHelper.ExecuteObject(new TOaArchivesBorrowVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaArchivesBorrow, TOaArchivesBorrowVo.T_OA_ARCHIVES_BORROW_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesBorrow">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesBorrow, TOaArchivesBorrowVo.T_OA_ARCHIVES_BORROW_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaArchivesBorrow.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesBorrow_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesBorrow_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesBorrowVo tOaArchivesBorrow_UpdateSet, TOaArchivesBorrowVo tOaArchivesBorrow_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesBorrow_UpdateSet, TOaArchivesBorrowVo.T_OA_ARCHIVES_BORROW_TABLE);
            strSQL += this.BuildWhereStatement(tOaArchivesBorrow_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_ARCHIVES_BORROW where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            string strSQL = "delete from T_OA_ARCHIVES_BORROW ";
            strSQL += this.BuildWhereStatement(tOaArchivesBorrow);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 借阅情况统计
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <returns></returns>
        public int GetSelectResultCountForSearch(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            string strSQL = @" select count(*) 
                                                from (select * from T_OA_ARCHIVES_BORROW {0} ) borrow 
                                                inner join T_OA_ARCHIVES_DOCUMENT document ON document.IS_DEL='0' and borrow.DOCUMENT_ID=document.ID";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesBorrow));
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
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
            string strSQL = @" select borrow.ID, borrow.LENT_OUT_STATE,borrow.HOLD_TIME,borrow.LOAN_TIME,document.DOCUMENT_CODE,employe.EMPLOYE_NAME as BORROWER_NAME 
                                                from (select * from T_OA_ARCHIVES_BORROW {0} ) borrow 
                                                left join T_OA_ARCHIVES_DOCUMENT document ON borrow.DOCUMENT_ID=document.ID
                                                left join T_OA_EMPLOYE_INFO employe ON borrow.BORROWER=employe.ID";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesBorrow));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaArchivesBorrow"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaArchivesBorrowVo tOaArchivesBorrow)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaArchivesBorrow)
            {

                //主键
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaArchivesBorrow.ID.ToString()));
                }
                //文件ID
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.DOCUMENT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOCUMENT_ID = '{0}'", tOaArchivesBorrow.DOCUMENT_ID.ToString()));
                }
                //借出状态，0为未借出，1为已借出
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.LENT_OUT_STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LENT_OUT_STATE = '{0}'", tOaArchivesBorrow.LENT_OUT_STATE.ToString()));
                }
                //借阅人/归还人
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.BORROWER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BORROWER = '{0}'", tOaArchivesBorrow.BORROWER.ToString()));
                }
                //借阅天数
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.HOLD_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOLD_TIME = '{0}'", tOaArchivesBorrow.HOLD_TIME.ToString()));
                }
                //借出时间/归还时间
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.LOAN_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOAN_TIME = '{0}'", tOaArchivesBorrow.LOAN_TIME.ToString()));
                }
                //借出备注/归还备注
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tOaArchivesBorrow.REMARK.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaArchivesBorrow.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaArchivesBorrow.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaArchivesBorrow.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaArchivesBorrow.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tOaArchivesBorrow.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaArchivesBorrow.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
