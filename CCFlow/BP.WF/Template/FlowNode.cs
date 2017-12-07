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
    /// ����  
    /// </summary>
    public class FlowNodeAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
    }
    /// <summary>
    /// ���̽ڵ�
    /// </summary>
    public class FlowNode : EntityMM
    {
        #region ��������
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        ///�ڵ�
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(FlowNodeAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(FlowNodeAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(FlowNodeAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(FlowNodeAttr.FK_Flow, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        public FlowNode() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_FlowNode");
                map.EnDesc = "���̳��ͽڵ�";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(FlowNodeAttr.FK_Flow, null, "���̱��", true, true, 1, 20, 20);
                map.AddTBStringPK(FlowNodeAttr.FK_Node, null, "�ڵ�", true, true, 1, 20, 20);

                //      map.AddDDLEntitiesPK(FlowNodeAttr.FK_Flow, null, "FK_Flow", new Flows(), true);
                //     map.AddDDLEntitiesPK(FlowNodeAttr.FK_Node, null, "�����ڵ�", new Nodes(), true);
                this._enMap = map;
                

                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ����
    /// </summary>
    public class FlowNodes : EntitiesMM
    {
        /// <summary>
        /// ���Ĺ����ڵ�
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (FlowNode ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;
            }
        }
        /// <summary>
        /// ���̳��ͽڵ�
        /// </summary>
        public FlowNodes() { }
        /// <summary>
        /// ���̳��ͽڵ�
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public FlowNodes(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowNodeAttr.FK_Flow, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// ���̳��ͽڵ�
        /// </summary>
        /// <param name="NodeNo">NodeNo </param>
        public FlowNodes(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowNodeAttr.FK_Node, NodeNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FlowNode();
            }
        }
        /// <summary>
        /// ���̳��ͽڵ�s
        /// </summary>
        /// <param name="sts">���̳��ͽڵ�</param>
        /// <returns></returns>
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
        /// ���̳��ͽڵ�
        /// </summary>
        /// <param name="NodeNo">�����ڵ���</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowNodeAttr.FK_Node, NodeNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (FlowNode en in this)
            {
                ens.AddEntity(new Node(en.FK_Flow));
            }
            return ens;
        }
        /// <summary>
        /// ת��˽ڵ�ļ��ϵ�Nodes
        /// </summary>
        /// <param name="nodeID">�˽ڵ��ID</param>
        /// <returns>ת��˽ڵ�ļ��ϵ�Nodes (FromNodes)</returns> 
        public Nodes GetHisNodes(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowNodeAttr.FK_Flow, nodeID);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (FlowNode en in this)
            {
                ens.AddEntity(new Node(en.FK_Node));
            }
            return ens;
        }
    }
}
