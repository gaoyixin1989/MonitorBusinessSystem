using System;
using System.Data.SqlClient;

namespace BP.DA
{
	/// <summary>
	///�����ӵ��ĸ����ϣ�
	///  ���Ǵ���� web.config ���б��ڣ�
	/// </summary>
	public enum DBUrlType
	{ 
		/// <summary>
		/// ��Ҫ��Ӧ�ó���
		/// </summary>
		AppCenterDSN,
		/// <summary>
		/// DBAccessOfOracle
		/// </summary>
		DBAccessOfOracle,
		/// <summary>
		/// DBAccessOfOracle1
		/// </summary>
		DBAccessOfOracle1,
		/// <summary>
		/// DBAccessOfMSMSSQL
		/// </summary>
		DBAccessOfMSMSSQL,
		/// <summary>
		/// access�����ӣ�
		/// </summary>
		DBAccessOfOLE,
		/// <summary>
		/// DBAccessOfODBC
		/// </summary>
		DBAccessOfODBC
	}
	/// <summary>
	/// DBUrl ��ժҪ˵����
	/// </summary>
	public class DBUrl
	{
		/// <summary>
		/// ����
		/// </summary>
		public DBUrl()
		{
		} 
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="type">����type</param>
		public DBUrl(DBUrlType type)
		{
			this.DBUrlType=type;
		}
		/// <summary>
		/// Ĭ��ֵ
		/// </summary>
		public DBUrlType  _DBUrlType=DBUrlType.AppCenterDSN;
		/// <summary>
		/// Ҫ���ӵĵ��Ŀ⡣
		/// </summary>
		public DBUrlType DBUrlType
		{
			get
			{
				return _DBUrlType;
			}
			set
			{
				_DBUrlType=value;
			}
		}
        public string DBVarStr
        {
            get
            {
                switch (this.DBType)
                {
                    case DBType.Oracle:
                        return ":";
                    case DBType.MySQL:
                    case DBType.MSSQL:
                        return "@";
                    case DBType.Informix:
                        return "?";
                    default:
                        return "@";
                }
            }
        }
		/// <summary>
		/// ���ݿ�����
		/// </summary>
		public DBType DBType
		{
			get
			{
				switch(this.DBUrlType)
				{
					case DBUrlType.AppCenterDSN:
						return DBAccess.AppCenterDBType ; 
					case DBUrlType.DBAccessOfMSMSSQL:
						return DBType.MSSQL;
					case DBUrlType.DBAccessOfOLE:
						return DBType.Access;
					case DBUrlType.DBAccessOfOracle1:
                    case DBUrlType.DBAccessOfOracle:
						return DBType.Oracle ;
					default:
						throw new Exception("����ȷ������");
				}
			}
		}
	}
	
}
