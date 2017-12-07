
/*
��飺�����ȡ���ݵ���
����ʱ�䣺2002-10
����޸�ʱ�䣺2004-2-1 ccb

 ˵����
  �ڴ��ļ��ִ�����4�ַ�ʽ�����ӡ�
  1�� sql server .
  2�� oracle.
  3�� ole.
  4,  odbc.
  
*/
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Data.OracleClient ; 
using System.EnterpriseServices;
using System.Data.OleDb;
using System.Web;
using System.Data.Odbc ; 
using System.IO;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Properties;
using IBM.Data;
using IBM.Data.Informix;
using IBM.Data.Utilities;
using BP.Sys;

namespace BP.DA
{
	/// <summary>
	/// ���ݿ���ʡ�
	/// ����ฺ������ ʵ����Ϣ
	/// </summary>
    public class DBAccess
    {
        #region �ļ��洢���ݿ⴦��.
        /// <summary>
        /// �����ļ������ݿ�
        /// </summary>
        /// <param name="fullFileName">��ɵ��ļ�·��</param>
        /// <param name="tableName">������</param>
        /// <param name="tablePK">������</param>
        /// <param name="pkVal">����ֵ</param>
        /// <param name="saveFileField">���浽�ֶ�</param>
        public static void SaveFileToDB(string fullFileName, string tableName, string tablePK, string pkVal, string saveFileField)
        {
        }
        /// <summary>
        /// �����ݿ�����ȡ�ļ�
        /// </summary>
        /// <param name="fullFilePath">Ҫ�洢���ļ�·��</param>
        /// <param name="tableName">����</param>
        /// <param name="tablePK">������</param>
        /// <param name="pkVal">����ֵ</param>
        /// <param name="fileSaveField">�ֶ�</param>
        public static void GetFileFromDB(string fullFileName, string tableName, string tablePK, string pkVal, string fileSaveField)
        {

        }
        #endregion �ļ��洢���ݿ⴦��.


        #region ������
        /// <summary>
        /// ִ������һ������
        /// </summary>
        public static void DoTransactionBegin()
        {
            return;

            //if (SystemConfig.AppCenterDBType != DBType.MSSQL)
            //    return;
            //if (BP.Web.WebUser.No == null)
            //    return;
            //SqlConnection conn = new SqlConnection(SystemConfig.AppCenterDSN);
            //BP.DA.Cash.SetConn(BP.Web.WebUser.No, conn);
            //DBAccess.RunSQL("BEGIN TRANSACTION");
        }
        /// <summary>
        /// �ع�����
        /// </summary>
        public static void DoTransactionRollback()
        {
            return;

            //if (SystemConfig.AppCenterDBType != DBType.MSSQL)
            //    return;

            //if (BP.Web.WebUser.No == null)
            //    return;

            //DBAccess.RunSQL("Rollback TRANSACTION");
            //SqlConnection conn = BP.DA.Cash.GetConn(BP.Web.WebUser.No) as SqlConnection;
            //conn.Close();
            //conn.Dispose();
        }
        /// <summary>
        /// �ύ����
        /// </summary>
        public static void DoTransactionCommit()
        {
            return;

            if (SystemConfig.AppCenterDBType != DBType.MSSQL)
                return;

            if (BP.Web.WebUser.No == null)
                return;

            DBAccess.RunSQL("Commit TRANSACTION");
        }
        #endregion ������

        public static Paras DealParasBySQL(string sql, Paras ps)
        {
            Paras myps = new Paras();
            foreach (Para p in ps)
            {
                if (sql.Contains(":" + p.ParaName) == false)
                    continue;
                myps.Add(p);
            } 
            return myps;
        }

        #region IO
        public static void copyDirectory(string Src, string Dst)
        {
            String[] Files;
            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                Dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
            Files = Directory.GetFileSystemEntries(Src);
            foreach (string Element in Files)
            {
                //   Sub   directories   
                if (Directory.Exists(Element))
                    copyDirectory(Element, Dst + Path.GetFileName(Element));
                //   Files   in   directory   
                else
                    File.Copy(Element, Dst + Path.GetFileName(Element), true);
            }
        }
        #endregion

        #region ��ȡXml

        #endregion

        #region �������д洢����

        #region ִ�д洢���̷���Ӱ�����
        public static int RunSP(string spName, string paraKey, object paraVal)
        {
            Paras pas = new Paras();
            pas.Add(paraKey, paraVal);
            return DBAccess.RunSP(spName, pas);
        }
        /// <summary>
        /// ���д洢����
        /// </summary>
        /// <param name="spName">����</param>
        /// <returns>����Ӱ�������</returns>
        public static int RunSP(string spName)
        {
            int i = 0;
            switch (BP.Sys.SystemConfig.AppCenterDBType)
            {
                case DBType.MSSQL:
                case DBType.Access:
                    return DBProcedure.RunSP(spName, (SqlConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Oracle:
                    return DBProcedure.RunSP(spName, (OracleConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Informix:
                    return DBProcedure.RunSP(spName, (IfxConnection)DBAccess.GetAppCenterDBConn);
                default:
                    throw new Exception("Error: " + BP.Sys.SystemConfig.AppCenterDBType);
            }
        }
        public static int RunSPReturnInt(string spName)
        {
            switch (BP.Sys.SystemConfig.AppCenterDBType)
            {
                case DBType.MSSQL:
                    return DBProcedure.RunSP(spName, (SqlConnection)DBAccess.GetAppCenterDBConn);
                case DBType.MySQL:
                    return DBProcedure.RunSP(spName, (MySqlConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Informix:
                    return DBProcedure.RunSP(spName, (IfxConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Access:
                case DBType.Oracle:
                    return DBProcedure.RunSP(spName, (OracleConnection)DBAccess.GetAppCenterDBConn);
                default:
                    throw new Exception("Error: " + BP.Sys.SystemConfig.AppCenterDBType);
            }
        }
         
        /// <summary>
        /// ���д洢����
        /// </summary>
        /// <param name="spName">����</param>
        /// <param name="paras">����</param>
        /// <returns>����Ӱ�������</returns>
        public static int RunSP(string spName, Paras paras)
        {
            int i = 0;
            switch (BP.Sys.SystemConfig.AppCenterDBType)
            {
                case DBType.MSSQL:
                   return DBProcedure.RunSP(spName, paras, (SqlConnection)DBAccess.GetAppCenterDBConn);
                case DBType.MySQL:
                case DBType.Access:
                  // return DBProcedure.RunSP(spName, paras, new MySqlConnection(SystemConfig.AppCenterDSN));
                   throw new Exception("@û��ʵ��...");
                case DBType.Oracle:
                   return DBProcedure.RunSP(spName, paras, (OracleConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Informix:
                   return DBProcedure.RunSP(spName, paras, (IfxConnection)DBAccess.GetAppCenterDBConn);
                default:
                    throw new Exception("Error " + BP.Sys.SystemConfig.AppCenterDBType);
            }
        }
        #endregion

        #region ���д洢���̷��� DataTable
        /// <summary>
        /// ���д洢����
        /// </summary>
        /// <param name="spName">����</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSPReTable(string spName)
        {
            switch (BP.Sys.SystemConfig.AppCenterDBType)
            {
                case DBType.MSSQL:
                case DBType.Access:
                    return DBProcedure.RunSPReturnDataTable(spName, (SqlConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Oracle:
                    return DBProcedure.RunSPReturnDataTable(spName, (OracleConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Informix:
                    return DBProcedure.RunSPReturnDataTable(spName, (IfxConnection)DBAccess.GetAppCenterDBConn);
                default:
                    throw new Exception("Error " + BP.Sys.SystemConfig.AppCenterDBType);

            }
        }
        /// <summary>
        /// ���д洢����
        /// </summary>
        /// <param name="spName">����</param>
        /// <param name="paras">����</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSPReTable(string spName, Paras paras)
        {
            switch (BP.Sys.SystemConfig.AppCenterDBType)
            {
                case DBType.MSSQL:
                    return DBProcedure.RunSPReturnDataTable(spName, paras, new SqlConnection(SystemConfig.AppCenterDSN));
                case DBType.Oracle:
                    return DBProcedure.RunSPReturnDataTable(spName, paras, new OracleConnection(SystemConfig.AppCenterDSN));
                case DBType.Informix:
                    return DBProcedure.RunSPReturnDataTable(spName, paras, new IfxConnection(SystemConfig.AppCenterDSN));
                case DBType.Access:
                default:
                    throw new Exception("Error " + BP.Sys.SystemConfig.AppCenterDBType);
            }
        }
        #endregion

        #endregion

        //���캯��
        static DBAccess()
        {
            CurrentSys_Serial = new Hashtable();
            KeyLockState = new Hashtable();
        }

        #region �����ж���ı���
        public static readonly Hashtable CurrentSys_Serial;
        private static int readCount = -1;
        private static readonly Hashtable KeyLockState;
        #endregion


        #region �������к��뷽��
        /// <summary>
        /// ���ݱ�ʶ���������к�
        /// </summary>
        /// <param name="type">OID</param>
        /// <returns></returns>
        public static int GenerSequenceNumber(string type)
        {
            if (readCount == -1)  //ϵͳ��һ������ʱ
            {
                DataTable tb = DBAccess.RunSQLReturnTable("SELECT CfgKey, IntVal FROM Sys_Serial ");
                foreach (DataRow row in tb.Rows)
                {
                    string str = row[0].ToString().Trim();
                    int id = Convert.ToInt32(row[1]);
                    try
                    {
                        CurrentSys_Serial.Add(str, id);
                        KeyLockState.Add(row[0].ToString().Trim(), false);
                    }
                    catch
                    {
                    }
                }
                readCount++;
            }
            if (CurrentSys_Serial.ContainsKey(type) == false)
            {
                DBAccess.RunSQL("insert into Sys_Serial values('" + type + "',1 )");
                return 1;
            }

            while (true)
            {
                while (!(bool)KeyLockState[type])
                {
                    KeyLockState[type] = true;
                    int cur = (int)CurrentSys_Serial[type];
                    if (readCount++ % 10 == 0)
                    {
                        readCount = 1;
                        int n = (int)CurrentSys_Serial[type] + 10;

                        Paras ps = new Paras();
                        ps.Add("intVal", n);
                        ps.Add("CfgKey", type);

                        string upd = "update Sys_Serial set intVal="+SystemConfig.AppCenterDBVarStr+"intVal WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
                        DBAccess.RunSQL(upd, ps);
                    }

                    cur++;
                    CurrentSys_Serial[type] = cur;
                    KeyLockState[type] = false;
                    return cur;
                }
            }
        }
        /// <summary>
        /// ���� GenerOIDByGUID.
        /// </summary>
        /// <returns></returns>
        public static int GenerOIDByGUID()
        {
            int i = BP.Tools.CRC32Helper.GetCRC32(Guid.NewGuid().ToString());
            if (i <= 0)
                i = -i;
            return i;
        }
        /// <summary>
        /// ���� GenerOIDByGUID.
        /// </summary>
        /// <returns></returns>
        public static int GenerOIDByGUID(string strs)
        {
            int i = BP.Tools.CRC32Helper.GetCRC32(strs);
            if (i <= 0)
                i = -i;
            return i;
        }
        /// <summary>
        /// ���� GenerGUID
        /// </summary>
        /// <returns></returns>
        public static string GenerGUID()
        {
            return Guid.NewGuid().ToString();
        }
        /// <summary>
        /// ����OID
        /// </summary>
        private static bool lock_OID = false;
        /// <summary>
        /// ����һ��OID
        /// </summary>
        /// <returns></returns>
        public static int GenerOID()
        {
            while (lock_OID == true)
            {
            }

            lock_OID = true;
            if (DBAccess.RunSQL("UPDATE Sys_Serial SET IntVal=IntVal+1 WHERE CfgKey='OID'") == 0)
                DBAccess.RunSQL("INSERT INTO Sys_Serial (CfgKey,IntVal) VALUES ('OID',100)");
            int oid = DBAccess.RunSQLReturnValInt("SELECT  IntVal FROM Sys_Serial WHERE CfgKey='OID'");
            lock_OID = false;
            return oid;
        }
        /// <summary>
        /// ��
        /// </summary>
        private  static  bool lock_OID_CfgKey = false;
        /// <summary>
        /// ����Ψһ�����к�
        /// </summary>
        /// <param name="cfgKey">������Ϣ</param>
        /// <returns>Ψһ�����к�</returns>
        public static Int64 GenerOID_2013(string cfgKey)
        {
            while (lock_OID_CfgKey == true)
            {
            }
            lock_OID_CfgKey = true;

            Paras ps = new Paras();
            ps.Add("CfgKey", cfgKey);
            string sql = "UPDATE Sys_Serial SET IntVal=IntVal+1 WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql, ps);
            if (num == 0)
            {
                sql = "INSERT INTO Sys_Serial (CFGKEY,INTVAL) VALUES ('" + cfgKey + "',100)";
                DBAccess.RunSQL(sql);
                lock_OID_CfgKey = false;
                return 100;
            }
            sql = "SELECT IntVal FROM Sys_Serial WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            num = DBAccess.RunSQLReturnValInt(sql, ps);
            lock_OID_CfgKey = false;
            return num;
        }
        #region �ڶ��汾������ OID��
        /// <summary>
        /// ��
        /// </summary>
        private static bool lock_HT_CfgKey = false;
        private static Hashtable lock_HT = new Hashtable();
        /// <summary>
        /// ����Ψһ�����к�
        /// </summary>
        /// <param name="cfgKey">������Ϣ</param>
        /// <returns>Ψһ�����к�</returns>
        public static Int64 GenerOID(string cfgKey)
        {
            while (lock_HT_CfgKey == true)
            {
            }
            lock_HT_CfgKey = true;

            if (lock_HT.ContainsKey(cfgKey) == false)
            {
            }
            else
            {
            }

            Paras ps = new Paras();
            ps.Add("CfgKey", cfgKey);
            string sql = "UPDATE Sys_Serial SET IntVal=IntVal+1 WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql, ps);
            if (num == 0)
            {
                sql = "INSERT INTO Sys_Serial (CFGKEY,INTVAL) VALUES ('" + cfgKey + "',100)";
                DBAccess.RunSQL(sql);
                lock_HT_CfgKey = false;

                if (lock_HT.ContainsKey(cfgKey) == false)
                    lock_HT.Add(cfgKey, 200);

                return 100;
            }
            sql = "SELECT IntVal FROM Sys_Serial WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            num = DBAccess.RunSQLReturnValInt(sql, ps);
            lock_HT_CfgKey = false;
            return num;
        }
        #endregion �ڶ��汾������ OID��

        /// <summary>
        /// ��ȡһ����OID, ���µ�OID.
        /// ����: ���Ѿ���ȷ֪��Ҫ�õ�260��OID, 
        /// ����Ϊ�˱�����ȡ�����Ч���˷ѣ��Ϳ���һ����ȡ�� 260��OID.
        /// </summary>
        /// <param name="cfgKey"></param>
        /// <param name="getOIDNum">Ҫ��ȡ��OID����.</param>
        /// <returns>��OID</returns>
        public static Int64 GenerOID(string cfgKey, int getOIDNum)
        {
            Paras ps = new Paras();
            ps.Add("CfgKey", cfgKey);
            string sql = "UPDATE Sys_Serial SET IntVal=IntVal+" + getOIDNum + " WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql, ps);
            if (num == 0)
            {
                getOIDNum = getOIDNum + 100;
                sql = "INSERT INTO Sys_Serial (CFGKEY,INTVAL) VALUES (" + SystemConfig.AppCenterDBVarStr + "CfgKey," + getOIDNum + ")";
                DBAccess.RunSQL(sql, ps);
                return 100;
            }
            sql = "SELECT  IntVal FROM Sys_Serial WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            return DBAccess.RunSQLReturnValInt(sql, ps) - getOIDNum;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intKey"></param>
        /// <returns></returns>
        public static Int64 GenerOIDByKey64(string intKey)
        {
            Paras ps = new Paras();
            ps.Add("CfgKey", intKey);
            string sql = "";
            sql = "UPDATE Sys_Serial SET IntVal=IntVal+1 WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql, ps);
            if (num == 0)
            {
                sql = "INSERT INTO Sys_Serial (CFGKEY,INTVAL) VALUES (" + SystemConfig.AppCenterDBVarStr + "CfgKey,'1')";
                DBAccess.RunSQL(sql,ps);
                return Int64.Parse(intKey + "1");
            }
            sql = "SELECT IntVal FROM Sys_Serial WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int val = DBAccess.RunSQLReturnValInt(sql,ps);
            return Int64.Parse(intKey + val.ToString());
        }
        public static Int32 GenerOIDByKey32(string intKey)
        {
            Paras ps = new Paras();
            ps.Add("CfgKey", intKey);

            string sql = "";
            sql = "UPDATE Sys_Serial SET IntVal=IntVal+1 WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql, ps);
            if (num == 0)
            {
                sql = "INSERT INTO Sys_Serial (CFGKEY,INTVAL) VALUES (" + SystemConfig.AppCenterDBVarStr + "CfgKey,'100')";
                DBAccess.RunSQL(sql, ps);
                return int.Parse(intKey + "100");
            }
            sql = "SELECT IntVal FROM Sys_Serial WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int val = DBAccess.RunSQLReturnValInt(sql, ps);
            return int.Parse(intKey + val.ToString());
        }
        public static Int64 GenerOID(string table, string intKey)
        {
            Paras ps = new Paras();
            ps.Add("CfgKey", intKey);

            string sql = "";
            sql = "UPDATE " + table + " SET IntVal=IntVal+1 WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql,ps);
            if (num == 0)
            {
                sql = "INSERT INTO " + table + " (CFGKEY,INTVAL) VALUES (" + SystemConfig.AppCenterDBVarStr + "CfgKey,100)";
                DBAccess.RunSQL(sql,ps);
                return int.Parse(intKey + "100");
            }
            sql = "SELECT  IntVal FROM " + table + " WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int val = DBAccess.RunSQLReturnValInt(sql,ps);

            return Int64.Parse(intKey + val.ToString());
        }
        #endregion

        #region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
        /// <summary>
        /// AppCenterDBType
        /// </summary>
        public static DBType AppCenterDBType
        {
            get
            {
                return SystemConfig.AppCenterDBType;
            }
        }
        public static IDbConnection GetAppCenterDBConn
        {
            get
            {
                string connstr = BP.Sys.SystemConfig.AppCenterDSN;
                switch (AppCenterDBType)
                {
                    case DBType.MSSQL:
                        return new SqlConnection(connstr);
                    case DBType.Oracle:
                        return new OracleConnection(connstr);
                    case DBType.MySQL:
                        return new MySqlConnection(connstr);
                    case DBType.Informix:
                        return new IfxConnection(connstr);
                    case DBType.Access:
                    default:
                        throw new Exception("����δ֪�����ݿ��������ͣ�");
                }
            }
        }
        public static IDbDataAdapter GetAppCenterDBAdapter
        {
            get
            {
                switch (AppCenterDBType)
                {
                    case DBType.MSSQL:
                        return new SqlDataAdapter();
                    case DBType.Oracle:
                        return new OracleDataAdapter();
                    case DBType.MySQL:
                        return new MySqlDataAdapter();
                    case DBType.Informix:
                        return new IfxDataAdapter();
                    case DBType.Access:
                    default:
                        throw new Exception("����δ֪�����ݿ��������ͣ�");
                }
            }
        }
        public static IDbCommand GetAppCenterDBCommand
        {
            get
            {
                switch (AppCenterDBType)
                {
                    case DBType.MSSQL:
                        return new SqlCommand();
                    case DBType.Oracle:
                        return new OracleCommand();
                    case DBType.MySQL:
                        return new MySqlCommand();
                    case DBType.Informix:
                        return new IfxCommand();
                    case DBType.Access:
                    default:
                        throw new Exception("����δ֪�����ݿ��������ͣ�");
                }
            }
        }

        #endregion ȡ�����Ӷ��� ��CS��BS��������

        /// <summary>
        /// ͬһ��Connetionִ�ж���sql����DataSet
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public static DataSet RunSQLReturnDataSet(string sqls)
        {
            IDbConnection conn = GetAppCenterDBConn;
            IDbDataAdapter ada = GetAppCenterDBAdapter;
            try
            {
                IDbCommand cmd = GetAppCenterDBCommand;
                cmd.CommandText = sqls;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                if (ada != null)
                {
                    ada.SelectCommand = cmd;
                }

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                DataSet oratb = new DataSet();
                ada.Fill(oratb);
                return oratb;
            }
            catch (Exception ex)
            {
                throw new Exception("SQLs=" + sqls + " Exception=" + ex.Message);
            }
            finally
            {
                (ada as System.Data.Common.DbDataAdapter).Dispose();
                conn.Close();
            }
        }

        #region ���� SQL

        #region ��ָ����Connection��ִ�� SQL ��䣬������Ӱ�������

        #region OleDbConnection
        public static int RunSQLDropTable(string table)
        {
            if (IsExitsObject(table))
            {
                switch (AppCenterDBType)
                {
                    case DBType.Oracle:
                    case DBType.MSSQL:
                    case DBType.Informix:
                    case DBType.Access:
                        return RunSQL("DROP TABLE " + table);
                    default:
                        throw new Exception(" Exception ");
                }
            }
            return 0;

            /* return RunSQL("TRUNCATE TABLE " + table);*/

        }

        public static int RunSQL(string sql, OleDbConnection conn, string dsn)
        {
            return RunSQL(sql, conn, CommandType.Text, dsn);
        }
        public static int RunSQL(string sql, OleDbConnection conn, CommandType sqlType, string dsn, params object[] pars)
        {
            try
            {
                if (conn == null)
                    conn = new OleDbConnection(dsn);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = dsn;
                    conn.Open();
                }

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.CommandType = sqlType;
                int i = cmd.ExecuteNonQuery();

                //cmd.ExecuteReader();

                cmd.Dispose();
                conn.Close();

                //lock_SQL_RunSQL = false;
                return i;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + sql);
            }
        }
        #endregion

        #region SqlConnection
        /// <summary>
        /// ����SQL
        /// </summary>
       // private static bool lock_SQL_RunSQL = false;
        /// <summary>
        /// ����SQL, ����Ӱ�������.
        /// </summary>
        /// <param name="sql">msSQL</param>
        /// <param name="conn">SqlConnection</param>
        /// <returns>�������н����</returns>
        public static int RunSQL(string sql, SqlConnection conn, string dsn)
        {
            return RunSQL(sql, conn, CommandType.Text, dsn);
        }
        /// <summary>
        /// ����SQL , ����Ӱ�������.
        /// </summary>
        /// <param name="sql">msSQL</param>
        /// <param name="conn">SqlConnection</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">params</param>
        /// <returns>�������н��</returns>
        public static int RunSQL(string sql, SqlConnection conn, CommandType sqlType, string dsn)
        {
            conn.Close();
#if DEBUG
            Debug.WriteLine(sql);
#endif
            //���������״̬���͵ȴ�
            //while (lock_SQL_RunSQL)
            //    ;
            // ��ʼִ��.
            //lock_SQL_RunSQL = true; //����
            string step = "1";
            try
            {

                if (conn == null)
                    conn = new SqlConnection(dsn);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = dsn;
                    conn.Open();
                }

                step = "2";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = sqlType;
                step = "3";

                step = "4";
                int i = 0;
                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    step = "5";
                    //lock_SQL_RunSQL = false;
                    cmd.Dispose();
                    step = "6";
                    throw new Exception("RunSQL step=" + step + ex.Message + " SQL=" + sql);
                }
                step = "7";
                cmd.Dispose();
               // lock_SQL_RunSQL = false;
                return i;
            }
            catch (System.Exception ex)
            {
                step = "8";
               // lock_SQL_RunSQL = false;
                throw new Exception("RunSQL2 step=" + step + ex.Message + " ��������ʱ��=" + conn.ConnectionTimeout);
            }
            finally
            {
                step = "9";
                //lock_SQL_RunSQL = false;
                conn.Close();
            }
        }
        #endregion

        #region OracleConnection
        public static int RunSQL(string sql, OracleConnection conn, string dsn)
        {
            return RunSQL(sql, conn, CommandType.Text, dsn);
        }
        public static int RunSQL(string sql, OracleConnection conn, CommandType sqlType, string dsn)
        {
#if DEBUG
            Debug.WriteLine(sql);
#endif
            //���������״̬���͵ȴ�
           // while (lock_SQL_RunSQL)
              //  ;
            // ��ʼִ��.
           // lock_SQL_RunSQL = true; //����
            string step = "1";
            try
            {
                if (conn == null)
                    conn = new OracleConnection(dsn);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = dsn;
                    conn.Open();
                }

                step = "2";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = sqlType;
                step = "3";
                int i = 0;
                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    step = "5";
                   // lock_SQL_RunSQL = false;
                    cmd.Dispose();
                    step = "6";
                    throw new Exception("RunSQL step=" + step + ex.Message + " SQL=" + sql);
                }
                step = "7";
                cmd.Dispose();

                
                //lock_SQL_RunSQL = false;
                return i;
            }
            catch (System.Exception ex)
            {
                step = "8";
               // lock_SQL_RunSQL = false;
                throw new Exception("RunSQL2 step=" + step + ex.Message);
            }
            finally
            {
                step = "9";
               // lock_SQL_RunSQL = false;
                conn.Close();
            }

            /*
            Debug.WriteLine( sql );
            try
            {
                OracleCommand cmd = new OracleCommand( sql ,conn);
                cmd.CommandType = sqlType;
                foreach(object par in pars)
                {
                    cmd.Parameters.Add( "par",par);
                }
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
				
                int i= cmd.ExecuteNonQuery();				 
                cmd.Dispose();
                return i;				 
            }
            catch(System.Exception ex)
            {
                throw new Exception(ex.Message + sql );
            }
            finally
            {
                conn.Close();

            }
            */
        }
        #endregion

        #endregion

        #region ͨ����Ӧ�ó�����������������sql
        #region pk
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="tab">�����</param>
        /// <param name="pk">����</param>
        public static void CreatePK(string tab, string pk, DBType db)
        {
            string sql;
            switch (db)
            {
                case DBType.Informix:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT  PRIMARY KEY(" + pk + ") CONSTRAINT " + tab + "pk ";
                    break;
                default:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT " + tab + "pk PRIMARY KEY(" + pk.ToUpper() + ")";
                    break;
            }
            DBAccess.RunSQL(sql);
        }
        public static void CreatePK(string tab, string pk1, string pk2, DBType db)
        {
            string sql;
            switch (db)
            {
                case DBType.Informix:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT  PRIMARY KEY(" + pk1.ToUpper() + "," + pk2.ToUpper() + ") CONSTRAINT " + tab + "pk ";
                    break;
                default:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT " + tab + "pk  PRIMARY KEY(" + pk1.ToUpper() + "," + pk2.ToUpper() + ")";
                    break;
            }
            DBAccess.RunSQL(sql);
        }
        public static void CreatePK(string tab, string pk1, string pk2, string pk3, DBType db)
        {
            string sql;
            switch (db)
            {
                case DBType.Informix:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT  PRIMARY KEY(" + pk1.ToUpper() + "," + pk2.ToUpper() + ","+pk3.ToUpper()+") CONSTRAINT " + tab + "pk ";
                    break;
                default:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT " + tab + "pk PRIMARY KEY(" + pk1.ToUpper() + "," + pk2.ToUpper() + "," + pk3.ToUpper() + ")";
                    break;
            }
            DBAccess.RunSQL(sql);
        }
        #endregion


        #region index
        public static void CreatIndex(string table, string pk)
        {
            string sql = "";
            try
            {
                sql = "DROP INDEX " + table + "ID ON " + table;
                DBAccess.RunSQL(sql);
            }
            catch
            {
            }

            try
            {
                sql = "CREATE INDEX " + table + "ID ON " + table + " (" + pk + ")";
                DBAccess.RunSQL(sql);
            }
            catch
            {
            }
        }
        public static void CreatIndex(string table, string pk1, string pk2)
        {
            try
            {
                DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk1 + "," + pk2 + ")");
            }
            catch
            {
            }
        }
        public static void CreatIndex(string table, string pk1, string pk2, string pk3)
        {
            DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk1 + "," + pk2 + "," + pk3 + ")");
        }
        public static void CreatIndex(string table, string pk1, string pk2, string pk3, string pk4)
        {
            DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk1 + "," + pk2 + "," + pk3 + "," + pk4 + ")");
        }
        #endregion

        public static int CreatTableFromODBC(string selectSQL, string table, string pk)
        {
            DBAccess.RunSQLDropTable(table);
            string sql = "SELECT * INTO " + table + " FROM OPENROWSET('MSDASQL','" + SystemConfig.AppSettings["DBAccessOfODBC"] + "','" + selectSQL + "')";
            int i = DBAccess.RunSQL(sql);
            DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk + ")");
            return i;
        }
        public static int CreatTableFromODBC(string selectSQL, string table, string pk1, string pk2)
        {
            DBAccess.RunSQLDropTable(table);
            //DBAccess.RunSQL("DROP TABLE "+table);
            string sql = "SELECT * INTO " + table + " FROM OPENROWSET('MSDASQL','" + SystemConfig.AppSettings["DBAccessOfODBC"] + "','" + selectSQL + "')";
            int i = DBAccess.RunSQL(sql);
            DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk1 + "," + pk2 + ")");
            return i;
        }
        public static int CreatTableFromODBC(string selectSQL, string table, string pk1, string pk2, string pk3)
        {
            DBAccess.RunSQLDropTable(table);
            string sql = "SELECT * INTO " + table + " FROM OPENROWSET('MSDASQL','" + SystemConfig.AppSettings["DBAccessOfODBC"] + "','" + selectSQL + "')";
            int i = DBAccess.RunSQL(sql);
            DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk1 + "," + pk2 + "," + pk3 + ")");
            return i;
        }
        #endregion

        #region �ڵ�ǰ��Connectionִ�� SQL ��䣬������Ӱ�������
        public static int RunSQL(string sql, CommandType sqlType, string dsn, params object[] pars)
        {
            IDbConnection oconn = GetAppCenterDBConn;
            if (oconn is SqlConnection)
                return RunSQL(sql, (SqlConnection)oconn, sqlType, dsn);
            else if (oconn is OracleConnection)
                return RunSQL(sql, (OracleConnection)oconn, sqlType, dsn);
            else
                throw new Exception("��ȡ���ݿ�����[GetAppCenterDBConn]ʧ�ܣ�");
        }
        public static DataTable ReadProText(string proName)
        {
            string sql = "";
            switch (BP.Sys.SystemConfig.AppCenterDBType)
            {
                case DBType.Oracle:
                    sql = "SELECT text FROM user_source WHERE name=UPPER('" + proName + "') ORDER BY LINE ";
                    break;
                default:
                    sql = "SP_Help  " + proName;
                    break;
            }
            try
            {
                return BP.DA.DBAccess.RunSQLReturnTable(sql);
            }
            catch
            {
                sql = "select * from Port_Emp WHERE 1=2";
                return BP.DA.DBAccess.RunSQLReturnTable(sql);
            }
        }
        public static void RunSQLScript(string sqlOfScriptFilePath)
        {
            string str = BP.DA.DataType.ReadTextFile(sqlOfScriptFilePath);
            string[] strs = str.Split(';');
            foreach (string s in strs)
            {
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                    continue;

                if (s.Contains("--"))
                    continue;

                if (s.Contains("/*"))
                    continue;

                BP.DA.DBAccess.RunSQL(s);
            }
        }
        /// <summary>
        /// ִ�о���Go��sql �ı���
        /// </summary>
        /// <param name="sqlOfScriptFilePath"></param>
        public static void RunSQLScriptGo(string sqlOfScriptFilePath)
        {
            string str = BP.DA.DataType.ReadTextFile(sqlOfScriptFilePath);
            string[] strs = str.Split(new String[] { "--GO--" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in strs)
            {
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                    continue;

                if (s.Contains("/**"))
                    continue;

                string mysql = s.Replace("--GO--", "");
                if (string.IsNullOrEmpty(mysql.Trim()))
                    continue;
                if (s.Contains("--"))
                    continue;

                BP.DA.DBAccess.RunSQL(mysql);
            }
        }

        public static void RunSQLs(string sql)
        {
            if (string.IsNullOrEmpty(sql))
                return;

            sql = sql.Replace("@GO","~");
            sql = sql.Replace("@", "~");
            string[] strs = sql.Split('~');
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str))
                    continue;

                if (str.Contains("--") || str.Contains("/*"))
                    continue;

                RunSQL(str);
            }
        }
        /// <summary>
        /// ���д��в�����sql
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static int RunSQL(Paras ps)
        {
            return RunSQL(ps.SQL, ps);
        }
        /// <summary>
        /// ����sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int RunSQL(string sql)
        {
            if (sql == null || sql.Trim() == "")
                return 1;
            Paras ps = new Paras();
            ps.SQL = sql;
            return RunSQL(ps);
        }
        public static int RunSQL(string sql, string paraKey, object val)
        {
            Paras ens = new Paras();
            ens.Add(paraKey, val);
            return RunSQL(sql, ens);
        }
        public static int RunSQL(string sql, string paraKey1, object val1, string paraKey2, object val2)
        {
            Paras ens = new Paras();
            ens.Add(paraKey1, val1);
            ens.Add(paraKey2, val2);
            return RunSQL(sql, ens);
        }
        public static int RunSQL(string sql, string paraKey1, object val1, string paraKey2, object val2, string k3, object v3)
        {
            Paras ens = new Paras();
            ens.Add(paraKey1, val1);
            ens.Add(paraKey2, val2);
            ens.Add(k3, v3);
            return RunSQL(sql, ens);
        }
        /// <summary>
        /// 
        /// </summary>
        private static bool lockRunSQL = false;
        /// <summary>
        /// ִ��sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int RunSQL(string sql, Paras paras)
        {
            if (sql == null || sql.Trim() == "")
                return 1;

            while (lockRunSQL == true)
            {
            };
            lockRunSQL = true;

            int result = 0;
            try
            {
                switch (AppCenterDBType)
                {
                    case DBType.MSSQL:
                        result = RunSQL_200705_SQL(sql, paras);
                        break;
                    case DBType.Oracle:
                        result = RunSQL_200705_Ora(sql.Replace("]", "").Replace("[", ""), paras);
                        break;
                    case DBType.MySQL:
                        result = RunSQL_200705_MySQL(sql, paras);
                        break;
                    case DBType.Informix:
                        result = RunSQL_201205_Informix(sql, paras);
                        break;
                    case DBType.Access:
                        result = RunSQL_200705_OLE(sql, paras);
                        break;
                    default:
                        throw new Exception("����δ֪�����ݿ��������ͣ�");
                }
                lockRunSQL = false;
                return result;
            }
            catch (Exception ex)
            {
                lockRunSQL = false;
                string msg = "";
                string mysql = sql.Clone() as string;
                foreach (Para p in paras)
                {
                    msg += "@" + p.ParaName + "=" + p.val + "," + p.DAType.ToString();
                    mysql = mysql.Replace(":" + p.ParaName + ",", "'" + p.val + "',");
                }
                throw new Exception("ִ��sql����:" + ex.Message + " Paras(" + paras.Count + ")=" + msg + "<hr>" + mysql);
            }
        }

        /// <summary>
        /// ����sql���ؽ��
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="paras">����</param>
        /// <returns>ִ�еĽ��</returns>
        private static int RunSQL_200705_SQL(string sql, Paras paras)
        {
            SqlConnection conn = new SqlConnection(SystemConfig.AppCenterDSN);
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.ConnectionString = SystemConfig.AppCenterDSN;
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                //if (SystemConfig.IsEnableNull )
                //{
                //    /*�����ֵ���͵�����Ϊnull, ��Ҫ������ж�. */
                //    foreach (Para para in paras)
                //    {
                //        switch (para.DAType)
                //        {
                //            case DbType.Int32:
                //            case DbType.Decimal: 
                //            case DbType.Double:
                //                if (para.val ==)
                //                {
                //                    SqlParameter oraP1 = new SqlParameter(para.ParaName, DBNull.Value);
                //                    cmd.Parameters.Add(oraP1);
                //                    continue;
                //                }
                //                break;
                //            default:
                //                break;
                //        }
                //        SqlParameter op = new SqlParameter(para.ParaName, para.val);
                //        cmd.Parameters.Add(op);
                //    }
                //}
                //else
                //{
                foreach (Para para in paras)
                {
                    SqlParameter oraP = new SqlParameter(para.ParaName, para.val);
                    cmd.Parameters.Add(oraP);
                }
                // }

                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                return i;
            }
            catch (System.Exception ex)
            {
                cmd.Dispose();
                conn.Close();

                paras.SQL = sql;
                string msg = "";
                if (paras.Count == 0)
                    msg = "SQL=" + sql + ",�쳣��Ϣ:" + ex.Message;
                else
                    msg = "SQL=" + paras.SQLNoPara + ",�쳣��Ϣ:" + ex.Message;

                Log.DefaultLogWriteLineInfo(msg);
                throw new Exception(msg);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
        /// <summary>
        /// ����sql
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>ִ�н��</returns>
        private static int RunSQL_200705_MySQL(string sql)
        {
            return RunSQL_200705_MySQL(sql, new Paras());
        }
        /// <summary>
        /// RunSQL_200705_MySQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static int RunSQL_200705_MySQL(string sql, Paras paras)
        {

             MySqlConnection   connOfMySQL = new MySqlConnection(SystemConfig.AppCenterDSN);
            if (connOfMySQL.State != System.Data.ConnectionState.Open)
            {
                connOfMySQL.ConnectionString = SystemConfig.AppCenterDSN;
                connOfMySQL.Open();
            }

            int i = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connOfMySQL);
                cmd.CommandType = CommandType.Text;
                foreach (Para para in paras)
                {
                    MySqlParameter oraP = new MySqlParameter(para.ParaName, para.val);
                    cmd.Parameters.Add(oraP);
                }
                i = cmd.ExecuteNonQuery();
                cmd.Dispose();

                connOfMySQL.Close();
                connOfMySQL.Dispose();
                return i;
            }
            catch (System.Exception ex)
            {
                connOfMySQL.Close();
                connOfMySQL.Dispose();
                throw new Exception(ex.Message+"@SQL:"+sql);
            }
        }
        private static int RunSQL_200705_Ora(string sql,Paras paras)
        {
            OracleConnection conn = new OracleConnection(SystemConfig.AppCenterDSN); 
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = SystemConfig.AppCenterDSN;
                    conn.Open();
                }

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                foreach (Para para in paras)
                {
                    OracleParameter oraP = new OracleParameter(para.ParaName, para.DATypeOfOra);
                    oraP.Size = para.Size;
                    oraP.Value = para.val;
                    cmd.Parameters.Add(oraP);
                }
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close(); //�����ر�.
                return i;
            }
            catch (System.Exception ex)
            {
                conn.Close(); //�����ر�.
                if (BP.Sys.SystemConfig.IsDebug)
                {
                    string msg = "RunSQL2   SQL=" + sql + ex.Message;
                    Log.DebugWriteError(msg);
                    throw new Exception(msg);
                }
                else
                {
                    Log.DebugWriteError(ex.Message);
                    throw new Exception(ex.Message + sql);
                }
            }
            finally
            {
                conn.Close();
            }
        }
        private static int RunSQL_200705_OLE(string sql, Paras para)
        {
            OleDbConnection conn = new OleDbConnection(SystemConfig.AppCenterDSN); // connofora.Conn;
            try
            {
                if (conn == null)
                    conn = new OleDbConnection(SystemConfig.AppCenterDSN);

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                foreach (Para mypara in para)
                {
                    OleDbParameter oraP = new OleDbParameter(mypara.ParaName, mypara.val);
                    cmd.Parameters.Add(oraP);
                }

                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                string msg = "RunSQL_200705_OLE   SQL=" + sql + ex.Message;
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
            }
        }
        private static int RunSQL_200705_OLE(string sql)
        {
            Paras ps = new Paras();
            return RunSQL_200705_OLE(sql, ps);
        }

        /// <summary>
        /// ����sql
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>ִ�н��</returns>
        private static int RunSQL_201205_Informix(string sql)
        {
            return RunSQL_201205_Informix(sql, new Paras());

            //ConnOfInformix connofora = (ConnOfInformix)DBAccess.GetAppCenterDBConn;
            //IfxConnection conn = connofora.Conn;
            //try
            //{
            //    if (conn == null)
            //        conn = new IfxConnection(SystemConfig.AppCenterDSN);

            //    if (conn.State != System.Data.ConnectionState.Open)
            //        conn.Open();

            //    IfxCommand cmd = new IfxCommand(sql, conn);
            //    cmd.CommandType = CommandType.Text;
            //    int i = cmd.ExecuteNonQuery();
            //    cmd.Dispose();
                
            //    conn.Close();
            //    conn.Dispose();

            //    HisConnOfInformix.PutPool(connofora);
            //    return i;
            //}
            //catch (System.Exception ex)
            //{
            //    conn.Close();
            //    conn.Dispose();

            //    HisConnOfInformix.PutPool(connofora);
            //    if (BP.Sys.SystemConfig.IsDebug)
            //    {
            //        string msg = "RunSQL2   SQL=" + sql + ex.Message;
            //        Log.DebugWriteError(msg);
            //        throw new Exception(msg);
            //    }
            //    else
            //    {
            //        throw new Exception(ex.Message + " RUN SQL=" + sql);
            //    }
            //}
            //finally
            //{
            //    if (SystemConfig.IsBSsystem_Test == false)
            //    {
            //        conn.Close();
            //        conn.Dispose();
            //    }

            //    conn.Close();
            //    HisConnOfInformix.PutPool(connofora);
            //}
        }
        private static int RunSQL_201205_Informix(string sql, Paras paras)
        {
            if (paras.Count != 0)
                sql = DealInformixSQL(sql);

            IfxConnection conn = new IfxConnection(SystemConfig.AppCenterDSN); 
            try
            {
                if (conn == null)
                    conn = new IfxConnection(SystemConfig.AppCenterDSN);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = SystemConfig.AppCenterDSN;
                    conn.Open();
                }

                IfxCommand cmd = new IfxCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                foreach (Para para in paras)
                {
                    IfxParameter oraP = new IfxParameter(para.ParaName, para.val);
                    cmd.Parameters.Add(oraP);
                }

                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                return i;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                string msg = "RunSQL2   SQL=" + sql + "\r\n Message=: " + ex.Message;
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #endregion

        #region ����SQL ���� DataTable
        #region ��ָ���� Connection ��ִ��

        #region SqlConnection
        /// <summary>
        /// ��
        /// </summary>
        private static bool lock_msSQL_ReturnTable = false;
        public static DataTable RunSQLReturnTable(string oraSQL, OracleConnection conn, CommandType sqlType, string dsn)
        {

#if DEBUG
            //Debug.WriteLine( oraSQL );
#endif

            
            try
            {
                if (conn == null)
                {
                    conn = new OracleConnection(dsn);
                    conn.Open();
                }

                if (conn.State != ConnectionState.Open)
                {
                    conn.ConnectionString = dsn;
                    conn.Open();
                }

                OracleDataAdapter oraAda = new OracleDataAdapter(oraSQL, conn);
                oraAda.SelectCommand.CommandType = sqlType;

               
                DataTable oratb = new DataTable("otb");
                oraAda.Fill(oratb);

                // peng add 07-19
                oraAda.Dispose();

                return oratb;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + " [RunSQLReturnTable on OracleConnection dsn=App ] sql=" + oraSQL + "<br>");
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msSQL"></param>
        /// <param name="sqlconn"></param>
        /// <param name="sqlType"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static DataTable RunSQLReturnTable(string msSQL, SqlConnection conn, string connStr, CommandType sqlType, params object[] pars)
        {
            string msg = "step1";

            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = connStr;
                conn.Open();
            }

#if DEBUG
            Debug.WriteLine(msSQL);
#endif

            while (lock_msSQL_ReturnTable)
                ;

            SqlDataAdapter msAda = new SqlDataAdapter(msSQL, conn);
            msg = "error 2";
            msAda.SelectCommand.CommandType = sqlType;
            if (pars != null)
            {
                //CommandType.
                foreach (object par in pars)
                {
                    msAda.SelectCommand.Parameters.AddWithValue("par", par);
                }
            }

            DataTable mstb = new DataTable("mstb");
            //���������״̬���͵ȴ�
            lock_msSQL_ReturnTable = true; //����
            try
            {
                msg = "error 3";
                try
                {
                    msg = "4";
                    msAda.Fill(mstb);
                }
                catch (Exception ex)
                {
                    msg = "5";
                    lock_msSQL_ReturnTable = false;
                    conn.Close();
                    throw new Exception(ex.Message + " msg=" + msg + " Run@DBAccess");
                }
                msg = "10";
                msAda.Dispose();
                msg = "11";
                //				if (SystemConfig.IsBSsystem==false )
                //				{
                //					msg="13";
                //					sqlconn.Close();
                //				}
                msg = "14";
                lock_msSQL_ReturnTable = false;// ����ǰһ��Ҫ����
                conn.Close();
            }
            catch (System.Exception ex)
            {
                lock_msSQL_ReturnTable = false;
                conn.Close();
                throw new Exception("[RunSQLReturnTable on SqlConnection 1] step = " + msg + "<BR>" + ex.Message + " sql=" + msSQL);
            }
            return mstb;
        }
        #endregion

        #region OleDbConnection
        /// <summary>
        /// ��
        /// </summary>
        private static bool lock_oleSQL_ReturnTable = false;
        /// <summary>
        /// ����sql ����Table
        /// </summary>
        /// <param name="oleSQL">oleSQL</param>
        /// <param name="oleconn">����</param>
        /// <param name="sqlType">����</param>
        /// <param name="pars">����</param>
        /// <returns>ִ��SQL���ص�DataTable</returns>
        public static DataTable RunSQLReturnTable(string oleSQL, OleDbConnection oleconn, CommandType sqlType, params object[] pars)
        {
#if DEBUG
            Debug.WriteLine(oleSQL);
#endif


            while (lock_oleSQL_ReturnTable)
            {
                ;
            }  //���������״̬���͵ȴ�
            lock_oleSQL_ReturnTable = true; //����
            string msg = "step1";
            try
            {
                OleDbDataAdapter msAda = new OleDbDataAdapter(oleSQL, oleconn);
                msg += "2";
                msAda.SelectCommand.CommandType = sqlType;
                foreach (object par in pars)
                {
                    msAda.SelectCommand.Parameters.AddWithValue("par", par);
                }
                DataTable mstb = new DataTable("mstb");
                msg += "3";
                msAda.Fill(mstb);
                msg += "4";
                // peng add 2004-07-19 .
                msAda.Dispose();
                msg += "5";
                if (SystemConfig.IsBSsystem_Test == false)
                {
                    msg += "6";
                    oleconn.Close();
                }
                msg += "7";
                lock_oleSQL_ReturnTable = false;//����ǰһ��Ҫ����
                return mstb;
            }
            catch (System.Exception ex)
            {
                lock_oleSQL_ReturnTable = false;//����ǰһ��Ҫ����
                throw new Exception("[RunSQLReturnTable on OleDbConnection] error  ��Ѵ��󽻸� peng . step = " + msg + "<BR>" + oleSQL + " ex=" + ex.Message);
            }
            finally
            {
                oleconn.Close();
            }
        }
        /// <summary>
        /// ����sql ����Table
        /// </summary>
        /// <param name="oleSQL">Ҫ���е�sql</param>
        /// <param name="sqlconn">OleDbConnection</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSQLReturnTable(string oleSQL, OleDbConnection sqlconn)
        {
            return RunSQLReturnTable(oleSQL, sqlconn, CommandType.Text);
        }
        #endregion

        #region OracleConnection
        private static DataTable RunSQLReturnTable_200705_Ora(string selectSQL, Paras paras)
        {
            OracleConnection conn = new OracleConnection(SystemConfig.AppCenterDSN); 
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OracleDataAdapter ada = new OracleDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;

                // �������
                foreach (Para para in paras)
                {
                    OracleParameter myParameter = new OracleParameter(para.ParaName, para.DATypeOfOra);
                    myParameter.Size = para.Size;
                    myParameter.Value = para.val;
                    ada.SelectCommand.Parameters.Add(myParameter);
                }

                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                conn.Close();
                return oratb;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                string msg = "@���в�ѯ��(RunSQLReturnTable_200705_Ora with paras)���� sql=" + selectSQL + " @�쳣��Ϣ��" + ex.Message;
                msg += "@Para Num= " + paras.Count;
                foreach (Para pa in paras)
                {
                    msg += "@" + pa.ParaName + "=" + pa.val;
                }
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
            }
        }
        private static DataTable RunSQLReturnTable_200705_SQL(string selectSQL)
        {
            SqlConnection conn = new SqlConnection(SystemConfig.AppCenterDSN); 
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlDataAdapter ada = new SqlDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                return oratb;
            }
            catch (System.Exception ex)
            {
                string msgErr = ex.Message;
                string msg = "@���в�ѯ��(RunSQLReturnTable_200705_SQL)���� sql=" + selectSQL + " @�쳣��Ϣ��" + msgErr;
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
        }
     
        private static DataTable RunSQLReturnTable_200705_SQL(string sql, Paras paras)
        {
            SqlConnection conn = new SqlConnection(SystemConfig.AppCenterDSN);
            if (conn.State != ConnectionState.Open)
                conn.Open();

            SqlDataAdapter ada = new SqlDataAdapter(sql, conn);
            ada.SelectCommand.CommandType = CommandType.Text;

            // �������
            foreach (Para para in paras)
            {
                SqlParameter myParameter = new SqlParameter(para.ParaName, para.val);
                myParameter.Size = para.Size;
                ada.SelectCommand.Parameters.Add(myParameter);
            }
            try
            {
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                conn.Close();
                return oratb;
            }
            catch (Exception ex)
            {
                ada.Dispose();
                conn.Close();
                throw new Exception("SQL=" + sql + " Exception=" + ex.Message);
            }
        }
        private static string DealInformixSQL(string sql)
        {
            if (sql.Contains("?") == false)
                return sql;

            string mysql = "";
            if (sql.Contains("? ") == true || sql.Contains("?,") == true)
            {
                /*����пո�,˵���Ѿ��滻���ˡ�*/
                return sql;
            }
            else
            {
                sql += " ";
                /*˵����Ҫ����ı���.*/
                string[] strs = sql.Split('?');
                mysql = strs[0];
                for (int i = 1; i < strs.Length; i++)
                {
                    string str = strs[i];
                    switch (str.Substring(0, 1))
                    {
                        case " ":
                            mysql += "?" + str;
                            break;
                        case ")":
                            mysql += "?" + str;
                            break;
                        case ",":
                            mysql += "?" + str;
                            break;
                        default:
                            char[] chs = str.ToCharArray();
                            foreach (char c in chs)
                            {
                                if (c == ',')
                                {
                                    int idx1 = str.IndexOf(",");
                                    mysql += "?" + str.Substring(idx1);
                                    break;
                                }

                                if (c == ')')
                                {
                                    int idx1 = str.IndexOf(")");
                                    mysql += "?" + str.Substring(idx1);
                                    break;
                                }

                                if (c == ' ')
                                {
                                    int idx1 = str.IndexOf(" ");
                                    mysql += "?" + str.Substring(idx1);
                                    break;
                                }
                            }

                            //else
                            //{
                            //    mysql += "?" + str;
                            //}

                            //if (str.Contains(")") == true)
                            //    mysql += "?" + str.Substring(str.IndexOf(")"));
                            //else
                            //    mysql += "?" + str;
                            break;
                    }
                }
            }
            return mysql;
        }
        /// <summary>
        /// RunSQLReturnTable_200705_Informix
        /// </summary>
        /// <param name="selectSQL">Ҫִ�е�sql</param>
        /// <returns>����table</returns>
        private static DataTable RunSQLReturnTable_201205_Informix(string sql, Paras paras)
        {
            //if (paras.Count != 0 && sql.Contains("?") == false)
            //{
            //    sql = DealInformixSQL(sql);
            //}
            sql = DealInformixSQL(sql);

            IfxConnection conn = new IfxConnection(SystemConfig.AppCenterDSN);
            if (conn.State != ConnectionState.Open)
                conn.Open();

            IfxDataAdapter ada = new IfxDataAdapter(sql, conn);
            ada.SelectCommand.CommandType = CommandType.Text;

            // �������
            foreach (Para para in paras)
            {
                IfxParameter myParameter = new IfxParameter(para.ParaName,para.val);
                myParameter.Size = para.Size;
                ada.SelectCommand.Parameters.Add(myParameter);
            }

            try
            {
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                conn.Close();
                return oratb;
            }
            catch (Exception ex)
            {
                ada.Dispose();
                conn.Close();
                Log.DefaultLogWriteLineError(sql);
                Log.DefaultLogWriteLineError(ex.Message);

                throw new Exception("SQL=" + sql + " Exception=" + ex.Message);
            }
            finally
            {
                ada.Dispose();
                conn.Close();
            }
        }
        /// <summary>
        /// RunSQLReturnTable_200705_SQL
        /// </summary>
        /// <param name="selectSQL">Ҫִ�е�sql</param>
        /// <returns>����table</returns>
        private static DataTable RunSQLReturnTable_200705_MySQL(string selectSQL)
        {
            return RunSQLReturnTable_200705_MySQL(selectSQL, new Paras());
        }
        /// <summary>
        /// RunSQLReturnTable_200705_SQL
        /// </summary>
        /// <param name="selectSQL">Ҫִ�е�sql</param>
        /// <returns>����table</returns>
        private static DataTable RunSQLReturnTable_200705_MySQL(string sql, Paras paras)
        {
          //  string mcs = "Data Source=127.0.0.1;User ID=root;Password=root;DataBase=wk;Charset=gb2312;";
          //  MySqlConnection conn = new MySqlConnection(SystemConfig.AppCenterDSN);
          //  SqlDataAdapter ad = new SqlDataAdapter("select username,password from person", conn);
          //  DataTable dt = new DataTable();
          //  conn.Open();
          //  ad.Fill(dt);
          //  conn.Close();
          //  return dt;

            MySqlConnection conn = new MySqlConnection(SystemConfig.AppCenterDSN);
            if (conn.State != ConnectionState.Open)
                conn.Open();

            MySqlDataAdapter ada = new MySqlDataAdapter(sql, conn);
            ada.SelectCommand.CommandType = CommandType.Text;
           
            // �������
            foreach (Para para in paras)
            {
                MySqlParameter myParameter = new MySqlParameter(para.ParaName, para.val);
                myParameter.Size = para.Size;
                ada.SelectCommand.Parameters.Add(myParameter);
            }

            try
            {
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();

                conn.Close();
                conn.Dispose();
                return oratb;
            }
            catch (Exception ex)
            {
                ada.Dispose();
                conn.Close();
                throw new Exception("SQL=" + sql + " Exception=" + ex.Message);
            }
        }
        /// <summary>
        /// RunSQLReturnTable_200705_SQL
        /// </summary>
        /// <param name="selectSQL">Ҫִ�е�sql</param>
        /// <returns>����table</returns>
        private static DataTable RunSQLReturnTable_201205_Informix(string selectSQL)
        {
            return RunSQLReturnTable_201205_Informix(selectSQL, new Paras());
        }
        /// <summary>
        /// RunSQLReturnTable_200705_SQL
        /// </summary>
        /// <param name="selectSQL">Ҫִ�е�sql</param>
        /// <returns>����table</returns>
        private static DataTable RunSQLReturnTable_200705_OLE(string selectSQL, Paras paras)
        {
            OleDbConnection conn = new OleDbConnection(SystemConfig.AppCenterDSN);
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OleDbDataAdapter ada = new OleDbDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;

                // �������
                foreach (Para para in paras)
                {
                    OleDbParameter myParameter = new OleDbParameter(para.ParaName, para.DATypeOfOra);
                    myParameter.Size = para.Size;
                    myParameter.Value = para.val;
                    ada.SelectCommand.Parameters.Add(myParameter);
                }

                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();

                conn.Close();

                return oratb;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                string msg = "@RunSQLReturnTable_200705_OLE with paras) Error sql=" + selectSQL + " @Messages��" + ex.Message;
                msg += "@Para Num= " + paras.Count;
                foreach (Para pa in paras)
                {
                    msg += "@" + pa.ParaName + "=" + pa.val + " type=" + pa.DAType.ToString();
                }
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion


        #endregion

        #region �ڵ�ǰConnection��ִ��
        public static DataTable RunSQLReturnTable(Paras ps)
        {
            return RunSQLReturnTable(ps.SQL, ps);
        }
        public static int RunSQLReturnTableCount = 0;
        /// <summary>
        /// ����һ��select ��䷵��һ����ѯ������ϡ�
        /// </summary>
        /// <param name="sql">select sql</param>
        /// <returns>��ѯ�������DataTable</returns>
        public static DataTable RunSQLReturnTable(string sql)
        {
            Paras ps = new Paras();
            return RunSQLReturnTable(sql, ps);
        }
        public static DataTable RunSQLReturnTable(string sql, string key1, object v1,string key2,object v2)
        {
            Paras ens = new Paras();
            ens.Add(key1, v1);
            ens.Add(key2, v2);
            return RunSQLReturnTable(sql, ens);
        }
        public static DataTable RunSQLReturnTable(string sql, string key, object val)
        {
            Paras ens = new Paras();
            ens.Add(key, val);
            return RunSQLReturnTable(sql, ens);
        }
        private static bool lockRunSQLReTable = false;
        public static DataTable RunSQLReturnTable(string sql, Paras paras)
        {
            if (string.IsNullOrEmpty(sql))
                throw new Exception("Ҫִ�е� sql =null ");
            try
            {
                DataTable dt = null;
                switch (AppCenterDBType)
                {
                    case DBType.MSSQL:
                        dt = RunSQLReturnTable_200705_SQL(sql, paras);
                        break;
                    case DBType.Oracle:
                        dt = RunSQLReturnTable_200705_Ora(sql, paras);
                        break;
                    case DBType.Informix:
                        dt = RunSQLReturnTable_201205_Informix(sql, paras);
                        break;
                    case DBType.MySQL:
                        dt = RunSQLReturnTable_200705_MySQL(sql, paras);
                        break;
                    case DBType.Access:
                        dt = RunSQLReturnTable_200705_OLE(sql, paras);
                        break;
                    default:
                        throw new Exception("@����δ֪�����ݿ��������ͣ�");
                }
                return dt;
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLineError(ex.Message);
               throw ex;
            }
        }
        #endregion �ڵ�ǰConnection��ִ��

        #endregion

       
      
        #region ��ѯ����ֵ�ķ���.

        #region OleDbConnection
        public static float RunSQLReturnValFloat(Paras ps)
        {
            return RunSQLReturnValFloat(ps.SQL, ps, 0);
        }
        public static float RunSQLReturnValFloat(string sql, Paras ps, float val)
        {
            ps.SQL=sql;
            object obj = DA.DBAccess.RunSQLReturnVal(ps);

            try
            {
                if (obj == null || obj.ToString() == "")
                    return val;
                else
                    return float.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + sql + " @OBJ=" + obj);
            }
        }
        /// <summary>
        /// ����sql����float
        /// </summary>
        /// <param name="sql">Ҫִ�е�sql,����һ��һ��.</param>
        /// <param name="isNullAsVal">����ǿ�ֵ�ͷ��ص�Ĭ��ֵ</param>
        /// <returns>float�ķ���ֵ</returns>
        public static float RunSQLReturnValFloat(string sql, float isNullAsVal)
        {
            return RunSQLReturnValFloat(sql, new Paras(), isNullAsVal);
        }
        /// <summary>
        /// sdfsd
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static float RunSQLReturnValFloat(string sql)
        {
            try
            {
                return float.Parse(DA.DBAccess.RunSQLReturnVal(sql).ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + sql);
            }
        }
        public static int RunSQLReturnValInt(Paras ps, int IsNullReturnVal)
        {
            return RunSQLReturnValInt(ps.SQL, ps, IsNullReturnVal);
        }
        /// <summary>
        /// sdfsd
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="IsNullReturnVal"></param>
        /// <returns></returns>
        public static int RunSQLReturnValInt(string sql, int IsNullReturnVal)
        {
            object obj = "";
            obj = DA.DBAccess.RunSQLReturnVal(sql);
            if (obj == null || obj.ToString() == "" || obj == DBNull.Value)
                return IsNullReturnVal;
            else
                return Convert.ToInt32(obj);
        }
        public static int RunSQLReturnValInt(string sql, int IsNullReturnVal, Paras paras)
        {
            object obj = "";

            obj = DA.DBAccess.RunSQLReturnVal(sql, paras);
            if (obj == null || obj.ToString() == "")
                return IsNullReturnVal;
            else
                return Convert.ToInt32(obj);
        }
        public static decimal RunSQLReturnValDecimal(string sql, decimal IsNullReturnVal, int blws)
        {
            Paras ps = new Paras();
            ps.SQL=sql;
            return RunSQLReturnValDecimal(ps, IsNullReturnVal, blws);
        }
        public static decimal RunSQLReturnValDecimal(Paras ps, decimal IsNullReturnVal, int blws)
        {
            try
            {
                object obj = DA.DBAccess.RunSQLReturnVal(ps);
                if (obj == null || obj.ToString() == "")
                    return IsNullReturnVal;
                else
                {
                    decimal d = decimal.Parse(obj.ToString());
                    return decimal.Round(d, blws);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ps.SQL );
            }
        }
        public static int RunSQLReturnValInt(Paras ps)
        {
            string str = DBAccess.RunSQLReturnString(ps.SQL, ps);
            if (str.Contains("."))
                str = str.Substring(0, str.IndexOf("."));
            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                throw new Exception("@" + ps.SQL + "   Val=" + str + ex.Message);
            }
        }
        public static int RunSQLReturnValInt(string sql)
        {
            object obj = DBAccess.RunSQLReturnVal(sql);
            if (obj == null || obj == DBNull.Value )
                throw new Exception("@û�л�ȡ��Ҫ��ѯ������,����SQL:" + sql + " @���ڲ�ѯ��������ϸ��Ϣ�Ѿ���¼��־�ļ����봦��");
            string s = obj.ToString();
            if (s.Contains("."))
                s = s.Substring(0, s.IndexOf("."));
            return Convert.ToInt32(s);
        }
        public static int RunSQLReturnValInt(string sql, Paras paras)
        {
            return Convert.ToInt32(DA.DBAccess.RunSQLReturnVal(sql, paras));
        }
        public static int RunSQLReturnValInt(string sql, Paras paras, int isNullAsVal)
        {
            try
            {
                return Convert.ToInt32(DA.DBAccess.RunSQLReturnVal(sql, paras));
            }
            catch
            {
                return isNullAsVal;
            }
        }
        /// <summary>
        /// ��ѯ����ֵ�ķ���
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="conn">OleDbConnection</param>
        /// <returns>object</returns>
        public static object RunSQLReturnVal(string sql, OleDbConnection conn, string dsn)
        {
            return RunSQLReturnVal(sql, conn, CommandType.Text, dsn);
        }

        public static string RunSQLReturnString(string sql, Paras ps)
        {
            if (ps == null)
                ps = new Paras();
            object obj = DBAccess.RunSQLReturnVal(sql, ps);
            if (obj == DBNull.Value || obj ==null )
                return null;
            else
                return obj.ToString();
        }
        /// <summary>
        /// ִ�в�ѯ���ؽ��,���ΪdbNull ���� null.
        /// </summary>
        /// <param name="sql">will run sql.</param>
        /// <returns>,���ΪdbNull ���� null.</returns>
        public static string RunSQLReturnString(string sql)
        {
            try
            {
                return RunSQLReturnString(sql, new Paras());
            }
            catch (Exception ex)
            {
                throw new Exception("@���� RunSQLReturnString���ִ���" + ex.Message + sql);
            }
        }
        public static string RunSQLReturnStringIsNull(Paras ps, string isNullAsVal)
        {
            string v = RunSQLReturnString(ps);
            if (v == null)
                return isNullAsVal;
            else
                return v;
        }
        /// <summary>
        /// ����sql����һ��ֵ
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="isNullAsVal"></param>
        /// <returns></returns>
        public static string RunSQLReturnStringIsNull(string sql, string isNullAsVal)
        {
            //try{
            string s = RunSQLReturnString(sql, new Paras());
            if (s == null)
                return isNullAsVal;
            return s;
            //}
            //catch (Exception ex)
            //{
            //    Log.DebugWriteInfo("RunSQLReturnStringIsNull@" + ex.Message);
            //    return isNullAsVal;
            //}
        }
        public static string RunSQLReturnString(Paras ps)
        {
            return RunSQLReturnString(ps.SQL,ps );
        }
        /// <summary>
        /// ��ѯ����ֵ�ķ���
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="conn">OleDbConnection</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">pars</param>
        /// <returns>object</returns>
        public static object RunSQLReturnVal(string sql, OleDbConnection conn, CommandType sqlType, params object[] pars)
        {

#if DEBUG
            Debug.WriteLine(sql);
#endif
            OleDbConnection tmpconn = new OleDbConnection(conn.ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sql, tmpconn);
            object val = null;
            try
            {
                tmpconn.Open();
                cmd.CommandType = sqlType;
                foreach (object par in pars)
                {
                    cmd.Parameters.AddWithValue("par", par);
                }
                val = cmd.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                cmd.Cancel();
                tmpconn.Close();
                cmd.Dispose();
                tmpconn.Dispose();
                throw new Exception(ex.Message + " [RunSQLReturnVal on OleDbConnection] " + sql);
            }
            tmpconn.Close();
            return val;
        }
        #endregion

        #region SqlConnection
        /// <summary>
        /// ��ѯ����ֵ�ķ���
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="conn">SqlConnection</param>
        /// <returns>object</returns>
        public static object RunSQLReturnVal(string sql, SqlConnection conn, string dsn)
        {
            return RunSQLReturnVal(sql, conn, CommandType.Text, dsn);

        }
        /// <summary>
        /// ��ѯ����ֵ�ķ���
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="conn">SqlConnection</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">pars</param>
        /// <returns>object</returns>
        public static object RunSQLReturnVal(string sql, SqlConnection conn, CommandType sqlType, string dsn, params object[] pars)
        {
            //return DBAccess.RunSQLReturnTable(sql,conn,dsn,sqlType,null).Rows[0][0];

#if DEBUG
            Debug.WriteLine(sql);
#endif

            object val = null;
            SqlCommand cmd = null;

            try
            {
                if (conn == null)
                {
                    conn = new SqlConnection(dsn);
                    conn.Open();
                }

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = dsn;
                    conn.Open();
                }

                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = sqlType;
                val = cmd.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                //return DBAccess.re

                cmd.Cancel();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
                throw new Exception(ex.Message + " [RunSQLReturnVal on SqlConnection] " + sql);
            }
            //conn.Close();
            return val;
        }
        #endregion

    
        #region �ڵ�ǰ��Connectionִ�� SQL ��䣬������������
        public static int RunSQLReturnCOUNT(string sql)
        {
            return RunSQLReturnTable(sql).Rows.Count;
            //return RunSQLReturnVal( sql ,sql, sql );
        }
        public static object RunSQLReturnVal(string sql, string pkey, object val)
        {
            Paras ps = new Paras();
            ps.Add(pkey, val);

            return RunSQLReturnVal(sql, ps);
        }

        public static object RunSQLReturnVal(string sql,Paras paras)
        {
            RunSQLReturnTableCount++;
          //  Log.DebugWriteInfo("NUMOF " + RunSQLReturnTableCount + "===RunSQLReturnTable sql=" + sql);
            DataTable dt = null;
            switch (SystemConfig.AppCenterDBType)
            {
                case DBType.Oracle:
                    dt = DBAccess.RunSQLReturnTable_200705_Ora(sql, paras);
                    break;
                case DBType.MSSQL:
                    dt = DBAccess.RunSQLReturnTable_200705_SQL(sql, paras);
                    break;
                case DBType.MySQL:
                    dt = DBAccess.RunSQLReturnTable_200705_MySQL(sql, paras);
                    break;
                case DBType.Informix:
                    dt = DBAccess.RunSQLReturnTable_201205_Informix(sql, paras);
                    break;
                case DBType.Access:
                    dt = DBAccess.RunSQLReturnTable_200705_OLE(sql, paras);
                    break;
                default:
                    throw new Exception("@û���жϵ����ݿ�����");
            }

            if (dt.Rows.Count == 0)
                return null;
            return dt.Rows[0][0];
        }
        public static object RunSQLReturnVal(Paras ps)
        {
            return RunSQLReturnVal(ps.SQL, ps);
        }
        public static object RunSQLReturnVal(string sql)
        {
            RunSQLReturnTableCount++;
            DataTable dt = null;
            switch (SystemConfig.AppCenterDBType)
            {
                case DBType.Oracle:
                    dt = DBAccess.RunSQLReturnTable_200705_Ora(sql, new Paras());
                    break;
                case DBType.MSSQL:
                    dt = DBAccess.RunSQLReturnTable_200705_SQL(sql, new Paras());
                    break;
                case DBType.Informix:
                    dt = DBAccess.RunSQLReturnTable_201205_Informix(sql, new Paras());
                    break;
                case DBType.MySQL:
                    dt = DBAccess.RunSQLReturnTable_200705_MySQL(sql, new Paras());
                    break;
                case DBType.Access:
                    dt = DBAccess.RunSQLReturnTable_200705_OLE(sql, new Paras());
                    break;
                default:
                    throw new Exception("@û���жϵ����ݿ�����");
            }
            if (dt.Rows.Count == 0)
            {
             
#warning ��Ӧ�ó��ֵ��쳣 2011-12-03 
                string cols = "";
                foreach (DataColumn dc in dt.Columns)
                    cols += " , " + dc.ColumnName;

                BP.DA.Log.DebugWriteInfo("@SQL="+sql+" . ��="+cols);
                return null;
            }
            return dt.Rows[0][0];
        }
        #endregion

        #endregion

        #region ����ǲ��Ǵ���
        /// <summary>
        /// ����ǲ��Ǵ���
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>����ǲ��Ǵ���</returns>
        public static bool IsExits(string sql)
        {
            if (sql.ToUpper().Contains("SELECT") == false)
                throw new Exception("@�Ƿ��Ĳ�ѯ���"+sql);

            if (RunSQLReturnVal(sql) == null)
                return false;
            return true;
        }
        public static bool IsExits(string sql, Paras ps)
        {
            if (RunSQLReturnVal(sql, ps) == null)
                return false;
            return true;
        }
        /// <summary>
        /// �ж��Ƿ��������pk .
        /// </summary>
        /// <param name="tab">�����</param>
        /// <returns>�Ƿ����</returns>
        public static bool IsExitsTabPK(string tab)
        {
            BP.DA.Paras ps = new Paras();
            ps.Add("Tab", tab);
            string sql = "";
            switch (AppCenterDBType)
            {
                case DBType.Access:
                    return false;
                case DBType.MSSQL:
                    sql = "SELECT column_name, table_name,CONSTRAINT_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE table_name =@Tab ";
                    break;
                case DBType.Oracle:
                    sql = "SELECT constraint_name, constraint_type,search_condition, r_constraint_name  from user_constraints WHERE table_name = upper(:tab) AND constraint_type = 'P'";
                    break;
                case DBType.MySQL:
                    sql = "SELECT column_name, table_name, CONSTRAINT_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE table_name =@Tab and table_schema='"+SystemConfig.AppCenterDBDatabase+"' ";
                    break;
                case DBType.Informix:
                    sql = "SELECT * FROM sysconstraints c inner join systables t on c.tabid = t.tabid where t.tabname = lower(?) and constrtype = 'P'";
                    break;
                default:
                    throw new Exception("ssseerr ");
            }

            DataTable dt = DBAccess.RunSQLReturnTable(sql, ps);
            if (dt.Rows.Count >= 1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// �Ƿ���view
        /// </summary>
        /// <param name="tabelOrViewName"></param>
        /// <returns></returns>
        public static bool IsView(string tabelOrViewName)
        {
            string sql = "";
            switch (SystemConfig.AppCenterDBType)
            {
                case DBType.Oracle:
                    sql = "SELECT TABTYPE  FROM TAB WHERE UPPER(TNAME)=:v";
                    DataTable oradt = DBAccess.RunSQLReturnTable(sql, "v", tabelOrViewName.ToUpper());
                    if (oradt.Rows.Count == 0)
                        throw new Exception("@������[" + tabelOrViewName + "]");
                    if (oradt.Rows[0][0].ToString().ToUpper().Trim() == "V".ToString())
                        return true;
                    else
                        return false;
                    break;
                case DBType.Access:
                    sql = "select   Type   from   msysobjects   WHERE   UCASE(name)='" + tabelOrViewName.ToUpper() + "'";
                    DataTable dtw = DBAccess.RunSQLReturnTable(sql);
                    if (dtw.Rows.Count == 0)
                        throw new Exception("@������[" + tabelOrViewName + "]");
                    if (dtw.Rows[0][0].ToString().Trim() == "5")
                        return true;
                    else
                        return false;
                case DBType.MSSQL:
                    sql = "select xtype from sysobjects WHERE name =" + SystemConfig.AppCenterDBVarStr + "v";
                    DataTable dt1 = DBAccess.RunSQLReturnTable(sql, "v", tabelOrViewName);
                    if (dt1.Rows.Count == 0)
                        throw new Exception("@������[" + tabelOrViewName + "]");

                    if (dt1.Rows[0][0].ToString().ToUpper().Trim() == "V".ToString())
                        return true;
                    else
                        return false;
                case DBType.Informix:
                    sql = "select tabtype from systables where tabname = '" + tabelOrViewName.ToLower() + "'";
                    DataTable dtaa = DBAccess.RunSQLReturnTable(sql);
                    if (dtaa.Rows.Count == 0)
                        throw new Exception("@������[" + tabelOrViewName + "]");

                    if (dtaa.Rows[0][0].ToString().ToUpper().Trim() == "V")
                        return true;
                    else
                        return false;
                case DBType.MySQL:
                    sql = "SELECT Table_Type FROM information_schema.TABLES WHERE table_name='" + tabelOrViewName + "' and table_schema='" + SystemConfig.AppCenterDBDatabase + "'";
                    DataTable dt2 = DBAccess.RunSQLReturnTable(sql);
                    if (dt2.Rows.Count == 0)
                        throw new Exception("@������[" + tabelOrViewName + "]");

                    if (dt2.Rows[0][0].ToString().ToUpper().Trim() == "VIEW")
                        return true;
                    else
                        return false;
                default:
                    throw new Exception("@û�������жϡ�");
            }

            DataTable dt = DBAccess.RunSQLReturnTable(sql, "v", tabelOrViewName.ToUpper());
            if (dt.Rows.Count == 0)
                throw new Exception("@������[" + tabelOrViewName + "]");

            if (dt.Rows[0][0].ToString() == "VIEW")
                return true;
            else
                return false;
            return true;
        }
        /// <summary>
        /// �ж�ϵͳ���Ƿ���ڶ���.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool IsExitsObject(string obj)
        {
            //�е�ͬ��д�ı�������dbo.���´���ʧ��.
            obj = obj.Replace("dbo.", "");

            // ���Ӳ���.
            Paras ps = new Paras();
            ps.Add("obj", obj);

            switch (AppCenterDBType)
            {
                case DBType.Oracle:
                    if (obj.IndexOf(".") != -1)
                        obj = obj.Split('.')[1];
                    return IsExits("select tname from tab WHERE  tname = upper(:obj) ", ps);
                case DBType.MSSQL:
                    return IsExits("SELECT name  FROM sysobjects  WHERE  name = '" + obj + "'");
                case DBType.Informix:
                    return IsExits("select tabname from systables where tabname = '" + obj.ToLower() + "'");
                case DBType.MySQL:
                    if (obj.IndexOf(".") != -1)
                        obj = obj.Split('.')[1];
                    return IsExits("SELECT table_name, table_type FROM information_schema.tables  WHERE table_name = '" + obj + "' AND   TABLE_SCHEMA='" + BP.Sys.SystemConfig.AppCenterDBDatabase + "' ");
                case DBType.Access:
                    //return false ; //IsExits("SELECT * FROM MSysObjects WHERE (((MSysObjects.Name) =  '"+obj+"' ))");
                    return IsExits("SELECT * FROM MSysObjects WHERE Name =  '" + obj + "'");
                default:
                    throw new Exception("û��ʶ������ݿ���");
            }
        }
        /// <summary>
        /// �����Ƿ����ָ������
        /// </summary>
        /// <param name="table">����</param>
        /// <param name="col">����</param>
        /// <returns>�Ƿ����</returns>
        public static bool IsExitsTableCol(string table, string col)
        {
            Paras ps = new Paras();
            ps.Add("tab", table);
            ps.Add("col", col);

            int i = 0;
            switch (DBAccess.AppCenterDBType)
            {
                case DBType.Access:
                    return false;
                    break;
                case DBType.MSSQL:
                    i = DBAccess.RunSQLReturnValInt("SELECT  COUNT(*)  FROM information_schema.COLUMNS  WHERE TABLE_NAME='"+table+"' AND COLUMN_NAME='"+col+"'", 0);
                    break;
                case DBType.MySQL:
                    string sql = "select count(*) FROM information_schema.columns WHERE TABLE_SCHEMA='" + BP.Sys.SystemConfig.AppCenterDBDatabase + "' AND table_name ='" +table + "' and column_Name='" +  col+ "'"; 
                    i = DBAccess.RunSQLReturnValInt(sql);
                    break;
                case DBType.Oracle:
                    if (table.IndexOf(".") != -1)
                        table = table.Split('.')[1];
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) from user_tab_columns  WHERE table_name= upper(:tab) AND column_name= upper(:col) ", ps);
                    break;
                case DBType.Informix:
                    i = DBAccess.RunSQLReturnValInt("select count(*) from syscolumns c where tabid in (select tabid	from systables	where tabname = lower('" + table + "')) and c.colname = lower('" + col + "')", 0);
                    break;
                default:
                    throw new Exception("error");
            }

            if (i == 1)
                return true;
            else
                return false;
        }
        #endregion

        /// <summary>
        /// ��ñ�Ļ�����Ϣ������������:
        /// 1, �ֶ����ƣ��ֶ��������ֶ����ͣ��ֶγ���.
        /// </summary>
        /// <param name="tableName">����</param>
        public static DataTable GetTableSchema(string tableName)
        {
            string sql = "";
            switch (SystemConfig.AppCenterDBType)
            {
                case DBType.MSSQL:
                    sql = "SELECT column_name as FNAME, data_type as FTYPE, CHARACTER_MAXIMUM_LENGTH as FLEN  from information_schema.columns where table_name='" + tableName + "'";
                    break;
                case DBType.Oracle:
                    sql = "SELECT COLUMN_NAME as FNAME,DATA_TYPE as FTYPE,DATA_LENGTH as FLEN from all_tab_columns where table_name = upper('" + tableName + "')";
                    break;
                case DBType.MySQL:
                    sql = "SELECT COLUMN_NAME FNAME,DATA_TYPE FTYPE,CHARACTER_MAXIMUM_LENGTH FLEN,COLUMN_COMMENT 'DESC' FROM information_schema.columns WHERE table_name='" + tableName + "'";
                    break;
                default:
                    break;
            }
            return BP.DA.DBAccess.RunSQLReturnTable(sql);
        }
        #region LoadConfig
        public static void LoadConfig(string cfgFile, string basePath)
        {
            if (!File.Exists(cfgFile))
                throw new Exception("�Ҳ��������ļ�==>[" + cfgFile + "]1");

            StreamReader read = new StreamReader(cfgFile);
            string firstline = read.ReadLine();
            string cfg = read.ReadToEnd();
            read.Close();

            int start = cfg.ToLower().IndexOf("<appsettings>");
            int end = cfg.ToLower().IndexOf("</appsettings>");

            cfg = cfg.Substring(start, end - start + "</appsettings".Length + 1);

            cfgFile = basePath + "\\__$AppConfig.cfg";
            StreamWriter write = new StreamWriter(cfgFile);
            write.WriteLine(firstline);
            write.Write(cfg);
            write.Flush();
            write.Close();

            DataSet dscfg = new DataSet("cfg");
            try
            {
                dscfg.ReadXml(cfgFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }

          //  BP.Sys.SystemConfig.CS_AppSettings = new System.Collections.Specialized.NameValueCollection();
            BP.Sys.SystemConfig.CS_DBConnctionDic.Clear();
            foreach (DataRow row in dscfg.Tables["add"].Rows)
            {
                BP.Sys.SystemConfig.CS_AppSettings.Add(row["key"].ToString().Trim(), row["value"].ToString().Trim());
            }
            dscfg.Dispose();

            BP.Sys.SystemConfig.IsBSsystem = false;
        }


        #endregion
    }

	#region ODBC
    public class DBAccessOfODBC
    {
        /// <summary>
        /// ����ǲ��Ǵ���
        /// </summary>
        public static bool IsExits(string selectSQL)
        {
            if (RunSQLReturnVal(selectSQL) == null)
                return false;
            return true;
        }

        #region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
        public static OdbcConnection GetSingleConn
        {
            get
            {
                if (SystemConfig.IsBSsystem_Test)
                {
                    OdbcConnection conn = HttpContext.Current.Session["DBAccessOfODBC"] as OdbcConnection;
                    if (conn == null)
                    {
                        conn = new OdbcConnection(SystemConfig.AppSettings["DBAccessOfODBC"]);
                        HttpContext.Current.Session["DBAccessOfODBC"] = conn;
                    }
                    return conn;
                }
                else
                {
                    OdbcConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfODBC"] as OdbcConnection;
                    if (conn == null)
                    {
                        conn = new OdbcConnection(SystemConfig.AppSettings["DBAccessOfODBC"]);
                        SystemConfig.CS_DBConnctionDic["DBAccessOfODBC"] = conn;
                    }
                    return conn;
                }
            }
        }
        #endregion ȡ�����Ӷ��� ��CS��BS��������


        #region ���� RunSQLReturnTable

        #region ʹ�ñ��ص�����
        public static DataTable RunSQLReturnTable(string sql)
        {
            return RunSQLReturnTable(sql, GetSingleConn, CommandType.Text);
        }
        public static DataTable RunSQLReturnTable(string sql, CommandType sqlType, params object[] pars)
        {
            return RunSQLReturnTable(sql, GetSingleConn, sqlType, pars);
        }

        #endregion

        #region ʹ��ָ��������
        public static DataTable RunSQLReturnTable(string sql, OdbcConnection conn)
        {
            return RunSQLReturnTable(sql, conn, CommandType.Text);
        }
        public static DataTable RunSQLReturnTable(string sql, OdbcConnection conn, CommandType sqlType, params object[] pars)
        {
            try
            {
                OdbcDataAdapter ada = new OdbcDataAdapter(sql, conn);
                ada.SelectCommand.CommandType = sqlType;
                foreach (object par in pars)
                {
                    ada.SelectCommand.Parameters.AddWithValue("par", par);
                }
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                DataTable dt = new DataTable("tb");
                ada.Fill(dt);
                // peng add 
                ada.Dispose();
                return dt;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + sql);
            }
        }
        #endregion

        #endregion

        #region ���� RunSQL

        #region ʹ�ñ��ص�����
        public static int RunSQLReturnCOUNT(string sql)
        {
            return RunSQLReturnTable(sql).Rows.Count;
        }
        public static int RunSQL(string sql)
        {
            return RunSQL(sql, GetSingleConn, CommandType.Text);
        }
        public static int RunSQL(string sql, CommandType sqlType, params object[] pars)
        {
            return RunSQL(sql, GetSingleConn, sqlType, pars);
        }
        #endregion ʹ�ñ��ص�����

        #region ʹ��ָ��������
        public static int RunSQL(string sql, OdbcConnection conn)
        {
            return RunSQL(sql, conn, CommandType.Text);
        }
        public static int RunSQL(string sql, OdbcConnection conn, CommandType sqlType, params object[] pars)
        {
            Debug.WriteLine(sql);
            try
            {
                OdbcCommand cmd = new OdbcCommand(sql, conn);
                cmd.CommandType = sqlType;
                foreach (object par in pars)
                {
                    cmd.Parameters.AddWithValue("par", par);
                }
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                return cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + sql);
            }
        }

        #endregion ʹ��ָ��������

        #endregion

        #region ִ��SQL ��������������

        /// <summary>
        /// ����select sql, ����һ��ֵ��
        /// </summary>
        /// <param name="sql">select sql</param>
        /// <returns>���ص�ֵobject</returns>
        public static float RunSQLReturnFloatVal(string sql)
        {
            return (float)RunSQLReturnVal(sql, GetSingleConn, CommandType.Text);
        }
        public static int RunSQLReturnValInt(string sql)
        {
            return (int)RunSQLReturnVal(sql, GetSingleConn, CommandType.Text);
        }
        /// <summary>
        /// ����select sql, ����һ��ֵ��
        /// </summary>
        /// <param name="sql">select sql</param>
        /// <returns>���ص�ֵobject</returns>
        public static object RunSQLReturnVal(string sql)
        {
            return RunSQLReturnVal(sql, GetSingleConn, CommandType.Text);
        }
        /// <summary>
        /// ����select sql, ����һ��ֵ��
        /// </summary>
        /// <param name="sql">select sql</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">params</param>
        /// <returns>���ص�ֵobject</returns>
        public static object RunSQLReturnVal(string sql, CommandType sqlType, params object[] pars)
        {
            return RunSQLReturnVal(sql, GetSingleConn, sqlType, pars);
        }
        public static object RunSQLReturnVal(string sql, OdbcConnection conn)
        {
            return RunSQLReturnVal(sql, conn, CommandType.Text);
        }
        public static object RunSQLReturnVal(string sql, OdbcConnection conn, CommandType sqlType, params object[] pars)
        {
            Debug.WriteLine(sql);
            OdbcConnection tmp = new OdbcConnection(conn.ConnectionString);
            OdbcCommand cmd = new OdbcCommand(sql, tmp);
            object val = null;
            try
            {
                cmd.CommandType = sqlType;
                foreach (object par in pars)
                {
                    cmd.Parameters.AddWithValue("par", par);
                }
                tmp.Open();
                val = cmd.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                tmp.Close();
                throw new Exception(ex.Message + sql);
            }
            tmp.Close();
            return val;
        }
        #endregion ִ��SQL ��������������

    }
	#endregion

	/// <summary>
	/// ����OLE ������
	/// </summary>
	public class DBAccessOfOLE
	{
		/// <summary>
		/// ����ǲ��Ǵ���
		/// </summary>
		/// <param name="selectSQL"></param>
		/// <returns>����ǲ��Ǵ���</returns>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}

		#region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
		public static OleDbConnection GetSingleConn
		{
			get
			{
                if (SystemConfig.IsBSsystem_Test)
				{
					OleDbConnection conn = HttpContext.Current.Session["DBAccessOfOLE"] as OleDbConnection;
					if ( conn==null )
					{
						conn = new OleDbConnection( SystemConfig.DBAccessOfOLE );
						HttpContext.Current.Session[ "DBAccessOfOLE" ] = conn;
					}
					return conn;
				}
				else
				{
					OleDbConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfOLE"] as OleDbConnection;
					if ( conn==null )
					{
						conn = new OleDbConnection( SystemConfig.DBAccessOfOLE  );
						SystemConfig.CS_DBConnctionDic[ "DBAccessOfOLE" ] = conn;
					}
					return conn;
				}
			}
		}
		#endregion ȡ�����Ӷ��� ��CS��BS��������

		#region ���� RunSQLReturnTable

		#region ʹ�ñ��ص�����
		public static int RunSQLReturnCOUNT(string sql)
		{
			return RunSQLReturnTable(sql).Rows.Count;
			//return RunSQLReturnVal( sql ,sql, sql );
		}
		/// <summary>
		/// ���в�ѯ��䷵��Table
		/// </summary>
		/// <param name="sql">select sql</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSQLReturnTable(string sql )
		{
			return RunSQLReturnTable( sql , GetSingleConn ,CommandType.Text );
		}
		/// <summary>
		/// ���в�ѯ��䷵��Table
		/// </summary>
		/// <param name="sql">select sql</param>
		/// <param name="sqlType">CommandType</param>
		/// <param name="pars">pareas</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSQLReturnTable(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQLReturnTable( sql , GetSingleConn ,sqlType ,pars);
		}	
		#endregion 

		#region ʹ��ָ��������
		public static DataTable RunSQLReturnTable(string sql , OleDbConnection conn )
		{
			return RunSQLReturnTable( sql ,conn ,CommandType.Text );
		}
		public static DataTable RunSQLReturnTable(string sql , OleDbConnection conn ,CommandType sqlType ,params object[] pars)
		{
			try
			{
				OleDbDataAdapter ada = new OleDbDataAdapter( sql ,conn);
				ada.SelectCommand.CommandType = sqlType;
				foreach(object par in pars)
				{
                    ada.SelectCommand.Parameters.AddWithValue("par", par);
				}
				if (conn.State != ConnectionState.Open)
					conn.Open();
				DataTable dt = new DataTable("tb");
				ada.Fill( dt );
				ada.Dispose();
				return dt;
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message + sql);
			}
		}
		#endregion 

		#endregion

		#region ���� RunSQL

		#region ʹ�ñ��ص�����
		public static int RunSQL(string sql )
		{
			return RunSQL( sql , GetSingleConn ,CommandType.Text);
		}
		public static int RunSQL(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQL( sql , GetSingleConn ,sqlType , pars);
		}
		#endregion ʹ�ñ��ص�����

		#region ʹ��ָ��������
		public static int RunSQL(string sql ,OleDbConnection conn )
		{
			return RunSQL( sql , conn ,CommandType.Text );
		}
		public static int RunSQL(string sql ,OleDbConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			try
			{
				OleDbCommand cmd = new OleDbCommand( sql ,conn);
				cmd.CommandType = sqlType;
				foreach(object par in pars)
				{
                    cmd.Parameters.AddWithValue("par", par);
				}

				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				return cmd.ExecuteNonQuery();				 
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message + sql );
			}
		}

		#endregion ʹ��ָ��������

		#endregion 

		#region ִ��SQL ��������������
		public static object RunSQLReturnVal(string sql )
		{
			return RunSQLReturnVal( sql ,GetSingleConn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQLReturnVal( sql ,GetSingleConn , sqlType ,pars );
		}

		public static object RunSQLReturnVal(string sql ,OleDbConnection conn )
		{
			return RunSQLReturnVal( sql ,conn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,OleDbConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			OleDbConnection tmpconn = new OleDbConnection(conn.ConnectionString);
			OleDbCommand cmd = new OleDbCommand( sql ,tmpconn);
			object val = null;
			try
			{
				cmd.CommandType = sqlType;
                foreach (object par in pars)
                {
                    cmd.Parameters.AddWithValue("par", par);
                }
				tmpconn.Open();
				val= cmd.ExecuteScalar();
			}
			catch(System.Exception ex)
			{
				cmd.Cancel();
				tmpconn.Close();
				throw new Exception(ex.Message + sql );
			}
			tmpconn.Close();
			return val;
		}
		#endregion ִ��SQL ��������������
	}
	/// <summary>
	/// ����DBAccessOfSQLServer2000 ������
	/// </summary>
	public class DBAccessOfMSMSSQL
	{
		#region �������д洢����
        /// <summary>
        /// ���д洢����.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static object RunSPReObj(string spName, Paras paras)
        {
            SqlConnection conn = DBAccessOfMSMSSQL.GetSingleConn;
            SqlCommand cmd = new SqlCommand(spName, conn );
            cmd.CommandType = CommandType.StoredProcedure;
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            foreach (Para para in paras)
            {
                SqlParameter myParameter = new SqlParameter(para.ParaName, para.val );
                myParameter.Direction = ParameterDirection.Input;
                myParameter.Size = para.Size;
                myParameter.Value = para.val;
                cmd.Parameters.Add(myParameter);
            }

            return cmd.ExecuteScalar();
        }
        /// <summary>
        /// ���д洢���̷���һ��decimal.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="paras"></param>
        /// <param name="isNullReVal"></param>
        /// <returns></returns>
        public static decimal RunSPReDecimal(string spName, Paras paras, decimal isNullReVal)
        {
            object obj = RunSPReObj(spName, paras);
            if (obj == null || obj == DBNull.Value)
                return isNullReVal;

            try
            {
                return decimal.Parse(obj.ToString() );
            }
            catch (Exception ex)
            {
                throw new Exception("RunSPReDecimal error="+ex.Message+" Obj="+obj );
            }
        }

		#region ִ�д洢���̷���Ӱ�����
		/// <summary>
		/// ���д洢����
		/// </summary>
		/// <param name="spName">����</param>
		/// <returns>����Ӱ�������</returns>
		public static int RunSP(string spName)
		{
			return DBProcedure.RunSP(spName,DBAccessOfMSMSSQL.GetSingleConn );
		}
		/// <summary>
		/// ���д洢����
		/// </summary>
		/// <param name="spName">����</param>
		/// <param name="paras">����</param>
		/// <returns>����Ӱ�������</returns>
		public static int RunSP(string spName, Paras paras)
		{
			return DBProcedure.RunSP(spName, paras, DBAccessOfMSMSSQL.GetSingleConn );
		}
		#endregion

		#region ���д洢���̷��� DataTable
		/// <summary>
		/// ���д洢����
		/// </summary>
		/// <param name="spName">����</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSPReTable(string spName)
		{
			return DBProcedure.RunSPReturnDataTable(spName,DBAccessOfMSMSSQL.GetSingleConn);
		}
		/// <summary>
		/// ���д洢����
		/// </summary>
		/// <param name="spName">����</param>
		/// <param name="paras">����</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSPReTable(string spName, Paras paras)
		{
			return DBProcedure.RunSPReturnDataTable(spName,paras,DBAccessOfMSMSSQL.GetSingleConn);
		}
		#endregion

		#endregion

		private static bool lock_SQL=false;
		/// <summary>
		/// ����ǲ��Ǵ���
		/// </summary>
		/// <param name="selectSQL"></param>
		/// <returns>����ǲ��Ǵ���</returns>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}


		#region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
		/// <summary>
		/// ȡ����ǰ�� ���ӡ�
		/// </summary>
		public static SqlConnection GetSingleConn
		{
			get
			{
                if (SystemConfig.IsBSsystem_Test)
				{
					SqlConnection conn = HttpContext.Current.Session["DBAccessOfMSMSSQL"] as SqlConnection;
					if (conn==null)
					{
						conn = new SqlConnection( SystemConfig.AppSettings[ "DBAccessOfMSMSSQL" ] );
						HttpContext.Current.Session[ "DBAccessOfMSMSSQL" ] = conn;
					}
					return conn;
				}
				else
				{
					SqlConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfMSMSSQL"] as SqlConnection;
					if (conn==null)
					{
						conn = new SqlConnection( SystemConfig.AppSettings[ "DBAccessOfMSMSSQL" ] );
						SystemConfig.CS_DBConnctionDic[ "DBAccessOfMSMSSQL" ] = conn;
					}
					return conn;
				}
			}
		}
		#endregion ȡ�����Ӷ��� ��CS��BS��������

		#region ���� RunSQLReturnTable
		public static DataTable RunSQLReturnTable(string sql )
		{
			return DBAccess.RunSQLReturnTable( sql , GetSingleConn , SystemConfig.DBAccessOfMSMSSQL, CommandType.Text );
		}
		public static DataTable RunSQLReturnTable(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQLReturnTable( sql , GetSingleConn , SystemConfig.DBAccessOfMSMSSQL, sqlType ,pars);
		}
		public static int RunSQLReturnCOUNT(string sql)
		{
			return RunSQLReturnTable(sql).Rows.Count;
			//return RunSQLReturnVal( sql ,sql, sql );
		}
        public static bool IsExitsObject(string obj)
        {
            return IsExits("SELECT name  FROM sysobjects  WHERE  name = '" + obj + "'");
        }
		/// <summary>
		/// ����SQL , ����Ӱ�������.
		/// </summary>
		/// <param name="sql">msSQL</param>
		/// <param name="conn">SqlConnection</param>
		/// <param name="sqlType">CommandType</param>
		/// <param name="pars">params</param>
		/// <returns>�������н��</returns>
		public static int RunSQL(string sql )
		{
			SqlConnection conn=	DBAccessOfMSMSSQL.GetSingleConn;
			//string step="step=1" ;
			//���������״̬���͵ȴ�.
			while(lock_SQL)  
			{
				lock_SQL=true; //����
			}
			try
			{		

				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();

				SqlCommand cmd = new SqlCommand( sql ,conn  );
				cmd.CommandType = CommandType.Text;

				int i = 0;
				try
				{
					i= cmd.ExecuteNonQuery();
				}
				catch(Exception ex)
				{
					throw ex;
				}
				cmd.Dispose();
				lock_SQL=false;
				return i;
			}
			catch(System.Exception ex)
			{
				lock_SQL=false;
				throw new Exception(ex.Message +"    SQL = "+sql );
			}
			finally
			{
				lock_SQL=false;
				conn.Close();
			}
		}
		#endregion

		#region ִ��SQL ��������������
		public static object RunSQLReturnVal(string sql )
		{
			return DBAccess.RunSQLReturnVal( sql ,GetSingleConn , CommandType.Text ,  SystemConfig.DBAccessOfMSMSSQL );
		}
		public static object RunSQLReturnVal(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQLReturnVal( sql ,GetSingleConn , sqlType ,SystemConfig.DBAccessOfMSMSSQL, pars );
		}
		#endregion ִ��SQL ��������������
        
	}
	/// <summary>
	/// Oracle �ķ���rt1224335647598
	/// </summary>
	public class DBAccessOfOracle1
	{

		#region �������д洢����

		#region ִ�д洢���̷���Ӱ�����
		/// <summary>
		/// ���д洢����
		/// </summary>
		/// <param name="spName">����</param>
		/// <returns>����Ӱ�������</returns>
		public static int RunSP(string spName)
		{
			return DBProcedure.RunSP(spName,DBAccessOfOracle.GetSingleConn );
		}

        public static int RunSP(string spName, string paraKey, string paraVal)
        {
            Paras pas = new Paras();
            pas.Add(paraKey, paraVal);

            return DBProcedure.RunSP(spName, pas, DBAccessOfOracle.GetSingleConn );
        }
		/// <summary>
		/// ���д洢����
		/// </summary>
		/// <param name="spName">����</param>
		/// <param name="paras">����</param>
		/// <returns>����Ӱ�������</returns>
		public static int RunSP(string spName, Paras paras)
		{
			return DBProcedure.RunSP(spName, paras, DBAccessOfOracle.GetSingleConn );
		}

        
        public static object RunSPReObj(string spName, Paras paras)
        {


            OracleConnection conn = DBAccessOfOracle.GetSingleConn;
            
            OracleCommand cmd = new OracleCommand(spName, conn );
            cmd.CommandType = CommandType.StoredProcedure;
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            foreach (Para para in paras)
            {
                OracleParameter myParameter = new OracleParameter(para.ParaName, OracleType.VarChar);
                myParameter.Direction = ParameterDirection.Input;
                myParameter.Size = para.Size;
                myParameter.Value = para.val;
                cmd.Parameters.Add(myParameter);
            }

            return cmd.ExecuteScalar();
        }
        public static decimal RunSPReDecimal(string spName, Paras paras, decimal isNullReVal)
        {
            object obj = RunSPReObj(spName, paras);

            if (obj == null || obj==DBNull.Value )
                return isNullReVal;

            try
            {
                return decimal.Parse( obj.ToString() );
            }
            catch 
            {
                return isNullReVal;
            }
        }
		#endregion

		#region ���д洢���̷��� DataTable
		/// <summary>
		/// ���д洢����
		/// </summary>
		/// <param name="spName">����</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSPReTable(string spName)
		{
			return DBProcedure.RunSPReturnDataTable(spName,DBAccessOfOracle.GetSingleConn);
		}
		/// <summary>
		/// ���д洢����
		/// </summary>
		/// <param name="spName">����</param>
		/// <param name="paras">����</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSPReTable(string spName, Paras paras)
		{
			return DBProcedure.RunSPReturnDataTable(spName,paras,DBAccessOfOracle.GetSingleConn);
		}
		#endregion

		#endregion


		/// <summary>
		/// ����ǲ��Ǵ���
		/// </summary>
		/// <param name="selectSQL"></param>
		/// <returns>����ǲ��Ǵ���</returns>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}
	

		#region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
		public static OracleConnection GetSingleConn
		{
			get
			{
                if (SystemConfig.IsBSsystem_Test)
                {
                    OracleConnection conn = HttpContext.Current.Session["DBAccessOfOracle"] as OracleConnection;
                    if (conn == null)
                    {
                        conn = new OracleConnection(SystemConfig.DBAccessOfOracle);
                        conn.Open();
                        HttpContext.Current.Session["DBAccessOfOracle"] = conn;
                    }
                    return conn;
                }
                else
                {
                    OracleConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfOracle"] as OracleConnection;
                    if (conn == null)
                    {
                        conn = new OracleConnection(SystemConfig.DBAccessOfOracle);
                        SystemConfig.CS_DBConnctionDic["DBAccessOfOracle"] = conn;
                        conn.Open();
                    }
                    else
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                    }
                    return conn;
                }
			}
		}
		#endregion ȡ�����Ӷ��� ��CS��BS��������

		#region ���� RunSQLReturnTable
		/// <summary>
		/// ����sql����table.
		/// </summary>
		/// <param name="sql">sql</param>
		/// <returns>���صĽ������</returns>
        public static DataTable RunSQLReturnTable(string sql)
        {
            return DBAccess.RunSQLReturnTable(sql, GetSingleConn,
                CommandType.Text,
                SystemConfig.DBAccessOfOracle);
        }
		/// <summary>
		/// ����sql����table.
		/// </summary>
		/// <param name="sql">sql</param>
		/// <param name="sqlType">CommandType</param>
		/// <param name="pars">para</param>
		/// <returns>���صĽ������</returns>
		public static DataTable RunSQLReturnTable(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQLReturnTable( sql , GetSingleConn ,sqlType , SystemConfig.DBAccessOfOracle  );
		}
		#endregion

		#region ���� RunSQL
		public static int RunSQLTRUNCATETable(string table)
		{
			return DBAccess.RunSQL( "  TRUNCATE TABLE "+table  );
		}
		/// <summary>
		/// ����SQL
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public static int RunSQL(string sql )
		{
			return DBAccess.RunSQL( sql , GetSingleConn ,CommandType.Text, SystemConfig.DBAccessOfOracle  );
		}
		#endregion 
				
		#region ִ��SQL ��������������
		/// <summary>
		/// ����sql ����һ��object .
		/// </summary>
		/// <param name="sql"></param>
		/// <returns>object</returns>
		public static object RunSQLReturnVal(string sql )
		{
            return DBAccessOfOracle.RunSQLReturnTable(sql).Rows[0][0];
		}
		/// <summary>
		/// run sql return object values
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
        public static int RunSQLReturnIntVal(string sql)
        {
            try
            {
                return Convert.ToInt32(DBAccessOfOracle.RunSQLReturnVal(sql));
            }
            catch (Exception ex)
            {
                throw new Exception("RunSQLReturnIntVal 9i =" + ex.Message + " str = "+sql);
            }
        }
		/// <summary>
		/// run sql return float values.
		/// </summary>
		/// <param name="sql">will run sql</param>
		/// <returns>values</returns>
		public static float RunSQLReturnFloatVal(string sql )
		{

			object obj=DBAccessOfOracle.RunSQLReturnVal( sql );

			if (obj.ToString()=="System.DBNull")
				throw new Exception("@ִ�з���DBAccessOfOracle.RunSQLReturnFloatVal(sql)����,���еĽ����null.����sql="+sql);

			try
			{
				return float.Parse(obj.ToString());
			}
			catch 
			{
				throw new Exception("@ִ�з���DBAccessOfOracle.RunSQLReturnFloatVal(sql)����,���еĽ����["+obj.ToString()+"].������float ת��,����sql="+sql);
			}
		}
		/// <summary>
		/// ����sql ����float
		/// </summary>
		/// <param name="sql">Ҫ���е�sql</param>
		/// <param name="isNullAsVal">�����Null, ���ص���Ϣ.</param>
		/// <returns></returns>
		public static float RunSQLReturnFloatVal(string sql, float isNullAsVal)
		{
			object obj=DBAccessOfOracle.RunSQLReturnVal( sql );
			try
			{
				System.DBNull dbnull=(System.DBNull)obj;
				return isNullAsVal;
			}
			catch
			{
			}		

			try
			{
				return float.Parse(obj.ToString());
			}
			catch 
			{
				throw new Exception("@ִ�з���DBAccessOfOracle.RunSQLReturnFloatVal(sql,isNullAsVal)����,���еĽ����["+obj+"].������float ת��,����sql="+sql);
			}
		}
		/// <summary>
		/// run sql return decimal val
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public static decimal RunSQLReturnDecimalVal(string sql )
		{			
			object obj=DBAccessOfOracle.RunSQLReturnVal( sql );
			if (obj==null)
				throw new Exception("@ִ�з���DBAccessOfOracle.RunSQLReturnDecimalVal()����,���еĽ����null.����sql="+sql);
			try				
			{
				return decimal.Parse(obj.ToString());
			}
			catch 
			{
				throw new Exception("@ִ�з���DBAccessOfOracle.RunSQLReturnDecimalVal()����,���еĽ����["+obj+"].������decimal ת��,����sql="+sql);
			}
		}
		/// <summary>
		/// run sql return decimal.
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="isNullAsVal"></param>
		/// <returns></returns>
		public static decimal RunSQLReturnDecimalVal(string sql, decimal isNullAsVal )
		{
			object obj=DBAccessOfOracle.RunSQLReturnVal( sql );
			try
			{
				System.DBNull dbnull=(System.DBNull)obj;
				return isNullAsVal;
			}
			catch
			{
			}		

			try
			{
				return decimal.Parse(obj.ToString());
			}
			catch 
			{
				throw new Exception("@ִ�з���DBAccessOfOracle.RunSQLReturnDecimalVal(sql,isNullAsVal)����,���еĽ����["+obj+"].������float ת��,����sql="+sql);
			}
		}
		#endregion ִ��SQL ��������������
	 
	}
	/// <summary>
	/// Oracle �ķ���
	/// </summary>
    public class DBAccessOfOracle
    {

        #region �������д洢����

        #region ִ�д洢���̷���Ӱ�����
        /// <summary>
        /// ���д洢����
        /// </summary>
        /// <param name="spName">����</param>
        /// <returns>����Ӱ�������</returns>
        public static int RunSP(string spName)
        {
            return DBProcedure.RunSP(spName, DBAccessOfOracle1.GetSingleConn);
        }
        /// <summary>
        /// ���д洢����
        /// </summary>
        /// <param name="spName">����</param>
        /// <param name="paras">����</param>
        /// <returns>����Ӱ�������</returns>
        public static int RunSP(string spName, Paras paras)
        {
            return DBProcedure.RunSP(spName, paras, DBAccessOfOracle1.GetSingleConn);
        }
        public static int RunSP(string spName, string para, string paraVal)
        {
            Paras paras = new Paras();
            Para p = new Para(para, DbType.String, paraVal);
            paras.Add(p);
            return DBProcedure.RunSP(spName, paras, DBAccessOfOracle1.GetSingleConn);
        }
        #endregion

        #region ���д洢���̷��� DataTable
        /// <summary>
        /// ���д洢����
        /// </summary>
        /// <param name="spName">����</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSPReTable(string spName)
        {
            return DBProcedure.RunSPReturnDataTable(spName, DBAccessOfOracle1.GetSingleConn);
        }
        /// <summary>
        /// ���д洢����
        /// </summary>
        /// <param name="spName">����</param>
        /// <param name="paras">����</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSPReTable(string spName, Paras paras)
        {
            return DBProcedure.RunSPReturnDataTable(spName, paras, DBAccessOfOracle1.GetSingleConn);
        }
        #endregion

        #endregion


        /// <summary>
        /// ����ǲ��Ǵ���
        /// </summary>
        /// <param name="selectSQL"></param>
        /// <returns>����ǲ��Ǵ���</returns>
        public static bool IsExits(string selectSQL)
        {
            if (RunSQLReturnVal(selectSQL) == null)
                return false;
            return true;
        }

        #region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
        public static OracleConnection GetSingleConn
        {
            get
            {
                if (SystemConfig.IsBSsystem_Test)
                {
                    OracleConnection conn = HttpContext.Current.Session["DBAccessOfOracle1"] as OracleConnection;
                    if (conn == null)
                    {
                        conn = new OracleConnection(SystemConfig.DBAccessOfOracle1);
                        conn.Open();
                        HttpContext.Current.Session["DBAccessOfOracle1"] = conn;
                    }
                    return conn;
                }
                else
                {
                    OracleConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfOracle1"] as OracleConnection;
                    if (conn == null)
                    {
                        conn = new OracleConnection(SystemConfig.DBAccessOfOracle1);
                        SystemConfig.CS_DBConnctionDic["DBAccessOfOracle1"] = conn;
                        conn.Open();
                    }
                    else
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                    }

                    return conn;
                }
            }
        }
        #endregion ȡ�����Ӷ��� ��CS��BS��������

        #region ���� RunSQLReturnTable
        /// <summary>
        /// ����sql����table.
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>���صĽ������</returns>
        public static DataTable RunSQLReturnTable(string sql)
        {
            return DBAccess.RunSQLReturnTable(sql, GetSingleConn, CommandType.Text, SystemConfig.DBAccessOfOracle1);
        }
        /// <summary>
        /// ����sql����table.
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">para</param>
        /// <returns>���صĽ������</returns>
        public static DataTable RunSQLReturnTable(string sql, CommandType sqlType, params object[] pars)
        {
            return DBAccess.RunSQLReturnTable(sql, GetSingleConn, sqlType, SystemConfig.DBAccessOfOracle1);
        }
        #endregion

        #region ���� RunSQL
        public static int RunSQLTRUNCATETable(string table)
        {
            return DBAccess.RunSQL("  TRUNCATE TABLE " + table);
        }
        /// <summary>
        /// ����SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int RunSQL(string sql)
        {
            return DBAccess.RunSQL(sql, GetSingleConn, CommandType.Text, SystemConfig.DBAccessOfOracle1);
        }
        /// <summary>
        /// ���� SQL
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">����</param>
        /// <returns>�����</returns>
        public static int RunSQL(string sql, CommandType sqlType, params object[] pars)
        {
            return DBAccess.RunSQL(sql, GetSingleConn, sqlType, SystemConfig.DBAccessOfOracle1);
        }
        #endregion

        #region ִ��SQL ��������������
        /// <summary>
        /// ����sql ����һ��object .
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>object</returns>
        public static object RunSQLReturnVal(string sql)
        {
            return DBAccess.RunSQLReturnTable(sql).Rows[0][0];

            //return DBAccess.RunSQLReturnVal(sql,GetSingleConn, SystemConfig.DBAccessOfOracle1);
        }
        /// <summary>
        /// run sql return object values
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int RunSQLReturnIntVal(string sql)
        {
            //return (DBAccessOfOracle1.RunSQLReturnVal( sql );
            string str = DBAccessOfOracle1.RunSQLReturnVal(sql).ToString();
            try
            {
                return int.Parse(str);
            }
            catch (Exception ex)
            {
                throw new Exception("RunSQLReturnIntVal 9i =" + ex.Message + " str = " + str);
            }
        }
        /// <summary>
        /// run sql return float values.
        /// </summary>
        /// <param name="sql">will run sql</param>
        /// <returns>values</returns>
        public static float RunSQLReturnFloatVal(string sql)
        {

            object obj = DBAccessOfOracle1.RunSQLReturnVal(sql);

            if (obj.ToString() == "System.DBNull")
                throw new Exception("@ִ�з���DBAccessOfOracle1.RunSQLReturnFloatVal(sql)����,���еĽ����null.����sql=" + sql);

            try
            {
                return float.Parse(obj.ToString());
            }
            catch
            {
                throw new Exception("@ִ�з���DBAccessOfOracle1.RunSQLReturnFloatVal(sql)����,���еĽ����[" + obj.ToString() + "].������float ת��,����sql=" + sql);
            }
        }
        /// <summary>
        /// ����sql ����float
        /// </summary>
        /// <param name="sql">Ҫ���е�sql</param>
        /// <param name="isNullAsVal">�����Null, ���ص���Ϣ.</param>
        /// <returns></returns>
        public static float RunSQLReturnFloatVal(string sql, float isNullAsVal)
        {
            object obj = DBAccessOfOracle1.RunSQLReturnVal(sql);
            try
            {
                System.DBNull dbnull = (System.DBNull)obj;
                return isNullAsVal;
            }
            catch
            {
            }

            try
            {
                return float.Parse(obj.ToString());
            }
            catch
            {
                throw new Exception("@ִ�з���DBAccessOfOracle1.RunSQLReturnFloatVal(sql,isNullAsVal)����,���еĽ����[" + obj + "].������float ת��,����sql=" + sql);
            }
        }
        /// <summary>
        /// run sql return decimal val
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static decimal RunSQLReturnDecimalVal(string sql)
        {
            object obj = DBAccessOfOracle1.RunSQLReturnVal(sql);
            if (obj == null)
                throw new Exception("@ִ�з���DBAccessOfOracle1.RunSQLReturnDecimalVal()����,���еĽ����null.����sql=" + sql);
            try
            {
                return decimal.Parse(obj.ToString());
            }
            catch
            {
                throw new Exception("@ִ�з���DBAccessOfOracle1.RunSQLReturnDecimalVal()����,���еĽ����[" + obj + "].������decimal ת��,����sql=" + sql);
            }
        }
        /// <summary>
        /// run sql return decimal.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="isNullAsVal"></param>
        /// <returns></returns>
        public static decimal RunSQLReturnDecimalVal(string sql, decimal isNullAsVal)
        {
            object obj = DBAccessOfOracle1.RunSQLReturnVal(sql);
            try
            {
                System.DBNull dbnull = (System.DBNull)obj;
                return isNullAsVal;
            }
            catch
            {
            }

            try
            {
                return decimal.Parse(obj.ToString());
            }
            catch
            {
                throw new Exception("@ִ�з���DBAccessOfOracle1.RunSQLReturnDecimalVal(sql,isNullAsVal)����,���еĽ����[" + obj + "].������float ת��,����sql=" + sql);
            }
        }
        #endregion ִ��SQL ��������������
    }
}
 