using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
//using BP.ZHZS.Base;

namespace BP.WF
{
    /// <summary>
    /// �ɳ����Ľڵ�����	  
    /// </summary>
    public class NodeCancelAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ������
        /// </summary>
        public const string CancelTo = "CancelTo";
    }
    /// <summary>
    /// �ɳ����Ľڵ�
    /// �ڵ�ĳ����������������.	 
    /// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
    /// Ҳ��¼�˵�����ڵ�������Ľڵ�.
    /// </summary>
    public class NodeCancel : EntityMM
    {
        #region ��������
        /// <summary>
        ///������
        /// </summary>
        public int CancelTo
        {
            get
            {
                return this.GetValIntByKey(NodeCancelAttr.CancelTo);
            }
            set
            {
                this.SetValByKey(NodeCancelAttr.CancelTo, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(NodeCancelAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(NodeCancelAttr.FK_Node, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ɳ����Ľڵ�
        /// </summary>
        public NodeCancel() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NodeCancel");
                map.EnDesc = "�ɳ����Ľڵ�";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(NodeCancelAttr.FK_Node, 0, "�ڵ�", true, true);
                map.AddTBIntPK(NodeCancelAttr.CancelTo, 0, "������", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// �ɳ����Ľڵ�
    /// </summary>
    public class NodeCancels : EntitiesMM
    {
        /// <summary>
        /// ���ĳ�����
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (NodeCancel ns in this)
                {
                    ens.AddEntity(new Node(ns.CancelTo));
                }
                return ens;
            }
        }
        /// <summary>
        /// �ɳ����Ľڵ�
        /// </summary>
        public NodeCancels() { }
        /// <summary>
        /// �ɳ����Ľڵ�
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public NodeCancels(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeCancelAttr.FK_Node, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// �ɳ����Ľڵ�
        /// </summary>
        /// <param name="NodeNo">NodeNo </param>
        public NodeCancels(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeCancelAttr.CancelTo, NodeNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeCancel();
            }
        }
        /// <summary>
        /// �ɳ����Ľڵ�s
        /// </summary>
        /// <param name="sts">�ɳ����Ľڵ�</param>
        /// <Cancels></Cancels>
        public Nodes GetHisNodes(Nodes sts)
        {
            Nodes nds = new Nodes();
            Nodes tmp = new Nodes();
            foreach (Node st in sts)
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
        /// �ɳ����Ľڵ�
        /// </summary>
        /// <param name="NodeNo">���������</param>
        /// <Cancels>�ڵ�s</Cancels>
        public Nodes GetHisNodes(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeCancelAttr.CancelTo, NodeNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeCancel en in this)
            {
                ens.AddEntity(new Node(en.FK_Node));
            }
            return ens;
        }
        /// <summary>
        /// ת��˽ڵ�ļ��ϵ�Nodes
        /// </summary>
        /// <param name="nodeID">�˽ڵ��ID</param>
        /// <Cancels>ת��˽ڵ�ļ��ϵ�Nodes (FromNodes)</Cancels> 
        public Nodes GetHisNodes(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeCancelAttr.FK_Node, nodeID);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeCancel en in this)
            {
                ens.AddEntity(new Node(en.CancelTo));
            }
            return ens;
        }
    }
}
