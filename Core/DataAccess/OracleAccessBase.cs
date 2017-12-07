using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Generic;

using i3.Core.ValueObject;

namespace i3.Core.DataAccess
{
    /// <summary>
    /// 功能描述：Oracle数据库访问基类
    /// 创建日期：2011-4-6 20:39:17
    /// 创建人  ：陈国迎
    /// </summary>
    public class OracleAccessBase : IDisposable
    {
        //数据库操作对象
        public OracleCommand objCommand;
        public OracleTransaction objTransaction;
        public OracleConnection objConnection;
        public OracleDataAdapter objAdapter;
        public OracleDataReader objReader;

        public string strConnection { get; set; }
        public static string strConnectionStatic { get; set; }

        //是否开启事务
        public bool bolTrans             = false;

        //异常信息提示
        public static StringBuilder Tips = new StringBuilder();

        public OracleAccessBase()
        {
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        #region 数据访问对象初始化
        /// <summary>
        /// 数据访问对象初始化
        /// </summary>
        public void DataInit()
        {
            if (objConnection == null)
            {
                objConnection = new OracleConnection(strConnection);
            }

            if (objAdapter == null)
            {
                objAdapter = new OracleDataAdapter();
            }
        }
        #endregion

        #region 获得数据页数
        /// <summary>
        /// 获得数据页数
        /// </summary>
        /// <param name="intPageSize">页面记录灵长</param>
        /// <param name="strTableName">表名称</param>
        /// <returns>页数</returns>
        public int GetDataPageCount(int intPageSize, string strTableName)
        {
            string strSQL = "select count(*) from " + strTableName + "";

            int intTemp = Convert.ToInt32(OracleHelper.ExecuteScalar(strConnection, CommandType.Text, strSQL));

            if (intTemp != 0)
            {
                intTemp = intTemp / intPageSize + 1;
            }

            return intTemp;
        }
        #endregion

        #region 获得分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页面记录数</param>
        /// <param name="strTableName">表名</param>
        /// <param name="strOrderField">排序字段</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataWithPage(int intPageIndex, int intPageSize, string strTableName, string strOrderField)
        {
            if (strOrderField == null || strOrderField == String.Empty || strOrderField == "")
            {
                strOrderField = "ID";
            }
            
            int intTemp = intPageSize * intPageIndex;

            string strSQL = "SELECT TOP " + intPageSize + " * FROM " + strTableName + " WHERE (ID NOT IN (SELECT TOP " + intTemp + " ID FROM " + strTableName + " ORDER BY " + strOrderField + ")) ORDER BY " + strOrderField + "";

            return OracleHelper.ExecuteDataSet(strConnection, CommandType.Text, strSQL).Tables[0];
        }
        #endregion

        #region 释放资源
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="bolDispose">是否释放</param>
        protected virtual void Dispose(bool bolDispose)
        {
            if (!bolDispose)
            {
                return;
            }

            if (objCommand != null)
            {
                try
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion

        #region 连接数据库
        /// <summary>
        /// 连接数据库
        /// </summary>
        public void OpenConnection()
        {
            objConnection = new OracleConnection(strConnection);

            if (objConnection.State == ConnectionState.Closed)
            {
                try
                {
                    objConnection.Open();
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion

        #region 开始事务
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginProcess()
        {
            try
            {
                OpenConnection();
                objTransaction = objConnection.BeginTransaction();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交事务
        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitProcess()
        {
            try
            {
                objTransaction.Commit();
                CloseConnection();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 事务回滚
        /// <summary>
        ///  事务回滚
        /// </summary>
        public void ProcessError()
        {
            try
            {
                objTransaction.Rollback();
                CloseConnection();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 释放连接
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseConnection()
        {
            if (objConnection != null)
            {
                try
                {
                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();

                    }

                    objConnection.Dispose();
                    objConnection = null;
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion

        #region 命令准备工作(重截，简化版)
        /// <summary>
        /// 命令准备工作(重截，简化版)
        /// </summary>
        /// <param name="objCommand">命令</param>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        protected void PrepareCommand(OracleCommand objCommand, CommandType strCommandType, string strCommandText)
        {
            if (objConnection.State != ConnectionState.Open)
            {
                objConnection.Open();
            }

            objCommand.Connection = objConnection;
            objCommand.CommandText = strCommandText;

            objCommand.CommandType = strCommandType;
        }
        #endregion

        #region 获得表的最大ID+1数
        /// <summary>
        /// 获得表最大ID+1数
        /// </summary>
        /// <param name="strTableName">表名称</param>
        /// <returns>最大值+1</returns>
        public string GetMaxID(string strTableName, string strFieldName)
        {
            string strSQL = "select nvl(max(ceil({1}))+1,1) from {0}";
            strSQL = String.Format(strSQL, strTableName, strFieldName);

            return OracleHelper.ExecuteScalar(this.strConnection, CommandType.Text, strSQL).ToString();
        }
        #endregion

        #region 获得表的数据行数
        /// <summary>
        /// 获得表的行数
        /// </summary>
        /// <param name="strTableName">表名称</param>
        public string GetRowCount(string strTableName)
        {
            string strSQL = "select nvl(count(*),0) from {0}";
            strSQL = String.Format(strSQL, strTableName);

            return OracleHelper.ExecuteScalar(this.strConnection, CommandType.Text, strSQL).ToString();
        }

        /// <summary>
        /// 获得表的行数
        /// </summary>
        /// <param name="strTableName">表名称</param>
        /// <param name="strWhereExpress">限定条件（无，直接传空即可）</param>
        public string GetRowCount(string strTableName, string strWhereExpress)
        {
            string strSQL = "select nvl(count(*),0) from {0} {1}";
            strSQL = String.Format(strSQL, strTableName, strWhereExpress);

            return OracleHelper.ExecuteScalar(this.strConnection, CommandType.Text, strSQL).ToString();
        }
        #endregion

        #region 组建分页语句
        /// <summary>
        /// 组建分页语句
        /// 备注：当页码和每页数量都为0时，不产生分页语句
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">每页记录数</param>
        /// <returns></returns>
        public static string BuildPagerExpress(string strSQL, int intPageIndex, int intPageSize)
        {
            if (intPageIndex != 0 && intPageSize != 0)
            {
                string strSqlExpress = "select * from( {0} ) where rowno>({1}-1)*{2} and rowno<={3}*{4} ";
                strSqlExpress = String.Format(strSqlExpress, strSQL, intPageIndex, intPageSize, intPageIndex, intPageSize);

                return strSqlExpress;
            }
            else
            {
                return strSQL;
            }
        }
        /// <summary>
        /// 组建分页语句
        /// 备注：
        ///     1、当页码和每页数量都为0时，不产生分页语句；
        ///     2、如果SQL语句存在倒序排列，则必须调用此方法；
        /// </summary>
        /// <param name="vo">值对象</param>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">每页记录数</param>
        /// <returns></returns>
        public static string BuildPagerExpress<T>(T vo, string strSQL, int intPageIndex, int intPageSize) where T : ObjectBase
        {
            if (null != vo && vo.SORT_TYPE == ConstValues.SortType.DESC && !String.IsNullOrEmpty(vo.SORT_FIELD))
            {
                if (intPageIndex != 0 && intPageSize != 0)
                {
                    string strSqlExpress = "select * from ( select rownum rowno,tblIn.* from( {0} ) tblIn ) where rowno>({1}-1)*{2} and rowno<={3}*{4} ";
                    strSqlExpress = String.Format(strSqlExpress, strSQL, intPageIndex, intPageSize, intPageIndex, intPageSize);

                    return strSqlExpress;
                }
                else
                {
                    return strSQL;
                }
            }
            else
            {
                if (intPageIndex != 0 && intPageSize != 0)
                {
                    string strSqlExpress = "select * from( {0} ) where rowno>({1}-1)*{2} and rowno<={3}*{4} ";
                    strSqlExpress = String.Format(strSqlExpress, strSQL, intPageIndex, intPageSize, intPageIndex, intPageSize);

                    return strSqlExpress;
                }
                else
                {
                    return strSQL;
                }
            }

        }
        #endregion

        #region 动态组建Update语句
        /// <summary>
        /// 动态组建Update语句
        /// 注：属性类型必须是string；
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="strDataTable">表名称</param>
        /// <returns>Update语句，除Where限定条件</returns>
        public static string BuildUpdateExpress<T>(T obj, string strDataTable, params string[] ArrayParams) where T : ObjectBase
        {
            string strSQL = " update {0} {1} ";
            string strSet = " set ";
            bool bolHaveUpdate = false;
            Type type = obj.GetType();
            PropertyInfo[] propertys = type.GetProperties();

            foreach (PropertyInfo property in propertys)
            {
                if (property.Name != ConstValues.VoSortInfo.SORT_FIELD && property.Name != ConstValues.VoSortInfo.SORT_TYPE)
                {
                    if (property.PropertyType.Name == "String")
                    {
                        if (property.GetValue(obj, null) != null)
                        {
                            if (!String.IsNullOrEmpty(property.GetValue(obj, null).ToString()))
                            {
                                bool bDateColumnComplete = false;
                                //如果更新字段值为"###"，则自动替换为空，解决无法将字符更新为空的情况
                                if (property.GetValue(obj, null).ToString() == ConstValues.SpecialCharacter.EmptyValuesFillChar)
                                {
                                    strSet += property.Name + " = '' ,";
                                    bolHaveUpdate = true;
                                    continue;
                                }

                                for (int i = 0; i < ArrayParams.Length; i++)
                                {
                                    if (property.Name.ToUpper().Trim() == ArrayParams[i])
                                    {
                                        strSet += property.Name + " = to_date('" + property.GetValue(obj, null).ToString() + "','yyyy-MM-dd HH24:MI:SS'),";
                                        bolHaveUpdate = true;
                                        bDateColumnComplete = true;
                                        break;
                                    }
                                }
                                if (!bDateColumnComplete)
                                {
                                    strSet += property.Name + " = '" + property.GetValue(obj, null).ToString() + "' ,";
                                    bolHaveUpdate = true;
                                }
                            }
                        }
                    }
                }
            }

            if (bolHaveUpdate)
            {
                //截取最后的逗号
                strSet = strSet.Substring(0, strSet.Length - 1);
                strSQL = String.Format(strSQL, strDataTable, strSet);
            }

            return strSQL;
        }

        #endregion

        #region 动态组建Insert语句
        /// <summary>
        /// 动态组建Insert语句
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="strDataTable">表名称</param>
        /// <returns>Insert语句</returns>
        public static string BuildInsertExpress<T>(T obj, string strDataTable, params string[] ArrayParams) where T : ObjectBase
        {
            string strSQL = " insert into {0} ({1}) values({2}) ";
            string strFields = "";
            string strValues = "";
            bool bolHaveInsert = false;
            Type type = obj.GetType();
            PropertyInfo[] propertys = type.GetProperties();

            foreach (PropertyInfo property in propertys)
            {
                //属性为字符串，且不为排序类型列时，生成插入信息
                if (property.PropertyType.Name == "String" && property.Name != "SORT_TYPE")
                {
                    if (property.GetValue(obj, null) != null)
                    {
                        if (!String.IsNullOrEmpty(property.GetValue(obj, null).ToString()))
                        {
                            bool bDateColumnComplete = false;

                            //如果更新字段值为"###"，则自动替换为空，解决无法将字符更新为空的情况
                            if (property.GetValue(obj, null).ToString() == ConstValues.SpecialCharacter.EmptyValuesFillChar)
                            {
                                strFields += property.Name + ",";
                                strValues += "'',";
                                continue;
                            }

                            for (int i = 0; i < ArrayParams.Length; i++)
                            {
                                if (property.Name.ToUpper().Trim() == ArrayParams[i])
                                {
                                    strFields += property.Name + ",";
                                    strValues += "to_date('" + property.GetValue(obj, null).ToString() + "','yyyy-MM-dd HH24:MI:SS'),";
                                    bolHaveInsert = true;
                                    bDateColumnComplete = true;
                                    break;
                                }
                            }
                            if (!bDateColumnComplete)
                            {
                                strFields += property.Name + ",";
                                strValues += "'" + property.GetValue(obj, null).ToString() + "',";
                                bolHaveInsert = true;
                            }
                        }
                    }
                }
            }

            if (bolHaveInsert)
            {
                //截取最后的逗号
                strFields = strFields.Substring(0, strFields.Length - 1);
                strValues = strValues.Substring(0, strValues.Length - 1);

                strSQL = String.Format(strSQL, strDataTable, strFields, strValues);
            }

            return strSQL;
        }

        #endregion

        #region 获取指定类型的序号
        /// <summary>
        /// 功能描述：获取序号
        /// 创建人：　陈国迎
        /// 创建日期：2007-1-22
        /// </summary>
        /// <param name="strSerialType">类型</param>
        /// <returns>序号</returns>
        public string GetSerialNumber(string strSerialType)
        {
            string strSQL = "COMMON.P_GetSerialNumber";
            string strResult = "";

            objConnection = new OracleConnection(strConnection);
            objCommand = new OracleCommand(strSQL, objConnection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.Add("serial_type_in", OracleType.VarChar, 30);
            objCommand.Parameters.Add("serial_out", OracleType.VarChar, 128);

            objCommand.Parameters["serial_type_in"].Direction = ParameterDirection.Input;
            objCommand.Parameters["serial_out"].Direction = ParameterDirection.Output;
            objCommand.Parameters["serial_type_in"].Value = strSerialType;

            try
            {
                objConnection.Open();
                objCommand.ExecuteNonQuery();
                strResult = objCommand.Parameters["serial_out"].Value.ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                objConnection.Close();
                objCommand.Dispose();
            }

            return strResult;
        }
        #endregion
    }
}
