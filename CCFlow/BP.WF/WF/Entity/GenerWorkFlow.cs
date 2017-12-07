using System;
using System.Data;
using BP.DA;
using BP.WF;
using BP.Port ;
using BP.Sys;
using BP.En;
using BP.WF.Template;

namespace BP.WF
{
    /// <summary>
    /// ʱ���
    /// </summary>
    public enum TSpan
    {
        /// <summary>
        /// ����
        /// </summary>
        ThisWeek,
        /// <summary>
        /// ����
        /// </summary>
        NextWeek,
        /// <summary>
        /// ������ǰ
        /// </summary>
        TowWeekAgo,
        /// <summary>
        /// ����
        /// </summary>
        More
    }
    /// <summary>
    /// ����״̬(��)
    /// </summary>
    public enum WFSta
    {
        /// <summary>
        /// ������
        /// </summary>
        Runing=0,
        /// <summary>
        /// �����
        /// </summary>
        Complete,
        /// <summary>
        /// ����
        /// </summary>
        Delete
    }
    /// <summary>
    /// ����״̬
    /// </summary>
    public enum TaskSta
    {
        /// <summary>
        /// ��
        /// </summary>
        None,
        /// <summary>
        /// ����
        /// </summary>
        Sharing,
        /// <summary>
        /// �Ѿ�ȡ��
        /// </summary>
        Takeback
    }
	/// <summary>
    /// ����ʵ��
	/// </summary>
    public class GenerWorkFlowAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ������
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ����״̬
        /// </summary>
        public const string WFState = "WFState";
        /// <summary>
        /// ����״̬
        /// </summary>
        public const string WFSta = "WFSta";
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ������
        /// </summary>
        public const string Starter = "Starter";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// �÷�
        /// </summary>
        public const string Cent = "Cent";
        /// <summary>
        /// ��ǰ�������Ľڵ�.
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ��ǰ������λ
        /// </summary>
        public const string FK_Station = "FK_Station";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FlowName = "FlowName";
        /// <summary>
        /// ����������
        /// </summary>
        public const string StarterName = "StarterName";
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public const string NodeName = "NodeName";
        /// <summary>
        /// ��������
        /// </summary>
        public const string DeptName = "DeptName";
        /// <summary>
        /// �������
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public const string PRI = "PRI";
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public const string SDTOfFlow = "SDTOfFlow";
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public const string SDTOfNode = "SDTOfNode";
        /// <summary>
        /// ������ID
        /// </summary>
        public const string PWorkID = "PWorkID";
        /// <summary>
        /// �������̵�FID
        /// </summary>
        public const string PFID = "PFID";
        /// <summary>
        /// �����̱��
        /// </summary>
        public const string PFlowNo = "PFlowNo";
        /// <summary>
        /// �����̽ڵ�
        /// </summary>
        public const string PNodeID = "PNodeID";
        /// <summary>
        /// �����̵ĵ�����.
        /// </summary>
        public const string PEmp = "PEmp";
        /// <summary>
        /// �ͻ����(���ڿͻ������������Ч)
        /// </summary>
        public const string GuestNo = "GuestNo";
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public const string GuestName = "GuestName";
        /// <summary>
        /// ���ݱ��
        /// </summary>
        public const string BillNo = "BillNo";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string FlowNote = "FlowNote";
        /// <summary>
        /// ������Ա
        /// </summary>
        public const string TodoEmps = "TodoEmps";
        /// <summary>
        /// ������Ա����
        /// </summary>
        public const string TodoEmpsNum = "TodoEmpsNum";
        /// <summary>
        /// ����״̬
        /// </summary>
        public const string TaskSta = "TaskSta";
        /// <summary>
        /// �������̱��
        /// </summary>
        public const string CFlowNo = "CFlowNo";
        /// <summary>
        /// ��������ID
        /// </summary>
        public const string CWorkID = "CWorkID";
        /// <summary>
        /// ��ʱ��ŵĲ���
        /// </summary>
        public const string AtPara = "AtPara";
        /// <summary>
        /// ������
        /// </summary>
        public const string Emps = "Emps";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
        /// <summary>
        /// FK_NY
        /// </summary>
        public const string FK_NY = "FK_NY";
        /// <summary>
        /// ������
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// ��������
        /// </summary>
        public const string SendDT = "SendDT";
        /// <summary>
        /// ʱ�䷶Χ
        /// </summary>
        public const string TSpan = "TSpan";
        /// <summary>
        /// ����
        /// </summary>
        public const string MyNum = "MyNum";
        #endregion
    }
	/// <summary>
    /// ����ʵ��
	/// </summary>
	public class GenerWorkFlow : Entity
	{	
		#region ��������
        /// <summary>
        /// ����
        /// </summary>
        public override string PK
        {
            get
            {
                return GenerWorkFlowAttr.WorkID;
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string FlowNote
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.FlowNote);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FlowNote, value);
            }
        }
		/// <summary>
		/// �������̱��
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.FK_Flow);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.FK_Flow,value);
			}
		}
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.BillNo);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.BillNo, value);
            }
        }
        /// <summary>
        /// ���ķ�����
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.Sender);
            }
            set
            {
                //������.
                this.SetValByKey(GenerWorkFlowAttr.Sender, value);

                //��ǰ����.
                this.SetValByKey(GenerWorkFlowAttr.SendDT, DataType.CurrentDataTime);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string SendDT
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.SendDT);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.SendDT, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.FlowName);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FlowName, value);
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
        /// ������Ա����
        /// </summary>
        public int TodoEmpsNum
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.TodoEmpsNum);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.TodoEmpsNum, value);
            }
        }
        /// <summary>
        /// ������Ա�б�
        /// </summary>
        public string TodoEmps
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.TodoEmps);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.TodoEmps, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.Emps);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.Emps, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public TaskSta TaskSta
        {
            get
            {
                return (TaskSta)this.GetValIntByKey(GenerWorkFlowAttr.TaskSta);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.TaskSta, (int)value);
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FK_FlowSort, value);
            }
        }
		public string  FK_Dept
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.FK_Dept);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.Title);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.Title,value);
			}
		}
        /// <summary>
        /// �ͻ����
        /// </summary>
        public string GuestNo
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.GuestNo);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.GuestNo, value);
            }
        }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public string GuestName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.GuestName);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.GuestName, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_NY
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.FK_NY);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FK_NY, value);
            }
        }
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.RDT);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.RDT,value);
                this.FK_NY = value.Substring(0,7);
			}
		}
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public string SDTOfNode
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.SDTOfNode);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.SDTOfNode, value);
            }
        }
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public string SDTOfFlow
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.SDTOfFlow);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.SDTOfFlow, value);
            }
        }
		/// <summary>
		/// ����ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
                return this.GetValInt64ByKey(GenerWorkFlowAttr.WorkID);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.WorkID,value);
			}
		}
        /// <summary>
        /// ���߳�ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowAttr.FID);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FID, value);
            }
        }
        /// <summary>
        /// ���ڵ�ID Ϊ����-1.
        /// </summary>
        public Int64 CWorkID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowAttr.CWorkID);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.CWorkID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string CFlowNo
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.CFlowNo);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.CFlowNo, value);
            }
        }
        /// <summary>
        /// ���ڵ����̱��.
        /// </summary>
        public Int64 PWorkID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowAttr.PWorkID);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.PWorkID, value);
            }
        }
        public Int64 PFID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowAttr.PFID);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.PFID, value);
            }
        }
        /// <summary>
        /// �����̵��õĽڵ�
        /// </summary>
        public int PNodeID
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.PNodeID);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.PNodeID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string PFlowNo
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.PFlowNo);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.PFlowNo, value);
            }
        }
        /// <summary>
        /// ���������̵���Ա
        /// </summary>
        public string PEmp
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.PEmp);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.PEmp, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Starter
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.Starter);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.Starter, value);
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string StarterName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.StarterName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.StarterName, value);
            }
        }
        /// <summary>
        /// �����˲�������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.DeptName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.DeptName, value);
            }
        }
        /// <summary>
        /// ��ǰ�ڵ�����
        /// </summary>
        public string NodeName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.NodeName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.NodeName, value);
            }
        }
		/// <summary>
		/// ��ǰ�������Ľڵ�
		/// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.FK_Node);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FK_Node, value);
            }
        }
        /// <summary>
		/// ��������״̬
		/// </summary>
        public WFState WFState
        {
            get
            {
                return (WFState)this.GetValIntByKey(GenerWorkFlowAttr.WFState);
            }
            set
            {
                if (value == WF.WFState.Complete)
                    SetValByKey(GenerWorkFlowAttr.WFSta, (int)WFSta.Complete);
                else if (value == WF.WFState.Delete)
                    SetValByKey(GenerWorkFlowAttr.WFSta, (int)WFSta.Delete);
                else
                    SetValByKey(GenerWorkFlowAttr.WFSta, (int)WFSta.Runing);

                SetValByKey(GenerWorkFlowAttr.WFState, (int)value);
            }
        }
        /// <summary>
        /// ״̬(��)
        /// </summary>
        public WFSta WFSta
        {
            get
            {
                return (WFSta)this.GetValIntByKey(GenerWorkFlowAttr.WFSta);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.WFSta, (int)value);
            }
        }
        public string WFStateText
        {
            get
            {
                BP.WF.WFState ws = (WFState)this.WFState;
                switch(ws)
                {
                    case WF.WFState.Complete:
                        return "�����";
                    case WF.WFState.Runing:
                        return "������";
                    case WF.WFState.HungUp:
                        return "����";
                    case WF.WFState.Askfor:
                        return "��ǩ";
                    default:
                        return "δ�ж�";
                }
            }
        }
        /// <summary>
        /// GUID
        /// </summary>
        public string GUID
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.GUID);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.GUID, value);
            }
        }
		#endregion

        #region ��չ����
        /// <summary>
        /// ����������
        /// </summary>
        public GenerWorkFlows HisSubFlowGenerWorkFlows
        {
            get
            {
                GenerWorkFlows ens = new GenerWorkFlows();
                ens.Retrieve(GenerWorkFlowAttr.PWorkID, this.WorkID);
                return ens;
            }
        }
        #endregion ��չ����

        #region ��������.
        public string Paras_ToNodes
        {

            get
            {
                return this.GetParaString("ToNodes");
            }

            set
            {
                this.SetPara("ToNodes", value);
            }
        }
        /// <summary>
        /// ��ǩ��Ϣ
        /// </summary>
        public string Paras_AskForReply
        {

            get
            {
                return this.GetParaString("AskForReply");
            }
            set
            {
                this.SetPara("AskForReply", value);
            }
        }
        /// <summary>
        /// �Ƿ����˻ز�ԭ·����.
        /// </summary>
        public bool Paras_IsTrackBack
        {

            get
            {
                return this.GetParaBoolen("IsTrackBack");
            }
            set
            {
                this.SetPara("IsTrackBack", value);
            }
        }
        /// <summary>
        /// ����Mark
        /// </summary>
        public string Paras_GroupMark
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
        /// �Ƿ����Զ�����
        /// 0=�Զ�����(Ĭ��).
        /// 1=�ֹ�����.
        /// ��������������.
        /// </summary>
        public bool IsAutoRun
        {
            get
            {
                return this.GetParaBoolen("IsAutoRun",true);
            }

            set
            {
                this.SetPara("IsAutoRun", value);
            }
        }
        #endregion ��������.

        #region ���캯��
        /// <summary>
		/// �����Ĺ�������
		/// </summary>
		public GenerWorkFlow()
		{
		}
        public GenerWorkFlow(Int64 workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkFlowAttr.WorkID, workId);
            if (qo.DoQuery() == 0)
                throw new Exception("���� GenerWorkFlow [" + workId + "]�����ڡ�");
        }
        /// <summary>
        /// ִ���޸�
        /// </summary>
        public void DoRepair()
        { 
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

                Map map = new Map("WF_GenerWorkFlow");
                map.EnDesc = "����ʵ��";

                map.AddTBIntPK(GenerWorkFlowAttr.WorkID, 0, "WorkID", true, true);
                map.AddTBInt(GenerWorkFlowAttr.FID, 0, "����ID", true, true);

                map.AddTBString(GenerWorkFlowAttr.FK_FlowSort, null, "�������", true, false, 0, 10, 10);
                map.AddTBString(GenerWorkFlowAttr.FK_Flow, null, "����", true, false, 0, 3, 10);
                map.AddTBString(GenerWorkFlowAttr.FlowName, null, "��������", true, false, 0, 100, 10);

                map.AddTBString(GenerWorkFlowAttr.Title, null, "����", true, false, 0, 100, 10);
                map.AddTBInt(GenerWorkFlowAttr.WFState, 0, "����״̬", true, false);
                map.AddTBInt(GenerWorkFlowAttr.WFSta, 0, "����״̬Ext", true, false);

                map.AddTBString(GenerWorkFlowAttr.Starter, null, "������", true, false, 0, 200, 10);
                map.AddTBString(GenerWorkFlowAttr.StarterName, null, "����������", true, false, 0, 200, 10);
                map.AddTBString(GenerWorkFlowAttr.Sender, null, "������", true, false, 0, 200, 10);


                map.AddTBDateTime(GenerWorkFlowAttr.RDT, "��¼����", true, true);
                map.AddTBInt(GenerWorkFlowAttr.FK_Node, 0, "�ڵ�", true, false);
                map.AddTBString(GenerWorkFlowAttr.NodeName, null, "�ڵ�����", true, false, 0, 100, 10);

                map.AddTBString(GenerWorkFlowAttr.FK_Dept, null, "����", true, false, 0, 100, 10);
                map.AddTBString(GenerWorkFlowAttr.DeptName, null, "��������", true, false, 0, 100, 10);
                map.AddTBInt(GenerWorkFlowAttr.PRI, 1, "���ȼ�", true, true);

                map.AddTBDateTime(GenerWorkFlowAttr.SDTOfNode, "�ڵ�Ӧ���ʱ��", true, true);
                map.AddTBDateTime(GenerWorkFlowAttr.SDTOfFlow, "����Ӧ���ʱ��", true, true);

                //����������Ϣ.
                map.AddTBString(GenerWorkFlowAttr.PFlowNo, null, "�����̱��", true, false, 0, 3, 10);
                map.AddTBInt(GenerWorkFlowAttr.PWorkID, 0, "������ID", true, true);
                map.AddTBInt(GenerWorkFlowAttr.PNodeID, 0, "�����̵��ýڵ�", true, true);
                map.AddTBInt(GenerWorkFlowAttr.PFID, 0, "�����̵��õ�PFID", true, true);
                map.AddTBString(GenerWorkFlowAttr.PEmp, null, "�����̵ĵ�����", true, false, 0,32, 10);


                map.AddTBString(GenerWorkFlowAttr.CFlowNo, null, "�������̱��", true, false, 0, 3, 10);
                map.AddTBInt(GenerWorkFlowAttr.CWorkID, 0, "��������ID", true, true);

                //�ͻ�������Ϣ.
                map.AddTBString(GenerWorkFlowAttr.GuestNo, null, "�ͻ����", true, false, 0, 100, 10);
                map.AddTBString(GenerWorkFlowAttr.GuestName, null, "�ͻ�����", true, false, 0, 100, 10);

                map.AddTBString(GenerWorkFlowAttr.BillNo, null, "���ݱ��", true, false, 0, 100, 10);
                map.AddTBString(GenerWorkFlowAttr.FlowNote, null, "��ע", true, false, 0, 4000, 10);

                //�������ء�
                map.AddTBString(GenerWorkFlowAttr.TodoEmps, null, "������Ա", true, false, 0, 4000, 10);
                map.AddTBInt(GenerWorkFlowAttr.TodoEmpsNum, 0, "������Ա����", true, true);
                map.AddTBInt(GenerWorkFlowAttr.TaskSta, 0, "����״̬", true, true);

                //����.
                map.AddTBString(GenerWorkFlowAttr.AtPara, null, "����(��������������ʱ�洢�Ĳ���)", true, false, 0, 2000, 10);
                map.AddTBString(GenerWorkFlowAttr.Emps, null, "������", true, false, 0, 4000, 10);
                map.AddTBString(GenerWorkFlowAttr.GUID, null, "GUID", false, false, 0, 36, 10);
                map.AddTBString(GenerWorkFlowAttr.FK_NY, null, "����", false, false, 0, 7, 7);
                map.AddTBInt(GenerWorkFlowAttr.TSpan, 0, "ʱ���", true, true);

                map.AddTBMyNum();


                RefMethod rm = new RefMethod();
                rm.Title = "�����켣";  // "��������";
                rm.ClassMethodName = this.ToString() + ".DoRpt";
                rm.Icon = "/WF/Img/FileType/doc.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "�����Լ�"; // "�����Լ�";
                rm.ClassMethodName = this.ToString() + ".DoSelfTestInfo";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "�����Լ첢�޸�";
                rm.ClassMethodName = this.ToString() + ".DoRepare";
                rm.Warning = "��ȷ��Ҫִ�д˹����� \t\n 1)����Ƕ����̣�����ͣ���ڵ�һ���ڵ��ϣ�ϵͳΪִ��ɾ������\t\n 2)����Ƿǵص�һ���ڵ㣬ϵͳ�᷵�ص��ϴη����λ�á�";
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

		#region ���ػ��෽��
		/// <summary>
		/// ɾ����,��Ҫ�ѹ������б�ҲҪɾ��.
		/// </summary>
        protected override void afterDelete()
        {
            // . clear bad worker .  
            DBAccess.RunSQLReturnTable("DELETE FROM WF_GenerWorkerlist WHERE WorkID in  ( select WorkID from WF_GenerWorkerlist WHERE WorkID not in (select WorkID from WF_GenerWorkFlow) )");

            WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID,this.FID);
            wf.DoDeleteWorkFlowByReal(true); /* ɾ������Ĺ�����*/
            base.afterDelete();
        }
		#endregion 

		#region ִ�����
        public string DoRpt()
        {
            PubClass.WinOpen("WFRpt.aspx?WorkID=" + this.WorkID + "&FID="+this.FID+"&FK_Flow="+this.FK_Flow);
            return null;
        }
		/// <summary>
		/// ִ���޸�
		/// </summary>
		/// <returns></returns>
        public string DoRepare()
        {
            if (this.DoSelfTestInfo() == "û�з����쳣��")
                return "û�з����쳣��";

            string sql = "SELECT FK_Node FROM WF_GenerWorkerlist WHERE WORKID='" + this.WorkID + "' ORDER BY FK_Node desc";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                /*����ǿ�ʼ�����ڵ㣬��ɾ������*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID );
                wf.DoDeleteWorkFlowByReal(true);
                return "����������Ϊ������ʧ�ܱ�ϵͳɾ����";
            }

            int FK_Node = int.Parse(dt.Rows[0][0].ToString());

            Node nd = new Node(FK_Node);
            if (nd.IsStartNode)
            {
                /*����ǿ�ʼ�����ڵ㣬��ɾ������*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID);
                wf.DoDeleteWorkFlowByReal(true);
                return "����������Ϊ������ʧ�ܱ�ϵͳɾ����";
            }

            this.FK_Node = nd.NodeID;
            this.NodeName = nd.Name;
            this.Update();

            string str = "";
            GenerWorkerLists wls = new GenerWorkerLists();
            wls.Retrieve(GenerWorkerListAttr.FK_Node, FK_Node, GenerWorkerListAttr.WorkID, this.WorkID);
            foreach (GenerWorkerList wl in wls)
            {
                str += wl.FK_Emp + wl.FK_EmpText + ",";
            }

            return "����������Ϊ[" + nd.Name + "]��������ʧ�ܱ��ع�����ǰλ�ã���ת��[" + str + "]�����޸��ɹ���";
        }
		public string DoSelfTestInfo()
		{
            GenerWorkerLists wls = new GenerWorkerLists(this.WorkID, this.FK_Flow);

			#region  �鿴һ�µ�ǰ�Ľڵ��Ƿ�ʼ�����ڵ㡣
			Node nd = new Node(this.FK_Node);
			if (nd.IsStartNode)
			{
				/* �ж��Ƿ����˻صĽڵ� */
				Work wk = nd.HisWork;
				wk.OID = this.WorkID;
				wk.Retrieve();
			}
			#endregion


			#region  �鿴һ���Ƿ��е�ǰ�Ĺ����ڵ���Ϣ��
			bool isHave=false;
            foreach (GenerWorkerList wl in wls)
			{
				if (wl.FK_Node==this.FK_Node)
					isHave=true;
			}

			if (isHave==false)
			{
				/*  */
				return "�Ѿ������ڵ�ǰ�Ĺ����ڵ���Ϣ����ɴ����̵�ԭ�������û�в����ϵͳ�쳣������ɾ�������̻��߽���ϵͳ�Զ��޸�����";
			}
			#endregion

			return "û�з����쳣��";
		}
		#endregion
	}
	/// <summary>
    /// ����ʵ��s
	/// </summary>
	public class GenerWorkFlows : Entities
	{
		/// <summary>
		/// ���ݹ�������,������Ա ID ��ѯ��������ǰ�������Ĺ���.
		/// </summary>
		/// <param name="flowNo">���̱��</param>
		/// <param name="empId">������ԱID</param>
		/// <returns></returns>
		public static DataTable QuByFlowAndEmp(string flowNo, int empId)
		{
			string sql="SELECT a.WorkID FROM WF_GenerWorkFlow a, WF_GenerWorkerlist b WHERE a.WorkID=b.WorkID   AND b.FK_Node=a.FK_Node  AND b.FK_Emp='"+empId.ToString()+"' AND a.FK_Flow='"+flowNo+"'";
			return DBAccess.RunSQLReturnTable(sql);
		}

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new GenerWorkFlow();
			}
		}
		/// <summary>
		/// ����ʵ������
		/// </summary>
		public GenerWorkFlows(){}
		#endregion
	}
	
}
