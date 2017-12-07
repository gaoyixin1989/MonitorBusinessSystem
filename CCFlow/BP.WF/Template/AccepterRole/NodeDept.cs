using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.WF.Port;

namespace BP.WF.Template
{
	/// <summary>
	/// �ڵ㲿������	  
	/// </summary>
	public class NodeDeptAttr
	{
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// ��������
		/// </summary>
		public const string FK_Dept="FK_Dept";
	}
	/// <summary>
	/// �ڵ㲿��
	/// �ڵ�Ĺ������������������.	 
	/// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
	/// Ҳ��¼�˵�����ڵ�������Ľڵ�.
	/// </summary>
	public class NodeDept :EntityMM
	{
		#region ��������
		/// <summary>
		///�ڵ�
		/// </summary>
		public int  FK_Node
		{
			get
			{
				return this.GetValIntByKey(NodeDeptAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(NodeDeptAttr.FK_Node,value);
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey(NodeDeptAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(NodeDeptAttr.FK_Dept,value);
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// �ڵ㲿��
		/// </summary>
		public NodeDept(){}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("WF_NodeDept");				 
				map.EnDesc="�ڵ㲿��";

				map.DepositaryOfEntity=Depositary.None;
				map.DepositaryOfMap=Depositary.Application;

				map.AddDDLEntitiesPK(NodeDeptAttr.FK_Node,0,DataType.AppInt,"�ڵ�",new Nodes(),
                    NodeAttr.NodeID,NodeAttr.Name,true);
				map.AddDDLEntitiesPK( NodeDeptAttr.FK_Dept,null,"����",new Depts(),true);

				this._enMap=map;
				 
				return this._enMap;
			}
		}
		#endregion

	}
	/// <summary>
	/// �ڵ㲿��
	/// </summary>
    public class NodeDepts : EntitiesMM
    {
        /// <summary>
        /// ���Ĺ�������
        /// </summary>
        public Stations HisStations
        {
            get
            {
                Stations ens = new Stations();
                foreach (NodeDept ns in this)
                {
                    ens.AddEntity(new Station(ns.FK_Dept));
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
                foreach (NodeDept ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;

            }
        }
        /// <summary>
        /// �ڵ㲿��
        /// </summary>
        public NodeDepts() { }
        /// <summary>
        /// �ڵ㲿��
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public NodeDepts(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeDeptAttr.FK_Node, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// �ڵ㲿��
        /// </summary>
        /// <param name="StationNo">StationNo </param>
        public NodeDepts(string StationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeDeptAttr.FK_Dept, StationNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeDept();
            }
        }
        /// <summary>
        /// ȡ��һ���������ż����ܹ����ʵ��Ľڵ�s
        /// </summary>
        /// <param name="sts">�������ż���</param>
        /// <returns></returns>
        public Nodes GetHisNodes(Stations sts)
        {
            Nodes nds = new Nodes();
            Nodes tmp = new Nodes();
            foreach (Station st in sts)
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
        /// ȡ��һ��������Ա�ܹ����ʵ��Ľڵ㡣
        /// </summary>
        /// <param name="empId">������ԱID</param>
        /// <returns></returns>
        public Nodes GetHisNodes_del(string empId)
        {
            Emp em = new Emp(empId);
            return this.GetHisNodes(em.HisStations);
        }
        /// <summary>
        /// �������Ŷ�Ӧ�Ľڵ�
        /// </summary>
        /// <param name="stationNo">�������ű��</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string stationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeDeptAttr.FK_Dept, stationNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeDept en in this)
            {
                ens.AddEntity(new Node(en.FK_Node));
            }
            return ens;
        }
        /// <summary>
        /// ת��˽ڵ�ļ��ϵ�Nodes
        /// </summary>
        /// <param name="nodeID">�˽ڵ��ID</param>
        /// <returns>ת��˽ڵ�ļ��ϵ�Nodes (FromNodes)</returns> 
        public Stations GetHisStations(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeDeptAttr.FK_Node, nodeID);
            qo.DoQuery();

            Stations ens = new Stations();
            foreach (NodeDept en in this)
            {
                ens.AddEntity(new Station(en.FK_Dept));
            }
            return ens;
        }

    }
}
