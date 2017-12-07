using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using IBM.Data.Informix;
using IBM.Data.Utilities;
using System.Data.OracleClient;
using MySql.Data;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using BP.Sys;

namespace BP.DA
{
	/// <summary>
	/// DBProcedure ��ժҪ˵����
	/// </summary>
	public class DBProcedure
	{		 
		#region �����в����� Para .
		/// <summary>
		/// ���д洢����,û��Para��
		/// ����Ӱ�����
		/// </summary>
		public static int RunSP(string spName, SqlConnection conn)
		{
			SqlCommand cmd = new SqlCommand(spName, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			if (conn.State==System.Data.ConnectionState.Closed)
			{
				conn.Open();
			}
			return cmd.ExecuteNonQuery();
		}
        public static int RunSP(string spName, MySqlConnection conn)
        {
            MySqlCommand cmd = new MySqlCommand(spName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            return cmd.ExecuteNonQuery();
        }
		public static int RunSP(string spName, OracleConnection conn)
		{
			OracleCommand cmd = new OracleCommand(spName, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			if (conn.State==System.Data.ConnectionState.Closed)
				conn.Open();

			return cmd.ExecuteNonQuery();
		}
        public static int RunSP(string spName, IfxConnection conn)
        {
            IfxCommand cmd = new IfxCommand(spName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            return cmd.ExecuteNonQuery();
        }
        public static object RunSPReturnObj(string spName, OracleConnection conn)
        {
            OracleCommand cmd = new OracleCommand(spName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            return cmd.ExecuteScalar();
        }
        public static int RunSPReturnInt(string spName, OracleConnection conn)
        {
            object obj = DBProcedure.RunSPReturnObj(spName, conn);
            if (obj == null || obj == DBNull.Value)
                throw new Exception("@SpName ����" + spName + ",���� null ֵ��");
            return int.Parse(obj.ToString());
        }
        public static float RunSPReturnFloat(string spName, OracleConnection conn)
        {
            return float.Parse(DBProcedure.RunSPReturnFloat(spName, conn).ToString());
        }
        public static decimal RunSPReturnDecimal(string spName, OracleConnection conn)
        {
            return decimal.Parse(DBProcedure.RunSPReturnDecimal(spName, conn).ToString());
        }
		#endregion

		#region ���в����� DBProcedure
        /// <summary>
        /// ���д洢����,��Para������Ӱ����С�
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="conn"></param>
        /// <param name="paras"></param>
        public static int RunSP(string spName, Paras paras, IfxConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            IfxCommand cmd = new IfxCommand(spName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // �������
            foreach (Para para in paras)
            {
                IfxParameter myParameter = new IfxParameter(para.ParaName, para.val);
                myParameter.Size = para.Size;
                cmd.Parameters.Add(myParameter);
            }

            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        
		/// <summary>
		/// ���д洢����,��Para������Ӱ����С�
		/// </summary>
		/// <param name="spName"></param>
		/// <param name="conn"></param>
		/// <param name="paras"></param>
        public static int RunSP(string spName, Paras paras, SqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand(spName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // �������
            foreach (Para para in paras)
            {
                SqlParameter myParameter = new SqlParameter(para.ParaName, para.val);
                myParameter.Size = para.Size;
                cmd.Parameters.Add(myParameter);
            }

            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        public static int RunSP(string spName, Paras paras, OracleConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            OracleCommand salesCMD = new OracleCommand(spName, conn);
            salesCMD.CommandType = CommandType.StoredProcedure;
            foreach (Para para in paras)
            {
                OracleParameter myParameter = new OracleParameter(para.ParaName, OracleType.VarChar);
                myParameter.Direction = ParameterDirection.Input;
                myParameter.Size = para.Size;
                myParameter.Value = para.val;
                salesCMD.Parameters.Add(myParameter);
            }
            return salesCMD.ExecuteNonQuery();
        }
		public static int RunSP(string spName,  Paras paras )
		{
            switch (DBAccess.AppCenterDBType)
            {
                case DBType.MSSQL:
                    SqlConnection conn = new SqlConnection(SystemConfig.AppCenterDSN);
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    return DBProcedure.RunSP(spName, paras, conn);
                    break;
                case DBType.Informix:
                    IfxConnection conn1 = new IfxConnection(SystemConfig.AppCenterDSN);
                    if (conn1.State != ConnectionState.Open)
                        conn1.Open();
                    return DBProcedure.RunSP(spName, paras, conn1);
                    break;
                default:
                    throw new Exception("��δ����");
                    break;
            }
		}
		#endregion 

		#region ���д洢���̷��� DataTable �����в���
        //public static DataTable RunSPReturnDataTable(string spName )
        //{
        //    if (DBAccess.AppCenterDBType==DBType.MSSQL)
        //        return DBProcedure.RunSPReturnDataTable(spName, new Paras(),(SqlConnection)DBAccess.GetAppCenterDBConn );
        //    else
        //        return DBProcedure.RunSPReturnDataTable(spName,new Paras(),(SqlConnection)DBAccess.GetAppCenterDBConn ); 
        //}
		/// <summary>
		/// ���д洢���̷���Table
		/// </summary>
		/// <param name="spName">�洢��������</param>		 
		/// <returns>ִ�к��Table</returns>
		public static DataTable RunSPReturnDataTable(string spName,  SqlConnection conn)
		{
			Paras ens =new Paras();
			return  DBProcedure.RunSPReturnDataTable(spName,ens,conn);
		}
        public static DataTable RunSPReturnDataTable(string spName, IfxConnection conn)
        {
            Paras ens = new Paras();
            return DBProcedure.RunSPReturnDataTable(spName, ens, conn);
        }
		public static DataTable RunSPReturnDataTable(string spName,  OracleConnection conn)
		{
			Paras ens =new Paras();
			return  DBProcedure.RunSPReturnDataTable(spName,ens,conn);
		}
		/// <summary>
		/// ���д洢���̷���Table
		/// </summary>
		/// <param name="spName">�洢��������</param>
		/// <param name="paras">��˵����</param>
		/// <returns>ִ�к��Table</returns>
		public static DataTable RunSPReturnDataTable(string spName,  Paras paras )
		{
			if (DBAccess.AppCenterDBType==DBType.MSSQL)
				return DBProcedure.RunSPReturnDataTable(spName,paras,(SqlConnection)DBAccess.GetAppCenterDBConn );
			else
				return DBProcedure.RunSPReturnDataTable(spName,paras,(SqlConnection)DBAccess.GetAppCenterDBConn ); 
		}
        public static DataTable RunSPReturnDataTable(string spName, Paras paras, OracleConnection conn)
        {

            OracleCommand salesCMD = new OracleCommand(spName, conn);
            salesCMD.CommandType = CommandType.StoredProcedure;

            /// �������ǵ�para			
            foreach (Para para in paras)
            {
                OracleParameter myParm = salesCMD.Parameters.AddWithValue(para.ParaName, para.DAType);
                myParm.Value = para.val;
            }

            //selectCMD.CommandTimeout =60;
            OracleDataAdapter sda = new OracleDataAdapter(salesCMD);
            //SqlDataAdapter sda = new SqlDataAdapter(salesCMD);
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            return dt;
        }
        /// <summary>
		/// ���д洢���̷���Table��
		/// </summary>
		/// <param name="spName"></param>
		/// <param name="paras"></param>
		/// <param name="conn"></param>
		/// <returns></returns>
		public static DataTable RunSPReturnDataTable(string spName, Paras paras, SqlConnection conn)
		{
			try
			{
				SqlCommand salesCMD = new SqlCommand(spName, conn);
				salesCMD.CommandType = CommandType.StoredProcedure;			 

				/// �������ǵĲ���			
				foreach(Para para in paras)
				{
                    SqlParameter myParm = salesCMD.Parameters.AddWithValue(para.ParaName, para.DAType);
					myParm.Value = para.val;
				}

				//selectCMD.CommandTimeout =60;
				SqlDataAdapter sda = new SqlDataAdapter(salesCMD);
				if (conn.State==System.Data.ConnectionState.Closed)				 
					conn.Open();
				DataTable dt = new DataTable();
				sda.Fill(dt);
				sda.Dispose();					
				return dt;
			}
			catch(System.Exception ex)
			{
				throw ex;
			}
		}

        public static DataTable RunSPReturnDataTable(string spName, Paras paras, IfxConnection conn)
        {
            try
            {
                IfxCommand salesCMD = new IfxCommand(spName, conn);
                salesCMD.CommandType = CommandType.StoredProcedure;

                /// �������ǵĲ���			
                foreach (Para para in paras)
                {
                    IfxParameter myParm = salesCMD.Parameters.Add(para.ParaName, para.DAType);
                    myParm.Value = para.val;
                }

                //selectCMD.CommandTimeout =60;
                IfxDataAdapter sda = new IfxDataAdapter(salesCMD);
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                return dt;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
		#endregion

        public static DataSet RunSPReturnDataSet(string spName, Paras p)
        {
            IDbConnection conn = DBAccess.GetAppCenterDBConn;
            IDbDataAdapter ada = DBAccess.GetAppCenterDBAdapter;
            try
            {
                IDbCommand cmd = DBAccess.GetAppCenterDBCommand;
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
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
                throw new Exception("SQLs=" + spName + " Exception=" + ex.Message);
            }
            finally
            {
                (ada as System.Data.Common.DbDataAdapter).Dispose();
                conn.Close();
            }
        }

        

	}
}
