using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
    /// <summary>
    /// �����б�
    /// </summary>
    public class EntityOIDAttr
    {
        /// <summary>
        /// OID
        /// </summary>
        public const string OID = "OID";
    }
	/// <summary>
	/// �����б�
	/// </summary>
    public class EntityOIDMyFileAttr : EntityOIDAttr
    {
        /// <summary>
        /// MyFileName
        /// </summary>
        public const string MyFileName = "MyFileName";
        /// <summary>
        /// MyFilePath
        /// </summary>
        public const string MyFilePath = "MyFilePath";
        /// <summary>
        /// MyFileExt
        /// </summary>
        public const string MyFileExt = "MyFileExt";
    }
	/// <summary>
	/// OIDʵ��,ֻ��һ��ʵ�����ʵ��ֻ��һ���������ԡ�
	/// </summary>
	abstract public class EntityOID : Entity
	{		 
		#region ����
        /// <summary>
        /// �Ƿ����Զ�������
        /// </summary>
        public virtual bool IsInnKey
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public override string PK
        {
            get
            {
                return "OID";
            }
        }
		/// <summary>
		/// OID, ����ǿյľͷ��� 0 . 
		/// </summary>
        public int OID
        {
            get
            {
                try
                {
                    return this.GetValIntByKey(EntityOIDAttr.OID);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                this.SetValByKey(EntityOIDAttr.OID, value);
            }
        }
		#endregion

		#region ���캯��
		/// <summary>
		/// ����һ����ʵ��
		/// </summary>
		protected EntityOID()
		{
		}
		/// <summary>
		/// ����OID����ʵ��
		/// </summary>
		/// <param name="oid">oid</param>
		protected EntityOID(int oid)  
		{
			this.SetValByKey(EntityOIDAttr.OID,oid);
			this.Retrieve();
		}
		#endregion
	 
		#region override ����
		public override int DirectInsert()
		{
            this.OID =DBAccess.GenerOID();
			return base.DirectInsert ();
		}
		public void InsertAsNew()
		{
			this.OID=0;
			this.Insert();
		}
        public override bool IsExits
        {
            get
            {
                if (this.OID == 0)
                    return false;

                try
                {
                    // �������ݿ��ж���䡣
                    string selectSQL = "SELECT " + this.PKField + " FROM " + this.EnMap.PhysicsTable + " WHERE OID=" + this.HisDBVarStr + "v";
                    Paras ens = new Paras();
                    ens.Add("v", this.OID);

                    // �����ݿ������ѯ���ж���û�С�
                    switch (this.EnMap.EnDBUrl.DBUrlType)
                    {
                        case DBUrlType.AppCenterDSN:
                            return DBAccess.IsExits(selectSQL, ens);
                        case DBUrlType.DBAccessOfMSMSSQL:
                            return DBAccessOfMSMSSQL.IsExits(selectSQL);
                        case DBUrlType.DBAccessOfOLE:
                            return DBAccessOfOLE.IsExits(selectSQL);
                        case DBUrlType.DBAccessOfOracle:
                            return DBAccessOfOracle.IsExits(selectSQL);
                        default:
                            throw new Exception("û����Ƶ���" + this.EnMap.EnDBUrl.DBType);
                    }
                }
                catch(Exception ex)
                {
                    this.CheckPhysicsTable();
                    throw ex;
                }

                /* DEL BY PENG 2008-04-27
				// �������ݿ��ж���䡣
				string selectSQL="SELECT "+this.PKField + " FROM "+ this.EnMap.PhysicsTable + " WHERE " ;
				switch(this.EnMap.EnDBUrl.DBType )
				{
					case DBType.MSSQL:
						selectSQL +=SqlBuilder.GetKeyConditionOfMS(this);
						break;
					case DBType.Access:
						selectSQL +=SqlBuilder.GetKeyConditionOfOLE(this);
						break;
					case DBType.Oracle:
						selectSQL +=SqlBuilder.GetKeyConditionOfOracle(this);
						break; 
					default:
						throw new Exception("û����Ƶ���"+this.EnMap.EnDBUrl.DBType);
				}

				// �����ݿ������ѯ���ж���û�С�
				switch(this.EnMap.EnDBUrl.DBUrlType )
				{
					case DBUrlType.AppCenterDSN:
						return DBAccess.IsExits( selectSQL) ;
					case DBUrlType.DBAccessOfMSMSSQL:
						return DBAccessOfMSMSSQL.IsExits( selectSQL) ;
					case DBUrlType.DBAccessOfOLE:
						return DBAccessOfOLE.IsExits( selectSQL) ;
					case DBUrlType.DBAccessOfOracle:
						return DBAccessOfOracle.IsExits( selectSQL) ;
					default:
						throw new Exception("û����Ƶ���"+this.EnMap.EnDBUrl.DBType);				
				}
                */
            }
        }
		/// <summary>
		/// ɾ��֮ǰ�Ĳ�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete() 
		{
			if (base.beforeDelete()==false)
				return false;			
			try 
			{				
				if (this.OID < 0 )
					throw new Exception("@ʵ��["+this.EnDesc+"]û�б�ʵ����������Delete().");
				return true;
			} 
			catch (Exception ex) 
			{
				throw new Exception("@["+this.EnDesc+"].beforeDelete err:"+ex.Message);
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction ();
		}

		/// <summary>
		/// beforeInsert ֮ǰ�Ĳ�����
		/// </summary>
		/// <returns></returns>
        protected override bool beforeInsert()
        {
            if (this.OID > 0)
                throw new Exception("@[" + this.EnDesc + "], ʵ���Ѿ���ʵ���� oid=[" + this.OID + "]������Insert.");

            if (this.IsInnKey)
                this.OID = -1;
            else
                this.OID = BP.DA.DBAccess.GenerOID();


            return base.beforeInsert();
         
        }
		/// <summary>
		/// beforeUpdate
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			if (base.beforeUpdate()==false)
				return false;

			/*
			if (this.OID <= 0 )
				throw new Exception("@ʵ��["+this.EnDesc+"]û�б�ʵ����������Update().");
				*/
			return true;
		}
        protected virtual string SerialKey
        {
            get
            {
                return "OID";
            }
        }
       
		#endregion

		#region public ����
		/// <summary>
		/// ��Ϊһ���µ�ʵ�屣�档
		/// </summary>
        public void SaveAsNew()
        {
            try
            {
                this.OID = DBAccess.GenerOIDByKey32(this.SerialKey);
                this.RunSQL(SqlBuilder.Insert(this));
            }
            catch (System.Exception ex)
            {
                this.CheckPhysicsTable();
                throw ex;
            }
        }
		/// <summary>
		/// ����ָ����OID Insert.
		/// </summary>
        public void InsertAsOID(int oid)
        {
            this.SetValByKey("OID", oid);
            try
            {
                this.RunSQL(SqlBuilder.Insert(this));
            }
            catch (Exception ex)
            {
                this.CheckPhysicsTable();
                throw ex;
            }
        }
        public void InsertAsOID(Int64 oid)
        {
            this.SetValByKey("OID", oid);
            try
            {
                this.RunSQL(SqlBuilder.Insert(this));
            }
            catch (Exception ex)
            {
                this.CheckPhysicsTable();
                throw ex;
            }
        }
		/// <summary>
		/// ����ָ����OID ����
		/// </summary>
		/// <param name="oid"></param>
		public void SaveAsOID(int oid)
		{
			this.SetValByKey("OID",oid);
			if (this.IsExits==false)
				this.InsertAsOID(oid);
			this.Update();
		}
		#endregion
	}
	abstract public class EntitiesOID : Entities
	{
        /// <summary>
        /// ����
        /// </summary>
		public EntitiesOID()
		{
		}
		 
		#region ��ѯ����, ר�����������йص�ʵ��
		/// <summary>
		/// ��ѯ����, �������ĵ�ʵ�� . 
		/// </summary>
		public void RetrieveAllCNEntities()
		{
			this.RetrieveByLanguageNo("CH") ; 
		}
		/// <summary>
		/// �����Բ�ѯ�� 
		/// </summary>
		public void RetrieveByLanguageNo(string LanguageNo )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( "LanguageNo", LanguageNo );
			qo.DoQuery();
		}
		#endregion
	}
}
