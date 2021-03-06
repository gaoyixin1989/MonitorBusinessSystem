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
    /// 功能：目录文件管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaArchivesDocumentAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = "select Count(*) from T_OA_ARCHIVES_DOCUMENT " + this.BuildWhereStatement(tOaArchivesDocument);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesDocumentVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_DOCUMENT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaArchivesDocumentVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesDocument">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesDocumentVo Details(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_DOCUMENT " + this.BuildWhereStatement(tOaArchivesDocument));
            return SqlHelper.ExecuteObject(new TOaArchivesDocumentVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaArchivesDocumentVo> SelectByObject(TOaArchivesDocumentVo tOaArchivesDocument, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_OA_ARCHIVES_DOCUMENT " + this.BuildWhereStatement(tOaArchivesDocument));
            return SqlHelper.ExecuteObjectList(tOaArchivesDocument, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaArchivesDocumentVo tOaArchivesDocument, int iIndex, int iCount)
        {

            string strSQL = " select * from T_OA_ARCHIVES_DOCUMENT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesDocument));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesDocument"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = "select * from T_OA_ARCHIVES_DOCUMENT " + this.BuildWhereStatement(tOaArchivesDocument);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <returns></returns>
        public TOaArchivesDocumentVo SelectByObject(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = "select * from T_OA_ARCHIVES_DOCUMENT " + this.BuildWhereStatement(tOaArchivesDocument);
            return SqlHelper.ExecuteObject(new TOaArchivesDocumentVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaArchivesDocument, TOaArchivesDocumentVo.T_OA_ARCHIVES_DOCUMENT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesDocument">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesDocument, TOaArchivesDocumentVo.T_OA_ARCHIVES_DOCUMENT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaArchivesDocument.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesDocument_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesDocument_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesDocumentVo tOaArchivesDocument_UpdateSet, TOaArchivesDocumentVo tOaArchivesDocument_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesDocument_UpdateSet, TOaArchivesDocumentVo.T_OA_ARCHIVES_DOCUMENT_TABLE);
            strSQL += this.BuildWhereStatement(tOaArchivesDocument_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_ARCHIVES_DOCUMENT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        public bool update(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = String.Format("update T_OA_ARCHIVES_DOCUMENT  set UPDATE_DATE='{0}' where ID='{1}'", tOaArchivesDocument.UPDATE_DATE, tOaArchivesDocument.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = "delete from T_OA_ARCHIVES_DOCUMENT ";
            strSQL += this.BuildWhereStatement(tOaArchivesDocument);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 表单查询
        /// </summary>
        /// <param name="tOaArchivesDocument">用户对象</param>
        /// <returns></returns>
        public int SelectByTableForSearchCount(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = "select count(*) from T_OA_ARCHIVES_DOCUMENT where  (IS_DEL='0' or IS_DEL='1')  ";
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_CODE))
            {
                strSQL += string.Format(" AND DOCUMENT_CODE='{0}'", tOaArchivesDocument.DOCUMENT_CODE);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_NAME))
            {
                strSQL += string.Format(" AND DOCUMENT_NAME like '%{0}%'", tOaArchivesDocument.DOCUMENT_NAME);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.SAVE_TYPE))
            {
                strSQL += string.Format(" AND SAVE_TYPE='{0}'", tOaArchivesDocument.SAVE_TYPE);
            }
            if (!String.IsNullOrEmpty(tOaArchivesDocument.P_KEY.ToString().Trim()))
            {
                //主题词查询处理
                string strWhere = "";
                string[] strKey = tOaArchivesDocument.P_KEY.Split('|');
                foreach (string str in strKey)
                {
                    strWhere += string.Format(" or P_KEY like '%{0}%'", str);
                }
                if (strWhere.Length > 0)
                    strSQL += " AND (" + strWhere.Remove(strWhere.IndexOf("or"), 2) + ")";
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_LOCATION))
            {
                strSQL += string.Format(" AND DOCUMENT_LOCATION like '%{0}%'", tOaArchivesDocument.DOCUMENT_LOCATION);
            }
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 表单查询(审核)
        /// </summary>
        /// <param name="tOaArchivesDocument">用户对象</param>
        /// <returns></returns>
        public int SelectTableForSearchCount(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = "select count(*) from T_OA_ARCHIVES_DOCUMENT where  (IS_DEL='0' or IS_DEL='1') ";
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_CODE))
            {
                strSQL += string.Format(" AND DOCUMENT_CODE='{0}'", tOaArchivesDocument.DOCUMENT_CODE);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_NAME))
            {
                strSQL += string.Format(" AND DOCUMENT_NAME like '%{0}%'", tOaArchivesDocument.DOCUMENT_NAME);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.SAVE_TYPE))
            {
                strSQL += string.Format(" AND SAVE_TYPE='{0}'", tOaArchivesDocument.SAVE_TYPE);
            }
            if (!String.IsNullOrEmpty(tOaArchivesDocument.P_KEY.ToString().Trim()))
            {
                //主题词查询处理
                string strWhere = "";
                string[] strKey = tOaArchivesDocument.P_KEY.Split('|');
                foreach (string str in strKey)
                {
                    strWhere += string.Format(" or P_KEY like '%{0}%'", str);
                }
                if (strWhere.Length > 0)
                    strSQL += " AND (" + strWhere.Remove(strWhere.IndexOf("or"), 2) + ")";
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_LOCATION))
            {
                strSQL += string.Format(" AND DOCUMENT_LOCATION like '%{0}%'", tOaArchivesDocument.DOCUMENT_LOCATION);
            }
            return Int32.Parse(SqlHelper.ExecuteScalar(strSQL).ToString());
        }

        /// <summary>
        /// 表单查询
        /// </summary>
        /// <param name="tOaArchivesDocument">用户对象</param>
        /// <returns></returns>
        public DataTable SelectByTableForSearch(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            string strSQL = "select * from T_OA_ARCHIVES_DOCUMENT where  IS_DEL='0'";
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_CODE))
            {
                strSQL += string.Format(" AND DOCUMENT_CODE='{0}'", tOaArchivesDocument.DOCUMENT_CODE);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_NAME))
            {
                strSQL += string.Format(" AND DOCUMENT_NAME like '%{0}%'", tOaArchivesDocument.DOCUMENT_NAME);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.SAVE_TYPE))
            {
                strSQL += string.Format(" AND SAVE_TYPE='{0}'", tOaArchivesDocument.SAVE_TYPE);
            }
            if (!String.IsNullOrEmpty(tOaArchivesDocument.P_KEY.ToString().Trim()))
            {
                //主题词查询处理
                string strWhere = "";
                string[] strKey = tOaArchivesDocument.P_KEY.Split('|');
                foreach (string str in strKey)
                {
                    strWhere += string.Format(" or P_KEY like '%{0}%'", str);
                }
                if (strWhere.Length > 0)
                    strSQL += " AND (" + strWhere.Remove(strWhere.IndexOf("or"), 2) + ")";
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_LOCATION))
            {
                strSQL += string.Format(" AND DOCUMENT_LOCATION like '%{0}%'", tOaArchivesDocument.DOCUMENT_LOCATION);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 表单查询
        /// </summary>
        /// <param name="tOaArchivesDocument">用户对象</param>
        /// <returns></returns>
        public DataTable SelectByTableForSearch(TOaArchivesDocumentVo tOaArchivesDocument, int intPageIndex, int intPageSize)
        {
            string strSQL = "select * from T_OA_ARCHIVES_DOCUMENT where (IS_DEL='0' or IS_DEL='1')   ";
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DIRECTORY_ID))
            {
                strSQL += string.Format(" AND DIRECTORY_ID='{0}'", tOaArchivesDocument.DIRECTORY_ID);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_CODE))
            {
                strSQL += string.Format(" AND DOCUMENT_CODE='{0}'", tOaArchivesDocument.DOCUMENT_CODE);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_NAME))
            {
                strSQL += string.Format(" AND DOCUMENT_NAME like '%{0}%'", tOaArchivesDocument.DOCUMENT_NAME);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.SAVE_TYPE))
            {
                strSQL += string.Format(" AND SAVE_TYPE='{0}'", tOaArchivesDocument.SAVE_TYPE);
            }
            if (!String.IsNullOrEmpty(tOaArchivesDocument.P_KEY.ToString().Trim()))
            {
                //主题词查询处理
                string strWhere = "";
                string[] strKey = tOaArchivesDocument.P_KEY.Split('|');
                foreach (string str in strKey)
                {
                    strWhere += string.Format(" or P_KEY like '%{0}%'", str);
                }
                if (strWhere.Length > 0)
                    strSQL += " AND (" + strWhere.Remove(strWhere.IndexOf("or"), 2) + ")";
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_LOCATION))
            {
                strSQL += string.Format(" AND DOCUMENT_LOCATION like '%{0}%'", tOaArchivesDocument.DOCUMENT_LOCATION);
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }


        /// <summary>
        /// 表单查询(审核)
        /// </summary>
        /// <param name="tOaArchivesDocument">用户对象</param>
        /// <returns></returns>
        public DataTable SelectTableForSearch(TOaArchivesDocumentVo tOaArchivesDocument, int intPageIndex, int intPageSize)
        {
            string strSQL = "select a.ID,a.DOCUMENT_CODE,a.DOCUMENT_NAME,a.VERSION,a.SAVE_TYPE,a.SAVE_YEAR,a.IS_DEL,b.DIRECTORY_NAME,a.UPDATE_DATE from T_OA_ARCHIVES_DOCUMENT a left join T_OA_ARCHIVES_DIRECTORY b on a.DIRECTORY_ID=b.id  where (a.IS_DEL='0' or a.IS_DEL='1')  ";
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DIRECTORY_ID))
            {
                strSQL += string.Format(" AND a.DIRECTORY_ID='{0}'", tOaArchivesDocument.DIRECTORY_ID);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_CODE))
            {
                strSQL += string.Format(" AND a.DOCUMENT_CODE='{0}'", tOaArchivesDocument.DOCUMENT_CODE);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_NAME))
            {
                strSQL += string.Format(" AND a.DOCUMENT_NAME like '%{0}%'", tOaArchivesDocument.DOCUMENT_NAME);
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.SAVE_TYPE))
            {
                strSQL += string.Format(" AND a.SAVE_TYPE='{0}'", tOaArchivesDocument.SAVE_TYPE);
            }
            if (!String.IsNullOrEmpty(tOaArchivesDocument.P_KEY.ToString().Trim()))
            {
                //主题词查询处理
                string strWhere = "";
                string[] strKey = tOaArchivesDocument.P_KEY.Split('|');
                foreach (string str in strKey)
                {
                    strWhere += string.Format(" or a.P_KEY like '%{0}%'", str);
                }
                if (strWhere.Length > 0)
                    strSQL += " AND (" + strWhere.Remove(strWhere.IndexOf("or"), 2) + ")";
            }
            if (!string.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_LOCATION))
            {
                strSQL += string.Format(" AND a.DOCUMENT_LOCATION like '%{0}%'", tOaArchivesDocument.DOCUMENT_LOCATION);
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        #region 废止&销毁
        /// <summary>
        /// 废止文档历史查询
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectByTableForDelete(TOaArchivesDocumentVo tOaArchivesDocument, int intPageIndex, int intPageSize)
        {
            string strSQL = @"select document.*,u.REAL_NAME as OPERATOR_NAME
                                            from (select * from T_OA_ARCHIVES_DOCUMENT {0}) document
                                            LEFT JOIN T_SYS_USER u ON document.OPERATOR=u.ID";
            strSQL = string.Format(strSQL, BuildWhereStatement(tOaArchivesDocument));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }
        #endregion

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaArchivesDocument"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaArchivesDocument)
            {

                //主键
                if (!String.IsNullOrEmpty(tOaArchivesDocument.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaArchivesDocument.ID.ToString()));
                }
                //目录ID
                if (!String.IsNullOrEmpty(tOaArchivesDocument.DIRECTORY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DIRECTORY_ID = '{0}'", tOaArchivesDocument.DIRECTORY_ID.ToString()));
                }
                //文件名称
                if (!String.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOCUMENT_NAME = '{0}'", tOaArchivesDocument.DOCUMENT_NAME.ToString()));
                }
                //文件类型
                if (!String.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOCUMENT_TYPE = '{0}'", tOaArchivesDocument.DOCUMENT_TYPE.ToString()));
                }
                //条形码
                if (!String.IsNullOrEmpty(tOaArchivesDocument.BAR_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BAR_CODE = '{0}'", tOaArchivesDocument.BAR_CODE.ToString()));
                }
                //文件大小
                if (!String.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_SIZE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOCUMENT_SIZE = '{0}'", tOaArchivesDocument.DOCUMENT_SIZE.ToString()));
                }
                //上传日期
                if (!String.IsNullOrEmpty(tOaArchivesDocument.UPLOADING_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPLOADING_DATE = '{0}'", tOaArchivesDocument.UPLOADING_DATE.ToString()));
                }
                //存放位置
                if (!String.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOCUMENT_LOCATION = '{0}'", tOaArchivesDocument.DOCUMENT_LOCATION.ToString()));
                }
                //文件路径
                if (!String.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_PATH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOCUMENT_PATH = '{0}'", tOaArchivesDocument.DOCUMENT_PATH.ToString()));
                }
                //文件描述
                if (!String.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_DESCRIPTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOCUMENT_DESCRIPTION = '{0}'", tOaArchivesDocument.DOCUMENT_DESCRIPTION.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaArchivesDocument.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaArchivesDocument.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaArchivesDocument.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaArchivesDocument.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaArchivesDocument.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaArchivesDocument.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tOaArchivesDocument.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaArchivesDocument.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tOaArchivesDocument.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaArchivesDocument.REMARK5.ToString()));
                }
                //档案编号
                if (!String.IsNullOrEmpty(tOaArchivesDocument.DOCUMENT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DOCUMENT_CODE = '{0}'", tOaArchivesDocument.DOCUMENT_CODE.ToString()));
                }
                //版本号/修订数
                if (!String.IsNullOrEmpty(tOaArchivesDocument.VERSION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERSION = '{0}'", tOaArchivesDocument.VERSION.ToString()));
                }
                //页码
                if (!String.IsNullOrEmpty(tOaArchivesDocument.PAGE_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PAGE_CODE = '{0}'", tOaArchivesDocument.PAGE_CODE.ToString()));
                }
                //总页数
                if (!String.IsNullOrEmpty(tOaArchivesDocument.PAGE_SIZE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PAGE_SIZE = '{0}'", tOaArchivesDocument.PAGE_SIZE.ToString()));
                }
                //颁布时间/修订时间
                if (!String.IsNullOrEmpty(tOaArchivesDocument.UPDATE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPDATE_DATE = '{0}'", tOaArchivesDocument.UPDATE_DATE.ToString()));
                }
                //结束标记/颁布机构
                if (!String.IsNullOrEmpty(tOaArchivesDocument.END_UNIT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_UNIT = '{0}'", tOaArchivesDocument.END_UNIT.ToString()));
                }
                //保存类型
                if (!String.IsNullOrEmpty(tOaArchivesDocument.SAVE_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAVE_TYPE = '{0}'", tOaArchivesDocument.SAVE_TYPE.ToString()));
                }
                //保存年份
                if (!String.IsNullOrEmpty(tOaArchivesDocument.SAVE_YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAVE_YEAR = '{0}'", tOaArchivesDocument.SAVE_YEAR.ToString()));
                }
                //是否废止
                if (!String.IsNullOrEmpty(tOaArchivesDocument.IS_OVER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_OVER = '{0}'", tOaArchivesDocument.IS_OVER.ToString()));
                }
                //是否销毁
                if (!String.IsNullOrEmpty(tOaArchivesDocument.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tOaArchivesDocument.IS_DEL.ToString()));
                }
                //主题词/关键字
                if (!String.IsNullOrEmpty(tOaArchivesDocument.P_KEY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND P_KEY = '{0}'", tOaArchivesDocument.P_KEY.ToString()));
                }
                //操作人
                if (!String.IsNullOrEmpty(tOaArchivesDocument.OPERATOR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OPERATOR = '{0}'", tOaArchivesDocument.OPERATOR.ToString()));
                }
                //操作时间
                if (!String.IsNullOrEmpty(tOaArchivesDocument.OPERATE_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OPERATE_TIME = '{0}'", tOaArchivesDocument.OPERATE_TIME.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
