using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
using BP.Sys;

namespace BP.WF.Template
{
    /// <summary>
    /// �ڵ������	  
    /// </summary>
    public class FrmNodeAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Frm = "FK_Frm";
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �Ƿ�readonly.
        /// </summary>
        public const string IsEdit = "IsEdit";
        /// <summary>
        /// IsPrint
        /// </summary>
        public const string IsPrint = "IsPrint";
        /// <summary>
        /// �Ƿ�����װ������¼�.
        /// </summary>
        public const string IsEnableLoadData = "IsEnableLoadData";
        /// <summary>
        /// Idx
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// FK_Flow
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ������
        /// </summary>
        public const string FrmType = "FrmType";
        /// <summary>
        /// ����
        /// </summary>
        public const string FrmSln = "FrmSln";
        /// <summary>
        /// ˭��������
        /// </summary>
        public const string WhoIsPK = "WhoIsPK";
    }
    /// <summary>
    /// ˭��������
    /// </summary>
    public enum WhoIsPK
    {
        /// <summary>
        /// ����ID������
        /// </summary>
        OID,
        /// <summary>
        /// ����ID������
        /// </summary>
        FID,
        /// <summary>
        /// ������ID������
        /// </summary>
        PWorkID,
        /// <summary>
        /// ��������ID������
        /// </summary>
        CWorkID
    }
    /// <summary>
    /// �ڵ��
    /// �ڵ�Ĺ����ڵ������������.	 
    /// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
    /// Ҳ��¼�˵�����ڵ�������Ľڵ�.
    /// </summary>
    public class FrmNode : EntityMyPK
    {
        #region ���ڽڵ���office����toolbarȨ�޿��Ʒ���.
        
        #endregion ���ڽڵ���office����toolbarȨ�޿��Ʒ���.

        #region ��������
        public string FrmUrl
        {
            get
            {
                switch (this.HisFrmType)
                {
                    case FrmType.Column4Frm:
                        return Glo.CCFlowAppPath + "WF/CCForm/FrmFix";
                    case FrmType.FreeFrm:
                        return Glo.CCFlowAppPath + "WF/CCForm/Frm";
                    case FrmType.SLFrm:
                        return Glo.CCFlowAppPath + "WF/CCForm/SLFrm";
                    default:
                        throw new Exception("err,δ����");
                }
            }
        }
        private Frm _hisFrm = null;
        public Frm HisFrm
        {
            get
            {
                if (this._hisFrm == null)
                {
                    this._hisFrm = new Frm(this.FK_Frm);
                    this._hisFrm.HisFrmNode = this;
                }
                return this._hisFrm;
            }
        }
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
        public BP.Sys.FrmType HisFrmType
        {
            get
            {
                return (BP.Sys.FrmType)this.GetValIntByKey(FrmNodeAttr.FrmType);
            }
            set
            {
                this.SetValByKey(FrmNodeAttr.FrmType, (int)value);
            }
        }
        /// <summary>
        /// �Ƿ�����װ������¼�
        /// </summary>
        public bool IsEnableLoadData
        {
            get
            {
                return this.GetValBooleanByKey(FrmNodeAttr.IsEnableLoadData);
            }
            set
            {
                this.SetValByKey(FrmNodeAttr.IsEnableLoadData, value);
            }
        }
        /// <summary>
        ///�ڵ�
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(FrmNodeAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(FrmNodeAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// ˳���
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(FrmNodeAttr.Idx);
            }
            set
            {
                this.SetValByKey(FrmNodeAttr.Idx, value);
            }
        }
        /// <summary>
        /// ˭��������
        /// </summary>
        public WhoIsPK WhoIsPK
        {
            get
            {
                return (WhoIsPK)this.GetValIntByKey(FrmNodeAttr.WhoIsPK);
            }
            set
            {
                this.SetValByKey(FrmNodeAttr.WhoIsPK, (int)value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FK_Frm
        {
            get
            {
                return this.GetValStringByKey(FrmNodeAttr.FK_Frm);
            }
            set
            {
                this.SetValByKey(FrmNodeAttr.FK_Frm, value);
            }
        }
        /// <summary>
        /// ��Ӧ�Ľ������
        /// </summary>
        public int FrmSln
        {
            get
            {
                return this.GetValIntByKey(FrmNodeAttr.FrmSln);
            }
            set
            {
                this.SetValByKey(FrmNodeAttr.FrmSln, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(FrmNodeAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(FrmNodeAttr.FK_Flow, value);
            }
        }
        public bool IsEdit
        {
            get
            {
                return this.GetValBooleanByKey(FrmNodeAttr.IsEdit);
            }
            set
            {
                this.SetValByKey(FrmNodeAttr.IsEdit, value);
            }
        }
        public bool IsPrint
        {
            get
            {
                return this.GetValBooleanByKey(FrmNodeAttr.IsPrint);
            }
            set
            {
                this.SetValByKey(FrmNodeAttr.IsPrint, value);
            }
        }
        public int IsEditInt
        {
            get
            {
                return this.GetValIntByKey(FrmNodeAttr.IsEdit);
            }
        }
        public int IsPrintInt
        {
            get
            {
                return this.GetValIntByKey(FrmNodeAttr.IsPrint);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ڵ��
        /// </summary>
        public FrmNode() { }
        /// <summary>
        /// �ڵ��
        /// </summary>
        /// <param name="mypk"></param>
        public FrmNode(string mypk)
            : base(mypk)
        {
        }
        /// <summary>
        /// �ڵ��
        /// </summary>
        /// <param name="fk_node">�ڵ�</param>
        /// <param name="fk_frm">��</param>
        public FrmNode(string fk_flow, int fk_node, string fk_frm)
        {
            int i = this.Retrieve(FrmNodeAttr.FK_Flow, fk_flow, FrmNodeAttr.FK_Node, fk_node, FrmNodeAttr.FK_Frm, fk_frm);
            if (i == 0)
            {
                this.IsPrint = false;
                this.IsEdit = false;
                return;
                throw new Exception("@��������Ϣ�ѱ�ɾ����");
            }
        }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_FrmNode");
                map.EnDesc = "�ڵ��";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();
                map.AddTBString(FrmNodeAttr.FK_Frm, null, "��ID", true, true, 1, 32, 32);
                map.AddTBInt(FrmNodeAttr.FK_Node, 0, "�ڵ���", true, false);
                map.AddTBString(FrmNodeAttr.FK_Flow, null, "���̱��", true, true, 1, 20, 20);

                map.AddTBString(FrmNodeAttr.FrmType, "0", "������", true, true, 1, 20, 20);

                //�˵��ڱ��ڵ��Ȩ�޿���.
                map.AddTBInt(FrmNodeAttr.IsEdit, 1, "�Ƿ���Ը���", true, false);
                map.AddTBInt(FrmNodeAttr.IsPrint, 0, "IsPrint", true, false);
                map.AddTBInt(FrmNodeAttr.IsEnableLoadData, 0, "�Ƿ�����װ������¼�", true, false);


                //��ʾ��
                map.AddTBInt(FrmNodeAttr.Idx, 0, "˳���", true, false);
                map.AddTBInt(FrmNodeAttr.FrmSln, 0, "�����Ʒ���", true, false);

                // add 2014-01-26
                map.AddTBInt(FrmNodeAttr.WhoIsPK, 0, "˭��������", true, false);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public void DoUp()
        {
            this.DoOrderUp(FrmNodeAttr.FK_Node, this.FK_Node.ToString(), FrmNodeAttr.Idx);
        }
        public void DoDown()
        {
            this.DoOrderDown(FrmNodeAttr.FK_Node, this.FK_Node.ToString(), FrmNodeAttr.Idx);
        }

        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_Frm + "_" + this.FK_Node + "_" + this.FK_Flow;
            return base.beforeUpdateInsertAction();
        }
    }
    /// <summary>
    /// �ڵ��
    /// </summary>
    public class FrmNodes : EntitiesMM
    {
        /// <summary>
        /// ���Ĺ����ڵ�
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (FrmNode ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;
            }
        }
        /// <summary>
        /// �ڵ��
        /// </summary>
        public FrmNodes() { }
        /// <summary>
        /// �ڵ��
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public FrmNodes(string fk_flow, int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FrmNodeAttr.FK_Flow, fk_flow);
            qo.addAnd();
            qo.AddWhere(FrmNodeAttr.FK_Node, nodeID);

            qo.addOrderBy(FrmNodeAttr.Idx);
            qo.DoQuery();
        }
        /// <summary>
        /// �ڵ��
        /// </summary>
        /// <param name="NodeNo">NodeNo </param>
        public FrmNodes(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FrmNodeAttr.FK_Node, NodeNo);
            qo.addOrderBy(FrmNodeAttr.Idx);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmNode();
            }
        }
        /// <summary>
        /// �ڵ��s
        /// </summary>
        /// <param name="sts">�ڵ��</param>
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
        /// �ڵ��
        /// </summary>
        /// <param name="NodeNo">�����ڵ���</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FrmNodeAttr.FK_Node, NodeNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (FrmNode en in this)
            {
                ens.AddEntity(new Node(en.FK_Frm));
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
            qo.AddWhere(FrmNodeAttr.FK_Frm, nodeID);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (FrmNode en in this)
            {
                ens.AddEntity(new Node(en.FK_Node));
            }
            return ens;
        }
    }
}
