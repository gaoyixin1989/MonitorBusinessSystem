using System;
using System.Threading;
using System.Collections;
using System.Data;
using BP.DA;
using BP.DTS;
using BP.En;
using BP.Web.Controls;
using BP.Web;

namespace BP.DTS
{
    public class AddEmpLeng : DataIOEn2
    {
        public AddEmpLeng()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "Ϊ����Ա��ų�������";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }

        public override void Do()
        {
            string sql = "";
            string sql2 = "";

            Log.DebugWriteInfo("ssssssssssssssssssss");

            ArrayList al = ClassFactory.GetObjects("BP.En.Entity");
            foreach (object obj in al)
            {
                Entity en = obj as Entity;
                Map map = en.EnMap;

                try
                {
                    if (map.IsView)
                        continue;
                }
                catch
                {
                }

                string table = en.EnMap.PhysicsTable;
                foreach (Attr attr in map.Attrs)
                {
                    if (attr.Key.IndexOf("Text") != -1)
                        continue;

                    if (attr.Key == "Rec" || attr.Key == "FK_Emp" || attr.UIBindKey == "BP.Port.Emps")
                    {
                        sql += "\n update " + table + " set " + attr.Key + "='01'||" + attr.Key + " WHERE length(" + attr.Key + ")=6;";
                    }
                    else if (attr.Key == "Checker")
                    {
                        sql2 += "\n update " + table + " set " + attr.Key + "='01'||" + attr.Key + " WHERE length(" + attr.Key + ")=6;";
                    }
                }
            }
            Log.DebugWriteInfo(sql);
            Log.DebugWriteInfo("===========================" + sql2);
        }
    }
	/// <summary>
	/// ִ������
	/// </summary>
	public enum DoType
	{
		/// <summary>
		/// û��ָ��
		/// </summary>
		UnName,
		/// <summary>
		/// ��ɾ�������,�ʺ��κ����,��������Ч�ʽϵͣ�
		/// </summary>
		DeleteInsert,
		/// <summary>
		/// ����ͬ�����ʺ����������,������˰�˵���˰��Ϣ.
		/// ����������ǰ�Ĳ��֣������£�
		/// </summary>
		Incremental,
		/// <summary>
		/// ͬ��,�ʺ����������,������˰�ˣ�
		/// </summary>
		Inphase,
		/// <summary>
		/// ֱ�ӵĵ���.��һ����������һ���ط����������
		/// </summary>
		Directly,
		/// <summary>
		/// ����ģ�
		/// </summary>
		Especial
	}
	/// <summary>
	/// ����
	/// </summary>
	abstract public class DataIOEn :DataIOEn2
	{
		public bool Enable=true;
	}
	
	/// <summary>
	/// EnMap ��ժҪ˵����
	/// </summary>
	abstract public class DataIOEn2
	{
		 
		/// <summary>
		/// ��ȡ�� DTS �еı�š�
		/// </summary>
		/// <returns></returns>
		public string GetNoInDTS()
		{
            //DTS.SysDTS dts =new SysDTS();
            //QueryObject qo = new QueryObject(dts);
            //qo.AddWhere(DTSAttr.RunText,this.ToString());
            //if (qo.DoQuery()==0)
            //    throw new Exception("û��ȡ�����ȵı��.");
            //else
            //    return dts.No;

            return null;
		}
        /// <summary>
        /// ִ���� ���߳��С�
        /// </summary>
        public void DoItInThread()
        {
            ThreadStart ts = new ThreadStart(this.Do);
            Thread thread = new Thread(ts);
            thread.Start();
        }

		#region Directly 
		 
		#endregion

		#region ��������.
		/// <summary>
		/// ѡ��sql .
		/// </summary>
		public string SELECTSQL=null;
		/// <summary>
		/// ����ͬ�����ͣ�
		/// </summary>
		public DoType HisDoType = DoType.UnName;
		/// <summary>
		/// ��������ʱ��
		/// </summary>
		public RunTimeType HisRunTimeType = RunTimeType.UnName;
        /// <summary>
        /// ����
        /// </summary>
		public string Title="δ��������ͬ��";
		/// <summary>
		/// WHERE .
		/// </summary>
		public string FromWhere=null;
		/// <summary>
		/// FFs
		/// </summary>
		public FFs FFs=null;
		/// <summary>
		/// ��Table .
		/// </summary>
		public string FromTable=null;
		/// <summary>
		/// ��Table.
		/// </summary>
		public string ToTable=null;
		/// <summary>
		/// ��DBUrl.
		/// </summary>
		public DBUrlType FromDBUrl;
		/// <summary>
		/// ��DBUrl.
		/// </summary>
		public DBUrlType ToDBUrl;
		/// <summary>
		/// �������
		/// </summary>
		public string UPDATEsql;
		/// <summary>
		/// ��ע
		/// </summary>
		public string Note="��";

		public string DefaultEveryMonth="99";
		public string DefaultEveryDay="99";
		public string DefaultEveryHH="99";
		public string DefaultEveryMin="99";
		/// <summary>
		/// ���
		/// </summary>
		public string FK_Sort="0";


//		/// <summary>
//		/// �������µ�����Դsql.
//		/// �����sql����ѯ����һ��������ϣ�����������ڸ��µļ��ϡ�
//		/// һ�����˵�����sql�Ǹ��ݵ�ǰ���·��Զ����ɵġ�
//		/// </summary>
//		public string IncrementalDBSourceSQL;
		#endregion

		/*
		 * ������ʵ��������ǰѵ��ȷ�Ϊ���¼��֡�
		 * 1���������ȡ�
		 *    ������˰����˰��Ϣ���ص㣺
		 *   a,������ʱ������������ӡ�
		 *   b,�·���ǰ�����ݲ��仯��
		 * 
		 *   �ܽ᣺ԭ��������ʱ��ֻ���ӣ�����ǰ�����ݲ��仯��         
		 * 
		 * 2���仯���ȡ�
		 *   ������˰����Ϣ��
		 *   �ص㣺Դ����˰����������ɾ���ģ����п��ܷ�����
		 *
		 * 3��ɾ����ʽͬ����
		 *   ���裺
		 * ��������ɾ����
		 * �����������µ����ݣ� 
		 * */
		/// <summary>
		/// ����
		/// </summary>
		public  DataIOEn2(){}


		/// <summary>
		/// ֱ�ӵ���
		/// </summary>
		/// <param name="fromSQL">sql</param>
		/// <param name="toPTable">table</param>
		/// <param name="pk">key,���ڽ���������pk</param>
		public void Directly(string fromSQL, string toPTable, string pk)
		{
			this.Directly(fromSQL,toPTable);
			this.ToDBUrlRunSQL("CREATE INDEX "+toPTable+"ID ON "+toPTable+" ("+pk+")");
		}
		/// <summary>
		/// ֱ�ӵ���
		/// </summary>
		/// <param name="fromSQL"></param>
		/// <param name="toPTable"></param>
		/// <param name="pk1"></param>
		/// <param name="pk2"></param>
		public void Directly(string fromSQL, string toPTable, string pk1,string pk2)
		{
			this.Directly(fromSQL,toPTable);
			this.ToDBUrlRunSQL("CREATE INDEX "+toPTable+"ID ON "+toPTable+" ("+pk1+","+pk2+")");
		}
		/// <summary>
		/// ֱ�ӵ���
		/// </summary>
		/// <param name="fromSQL"></param>
		/// <param name="toPTable"></param>
		/// <param name="pk1"></param>
		/// <param name="pk2"></param>
		public void Directly(string fromSQL, string toPTable, string pk1,string pk2,string pk3)
		{
			this.Directly(fromSQL,toPTable);
			this.ToDBUrlRunSQL("CREATE INDEX "+toPTable+"ID ON "+toPTable+" ("+pk1+","+pk2+","+pk3+")");
		}
		/// <summary>
		/// ֱ�Ӵ�����һ�����ݿ��У������ݵ��뵽��Ŀ�����ݿ⣮
		/// ���ڸ��ӵĵ�����������ַ�ʽ,�������⴦��
		/// ����ԴȪ�� SELECTSQL ,ָ����sql.
		/// selectsql ֻ��ʶ�������������ͣ�
		/// char, int , float, decimal . 
		/// ��������ʺ����ϵ��������ͣ���ת��Ϊ���ϵ��������ͣ���
		/// </summary>
		public void Directly(string fromSQL, string toPTable)
		{
			// �õ�����ԴȪ��
			DataTable dt =this.FromDBUrlRunSQLReturnTable( fromSQL ); 
 
			#region �γ� insert into ��ǰһ���֣�
			string sql=null;
			sql="INSERT INTO "+toPTable+"(";
			foreach(DataColumn dc in dt.Columns )
			{
				sql+=dc.ColumnName+",";
			}
			sql=sql.Substring(0,sql.Length-1);
			sql+=") VALUES (";
			#endregion


			// ɾ��Ŀ�ı����ݣ�
			try
			{
				this.ToDBUrlRunSQL(" drop table "+ toPTable  );
			}
			catch
			{
			}

			// ����һ���±�
			string createTable="CREATE TABLE "+toPTable+" (";
			foreach(DataColumn dc in dt.Columns)
			{
				switch(dc.DataType.ToString())
				{
					case "System.String":
						// ȡ�����ĳ��ȡ�
//						int len=0;
//						foreach(DataRow dr in dt.Rows)
//						{
//							if (len < dr[dc.ColumnName].ToString().Length )
//								len=dr[dc.ColumnName].ToString().Length;
//						}
//						len+=10;
						createTable+=dc.ColumnName+" nvarchar (700) NULL  ," ;
						break;
					case "System.Int16":
					case "System.Int32":
					case "System.Int64":
						createTable+=dc.ColumnName+" int NULL," ;
						break;
					case "System.Decimal":
						createTable+=dc.ColumnName+" decimal NULL,";
						break;
					default:
						createTable+=dc.ColumnName+" float NULL,"; 
						break;
				}
			}
			createTable=createTable.Substring(0,createTable.Length-1);
			createTable+=")";
			this.ToDBUrlRunSQL(createTable);



			string sql2=null; 
			// ��������Դ��inset ��Ŀ�ı���
			string errormsg="";
			foreach(DataRow dr in dt.Rows)
			{
				sql2=sql;
				foreach(DataColumn dc in dt.Columns)
				{
					sql2+="'"+dr[dc.ColumnName]+"',";
				}
				sql2=sql2.Substring(0,sql2.Length-1)+")";
				try
				{
					this.ToDBUrlRunSQL(sql2);
				}
				catch(Exception ex)
				{
					errormsg+=ex.Message;
				}
			}
			if (errormsg!="")
				throw new Exception(" data output error: "+errormsg );

		}
		#region ����������
		/// <summary>
		/// ����Դ run sql ,����table .
		/// </summary>
		/// <param name="selectSql"></param>
		/// <returns></returns>
		public DataTable FromDBUrlRunSQLReturnTable(string selectSql)
		{
			// �õ�����Դ��
			DataTable dt = new DataTable();
			switch(this.FromDBUrl)
			{
				case DBUrlType.AppCenterDSN:
					dt=DBAccess.RunSQLReturnTable( selectSql);
					break;
				case DBUrlType.DBAccessOfMSMSSQL:
					dt=DBAccessOfMSMSSQL.RunSQLReturnTable( selectSql );
					break;
				case DBUrlType.DBAccessOfODBC:
					dt=DBAccessOfODBC.RunSQLReturnTable( selectSql );
					break;
				case DBUrlType.DBAccessOfOLE:
					dt=DBAccessOfOLE.RunSQLReturnTable( selectSql );
					break;
				case DBUrlType.DBAccessOfOracle:
					dt=DBAccessOfOracle.RunSQLReturnTable( selectSql );
					break;
                //case DBUrlType.DBAccessOfOracle1:
                //    dt=DBAccessOfOracle1.RunSQLReturnTable( selectSql );
                //    break;
				default:
					break;
			}
			return dt;
		}
		public int ToDBUrlRunSQL(string sql)
		{
			switch(this.ToDBUrl)
			{
				case DBUrlType.AppCenterDSN:
					return DBAccess.RunSQL(sql);
				case DBUrlType.DBAccessOfMSMSSQL:
					return DBAccessOfMSMSSQL.RunSQL(sql);
				case DBUrlType.DBAccessOfODBC:
					return DBAccessOfODBC.RunSQL(sql);
				case DBUrlType.DBAccessOfOLE:
					return DBAccessOfOLE.RunSQL(sql);
				case DBUrlType.DBAccessOfOracle:
					return DBAccessOfOracle.RunSQL(sql);
				default:
					throw new Exception("@ error it");
			}
		}
		public int ToDBUrlRunDropTable(string table)
		{
			switch(this.ToDBUrl)
			{
				case DBUrlType.AppCenterDSN:
					return DBAccess.RunSQLDropTable(table);
				case DBUrlType.DBAccessOfMSMSSQL:
					return DBAccessOfMSMSSQL.RunSQL(table);
				case DBUrlType.DBAccessOfODBC:
					return DBAccessOfODBC.RunSQL(table);
				case DBUrlType.DBAccessOfOLE:
					return DBAccessOfOLE.RunSQL(table);
				case DBUrlType.DBAccessOfOracle:
					return DBAccessOfOracle.RunSQLTRUNCATETable(table);
				default:
					throw new Exception("@ error it");
			}
		}
		/// <summary>
		/// �Ƿ����?
		/// </summary>
		/// <param name="sql">Ҫ�жϵ�sql</param>
		/// <returns></returns>
		public bool ToDBUrlIsExit(string sql)
		{
			switch(this.ToDBUrl)
			{
				case DBUrlType.AppCenterDSN:
					return DBAccess.IsExits(sql);
				case DBUrlType.DBAccessOfMSMSSQL:
					return DBAccessOfMSMSSQL.IsExits(sql);
				case DBUrlType.DBAccessOfODBC:
					return DBAccessOfODBC.IsExits(sql);
				case DBUrlType.DBAccessOfOLE:
					return DBAccessOfOLE.IsExits(sql);
				case DBUrlType.DBAccessOfOracle:
					return DBAccessOfOracle.IsExits(sql);
				default:
					throw new Exception("@ error it");
			}
		}
		#endregion

		#region ������ New   2005-01-29

		/// <summary>
		/// ִ�У������������д��
		/// </summary>
		public virtual void Do()
		{
			if ( this.HisDoType==DoType.UnName)
				throw new Exception("@û��˵��ͬ��������,���ڻ�����Ϣ��������ͬ��������(���캯����)��");

			if (this.HisDoType==DoType.DeleteInsert)
				this.DeleteInsert();

			if (this.HisDoType==DoType.Inphase)
				this.Inphase();

			if (this.HisDoType==DoType.Incremental)
				this.Incremental();
		}

		#region ������������
		/// <summary>
		/// �������ȣ�
		/// ���磺 ��˰�˵���˰��Ϣ��
		/// �ص㣺1�� ������ʱ������������ӡ�
		///       2�� �·���ǰ�����ݲ��仯��
		/// </summary>
		public void Incremental()
		{
			/*
			 * ʵ�ֲ��裺
			 * 1�����sql.
			 * 2��ִ�и��¡�
			 *  
			 * */
			this.DoBefore();  // ���ã�����ǰ��ҵ���߼�����

			#region  �õ�Ҫ���µ�����Դ��
			DataTable FromDataTable= this.GetFromDataTable();
			#endregion

			#region ��ʼִ�и��¡�
			string isExitSql="";
			string InsertSQL="";
			//���� ����Դ��.
			foreach(DataRow FromDR in FromDataTable.Rows)
			{
				#region �ж��Ƿ���ڣ�
				/* �ж��Ƿ���ڣ��������continue. �����ھ� insert.  */
			    isExitSql="SELECT * FROM "+this.ToTable+" WHERE ";
				foreach(FF ff in this.FFs)
				{
					if (ff.IsPK==false)
						continue;
					isExitSql+= ff.ToField +"='"+FromDR[ff.FromField]+ "' AND ";
				}

				isExitSql=isExitSql.Substring(0,isExitSql.Length-5);

				if (DBAccess.IsExits(isExitSql))  //��������ھ� insert . 
					continue;
				#endregion  �ж��Ƿ����

				#region ִ�в������
				InsertSQL="INSERT INTO "+this.ToTable +"(";
				foreach(FF ff in this.FFs)
				{
					InsertSQL+=ff.ToField.ToString()+",";
				}
				InsertSQL=InsertSQL.Substring(0,InsertSQL.Length-1);
				InsertSQL+=") values(";
				foreach(FF ff in this.FFs)
				{
					if(ff.DataType==DataType.AppString||ff.DataType==DataType.AppDateTime)
					{
						InsertSQL+="'"+FromDR[ff.FromField].ToString()+"',";
					}
					else
					{
						InsertSQL+=FromDR[ff.FromField].ToString()+",";
					}
				}
				InsertSQL=InsertSQL.Substring(0,InsertSQL.Length-1);
				InsertSQL+=")";
				switch(this.ToDBUrl)
				{
					case DA.DBUrlType.AppCenterDSN:
						DBAccess.RunSQL(InsertSQL);
						break;
					case DA.DBUrlType.DBAccessOfMSMSSQL:
						DBAccessOfOLE.RunSQL(InsertSQL);
						break;
					case DA.DBUrlType.DBAccessOfOLE:
						DBAccessOfOLE.RunSQL(InsertSQL);
						break;
					case DA.DBUrlType.DBAccessOfOracle:
						DBAccessOfOracle.RunSQL(InsertSQL);
						break;
					case DA.DBUrlType.DBAccessOfODBC:
						DBAccessOfODBC.RunSQL(InsertSQL);
						break;
					default:
						break;
				}
				#endregion ִ�в������

			}
			#endregion ����,��ʼִ�и���

			this.DoAfter(); // ���ã�����֮���ҵ����
		}
		/// <summary>
		/// ����������ǰҪִ�еķ�����
		/// </summary>
		protected virtual void DoBefore()
		{
		}
		/// <summary>
		/// ��������֮��Ҫִ�еķ�����
		/// </summary>
		protected virtual void DoAfter()
		{
		}
		#endregion

		#region ɾ��(���) ֮�����(�ʺ��κ�һ�����ݵ���)
		/// <summary>
		/// ɾ��֮�����, ������������̫��,����Ƶ�ʲ�̫Ƶ�������ݴ���.
		/// </summary>
		public  void DeleteInsert()
		{
			this.DoBefore(); //����ҵ����
			// �õ�Դ��.
			DataTable FromDataTable= this.GetFromDataTable();
			this.DeleteObjData();

			#region  ����Դ�� �������
			string InsertSQL="";
			foreach(DataRow FromDR in FromDataTable.Rows)
			{
				 
				InsertSQL="INSERT INTO "+this.ToTable +"(";
				foreach(FF ff in this.FFs)
				{
					InsertSQL+=ff.ToField.ToString()+",";
				}
				InsertSQL=InsertSQL.Substring(0,InsertSQL.Length-1);
				InsertSQL+=") values(";
				foreach(FF ff in this.FFs)
				{
					if(ff.DataType==DataType.AppString||ff.DataType==DataType.AppDateTime)
						InsertSQL+="'"+FromDR[ff.FromField].ToString()+"',";
					else
						InsertSQL+=FromDR[ff.FromField].ToString()+",";
				}
				InsertSQL=InsertSQL.Substring(0,InsertSQL.Length-1);
				InsertSQL+=")";
				
				switch(this.ToDBUrl)
				{
					case DA.DBUrlType.AppCenterDSN:
						DBAccess.RunSQL(InsertSQL);
						break;
					case DA.DBUrlType.DBAccessOfMSMSSQL:
						DBAccessOfMSMSSQL.RunSQL(InsertSQL);
						break;
					case DA.DBUrlType.DBAccessOfOLE:
						DBAccessOfOLE.RunSQL(InsertSQL);
						break;
					case DA.DBUrlType.DBAccessOfOracle:
						DBAccessOfOracle.RunSQL(InsertSQL);
						break;
					case DA.DBUrlType.DBAccessOfODBC:
						DBAccessOfODBC.RunSQL(InsertSQL);
						break;
					default:
						break;
				}
				 
			}
			#endregion

			

			this.DoAfter(); // ����ҵ����

		}
		public void DeleteObjData()
		{
			#region ɾ��������
			switch(this.ToDBUrl)
			{
				case DA.DBUrlType.AppCenterDSN:
                    DBAccess.RunSQL("DELETE FROM  " + this.ToTable);
					break;
				case DA.DBUrlType.DBAccessOfMSMSSQL:
                    DBAccess.RunSQL("DELETE  FROM " + this.ToTable);						
					break;
				case DA.DBUrlType.DBAccessOfOLE:
                    DBAccessOfOLE.RunSQL("DELETE FROM  " + this.ToTable);
					break;
				case DA.DBUrlType.DBAccessOfOracle:
                    DBAccessOfOracle.RunSQL("DELETE  FROM " + this.ToTable);
					break;
				case DA.DBUrlType.DBAccessOfODBC:
                    DBAccessOfODBC.RunSQL("DELETE FROM  " + this.ToTable);
					break;
				default:
					break;
			}
			#endregion
		}
		 
		#endregion

		#region ͬ�����ݡ�
		/// <summary>
		/// �õ�����Դ��
		/// </summary>
		/// <returns></returns>
		public DataTable GetToDataTable()
		{
			string sql="SELECT * FROM "+this.ToTable;
			DataTable FromDataTable = new DataTable();
			switch(this.ToDBUrl)
			{
				case DA.DBUrlType.AppCenterDSN:
					FromDataTable=DBAccess.RunSQLReturnTable(sql);
					break;
				case DA.DBUrlType.DBAccessOfMSMSSQL:
					FromDataTable=DBAccess.RunSQLReturnTable(sql);
					break;
				case DA.DBUrlType.DBAccessOfOLE:
					FromDataTable=DBAccessOfOLE.RunSQLReturnTable(sql);
					break;
				case DA.DBUrlType.DBAccessOfOracle:
					FromDataTable=DBAccessOfOracle.RunSQLReturnTable(sql);
					break;
				case DA.DBUrlType.DBAccessOfODBC:
					FromDataTable=DBAccessOfODBC.RunSQLReturnTable(sql);
					break;
				default:
					throw new Exception("the to dburl error DBUrlType ");
			}

			return FromDataTable;

		}
		/// <summary>
		/// �õ�����Դ��
		/// </summary>
		/// <returns>����Դ</returns> 
		public DataTable GetFromDataTable()
		{
			string FromSQL="SELECT ";
			foreach(FF ff in this.FFs)
			{
				//�������͵��ж�
				if(ff.DataType==DataType.AppDateTime)
				{
					FromSQL+=" CASE  "+
                        " when datalength( CONVERT(NVARCHAR,datepart(month," + ff.FromField + " )))=1 then datename(year," + ff.FromField + " )+'-'+('0'+CONVERT(NVARCHAR,datepart(month," + ff.FromField + " ))) " +
						" else "+
                        " datename(year," + ff.FromField + " )+'-'+CONVERT(NVARCHAR,datepart(month," + ff.FromField + " )) " +
						" END "+
						" AS "+ff.FromField+" , ";
				}
				else
				{
					FromSQL+=ff.FromField+",";
				}
			}

			FromSQL=FromSQL.Substring(0,FromSQL.Length-1);
			FromSQL+=" from "+ this.FromTable;
			FromSQL+=this.FromWhere;
			DataTable FromDataTable=new DataTable();
			switch(this.FromDBUrl)
			{
				case DA.DBUrlType.AppCenterDSN:
					FromDataTable=DBAccess.RunSQLReturnTable(FromSQL);
					break;
				case DA.DBUrlType.DBAccessOfMSMSSQL:
					FromDataTable=DBAccess.RunSQLReturnTable(FromSQL);
					break;
				case DA.DBUrlType.DBAccessOfOLE:
					FromDataTable=DBAccessOfOLE.RunSQLReturnTable(FromSQL);
					break;
				case DA.DBUrlType.DBAccessOfOracle:
					FromDataTable=DBAccessOfOracle.RunSQLReturnTable(FromSQL);
					break;
				case DA.DBUrlType.DBAccessOfODBC:
					FromDataTable=DBAccessOfODBC.RunSQLReturnTable(FromSQL);
					break;
				default:
					throw new Exception("the from dburl error DBUrlType ");
			}
			return FromDataTable;
		}
		#endregion

		#endregion end ����New peng 2005-01-29

		#region ����
		/// <summary>
		/// ͬ������.
		/// </summary>
		public void Inphase()
		{
			#region �õ�Դ��
			this.DoBefore();

			string FromSQL="SELECT ";
			foreach(FF ff in this.FFs)
			{
				//�������͵��ж�
				if(ff.DataType==DataType.AppDateTime)
				{
					FromSQL+=" CASE  "+
                        " when datalength( CONVERT(NVARCHAR,datepart(month," + ff.FromField + " )))=1 then datename(year," + ff.FromField + " )+'-'+('0'+CONVERT(NVARCHAR,datepart(month," + ff.FromField + " ))) " +
						" else "+
                        " datename(year," + ff.FromField + " )+'-'+CONVERT(NVARCHAR,datepart(month," + ff.FromField + " )) " +
						" END "+
						" AS "+ff.FromField+" , ";
				}
				else
				{
					FromSQL+=ff.FromField+",";
				}
			}
			FromSQL=FromSQL.Substring(0,FromSQL.Length-1);
			FromSQL+=" from "+ this.FromTable;
			FromSQL+=this.FromWhere;
			DataTable FromDataTable=new DataTable();
			switch(this.FromDBUrl)
			{
				case DA.DBUrlType.AppCenterDSN:
					FromDataTable=DBAccess.RunSQLReturnTable(FromSQL);
					break;
				case DA.DBUrlType.DBAccessOfMSMSSQL:
					FromDataTable=DBAccess.RunSQLReturnTable(FromSQL);
					break;
				case DA.DBUrlType.DBAccessOfOLE:
					FromDataTable=DBAccessOfOLE.RunSQLReturnTable(FromSQL);
					break;
				case DA.DBUrlType.DBAccessOfOracle:
					FromDataTable=DBAccessOfOracle.RunSQLReturnTable(FromSQL);
					break;
				case DA.DBUrlType.DBAccessOfODBC:
					FromDataTable=DBAccessOfODBC.RunSQLReturnTable(FromSQL);
					break;
				default:
					break;
			}
			#endregion

			#region �õ�Ŀ�ı�(�ֶ�ֻ��������)
			string ToSQL="SELECT ";
			foreach(FF ff in this.FFs)
			{
				if (ff.IsPK==false)
					continue;
				ToSQL+=ff.ToField+",";
			}
			ToSQL=ToSQL.Substring(0,ToSQL.Length-1);
			ToSQL+=" FROM "+ this.ToTable;
			DataTable ToDataTable=new DataTable();
			switch(this.ToDBUrl)
			{
				case DA.DBUrlType.AppCenterDSN:
					ToDataTable=DBAccess.RunSQLReturnTable(ToSQL);
					break;
				case DA.DBUrlType.DBAccessOfMSMSSQL:
					ToDataTable=DBAccess.RunSQLReturnTable(ToSQL);
					break;
				case DA.DBUrlType.DBAccessOfOLE:
					ToDataTable=DBAccessOfOLE.RunSQLReturnTable(ToSQL);
					break;
				case DA.DBUrlType.DBAccessOfOracle:
					ToDataTable=DBAccessOfOracle.RunSQLReturnTable(ToSQL);
					break;
				case DA.DBUrlType.DBAccessOfODBC:
					ToDataTable=DBAccessOfODBC.RunSQLReturnTable(ToSQL);
					break;
				default:
					break;
			}
			#endregion

			string SELECTSQL="";
			string InsertSQL="";
			string UpdateSQL="";
			string DeleteSQL="";
			//int i=0;
			//int j=0;
			int result=0;

			#region  ����Դ��
			foreach(DataRow FromDR in FromDataTable.Rows)
			{
				UpdateSQL="UPDATE  "+this.ToTable+" SET ";				
				foreach(FF ff in this.FFs)
				{
					switch(ff.DataType)
					{
						case DataType.AppDateTime:
						case DataType.AppString:
							UpdateSQL+=  ff.ToField+ "='"+FromDR[ff.FromField].ToString()+"',";
							break;
						case DataType.AppFloat:
						case DataType.AppInt:
						case DataType.AppMoney:
						case DataType.AppRate:
						case DataType.AppDate:
						case DataType.AppDouble:
							UpdateSQL+=  ff.ToField+ "="+FromDR[ff.FromField].ToString()+"," ;
							break;
						default:
							throw new Exception("û���漰������������.");
					}
				}
				UpdateSQL=UpdateSQL.Substring(0,UpdateSQL.Length-1);
				UpdateSQL+=" WHERE ";
				foreach(FF ff in this.FFs)
				{
					if (ff.IsPK==false)
						continue;
					UpdateSQL+= ff.ToField +"='"+FromDR[ff.FromField]+ "' AND ";
				}

				UpdateSQL=UpdateSQL.Substring(0,UpdateSQL.Length-5);
				switch(this.ToDBUrl)
				{
					case DA.DBUrlType.AppCenterDSN:
						result=DBAccess.RunSQL(UpdateSQL);
						break;
					case DA.DBUrlType.DBAccessOfMSMSSQL:
						string a=UpdateSQL;
						result=DBAccess.RunSQL(UpdateSQL);						
						break;
					case DA.DBUrlType.DBAccessOfOLE:
						result=DBAccessOfOLE.RunSQL(UpdateSQL);						
						break;
					case DA.DBUrlType.DBAccessOfOracle:
						result=DBAccessOfOracle.RunSQL(UpdateSQL);	
						break;
					case DA.DBUrlType.DBAccessOfODBC:
						result=DBAccessOfODBC.RunSQL(UpdateSQL);		
						break;
					default:
						break;
				}
				if(result==0)
				{
					//�������
					InsertSQL="INSERT INTO "+this.ToTable +"(";
					foreach(FF ff in this.FFs)
					{
						InsertSQL+=ff.ToField.ToString()+",";
					}
					InsertSQL=InsertSQL.Substring(0,InsertSQL.Length-1);
					InsertSQL+=") values(";
					foreach(FF ff in this.FFs)
					{
						if(ff.DataType==DataType.AppString||ff.DataType==DataType.AppDateTime)
						{
							InsertSQL+="'"+FromDR[ff.FromField].ToString()+"',";
						}
						else
						{
							InsertSQL+=FromDR[ff.FromField].ToString()+",";
						}
					}
					InsertSQL=InsertSQL.Substring(0,InsertSQL.Length-1);
					InsertSQL+=")";
					switch(this.ToDBUrl)
					{
						case DA.DBUrlType.AppCenterDSN:
							DBAccess.RunSQL(InsertSQL);
							break;
						case DA.DBUrlType.DBAccessOfMSMSSQL:
							DBAccess.RunSQL(InsertSQL);
							break;
						case DA.DBUrlType.DBAccessOfOLE:
							DBAccessOfOLE.RunSQL(InsertSQL);
							break;
						case DA.DBUrlType.DBAccessOfOracle:
							DBAccessOfOracle.RunSQL(InsertSQL);
							break;
						case DA.DBUrlType.DBAccessOfODBC:
							DBAccessOfODBC.RunSQL(InsertSQL);
							break;
						default:
							break;
					}
				}
				
			}
			#endregion

			#region	����Ŀ�ı� ���������¼����,continue,���������¼������,���������ɾ��Ŀ�ı�Ķ�Ӧ����
			foreach(DataRow ToDR in ToDataTable.Rows)
			{
				SELECTSQL="SELECT ";
				foreach(FF ff in this.FFs)
				{
					if (ff.IsPK==false)
						continue;
					SELECTSQL+=ff.FromField+",";
				}
				SELECTSQL=SELECTSQL.Substring(0,SELECTSQL.Length-1);
				SELECTSQL+=" FROM "+this.FromTable+" WHERE ";
				foreach(FF ff in this.FFs)
				{
					if (ff.IsPK==false)
						continue;
					if(ff.DataType==DataType.AppDateTime)
					{
						SELECTSQL+=" case "+
							" when datalength( CONVERT(NVARCHAR,datepart(month,"+ff.FromField+" )))=1 then datename(year,"+ff.FromField+" )+'-'+('0'+CONVERT(VARCHAR,datepart(month,"+ff.FromField+" ))) "+
							" else "+
							" datename(year,"+ff.FromField+" )+'-'+CONVERT(VARCHAR,datepart(month,"+ff.FromField+" )) "+
							" END "+
							"='"+ToDR[ff.ToField].ToString()+"' AND ";
					}
					else
					{
						if(ff.DataType==DataType.AppString)
							SELECTSQL+=ff.FromField+"='"+ToDR[ff.ToField].ToString()+"' AND ";
						else
							SELECTSQL+=ff.FromField+"="+ToDR[ff.ToField].ToString()+" AND ";
					}
				}
				SELECTSQL=SELECTSQL.Substring(0,SELECTSQL.Length-5);
				//SELECTSQL+=this.FromWhere;
				result=0;
				switch(this.FromDBUrl)
				{
					case DA.DBUrlType.AppCenterDSN:
						result=DBAccess.RunSQLReturnCOUNT(SELECTSQL);
						break;
					case DA.DBUrlType.DBAccessOfMSMSSQL:
						result=DBAccess.RunSQLReturnCOUNT(SELECTSQL);
						break;
					case DA.DBUrlType.DBAccessOfOLE:
						result=DBAccessOfOLE.RunSQLReturnCOUNT(SELECTSQL);
						break;
					case DA.DBUrlType.DBAccessOfOracle:
						result=DBAccessOfOracle.RunSQL(SELECTSQL);
						break;
					case DA.DBUrlType.DBAccessOfODBC:
						result=DBAccessOfODBC.RunSQLReturnCOUNT(SELECTSQL);
						break;
					default:
						break;
				}

				if(result!=1)
				{
					//delete
                    DeleteSQL = "delete FROM  " + this.ToTable + " WHERE ";
					foreach(FF ff in this.FFs)
					{
						if (ff.IsPK==false)
							continue;
						if(ff.DataType==DataType.AppString)
							DeleteSQL+=ff.ToField+"='"+ToDR[ff.ToField].ToString()+"' AND ";
						else
							DeleteSQL+=ff.ToField+"="+ToDR[ff.ToField].ToString()+" AND ";
					}
					DeleteSQL=DeleteSQL.Substring(0,DeleteSQL.Length-5);
					switch(this.ToDBUrl)
					{
						case DA.DBUrlType.AppCenterDSN:
							DBAccess.RunSQL(DeleteSQL);
							break;
						case DA.DBUrlType.DBAccessOfMSMSSQL:
							DBAccess.RunSQL(DeleteSQL);						
							break;
						case DA.DBUrlType.DBAccessOfOLE:
							DBAccessOfOLE.RunSQL(DeleteSQL);
							break;
						case DA.DBUrlType.DBAccessOfOracle:
							DBAccessOfOracle.RunSQL(DeleteSQL);
							break;
						case DA.DBUrlType.DBAccessOfODBC:
							DBAccessOfODBC.RunSQL(DeleteSQL);
							break;
						default:
							break;
					}
					continue;
				}
				else if(result>1)
				{
					throw new Exception("Ŀ�������쳣���󣫱������ؼ���"+this.ToTable+"�ؼ���"+ToDR[0].ToString());
				} 
			}
			#endregion			

			if(this.UPDATEsql!=null)
			{
				switch(this.ToDBUrl)
				{
					case DA.DBUrlType.AppCenterDSN:
						DBAccess.RunSQL(UPDATEsql);
						break;
					case DA.DBUrlType.DBAccessOfMSMSSQL:
						DBAccess.RunSQL(UPDATEsql);						
						break;
					case DA.DBUrlType.DBAccessOfOLE:
						DBAccessOfOLE.RunSQL(UPDATEsql);
						break;
					case DA.DBUrlType.DBAccessOfOracle:
						DBAccessOfOracle.RunSQL(UPDATEsql);
						break;
					case DA.DBUrlType.DBAccessOfODBC:
						DBAccessOfODBC.RunSQL(UPDATEsql);
						break;
					default:
						break;
				}
			}
			this.DoAfter();
		}		 
		#endregion
	}	
}
