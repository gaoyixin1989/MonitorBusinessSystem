using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// ������Ա����
    /// </summary>
    public class GenerWorkerListAttr
    {
        #region ��������
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// �������ݱ��
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ��������ǲ��Ƿ���
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// ʹ�õĸ�λ
        /// </summary>
        public const string UseStation_del = "UseStation";
        /// <summary>
        /// ʹ�õĲ���
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// Ӧ�����ʱ��
        /// </summary>
        public const string SDT = "SDT";
        /// <summary>
        /// ��������
        /// </summary>
        public const string DTOfWarning = "DTOfWarning";
        /// <summary>
        /// ��¼ʱ��
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// WarningHour
        /// </summary>
        public const string WarningHour = "WarningHour";
        /// <summary>
        /// �Ƿ��Զ�����
        /// </summary>
        //public const  string IsAutoGener="IsAutoGener";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        //public const  string GenerDateTime="GenerDateTime";
        /// <summary>
        /// IsPass
        /// </summary>
        public const string IsPass = "IsPass";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// ��Ա����
        /// </summary>
        public const string FK_EmpText = "FK_EmpText";
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public const string FK_NodeText = "FK_NodeText";
        /// <summary>
        /// ������
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// ˭ִ����?
        /// </summary>
        public const string WhoExeIt = "WhoExeIt";
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public const string PRI = "PRI";
        /// <summary>
        /// �Ƿ��ȡ��
        /// </summary>
        public const string IsRead = "IsRead";
        /// <summary>
        /// �߰����
        /// </summary>
        public const string PressTimes = "PressTimes";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string Tag = "Tag";
        /// <summary>
        /// ����
        /// </summary>
        public const string Paras = "Paras";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string DTOfHungUp = "DTOfHungUp";
        /// <summary>
        /// �������ʱ��
        /// </summary>
        public const string DTOfUnHungUp = "DTOfUnHungUp";
        /// <summary>
        /// �������
        /// </summary>
        public const string HungUpTimes = "HungUpTimes";
        /// <summary>
        /// �ⲿ�û����
        /// </summary>
        public const string GuestNo = "GuestNo";
        /// <summary>
        /// �ⲿ�û�����
        /// </summary>
        public const string GuestName = "GuestName";
        #endregion

        /// <summary>
        /// ������
        /// </summary>
        public const string GroupMark = "GroupMark";
        /// <summary>
        /// ��IDs
        /// </summary>
        public const string FrmIDs = "FrmIDs";
    }
    /// <summary>
    /// �������б�
    /// </summary>
    public class GenerWorkerList : Entity
    {
        #region ��������.
        /// <summary>
        /// ������
        /// </summary>
        public string GroupMark
        {
            get
            {
                return this.GetParaString(GenerWorkerListAttr.GroupMark);
            }
            set
            {
                this.SetPara(GenerWorkerListAttr.GroupMark, value);
            }
        }
        /// <summary>
        /// ��ID(���ڶ�̬������Ч.)
        /// </summary>
        public string FrmIDs
        {
            get
            {
                return this.GetParaString(GenerWorkerListAttr.FrmIDs);
            }
            set
            {
                this.SetPara(GenerWorkerListAttr.FrmIDs, value);
            }
        }
        #endregion ��������.

        #region ��������
        /// <summary>
        /// ˭��ִ����
        /// </summary>
        public int WhoExeIt
        {
            get
            {
                return this.GetValIntByKey(GenerWorkerListAttr.WhoExeIt);
            }
            set
            {
                SetValByKey(GenerWorkerListAttr.WhoExeIt, value);
            }
        }
        public int PressTimes
        {
            get
            {
                return this.GetValIntByKey(GenerWorkerListAttr.PressTimes);
            }
            set
            {
                SetValByKey(GenerWorkerListAttr.PressTimes, value);
            }
        }
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int PRI
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.PRI);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.PRI, value);
            }
        }
        /// <summary>
        /// WorkID
        /// </summary>
        public override string PK
        {
            get
            {
                return "WorkID,FK_Emp,FK_Node";
            }
        }
        /// <summary>
        /// �Ƿ����(�ڷ��乤��ʱ��Ч)
        /// </summary>
        public bool IsEnable
        {
            get
            {
                return this.GetValBooleanByKey(GenerWorkerListAttr.IsEnable);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.IsEnable, value);
            }
        }
        /// <summary>
        /// �Ƿ�ͨ��(����˵Ļ�ǩ�ڵ���Ч)
        /// </summary>
        public bool IsPass
        {
            get
            {
                return this.GetValBooleanByKey(GenerWorkerListAttr.IsPass);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.IsPass, value);
            }
        }
        /// <summary>
        /// 0=δ����.
        /// 1=�Ѿ�ͨ��.
        /// -2=  ��־�ýڵ��Ǹ�������Ա����Ľڵ㣬Ŀ��Ϊ���÷����ڵ����Ա���Կ�������.
        /// </summary>
        public int IsPassInt
        {
            get
            {
                return this.GetValIntByKey(GenerWorkerListAttr.IsPass);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.IsPass, value);
            }
        }
        public bool IsRead
        {
            get
            {
                return this.GetValBooleanByKey(GenerWorkerListAttr.IsRead);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.IsRead, value);
            }
        }
        /// <summary>
        /// WorkID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkerListAttr.WorkID);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.WorkID, value);
            }
        }
        /// <summary>
        /// Node
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(GenerWorkerListAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_Node, value);
            }
           
        }
        public string FK_DeptT
        {
            get
            {
                try
                {
                    Dept d = new Dept(this.FK_Dept);
                    return d.Name;
                }
                catch
                {
                    return "";
                }
            }
        }
        public string FK_Dept
        {
            get
            {
                return this.GetValStrByKey(GenerWorkerListAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_Dept, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStrByKey(GenerWorkerListAttr.Sender);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.Sender, value);
            }
        }
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public string FK_NodeText
        {
            get
            {
                return this.GetValStrByKey(GenerWorkerListAttr.FK_NodeText);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_NodeText, value);
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkerListAttr.FID);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FID, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public float WarningHour
        {
            get
            {
                return this.GetValFloatByKey(GenerWorkerListAttr.WarningHour);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.WarningHour, value);
            }
        }
        /// <summary>
        /// ������Ա
        /// </summary>
        public Emp HisEmp
        {
            get
            {
                return new Emp(this.FK_Emp);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.RDT);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.RDT, value);
            }
        }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public string CDT
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.CDT);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.CDT, value);
            }
        }
        /// <summary>
        /// Ӧ���������
        /// </summary>
        public string SDT
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.SDT);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.SDT, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string DTOfWarning
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.DTOfWarning);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.DTOfWarning, value);
            }
        }
        /// <summary>
        /// ��Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// ��Ա����
        /// </summary>
        public string FK_EmpText
        {
            get
            {
                return this.GetValStrByKey(GenerWorkerListAttr.FK_EmpText);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_EmpText, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>		 
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_Flow, value);
            }
        }

        public string GuestNo
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.GuestNo);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.GuestNo, value);
            }
        }
        public string GuestName
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.GuestName);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.GuestName, value);
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string DTOfHungUp
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.DTOfHungUp);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.DTOfHungUp, value);
            }
        }
        /// <summary>
        /// �������ʱ��
        /// </summary>
        public string DTOfUnHungUp
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.DTOfUnHungUp);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.DTOfUnHungUp, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public int HungUpTimes
        {
            get
            {
                return this.GetValIntByKey(GenerWorkerListAttr.HungUpTimes);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.HungUpTimes, value);
            }
        }
        #endregion 

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public override string PKField
        {
            get
            {
                return "WorkID,FK_Emp,FK_Node";
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public GenerWorkerList()
        {
        }
        public GenerWorkerList(Int64 workid, int FK_Node, string FK_Emp)
        {
            if (this.WorkID == 0)
                return;

            this.WorkID = workid;
            this.FK_Node = FK_Node;
            this.FK_Emp = FK_Emp;
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

                Map map = new Map("WF_GenerWorkerlist");
                map.EnDesc = "������";
                map.AddTBIntPK(GenerWorkerListAttr.WorkID, 0, "����ID", true, true);
                map.AddTBStringPK(GenerWorkerListAttr.FK_Emp, null, "��Ա", true, false, 0, 20, 100);
                map.AddTBIntPK(GenerWorkerListAttr.FK_Node, 0, "�ڵ�ID", true, false);
                map.AddTBInt(GenerWorkerListAttr.FID, 0, "����ID", true, false);

                map.AddTBString(GenerWorkerListAttr.FK_EmpText, null, "��Ա����", true, false, 0, 30, 100);
                map.AddTBString(GenerWorkerListAttr.FK_NodeText, null, "�ڵ�����", true, false, 0, 100, 100);

                map.AddTBString(GenerWorkerListAttr.FK_Flow, null, "����", true, false, 0, 3, 100);
                map.AddTBString(GenerWorkerListAttr.FK_Dept, null, "ʹ�ò���", true, false, 0, 100, 100);

                //������������������ƵľͰ��������������㡣
                map.AddTBDateTime(GenerWorkerListAttr.SDT, "Ӧ�������", false, false);
                map.AddTBDateTime(GenerWorkerListAttr.DTOfWarning, "��������", false, false);
                map.AddTBFloat(GenerWorkerListAttr.WarningHour, 0, "Ԥ����", true, false);
                map.AddTBDateTime(GenerWorkerListAttr.RDT, "��¼ʱ��", false, false);
                map.AddTBDateTime(GenerWorkerListAttr.CDT, "���ʱ��", false, false);
                map.AddBoolean(GenerWorkerListAttr.IsEnable, true, "�Ƿ����", true, true);

                //  add for �Ϻ� 2012-11-30
                map.AddTBInt(GenerWorkerListAttr.IsRead, 0, "�Ƿ��ȡ", true, true);

                //�Ի�ǩ�ڵ���Ч
                map.AddTBInt(GenerWorkerListAttr.IsPass, 0, "�Ƿ�ͨ��(�Ժ����ڵ���Ч)", false, false);

                // ˭ִ������
                map.AddTBInt(GenerWorkerListAttr.WhoExeIt, 0, "˭ִ����", false, false);

                //������. 2011-11-12 Ϊ����û����ӡ�
                map.AddTBString(GenerWorkerListAttr.Sender, null, "������", true, false, 0, 200, 100);

                //���ȼ���2012-06-15 Ϊ�ൺ�û����ӡ�
                map.AddTBInt(GenerWorkFlowAttr.PRI, 1, "���ȼ�", true, true);

                //�߰����. Ϊ������ͨ��.
                map.AddTBInt(GenerWorkerListAttr.PressTimes, 0, "�߰����", true, false);

                // ����
                map.AddTBDateTime(GenerWorkerListAttr.DTOfHungUp,null, "����ʱ��", false, false);
                map.AddTBDateTime(GenerWorkerListAttr.DTOfUnHungUp,null, "Ԥ�ƽ������ʱ��", false, false);
                map.AddTBInt(GenerWorkerListAttr.HungUpTimes, 0, "�������", true, false);

                //�ⲿ�û�. 203-08-30
                map.AddTBString(GenerWorkerListAttr.GuestNo, null, "�ⲿ�û����", true, false, 0, 30, 100);
                map.AddTBString(GenerWorkerListAttr.GuestName, null, "�ⲿ�û�����", true, false, 0, 100, 100);

                //������� 2014-04-05.
                map.AddTBAtParas(4000); 

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeInsert()
        {
            if (this.FID != 0)
            {
                if (this.FID == this.WorkID)
                    this.FID = 0;
            }
            //this.Sender = BP.Web.WebUser.No;
            return base.beforeInsert();
        }
    }
    /// <summary>
    /// ������Ա����
    /// </summary>
    public class GenerWorkerLists : Entities
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GenerWorkerList();
            }
        }
        /// <summary>
        /// GenerWorkerList
        /// </summary>
        public GenerWorkerLists() { }
        public GenerWorkerLists(Int64 workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkerListAttr.WorkID, workId);
            qo.addOrderBy(GenerWorkerListAttr.RDT);
            qo.DoQuery();
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workId"></param>
        /// <param name="nodeId"></param>
        public GenerWorkerLists(Int64 workId, int nodeId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkerListAttr.WorkID, workId);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Node, nodeId);
            qo.DoQuery();
            return;
        }
        public GenerWorkerLists(Int64 workId, int nodeId,string FK_Emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkerListAttr.WorkID, workId);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Node, nodeId);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Emp, FK_Emp);
            qo.DoQuery();
            return;
        }
        /// <summary>
        /// ���칤����Ա����
        /// </summary>
        /// <param name="workId">����ID</param>
        /// <param name="nodeId">�ڵ�ID</param>
        /// <param name="isWithEmpExts">�Ƿ����Ϊ�������Ա</param>
        public GenerWorkerLists(Int64 workId, int nodeId, bool isWithEmpExts)
        {
            QueryObject qo = new QueryObject(this);
            qo.addLeftBracket();
            qo.AddWhere(GenerWorkerListAttr.WorkID, workId);
            qo.addOr();
            qo.AddWhere(GenerWorkerListAttr.FID, workId);
            qo.addRightBracket();
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Node, nodeId);
            int i = qo.DoQuery();

            if (isWithEmpExts == false)
                return;

            if (i == 0)
                throw new Exception("@ϵͳ���󣬹�����Ա��ʧ�������Ա��ϵ��NodeID=" + nodeId + " WorkID=" + workId);

            RememberMe rm = new RememberMe();
            rm.FK_Emp = BP.Web.WebUser.No;
            rm.FK_Node = nodeId;
            if (rm.RetrieveFromDBSources() == 0)
                return;

            GenerWorkerList wl = (GenerWorkerList)this[0];
            string[] emps = rm.Emps.Split('@');
            foreach (string emp in emps)
            {
                if (emp==null || emp=="")
                    continue;

                if (this.GetCountByKey(GenerWorkerListAttr.FK_Emp, emp) >= 1)
                    continue;

                GenerWorkerList mywl = new GenerWorkerList();
                mywl.Copy(wl);
                mywl.IsEnable = false;
                mywl.FK_Emp = emp;
                WF.Port.WFEmp myEmp = new Port.WFEmp(emp);
                mywl.FK_EmpText = myEmp.Name;
                try
                {
                    mywl.Insert();
                }
                catch
                {
                    mywl.Update();
                    continue;
                }
                this.AddEntity(mywl);
            }
            return;
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="workId">������ID</param>
        /// <param name="flowNo">���̱��</param>
        public GenerWorkerLists(Int64 workId, string flowNo)
        {
            if (workId == 0)
                return;

            Flow fl = new Flow(flowNo);
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkerListAttr.WorkID, workId);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Flow, flowNo);
            qo.DoQuery();
        }
        #endregion
    }
}
