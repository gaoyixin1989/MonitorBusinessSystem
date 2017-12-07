using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Data.OleDb;

namespace i3.Core.DataAccess
{
    /// <summary>
    /// 功能描述：Oracle数据访问封装
    /// 创建　人：陈国迎
    /// 创建日期：2011-4-6 20:49:36
    /// </summary>
    public abstract class OracleHelper : OracleAccessBase
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
        private static void PrepareCommand(OracleCommand objCommand, OracleConnection objConnection, OracleTransaction objTransaction, CommandType strCommandType, string strCommandText)
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
            OracleCommand objCommand = new OracleCommand();
            int intReturn = -1;

            using (OracleConnection objConnection = new OracleConnection(strConnection))
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
            OracleCommand objCommand = new OracleCommand();
            int intReturn = -1;

            using (OracleConnection objConnection = new OracleConnection(strConnectionStatic))
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
            OracleCommand objCommand = new OracleCommand();
            int intReturn = -1;

            using (OracleConnection objConnection = new OracleConnection(strConnectionStatic))
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

        /// <summary>
        /// 执行存储过程，返回影响行数
        /// </summary>
        /// <param name="OracleCmd"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(OracleCommand OracleCmd)
        {
            int intReturn = -1;
            using (OracleConnection objConnection = new OracleConnection(strConnectionStatic))
            {
                try
                {
                    if (objConnection.State != ConnectionState.Open) objConnection.Open();
                    OracleCmd.Connection = objConnection;
                    intReturn = OracleCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Tips.AppendLine(ex.Message);
                }
                finally
                {
                    objConnection.Close();
                    OracleCmd.Dispose();
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
        public static OracleDataReader ExecuteReader(string strConnection, CommandType strCommandType, string strCommandText)
        {
            OracleCommand objCommand = new OracleCommand();
            OracleConnection objConnection = new OracleConnection(strConnection);
            OracleDataReader objDataReader = null;

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
        public static OracleDataReader ExecuteReader(CommandType strCommandType, string strCommandText)
        {
            OracleCommand objCommand = new OracleCommand();
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);
            OracleDataReader objDataReader = null;

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
        public static OracleDataReader ExecuteReader(string strCommandText)
        {
            OracleCommand objCommand = new OracleCommand();
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);
            OracleDataReader objDataReader = null;

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
            OracleCommand objCommand = new OracleCommand(strCommandText);
            objCommand.CommandType = strCommandType;
            OracleConnection objConnection = new OracleConnection(strConnection);
            OracleDataAdapter objAdapter = new OracleDataAdapter(objCommand);
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
            OracleCommand objCommand = new OracleCommand(strCommandText);
            objCommand.CommandType = strCommandType;
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);
            OracleDataAdapter objAdapter = new OracleDataAdapter(objCommand);
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
            OracleCommand objCommand = new OracleCommand(strCommandText);
            objCommand.CommandType = CommandType.Text;
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);
            OracleDataAdapter objAdapter = new OracleDataAdapter(objCommand);
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


        /// <summary>
        /// 执行存储过程，返回DataSet
        /// </summary>
        /// <param name="OracleCmd"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(OracleCommand OracleCmd)
        {
            DataSet objDataSet = new DataSet();
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);
            OracleDataAdapter objAdapter = new OracleDataAdapter();
            try
            {
                if (objConnection.State != ConnectionState.Open) objConnection.Open();
                OracleCmd.Connection = objConnection;
                objAdapter.SelectCommand = OracleCmd;
                objAdapter.Fill(objDataSet);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objConnection.Close();
                objAdapter.Dispose();
                OracleCmd.Dispose();
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
            OracleCommand objCommand = new OracleCommand(strCommandText);
            objCommand.CommandType = strCommandType;
            OracleConnection objConnection = new OracleConnection(strConnection);
            OracleDataAdapter objAdapter = new OracleDataAdapter(objCommand);
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
        /// 执行存储过程，返回DataTable
        /// </summary>
        /// <param name="OracleCmd"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(OracleCommand OracleCmd)
        {
            DataTable objTable = new DataTable();
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);
            OracleDataAdapter objAdapter = new OracleDataAdapter();
            try
            {
                if (objConnection.State != ConnectionState.Open) objConnection.Open();
                OracleCmd.Connection = objConnection;
                objAdapter.SelectCommand = OracleCmd;
                objAdapter.Fill(objTable);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
            }
            finally
            {
                objConnection.Close();
                objAdapter.Dispose();
                OracleCmd.Dispose();
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
            OracleCommand objCommand = new OracleCommand(strCommandText);
            objCommand.CommandType = strCommandType;
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);
            OracleDataAdapter objAdapter = new OracleDataAdapter(objCommand);
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
            OracleCommand objCommand = new OracleCommand(strCommandText);
            objCommand.CommandType = CommandType.Text;
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);
            OracleDataAdapter objAdapter = new OracleDataAdapter(objCommand);
            DataTable objTable = new DataTable();

            try
            {
                PrepareCommand(objCommand, objConnection, null, CommandType.Text, strCommandText);

                objAdapter.Fill(objTable);
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
                objTable.TableName = ex.Message;
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
            OracleCommand objCommand = new OracleCommand();
            object intReturn = null;

            using (OracleConnection objConnection = new OracleConnection(strConnection))
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
            OracleCommand objCommand = new OracleCommand();
            object intReturn = null;

            using (OracleConnection objConnection = new OracleConnection(strConnectionStatic))
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

            OracleCommand objCommand = new OracleCommand();
            object intReturn = null;

            using (OracleConnection objConnection = new OracleConnection(strConnectionStatic))
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
            OracleCommand objCommand = new OracleCommand();
            objCommand.CommandType = strCommandType;
            OracleConnection objConnection = new OracleConnection(strConnection);

            if (objConnection.State != ConnectionState.Open)
            {
                objConnection.Open();
            }
            OracleTransaction objTransaction = objConnection.BeginTransaction();
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
            OracleCommand objCommand = new OracleCommand();
            objCommand.CommandType = strCommandType;
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);

            if (objConnection.State != ConnectionState.Open)
            {
                objConnection.Open();
            }
            OracleTransaction objTransaction = objConnection.BeginTransaction();
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
            OracleCommand objCommand = new OracleCommand();
            objCommand.CommandType = CommandType.Text;
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);

            if (objConnection.State != ConnectionState.Open)
            {
                objConnection.Open();
            }
            OracleTransaction objTransaction = objConnection.BeginTransaction();
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
        private static void ExecuteListCommand(OracleCommand objCommand, OracleConnection objConnection, OracleTransaction objTransaction, CommandType strCommandType, string[] strCommandTextList)
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


        /// <summary>
        /// 执行存储过程,返回执行结果,无需connection
        /// </summary>
        /// <param name="OracleCmd">执行命令</param>
        /// <returns></returns>
        public static bool ExecuteProcedure(OracleCommand OracleCmd)
        {
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);
            try
            {
                if (objConnection.State != ConnectionState.Open) objConnection.Open();
                OracleCmd.Connection = objConnection;
                OracleCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Tips.AppendLine(ex.Message);
                return false;
            }
            finally
            {
                objConnection.Close();
                OracleCmd.Dispose();
                objConnection.Dispose();
            }
            return true;
        }

        /// <summary>
        /// 执行存储过程更新表单数据，返回执行结果
        /// </summary>
        /// <param name="OracleCmd">执行命令</param>
        /// <param name="dt">要更新的数据表单</param>
        /// <returns></returns>
        public static bool ExecuteProcedureDataTable(OracleCommand OracleCmd, DataTable dt)
        {
            OracleConnection objConnection = new OracleConnection(strConnectionStatic);
            OracleDataAdapter objadapter = null;
            if (OracleCmd == null)
            {
                return false;
            }
            else
            {
                OracleCmd.Connection = objConnection;
            }

            if (objadapter == null)
            {
                objadapter = new OracleDataAdapter();
            }
            objadapter.InsertCommand = OracleCmd;
            try
            {
                objadapter.Update(dt);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            OracleCommand objCommand = new OracleCommand();
            OracleTransaction objTransaction;
            using (OracleConnection objConnection = new OracleConnection(strConnectionStatic))
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
            OracleCommand objCommand = new OracleCommand();
            OracleConnection objConnection = new OracleConnection(strConnection);

            if (objConnection.State != ConnectionState.Open)
            {
                objConnection.Open();
            }
            OracleTransaction objTransaction = objConnection.BeginTransaction();

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
    }
}
