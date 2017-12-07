using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// ��ʼ������������
	/// </summary>
    public class StartWorkAttr : WorkAttr
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// �������ݱ���
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// PRI
        /// </summary>
        public const string PRI = "PRI";
        #endregion

        #region ������������
        public const string PFlowNo = "PFlowNo";
        public const string PWorkID = "PWorkID";
        public const string PNodeID = "PNodeID";
        #endregion ������������
    }
	/// <summary>	 
	/// ��ʼ��������,���п�ʼ������Ҫ������̳�
	/// </summary>
	abstract public class StartWork : Work 
	{
        #region ��_SQLCash �����й�
        private SQLCash _SQLCash = null;
        public override SQLCash SQLCash
        {
            get
            {
                if (_SQLCash == null)
                {
                    _SQLCash = BP.DA.Cash.GetSQL(this.NodeID.ToString());
                    if (_SQLCash == null)
                    {
                        _SQLCash = new SQLCash(this);
                        BP.DA.Cash.SetSQL(this.NodeID.ToString(), _SQLCash);
                    }
                }
                return _SQLCash;
            }
            set
            {
                _SQLCash = value;
            }
        }
        #endregion

		#region  ��������
		/// <summary>
		/// FK_Dept
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey(StartWorkAttr.FK_Dept);
			}
            set
            {
                this.SetValByKey(StartWorkAttr.FK_Dept, value);
            } 
		}
        //public string FK_DeptOf2Code
        //{
        //    get
        //    {
        //        return this.FK_Dept.Substring(6);
        //    } 
        //}
		/// <summary>
		/// FK_XJ
		/// </summary>
        //public string FK_XJ
        //{
        //    get
        //    {
        //        return this.GetValStringByKey(StartWorkAttr.FK_Dept);
        //    }
        //    set
        //    {
        //        this.SetValByKey(StartWorkAttr.FK_Dept, value);
        //    }
        //}
		#endregion

		#region ��������
		/// <summary>
		/// �������ݱ���
		/// </summary>
		public string Title
		{
			get
			{
				return this.GetValStringByKey(StartWorkAttr.Title);
			}
			set
			{
				this.SetValByKey(StartWorkAttr.Title,value);
			} 
		}
		#endregion

		#region ���캯��
		/// <summary>
		/// ��������
		/// </summary>
		protected StartWork()
		{
		}
        protected StartWork(Int64 oid):base(oid)
        {
        }
		#endregion
		
		#region  ��д����ķ�����
		/// <summary>
		/// ɾ��֮ǰ�Ĳ�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete() 
		{
			if (base.beforeDelete()==false)
				return false;			 
			if (this.OID < 0 )
				throw new Exception("@ʵ��["+this.EnDesc+"]û�б�ʵ����������Delete().");
			return true;
		}
		/// <summary>
		/// ����֮ǰ�Ĳ�����
		/// </summary>
		/// <returns></returns>
        protected override bool beforeInsert()
        {
            if (this.OID > 0)
                throw new Exception("@ʵ��[" + this.EnDesc + "], �Ѿ���ʵ����������Insert.");

            this.SetValByKey("OID", DBAccess.GenerOID());
            return base.beforeInsert();
        }
        protected override bool beforeUpdateInsertAction()
        {
            this.Emps = BP.Web.WebUser.No;
            return base.beforeUpdateInsertAction();
        }
		/// <summary>
		/// ���²���
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			if (base.beforeUpdate()==false)
				return false;
			if (this.OID < 0 )			
				throw new Exception("@ʵ��["+this.EnDesc+"]û�б�ʵ����������Update().");
			return base.beforeUpdate();
		}
		#endregion
	}
	/// <summary>
	/// �������̲ɼ���Ϣ�Ļ��� ����
	/// </summary>
	abstract public class StartWorks : Works
	{
		#region ���췽��
		/// <summary>
		/// ��Ϣ�ɼ�����
		/// </summary>
		public StartWorks()
		{
		}
		#endregion 

		#region ������ѯ����
		/// <summary>
		/// ��ѯ���ҵ�����.
		/// </summary>		 
		/// <returns></returns>
		public DataTable RetrieveMyTask_del(string flow)
		{
			QueryObject qo = new QueryObject(this);
			//qo.Top=50;
            qo.AddWhere(StartWorkAttr.OID, " in ", " ( SELECT WorkID FROM V_WF_Msg  WHERE  FK_Flow='" + flow + "' AND FK_Emp='" + BP.Web.WebUser.No + "' )");
			return qo.DoQueryToTable();
		}
		/// <summary>
		/// ��ѯ���ҵ�����.
		/// </summary>		 
		/// <returns></returns>
		public DataTable RetrieveMyTask(string flow)
		{
			//string sql="SELECT OID AS WORKID, TITLE, ";
			QueryObject qo = new QueryObject(this);
			//qo.Top=50;
			if (BP.Sys.SystemConfig.AppCenterDBType==DBType.Oracle)
                qo.AddWhere(StartWorkAttr.OID, " in ", " (  SELECT WorkID FROM WF_GenerWorkFlow WHERE FK_Node IN ( SELECT FK_Node FROM WF_GenerWorkerlist WHERE FK_Emp='" + BP.Web.WebUser.No + "' AND FK_Flow='" + flow + "' AND WORKID=WF_GenerWorkFlow.WORKID ) )");
			else
                qo.AddWhere(StartWorkAttr.OID, " in ", " (  SELECT WorkID FROM WF_GenerWorkFlow WHERE FK_Node IN ( SELECT FK_Node FROM WF_GenerWorkerlist WHERE FK_Emp='" + BP.Web.WebUser.No + "' AND FK_Flow='" + flow + "' AND WORKID=WF_GenerWorkFlow.WORKID ) )");
			return qo.DoQueryToTable();
		}
		/// <summary>
		/// ��ѯ���ҵ�����.
		/// </summary>		 
		/// <returns></returns>
		public DataTable RetrieveMyTaskV2(string flow)
		{
            string sql = "SELECT OID, TITLE, RDT, Rec FROM  " + this.GetNewEntity.EnMap.PhysicsTable + " WHERE OID IN (   SELECT WorkID FROM WF_GenerWorkFlow WHERE FK_Node IN ( SELECT FK_Node FROM WF_GenerWorkerlist WHERE FK_Emp='" + BP.Web.WebUser.No + "' AND FK_Flow='" + flow + "' AND WORKID=WF_GenerWorkFlow.WORKID )  )";
			return DBAccess.RunSQLReturnTable(sql) ;
			/*
			QueryObject qo = new QueryObject(this);
			//qo.Top=50;
			qo.AddWhere(StartWorkAttr.OID," in ", " ( SELECT WorkID FROM V_WF_Msg  WHERE  FK_Flow='"+flow+"' AND FK_Emp="+Web.WebUser.No+")" );
			return qo.DoQueryToTable();
			*/
		}
		/// <summary>
		/// ��ѯ���ҵ�����.
		/// </summary>		 
		/// <returns></returns>
		public DataTable RetrieveMyTask(string flow,string flowSort)
		{
			QueryObject qo = new QueryObject(this);
			//qo.Top=50;
            qo.AddWhere(StartWorkAttr.OID, " IN ", " ( SELECT WorkID FROM V_WF_Msg  WHERE  (FK_Flow='" + flow + "' AND FK_Emp='" + BP.Web.WebUser.No + "' ) AND ( FK_Flow in ( SELECT No from WF_Flow WHERE FK_FlowSort='" + flowSort + "' )) )");
			return qo.DoQueryToTable();			 
		}
		#endregion 
	}
}
