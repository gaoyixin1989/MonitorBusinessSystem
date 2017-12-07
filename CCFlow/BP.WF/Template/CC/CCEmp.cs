using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.WF.Port;
//using BP.ZHZS.Base;

namespace BP.WF.Template
{
	/// <summary>
	/// �ڵ㵽��Ա����
	/// </summary>
	public class CCEmpAttr
	{
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// ����Ա
		/// </summary>
		public const string FK_Emp="FK_Emp";
	}
	/// <summary>
	/// �ڵ㵽��Ա
	/// �ڵ�ĵ���Ա�����������.	 
	/// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
	/// Ҳ��¼�˵�����ڵ�������Ľڵ�.
	/// </summary>
	public class CCEmp :EntityMM
	{
		#region ��������
		/// <summary>
		///�ڵ�
		/// </summary>
		public int  FK_Node
		{
			get
			{
				return this.GetValIntByKey(CCEmpAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(CCEmpAttr.FK_Node,value);
			}
		}
		/// <summary>
		/// ����Ա
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(CCEmpAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(CCEmpAttr.FK_Emp,value);
			}
		}
        public string FK_EmpT
        {
            get
            {
                return this.GetValRefTextByKey(CCEmpAttr.FK_Emp);
            }
        }
		#endregion 

		#region ���췽��
		/// <summary>
		/// �ڵ㵽��Ա
		/// </summary>
		public CCEmp(){}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_CCEmp");
                map.EnDesc = "������Ա";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(CCEmpAttr.FK_Node, 0, "�ڵ�", true, true);
                map.AddDDLEntitiesPK(CCEmpAttr.FK_Emp, null, "��Ա", new Emps(), true);

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion
	}
	/// <summary>
	/// �ڵ㵽��Ա
	/// </summary>
    public class CCEmps : EntitiesMM
    {
        /// <summary>
        /// ���ĵ���Ա
        /// </summary>
        public Emps HisEmps
        {
            get
            {
                Emps ens = new Emps();
                foreach (CCEmp ns in this)
                {
                    ens.AddEntity(new Emp(ns.FK_Emp));
                }
                return ens;
            }
        }
        /// <summary>
        /// ���Ĺ����ڵ�
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (CCEmp ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;

            }
        }
        /// <summary>
        /// �ڵ㵽��Ա
        /// </summary>
        public CCEmps() { }
        /// <summary>
        /// �ڵ㵽��Ա
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public CCEmps(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CCEmpAttr.FK_Node, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// �ڵ㵽��Ա
        /// </summary>
        /// <param name="EmpNo">EmpNo </param>
        public CCEmps(string EmpNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CCEmpAttr.FK_Emp, EmpNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new CCEmp();
            }
        }
        /// <summary>
        /// ȡ��һ������Ա�����ܹ����ʵ��Ľڵ�s
        /// </summary>
        /// <param name="sts">����Ա����</param>
        /// <returns></returns>
        public Nodes GetHisNodes(Emps sts)
        {
            Nodes nds = new Nodes();
            Nodes tmp = new Nodes();
            foreach (Emp st in sts)
            {
                tmp = this.GetHisNodes(st.No);
                foreach (Node nd in tmp)
                {
                    if (nds.Contains(nd))
                        continue;
                    nds.AddEntity(nd);
                }
            }
            return nds;
        }
        /// <summary>
        /// ����Ա��Ӧ�Ľڵ�
        /// </summary>
        /// <param name="EmpNo">����Ա���</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string EmpNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CCEmpAttr.FK_Emp, EmpNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (CCEmp en in this)
            {
                ens.AddEntity(new Node(en.FK_Node));
            }
            return ens;
        }
        /// <summary>
        /// ת��˽ڵ�ļ��ϵ� Nodes
        /// </summary>
        /// <param name="nodeID">�˽ڵ��ID</param>
        /// <returns>ת��˽ڵ�ļ��ϵ�Nodes (FromNodes)</returns> 
        public Emps GetHisEmps(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CCEmpAttr.FK_Node, nodeID);
            qo.DoQuery();

            Emps ens = new Emps();
            foreach (CCEmp en in this)
            {
                ens.AddEntity(new Emp(en.FK_Emp));
            }
            return ens;
        }
    }
}
