using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.OA.Notice;
using i3.ValueObject;

namespace i3.DataAccess.Channels.OA.Notice
{
    /// <summary>
    /// 功能：公告管理
    /// 创建日期：2013-02-23
    /// 创建人：熊卫华
    /// </summary>
    public class TOaNoticeAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaNotice">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaNoticeVo tOaNotice)
        {
            string strSQL = "select Count(*) from T_OA_NOTICE " + this.BuildWhereStatement(tOaNotice);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaNoticeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_NOTICE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaNoticeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaNotice">对象条件</param>
        /// <returns>对象</returns>
        public TOaNoticeVo Details(TOaNoticeVo tOaNotice)
        {
            string strSQL = String.Format("select * from  T_OA_NOTICE " + this.BuildWhereStatement(tOaNotice));
            return SqlHelper.ExecuteObject(new TOaNoticeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaNotice">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaNoticeVo> SelectByObject(TOaNoticeVo tOaNotice, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_OA_NOTICE " + this.BuildWhereStatement(tOaNotice));
            return SqlHelper.ExecuteObjectList(tOaNotice, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaNotice">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaNoticeVo tOaNotice, int iIndex, int iCount)
        {

            string strSQL = " select * from T_OA_NOTICE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaNotice));
            strSQL += " order by ID desc";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaNotice"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaNoticeVo tOaNotice)
        {
            string strSQL = "select * from T_OA_NOTICE " + this.BuildWhereStatement(tOaNotice);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaNotice">对象</param>
        /// <returns></returns>
        public TOaNoticeVo SelectByObject(TOaNoticeVo tOaNotice)
        {
            string strSQL = "select * from T_OA_NOTICE " + this.BuildWhereStatement(tOaNotice);
            return SqlHelper.ExecuteObject(new TOaNoticeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaNotice">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaNoticeVo tOaNotice)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaNotice, TOaNoticeVo.T_OA_NOTICE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaNotice">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaNoticeVo tOaNotice)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaNotice, TOaNoticeVo.T_OA_NOTICE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaNotice.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 置顶前修改所有非置顶状态 黄进军20141112
        /// </summary>
        /// <param name="tOaNotice">用户对象</param>
        /// <returns>是否成功</returns>
        public bool EditAll()
        {
            string strSQL = "update T_OA_NOTICE set REMARK1='0'";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 置顶修改置顶状态 黄进军20141112
        /// </summary>
        /// <param name="tOaNotice">用户对象</param>
        /// <returns>是否成功</returns>
        public bool EditSetTopOne(string id)
        {
            string strSQL = "update T_OA_NOTICE set REMARK1='1'";
            strSQL += string.Format(" where ID='{0}' ", id);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 按时间获取最早十条数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTopTenData()
        {
            string strSQL = @"select top 10.* from T_OA_NOTICE order by isnull(REMARK1,'0') desc,ID desc";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaNotice_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaNotice_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaNoticeVo tOaNotice_UpdateSet, TOaNoticeVo tOaNotice_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaNotice_UpdateSet, TOaNoticeVo.T_OA_NOTICE_TABLE);
            strSQL += this.BuildWhereStatement(tOaNotice_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_NOTICE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaNoticeVo tOaNotice)
        {
            string strSQL = "delete from T_OA_NOTICE ";
            strSQL += this.BuildWhereStatement(tOaNotice);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaNotice"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaNoticeVo tOaNotice)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaNotice)
            {

                //编号
                if (!String.IsNullOrEmpty(tOaNotice.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaNotice.ID.ToString()));
                }
                //公告标题
                if (!String.IsNullOrEmpty(tOaNotice.TITLE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TITLE like '%{0}%'", tOaNotice.TITLE.ToString()));
                }
                //公告内容
                if (!String.IsNullOrEmpty(tOaNotice.CONTENT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTENT = '{0}'", tOaNotice.CONTENT.ToString()));
                }
                //公告类别
                if (!String.IsNullOrEmpty(tOaNotice.NOTICE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NOTICE_TYPE = '{0}'", tOaNotice.NOTICE_TYPE.ToString()));
                }
                //发布时间
                if (!String.IsNullOrEmpty(tOaNotice.RELEASE_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RELEASE_TIME = '{0}'", tOaNotice.RELEASE_TIME.ToString()));
                }
                //发布人
                if (!String.IsNullOrEmpty(tOaNotice.RELIEASER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RELIEASER = '{0}'", tOaNotice.RELIEASER.ToString()));
                }
                //发布方式
                if (!String.IsNullOrEmpty(tOaNotice.RELIEASER_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RELIEASER_TYPE = '{0}'", tOaNotice.RELIEASER_TYPE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaNotice.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaNotice.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaNotice.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaNotice.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaNotice.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaNotice.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tOaNotice.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaNotice.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tOaNotice.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaNotice.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
