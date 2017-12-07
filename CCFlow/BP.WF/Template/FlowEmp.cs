using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// ���̸�λ��������	  
    /// </summary>
    public class FlowEmpAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ��Ա
        /// </summary>
        public const string FK_Emp = "FK_Emp";
    }
    /// <summary>
    /// ���̸�λ����
    /// ���̵���Ա�����������.	 
    /// ��¼�˴�һ�����̵������Ķ������.
    /// Ҳ��¼�˵�������̵�����������.
    /// </summary>
    public class FlowEmp : EntityMM
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
        ///����
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(FlowEmpAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(FlowEmpAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// ��Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(FlowEmpAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(FlowEmpAttr.FK_Emp, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        public FlowEmp() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_FlowEmp");
                map.EnDesc = "���̸�λ������Ϣ";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddDDLEntitiesPK(FlowEmpAttr.FK_Flow, null, "FK_Flow", new Flows(), true);
                map.AddDDLEntitiesPK(FlowEmpAttr.FK_Emp, null, "��Ա", new Emps(), true);
                this._enMap = map;

                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ���̸�λ����
    /// </summary>
    public class FlowEmps : EntitiesMM
    {
        /// <summary>
        /// ������Ա
        /// </summary>
        public Stations HisStations
        {
            get
            {
                Stations ens = new Stations();
                foreach (FlowEmp ns in this)
                {
                    ens.AddEntity(new Station(ns.FK_Emp));
                }
                return ens;
            }
        }
        /// <summary>
        /// ���Ĺ�������
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (FlowEmp ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Flow));
                }
                return ens;

            }
        }
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        public FlowEmps() { }
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        /// <param name="NodeID">����ID</param>
        public FlowEmps(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowEmpAttr.FK_Flow, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        /// <param name="StationNo">StationNo </param>
        public FlowEmps(string StationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowEmpAttr.FK_Emp, StationNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FlowEmp();
            }
        }
        /// <summary>
        /// ȡ��һ����Ա�����ܹ����ʵ�������s
        /// </summary>
        /// <param name="sts">��Ա����</param>
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
        /// ȡ��һ��������Ա�ܹ����ʵ������̡�
        /// </summary>
        /// <param name="empId">������ԱID</param>
        /// <returns></returns>
        public Nodes GetHisNodes_del(string empId)
        {
            Emp em = new Emp(empId);
            return this.GetHisNodes(em.HisStations);
        }
        /// <summary>
        /// ��Ա��Ӧ������
        /// </summary>
        /// <param name="stationNo">��Ա���</param>
        /// <returns>����s</returns>
        public Nodes GetHisNodes(string stationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowEmpAttr.FK_Emp, stationNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (FlowEmp en in this)
            {
                ens.AddEntity(new Node(en.FK_Flow));
            }
            return ens;
        }
        /// <summary>
        /// ת������̵ļ��ϵ�Nodes
        /// </summary>
        /// <param name="nodeID">�����̵�ID</param>
        /// <returns>ת������̵ļ��ϵ�Nodes (FromNodes)</returns> 
        public Stations GetHisStations(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowEmpAttr.FK_Flow, nodeID);
            qo.DoQuery();

            Stations ens = new Stations();
            foreach (FlowEmp en in this)
            {
                ens.AddEntity(new Station(en.FK_Emp));
            }
            return ens;
        }
    }
}
