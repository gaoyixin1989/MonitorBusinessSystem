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
    /// 功能：文件目录管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaArchivesDirectoryAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            string strSQL = "select Count(*) from T_OA_ARCHIVES_DIRECTORY " + this.BuildWhereStatement(tOaArchivesDirectory);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesDirectoryVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_DIRECTORY  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaArchivesDirectoryVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesDirectoryVo Details(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            string strSQL = String.Format("select * from  T_OA_ARCHIVES_DIRECTORY " + this.BuildWhereStatement(tOaArchivesDirectory));
            return SqlHelper.ExecuteObject(new TOaArchivesDirectoryVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaArchivesDirectoryVo> SelectByObject(TOaArchivesDirectoryVo tOaArchivesDirectory, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_OA_ARCHIVES_DIRECTORY " + this.BuildWhereStatement(tOaArchivesDirectory));
            return SqlHelper.ExecuteObjectList(tOaArchivesDirectory, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaArchivesDirectoryVo tOaArchivesDirectory, int iIndex, int iCount)
        {

            string strSQL = " select * from T_OA_ARCHIVES_DIRECTORY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaArchivesDirectory));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesDirectory"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            string strSQL = "select * from T_OA_ARCHIVES_DIRECTORY " + this.BuildWhereStatement(tOaArchivesDirectory);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <returns></returns>
        public TOaArchivesDirectoryVo SelectByObject(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            string strSQL = "select * from T_OA_ARCHIVES_DIRECTORY " + this.BuildWhereStatement(tOaArchivesDirectory);
            return SqlHelper.ExecuteObject(new TOaArchivesDirectoryVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaArchivesDirectory, TOaArchivesDirectoryVo.T_OA_ARCHIVES_DIRECTORY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesDirectory">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesDirectory, TOaArchivesDirectoryVo.T_OA_ARCHIVES_DIRECTORY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaArchivesDirectory.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesDirectory_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesDirectory_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesDirectoryVo tOaArchivesDirectory_UpdateSet, TOaArchivesDirectoryVo tOaArchivesDirectory_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaArchivesDirectory_UpdateSet, TOaArchivesDirectoryVo.T_OA_ARCHIVES_DIRECTORY_TABLE);
            strSQL += this.BuildWhereStatement(tOaArchivesDirectory_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_ARCHIVES_DIRECTORY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            string strSQL = "delete from T_OA_ARCHIVES_DIRECTORY ";
            strSQL += this.BuildWhereStatement(tOaArchivesDirectory);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获取子级文档目录最大排序号
        /// </summary>
        /// <param name="tOaArchivesDirectory">对象</param>
        /// <returns></returns>
        public string getNum(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            string strSQL = "select MAX(NUM) from T_OA_ARCHIVES_DIRECTORY";
            strSQL += BuildWhereStatement(tOaArchivesDirectory);
            return SqlHelper.ExecuteScalar(strSQL).ToString();
        }

        /// <summary>
        /// 删除文档目录及其子目录
        /// </summary>
        /// <param name="strId">目录ID</param>
        /// <returns></returns>
        public bool DeleteTran(string strAllID)
        {
            string strSQL = "UPDATE  T_OA_ARCHIVES_DIRECTORY SET IS_USE='1' where ID in ({0})";
            strSQL = string.Format(strSQL, strAllID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaArchivesDirectory"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaArchivesDirectoryVo tOaArchivesDirectory)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaArchivesDirectory)
            {

                //主键
                if (!String.IsNullOrEmpty(tOaArchivesDirectory.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaArchivesDirectory.ID.ToString()));
                }
                //目录名称
                if (!String.IsNullOrEmpty(tOaArchivesDirectory.DIRECTORY_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DIRECTORY_NAME = '{0}'", tOaArchivesDirectory.DIRECTORY_NAME.ToString()));
                }
                //父目录ID，如果为根目录，则存储“0”
                if (!String.IsNullOrEmpty(tOaArchivesDirectory.PARENT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PARENT_ID = '{0}'", tOaArchivesDirectory.PARENT_ID.ToString()));
                }
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tOaArchivesDirectory.IS_USE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_USE = '{0}'", tOaArchivesDirectory.IS_USE.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tOaArchivesDirectory.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tOaArchivesDirectory.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaArchivesDirectory.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaArchivesDirectory.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaArchivesDirectory.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaArchivesDirectory.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaArchivesDirectory.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaArchivesDirectory.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tOaArchivesDirectory.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaArchivesDirectory.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tOaArchivesDirectory.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaArchivesDirectory.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
