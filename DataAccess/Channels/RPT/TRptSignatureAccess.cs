using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.RPT;
using i3.ValueObject;
using System.Data.SqlClient;

namespace i3.DataAccess.Channels.RPT
{
    /// <summary>
    /// 功能：印章表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptSignatureAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptSignatureVo tRptSignature)
        {
            string strSQL = "select Count(*) from T_RPT_SIGNATURE " + this.BuildWhereStatement(tRptSignature);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptSignatureVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_RPT_SIGNATURE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TRptSignatureVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptSignature">对象条件</param>
        /// <returns>对象</returns>
        public TRptSignatureVo Details(TRptSignatureVo tRptSignature)
        {
            string strSQL = String.Format("select * from  T_RPT_SIGNATURE " + this.BuildWhereStatement(tRptSignature));
            return SqlHelper.ExecuteObject(new TRptSignatureVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptSignatureVo> SelectByObject(TRptSignatureVo tRptSignature, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_RPT_SIGNATURE " + this.BuildWhereStatement(tRptSignature));
            return SqlHelper.ExecuteObjectList(tRptSignature, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptSignatureVo tRptSignature, int iIndex, int iCount)
        {

            string strSQL = " select * from T_RPT_SIGNATURE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tRptSignature));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptSignature"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptSignatureVo tRptSignature)
        {
            string strSQL = "select * from T_RPT_SIGNATURE " + this.BuildWhereStatement(tRptSignature);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <returns></returns>
        public TRptSignatureVo SelectByObject(TRptSignatureVo tRptSignature)
        {
            string strSQL = "select * from T_RPT_SIGNATURE " + this.BuildWhereStatement(tRptSignature);
            return SqlHelper.ExecuteObject(new TRptSignatureVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptSignatureVo tRptSignature)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tRptSignature, TRptSignatureVo.T_RPT_SIGNATURE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptSignature">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptSignatureVo tRptSignature)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptSignature, TRptSignatureVo.T_RPT_SIGNATURE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tRptSignature.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptSignature_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tRptSignature_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptSignatureVo tRptSignature_UpdateSet, TRptSignatureVo tRptSignature_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tRptSignature_UpdateSet, TRptSignatureVo.T_RPT_SIGNATURE_TABLE);
            strSQL += this.BuildWhereStatement(tRptSignature_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_RPT_SIGNATURE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TRptSignatureVo tRptSignature)
        {
            string strSQL = "delete from T_RPT_SIGNATURE ";
            strSQL += this.BuildWhereStatement(tRptSignature);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <param name="strDept">部门</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountByDept(TRptSignatureVo tRptSignature, string strDeptID)
        {
            string strSQL = " select count(*) from T_SYS_USER where IS_DEL='0' and IS_USE='1' and IS_HIDE='0' and ID in " +
                                " (select USER_ID from T_SYS_USER_POST where POST_ID in" +
                                " (select ID from T_SYS_POST where 1=1 {0}))";
            //当部门ID为空时查询所有的  Modify By Castle（胡方扬） 2012-11-20                    
            if (!String.IsNullOrEmpty(strDeptID))
            {
                strSQL = String.Format(strSQL, " AND POST_DEPT_ID='" + strDeptID + "'");
            }
            else
            {
                strSQL = string.Format(strSQL, "");
            }
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <param name="strDept">部门</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableByDept(TRptSignatureVo tRptSignature, string strDeptID, int iIndex, int iCount)
        {
            string strSQL = "select u.REAL_NAME,u.USER_NAME as USER_ID,s.* from " +
                                            " (select * from T_SYS_USER where IS_DEL='0' and IS_USE='1' and IS_HIDE='0' and ID in " +
                                            " (select USER_ID from T_SYS_USER_POST where POST_ID in" +
                                            " (select ID from T_SYS_POST where 1=1 {0}))) u" +
                                            " left join (select * from T_RPT_SIGNATURE " + BuildWhereStatement(tRptSignature) + ") s on s.USER_NAME=u.USER_NAME";
            //当部门ID为空时查询所有的  Modify By Castle（胡方扬） 2012-11-20                    
            if (!String.IsNullOrEmpty(strDeptID))
            {
                strSQL = String.Format(strSQL, " AND POST_DEPT_ID='" + strDeptID + "'");
            }
            else
            {
                strSQL = string.Format(strSQL, "");
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region WebOffice
        /// <summary>
        /// 调取所有的印章
        /// </summary>
        /// <param name="MarkList">印章列表</param>
        /// <returns>是否成功</returns>
        public bool LoadMarkList(out string MarkList)
        {
            bool bolResult = false;
            string strSQL = " select mark_name from T_RPT_SIGNATURE ";
            StringBuilder objMark = new StringBuilder();
            objConnection = new SqlConnection(strConnection);
            objCommand = new SqlCommand(strSQL, objConnection);

            try
            {
                objConnection.Open();
                objReader = objCommand.ExecuteReader();

                while (objReader.Read())
                {
                    objMark.Append(objReader["mark_name"] + "\r\n");
                }
            }
            catch (Exception ex)
            {
                Tips.Append(ex.Message);
            }
            finally
            {
                if (objMark.Length > 0)
                {
                    //转化值对象
                    MarkList = objMark.ToString();
                    bolResult = true;
                }
                else
                {
                    MarkList = String.Empty;
                }

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                objReader.Dispose();
            }

            return bolResult;
        }

        /// <summary>
        /// 根据用户名和密码调取印章
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="Mark_Body">文件流</param>
        /// <param name="Mark_Type">文件类型</param>
        /// <returns>是否成功</returns>
        public bool LoadMarkImage(string UserName, string PassWord, out byte[] Mark_Body, out string Mark_Type)
        {
            bool bolResult = false;
            Mark_Body = null;
            Mark_Type = String.Empty;
            string strSQL = " select mark_body,mark_type from T_RPT_SIGNATURE where user_name='{0}' and pass_word='{1}' ";
            strSQL = String.Format(strSQL, UserName, PassWord);
            objConnection = new SqlConnection(strConnection);
            objCommand = new SqlCommand(strSQL, objConnection);

            try
            {
                objConnection.Open();
                objReader = objCommand.ExecuteReader();

                if (objReader.Read())
                {
                    Mark_Body = objReader.GetSqlBinary(0).Value;
                    Mark_Type = objReader.GetString(1).ToString();

                    bolResult = true;
                }
            }
            catch (Exception ex)
            {
                Tips.Append(ex.Message);
            }
            finally
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                objReader.Dispose();
            }

            return bolResult;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tRptSignature"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TRptSignatureVo tRptSignature)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tRptSignature)
            {

                //ID
                if (!String.IsNullOrEmpty(tRptSignature.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tRptSignature.ID.ToString()));
                }
                //印章名称
                if (!String.IsNullOrEmpty(tRptSignature.MARK_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MARK_NAME = '{0}'", tRptSignature.MARK_NAME.ToString()));
                }
                //印章文件
                //if (!String.IsNullOrEmpty(tRptSignature.MARK_BODY.ToString().Trim()))
                //{
                //    strWhereStatement.Append(string.Format(" AND MARK_BODY = '{0}'", tRptSignature.MARK_BODY.ToString()));
                //}
                //文件类型
                if (!String.IsNullOrEmpty(tRptSignature.MARK_TYPE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MARK_TYPE = '{0}'", tRptSignature.MARK_TYPE.ToString()));
                }
                //文件路径
                if (!String.IsNullOrEmpty(tRptSignature.MARK_PATH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MARK_PATH = '{0}'", tRptSignature.MARK_PATH.ToString()));
                }
                //文件大小
                if (!String.IsNullOrEmpty(tRptSignature.MARK_SIZE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MARK_SIZE = '{0}'", tRptSignature.MARK_SIZE.ToString()));
                }
                //用户名
                if (!String.IsNullOrEmpty(tRptSignature.USER_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND USER_NAME = '{0}'", tRptSignature.USER_NAME.ToString()));
                }
                //用户密码
                if (!String.IsNullOrEmpty(tRptSignature.PASS_WORD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PASS_WORD = '{0}'", tRptSignature.PASS_WORD.ToString()));
                }
                //添加日期
                if (!String.IsNullOrEmpty(tRptSignature.ADD_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_TIME = '{0}'", tRptSignature.ADD_TIME.ToString()));
                }
                //添加IP
                if (!String.IsNullOrEmpty(tRptSignature.ADD_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_USER = '{0}'", tRptSignature.ADD_USER.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tRptSignature.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tRptSignature.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tRptSignature.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tRptSignature.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tRptSignature.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tRptSignature.REMARK3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
