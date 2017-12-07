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
    /// �ڵ㹤����λ����	  
    /// </summary>
    public class NodeStationAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ������λ
        /// </summary>
        public const string FK_Station = "FK_Station";
    }
    /// <summary>
    /// �ڵ㹤����λ
    /// �ڵ�Ĺ�����λ�����������.	 
    /// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
    /// Ҳ��¼�˵�����ڵ�������Ľڵ�.
    /// </summary>
    public class NodeStation : EntityMM
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
                uac.OpenAll();
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
                return this.GetValIntByKey(NodeStationAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(NodeStationAttr.FK_Node, value);
            }
        }
        public string FK_StationT
        {
            get
            {
                return this.GetValRefTextByKey(NodeStationAttr.FK_Station);
            }
        }
        /// <summary>
        /// ������λ
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(NodeStationAttr.FK_Station);
            }
            set
            {
                this.SetValByKey(NodeStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ڵ㹤����λ
        /// </summary>
        public NodeStation() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NodeStation");
                map.EnDesc = "�ڵ��λ";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.AddTBIntPK(NodeStationAttr.FK_Node, 0,"�ڵ�", false,false);

                if (Glo.OSModel == OSModel.WorkFlow)
                {
                    map.AddDDLEntitiesPK(NodeStationAttr.FK_Station, null, "������λ",
                        new BP.Port.Stations(), true);
                }
                else
                {
 // #warning ,����Ϊ�˷����û�ѡ���÷��鶼ͳһ������ö������. edit zhoupeng. 2015.04.28. ע��jflowҲҪ�޸�.
                    map.AddDDLEntitiesPK(NodeStationAttr.FK_Station, null, "������λ",
                       new BP.GPM.Stations(), true);
                }
                this._enMap = map;
                return this._enMap;
            }
        }

        /// <summary>
        /// �ڵ��λ�����仯��ɾ���ýڵ����Ľ�����Ա��
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            RememberMe remeberMe = new RememberMe();
            remeberMe.Delete(RememberMeAttr.FK_Node, this.FK_Node);
            return base.beforeInsert();
        }
        #endregion

    }
    /// <summary>
    /// �ڵ㹤����λ
    /// </summary>
    public class NodeStations : EntitiesMM
    {
        /// <summary>
        /// ���Ĺ�����λ
        /// </summary>
        public Stations HisStations
        {
            get
            {
                Stations ens = new Stations();
                foreach (NodeStation ns in this)
                {
                    ens.AddEntity(new Station(ns.FK_Station));
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
                foreach (NodeStation ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;

            }
        }
        /// <summary>
        /// �ڵ㹤����λ
        /// </summary>
        public NodeStations() { }
        /// <summary>
        /// �ڵ㹤����λ
        /// </summary>
        /// <param name="nodeID">�ڵ�ID</param>
        public NodeStations(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeStationAttr.FK_Node, nodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// �ڵ㹤����λ
        /// </summary>
        /// <param name="StationNo">StationNo </param>
        public NodeStations(string StationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeStationAttr.FK_Station, StationNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeStation();
            }
        }
        /// <summary>
        /// ȡ��һ��������λ�����ܹ����ʵ��Ľڵ�s
        /// </summary>
        /// <param name="sts">������λ����</param>
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
        /// ������λ��Ӧ�Ľڵ�
        /// </summary>
        /// <param name="stationNo">������λ���</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string stationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeStationAttr.FK_Station, stationNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeStation en in this)
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
            qo.AddWhere(NodeStationAttr.FK_Node, nodeID);
            qo.DoQuery();

            Stations ens = new Stations();
            foreach (NodeStation en in this)
            {
                ens.AddEntity(new Station(en.FK_Station));
            }
            return ens;
        }
    }
}
