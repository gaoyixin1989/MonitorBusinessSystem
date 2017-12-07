using System;
using System.Data;
using BP.DA;
using BP.WF;
using BP.Port ;
using BP.Sys;
using BP.En;

namespace BP.WF.Data
{
	/// <summary>
    /// �Ҳ��ŵ�����
	/// </summary>
    public class MyDeptFlowAttr
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
        /// ����״̬(��)
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
        #endregion
    }
	/// <summary>
    /// �Ҳ��ŵ�����
	/// </summary>
	public class MyDeptFlow : Entity
	{	
		#region ��������
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.Readonly();
                return uac;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public override string PK
        {
            get
            {
                return MyDeptFlowAttr.WorkID;
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string FlowNote
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.FlowNote);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.FlowNote, value);
            }
        }
		/// <summary>
		/// �������̱��
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStrByKey(MyDeptFlowAttr.FK_Flow);
			}
			set
			{
				SetValByKey(MyDeptFlowAttr.FK_Flow,value);
			}
		}
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.BillNo);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.BillNo, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.FlowName);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.FlowName, value);
            }
        }
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int PRI
        {
            get
            {
                return this.GetValIntByKey(MyDeptFlowAttr.PRI);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.PRI, value);
            }
        }
        /// <summary>
        /// ������Ա����
        /// </summary>
        public int TodoEmpsNum
        {
            get
            {
                return this.GetValIntByKey(MyDeptFlowAttr.TodoEmpsNum);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.TodoEmpsNum, value);
            }
        }
        /// <summary>
        /// ������Ա�б�
        /// </summary>
        public string TodoEmps
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.TodoEmps);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.TodoEmps, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.Emps);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.Emps, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public TaskSta TaskSta
        {
            get
            {
                return (TaskSta)this.GetValIntByKey(MyDeptFlowAttr.TaskSta);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.TaskSta, (int)value);
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.FK_FlowSort, value);
            }
        }
        /// <summary>
        /// ���ű��
        /// </summary>
		public string  FK_Dept
		{
			get
			{
				return this.GetValStrByKey(MyDeptFlowAttr.FK_Dept);
			}
			set
			{
				SetValByKey(MyDeptFlowAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStrByKey(MyDeptFlowAttr.Title);
			}
			set
			{
				SetValByKey(MyDeptFlowAttr.Title,value);
			}
		}
        /// <summary>
        /// �ͻ����
        /// </summary>
        public string GuestNo
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.GuestNo);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.GuestNo, value);
            }
        }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public string GuestName
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.GuestName);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.GuestName, value);
            }
        }
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStrByKey(MyDeptFlowAttr.RDT);
			}
			set
			{
				SetValByKey(MyDeptFlowAttr.RDT,value);
			}
		}
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public string SDTOfNode
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.SDTOfNode);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.SDTOfNode, value);
            }
        }
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public string SDTOfFlow
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.SDTOfFlow);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.SDTOfFlow, value);
            }
        }
		/// <summary>
		/// ����ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
                return this.GetValInt64ByKey(MyDeptFlowAttr.WorkID);
			}
			set
			{
				SetValByKey(MyDeptFlowAttr.WorkID,value);
			}
		}
        /// <summary>
        /// ���߳�ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(MyDeptFlowAttr.FID);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.FID, value);
            }
        }
        /// <summary>
        /// ���ڵ�ID Ϊ����-1.
        /// </summary>
        public Int64 CWorkID
        {
            get
            {
                return this.GetValInt64ByKey(MyDeptFlowAttr.CWorkID);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.CWorkID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string CFlowNo
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.CFlowNo);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.CFlowNo, value);
            }
        }
        /// <summary>
        /// ���ڵ����̱��.
        /// </summary>
        public Int64 PWorkID
        {
            get
            {
                return this.GetValInt64ByKey(MyDeptFlowAttr.PWorkID);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.PWorkID, value);
            }
        }
        /// <summary>
        /// �����̵��õĽڵ�
        /// </summary>
        public int PNodeID
        {
            get
            {
                return this.GetValIntByKey(MyDeptFlowAttr.PNodeID);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.PNodeID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string PFlowNo
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.PFlowNo);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.PFlowNo, value);
            }
        }
        /// <summary>
        /// ���������̵���Ա
        /// </summary>
        public string PEmp
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.PEmp);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.PEmp, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Starter
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.Starter);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.Starter, value);
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string StarterName
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.StarterName);
            }
            set
            {
                this.SetValByKey(MyDeptFlowAttr.StarterName, value);
            }
        }
        /// <summary>
        /// �����˲�������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.DeptName);
            }
            set
            {
                this.SetValByKey(MyDeptFlowAttr.DeptName, value);
            }
        }
        /// <summary>
        /// ��ǰ�ڵ�����
        /// </summary>
        public string NodeName
        {
            get
            {
                return this.GetValStrByKey(MyDeptFlowAttr.NodeName);
            }
            set
            {
                this.SetValByKey(MyDeptFlowAttr.NodeName, value);
            }
        }
		/// <summary>
		/// ��ǰ�������Ľڵ�
		/// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(MyDeptFlowAttr.FK_Node);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.FK_Node, value);
            }
        }
        /// <summary>
		/// ��������״̬
		/// </summary>
        public WFState WFState
        {
            get
            {
                return (WFState)this.GetValIntByKey(MyDeptFlowAttr.WFState);
            }
            set
            {
                if (value == WF.WFState.Complete)
                    SetValByKey(MyDeptFlowAttr.WFSta, (int)WFSta.Complete);
                else if (value == WF.WFState.Delete)
                    SetValByKey(MyDeptFlowAttr.WFSta, (int)WFSta.Delete);
                else
                    SetValByKey(MyDeptFlowAttr.WFSta, (int)WFSta.Runing);

                SetValByKey(MyDeptFlowAttr.WFState, (int)value);
            }
        }
        /// <summary>
        /// ״̬(��)
        /// </summary>
        public WFSta WFSta
        {
            get
            {
                return (WFSta)this.GetValIntByKey(MyDeptFlowAttr.WFSta);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.WFSta, (int)value);
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
                return this.GetValStrByKey(MyDeptFlowAttr.GUID);
            }
            set
            {
                SetValByKey(MyDeptFlowAttr.GUID, value);
            }
        }
		#endregion

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
        #endregion ��������.

        #region ���캯��
        /// <summary>
		/// �����Ĺ�������
		/// </summary>
		public MyDeptFlow()
		{
		}
        public MyDeptFlow(Int64 workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MyDeptFlowAttr.WorkID, workId);
            if (qo.DoQuery() == 0)
                throw new Exception("���� MyDeptFlow [" + workId + "]�����ڡ�");
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
                map.EnDesc = "�Ҳ��ŵ�����";
                map.EnType = EnType.View;

                map.AddTBIntPK(MyDeptFlowAttr.WorkID, 0, "WorkID", true, true);
                map.AddTBInt(MyDeptFlowAttr.FID, 0, "FID", false, false);

                map.AddDDLEntities(MyDeptFlowAttr.FK_Flow, null, "����", new Flows(), false);
                map.AddTBString(MyDeptFlowAttr.StarterName, null, "������", true, false, 0, 30, 10);
                map.AddTBString(MyDeptFlowAttr.Title, null, "����", true, false, 0, 100, 10);
                map.AddDDLSysEnum(MyDeptFlowAttr.WFSta, 0, "״̬", true, false, MyDeptFlowAttr.WFSta);
                map.AddTBString(MyDeptFlowAttr.NodeName, null, "��ǰ�ڵ�", true, false, 0, 100, 10);
                map.AddTBString(MyDeptFlowAttr.TodoEmps, null, "��ǰ������", true, false, 0, 100, 10);
                map.AddTBDateTime(MyDeptFlowAttr.RDT, "��������", true, true);
                map.AddTBString(MyDeptFlowAttr.FlowNote, null, "��ע", true, false, 0, 4000, 10);
                map.AddTBString(MyDeptFlowAttr.FK_Dept, null, "����", false, false, 0, 30, 10);
                map.AddTBMyNum();

                map.AddSearchAttr(MyDeptFlowAttr.FK_Flow);
                map.AddSearchAttr(MyDeptFlowAttr.WFSta);

                //�������صĲ�ѯ����.
                AttrOfSearch search = new AttrOfSearch(MyDeptFlowAttr.FK_Dept, "����",
                    MyDeptFlowAttr.FK_Dept, "=", BP.Web.WebUser.FK_Dept, 0, true);

                map.AttrsOfSearch.Add(search);

                RefMethod rm = new RefMethod();
                rm.Title = "���̹켣";  
                rm.ClassMethodName = this.ToString() + ".DoTrack";
                rm.Icon = "/WF/Img/FileType/doc.gif";
                map.AddRefMethod(rm);
              
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

		#region ִ�����
        public string DoTrack()
        {
            PubClass.WinOpen("/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID="+this.FID+"&FK_Flow="+this.FK_Flow,900,800);
            return null;
        }
		#endregion
	}
	/// <summary>
    /// �Ҳ��ŵ�����s
	/// </summary>
	public class MyDeptFlows : Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new MyDeptFlow();
			}
		}
		/// <summary>
		/// �Ҳ��ŵ����̼���
		/// </summary>
		public MyDeptFlows(){}
		#endregion
	}
	
}
