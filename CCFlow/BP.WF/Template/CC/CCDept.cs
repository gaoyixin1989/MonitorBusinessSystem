using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.WF.Port;

namespace BP.WF.Template
{
	/// <summary>
	/// ���Ͳ�������	  
	/// </summary>
	public class CCDeptAttr
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
	/// ���Ͳ���
	/// �ڵ�Ĺ������������������.	 
	/// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
	/// Ҳ��¼�˵�����ڵ�������Ľڵ�.
	/// </summary>
	public class CCDept :EntityMM
	{
		#region ��������
		/// <summary>
		///�ڵ�
		/// </summary>
		public int  FK_Node
		{
			get
			{
				return this.GetValIntByKey(CCDeptAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(CCDeptAttr.FK_Node,value);
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey(CCDeptAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(CCDeptAttr.FK_Dept,value);
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ���Ͳ���
		/// </summary>
		public CCDept(){}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("WF_CCDept");				 
				map.EnDesc="���Ͳ���";

				map.DepositaryOfEntity=Depositary.None;
				map.DepositaryOfMap=Depositary.Application;
				map.AddDDLEntitiesPK(CCDeptAttr.FK_Node,0,DataType.AppInt,"�ڵ�",new Nodes(),NodeAttr.NodeID,NodeAttr.Name,true);
				map.AddDDLEntitiesPK( CCDeptAttr.FK_Dept,null,"����",new Depts(),true);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion
	}
	/// <summary>
	/// ���Ͳ���
	/// </summary>
    public class CCDepts : EntitiesMM
    {
        /// <summary>
        /// ���Ĺ�������
        /// </summary>
        public Stations HisStations
        {
            get
            {
                Stations ens = new Stations();
                foreach (CCDept ns in this)
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
                foreach (CCDept ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;
            }
        }
        /// <summary>
        /// ���Ͳ���
        /// </summary>
        public CCDepts() { }
        /// <summary>
        /// ���Ͳ���
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public CCDepts(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CCDeptAttr.FK_Node, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// ���Ͳ���
        /// </summary>
        /// <param name="StationNo">StationNo </param>
        public CCDepts(string StationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CCDeptAttr.FK_Dept, StationNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new CCDept();
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
            qo.AddWhere(CCDeptAttr.FK_Dept, stationNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (CCDept en in this)
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
            qo.AddWhere(CCDeptAttr.FK_Node, nodeID);
            qo.DoQuery();

            Stations ens = new Stations();
            foreach (CCDept en in this)
            {
                ens.AddEntity(new Station(en.FK_Dept));
            }
            return ens;
        }
    }
}
