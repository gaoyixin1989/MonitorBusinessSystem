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
    /// 功能：档案文件修订
    /// 创建日期：2013-01-31
    /// 创建人：邵世卓
    /// </summary>
    public class TOaArchivesCheckAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesCheckVo tOaArchivesCheck)
        {
            string strSQL = "select Count(*) from T_OA_ARCHIVES_CHECK " + this.BuildWhereStatement(tOaArchivesCheck);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesCheckVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_CHECK  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaArchivesCheckVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesCheck">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesCheckVo Details(TOaArchivesCheckVo tOaArchivesCheck)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_CHECK " + this.BuildWhereStatement(tOaArchivesCheck));
            return SqlHelper.ExecuteObject(new TOaArchivesCheckVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaArchivesCheckVo> SelectByObject(TOaArchivesCheckVo tOaArchivesCheck, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_OA_ARCHIVES_CHECK " + this.BuildWhereStatement(tOaArchivesCheck));
            return SqlHelper.ExecuteObjectList(tOaArchivesCheck, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaArchivesCheckVo tOaArchivesCheck, int iIndex, int iCount)
        {

            string strSQL = " select * from T_OA_ARCHIVES_CHECK {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesCheck));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesCheck"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesCheckVo tOaArchivesCheck)
        {
            string strSQL = "select * from T_OA_ARCHIVES_CHECK " + this.BuildWhereStatement(tOaArchivesCheck);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <returns></returns>
        public TOaArchivesCheckVo SelectByObject(TOaArchivesCheckVo tOaArchivesCheck)
        {
            string strSQL = "select * from T_OA_ARCHIVES_CHECK " + this.BuildWhereStatement(tOaArchivesCheck);
            return SqlHelper.ExecuteObject(new TOaArchivesCheckVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesCheckVo tOaArchivesCheck)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaArchivesCheck, TOaArchivesCheckVo.T_OA_ARCHIVES_CHECK_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesCheck">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesCheckVo tOaArchivesCheck)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesCheck, TOaArchivesCheckVo.T_OA_ARCHIVES_CHECK_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaArchivesCheck.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesCheck_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesCheck_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesCheckVo tOaArchivesCheck_UpdateSet, TOaArchivesCheckVo tOaArchivesCheck_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesCheck_UpdateSet, TOaArchivesCheckVo.T_OA_ARCHIVES_CHECK_TABLE);
            strSQL += this.BuildWhereStatement(tOaArchivesCheck_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_ARCHIVES_CHECK where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaArchivesCheckVo tOaArchivesCheck)
        {
            string strSQL = "delete from T_OA_ARCHIVES_CHECK ";
            strSQL += this.BuildWhereStatement(tOaArchivesCheck);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        #region 特定查询
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountForSearch(TOaArchivesCheckVo tOaArchivesCheck)
        {
            string strSQL = @" select count(*) from (select * from T_OA_ARCHIVES_CHECK {0}) c
                                                JOIN T_OA_ARCHIVES_DOCUMENT doc ON doc.IS_DEL='0' and doc.ID=c.DOCUMENT_ID";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesCheck));
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 特定查询
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectByTableForSearch(TOaArchivesCheckVo tOaArchivesCheck, int intPageIndex, int intPageSize)
        {
            string strSQL = @" select c.*,(case when c.UPDATE_DATE is not null then convert(nvarchar(16),c.UPDATE_DATE,23) else '' end) as UPDATE_TIME,doc.DOCUMENT_CODE,doc.DOCUMENT_NAME from (select * from T_OA_ARCHIVES_CHECK {0}) c
                                                INNER JOIN T_OA_ARCHIVES_DOCUMENT doc ON  doc.IS_DEL='0' and doc.ID=c.DOCUMENT_ID";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesCheck));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }
        #endregion
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaArchivesCheck"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaArchivesCheckVo tOaArchivesCheck)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaArchivesCheck)
            {

                //ID
                if (!String.IsNullOrEmpty(tOaArchivesCheck.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaArchivesCheck.ID.ToString()));
                }
                //档案文件ID
                if (!String.IsNullOrEmpty(tOaArchivesCheck.DOCUMENT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOCUMENT_ID = '{0}'", tOaArchivesCheck.DOCUMENT_ID.ToString()));
                }
                //修订类别（换页、改版）
                if (!String.IsNullOrEmpty(tOaArchivesCheck.UPDATE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPDATE_TYPE = '{0}'", tOaArchivesCheck.UPDATE_TYPE.ToString()));
                }
                //页号
                if (!String.IsNullOrEmpty(tOaArchivesCheck.PAGE_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PAGE_NUM = '{0}'", tOaArchivesCheck.PAGE_NUM.ToString()));
                }
                //版本
                if (!String.IsNullOrEmpty(tOaArchivesCheck.VERSION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERSION = '{0}'", tOaArchivesCheck.VERSION.ToString()));
                }
                //改版前名称
                if (!String.IsNullOrEmpty(tOaArchivesCheck.OLD_FILE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OLD_FILE_NAME = '{0}'", tOaArchivesCheck.OLD_FILE_NAME.ToString()));
                }
                //原附件名
                if (!String.IsNullOrEmpty(tOaArchivesCheck.OLD_ATT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OLD_ATT_NAME = '{0}'", tOaArchivesCheck.OLD_ATT_NAME.ToString()));
                }
                //原附件说明
                if (!String.IsNullOrEmpty(tOaArchivesCheck.OLD_ATT_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OLD_ATT_INFO = '{0}'", tOaArchivesCheck.OLD_ATT_INFO.ToString()));
                }
                //原附件路径
                if (!String.IsNullOrEmpty(tOaArchivesCheck.OLD_ATT_URL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OLD_ATT_URL = '{0}'", tOaArchivesCheck.OLD_ATT_URL.ToString()));
                }
                //修改人ID
                if (!String.IsNullOrEmpty(tOaArchivesCheck.UPDATE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPDATE_ID = '{0}'", tOaArchivesCheck.UPDATE_ID.ToString()));
                }
                //修改日期
                if (!String.IsNullOrEmpty(tOaArchivesCheck.UPDATE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPDATE_DATE = '{0}'", tOaArchivesCheck.UPDATE_DATE.ToString()));
                }
                //是否销毁
                if (!String.IsNullOrEmpty(tOaArchivesCheck.IS_DESTROY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DESTROY = '{0}'", tOaArchivesCheck.IS_DESTROY.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaArchivesCheck.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaArchivesCheck.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaArchivesCheck.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaArchivesCheck.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaArchivesCheck.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaArchivesCheck.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tOaArchivesCheck.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaArchivesCheck.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tOaArchivesCheck.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaArchivesCheck.REMARK5.ToString()));
                }
                //申请内容
                if (!String.IsNullOrEmpty(tOaArchivesCheck.UPDATE_INFO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPDATE_INFO = '{0}'", tOaArchivesCheck.UPDATE_INFO.ToString()));
                }
                //备注
                if (!String.IsNullOrEmpty(tOaArchivesCheck.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tOaArchivesCheck.REMARK.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
