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
    /// ���͵���λ����	  
    /// </summary>
    public class CCStationAttr
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
    /// ���͵���λ
    /// �ڵ�Ĺ�����λ�����������.	 
    /// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
    /// Ҳ��¼�˵�����ڵ�������Ľڵ�.
    /// </summary>
    public class CCStation : EntityMM
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
                return this.GetValIntByKey(CCStationAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(CCStationAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// ��λ����
        /// </summary>
        public string FK_StationT
        {
            get
            {
                return this.GetValRefTextByKey(CCStationAttr.FK_Station);
            }
        }
        /// <summary>
        /// ������λ
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(CCStationAttr.FK_Station);
            }
            set
            {
                this.SetValByKey(CCStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ���͵���λ
        /// </summary>
        public CCStation() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_CCStation");
                map.EnDesc = "���͸�λ";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddDDLEntitiesPK(CCStationAttr.FK_Node, 0, DataType.AppInt, "�ڵ�", new Nodes(), NodeAttr.NodeID, NodeAttr.Name, true);
                map.AddDDLEntitiesPK(CCStationAttr.FK_Station, null, "������λ", new Stations(), true);
              
                this._enMap = map;

                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ���͵���λ
    /// </summary>
    public class CCStations : EntitiesMM
    {
        /// <summary>
        /// ���Ĺ�����λ
        /// </summary>
        public Stations HisStations
        {
            get
            {
                Stations ens = new Stations();
                foreach (CCStation ns in this)
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
                foreach (CCStation ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;

            }
        }
        /// <summary>
        /// ���͵���λ
        /// </summary>
        public CCStations() { }
        /// <summary>
        /// ���͵���λ
        /// </summary>
        /// <param name="nodeID">�ڵ�ID</param>
        public CCStations(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CCStationAttr.FK_Node, nodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// ���͵���λ
        /// </summary>
        /// <param name="StationNo">StationNo </param>
        public CCStations(string StationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CCStationAttr.FK_Station, StationNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new CCStation();
            }
        }
        /// <summary>
        /// ������λ��Ӧ�Ľڵ�
        /// </summary>
        /// <param name="stationNo">������λ���</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string stationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CCStationAttr.FK_Station, stationNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (CCStation en in this)
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
            qo.AddWhere(CCStationAttr.FK_Node, nodeID);
            qo.DoQuery();

            Stations ens = new Stations();
            foreach (CCStation en in this)
            {
                ens.AddEntity(new Station(en.FK_Station));
            }
            return ens;
        }
    }
}
