using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// ��Ϣ��������
	/// </summary>
    public class ListenAttr
    {
        public const string Doc = "Doc";
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string Nodes = "Nodes";
        /// <summary>
        /// ����
        /// </summary>
        public const string NodesDesc = "NodesDesc";
        /// <summary>
        /// ������ʽ
        /// </summary>
        public const string AlertWay = "AlertWay";
        /// <summary>
        /// Title
        /// </summary>
        public const string Title = "Title";
    }
	/// <summary>
	/// ��Ϣ����
	/// �ڵ�������ڵ������������.	 
	/// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
	/// Ҳ��¼�˵�����ڵ�������Ľڵ�.
	/// </summary>
    public class Listen : EntityOID
    {
        #region ��������
        //public BP.WF.Port.AlertWay HisAlertWay
        //{
        //    get
        //    {
        //        return (BP.WF.Port.AlertWay)this.GetValIntByKey(ListenAttr.AlertWay);
        //    }
        //    set
        //    {
        //        this.SetValByKey(ListenAttr.AlertWay, (int)value);
        //    }
        //}
      
        /// <summary>
        ///�ڵ�
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(ListenAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(ListenAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public string Nodes
        {
            get
            {
                return this.GetValStringByKey(ListenAttr.Nodes);
            }
            set
            {
                this.SetValByKey(ListenAttr.Nodes, value);
            }
        }
        public string NodesDesc
        {
            get
            {
                return this.GetValStringByKey(ListenAttr.NodesDesc);
            }
            set
            {
                this.SetValByKey(ListenAttr.NodesDesc, value);
            }
        }
        public string Doc
        {
            get
            {
                string s= this.GetValStringByKey(ListenAttr.Doc);
                if (string.IsNullOrEmpty(s) == true)
                    s = "";
                return s;
            }
            set
            {
                this.SetValByKey(ListenAttr.Doc, value);
            }
        }
        public string Title
        {
            get
            {
                return this.GetValStringByKey(ListenAttr.Title);
            }
            set
            {
                this.SetValByKey(ListenAttr.Title, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public Listen()
        {
        }
        public Listen(int oid)
        {
            this.OID = oid;
            this.Retrieve();
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

                Map map = new Map("WF_Listen");
                map.EnDesc = "��Ϣ����";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();
                map.AddTBInt(ListenAttr.FK_Node, 0, "�ڵ�", true, false);
                map.AddTBString(ListenAttr.Nodes, null, "Nodes", true, false, 0, 400, 10);
                map.AddTBString(ListenAttr.NodesDesc, null, "����", true, false, 0, 400, 10);
                map.AddTBString(ListenAttr.Title, null, "Title", true, false, 0, 400, 10);
                map.AddTBStringDoc();
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ��Ϣ����
	/// </summary>
    public class Listens : EntitiesOID
    {
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public Listens() { }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="fk_flow"></param>
        public Listens(string fk_flow)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(ListenAttr.FK_Node, "SELECT NodeID FROM WF_Node WHERE FK_Flow='" + fk_flow + "'");
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Listen();
            }
        }
    }
}
