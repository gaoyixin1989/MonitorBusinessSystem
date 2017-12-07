using System;
using System.Data;
using System.Collections;
using System.Text;
using BP.DA;
using BP.En;
using MySql.Data.MySqlClient;

namespace BP.Sys
{
    /// <summary>
    /// ����Դ����
    /// </summary>
    public enum DBSrcType
    {
        /// <summary>
        /// �������ݿ�
        /// </summary>
        Localhost,
        /// <summary>
        /// SQL
        /// </summary>
        SQLServer,
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle,
        /// <summary>
        /// MySQL
        /// </summary>
        MySQL,
        /// <summary>
        /// Infomax
        /// </summary>
        Infomax
    }
    /// <summary>
    /// ����Դ
    /// </summary>
    public class SFDBSrcAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ����Դ����
        /// </summary>
        public const string DBSrcType = "DBSrcType";
        /// <summary>
        /// �û����
        /// </summary>
        public const string UserID = "UserID";
        /// <summary>
        /// ����
        /// </summary>
        public const string Password = "Password";
        /// <summary>
        /// IP��ַ
        /// </summary>
        public const string IP = "IP";
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public const string DBName = "DBName";
    }
    /// <summary>
    /// ����Դ
    /// </summary>
    public class SFDBSrc : EntityNoName
    {
        #region ����
        /// <summary>
        /// �Ƿ�������ʵ��?
        /// </summary>
        public string UserID
        {
            get
            {
                return this.GetValStringByKey(SFDBSrcAttr.UserID);
            }
            set
            {
                this.SetValByKey(SFDBSrcAttr.UserID, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Password
        {
            get
            {
                return this.GetValStringByKey(SFDBSrcAttr.Password);
            }
            set
            {
                this.SetValByKey(SFDBSrcAttr.Password, value);
            }
        }
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public string DBName
        {
            get
            {
                return this.GetValStringByKey(SFDBSrcAttr.DBName);
            }
            set
            {
                this.SetValByKey(SFDBSrcAttr.DBName, value);
            }
        }
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public DBSrcType DBSrcType
        {
            get
            {
                return (DBSrcType)this.GetValIntByKey(SFDBSrcAttr.DBSrcType);
            }
            set
            {
                this.SetValByKey(SFDBSrcAttr.DBSrcType, (int)value);
            }
        }
        public string IP
        {
            get
            {
                return this.GetValStringByKey(SFDBSrcAttr.IP);
            }
            set
            {
                this.SetValByKey(SFDBSrcAttr.IP, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����Դ
        /// </summary>
        public SFDBSrc()
        {
        }
        public SFDBSrc(string mypk)
        {
            this.No = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_SFDBSrc");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "����Դ";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(SFDBSrcAttr.No, null, "����Դ���(������Ӣ��)", true, false, 1, 20, 20);
                map.AddTBString(SFDBSrcAttr.Name, null, "����Դ����", true, false, 0, 30, 20);

                map.AddTBString(SFDBSrcAttr.UserID, null, "���ݿ��¼�û�ID", true, false, 0, 30, 20);
                map.AddTBString(SFDBSrcAttr.Password, null, "���ݿ��¼�û�����", true, false, 0, 30, 20);
                map.AddTBString(SFDBSrcAttr.IP, null, "IP��ַ", true, false, 0, 50, 20);
                map.AddTBString(SFDBSrcAttr.DBName, null, "���ݿ�����", true, false, 0, 30, 20);

                map.AddDDLSysEnum(SFDBSrcAttr.DBSrcType, 0, "����Դ����", true, true,
                    SFDBSrcAttr.DBSrcType, "@0=Ӧ��ϵͳ�����ݿ�@1=SQLServer@2=Oracle@3=MySQL@4=Infomix");

                RefMethod rm = new RefMethod();
                rm = new RefMethod();
                rm.Title = "��������";
                rm.ClassMethodName = this.ToString() + ".DoConn";
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        /// <summary>
        /// ִ������
        /// </summary>
        /// <returns></returns>
        public string DoConn()
        {
            if (this.No == "local")
                return "�������Ӳ���Ҫ�����Ƿ����ӳɹ�.";

            if (this.DBSrcType == Sys.DBSrcType.Localhost)
                throw new Exception("@�ڸ�ϵͳ��ֻ����һ����������.");

            string dsn = "";
            if (this.DBSrcType == Sys.DBSrcType.SQLServer)
            {
                try
                {
                    dsn = "Password=" + this.Password + ";Persist Security Info=True;User ID=" + this.UserID + ";Initial Catalog=" + this.DBName + ";Data Source=" + this.IP + ";Timeout=999;MultipleActiveResultSets=true";
                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                    conn.ConnectionString = dsn;
                    conn.Open();
                    conn.Close();

                    //ɾ��Ӧ��.
                    try
                    {
                        BP.DA.DBAccess.RunSQL("Exec sp_droplinkedsrvlogin " + this.No + ",Null ");
                        BP.DA.DBAccess.RunSQL("Exec sp_dropserver " + this.No);
                    }
                    catch
                    {
                    }

                    //����Ӧ��.
                    string sql = "";
                    sql += "sp_addlinkedserver @server='" + this.No + "', @srvproduct='', @provider='SQLOLEDB', @datasrc='" + this.IP + "'";
                    BP.DA.DBAccess.RunSQL(sql);

                    //ִ�е�¼.
                    sql = "";
                    sql += " EXEC sp_addlinkedsrvlogin '" + this.No + "','false', NULL, '" + this.UserID + "', '" + this.Password + "'";
                    BP.DA.DBAccess.RunSQL(sql);

                    return "��ϲ������(" + this.Name + ")�������óɹ���";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            if (this.DBSrcType == Sys.DBSrcType.Oracle)
            {
                try
                {
                    dsn = "user id=" + this.UserID + ";data source=" + this.DBName + ";password=" + this.Password + ";Max Pool Size=200";
                    System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection();
                    conn.ConnectionString = dsn;
                    conn.Open();
                    conn.Close();
                    return "��ϲ������(" + this.Name + ")�������óɹ���";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            if (this.DBSrcType == Sys.DBSrcType.MySQL)
            {
                try
                {
                    dsn = "Data Source=" + this.IP + ";Persist Security info=True;Initial Catalog=" + this.DBName + ";User ID=" + this.UserID + ";Password=" + this.Password + ";";
                    MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();
                    conn.ConnectionString = dsn;
                    conn.Open();
                    conn.Close();
                    return "��ϲ������(" + this.Name + ")�������óɹ���";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            //  if (this.DBSrcType == Sys.DBSrcType.Infomax)
            //{
            //    try
            //    {
            ////Host=10.0.2.36;Service=8001;Server=niosserver; Database=ccflow; UId=npmuser; Password=npmoptr2012;Database locale=en_US.819;Client Locale=en_US.CP1252
            //        dsn = "Data Source="+this.IP+";Persist Security info=True;Initial Catalog="+this.DBName+";User ID="+this.UserID+";Password="+this.Password+";";
            //        MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();
            //        conn.ConnectionString = dsn;
            //        conn.Open();
            //        conn.Close();
            //        return "��ϲ������(" + this.Name + ")�������óɹ���";
            //    }
            //    catch (Exception ex)
            //    {
            //        return ex.Message;
            //    }
            //}
            return "û���漰�������Ӳ�������...";
        }
        /// <summary>
        /// ��������б�.
        /// </summary>
        /// <returns></returns>
        public DataTable GetTables()
        {
            var sql = new StringBuilder();
            sql.AppendFormat("SELECT ss.SrcTable FROM Sys_SFTable ss WHERE ss.FK_SFDBSrc = '{0}'", this.No);

            var allTablesExist = DBAccess.RunSQLReturnTable(sql.ToString());
            
            sql.Clear();

            var dbType = this.DBSrcType;
            if (dbType == Sys.DBSrcType.Localhost)
            {
                switch (SystemConfig.AppCenterDBType)
                {
                    case DBType.MSSQL:
                        dbType = Sys.DBSrcType.SQLServer;
                        break;
                    case DBType.Oracle:
                        dbType = Sys.DBSrcType.Oracle;
                        break;
                    case DBType.MySQL:
                        dbType = Sys.DBSrcType.MySQL;
                        break;
                    case DBType.Informix:
                        dbType = Sys.DBSrcType.Infomax;
                        break;
                    default:
                        throw new Exception("û���漰�������Ӳ�������...");
                }
            }

            switch (dbType)
            {
                case Sys.DBSrcType.SQLServer:
                    sql.AppendLine("SELECT NAME AS No,");
                    sql.AppendLine("       [Name] = '[' + (CASE xtype WHEN 'U' THEN '��' ELSE '��ͼ' END) + '] ' + ");
                    sql.AppendLine("       NAME,");
                    sql.AppendLine("       xtype");
                    sql.AppendLine("FROM   sysobjects");
                    sql.AppendLine("WHERE  (xtype = 'U' OR xtype = 'V')");
                    //   sql.AppendLine("       AND (NAME NOT LIKE 'ND%')");
                    sql.AppendLine("       AND (NAME NOT LIKE 'Demo_%')");
                    sql.AppendLine("       AND (NAME NOT LIKE 'Sys_%')");
                    sql.AppendLine("       AND (NAME NOT LIKE 'WF_%')");
                    sql.AppendLine("       AND (NAME NOT LIKE 'GPM_%')");
                    sql.AppendLine("ORDER BY");
                    sql.AppendLine("       xtype,");
                    sql.AppendLine("       NAME");
                    break;
                case Sys.DBSrcType.Oracle:
                    sql.AppendLine("SELECT uo.OBJECT_NAME AS No,");
                    sql.AppendLine("       '[' || (CASE uo.OBJECT_TYPE");
                    sql.AppendLine("         WHEN 'TABLE' THEN");
                    sql.AppendLine("          '��'");
                    sql.AppendLine("         ELSE");
                    sql.AppendLine("          '��ͼ'");
                    sql.AppendLine("       END) || '] ' || uo.OBJECT_NAME AS Name,");
                    sql.AppendLine("       CASE uo.OBJECT_TYPE");
                    sql.AppendLine("         WHEN 'TABLE' THEN");
                    sql.AppendLine("          'U'");
                    sql.AppendLine("         ELSE");
                    sql.AppendLine("          'V'");
                    sql.AppendLine("       END AS xtype");
                    sql.AppendLine("  FROM user_objects uo");
                    sql.AppendLine(" WHERE (uo.OBJECT_TYPE = 'TABLE' OR uo.OBJECT_TYPE = 'VIEW')");
                    //sql.AppendLine("   AND uo.OBJECT_NAME NOT LIKE 'ND%'");
                    sql.AppendLine("   AND uo.OBJECT_NAME NOT LIKE 'Demo_%'");
                    sql.AppendLine("   AND uo.OBJECT_NAME NOT LIKE 'Sys_%'");
                    sql.AppendLine("   AND uo.OBJECT_NAME NOT LIKE 'WF_%'");
                    sql.AppendLine("   AND uo.OBJECT_NAME NOT LIKE 'GPM_%'");
                    sql.AppendLine(" ORDER BY uo.OBJECT_TYPE, uo.OBJECT_NAME");
                    break;
                case Sys.DBSrcType.MySQL:
                    sql.AppendLine("SELECT ");
                    sql.AppendLine("    table_name AS No,");
                    sql.AppendLine("    CONCAT('[',");
                    sql.AppendLine("            CASE table_type");
                    sql.AppendLine("                WHEN 'BASE TABLE' THEN '��'");
                    sql.AppendLine("                ELSE '��ͼ'");
                    sql.AppendLine("            END,");
                    sql.AppendLine("            '] ',");
                    sql.AppendLine("            table_name) AS Name,");
                    sql.AppendLine("    CASE table_type");
                    sql.AppendLine("        WHEN 'BASE TABLE' THEN 'U'");
                    sql.AppendLine("        ELSE 'V'");
                    sql.AppendLine("    END AS xtype");
                    sql.AppendLine("FROM");
                    sql.AppendLine("    information_schema.tables");
                    sql.AppendLine("WHERE");
                    sql.AppendLine(string.Format("    table_schema = '{0}'", this.DBSrcType == Sys.DBSrcType.Localhost ? DBAccess.GetAppCenterDBConn.Database : this.DBName));
                    sql.AppendLine("        AND (table_type = 'BASE TABLE'");
                    sql.AppendLine("        OR table_type = 'VIEW')");
                    //   sql.AppendLine("       AND (table_name NOT LIKE 'ND%'");
                    sql.AppendLine("        AND table_name NOT LIKE 'Demo_%'");
                    sql.AppendLine("        AND table_name NOT LIKE 'Sys_%'");
                    sql.AppendLine("        AND table_name NOT LIKE 'WF_%'");
                    sql.AppendLine("        AND table_name NOT LIKE 'GPM_%'");
                    sql.AppendLine("ORDER BY table_type , table_name;");
                    break;
                case Sys.DBSrcType.Infomax:
                    sql.AppendLine("");
                    break;
                default:
                    break;
            }

            DataTable allTables = null;
            if (this.No == "local")
            {
                allTables = DBAccess.RunSQLReturnTable(sql.ToString());
            }
            else
            {
                var dsn = GetDSN();
                var conn = GetConnection(dsn);

                try
                {
                    //System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                    //conn.ConnectionString = dsn;
                    conn.Open();

                    allTables = RunSQLReturnTable(sql.ToString(), conn, dsn, CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw new Exception("@ʧ��:" + ex.Message + " dns:" + dsn);
                }
            }

            //ȥ���Ѿ�ʹ�õı�
            var filter = string.Empty;
            foreach (DataRow dr in allTablesExist.Rows)
                filter += string.Format("No='{0}' OR ", dr[0]);

            if (filter != "")
            {
                var deletedRows = allTables.Select(filter.TrimEnd(" OR ".ToCharArray()));
                foreach (DataRow dr in deletedRows)
                {
                    allTables.Rows.Remove(dr);
                }
            }

            return allTables;
        }

        /// <summary>
        /// ��ȡ�����ַ���
        /// <para></para>
        /// <para>added by liuxc,2015-6-9</para>
        /// </summary>
        /// <returns></returns>
        private string GetDSN()
        {
            string dsn = "";

            var dbType = this.DBSrcType;
            if (dbType == Sys.DBSrcType.Localhost)
            {
                switch (SystemConfig.AppCenterDBType)
                {
                    case DBType.MSSQL:
                        dbType = Sys.DBSrcType.SQLServer;
                        break;
                    case DBType.Oracle:
                        dbType = Sys.DBSrcType.Oracle;
                        break;
                    case DBType.MySQL:
                        dbType = Sys.DBSrcType.MySQL;
                        break;
                    case DBType.Informix:
                        dbType = Sys.DBSrcType.Infomax;
                        break;
                    default:
                        throw new Exception("û���漰�������Ӳ�������...");
                }
            }

            switch (dbType)
            {
                case Sys.DBSrcType.SQLServer:
                    dsn = "Password=" + this.Password + ";Persist Security Info=True;User ID=" + this.UserID +
                      ";Initial Catalog=" + this.DBName + ";Data Source=" + this.IP +
                      ";Timeout=999;MultipleActiveResultSets=true";
                    break;
                case Sys.DBSrcType.Oracle:
                    dsn = "user id=" + this.UserID + ";data source=" + this.DBName + ";password=" + this.Password + ";Max Pool Size=200";
                    break;
                case Sys.DBSrcType.MySQL:
                    dsn = "Data Source=" + this.IP + ";Persist Security info=True;Initial Catalog=" + this.DBName + ";User ID=" + this.UserID + ";Password=" + this.Password + ";";
                    break;
                case Sys.DBSrcType.Infomax:
                    dsn = "Provider=Ifxoledbc;Data Source=" + this.DBName + "@" + this.IP + ";User ID=" + this.UserID + ";Password=" + this.Password + ";";
                    break;
                default:
                    throw new Exception("û���漰�������Ӳ�������...");
            }

            return dsn;
        }

        /// <summary>
        /// ��ȡ���ݿ�����
        /// </summary>
        /// <param name="dsn">�����ַ���</param>
        /// <returns></returns>
        private System.Data.Common.DbConnection GetConnection(string dsn)
        {
            System.Data.Common.DbConnection conn = null;

            var dbType = this.DBSrcType;
            if (dbType == Sys.DBSrcType.Localhost)
            {
                switch (SystemConfig.AppCenterDBType)
                {
                    case DBType.MSSQL:
                        dbType = Sys.DBSrcType.SQLServer;
                        break;
                    case DBType.Oracle:
                        dbType = Sys.DBSrcType.Oracle;
                        break;
                    case DBType.MySQL:
                        dbType = Sys.DBSrcType.MySQL;
                        break;
                    case DBType.Informix:
                        dbType = Sys.DBSrcType.Infomax;
                        break;
                    default:
                        throw new Exception("û���漰�������Ӳ�������...");
                }
            }

            switch (dbType)
            {
                case Sys.DBSrcType.SQLServer:
                    conn = new System.Data.SqlClient.SqlConnection(dsn);
                    break;
                case Sys.DBSrcType.Oracle:
                    conn = new System.Data.OracleClient.OracleConnection(dsn);
                    break;
                case Sys.DBSrcType.MySQL:
                    conn = new MySql.Data.MySqlClient.MySqlConnection(dsn);
                    break;
                case Sys.DBSrcType.Infomax:
                    conn = new System.Data.OleDb.OleDbConnection(dsn);
                    break;
            }

            return conn;
        }

        private DataTable RunSQLReturnTable(string sql, System.Data.Common.DbConnection conn, string dsn, CommandType cmdType)
        {
            if (conn is System.Data.SqlClient.SqlConnection)
                return BP.DA.DBAccess.RunSQLReturnTable(sql, (System.Data.SqlClient.SqlConnection)conn, dsn, cmdType);
            if (conn is System.Data.OleDb.OleDbConnection)
                return BP.DA.DBAccess.RunSQLReturnTable(sql, (System.Data.OleDb.OleDbConnection)conn, cmdType);
            if (conn is System.Data.OracleClient.OracleConnection)
                return BP.DA.DBAccess.RunSQLReturnTable(sql, (System.Data.OracleClient.OracleConnection)conn, cmdType, dsn);
            if (conn is MySqlConnection)
            {
                var mySqlConn = (MySqlConnection)conn;
                if (mySqlConn.State != ConnectionState.Open)
                    mySqlConn.Open();

                var ada = new MySqlDataAdapter(sql, mySqlConn);
                ada.SelectCommand.CommandType = CommandType.Text;


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

            throw new Exception("û���漰�������Ӳ�������...");
            return null;
        }

        /// <summary>
        /// ��ȡ����ֶ���Ϣ
        /// </summary>
        /// <param name="tableName">��/��ͼ����</param>
        /// <returns></returns>
        public DataTable GetColumns(string tableName)
        {
            //SqlServer���ݿ�
            var sql = new StringBuilder();

            var dbType = this.DBSrcType;
            if (dbType == Sys.DBSrcType.Localhost)
            {
                switch (SystemConfig.AppCenterDBType)
                {
                    case DBType.MSSQL:
                        dbType = Sys.DBSrcType.SQLServer;
                        break;
                    case DBType.Oracle:
                        dbType = Sys.DBSrcType.Oracle;
                        break;
                    case DBType.MySQL:
                        dbType = Sys.DBSrcType.MySQL;
                        break;
                    case DBType.Informix:
                        dbType = Sys.DBSrcType.Infomax;
                        break;
                    default:
                        throw new Exception("û���漰�������Ӳ�������...");
                }
            }

            switch (dbType)
            {
                case Sys.DBSrcType.SQLServer:
                    sql.AppendLine("SELECT sc.name,");
                    sql.AppendLine("       st.name AS [type],");
                    sql.AppendLine("       (");
                    sql.AppendLine("           CASE ");
                    sql.AppendLine("                WHEN st.name = 'nchar' OR st.name = 'nvarchar' THEN sc.length / 2");
                    sql.AppendLine("                ELSE sc.length");
                    sql.AppendLine("           END");
                    sql.AppendLine("       ) AS length,");
                    sql.AppendLine("       sc.colid,");
                    sql.AppendLine("       ISNULL(ep.[value], '') AS [Desc]");
                    sql.AppendLine("FROM   dbo.syscolumns sc");
                    sql.AppendLine("       INNER JOIN dbo.systypes st");
                    sql.AppendLine("            ON  sc.xtype = st.xusertype");
                    sql.AppendLine("       LEFT OUTER JOIN sys.extended_properties ep");
                    sql.AppendLine("            ON  sc.id = ep.major_id");
                    sql.AppendLine("            AND sc.colid = ep.minor_id");
                    sql.AppendLine("            AND ep.name = 'MS_Description'");
                    sql.AppendLine(string.Format("WHERE  sc.id = OBJECT_ID('dbo.{0}')", tableName));
                    break;
                case Sys.DBSrcType.Oracle:
                    sql.AppendLine("SELECT utc.COLUMN_NAME AS Name,");
                    sql.AppendLine("       utc.DATA_TYPE   AS type,");
                    sql.AppendLine("       utc.CHAR_LENGTH AS length,");
                    sql.AppendLine("       utc.COLUMN_ID   AS colid,");
                    sql.AppendLine("       ucc.comments    AS Desc");
                    sql.AppendLine("  FROM user_tab_cols utc");
                    sql.AppendLine("  LEFT JOIN user_col_comments ucc");
                    sql.AppendLine("    ON ucc.table_name = utc.TABLE_NAME");
                    sql.AppendLine("   AND ucc.column_name = utc.COLUMN_NAME");
                    sql.AppendLine(string.Format(" WHERE utc.TABLE_NAME = '{0}'", tableName.ToUpper()));
                    sql.AppendLine(" ORDER BY colid ASC");

                    break;
                case Sys.DBSrcType.MySQL:
                    sql.AppendLine("SELECT ");
                    sql.AppendLine("    column_name AS 'name',");
                    sql.AppendLine("    data_type AS 'type',");
                    sql.AppendLine("    IFNULL(character_maximum_length,");
                    sql.AppendLine("            numeric_precision) AS length,");
                    sql.AppendLine("    ordinal_position AS colid,");
                    sql.AppendLine("    column_comment AS 'Desc'");
                    sql.AppendLine("FROM");
                    sql.AppendLine("    information_schema.columns");
                    sql.AppendLine("WHERE");
                    sql.AppendLine(string.Format("    table_schema = '{0}'", this.DBSrcType == Sys.DBSrcType.Localhost ? DBAccess.GetAppCenterDBConn.Database : this.DBName));
                    sql.AppendLine(string.Format("        AND table_name = '{0}';", tableName));
                    break;
                case Sys.DBSrcType.Infomax:
                    break;
                default:
                    throw new Exception("û���漰�������Ӳ�������...");
            }


            if (this.No == "local")
                return DBAccess.RunSQLReturnTable(sql.ToString());

            var dsn = GetDSN();
            var conn = GetConnection(dsn);

            try
            {
                //System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                //conn.ConnectionString = dsn;
                conn.Open();

                return RunSQLReturnTable(sql.ToString(), conn, dsn, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("@ʧ��:" + ex.Message + " dns:" + dsn);
            }

            return null;
        }

        protected override bool beforeDelete()
        {
            if (this.No == "local")
                throw new Exception("@Ĭ������(local)������ɾ��������.");
            return base.beforeDelete();
        }
        protected override bool beforeUpdate()
        {
            if (this.No == "local")
                throw new Exception("@Ĭ������(local)������ɾ��������.");
            return base.beforeUpdate();
        }

    }
    /// <summary>
    /// ����Դs
    /// </summary>
    public class SFDBSrcs : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// ����Դs
        /// </summary>
        public SFDBSrcs()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SFDBSrc();
            }
        }
        #endregion

        public override int RetrieveAll()
        {
            int i = this.RetrieveAllFromDBSource();
            if (i == 0)
            {
                SFDBSrc src = new SFDBSrc();
                src.No = "local";
                src.Name = "Ӧ��ϵͳ�����ݿ�(Ĭ��)";
                src.Insert();
                this.AddEntity(src);
                return 1;
            }
            return i;
        }
    }
}
