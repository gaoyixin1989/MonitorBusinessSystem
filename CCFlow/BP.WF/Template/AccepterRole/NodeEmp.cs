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
	public class NodeEmpAttr
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
	public class NodeEmp :EntityMM
	{
		#region ��������
		/// <summary>
		///�ڵ�
		/// </summary>
		public int  FK_Node
		{
			get
			{
				return this.GetValIntByKey(NodeEmpAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(NodeEmpAttr.FK_Node,value);
			}
		}
		/// <summary>
		/// ����Ա
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(NodeEmpAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(NodeEmpAttr.FK_Emp,value);
			}
		}
        public string FK_EmpT
        {
            get
            {
                return this.GetValRefTextByKey(NodeEmpAttr.FK_Emp);
            }
        }
		#endregion 

		#region ���췽��
		/// <summary>
		/// �ڵ㵽��Ա
		/// </summary>
		public NodeEmp(){}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NodeEmp");
                map.EnDesc = "�ڵ���Ա";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(NodeEmpAttr.FK_Node,0,"Node",true,true );
                map.AddDDLEntitiesPK(NodeEmpAttr.FK_Emp, null, "����Ա", new Emps(), true);

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion
	}
	/// <summary>
	/// �ڵ㵽��Ա
	/// </summary>
    public class NodeEmps : EntitiesMM
    {
        /// <summary>
        /// ���ĵ���Ա
        /// </summary>
        public Emps HisEmps
        {
            get
            {
                Emps ens = new Emps();
                foreach (NodeEmp ns in this)
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
                foreach (NodeEmp ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;

            }
        }
        /// <summary>
        /// �ڵ㵽��Ա
        /// </summary>
        public NodeEmps() { }
        /// <summary>
        /// �ڵ㵽��Ա
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public NodeEmps(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeEmpAttr.FK_Node, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// �ڵ㵽��Ա
        /// </summary>
        /// <param name="EmpNo">EmpNo </param>
        public NodeEmps(string EmpNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeEmpAttr.FK_Emp, EmpNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeEmp();
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
            qo.AddWhere(NodeEmpAttr.FK_Emp, EmpNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeEmp en in this)
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
            qo.AddWhere(NodeEmpAttr.FK_Node, nodeID);
            qo.DoQuery();

            Emps ens = new Emps();
            foreach (NodeEmp en in this)
            {
                ens.AddEntity(new Emp(en.FK_Emp));
            }
            return ens;
        }
    }
}
