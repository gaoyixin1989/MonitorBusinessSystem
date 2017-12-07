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
    /// �Ҳ��������
	/// </summary>
    public class MyFlowAttr
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
        /// ʱ���
        /// </summary>
        public const string TSpan = "TSpan";
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
    /// �Ҳ��������
	/// </summary>
	public class MyFlow : Entity
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
                return MyFlowAttr.WorkID;
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string FlowNote
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.FlowNote);
            }
            set
            {
                SetValByKey(MyFlowAttr.FlowNote, value);
            }
        }
		/// <summary>
		/// �������̱��
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStrByKey(MyFlowAttr.FK_Flow);
			}
			set
			{
				SetValByKey(MyFlowAttr.FK_Flow,value);
			}
		}
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.BillNo);
            }
            set
            {
                SetValByKey(MyFlowAttr.BillNo, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.FlowName);
            }
            set
            {
                SetValByKey(MyFlowAttr.FlowName, value);
            }
        }
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int PRI
        {
            get
            {
                return this.GetValIntByKey(MyFlowAttr.PRI);
            }
            set
            {
                SetValByKey(MyFlowAttr.PRI, value);
            }
        }
        /// <summary>
        /// ������Ա����
        /// </summary>
        public int TodoEmpsNum
        {
            get
            {
                return this.GetValIntByKey(MyFlowAttr.TodoEmpsNum);
            }
            set
            {
                SetValByKey(MyFlowAttr.TodoEmpsNum, value);
            }
        }
        /// <summary>
        /// ������Ա�б�
        /// </summary>
        public string TodoEmps
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.TodoEmps);
            }
            set
            {
                SetValByKey(MyFlowAttr.TodoEmps, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.Emps);
            }
            set
            {
                SetValByKey(MyFlowAttr.Emps, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public TaskSta TaskSta
        {
            get
            {
                return (TaskSta)this.GetValIntByKey(MyFlowAttr.TaskSta);
            }
            set
            {
                SetValByKey(MyFlowAttr.TaskSta, (int)value);
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(MyFlowAttr.FK_FlowSort, value);
            }
        }
        /// <summary>
        /// ���ű��
        /// </summary>
		public string  FK_Dept
		{
			get
			{
				return this.GetValStrByKey(MyFlowAttr.FK_Dept);
			}
			set
			{
				SetValByKey(MyFlowAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStrByKey(MyFlowAttr.Title);
			}
			set
			{
				SetValByKey(MyFlowAttr.Title,value);
			}
		}
        /// <summary>
        /// �ͻ����
        /// </summary>
        public string GuestNo
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.GuestNo);
            }
            set
            {
                SetValByKey(MyFlowAttr.GuestNo, value);
            }
        }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public string GuestName
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.GuestName);
            }
            set
            {
                SetValByKey(MyFlowAttr.GuestName, value);
            }
        }
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStrByKey(MyFlowAttr.RDT);
			}
			set
			{
				SetValByKey(MyFlowAttr.RDT,value);
			}
		}
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public string SDTOfNode
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.SDTOfNode);
            }
            set
            {
                SetValByKey(MyFlowAttr.SDTOfNode, value);
            }
        }
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public string SDTOfFlow
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.SDTOfFlow);
            }
            set
            {
                SetValByKey(MyFlowAttr.SDTOfFlow, value);
            }
        }
		/// <summary>
		/// ����ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
                return this.GetValInt64ByKey(MyFlowAttr.WorkID);
			}
			set
			{
				SetValByKey(MyFlowAttr.WorkID,value);
			}
		}
        /// <summary>
        /// ���߳�ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(MyFlowAttr.FID);
            }
            set
            {
                SetValByKey(MyFlowAttr.FID, value);
            }
        }
        /// <summary>
        /// ���ڵ�ID Ϊ����-1.
        /// </summary>
        public Int64 CWorkID
        {
            get
            {
                return this.GetValInt64ByKey(MyFlowAttr.CWorkID);
            }
            set
            {
                SetValByKey(MyFlowAttr.CWorkID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string CFlowNo
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.CFlowNo);
            }
            set
            {
                SetValByKey(MyFlowAttr.CFlowNo, value);
            }
        }
        /// <summary>
        /// ���ڵ����̱��.
        /// </summary>
        public Int64 PWorkID
        {
            get
            {
                return this.GetValInt64ByKey(MyFlowAttr.PWorkID);
            }
            set
            {
                SetValByKey(MyFlowAttr.PWorkID, value);
            }
        }
        /// <summary>
        /// �����̵��õĽڵ�
        /// </summary>
        public int PNodeID
        {
            get
            {
                return this.GetValIntByKey(MyFlowAttr.PNodeID);
            }
            set
            {
                SetValByKey(MyFlowAttr.PNodeID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string PFlowNo
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.PFlowNo);
            }
            set
            {
                SetValByKey(MyFlowAttr.PFlowNo, value);
            }
        }
        /// <summary>
        /// ���������̵���Ա
        /// </summary>
        public string PEmp
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.PEmp);
            }
            set
            {
                SetValByKey(MyFlowAttr.PEmp, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Starter
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.Starter);
            }
            set
            {
                SetValByKey(MyFlowAttr.Starter, value);
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string StarterName
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.StarterName);
            }
            set
            {
                this.SetValByKey(MyFlowAttr.StarterName, value);
            }
        }
        /// <summary>
        /// �����˲�������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.DeptName);
            }
            set
            {
                this.SetValByKey(MyFlowAttr.DeptName, value);
            }
        }
        /// <summary>
        /// ��ǰ�ڵ�����
        /// </summary>
        public string NodeName
        {
            get
            {
                return this.GetValStrByKey(MyFlowAttr.NodeName);
            }
            set
            {
                this.SetValByKey(MyFlowAttr.NodeName, value);
            }
        }
		/// <summary>
		/// ��ǰ�������Ľڵ�
		/// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(MyFlowAttr.FK_Node);
            }
            set
            {
                SetValByKey(MyFlowAttr.FK_Node, value);
            }
        }
        /// <summary>
		/// ��������״̬
		/// </summary>
        public WFState WFState
        {
            get
            {
                return (WFState)this.GetValIntByKey(MyFlowAttr.WFState);
            }
            set
            {
                if (value == WF.WFState.Complete)
                    SetValByKey(MyFlowAttr.WFSta, (int)WFSta.Complete);
                else if (value == WF.WFState.Delete)
                    SetValByKey(MyFlowAttr.WFSta, (int)WFSta.Delete);
                else
                    SetValByKey(MyFlowAttr.WFSta, (int)WFSta.Runing);

                SetValByKey(MyFlowAttr.WFState, (int)value);
            }
        }
        /// <summary>
        /// ״̬(��)
        /// </summary>
        public WFSta WFSta
        {
            get
            {
                return (WFSta)this.GetValIntByKey(MyFlowAttr.WFSta);
            }
            set
            {
                SetValByKey(MyFlowAttr.WFSta, (int)value);
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
                return this.GetValStrByKey(MyFlowAttr.GUID);
            }
            set
            {
                SetValByKey(MyFlowAttr.GUID, value);
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
		public MyFlow()
		{
		}
        public MyFlow(Int64 workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MyFlowAttr.WorkID, workId);
            if (qo.DoQuery() == 0)
                throw new Exception("���� MyFlow [" + workId + "]�����ڡ�");
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
                map.EnDesc = "�Ҳ��������";
                map.EnType = EnType.View;
                map.AddTBIntPK(MyFlowAttr.WorkID, 0, "WorkID", false, false);
                map.AddTBInt(MyFlowAttr.FID, 0, "FID", false, false);

                map.AddTBString(MyFlowAttr.Title, null, "���̱���", true, false, 0, 100, 10, true);
                map.AddDDLEntities(MyFlowAttr.FK_Flow, null, "��������", new Flows(), false);
                
                //map.AddDDLEntities(MyFlowAttr.FK_Dept, null, "�����˲���", new BP.Port.Depts(), false);
                //map.AddTBString(MyFlowAttr.Starter, null, "�����˱��", true, false, 0, 30, 10);
                //map.AddTBString(MyFlowAttr.StarterName, null, "����������", true, false, 0, 30, 10);
                //map.AddTBString(MyFlowAttr.BillNo, null, "���ݱ��", true, false, 0, 100, 10);

                map.AddTBDateTime(MyFlowAttr.RDT, "��������", true, true);
                map.AddDDLSysEnum(MyFlowAttr.WFSta, 0, "״̬", true, false, MyFlowAttr.WFSta, "@0=������@1=�����@2=����");

                map.AddDDLSysEnum(MyFlowAttr.TSpan, 0, "ʱ���", true, false, MyFlowAttr.TSpan, "@0=����@1=����@2=������ǰ@3=������ǰ@4=����");
                map.AddTBString(MyFlowAttr.NodeName, null, "��ǰ�ڵ�", true, false, 0, 100, 10,true);
                map.AddTBString(MyStartFlowAttr.TodoEmps, null, "��ǰ������", true, false, 0, 100, 10,true);

                map.AddTBString(MyFlowAttr.Emps, null, "������", true, false, 0, 4000, 10, true);
                map.AddTBStringDoc(MyFlowAttr.FlowNote, null, "��ע", true, false,true);


                map.AddTBMyNum();

                map.AddSearchAttr(MyFlowAttr.FK_Flow);
                map.AddSearchAttr(MyFlowAttr.WFSta);
                map.AddSearchAttr(MyFlowAttr.TSpan);


                //�������صĲ�ѯ����.
                AttrOfSearch search = new AttrOfSearch(MyFlowAttr.Emps, "��Ա",
                    MyFlowAttr.Emps, " LIKE ", "%"+BP.Web.WebUser.No+"%" , 0, true);
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
    /// �Ҳ��������s
	/// </summary>
	public class MyFlows : Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new MyFlow();
			}
		}
		/// <summary>
		/// �Ҳ�������̼���
		/// </summary>
		public MyFlows(){}
		#endregion
	}
	
}
