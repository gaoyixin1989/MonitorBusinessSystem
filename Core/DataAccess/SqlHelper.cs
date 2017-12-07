using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Data.Common;
using i3.Core.DataAccess;

namespace i3.Core.DataAccess
{
    /// <summary>
    /// 功能描述：SQL Server数据访问封装
    /// 创建　人：陈国迎
    /// 创建日期：2008-1-2
    /// </summary>
    public abstract class SqlHelper : SqlAccessBase
    {
        /// <summary>
        /// 设备数据库连接字符串，包含二个：strConnection、strConnectionStatic
        /// </summary>
        public abstract void SetConnectionString();

        #region 命令准备
        /// <summary>
        /// 命令准备
        /// </summary>
        /// <param name="objCommand">命令</param>
        /// <param name="objConnection">连接</param>
        /// <param name="objTransaction">事务</param>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令明细</param>
        private static void PrepareCommand(SqlCommand objCommand, SqlConnection objConnection, SqlTransaction objTransaction, CommandType strCommandType, string strCommandText)
        {
            if (objConnection.State != ConnectionState.Open)
            {
                objConnection.Open();
            }

            objCommand.Connection = objConnection;
            objCommand.CommandText = strCommandText;

            if (objTransaction != null)
            {
                objCommand.Transaction = objTransaction;
            }

            objCommand.CommandType = strCommandType;
        }
        #endregion

        #region 执行无参数SQL语句或者存储过程，返回影响行数
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回影响行数
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strConnection">连接字符串</param>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string strConnection, CommandType strCommandType, string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand();
            int intReturn = -1;

            using (SqlConnection objConnection = new SqlConnection(strConnection))
            {
                PrepareCommand(objCommand, objConnection, null, strCommandType, strCommandText);

                try
                {
                    intReturn = objCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Tips.AppendLine(ex.Message);
                }
                finally
                {
                    objCommand.Dispose();
                    objConnection.Close();
                    objConnection.Dispose();
                }

                return intReturn;
            }
        }
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回影响行数
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(CommandType strCommandType, string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand();
            int intReturn = -1;

            using (SqlConnection objConnection = new SqlConnection(strConnectionStatic))
            {
                PrepareCommand(objCommand, objConnection, null, strCommandType, strCommandText);

                try
                {
                    intReturn = objCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Tips.AppendLine(ex.Message);
                }
                finally
                {
                    objCommand.Dispose();
                    objConnection.Close();
                    objConnection.Dispose();
                }

                return intReturn;
            }
        }
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回影响行数
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand();
            int intReturn = -1;

            using (SqlConnection objConnection = new SqlConnection(strConnectionStatic))
            {
                PrepareCommand(objCommand, objConnection, null, CommandType.Text, strCommandText);

                try
                {
                    intReturn = objCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Tips.AppendLine(ex.Message);
                }
                finally
                {
                    objCommand.Dispose();
                    objConnection.Close();
                    objConnection.Dispose();
                }
                return intReturn;
            }
        }
        #endregion
        #region 执行无参数SQL语句或者存储过程，返回DataReader
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回DataReader
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strConnection">连接字符串</param>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>DataReader</returns>
        public static SqlDataReader ExecuteReader(string strConnection, CommandType strCommandType, string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlConnection objConnection = new SqlConnection(strConnection);
            SqlDataReader objDataReader = null;

            try
            {
                PrepareCommand(objCommand, objConnection, null, strCommandType, strCommandText);
                objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objDataReader.Dispose();
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }

            return objDataReader;
        }
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回DataReader
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>DataReader</returns>
        public static SqlDataReader ExecuteReader(CommandType strCommandType, string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlConnection objConnection = new SqlConnection(strConnectionStatic);
            SqlDataReader objDataReader = null;

            try
            {
                PrepareCommand(objCommand, objConnection, null, strCommandType, strCommandText);
                objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objDataReader.Dispose();
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }

            return objDataReader;
        }
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回DataReader
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>DataReader</returns>
        public static SqlDataReader ExecuteReader(string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlConnection objConnection = new SqlConnection(strConnectionStatic);
            SqlDataReader objDataReader = null;

            try
            {
                PrepareCommand(objCommand, objConnection, null, CommandType.Text, strCommandText);
                objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objDataReader.Dispose();
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }

            return objDataReader;
        }

        #endregion
        #region 执行无参数SQL语句或者存储过程，返回DatatSet
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回DatatSet
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strConnection">连接字符串</param>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string strConnection, CommandType strCommandType, string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand(strCommandText);
            objCommand.CommandType = strCommandType;
            SqlConnection objConnection = new SqlConnection(strConnection);
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            DataSet objDataSet = new DataSet();

            try
            {
                PrepareCommand(objCommand, objConnection, null, strCommandType, strCommandText);

                objAdapter.Fill(objDataSet);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objAdapter.Dispose();
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }

            return objDataSet;
        }
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回DatatSet
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(CommandType strCommandType, string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand(strCommandText);
            objCommand.CommandType = strCommandType;
            SqlConnection objConnection = new SqlConnection(strConnectionStatic);
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            DataSet objDataSet = new DataSet();

            try
            {
                PrepareCommand(objCommand, objConnection, null, strCommandType, strCommandText);

                objAdapter.Fill(objDataSet);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objAdapter.Dispose();
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }

            return objDataSet;
        }
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回DatatSet
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand(strCommandText);
            objCommand.CommandType = CommandType.Text;
            SqlConnection objConnection = new SqlConnection(strConnectionStatic);
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            DataSet objDataSet = new DataSet();

            try
            {
                PrepareCommand(objCommand, objConnection, null, CommandType.Text, strCommandText);

                objAdapter.Fill(objDataSet);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objAdapter.Dispose();
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }

            return objDataSet;
        }

        #endregion
        #region 执行无参数SQL语句或者存储过程，返回DataTable
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回DatatSet
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strConnection">连接字符串</param>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(string strConnection, CommandType strCommandType, string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand(strCommandText);
            objCommand.CommandType = strCommandType;
            SqlConnection objConnection = new SqlConnection(strConnection);
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            DataTable objTable = new DataTable();

            try
            {
                PrepareCommand(objCommand, objConnection, null, strCommandType, strCommandText);

                objAdapter.Fill(objTable);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objAdapter.Dispose();
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }

            return objTable;
        }

        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回DatatSet
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(CommandType strCommandType, string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand(strCommandText);
            objCommand.CommandType = strCommandType;
            SqlConnection objConnection = new SqlConnection(strConnectionStatic);
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            DataTable objTable = new DataTable();

            try
            {
                PrepareCommand(objCommand, objConnection, null, strCommandType, strCommandText);

                objAdapter.Fill(objTable);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objAdapter.Dispose();
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }

            return objTable;
        }

        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回DataTable
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand(strCommandText);
            objCommand.CommandType = CommandType.Text;
            SqlConnection objConnection = new SqlConnection(strConnectionStatic);
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            DataTable objTable = new DataTable();

            try
            {
                PrepareCommand(objCommand, objConnection, null, CommandType.Text, strCommandText);

                objAdapter.Fill(objTable);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objAdapter.Dispose();
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }

            return objTable;
        }
        #endregion
        #region 执行无参数SQL语句或者存储过程，返回Scalar值
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回Scalar值；
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strConnection">连接字符串</param>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>Object</returns>
        public static object ExecuteScalar(string strConnection, CommandType strCommandType, string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand();
            object intReturn = null;

            using (SqlConnection objConnection = new SqlConnection(strConnection))
            {
                PrepareCommand(objCommand, objConnection, null, strCommandType, strCommandText);

                try
                {
                    intReturn = objCommand.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    Tips.AppendLine(ex.Message);
                }
                finally
                {
                    objCommand.Dispose();
                    objConnection.Close();
                    objConnection.Dispose();
                }

                return intReturn;
            }
        }
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回Scalar值；
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>Object</returns>
        public static object ExecuteScalar(CommandType strCommandType, string strCommandText)
        {
            SqlCommand objCommand = new SqlCommand();
            object intReturn = null;

            using (SqlConnection objConnection = new SqlConnection(strConnectionStatic))
            {
                PrepareCommand(objCommand, objConnection, null, strCommandType, strCommandText);

                try
                {
                    intReturn = objCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Tips.AppendLine(ex.Message);
                }
                finally
                {
                    objCommand.Dispose();
                    objConnection.Close();
                    objConnection.Dispose();
                }

                return intReturn;
            }
        }
        /// <summary>
        /// 功能描述：执行无参数SQL语句或者存储过程，返回Scalar值；
        /// 创建人：　陈国迎
        /// 创建日期：2006-5-15
        /// </summary>
        /// <param name="strCommandText">命令文本</param>
        /// <returns>Object</returns>
        public static object ExecuteScalar(string strCommandText)
        {
            //解决SQL的COUNT语句无法进行ORDER BY的问题
            if (strCommandText.Contains("count(*)") && strCommandText.Contains("order by"))
            {
                strCommandText = strCommandText.Substring(0, strCommandText.IndexOf("order by"));
            }

            SqlCommand objCommand = new SqlCommand();
            object intReturn = null;

            using (SqlConnection objConnection = new SqlConnection(strConnectionStatic))
            {
                PrepareCommand(objCommand, objConnection, null, CommandType.Text, strCommandText);

                try
                {
                    intReturn = objCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Tips.AppendLine(ex.Message);
                }
                finally
                {
                    objCommand.Dispose();
                    objConnection.Close();
                    objConnection.Dispose();
                }

                return intReturn;
            }
        }
        #endregion
        #region 执行无参数SQL语句或者存储过程，返回泛型对象
        public static T ExecuteObject<T>(T obj, string strConnection, CommandType strCommandType, string strCommandText)
        {
            DataTable objTable = ExecuteDataTable(strConnection, strCommandType, strCommandText);
            AutoTransformer ope = new AutoTransformer();

            return ope.ConvertTableToOjbect(objTable, obj);
        }
        public static T ExecuteObject<T>(T obj, CommandType strCommandType, string strCommandText)
        {
            DataTable objTable = ExecuteDataTable(strCommandType, strCommandText);
            AutoTransformer ope = new AutoTransformer();

            return ope.ConvertTableToOjbect(objTable, obj);
        }
        public static T ExecuteObject<T>(T obj, string strCommandText)
        {
            DataTable objTable = ExecuteDataTable(strCommandText);
            AutoTransformer ope = new AutoTransformer();

            return ope.ConvertTableToOjbect(objTable, obj);
        }
        #endregion
        #region 执行无参数SQL语句或者存储过程，返回泛型对象的List
        public static List<T> ExecuteObjectList<T>(T obj, string strConnection, CommandType strCommandType, string strCommandText)
        {
            DataTable objTable = ExecuteDataTable(strConnection, strCommandType, strCommandText);
            AutoTransformer ope = new AutoTransformer();

            return ope.ConvertTableToOjbectList(objTable, obj);
        }
        public static List<T> ExecuteObjectList<T>(T obj, CommandType strCommandType, string strCommandText)
        {
            DataTable objTable = ExecuteDataTable(strCommandType, strCommandText);
            AutoTransformer ope = new AutoTransformer();

            return ope.ConvertTableToOjbectList(objTable, obj);
        }
        public static List<T> ExecuteObjectList<T>(T obj, string strCommandText)
        {
            DataTable objTable = ExecuteDataTable(strCommandText);
            AutoTransformer ope = new AutoTransformer();

            return ope.ConvertTableToOjbectList(objTable, obj);
        }
        #endregion
        #region 执行无参数SQL语句 列表或者存储过程列表，返回执行结果
        /// <summary>
        /// 功能描述：执行无参数SQL语句列表或者存储过程，返回执行结果
        /// 创建人：　石磊
        /// 创建日期：2008-01-04
        /// </summary>
        /// <param name="strConnection">连接字符串</param>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本列表</param>
        /// <returns>返回执行结果</returns>
        public static bool ExecuteSQLTextList(string strConnection, CommandType strCommandType, string[] strCommandTextList)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = strCommandType;
            SqlConnection objConnection = new SqlConnection(strConnection);

            if (objConnection.State != ConnectionState.Open)
            {
                objConnection.Open();
            }
            SqlTransaction objTransaction = objConnection.BeginTransaction();
            objCommand.Connection = objConnection;

            try
            {
                ExecuteListCommand(objCommand, objConnection, objTransaction, strCommandType, strCommandTextList);
                objTransaction.Commit();
            }
            catch (Exception ex)
            {
                objTransaction.Rollback();
                Tips.AppendLine(ex.Message);
                return false;
            }
            finally
            {
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }
            return true;
        }
        /// <summary>
        /// 功能描述：执行无参数SQL语句列表或者存储过程，返回执行结果
        /// 创建人：　石磊
        /// 创建日期：2008-01-04
        /// </summary>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本列表</param>
        /// <returns>返回执行结果</returns>
        public static bool ExecuteSQLTextList(CommandType strCommandType, string[] strCommandTextList)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = strCommandType;
            SqlConnection objConnection = new SqlConnection(strConnectionStatic);

            if (objConnection.State != ConnectionState.Open)
            {
                objConnection.Open();
            }
            SqlTransaction objTransaction = objConnection.BeginTransaction();
            try
            {
                ExecuteListCommand(objCommand, objConnection, objTransaction, strCommandType, strCommandTextList);
                objTransaction.Commit();
            }
            catch (Exception ex)
            {
                objTransaction.Rollback();
                Tips.AppendLine(ex.Message);
                return false;
            }
            finally
            {
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }
            return true;

        }
        /// <summary>
        /// 功能描述：执行无参数SQL语句列表或者存储过程，返回执行结果
        /// 创建人：　石磊
        /// 创建日期：2008-01-04
        /// </summary>
        /// <param name="strCommandText">命令文本列表</param>
        /// <returns>返回执行结果</returns>
        public static bool ExecuteSQLTextList(string[] strCommandTextList)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.Text;
            SqlConnection objConnection = new SqlConnection(strConnectionStatic);

            if (objConnection.State != ConnectionState.Open)
            {
                objConnection.Open();
            }
            SqlTransaction objTransaction = objConnection.BeginTransaction();
            try
            {
                ExecuteListCommand(objCommand, objConnection, objTransaction, CommandType.Text, strCommandTextList);
                objTransaction.Commit();
            }
            catch (Exception ex)
            {
                objTransaction.Rollback();
                Tips.AppendLine(ex.Message);
                return false;
            }
            finally
            {
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }
            return true;
        }
        /// <summary>
        /// 针对 SQL 列表的命令准备函数
        /// </summary>
        /// <param name="objCommand">执行命令</param>
        /// <param name="objConnection">数据库连接</param>
        /// <param name="objTransaction">事务</param>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令体列表</param>
        private static void ExecuteListCommand(SqlCommand objCommand, SqlConnection objConnection, SqlTransaction objTransaction, CommandType strCommandType, string[] strCommandTextList)
        {
            objCommand.CommandType = strCommandType;
            objCommand.Connection = objConnection;

            if (objTransaction != null)
            {
                objCommand.Transaction = objTransaction;
            }

            foreach (string strCommandText in strCommandTextList)
            {
                //石磊添加 对于null 和 ""的判断规则
                if (null == strCommandText || strCommandText.Trim() == "")
                    continue;
                objCommand.CommandText = strCommandText;
                objCommand.ExecuteNonQuery();
            }
        }
        #endregion
        #region 执行SQL存储过程，返回string类型
        /// <summary>
        /// 执行SQL存储过程，返回string类型
        /// </summary>
        /// <param name="StoreName">存储过程名称</param>
        /// <param name="Parameters">参数集</param>
        /// <returns></returns>
        public static string ExecuteStored(string StoreName, string[] paramNames, DbType[] paramTypes, int[] paramSizes, ParameterDirection[] pd, object[] InputparamValues)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlConnection objConnection = new SqlConnection(strConnectionStatic);
            string OutParameterName = "";
            string ReturnValue = "";
            try
            {
                for (int i = 0; i < paramNames.Length; ++i)
                {
                    DbParameter p = objCommand.CreateParameter();
                    p.ParameterName = "@" + paramNames[i].Replace("@", "");
                    p.Direction = pd[i];
                    p.Size = paramSizes[i];
                    p.DbType = paramTypes[i];
                    p.Value = InputparamValues[i];
                    objCommand.Parameters.Add(p);
                    if (pd[i] == ParameterDirection.Output) OutParameterName = p.ParameterName;
                }
                PrepareCommand(objCommand, objConnection, null, CommandType.StoredProcedure, StoreName);
                objCommand.ExecuteNonQuery();
                ReturnValue = objCommand.Parameters[OutParameterName].Value.ToString();
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }
            return ReturnValue;
        }
        #endregion
        #region 执行SQL语句事务

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="strSql">一个或多个要执行的sql语句</param>
        /// <returns></returns>
        public static bool ExecuteSQLByTransaction(ArrayList strSql)
        {
            if (strSql.Count < 1)
                return true; 
            SqlCommand objCommand = new SqlCommand();
            SqlTransaction objTransaction;
            using (SqlConnection objConnection = new SqlConnection(strConnectionStatic))
            {
                if (objConnection.State != ConnectionState.Open)
                    objConnection.Open();
                objTransaction = objConnection.BeginTransaction();
                try
                {
                    foreach (string str in strSql)
                    {
                        PrepareCommand(objCommand, objConnection, objTransaction, CommandType.Text, str);
                        objCommand.ExecuteNonQuery();
                    }
                    objTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Tips.AppendLine(ex.Message);
                    objTransaction.Rollback();
                    return false;
                }
                finally
                {
                    objCommand.Dispose();
                    objConnection.Close();
                    objConnection.Dispose();
                    objTransaction.Dispose();
                }
            }
        }


        /// <summary>
        /// 功能描述：执行指定连接的事务
        /// 创建人：　吕晓林
        /// 创建日期：2011-05-025
        /// </summary>
        /// <param name="strConnection">连接字符串</param>
        /// <param name="strCommandType">命令类型</param>
        /// <param name="strCommandText">命令文本列表</param>
        /// <returns>返回执行结果</returns>
        public static bool ExecuteSQLByTransaction(string strConnection, CommandType strCommandType, ArrayList strSql)
        {
            if (strSql.Count < 1)
                return true;
            SqlCommand objCommand = new SqlCommand();
            SqlConnection objConnection = new SqlConnection(strConnection);

            if (objConnection.State != ConnectionState.Open)
            {
                objConnection.Open();
            }
            SqlTransaction objTransaction = objConnection.BeginTransaction();

            try
            {
                foreach (string str in strSql)
                {
                    PrepareCommand(objCommand, objConnection, objTransaction, CommandType.Text, str);
                    objCommand.ExecuteNonQuery();
                }
                objTransaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                objTransaction.Rollback();
                Tips.AppendLine(ex.Message);
                return false;
            }
            finally
            {
                objCommand.Dispose();
                objConnection.Close();
                objConnection.Dispose();
            }
        }

        #endregion
        #region 执行存储过程，返回DataSet

        /// <summary>
        /// 执行存储过程，返回DataSet
        /// </summary>
        /// <param name="comText">存储过程名</param>
        /// <param name="para">参数</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string comText, SqlParameter[] para)
        {
            SqlConnection con = new SqlConnection(strConnectionStatic);
            SqlCommand com = new SqlCommand(comText, con);
            com.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter p in para)
            {
                com.Parameters.Add(p);
            }
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
        #endregion

        #region//执行SQL语句
        /// <summary>
        /// 执行事务（刘静楠 2013/7/15）
        /// </summary>
        /// <param name="strSql">一个或多个要执行的sql语句</param>
        /// <returns></returns>
        public static string  ExecuteListSQLByTransaction(ArrayList strSql)
        {
            string Error = string.Empty;
            if (strSql.Count < 1)
                return Error="传入的集合为空";
            SqlCommand objCommand = new SqlCommand();
            SqlTransaction objTransaction;
            using (SqlConnection objConnection = new SqlConnection(strConnectionStatic))
            {
                if (objConnection.State != ConnectionState.Open)
                    objConnection.Open();
                objTransaction = objConnection.BeginTransaction();
                try
                {
                    foreach (string str in strSql)
                    {
                        PrepareCommand(objCommand, objConnection, objTransaction, CommandType.Text, str);
                        objCommand.ExecuteNonQuery();
                    }
                    objTransaction.Commit();
                    return Error;
                }
                catch (Exception ex)
                {
                    Tips.AppendLine(ex.Message);
                    objTransaction.Rollback();
                    return Error = ex.Message;
                }
                finally
                {
                    objCommand.Dispose();
                    objConnection.Close();
                    objConnection.Dispose();
                    objTransaction.Dispose();
                }
            }
        }
        #endregion

    }
}
