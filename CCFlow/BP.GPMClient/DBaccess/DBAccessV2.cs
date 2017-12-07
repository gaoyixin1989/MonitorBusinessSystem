
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

namespace CCPortal.DA
{
	/// <summary>
	/// ���ݿ���ʡ�
	/// ����ฺ������ ʵ����Ϣ
	/// </summary>
    public class DBAccess
    {

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
        public static CCPortal.DA.ConnOfOras HisConnOfOras = null;
        public static CCPortal.DA.ConnOfSQLs HisConnOfSQLs = null;
        public static CCPortal.DA.ConnOfOLEs HisConnOfOLEs = null;
        public static CCPortal.DA.ConnOfMySQLs HisConnOfMySQLs = null;
        public static CCPortal.DA.ConnOfInformixs HisConnOfInformix = null;
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
        private static bool lock_OID_CfgKey = false;
        /// <summary>
        /// ����Ψһ�����к�
        /// </summary>
        /// <param name="cfgKey">������Ϣ</param>
        /// <returns>Ψһ�����к�</returns>
        public static Int64 GenerOID(string cfgKey)
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
            sql = "SELECT  IntVal FROM Sys_Serial WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            num = DBAccess.RunSQLReturnValInt(sql, ps);
            lock_OID_CfgKey = false;
            return num;
        }
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

        #region ���Ȩ��
        /// <summary>
        /// ���session . ��Ҫ���ж��ǲ������û���½��Ϣ��
        /// </summary>
        public static void DoCheckSession()
        {
            if (HttpContext.Current != null && SystemConfig.IsDebug == false)
            {
                HttpContext.Current.Session["url"] = HttpContext.Current.Request.RawUrl;
                string str = "���ĵ�½ʱ��̫���������µ�½��";
                HttpContext.Current.Session["info"] = str;
                System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath + SystemConfig.PageOfLostSession, true);
                //System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Portal/ErrPage.aspx");
            }
        }
        #endregion

        #region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
        public static object GetAppCenterDBConn
        {
            get
            {
                string connstr = CCPortal.SystemConfig.AppCenterDSN;
                switch (AppCenterDBType)
                {
                    case DBType.MSSQL:
                        if (HisConnOfSQLs == null)
                        {
                            HisConnOfSQLs = new ConnOfSQLs();
                            HisConnOfSQLs.Init();
                        }
                        return HisConnOfSQLs.GetOne();
                    case DBType.Oracle:
                        if (HisConnOfOras == null)
                        {
                            HisConnOfOras = new ConnOfOras();
                            HisConnOfOras.Init();
                        }
                        return HisConnOfOras.GetOneV2();
                    case DBType.MySQL:
                        if (HisConnOfOras == null)
                        {
                            HisConnOfMySQLs = new ConnOfMySQLs();
                            HisConnOfMySQLs.Init();
                        }
                        return HisConnOfMySQLs.GetOne();
                    case DBType.Informix:
                        if (HisConnOfOras == null)
                        {
                            HisConnOfInformix = new ConnOfInformixs();
                            HisConnOfInformix.Init();
                        }
                        return HisConnOfInformix.GetOne();
                    case DBType.Access:
                        if (HisConnOfOLEs == null)
                        {
                            HisConnOfOLEs = new ConnOfOLEs();
                            HisConnOfOLEs.Init();
                        }
                        return HisConnOfOLEs.GetOne();
                    default:
                        throw new Exception("����δ֪�����ݿ��������ͣ�");
                }
            }
        }

        #endregion ȡ�����Ӷ��� ��CS��BS��������
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
                sql = "DROP INDEX " + table + "." + table + "ID";
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
            object oconn = GetAppCenterDBConn;
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
            switch (CCPortal.SystemConfig.AppCenterDBType)
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
                return CCPortal.DA.DBAccess.RunSQLReturnTable(sql);
            }
            catch
            {
                sql = "SELECT * FROM Port_Emp WHERE 1=2";
                return CCPortal.DA.DBAccess.RunSQLReturnTable(sql);
            }
        }
        public static void RunSQLScript(string sqlOfScriptFilePath)
        {
            string str = CCPortal.DA.DataType.ReadTextFile(sqlOfScriptFilePath);
            string[] strs = str.Split(';');
            foreach (string s in strs)
            {
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                    continue;

                if (s.Contains("--"))
                    continue;

                if (s.Contains("/*"))
                    continue;

                CCPortal.DA.DBAccess.RunSQL(s);
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
                foreach (Para para in paras)
                {
                    SqlParameter oraP = new SqlParameter(para.ParaName, para.val);
                    cmd.Parameters.Add(oraP);
                }

                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                return i;
            }
            catch (System.Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                string msg = " RunSQL_200705_SQL (2012-11-29 add this log) SQL=" + sql + " Paras:" + paras.ToDesc() + ",�쳣��Ϣ:" + ex.Message;
                //Log.DefaultLogWriteLineInfo(msg);
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
            MySqlConnection conn = new MySqlConnection(SystemConfig.AppCenterDSN);
            int i = 0;
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = SystemConfig.AppCenterDSN;
                    conn.Open();
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                foreach (Para para in paras)
                {
                    MySqlParameter oraP = new MySqlParameter(para.ParaName, para.val);
                    cmd.Parameters.Add(oraP);
                }
                i = cmd.ExecuteNonQuery();
                cmd.Dispose();

                conn.Close();
                conn.Dispose();
                return i;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw new Exception(ex.Message+"@SQL:"+sql);
            }
            //finally
            //{
            //    conn.Close();
            //    conn.Dispose();
            //    return i;
            //}
        }
        private static int RunSQL_200705_Ora(string sql,Paras paras)
        {
            ConnOfOra connofora = (ConnOfOra)DBAccess.GetAppCenterDBConn;
            connofora.AddSQL(sql); //����

            OracleConnection conn = connofora.Conn;
            try
            {
                if (conn == null)
                    conn = new OracleConnection(SystemConfig.AppCenterDSN);

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
                HisConnOfOras.PutPool(connofora);
                return i;
            }
            catch (System.Exception ex)
            {
                HisConnOfOras.PutPool(connofora);
                if (CCPortal.SystemConfig.IsDebug)
                {
                    string msg = "RunSQL2   SQL=" + sql + ex.Message;
                    //Log.DebugWriteError(msg);
                    throw new Exception(msg);
                }
                else
                {
                    //Log.DebugWriteError(ex.Message);
                    throw new Exception(ex.Message + sql);
                }
            }
            finally
            {
                if (SystemConfig.IsBSsystem_Test == false)
                    conn.Close();
                HisConnOfOras.PutPool(connofora);
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
              //  Log.DebugWriteError(msg);
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
            //    if (CCPortal.SystemConfig.IsDebug)
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

            ConnOfInformix connofora = (ConnOfInformix)DBAccess.GetAppCenterDBConn;
            //  connofora.AddSQL(sql); //����.

            IfxConnection conn = connofora.Conn;
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
                HisConnOfInformix.PutPool(connofora);
                return i;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                HisConnOfInformix.PutPool(connofora);
                string msg = "RunSQL2   SQL=" + sql + "\r\n Message=: " + ex.Message;
                //Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
                HisConnOfInformix.PutPool(connofora);
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

                if (SystemConfig.IsBSsystem_Test == false)
                    conn.Close();

                return oratb;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + " [RunSQLReturnTable on OracleConnection dsn=App ] sql=" + oraSQL + "<br>");
            }
            finally
            {
                // oraconn.Close();
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
            //CommandType.
            foreach (object par in pars)
            {
                msAda.SelectCommand.Parameters.AddWithValue("par", par);
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
            ConnOfOra connofObj = GetAppCenterDBConn as ConnOfOra;
            connofObj.AddSQL(selectSQL);
            OracleConnection conn = connofObj.Conn;
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
                HisConnOfOras.PutPool(connofObj);
                return oratb;
            }
            catch (System.Exception ex)
            {
                HisConnOfOras.PutPool(connofObj);
                string msg = "@���в�ѯ��(RunSQLReturnTable_200705_Ora with paras)���� sql=" + selectSQL + " @�쳣��Ϣ��" + ex.Message;

                msg += "@Para Num= " + paras.Count;
                foreach (Para pa in paras)
                {
                    msg += "@" + pa.ParaName + "=" + pa.val;
                }
              //  Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                HisConnOfOras.PutPool(connofObj);
            }
        }
        /// <summary>
        /// RunSQLReturnTable_200705_Ora
        /// </summary>
        /// <param name="selectSQL">Ҫִ�е�sql</param>
        /// <returns>����table</returns>
        private static DataTable RunSQLReturnTable_200705_Ora_del(string selectSQL)
        {
            if (selectSQL.Contains(":"))
                throw new Exception("@sql ���в�����"+selectSQL );

            ConnOfOra connofObj = GetAppCenterDBConn as ConnOfOra;
            OracleConnection conn = connofObj.Conn;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OracleDataAdapter ada = new OracleDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                HisConnOfOras.PutPool(connofObj);
                return oratb;
            }
            catch (System.Exception ex)
            {
                HisConnOfOras.PutPool(connofObj);
                string msg = "@���в�ѯ��(RunSQLReturnTable_200705_Ora NoParas)���� sql=" + selectSQL + " @�쳣��Ϣ��" + ex.Message;
                //Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                HisConnOfOras.PutPool(connofObj);
            }
        }
        /// <summary>
        /// RunSQLReturnTable_200705_SQL
        /// </summary>
        /// <param name="selectSQL">Ҫִ�е�sql</param>
        /// <returns>����table</returns>
        private static DataTable RunSQLReturnTable_200705_SQL_bak(string selectSQL, Paras paras)
        {
            ConnOfSQL connofObj = GetAppCenterDBConn as ConnOfSQL;
            connofObj.AddSQL(selectSQL);
            SqlConnection conn = connofObj.Conn;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlDataAdapter ada = new SqlDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;

                // �������
                foreach (Para para in paras)
                {
                    SqlParameter myParameter = new SqlParameter(para.ParaName, para.val);
                    myParameter.Size = para.Size;
                    ada.SelectCommand.Parameters.Add(myParameter);
                }

                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();

                HisConnOfSQLs.PutPool(connofObj);
                return oratb;
            }
            catch (System.Exception ex)
            {
                HisConnOfSQLs.PutPool(connofObj);
                string msg = "@���в�ѯ��(RunSQLReturnTable_200705_SQL with paras)���� sql=" + selectSQL + " @�쳣��Ϣ��" + ex.Message;

                msg += "@Para Num= " + paras.Count;
                foreach (Para pa in paras)
                {
                    msg += "@" + pa.ParaName + "=" + pa.val;
                }
             //   Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                HisConnOfSQLs.PutPool(connofObj);
            }
        }
        private static DataTable RunSQLReturnTable_200705_SQL(string selectSQL)
        {
            ConnOfSQL connofObj = GetAppCenterDBConn as ConnOfSQL;
            SqlConnection conn = connofObj.Conn;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlDataAdapter ada = new SqlDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                HisConnOfSQLs.PutPool(connofObj);
                return oratb;
            }
            catch (System.Exception ex)
            {
                HisConnOfSQLs.PutPool(connofObj);
                string msgErr = ex.Message;
                //if (msgErr.Contains("DataReader"))
                //{
                //    /*�������ӵ�����*/
                //    conn.Close();
                //    return RunSQLReturnTable_200705_SQL(selectSQL);
                //}
                string msg = "@���в�ѯ��(RunSQLReturnTable_200705_SQL)���� sql=" + selectSQL + " @�쳣��Ϣ��" + msgErr;
                //Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            //finally
            //{
            //    conn.Close();
            //    HisConnOfSQLs.PutPool(connofObj);
            //}
            //return RunSQLReturnTable_200705_SQL(selectSQL);
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
                //Log.DefaultLogWriteLineError(sql);
                //Log.DefaultLogWriteLineError(ex.Message);

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
              //  Log.DebugWriteError(msg);
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
                //Log.DefaultLogWriteLineError(ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// ����һ��select ��䷵��һ����ѯDataSet����
        /// </summary>
        /// <param name="sql">select sql</param>
        /// <returns>��ѯ�������DataSet</returns>
        public static DataSet RunSQLReturnDataSet(string sql)
        {
            DataTable dt = RunSQLReturnTable(sql);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
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
        /// sdfsd
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static float RunSQLReturnValFloat(string sql, float val)
        {
            return RunSQLReturnValFloat(sql, new Paras(), val);
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
        public static string RunSQLReturnStringIsNull(string sql,string isNullAsVal)
        {
            try
            {
                string s= RunSQLReturnString(sql, new Paras());
                if (s == null )
                    return isNullAsVal;
                return s;
            }
            catch (Exception ex)
            {
                //Log.DebugWriteInfo("RunSQLReturnStringIsNull@" + ex.Message);
                return isNullAsVal;
            }
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

             //   CCPortal.DA.Log.DebugWriteInfo("@SQL="+sql+" . ��="+cols);
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
            CCPortal.DA.Paras ps = new Paras();
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
                    sql = "SELECT column_name, table_name, CONSTRAINT_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE table_name =@Tab ";
                    break;
                case DBType.Informix:
                    sql = "SELECT * FROM sysconstraints c inner join systables t on c.tabid = t.tabid WHERE t.tabname = lower(?) and constrtype = 'P'";
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
        /// �ж�ϵͳ���Ƿ���ڶ���.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool IsExitsObject(string obj)
        {
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
                    return IsExits("select tabname from systables WHERE tabname = '" + obj.ToLower() + "'");
                case DBType.MySQL:
                    if (obj.IndexOf(".") != -1)
                        obj = obj.Split('.')[1];
                    return IsExits("SELECT table_name, table_type FROM information_schema.tables  WHERE table_name = '" + obj + "'");
                case DBType.Access:
                    //return false ; //IsExits("SELECT * FROM MSysObjects WHERE (((MSysObjects.Name) =  '"+obj+"' ))");
                    return IsExits("SELECT * FROM MSysObjects WHERE Name =  '" + obj + "'");
                default:
                    throw new Exception("û��ʶ������ݿ���");
            }
        }

//        public static bool ss( string tbname, k)
//        {
//'���ܣ���ȡ���ݱ�����ֶ�����
//'������tbname---������ktype---�����ͣ�1Ϊ������2Ϊ�����3ΪΨһ����
    
    


//Dim cnn As New ADODB.Connection
//Dim cat As New ADOX.Catalog
//Dim tbl As ADOX.Table
//Dim i As Long, j As Long
//Set cnn = CurrentProject.Connection
//Set cat.ActiveConnection = cnn
//Set tbl = cat.Tables(tbname)
//For i = 0 To tbl.Keys.Count - 1
//    If tbl.Keys(i).Type = ktype Then
//    For j = 0 To tbl.Keys(i).Columns.Count - 1
//    Keyname = Keyname & tbl.Keys(i).Columns(j).Name & ";"
//    Next
//    End If
//Next
//End Function  
//        }
        
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
                case DBType.MSSQL:
                    i = DBAccess.RunSQLReturnValInt("SELECT  COUNT(*)  FROM information_schema.COLUMNS  WHERE TABLE_NAME='"+table+"' AND COLUMN_NAME='"+col+"'", 0);
                    break;
                case DBType.MySQL:
                    i = DBAccess.RunSQLReturnValInt("SELECT  COUNT(*)  FROM information_schema.COLUMNS  WHERE TABLE_NAME='" + table + "' AND COLUMN_NAME='" + col + "'", 0);
                    break;
                case DBType.Oracle:
                    if (table.IndexOf(".") != -1)
                        table = table.Split('.')[1];
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) from user_tab_columns  WHERE table_name= upper(:tab) AND column_name= upper(:col) ", ps);
                    break;
                case DBType.Informix:
                    i = DBAccess.RunSQLReturnValInt("select count(*) from syscolumns c WHERE tabid in (select tabid	from systables	where tabname = lower('" + table + "')) and c.colname = lower('" + col + "')", 0);
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

          //  CCPortal.SystemConfig.CS_AppSettings = new System.Collections.Specialized.NameValueCollection();
            CCPortal.SystemConfig.CS_DBConnctionDic.Clear();
            foreach (DataRow row in dscfg.Tables["add"].Rows)
            {
                CCPortal.SystemConfig.CS_AppSettings.Add(row["key"].ToString().Trim(), row["value"].ToString().Trim());
            }
            dscfg.Dispose();

            CCPortal.SystemConfig.IsBSsystem = false;
        }


        #endregion
    }

	 
 
	 
}
 