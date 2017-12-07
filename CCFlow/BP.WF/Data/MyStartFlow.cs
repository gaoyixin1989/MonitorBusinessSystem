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
    /// �ҷ��������
	/// </summary>
    public class MyStartFlowAttr
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
        /// TSpan
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
    /// �ҷ��������
	/// </summary>
	public class MyStartFlow : Entity
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
                return MyStartFlowAttr.WorkID;
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string FlowNote
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.FlowNote);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.FlowNote, value);
            }
        }
		/// <summary>
		/// �������̱��
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStrByKey(MyStartFlowAttr.FK_Flow);
			}
			set
			{
				SetValByKey(MyStartFlowAttr.FK_Flow,value);
			}
		}
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.BillNo);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.BillNo, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.FlowName);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.FlowName, value);
            }
        }
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int PRI
        {
            get
            {
                return this.GetValIntByKey(MyStartFlowAttr.PRI);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.PRI, value);
            }
        }
        /// <summary>
        /// ������Ա����
        /// </summary>
        public int TodoEmpsNum
        {
            get
            {
                return this.GetValIntByKey(MyStartFlowAttr.TodoEmpsNum);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.TodoEmpsNum, value);
            }
        }
        /// <summary>
        /// ������Ա�б�
        /// </summary>
        public string TodoEmps
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.TodoEmps);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.TodoEmps, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.Emps);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.Emps, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public TaskSta TaskSta
        {
            get
            {
                return (TaskSta)this.GetValIntByKey(MyStartFlowAttr.TaskSta);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.TaskSta, (int)value);
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.FK_FlowSort, value);
            }
        }
        /// <summary>
        /// ���ű��
        /// </summary>
		public string  FK_Dept
		{
			get
			{
				return this.GetValStrByKey(MyStartFlowAttr.FK_Dept);
			}
			set
			{
				SetValByKey(MyStartFlowAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStrByKey(MyStartFlowAttr.Title);
			}
			set
			{
				SetValByKey(MyStartFlowAttr.Title,value);
			}
		}
        /// <summary>
        /// �ͻ����
        /// </summary>
        public string GuestNo
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.GuestNo);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.GuestNo, value);
            }
        }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public string GuestName
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.GuestName);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.GuestName, value);
            }
        }
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStrByKey(MyStartFlowAttr.RDT);
			}
			set
			{
				SetValByKey(MyStartFlowAttr.RDT,value);
			}
		}
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public string SDTOfNode
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.SDTOfNode);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.SDTOfNode, value);
            }
        }
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public string SDTOfFlow
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.SDTOfFlow);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.SDTOfFlow, value);
            }
        }
		/// <summary>
		/// ����ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
                return this.GetValInt64ByKey(MyStartFlowAttr.WorkID);
			}
			set
			{
				SetValByKey(MyStartFlowAttr.WorkID,value);
			}
		}
        /// <summary>
        /// ���߳�ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(MyStartFlowAttr.FID);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.FID, value);
            }
        }
        /// <summary>
        /// ���ڵ�ID Ϊ����-1.
        /// </summary>
        public Int64 CWorkID
        {
            get
            {
                return this.GetValInt64ByKey(MyStartFlowAttr.CWorkID);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.CWorkID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string CFlowNo
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.CFlowNo);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.CFlowNo, value);
            }
        }
        /// <summary>
        /// ���ڵ����̱��.
        /// </summary>
        public Int64 PWorkID
        {
            get
            {
                return this.GetValInt64ByKey(MyStartFlowAttr.PWorkID);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.PWorkID, value);
            }
        }
        /// <summary>
        /// �����̵��õĽڵ�
        /// </summary>
        public int PNodeID
        {
            get
            {
                return this.GetValIntByKey(MyStartFlowAttr.PNodeID);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.PNodeID, value);
            }
        }
        /// <summary>
        /// PFlowNo
        /// </summary>
        public string PFlowNo
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.PFlowNo);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.PFlowNo, value);
            }
        }
        /// <summary>
        /// ���������̵���Ա
        /// </summary>
        public string PEmp
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.PEmp);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.PEmp, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Starter
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.Starter);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.Starter, value);
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string StarterName
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.StarterName);
            }
            set
            {
                this.SetValByKey(MyStartFlowAttr.StarterName, value);
            }
        }
        /// <summary>
        /// �����˲�������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.DeptName);
            }
            set
            {
                this.SetValByKey(MyStartFlowAttr.DeptName, value);
            }
        }
        /// <summary>
        /// ��ǰ�ڵ�����
        /// </summary>
        public string NodeName
        {
            get
            {
                return this.GetValStrByKey(MyStartFlowAttr.NodeName);
            }
            set
            {
                this.SetValByKey(MyStartFlowAttr.NodeName, value);
            }
        }
		/// <summary>
		/// ��ǰ�������Ľڵ�
		/// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(MyStartFlowAttr.FK_Node);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.FK_Node, value);
            }
        }
        /// <summary>
		/// ��������״̬
		/// </summary>
        public WFState WFState
        {
            get
            {
                return (WFState)this.GetValIntByKey(MyStartFlowAttr.WFState);
            }
            set
            {
                if (value == WF.WFState.Complete)
                    SetValByKey(MyStartFlowAttr.WFSta, (int)WFSta.Complete);
                else if (value == WF.WFState.Delete)
                    SetValByKey(MyStartFlowAttr.WFSta, (int)WFSta.Delete);
                else
                    SetValByKey(MyStartFlowAttr.WFSta, (int)WFSta.Runing);

                SetValByKey(MyStartFlowAttr.WFState, (int)value);
            }
        }
        /// <summary>
        /// ״̬(��)
        /// </summary>
        public WFSta WFSta
        {
            get
            {
                return (WFSta)this.GetValIntByKey(MyStartFlowAttr.WFSta);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.WFSta, (int)value);
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
                return this.GetValStrByKey(MyStartFlowAttr.GUID);
            }
            set
            {
                SetValByKey(MyStartFlowAttr.GUID, value);
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
		public MyStartFlow()
		{
		}
        public MyStartFlow(Int64 workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MyStartFlowAttr.WorkID, workId);
            if (qo.DoQuery() == 0)
                throw new Exception("���� MyStartFlow [" + workId + "]�����ڡ�");
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
                map.EnDesc = "�ҷ��������";
                map.EnType = EnType.View;

                map.AddTBInt(MyStartFlowAttr.FID, 0, "FID", false, false);

                map.AddDDLEntities(MyStartFlowAttr.FK_Flow, null, "����", new Flows(), false);
                map.AddTBString(MyStartFlowAttr.BillNo, null, "���ݱ��", true, false, 0, 100, 10);

                map.AddTBString(MyStartFlowAttr.Title, null, "����", true, false, 0, 100, 10,true);
              
                map.AddDDLSysEnum(MyStartFlowAttr.WFSta, 0, "״̬", true, false, MyStartFlowAttr.WFSta, "@0=������@1=�����@2=����");

                map.AddTBString(MyStartFlowAttr.NodeName, null, "ͣ���ڵ�", true, false, 0, 100, 10,true);
                map.AddTBString(MyStartFlowAttr.TodoEmps, null, "��ǰ������", true, false, 0, 100, 10,true);
                map.AddTBString(MyFlowAttr.Emps, null, "������", true, false, 0, 4000, 10, true);
                map.AddTBStringDoc(MyFlowAttr.FlowNote, null, "��ע", true, false, true);

                map.AddTBDateTime(MyStartFlowAttr.RDT, "��������", true, true);
                map.AddDDLSysEnum(MyFlowAttr.TSpan, 0, "ʱ���", true, false, MyFlowAttr.TSpan, "@0=����@1=����@2=������ǰ@3=������ǰ@4=����");


                map.AddTBIntPK(MyStartFlowAttr.WorkID, 0, "WorkID", true, false);
                map.AddTBString(MyStartFlowAttr.Starter, null, "������", false, false, 0, 100, 10);
                map.AddTBMyNum();

                map.AddSearchAttr(MyStartFlowAttr.FK_Flow);
                map.AddSearchAttr(MyStartFlowAttr.WFSta);
                map.AddSearchAttr(MyStartFlowAttr.TSpan);


                AttrOfSearch search = new AttrOfSearch(MyStartFlowAttr.Starter, "������",
                    MyStartFlowAttr.Starter,"=", BP.Web.WebUser.No,0,true);
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
    /// �ҷ��������s
	/// </summary>
	public class MyStartFlows : Entities
	{
		/// <summary>
		/// ���ݹ�������,������Ա ID ��ѯ��������ǰ�������Ĺ���.
		/// </summary>
		/// <param name="flowNo">���̱��</param>
		/// <param name="empId">������ԱID</param>
		/// <returns></returns>
		public static DataTable QuByFlowAndEmp(string flowNo, int empId)
		{
			string sql="SELECT a.WorkID FROM WF_MyStartFlow a, WF_GenerWorkerlist b WHERE a.WorkID=b.WorkID   AND b.FK_Node=a.FK_Node  AND b.FK_Emp='"+empId.ToString()+"' AND a.FK_Flow='"+flowNo+"'";
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
				return new MyStartFlow();
			}
		}
		/// <summary>
		/// �ҷ�������̼���
		/// </summary>
		public MyStartFlows(){}
		#endregion
	}
	
}
