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
    /// 功能：档案文件分发回收
    /// 创建日期：2013-01-31
    /// 创建人：邵世卓
    /// </summary>
    public class TOaArchivesSendAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesSend">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesSendVo tOaArchivesSend)
        {
            string strSQL = "select Count(*) from T_OA_ARCHIVES_SEND " + this.BuildWhereStatement(tOaArchivesSend);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesSendVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_SEND  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaArchivesSendVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesSend">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesSendVo Details(TOaArchivesSendVo tOaArchivesSend)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_SEND " + this.BuildWhereStatement(tOaArchivesSend));
            return SqlHelper.ExecuteObject(new TOaArchivesSendVo(), strSQL);
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

            string strSQL = String.Format("select * from  T_OA_ARCHIVES_SEND " + this.BuildWhereStatement(tOaArchivesSend));
            return SqlHelper.ExecuteObjectList(tOaArchivesSend, BuildPagerExpress(strSQL, iIndex, iCount));

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

            string strSQL = " select * from T_OA_ARCHIVES_SEND {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesSend));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesSend"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesSendVo tOaArchivesSend)
        {
            string strSQL = "select * from T_OA_ARCHIVES_SEND " + this.BuildWhereStatement(tOaArchivesSend);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesSend">对象</param>
        /// <returns></returns>
        public TOaArchivesSendVo SelectByObject(TOaArchivesSendVo tOaArchivesSend)
        {
            string strSQL = "select * from T_OA_ARCHIVES_SEND " + this.BuildWhereStatement(tOaArchivesSend);
            return SqlHelper.ExecuteObject(new TOaArchivesSendVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaArchivesSend">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesSendVo tOaArchivesSend)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaArchivesSend, TOaArchivesSendVo.T_OA_ARCHIVES_SEND_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesSend">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesSendVo tOaArchivesSend)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesSend, TOaArchivesSendVo.T_OA_ARCHIVES_SEND_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaArchivesSend.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesSend_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesSend_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesSendVo tOaArchivesSend_UpdateSet, TOaArchivesSendVo tOaArchivesSend_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesSend_UpdateSet, TOaArchivesSendVo.T_OA_ARCHIVES_SEND_TABLE);
            strSQL += this.BuildWhereStatement(tOaArchivesSend_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_ARCHIVES_SEND where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaArchivesSendVo tOaArchivesSend)
        {
            string strSQL = "delete from T_OA_ARCHIVES_SEND ";
            strSQL += this.BuildWhereStatement(tOaArchivesSend);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 分发情况统计
        /// </summary>
        /// <param name="tOaArchivesBorrow">对象</param>
        /// <returns></returns>
        public int GetSelectResultCountForSearch(TOaArchivesSendVo tOaArchivesSend)
        {
            string strSQL = @" select count(*)
                                                from (select * from T_OA_ARCHIVES_SEND {0} ) send 
                                                inner join T_OA_ARCHIVES_DOCUMENT document ON document.IS_DEL='0' and send.DOCUMENT_ID=document.ID";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesSend));
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
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
            string strSQL = @" select send.ID, send.BORROWER,send.LENT_OUT_STATE,send.HOLD_TIME,send.LOAN_TIME,document.DOCUMENT_CODE
                                                from (select * from T_OA_ARCHIVES_SEND {0} ) send 
                                                left join T_OA_ARCHIVES_DOCUMENT document ON send.DOCUMENT_ID=document.ID";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesSend));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 批量增加分发记录
        /// </summary>
        /// <param name="tOaArchivesSend">对象</param>
        /// <returns></returns>
        public bool CreateTrans(TOaArchivesSendVo tOaArchivesSend)
        {
            ArrayList arrSql = new ArrayList();
            if (tOaArchivesSend.BORROWER.Length > 0)
            {
                string[] strBorrow = tOaArchivesSend.BORROWER.Split(',');//所有选择的分发对象
                foreach (string str in strBorrow)
                {
                    if (str.Length > 0)
                    {
                        tOaArchivesSend.ID = GetSerialNumber("t_oa_archivessend");
                        tOaArchivesSend.BORROWER = str;
                        string strSQL = SqlHelper.BuildInsertExpress(tOaArchivesSend, TOaArchivesSendVo.T_OA_ARCHIVES_SEND_TABLE);
                        arrSql.Add(strSQL);
                    }
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrSql);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaArchivesSend"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaArchivesSendVo tOaArchivesSend)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaArchivesSend)
            {

                //主键
                if (!String.IsNullOrEmpty(tOaArchivesSend.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaArchivesSend.ID.ToString()));
                }
                //文件ID
                if (!String.IsNullOrEmpty(tOaArchivesSend.DOCUMENT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOCUMENT_ID = '{0}'", tOaArchivesSend.DOCUMENT_ID.ToString()));
                }
                //分发状态，0为回收，1为分发
                if (!String.IsNullOrEmpty(tOaArchivesSend.LENT_OUT_STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LENT_OUT_STATE = '{0}'", tOaArchivesSend.LENT_OUT_STATE.ToString()));
                }
                //接收人/回收人
                if (!String.IsNullOrEmpty(tOaArchivesSend.BORROWER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BORROWER = '{0}'", tOaArchivesSend.BORROWER.ToString()));
                }
                //份数
                if (!String.IsNullOrEmpty(tOaArchivesSend.HOLD_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOLD_TIME = '{0}'", tOaArchivesSend.HOLD_TIME.ToString()));
                }
                //分发时间/回收时间
                if (!String.IsNullOrEmpty(tOaArchivesSend.LOAN_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOAN_TIME = '{0}'", tOaArchivesSend.LOAN_TIME.ToString()));
                }
                //借出备注/归还备注
                if (!String.IsNullOrEmpty(tOaArchivesSend.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tOaArchivesSend.REMARK.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaArchivesSend.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaArchivesSend.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaArchivesSend.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaArchivesSend.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaArchivesSend.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaArchivesSend.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tOaArchivesSend.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaArchivesSend.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tOaArchivesSend.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaArchivesSend.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
