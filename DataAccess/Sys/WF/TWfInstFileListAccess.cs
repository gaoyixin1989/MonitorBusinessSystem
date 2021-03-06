using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.WF;
using i3.ValueObject;

namespace i3.DataAccess.Sys.WF
{
    /// <summary>
    /// 功能：工作流实例附件明细表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstFileListAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstFileList">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstFileListVo tWfInstFileList)
        {
            string strSQL = "select Count(*) from T_WF_INST_FILE_LIST " + this.BuildWhereStatement(tWfInstFileList);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstFileListVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_WF_INST_FILE_LIST  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TWfInstFileListVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstFileList">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstFileListVo Details(TWfInstFileListVo tWfInstFileList)
        {
            string strSQL = String.Format("select * from  T_WF_INST_FILE_LIST " + this.BuildWhereStatement(tWfInstFileList));
            return SqlHelper.ExecuteObject(new TWfInstFileListVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstFileList">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstFileListVo> SelectByObject(TWfInstFileListVo tWfInstFileList, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_WF_INST_FILE_LIST " + this.BuildWhereStatement(tWfInstFileList));
            return SqlHelper.ExecuteObjectList(tWfInstFileList, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstFileList">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstFileListVo tWfInstFileList, int iIndex, int iCount)
        {

            string strSQL = " select * from T_WF_INST_FILE_LIST {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tWfInstFileList));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstFileList"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstFileListVo tWfInstFileList)
        {
            string strSQL = "select * from T_WF_INST_FILE_LIST " + this.BuildWhereStatement(tWfInstFileList);
            if (!string.IsNullOrEmpty(tWfInstFileList.SORT_FIELD))
            {
                strSQL += (" ORDER BY " + tWfInstFileList.SORT_FIELD + (tWfInstFileList.SORT_TYPE == "" ? " ASC " : (" " + tWfInstFileList.SORT_TYPE)));
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstFileList">对象</param>
        /// <returns></returns>
        public TWfInstFileListVo SelectByObject(TWfInstFileListVo tWfInstFileList)
        {
            string strSQL = "select * from T_WF_INST_FILE_LIST " + this.BuildWhereStatement(tWfInstFileList);
            return SqlHelper.ExecuteObject(new TWfInstFileListVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tWfInstFileList">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstFileListVo tWfInstFileList)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tWfInstFileList, TWfInstFileListVo.T_WF_INST_FILE_LIST_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstFileList">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstFileListVo tWfInstFileList)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstFileList, TWfInstFileListVo.T_WF_INST_FILE_LIST_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tWfInstFileList.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstFileList_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstFileList_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstFileListVo tWfInstFileList_UpdateSet, TWfInstFileListVo tWfInstFileList_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tWfInstFileList_UpdateSet, TWfInstFileListVo.T_WF_INST_FILE_LIST_TABLE);
            strSQL += this.BuildWhereStatement(tWfInstFileList_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_WF_INST_FILE_LIST where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TWfInstFileListVo tWfInstFileList)
        {
            string strSQL = "delete from T_WF_INST_FILE_LIST ";
            strSQL += this.BuildWhereStatement(tWfInstFileList);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tWfInstFileList"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TWfInstFileListVo tWfInstFileList)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tWfInstFileList)
            {

                //编号
                if (!String.IsNullOrEmpty(tWfInstFileList.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tWfInstFileList.ID.ToString()));
                }
                //流程实例编号
                if (!String.IsNullOrEmpty(tWfInstFileList.WF_INST_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_INST_ID = '{0}'", tWfInstFileList.WF_INST_ID.ToString()));
                }
                //流程编号
                if (!String.IsNullOrEmpty(tWfInstFileList.WF_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_ID = '{0}'", tWfInstFileList.WF_ID.ToString()));
                }
                //流水号
                if (!String.IsNullOrEmpty(tWfInstFileList.WF_SERIAL_NO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_SERIAL_NO = '{0}'", tWfInstFileList.WF_SERIAL_NO.ToString()));
                }
                //文件全路径
                if (!String.IsNullOrEmpty(tWfInstFileList.WF_FILE_FULLNAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_FILE_FULLNAME = '{0}'", tWfInstFileList.WF_FILE_FULLNAME.ToString()));
                }
                //文件名称
                if (!String.IsNullOrEmpty(tWfInstFileList.WF_FILE_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_FILE_NAME = '{0}'", tWfInstFileList.WF_FILE_NAME.ToString()));
                }
                //上传用户
                if (!String.IsNullOrEmpty(tWfInstFileList.UPLOAD_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPLOAD_USER = '{0}'", tWfInstFileList.UPLOAD_USER.ToString()));
                }
                //上传用户
                if (!String.IsNullOrEmpty(tWfInstFileList.UPLOAD_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UPLOAD_TIME = '{0}'", tWfInstFileList.UPLOAD_TIME.ToString()));
                }
                //文件图标
                if (!String.IsNullOrEmpty(tWfInstFileList.WF_FILE_ICO.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WF_FILE_ICO = '{0}'", tWfInstFileList.WF_FILE_ICO.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tWfInstFileList.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tWfInstFileList.IS_DEL.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
