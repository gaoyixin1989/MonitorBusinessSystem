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
    /// ���˻صĽڵ�����	  
    /// </summary>
    public class NodeReturnAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �˻ص�
        /// </summary>
        public const string ReturnTo = "ReturnTo";
        /// <summary>
        /// �м��
        /// </summary>
        public const string Dots = "Dots";
    }
    /// <summary>
    /// ���˻صĽڵ�
    /// �ڵ���˻ص������������.	 
    /// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
    /// Ҳ��¼�˵�����ڵ�������Ľڵ�.
    /// </summary>
    public class NodeReturn : EntityMM
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
        ///�˻ص�
        /// </summary>
        public int ReturnTo
        {
            get
            {
                return this.GetValIntByKey(NodeReturnAttr.ReturnTo);
            }
            set
            {
                this.SetValByKey(NodeReturnAttr.ReturnTo, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(NodeReturnAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(NodeReturnAttr.FK_Node, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ���˻صĽڵ�
        /// </summary>
        public NodeReturn() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NodeReturn");
                map.EnDesc = "���˻صĽڵ�";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(NodeReturnAttr.FK_Node, 0, "�ڵ�", true, true);
                map.AddTBIntPK(NodeReturnAttr.ReturnTo, 0, "�˻ص�", true, true);
                map.AddTBString(NodeReturnAttr.Dots, null, "�켣��Ϣ", true, true,0,300,0,false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ���˻صĽڵ�
    /// </summary>
    public class NodeReturns : EntitiesMM
    {
        /// <summary>
        /// �����˻ص�
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (NodeReturn ns in this)
                {
                    ens.AddEntity(new Node(ns.ReturnTo));
                }
                return ens;
            }
        }
        /// <summary>
        /// ���˻صĽڵ�
        /// </summary>
        public NodeReturns() { }
        /// <summary>
        /// ���˻صĽڵ�
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public NodeReturns(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.FK_Node, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// ���˻صĽڵ�
        /// </summary>
        /// <param name="NodeNo">NodeNo </param>
        public NodeReturns(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.ReturnTo, NodeNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeReturn();
            }
        }
        /// <summary>
        /// ���˻صĽڵ�s
        /// </summary>
        /// <param name="sts">���˻صĽڵ�</param>
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
        /// ���˻صĽڵ�
        /// </summary>
        /// <param name="NodeNo">�˻ص����</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.ReturnTo, NodeNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeReturn en in this)
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
        public Nodes GetHisNodes(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.FK_Node, nodeID);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeReturn en in this)
            {
                ens.AddEntity(new Node(en.ReturnTo));
            }
            return ens;
        }
    }
}
