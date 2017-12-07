using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.WF.Port;

namespace BP.WF
{
	/// <summary>
	/// �ڵ��������������
	/// </summary>
    public class NodeFlowAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ����������
        /// </summary>
        public const string FK_Flow = "FK_Flow";
    }
	/// <summary>
	/// �ڵ����������
	/// �ڵ�ĵ��������������������.	 
	/// ��¼�˴�һ���ڵ���������Ķ���ڵ�.
	/// Ҳ��¼�˵�������ڵ�������Ľڵ�.
	/// </summary>
    public class NodeFlow : EntityMM
    {
        #region ��������
        /// <summary>
        ///�ڵ�
        /// </summary>
        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(NodeFlowAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(NodeFlowAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(NodeFlowAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(NodeFlowAttr.FK_Flow, value);
            }
        }
        public string FK_FlowT
        {
            get
            {
                return this.GetValRefTextByKey(NodeFlowAttr.FK_Flow);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ڵ����������
        /// </summary>
        public NodeFlow() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NodeFlow");
                map.EnDesc = "�ڵ����������";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(NodeFlowAttr.FK_Node, 0, "�ڵ�", true, true);
                // map.AddDDLEntitiesPK(NodeFlowAttr.FK_Node, null, "�ڵ�", new NodeSheets(), true);
                map.AddDDLEntitiesPK(NodeFlowAttr.FK_Flow, null, "������", new Flows(), true);

                //map.AddSearchAttr(NodeFlowAttr.FK_Node);
                map.AddSearchAttr(NodeFlowAttr.FK_Flow);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// �ڵ����������
	/// </summary>
    public class NodeFlows : EntitiesMM
    {
        /// <summary>
        /// ���ĵ���������
        /// </summary>
        public Emps HisEmps
        {
            get
            {
                Emps ens = new Emps();
                foreach (NodeFlow ns in this)
                {
                    ens.AddEntity(new Emp(ns.FK_Flow));
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
                foreach (NodeFlow ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;

            }
        }
        /// <summary>
        /// �ڵ����������
        /// </summary>
        public NodeFlows() { }
        /// <summary>
        /// �ڵ����������
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public NodeFlows(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeFlowAttr.FK_Node, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// �ڵ����������
        /// </summary>
        /// <param name="EmpNo">EmpNo </param>
        public NodeFlows(string EmpNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeFlowAttr.FK_Flow, EmpNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ������� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeFlow();
            }
        }
        /// <summary>
        /// ȡ����һ�����������̼����ܹ����ʵ��õĽڵ�s
        /// </summary>
        /// <param name="sts">���������̼���</param>
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
        /// ���������̶�Ӧ�Ľڵ�
        /// </summary>
        /// <param name="EmpNo">���������̱��</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string EmpNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeFlowAttr.FK_Flow, EmpNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeFlow en in this)
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
            qo.AddWhere(NodeFlowAttr.FK_Node, nodeID);
            qo.DoQuery();

            Emps ens = new Emps();
            foreach (NodeFlow en in this)
            {
                ens.AddEntity(new Emp(en.FK_Flow));
            }
            return ens;
        }
    }
}
