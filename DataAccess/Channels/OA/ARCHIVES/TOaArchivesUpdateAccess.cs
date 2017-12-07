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
    /// 功能：档案文件查新
    /// 创建日期：2013-01-31
    /// 创建人：邵世卓
    /// </summary>
    public class TOaArchivesUpdateAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            string strSQL = "select Count(*) from T_OA_ARCHIVES_UPDATE " + this.BuildWhereStatement(tOaArchivesUpdate);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesUpdateVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_UPDATE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaArchivesUpdateVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesUpdateVo Details(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_UPDATE " + this.BuildWhereStatement(tOaArchivesUpdate));
            return SqlHelper.ExecuteObject(new TOaArchivesUpdateVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaArchivesUpdateVo> SelectByObject(TOaArchivesUpdateVo tOaArchivesUpdate, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_OA_ARCHIVES_UPDATE " + this.BuildWhereStatement(tOaArchivesUpdate));
            return SqlHelper.ExecuteObjectList(tOaArchivesUpdate, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaArchivesUpdateVo tOaArchivesUpdate, int iIndex, int iCount)
        {

            string strSQL = " select * from T_OA_ARCHIVES_UPDATE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesUpdate));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesUpdate"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            string strSQL = "select * from T_OA_ARCHIVES_UPDATE " + this.BuildWhereStatement(tOaArchivesUpdate);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象</param>
        /// <returns></returns>
        public TOaArchivesUpdateVo SelectByObject(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            string strSQL = "select * from T_OA_ARCHIVES_UPDATE " + this.BuildWhereStatement(tOaArchivesUpdate);
            return SqlHelper.ExecuteObject(new TOaArchivesUpdateVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaArchivesUpdate, TOaArchivesUpdateVo.T_OA_ARCHIVES_UPDATE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesUpdate">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesUpdate, TOaArchivesUpdateVo.T_OA_ARCHIVES_UPDATE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaArchivesUpdate.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesUpdate_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesUpdate_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesUpdateVo tOaArchivesUpdate_UpdateSet, TOaArchivesUpdateVo tOaArchivesUpdate_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesUpdate_UpdateSet, TOaArchivesUpdateVo.T_OA_ARCHIVES_UPDATE_TABLE);
            strSQL += this.BuildWhereStatement(tOaArchivesUpdate_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_ARCHIVES_UPDATE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            string strSQL = "delete from T_OA_ARCHIVES_UPDATE ";
            strSQL += this.BuildWhereStatement(tOaArchivesUpdate);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        #region 特定查询
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountForSearch(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            string strSQL = @" select count(*) from (select * from T_OA_ARCHIVES_UPDATE {0}) update
                                                JOIN T_OA_ARCHIVES_DOCUMENT doc ON doc.IS_DEL='0' and doc.ID=update.DOCUMENT_ID";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesUpdate));
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 特定查询
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectByTableForSearch(TOaArchivesUpdateVo tOaArchivesUpdate, int intPageIndex, int intPageSize)
        {
            string strSQL = @" select u.*,doc.DOCUMENT_CODE,doc.DOCUMENT_NAME from (select * from T_OA_ARCHIVES_UPDATE {0}) u
                                                INNER JOIN T_OA_ARCHIVES_DOCUMENT doc ON doc.IS_DEL='0' and doc.ID=u.BEFORE_NAME";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesUpdate));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }
        #endregion

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaArchivesUpdate"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaArchivesUpdate)
            {

                //
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaArchivesUpdate.ID.ToString()));
                }
                //查新人ID
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.PERSON_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PERSON_ID = '{0}'", tOaArchivesUpdate.PERSON_ID.ToString()));
                }
                //查新日期
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.UPDATE_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPDATE_TIME = '{0}'", tOaArchivesUpdate.UPDATE_TIME.ToString()));
                }
                //方式（1、废止2、替换）
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.UPDATE_WAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPDATE_WAY = '{0}'", tOaArchivesUpdate.UPDATE_WAY.ToString()));
                }
                //查新前档案ID
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.BEFORE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEFORE_NAME = '{0}'", tOaArchivesUpdate.BEFORE_NAME.ToString()));
                }
                //查新后档案ID
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.AFTER_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AFTER_NAME = '{0}'", tOaArchivesUpdate.AFTER_NAME.ToString()));
                }
                //备注
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tOaArchivesUpdate.REMARK.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaArchivesUpdate.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaArchivesUpdate.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaArchivesUpdate.REMARK3.ToString()));
                }
                //删除标识
                if (!String.IsNullOrEmpty(tOaArchivesUpdate.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tOaArchivesUpdate.IS_DEL.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
